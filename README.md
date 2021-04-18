# Fader

### Description

Fader allows you to lerp between values and colors for a given duration and toggle lerp direction.

Specific implementations can e. g. fade Materials, Enviro Time, VFX Spawn Rate, etc in and out in Unity.


### Example Implementations

* Material Property
  - Fade Float and Color Properties of Materials
* Material Object
  - Fade two material using Unity's internal [Material.Lerp](https://docs.unity3d.com/ScriptReference/Material.Lerp.html) function
* VFX
  - Fade VFX property values
* Enviro
  - Fade Enviro's time in and out. This requires the scripting define symbol `ENVIRO` to be defined
* Aura 2
  - Fade the Aura 2 Camera and Fade the Aura 2 Volume


### Example Setup

![setup](https://user-images.githubusercontent.com/10963432/110688054-783fbd80-81e1-11eb-8f16-ef542a81a9d9.png)

In my case I was toying around with the new Malbers Realistic Wolf.
The eyes use the Standard material, property `_EmissionColor` as color. 
The fur is a special shader with a property `_EmissionPower` as float. 

Looks like this then:

![magic wolf](https://user-images.githubusercontent.com/10963432/110688835-4e3acb00-81e2-11eb-84cf-e16a9603710f.gif)

This video clip shows how it looks with both the Material Property and Enviro Fader applied:

[![](https://img.youtube.com/vi/A6mdaOySVQM/0.jpg)](https://www.youtube.com/watch?v=A6mdaOySVQM)

If you need a custom fader, I suggest to check out VFXFader.cs or EnviroTimeFader.cs. The implementation should be straightforward.
