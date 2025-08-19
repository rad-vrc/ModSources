using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020006A0 RID: 1696
	public class NPCStatsReportInfoElement : IBestiaryInfoElement, IUpdateBeforeSorting
	{
		// Token: 0x1400006D RID: 109
		// (add) Token: 0x06004835 RID: 18485 RVA: 0x00647EDC File Offset: 0x006460DC
		// (remove) Token: 0x06004836 RID: 18486 RVA: 0x00647F14 File Offset: 0x00646114
		public event NPCStatsReportInfoElement.StatAdjustmentStep OnRefreshStats;

		// Token: 0x06004837 RID: 18487 RVA: 0x00647F49 File Offset: 0x00646149
		public NPCStatsReportInfoElement(int npcNetId)
		{
			this.NpcId = npcNetId;
			this._instance = new NPC();
			this.RefreshStats(Main.GameModeInfo, this._instance);
		}

		// Token: 0x06004838 RID: 18488 RVA: 0x00647F74 File Offset: 0x00646174
		public void UpdateBeforeSorting()
		{
			this.RefreshStats(Main.GameModeInfo, this._instance);
		}

		// Token: 0x06004839 RID: 18489 RVA: 0x00647F88 File Offset: 0x00646188
		private void RefreshStats(GameModeData gameModeFound, NPC instance)
		{
			instance.SetDefaults(this.NpcId, new NPCSpawnParams
			{
				gameModeData = new GameModeData?(gameModeFound),
				strengthMultiplierOverride = null
			});
			this.Damage = instance.damage;
			this.LifeMax = instance.lifeMax;
			this.MonetaryValue = instance.value;
			this.Defense = instance.defense;
			this.KnockbackResist = instance.knockBackResist;
			if (this.OnRefreshStats != null)
			{
				this.OnRefreshStats(this);
			}
		}

		// Token: 0x0600483A RID: 18490 RVA: 0x00648014 File Offset: 0x00646214
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return null;
			}
			this.RefreshStats(Main.GameModeInfo, this._instance);
			UIElement uIElement = new UIElement
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(109f, 0f)
			};
			int num = 99;
			int num2 = 35;
			int num3 = 3;
			int num4 = 0;
			UIImage uIImage = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_HP"))
			{
				Top = new StyleDimension((float)num4, 0f),
				Left = new StyleDimension((float)num3, 0f)
			};
			UIImage uIImage2 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Attack"))
			{
				Top = new StyleDimension((float)(num4 + num2), 0f),
				Left = new StyleDimension((float)num3, 0f)
			};
			UIImage uIImage3 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Defense"))
			{
				Top = new StyleDimension((float)(num4 + num2), 0f),
				Left = new StyleDimension((float)(num3 + num), 0f)
			};
			UIImage uIImage4 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Knockback"))
			{
				Top = new StyleDimension((float)num4, 0f),
				Left = new StyleDimension((float)(num3 + num), 0f)
			};
			uIElement.Append(uIImage);
			uIElement.Append(uIImage2);
			uIElement.Append(uIImage3);
			uIElement.Append(uIImage4);
			int num5 = -10;
			int num6 = 0;
			int num12 = (int)this.MonetaryValue;
			string text = Utils.Clamp<int>(num12 / 1000000, 0, 999).ToString();
			string text2 = Utils.Clamp<int>(num12 % 1000000 / 10000, 0, 99).ToString();
			string text3 = Utils.Clamp<int>(num12 % 10000 / 100, 0, 99).ToString();
			string text4 = Utils.Clamp<int>(num12 % 100 / 1, 0, 99).ToString();
			if (num12 / 1000000 < 1)
			{
				text = "-";
			}
			if (num12 / 10000 < 1)
			{
				text2 = "-";
			}
			if (num12 / 100 < 1)
			{
				text3 = "-";
			}
			if (num12 < 1)
			{
				text4 = "-";
			}
			string text5 = this.LifeMax.ToString();
			string text6 = this.Damage.ToString();
			string text7 = this.Defense.ToString();
			string text8 = (this.KnockbackResist > 0.8f) ? Language.GetText("BestiaryInfo.KnockbackHigh").Value : ((this.KnockbackResist > 0.4f) ? Language.GetText("BestiaryInfo.KnockbackMedium").Value : ((this.KnockbackResist <= 0f) ? Language.GetText("BestiaryInfo.KnockbackNone").Value : Language.GetText("BestiaryInfo.KnockbackLow").Value));
			if (info.UnlockState < BestiaryEntryUnlockState.CanShowStats_2)
			{
				text2 = (text = (text3 = (text4 = "?")));
				text6 = (text5 = (text7 = (text8 = "???")));
			}
			UIText element = new UIText(text5, 1f, false)
			{
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension((float)num5, 0f),
				Top = new StyleDimension((float)num6, 0f),
				IgnoresMouseInteraction = true
			};
			UIText element2 = new UIText(text8, 1f, false)
			{
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension((float)num5, 0f),
				Top = new StyleDimension((float)num6, 0f),
				IgnoresMouseInteraction = true
			};
			UIText element3 = new UIText(text6, 1f, false)
			{
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension((float)num5, 0f),
				Top = new StyleDimension((float)num6, 0f),
				IgnoresMouseInteraction = true
			};
			UIText element4 = new UIText(text7, 1f, false)
			{
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension((float)num5, 0f),
				Top = new StyleDimension((float)num6, 0f),
				IgnoresMouseInteraction = true
			};
			uIImage.Append(element);
			uIImage2.Append(element3);
			uIImage3.Append(element4);
			uIImage4.Append(element2);
			int num7 = 66;
			if (num12 > 0)
			{
				UIHorizontalSeparator element5 = new UIHorizontalSeparator(2, true)
				{
					Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
					Color = new Color(89, 116, 213, 255) * 0.9f,
					Left = new StyleDimension(0f, 0f),
					Top = new StyleDimension((float)(num6 + num2 * 2), 0f)
				};
				uIElement.Append(element5);
				num7 += 4;
				int num8 = num3;
				int num9 = num7 + 8;
				int num10 = 49;
				UIImage uIImage5 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Platinum"))
				{
					Top = new StyleDimension((float)num9, 0f),
					Left = new StyleDimension((float)num8, 0f)
				};
				UIImage uIImage6 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Gold"))
				{
					Top = new StyleDimension((float)num9, 0f),
					Left = new StyleDimension((float)(num8 + num10), 0f)
				};
				UIImage uIImage7 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Silver"))
				{
					Top = new StyleDimension((float)num9, 0f),
					Left = new StyleDimension((float)(num8 + num10 * 2 + 1), 0f)
				};
				UIImage uIImage8 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Copper"))
				{
					Top = new StyleDimension((float)num9, 0f),
					Left = new StyleDimension((float)(num8 + num10 * 3 + 1), 0f)
				};
				if (text != "-")
				{
					uIElement.Append(uIImage5);
				}
				if (text2 != "-")
				{
					uIElement.Append(uIImage6);
				}
				if (text3 != "-")
				{
					uIElement.Append(uIImage7);
				}
				if (text4 != "-")
				{
					uIElement.Append(uIImage8);
				}
				int num11 = num5 + 3;
				float textScale = 0.85f;
				UIText element6 = new UIText(text, textScale, false)
				{
					HAlign = 1f,
					VAlign = 0.5f,
					Left = new StyleDimension((float)num11, 0f),
					Top = new StyleDimension((float)num6, 0f)
				};
				UIText element7 = new UIText(text2, textScale, false)
				{
					HAlign = 1f,
					VAlign = 0.5f,
					Left = new StyleDimension((float)num11, 0f),
					Top = new StyleDimension((float)num6, 0f)
				};
				UIText element8 = new UIText(text3, textScale, false)
				{
					HAlign = 1f,
					VAlign = 0.5f,
					Left = new StyleDimension((float)num11, 0f),
					Top = new StyleDimension((float)num6, 0f)
				};
				UIText element9 = new UIText(text4, textScale, false)
				{
					HAlign = 1f,
					VAlign = 0.5f,
					Left = new StyleDimension((float)num11, 0f),
					Top = new StyleDimension((float)num6, 0f)
				};
				uIImage5.Append(element6);
				uIImage6.Append(element7);
				uIImage7.Append(element8);
				uIImage8.Append(element9);
				num7 += 34;
			}
			num7 += 4;
			uIElement.Height.Pixels = (float)num7;
			uIImage2.OnUpdate += this.ShowStats_Attack;
			uIImage3.OnUpdate += this.ShowStats_Defense;
			uIImage.OnUpdate += this.ShowStats_Life;
			uIImage4.OnUpdate += this.ShowStats_Knockback;
			return uIElement;
		}

		// Token: 0x0600483B RID: 18491 RVA: 0x00648814 File Offset: 0x00646A14
		private void ShowStats_Attack(UIElement element)
		{
			if (element.IsMouseHovering)
			{
				Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Attack"), 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x0600483C RID: 18492 RVA: 0x00648844 File Offset: 0x00646A44
		private void ShowStats_Defense(UIElement element)
		{
			if (element.IsMouseHovering)
			{
				Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Defense"), 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x00648874 File Offset: 0x00646A74
		private void ShowStats_Knockback(UIElement element)
		{
			if (element.IsMouseHovering)
			{
				Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Knockback"), 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x0600483E RID: 18494 RVA: 0x006488A4 File Offset: 0x00646AA4
		private void ShowStats_Life(UIElement element)
		{
			if (element.IsMouseHovering)
			{
				Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Life"), 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x04005C1C RID: 23580
		public int NpcId;

		// Token: 0x04005C1D RID: 23581
		public int Damage;

		// Token: 0x04005C1E RID: 23582
		public int LifeMax;

		// Token: 0x04005C1F RID: 23583
		public float MonetaryValue;

		// Token: 0x04005C20 RID: 23584
		public int Defense;

		// Token: 0x04005C21 RID: 23585
		public float KnockbackResist;

		// Token: 0x04005C22 RID: 23586
		private NPC _instance;

		// Token: 0x02000D2F RID: 3375
		// (Invoke) Token: 0x06006357 RID: 25431
		public delegate void StatAdjustmentStep(NPCStatsReportInfoElement element);
	}
}
