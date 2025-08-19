using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.Initializers
{
	// Token: 0x020000DF RID: 223
	public class LinkButtonsInitializer
	{
		// Token: 0x06001559 RID: 5465 RVA: 0x004B59E0 File Offset: 0x004B3BE0
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
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x004B5A8C File Offset: 0x004B3C8C
		private static TitleLinkButton MakeSimpleButton(string textKey, string linkUrl, int horizontalFrameIndex)
		{
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/TitleLinkButtons", 1);
			Rectangle value = asset.Frame(7, 2, horizontalFrameIndex, 0, 0, 0);
			Rectangle value2 = asset.Frame(7, 2, horizontalFrameIndex, 1, 0, 0);
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
