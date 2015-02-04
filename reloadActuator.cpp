#include "Arduino.h"
#include "reloadActuator.h"

ReloadActuator::ReloadActuator(int pin1, int pin2) : Actuator(pin1, pin2)
{
	// status
	//	0 = stopped
	//	1 = extend
	//	2 = retract 
	status = 0;
}

void ReloadActuator::Reload() {
	status = 2;
	Retract();
}

int ReloadActuator::GetState() {
	return status;
}

void ReloadActuator::LimitHit() {
	switch (status) {
	case 0: //stopped
		break;

	case 1: //extend
		status = 0;
		Stop();
		break;

	case 2: //retract
		status = 1;
		Extend();
		break;
	}
}
