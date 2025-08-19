using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200031D RID: 797
	public class NPCStatsReportInfoElement : IBestiaryInfoElement, IUpdateBeforeSorting
	{
		// Token: 0x06002439 RID: 9273 RVA: 0x005596F1 File Offset: 0x005578F1
		public NPCStatsReportInfoElement(int npcNetId)
		{
			this.NpcId = npcNetId;
			this._instance = new NPC();
			this.RefreshStats(Main.GameModeInfo, this._instance);
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x0600243A RID: 9274 RVA: 0x0055971C File Offset: 0x0055791C
		// (remove) Token: 0x0600243B RID: 9275 RVA: 0x00559754 File Offset: 0x00557954
		public event NPCStatsReportInfoElement.StatAdjustmentStep OnRefreshStats;

		// Token: 0x0600243C RID: 9276 RVA: 0x00559789 File Offset: 0x00557989
		public void UpdateBeforeSorting()
		{
			this.RefreshStats(Main.GameModeInfo, this._instance);
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x0055979C File Offset: 0x0055799C
		private void RefreshStats(GameModeData gameModeFound, NPC instance)
		{
			instance.SetDefaults(this.NpcId, new NPCSpawnParams
			{
				gameModeData = gameModeFound,
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

		// Token: 0x0600243E RID: 9278 RVA: 0x00559824 File Offset: 0x00557A24
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return null;
			}
			this.RefreshStats(Main.GameModeInfo, this._instance);
			UIElement uielement = new UIElement
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(109f, 0f)
			};
			int num = 99;
			int num2 = 35;
			int num3 = 3;
			int num4 = 0;
			UIImage uiimage = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_HP", 1))
			{
				Top = new StyleDimension((float)num4, 0f),
				Left = new StyleDimension((float)num3, 0f)
			};
			UIImage uiimage2 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Attack", 1))
			{
				Top = new StyleDimension((float)(num4 + num2), 0f),
				Left = new StyleDimension((float)num3, 0f)
			};
			UIImage uiimage3 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Defense", 1))
			{
				Top = new StyleDimension((float)(num4 + num2), 0f),
				Left = new StyleDimension((float)(num3 + num), 0f)
			};
			UIImage uiimage4 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Knockback", 1))
			{
				Top = new StyleDimension((float)num4, 0f),
				Left = new StyleDimension((float)(num3 + num), 0f)
			};
			uielement.Append(uiimage);
			uielement.Append(uiimage2);
			uielement.Append(uiimage3);
			uielement.Append(uiimage4);
			int num5 = -10;
			int num6 = 0;
			int num7 = (int)this.MonetaryValue;
			string text = Utils.Clamp<int>(num7 / 1000000, 0, 999).ToString();
			string text2 = Utils.Clamp<int>(num7 % 1000000 / 10000, 0, 99).ToString();
			string text3 = Utils.Clamp<int>(num7 % 10000 / 100, 0, 99).ToString();
			string text4 = Utils.Clamp<int>(num7 % 100 / 1, 0, 99).ToString();
			if (num7 / 1000000 < 1)
			{
				text = "-";
			}
			if (num7 / 10000 < 1)
			{
				text2 = "-";
			}
			if (num7 / 100 < 1)
			{
				text3 = "-";
			}
			if (num7 < 1)
			{
				text4 = "-";
			}
			string text5 = this.LifeMax.ToString();
			string text6 = this.Damage.ToString();
			string text7 = this.Defense.ToString();
			string text8;
			if (this.KnockbackResist > 0.8f)
			{
				text8 = Language.GetText("BestiaryInfo.KnockbackHigh").Value;
			}
			else if (this.KnockbackResist > 0.4f)
			{
				text8 = Language.GetText("BestiaryInfo.KnockbackMedium").Value;
			}
			else if (this.KnockbackResist > 0f)
			{
				text8 = Language.GetText("BestiaryInfo.KnockbackLow").Value;
			}
			else
			{
				text8 = Language.GetText("BestiaryInfo.KnockbackNone").Value;
			}
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
			uiimage.Append(element);
			uiimage2.Append(element3);
			uiimage3.Append(element4);
			uiimage4.Append(element2);
			int num8 = 66;
			if (num7 > 0)
			{
				UIHorizontalSeparator element5 = new UIHorizontalSeparator(2, true)
				{
					Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
					Color = new Color(89, 116, 213, 255) * 0.9f,
					Left = new StyleDimension(0f, 0f),
					Top = new StyleDimension((float)(num6 + num2 * 2), 0f)
				};
				uielement.Append(element5);
				num8 += 4;
				int num9 = num3;
				int num10 = num8 + 8;
				int num11 = 49;
				UIImage uiimage5 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Platinum", 1))
				{
					Top = new StyleDimension((float)num10, 0f),
					Left = new StyleDimension((float)num9, 0f)
				};
				UIImage uiimage6 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Gold", 1))
				{
					Top = new StyleDimension((float)num10, 0f),
					Left = new StyleDimension((float)(num9 + num11), 0f)
				};
				UIImage uiimage7 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Silver", 1))
				{
					Top = new StyleDimension((float)num10, 0f),
					Left = new StyleDimension((float)(num9 + num11 * 2 + 1), 0f)
				};
				UIImage uiimage8 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Copper", 1))
				{
					Top = new StyleDimension((float)num10, 0f),
					Left = new StyleDimension((float)(num9 + num11 * 3 + 1), 0f)
				};
				if (text != "-")
				{
					uielement.Append(uiimage5);
				}
				if (text2 != "-")
				{
					uielement.Append(uiimage6);
				}
				if (text3 != "-")
				{
					uielement.Append(uiimage7);
				}
				if (text4 != "-")
				{
					uielement.Append(uiimage8);
				}
				int num12 = num5 + 3;
				float textScale = 0.85f;
				UIText element6 = new UIText(text, textScale, false)
				{
					HAlign = 1f,
					VAlign = 0.5f,
					Left = new StyleDimension((float)num12, 0f),
					Top = new StyleDimension((float)num6, 0f)
				};
				UIText element7 = new UIText(text2, textScale, false)
				{
					HAlign = 1f,
					VAlign = 0.5f,
					Left = new StyleDimension((float)num12, 0f),
					Top = new StyleDimension((float)num6, 0f)
				};
				UIText element8 = new UIText(text3, textScale, false)
				{
					HAlign = 1f,
					VAlign = 0.5f,
					Left = new StyleDimension((float)num12, 0f),
					Top = new StyleDimension((float)num6, 0f)
				};
				UIText element9 = new UIText(text4, textScale, false)
				{
					HAlign = 1f,
					VAlign = 0.5f,
					Left = new StyleDimension((float)num12, 0f),
					Top = new StyleDimension((float)num6, 0f)
				};
				uiimage5.Append(element6);
				uiimage6.Append(element7);
				uiimage7.Append(element8);
				uiimage8.Append(element9);
				num8 += 34;
			}
			num8 += 4;
			uielement.Height.Pixels = (float)num8;
			uiimage2.OnUpdate += this.ShowStats_Attack;
			uiimage3.OnUpdate += this.ShowStats_Defense;
			uiimage.OnUpdate += this.ShowStats_Life;
			uiimage4.OnUpdate += this.ShowStats_Knockback;
			return uielement;
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x0055A030 File Offset: 0x00558230
		private void ShowStats_Attack(UIElement element)
		{
			if (!element.IsMouseHovering)
			{
				return;
			}
			Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Attack"), 0, 0, -1, -1, -1, -1, 0);
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x0055A064 File Offset: 0x00558264
		private void ShowStats_Defense(UIElement element)
		{
			if (!element.IsMouseHovering)
			{
				return;
			}
			Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Defense"), 0, 0, -1, -1, -1, -1, 0);
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x0055A098 File Offset: 0x00558298
		private void ShowStats_Knockback(UIElement element)
		{
			if (!element.IsMouseHovering)
			{
				return;
			}
			Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Knockback"), 0, 0, -1, -1, -1, -1, 0);
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x0055A0CC File Offset: 0x005582CC
		private void ShowStats_Life(UIElement element)
		{
			if (!element.IsMouseHovering)
			{
				return;
			}
			Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Life"), 0, 0, -1, -1, -1, -1, 0);
		}

		// Token: 0x04004879 RID: 18553
		public int NpcId;

		// Token: 0x0400487A RID: 18554
		public int Damage;

		// Token: 0x0400487B RID: 18555
		public int LifeMax;

		// Token: 0x0400487C RID: 18556
		public float MonetaryValue;

		// Token: 0x0400487D RID: 18557
		public int Defense;

		// Token: 0x0400487E RID: 18558
		public float KnockbackResist;

		// Token: 0x0400487F RID: 18559
		private NPC _instance;

		// Token: 0x020006EF RID: 1775
		// (Invoke) Token: 0x06003709 RID: 14089
		public delegate void StatAdjustmentStep(NPCStatsReportInfoElement element);
	}
}
