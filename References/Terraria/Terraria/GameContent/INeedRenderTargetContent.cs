using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent
{
	// Token: 0x020001D3 RID: 467
	public interface INeedRenderTargetContent
	{
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06001C0A RID: 7178
		bool IsReady { get; }

		// Token: 0x06001C0B RID: 7179
		void PrepareRenderTarget(GraphicsDevice device, SpriteBatch spriteBatch);

		// Token: 0x06001C0C RID: 7180
		void Reset();
	}
}
