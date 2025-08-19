using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000125 RID: 293
	public abstract class ABasicParticle : IPooledParticle, IParticle
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x004D272C File Offset: 0x004D092C
		// (set) Token: 0x0600175F RID: 5983 RVA: 0x004D2734 File Offset: 0x004D0934
		public bool ShouldBeRemovedFromRenderer { get; protected set; }

		// Token: 0x06001760 RID: 5984 RVA: 0x004D2740 File Offset: 0x004D0940
		public ABasicParticle()
		{
			this._texture = null;
			this._frame = Rectangle.Empty;
			this._origin = Vector2.Zero;
			this.Velocity = Vector2.Zero;
			this.LocalPosition = Vector2.Zero;
			this.ShouldBeRemovedFromRenderer = false;
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x004D2790 File Offset: 0x004D0990
		public virtual void SetBasicInfo(Asset<Texture2D> textureAsset, Rectangle? frame, Vector2 initialVelocity, Vector2 initialLocalPosition)
		{
			this._texture = textureAsset;
			this._frame = ((frame != null) ? frame.Value : this._texture.Frame(1, 1, 0, 0, 0, 0));
			this._origin = this._frame.Size() / 2f;
			this.Velocity = initialVelocity;
			this.LocalPosition = initialLocalPosition;
			this.ShouldBeRemovedFromRenderer = false;
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x004D2800 File Offset: 0x004D0A00
		public virtual void Update(ref ParticleRendererSettings settings)
		{
			this.Velocity += this.AccelerationPerFrame;
			this.LocalPosition += this.Velocity;
			this.RotationVelocity += this.RotationAcceleration;
			this.Rotation += this.RotationVelocity;
			this.ScaleVelocity += this.ScaleAcceleration;
			this.Scale += this.ScaleVelocity;
		}

		// Token: 0x06001763 RID: 5987
		public abstract void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch);

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x004D288F File Offset: 0x004D0A8F
		// (set) Token: 0x06001765 RID: 5989 RVA: 0x004D2897 File Offset: 0x004D0A97
		public bool IsRestingInPool { get; private set; }

		// Token: 0x06001766 RID: 5990 RVA: 0x004D28A0 File Offset: 0x004D0AA0
		public void RestInPool()
		{
			this.IsRestingInPool = true;
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x004D28AC File Offset: 0x004D0AAC
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

		// Token: 0x04001411 RID: 5137
		public Vector2 AccelerationPerFrame;

		// Token: 0x04001412 RID: 5138
		public Vector2 Velocity;

		// Token: 0x04001413 RID: 5139
		public Vector2 LocalPosition;

		// Token: 0x04001414 RID: 5140
		protected Asset<Texture2D> _texture;

		// Token: 0x04001415 RID: 5141
		protected Rectangle _frame;

		// Token: 0x04001416 RID: 5142
		protected Vector2 _origin;

		// Token: 0x04001417 RID: 5143
		public float Rotation;

		// Token: 0x04001418 RID: 5144
		public float RotationVelocity;

		// Token: 0x04001419 RID: 5145
		public float RotationAcceleration;

		// Token: 0x0400141A RID: 5146
		public Vector2 Scale;

		// Token: 0x0400141B RID: 5147
		public Vector2 ScaleVelocity;

		// Token: 0x0400141C RID: 5148
		public Vector2 ScaleAcceleration;
	}
}
