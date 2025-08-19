using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	// Token: 0x0200023D RID: 573
	internal class UIBestiaryBar : UIElement
	{
		// Token: 0x060028FD RID: 10493 RVA: 0x005105AC File Offset: 0x0050E7AC
		public UIBestiaryBar(BestiaryDatabase db)
		{
			this._db = db;
			this._bestiaryBarItems = new List<UIBestiaryBar.BestiaryBarItem>();
			this.RecalculateBars();
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x00510670 File Offset: 0x0050E870
		public void RecalculateBars()
		{
			this._bestiaryBarItems.Clear();
			int total = this._db.Entries.Count;
			int totalCollected = this._db.Entries.Count((BestiaryEntry e) => e.UIInfoProvider.GetEntryUICollectionInfo().UnlockState > BestiaryEntryUnlockState.NotKnownAtAll_0);
			List<UIBestiaryBar.BestiaryBarItem> bestiaryBarItems = this._bestiaryBarItems;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Total: ");
			defaultInterpolatedStringHandler.AppendFormatted<float>((float)totalCollected / (float)total * 100f, "N2");
			defaultInterpolatedStringHandler.AppendLiteral("% Collected");
			bestiaryBarItems.Add(new UIBestiaryBar.BestiaryBarItem(defaultInterpolatedStringHandler.ToStringAndClear(), total, totalCollected, Main.OurFavoriteColor));
			List<BestiaryEntry> items = this._db.GetBestiaryEntriesByMod(null);
			int collected = items.Count((BestiaryEntry oe) => oe.UIInfoProvider.GetEntryUICollectionInfo().UnlockState > BestiaryEntryUnlockState.NotKnownAtAll_0);
			List<UIBestiaryBar.BestiaryBarItem> bestiaryBarItems2 = this._bestiaryBarItems;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Terraria: ");
			defaultInterpolatedStringHandler.AppendFormatted<float>((float)collected / (float)items.Count * 100f, "N2");
			defaultInterpolatedStringHandler.AppendLiteral("% Collected");
			bestiaryBarItems2.Add(new UIBestiaryBar.BestiaryBarItem(defaultInterpolatedStringHandler.ToStringAndClear(), items.Count, collected, this._colors[0]));
			for (int i = 1; i < ModLoader.Mods.Length; i++)
			{
				items = this._db.GetBestiaryEntriesByMod(ModLoader.Mods[i]);
				if (items != null)
				{
					collected = items.Count((BestiaryEntry oe) => oe.UIInfoProvider.GetEntryUICollectionInfo().UnlockState > BestiaryEntryUnlockState.NotKnownAtAll_0);
					List<UIBestiaryBar.BestiaryBarItem> bestiaryBarItems3 = this._bestiaryBarItems;
					defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 2);
					defaultInterpolatedStringHandler.AppendFormatted(ModLoader.Mods[i].DisplayName);
					defaultInterpolatedStringHandler.AppendLiteral(": ");
					defaultInterpolatedStringHandler.AppendFormatted<float>((float)collected / (float)items.Count * 100f, "N2");
					defaultInterpolatedStringHandler.AppendLiteral("% Collected");
					bestiaryBarItems3.Add(new UIBestiaryBar.BestiaryBarItem(defaultInterpolatedStringHandler.ToStringAndClear(), items.Count, collected, this._colors[i % this._colors.Length]));
				}
			}
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x005108A0 File Offset: 0x0050EAA0
		protected override void DrawSelf(SpriteBatch sb)
		{
			int xOffset = 0;
			Rectangle rectangle = base.GetDimensions().ToRectangle();
			rectangle.Height -= 3;
			bool drawHover = false;
			UIBestiaryBar.BestiaryBarItem hoverData = null;
			for (int i = 1; i < this._bestiaryBarItems.Count; i++)
			{
				UIBestiaryBar.BestiaryBarItem barData = this._bestiaryBarItems[i];
				int offset = (int)((float)rectangle.Width * ((float)barData.EntryCount / (float)this._db.Entries.Count));
				if (i == this._bestiaryBarItems.Count - 1)
				{
					offset = rectangle.Width - xOffset;
				}
				int width = (int)((float)offset * ((float)barData.CompletedCount / (float)barData.EntryCount));
				Rectangle drawArea;
				drawArea..ctor(rectangle.X + xOffset, rectangle.Y, width, rectangle.Height);
				Rectangle outlineArea;
				outlineArea..ctor(rectangle.X + xOffset, rectangle.Y, offset, rectangle.Height);
				xOffset += offset;
				sb.Draw(TextureAssets.MagicPixel.Value, outlineArea, barData.DrawColor * 0.3f);
				sb.Draw(TextureAssets.MagicPixel.Value, drawArea, barData.DrawColor);
				if (!drawHover && outlineArea.Contains(new Point(Main.mouseX, Main.mouseY)))
				{
					drawHover = true;
					hoverData = barData;
				}
			}
			UIBestiaryBar.BestiaryBarItem bottomData = this._bestiaryBarItems[0];
			int bottomWidth = (int)((float)rectangle.Width * ((float)bottomData.CompletedCount / (float)bottomData.EntryCount));
			Rectangle bottomDrawArea;
			bottomDrawArea..ctor(rectangle.X, rectangle.Bottom, bottomWidth, 3);
			Rectangle bottomOutlineArea;
			bottomOutlineArea..ctor(rectangle.X, rectangle.Bottom, rectangle.Width, 3);
			sb.Draw(TextureAssets.MagicPixel.Value, bottomOutlineArea, bottomData.DrawColor * 0.3f);
			sb.Draw(TextureAssets.MagicPixel.Value, bottomDrawArea, bottomData.DrawColor);
			if (!drawHover && bottomOutlineArea.Contains(new Point(Main.mouseX, Main.mouseY)))
			{
				drawHover = true;
				hoverData = bottomData;
			}
			if (drawHover && hoverData != null)
			{
				Main.instance.MouseText(hoverData.Tooltop, 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x040019FE RID: 6654
		private BestiaryDatabase _db;

		// Token: 0x040019FF RID: 6655
		private List<UIBestiaryBar.BestiaryBarItem> _bestiaryBarItems;

		// Token: 0x04001A00 RID: 6656
		private readonly Color[] _colors = new Color[]
		{
			new Color(232, 76, 61),
			new Color(155, 88, 181),
			new Color(27, 188, 155),
			new Color(243, 156, 17),
			new Color(45, 204, 112),
			new Color(241, 196, 15)
		};

		// Token: 0x020009F1 RID: 2545
		private class BestiaryBarItem
		{
			// Token: 0x0600572C RID: 22316 RVA: 0x0069D5EC File Offset: 0x0069B7EC
			public BestiaryBarItem(string tooltop, int entryCount, int completedCount, Color drawColor)
			{
				this.Tooltop = tooltop;
				this.EntryCount = entryCount;
				this.CompletedCount = completedCount;
				this.DrawColor = drawColor;
			}

			// Token: 0x04006BE8 RID: 27624
			internal readonly string Tooltop;

			// Token: 0x04006BE9 RID: 27625
			internal readonly int EntryCount;

			// Token: 0x04006BEA RID: 27626
			internal readonly int CompletedCount;

			// Token: 0x04006BEB RID: 27627
			internal readonly Color DrawColor;
		}
	}
}
