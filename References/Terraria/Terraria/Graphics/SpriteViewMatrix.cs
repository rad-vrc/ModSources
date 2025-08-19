using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics
{
	// Token: 0x020000F5 RID: 245
	public class SpriteViewMatrix
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x004C47D8 File Offset: 0x004C29D8
		// (set) Token: 0x060015E2 RID: 5602 RVA: 0x004C47E0 File Offset: 0x004C29E0
		public Vector2 Zoom
		{
			get
			{
				return this._zoom;
			}
			set
			{
				if (this._zoom != value)
				{
					this._zoom = value;
					this._needsRebuild = true;
				}
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x004C47FE File Offset: 0x004C29FE
		public Vector2 Translation
		{
			get
			{
				if (this.ShouldRebuild())
				{
					this.Rebuild();
				}
				return this._translation;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x004C4814 File Offset: 0x004C2A14
		public Matrix ZoomMatrix
		{
			get
			{
				if (this.ShouldRebuild())
				{
					this.Rebuild();
				}
				return this._zoomMatrix;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x004C482A File Offset: 0x004C2A2A
		public Matrix TransformationMatrix
		{
			get
			{
				if (this.ShouldRebuild())
				{
					this.Rebuild();
				}
				return this._transformationMatrix;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x004C4840 File Offset: 0x004C2A40
		public Matrix NormalizedTransformationmatrix
		{
			get
			{
				if (this.ShouldRebuild())
				{
					this.Rebuild();
				}
				return this._normalizedTransformationMatrix;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x004C4856 File Offset: 0x004C2A56
		// (set) Token: 0x060015E8 RID: 5608 RVA: 0x004C485E File Offset: 0x004C2A5E
		public SpriteEffects Effects
		{
			get
			{
				return this._effects;
			}
			set
			{
				if (this._effects != value)
				{
					this._effects = value;
					this._needsRebuild = true;
				}
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x004C4877 File Offset: 0x004C2A77
		public Matrix EffectMatrix
		{
			get
			{
				if (this.ShouldRebuild())
				{
					this.Rebuild();
				}
				return this._effectMatrix;
			}
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x004C4890 File Offset: 0x004C2A90
		public SpriteViewMatrix(GraphicsDevice graphicsDevice)
		{
			this._graphicsDevice = graphicsDevice;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x004C48E8 File Offset: 0x004C2AE8
		private void Rebuild()
		{
			if (!this._overrideSystemViewport)
			{
				this._viewport = this._graphicsDevice.Viewport;
			}
			Vector2 vector = new Vector2((float)this._viewport.Width, (float)this._viewport.Height);
			Matrix matrix = Matrix.Identity;
			if (this._effects.HasFlag(SpriteEffects.FlipHorizontally))
			{
				matrix *= Matrix.CreateScale(-1f, 1f, 1f) * Matrix.CreateTranslation(vector.X, 0f, 0f);
			}
			if (this._effects.HasFlag(SpriteEffects.FlipVertically))
			{
				matrix *= Matrix.CreateScale(1f, -1f, 1f) * Matrix.CreateTranslation(0f, vector.Y, 0f);
			}
			Vector2 value = vector * 0.5f;
			Vector2 vector2 = value - value / this._zoom;
			Matrix matrix2 = Matrix.CreateOrthographicOffCenter(0f, vector.X, vector.Y, 0f, 0f, 1f);
			this._translation = vector2;
			this._zoomMatrix = Matrix.CreateTranslation(-vector2.X, -vector2.Y, 0f) * Matrix.CreateScale(this._zoom.X, this._zoom.Y, 1f);
			this._effectMatrix = matrix;
			this._transformationMatrix = matrix * this._zoomMatrix;
			this._normalizedTransformationMatrix = Matrix.Invert(matrix) * this._zoomMatrix * matrix2;
			this._needsRebuild = false;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x004C4A97 File Offset: 0x004C2C97
		public void SetViewportOverride(Viewport viewport)
		{
			this._viewport = viewport;
			this._overrideSystemViewport = true;
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x004C4AA7 File Offset: 0x004C2CA7
		public void ClearViewportOverride()
		{
			this._overrideSystemViewport = false;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x004C4AB0 File Offset: 0x004C2CB0
		private bool ShouldRebuild()
		{
			return this._needsRebuild || (!this._overrideSystemViewport && (this._graphicsDevice.Viewport.Width != this._viewport.Width || this._graphicsDevice.Viewport.Height != this._viewport.Height));
		}

		// Token: 0x040012EC RID: 4844
		private Vector2 _zoom = Vector2.One;

		// Token: 0x040012ED RID: 4845
		private Vector2 _translation = Vector2.Zero;

		// Token: 0x040012EE RID: 4846
		private Matrix _zoomMatrix = Matrix.Identity;

		// Token: 0x040012EF RID: 4847
		private Matrix _transformationMatrix = Matrix.Identity;

		// Token: 0x040012F0 RID: 4848
		private Matrix _normalizedTransformationMatrix = Matrix.Identity;

		// Token: 0x040012F1 RID: 4849
		private SpriteEffects _effects;

		// Token: 0x040012F2 RID: 4850
		private Matrix _effectMatrix;

		// Token: 0x040012F3 RID: 4851
		private GraphicsDevice _graphicsDevice;

		// Token: 0x040012F4 RID: 4852
		private Viewport _viewport;

		// Token: 0x040012F5 RID: 4853
		private bool _overrideSystemViewport;

		// Token: 0x040012F6 RID: 4854
		private bool _needsRebuild = true;
	}
}
