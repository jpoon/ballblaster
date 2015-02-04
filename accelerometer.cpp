#include "Arduino.h"
#include "accelerometer.h"

#define ADC_AMPLITUDE 1024	//amplitude of the 10bit-ADC of Arduino is 1024LSB
#define ADC_REF 3			//ADC reference is 5v
#define ZERO_X  1.22		//accleration of X-AXIS is 0g, the voltage of X-AXIS is 1.22v
#define ZERO_Y  1.22		//
#define ZERO_Z  1.25		//
#define SENSITIVITY 0.25	//sensitivity of X/Y/Z axis is 0.25v/g

Accelerometer::Accelerometer(int x_pin, int y_pin, int z_pin)
{
	_x_pin = x_pin;
	_y_pin = y_pin;
	_z_pin = z_pin;
}

void Accelerometer::calibrate()
{

}

void Accelerometer::getXYZ(int16_t* x, int16_t* y, int16_t* z)
{
	*x = analogRead(_x_pin);
	*y = analogRead(_y_pin);
	*z = analogRead(_z_pin);
}

void Accelerometer::getAcceleration(float *ax, float *ay, float *az)
{
	int x, y, z;
	float xvoltage, yvoltage, zvoltage;

	getXYZ(&x, &y, &z);

	xvoltage = (float)x*ADC_REF / ADC_AMPLITUDE;
	yvoltage = (float)y*ADC_REF / ADC_AMPLITUDE;
	zvoltage = (float)z*ADC_REF / ADC_AMPLITUDE;

	*ax = (xvoltage - ZERO_X) / SENSITIVITY;
	*ay = (yvoltage - ZERO_Y) / SENSITIVITY;
	*az = (zvoltage - ZERO_Z) / SENSITIVITY;
}