using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent
{
	// Token: 0x020001D7 RID: 471
	public class NPCHeadDrawRenderTargetContent : AnOutlinedDrawRenderTargetContent
	{
		// Token: 0x06001C1F RID: 7199 RVA: 0x004F1887 File Offset: 0x004EFA87
		public void SetTexture(Texture2D texture)
		{
			if (this._theTexture == texture)
			{
				return;
			}
			this._theTexture = texture;
			this._wasPrepared = false;
			this.width = texture.Width + 8;
			this.height = texture.Height + 8;
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x004F18C0 File Offset: 0x004EFAC0
		internal override void DrawTheContent(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(this._theTexture, new Vector2(4f, 4f), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}

		// Token: 0x04004370 RID: 17264
		private Texture2D _theTexture;
	}
}
