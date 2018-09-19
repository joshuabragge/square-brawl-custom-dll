using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020001A7 RID: 423
public partial class Player : MonoBehaviour
{
	// Token: 0x06000964 RID: 2404 RVA: 0x0003F4C4 File Offset: 0x0003D6C4
	private void Update()
	{
		this.cooldown1 = this.cd1;
		this.cooldown2 = this.cd2;
		if (this.canLookJump)
		{
			this.rig.AddForce(Vector2.up * Time.deltaTime * 750f);
		}
		if (this.overHeat1 && this.extraCD1 > -0.07f && this.cd1 > this.CD1 + this.extraCD1 + 0.2f)
		{
			this.extraCD1 -= Time.deltaTime;
		}
		if (this.overHeat2 && this.extraCD2 > -0.07f && this.cd2 > this.CD2 + this.extraCD2 + 0.2f)
		{
			this.extraCD2 -= Time.deltaTime;
		}
		if (this.bot)
		{
			this.CheckTravel();
		}
		this.checkBoundaries();
		this.botChangeAim += Time.deltaTime;
		if ((this.canLookJump || this.bot) && (!this.botCanMove || !this.bot))
		{
			this.jumpHeight = 100f;
		}
		else
		{
			if (this.jumpHeight == 100f)
			{
				this.dangers = UnityEngine.Object.FindObjectsOfType<Danger>();
			}
			this.jumpHeight = 1000f;
		}
		if (this.dif < 4)
		{
			if (this.timeToSwitchFire < 0f)
			{
				if (this.timeToFire)
				{
					this.timeToSwitchFire = (float)UnityEngine.Random.Range(1 / this.dif, 5 / this.dif);
				}
				else
				{
					this.timeToSwitchFire = (float)UnityEngine.Random.Range(1 * this.dif, 3 * this.dif);
				}
				this.timeToFire = !this.timeToFire;
			}
			this.timeToSwitchFire -= Time.deltaTime;
		}
		else
		{
			this.timeToFire = true;
		}
		if (this.extraDamageShake > 0f)
		{
			this.extraDamageShake -= Time.deltaTime * 1f;
		}
		this.CDAim -= Time.deltaTime;
		if (!this.canLookJump)
		{
			this.lifeTime += Time.deltaTime;
		}
		this.lifeBox.transform.localScale = new Vector3(this.life / (float)this.m_startHealth, this.life / (float)this.m_startHealth, 1f);
		int num = 500;
		if (!this.bot && this.canMove)
		{
			if (this.actions.Left.Value > this.thresh)
			{
				if (!this.canLookJump)
				{
					if (Mathf.Abs(this.rig.velocity.x) < 500f)
					{
						this.rig.AddForce(Vector3.right * Time.deltaTime * -this.speed);
					}
					if (Mathf.Abs(this.rig.angularVelocity) < 250f)
					{
						this.rig.AddTorque(this.speed * Time.deltaTime / 5f);
					}
				}
				this.aimInt = 180;
			}
			if (this.actions.Right.Value > this.thresh)
			{
				if (!this.canLookJump)
				{
					if (Mathf.Abs(this.rig.velocity.x) < 500f)
					{
						this.rig.AddForce(Vector3.right * Time.deltaTime * this.speed);
					}
					if (Mathf.Abs(this.rig.angularVelocity) < 250f)
					{
						this.rig.AddTorque(this.speed * -Time.deltaTime / 5f);
					}
				}
				this.aimInt = 0;
			}
			if (this.actions.Jump.IsPressed && this.jumpTime < 0.2f && this.jumpCD > 0.25f)
			{
				this.rig.AddForce(Vector3.up * this.jumpHeight);
				this.jumpTime = 1f;
				this.jumpCD = 0f;
				this.myAudioSource.PlayOneShot(this.jump);
				if (!this.canLookJump)
				{
					if (this.actions.Left.Value > 0f)
					{
						this.rig.AddTorque(50f);
					}
					if (this.actions.Right.Value > 0f)
					{
						this.rig.AddTorque(-50f);
					}
				}
			}
			if (this.actions.Up.Value > this.thresh)
			{
				if (!this.canLookJump)
				{
					this.rig.AddForce(Vector3.up * (float)num * Time.deltaTime);
				}
				this.aimInt = -90;
				if (this.actions.Right.Value > this.thresh)
				{
					this.aimInt = -45;
				}
				if (this.actions.Left.Value > this.thresh)
				{
					this.aimInt = -135;
				}
			}
			if (this.actions.Down.Value > this.thresh)
			{
				if (!this.canLookJump)
				{
					this.rig.AddForce(Vector3.up * (float)(-(float)num) * 2f * Time.deltaTime);
				}
				this.aimInt = 90;
				if (this.actions.Right.Value > this.thresh)
				{
					this.aimInt = 45;
				}
				if (this.actions.Left.Value > this.thresh)
				{
					this.aimInt = 135;
				}
			}
		}
		else if (this.bot)
		{
			this.botStartTimer += Time.deltaTime;
			if (this.botStartTimer > 4f)
			{
				this.botCanMove = true;
			}
			if (this.botStartTimer > 4.5f)
			{
				this.botCanFire = true;
			}
			this.targetSwitchTime -= Time.deltaTime;
			if (this.botDodgeTimer > -0.1f)
			{
				this.botDodgeTimer -= Time.deltaTime;
			}
			if (this.botStateUpdate > -0.1f)
			{
				this.botStateUpdate -= Time.deltaTime;
			}
			if (this.point != null)
			{
				this.currentTargetPoint = this.point.position;
			}
			this.rightHit = Physics2D.Raycast(base.transform.position, Vector2.right, 1f, this.mapMask);
			this.leftHit = Physics2D.Raycast(base.transform.position, -Vector2.right, 1f, this.mapMask);
			this.upHit = Physics2D.Raycast(base.transform.position, Vector2.up, 1f, this.mapMask);
			this.downHit = Physics2D.Raycast(base.transform.position, -Vector2.up, 1f, this.mapMask);
			if (this.botCanMove)
			{
				this.voidCheck = Physics2D.Raycast(base.transform.position, -Vector2.up, 500f, this.safeMask);
				if (this.dangers != null)
				{
					foreach (Danger danger in this.dangers)
					{
						if (Vector3.Distance(danger.transform.position, base.transform.position) < 5f)
						{
							this.changeTargetPoint(danger.transform.position);
						}
					}
				}
				if (this.currentState == Player.botState.aggro)
				{
					Debug.DrawLine(base.transform.position, this.currentTargetPoint, Color.red);
				}
				else if (this.currentState == Player.botState.defense)
				{
					Debug.DrawLine(base.transform.position, this.currentTargetPoint, Color.yellow);
				}
				if ((Vector3.Distance(base.transform.position, new Vector3(-16f, base.transform.position.y, base.transform.position.z)) < 7f && !this.directionRight) || Vector3.Distance(base.transform.position, new Vector3(16f, base.transform.position.y, base.transform.position.z)) >= 7f || this.directionRight)
				{
				}
				if (this.voidCheck.collider == null || this.voidCheck.collider.tag == "kill")
				{
					this.panic = true;
					if (Vector3.Distance(base.transform.position, Physics2D.Raycast(base.transform.position, Vector2.left + Vector2.down, 500f, this.safeMask).point) < Vector3.Distance(base.transform.position, Physics2D.Raycast(base.transform.position, Vector2.right + Vector2.down, 500f, this.safeMask).point))
					{
						Debug.Log("Panic Moving LEFT!");
						this.directionRight = false;
						this.botMoveLeft();
					}
					else
					{
						Debug.Log("Panic Moving RIGHT!");
						this.directionRight = true;
						this.botMoveRight();
					}
				}
				else
				{
					this.panic = false;
				}
				if (this.currentTargetPoint.y < base.transform.position.y)
				{
					if (this.currentTargetPoint.x < base.transform.position.x)
					{
						if (this.leftHit.collider && this.downHit.collider)
						{
							if (!this.upHit.collider)
							{
								this.botJump();
							}
						}
						else if (!this.panic)
						{
							if (!this.leftHit.collider)
							{
								this.directionRight = false;
							}
							if (!this.directionRight)
							{
								this.botMoveLeft();
							}
						}
					}
					else if (this.rightHit.collider)
					{
						this.botJump();
					}
					else if (!this.panic)
					{
						if (this.leftHit.collider && !this.directionRight)
						{
							this.directionRight = true;
						}
						if (this.directionRight)
						{
							this.botMoveRight();
						}
						else
						{
							this.botMoveLeft();
						}
					}
				}
				else if (this.currentTargetPoint.y > base.transform.position.y)
				{
					if (this.currentTargetPoint.x > base.transform.position.x)
					{
						if (this.rightHit.collider)
						{
							if (!this.upHit.collider)
							{
								this.botJump();
							}
						}
						else
						{
							if (this.leftHit.collider && !this.directionRight)
							{
								if (UnityEngine.Random.Range(0, 2) > 0)
								{
									this.botJump();
								}
								else
								{
									this.directionRight = true;
								}
							}
							if (!this.panic)
							{
								if (this.directionRight)
								{
									this.botMoveRight();
								}
								else
								{
									this.botMoveLeft();
								}
							}
						}
					}
					else if (this.leftHit.collider)
					{
						if (!this.upHit.collider)
						{
							this.botJump();
						}
					}
					else
					{
						if (!this.leftHit.collider)
						{
							this.botJump();
							this.directionRight = false;
						}
						if (!this.panic && !this.directionRight)
						{
							this.botMoveLeft();
						}
					}
				}
				if (this.leftHit.collider != null)
				{
				}
				if (!(this.rightHit.collider != null) || this.rightHit.collider.transform.root.GetComponent<Player>() != null)
				{
				}
			}
			Collider2D[] array2 = Physics2D.OverlapCircleAll(base.transform.position, 50f, this.projectilePlayerMask);
			int num2 = 0;
			foreach (Collider2D collider2D in array2)
			{
				if (collider2D != null && collider2D.gameObject.layer != base.gameObject.layer && collider2D.transform.root.GetComponent<projectile>() && this.timeToFire && this.botCanFire && !this.panic)
				{
					if (this.fire1 == "shield")
					{
						if (Vector3.Distance(base.transform.position, collider2D.transform.position) < 5f && UnityEngine.Random.Range(0f, 1.1f) > this.sightThreshold * Time.deltaTime * 60f)
						{
							this.botFire(true);
							this.botDodgeTimer = 0.2f;
						}
					}
					else if (this.fire2 == "shield" && Vector3.Distance(base.transform.position, collider2D.transform.position) < 5f && UnityEngine.Random.Range(0f, 1.1f) > this.sightThreshold * Time.deltaTime * 60f)
					{
						this.botFire(false);
						this.botDodgeTimer = 0.2f;
					}
					if (this.botDodgeTimer <= 0f && Vector3.Distance(base.transform.position, collider2D.transform.position) < 10f - this.sightThreshold * 5f)
					{
						float num3 = UnityEngine.Random.Range(0f, 1.1f);
						if (num3 > this.sightThreshold)
						{
							this.botJump();
						}
						this.botDodgeTimer = 0.4f;
					}
				}
				if (collider2D != null && collider2D.gameObject.layer != base.gameObject.layer && collider2D.transform.GetComponent<triggerEffect>() && this.botDodgeTimer <= 0f && collider2D.transform.GetComponent<triggerEffect>().mine && Vector3.Distance(base.transform.position, collider2D.transform.position) < 5f)
				{
					float num4 = UnityEngine.Random.Range(0f, 1.1f);
					if (num4 > this.sightThreshold)
					{
						this.botJump();
					}
					this.botDodgeTimer = 0.4f;
				}
				if (collider2D != null && collider2D.transform.GetComponent<Player>() != null && collider2D.transform != base.transform && this.botCanFire && collider2D.transform.GetComponent<Player>().team != this.team)
				{
					if (collider2D.transform != this.otherPlayer)
					{
						this.otherPlayer = collider2D.transform;
					}
					if (Vector3.Distance(base.transform.position, collider2D.transform.position) < 2f)
					{
						if (this.fire1 == "shield")
						{
							this.botFire(true);
						}
						if (this.fire2 == "shield")
						{
							this.botFire(false);
						}
						if (UnityEngine.Random.Range(0, 2) > 0 && this.botDodgeTimer <= 0f)
						{
							this.botJump();
							this.botDodgeTimer = 0.4f;
						}
					}
					Vector3 value = collider2D.transform.position - base.transform.position;
					Vector3 vector = base.transform.position + Vector3.Normalize(value) * 0.5f;
					RaycastHit2D raycastHit2D = Physics2D.Raycast(vector, new Vector2(value.x, value.y), 100f, this.myMask);
					if (raycastHit2D.transform != base.transform && raycastHit2D.transform.tag == "player")
					{
						if (raycastHit2D.transform.GetComponent<Player>())
						{
							if (this.sightCounter < this.sightThreshold + 0.4f)
							{
								this.sightCounter += Time.deltaTime;
							}
							if (this.botChangeAim > 0.3f)
							{
								this.botChangeAim = 0f;
								this.previousCollider = num2;
								if (value.normalized.x > 0.8f)
								{
									this.aimInt = 0;
								}
								else if (value.normalized.x < -0.8f)
								{
									this.aimInt = 180;
								}
								else if (value.normalized.y > 0.8f)
								{
									this.aimInt = -90;
								}
								else if (value.normalized.y < -0.8f)
								{
									this.aimInt = 90;
								}
								else if (value.normalized.x > 0f && value.normalized.y > 0f)
								{
									this.aimInt = -45;
								}
								else if (value.normalized.x < 0f && value.normalized.y < 0f)
								{
									this.aimInt = 135;
								}
								else if (value.normalized.x < 0f && value.normalized.y > 0f)
								{
									this.aimInt = -135;
								}
								else if (value.normalized.x > 0f && value.normalized.y < 0f)
								{
									this.aimInt = -45;
								}
								num2++;
							}
						}
					}
					else if (this.previousCollider == -1 && this.sightCounter > 0f)
					{
						this.sightCounter -= Time.deltaTime;
					}
				}
			}
			if (num2 == 0)
			{
				this.previousCollider = -1;
			}
		}
		this.jumpTime += Time.deltaTime;
		this.jumpCD += Time.deltaTime;
		this.cd1 += Time.deltaTime;
		this.cd2 += Time.deltaTime;
		this.damageColor -= Time.deltaTime;
		if (this.lifeTime > 2f)
		{
			if (this.damageColor > 0f)
			{
				foreach (SpriteRenderer spriteRenderer in base.gameObject.GetComponentsInChildren<SpriteRenderer>())
				{
					spriteRenderer.color = new Color(1f, 1f, 1f);
				}
			}
			else
			{
				foreach (SpriteRenderer spriteRenderer2 in base.gameObject.GetComponentsInChildren<SpriteRenderer>())
				{
					SpriteRenderer spriteRenderer3 = spriteRenderer2;
					Color color = new Color(this.color.r + this.freezeTime / 2f, this.color.g + this.freezeTime / 2f, this.color.b + this.freezeTime / 2f, 1f);
					spriteRenderer2.color = color;
					spriteRenderer3.color = color;
					if (spriteRenderer2.transform == base.transform)
					{
						spriteRenderer2.color = new Color(this.color.r + this.freezeTime / 2f, this.color.g + this.freezeTime / 2f, this.color.b + this.freezeTime / 2f, 0.5f);
					}
				}
			}
		}
		else
		{
			this.blinkCounter++;
			if (this.blinkCounter > 15)
			{
				this.blinkCounter = 0;
			}
			foreach (SpriteRenderer spriteRenderer4 in base.gameObject.GetComponentsInChildren<SpriteRenderer>())
			{
				if (this.blinkCounter < 12)
				{
					spriteRenderer4.color = new Color(this.color.r, this.color.g, this.color.b, 1f);
				}
				else
				{
					spriteRenderer4.color = new Color(1f, 1f, 1f, 0f);
				}
			}
		}
	}
}
