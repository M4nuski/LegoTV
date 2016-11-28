# LegoTV
Software to update and run a small electronic-"picture frame"-esque "construction block" compatible TV

Hardware/Software:
- Uses a 1.8in TFT LCD screen (JT-D1800 ?) with ST7735 controller.
- Data is stored on a Windbond W25Q128FV SPI flash chip.
- Loads basic color mode + addresses controls + number of image + number of bytes per image from flash data.
- Data is spread accross the flash memory by sectors according to the bytes-per-image length.
- Automatically switch to next image after about 5 seconds.
- Supports Next and Previous switches.

## PIC Firmware 
The PIC16F88 assembly files of the actual firmware
Includes a few quick macro for 16bit operations and wrapped asm branching

## TFT Data Manager
A simple image library style editor to load and adjust image file to fit the TFT screen and then convert the resulting "library" in a pure flash data file readable by the TFT screen.
Also convert to different RGB color scheme.

## SPI Flash
Software interface to use the FTDI UM245R in a software SPI interface to transfer the data file into the SPI flash chip.
Support Windbond W25Q128FV 128mbit SPI flash devices and basic support of SST25VF016B

# The Project !

## Hardware

# Physical
The "block" used is custom machined on a taig mill using only DRO for reference and a custom cutter for the studs.
Top and Bottom paltes where actually test parts that got fused togheter with a casting of 2-parts polyurethane plastic then hollowed-out by the same milling machine.
Backplate is the actual SMT PCB

# Electrical
* Uses CMOS 3.3V chips for MCU + EEPROM + Display
* Home-made PCB with laser printer transfer sheets
* Diode used to prevent accidental introduction of batteries in reverse oder (project is intended to be used by childen) so 4 * 1.5V batteries are used in the pack
* Integrated 3.3V LDO regulator
* PIC 16F88 with internal 8MHz osc.  Also works with external 20MHz osc by adjusting the internal timing and adding external components but it proved to be an un-necessary complexity on the PCB
* 128Mbit /  16Mbyte flash can contains either 511 images in RGB444 (actually capped to 255 due to 1 byte num_image variable) or 255 RGB666 (actually RGB888 because lazyness) images

## Software

# TFT Data Manager
* C# 
* Support Load library, Add library (to current library), Save Library and Export data.
* Basic list of image to load, crop/resize and change position in list
* Export to tightly packed raw data

# SPI Flash
* C#
* Use the FTDI UM245R USB interface as an software emulated SPI host
* Configured to read and write to 2 EEPROM (flash) memory chip)
* Can load TFT Data Manager raw data and flash windbond chip with proper sector alignment

## Pictures and videos
#Photos
* TODO !

#Video
* Test 1 [firmware on breadboard] (https://youtu.be/bwzhosgC7QQ)
* Test 2 [PCB] (https://youtu.be/HSaehFAtPZ8)
* Test 3 [PCB inside with screen inside the block] (https://youtu.be/ZGKneOr_af8)
* Test 4 [final block with buttons] Inwork!
