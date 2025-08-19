using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200012C RID: 300
	public interface IPlayerRenderer
	{
		// Token: 0x06001787 RID: 6023
		void DrawPlayers(Camera camera, IEnumerable<Player> players);

		// Token: 0x06001788 RID: 6024
		void DrawPlayerHead(Camera camera, Player drawPlayer, Vector2 position, float alpha = 1f, float scale = 1f, Color borderColor = default(Color));

		// Token: 0x06001789 RID: 6025
		void DrawPlayer(Camera camera, Player drawPlayer, Vector2 position, float rotation, Vector2 rotationOrigin, float shadow = 0f, float scale = 1f);
	}
}
