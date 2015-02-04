#ifndef Pulse_h
#define Pulse_h

#include "Arduino.h"

class Pulse
{
public:
	Pulse(int pin);
	void Fire();
private:
	int _pin;
};

#endif