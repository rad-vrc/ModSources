using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200030F RID: 783
	public class MoonLordPortraitBackgroundProviderBestiaryInfoElement : IBestiaryInfoElement, IBestiaryBackgroundImagePathAndColorProvider
	{
		// Token: 0x06002404 RID: 9220 RVA: 0x005588C1 File Offset: 0x00556AC1
		public Asset<Texture2D> GetBackgroundImage()
		{
			return Main.Assets.Request<Texture2D>("Images/MapBG1", 1);
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x005588D3 File Offset: 0x00556AD3
		public Color? GetBackgroundColor()
		{
			return new Color?(Color.Black);
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}
	}
}
