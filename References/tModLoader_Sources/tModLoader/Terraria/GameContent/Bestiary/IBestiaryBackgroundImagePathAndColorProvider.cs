using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000686 RID: 1670
	public interface IBestiaryBackgroundImagePathAndColorProvider
	{
		// Token: 0x060047EB RID: 18411
		Asset<Texture2D> GetBackgroundImage();

		// Token: 0x060047EC RID: 18412
		Color? GetBackgroundColor();
	}
}
