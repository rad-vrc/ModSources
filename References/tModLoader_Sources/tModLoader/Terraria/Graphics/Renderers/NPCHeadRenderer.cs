using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000458 RID: 1112
	public class NPCHeadRenderer : INeedRenderTargetContent
	{
		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060036A4 RID: 13988 RVA: 0x0057E000 File Offset: 0x0057C200
		public bool IsReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x0057E003 File Offset: 0x0057C203
		public NPCHeadRenderer(Asset<Texture2D>[] matchingArray)
		{
			this._matchingArray = matchingArray;
			this.Reset();
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x0057E018 File Offset: 0x0057C218
		public void Reset()
		{
			this._contents = new NPCHeadDrawRenderTargetContent[this._matchingArray.Length];
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x0057E030 File Offset: 0x0057C230
		public void DrawWithOutlines(Entity entity, int headId, Vector2 position, Color color, float rotation, float scale, SpriteEffects effects)
		{
			if (this._contents[headId] == null)
			{
				this._contents[headId] = new NPCHeadDrawRenderTargetContent();
				this._contents[headId].SetTexture(this._matchingArray[headId].Value);
			}
			NPCHeadDrawRenderTargetContent nPCHeadDrawRenderTargetContent = this._contents[headId];
			if (nPCHeadDrawRenderTargetContent.IsReady)
			{
				RenderTarget2D target = nPCHeadDrawRenderTargetContent.GetTarget();
				Main.spriteBatch.Draw(target, position, null, color, rotation, target.Size() / 2f, scale, effects, 0f);
				return;
			}
			nPCHeadDrawRenderTargetContent.Request();
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x0057E0C0 File Offset: 0x0057C2C0
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

		// Token: 0x04005072 RID: 20594
		private NPCHeadDrawRenderTargetContent[] _contents;

		// Token: 0x04005073 RID: 20595
		private Asset<Texture2D>[] _matchingArray;
	}
}
