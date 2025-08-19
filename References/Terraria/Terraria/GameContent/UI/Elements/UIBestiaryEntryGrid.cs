using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000360 RID: 864
	public class UIBestiaryEntryGrid : UIElement
	{
		// Token: 0x1400004C RID: 76
		// (add) Token: 0x060027C5 RID: 10181 RVA: 0x00585B38 File Offset: 0x00583D38
		// (remove) Token: 0x060027C6 RID: 10182 RVA: 0x00585B70 File Offset: 0x00583D70
		public event Action OnGridContentsChanged;

		// Token: 0x060027C7 RID: 10183 RVA: 0x00585BA8 File Offset: 0x00583DA8
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

		// Token: 0x060027C8 RID: 10184 RVA: 0x00585C0A File Offset: 0x00583E0A
		public void UpdateEntries()
		{
			this._lastEntry = this._workingSetEntries.Count;
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x00585C20 File Offset: 0x00583E20
		public void FillBestiarySpaceWithEntries()
		{
			base.RemoveAllChildren();
			this.UpdateEntries();
			int num;
			int num2;
			int num3;
			this.GetEntriesToShow(out num, out num2, out num3);
			this.FixBestiaryRange(0, num3);
			int atEntryIndex = this._atEntryIndex;
			int num4 = Math.Min(this._lastEntry, atEntryIndex + num3);
			List<BestiaryEntry> list = new List<BestiaryEntry>();
			for (int i = atEntryIndex; i < num4; i++)
			{
				list.Add(this._workingSetEntries[i]);
			}
			int num5 = 0;
			float num6 = 0.5f / (float)num;
			float num7 = 0.5f / (float)num2;
			for (int j = 0; j < num2; j++)
			{
				int num8 = 0;
				while (num8 < num && num5 < list.Count)
				{
					UIElement uielement = new UIBestiaryEntryButton(list[num5], false);
					num5++;
					uielement.OnLeftClick += this._clickOnEntryEvent;
					uielement.VAlign = (uielement.HAlign = 0.5f);
					uielement.Left.Set(0f, (float)num8 / (float)num - 0.5f + num6);
					uielement.Top.Set(0f, (float)j / (float)num2 - 0.5f + num7);
					uielement.SetSnapPoint("Entries", num5, new Vector2?(new Vector2(0.2f, 0.7f)), null);
					base.Append(uielement);
					num8++;
				}
			}
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x00585D91 File Offset: 0x00583F91
		public override void Recalculate()
		{
			base.Recalculate();
			this.FillBestiarySpaceWithEntries();
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x00585DA0 File Offset: 0x00583FA0
		public void GetEntriesToShow(out int maxEntriesWidth, out int maxEntriesHeight, out int maxEntriesToHave)
		{
			Rectangle rectangle = base.GetDimensions().ToRectangle();
			maxEntriesWidth = rectangle.Width / 72;
			maxEntriesHeight = rectangle.Height / 72;
			int num = 0;
			maxEntriesToHave = maxEntriesWidth * maxEntriesHeight - num;
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x00585DE0 File Offset: 0x00583FE0
		public string GetRangeText()
		{
			int num;
			int num2;
			int num3;
			this.GetEntriesToShow(out num, out num2, out num3);
			int atEntryIndex = this._atEntryIndex;
			int num4 = Math.Min(this._lastEntry, atEntryIndex + num3);
			int num5 = Math.Min(atEntryIndex + 1, num4);
			return string.Format("{0}-{1} ({2})", num5, num4, this._lastEntry);
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x00585E40 File Offset: 0x00584040
		public void MakeButtonGoByOffset(UIElement element, int howManyPages)
		{
			element.OnLeftClick += delegate(UIMouseEvent e, UIElement v)
			{
				this.OffsetLibraryByPages(howManyPages);
			};
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x00585E74 File Offset: 0x00584074
		public void OffsetLibraryByPages(int howManyPages)
		{
			int num;
			int num2;
			int num3;
			this.GetEntriesToShow(out num, out num2, out num3);
			this.OffsetLibrary(howManyPages * num3);
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x00585E98 File Offset: 0x00584098
		public void OffsetLibrary(int offset)
		{
			int num;
			int num2;
			int maxEntriesToHave;
			this.GetEntriesToShow(out num, out num2, out maxEntriesToHave);
			this.FixBestiaryRange(offset, maxEntriesToHave);
			this.FillBestiarySpaceWithEntries();
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x00585EBF File Offset: 0x005840BF
		private void FixBestiaryRange(int offset, int maxEntriesToHave)
		{
			this._atEntryIndex = Utils.Clamp<int>(this._atEntryIndex + offset, 0, Math.Max(0, this._lastEntry - maxEntriesToHave));
			if (this.OnGridContentsChanged != null)
			{
				this.OnGridContentsChanged();
			}
		}

		// Token: 0x04004B19 RID: 19225
		private List<BestiaryEntry> _workingSetEntries;

		// Token: 0x04004B1A RID: 19226
		private UIElement.MouseEvent _clickOnEntryEvent;

		// Token: 0x04004B1B RID: 19227
		private int _atEntryIndex;

		// Token: 0x04004B1C RID: 19228
		private int _lastEntry;
	}
}
