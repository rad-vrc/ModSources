using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Events;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x0200056C RID: 1388
	public class SandstormSky : CustomSky
	{
		// Token: 0x06004184 RID: 16772 RVA: 0x005E7A24 File Offset: 0x005E5C24
		public override void OnLoad()
		{
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x005E7A28 File Offset: 0x005E5C28
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

		// Token: 0x06004186 RID: 16774 RVA: 0x005E7ABC File Offset: 0x005E5CBC
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (minDepth < 1f || maxDepth == 3.4028235E+38f)
			{
				float num = Math.Min(1f, Sandstorm.Severity * 1.5f);
				Color color = new Color(new Vector4(0.85f, 0.66f, 0.33f, 1f) * 0.8f * Main.ColorOfTheSkies.ToVector4()) * this._opacity * num;
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color);
			}
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x005E7B5D File Offset: 0x005E5D5D
		public override void Activate(Vector2 position, params object[] args)
		{
			this._isActive = true;
			this._isLeaving = false;
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x005E7B6D File Offset: 0x005E5D6D
		public override void Deactivate(params object[] args)
		{
			this._isLeaving = true;
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x005E7B76 File Offset: 0x005E5D76
		public override void Reset()
		{
			this._opacity = 0f;
			this._isActive = false;
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x005E7B8A File Offset: 0x005E5D8A
		public override bool IsActive()
		{
			return this._isActive;
		}

		// Token: 0x040058D9 RID: 22745
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040058DA RID: 22746
		private bool _isActive;

		// Token: 0x040058DB RID: 22747
		private bool _isLeaving;

		// Token: 0x040058DC RID: 22748
		private float _opacity;
	}
}
