#ifndef Actuator_h
#define Actuator_h

#include "Arduino.h"

class Actuator
{
public:
	Actuator(int pin1, int pin2);
	void Extend();
	void Retract();
	void Stop();
private:
	int _pin1;
	int _pin2;
};

#endif