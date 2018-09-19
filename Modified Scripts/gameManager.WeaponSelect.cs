// gameManager
// Token: 0x060009CE RID: 2510 RVA: 0x0004A838 File Offset: 0x00048A38
public void WeaponSelect(GameObject weaponButton, int player)
{
	if (weaponButton.tag == "weapon" && !this.dontAccepInput)
	{
		Debug.LogError(string.Concat(new object[]
		{
			"PLAYER: ",
			player,
			"WEAPON: ",
			this.weaponNumber
		}));
		switch (player)
		{
		case 1:
			if (this.w11 == string.Empty)
			{
				this.w11 = weaponButton.name;
				Debug.LogError(string.Concat(new object[]
				{
					"Player ",
					player,
					" Weapon 1: ",
					this.w11
				}));
				this.cd11 = weaponButton.GetComponent<cooldown>().cd;
				this.pb11 = weaponButton.GetComponent<cooldown>().pushback;
				this.follow11 = weaponButton.GetComponent<cooldown>().followPlayer;
				this.setPlayerWeaponMarkers(0);
			}
			else if (this.w21 == string.Empty && weaponButton.name != this.w11)
			{
				this.w21 = weaponButton.name;
				Debug.LogError(string.Concat(new object[]
				{
					"Player ",
					player,
					" Weapon 2: ",
					this.w21
				}));
				this.cd21 = weaponButton.GetComponent<cooldown>().cd;
				this.pb21 = weaponButton.GetComponent<cooldown>().pushback;
				this.follow21 = weaponButton.GetComponent<cooldown>().followPlayer;
				this.setPlayerWeaponMarkers(0, true);
				this.currentPlayer++;
				Debug.Log("currentPlayer: " + this.currentPlayer);
			}
			break;
		case 2:
			if (this.w12 == string.Empty)
			{
				this.w12 = weaponButton.name;
				Debug.LogError(string.Concat(new object[]
				{
					"Player ",
					player,
					" Weapon 1: ",
					this.w12
				}));
				this.cd12 = weaponButton.GetComponent<cooldown>().cd;
				this.pb12 = weaponButton.GetComponent<cooldown>().pushback;
				this.follow12 = weaponButton.GetComponent<cooldown>().followPlayer;
				this.setPlayerWeaponMarkers(1);
			}
			else if (this.w22 == string.Empty && weaponButton.name != this.w12)
			{
				this.w22 = weaponButton.name;
				Debug.LogError(string.Concat(new object[]
				{
					"Player ",
					player,
					" Weapon 2: ",
					this.w22
				}));
				this.cd22 = weaponButton.GetComponent<cooldown>().cd;
				this.pb22 = weaponButton.GetComponent<cooldown>().pushback;
				this.follow22 = weaponButton.GetComponent<cooldown>().followPlayer;
				this.setPlayerWeaponMarkers(1, true);
				this.currentPlayer++;
			}
			break;
		case 3:
			if (this.w13 == string.Empty)
			{
				this.w13 = weaponButton.name;
				this.cd13 = weaponButton.GetComponent<cooldown>().cd;
				this.pb13 = weaponButton.GetComponent<cooldown>().pushback;
				this.follow13 = weaponButton.GetComponent<cooldown>().followPlayer;
				this.setPlayerWeaponMarkers(2);
			}
			else if (this.w23 == string.Empty && weaponButton.name != this.w13)
			{
				this.w23 = weaponButton.name;
				this.cd23 = weaponButton.GetComponent<cooldown>().cd;
				this.pb23 = weaponButton.GetComponent<cooldown>().pushback;
				this.follow23 = weaponButton.GetComponent<cooldown>().followPlayer;
				this.setPlayerWeaponMarkers(2, true);
				this.currentPlayer++;
			}
			break;
		case 4:
			if (this.w14 == string.Empty)
			{
				this.w14 = weaponButton.name;
				this.cd14 = weaponButton.GetComponent<cooldown>().cd;
				this.pb14 = weaponButton.GetComponent<cooldown>().pushback;
				this.follow14 = weaponButton.GetComponent<cooldown>().followPlayer;
				this.setPlayerWeaponMarkers(3);
			}
			else if (this.w24 == string.Empty && weaponButton.name != this.w14)
			{
				this.w24 = weaponButton.name;
				this.cd24 = weaponButton.GetComponent<cooldown>().cd;
				this.pb24 = weaponButton.GetComponent<cooldown>().pushback;
				this.follow24 = weaponButton.GetComponent<cooldown>().followPlayer;
				this.setPlayerWeaponMarkers(3, true);
				this.currentPlayer++;
			}
			break;
		}
		Debug.Log(this.currentPlayer + " PLAYERS READY!");
		this.playerColor(this.currentPlayer);
		foreach (Button button in GameObject.Find("menu5").GetComponentsInChildren<Button>())
		{
		}
		if (this.currentPlayer > this.players)
		{
			Debug.Log(4);
			this.m_ButtonSound.submitSound();
			this.menuState(true);
			this.currentPlayer = 1;
			if (!this.currentGameMode._oneLife)
			{
				this.pointsToWin = 10000;
			}
		}
	}
}
