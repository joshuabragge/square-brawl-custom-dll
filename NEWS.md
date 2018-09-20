<!-- -*- mode: markdown; fill-column: 8192 -*- -->

## v1.1

*2018-09-19*

Added character aim direction to features being output. Character aim is being stored as a quaternion - need to find a way to make it a bit more interpretable.

TODO:
* Grab all projectiles on screen and writeout data structure - limit of two projectiles per color?
* use gamemanager.SpawnPlayer to setup a local TCP client to send data through.
* use gamemanager.Update to send TCP client updates.
* Add the features currently being output for player one but for player two as well.
* Confirm if game has ability to transform quanternion variables into more simplistic data structure - will it have to be done in python?

*2018-09-18*

Increased points to win from 10 to 10,000 - variable was in gameManager.WeaponSelect (who would have guessed?)

Collecting more variables from the game to write out. Most of the additions are defaults from the player class
 * (x,y,z) coordinates - z is not relevant for this two dimensional game values seem to range from -10 to 10 for both x and y.
 * canfire - boolean switches to False on spawn - can be used to measure death since health doesn't update to zero
 * aim.position - same coordinates as player. Found somewhere else in the code aim.roation - might be actual character direction?
 * CD1 and CD2 - static cooldown parameters for the two weapons.
 * canLookJump - not 100% certain but has to do with something on spawn during immunity - not really relevant to what I need anyway.
 * canMove - boolean - self explanatory.
 * fire1/fire2 - actual name of the weapons assigned to the character.
 * cooldown1/cooldown2 - custom public class inserted into the player class to copy the value of the private cooldown timers. Player private cooldown timers reset upon firing weapons. Once the timer is greater than the static cooldown of the weapon, the weapon can be fired again.
 
 Other items to explore:
 * projectiles class - I believe the update function has the data I need to grab all the projectiles on screen along with their x and y coordinates. Will probably have to stick to *one shot*kind of weapons (sniper, revolver, boom?). How would I represent a shot guns multiple bullets in a way that makes sense for a machine?
 * damageOnce class - has other hints for how to accomblish the results described above.
 
TODO:
* Grab all projectiles on screen and writeout data structure - limit of two projectiles per color?
* ~Write out character aim direction.~
* use gamemanager.SpawnPlayer to setup a local TCP client to send data through.
* use gamemanager.Update to send TCP client updates.


## v1.0

*2018-09-10*

Added initial proof of concept for writing out data from the video game square brawl.

* gameManager.SpawnPlayer generates the player's and bot's characters, assigns weapons, controls, cooldowns, colors, etc.
* MODIFIED gameManager.SpawnPlayer gives the unity gameObject character a unique name depending on the player's position (1 to 4).
* By giving the unity gameObject a unique name the character's health and other qualities can be read later.
* gameManager.Update is responsible for handling actions, screen updates, etc and everything else that needs to happen from frame-to-frame.
* MODIFIED gameManager.Update reads the character's health and color and writes it to a text file in a fixed location.

TODO:
* ~Investigate character x and y coordinates.~
* How to grab projectiles and represent data structure.
* use SpawnPlayer to setup a local TCP client to send data through.
* use Update to character updates.
