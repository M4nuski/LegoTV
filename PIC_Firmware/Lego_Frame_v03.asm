	LIST		p=16F88			;Processor
	#INCLUDE	<p16F88.inc>	;Processor Specific Registers
	#INCLUDE	<PIC16F88_Macro.asm>	;Bank switching, 16bit methods , wrapped jumps 

	__CONFIG	_CONFIG1, _CP_OFF & _CCP1_RB0 & _DEBUG_OFF & _WRT_PROTECT_OFF & _CPD_OFF & _LVP_OFF & _BODEN_OFF & _MCLR_OFF & _PWRTE_ON & _WDT_OFF & _INTRC_IO
	__CONFIG	_CONFIG2, _IESO_OFF & _FCMEN_ON

; Lego Frame MCU
; Version 03
; Production firmware v1.0
; Data Source: Flash W25Q128FV (Winbond) 128mbit / 16mbyte 3V SPI
; Display: ST7735 1.8in 160x128 TFT in portrait mode
; Internal 8MHz OSC 2MOPS 0.5us per ops

; SPI 0 Config	- TFT
#define _SPI0_CS	PORTA, 3
#define _SPI0_CLK	PORTA, 2
#define _SPI0_DTA	PORTA, 4
#define _SPI0_A0	PORTA, 6 ; register select / A0 command:0, data:1
#define _SPI0_RST	PORTA, 0

; SPI 1 Config - Flash
#define _SPI1_CLK	PORTB, 4
#define _SPI1_DTAO	PORTB, 5
#define _SPI1_DTAI	PORTB, 2
#define _SPI1_CS	PORTB, 3

; Human Interface
#define _SW_Next	PORTA, 7 ; default 1 active 0 
#define _SW_Prev	PORTA, 1 ; default 0 active 1

_delay	EQU 0x2A ;about 5sec delay between images ( 42 * 120ms )

; ST7735 TFT Controller command constants
_TFT_SWRESET EQU 0x01;
_TFT_SLPOUT  EQU 0x11;
_TFT_NORON 	 EQU 0x13;
_TFT_DISPON  EQU 0x29;
_TFT_CASET 	 EQU 0x2A;
_TFT_RASET 	 EQU 0x2B;
_TFT_RAMWR 	 EQU 0x2C;
_TFT_MADCTL  EQU 0x36;
_TFT_COLMOD  EQU 0x3A;


; W25Q128FV Flash memory command constants
_W25_read  		EQU 0x03
_W25_read_SSR	EQU 0x05;
_W25_read_JEDEC	EQU 0x9F;


; W25Q128FV MFR and ID
_W25_MFR	EQU 0xEF
_W25_ID0	EQU 0x40
_W25_ID1	EQU 0x18

;GPR
	cblock 0x020
		init_status
		d1, d2, d3		; loops counters

		SPI_Buffer	; Output buffer for SPI 0 or 1
		ClearColor:3; r g b

		SPI1_Bytes_to_Read:2

		; config struct from flash
		ColMod  ; 444:03h 666:06h 
		MadCtl
		NumImages
		BytesPerImage:2 ; 444:7800h  666: F000h F000 444 A000

		; state
		CurrentImage
		CurrentAddress:2 ; actually 3 bytes but byte 0 is always 0 
		BytesPerSector:2 ; 64k :   0x 01 00 00  32K 0x00 80 00
		SSR
		ID:3
	endc

	
	ORG	0x0000

; setup MCU

	BANK0
	BCF	INTCON, GIE	; clear global interrupts	
	CLRF init_status

	BANK1
	BSF	OSCCON, IRCF0	; internal OSC setup
	BSF	OSCCON, IRCF1	; 8mhz
	BSF	OSCCON, IRCF2	

	CLRF ANSEL			; all digital IO

	; SPI
	BCF _SPI0_CS
	BCF _SPI0_CLK
	BCF _SPI0_DTA
	BCF _SPI0_RST
	BCF _SPI0_A0	

	BCF _SPI1_CLK
	BCF _SPI1_DTAO
	BSF _SPI1_DTAI
	BCF _SPI1_CS

	; HID
	BSF _SW_Prev
	BSF _SW_Next

	BANK0		
	; default state SPI mode 3
	BSF _SPI0_CS
	BSF _SPI0_CLK
	BCF _SPI0_RST

	BSF _SPI1_CS
	BSF _SPI1_CLK

