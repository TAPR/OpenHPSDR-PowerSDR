#include <LiquidCrystal440.h>

/* 

////////////////////////////////////////////////////////////////////////
///                                                                  ///
///     SOLID STATE POWER AMPLIFIER  READOUT & PROTECTION  CIRCUIT   /// 
///                                                                  ///
////////////////////////////////////////////////////////////////////////

By ON7EQ 02/2015
Modify for the ANAN-8000DLE
Compiled with IDE 0.22 for ARDUINO NANO board

ARDUINO NANO CONNECTIONS
________________________

PIN		DESCRIPTION	                         TO PIN
			
ANALOG			
A0		Forward voltage directional coupler	Dir coupler FWD
A1		Reflected voltage directional coupler	Dir coupler REFL
A2		NTC 10k	- Temp                          NTC (10k to +5v)
A3		50v supply - voltage	                ACS713 Voltage
A4		50v supply - current	                ACS713 Current
A5		N/A	
A6		N/A	
A7		TXRX_STATUS                             PTT - read status	
			
DIGITAL	I/O		
0	I	Serial RX  	
1	O	Serial TX  	
2	I	"Operate" STATUS read	Logic HI = operate 
3	O	+50v Supply control	Output, logic HI = ON
4	O	Buzzer	Output,         Logic HI = sounding
5	O	FAN Control (PWM port)	Output, Logic HI = 'ON' pulse to Mosfet Gate
6	O	LCD : DB7	LCD 01
7	O	LCD : DB6	LCD 02
8	O	LCD : DB5	LCD 03
9	O	LCD : DB4	LCD 04
10	O	LCD : En2	LCD 15
11	O	LCD : En1	LCD 09
12	O	LCD : RS	LCD 11
13	O	PTT DISABLE     Logic HI = Kill PTT via open collector output

*/


// include 4x40 LED driver
#include <LiquidCrystal440.h>

// include math functions
#include "math.h"  

uint8_t nRows = 4;      //number of rows on LCD
uint8_t nColumns =40;   //number of columns

// Modify the pin number below to meet your board

//Analog pins
#define IN_FWD    A0  // analog input for left channel
#define IN_REFL   A1  // analog input for right channel
#define IN_NTC    A2  // analog input for right channel
#define IN_50vv   A3  // analog input for 50v voltage
#define IN_50vI   A4  // analog input for 50v current
#define IN_PTT    A7  // analog input for PTT detect  (0 = keyed)

//  Digital pins
#define IN_reset   0 //  input for recover/SELFTEST switch detect - momentary pulling to GND (0 = recover/SELFTEST)
#define IN_OPERATE 2 //  input for operate detect  (1 = OPERATE)

#define supply_50v 3 //  output for 50v supply control  (1 = 50v ON) 
#define Buzzer     4 //  output for Buzzer control  (1 = ON)
#define FANcontrol 5 //  output for FAN control  (PWM !)
#define PTT_block  13//  output for PTT block control  (no alarm = 0, FAULT = 1...n)

// Other minor configurable value

#define T_REFRESH1   100          // msec bargraph refresh rate
#define T_REFRESH2   500          // msec refresh rate other variables
#define T_PEAKHOLD   600          // msec peak hold time before return
#define T_pepHOLD    600          // msec pep hold time before return

// local variable

byte  fill[6]={ 0x20,0x00,0x01,0x02,0x03,0xFF };      // character used to fill (0=empty  5=full)
byte  peak[7]={ 0x20,0x00,0x04,0x05,0x06,0x07,0x20 }; // character used to peak indicator
int   lmax[5];                                        // level max memory
int   dly[5];                                         // delay & speed for peak return
long  lastT1=0;                                        // update display timer1
long  lastT2=0;                                        // update display timer2
long  lastTpep = 0;                                   // update PEP display
long  lastTempTime = 0;                               // update last temp readout for fan control
long  LastTXtime = 0;                                  // timestamp when last transmission ended (for FAN control)
long  HiPowerTime = 0;                                 // timestamp Hig Power - for EME conditions
long  HiAmp50Time = 0;                                 // timestamp high current value on 50v power supply
long  LoVolt50Time = 0;                                // timestamp low voltage value on 50v power supply



int   anF = 0;                            // analog read forward power
int   anR = 0;                            // analog read reflected power

// DEBUG
int debug_volt_50 = 0;
int debug_amp_50 = 0;

int  volt_50 = 0;                         // 50v volt supply - voltage (in 0,1 volt)
int  amp_50  = 0;                         // 50v volt supply - current (in 0,1 Amp)
int amp_50_offset = 69;                   // 50v volt supply - raw value when current 0.0 Amp
int  amp_50_max = 0;                      // 50v volt supply - max current (in 0,1 Amp)
int supply_50_stby = 0;

