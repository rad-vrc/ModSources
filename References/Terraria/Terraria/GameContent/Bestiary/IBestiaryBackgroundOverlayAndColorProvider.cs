using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000301 RID: 769
	public interface IBestiaryBackgroundOverlayAndColorProvider
	{
		// Token: 0x060023C9 RID: 9161
		Asset<Texture2D> GetBackgroundOverlayImage();

		// Token: 0x060023CA RID: 9162
		Color? GetBackgroundOverlayColor();

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060023CB RID: 9163
		float DisplayPriority { get; }
	}
}
