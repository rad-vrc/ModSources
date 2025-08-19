using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Social.Base;
using Terraria.Social.Steam;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.ModLoader.UI.ModBrowser
{
	// Token: 0x02000271 RID: 625
	internal class UIModTagFilterDropdown : UIPanel
	{
		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06002B3A RID: 11066 RVA: 0x0052154C File Offset: 0x0051F74C
		// (remove) Token: 0x06002B3B RID: 11067 RVA: 0x00521584 File Offset: 0x0051F784
		public event Action OnClickingTag;

		// Token: 0x06002B3C RID: 11068 RVA: 0x005215BC File Offset: 0x0051F7BC
		public UIModTagFilterDropdown()
		{
			this.tagButtons = new List<GroupOptionButton<int>>();
			this.Width = new StyleDimension(-25f, 1f);
			this.Height = new StyleDimension(-50f, 1f);
			this.Top = new StyleDimension(50f, 0f);
			this.BackgroundColor = new Color(35, 40, 83) * 0.5f;
			this.BorderColor = new Color(35, 40, 83) * 0.5f;
			this.IgnoresMouseInteraction = false;
			base.SetPadding(0f);
			this.BuildGrid();
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x00521668 File Offset: 0x0051F868
		private void BuildGrid()
		{
			int labelWidth = 100;
			foreach (WorkshopTagOption workshopTagOption in SteamedWraps.ModTags)
			{
				string tagName = Language.GetTextValue(workshopTagOption.NameKey);
				labelWidth = Math.Max(labelWidth, (int)ChatManager.GetStringSize(FontAssets.MouseText.Value, tagName, new Vector2(0.8f), -1f).X + 10);
			}
			int padding = 2;
			int buttonHeight = 26 + padding;
			int tagCount = SteamedWraps.ModTags.Count;
			this.indexOfLanguageTags = SteamedWraps.ModTags.FindIndex((WorkshopTagOption x) => x.InternalNameForAPIs == "English");
			int maxColumnCount = Math.Max(this.indexOfLanguageTags, tagCount - this.indexOfLanguageTags);
			UIPanel dropdownPanel = new UIPanel
			{
				Width = new StyleDimension((float)(15 + labelWidth * 2), 0f),
				Height = new StyleDimension((float)((maxColumnCount + 1) * buttonHeight + 5 + 3), 0f),
				Left = new StyleDimension((float)Math.Max(0, 160 - (15 + labelWidth * 2) / 2), 0f),
				Top = new StyleDimension(0f, 0f)
			};
			dropdownPanel.BorderColor = new Color(89, 116, 213, 255) * 0.9f;
			dropdownPanel.BackgroundColor = new Color(73, 94, 171) * 0.9f;
			dropdownPanel.SetPadding(0f);
			base.Append(dropdownPanel);
			for (int i = 0; i < tagCount; i++)
			{
				WorkshopTagOption tag = SteamedWraps.ModTags[i];
				int top = 5 + buttonHeight * i;
				int left = 5;
				if (i >= this.indexOfLanguageTags)
				{
					top = 5 + buttonHeight * (i - this.indexOfLanguageTags);
					left = 10 + labelWidth;
				}
				GroupOptionButton<int> groupOptionButton = new GroupOptionButton<int>(i, Language.GetText(tag.NameKey), null, Color.White, null, 0.8f, 0.5f, 10f)
				{
					Width = new StyleDimension((float)labelWidth, 0f),
					Height = new StyleDimension((float)(buttonHeight - padding), 0f),
					Top = new StyleDimension((float)top, 0f),
					Left = new StyleDimension((float)left, 0f)
				};
				groupOptionButton.ShowHighlightWhenSelected = false;
				if (i >= this.indexOfLanguageTags)
				{
					groupOptionButton.OnLeftClick += this.ClickLanguageTag;
				}
				else
				{
					groupOptionButton.OnLeftClick += this.ClickCategoryTag;
				}
				dropdownPanel.Append(groupOptionButton);
				this.tagButtons.Add(groupOptionButton);
			}
			foreach (GroupOptionButton<int> groupOptionButton2 in this.tagButtons)
			{
				groupOptionButton2.SetCurrentOption(-1);
			}
			this.clearTagsButton = new GroupOptionButton<int>(0, Language.GetText("tModLoader.ModConfigClear"), null, Color.White, null, 0.8f, 0.5f, 10f)
			{
				Width = new StyleDimension((float)labelWidth, 0f),
				Height = new StyleDimension((float)(buttonHeight - padding), 0f),
				Top = new StyleDimension((float)(5 + buttonHeight * maxColumnCount), 0f),
				Left = new StyleDimension((float)(8 + labelWidth / 2), 0f)
			};
			this.clearTagsButton.SetColor(new Color(226, 49, 85), 1f);
			this.clearTagsButton.ShowHighlightWhenSelected = false;
			this.clearTagsButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				Interface.modBrowser.ResetTagFilters();
				Action onClickingTag = this.OnClickingTag;
				if (onClickingTag == null)
				{
					return;
				}
				onClickingTag();
			};
			dropdownPanel.Append(this.clearTagsButton);
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x00521A38 File Offset: 0x0051FC38
		private void ClickCategoryTag(UIMouseEvent evt, UIElement listeningElement)
		{
			int tagIndex = ((GroupOptionButton<int>)listeningElement).OptionValue;
			string internalNameForAPIs = SteamedWraps.ModTags[tagIndex].InternalNameForAPIs;
			if (Interface.modBrowser.CategoryTagsFilter.Contains(tagIndex))
			{
				Interface.modBrowser.CategoryTagsFilter.Remove(tagIndex);
			}
			else
			{
				Interface.modBrowser.CategoryTagsFilter.Add(tagIndex);
			}
			this.RefreshSelectionStates();
			Action onClickingTag = this.OnClickingTag;
			if (onClickingTag == null)
			{
				return;
			}
			onClickingTag();
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x00521AB0 File Offset: 0x0051FCB0
		private void ClickLanguageTag(UIMouseEvent evt, UIElement listeningElement)
		{
			int tagIndex = ((GroupOptionButton<int>)listeningElement).OptionValue;
			if (Interface.modBrowser.LanguageTagFilter == tagIndex)
			{
				Interface.modBrowser.LanguageTagFilter = -1;
			}
			else
			{
				Interface.modBrowser.LanguageTagFilter = tagIndex;
			}
			this.RefreshSelectionStates();
			Action onClickingTag = this.OnClickingTag;
			if (onClickingTag == null)
			{
				return;
			}
			onClickingTag();
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x00521B04 File Offset: 0x0051FD04
		internal void RefreshSelectionStates()
		{
			for (int i = 0; i < this.tagButtons.Count; i++)
			{
				GroupOptionButton<int> item = this.tagButtons[i];
				bool selected = (i < this.indexOfLanguageTags) ? Interface.modBrowser.CategoryTagsFilter.Contains(item.OptionValue) : (Interface.modBrowser.LanguageTagFilter == item.OptionValue);
				item.SetCurrentOption(selected ? i : -1);
				if (selected)
				{
					item.SetColor(new Color(152, 175, 235), 1f);
				}
				else
				{
					item.SetColor(Colors.InventoryDefaultColor, 0.7f);
				}
			}
			Interface.modBrowser.RefreshTagFilterState();
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x00521BB8 File Offset: 0x0051FDB8
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			for (int i = 0; i < this.indexOfLanguageTags; i++)
			{
				GroupOptionButton<int> item = this.tagButtons[i];
				if (item.IsMouseHovering)
				{
					UICommon.TooltipMouseText(Language.GetTextValue(SteamedWraps.ModTags[item.OptionValue].NameKey + "Description"));
				}
			}
			if (this.clearTagsButton.IsMouseHovering)
			{
				UICommon.TooltipMouseText(Language.GetTextValue("tModLoader.MBTagsClear"));
			}
		}

		// Token: 0x04001BC0 RID: 7104
		private List<GroupOptionButton<int>> tagButtons;

		// Token: 0x04001BC1 RID: 7105
		private GroupOptionButton<int> clearTagsButton;

		// Token: 0x04001BC2 RID: 7106
		private int indexOfLanguageTags;
	}
}
