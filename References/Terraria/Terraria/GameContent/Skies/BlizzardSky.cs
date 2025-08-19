using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000322 RID: 802
	public class BlizzardSky : CustomSky
	{
		// Token: 0x06002463 RID: 9315 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void OnLoad()
		{
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x0055B204 File Offset: 0x00559404
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

		// Token: 0x06002465 RID: 9317 RVA: 0x0055B298 File Offset: 0x00559498
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (minDepth < 1f || maxDepth == 3.4028235E+38f)
			{
				float scale = Math.Min(1f, Main.cloudAlpha * 2f);
				Color color = new Color(new Vector4(1f) * Main.ColorOfTheSkies.ToVector4()) * this._opacity * 0.7f * scale;
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color);
			}
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x0055B327 File Offset: 0x00559527
		public override void Activate(Vector2 position, params object[] args)
		{
			this._isActive = true;
			this._isLeaving = false;
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x0055B337 File Offset: 0x00559537
		public override void Deactivate(params object[] args)
		{
			this._isLeaving = true;
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x0055B340 File Offset: 0x00559540
		public override void Reset()
		{
			this._opacity = 0f;
			this._isActive = false;
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x0055B354 File Offset: 0x00559554
		public override bool IsActive()
		{
			return this._isActive;
		}

		// Token: 0x04004895 RID: 18581
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x04004896 RID: 18582
		private bool _isActive;

		// Token: 0x04004897 RID: 18583
		private bool _isLeaving;

		// Token: 0x04004898 RID: 18584
		private float _opacity;
	}
}
