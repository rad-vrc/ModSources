using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
	// Token: 0x02000576 RID: 1398
	public class SandstormShaderData : ScreenShaderData
	{
		// Token: 0x060041D5 RID: 16853 RVA: 0x005F108D File Offset: 0x005EF28D
		public SandstormShaderData(string passName) : base(passName)
		{
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x005F10A4 File Offset: 0x005EF2A4
		public override void Update(GameTime gameTime)
		{
			Vector2 vector = new Vector2(0f - Main.windSpeedCurrent, -1f) * new Vector2(20f, 0.1f);
			vector.Normalize();
			vector *= new Vector2(2f, 0.2f);
			if (!Main.gamePaused && Main.hasFocus)
			{
				this._texturePosition += vector * (float)gameTime.ElapsedGameTime.TotalSeconds;
			}
			this._texturePosition.X = this._texturePosition.X % 10f;
			this._texturePosition.Y = this._texturePosition.Y % 10f;
			base.UseDirection(vector);
			base.Update(gameTime);
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x005F1163 File Offset: 0x005EF363
		public override void Apply()
		{
			base.UseTargetPosition(this._texturePosition);
			base.Apply();
		}

		// Token: 0x04005908 RID: 22792
		private Vector2 _texturePosition = Vector2.Zero;
	}
}
