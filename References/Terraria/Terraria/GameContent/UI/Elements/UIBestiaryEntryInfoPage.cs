using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000367 RID: 871
	public class UIBestiaryEntryInfoPage : UIPanel
	{
		// Token: 0x060027FD RID: 10237 RVA: 0x00587888 File Offset: 0x00585A88
		public UIBestiaryEntryInfoPage()
		{
			this.Width.Set(230f, 0f);
			this.Height.Set(0f, 1f);
			base.SetPadding(0f);
			this.BorderColor = new Color(89, 116, 213, 255);
			this.BackgroundColor = new Color(73, 94, 171);
			UIList uilist = new UIList
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f)
			};
			uilist.SetPadding(2f);
			uilist.PaddingBottom = 4f;
			uilist.PaddingTop = 4f;
			base.Append(uilist);
			this._list = uilist;
			uilist.ListPadding = 4f;
			uilist.ManualSortMethod = new Action<List<UIElement>>(this.ManualIfnoSortingMethod);
			UIScrollbar uiscrollbar = new UIScrollbar();
			uiscrollbar.SetView(100f, 1000f);
			uiscrollbar.Height.Set(-20f, 1f);
			uiscrollbar.HAlign = 1f;
			uiscrollbar.VAlign = 0.5f;
			uiscrollbar.Left.Set(-6f, 0f);
			this._scrollbar = uiscrollbar;
			this._list.SetScrollbar(this._scrollbar);
			this.CheckScrollBar();
			this.AppendBorderOverEverything();
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x005879F3 File Offset: 0x00585BF3
		public void UpdateScrollbar(int scrollWheelValue)
		{
			if (this._scrollbar != null)
			{
				this._scrollbar.ViewPosition -= (float)scrollWheelValue;
			}
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x00587A14 File Offset: 0x00585C14
		private void AppendBorderOverEverything()
		{
			UIPanel uipanel = new UIPanel
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(0f, 1f),
				IgnoresMouseInteraction = true
			};
			uipanel.BorderColor = new Color(89, 116, 213, 255);
			uipanel.BackgroundColor = Color.Transparent;
			base.Append(uipanel);
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		private void ManualIfnoSortingMethod(List<UIElement> list)
		{
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x00587A83 File Offset: 0x00585C83
		public override void Recalculate()
		{
			base.Recalculate();
			this.CheckScrollBar();
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x00587A94 File Offset: 0x00585C94
		private void CheckScrollBar()
		{
			if (this._scrollbar != null)
			{
				bool flag = this._scrollbar.CanScroll;
				flag = true;
				if (this._isScrollbarAttached && !flag)
				{
					base.RemoveChild(this._scrollbar);
					this._isScrollbarAttached = false;
					this._list.Width.Set(0f, 1f);
					return;
				}
				if (!this._isScrollbarAttached && flag)
				{
					base.Append(this._scrollbar);
					this._isScrollbarAttached = true;
					this._list.Width.Set(-20f, 1f);
				}
			}
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x00587B2D File Offset: 0x00585D2D
		public void FillInfoForEntry(BestiaryEntry entry, ExtraBestiaryInfoPageInformation extraInfo)
		{
			this._list.Clear();
			if (entry == null)
			{
				return;
			}
			this.AddInfoToList(entry, extraInfo);
			this.Recalculate();
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x00587B4C File Offset: 0x00585D4C
		private BestiaryUICollectionInfo GetUICollectionInfo(BestiaryEntry entry, ExtraBestiaryInfoPageInformation extraInfo)
		{
			IBestiaryUICollectionInfoProvider uiinfoProvider = entry.UIInfoProvider;
			BestiaryUICollectionInfo result;
			if (uiinfoProvider != null)
			{
				result = uiinfoProvider.GetEntryUICollectionInfo();
			}
			else
			{
				result = default(BestiaryUICollectionInfo);
			}
			result.OwnerEntry = entry;
			return result;
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x00587B80 File Offset: 0x00585D80
		private void AddInfoToList(BestiaryEntry entry, ExtraBestiaryInfoPageInformation extraInfo)
		{
			BestiaryUICollectionInfo uicollectionInfo = this.GetUICollectionInfo(entry, extraInfo);
			IEnumerable<IGrouping<UIBestiaryEntryInfoPage.BestiaryInfoCategory, IBestiaryInfoElement>> enumerable = from x in new List<IBestiaryInfoElement>(entry.Info).GroupBy(new Func<IBestiaryInfoElement, UIBestiaryEntryInfoPage.BestiaryInfoCategory>(this.GetBestiaryInfoCategory))
			orderby x.Key
			select x;
			UIElement item = null;
			foreach (IGrouping<UIBestiaryEntryInfoPage.BestiaryInfoCategory, IBestiaryInfoElement> source in enumerable)
			{
				if (source.Count<IBestiaryInfoElement>() != 0)
				{
					bool flag = false;
					foreach (IBestiaryInfoElement bestiaryInfoElement in source.OrderByDescending(new Func<IBestiaryInfoElement, float>(this.GetIndividualElementPriority)))
					{
						UIElement uielement = bestiaryInfoElement.ProvideUIElement(uicollectionInfo);
						if (uielement != null)
						{
							this._list.Add(uielement);
							flag = true;
						}
					}
					if (flag)
					{
						UIHorizontalSeparator uihorizontalSeparator = new UIHorizontalSeparator(2, true)
						{
							Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
							Color = new Color(89, 116, 213, 255) * 0.9f
						};
						this._list.Add(uihorizontalSeparator);
						item = uihorizontalSeparator;
					}
				}
			}
			this._list.Remove(item);
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x00587CE4 File Offset: 0x00585EE4
		private float GetIndividualElementPriority(IBestiaryInfoElement element)
		{
			IBestiaryPrioritizedElement bestiaryPrioritizedElement = element as IBestiaryPrioritizedElement;
			if (bestiaryPrioritizedElement != null)
			{
				return bestiaryPrioritizedElement.OrderPriority;
			}
			return 0f;
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x00587D08 File Offset: 0x00585F08
		private UIBestiaryEntryInfoPage.BestiaryInfoCategory GetBestiaryInfoCategory(IBestiaryInfoElement element)
		{
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

		// Token: 0x04004B38 RID: 19256
		private UIList _list;

		// Token: 0x04004B39 RID: 19257
		private UIScrollbar _scrollbar;

		// Token: 0x04004B3A RID: 19258
		private bool _isScrollbarAttached;

		// Token: 0x0200074D RID: 1869
		private enum BestiaryInfoCategory
		{
			// Token: 0x04006424 RID: 25636
			Nameplate,
			// Token: 0x04006425 RID: 25637
			Portrait,
			// Token: 0x04006426 RID: 25638
			FlavorText,
			// Token: 0x04006427 RID: 25639
			Stats,
			// Token: 0x04006428 RID: 25640
			ItemsFromCatchingNPC,
			// Token: 0x04006429 RID: 25641
			ItemsFromDrops,
			// Token: 0x0400642A RID: 25642
			Misc
		}
	}
}
