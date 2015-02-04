#include "actuator.h"
#include "accelerometer.h"
#include "reloadActuator.h"

#include <avr/io.h>
#include <avr/interrupt.h>

// accelerometer
#define ADXL_X A0
#define ADXL_Y A1
#define ADXL_Z A2

Actuator actuator_reload(10, 11);
Actuator actuator_pitch(12, 13);
Actuator actuator_yaw(8, 9);

Accelerometer accelerometer(ADXL_X, ADXL_Y, ADXL_Z);

void actuator_reload_limit_change() 
{
    Serial.print("change ");
	int limitVal = digitalRead(2);
	Serial.println(limitVal);
}

void setup()
{
	Serial.begin(115200);

	// External Interrupts: 
	//	3 (interrupt 0)
	//	2 (interrupt 1)
	//	0 (interrupt 2)
	//	1 (interrupt 3)
	//	7 (interrupt 4)
	attachInterrupt(0, actuator_reload_limit_change, CHANGE);
	//attachInterrupt(1, actuator_reload_limit_change, CHANGE);

	pinMode(2, INPUT_PULLUP);
	//pinMode(3, INPUT_PULLUP);
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
			/* reload */
			case 'a': 
				actuator_reload.Retract();
				break;
			case 'b': 
				actuator_reload.Extend();
				break;
			case 'c': 
				actuator_reload.Stop();
				break;

			/* pitch */	
			case 'd': 
				actuator_pitch.Retract();
				break;
			case 'e': 
				actuator_pitch.Extend();
				break;
			case 'f': 
				actuator_pitch.Stop();
				break;

      /* yaw */ 
      case 'g': 
        actuator_yaw.Retract();
        break;
      case 'h': 
        actuator_yaw.Extend();
        break;
      case 'i': 
        actuator_yaw.Stop();
        break;

			/* accel */
			case 'z':
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

