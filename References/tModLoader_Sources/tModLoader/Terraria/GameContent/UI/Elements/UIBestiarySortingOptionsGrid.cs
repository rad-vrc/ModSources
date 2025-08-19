using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200050A RID: 1290
	public class UIBestiarySortingOptionsGrid : UIPanel
	{
		// Token: 0x14000063 RID: 99
		// (add) Token: 0x06003E15 RID: 15893 RVA: 0x005CF204 File Offset: 0x005CD404
		// (remove) Token: 0x06003E16 RID: 15894 RVA: 0x005CF23C File Offset: 0x005CD43C
		public event Action OnClickingOption;

		// Token: 0x06003E17 RID: 15895 RVA: 0x005CF274 File Offset: 0x005CD474
		public UIBestiarySortingOptionsGrid(EntrySorter<BestiaryEntry, IBestiarySortStep> sorter)
		{
			this._sorter = sorter;
			this._buttonsBySorting = new List<GroupOptionButton<int>>();
			this.Width = new StyleDimension(0f, 1f);
			this.Height = new StyleDimension(0f, 1f);
			this.BackgroundColor = new Color(35, 40, 83) * 0.5f;
			this.BorderColor = new Color(35, 40, 83) * 0.5f;
			this.IgnoresMouseInteraction = false;
			base.SetPadding(0f);
			this.BuildGrid();
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x005CF318 File Offset: 0x005CD518
		private void BuildGrid()
		{
			int num = 2;
			int num2 = 26 + num;
			int num3 = 0;
			for (int i = 0; i < this._sorter.Steps.Count; i++)
			{
				if (!this._sorter.Steps[i].HiddenFromSortOptions)
				{
					num3++;
				}
			}
			UIPanel uIPanel = new UIPanel
			{
				Width = new StyleDimension(126f, 0f),
				Height = new StyleDimension((float)(num3 * num2 + 5 + 3), 0f),
				HAlign = 1f,
				VAlign = 0f,
				Left = new StyleDimension(-118f, 0f),
				Top = new StyleDimension(0f, 0f)
			};
			uIPanel.BorderColor = new Color(89, 116, 213, 255) * 0.9f;
			uIPanel.BackgroundColor = new Color(73, 94, 171) * 0.9f;
			uIPanel.SetPadding(0f);
			base.Append(uIPanel);
			int num4 = 0;
			for (int j = 0; j < this._sorter.Steps.Count; j++)
			{
				IBestiarySortStep bestiarySortStep = this._sorter.Steps[j];
				if (!bestiarySortStep.HiddenFromSortOptions)
				{
					GroupOptionButton<int> groupOptionButton = new GroupOptionButton<int>(j, Language.GetText(bestiarySortStep.GetDisplayNameKey()), null, Color.White, null, 0.8f, 0.5f, 10f)
					{
						Width = new StyleDimension(114f, 0f),
						Height = new StyleDimension((float)(num2 - num), 0f),
						HAlign = 0.5f,
						Top = new StyleDimension((float)(5 + num2 * num4), 0f)
					};
					groupOptionButton.ShowHighlightWhenSelected = false;
					groupOptionButton.OnLeftClick += this.ClickOption;
					groupOptionButton.SetSnapPoint("SortSteps", num4, null, null);
					uIPanel.Append(groupOptionButton);
					this._buttonsBySorting.Add(groupOptionButton);
					num4++;
				}
			}
			foreach (GroupOptionButton<int> groupOptionButton2 in this._buttonsBySorting)
			{
				groupOptionButton2.SetCurrentOption(-1);
			}
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x005CF58C File Offset: 0x005CD78C
		private void ClickOption(UIMouseEvent evt, UIElement listeningElement)
		{
			int num = ((GroupOptionButton<int>)listeningElement).OptionValue;
			if (num == this._currentSelected)
			{
				num = this._defaultStepIndex;
			}
			foreach (GroupOptionButton<int> item in this._buttonsBySorting)
			{
				bool flag = num == item.OptionValue;
				item.SetCurrentOption(flag ? num : -1);
				if (flag)
				{
					item.SetColor(new Color(152, 175, 235), 1f);
				}
				else
				{
					item.SetColor(Colors.InventoryDefaultColor, 0.7f);
				}
			}
			this._currentSelected = num;
			this._sorter.SetPrioritizedStepIndex(num);
			if (this.OnClickingOption != null)
			{
				this.OnClickingOption();
			}
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x005CF668 File Offset: 0x005CD868
		public void GetEntriesToShow(out int maxEntriesWidth, out int maxEntriesHeight, out int maxEntriesToHave)
		{
			maxEntriesWidth = 1;
			maxEntriesHeight = this._buttonsBySorting.Count;
			maxEntriesToHave = this._buttonsBySorting.Count;
		}

		// Token: 0x040056B6 RID: 22198
		private EntrySorter<BestiaryEntry, IBestiarySortStep> _sorter;

		// Token: 0x040056B7 RID: 22199
		private List<GroupOptionButton<int>> _buttonsBySorting;

		// Token: 0x040056B8 RID: 22200
		private int _currentSelected = -1;

		// Token: 0x040056B9 RID: 22201
		private int _defaultStepIndex;
	}
}
