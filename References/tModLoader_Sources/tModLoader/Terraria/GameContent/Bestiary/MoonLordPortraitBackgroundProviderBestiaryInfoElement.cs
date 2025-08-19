using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200069A RID: 1690
	public class MoonLordPortraitBackgroundProviderBestiaryInfoElement : IBestiaryInfoElement, IBestiaryBackgroundImagePathAndColorProvider
	{
		// Token: 0x06004817 RID: 18455 RVA: 0x00647373 File Offset: 0x00645573
		public Asset<Texture2D> GetBackgroundImage()
		{
			return Main.Assets.Request<Texture2D>("Images/MapBG1");
		}

		// Token: 0x06004818 RID: 18456 RVA: 0x00647384 File Offset: 0x00645584
		public Color? GetBackgroundColor()
		{
			return new Color?(Color.Black);
		}

		// Token: 0x06004819 RID: 18457 RVA: 0x00647390 File Offset: 0x00645590
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}
	}
}
