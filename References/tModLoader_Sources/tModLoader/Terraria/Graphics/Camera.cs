using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics
{
	// Token: 0x02000436 RID: 1078
	public class Camera
	{
		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06003595 RID: 13717 RVA: 0x00577A17 File Offset: 0x00575C17
		public Vector2 UnscaledPosition
		{
			get
			{
				return Main.screenPosition;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06003596 RID: 13718 RVA: 0x00577A1E File Offset: 0x00575C1E
		public Vector2 UnscaledSize
		{
			get
			{
				return new Vector2((float)Main.screenWidth, (float)Main.screenHeight);
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06003597 RID: 13719 RVA: 0x00577A31 File Offset: 0x00575C31
		public Vector2 ScaledPosition
		{
			get
			{
				return this.UnscaledPosition + this.GameViewMatrix.Translation;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06003598 RID: 13720 RVA: 0x00577A49 File Offset: 0x00575C49
		public Vector2 ScaledSize
		{
			get
			{
				return this.UnscaledSize - this.GameViewMatrix.Translation * 2f;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06003599 RID: 13721 RVA: 0x00577A6B File Offset: 0x00575C6B
		public RasterizerState Rasterizer
		{
			get
			{
				return Main.Rasterizer;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x0600359A RID: 13722 RVA: 0x00577A72 File Offset: 0x00575C72
		public SamplerState Sampler
		{
			get
			{
				return Main.DefaultSamplerState;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x0600359B RID: 13723 RVA: 0x00577A79 File Offset: 0x00575C79
		public SpriteViewMatrix GameViewMatrix
		{
			get
			{
				return Main.GameViewMatrix;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x0600359C RID: 13724 RVA: 0x00577A80 File Offset: 0x00575C80
		public SpriteBatch SpriteBatch
		{
			get
			{
				return Main.spriteBatch;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x0600359D RID: 13725 RVA: 0x00577A87 File Offset: 0x00575C87
		public Vector2 Center
		{
			get
			{
				return this.UnscaledPosition + this.UnscaledSize * 0.5f;
			}
		}
	}
}
