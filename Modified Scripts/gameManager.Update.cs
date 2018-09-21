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
	
	// Token: 0x060009AA RID: 2474
	private void Update()
	{
				try
		{
			this.playerOne = GameObject.Find("PlayerOne");
			this.playerOneLife = this.playerOne.GetComponent<Player>().life.ToString();
			this.playerOneColor = ColorUtility.ToHtmlStringRGBA(this.playerOne.GetComponent<Player>().color);
			this.playerOnePosition = this.playerOne.transform.position.ToString();
			this.playerOneCanFire = this.playerOne.GetComponent<Player>().canFire.ToString();
			this.playerOneAimPosition = this.playerOne.GetComponent<Player>().aim.position.ToString();
			this.playerOneAimRotation = this.playerOne.GetComponent<Player>().aim.rotation.eulerAngles.ToString();
			this.playerOneCD1 = this.playerOne.GetComponent<Player>().CD1.ToString();
			this.playerOneCD2 = this.playerOne.GetComponent<Player>().CD2.ToString();
			this.playerOneCDAim = this.playerOne.GetComponent<Player>().CDAim.ToString();
			this.playerOneCanLookJump = this.playerOne.GetComponent<Player>().canLookJump.ToString();
			this.playerOneCanMove = this.playerOne.GetComponent<Player>().canMove.ToString();
			this.playerOneFire1 = this.playerOne.GetComponent<Player>().fire1;
			this.playerOneFire2 = this.playerOne.GetComponent<Player>().fire2;
			this.playerOneCoolDown1 = this.playerOne.GetComponent<Player>().cooldown1.ToString();
			this.playerOneCoolDown2 = this.playerOne.GetComponent<Player>().cooldown2.ToString();
		}
		catch
		{
		}
		try
		{
			this.playerTwo = GameObject.Find("PlayerTwo");
			this.playerTwoLife = this.playerTwo.GetComponent<Player>().life.ToString();
			this.playerTwoColor = ColorUtility.ToHtmlStringRGBA(this.playerTwo.GetComponent<Player>().color);
			this.playerTwoPosition = this.playerTwo.transform.position.ToString();
			this.playerTwoCanFire = this.playerOne.GetComponent<Player>().canFire.ToString();
			this.playerTwoAimPosition = this.playerOne.GetComponent<Player>().aim.position.ToString();
			this.playerTwoAimRotation = this.playerOne.GetComponent<Player>().aim.rotation.eulerAngles.ToString();
			this.playerTwoCD1 = this.playerOne.GetComponent<Player>().CD1.ToString();
			this.playerTwoCD2 = this.playerOne.GetComponent<Player>().CD2.ToString();
			this.playerTwoCDAim = this.playerOne.GetComponent<Player>().CDAim.ToString();
			this.playerTwoCanLookJump = this.playerOne.GetComponent<Player>().canLookJump.ToString();
			this.playerTwoCanMove = this.playerOne.GetComponent<Player>().canMove.ToString();
			this.playerTwoFire1 = this.playerOne.GetComponent<Player>().fire1;
			this.playerTwoFire2 = this.playerOne.GetComponent<Player>().fire2;
			this.playerTwoCoolDown1 = this.playerOne.GetComponent<Player>().cooldown1.ToString();
			this.playerTwoCoolDown2 = this.playerOne.GetComponent<Player>().cooldown2.ToString();
		}
		catch
		{
		}
		try
		{
			string playerOneRow = string.Concat(new string[]
			{
				"P1,",
				this.playerOneColor,
				",",
				this.playerOneLife,
				",",
				this.playerOnePosition,
				",",
				this.playerOneCanFire,
				",",
				this.playerOneAimPosition,
				",",
				this.playerOneCD1,
				",",
				this.playerOneCD2,
				",",
				this.playerOneCDAim,
				",",
				this.playerOneCanLookJump,
				",",
				this.playerOneCanMove,
				",",
				this.playerOneFire1,
				",",
				this.playerOneFire2,
				",",
				this.playerOneCoolDown1,
				",",
				this.playerOneCoolDown2,
				",",
				this.playerOneAimRotation
			});
			string playerTwoRow = string.Concat(new string[]
			{
				"P2,",
				this.playerTwoColor,
				",",
				this.playerTwoLife,
				",",
				this.playerTwoPosition,
				",",
				this.playerTwoCanFire,
				",",
				this.playerTwoAimPosition,
				",",
				this.playerTwoCD1,
				",",
				this.playerTwoCD2,
				",",
				this.playerTwoCDAim,
				",",
				this.playerTwoCanLookJump,
				",",
				this.playerTwoCanMove,
				",",
				this.playerTwoFire1,
				",",
				this.playerTwoFire2,
				",",
				this.playerTwoCoolDown1,
				",",
				this.playerTwoCoolDown2,
				",",
				this.playerTwoAimRotation
			});
			string dateTime = DateTime.Now.ToString("yyyy-MM-ddTHH':'mm':'sszzz");
			this.rowResults = string.Concat(new string[]
			{
				dateTime,
				",",
				playerOneRow,
				",",
				playerTwoRow
			});
		}
		catch
		{
		}
		try
		{
			File.AppendAllText("C:\\Users\\Jimmy\\Documents\\Projects\\Square Brawl Decompiled\\update.txt", this.rowResults + "\n ");
		}
		catch
		{
		}
		this.gameManLifeTime += Time.deltaTime;
		if (this.playing)
		{
			this.playTime += Time.deltaTime;
		}
		foreach (BindingSource bindingSource in this.keyboardListener2.Up.Bindings)
		{
			UnityEngine.Debug.Log(bindingSource.Name);
		}
		if (!this.menu4.activeInHierarchy && !this.menu2.activeInHierarchy && EventSystem.current.currentSelectedGameObject == null)
		{
			EventSystem.current.SetSelectedGameObject(this._back_button);
		}
		if (this.menu2.activeInHierarchy)
		{
			this.TickPlayerDeviceDelay();
			if (this.selectableGameObjects[0].tag != "color")
			{
				this.selectableGameObjects = new GameObject[GameObject.FindGameObjectsWithTag("color").Length + 1];
				GameObject[] array = GameObject.FindGameObjectsWithTag("color");
				for (int i = 0; i < array.Length; i++)
				{
					this.selectableGameObjects[i] = array[i];
				}
				this.selectableGameObjects[this.selectableGameObjects.Length - 1] = this._back_button;
				UnityEngine.Debug.Log(this.selectableGameObjects[this.selectableGameObjects.Length - 1].name);
			}
			if (this.canCheckDevice)
			{
				this.checkPlayerInput();
			}
			InputDevice activeDevice = InputManager.ActiveDevice;
			if (activeDevice.AnyButton.WasPressed)
			{
				UnityEngine.Debug.Log("Joystick Pressed!");
				if (!this.DeviceAlreadyAssigned(activeDevice) && this.playerControllers.Count < this.players)
				{
					if (this.playerControllers.Count == 0)
					{
						UnityEngine.Debug.LogError("First Insert!");
						this.m_ButtonSound.submitSound();
						base.StartCoroutine(this.DeviceAddDelay());
						this.playerControllers.Add(new gameManager.PlayerInstance(activeDevice));
					}
					else
					{
						bool flag = false;
						for (int j = 0; j < this.playerControllers.Count; j++)
						{
							if (this.playerControllers[j] == null)
							{
								UnityEngine.Debug.LogError("Inserting at index: " + j);
								base.StartCoroutine(this.DeviceAddDelay());
								this.playerControllers[j] = new gameManager.PlayerInstance(activeDevice);
								this.m_ButtonSound.submitSound();
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							UnityEngine.Debug.LogError("Else Inserting!");
							base.StartCoroutine(this.DeviceAddDelay());
							this.playerControllers.Add(new gameManager.PlayerInstance(activeDevice));
							this.m_ButtonSound.submitSound();
						}
					}
					base.StartCoroutine(this.VibrateInSeconds(activeDevice, 0.5f, 10f));
					UnityEngine.Debug.Log("New Device assigned to Player: " + (this.playerControllers.IndexOf(this.findPlayerWithDevice(activeDevice)) + 1).ToString() + " With Device: " + activeDevice.Name);
					this.playerPointersGobj[this.playerControllers.IndexOf(this.findPlayerWithDevice(activeDevice))] = this.menu2.transform.FindChild("p" + (this.playerControllers.IndexOf(this.findPlayerWithDevice(activeDevice)) + 1).ToString()).gameObject;
					float num = 0.152941182f;
					this.playerPointersGobj[this.playerControllers.IndexOf(this.findPlayerWithDevice(activeDevice))].GetComponent<Image>().color = new Color(num, num, num);
					this.updatePlayerPointersPosition();
					this.playerPointersGobj[this.playerControllers.IndexOf(this.findPlayerWithDevice(activeDevice))].SetActive(true);
				}
			}
			if (this.keyboardListener.Fire1.WasPressed || this.keyboardListener2.Fire1.WasPressed || this.keyboardListener.Fire2.WasPressed || this.keyboardListener2.Fire2.WasPressed)
			{
				bool flag2 = false;
				if (this.keyboardListener.Submit.WasPressed)
				{
					UnityEngine.Debug.Log("Keyboard 1 Press");
					flag2 = true;
				}
				else if (this.keyboardListener2.Submit.WasPressed)
				{
					UnityEngine.Debug.Log("Keyboard 2 Press");
					flag2 = false;
				}
				if (!this.DeviceAlreadyAssigned(flag2) && this.playerControllers.Count <= this.players)
				{
					if (this.playerControllers.Count == 0)
					{
						this.m_ButtonSound.submitSound();
						base.StartCoroutine(this.DeviceAddDelay());
						this.playerControllers.Add(new gameManager.PlayerInstance(flag2));
					}
					else
					{
						bool flag3 = false;
						for (int k = 0; k < this.playerControllers.Count; k++)
						{
							if (this.playerControllers[k] == null)
							{
								UnityEngine.Debug.LogError("Inserting at index: " + k);
								base.StartCoroutine(this.DeviceAddDelay());
								this.canCheckDevice = false;
								this.playerControllers[k] = new gameManager.PlayerInstance(flag2);
								this.m_ButtonSound.submitSound();
								flag3 = true;
								break;
							}
						}
						if (!flag3)
						{
							UnityEngine.Debug.LogError("Else Inserting!");
							base.StartCoroutine(this.DeviceAddDelay());
							this.canCheckDevice = false;
							this.playerControllers.Add(new gameManager.PlayerInstance(flag2));
							this.m_ButtonSound.submitSound();
						}
					}
					UnityEngine.Debug.Log("New Keyboard assigned to Player: " + (this.playerControllers.IndexOf(this.findPlayerWithDevice(flag2)) + 1).ToString());
					this.playerPointersGobj[this.playerControllers.IndexOf(this.findPlayerWithDevice(flag2))] = this.menu2.transform.FindChild("p" + (this.playerControllers.IndexOf(this.findPlayerWithDevice(flag2)) + 1).ToString()).gameObject;
					float num2 = 0.152941182f;
					this.playerPointersGobj[this.playerControllers.IndexOf(this.findPlayerWithDevice(flag2))].GetComponent<Image>().color = new Color(num2, num2, num2);
					this.updatePlayerPointersPosition();
					this.playerPointersGobj[this.playerControllers.IndexOf(this.findPlayerWithDevice(flag2))].SetActive(true);
				}
				else
				{
					UnityEngine.Debug.Log("PlayerController.Count() = " + this.playerControllers.Count);
				}
			}
			if (this.playerControllers.Count != 0)
			{
				this.updatePlayerPointers();
			}
		}
		if (this.menu4.activeInHierarchy)
		{
			if (EventSystem.current.currentSelectedGameObject != this._back_button)
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
			this.TickPlayerDeviceDelay();
			this.checkPlayerInput();
			if (this.selectableGameObjects[0].tag != "weapon")
			{
				this.selectableGameObjects = new GameObject[GameObject.FindGameObjectsWithTag("weapon").Length + 1];
				GameObject[] array2 = GameObject.FindGameObjectsWithTag("weapon");
				for (int l = 0; l < array2.Length; l++)
				{
					this.selectableGameObjects[l] = array2[l];
				}
				this.selectableGameObjects[this.selectableGameObjects.Length - 1] = this._back_button;
				this.resetPlayerPointers();
				for (int m = 0; m < this.teams.Length; m++)
				{
					UnityEngine.Debug.Log(string.Concat(new object[]
					{
						"Player: ",
						m + 1,
						" In Team: ",
						this.teams[m]
					}));
				}
				foreach (gameManager.PlayerInstance item in this.playerControllers)
				{
					this.playerPointersGobj[this.playerControllers.IndexOf(item)] = this.menu4.transform.FindChild("p" + (this.playerControllers.IndexOf(item) + 1).ToString()).gameObject;
					switch (this.playerControllers.IndexOf(item))
					{
					case 0:
						this.playerPointersGobj[this.playerControllers.IndexOf(item)].GetComponent<Image>().color = this.c1;
						break;
					case 1:
						this.playerPointersGobj[this.playerControllers.IndexOf(item)].GetComponent<Image>().color = this.c2;
						break;
					case 2:
						this.playerPointersGobj[this.playerControllers.IndexOf(item)].GetComponent<Image>().color = this.c3;
						break;
					case 3:
						this.playerPointersGobj[this.playerControllers.IndexOf(item)].GetComponent<Image>().color = this.c4;
						break;
					}
					this.playerPointersGobj[this.playerControllers.IndexOf(item)].SetActive(true);
				}
			}
			this.updatePlayerWeaponPointers();
		}
		this.color1 = this.c1;
		this.color2 = this.c2;
		this.color3 = this.c3;
		if (Application.isEditor && Input.GetKeyDown(KeyCode.Alpha0))
		{
			this.addScore(1, 0);
		}
		if (!this.matchOver && this.currentGameMode != null && !this.currentGameMode._oneLife)
		{
			if (this.score1 >= this.pointsToWin)
			{
				this.win(1, true);
				this.winText.color = this.c1;
			}
			if (this.score2 >= this.pointsToWin)
			{
				this.win(2, true);
				this.winText.color = this.c2;
			}
			if (this.score3 >= this.pointsToWin)
			{
				this.win(3, true);
				this.winText.color = this.c3;
			}
			if (this.score4 >= this.pointsToWin)
			{
				this.win(4, true);
				this.winText.color = this.c4;
			}
		}
		if (this.matchOver)
		{
			this.winCounter += Time.deltaTime;
			if (this.winCounter > 0.5f && this.anim.GetInteger("state") == 0)
			{
				if (this.currentGameMode._oneLife)
				{
					base.StartCoroutine(this.scoreAwards());
				}
				else
				{
					this.anim.SetInteger("state", 2);
					this.Swoosh();
				}
			}
			if ((double)this.winCounter > 1.5)
			{
				GameObject[] array3 = GameObject.FindGameObjectsWithTag("player");
				for (int n = 0; n < array3.Length; n++)
				{
					UnityEngine.Object.Destroy(array3[n]);
				}
			}
			if (this.winCounter > 4f && !this.currentGameMode._oneLife)
			{
				base.StartCoroutine(this.WaitForInput());
			}
		}
		int num3 = 1;
		while ((float)num3 <= (float)this.players + this.botSlider.value)
		{
			if (this.playing)
			{
				if (this.playerRespawns[num3] > 0f)
				{
					this.playerRespawns[num3] += Time.deltaTime;
				}
				if (this.currentGameMode != null && !this.currentGameMode._oneLife && this.playerRespawns[num3] > (float)this.respawnTime)
				{
					this.playerRespawns[num3] = 0f;
					if (num3 > this.players)
					{
						this.SpawnPlayer(num3, true);
					}
					else
					{
						this.SpawnPlayer(num3, false);
					}
				}
			}
			if (this.matchOver)
			{
				this.playerRespawns[num3] = 0f;
			}
			num3++;
		}
		if (this.currentGameMode != null && this.currentGameMode._oneLife)
		{
			int num4 = 0;
			float[] array4 = this.playerRespawns;
			for (int num5 = 0; num5 < array4.Length; num5++)
			{
				if (array4[num5] > 0f)
				{
					num4++;
				}
			}
			if ((float)num4 == (float)this.players + this.botSlider.value - 1f)
			{
				UnityEngine.Debug.LogError("ALL WORMS END UP DEAD");
				this.win(this.latestScore, false);
				switch (this.latestScore)
				{
				case 1:
					this.winText.color = this.c1;
					break;
				case 2:
					this.winText.color = this.c2;
					break;
				case 3:
					this.winText.color = this.c3;
					break;
				case 4:
					this.winText.color = this.c4;
					break;
				}
			}
			else if (this.currentGameMode._teamBased && UnityEngine.Object.FindObjectsOfType<Player>().Length > 1 && !this.matchOver)
			{
				int num6 = 0;
				bool flag4 = false;
				foreach (Player player in UnityEngine.Object.FindObjectsOfType<Player>())
				{
					if (num6 == 0)
					{
						num6 = player.team;
					}
					else if (num6 != player.team)
					{
						flag4 = true;
					}
				}
				if (!flag4)
				{
					this.win(this.latestScore, false);
				}
			}
		}
		if (InputManager.ActiveDevice.MenuWasPressed || Input.GetKeyDown(KeyCode.Escape))
		{
			if (this.state == 6)
			{
				this.PauseMenu();
			}
			else
			{
				this.restart(true);
			}
		}
		if (this.currentPlayer == 1 && (!gameManager.onlineMode || this.state != 2))
		{
			this.localTurn = true;
		}
		if (this.currentPlayer == 2 && (!gameManager.onlineMode || this.state != 2))
		{
			UnityEngine.Object.FindObjectOfType<StandaloneInputModule>().horizontalAxis = "hm2";
			UnityEngine.Object.FindObjectOfType<StandaloneInputModule>().verticalAxis = "vm2";
			UnityEngine.Object.FindObjectOfType<StandaloneInputModule>().submitButton = "vc2";
			this.localTurn = true;
		}
		if (this.currentPlayer == 3 && (!gameManager.onlineMode || this.state != 2))
		{
			UnityEngine.Object.FindObjectOfType<StandaloneInputModule>().horizontalAxis = "hm3";
			UnityEngine.Object.FindObjectOfType<StandaloneInputModule>().verticalAxis = "vm3";
			UnityEngine.Object.FindObjectOfType<StandaloneInputModule>().submitButton = "vc3";
			this.localTurn = true;
		}
		if (this.currentPlayer == 4 && ((gameManager.onlineMode && this.c4 == Color.white) || !gameManager.onlineMode || this.state != 2))
		{
			UnityEngine.Object.FindObjectOfType<StandaloneInputModule>().horizontalAxis = "hm4";
			UnityEngine.Object.FindObjectOfType<StandaloneInputModule>().verticalAxis = "vm4";
			UnityEngine.Object.FindObjectOfType<StandaloneInputModule>().submitButton = "vc4";
			this.localTurn = true;
		}
	}
}
