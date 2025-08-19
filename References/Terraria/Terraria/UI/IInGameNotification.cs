using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.UI
{
	// Token: 0x0200008F RID: 143
	public interface IInGameNotification
	{
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06001238 RID: 4664
		object CreationObject { get; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06001239 RID: 4665
		bool ShouldBeRemoved { get; }

		// Token: 0x0600123A RID: 4666
		void Update();

		// Token: 0x0600123B RID: 4667
		void DrawInGame(SpriteBatch spriteBatch, Vector2 bottomAnchorPosition);

		// Token: 0x0600123C RID: 4668
		void PushAnchor(ref Vector2 positionAnchorBottom);

		// Token: 0x0600123D RID: 4669
		void DrawInNotificationsArea(SpriteBatch spriteBatch, Rectangle area, ref int gamepadPointLocalIndexTouse);
	}
}