float Amps = 0;
int AmpsDis = 0;

// int  volt_26 = 0;                         // 26v volt supply - voltage (in 0,1 volt)
// int  volt_12 = 0;                         // 13,8 v volt supply - voltage (in 0,1 volt)

int  temp = 0;                            // temperature (in 1 °C)
int  lasttemp = 0;                        // last temperature for fan control    

byte  PTT = 0;                             // 0 = RX, 1 = TX
byte  wasPTT = 0;                          // former RX/TX status byte : if 1 = was in TX mode

byte  TestMode = 0;                        // 0 = normal mode, 1 = TestMode (PA will 'operate' with no +50v). Testmode is
                                           // initiated by depressing 'RESET4 button upon startup of SSPA

int  Operate = 0;                         // When operate = 0 , PA in stby mode

byte HiTemp = (0);                       // Temperature passing above PID start temp

byte FAULT = 0;                           // when FAULT = 0 : NO FAULT 
                                          // when FAULT = 1 : Overcurrent
                                          // when FAULT = 2 : voltage error 50v
                                          // when FAULT = 3 : voltage error 26v
                                          // when FAULT = 4 : voltage error 12v
                                          // when FAULT = 5 : power out error 
                                          // when FAULT = 6 : SWR error 
                                          // when FAULT = 7 : NTC sensor read error 
                                          // when FAULT = 8 : Temperature error
                                          // when FAULT = 9 : 50v idle current error 
                                          // when FAULT = 10: W6PQL control PCB SWR exceed fault                                           
                                          
byte FirstLoop = (1);                     // First loop detect 
byte SelfTest  = (1);                     // 1 to force selftest
byte Debug = (0);                         // Set to 1 for debug output to serial port

byte HiAmp50 = (0);                       // Flag to detect long lasting current peaks on 50v supply

byte LoVolt50 = (0);                      // Flag to detect long lasting voltage dip on 50v supply

byte EMEmode = (0);                       // 0 = normal, 1 = EME conditions (no SSB but CW/JT65)

// DEBUG
unsigned long debug_pow_fwd = 0;
unsigned long debug_pow_ref = 0;

// POWER/SWR
#define SDISP      10
#define S_SWR      10
#define SMULT       8
const unsigned long calibrP = 3873;
const unsigned long Vdiode = 9; 
unsigned long pow_fwd_adc  = 0;           // forward power reading from ADC (ADC units)
unsigned long pow_ref_adc  = 0;           // reverse power reading from ADC (ADC units)
unsigned long pow_fwd_disp = 0;           // forward power for bar display (watts << SMULT)
unsigned long pow_ref_disp = 0;           // reverse power for bar display (watts << SMULT)
unsigned long pow_fwd_swr  = 0;           // forward power reading for swr calculations (ADC_units^2)
unsigned long pow_ref_swr  = 0;           // reverse power reading for swr calculations (ADC_units^2)
unsigned long tpow0 = 0;                  // temporary power variable
unsigned long tpow1 = 0;                  // temporary power variable

float SWR = 0;                            // SWR 
float Pratio = 0;                         // Power ratio P forward / P refl
int SWRDis = 0;                           // power calculation for showing in display

float Fvolt = 0;
int FvoltDis = 0;
float Rvolt = 0;
int RvoltDis = 0;

// Define various ADC prescaler
const unsigned char PS_16 = (1 << ADPS2);
const unsigned char PS_32 = (1 << ADPS2) | (1 << ADPS0);
const unsigned char PS_64 = (1 << ADPS2) | (1 << ADPS1);
const unsigned char PS_128 = (1 << ADPS2) | (1 << ADPS1) | (1 << ADPS0);

LiquidCrystal lcd(12,255,11,10,9,8,7,6);  // (RS,RW,En1,En2,D4,D5,D6,D7) 255 if RW  is connected to GND and not controlled by the interface.

//=====for a 4x40 LCD with 2 HD44780 type chips and 18 pin interface in 2 rows of 9; 

//   LCD     Nano     Signal
//    18              Gnd        Backlight white on blue 4x LED draws 40 mAmps
//    17              +5V        Backligt + through external resistor 15 Ohm.   
//    16      NC      not used
//    15      10      En2 -- enable the 2nd HD44780 chip which controls the bottom 2 rows of the display
//    14              +5V supply logic
//    13              Gnd logic
//    12              Wiper of contrast resistor (22k between +5v and Gnd)
//    11      12      RS
//    10              Connect to Gnd 
//     9      11      En1 -- enable the 1st HD44780 which controls the top 2 rows
//    5-8             Data 0-3: not used in 4 bit modes
//    1-4    09-06    Data 4-7: LCD DB7 to ARDUINO 06, DB6 to 07, DB5 to 08, DB4 to 09

