using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.Localization;

namespace Terraria
{
	// Token: 0x0200001A RID: 26
	public class PopupText
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000BB7B File Offset: 0x00009D7B
		public bool notActuallyAnItem
		{
			get
			{
				return this.npcNetID != 0 || this.freeAdvanced;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000BB8D File Offset: 0x00009D8D
		public static float TargetScale
		{
			get
			{
				return Main.UIScale / Main.GameViewMatrix.Zoom.X;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000BBA4 File Offset: 0x00009DA4
		public static void ClearSonarText()
		{
			if (PopupText.sonarText < 0)
			{
				return;
			}
			if (Main.popupText[PopupText.sonarText].sonar)
			{
				Main.popupText[PopupText.sonarText].active = false;
				PopupText.sonarText = -1;
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000BBD8 File Offset: 0x00009DD8
		public static void ResetText(PopupText text)
		{
			text.NoStack = false;
			text.coinText = false;
			text.coinValue = 0L;
			text.sonar = false;
			text.npcNetID = 0;
			text.expert = false;
			text.master = false;
			text.freeAdvanced = false;
			text.scale = 0f;
			text.rotation = 0f;
			text.alpha = 1f;
			text.alphaDir = -1;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000BC48 File Offset: 0x00009E48
		public static int NewText(AdvancedPopupRequest request, Vector2 position)
		{
			if (!Main.showItemText)
			{
				return -1;
			}
			if (Main.netMode == 2)
			{
				return -1;
			}
			int num = PopupText.FindNextItemTextSlot();
			if (num >= 0)
			{
				string text = request.Text;
				Vector2 value = FontAssets.MouseText.Value.MeasureString(text);
				PopupText popupText = Main.popupText[num];
				PopupText.ResetText(popupText);
				popupText.active = true;
				popupText.position = position - value / 2f;
				popupText.name = text;
				popupText.stack = 1L;
				popupText.velocity = request.Velocity;
				popupText.lifeTime = request.DurationInFrames;
				popupText.context = PopupTextContext.Advanced;
				popupText.freeAdvanced = true;
				popupText.color = request.Color;
			}
			return num;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000BCFC File Offset: 0x00009EFC
		public static int NewText(PopupTextContext context, int npcNetID, Vector2 position, bool stay5TimesLonger)
		{
			if (!Main.showItemText)
			{
				return -1;
			}
			if (npcNetID == 0)
			{
				return -1;
			}
			if (Main.netMode == 2)
			{
				return -1;
			}
			int num = PopupText.FindNextItemTextSlot();
			if (num >= 0)
			{
				NPC npc = new NPC();
				npc.SetDefaults(npcNetID, default(NPCSpawnParams));
				string typeName = npc.TypeName;
				Vector2 value = FontAssets.MouseText.Value.MeasureString(typeName);
				PopupText popupText = Main.popupText[num];
				PopupText.ResetText(popupText);
				popupText.active = true;
				popupText.position = position - value / 2f;
				popupText.name = typeName;
				popupText.stack = 1L;
				popupText.velocity.Y = -7f;
				popupText.lifeTime = 60;
				popupText.context = context;
				if (stay5TimesLonger)
				{
					popupText.lifeTime *= 5;
				}
				popupText.npcNetID = npcNetID;
				popupText.color = Color.White;
				if (context == PopupTextContext.SonarAlert)
				{
					popupText.color = Color.Lerp(Color.White, Color.Crimson, 0.5f);
				}
			}
			return num;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000BDF8 File Offset: 0x00009FF8
		public static int NewText(PopupTextContext context, Item newItem, int stack, bool noStack = false, bool longText = false)
		{
			if (!Main.showItemText)
			{
				return -1;
			}
			if (newItem.Name == null || !newItem.active)
			{
				return -1;
			}
			if (Main.netMode == 2)
			{
				return -1;
			}
			bool flag = newItem.type >= 71 && newItem.type <= 74;
			for (int i = 0; i < 20; i++)
			{
				PopupText popupText = Main.popupText[i];
				if (popupText.active && !popupText.notActuallyAnItem && (popupText.name == newItem.AffixName() || (flag && popupText.coinText)) && !popupText.NoStack && !noStack)
				{
					string text = string.Concat(new object[]
					{
						newItem.Name,
						" (",
						popupText.stack + (long)stack,
						")"
					});
					string text2 = newItem.Name;
					if (popupText.stack > 1L)
					{
						text2 = string.Concat(new object[]
						{
							text2,
							" (",
							popupText.stack,
							")"
						});
					}
					Vector2 vector = FontAssets.MouseText.Value.MeasureString(text2);
					vector = FontAssets.MouseText.Value.MeasureString(text);
					if (popupText.lifeTime < 0)
					{
						popupText.scale = 1f;
					}
					if (popupText.lifeTime < 60)
					{
						popupText.lifeTime = 60;
					}
					if (flag && popupText.coinText)
					{
						long num = 0L;
						if (newItem.type == 71)
						{
							num += (long)stack;
						}
						else if (newItem.type == 72)
						{
							num += (long)(100 * stack);
						}
						else if (newItem.type == 73)
						{
							num += (long)(10000 * stack);
						}
						else if (newItem.type == 74)
						{
							num += (long)(1000000 * stack);
						}
						popupText.AddToCoinValue(num);
						text = PopupText.ValueToName(popupText.coinValue);
						vector = FontAssets.MouseText.Value.MeasureString(text);
						popupText.name = text;
						if (popupText.coinValue >= 1000000L)
						{
							if (popupText.lifeTime < 300)
							{
								popupText.lifeTime = 300;
							}
							popupText.color = new Color(220, 220, 198);
						}
						else if (popupText.coinValue >= 10000L)
						{
							if (popupText.lifeTime < 240)
							{
								popupText.lifeTime = 240;
							}
							popupText.color = new Color(224, 201, 92);
						}
						else if (popupText.coinValue >= 100L)
						{
							if (popupText.lifeTime < 180)
							{
								popupText.lifeTime = 180;
							}
							popupText.color = new Color(181, 192, 193);
						}
						else if (popupText.coinValue >= 1L)
						{
							if (popupText.lifeTime < 120)
							{
								popupText.lifeTime = 120;
							}
							popupText.color = new Color(246, 138, 96);
						}
					}
					popupText.stack += (long)stack;
					popupText.scale = 0f;
					popupText.rotation = 0f;
					popupText.position.X = newItem.position.X + (float)newItem.width * 0.5f - vector.X * 0.5f;
					popupText.position.Y = newItem.position.Y + (float)newItem.height * 0.25f - vector.Y * 0.5f;
					popupText.velocity.Y = -7f;
					popupText.context = context;
					popupText.npcNetID = 0;
					if (popupText.coinText)
					{
						popupText.stack = 1L;
					}
					return i;
				}
			}
			int num2 = PopupText.FindNextItemTextSlot();
			if (num2 >= 0)
			{
				string text3 = newItem.AffixName();
				if (stack > 1)
				{
					text3 = string.Concat(new object[]
					{
						text3,
						" (",
						stack,
						")"
					});
				}
				Vector2 vector2 = FontAssets.MouseText.Value.MeasureString(text3);
				PopupText popupText2 = Main.popupText[num2];
				PopupText.ResetText(popupText2);
				popupText2.active = true;
				popupText2.position.X = newItem.position.X + (float)newItem.width * 0.5f - vector2.X * 0.5f;
				popupText2.position.Y = newItem.position.Y + (float)newItem.height * 0.25f - vector2.Y * 0.5f;
				popupText2.color = Color.White;
				if (newItem.rare == 1)
				{
					popupText2.color = new Color(150, 150, 255);
				}
				else if (newItem.rare == 2)
				{
					popupText2.color = new Color(150, 255, 150);
				}
				else if (newItem.rare == 3)
				{
					popupText2.color = new Color(255, 200, 150);
				}
				else if (newItem.rare == 4)
				{
					popupText2.color = new Color(255, 150, 150);
				}
				else if (newItem.rare == 5)
				{
					popupText2.color = new Color(255, 150, 255);
				}
				else if (newItem.rare == -13)
				{
					popupText2.master = true;
				}
				else if (newItem.rare == -11)
				{
					popupText2.color = new Color(255, 175, 0);
				}
				else if (newItem.rare == -1)
				{
					popupText2.color = new Color(130, 130, 130);
				}
				else if (newItem.rare == 6)
				{
					popupText2.color = new Color(210, 160, 255);
				}
				else if (newItem.rare == 7)
				{
					popupText2.color = new Color(150, 255, 10);
				}
				else if (newItem.rare == 8)
				{
					popupText2.color = new Color(255, 255, 10);
				}
				else if (newItem.rare == 9)
				{
					popupText2.color = new Color(5, 200, 255);
				}
				else if (newItem.rare == 10)
				{
					popupText2.color = new Color(255, 40, 100);
				}
				else if (newItem.rare >= 11)
				{
					popupText2.color = new Color(180, 40, 255);
				}
				popupText2.expert = newItem.expert;
				popupText2.name = newItem.AffixName();
				popupText2.stack = (long)stack;
				popupText2.velocity.Y = -7f;
				popupText2.lifeTime = 60;
				popupText2.context = context;
				if (longText)
				{
					popupText2.lifeTime *= 5;
				}
				popupText2.coinValue = 0L;
				popupText2.coinText = (newItem.type >= 71 && newItem.type <= 74);
				if (popupText2.coinText)
				{
					long num3 = 0L;
					if (newItem.type == 71)
					{
						num3 += popupText2.stack;
					}
					else if (newItem.type == 72)
					{
						num3 += 100L * popupText2.stack;
					}
					else if (newItem.type == 73)
					{
						num3 += 10000L * popupText2.stack;
					}
					else if (newItem.type == 74)
					{
						num3 += 1000000L * popupText2.stack;
					}
					popupText2.AddToCoinValue(num3);
					popupText2.ValueToName();
					popupText2.stack = 1L;
					if (popupText2.coinValue >= 1000000L)
					{
						if (popupText2.lifeTime < 300)
						{
							popupText2.lifeTime = 300;
						}
						popupText2.color = new Color(220, 220, 198);
					}
					else if (popupText2.coinValue >= 10000L)
					{
						if (popupText2.lifeTime < 240)
						{
							popupText2.lifeTime = 240;
						}
						popupText2.color = new Color(224, 201, 92);
					}
					else if (popupText2.coinValue >= 100L)
					{
						if (popupText2.lifeTime < 180)
						{
							popupText2.lifeTime = 180;
						}
						popupText2.color = new Color(181, 192, 193);
					}
					else if (popupText2.coinValue >= 1L)
					{
						if (popupText2.lifeTime < 120)
						{
							popupText2.lifeTime = 120;
						}
						popupText2.color = new Color(246, 138, 96);
					}
				}
			}
			return num2;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000C6CC File Offset: 0x0000A8CC
		private void AddToCoinValue(long addedValue)
		{
			long val = this.coinValue + addedValue;
			this.coinValue = Math.Min(999999999L, Math.Max(0L, val));
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000C6FC File Offset: 0x0000A8FC
		private static int FindNextItemTextSlot()
		{
			int num = -1;
			for (int i = 0; i < 20; i++)
			{
				if (!Main.popupText[i].active)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				double num2 = (double)Main.bottomWorld;
				for (int j = 0; j < 20; j++)
				{
					if (num2 > (double)Main.popupText[j].position.Y)
					{
						num = j;
						num2 = (double)Main.popupText[j].position.Y;
					}
				}
			}
			return num;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000C76D File Offset: 0x0000A96D
		public static void AssignAsSonarText(int sonarTextIndex)
		{
			PopupText.sonarText = sonarTextIndex;
			if (PopupText.sonarText > -1)
			{
				Main.popupText[PopupText.sonarText].sonar = true;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000C790 File Offset: 0x0000A990
		public static string ValueToName(long coinValue)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			long num5 = coinValue;
			while (num5 > 0L)
			{
				if (num5 >= 1000000L)
				{
					num5 -= 1000000L;
					num++;
				}
				else if (num5 >= 10000L)
				{
					num5 -= 10000L;
					num2++;
				}
				else if (num5 >= 100L)
				{
					num5 -= 100L;
					num3++;
				}
				else if (num5 >= 1L)
				{
					num5 -= 1L;
					num4++;
				}
			}
			string text = "";
			if (num > 0)
			{
				text = text + num + string.Format(" {0} ", Language.GetTextValue("Currency.Platinum"));
			}
			if (num2 > 0)
			{
				text = text + num2 + string.Format(" {0} ", Language.GetTextValue("Currency.Gold"));
			}
			if (num3 > 0)
			{
				text = text + num3 + string.Format(" {0} ", Language.GetTextValue("Currency.Silver"));
			}
			if (num4 > 0)
			{
				text = text + num4 + string.Format(" {0} ", Language.GetTextValue("Currency.Copper"));
			}
			if (text.Length > 1)
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000C8D8 File Offset: 0x0000AAD8
		private void ValueToName()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			long num5 = this.coinValue;
			while (num5 > 0L)
			{
				if (num5 >= 1000000L)
				{
					num5 -= 1000000L;
					num++;
				}
				else if (num5 >= 10000L)
				{
					num5 -= 10000L;
					num2++;
				}
				else if (num5 >= 100L)
				{
					num5 -= 100L;
					num3++;
				}
				else if (num5 >= 1L)
				{
					num5 -= 1L;
					num4++;
				}
			}
			this.name = "";
			if (num > 0)
			{
				this.name = this.name + num + string.Format(" {0} ", Language.GetTextValue("Currency.Platinum"));
			}
			if (num2 > 0)
			{
				this.name = this.name + num2 + string.Format(" {0} ", Language.GetTextValue("Currency.Gold"));
			}
			if (num3 > 0)
			{
				this.name = this.name + num3 + string.Format(" {0} ", Language.GetTextValue("Currency.Silver"));
			}
			if (num4 > 0)
			{
				this.name = this.name + num4 + string.Format(" {0} ", Language.GetTextValue("Currency.Copper"));
			}
			if (this.name.Length > 1)
			{
				this.name = this.name.Substring(0, this.name.Length - 1);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000CA50 File Offset: 0x0000AC50
		public void Update(int whoAmI)
		{
			if (this.active)
			{
				float targetScale = PopupText.TargetScale;
				this.alpha += (float)this.alphaDir * 0.01f;
				if ((double)this.alpha <= 0.7)
				{
					this.alpha = 0.7f;
					this.alphaDir = 1;
				}
				if (this.alpha >= 1f)
				{
					this.alpha = 1f;
					this.alphaDir = -1;
				}
				if (this.expert)
				{
					this.color = new Color((int)((byte)Main.DiscoR), (int)((byte)Main.DiscoG), (int)((byte)Main.DiscoB), (int)Main.mouseTextColor);
				}
				else if (this.master)
				{
					this.color = new Color(255, (int)((byte)(Main.masterColor * 200f)), 0, (int)Main.mouseTextColor);
				}
				bool flag = false;
				Vector2 textHitbox = this.GetTextHitbox();
				Rectangle rectangle = new Rectangle((int)(this.position.X - textHitbox.X / 2f), (int)(this.position.Y - textHitbox.Y / 2f), (int)textHitbox.X, (int)textHitbox.Y);
				for (int i = 0; i < 20; i++)
				{
					PopupText popupText = Main.popupText[i];
					if (popupText.active && i != whoAmI)
					{
						Vector2 textHitbox2 = popupText.GetTextHitbox();
						Rectangle value = new Rectangle((int)(popupText.position.X - textHitbox2.X / 2f), (int)(popupText.position.Y - textHitbox2.Y / 2f), (int)textHitbox2.X, (int)textHitbox2.Y);
						if (rectangle.Intersects(value) && (this.position.Y < popupText.position.Y || (this.position.Y == popupText.position.Y && whoAmI < i)))
						{
							flag = true;
							int num = PopupText.numActive;
							if (num > 3)
							{
								num = 3;
							}
							popupText.lifeTime = PopupText.activeTime + 15 * num;
							this.lifeTime = PopupText.activeTime + 15 * num;
						}
					}
				}
				if (!flag)
				{
					this.velocity.Y = this.velocity.Y * 0.86f;
					if (this.scale == targetScale)
					{
						this.velocity.Y = this.velocity.Y * 0.4f;
					}
				}
				else if (this.velocity.Y > -6f)
				{
					this.velocity.Y = this.velocity.Y - 0.2f;
				}
				else
				{
					this.velocity.Y = this.velocity.Y * 0.86f;
				}
				this.velocity.X = this.velocity.X * 0.93f;
				this.position += this.velocity;
				this.lifeTime--;
				if (this.lifeTime <= 0)
				{
					this.scale -= 0.03f * targetScale;
					if ((double)this.scale < 0.1 * (double)targetScale)
					{
						this.active = false;
						if (PopupText.sonarText == whoAmI)
						{
							PopupText.sonarText = -1;
						}
					}
					this.lifeTime = 0;
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
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000CD9C File Offset: 0x0000AF9C
		private Vector2 GetTextHitbox()
		{
			string text = this.name;
			if (this.stack > 1L)
			{
				text = string.Concat(new object[]
				{
					text,
					" (",
					this.stack,
					")"
				});
			}
			Vector2 vector = FontAssets.MouseText.Value.MeasureString(text);
			vector *= this.scale;
			vector.Y *= 0.8f;
			return vector;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000CE18 File Offset: 0x0000B018
		public static void UpdateItemText()
		{
			int num = 0;
			for (int i = 0; i < 20; i++)
			{
				if (Main.popupText[i].active)
				{
					num++;
					Main.popupText[i].Update(i);
				}
			}
			PopupText.numActive = num;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000CE5C File Offset: 0x0000B05C
		public static void ClearAll()
		{
			for (int i = 0; i < 20; i++)
			{
				Main.popupText[i] = new PopupText();
			}
			PopupText.numActive = 0;
		}

		// Token: 0x0400008E RID: 142
		public Vector2 position;

		// Token: 0x0400008F RID: 143
		public Vector2 velocity;

		// Token: 0x04000090 RID: 144
		public float alpha;

		// Token: 0x04000091 RID: 145
		public int alphaDir = 1;

		// Token: 0x04000092 RID: 146
		public string name;

		// Token: 0x04000093 RID: 147
		public long stack;

		// Token: 0x04000094 RID: 148
		public float scale = 1f;

		// Token: 0x04000095 RID: 149
		public float rotation;

		// Token: 0x04000096 RID: 150
		public Color color;

		// Token: 0x04000097 RID: 151
		public bool active;

		// Token: 0x04000098 RID: 152
		public int lifeTime;

		// Token: 0x04000099 RID: 153
		public static int activeTime = 60;

		// Token: 0x0400009A RID: 154
		public static int numActive;

		// Token: 0x0400009B RID: 155
		public bool NoStack;

		// Token: 0x0400009C RID: 156
		public bool coinText;

		// Token: 0x0400009D RID: 157
		public long coinValue;

		// Token: 0x0400009E RID: 158
		public static int sonarText = -1;

		// Token: 0x0400009F RID: 159
		public bool expert;

		// Token: 0x040000A0 RID: 160
		public bool master;

		// Token: 0x040000A1 RID: 161
		public bool sonar;

		// Token: 0x040000A2 RID: 162
		public PopupTextContext context;

		// Token: 0x040000A3 RID: 163
		public int npcNetID;

		// Token: 0x040000A4 RID: 164
		public bool freeAdvanced;
	}
}
