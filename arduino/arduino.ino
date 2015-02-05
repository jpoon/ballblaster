#include "actuator.h"
#include "pulse.h"
#include "accelerometer.h"
#include "reloadActuator.h"

#include <avr/io.h>
#include <avr/interrupt.h>

// accelerometer
#define ADXL_X A0
#define ADXL_Y A1
#define ADXL_Z A2

// limit switches
#define RELOAD_LIMIT_PIN 3

// trigger relay
Pulse trigger(6);

// actuator
ReloadActuator actuator_reload(9, 10);
Actuator actuator_pitch(11, 12);
Actuator actuator_yaw(7, 8);

Accelerometer accelerometer(ADXL_X, ADXL_Y, ADXL_Z);

void actuator_reload_limit_change() 
{
    static unsigned long last_interrupt_time = 0;
    unsigned long interrupt_time = millis();

    int buttonState = digitalRead(RELOAD_LIMIT_PIN);
    if (buttonState == 1) {
		unsigned int debounceTime = 250;

		int actuatorStatus = actuator_reload.GetState();
		switch(actuatorStatus) {
		case 0: //stopped
			break;

		case 1: //extend
			break;

		case 2: //retract
			debounceTime = 750;
			break;
		}

        if (interrupt_time - last_interrupt_time > debounceTime)
        {
            actuator_reload.LimitHit();
        }
        last_interrupt_time = interrupt_time;
    }
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

    pinMode(RELOAD_LIMIT_PIN, INPUT_PULLUP);
}

void loop()
{
    char cmd;
    while (Serial.available()) 
    {
        cmd = (char)Serial.read();
        switch (cmd) 
        {
        // reload
        case 'a': 
            actuator_reload.Retract();
            break;
        case 'b': 
            actuator_reload.Extend();
            break;
        case 'c': 
            actuator_reload.Stop();
            break;
        case 'd':
            detachInterrupt(0);
            actuator_reload.Reload();
            delay(750);
            attachInterrupt(0, &actuator_reload_limit_change, CHANGE);
            break;

        // pitch
        case 'g': 
            actuator_pitch.Retract();
            break;
        case 'h': 
            actuator_pitch.Extend();
            break;
        case 'i': 
            actuator_pitch.Stop();
            break;

        // yaw
        case 'l': 
            actuator_yaw.Retract();
            break;
        case 'm': 
            actuator_yaw.Extend();
            break;
        case 'n': 
            actuator_yaw.Stop();
            break;

        // trigger
        case 'q':
            trigger.Fire();
            break;

        // accel
        case 'z':
            int x, y, z;
            float xvoltage, yvoltage, zvoltage;
            accelerometer.getXYZ(&x, &y, &z);
            accelerometer.getAcceleration(&xvoltage, &yvoltage, &zvoltage);
           
			/* 
            Serial.println("xyz:");
            Serial.println(x);
            Serial.println(y);
            Serial.println(z);
            Serial.println("voltage:");
            Serial.println(xvoltage);
            Serial.println(yvoltage);
            Serial.println(zvoltage);
			*/
            break;
        }
    }
}

