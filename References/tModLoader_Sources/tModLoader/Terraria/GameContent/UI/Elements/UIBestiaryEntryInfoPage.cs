using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000505 RID: 1285
	public class UIBestiaryEntryInfoPage : UIPanel
	{
		// Token: 0x06003DE2 RID: 15842 RVA: 0x005CDBFC File Offset: 0x005CBDFC
		public UIBestiaryEntryInfoPage()
		{
			this.Width.Set(230f, 0f);
			this.Height.Set(0f, 1f);
			base.SetPadding(0f);
			this.BorderColor = new Color(89, 116, 213, 255);
			this.BackgroundColor = new Color(73, 94, 171);
			UIList uIList = new UIList
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f)
			};
			uIList.SetPadding(2f);
			uIList.PaddingBottom = 4f;
			uIList.PaddingTop = 4f;
			base.Append(uIList);
			this._list = uIList;
			uIList.ListPadding = 4f;
			uIList.ManualSortMethod = new Action<List<UIElement>>(this.ManualIfnoSortingMethod);
			UIScrollbar uIScrollbar = new UIScrollbar();
			uIScrollbar.SetView(100f, 1000f);
			uIScrollbar.Height.Set(-20f, 1f);
			uIScrollbar.HAlign = 1f;
			uIScrollbar.VAlign = 0.5f;
			uIScrollbar.Left.Set(-6f, 0f);
			this._scrollbar = uIScrollbar;
			this._list.SetScrollbar(this._scrollbar);
			this.CheckScrollBar();
			this.AppendBorderOverEverything();
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x005CDD67 File Offset: 0x005CBF67
		public void UpdateScrollbar(int scrollWheelValue)
		{
			if (this._scrollbar != null)
			{
				this._scrollbar.ViewPosition -= (float)scrollWheelValue;
			}
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x005CDD88 File Offset: 0x005CBF88
		private void AppendBorderOverEverything()
		{
			UIPanel uIPanel = new UIPanel
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(0f, 1f),
				IgnoresMouseInteraction = true
			};
			uIPanel.BorderColor = new Color(89, 116, 213, 255);
			uIPanel.BackgroundColor = Color.Transparent;
			base.Append(uIPanel);
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x005CDDF7 File Offset: 0x005CBFF7
		private void ManualIfnoSortingMethod(List<UIElement> list)
		{
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x005CDDF9 File Offset: 0x005CBFF9
		public override void Recalculate()
		{
			base.Recalculate();
			this.CheckScrollBar();
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x005CDE08 File Offset: 0x005CC008
		private void CheckScrollBar()
		{
			if (this._scrollbar != null)
			{
				bool canScroll = this._scrollbar.CanScroll;
				canScroll = true;
				if (this._isScrollbarAttached && !canScroll)
				{
					base.RemoveChild(this._scrollbar);
					this._isScrollbarAttached = false;
					this._list.Width.Set(0f, 1f);
					return;
				}
				if (!this._isScrollbarAttached && canScroll)
				{
					base.Append(this._scrollbar);
					this._isScrollbarAttached = true;
					this._list.Width.Set(-20f, 1f);
				}
			}
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x005CDEA1 File Offset: 0x005CC0A1
		public void FillInfoForEntry(BestiaryEntry entry, ExtraBestiaryInfoPageInformation extraInfo)
		{
			this._list.Clear();
			if (entry != null)
			{
				this.AddInfoToList(entry, extraInfo);
				this.Recalculate();
			}
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x005CDEC0 File Offset: 0x005CC0C0
		private BestiaryUICollectionInfo GetUICollectionInfo(BestiaryEntry entry, ExtraBestiaryInfoPageInformation extraInfo)
		{
			IBestiaryUICollectionInfoProvider uiinfoProvider = entry.UIInfoProvider;
			BestiaryUICollectionInfo result = (uiinfoProvider != null) ? uiinfoProvider.GetEntryUICollectionInfo() : default(BestiaryUICollectionInfo);
			result.OwnerEntry = entry;
			return result;
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x005CDEF4 File Offset: 0x005CC0F4
		private void AddInfoToList(BestiaryEntry entry, ExtraBestiaryInfoPageInformation extraInfo)
		{
			BestiaryUICollectionInfo uICollectionInfo = this.GetUICollectionInfo(entry, extraInfo);
			IEnumerable<IGrouping<UIBestiaryEntryInfoPage.BestiaryInfoCategory, IBestiaryInfoElement>> enumerable = from x in new List<IBestiaryInfoElement>(entry.Info).GroupBy(new Func<IBestiaryInfoElement, UIBestiaryEntryInfoPage.BestiaryInfoCategory>(this.GetBestiaryInfoCategory))
			orderby x.Key
			select x;
			UIElement item = null;
			foreach (IGrouping<UIBestiaryEntryInfoPage.BestiaryInfoCategory, IBestiaryInfoElement> item2 in enumerable)
			{
				if (item2.Count<IBestiaryInfoElement>() != 0)
				{
					bool flag = false;
					foreach (IBestiaryInfoElement bestiaryInfoElement in item2.OrderByDescending(new Func<IBestiaryInfoElement, float>(this.GetIndividualElementPriority)))
					{
						UIElement uIElement = bestiaryInfoElement.ProvideUIElement(uICollectionInfo);
						if (uIElement != null)
						{
							this._list.Add(uIElement);
							flag = true;
						}
					}
					if (flag)
					{
						UIHorizontalSeparator uIHorizontalSeparator = new UIHorizontalSeparator(2, true)
						{
							Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
							Color = new Color(89, 116, 213, 255) * 0.9f
						};
						this._list.Add(uIHorizontalSeparator);
						item = uIHorizontalSeparator;
					}
				}
			}
			this._list.Remove(item);
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x005CE058 File Offset: 0x005CC258
		private float GetIndividualElementPriority(IBestiaryInfoElement element)
		{
			IBestiaryPrioritizedElement bestiaryPrioritizedElement = element as IBestiaryPrioritizedElement;
			if (bestiaryPrioritizedElement != null)
			{
				return bestiaryPrioritizedElement.OrderPriority;
			}
			return 0f;
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x005CE07C File Offset: 0x005CC27C
		private UIBestiaryEntryInfoPage.BestiaryInfoCategory GetBestiaryInfoCategory(IBestiaryInfoElement element)
		{
			ICategorizedBestiaryInfoElement categorizedElement = element as ICategorizedBestiaryInfoElement;
			if (categorizedElement != null)
			{
				return categorizedElement.ElementCategory;
			}
			if (element is NPCPortraitInfoElement)
			{
				return UIBestiaryEntryInfoPage.BestiaryInfoCategory.Portrait;
			}
			if (element is FlavorTextBestiaryInfoElement)
			{
				return UIBestiaryEntryInfoPage.BestiaryInfoCategory.FlavorText;
			}
			if (element is NamePlateInfoElement)
			{
				return UIBestiaryEntryInfoPage.BestiaryInfoCategory.Nameplate;
			}
			if (element is ItemFromCatchingNPCBestiaryInfoElement)
			{
				return UIBestiaryEntryInfoPage.BestiaryInfoCategory.ItemsFromCatchingNPC;
			}
			if (element is ItemDropBestiaryInfoElement)
			{
				return UIBestiaryEntryInfoPage.BestiaryInfoCategory.ItemsFromDrops;
			}
			if (element is NPCStatsReportInfoElement || element is NPCKillCounterInfoElement)
			{
				return UIBestiaryEntryInfoPage.BestiaryInfoCategory.Stats;
			}
			return UIBestiaryEntryInfoPage.BestiaryInfoCategory.Misc;
		}

		// Token: 0x040056A4 RID: 22180
		private UIList _list;

		// Token: 0x040056A5 RID: 22181
		private UIScrollbar _scrollbar;

		// Token: 0x040056A6 RID: 22182
		private bool _isScrollbarAttached;

		// Token: 0x02000C11 RID: 3089
		public enum BestiaryInfoCategory
		{
			// Token: 0x04007856 RID: 30806
			Nameplate,
			// Token: 0x04007857 RID: 30807
			Portrait,
			// Token: 0x04007858 RID: 30808
			FlavorText,
			// Token: 0x04007859 RID: 30809
			Stats,
			// Token: 0x0400785A RID: 30810
			ItemsFromCatchingNPC,
			// Token: 0x0400785B RID: 30811
			ItemsFromDrops,
			// Token: 0x0400785C RID: 30812
			Misc
		}
	}
}
