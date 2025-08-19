using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terraria.Map
{
	// Token: 0x020003D2 RID: 978
	public class TeleportPylonsMapLayer : IMapLayer
	{
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06003364 RID: 13156 RVA: 0x00553D05 File Offset: 0x00551F05
		// (set) Token: 0x06003365 RID: 13157 RVA: 0x00553D0D File Offset: 0x00551F0D
		public bool Visible { get; set; } = true;

		// Token: 0x06003366 RID: 13158 RVA: 0x00553D18 File Offset: 0x00551F18
		public void Draw(ref MapOverlayDrawContext context, ref string text)
		{
			IReadOnlyList<TeleportPylonInfo> pylons = Main.PylonSystem.Pylons;
			Texture2D value = TextureAssets.Extra[182].Value;
			bool num2 = TeleportPylonsSystem.IsPlayerNearAPylon(Main.LocalPlayer) && (Main.DroneCameraTracker == null || !Main.DroneCameraTracker.IsInUse());
			for (int i = 0; i < pylons.Count; i++)
			{
				TeleportPylonInfo info = pylons[i];
				float num3 = 1f;
				float scaleIfSelected = num3 * 2f;
				Color color = Color.White;
				if (!num2)
				{
					color = Color.Gray * 0.5f;
				}
				if (PylonLoader.PreDrawMapIcon(ref context, ref text, ref info, ref num2, ref color, ref num3, ref scaleIfSelected))
				{
					ModPylon pylon = info.ModPylon;
					if (pylon != null)
					{
						pylon.DrawMapIcon(ref context, ref text, info, num2, color, num3, scaleIfSelected);
					}
					else if (context.Draw(value, info.PositionInTiles.ToVector2() + new Vector2(1.5f, 2f), color, new SpriteFrame(9, 1, (byte)info.TypeOfPylon, 0)
					{
						PaddingY = 0
					}, num3, scaleIfSelected, Alignment.Center).IsMouseOver)
					{
						Main.cancelWormHole = true;
						string itemNameValue = Lang.GetItemNameValue(TETeleportationPylon.GetPylonItemTypeFromTileStyle((int)info.TypeOfPylon));
						text = itemNameValue;
						if (Main.mouseLeft && Main.mouseLeftRelease)
						{
							Main.mouseLeftRelease = false;
							Main.mapFullscreen = false;
							PlayerInput.LockGamepadButtons("MouseLeft");
							Main.PylonSystem.RequestTeleportation(info, Main.LocalPlayer);
							SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
						}
					}
				}
			}
		}
	}
}