/////////////////// DRAW BAR //////////////////////////

void bar ( int row,int lev )
{
    lcd.setCursor( 3,row );
    lcd.write( row ? ' ' : ' ' );
    for( int i=1 ; i<30 ; i++ )
    {
        int f = constrain( lev      -i*5,0,5 );  // Level instant
        int p = constrain( lmax[row]-i*5,0,6 );  // Level maximum (=peak)
        if( f )
            lcd.write( fill[ f ] );
        else
            lcd.write( peak[ p ] );
    }
    if( lev > lmax[row] )
    {
        lmax[row] = lev;
        dly[row]  = -(T_PEAKHOLD)/T_REFRESH1;                // Starting delay value. Negative=peak don't move
    }
    else
    {
        if( dly[row]>0 )
            lmax[row] -= dly[row]; 
  
        if( lmax[row]<0 )
            lmax[row]=0;
        else
            dly[row]++;
    }
}

byte block[8][8]=
{
    { 0x10,0x10,0x10,0x10,0x10,0x10,0x10,0x10 },  // define character for fill the bar
    { 0x18,0x18,0x18,0x18,0x18,0x18,0x18,0x18 },
    { 0x1C,0x1C,0x1C,0x1C,0x1C,0x1C,0x1C,0x1C },
    { 0x1E,0x1E,0x1E,0x1E,0x1E,0x1E,0x1E,0x1E },
  
    { 0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08 },  // define character for peak level
    { 0x04,0x04,0x04,0x04,0x04,0x04,0x04,0x04 },
    { 0x02,0x02,0x02,0x02,0x02,0x02,0x02,0x02 },
    { 0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01 },
};

///////////////// PRINT TEMPLATE  ///////////////////

void printtemplate () {

  lcd.setCursor( 0,0 );
  lcd.print( "FWD                                     ");
  lcd.setCursor( 0,1 );
  lcd.print( "REF                                     ");

  lcd.setCursor( 6,2 );
  lcd.print( "Vd --.-"); 
  lcd.setCursor( 6,3 );
  lcd.print( "Id --.-"); 

  //lcd.setCursor( 16,2 );
  //lcd.print( "Vr=--.-"); 
  //lcd.setCursor( 16,3 );
  //lcd.print( "Vs=--.-"); 

if (TestMode == 1) {
    lcd.setCursor( 26,2 );
    lcd.print( "Vfwd -.-");
    lcd.setCursor( 26,3 );
    lcd.print( "Vref -.-");
}

  //lcd.setCursor( 26,2 );
  lcd.setCursor( 16,2 );
  lcd.print( "temp --"); 
  lcd.print((char)223);  //degree symbol
  //lcd.print((char)165); 

  lcd.setCursor( 0,2 );
  lcd.print( "swr"); 
 
  lcd.setCursor( 0,3 );
  lcd.print( "-.-"); 
 
 /* PREAMP 
  lcd.setCursor( 31,2 );
  lcd.print( "PRE"); 
  lcd.setCursor( 31,3 );
  lcd.print( "AMP"); 
 */
 
  lcd.setCursor( 36,2 );
  lcd.print( "stby"); 
  
  lcd.setCursor( 4,2 ); 
  lcd.write( peak[3] );
  lcd.setCursor( 4,3 ); 
  lcd.write( peak[3] );
  
  lcd.setCursor( 14,2 ); 
  lcd.write( peak[3] );
  lcd.setCursor( 14,3 ); 
  lcd.write( peak[3] );
   
  lcd.setCursor( 25,2 ); 
  lcd.write( peak[3] );
 // lcd.setCursor( 24,3 ); 
  //lcd.write( peak[3] );
   
  //lcd.setCursor( 30,2 ); 
  //lcd.write( peak[3] );
  //lcd.setCursor( 30,3 ); 
  //lcd.write( peak[3] );  
  
  lcd.setCursor( 34,2 ); 
  lcd.write( peak[3] );
  lcd.setCursor( 34,3 ); 
  lcd.write( peak[3] );
  
}

/////////// This function will calculate temperature from 10k NTC readout /////////////

double Thermister(int RawADC) {
 double Temp;
 Temp = log(((10240000/RawADC) - 10000));
 //Temp = log(10000.0*((1024.0/RawADC-1)));
 //Temp = 1 / (0.001129148 + (0.000234125 + (0.0000000876741 * Temp * Temp ))* Temp );
  Temp = 1 / (0.001085894282 + (0.0002396216935 + (0.00000007790843993 * Temp * Temp ))* Temp );

 Temp = Temp - 273.15;            // Convert Kelvin to Celcius
// Temp = (Temp * 9.0)/ 5.0 + 32.0; // Convert Celcius to Fahrenheit
 return Temp;
}


