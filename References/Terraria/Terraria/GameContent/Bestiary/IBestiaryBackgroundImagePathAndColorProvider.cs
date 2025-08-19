using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020002FF RID: 767
	public interface IBestiaryBackgroundImagePathAndColorProvider
	{
		// Token: 0x060023C6 RID: 9158
		Asset<Texture2D> GetBackgroundImage();

		// Token: 0x060023C7 RID: 9159
		Color? GetBackgroundColor();
	}
}
