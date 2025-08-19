using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000506 RID: 1286
	public class UIBestiaryFilteringOptionsGrid : UIPanel
	{
		// Token: 0x14000062 RID: 98
		// (add) Token: 0x06003DED RID: 15853 RVA: 0x005CE0E0 File Offset: 0x005CC2E0
		// (remove) Token: 0x06003DEE RID: 15854 RVA: 0x005CE118 File Offset: 0x005CC318
		public event Action OnClickingOption;

		// Token: 0x06003DEF RID: 15855 RVA: 0x005CE150 File Offset: 0x005CC350
		public UIBestiaryFilteringOptionsGrid(EntryFilterer<BestiaryEntry, IBestiaryEntryFilter> filterer)
		{
			this._filterer = filterer;
			this._filterButtons = new List<GroupOptionButton<int>>();
			this._areFiltersAvailable = new List<bool>();
			this._filterAvailabilityTests = new List<List<BestiaryEntry>>();
			this.Width = new StyleDimension(0f, 1f);
			this.Height = new StyleDimension(0f, 1f);
			this.BackgroundColor = new Color(35, 40, 83) * 0.5f;
			this.BorderColor = new Color(35, 40, 83) * 0.5f;
			this.IgnoresMouseInteraction = false;
			base.SetPadding(0f);
			this.BuildContainer();
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x005CE204 File Offset: 0x005CC404
		private void BuildContainer()
		{
			int num;
			int num2;
			int widthWithSpacing;
			int heightWithSpacing;
			int perRow;
			float num3;
			float num4;
			int howManyRows;
			this.GetDisplaySettings(out num, out num2, out widthWithSpacing, out heightWithSpacing, out perRow, out num3, out num4, out howManyRows);
			UIPanel uIPanel = new UIPanel
			{
				Width = new StyleDimension((float)(perRow * widthWithSpacing + 10), 0f),
				Height = new StyleDimension((float)(howManyRows * heightWithSpacing + 10), 0f),
				HAlign = 1f,
				VAlign = 0f,
				Left = new StyleDimension(0f, 0f),
				Top = new StyleDimension(0f, 0f)
			};
			uIPanel.BorderColor = new Color(89, 116, 213, 255) * 0.9f;
			uIPanel.BackgroundColor = new Color(73, 94, 171) * 0.9f;
			uIPanel.SetPadding(0f);
			base.Append(uIPanel);
			this._container = uIPanel;
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x005CE2FC File Offset: 0x005CC4FC
		public void SetupAvailabilityTest(List<BestiaryEntry> allAvailableEntries)
		{
			this._filterAvailabilityTests.Clear();
			for (int i = 0; i < this._filterer.AvailableFilters.Count; i++)
			{
				List<BestiaryEntry> list = new List<BestiaryEntry>();
				this._filterAvailabilityTests.Add(list);
				IBestiaryEntryFilter bestiaryEntryFilter = this._filterer.AvailableFilters[i];
				for (int j = 0; j < allAvailableEntries.Count; j++)
				{
					if (bestiaryEntryFilter.FitsFilter(allAvailableEntries[j]))
					{
						list.Add(allAvailableEntries[j]);
					}
				}
			}
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x005CE380 File Offset: 0x005CC580
		public void UpdateAvailability()
		{
			int widthPerButton;
			int heightPerButton;
			int widthWithSpacing;
			int heightWithSpacing;
			int perRow;
			float offsetLeft;
			float offsetTop;
			int num5;
			this.GetDisplaySettings(out widthPerButton, out heightPerButton, out widthWithSpacing, out heightWithSpacing, out perRow, out offsetLeft, out offsetTop, out num5);
			this._container.RemoveAllChildren();
			this._filterButtons.Clear();
			this._areFiltersAvailable.Clear();
			int num = -1;
			int num2 = -1;
			for (int i = 0; i < this._filterer.AvailableFilters.Count; i++)
			{
				int num3 = i / perRow;
				int num4 = i % perRow;
				IBestiaryEntryFilter bestiaryEntryFilter = this._filterer.AvailableFilters[i];
				List<BestiaryEntry> entries = this._filterAvailabilityTests[i];
				if (this.GetIsFilterAvailableForEntries(bestiaryEntryFilter, entries))
				{
					GroupOptionButton<int> groupOptionButton = new GroupOptionButton<int>(i, null, null, Color.White, null, 1f, 0.5f, 10f)
					{
						Width = new StyleDimension((float)widthPerButton, 0f),
						Height = new StyleDimension((float)heightPerButton, 0f),
						HAlign = 0f,
						VAlign = 0f,
						Top = new StyleDimension(offsetTop + (float)(num3 * heightWithSpacing), 0f),
						Left = new StyleDimension(offsetLeft + (float)(num4 * widthWithSpacing), 0f)
					};
					groupOptionButton.OnLeftClick += this.ClickOption;
					groupOptionButton.SetSnapPoint("Filters", i, null, null);
					groupOptionButton.ShowHighlightWhenSelected = false;
					this.AddOnHover(bestiaryEntryFilter, groupOptionButton);
					this._container.Append(groupOptionButton);
					UIElement image = bestiaryEntryFilter.GetImage();
					if (image != null)
					{
						image.Left = new StyleDimension((float)num, 0f);
						image.Top = new StyleDimension((float)num2, 0f);
						groupOptionButton.Append(image);
					}
					this._filterButtons.Add(groupOptionButton);
				}
				else
				{
					this._filterer.ActiveFilters.Remove(bestiaryEntryFilter);
					GroupOptionButton<int> groupOptionButton2 = new GroupOptionButton<int>(-2, null, null, Color.White, null, 1f, 0.5f, 10f)
					{
						Width = new StyleDimension((float)widthPerButton, 0f),
						Height = new StyleDimension((float)heightPerButton, 0f),
						HAlign = 0f,
						VAlign = 0f,
						Top = new StyleDimension(offsetTop + (float)(num3 * heightWithSpacing), 0f),
						Left = new StyleDimension(offsetLeft + (float)(num4 * widthWithSpacing), 0f),
						FadeFromBlack = 0.5f
					};
					groupOptionButton2.ShowHighlightWhenSelected = false;
					groupOptionButton2.SetPadding(0f);
					groupOptionButton2.SetSnapPoint("Filters", i, null, null);
					Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Tags_Shadow");
					UIImageFramed uIImageFramed = new UIImageFramed(asset, asset.Frame(16, 5, 0, 4, 0, 0))
					{
						HAlign = 0.5f,
						VAlign = 0.5f,
						Color = Color.White * 0.2f
					};
					uIImageFramed.Left = new StyleDimension((float)num, 0f);
					uIImageFramed.Top = new StyleDimension((float)num2, 0f);
					groupOptionButton2.Append(uIImageFramed);
					this._filterButtons.Add(groupOptionButton2);
					this._container.Append(groupOptionButton2);
				}
			}
			this.UpdateButtonSelections();
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x005CE6DC File Offset: 0x005CC8DC
		public void GetEntriesToShow(out int maxEntriesWidth, out int maxEntriesHeight, out int maxEntriesToHave)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int perRow;
			float num5;
			float num6;
			int howManyRows;
			this.GetDisplaySettings(out num, out num2, out num3, out num4, out perRow, out num5, out num6, out howManyRows);
			maxEntriesWidth = perRow;
			maxEntriesHeight = howManyRows;
			maxEntriesToHave = this._filterer.AvailableFilters.Count;
		}

		// Token: 0x06003DF4 RID: 15860 RVA: 0x005CE718 File Offset: 0x005CC918
		private void GetDisplaySettings(out int widthPerButton, out int heightPerButton, out int widthWithSpacing, out int heightWithSpacing, out int perRow, out float offsetLeft, out float offsetTop, out int howManyRows)
		{
			widthPerButton = 32;
			heightPerButton = 32;
			int num = 2;
			widthWithSpacing = widthPerButton + num;
			heightWithSpacing = heightPerButton + num;
			perRow = (int)Math.Ceiling(Math.Sqrt((double)this._filterer.AvailableFilters.Count));
			perRow = 12;
			howManyRows = (int)Math.Ceiling((double)((float)this._filterer.AvailableFilters.Count / (float)perRow));
			offsetLeft = (float)(perRow * widthWithSpacing - num) * 0.5f;
			offsetTop = (float)(howManyRows * heightWithSpacing - num) * 0.5f;
			offsetLeft = 6f;
			offsetTop = 6f;
		}

		// Token: 0x06003DF5 RID: 15861 RVA: 0x005CE7B8 File Offset: 0x005CC9B8
		private void UpdateButtonSelections()
		{
			foreach (GroupOptionButton<int> filterButton in this._filterButtons)
			{
				bool flag = this._filterer.IsFilterActive(filterButton.OptionValue);
				filterButton.SetCurrentOption(flag ? filterButton.OptionValue : -1);
				if (flag)
				{
					filterButton.SetColor(new Color(152, 175, 235), 1f);
				}
				else
				{
					filterButton.SetColor(Colors.InventoryDefaultColor, 0.7f);
				}
			}
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x005CE85C File Offset: 0x005CCA5C
		private bool GetIsFilterAvailableForEntries(IBestiaryEntryFilter filter, List<BestiaryEntry> entries)
		{
			bool? forcedDisplay = filter.ForcedDisplay;
			if (forcedDisplay != null)
			{
				return forcedDisplay.Value;
			}
			for (int i = 0; i < entries.Count; i++)
			{
				if (filter.FitsFilter(entries[i]) && entries[i].UIInfoProvider.GetEntryUICollectionInfo().UnlockState > BestiaryEntryUnlockState.NotKnownAtAll_0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x005CE8C0 File Offset: 0x005CCAC0
		private void AddOnHover(IBestiaryEntryFilter filter, UIElement button)
		{
			button.OnUpdate += delegate(UIElement element)
			{
				this.ShowButtonName(element, filter);
			};
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x005CE8F4 File Offset: 0x005CCAF4
		private void ShowButtonName(UIElement element, IBestiaryEntryFilter number)
		{
			if (element.IsMouseHovering)
			{
				string textValue = Language.GetTextValue(number.GetDisplayNameKey());
				Main.instance.MouseText(textValue, 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x005CE928 File Offset: 0x005CCB28
		private void ClickOption(UIMouseEvent evt, UIElement listeningElement)
		{
			int optionValue = ((GroupOptionButton<int>)listeningElement).OptionValue;
			this._filterer.ToggleFilter(optionValue);
			this.UpdateButtonSelections();
			if (this.OnClickingOption != null)
			{
				this.OnClickingOption();
			}
		}

		// Token: 0x040056A7 RID: 22183
		private EntryFilterer<BestiaryEntry, IBestiaryEntryFilter> _filterer;

		// Token: 0x040056A8 RID: 22184
		private List<GroupOptionButton<int>> _filterButtons;

		// Token: 0x040056A9 RID: 22185
		private List<bool> _areFiltersAvailable;

		// Token: 0x040056AA RID: 22186
		private List<List<BestiaryEntry>> _filterAvailabilityTests;

		// Token: 0x040056AB RID: 22187
		private UIElement _container;
	}
}
