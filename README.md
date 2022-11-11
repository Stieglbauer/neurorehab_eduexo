# neurorehab_eduexo

The Arduino script we used can be found in ArduinoScripts/UnityRead.c/UnityCom.c/UnityCom.c.ino (yeah we accidently saved the directories with a .c file ending... and now we are too scared to change it back).
To switch between emg-controlled mode and force-controlled mode, set the useemg variable to 1 or 0 respectively.

The Unity project can be found in EduExoGame/EduExo_UnityGame.
All the c# scripts are written by us + all assets have been created by us. The only thing not originally created by us are the three sound effects which are downloaded from the web via https://mixkit.co/free-sound-effects/game/ (but we applied some effects on them to make them more fitting to the game).

Data logged during our sessions can be found in DataAnalysis/. There you can also find a script we programmed to process the data into more convenient data (transformer.py). E.g., it combines information into buckets of a given time-span or extracts only grab data.

The build is not usable, since many parts of it were too big to be uploaded to the repository. So to use the programm, Unity has to be used.

To use the program, upload the Arduino script from above to the micro controller of the exoskeleton. Then connect the microcontroller to the PC via USB. Start the game and calibrate the exoskeleton after setting the port correctly in the calibration menu. Once everything is calibrated, start the play scene and setup the game. Then press start. The game should log all information of the session automatically.