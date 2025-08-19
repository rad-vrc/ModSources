using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
	// Token: 0x02000573 RID: 1395
	public class BloodMoonScreenShaderData : ScreenShaderData
	{
		// Token: 0x060041D0 RID: 16848 RVA: 0x005F0ED8 File Offset: 0x005EF0D8
		public BloodMoonScreenShaderData(string passName) : base(passName)
		{
		}

		// Token: 0x060041D1 RID: 16849 RVA: 0x005F0EE4 File Offset: 0x005EF0E4
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
