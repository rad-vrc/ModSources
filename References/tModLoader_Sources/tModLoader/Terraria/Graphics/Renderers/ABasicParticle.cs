using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200044C RID: 1100
	public abstract class ABasicParticle : IPooledParticle, IParticle
	{
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06003655 RID: 13909 RVA: 0x0057B8C8 File Offset: 0x00579AC8
		// (set) Token: 0x06003656 RID: 13910 RVA: 0x0057B8D0 File Offset: 0x00579AD0
		public bool ShouldBeRemovedFromRenderer { get; protected set; }

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06003657 RID: 13911 RVA: 0x0057B8D9 File Offset: 0x00579AD9
		// (set) Token: 0x06003658 RID: 13912 RVA: 0x0057B8E1 File Offset: 0x00579AE1
		public bool IsRestingInPool { get; private set; }

		// Token: 0x06003659 RID: 13913 RVA: 0x0057B8EC File Offset: 0x00579AEC
		public ABasicParticle()
		{
			this._texture = null;
			this._frame = Rectangle.Empty;
			this._origin = Vector2.Zero;
			this.Velocity = Vector2.Zero;
			this.LocalPosition = Vector2.Zero;
			this.ShouldBeRemovedFromRenderer = false;
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x0057B93C File Offset: 0x00579B3C
		public virtual void SetBasicInfo(Asset<Texture2D> textureAsset, Rectangle? frame, Vector2 initialVelocity, Vector2 initialLocalPosition)
		{
			this._texture = textureAsset;
			this._frame = ((frame != null) ? frame.Value : this._texture.Frame(1, 1, 0, 0, 0, 0));
			this._origin = this._frame.Size() / 2f;
			this.Velocity = initialVelocity;
			this.LocalPosition = initialLocalPosition;
			this.ShouldBeRemovedFromRenderer = false;
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x0057B9AC File Offset: 0x00579BAC
		public virtual void Update(ref ParticleRendererSettings settings)
		{
			this.Velocity += this.AccelerationPerFrame;
			this.LocalPosition += this.Velocity;
			this.RotationVelocity += this.RotationAcceleration;
			this.Rotation += this.RotationVelocity;
			this.ScaleVelocity += this.ScaleAcceleration;
			this.Scale += this.ScaleVelocity;
		}

		// Token: 0x0600365C RID: 13916
		public abstract void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch);

		// Token: 0x0600365D RID: 13917 RVA: 0x0057BA3B File Offset: 0x00579C3B
		public void RestInPool()
		{
			this.IsRestingInPool = true;
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x0057BA44 File Offset: 0x00579C44
		public virtual void FetchFromPool()
		{
			this.IsRestingInPool = false;
			this.ShouldBeRemovedFromRenderer = false;
			this.AccelerationPerFrame = Vector2.Zero;
			this.Velocity = Vector2.Zero;
			this.LocalPosition = Vector2.Zero;
			this._texture = null;
			this._frame = Rectangle.Empty;
			this._origin = Vector2.Zero;
			this.Rotation = 0f;
			this.RotationVelocity = 0f;
			this.RotationAcceleration = 0f;
			this.Scale = Vector2.Zero;
			this.ScaleVelocity = Vector2.Zero;
			this.ScaleAcceleration = Vector2.Zero;
		}

		// Token: 0x0400502B RID: 20523
		public Vector2 AccelerationPerFrame;

		// Token: 0x0400502C RID: 20524
		public Vector2 Velocity;

		// Token: 0x0400502D RID: 20525
		public Vector2 LocalPosition;

		// Token: 0x0400502E RID: 20526
		protected Asset<Texture2D> _texture;

		// Token: 0x0400502F RID: 20527
		protected Rectangle _frame;

		// Token: 0x04005030 RID: 20528
		protected Vector2 _origin;

		// Token: 0x04005031 RID: 20529
		public float Rotation;

		// Token: 0x04005032 RID: 20530
		public float RotationVelocity;

		// Token: 0x04005033 RID: 20531
		public float RotationAcceleration;

		// Token: 0x04005034 RID: 20532
		public Vector2 Scale;

		// Token: 0x04005035 RID: 20533
		public Vector2 ScaleVelocity;

		// Token: 0x04005036 RID: 20534
		public Vector2 ScaleAcceleration;
	}
}
