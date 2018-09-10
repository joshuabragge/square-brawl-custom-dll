<!-- -*- mode: markdown; fill-column: 8192 -*- -->

## v1.0

*2018-09-10*

Added initial proof of concept for writing out data from the video game square brawl.

* gameManager.SpawnPlayer generates the player's and bot's characters, assigns weapons, controls, cooldowns, colors, etc.
* MODIFIED gameManager.SpawnPlayer gives the unity gameObject character a unique name depending on the player's position (1 to 4).
* By giving the unity gameObject a unique name the character's health and other qualities can be read later.
* gameManager.Update is responsible for handling actions, screen updates, etc and everything else that needs to happen from frame-to-frame.
* MODIFIED gameManager.Update reads the character's health and color and writes it to a text file in a fixed location.

TODO:
* Investigate character x and y coordinates
* How to grab projectiles and represent data structure
* use SpawnPlayer to setup a local TCP client to send data through
* use Update to character updates