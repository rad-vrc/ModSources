using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.WorldChanges
{
	// Token: 0x02000214 RID: 532
	public class MapTeleporting : ModSystem
	{
		// Token: 0x06000D02 RID: 3330 RVA: 0x00064FE0 File Offset: 0x000631E0
		public static void TryToTeleportPlayerOnMap()
		{
			if (Main.mouseRight && Main.keyState.IsKeyUp(162))
			{
				Vector2 worldSize;
				worldSize..ctor((float)(Main.maxTilesX * 16), (float)(Main.maxTilesY * 16));
				Vector2 mousePos;
				mousePos..ctor((float)(Main.mouseX - Main.screenWidth / 2), (float)(Main.mouseY - Main.screenHeight / 2));
				Vector2 fullscreenMapPos = Main.mapFullscreenPos;
				mousePos /= 16f;
				mousePos *= 16f / Main.mapFullscreenScale;
				fullscreenMapPos += mousePos;
				fullscreenMapPos *= 16f;
				fullscreenMapPos.Y -= (float)Main.LocalPlayer.height;
				if (fullscreenMapPos.X < 0f)
				{
					fullscreenMapPos.X = 0f;
				}
				else if (fullscreenMapPos.X + (float)Main.LocalPlayer.width > worldSize.X)
				{
					fullscreenMapPos.X = worldSize.X - (float)Main.LocalPlayer.width;
				}
				if (fullscreenMapPos.Y < 0f)
				{
					fullscreenMapPos.Y = 0f;
				}
				else if (fullscreenMapPos.Y + (float)Main.LocalPlayer.height > worldSize.Y)
				{
					fullscreenMapPos.Y = worldSize.Y - (float)Main.LocalPlayer.height;
				}
				if (Main.LocalPlayer.position != fullscreenMapPos)
				{
					Main.LocalPlayer.Teleport(fullscreenMapPos, 1, 0);
					Main.LocalPlayer.position = fullscreenMapPos;
					Main.LocalPlayer.velocity = Vector2.Zero;
					Main.LocalPlayer.fallStart = (int)(Main.LocalPlayer.position.Y / 16f);
					NetMessage.SendData(65, -1, -1, null, 0, (float)Main.myPlayer, fullscreenMapPos.X, fullscreenMapPos.Y, 1, 0, 0);
				}
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x000651AA File Offset: 0x000633AA
		public override void PostDrawFullscreenMap(ref string mouseText)
		{
			if (QoLCompendium.mainConfig.MapTeleporting)
			{
				MapTeleporting.TryToTeleportPlayerOnMap();
			}
		}
	}
}
