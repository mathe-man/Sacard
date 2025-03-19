
# Sacard Physic Engine  -   Sacard PE

## Presentation
Sacard Physic Engine (or Sacard PE and also SPE) is small physic engine which can simulate the gravitation of multiple celestial bodies.

## Installation
First download the file corresponding to your Operating System and architecture.
Move this file in a new folder and run it for the first time (Read the [Initialization](###Initialization) section)

## Running
Here SacardPE-XX is an installation file from the latest release, replace it with your file name.
### Linux
Go into Sacard file properties and make sure that execution as a program is enabled.

Now you can try double click or right-click> Run.
But I recommend you to run it with a command line:
Open a terminal in the directory or use cd to get in and then run
```bash
./SacardPE-XX   #replace with your file name
```

### Window
You can simply go into the file's folder and **double-click** on it or **right-click > Run**.
Or open a terminal in the directory then enter
```bash
./start SacardPE-XX.exe   #replace with your file name
```

### Initialization
After you run the program for the first time he will initialize (init) and a few files and folders will be generated (More info about them in the Files section). You should avoid deleting them.

## Documentation
***A complete documentation should be added really soon.***<br>

Read the documentation to have a better understanding of the terms used in the suit.

## Configuration

After init, a file named "config.json" will be next to the program file. The default content of this file is:
```json
{
  "asInitied": "true",
  "useDefaultEnvironment": "false",
  "useDefaultObjects": "true",
  "defaultEnvironment": "init/space.env",
  "defaultObjects": "init/ListA.objs"
}
```
This goal of this section is to explain each of these properties

### asInitied
If the config file doesn't exist or this property is not true then the program will init.
It can be usefully to regenerate default files.

### useDefaultEnvironment
If it's true, then the simulation will use the default environment specified below.<br>
If it's false, then the program will ask the user to enter an environment file path.


### useDefaultObjects
It acts like the useDefaultEnvironment but with the simulation objects.

### defaultEnvironment and defaultObjects
Specify the environment and objects file path to use as default.



## Bug
List of the bugs to fix <br>
- [Fixed?] Name: Description
- [ ] Terminal Only: Program doesn't launch by double-click when he should ask the user for path (Objects and env).
- [ ] Ultra speed: Calculation error when objects are in the exact same position.
- [x] Collision bug.
