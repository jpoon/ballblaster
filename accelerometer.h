#ifndef Accelerometer_h
#define Accelerometer_h

#include "Arduino.h"

class Accelerometer
{
public:
	Accelerometer(int x_pin, int y_pin, int z_pin);
	void calibrate();
	void getXYZ(int16_t* x, int16_t* y, int16_t* z);
	void getAcceleration(float *ax, float *ay, float *az);
private:
	int _x_pin;
	int _y_pin;
	int _z_pin;
};

#endif