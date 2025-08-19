using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent;

namespace Terraria
{
	// Token: 0x02000030 RID: 48
	public class CombatText
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x0003D678 File Offset: 0x0003B878
		public static int NewText(Rectangle location, Color color, int amount, bool dramatic = false, bool dot = false)
		{
			return CombatText.NewText(location, color, amount.ToString(), dramatic, dot);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0003D68C File Offset: 0x0003B88C
		public static int NewText(Rectangle location, Color color, string text, bool dramatic = false, bool dot = false)
		{
			if (Main.netMode == 2)
			{
				return 100;
			}
			for (int i = 0; i < 100; i++)
			{
				if (!Main.combatText[i].active)
				{
					int num = 0;
					if (dramatic)
					{
						num = 1;
					}
					Vector2 vector = FontAssets.CombatText[num].Value.MeasureString(text);
					Main.combatText[i].alpha = 1f;
					Main.combatText[i].alphaDir = -1;
					Main.combatText[i].active = true;
					Main.combatText[i].scale = 0f;
					Main.combatText[i].rotation = 0f;
					Main.combatText[i].position.X = (float)location.X + (float)location.Width * 0.5f - vector.X * 0.5f;
					Main.combatText[i].position.Y = (float)location.Y + (float)location.Height * 0.25f - vector.Y * 0.5f;
					CombatText combatText = Main.combatText[i];
					combatText.position.X = combatText.position.X + (float)Main.rand.Next(-(int)((double)location.Width * 0.5), (int)((double)location.Width * 0.5) + 1);
					CombatText combatText2 = Main.combatText[i];
					combatText2.position.Y = combatText2.position.Y + (float)Main.rand.Next(-(int)((double)location.Height * 0.5), (int)((double)location.Height * 0.5) + 1);
					Main.combatText[i].color = color;
					Main.combatText[i].text = text;
					Main.combatText[i].velocity.Y = -7f;
					if (Main.player[Main.myPlayer].gravDir == -1f)
					{
						CombatText combatText3 = Main.combatText[i];
						combatText3.velocity.Y = combatText3.velocity.Y * -1f;
						Main.combatText[i].position.Y = (float)location.Y + (float)location.Height * 0.75f + vector.Y * 0.5f;
					}
					Main.combatText[i].lifeTime = 60;
					Main.combatText[i].crit = dramatic;
					Main.combatText[i].dot = dot;
					if (dramatic)
					{
						Main.combatText[i].text = text;
						Main.combatText[i].lifeTime *= 2;
						CombatText combatText4 = Main.combatText[i];
						combatText4.velocity.Y = combatText4.velocity.Y * 2f;
						Main.combatText[i].velocity.X = (float)Main.rand.Next(-25, 26) * 0.05f;
						Main.combatText[i].rotation = (float)(Main.combatText[i].lifeTime / 2) * 0.002f;
						if (Main.combatText[i].velocity.X < 0f)
						{
							Main.combatText[i].rotation *= -1f;
						}
					}
					if (dot)
					{
						Main.combatText[i].velocity.Y = -4f;
						Main.combatText[i].lifeTime = 40;
					}
					return i;
				}
			}
			return 100;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0003D9C4 File Offset: 0x0003BBC4
		public static void clearAll()
		{
			for (int i = 0; i < 100; i++)
			{
				Main.combatText[i].active = false;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0003D9EB File Offset: 0x0003BBEB
		public static float TargetScale
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0003D9F4 File Offset: 0x0003BBF4
		public void Update()
		{
			if (this.active)
			{
				float targetScale = CombatText.TargetScale;
				this.alpha += (float)this.alphaDir * 0.05f;
				if ((double)this.alpha <= 0.6)
				{
					this.alphaDir = 1;
				}
				if (this.alpha >= 1f)
				{
					this.alpha = 1f;
					this.alphaDir = -1;
				}
				if (this.dot)
				{
					this.velocity.Y = this.velocity.Y + 0.15f;
				}
				else
				{
					this.velocity.Y = this.velocity.Y * 0.92f;
					if (this.crit)
					{
						this.velocity.Y = this.velocity.Y * 0.92f;
					}
				}
				this.velocity.X = this.velocity.X * 0.93f;
				this.position += this.velocity;
				this.lifeTime--;
				if (this.lifeTime <= 0)
				{
					this.scale -= 0.1f * targetScale;
					if ((double)this.scale < 0.1)
					{
						this.active = false;
					}
					this.lifeTime = 0;
					if (this.crit)
					{
						this.alphaDir = -1;
						this.scale += 0.07f * targetScale;
						return;
					}
				}
				else
				{
					if (this.crit)
					{
						if (this.velocity.X < 0f)
						{
							this.rotation += 0.001f;
						}
						else
						{
							this.rotation -= 0.001f;
						}
					}
					if (this.dot)
					{
						this.scale += 0.5f * targetScale;
						if ((double)this.scale > 0.8 * (double)targetScale)
						{
							this.scale = 0.8f * targetScale;
							return;
						}
					}
					else
					{
						if (this.scale < targetScale)
						{
							this.scale += 0.1f * targetScale;
						}
						if (this.scale > targetScale)
						{
							this.scale = targetScale;
						}
					}
				}
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0003DBFC File Offset: 0x0003BDFC
		public static void UpdateCombatText()
		{
			for (int i = 0; i < 100; i++)
			{
				if (Main.combatText[i].active)
				{
					Main.combatText[i].Update();
				}
			}
		}

		// Token: 0x04000203 RID: 515
		public static readonly Color DamagedFriendly = new Color(255, 80, 90, 255);

		// Token: 0x04000204 RID: 516
		public static readonly Color DamagedFriendlyCrit = new Color(255, 100, 30, 255);

		// Token: 0x04000205 RID: 517
		public static readonly Color DamagedHostile = new Color(255, 160, 80, 255);

		// Token: 0x04000206 RID: 518
		public static readonly Color DamagedHostileCrit = new Color(255, 100, 30, 255);

		// Token: 0x04000207 RID: 519
		public static readonly Color OthersDamagedHostile = CombatText.DamagedHostile * 0.4f;

		// Token: 0x04000208 RID: 520
		public static readonly Color OthersDamagedHostileCrit = CombatText.DamagedHostileCrit * 0.4f;

		// Token: 0x04000209 RID: 521
		public static readonly Color HealLife = new Color(100, 255, 100, 255);

		// Token: 0x0400020A RID: 522
		public static readonly Color HealMana = new Color(100, 100, 255, 255);

		// Token: 0x0400020B RID: 523
		public static readonly Color LifeRegen = new Color(255, 60, 70, 255);

		// Token: 0x0400020C RID: 524
		public static readonly Color LifeRegenNegative = new Color(255, 140, 40, 255);

		// Token: 0x0400020D RID: 525
		public Vector2 position;

		// Token: 0x0400020E RID: 526
		public Vector2 velocity;

		// Token: 0x0400020F RID: 527
		public float alpha;

		// Token: 0x04000210 RID: 528
		public int alphaDir = 1;

		// Token: 0x04000211 RID: 529
		public string text = "";

		// Token: 0x04000212 RID: 530
		public float scale = 1f;

		// Token: 0x04000213 RID: 531
		public float rotation;

		// Token: 0x04000214 RID: 532
		public Color color;

		// Token: 0x04000215 RID: 533
		public bool active;

		// Token: 0x04000216 RID: 534
		public int lifeTime;

		// Token: 0x04000217 RID: 535
		public bool crit;

		// Token: 0x04000218 RID: 536
		public bool dot;
	}
}
