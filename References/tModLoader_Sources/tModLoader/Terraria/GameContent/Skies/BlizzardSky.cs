using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000565 RID: 1381
	public class BlizzardSky : CustomSky
	{
		// Token: 0x0600413E RID: 16702 RVA: 0x005E562E File Offset: 0x005E382E
		public override void OnLoad()
		{
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x005E5630 File Offset: 0x005E3830
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

		// Token: 0x06004140 RID: 16704 RVA: 0x005E56C4 File Offset: 0x005E38C4
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (minDepth < 1f || maxDepth == 3.4028235E+38f)
			{
				float num = Math.Min(1f, Main.cloudAlpha * 2f);
				Color color = new Color(new Vector4(1f) * Main.ColorOfTheSkies.ToVector4()) * this._opacity * 0.7f * num;
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color);
			}
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x005E5753 File Offset: 0x005E3953
		public override void Activate(Vector2 position, params object[] args)
		{
			this._isActive = true;
			this._isLeaving = false;
		}

		// Token: 0x06004142 RID: 16706 RVA: 0x005E5763 File Offset: 0x005E3963
		public override void Deactivate(params object[] args)
		{
			this._isLeaving = true;
		}

		// Token: 0x06004143 RID: 16707 RVA: 0x005E576C File Offset: 0x005E396C
		public override void Reset()
		{
			this._opacity = 0f;
			this._isActive = false;
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x005E5780 File Offset: 0x005E3980
		public override bool IsActive()
		{
			return this._isActive;
		}

		// Token: 0x040058AA RID: 22698
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040058AB RID: 22699
		private bool _isActive;

		// Token: 0x040058AC RID: 22700
		private bool _isLeaving;

		// Token: 0x040058AD RID: 22701
		private float _opacity;
	}
}
