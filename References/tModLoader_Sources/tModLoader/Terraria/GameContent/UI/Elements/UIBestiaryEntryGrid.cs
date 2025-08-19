using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000503 RID: 1283
	public class UIBestiaryEntryGrid : UIElement
	{
		// Token: 0x14000061 RID: 97
		// (add) Token: 0x06003DD2 RID: 15826 RVA: 0x005CD604 File Offset: 0x005CB804
		// (remove) Token: 0x06003DD3 RID: 15827 RVA: 0x005CD63C File Offset: 0x005CB83C
		public event Action OnGridContentsChanged;

		// Token: 0x06003DD4 RID: 15828 RVA: 0x005CD674 File Offset: 0x005CB874
		public UIBestiaryEntryGrid(List<BestiaryEntry> workingSet, UIElement.MouseEvent clickOnEntryEvent)
		{
			this.Width = new StyleDimension(0f, 1f);
			this.Height = new StyleDimension(0f, 1f);
			this._workingSetEntries = workingSet;
			this._clickOnEntryEvent = clickOnEntryEvent;
			base.SetPadding(0f);
			this.UpdateEntries();
			this.FillBestiarySpaceWithEntries();
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x005CD6D6 File Offset: 0x005CB8D6
		public void UpdateEntries()
		{
			this._lastEntry = this._workingSetEntries.Count;
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x005CD6EC File Offset: 0x005CB8EC
		public void FillBestiarySpaceWithEntries()
		{
			base.RemoveAllChildren();
			this.UpdateEntries();
			int maxEntriesWidth;
			int maxEntriesHeight;
			int maxEntriesToHave;
			this.GetEntriesToShow(out maxEntriesWidth, out maxEntriesHeight, out maxEntriesToHave);
			this.FixBestiaryRange(0, maxEntriesToHave);
			int atEntryIndex = this._atEntryIndex;
			int num = Math.Min(this._lastEntry, atEntryIndex + maxEntriesToHave);
			List<BestiaryEntry> list = new List<BestiaryEntry>();
			for (int i = atEntryIndex; i < num; i++)
			{
				list.Add(this._workingSetEntries[i]);
			}
			int num2 = 0;
			float num3 = 0.5f / (float)maxEntriesWidth;
			float num4 = 0.5f / (float)maxEntriesHeight;
			for (int j = 0; j < maxEntriesHeight; j++)
			{
				int k = 0;
				while (k < maxEntriesWidth && num2 < list.Count)
				{
					UIElement uIElement = new UIBestiaryEntryButton(list[num2], false);
					num2++;
					uIElement.OnLeftClick += this._clickOnEntryEvent;
					uIElement.VAlign = (uIElement.HAlign = 0.5f);
					uIElement.Left.Set(0f, (float)k / (float)maxEntriesWidth - 0.5f + num3);
					uIElement.Top.Set(0f, (float)j / (float)maxEntriesHeight - 0.5f + num4);
					uIElement.SetSnapPoint("Entries", num2, new Vector2?(new Vector2(0.2f, 0.7f)), null);
					base.Append(uIElement);
					k++;
				}
			}
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x005CD85D File Offset: 0x005CBA5D
		public override void Recalculate()
		{
			base.Recalculate();
			this.FillBestiarySpaceWithEntries();
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x005CD86C File Offset: 0x005CBA6C
		public void GetEntriesToShow(out int maxEntriesWidth, out int maxEntriesHeight, out int maxEntriesToHave)
		{
			Rectangle rectangle = base.GetDimensions().ToRectangle();
			maxEntriesWidth = rectangle.Width / 72;
			maxEntriesHeight = rectangle.Height / 72;
			int num = 0;
			maxEntriesToHave = maxEntriesWidth * maxEntriesHeight - num;
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x005CD8AC File Offset: 0x005CBAAC
		public string GetRangeText()
		{
			int num3;
			int num4;
			int maxEntriesToHave;
			this.GetEntriesToShow(out num3, out num4, out maxEntriesToHave);
			int atEntryIndex = this._atEntryIndex;
			int num = Math.Min(this._lastEntry, atEntryIndex + maxEntriesToHave);
			int num2 = Math.Min(atEntryIndex + 1, num);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 3);
			defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
			defaultInterpolatedStringHandler.AppendLiteral("-");
			defaultInterpolatedStringHandler.AppendFormatted<int>(num);
			defaultInterpolatedStringHandler.AppendLiteral(" (");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this._lastEntry);
			defaultInterpolatedStringHandler.AppendLiteral(")");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x005CD938 File Offset: 0x005CBB38
		public void MakeButtonGoByOffset(UIElement element, int howManyPages)
		{
			element.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				this.OffsetLibraryByPages(howManyPages);
			};
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x005CD96C File Offset: 0x005CBB6C
		public void OffsetLibraryByPages(int howManyPages)
		{
			int num;
			int num2;
			int maxEntriesToHave;
			this.GetEntriesToShow(out num, out num2, out maxEntriesToHave);
			this.OffsetLibrary(howManyPages * maxEntriesToHave);
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x005CD990 File Offset: 0x005CBB90
		public void OffsetLibrary(int offset)
		{
			int num;
			int num2;
			int maxEntriesToHave;
			this.GetEntriesToShow(out num, out num2, out maxEntriesToHave);
			this.FixBestiaryRange(offset, maxEntriesToHave);
			this.FillBestiarySpaceWithEntries();
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x005CD9B7 File Offset: 0x005CBBB7
		private void FixBestiaryRange(int offset, int maxEntriesToHave)
		{
			this._atEntryIndex = Utils.Clamp<int>(this._atEntryIndex + offset, 0, Math.Max(0, this._lastEntry - maxEntriesToHave));
			if (this.OnGridContentsChanged != null)
			{
				this.OnGridContentsChanged();
			}
		}

		// Token: 0x0400569A RID: 22170
		private List<BestiaryEntry> _workingSetEntries;

		// Token: 0x0400569B RID: 22171
		private UIElement.MouseEvent _clickOnEntryEvent;

		// Token: 0x0400569C RID: 22172
		private int _atEntryIndex;

		// Token: 0x0400569D RID: 22173
		private int _lastEntry;
	}
}
