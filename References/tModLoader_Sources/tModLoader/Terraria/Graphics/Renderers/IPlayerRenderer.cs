using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000452 RID: 1106
	public interface IPlayerRenderer
	{
		// Token: 0x06003676 RID: 13942
		void DrawPlayers(Camera camera, IEnumerable<Player> players);

		// Token: 0x06003677 RID: 13943
		void DrawPlayerHead(Camera camera, Player drawPlayer, Vector2 position, float alpha = 1f, float scale = 1f, Color borderColor = default(Color));

		// Token: 0x06003678 RID: 13944
		void DrawPlayer(Camera camera, Player drawPlayer, Vector2 position, float rotation, Vector2 rotationOrigin, float shadow = 0f, float scale = 1f);
	}
}
