using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
	// Token: 0x02000223 RID: 547
	public class BlizzardShaderData : ScreenShaderData
	{
		// Token: 0x06001EC3 RID: 7875 RVA: 0x0050CB9F File Offset: 0x0050AD9F
		public BlizzardShaderData(string passName) : base(passName)
		{
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x0050CBC0 File Offset: 0x0050ADC0
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
			Vector2 vector = new Vector2(-this.windSpeed, -1f) * new Vector2(10f, 2f);
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

		// Token: 0x06001EC5 RID: 7877 RVA: 0x0050CCD6 File Offset: 0x0050AED6
		public override void Apply()
		{
			base.UseTargetPosition(this._texturePosition);
			base.Apply();
		}

		// Token: 0x040045C2 RID: 17858
		private Vector2 _texturePosition = Vector2.Zero;

		// Token: 0x040045C3 RID: 17859
		private float windSpeed = 0.1f;
	}
}
