using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.ModLoader.UI;

namespace Terraria.Initializers
{
	// Token: 0x020003EE RID: 1006
	public class LinkButtonsInitializer
	{
		// Token: 0x060034DE RID: 13534 RVA: 0x005668E8 File Offset: 0x00564AE8
		public static void Load()
		{
			List<TitleLinkButton> titleLinks = Main.TitleLinks;
			titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Discord", "https://discord.gg/terraria", 0));
			titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Instagram", "https://www.instagram.com/terraria_logic/", 1));
			titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Reddit", "https://www.reddit.com/r/Terraria/", 2));
			titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Twitter", "https://twitter.com/Terraria_Logic", 3));
			titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Forums", "https://forums.terraria.org/index.php", 4));
			titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Merch", "https://terraria.org/store", 5));
			titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Wiki", "https://terraria.wiki.gg/", 6));
			List<TitleLinkButton> tModLoaderTitleLinks = Main.tModLoaderTitleLinks;
			tModLoaderTitleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Discord", "https://tmodloader.net/discord", 0));
			tModLoaderTitleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Twitter", "https://twitter.com/tModLoader", 3));
			tModLoaderTitleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Wiki", "https://github.com/tModLoader/tModLoader/wiki", 6));
			tModLoaderTitleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Patreon", "https://www.patreon.com/tmodloader", 7));
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x005669F0 File Offset: 0x00564BF0
		private static TitleLinkButton MakeSimpleButton(string textKey, string linkUrl, int horizontalFrameIndex)
		{
			Asset<Texture2D> asset = UICommon.tModLoaderTitleLinkButtonsTexture;
			Rectangle value = asset.Frame(8, 2, horizontalFrameIndex, 0, 0, 0);
			Rectangle value2 = asset.Frame(8, 2, horizontalFrameIndex, 1, 0, 0);
			value.Width--;
			value.Height--;
			value2.Width--;
			value2.Height--;
			return new TitleLinkButton
			{
				TooltipTextKey = textKey,
				LinkUrl = linkUrl,
				FrameWehnSelected = new Rectangle?(value2),
				FrameWhenNotSelected = new Rectangle?(value),
				Image = asset
			};
		}
	}
}
