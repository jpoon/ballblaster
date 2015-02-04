#include "Arduino.h"
#include "actuator.h"

Actuator::Actuator(int pin1, int pin2)
{
	_pin1 = pin1;
	_pin2 = pin2;

	pinMode(_pin1, OUTPUT);
	pinMode(_pin2, OUTPUT);

	digitalWrite(_pin1, HIGH);
	digitalWrite(_pin2, HIGH);
}

void Actuator::Extend()
{
	digitalWrite(_pin1, HIGH);
	digitalWrite(_pin2, LOW);
}

void Actuator::Retract()
{
	digitalWrite(_pin1, LOW);
	digitalWrite(_pin2, HIGH);
}

void Actuator::Stop()
{
	digitalWrite(_pin1, HIGH);
	digitalWrite(_pin2, HIGH);
}
