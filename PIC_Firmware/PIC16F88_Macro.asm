BANK0	MACRO
	BCF	STATUS, RP0
	BCF	STATUS, RP1
	ENDM

BANK1	MACRO
	BSF	STATUS, RP0
	BCF	STATUS, RP1
	ENDM

BANK2	MACRO
	BCF	STATUS, RP0
	BSF	STATUS, RP1
	ENDM

BANK3	MACRO
	BSF	STATUS, RP0
	BSF	STATUS, RP1
	ENDM
	
MOV_short	MACRO from_byte, to_byte
	MOVF from_byte, W
	MOVWF to_byte
	MOVF from_byte+1, W
	MOVWF to_byte+1
	ENDM
	
ADD_short	MACRO increm, dest ;dest = dest + increm
	MOVF increm, W
	ADDWF dest, F
	BTFSC STATUS, C
	INCF dest+1, F
	MOVF increm+1, W
	ADDWF dest+1, F
	ENDM
	
SUB_short	MACRO decrem, dest ;dest = dest - decrem
	MOVF decrem, W
	SUBWF dest, F
	BTFSS STATUS, C
	DECF dest+1, F
	MOVF decrem+1, W
	SUBWF dest+1, F
	ENDM
	
BR_FF_NE	MACRO var1, var2, dest	;branch file-file not equal
	MOVF var1, W
	XORWF var2, W
	BTFSS STATUS, Z ; EQ: z=1, NEQ: Z=0;
	GOTO dest
	ENDM
	
BR_FF_EQ	MACRO var1, var2, dest	;branch file-file equal
	MOVF var1, W
	XORWF var2, W
	BTFSC STATUS, Z
	GOTO dest
	ENDM
	
BR_LF_NE	MACRO lit1, var2, dest ;branch literate-file not equal
	MOVLW lit1
	XORWF var2, W
	BTFSS STATUS, Z
	GOTO dest
	ENDM
	
BR_LF_EQ	MACRO lit1, var2, dest ;branch literate-file equal
	MOVLW lit1
	XORWF var2, W
	BTFSC STATUS, Z
	GOTO dest
	ENDM	
	
	