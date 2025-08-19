using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// Specifies a slider color for ModConfig elements that use a slider. The default color is white.
	/// </summary>
	// Token: 0x0200036A RID: 874
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
	public class SliderColorAttribute : Attribute
	{
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06003088 RID: 12424 RVA: 0x0053CE71 File Offset: 0x0053B071
		public Color Color { get; }

		// Token: 0x06003089 RID: 12425 RVA: 0x0053CE79 File Offset: 0x0053B079
		public SliderColorAttribute(int r, int g, int b, int a = 255)
		{
			this.Color = new Color(r, g, b, a);
		}
	}
}