////////////////////////////////////////////////////////////////
///////////////////////////    SETUP      //////////////////////
////////////////////////////////////////////////////////////////

void setup() {

      if(Debug==1) {
        Serial.begin(9600);
        Serial.println("8000DLE");
      }
      
      /// Prepare I/O pins
            
     // pinMode(IN_PREAMP, INPUT); // not used
      pinMode(IN_OPERATE, INPUT);
      digitalWrite(IN_OPERATE, HIGH); // enable internal pull-up resistor for STBY switch
      
      pinMode(IN_reset, INPUT);
     // digitalWrite(IN_reset, HIGH);
      
      //digitalWrite(supply_50v, LOW);     // We do not allow +50v yet
      digitalWrite(supply_50v, HIGH);
      pinMode(supply_50v, OUTPUT);
      
      digitalWrite(Buzzer,0);
      pinMode(Buzzer, OUTPUT);
      
      digitalWrite(PTT_block,HIGH);      // we force RX mode till all is OK !
      pinMode(PTT_block, OUTPUT);

      //amp_50_offset = analogRead(IN_50vI); // get the base value when no current
      //if(Debug==1) {
      //  Serial.print("amp_50: offset=");Serial.println(amp_50_offset);
      //}
      
      lcd.begin(nColumns,nRows);     
                                   

      for( int i=0 ; i<8 ; i++ )
      lcd.createChar( i,block[i] );
      
      lcd.clear();
      
      lcd.setCursor(5,0 );   
      lcd.print ("       ANAN-8000DLE ");   
      lcd.setCursor(5,2 );   
      lcd.print ("       Apache-Labs           ");   
      lcd.setCursor(5,3 );   
      lcd.print ("     03/2017  -  V1.07       ");      
      delay (2000);      
      lcd.clear();      
      delay (500); 
      printtemplate();
      
  
      lastT2 = millis();                // set T2 display refresh

      // set up the ADC
      ADCSRA &= ~PS_128;  // remove bits set by Arduino library

      // you can choose a prescaler from above.
      // PS_16, PS_32, PS_64 or PS_128
     // ADCSRA |= PS_128; // 128 prescaler (8.62kSPS)
     // ADCSRA |= PS_64;  // 64 prescaler (16.66kSPS)
     // ADCSRA |= PS_32;  // 32 prescaler (31.25kSPS)
        ADCSRA |= PS_16;  // 16 prescaler (50kSPS)
  }

///////////////////////////////////////////////////////////
/////////////////////////// LOOP //////////////////////////
///////////////////////////////////////////////////////////


