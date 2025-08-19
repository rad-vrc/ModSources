using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200045A RID: 1114
	public class ParticleRenderer
	{
		// Token: 0x060036AC RID: 13996 RVA: 0x0057E1EE File Offset: 0x0057C3EE
		public ParticleRenderer()
		{
			this.Settings = default(ParticleRendererSettings);
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x0057E20D File Offset: 0x0057C40D
		public void Add(IParticle particle)
		{
			this.Particles.Add(particle);
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x0057E21B File Offset: 0x0057C41B
		public void Clear()
		{
			this.Particles.Clear();
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x0057E228 File Offset: 0x0057C428
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

		// Token: 0x060036B0 RID: 14000 RVA: 0x0057E2A4 File Offset: 0x0057C4A4
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

		// Token: 0x04005076 RID: 20598
		public ParticleRendererSettings Settings;

		// Token: 0x04005077 RID: 20599
		public List<IParticle> Particles = new List<IParticle>();
	}
}
