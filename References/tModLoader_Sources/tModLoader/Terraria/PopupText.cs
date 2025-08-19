using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terraria
{
	/// <summary>
	/// Represents an in-world floating text object. <br />
	/// <VariousTextOptionsSummary>
	///         <br />Summary of options to display text to the user:<br />
	///         • <see cref="M:Terraria.Main.NewText(System.String,System.Byte,System.Byte,System.Byte)" /> to display a message in chat. <br />
	///         • <see cref="T:Terraria.CombatText" /> to display floating damage numbers in-game. Used for damage and healing numbers. <br />
	///         • <see cref="T:Terraria.PopupText" /> to display non-overlapping floating in-game text. Used for reforge and item pickup messages. <br />
	///     </VariousTextOptionsSummary>
	/// </summary>
	// Token: 0x02000045 RID: 69
	public class PopupText
	{
		/// <summary>
		/// If <see langword="true" />, this <see cref="T:Terraria.PopupText" /> is not for an item.
		/// </summary>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x002E1A85 File Offset: 0x002DFC85
		public bool notActuallyAnItem
		{
			get
			{
				return this.npcNetID != 0 || this.freeAdvanced;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x002E1A97 File Offset: 0x002DFC97
		public static float TargetScale
		{
			get
			{
				return Main.UIScale / Main.GameViewMatrix.Zoom.X;
			}
		}

		/// <summary>
		/// Destroys the <see cref="T:Terraria.PopupText" /> in <see cref="F:Terraria.Main.popupText" /> at the index <see cref="F:Terraria.PopupText.sonarText" /> if that text has <see cref="F:Terraria.PopupText.sonar" /> set to <see langword="true" />.
		/// <para /> Note that multiple fishing lures is a common feature of many popular mods, so this method won't clear all PopupText in that situation because only 1 index is tracked. The Projectile.localAI[2] of a bobber projectile is used to store the index of its sonar PopupText. That stored value is offset by +1.
		/// </summary>
		// Token: 0x06000CA6 RID: 3238 RVA: 0x002E1AAE File Offset: 0x002DFCAE
		public static void ClearSonarText()
		{
			if (PopupText.sonarText >= 0 && Main.popupText[PopupText.sonarText].sonar)
			{
				Main.popupText[PopupText.sonarText].active = false;
				PopupText.sonarText = -1;
			}
		}

		/// <summary>
		/// Resets a <see cref="T:Terraria.PopupText" /> to its default values.
		/// </summary>
		/// <param name="text">The <see cref="T:Terraria.PopupText" /> to reset.</param>
		// Token: 0x06000CA7 RID: 3239 RVA: 0x002E1AE4 File Offset: 0x002DFCE4
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
			text.rarity = 0;
		}

		/// <summary>
		/// Creates a new <see cref="T:Terraria.PopupText" /> in <see cref="F:Terraria.Main.popupText" /> at <paramref name="position" /> using the settings from <paramref name="request" />.
		/// <br /> The new <see cref="T:Terraria.PopupText" /> is not bound to a specific <see cref="T:Terraria.Item" /> or <see cref="T:Terraria.ID.NPCID" />.
		/// <br /> All <see cref="T:Terraria.PopupText" />s created using this method have <c><see cref="F:Terraria.PopupText.context" /> == <see cref="F:Terraria.PopupTextContext.Advanced" /></c> and <see cref="F:Terraria.PopupText.freeAdvanced" /> set to <see langword="true" />.
		/// </summary>
		/// <param name="request">The settings for the new <see cref="T:Terraria.PopupText" />.</param>
		/// <param name="position">The position of the new <see cref="T:Terraria.PopupText" /> in world coordinates.</param>
		/// <returns>
		/// <c>-1</c> if a new <see cref="T:Terraria.PopupText" /> could not be made, if <c><see cref="F:Terraria.Main.netMode" /> == <see cref="F:Terraria.ID.NetmodeID.Server" /></c>, or if the current player has item text disabled (<see cref="F:Terraria.Main.showItemText" />).
		/// <br /> Otherwise, return the index in <see cref="F:Terraria.Main.popupText" /> of the new <see cref="T:Terraria.PopupText" />
		/// </returns>
		// Token: 0x06000CA8 RID: 3240 RVA: 0x002E1B5C File Offset: 0x002DFD5C
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
				Vector2 vector = FontAssets.MouseText.Value.MeasureString(text);
				PopupText popupText = Main.popupText[num];
				PopupText.ResetText(popupText);
				popupText.active = true;
				popupText.position = position - vector / 2f;
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

		/// <summary>
		/// Creates a new <see cref="T:Terraria.PopupText" /> in <see cref="F:Terraria.Main.popupText" /> at <paramref name="position" /> bound to a given <paramref name="npcNetID" />.
		/// </summary>
		/// <param name="context">
		/// The <see cref="T:Terraria.PopupTextContext" /> in which this <see cref="T:Terraria.PopupText" /> was created.
		/// <br /> If <c><paramref name="context" /> == <see cref="F:Terraria.PopupTextContext.SonarAlert" /></c>, then <see cref="F:Terraria.PopupText.color" /> will be a shade of red.
		/// </param>
		/// <param name="npcNetID">The <see cref="T:Terraria.ID.NPCID" /> this <see cref="T:Terraria.PopupText" /> represents.</param>
		/// <param name="position"></param>
		/// <param name="stay5TimesLonger">If <see langword="true" />, then this <see cref="T:Terraria.PopupText" /> will spawn with <c><see cref="F:Terraria.PopupText.lifeTime" /> == 5 * 60</c>.</param>
		/// <returns>
		/// <inheritdoc cref="M:Terraria.PopupText.NewText(Terraria.AdvancedPopupRequest,Microsoft.Xna.Framework.Vector2)" />
		/// <br /> Also returns <c>-1</c> if <c><paramref name="npcNetID" /> == 0</c>.
		/// </returns>
		// Token: 0x06000CA9 RID: 3241 RVA: 0x002E1C10 File Offset: 0x002DFE10
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
				Vector2 vector = FontAssets.MouseText.Value.MeasureString(typeName);
				PopupText popupText = Main.popupText[num];
				PopupText.ResetText(popupText);
				popupText.active = true;
				popupText.position = position - vector / 2f;
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

		/// <summary>
		/// Creates a new <see cref="T:Terraria.PopupText" /> in <see cref="F:Terraria.Main.popupText" /> at the center of the picked-up <paramref name="newItem" />.
		/// <br /> If a <see cref="T:Terraria.PopupText" /> already exists with the <see cref="M:Terraria.Item.AffixName" /> of <paramref name="newItem" />, that text will instead be modified unless <paramref name="noStack" /> is <see langword="true" />.
		/// </summary>
		/// <param name="context">The <see cref="T:Terraria.PopupTextContext" /> in which this <see cref="T:Terraria.PopupText" /> was created.</param>
		/// <param name="newItem">The <see cref="T:Terraria.Item" /> to create the new text from.</param>
		/// <param name="stack">The stack of <paramref name="newItem" />.</param>
		/// <param name="noStack">If <see langword="true" />, always create a new <see cref="T:Terraria.PopupText" /> instead of modifying an existing one.</param>
		/// <param name="longText">If <see langword="true" />, then this <see cref="T:Terraria.PopupText" /> will spawn with <c><see cref="F:Terraria.PopupText.lifeTime" /> == 5 * 60</c>.</param>
		/// <returns>
		/// <inheritdoc cref="M:Terraria.PopupText.NewText(Terraria.AdvancedPopupRequest,Microsoft.Xna.Framework.Vector2)" />
		/// <br /> Also returns <c>-1</c> if <see cref="P:Terraria.Item.Name" /> is <see langword="null" />.
		/// </returns>
		// Token: 0x06000CAA RID: 3242 RVA: 0x002E1D0C File Offset: 0x002DFF0C
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
					string text = newItem.Name + " (" + (popupText.stack + (long)stack).ToString() + ")";
					string text2 = newItem.Name;
					if (popupText.stack > 1L)
					{
						text2 = text2 + " (" + popupText.stack.ToString() + ")";
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
					text3 = text3 + " (" + stack.ToString() + ")";
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
				else if (newItem.rare == 11)
				{
					popupText2.color = new Color(180, 40, 255);
				}
				else if (newItem.rare >= 12)
				{
					popupText2.color = RarityLoader.GetRarity(newItem.rare).RarityColor;
				}
				popupText2.rarity = newItem.rare;
				popupText2.expert = newItem.expert;
				popupText2.master = newItem.master;
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

		// Token: 0x06000CAB RID: 3243 RVA: 0x002E25E4 File Offset: 0x002E07E4
		private void AddToCoinValue(long addedValue)
		{
			long val = this.coinValue + addedValue;
			this.coinValue = Math.Min(999999999L, Math.Max(0L, val));
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x002E2614 File Offset: 0x002E0814
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

		/// <summary>
		/// Marks the <see cref="T:Terraria.PopupText" /> in <see cref="F:Terraria.Main.popupText" /> at <paramref name="sonarTextIndex" /> as sonar text, assigning <see cref="F:Terraria.PopupText.sonarText" /> and setting <see cref="F:Terraria.PopupText.sonar" /> to <see langword="true" />.
		/// </summary>
		/// <param name="sonarTextIndex"></param>
		// Token: 0x06000CAD RID: 3245 RVA: 0x002E2685 File Offset: 0x002E0885
		public static void AssignAsSonarText(int sonarTextIndex)
		{
			PopupText.sonarText = sonarTextIndex;
			if (PopupText.sonarText > -1)
			{
				Main.popupText[PopupText.sonarText].sonar = true;
			}
		}

		/// <summary>
		/// Converts a value in copper coins to a formatted string.
		/// </summary>
		/// <param name="coinValue">The value to format in copper coins.</param>
		/// <returns>The formatted text.</returns>
		// Token: 0x06000CAE RID: 3246 RVA: 0x002E26A8 File Offset: 0x002E08A8
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
				text = text + num.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Platinum"));
			}
			if (num2 > 0)
			{
				text = text + num2.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Gold"));
			}
			if (num3 > 0)
			{
				text = text + num3.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Silver"));
			}
			if (num4 > 0)
			{
				text = text + num4.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Copper"));
			}
			if (text.Length > 1)
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x002E27F4 File Offset: 0x002E09F4
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
				this.name = this.name + num.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Platinum"));
			}
			if (num2 > 0)
			{
				this.name = this.name + num2.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Gold"));
			}
			if (num3 > 0)
			{
				this.name = this.name + num3.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Silver"));
			}
			if (num4 > 0)
			{
				this.name = this.name + num4.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Copper"));
			}
			if (this.name.Length > 1)
			{
				this.name = this.name.Substring(0, this.name.Length - 1);
			}
		}

		/// <summary>
		/// Updates this <see cref="T:Terraria.PopupText" />.
		/// </summary>
		/// <param name="whoAmI">The index in <see cref="F:Terraria.Main.popupText" /> of this <see cref="T:Terraria.PopupText" />.</param>
		// Token: 0x06000CB0 RID: 3248 RVA: 0x002E2970 File Offset: 0x002E0B70
		public void Update(int whoAmI)
		{
			if (!this.active)
			{
				return;
			}
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
			if (this.rarity > 11)
			{
				this.color = RarityLoader.GetRarity(this.rarity).RarityColor;
			}
			bool flag = false;
			Vector2 textHitbox = this.GetTextHitbox();
			Rectangle rectangle;
			rectangle..ctor((int)(this.position.X - textHitbox.X / 2f), (int)(this.position.Y - textHitbox.Y / 2f), (int)textHitbox.X, (int)textHitbox.Y);
			for (int i = 0; i < 20; i++)
			{
				PopupText popupText = Main.popupText[i];
				if (popupText.active && i != whoAmI)
				{
					Vector2 textHitbox2 = popupText.GetTextHitbox();
					Rectangle value;
					value..ctor((int)(popupText.position.X - textHitbox2.X / 2f), (int)(popupText.position.Y - textHitbox2.Y / 2f), (int)textHitbox2.X, (int)textHitbox2.Y);
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

		// Token: 0x06000CB1 RID: 3249 RVA: 0x002E2CD8 File Offset: 0x002E0ED8
		private Vector2 GetTextHitbox()
		{
			string text = this.name;
			if (this.stack > 1L)
			{
				text = text + " (" + this.stack.ToString() + ")";
			}
			Vector2 result = FontAssets.MouseText.Value.MeasureString(text);
			result *= this.scale;
			result.Y *= 0.8f;
			return result;
		}

		/// <summary>
		/// Calls <see cref="M:Terraria.PopupText.Update(System.Int32)" /> on all <see cref="F:Terraria.PopupText.active" /> <see cref="T:Terraria.PopupText" />s in <see cref="F:Terraria.Main.popupText" /> and assigns  <see cref="F:Terraria.PopupText.numActive" />.
		/// </summary>
		// Token: 0x06000CB2 RID: 3250 RVA: 0x002E2D44 File Offset: 0x002E0F44
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

		/// <summary>
		/// Sets all <see cref="T:Terraria.PopupText" />s in <see cref="F:Terraria.Main.popupText" /> to a new instance and assigns <see cref="F:Terraria.PopupText.numActive" /> to <c>0</c>.
		/// </summary>
		// Token: 0x06000CB3 RID: 3251 RVA: 0x002E2D88 File Offset: 0x002E0F88
		public static void ClearAll()
		{
			for (int i = 0; i < 20; i++)
			{
				Main.popupText[i] = new PopupText();
			}
			PopupText.numActive = 0;
		}

		/// <summary>
		/// The position of this <see cref="T:Terraria.PopupText" /> in world coordinates.
		/// </summary>
		// Token: 0x04000D87 RID: 3463
		public Vector2 position;

		/// <summary>
		/// The velocity of this <see cref="T:Terraria.PopupText" /> in world coordinates per tick.
		/// </summary>
		// Token: 0x04000D88 RID: 3464
		public Vector2 velocity;

		/// <summary>
		/// The opacity of this <see cref="T:Terraria.PopupText" /> in the range [0f, 1f], where <c>0f</c> is transparent and <c>1f</c> is opaque.
		/// </summary>
		// Token: 0x04000D89 RID: 3465
		public float alpha;

		/// <summary>
		/// The direction this <see cref="T:Terraria.PopupText" />'s <see cref="F:Terraria.PopupText.alpha" /> changes in.
		/// </summary>
		// Token: 0x04000D8A RID: 3466
		public int alphaDir = 1;

		/// <summary>
		/// The text displayed by this <see cref="T:Terraria.PopupText" />.
		/// </summary>
		// Token: 0x04000D8B RID: 3467
		public string name;

		/// <summary>
		/// The optional stack size appended to <see cref="F:Terraria.PopupText.name" />.
		/// <br /> Will only be displayed is <c><see cref="F:Terraria.PopupText.stack" /> &gt; 1</c>.
		/// </summary>
		// Token: 0x04000D8C RID: 3468
		public long stack;

		/// <summary>
		/// The scale this <see cref="T:Terraria.PopupText" /> draws at.
		/// </summary>
		// Token: 0x04000D8D RID: 3469
		public float scale = 1f;

		/// <summary>
		/// The clockwise rotation of this <see cref="T:Terraria.PopupText" /> in radians.
		/// </summary>
		// Token: 0x04000D8E RID: 3470
		public float rotation;

		/// <summary>
		/// The color of this <see cref="T:Terraria.PopupText" />'s text.
		/// </summary>
		// Token: 0x04000D8F RID: 3471
		public Color color;

		/// <summary>
		/// If <see langword="true" />, this <see cref="T:Terraria.PopupText" /> is visible in the world.
		/// </summary>
		// Token: 0x04000D90 RID: 3472
		public bool active;

		/// <summary>
		/// The time in ticks this <see cref="T:Terraria.PopupText" /> will remain for until it starts to disappear.
		/// </summary>
		// Token: 0x04000D91 RID: 3473
		public int lifeTime;

		/// <summary>
		/// The default <see cref="F:Terraria.PopupText.lifeTime" /> of a <see cref="T:Terraria.PopupText" />.
		/// </summary>
		// Token: 0x04000D92 RID: 3474
		public static int activeTime = 60;

		/// <summary>
		/// The number of <see cref="F:Terraria.PopupText.active" /> <see cref="T:Terraria.PopupText" />s in <see cref="F:Terraria.Main.popupText" />.
		/// <br /> Assigned after <see cref="M:Terraria.PopupText.UpdateItemText" /> runs.
		/// </summary>
		// Token: 0x04000D93 RID: 3475
		public static int numActive;

		/// <summary>
		/// If <see langword="true" />, this <see cref="T:Terraria.PopupText" /> can't be modified when creating a new item <see cref="T:Terraria.PopupText" />.
		/// </summary>
		// Token: 0x04000D94 RID: 3476
		public bool NoStack;

		/// <summary>
		/// If <see langword="true" />, this <see cref="T:Terraria.PopupText" /> is specifically for coins.
		/// </summary>
		// Token: 0x04000D95 RID: 3477
		public bool coinText;

		/// <summary>
		/// The value of coins this <see cref="T:Terraria.PopupText" /> represents in the range [0, 999999999].
		/// </summary>
		// Token: 0x04000D96 RID: 3478
		public long coinValue;

		/// <summary>
		/// The index in <see cref="F:Terraria.Main.popupText" /> of the last known sonar text.
		/// <br /> Assign and clear using <see cref="M:Terraria.PopupText.AssignAsSonarText(System.Int32)" /> and <see cref="M:Terraria.PopupText.ClearSonarText" />.
		/// </summary>
		// Token: 0x04000D97 RID: 3479
		public static int sonarText = -1;

		/// <summary>
		/// If <see langword="true" />, this <see cref="T:Terraria.PopupText" /> will draw in the Expert Mode rarity color.
		/// </summary>
		// Token: 0x04000D98 RID: 3480
		public bool expert;

		/// <summary>
		/// If <see langword="true" />, this <see cref="T:Terraria.PopupText" /> will draw in the Master Mode rarity color.
		/// </summary>
		// Token: 0x04000D99 RID: 3481
		public bool master;

		/// <summary>
		/// Marks this <see cref="T:Terraria.PopupText" /> as this player's Sonar Potion text.
		/// </summary>
		// Token: 0x04000D9A RID: 3482
		public bool sonar;

		/// <summary>
		/// The context in which this <see cref="T:Terraria.PopupText" /> was created.
		/// </summary>
		// Token: 0x04000D9B RID: 3483
		public PopupTextContext context;

		/// <summary>
		/// The NPC type (<see cref="F:Terraria.NPC.type" />) this <see cref="T:Terraria.PopupText" /> is bound to, or <c>0</c> if not bound to an NPC.
		/// </summary>
		// Token: 0x04000D9C RID: 3484
		public int npcNetID;

		/// <summary>
		/// If <see langword="true" />, this <see cref="T:Terraria.PopupText" /> is not bound to an item or NPC.
		/// </summary>
		// Token: 0x04000D9D RID: 3485
		public bool freeAdvanced;

		/// <summary>
		/// The <see cref="T:Terraria.ID.ItemRarityID" /> this <see cref="T:Terraria.PopupText" /> uses for its main color.
		/// </summary>
		// Token: 0x04000D9E RID: 3486
		public int rarity;
	}
}