void loop() {  

    // Check if TESTMODE required. Testmode is initiated by depressing the RESET button upon powering up.
    // In testmode, no 50v supply is applied to PA pallet, and no 50v is required to go into 'OPERATE' and 'TX' mode
    // exiting TESTMODE is only possible by powering up again SSPA
  
    if (((digitalRead(IN_OPERATE) == LOW) or (TestMode == 1)) and (FirstLoop == 1))   // TestMode if switch is in STBY mode during power on
    {   
        TestMode = 1;
        lcd.setCursor(10,0 );   
        lcd.print                    ("T E S T    M O D E  !");
        
        digitalWrite(Buzzer,HIGH);
        delay (200);    
        digitalWrite(Buzzer,LOW);
     
        lcd.setCursor(10,0 );   
        lcd.print                    ("                     ");
        lcd.setCursor( 7,1 );   
        lcd.print                 ("                           ");    
      
        delay (200); 
        digitalWrite(Buzzer,HIGH);
     
        lcd.setCursor(10,0 );   
        lcd.print                    ("T E S T    M O D E  !");
     
        delay (200);    
        digitalWrite(Buzzer,LOW);
     
        lcd.setCursor(10,0 );   
        lcd.print                    ("                     ");
        lcd.setCursor( 7,1 );   
        lcd.print                 ("                           ");    
          
        delay (200); 
        digitalWrite(Buzzer,HIGH);
        lcd.setCursor(10,0 );   
        lcd.print                    ("T E S T    M O D E  !");
       
        delay (200);    
        digitalWrite(Buzzer,LOW);
     
        delay (500);
        printtemplate();
          
    }   // End TestMode
       
    // update OPERATE / STBY mode
    if (digitalRead(IN_OPERATE) == HIGH) 
    {
        digitalWrite(supply_50v, HIGH);
        Operate = 1;
    }
    else 
    {
        Operate = 0;
        supply_50_stby = 1;
        digitalWrite(supply_50v, LOW);
    }     
      
    // update PTT status
    if (analogRead(IN_PTT) < 512)  PTT = 1;
    else                           PTT = 0;
  
    // Check if no PTT & operate mode at startup of our PA
    if ((Operate == 1) and (FirstLoop == 1) and (PTT == 1)) 
    {
        digitalWrite(Buzzer,HIGH);
        lcd.setCursor(5,0 );   
        lcd.print                    ("SET TRANSCEIVER IN RX PLEASE !");
        delay (500);
        digitalWrite(Buzzer,LOW); 
        printtemplate();
        delay (500);
        loop();
    }
  
    // perform every loop some measurement we need for critical faults detection
    //         __________

    // Current 50v
    amp_50 = analogRead(IN_50vI);
    Amps = (.039 * amp_50 - 2.7);
    if (Amps < 0) Amps = 0;

    // DEBUG
    if(Debug==1) 
    {
        if((amp_50<(debug_amp_50-1)) or (amp_50>(debug_amp_50+1))) 
        {
            Serial.print("amp_50: raw=");Serial.println(amp_50);
            debug_amp_50=amp_50;
        }
    }
    amp_50 = Amps * 10;    
    if (amp_50 > amp_50_max) amp_50_max = amp_50;             // measure peak current every T1 to display max every T2
  
    //Detect current peak (instantaneous, 'never exceed' value) :
    if (amp_50 >= 120) // Critical overcurrent ! peak with more than 12A !
    {     
        FAULT = 1;
        fault();           // Jump to FAULT handling
    }
                  
    //Detect current 'long lasting' peak (overdrive of PA - protect LDMOS) :       
    if (amp_50 >= 120)     // more than 12A
    {
        if (HiAmp50 == 0) HiAmp50Time =  millis();  // timestamp reading
        HiAmp50 = 1;
    }
    else HiAmp50 = 0;
                   
    if ((HiAmp50 == 1) and ((millis() - HiAmp50Time) > 30))   // we have a peak lasting more than 30 ms
    {
        HiAmp50 = 0;
        FAULT = 1;
        fault();           // Jump to FAULT handling
    }           
      
    // Voltage 50v 
    volt_50=analogRead(IN_50vv);

    // DEBUG
    if(Debug==1) 
    {
        if((volt_50 < (debug_volt_50 - 1)) or (volt_50 > (debug_volt_50 + 1))) 
        {
            Serial.print("volt_50: raw=");
            Serial.println(volt_50);
            debug_volt_50 = volt_50;
        }
    }

    volt_50 = map(volt_50,0,1023,0,1048);
       
    if ((volt_50 > 430) and (Operate == 1) and (supply_50_stby == 1)) supply_50_stby = 0;
  
    if (volt_50 >= 560)                        // Overvoltage, always alarm !  
    {   
        FAULT = 2;
        fault();           // Jump to FAULT handling
    }
                          
    if ((volt_50 <= 430) and (Operate == 1) and  (TestMode == 0) and (PTT == 0) and (supply_50_stby == 0))  // Undervoltage while in operate mode, in RX ! Fault in pallet ?    
    {
        FAULT = 2;
        fault();           // Jump to FAULT handling
    }                 
           
    // detect long lasting voltage dip
    if ((volt_50 <= 450) and (Operate == 1) and (TestMode == 0)  and (PTT == 1) )  // Undervoltage while in TX mode    
    {
        if (LoVolt50 == 0) LoVolt50Time = millis();
        LoVolt50 = 1;          
    }                 
    else LoVolt50 = 0;
                    
    if ((LoVolt50 == 1) and ((millis() - LoVolt50Time) > 100))   // we have a dip >100 ms !
    {
        LoVolt50 = 0;
        FAULT = 2;
        fault();           // Jump to FAULT handling
    }      

    pow_fwd_adc = analogRead(IN_FWD);            // read forward power
    pow_ref_adc = analogRead(IN_REFL);           // read reverse power

    tpow0 = pow_fwd_adc + Vdiode;
    tpow1 = tpow0 * tpow0;
    if (tpow1 > pow_fwd_swr)
        pow_fwd_swr = tpow1;
    else
        pow_fwd_swr = ((pow_fwd_swr << S_SWR) - pow_fwd_swr) >> S_SWR;
    tpow1 = (tpow1 << SMULT) / calibrP;
    if (tpow1 > pow_fwd_disp)
        pow_fwd_disp = tpow1;
    else
        pow_fwd_disp = ((pow_fwd_disp << SDISP) - pow_fwd_disp) >> SDISP;

    tpow0 = pow_ref_adc + Vdiode;
    tpow1 = tpow0 * tpow0;
    if (tpow1 > pow_ref_swr)
        pow_ref_swr = tpow1;
    else
        pow_ref_swr = ((pow_ref_swr << S_SWR) - pow_ref_swr) >> S_SWR;
    tpow1 = (tpow1 << SMULT) / calibrP;
    if (tpow1 > pow_ref_disp)
        pow_ref_disp = tpow1;
    else
        pow_ref_disp = ((pow_ref_disp << SDISP) - pow_ref_disp) >> SDISP;

    // DEBUG
    if(Debug==1) 
    {
        Fvolt = (pow_fwd_disp >> SMULT) * (5.0 / 1023.0);
        if(pow_fwd_disp!=debug_pow_fwd) 
        {
            Serial.print("fwd raw="); 
            Serial.print(pow_fwd_disp>>SMULT); 
            Serial.print(" v="); 
            Serial.println(Fvolt);
            debug_pow_fwd=pow_fwd_disp;
        }
    }

    // TESTMODE:  display coupler volatage for testing
    // place jumper on J7
    if (TestMode == 1) 
    {
        Fvolt = (pow_fwd_disp >> SMULT) * (5.0 / 1023.0);
        lcd.setCursor( 31,2 );
        FvoltDis = (Fvolt * 10);
        lcd.print((FvoltDis/10), DEC);
        lcd.print(".");
        lcd.print((FvoltDis)%10, DEC); 
        //lcd.print(pow_fwd,DEC);
    }

    // DEBUG
    if(Debug==1) 
    {
        if(pow_ref_disp!=debug_pow_ref) 
        {
            Rvolt = (pow_ref_disp >> SMULT) * (5.0 / 1023.0);
            Serial.print("ref raw="); 
            Serial.print(pow_ref_disp>>SMULT); 
            Serial.print(" v="); 
            Serial.println(Rvolt);
            debug_pow_ref=pow_ref_disp;
        }
    }

    // TESTMODE:  display coupler volatage for testing
    // place jumper on J7
    if (TestMode == 1) 
    {
        Rvolt = (pow_ref_disp >> SMULT) * (5.0 / 1023.0);
        lcd.setCursor( 31,3 );
        RvoltDis = (Rvolt * 10);
        lcd.print((RvoltDis/10), DEC);
        lcd.print(".");
        lcd.print((RvoltDis)%10, DEC); 
    }
      
    // detect SWR error / load mismatch 
    Pratio = (float)pow_ref_swr / (float)pow_fwd_swr;
    SWR = abs ((1+sqrt(Pratio)) / (1-sqrt(Pratio))) ; 
    
    if ((SWR > 3.5) and ((pow_fwd_disp >> SMULT) > 50))   //  only when forward power > 50w
    {
        FAULT = 6;                
        fault();           // Jump to FAULT handling    
    }

    // detect PA overdrive
    if ((pow_fwd_disp >> SMULT) > 270)         // PA overdriven !
    {
        FAULT = 5;
        fault();           // Jump to FAULT handling                       
    }
                        
    // All critical parameters checked, if selftest on startup we can now release PTT block and allow +50v supply PA   
   
    if ((SelfTest == 1) ) 
    {
        amp_50  = map (analogRead(IN_50vI)-69,0,1023,0,675);
        if ((amp_50 > 5) /* and (TestMode == 0)*/ )             // more than 0.5 A idle current !
        {
            digitalWrite(supply_50v, LOW);  // immediately switch off 50v !
            FAULT = 9;
            fault();           // Jump to FAULT handling
        } 
    
        lcd.setCursor( 13,0 );   
        lcd.print  ("SELFTEST = OK !");
        
        // Morse code
        // Oscar
        digitalWrite(Buzzer,HIGH);
        delay (240);
        digitalWrite(Buzzer,LOW);
        delay (80);
        digitalWrite(Buzzer,HIGH);
        delay (240);
        digitalWrite(Buzzer,LOW);
        delay (80);        
        digitalWrite(Buzzer,HIGH);
        delay (240);
        digitalWrite(Buzzer,LOW);
        // space 

        delay (240);        
        // Kilo
        digitalWrite(Buzzer,HIGH);
        delay (240);
        digitalWrite(Buzzer,LOW);
        delay (80);         
        digitalWrite(Buzzer,HIGH);        
        delay (80);            
        digitalWrite(Buzzer,LOW);        
        delay (80); 
        digitalWrite(Buzzer,HIGH);
        delay (240);
        digitalWrite(Buzzer,LOW);
        delay (500);
        // end CW tune
                  
        // NOW PA ready to go !
        digitalWrite(PTT_block,LOW);
        SelfTest = (0);
        
        LastTXtime = millis() - 35000;    // avoid FAN running at startup
        
        
    }   // end of selftest        

    ///// we must now update screen for timer T1 = short timer //////
     
    if (( millis() < lastT1 ) and (FirstLoop == 0))  return;
  
    lastT1 = millis() + T_REFRESH1;
      
    // bargraph display
    anF = map( pow_fwd_disp>>SMULT,0,250,       0,150 );    // 150 = 30 x 5 colums, full scale 250w
    anR = map( pow_ref_disp,       0, 25<<SMULT,0,150 );    // 150 = 30 x 5 colums, full scale  25w
    bar( 0,anF );
    bar( 1,anR );
  
    // update OPER / STBY STATUS
    if (Operate == HIGH)      // OPERATE
    {
        digitalWrite(PTT_block,LOW);
        lcd.setCursor( 36,2 );
        lcd.print( "OPER");
    }
    else                      // STANDBY
    {
        digitalWrite(PTT_block,HIGH);
        lcd.setCursor( 36,2 );
        lcd.print( "stby"); 
        lcd.setCursor( 36,3 );
        lcd.print( "    ");    // Clear TX / RX indicator
        LastTXtime = millis() - 31000;    // reset FAN timer
    }
 
    // update TX / RX STATUS
    if (Operate == 1)
    {
        lcd.setCursor( 36,3 );
        if (PTT == 1)
        {
            lcd.print( "-TX-");
            wasPTT = 1;
        }
        else
        {
            lcd.print( "-RX-");
            if (wasPTT == 1)   // previous state was TX !
            {  
                wasPTT = 0;
                LastTXtime = millis();
            }
        }
    }
      
    //////////// we must update screen for timer T2 = long timer //////////////
    if ((millis()-lastT2) > T_REFRESH2) 
    {   
        lastT2 = millis();
   
        // SWR display
        SWRDis = (SWR * 10) + 0.5;   // display SWR one figure after DP
 
        if (SWRDis < 10)    // SWR cannot be lower than 1.0
            SWRDis = 10 ;
        lcd.setCursor( 0,3 );
        if (pow_fwd_adc < 5)
            lcd.print("-.-");
        else if (SWRDis >= 50)
            lcd.print(">5!");
        else 
        {
            lcd.print((SWRDis/10), DEC);
            lcd.print(".");
            lcd.print((SWRDis)%10, DEC); 
        }   

        // Temperature 
        temp = Thermister(300 + analogRead(IN_NTC));
        lcd.setCursor( 21,2 );
        if ((temp < 0) or (temp > 99))
        {
            lcd.print("??");
            FAULT = 7;      // NTC error !
            fault();
        } 
        else 
        {
            constrain (temp,0,99);
            if ((temp)<10) lcd.print("0"); 
            lcd.print(temp, DEC); 
        }
        if (((temp >= 0) and (temp <=  5)) or (temp >= 60))     // FAULT check  
        {   
            FAULT = 8;      // temperature error, too cold or hot  !
            fault();        
        }            
                  
        // +50v SUPPLY  Volt & Amp measurements display
        lcd.setCursor( 9,2 );                    // Volts
        if (volt_50 > 999) volt_50 = 999;        // smoke in the shack !
        if (volt_50<100) lcd.print(" ");
        lcd.print((volt_50/10), DEC);
        lcd.print(".");
        lcd.print((volt_50)%10, DEC); 
        lcd.setCursor( 9,3 );                    // Amps
        if (amp_50_max > 999) amp_50_max = 999;
        if (amp_50_max<100) lcd.print(" ");
        lcd.print((amp_50_max/10), DEC);
        lcd.print(".");
        lcd.print((amp_50_max)%10, DEC); 
        amp_50_max = 0;        // reset after display
           
        FirstLoop = 0;      // first loop completely run
      
    } /////////////////// END T2 Refresh ///////////////////////// 

}  ////// this is end of loop 