;setup TFT

	; reset TFT	
	call d50us	;actual minimum is 10us or 20 ops
	BSF _SPI0_RST
	call d50us	
	movlw _TFT_SWRESET
	call SPI0_Send_CMD
	call d120ms

	; init TFT
	movlw _TFT_SLPOUT
	call SPI0_Send_CMD

	movlw _TFT_NORON
	call SPI0_Send_CMD	

	;set defaults
 	movlw _TFT_COLMOD
	call SPI0_Send_CMD	  
 	movlw 0x06			;default RGB666
	call SPI0_Send_DTA	  

	movlw _TFT_MADCTL
	call SPI0_Send_CMD
	movlw 0xA0			;default MY1 MX0 MV1 ML0 RGB MH0
	call SPI0_Send_DTA 

	movlw _TFT_CASET
	call SPI0_Send_CMD
	CLRW				; offset h 0x00
	call SPI0_Send_DTA
	CLRW				; offset l 0x00
	call SPI0_Send_DTA
	CLRW				; width h 0x00
	call SPI0_Send_DTA 
	movlw 0x9F			; width l 0x9F = 160 pixels wide
	call SPI0_Send_DTA

	movlw _TFT_RASET
	call SPI0_Send_CMD	
	CLRW				; offset h 0x00
	call SPI0_Send_DTA
	CLRW				; offset l 0x00
	call SPI0_Send_DTA
	CLRW				; height h 0x00
	call SPI0_Send_DTA
	movlw 0x7F			; height ; 0x7F = 128 pixels high
	call SPI0_Send_DTA

	; clear RAM
	CLRF ClearColor
	CLRF ClearColor+1
	CLRF ClearColor+2	
	call SPI0_Fill

	; turn display on
	movlw _TFT_DISPON
	call SPI0_Send_CMD	

	; reset position
	movlw _TFT_RAMWR
	call SPI0_Send_CMD		

	; all green so far!
	call Line_GREEN	

;setup Flash

	;read FLASH DATA
	MOVLW 0x01
	MOVWF SPI1_Bytes_to_Read
	MOVLW SSR
	MOVWF FSR
	MOVLW _W25_Read_SSR
	call SPI1_CMD	

	BTFSS SSR, 0 ;check busy
	goto mem1b
	call Line_RED
	BSF init_status, 0
	goto mem2
mem1b 
	call Line_GREEN

	
mem2	
	MOVLW 0x03
	MOVWF SPI1_Bytes_to_Read
	MOVLW ID
	MOVWF FSR
	MOVLW _W25_read_JEDEC
	call SPI1_CMD

	BR_LF_EQ _W25_MFR, ID, mem2b
	call Line_RED
	BSF init_status, 1
	goto mem3	
	
mem2b
	CALL line_GREEN

mem3	
	BR_LF_EQ _W25_ID0, ID+1, mem3b
	call Line_RED
	BSF init_status, 2
	goto mem4	
	
mem3b
	CALL line_GREEN

mem4	
	BR_LF_EQ _W25_ID1, ID+2, mem4b
	call Line_RED
	BSF init_status, 3
	goto mem5	
	
mem4b
	CALL line_GREEN

mem5
	;display state bytes
	MOVLW SSR
	MOVWF FSR 
	call disp_byte
	
	MOVLW ID
	MOVWF FSR 
	call disp_byte
	INCF FSR, F 
	call disp_byte	
	INCF FSR, F 
	call disp_byte	
	
	;read sector 0 struct of ram
	CLRF CurrentAddress	;Address 00 00 00
	CLRF CurrentAddress+1
	MOVLW 0x05
	MOVWF SPI1_Bytes_to_Read
	MOVLW ColMod
	MOVWF FSR
	call SPI1_Read		
	
	
;display content of sector 0 struct
	MOVLW ColMod
	MOVWF FSR 
	call disp_byte	;colmod
	INCF FSR, F 
	call disp_byte	;madctl
	INCF FSR, F 
	call disp_byte	;numImage
	
	INCF FSR, F 
	call disp_byte	;bytes per image 0
	INCF FSR, F 
	call disp_byte	;bytes per image 1
	
	MOVF numImages, F ;test content of numImages for 0
	BTFSS STATUS, Z
	goto mem5b
	call line_RED
	BSF init_status, 4
	goto mem6
