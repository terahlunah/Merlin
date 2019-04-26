# Merlin (Not maintained anymore)

An experimental Modding API for Kingdoms and Castles

Limited functionality at the moment, looking for contributors !

## Quick Install Guide

(You can find a detailled install guide in the downloaded zip file)

To install Merlin :
- Download the [lastest version](https://github.com/terahxluna/Merlin/releases)
- Unzip it
- Launch MerlinPatcher
- Find the K&C root folder
- Patch it !

To install mods :
- You can find my mod pack here : [TerahModPack](https://github.com/terahxluna/TerahModPack)
- Copy the mod .dll file to the "Mods" folder inside K&C root folder

## Creating Mods and Contributing

### Creating Mods

- Create a new Class Library Project
- Reference these Assemblies (you can find them in "C:/Path/to/Kingdom and Castles/KingdomsAndCastles_Data/Managed/) :
    - Merlin.dll
    - Assembly-CSharp.dll
    - UnityEngine.dll
    - (Optional, if you need it) UnityEngine.UI.dll
- Create a public class inheriting MerlinMod (using Merlin;)
    - MerlinMod inherits MonoBehavior so you have access to MonoBehavior methods)
    - Look at [TerahModPack](https://github.com/terahxluna/TerahModPack) for examples
- Just copy your dll in the "Mods" folder (in K&C root folder) and it will be loaded when the game starts

### Contributing to Merlin

Merlin functionalities are limited at the moment, if you need a specific functionnality, simply create an Issue.

If you want to help fix Issues and Contribute to Merlin, create a pull request on this repository.

Look at Merlin and MerlinPatcher code to get started, more documentation will come when I find time to work on it.
