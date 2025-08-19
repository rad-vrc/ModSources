using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.Map
{
	// Token: 0x020003D1 RID: 977
	public class SpawnMapLayer : IMapLayer
	{
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06003360 RID: 13152 RVA: 0x00553C4C File Offset: 0x00551E4C
		// (set) Token: 0x06003361 RID: 13153 RVA: 0x00553C54 File Offset: 0x00551E54
		public bool Visible { get; set; } = true;

		// Token: 0x06003362 RID: 13154 RVA: 0x00553C60 File Offset: 0x00551E60
		public void Draw(ref MapOverlayDrawContext context, ref string text)
		{
			Player localPlayer = Main.LocalPlayer;
			Vector2 position;
			position..ctor((float)localPlayer.SpawnX, (float)localPlayer.SpawnY);
			Vector2 position2;
			position2..ctor((float)Main.spawnTileX, (float)Main.spawnTileY);
			if (context.Draw(TextureAssets.SpawnPoint.Value, position2, Alignment.Bottom).IsMouseOver)
			{
				text = Language.GetTextValue("UI.SpawnPoint");
			}
			if (localPlayer.SpawnX != -1 && context.Draw(TextureAssets.SpawnBed.Value, position, Alignment.Bottom).IsMouseOver)
			{
				text = Language.GetTextValue("UI.SpawnBed");
			}
		}
	}
}