mem5b	
	CALL line_GREEN
mem6	
	

	MOVF init_status, F
	BTFSC STATUS, Z
	GOTO mem7 
	CALL line_RED
	MOVLW init_status
	MOVWF FSR 
	CALL disp_byte
	
	;stall display if there was errors during setup
init_stall
	GOTO init_stall
	
	;all green
mem7	
	CALL line_GREEN
	
;setup display per sector 0 data
	movlw _TFT_COLMOD
	call SPI0_Send_CMD	  
 	movf ColMod, W
	call SPI0_Send_DTA	  
	
	movlw _TFT_MADCTL
	call SPI0_Send_CMD
	movf madctl, W	
	call SPI0_Send_DTA 	
	
	; prep for 64KB sectors 
	; 64k :   0x 01 00 00  32K 0x00 80 00
	MOVLW 0x00
	MOVWF BytesPerSector ;L
	MOVLW 0x01
	MOVWF BytesPerSector+1	;H
	
	; check colmod
	BR_LF_NE 0x03, ColMod, main
	
	;sector size for 32K / colmod == 0x03
	MOVLW 0x80
	MOVWF BytesPerSector ;L
	MOVLW 0x00
	MOVWF BytesPerSector+1	;H	
	
	
main	
	; dev stall if keypress
	BTFSS _SW_Next ; default 1 active 0 
	GOTO main

	; start image
	MOVLW 0x01
	MOVWF CurrentImage
		
	; start address
	MOV_short BytesPerSector, CurrentAddress
	
	;send first image
	CALL SEND_IMG

	MOVLW _delay
	MOVWF D3	
tloop	
	CALL d120ms	

	BTFSS _SW_Next ; default 1 active 0 
	CALL INC_IMG
	BTFSC _SW_Prev ; default 0 active 1
	CALL DEC_IMG
	
	DECFSZ D3, F
	GOTO tloop
	
	CALL INC_IMG
	GOTO tloop
	
e	GOTO e ;end of program stall
	



inc_img
	BR_FF_EQ CurrentImage, numImages, inc_img_rst
	INCF CurrentImage, F
	ADD_short BytesPerSector, CurrentAddress	
	call SEND_IMG
	return	
inc_img_rst	
	MOVLW 0x01
	MOVWF CurrentImage
	MOV_short BytesPerSector, CurrentAddress
	call SEND_IMG
	return
	
	
	
	
dec_img
	DECF CurrentImage, W
	BTFSC STATUS, Z ;if currentimage is already 1, DECF put 0 in W, C is Set
	return	;return immediatly
	;otherwise
	DECF CurrentImage, F; actually decrease CurrentImage
	SUB_short BytesPerSector, CurrentAddress
	call SEND_IMG	
	return


send_img 	;shadow send 3 bytes * BytesPerImage

	;reset display position and mode
	movlw _TFT_RAMWR
	call SPI0_Send_CMD	
	
	BSF _SPI0_A0 ;data
	
	;init hardware
	BANK1
	BSF _SPI0_DTA 	; set DTA to input to avoid inhibiting transfer 
	BANK0
	BCF _SPI0_CS
	BCF _SPI1_CS
	call d50us
	;send read command
	MOVLW _W25_Read	
	CALL SPI1_TX_Byte
	
	MOVF CurrentAddress+1, W ; A2
	;MOVLW 0x01	
	CALL SPI1_TX_Byte
	
	MOVF CurrentAddress, W ; A1
	;MOVLW 0x00
	CALL SPI1_TX_Byte
	CLRW	;always 0		  ; A0
	CALL SPI1_TX_Byte
	
	;the next clk cycle will send the data to the TFT
		;init counters
	movf BytesPerImage, W
	MOVWF D1
	
	MOVF BytesPerImage+1, W
	MOVWF D2
	
sil		
	movlw 0x08
	movwf D3
silil	
	bcf _SPI1_CLK
	bcf _SPI0_CLK
	bsf _SPI0_CLK		
	bsf _SPI1_CLK	
	
	decfsz D3, F
	GOTO silil	; 8 bitperbyte loop
	
	;check loop
	DECFSZ D1, F
	GOTO sil 	; bytesperimage loop
	;reset D1 loop
	movf BytesPerImage, W
	MOVWF D1	
	
	DECFSZ D2, F
	GOTO sil	; bytesperimage+1 loop

	;reset hardware
	BSF _SPI0_CS
	BSF _SPI1_CS

	BANK1
	BCF _SPI0_DTA 	; set DTA to output 
	BANK0

	
	movlw _delay
	movwf D3 ;restore delay
	return	
	
	
	
	
	
	
