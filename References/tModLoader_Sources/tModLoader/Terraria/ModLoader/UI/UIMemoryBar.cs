using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	// Token: 0x0200024D RID: 589
	internal class UIMemoryBar : UIElement
	{
		// Token: 0x060029BE RID: 10686 RVA: 0x00514270 File Offset: 0x00512470
		public UIMemoryBar()
		{
			this.Width.Set(0f, 1f);
			this.Height.Set(20f, 0f);
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x0051434E File Offset: 0x0051254E
		public override void OnActivate()
		{
			base.OnActivate();
			this.Show();
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x0051435C File Offset: 0x0051255C
		internal void Show()
		{
			UIMemoryBar.RecalculateMemoryNeeded = true;
			Task.Run(new Action(this.RecalculateMemory));
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x00514378 File Offset: 0x00512578
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (UIMemoryBar.RecalculateMemoryNeeded)
			{
				return;
			}
			Rectangle rectangle = base.GetInnerDimensions().ToRectangle();
			Point mouse;
			mouse..ctor(Main.mouseX, Main.mouseY);
			int xOffset = 0;
			bool drawHover = false;
			UIMemoryBar.MemoryBarItem hoverData = null;
			for (int i = 0; i < this._memoryBarItems.Count; i++)
			{
				UIMemoryBar.MemoryBarItem memoryBarData = this._memoryBarItems[i];
				int width = (int)((float)rectangle.Width * ((float)memoryBarData.Memory / (float)this.allocatedMemory));
				if (i == this._memoryBarItems.Count - 1)
				{
					width = rectangle.Right - xOffset - rectangle.X;
				}
				Rectangle drawArea;
				drawArea..ctor(rectangle.X + xOffset, rectangle.Y, width, rectangle.Height);
				xOffset += width;
				Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, drawArea, memoryBarData.DrawColor);
				if (!drawHover && drawArea.Contains(mouse))
				{
					drawHover = true;
					hoverData = memoryBarData;
				}
			}
			Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(rectangle.X, rectangle.Y, 2, rectangle.Height), Color.Black);
			Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, 2), Color.Black);
			Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(rectangle.X + rectangle.Width - 2, rectangle.Y, 2, rectangle.Height), Color.Black);
			Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - 2, rectangle.Width, 2), Color.Black);
			if (drawHover && hoverData != null)
			{
				UICommon.TooltipMouseText(hoverData.Tooltip);
			}
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x00514560 File Offset: 0x00512760
		private void RecalculateMemory()
		{
			this._memoryBarItems.Clear();
			long totalModMemory = 0L;
			int i = 0;
			foreach (KeyValuePair<string, ModMemoryUsage> entry in from v in MemoryTracking.modMemoryUsageEstimates
			orderby -v.Value.total
			select v)
			{
				string modName = entry.Key;
				ModMemoryUsage usage = entry.Value;
				if (usage.total > 0L && !(modName == "ModLoader"))
				{
					totalModMemory += usage.total;
					StringBuilder sb = new StringBuilder();
					sb.Append(ModLoader.GetMod(modName).DisplayName);
					StringBuilder stringBuilder = sb;
					StringBuilder stringBuilder2 = stringBuilder;
					StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(2, 1, stringBuilder);
					appendInterpolatedStringHandler.AppendLiteral("\n ");
					appendInterpolatedStringHandler.AppendFormatted(Language.GetTextValue("tModLoader.LastLoadRamUsage", UIMemoryBar.SizeSuffix(usage.total, 1)));
					stringBuilder2.Append(ref appendInterpolatedStringHandler);
					if (usage.managed > 0L)
					{
						stringBuilder = sb;
						StringBuilder stringBuilder3 = stringBuilder;
						appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(3, 1, stringBuilder);
						appendInterpolatedStringHandler.AppendLiteral("\n  ");
						appendInterpolatedStringHandler.AppendFormatted(Language.GetTextValue("tModLoader.ManagedMemory", UIMemoryBar.SizeSuffix(usage.managed, 1)));
						stringBuilder3.Append(ref appendInterpolatedStringHandler);
					}
					if (usage.code > 0L)
					{
						stringBuilder = sb;
						StringBuilder stringBuilder4 = stringBuilder;
						appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(3, 1, stringBuilder);
						appendInterpolatedStringHandler.AppendLiteral("\n  ");
						appendInterpolatedStringHandler.AppendFormatted(Language.GetTextValue("tModLoader.CodeMemory", UIMemoryBar.SizeSuffix(usage.code, 1)));
						stringBuilder4.Append(ref appendInterpolatedStringHandler);
					}
					if (usage.sounds > 0L)
					{
						stringBuilder = sb;
						StringBuilder stringBuilder5 = stringBuilder;
						appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(3, 1, stringBuilder);
						appendInterpolatedStringHandler.AppendLiteral("\n  ");
						appendInterpolatedStringHandler.AppendFormatted(Language.GetTextValue("tModLoader.SoundMemory", UIMemoryBar.SizeSuffix(usage.sounds, 1)));
						stringBuilder5.Append(ref appendInterpolatedStringHandler);
					}
					if (usage.textures > 0L)
					{
						stringBuilder = sb;
						StringBuilder stringBuilder6 = stringBuilder;
						appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(3, 1, stringBuilder);
						appendInterpolatedStringHandler.AppendLiteral("\n  ");
						appendInterpolatedStringHandler.AppendFormatted(Language.GetTextValue("tModLoader.TextureMemory", UIMemoryBar.SizeSuffix(usage.textures, 1)));
						stringBuilder6.Append(ref appendInterpolatedStringHandler);
					}
					this._memoryBarItems.Add(new UIMemoryBar.MemoryBarItem(sb.ToString(), usage.total, this._colors[i++ % this._colors.Length]));
				}
			}
			Process process = Process.GetCurrentProcess();
			process.Refresh();
			this.allocatedMemory = process.PrivateMemorySize64;
			long nonModMemory = this.allocatedMemory - totalModMemory;
			List<UIMemoryBar.MemoryBarItem> memoryBarItems = this._memoryBarItems;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 3);
			defaultInterpolatedStringHandler.AppendFormatted(Language.GetTextValue("tModLoader.TerrariaMemory", UIMemoryBar.SizeSuffix(nonModMemory, 1)));
			defaultInterpolatedStringHandler.AppendLiteral("\n ");
			defaultInterpolatedStringHandler.AppendFormatted(Language.GetTextValue("tModLoader.TotalMemory", UIMemoryBar.SizeSuffix(this.allocatedMemory, 1)));
			defaultInterpolatedStringHandler.AppendLiteral("\n\n");
			defaultInterpolatedStringHandler.AppendFormatted(Language.GetTextValue("tModLoader.InstalledMemory", UIMemoryBar.SizeSuffix(UIMemoryBar.GetTotalMemory(), 1)));
			memoryBarItems.Add(new UIMemoryBar.MemoryBarItem(defaultInterpolatedStringHandler.ToStringAndClear(), nonModMemory, Color.DeepSkyBlue));
			UIMemoryBar.RecalculateMemoryNeeded = false;
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x005148B0 File Offset: 0x00512AB0
		internal static string SizeSuffix(long value, int decimalPlaces = 1)
		{
			if (value < 0L)
			{
				return "-" + UIMemoryBar.SizeSuffix(-value, 1);
			}
			if (value == 0L)
			{
				return "0.0 bytes";
			}
			int mag = (int)Math.Log((double)value, 1024.0);
			decimal adjustedSize = value / (1L << mag * 10);
			if (Math.Round(adjustedSize, decimalPlaces) >= 1000m)
			{
				mag++;
				adjustedSize /= 1024m;
			}
			return string.Format("{0:n" + decimalPlaces.ToString() + "} {1}", adjustedSize, UIMemoryBar.SizeSuffixes[mag]);
		}

		/// <summary> Returns total installed RAM </summary>
		// Token: 0x060029C4 RID: 10692 RVA: 0x00514960 File Offset: 0x00512B60
		public static long GetTotalMemory()
		{
			return GC.GetGCMemoryInfo().TotalAvailableMemoryBytes;
		}

		// Token: 0x04001A87 RID: 6791
		internal static bool RecalculateMemoryNeeded = true;

		// Token: 0x04001A88 RID: 6792
		private readonly List<UIMemoryBar.MemoryBarItem> _memoryBarItems = new List<UIMemoryBar.MemoryBarItem>();

		// Token: 0x04001A89 RID: 6793
		private long allocatedMemory;

		// Token: 0x04001A8A RID: 6794
		private readonly Color[] _colors = new Color[]
		{
			new Color(232, 76, 61),
			new Color(155, 88, 181),
			new Color(27, 188, 155),
			new Color(243, 156, 17),
			new Color(45, 204, 112),
			new Color(241, 196, 15)
		};

		// Token: 0x04001A8B RID: 6795
		private static readonly string[] SizeSuffixes = new string[]
		{
			"bytes",
			"KB",
			"MB",
			"GB",
			"TB",
			"PB",
			"EB",
			"ZB",
			"YB"
		};

		// Token: 0x020009FE RID: 2558
		private class MemoryBarItem
		{
			// Token: 0x06005754 RID: 22356 RVA: 0x0069D9FA File Offset: 0x0069BBFA
			public MemoryBarItem(string tooltip, long memory, Color drawColor)
			{
				this.Tooltip = tooltip;
				this.Memory = memory;
				this.DrawColor = drawColor;
			}

			// Token: 0x04006C11 RID: 27665
			internal readonly string Tooltip;

			// Token: 0x04006C12 RID: 27666
			internal readonly long Memory;

			// Token: 0x04006C13 RID: 27667
			internal readonly Color DrawColor;
		}
	}
}
