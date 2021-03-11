# Fader

General purpose fader to fade Materials, Enviro, etc in Unity

Example Setup:

![setup](https://user-images.githubusercontent.com/10963432/110688054-783fbd80-81e1-11eb-8f16-ef542a81a9d9.png)

In my case I was toying around with the new Malbers Realistic Wolf.
The eyes use the Standard material, property _EmissionColor as color. 
The fur is a special shader with a property _EmissionPower as float. 

Looks like this then:

![magic wolf](https://user-images.githubusercontent.com/10963432/110688835-4e3acb00-81e2-11eb-84cf-e16a9603710f.gif)

Note: Unity also has a working material lerp mechanism:

https://docs.unity3d.com/ScriptReference/Material.Lerp.html

This video clip shows how it looks with the Material and Enviro fader applied:

[![](https://img.youtube.com/vi/A6mdaOySVQM/0.jpg)](https://www.youtube.com/watch?v=A6mdaOySVQM)

If you need a custom fader, I suggest to check out EnviroTimeFader.cs, the implementation should be straightforward. 
Please also note that for the Enviro fader to work you need to have the ENVIRO scripting define symbol. I just added it so that you don't get compiler errors in case you don't have Enviro.