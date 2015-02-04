#include "Arduino.h"
#include "reloadActuator.h"

ReloadActuator::ReloadActuator(int pin1, int pin2) : Actuator(pin1, pin2)
{
	// status
	//	0 = stopped
	//	1 = extend
	//	2 = release
	status = 0;
}

void ReloadActuator::Reload() {
	status = 1;
	Extend();
}

void ReloadActuator::LimitHit() {
	switch (status) {
	case 0:
		break;

	case 1: 
		status = 2;
		Retract();
		break;

	case 2:
		status = 0;
		Stop();
		break;
	}
}
