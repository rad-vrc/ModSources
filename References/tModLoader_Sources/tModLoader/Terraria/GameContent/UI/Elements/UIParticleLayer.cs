using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Renderers;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000528 RID: 1320
	public class UIParticleLayer : UIElement
	{
		// Token: 0x06003F0D RID: 16141 RVA: 0x005D79F2 File Offset: 0x005D5BF2
		public UIParticleLayer()
		{
			this.IgnoresMouseInteraction = true;
			this.ParticleSystem = new ParticleRenderer();
			base.OnUpdate += this.ParticleSystemUpdate;
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x005D7A1E File Offset: 0x005D5C1E
		private void ParticleSystemUpdate(UIElement affectedElement)
		{
			this.ParticleSystem.Update();
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x005D7A2C File Offset: 0x005D5C2C
		public override void Recalculate()
		{
			base.Recalculate();
			Rectangle r = base.GetDimensions().ToRectangle();
			this.ParticleSystem.Settings.AnchorPosition = r.TopLeft() + this.AnchorPositionOffsetByPercents * r.Size() + this.AnchorPositionOffsetByPixels;
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x005D7A85 File Offset: 0x005D5C85
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			this.ParticleSystem.Draw(spriteBatch);
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x005D7A93 File Offset: 0x005D5C93
		public void AddParticle(IParticle particle)
		{
			this.ParticleSystem.Add(particle);
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x005D7AA1 File Offset: 0x005D5CA1
		public void ClearParticles()
		{
			this.ParticleSystem.Clear();
		}

		// Token: 0x0400578A RID: 22410
		public ParticleRenderer ParticleSystem;

		// Token: 0x0400578B RID: 22411
		public Vector2 AnchorPositionOffsetByPercents;

		// Token: 0x0400578C RID: 22412
		public Vector2 AnchorPositionOffsetByPixels;
	}
}
