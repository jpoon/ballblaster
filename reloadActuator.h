#ifndef ReloadActuator_h
#define ReloadActuator_h

#include "Arduino.h"
#include "actuator.h"

class ReloadActuator : public Actuator {
public:
	ReloadActuator(int pin1, int pin2);
	void Reload();
	void LimitHit();
	int GetState();
private:
	volatile int status;
};

#endif