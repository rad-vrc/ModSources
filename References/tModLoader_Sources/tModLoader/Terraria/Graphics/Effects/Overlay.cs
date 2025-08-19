using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Effects
{
	// Token: 0x0200046F RID: 1135
	public abstract class Overlay : GameEffect
	{
		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06003742 RID: 14146 RVA: 0x00587061 File Offset: 0x00585261
		public RenderLayers Layer
		{
			get
			{
				return this._layer;
			}
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x00587069 File Offset: 0x00585269
		public Overlay(EffectPriority priority, RenderLayers layer)
		{
			this._priority = priority;
			this._layer = layer;
		}

		// Token: 0x06003744 RID: 14148
		public abstract void Draw(SpriteBatch spriteBatch);

		// Token: 0x06003745 RID: 14149
		public abstract void Update(GameTime gameTime);

		// Token: 0x040050F4 RID: 20724
		public OverlayMode Mode = OverlayMode.Inactive;

		// Token: 0x040050F5 RID: 20725
		private RenderLayers _layer = RenderLayers.All;
	}
}
