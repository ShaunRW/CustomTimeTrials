# Custom Time Trials v0.4.0 #
Adds a new time trial racing mode to GTA V. Also includes an editor to create the time trials.


### Requires: ###

* ScriptHookV
* ScriptHookVdotnet
* NativeUI


### Installation: ###

Copy the following items into the 'Grand Theft Auto V/scripts' directory.
* CustomTimeTrials.dll file.
* Newtonsoft.json.dll file.


### How to use: ###

* Once in-game, press F10 to access the menu. from here you can select the time trial you wish to start, or if you wish to create your own time trial using the editor.
* While in a time trial, press F10 to access the in race menu. From here you can exit or restart the time trial.


### Todo: ###

##### Planned for v1.0.0 #####
* Add Full Race Results and Parameters to the finish notification.
* Upgrade to ScriptHookVdotnet3

##### Planned: #####
* Add Race Option for police. eg, Disabled, Allowed, Always [1-5] star.

##### Ideas: #####
* Add traffic levels to the Traffic option. eg, Off, Light, Moderate, heavy, etc
* Disable missions while racing. Not sure if this is possible.


### Known Bugs: ###

* With traffic set to off, a very small number of vehicles still spawn. Might need to remove them on the fly every so many frames.



### Change Log ###

##### v0.4.0 #####
* Moved TimeTrials folder to the Documents folder to move them out of the protected GTA V directory.

##### v0.3.1 #####
* Fixed issue where Fastest Time and Fastest Lap Time was getting stored correctly for sprint time trials.

##### v0.3.0 #####
* Fastest Time and Fastest Lap time is now stored and updatable

##### v0.2.0 #####
* Current Lap Time and Fastest Lap Time are now displayed on the HUD.
