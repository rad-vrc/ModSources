using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics
{
	// Token: 0x020000F4 RID: 244
	public class Camera
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x004C474B File Offset: 0x004C294B
		public Vector2 UnscaledPosition
		{
			get
			{
				return Main.screenPosition;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x004C4752 File Offset: 0x004C2952
		public Vector2 UnscaledSize
		{
			get
			{
				return new Vector2((float)Main.screenWidth, (float)Main.screenHeight);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x004C4765 File Offset: 0x004C2965
		public Vector2 ScaledPosition
		{
			get
			{
				return this.UnscaledPosition + this.GameViewMatrix.Translation;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x004C477D File Offset: 0x004C297D
		public Vector2 ScaledSize
		{
			get
			{
				return this.UnscaledSize - this.GameViewMatrix.Translation * 2f;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x004C479F File Offset: 0x004C299F
		public RasterizerState Rasterizer
		{
			get
			{
				return Main.Rasterizer;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x004C47A6 File Offset: 0x004C29A6
		public SamplerState Sampler
		{
			get
			{
				return Main.DefaultSamplerState;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x004C47AD File Offset: 0x004C29AD
		public SpriteViewMatrix GameViewMatrix
		{
			get
			{
				return Main.GameViewMatrix;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x004C47B4 File Offset: 0x004C29B4
		public SpriteBatch SpriteBatch
		{
			get
			{
				return Main.spriteBatch;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x004C47BB File Offset: 0x004C29BB
		public Vector2 Center
		{
			get
			{
				return this.UnscaledPosition + this.UnscaledSize * 0.5f;
			}
		}
	}
}
