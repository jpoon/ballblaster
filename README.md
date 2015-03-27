# ballblaster 

![action jackson](https://github.com/jpoon/ballblaster/blob/master/media/ballblaster.gif)

Hackathon project to control an [8 ball launcher](http://www.tshirtgun.com/8balllauncher.html). The launcher is set on a platform and connected to several actuators and a solenoid to allow it to control the cannon's pitch, yaw, reload mechanism, and trigger. The mechanical parts are controlled by the Arduino Yun which exposes an API that is consumed by the OpenCV program that tracks our target. 

## Team
* [@stefangordon](https://github.com/stefangordon)
* [@jamesbak](https://github.com/jamesbak)
* [@obsoleted](https://github.com/obsoleted)
* [@haishibai](https://github.com/HaishiBai)
* Myself

## Parts

* [8 Ball Launcher](http://www.tshirtgun.com/8balllauncher.html)
* [Arduino Yun](http://arduino.cc/en/Main/ArduinoBoardYun)
* 3x Linear Actuators (for pitch, yaw, reload)
* 1x Solenoid
* 2x Relay
* HD Camera

## Structure

* **arduino** contains the firmware that lives on the Arduino ATmega32u4.
* **client** contains a basic test windows form app that communicates to the arduino over the serial port

