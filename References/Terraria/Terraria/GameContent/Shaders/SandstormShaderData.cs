using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
	// Token: 0x02000222 RID: 546
	public class SandstormShaderData : ScreenShaderData
	{
		// Token: 0x06001EC0 RID: 7872 RVA: 0x0050CABB File Offset: 0x0050ACBB
		public SandstormShaderData(string passName) : base(passName)
		{
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x0050CAD0 File Offset: 0x0050ACD0
		public override void Update(GameTime gameTime)
		{
			Vector2 vector = new Vector2(-Main.windSpeedCurrent, -1f) * new Vector2(20f, 0.1f);
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

		// Token: 0x06001EC2 RID: 7874 RVA: 0x0050CB8A File Offset: 0x0050AD8A
		public override void Apply()
		{
			base.UseTargetPosition(this._texturePosition);
			base.Apply();
		}

		// Token: 0x040045C1 RID: 17857
		private Vector2 _texturePosition = Vector2.Zero;
	}
}
