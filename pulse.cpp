#include "Arduino.h"
#include "pulse.h"

Pulse::Pulse(int pin)
{
	_pin = pin;

	pinMode(_pin, OUTPUT);

	digitalWrite(_pin, HIGH);
}

void Pulse::Fire()
{
	digitalWrite(_pin, LOW);

	delay(500);

	digitalWrite(_pin, HIGH);
}
