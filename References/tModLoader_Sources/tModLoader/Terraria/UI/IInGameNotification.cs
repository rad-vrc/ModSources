using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.UI
{
	// Token: 0x020000A4 RID: 164
	public interface IInGameNotification
	{
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060014F1 RID: 5361
		bool ShouldBeRemoved { get; }

		// Token: 0x060014F2 RID: 5362
		void Update();

		// Token: 0x060014F3 RID: 5363
		void DrawInGame(SpriteBatch spriteBatch, Vector2 bottomAnchorPosition);

		// Token: 0x060014F4 RID: 5364
		void PushAnchor(ref Vector2 positionAnchorBottom);
	}
}
