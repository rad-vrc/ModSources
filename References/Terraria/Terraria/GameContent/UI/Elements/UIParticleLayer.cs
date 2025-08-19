using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Renderers;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200035C RID: 860
	public class UIParticleLayer : UIElement
	{
		// Token: 0x060027AF RID: 10159 RVA: 0x005852DC File Offset: 0x005834DC
		public UIParticleLayer()
		{
			this.IgnoresMouseInteraction = true;
			this.ParticleSystem = new ParticleRenderer();
			base.OnUpdate += this.ParticleSystemUpdate;
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x00585308 File Offset: 0x00583508
		private void ParticleSystemUpdate(UIElement affectedElement)
		{
			this.ParticleSystem.Update();
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x00585318 File Offset: 0x00583518
		public override void Recalculate()
		{
			base.Recalculate();
			Rectangle r = base.GetDimensions().ToRectangle();
			this.ParticleSystem.Settings.AnchorPosition = r.TopLeft() + this.AnchorPositionOffsetByPercents * r.Size() + this.AnchorPositionOffsetByPixels;
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x00585371 File Offset: 0x00583571
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			this.ParticleSystem.Draw(spriteBatch);
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x0058537F File Offset: 0x0058357F
		public void AddParticle(IParticle particle)
		{
			this.ParticleSystem.Add(particle);
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x0058538D File Offset: 0x0058358D
		public void ClearParticles()
		{
			this.ParticleSystem.Clear();
		}

		// Token: 0x04004B0B RID: 19211
		public ParticleRenderer ParticleSystem;

		// Token: 0x04004B0C RID: 19212
		public Vector2 AnchorPositionOffsetByPercents;

		// Token: 0x04004B0D RID: 19213
		public Vector2 AnchorPositionOffsetByPixels;
	}
}
