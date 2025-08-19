using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
	// Token: 0x0200043C RID: 1084
	public struct RainbowRodDrawer
	{
		// Token: 0x060035B5 RID: 13749 RVA: 0x005785C0 File Offset: 0x005767C0
		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply(null);
			RainbowRodDrawer._vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, new VertexStrip.StripColorFunction(this.StripColors), new VertexStrip.StripHalfWidthFunction(this.StripWidth), -Main.screenPosition + proj.Size / 2f, false, true);
			RainbowRodDrawer._vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x0057868C File Offset: 0x0057688C
		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb((progressOnStrip * 1.6f - Main.GlobalTimeWrappedHourly) % 1f, 1f, 0.5f, byte.MaxValue);
			Color result = Color.Lerp(Color.White, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, false));
			result.A = 0;
			return result;
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x00578704 File Offset: 0x00576904
		private float StripWidth(float progressOnStrip)
		{
			float num = 1f;
			float lerpValue = Utils.GetLerpValue(0f, 0.2f, progressOnStrip, true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}

		// Token: 0x04004FD6 RID: 20438
		private static VertexStrip _vertexStrip = new VertexStrip();
	}
}
