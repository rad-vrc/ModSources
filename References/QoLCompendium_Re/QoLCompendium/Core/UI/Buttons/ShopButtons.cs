using System;
using Microsoft.Xna.Framework;
using QoLCompendium.Core.UI.Other;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.UI;

namespace QoLCompendium.Core.UI.Buttons
{
	// Token: 0x0200029A RID: 666
	public class ShopButtons : GameInterfaceLayer
	{
		// Token: 0x0600113B RID: 4411 RVA: 0x00086DB6 File Offset: 0x00084FB6
		public ShopButtons() : base("QoLCompendium: Shop Buttons", InterfaceScaleType.UI)
		{
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00086DC4 File Offset: 0x00084FC4
		protected override bool DrawSelf()
		{
			if (Main.npcShop <= 0)
			{
				return true;
			}
			ShopExpander shop = ShopExpander.instance;
			float factor = 0.755f;
			Vector2 size = TextureAssets.InventoryBack6.Size() * factor;
			int width = TextureAssets.CraftUpButton.Width();
			int height = TextureAssets.CraftUpButton.Height();
			int width2 = TextureAssets.ScrollLeftButton.Value.Width;
			int maxX = (int)(73f + 560f * Main.inventoryScale);
			int invBottom = Main.instance.invBottom;
			int maxY = (int)((float)Main.instance.invBottom + 168f * Main.inventoryScale + size.Y - (float)height);
			bool changed = false;
			if (shop.page > 0)
			{
				Rectangle scrollLeft;
				scrollLeft..ctor(maxX, maxY - height - 4, width, height);
				Color color = Color.White * 0.8f;
				if (scrollLeft.Contains(Main.MouseScreen.ToPoint()))
				{
					Main.LocalPlayer.mouseInterface = true;
					if (Main.mouseLeft && Main.mouseLeftRelease)
					{
						shop.page--;
						changed = true;
					}
					color = Color.White;
				}
				Main.spriteBatch.Draw(TextureAssets.CraftUpButton.Value, scrollLeft, color);
			}
			if (shop.page < shop.pageCount - 1)
			{
				Rectangle scrollRight;
				scrollRight..ctor(maxX, maxY, width, height);
				Color color2 = Color.White * 0.8f;
				if (scrollRight.Contains(Main.MouseScreen.ToPoint()))
				{
					Main.LocalPlayer.mouseInterface = true;
					if (Main.mouseLeft && Main.mouseLeftRelease)
					{
						shop.page++;
						changed = true;
					}
					color2 = Color.White;
				}
				Main.spriteBatch.Draw(TextureAssets.CraftDownButton.Value, scrollRight, color2);
			}
			if (changed)
			{
				shop.Refresh();
				SoundEngine.PlaySound(SoundID.MenuTick, null, null);
			}
			return true;
		}
	}
}
