using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200011D RID: 285
	public interface IParticle
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06001736 RID: 5942
		bool ShouldBeRemovedFromRenderer { get; }

		// Token: 0x06001737 RID: 5943
		void Update(ref ParticleRendererSettings settings);

		// Token: 0x06001738 RID: 5944
		void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch);
	}
}