SPI1_TX_BYTE ;write 1 byte to SPI1, taking byte in W, using SPI_Buffer
	MOVWF SPI_Buffer	
	
	;cute version
;	BSF STATUS, C	; ensure marker of at least 1 bit will be set in data buffer
	;RLF	SPI_Buffer, F ; put MSB of buffer in C and marker in buffer	;
;spi1_TX_loop		; "clk down-latch-clk up-hold" - loop
	;bcf _SPI1_CLK	
	;BCF _SPI1_DTAO	; pre-clear output pin
	;BTFSC STATUS, C ; check bit to send
	;BSF _SPI1_DTAO 	; if C is 1, set output pin	
	;BCF STATUS, C	; clear C
	;RLF SPI_Buffer, F	; put next bit in C, and a 0 in buffer	
	;bsf _SPI1_CLK	
	;MOVF SPI_Buffer, F; eval content of buffer in STATUS
	;BTFSS STATUS, Z ; leave loop if all bits have been replaced by 0 from C, and initial 1 flag got back into C
	;GOTO spi1_TX_loop	
	
	;ugly and fast version
	BCF _SPI1_CLK	;clk down
	BCF _SPI1_DTAO	;pre clear data bit
	BTFSC SPI_Buffer, 7 	;check actual data
	BSF _SPI1_DTAO 	; if nec set data bit
	BSF _SPI1_CLK	; clk up	
	BCF _SPI1_CLK	;clk down
	BCF _SPI1_DTAO	;pre clear data bit
	BTFSC SPI_Buffer, 6 	;check actual data
	BSF _SPI1_DTAO 	; if nec set data bit
	BSF _SPI1_CLK	; clk up	
	BCF _SPI1_CLK	;clk down
	BCF _SPI1_DTAO	;pre clear data bit
	BTFSC SPI_Buffer, 5 	;check actual data
	BSF _SPI1_DTAO 	; if nec set data bit
	BSF _SPI1_CLK	; clk up
	
	BCF _SPI1_CLK
	BCF _SPI1_DTAO
	BTFSC SPI_Buffer, 4
	BSF _SPI1_DTAO 
	BSF _SPI1_CLK	
	BCF _SPI1_CLK
	BCF _SPI1_DTAO
	BTFSC SPI_Buffer, 3 
	BSF _SPI1_DTAO
	BSF _SPI1_CLK		
	BCF _SPI1_CLK
	BCF _SPI1_DTAO
	BTFSC SPI_Buffer, 2 
	BSF _SPI1_DTAO
	BSF _SPI1_CLK
	BCF _SPI1_CLK
	BCF _SPI1_DTAO
	BTFSC SPI_Buffer, 1 
	BSF _SPI1_DTAO
	BSF _SPI1_CLK
	BCF _SPI1_CLK
	BCF _SPI1_DTAO
	BTFSC SPI_Buffer, 0 
	BSF _SPI1_DTAO
	BSF _SPI1_CLK	

	RETURN




SPI1_RX_BYTE ;read 1 byte from SPI1, writing it in INDF referenced by FSR, uses D1 loop
	CLRF INDF 		; clear destination
	
	;cute loopy version
	;movlw 0x08
	;movwf d1	
