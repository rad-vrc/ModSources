using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000451 RID: 1105
	public interface IParticle
	{
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06003673 RID: 13939
		bool ShouldBeRemovedFromRenderer { get; }

		// Token: 0x06003674 RID: 13940
		void Update(ref ParticleRendererSettings settings);

		// Token: 0x06003675 RID: 13941
		void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch);
	}
}
