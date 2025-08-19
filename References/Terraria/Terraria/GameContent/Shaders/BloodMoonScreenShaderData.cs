using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
	// Token: 0x02000226 RID: 550
	public class BloodMoonScreenShaderData : ScreenShaderData
	{
		// Token: 0x06001ED4 RID: 7892 RVA: 0x0050C7B8 File Offset: 0x0050A9B8
		public BloodMoonScreenShaderData(string passName) : base(passName)
		{
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x0050E09C File Offset: 0x0050C29C
		public override void Update(GameTime gameTime)
		{
			float num = 1f - Utils.SmoothStep((float)Main.worldSurface + 50f, (float)Main.rockLayer + 100f, (Main.screenPosition.Y + (float)(Main.screenHeight / 2)) / 16f);
			if (Main.remixWorld)
			{
				num = Utils.SmoothStep((float)(Main.rockLayer + Main.worldSurface) / 2f, (float)Main.rockLayer, (Main.screenPosition.Y + (float)(Main.screenHeight / 2)) / 16f);
			}
			if (Main.shimmerAlpha > 0f)
			{
				num *= 1f - Main.shimmerAlpha;
			}
			base.UseOpacity(num * 0.75f);
		}
	}
}
