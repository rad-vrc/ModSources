using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000507 RID: 1287
	public class UIBestiaryInfoItemLine : UIPanel, IManuallyOrderedUIElement
	{
		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06003DFA RID: 15866 RVA: 0x005CE966 File Offset: 0x005CCB66
		// (set) Token: 0x06003DFB RID: 15867 RVA: 0x005CE96E File Offset: 0x005CCB6E
		public int OrderInUIList { get; set; }

		// Token: 0x06003DFC RID: 15868 RVA: 0x005CE978 File Offset: 0x005CCB78
		public UIBestiaryInfoItemLine(DropRateInfo info, BestiaryUICollectionInfo uiinfo, float textScale = 1f)
		{
			this._infoDisplayItem = new Item();
			this._infoDisplayItem.SetDefaults(info.itemId);
			this.SetBestiaryNotesOnItemCache(info);
			base.SetPadding(0f);
			this.PaddingLeft = 10f;
			this.PaddingRight = 10f;
			this.Width.Set(-14f, 1f);
			this.Height.Set(32f, 0f);
			this.Left.Set(5f, 0f);
			base.OnMouseOver += this.MouseOver;
			base.OnMouseOut += this.MouseOut;
			this.BorderColor = new Color(89, 116, 213, 255);
			string stackRange;
			string droprate;
			this.GetDropInfo(info, uiinfo, out stackRange, out droprate);
			if (uiinfo.UnlockState < BestiaryEntryUnlockState.CanShowDropsWithoutDropRates_3)
			{
				this._hideMouseOver = true;
				Asset<Texture2D> texture = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Locked");
				UIElement uIElement = new UIElement
				{
					Height = new StyleDimension(0f, 1f),
					Width = new StyleDimension(0f, 1f),
					HAlign = 0.5f,
					VAlign = 0.5f
				};
				uIElement.SetPadding(0f);
				UIImage element = new UIImage(texture)
				{
					ImageScale = 0.55f,
					HAlign = 0.5f,
					VAlign = 0.5f
				};
				uIElement.Append(element);
				base.Append(uIElement);
				return;
			}
			UIItemIcon element2 = new UIItemIcon(this._infoDisplayItem, uiinfo.UnlockState < BestiaryEntryUnlockState.CanShowDropsWithoutDropRates_3)
			{
				IgnoresMouseInteraction = true,
				HAlign = 0f,
				Left = new StyleDimension(4f, 0f)
			};
			base.Append(element2);
			if (!string.IsNullOrEmpty(stackRange))
			{
				droprate = stackRange + " " + droprate;
			}
			UITextPanel<string> element3 = new UITextPanel<string>(droprate, textScale, false)
			{
				IgnoresMouseInteraction = true,
				DrawPanel = false,
				HAlign = 1f,
				Top = new StyleDimension(-4f, 0f)
			};
			base.Append(element3);
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x005CEB9C File Offset: 0x005CCD9C
		protected void GetDropInfo(DropRateInfo dropRateInfo, BestiaryUICollectionInfo uiinfo, out string stackRange, out string droprate)
		{
			if (dropRateInfo.stackMin != dropRateInfo.stackMax)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 2);
				defaultInterpolatedStringHandler.AppendLiteral(" (");
				defaultInterpolatedStringHandler.AppendFormatted<int>(dropRateInfo.stackMin);
				defaultInterpolatedStringHandler.AppendLiteral("-");
				defaultInterpolatedStringHandler.AppendFormatted<int>(dropRateInfo.stackMax);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				stackRange = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			else if (dropRateInfo.stackMin == 1)
			{
				stackRange = "";
			}
			else
			{
				stackRange = " (" + dropRateInfo.stackMin.ToString() + ")";
			}
			string originalFormat = "P";
			if ((double)dropRateInfo.dropRate < 0.001)
			{
				originalFormat = "P4";
			}
			if (dropRateInfo.dropRate != 1f)
			{
				droprate = Utils.PrettifyPercentDisplay(dropRateInfo.dropRate, originalFormat);
			}
			else
			{
				droprate = "100%";
			}
			if (uiinfo.UnlockState != BestiaryEntryUnlockState.CanShowDropsWithDropRates_4)
			{
				droprate = "???";
				stackRange = "";
			}
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x005CEC94 File Offset: 0x005CCE94
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			if (base.IsMouseHovering && !this._hideMouseOver)
			{
				this.DrawMouseOver();
			}
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x005CECB4 File Offset: 0x005CCEB4
		private void DrawMouseOver()
		{
			Main.HoverItem = this._infoDisplayItem;
			Main.instance.MouseText("", 0, 0, -1, -1, -1, -1, 0);
			Main.mouseText = true;
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x005CECE8 File Offset: 0x005CCEE8
		public override int CompareTo(object obj)
		{
			IManuallyOrderedUIElement manuallyOrderedUIElement = obj as IManuallyOrderedUIElement;
			if (manuallyOrderedUIElement != null)
			{
				return this.OrderInUIList.CompareTo(manuallyOrderedUIElement.OrderInUIList);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x005CED1C File Offset: 0x005CCF1C
		private void SetBestiaryNotesOnItemCache(DropRateInfo info)
		{
			List<string> list = new List<string>();
			if (info.conditions == null)
			{
				return;
			}
			foreach (IItemDropRuleCondition condition in info.conditions)
			{
				if (condition != null)
				{
					string conditionDescription = condition.GetConditionDescription();
					if (!string.IsNullOrWhiteSpace(conditionDescription))
					{
						list.Add(conditionDescription);
					}
				}
			}
			this._infoDisplayItem.BestiaryNotes = string.Join("\n", list);
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x005CEDA8 File Offset: 0x005CCFA8
		private void MouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this.BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x005CEDCA File Offset: 0x005CCFCA
		private void MouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this.BorderColor = new Color(89, 116, 213, 255);
		}

		// Token: 0x040056AD RID: 22189
		private Item _infoDisplayItem;

		// Token: 0x040056AE RID: 22190
		private bool _hideMouseOver;
	}
}
