using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
	// Token: 0x02000572 RID: 1394
	public class BlizzardShaderData : ScreenShaderData
	{
		// Token: 0x060041CD RID: 16845 RVA: 0x005F0D87 File Offset: 0x005EEF87
		public BlizzardShaderData(string passName) : base(passName)
		{
		}

		// Token: 0x060041CE RID: 16846 RVA: 0x005F0DA8 File Offset: 0x005EEFA8
		public override void Update(GameTime gameTime)
		{
			float num = Main.windSpeedCurrent;
			if (num >= 0f && num <= 0.1f)
			{
				num = 0.1f;
			}
			else if (num <= 0f && num >= -0.1f)
			{
				num = -0.1f;
			}
			this.windSpeed = num * 0.05f + this.windSpeed * 0.95f;
			Vector2 vector = new Vector2(0f - this.windSpeed, -1f) * new Vector2(10f, 2f);
			vector.Normalize();
			vector *= new Vector2(0.8f, 0.6f);
			if (!Main.gamePaused && Main.hasFocus)
			{
				this._texturePosition += vector * (float)gameTime.ElapsedGameTime.TotalSeconds;
			}
			this._texturePosition.X = this._texturePosition.X % 10f;
			this._texturePosition.Y = this._texturePosition.Y % 10f;
			base.UseDirection(vector);
			base.UseTargetPosition(this._texturePosition);
			base.Update(gameTime);
		}

		// Token: 0x060041CF RID: 16847 RVA: 0x005F0EC3 File Offset: 0x005EF0C3
		public override void Apply()
		{
			base.UseTargetPosition(this._texturePosition);
			base.Apply();
		}

		// Token: 0x04005900 RID: 22784
		private Vector2 _texturePosition = Vector2.Zero;

		// Token: 0x04005901 RID: 22785
		private float windSpeed = 0.1f;
	}
}
