using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// Specifies a background color to be used for the property, field, or class in the ModConfig UI.
	/// </summary>
	// Token: 0x02000369 RID: 873
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
	public class BackgroundColorAttribute : Attribute
	{
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06003086 RID: 12422 RVA: 0x0053CE51 File Offset: 0x0053B051
		public Color Color { get; }

		// Token: 0x06003087 RID: 12423 RVA: 0x0053CE59 File Offset: 0x0053B059
		public BackgroundColorAttribute(int r, int g, int b, int a = 255)
		{
			this.Color = new Color(r, g, b, a);
		}
	}
}
