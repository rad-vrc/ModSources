using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.Map
{
	// Token: 0x020000CD RID: 205
	public class SpawnMapLayer : IMapLayer
	{
		// Token: 0x06001472 RID: 5234 RVA: 0x004A32CC File Offset: 0x004A14CC
		public void Draw(ref MapOverlayDrawContext context, ref string text)
		{
			Player localPlayer = Main.LocalPlayer;
			Vector2 position = new Vector2((float)localPlayer.SpawnX, (float)localPlayer.SpawnY);
			Vector2 position2 = new Vector2((float)Main.spawnTileX, (float)Main.spawnTileY);
			if (context.Draw(TextureAssets.SpawnPoint.Value, position2, Alignment.Bottom).IsMouseOver)
			{
				text = Language.GetTextValue("UI.SpawnPoint");
			}
			if (localPlayer.SpawnX == -1)
			{
				return;
			}
			if (context.Draw(TextureAssets.SpawnBed.Value, position, Alignment.Bottom).IsMouseOver)
			{
				text = Language.GetTextValue("UI.SpawnBed");
			}
		}
	}
}
