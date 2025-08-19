using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Effects
{
	// Token: 0x02000111 RID: 273
	public abstract class Overlay : GameEffect
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x004C9E8B File Offset: 0x004C808B
		public RenderLayers Layer
		{
			get
			{
				return this._layer;
			}
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x004C9E93 File Offset: 0x004C8093
		public Overlay(EffectPriority priority, RenderLayers layer)
		{
			this._priority = priority;
			this._layer = layer;
		}

		// Token: 0x060016D8 RID: 5848
		public abstract void Draw(SpriteBatch spriteBatch);

		// Token: 0x060016D9 RID: 5849
		public abstract void Update(GameTime gameTime);

		// Token: 0x04001397 RID: 5015
		public OverlayMode Mode = OverlayMode.Inactive;

		// Token: 0x04001398 RID: 5016
		private RenderLayers _layer = RenderLayers.All;
	}
}
