Robotics
========

This code is built and tested on OSX's Xamarin Studio and it is inteded to run on Lego Mindstorm EV3 Brick booted using MonoBrickFirmware.
Since MonoBrick runs on Ubuntu natively, it is a natural fit for NancyFX SelfHost module which is used as a simple REST service for remote control of the brick.

This code relies on:
* NancyFX: http://nancyfx.org/
* MonoBrickFirmware: http://www.monobrick.dk/software/ev3firmware/

For more details about self hosting NancyFx & Installing it on Xamarin & MonoBrick see:
* NancyFx - SelfHosting: https://github.com/NancyFx/Nancy/wiki/Self-Hosting-Nancy
* Native Nuget on Xamrin 3+! http://blog.xamarin.com/xamarin-studio-and-nuget/
* MonoBrick install tutorial: http://www.monobrick.dk/guides/monobrickfirmwaresdcardposix/
* MonoBrick programming tutorial: http://www.monobrick.dk/guides/ev3firmwareprogrammingguide/
* MonoBrick documentation: http://www.monobrick.dk/MonoBrickFirmwareDocumentation/annotated.html