using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameInput;
using Terraria.UI;

namespace Terraria.Map
{
	// Token: 0x020000CE RID: 206
	public class TeleportPylonsMapLayer : IMapLayer
	{
		// Token: 0x06001474 RID: 5236 RVA: 0x004A3364 File Offset: 0x004A1564
		public void Draw(ref MapOverlayDrawContext context, ref string text)
		{
			List<TeleportPylonInfo> pylons = Main.PylonSystem.Pylons;
			float num = 1f;
			float scaleIfSelected = num * 2f;
			Texture2D value = TextureAssets.Extra[182].Value;
			bool flag = TeleportPylonsSystem.IsPlayerNearAPylon(Main.LocalPlayer) && (Main.DroneCameraTracker == null || !Main.DroneCameraTracker.IsInUse());
			Color color = Color.White;
			if (!flag)
			{
				color = Color.Gray * 0.5f;
			}
			for (int i = 0; i < pylons.Count; i++)
			{
				TeleportPylonInfo teleportPylonInfo = pylons[i];
				if (context.Draw(value, teleportPylonInfo.PositionInTiles.ToVector2() + new Vector2(1.5f, 2f), color, new SpriteFrame(9, 1, (byte)teleportPylonInfo.TypeOfPylon, 0)
				{
					PaddingY = 0
				}, num, scaleIfSelected, Alignment.Center).IsMouseOver)
				{
					Main.cancelWormHole = true;
					string itemNameValue = Lang.GetItemNameValue(TETeleportationPylon.GetPylonItemTypeFromTileStyle((int)teleportPylonInfo.TypeOfPylon));
					text = itemNameValue;
					if (Main.mouseLeft && Main.mouseLeftRelease)
					{
						Main.mouseLeftRelease = false;
						Main.mapFullscreen = false;
						PlayerInput.LockGamepadButtons("MouseLeft");
						Main.PylonSystem.RequestTeleportation(teleportPylonInfo, Main.LocalPlayer);
						SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
					}
				}
			}
		}
	}
}
