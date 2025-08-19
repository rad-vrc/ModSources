using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent
{
	// Token: 0x020004A6 RID: 1190
	public class NPCHeadDrawRenderTargetContent : AnOutlinedDrawRenderTargetContent
	{
		// Token: 0x06003975 RID: 14709 RVA: 0x005982EC File Offset: 0x005964EC
		public void SetTexture(Texture2D texture)
		{
			if (this._theTexture != texture)
			{
				this._theTexture = texture;
				this._wasPrepared = false;
				this.width = texture.Width + 8;
				this.height = texture.Height + 8;
			}
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x00598324 File Offset: 0x00596524
		internal override void DrawTheContent(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(this._theTexture, new Vector2(4f, 4f), null, Color.White, 0f, Vector2.Zero, 1f, 0, 0f);
		}

		// Token: 0x0400525C RID: 21084
		private Texture2D _theTexture;
	}
}
