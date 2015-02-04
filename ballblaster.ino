#include "actuator.h"
#include "accelerometer.h"

#define RELAY1  6                        
#define RELAY2  7                        
#define RELAY3  8                        
#define RELAY4  9

// accelerometer

#define ADXL_Z A2

Actuator actuator1(10, 11);
Actuator actuator2(12, 13);
Accelerometer accelerometer(ADXL_X, ADXL_Y, ADXL_Z);

ISR(EXT_INT0_vect)
{

	Serial.println('test');
}

void setup()
{
pinMode(2,INPUT_PULLUP);

  sei();
  Serial.begin(115200);
}

void loop()
{
  char cmd;
  while (Serial.available() > 0) 
  {
    cmd = (char)Serial.read();
    Serial.print(cmd);
    
    switch (cmd) 
    {
      case 'a': 
        actuator1.Retract();
        break;
      case 'b': 
        actuator1.Extend();
        break;
      case 'c': 
        actuator1.Stop();
        break;

      case 'd': 
        actuator2.Retract();
        break;
      case 'e': 
        actuator2.Extend();
        break;
      case 'f': 
        actuator2.Stop();
        break;

      case 'g':
	   	int x, y, z;
		float xvoltage, yvoltage, zvoltage;
        accelerometer.getXYZ(&x, &y, &z);
        accelerometer.getAcceleration(&xvoltage, &yvoltage, &zvoltage);
		
		Serial.println("xyz:");
		Serial.println(x);
		Serial.println(y);
		Serial.println(z);

		Serial.println("voltage:");
		Serial.println(xvoltage);
		Serial.println(yvoltage);
		Serial.println(zvoltage);
        break;
    }
  }
}

