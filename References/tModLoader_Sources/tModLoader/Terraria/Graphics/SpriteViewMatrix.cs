using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics
{
	// Token: 0x0200043E RID: 1086
	public class SpriteViewMatrix
	{
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060035BD RID: 13757 RVA: 0x00578932 File Offset: 0x00576B32
		// (set) Token: 0x060035BE RID: 13758 RVA: 0x0057893A File Offset: 0x00576B3A
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

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060035BF RID: 13759 RVA: 0x00578958 File Offset: 0x00576B58
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

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060035C0 RID: 13760 RVA: 0x0057896E File Offset: 0x00576B6E
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

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060035C1 RID: 13761 RVA: 0x00578984 File Offset: 0x00576B84
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

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060035C2 RID: 13762 RVA: 0x0057899A File Offset: 0x00576B9A
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

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060035C3 RID: 13763 RVA: 0x005789B0 File Offset: 0x00576BB0
		// (set) Token: 0x060035C4 RID: 13764 RVA: 0x005789B8 File Offset: 0x00576BB8
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

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060035C5 RID: 13765 RVA: 0x005789D1 File Offset: 0x00576BD1
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

		// Token: 0x060035C6 RID: 13766 RVA: 0x005789E8 File Offset: 0x00576BE8
		public SpriteViewMatrix(GraphicsDevice graphicsDevice)
		{
			this._graphicsDevice = graphicsDevice;
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x00578A40 File Offset: 0x00576C40
		private void Rebuild()
		{
			if (!this._overrideSystemViewport)
			{
				this._viewport = this._graphicsDevice.Viewport;
			}
			Vector2 vector;
			vector..ctor((float)this._viewport.Width, (float)this._viewport.Height);
			Matrix identity = Matrix.Identity;
			if (this._effects.HasFlag(1))
			{
				identity *= Matrix.CreateScale(-1f, 1f, 1f) * Matrix.CreateTranslation(vector.X, 0f, 0f);
			}
			if (this._effects.HasFlag(2))
			{
				identity *= Matrix.CreateScale(1f, -1f, 1f) * Matrix.CreateTranslation(0f, vector.Y, 0f);
			}
			Vector2 vector2 = vector * 0.5f;
			Vector2 translation = vector2 - vector2 / this._zoom;
			Matrix matrix = Matrix.CreateOrthographicOffCenter(0f, vector.X, vector.Y, 0f, 0f, 1f);
			this._translation = translation;
			this._zoomMatrix = Matrix.CreateTranslation(0f - translation.X, 0f - translation.Y, 0f) * Matrix.CreateScale(this._zoom.X, this._zoom.Y, 1f);
			this._effectMatrix = identity;
			this._transformationMatrix = identity * this._zoomMatrix;
			float num = 0.00390625f;
			Matrix perspectiveFix = Matrix.CreateTranslation(num, num, 0f);
			this._transformationMatrix = perspectiveFix * this._transformationMatrix;
			this._normalizedTransformationMatrix = Matrix.Invert(identity) * this._zoomMatrix * matrix;
			this._needsRebuild = false;
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x00578C1E File Offset: 0x00576E1E
		public void SetViewportOverride(Viewport viewport)
		{
			this._viewport = viewport;
			this._overrideSystemViewport = true;
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x00578C2E File Offset: 0x00576E2E
		public void ClearViewportOverride()
		{
			this._overrideSystemViewport = false;
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x00578C38 File Offset: 0x00576E38
		private bool ShouldRebuild()
		{
			return this._needsRebuild || (!this._overrideSystemViewport && (this._graphicsDevice.Viewport.Width != this._viewport.Width || this._graphicsDevice.Viewport.Height != this._viewport.Height));
		}

		// Token: 0x04004FD7 RID: 20439
		private Vector2 _zoom = Vector2.One;

		// Token: 0x04004FD8 RID: 20440
		private Vector2 _translation = Vector2.Zero;

		// Token: 0x04004FD9 RID: 20441
		private Matrix _zoomMatrix = Matrix.Identity;

		// Token: 0x04004FDA RID: 20442
		private Matrix _transformationMatrix = Matrix.Identity;

		// Token: 0x04004FDB RID: 20443
		private Matrix _normalizedTransformationMatrix = Matrix.Identity;

		// Token: 0x04004FDC RID: 20444
		private SpriteEffects _effects;

		// Token: 0x04004FDD RID: 20445
		private Matrix _effectMatrix;

		// Token: 0x04004FDE RID: 20446
		private GraphicsDevice _graphicsDevice;

		// Token: 0x04004FDF RID: 20447
		private Viewport _viewport;

		// Token: 0x04004FE0 RID: 20448
		private bool _overrideSystemViewport;

		// Token: 0x04004FE1 RID: 20449
		private bool _needsRebuild = true;
	}
}