;spi1_RX_loop	
;	bcf _SPI1_CLK
;	BCF STATUS, C ; default to 0
;	bsf _SPI1_CLK	
;	;nop
;	BTFSC _SPI1_DTAI; Chech if data input is different than 0 already in C
;	BSF STATUS, C	; if not, set C
;	RLF INDF, F		; load C to INDF LSB
;	DECFSZ d1, F
;	GOTO spi1_RX_loop

	;ugly fast version	
	BCF _SPI1_CLK
	BSF _SPI1_CLK	
	BTFSC _SPI1_DTAI
	BSF INDF, 7	
	BCF _SPI1_CLK
	BSF _SPI1_CLK	
	BTFSC _SPI1_DTAI
	BSF INDF, 6	
	BCF _SPI1_CLK
	BSF _SPI1_CLK	
	BTFSC _SPI1_DTAI
	BSF INDF, 5		
	BCF _SPI1_CLK
	BSF _SPI1_CLK	
	BTFSC _SPI1_DTAI
	BSF INDF, 4	
	BCF _SPI1_CLK
	BSF _SPI1_CLK	
	BTFSC _SPI1_DTAI
	BSF INDF, 3	
	BCF _SPI1_CLK
	BSF _SPI1_CLK	
	BTFSC _SPI1_DTAI
	BSF INDF, 2	
	BCF _SPI1_CLK
	BSF _SPI1_CLK	
	BTFSC _SPI1_DTAI
	BSF INDF, 1		
	BCF _SPI1_CLK
	BSF _SPI1_CLK	
	BTFSC _SPI1_DTAI
	BSF INDF, 0	
	
	RETURN
	
	
	
SPI1_CMD ; send command and read result, command in W, result in INDF/FSR, uses D1Loop and SPI_Buffer
	BCF _SPI1_CS
	call SPI1_TX_BYTE

	MOVF SPI1_Bytes_to_Read, F ;test file content
	BTFSC STATUS, Z
	GOTO spI1_CMD_End 	;skip if nothing to read	
	
SPI1_CMD_RD	
	call SPI1_RX_BYTE
	
	INCF FSR, F ;next byte address
	DECFSZ SPI1_Bytes_to_Read, F
	GOTO SPI1_CMD_RD
		
spI1_CMD_End	
	BSF _SPI1_CS	
	call d50us
	RETURN	
	
	
	
	
	
	
SPI1_Read	; send read command and read at least 1 data, result in INDF/FSR, uses D1Loop and SPI_Buffer, address in currentAddress, bytes to read in sp1_bytes_to_read
	BCF _SPI1_CS
	call d50us
	MOVLW _W25_Read		; read command
	CALL SPI1_TX_Byte
	
	MOVF CurrentAddress+1, W ; A2
	CALL SPI1_TX_Byte
	MOVF CurrentAddress, W ; A1
	CALL SPI1_TX_Byte
	CLRW	;always 0		  ; A0
	CALL SPI1_TX_Byte

SPI1_RD_RD	
	CALL SPI1_RX_Byte	
	
	INCF FSR, F ;next byte address
	DECFSZ SPI1_Bytes_to_Read, F
	GOTO SPI1_RD_RD
		
	BSF _SPI1_CS		
	RETURN	
	

d50us ; delay 50 us: 6ops overhead (3us) + 3 (1.5us) per loop, 32 loops
	MOVLW 0x20
	MOVWF d1
d50l
	DECFSZ d1, f
	GOTO d50l
	RETURN
	
	
d120ms	; delay 120ms : header + d1loops * (d1 overhead + (d2loops * (d2 delay)))
	MOVLW 0x5E ; 94 * 1.28
	MOVWF d1
	CLRF d2
	;header+return : 7
d120l	;5us per d2 loop*256 = 1.28 ms per 256d2
	NOP
	NOP
	NOP
	NOP
	
	NOP
	NOP
	NOP
	DECFSZ d2, f
	GOTO d120l	;loop d2 : 10op / cycles, = 2560 op / d1
	DECFSZ d1, f
	GOTO d120l	;loop d1 overhead = 3 
	
	RETURN
	

	
SPI0_Send_CMD	; send command in W
	BCF _SPI0_A0
	GOTO SPI0_TX_BYTE
	
SPI0_Send_DTA	; send data in W
	BSF _SPI0_A0

