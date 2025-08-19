using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent
{
	// Token: 0x0200049D RID: 1181
	public interface INeedRenderTargetContent
	{
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06003937 RID: 14647
		bool IsReady { get; }

		// Token: 0x06003938 RID: 14648
		void PrepareRenderTarget(GraphicsDevice device, SpriteBatch spriteBatch);

		// Token: 0x06003939 RID: 14649
		void Reset();
	}
}
