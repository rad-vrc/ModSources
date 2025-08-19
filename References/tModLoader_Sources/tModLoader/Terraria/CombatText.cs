using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent;

namespace Terraria
{
	/// <summary>
	/// Represents floating text in the game world most typically used for damage numbers and healing numbers. <br />
	/// For non-overlapping in-game text, such as reforge messages, use <see cref="T:Terraria.PopupText" /> instead.
	/// Use the <see cref="M:Terraria.CombatText.NewText(Microsoft.Xna.Framework.Rectangle,Microsoft.Xna.Framework.Color,System.Int32,System.Boolean,System.Boolean)" /> or <see cref="M:Terraria.CombatText.NewText(Microsoft.Xna.Framework.Rectangle,Microsoft.Xna.Framework.Color,System.String,System.Boolean,System.Boolean)" /> methods to create a new instance. <br />
	/// In multiplayer, <see cref="F:Terraria.ID.MessageID.CombatTextInt" /> and <see cref="F:Terraria.ID.MessageID.CombatTextString" /> network messages can be used to sync a combat text if manually spawned. <br />
	/// <VariousTextOptionsSummary>
	///         <br />Summary of options to display text to the user:<br />
	///         • <see cref="M:Terraria.Main.NewText(System.String,System.Byte,System.Byte,System.Byte)" /> to display a message in chat. <br />
	///         • <see cref="T:Terraria.CombatText" /> to display floating damage numbers in-game. Used for damage and healing numbers. <br />
	///         • <see cref="T:Terraria.PopupText" /> to display non-overlapping floating in-game text. Used for reforge and item pickup messages. <br />
	///     </VariousTextOptionsSummary>
	/// </summary>
	// Token: 0x02000027 RID: 39
	public class CombatText
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000169B2 File Offset: 0x00014BB2
		public static float TargetScale
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000169B9 File Offset: 0x00014BB9
		public static int NewText(Rectangle location, Color color, int amount, bool dramatic = false, bool dot = false)
		{
			return CombatText.NewText(location, color, amount.ToString(), dramatic, dot);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000169CC File Offset: 0x00014BCC
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

		// Token: 0x06000180 RID: 384 RVA: 0x00016D04 File Offset: 0x00014F04
		public static void clearAll()
		{
			for (int i = 0; i < 100; i++)
			{
				Main.combatText[i].active = false;
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00016D2C File Offset: 0x00014F2C
		public void Update()
		{
			if (!this.active)
			{
				return;
			}
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
				}
				return;
			}
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
				}
				return;
			}
			if (this.scale < targetScale)
			{
				this.scale += 0.1f * targetScale;
			}
			if (this.scale > targetScale)
			{
				this.scale = targetScale;
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00016F30 File Offset: 0x00015130
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

		// Token: 0x040000C6 RID: 198
		public static readonly Color DamagedFriendly = new Color(255, 80, 90, 255);

		// Token: 0x040000C7 RID: 199
		public static readonly Color DamagedFriendlyCrit = new Color(255, 100, 30, 255);

		// Token: 0x040000C8 RID: 200
		public static readonly Color DamagedHostile = new Color(255, 160, 80, 255);

		// Token: 0x040000C9 RID: 201
		public static readonly Color DamagedHostileCrit = new Color(255, 100, 30, 255);

		// Token: 0x040000CA RID: 202
		public static readonly Color OthersDamagedHostile = CombatText.DamagedHostile * 0.4f;

		// Token: 0x040000CB RID: 203
		public static readonly Color OthersDamagedHostileCrit = CombatText.DamagedHostileCrit * 0.4f;

		// Token: 0x040000CC RID: 204
		public static readonly Color HealLife = new Color(100, 255, 100, 255);

		// Token: 0x040000CD RID: 205
		public static readonly Color HealMana = new Color(100, 100, 255, 255);

		// Token: 0x040000CE RID: 206
		public static readonly Color LifeRegen = new Color(255, 60, 70, 255);

		// Token: 0x040000CF RID: 207
		public static readonly Color LifeRegenNegative = new Color(255, 140, 40, 255);

		// Token: 0x040000D0 RID: 208
		public Vector2 position;

		// Token: 0x040000D1 RID: 209
		public Vector2 velocity;

		// Token: 0x040000D2 RID: 210
		public float alpha;

		// Token: 0x040000D3 RID: 211
		public int alphaDir = 1;

		// Token: 0x040000D4 RID: 212
		public string text = "";

		// Token: 0x040000D5 RID: 213
		public float scale = 1f;

		// Token: 0x040000D6 RID: 214
		public float rotation;

		// Token: 0x040000D7 RID: 215
		public Color color;

		// Token: 0x040000D8 RID: 216
		public bool active;

		// Token: 0x040000D9 RID: 217
		public int lifeTime;

		// Token: 0x040000DA RID: 218
		public bool crit;

		// Token: 0x040000DB RID: 219
		public bool dot;
	}
}