SPI0_TX_BYTE	; very fast and ugly version
	MOVWF SPI_Buffer	; byte to send in internal buffer
	BCF _SPI0_CS	
	
	BCF _SPI0_CLK	;clk down
	BCF _SPI0_DTA	;pre clear data bit
	BTFSC SPI_Buffer, 7 	;check actual data
	BSF _SPI0_DTA 	; if nec set data bit
	BSF _SPI0_CLK	; clk up


	BCF _SPI0_CLK	
	BCF _SPI0_DTA
	BTFSC SPI_Buffer, 6
	BSF _SPI0_DTA 
	BSF _SPI0_CLK	


	BCF _SPI0_CLK	
	BCF _SPI0_DTA
	BTFSC SPI_Buffer, 5
	BSF _SPI0_DTA 
	BSF _SPI0_CLK	


	BCF _SPI0_CLK	
	BCF _SPI0_DTA
	BTFSC SPI_Buffer, 4
	BSF _SPI0_DTA 
	BSF _SPI0_CLK	

	BCF _SPI0_CLK	
	BCF _SPI0_DTA
	BTFSC SPI_Buffer, 3
	BSF _SPI0_DTA 
	BSF _SPI0_CLK	


	BCF _SPI0_CLK	
	BCF _SPI0_DTA
	BTFSC SPI_Buffer, 2
	BSF _SPI0_DTA 
	BSF _SPI0_CLK	


	BCF _SPI0_CLK	
	BCF _SPI0_DTA
	BTFSC SPI_Buffer, 1
	BSF _SPI0_DTA 
	BSF _SPI0_CLK	


	BCF _SPI0_CLK	
	BCF _SPI0_DTA
	BTFSC SPI_Buffer, 0
	BSF _SPI0_DTA 
	BSF _SPI0_CLK		
	NOP
	BSF _SPI0_CS		
	RETURN
	
	
	
SPI0_Fill	; send 160 * 128 * RGB(ClearColor)
	movlw _TFT_RAMWR
	call SPI0_Send_CMD	
	
	movlw 0xA0
	movwf d1
	movlw 0x80
	movwf d2

s0fl	;SPI 0 Fill Loop
	movf ClearColor, W	;R
	call SPI0_Send_DTA
	movf ClearColor+1, W;G
	call SPI0_Send_DTA
	movf ClearColor+2, W;B
	call SPI0_Send_DTA
	
	decfsz d1, F	;EOL
	goto s0fl	
	movlw 0xA0
	movwf d1	
	decfsz d2, F	;EOF
	goto s0fl	

	return
	
SPI0_Line	; send 160 * RGB(ClearColor)
	movlw 0xA0
	movwf d1
s0ll	;SPI 0 Fill Loop
	movf ClearColor, W	;R
	call SPI0_Send_DTA
	movf ClearColor+1, W;G
	call SPI0_Send_DTA
	movf ClearColor+2, W;B
	call SPI0_Send_DTA
	
	decfsz d1, F	;EOL
	goto s0ll	
	
	call d120ms
	return
	
line_RED
	CLRF ClearColor
	DECF ClearColor, F
	CLRF ClearColor+1
	CLRF ClearColor+2
	call SPI0_Line		
	RETURN
	
line_GREEN	
	CLRF ClearColor
	CLRF ClearColor+1
	DECF ClearColor+1, F	
	CLRF ClearColor+2
	call SPI0_Line		
	RETURN
	
	
	
	
	
disp_byte
	movf INDF, W
	movwf d3 ;save data 
	call mark_red
	movlw 0x08
	movwf d1
s_ssr	
	RLF d3, F
	BTFSC STATUS, C
	goto s_ssrS
	call mark_black
	goto s_ssrE
s_ssrS	
	call mark_white
s_ssrE	
	decfsz d1, F
	goto s_ssr
	
	return
	
mark_red	
	movlw 0xFF
	call SPI0_Send_DTA
	movlw 0x00
	call SPI0_Send_DTA	
	movlw 0x00
	call SPI0_Send_DTA	
	movlw 0xFF
	call SPI0_Send_DTA
	movlw 0x00
	call SPI0_Send_DTA	
	movlw 0x00
	call SPI0_Send_DTA	
	return
mark_white
	movlw 0x80
	call SPI0_Send_DTA
	movlw 0x80
	call SPI0_Send_DTA	
	movlw 0x80
	call SPI0_Send_DTA	
	movlw 0xFF
	call SPI0_Send_DTA
	movlw 0xFF
	call SPI0_Send_DTA	
	movlw 0xFF
	call SPI0_Send_DTA	
	return
mark_black
	movlw 0x80
	call SPI0_Send_DTA
	movlw 0x80
	call SPI0_Send_DTA	
	movlw 0x80
	call SPI0_Send_DTA	
	movlw 0x00
	call SPI0_Send_DTA
	movlw 0x00
	call SPI0_Send_DTA	
	movlw 0x00
	call SPI0_Send_DTA	
	return	
	
	
	
	END
	
	

	
	
	