using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using InControl;
using Photon;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020001B1 RID: 433
public partial class gameManager : Photon.MonoBehaviour
{
	// Token: 0x060009C4 RID: 2500
	private void SpawnPlayer(int i, bool bot)
	{
		File.AppendAllText("C:\\Users\\Jimmy\\Documents\\Projects\\Square Brawl Decompiled\\SpawnPlayer.txt", "Spawning int " + i.ToString() + " \n");
		this.spawnCounter++;
		UnityEngine.Debug.Log("Spawn Counter: " + this.spawnCounter);
		GameObject gameObject = base.gameObject;
		int num = i;
		Vector3 vector = new Vector3((float)UnityEngine.Random.Range(-10, 10), (float)UnityEngine.Random.Range(-7, 7), 0f);
		RaycastHit2D raycastHit2D = Physics2D.Raycast(vector, Vector3.down, 50f, this.mapMask);
		RaycastHit2D raycastHit2D2 = Physics2D.Raycast(new Vector3(vector.x - 0.5f, vector.y, vector.z), Vector3.down, 50f, this.mapMask);
		RaycastHit2D raycastHit2D3 = Physics2D.Raycast(new Vector3(vector.x + 0.5f, vector.y, vector.z), Vector3.down, 50f, this.mapMask);
		if ((raycastHit2D.collider && Vector3.Distance(vector, raycastHit2D.point) > 1f && raycastHit2D.transform.tag != "kill" && this.canSpawnPlayers && raycastHit2D2.collider && raycastHit2D3.collider) || this.spawnCounter > 1000)
		{
			if (this.spawnCounter <= 100)
			{
				UnityEngine.Debug.Log("SPAWN POINT! " + raycastHit2D.collider.name);
			}
			if (bot)
			{
				InputModuleActions actions = InputModuleActions.CreateNullActions();
				gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("player"), vector, Quaternion.identity);
				gameObject.GetComponent<Player>().setActions(actions);
				gameObject.GetComponent<Player>().bot = true;
				gameObject.GetComponent<Player>().ChangeDifficulty((int)this.botDifSlider.value);
				if (this.currentGameMode._teamBased)
				{
					gameObject.GetComponent<Player>().team = this.teams[i - 1];
				}
				else
				{
					gameObject.GetComponent<Player>().team = i;
				}
				if (this.zombie)
				{
					gameObject.GetComponent<Player>().life = 10f;
					gameObject.GetComponent<Player>().zombie = true;
					num = 5;
				}
				if (i == 1)
				{
					gameObject.name = "PlayerOne";
				}
				if (i == 2)
				{
					gameObject.name = "PlayerTwo";
				}
				if (i == 3)
				{
					gameObject.name = "PlayerThree";
				}
				if (i == 4)
				{
					gameObject.name = "PlayerFour";
				}
			}
			else
			{
				InputModuleActions.CreateActions().Device = this.playerControllers[i - 1].getDevice();
				gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("player"), vector, Quaternion.identity);
				if (this.currentGameMode._teamBased)
				{
					gameObject.GetComponent<Player>().team = this.teams[i - 1];
				}
				else
				{
					gameObject.GetComponent<Player>().team = i;
				}
				gameObject.GetComponent<Player>().setActions(this.playerControllers[i - 1].getActions());
				if (i == 1)
				{
					gameObject.name = "PlayerOne";
				}
				if (i == 2)
				{
					gameObject.name = "PlayerTwo";
				}
				if (i == 3)
				{
					gameObject.name = "PlayerThree";
				}
				if (i == 4)
				{
					gameObject.name = "PlayerFour";
				}
			}
			if (gameManager.onlineMode)
			{
				num = this.playersInLobby[i - 1];
			}
			gameObject.SendMessage("setControls", i);
			switch (num)
			{
			case 1:
				gameObject.SendMessage("setColor", this.c1);
				if (!bot)
				{
					gameObject.GetComponent<Player>().fire1 = this.w11;
					gameObject.GetComponent<Player>().CD1 = this.cd11;
					gameObject.GetComponent<Player>().fire2 = this.w21;
					gameObject.GetComponent<Player>().CD2 = this.cd21;
					gameObject.GetComponent<Player>().pushback1 = this.pb11;
					gameObject.GetComponent<Player>().pushback2 = this.pb21;
					gameObject.GetComponent<Player>().follow1 = this.follow11;
					gameObject.GetComponent<Player>().follow2 = this.follow21;
				}
				else
				{
					gameManager.weapon weapon = this.botWeapons[UnityEngine.Random.Range(0, this.botWeapons.Length)];
					gameManager.weapon weapon2 = this.botWeapons[UnityEngine.Random.Range(0, this.botWeapons.Length)];
					gameObject.GetComponent<Player>().fire1 = weapon.name;
					gameObject.GetComponent<Player>().CD1 = weapon.coolDown;
					gameObject.GetComponent<Player>().fire2 = weapon2.name;
					gameObject.GetComponent<Player>().CD2 = weapon2.coolDown;
					gameObject.GetComponent<Player>().pushback1 = weapon.PushBack;
					gameObject.GetComponent<Player>().pushback2 = weapon2.PushBack;
					gameObject.GetComponent<Player>().follow1 = weapon.follow;
					gameObject.GetComponent<Player>().follow2 = weapon2.follow;
				}
				break;
			case 2:
				gameObject.SendMessage("setColor", this.c2);
				if (!bot)
				{
					UnityEngine.Debug.LogError("HELLO!" + this.w12);
					gameObject.GetComponent<Player>().fire1 = this.w12;
					gameObject.GetComponent<Player>().CD1 = this.cd12;
					gameObject.GetComponent<Player>().fire2 = this.w22;
					gameObject.GetComponent<Player>().CD2 = this.cd22;
					gameObject.GetComponent<Player>().pushback1 = this.pb12;
					gameObject.GetComponent<Player>().pushback2 = this.pb22;
					gameObject.GetComponent<Player>().follow1 = this.follow12;
					gameObject.GetComponent<Player>().follow2 = this.follow22;
				}
				else if (this.w12 == string.Empty)
				{
					gameManager.weapon weapon3 = this.botWeapons[UnityEngine.Random.Range(0, 10)];
					gameManager.weapon weapon4 = this.botWeapons[UnityEngine.Random.Range(0, 10)];
					gameObject.GetComponent<Player>().fire1 = weapon3.name;
					gameObject.GetComponent<Player>().CD1 = weapon3.coolDown;
					gameObject.GetComponent<Player>().fire2 = weapon4.name;
					gameObject.GetComponent<Player>().CD2 = weapon4.coolDown;
					gameObject.GetComponent<Player>().pushback1 = weapon3.PushBack;
					gameObject.GetComponent<Player>().pushback2 = weapon4.PushBack;
					gameObject.GetComponent<Player>().follow1 = weapon3.follow;
					gameObject.GetComponent<Player>().follow2 = weapon4.follow;
					UnityEngine.Debug.Log("bot WEAPONS: " + weapon3.name + "    AND:    " + weapon4.name);
					this.w12 = weapon3.name;
					this.cd12 = weapon3.coolDown;
					this.w22 = weapon4.name;
					this.cd22 = weapon4.coolDown;
					this.pb12 = weapon3.PushBack;
					this.pb22 = weapon4.PushBack;
					this.follow12 = weapon3.follow;
					this.follow22 = weapon4.follow;
				}
				else
				{
					gameObject.GetComponent<Player>().fire1 = this.w12;
					gameObject.GetComponent<Player>().CD1 = this.cd12;
					gameObject.GetComponent<Player>().fire2 = this.w22;
					gameObject.GetComponent<Player>().CD2 = this.cd22;
					gameObject.GetComponent<Player>().pushback1 = this.pb12;
					gameObject.GetComponent<Player>().pushback2 = this.pb22;
					gameObject.GetComponent<Player>().follow1 = this.follow12;
					gameObject.GetComponent<Player>().follow2 = this.follow22;
				}
				break;
			case 3:
				gameObject.SendMessage("setColor", this.c3);
				if (!bot)
				{
					gameObject.GetComponent<Player>().fire1 = this.w13;
					gameObject.GetComponent<Player>().CD1 = this.cd13;
					gameObject.GetComponent<Player>().fire2 = this.w23;
					gameObject.GetComponent<Player>().CD2 = this.cd23;
					gameObject.GetComponent<Player>().pushback1 = this.pb13;
					gameObject.GetComponent<Player>().pushback2 = this.pb23;
					gameObject.GetComponent<Player>().follow1 = this.follow13;
					gameObject.GetComponent<Player>().follow2 = this.follow23;
				}
				else if (this.w13 == string.Empty)
				{
					gameManager.weapon weapon5 = this.botWeapons[UnityEngine.Random.Range(0, 10)];
					gameManager.weapon weapon6 = this.botWeapons[UnityEngine.Random.Range(0, 10)];
					gameObject.GetComponent<Player>().fire1 = weapon5.name;
					gameObject.GetComponent<Player>().CD1 = weapon5.coolDown;
					gameObject.GetComponent<Player>().fire2 = weapon6.name;
					gameObject.GetComponent<Player>().CD2 = weapon6.coolDown;
					gameObject.GetComponent<Player>().pushback1 = weapon5.PushBack;
					gameObject.GetComponent<Player>().pushback2 = weapon6.PushBack;
					UnityEngine.Debug.Log("bot WEAPONS: " + weapon5.name + "    AND:    " + weapon6.name);
					gameObject.GetComponent<Player>().follow1 = weapon5.follow;
					gameObject.GetComponent<Player>().follow2 = weapon6.follow;
					this.w13 = weapon5.name;
					this.cd13 = weapon5.coolDown;
					this.w23 = weapon6.name;
					this.cd23 = weapon6.coolDown;
					this.pb13 = weapon5.PushBack;
					this.pb23 = weapon6.PushBack;
					this.follow13 = weapon5.follow;
					this.follow23 = weapon6.follow;
				}
				else
				{
					gameObject.GetComponent<Player>().fire1 = this.w13;
					gameObject.GetComponent<Player>().CD1 = this.cd13;
					gameObject.GetComponent<Player>().fire2 = this.w23;
					gameObject.GetComponent<Player>().CD2 = this.cd23;
					gameObject.GetComponent<Player>().pushback1 = this.pb13;
					gameObject.GetComponent<Player>().pushback2 = this.pb23;
					gameObject.GetComponent<Player>().follow1 = this.follow13;
					gameObject.GetComponent<Player>().follow2 = this.follow23;
				}
				break;
			case 4:
				gameObject.SendMessage("setColor", this.c4);
				if (!bot)
				{
					gameObject.GetComponent<Player>().fire1 = this.w14;
					gameObject.GetComponent<Player>().CD1 = this.cd14;
					gameObject.GetComponent<Player>().fire2 = this.w24;
					gameObject.GetComponent<Player>().CD2 = this.cd24;
					gameObject.GetComponent<Player>().pushback1 = this.pb14;
					gameObject.GetComponent<Player>().pushback2 = this.pb24;
					gameObject.GetComponent<Player>().follow1 = this.follow14;
					gameObject.GetComponent<Player>().follow2 = this.follow24;
				}
				else if (this.w14 == string.Empty)
				{
					gameManager.weapon weapon7 = this.botWeapons[UnityEngine.Random.Range(0, 10)];
					gameManager.weapon weapon8 = this.botWeapons[UnityEngine.Random.Range(0, 10)];
					gameObject.GetComponent<Player>().fire1 = weapon7.name;
					gameObject.GetComponent<Player>().CD1 = weapon7.coolDown;
					gameObject.GetComponent<Player>().fire2 = weapon8.name;
					gameObject.GetComponent<Player>().CD2 = weapon8.coolDown;
					gameObject.GetComponent<Player>().pushback1 = weapon7.PushBack;
					gameObject.GetComponent<Player>().pushback2 = weapon8.PushBack;
					gameObject.GetComponent<Player>().follow1 = weapon7.follow;
					gameObject.GetComponent<Player>().follow2 = weapon8.follow;
					this.w14 = weapon7.name;
					this.cd14 = weapon7.coolDown;
					this.w24 = weapon8.name;
					this.cd24 = weapon8.coolDown;
					this.pb14 = weapon7.PushBack;
					this.pb24 = weapon8.PushBack;
					this.follow14 = weapon7.follow;
					this.follow24 = weapon8.follow;
					UnityEngine.Debug.Log("bot WEAPONS: " + weapon7.name + "    AND:    " + weapon8.name);
				}
				else
				{
					gameObject.GetComponent<Player>().fire1 = this.w14;
					gameObject.GetComponent<Player>().CD1 = this.cd14;
					gameObject.GetComponent<Player>().fire2 = this.w24;
					gameObject.GetComponent<Player>().CD2 = this.cd24;
					gameObject.GetComponent<Player>().pushback1 = this.pb14;
					gameObject.GetComponent<Player>().pushback2 = this.pb24;
					gameObject.GetComponent<Player>().follow1 = this.follow14;
					gameObject.GetComponent<Player>().follow2 = this.follow24;
				}
				break;
			case 5:
				gameObject.SendMessage("setColor", Color.black);
				gameObject.GetComponent<Player>().fire1 = "shield";
				gameObject.GetComponent<Player>().CD1 = 3f;
				gameObject.GetComponent<Player>().fire2 = "shield";
				gameObject.GetComponent<Player>().CD2 = 3f;
				gameObject.GetComponent<Player>().pushback1 = 0f;
				gameObject.GetComponent<Player>().pushback2 = 0f;
				break;
			}
			if (this.activeGameModifiers.Contains(gameManager.gameModifier.SnipeBattle))
			{
				gameManager.weapon weapon9 = this.botWeapons[4];
				gameObject.GetComponent<Player>().fire1 = weapon9.name;
				gameObject.GetComponent<Player>().CD1 = weapon9.coolDown;
				gameObject.GetComponent<Player>().fire2 = weapon9.name;
				gameObject.GetComponent<Player>().CD2 = weapon9.coolDown;
				gameObject.GetComponent<Player>().pushback1 = weapon9.PushBack;
				gameObject.GetComponent<Player>().pushback2 = weapon9.PushBack;
				gameObject.GetComponent<Player>().follow1 = false;
				gameObject.GetComponent<Player>().follow2 = false;
			}
			if (this.activeGameModifiers.Contains(gameManager.gameModifier.RandomWeapons))
			{
				gameManager.weapon weapon10 = this.botWeapons[UnityEngine.Random.Range(0, 10)];
				gameManager.weapon weapon11 = this.botWeapons[UnityEngine.Random.Range(0, 10)];
				gameObject.GetComponent<Player>().fire1 = weapon10.name;
				gameObject.GetComponent<Player>().CD1 = weapon10.coolDown;
				gameObject.GetComponent<Player>().fire2 = weapon11.name;
				gameObject.GetComponent<Player>().CD2 = weapon11.coolDown;
				gameObject.GetComponent<Player>().pushback1 = weapon10.PushBack;
				gameObject.GetComponent<Player>().pushback2 = weapon11.PushBack;
				gameObject.GetComponent<Player>().follow1 = weapon10.follow;
				gameObject.GetComponent<Player>().follow2 = weapon11.follow;
			}
			if (this.activeGameModifiers.Contains(gameManager.gameModifier.FistFight))
			{
				gameObject.GetComponent<Player>().fire1 = "shield";
				gameObject.GetComponent<Player>().CD1 = 3f;
				gameObject.GetComponent<Player>().fire2 = "charge";
				gameObject.GetComponent<Player>().CD2 = 3f;
				gameObject.GetComponent<Player>().pushback1 = 0f;
				gameObject.GetComponent<Player>().pushback2 = 0f;
				gameObject.GetComponent<Player>().follow1 = true;
				gameObject.GetComponent<Player>().follow2 = true;
			}
			return;
		}
		this.SpawnPlayer(i, bot);
	}
}
