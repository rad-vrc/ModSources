using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000687 RID: 1671
	public interface IBestiaryBackgroundOverlayAndColorProvider
	{
		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x060047ED RID: 18413
		float DisplayPriority { get; }

		// Token: 0x060047EE RID: 18414
		Asset<Texture2D> GetBackgroundOverlayImage();

		// Token: 0x060047EF RID: 18415
		Color? GetBackgroundOverlayColor();
	}
}
