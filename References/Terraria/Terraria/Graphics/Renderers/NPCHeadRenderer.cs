using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200012F RID: 303
	public class NPCHeadRenderer : INeedRenderTargetContent
	{
		// Token: 0x0600179C RID: 6044 RVA: 0x004D4E1E File Offset: 0x004D301E
		public NPCHeadRenderer(Asset<Texture2D>[] matchingArray)
		{
			this._matchingArray = matchingArray;
			this.Reset();
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x004D4E33 File Offset: 0x004D3033
		public void Reset()
		{
			this._contents = new NPCHeadDrawRenderTargetContent[this._matchingArray.Length];
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x004D4E48 File Offset: 0x004D3048
		public void DrawWithOutlines(Entity entity, int headId, Vector2 position, Color color, float rotation, float scale, SpriteEffects effects)
		{
			if (this._contents[headId] == null)
			{
				this._contents[headId] = new NPCHeadDrawRenderTargetContent();
				this._contents[headId].SetTexture(this._matchingArray[headId].Value);
			}
			NPCHeadDrawRenderTargetContent npcheadDrawRenderTargetContent = this._contents[headId];
			if (npcheadDrawRenderTargetContent.IsReady)
			{
				RenderTarget2D target = npcheadDrawRenderTargetContent.GetTarget();
				Main.spriteBatch.Draw(target, position, null, color, rotation, target.Size() / 2f, scale, effects, 0f);
				return;
			}
			npcheadDrawRenderTargetContent.Request();
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		public bool IsReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x004D4ED8 File Offset: 0x004D30D8
		public void PrepareRenderTarget(GraphicsDevice device, SpriteBatch spriteBatch)
		{
			for (int i = 0; i < this._contents.Length; i++)
			{
				if (this._contents[i] != null && !this._contents[i].IsReady)
				{
					this._contents[i].PrepareRenderTarget(device, spriteBatch);
				}
			}
		}

		// Token: 0x04001452 RID: 5202
		private NPCHeadDrawRenderTargetContent[] _contents;

		// Token: 0x04001453 RID: 5203
		private Asset<Texture2D>[] _matchingArray;
	}
}
