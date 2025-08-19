using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200011F RID: 287
	public class ParticleRenderer
	{
		// Token: 0x06001739 RID: 5945 RVA: 0x004D1D74 File Offset: 0x004CFF74
		public ParticleRenderer()
		{
			this.Settings = default(ParticleRendererSettings);
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x004D1DA1 File Offset: 0x004CFFA1
		public void Add(IParticle particle)
		{
			this.Particles.Add(particle);
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x004D1DAF File Offset: 0x004CFFAF
		public void Clear()
		{
			this.Particles.Clear();
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x004D1DBC File Offset: 0x004CFFBC
		public void Update()
		{
			for (int i = 0; i < this.Particles.Count; i++)
			{
				if (this.Particles[i].ShouldBeRemovedFromRenderer)
				{
					IPooledParticle pooledParticle = this.Particles[i] as IPooledParticle;
					if (pooledParticle != null)
					{
						pooledParticle.RestInPool();
					}
					this.Particles.RemoveAt(i);
					i--;
				}
				else
				{
					this.Particles[i].Update(ref this.Settings);
				}
			}
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x004D1E38 File Offset: 0x004D0038
		public void Draw(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < this.Particles.Count; i++)
			{
				if (!this.Particles[i].ShouldBeRemovedFromRenderer)
				{
					this.Particles[i].Draw(ref this.Settings, spriteBatch);
				}
			}
		}

		// Token: 0x040013F1 RID: 5105
		public ParticleRendererSettings Settings;

		// Token: 0x040013F2 RID: 5106
		public List<IParticle> Particles = new List<IParticle>();
	}
}