/////////////////////   FAULT CONDITIONS /////////////////////

// when FAULT = 0 : NO FAULT 
// when FAULT = 1 : Overcurrent
// when FAULT = 2 : voltage error 50v
// when FAULT = 3 : voltage error 26v
// when FAULT = 4 : voltage error 12v
// when FAULT = 5 : power out error 
// when FAULT = 6 : SWR error 
// when FAULT = 7 : NTC read error 
// when FAULT = 8 : Temperature error
// when FAULT = 9 : 50v idle current error
// when FAULT = 10: W6PQL control PCB SWR exceed fault  

void fault() {
  
//return; // uncomment for testing

  if (FAULT == 0) return;          // No fault !
  
  // take vital actions !
 
  digitalWrite(PTT_block,HIGH);     // signal to sequencer that we force RX mode !
  //digitalWrite(supply_50v, LOW);    // remove + 50v supply from pallet
  
  // update TX / RX STATUS while in FAULT void
   
   if (Operate == 1){
       lcd.setCursor( 36,3 );
        lcd.print( "-RX-");
        }
  else  {
       lcd.setCursor( 36,3 );
       lcd.print( "    "); 
        }      
 

  digitalWrite(Buzzer,HIGH);        // Sound buzzer

  // Signal on display
  
  lcd.setCursor( 0,0 );   
  lcd.print                    ("         F   A   U   L   T    !         ");
  lcd.setCursor( 0,1 ); 
  if (FAULT == 1){
    digitalWrite(supply_50v,LOW);
       lcd.print("         OVERCURRENT 50v SUPPLY         "); 
  // Show Error value
       lcd.setCursor( 9,3 );                    // Amps
       if (amp_50 > 999) amp_50 = 999;
       
       if (amp_50<100) lcd.print(" ");
       
       lcd.print((amp_50/10), DEC);
       lcd.print(".");
       lcd.print((amp_50)%10, DEC); 
   }
   
  if (FAULT == 2)   {
    digitalWrite(supply_50v,LOW);
   lcd.print("      UNDER/OVERVOLTAGE 50v SUPPLY      ");    
    // Show Volt_50 error value
   lcd.setCursor( 9,2 );
   if (volt_50 > 999) volt_50 = 999;
   if (volt_50<100) lcd.print(" ");
   lcd.print((volt_50/10), DEC);lcd.print(".");
   lcd.print((volt_50)%10, DEC); 
   }; 
  
 // if (FAULT == 3)   {
 //   lcd.print("      UNDER/OVERVOLTAGE 26v SUPPLY      "); 
    // Show Volt_26 error value
  //  lcd.setCursor( 19,2 );   
  //  if (volt_26 < 100) lcd.print(" ");  // less than 10v
  //  lcd.print((volt_26/10), DEC);
  //  lcd.print(".");
  //  lcd.print((volt_26)%10, DEC); 
  //  };    
    
 // if (FAULT == 4)   {
   // lcd.print("     UNDER/OVERVOLTAGE 13.8v SUPPLY     ");    
    // Show Volt_12 error value 
   // lcd.setCursor( 19,3 );   
   // if (volt_12 < 100) lcd.print(" ");  // less than 10v
   // lcd.print((volt_12/10), DEC);
  //  lcd.print(".");
  //  lcd.print((volt_12)%10, DEC); 
  //  };    
    
  if (FAULT == 5)     lcd.print("       POWER AMPLIFIER OVERDRIVE        ");  
  if (FAULT == 6)     lcd.print("        LOAD MISMATCH (SWR EXCEED)      ");    
  if (FAULT == 7)     lcd.print("        TEMP. PROBE CIRCUIT ERROR     ");   
  if (FAULT == 8)     lcd.print("         UNDER/OVER TEMPERATURE         ");
  if((FAULT == 8) and (temp > 40 )) analogWrite (FANcontrol,255);      // cooling down ...                    
  if (FAULT == 9)     lcd.print("     50v SUPPLY IDLE CURRENT EXCEED     ");
  if (FAULT ==10)     lcd.print("    SWR ERROR - POWER OFF & ON     ");
  
  delay (500);
 
  digitalWrite(Buzzer,LOW);        
  
  lcd.setCursor( 0,0 );
  lcd.print  ("                                        ");
  delay (500);
  
  if (digitalRead(IN_reset) == LOW) ResetFault(); 
  
  fault();
  }
