using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D7 RID: 471
public partial class damageOnce : MonoBehaviour
{
	// Token: 0x06000AAB RID: 2731 RVA: 0x00056AF8 File Offset: 0x00054CF8
	private void Update()
	{
		if (this.useTime)
		{
			this.lifeTime -= Time.deltaTime;
		}
		if (!this.useTime || this.lifeTime > 0f)
		{
			Debug.Log(this.lifeTime);
			foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(base.transform.position, this.size))
			{
				if (collider2D.transform.root.gameObject.layer != base.transform.root.gameObject.layer && !this.targets.Contains(collider2D.transform))
				{
					if (collider2D.transform.root.tag == "projectile" && collider2D.transform.root.GetComponent<projectile>().team != base.transform.root.GetComponent<setColor>().team)
					{
						collider2D.transform.parent.GetComponent<projectile>().TakeDamage(50f, base.transform);
					}
					if (collider2D.transform.tag == "player")
					{
						bool flag = true;
						if (this.useTime)
						{
							this.myMask = ~(1 << base.gameObject.layer);
							string[] layerNames = new string[]
							{
								"team1",
								"team2",
								"team3",
								"team4",
								"map",
								"mapMove"
							};
							LayerMask mask = LayerMask.GetMask(layerNames);
							this.myMask &= mask;
							if (Physics2D.Raycast(base.transform.root.position, Vector3.Normalize(collider2D.transform.position - base.transform.root.position), Vector3.Distance(base.transform.root.position, collider2D.transform.position), this.myMask).transform != collider2D.transform)
							{
								flag = false;
							}
						}
						if (collider2D.gameObject.GetComponent<Player>().team != base.transform.root.GetComponent<setColor>().team && flag)
						{
							if (this.effect != string.Empty)
							{
								GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(this.effect), base.transform.root.position, base.transform.root.rotation);
								gameObject.SendMessage("color", base.transform.root.GetComponent<setColor>().savedColor);
								gameObject.layer = base.transform.root.gameObject.layer;
							}
							this.targets.Add(collider2D.transform);
							if (this.ParticleDamage > 0f)
							{
								collider2D.gameObject.GetComponent<Rigidbody2D>().AddForce(base.transform.root.forward * this.force);
							}
							else
							{
								collider2D.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize(collider2D.transform.position - base.transform.position) * this.force);
							}
							collider2D.gameObject.GetComponent<Player>().TakeDamage(this.damage, base.transform.root.GetComponent<setColor>().team, base.transform.root.forward);
							Camera.main.SendMessage("setShake", this.damage / 100f);
						}
					}
				}
			}
		}
	}
}
