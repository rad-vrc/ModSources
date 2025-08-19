using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Events;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000324 RID: 804
	public class SandstormSky : CustomSky
	{
		// Token: 0x06002476 RID: 9334 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void OnLoad()
		{
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x0055BB40 File Offset: 0x00559D40
		public override void Update(GameTime gameTime)
		{
			if (Main.gamePaused || !Main.hasFocus)
			{
				return;
			}
			if (this._isLeaving)
			{
				this._opacity -= (float)gameTime.ElapsedGameTime.TotalSeconds;
				if (this._opacity < 0f)
				{
					this._isActive = false;
					this._opacity = 0f;
					return;
				}
			}
			else
			{
				this._opacity += (float)gameTime.ElapsedGameTime.TotalSeconds;
				if (this._opacity > 1f)
				{
					this._opacity = 1f;
				}
			}
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x0055BBD4 File Offset: 0x00559DD4
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (minDepth < 1f || maxDepth == 3.4028235E+38f)
			{
				float scale = Math.Min(1f, Sandstorm.Severity * 1.5f);
				Color color = new Color(new Vector4(0.85f, 0.66f, 0.33f, 1f) * 0.8f * Main.ColorOfTheSkies.ToVector4()) * this._opacity * scale;
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color);
			}
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x0055BC75 File Offset: 0x00559E75
		public override void Activate(Vector2 position, params object[] args)
		{
			this._isActive = true;
			this._isLeaving = false;
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x0055BC85 File Offset: 0x00559E85
		public override void Deactivate(params object[] args)
		{
			this._isLeaving = true;
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x0055BC8E File Offset: 0x00559E8E
		public override void Reset()
		{
			this._opacity = 0f;
			this._isActive = false;
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x0055BCA2 File Offset: 0x00559EA2
		public override bool IsActive()
		{
			return this._isActive;
		}

		// Token: 0x040048A1 RID: 18593
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040048A2 RID: 18594
		private bool _isActive;

		// Token: 0x040048A3 RID: 18595
		private bool _isLeaving;

		// Token: 0x040048A4 RID: 18596
		private float _opacity;
	}
}