/////////////////// END FAULT CONDITION ////////////////////


//////////////////   RECOVER PROCEDURE   /////////////////////

void ResetFault() {
  
  digitalWrite(Buzzer,LOW);
  lcd.setCursor( 0,1 );   
  lcd.print                    ("                                        ");  
  lcd.setCursor( 0,0 );   
  if (PTT ==0) lcd.print                    ("         R E S E T T I N G              ");
  if (PTT ==1) lcd.print                    ("       R E S E T T I N G    B U T       ");
 
  
  
  if (PTT ==0)delay (800);
  lcd.setCursor( 0,0 );   
  if (PTT ==0)lcd.print                    ("         R E S E T T I N G .            ");
  delay (800);  
  lcd.setCursor( 0,0 );   
  if (PTT ==0)lcd.print                    ("         R E S E T T I N G . .          ");
  if (PTT ==0)delay (800);  
  lcd.setCursor( 0,0 );   
  if (PTT ==0)lcd.print                    ("         R E S E T T I N G . . .        ");
  if (PTT ==0)delay (1500);      
  
 // update PTT status : restart / recover only in RX mode
 
 if (analogRead(IN_PTT) < 512) {      // We are still in TX mode !
   PTT = 1;
   digitalWrite(Buzzer,HIGH);
   lcd.setCursor( 0,0 );   
   lcd.print                   ("      SET TRANSCEIVER IN RX PLEASE !    ");
   delay (800);
   ResetFault();                            // stay in loop till RX mode
   }
  
  
 // Recovery
     digitalWrite(Buzzer,LOW);
     digitalWrite(PTT_block,LOW);         // signal to sequencer that we allow again PTT mode !
   
     FAULT = 0;                   //recover fault condition
     printtemplate ();
     SelfTest = 1;                // Force Selftest
    }




