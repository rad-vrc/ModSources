using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
	// Token: 0x020000F0 RID: 240
	public struct FlameLashDrawer
	{
		// Token: 0x060015C5 RID: 5573 RVA: 0x004C3E08 File Offset: 0x004C2008
		public void Draw(Projectile proj)
		{
			this.transitToDark = Utils.GetLerpValue(0f, 6f, proj.localAI[0], true);
			MiscShaderData miscShaderData = GameShaders.Misc["FlameLash"];
			miscShaderData.UseSaturation(-2f);
			miscShaderData.UseOpacity(MathHelper.Lerp(4f, 8f, this.transitToDark));
			miscShaderData.Apply(null);
			FlameLashDrawer._vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, new VertexStrip.StripColorFunction(this.StripColors), new VertexStrip.StripHalfWidthFunction(this.StripWidth), -Main.screenPosition + proj.Size / 2f, false, true);
			FlameLashDrawer._vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x004C3F00 File Offset: 0x004C2100
		private Color StripColors(float progressOnStrip)
		{
			float lerpValue = Utils.GetLerpValue(0f - 0.1f * this.transitToDark, 0.7f - 0.2f * this.transitToDark, progressOnStrip, true);
			Color result = Color.Lerp(Color.Lerp(Color.White, Color.Orange, this.transitToDark * 0.5f), Color.Red, lerpValue) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, false));
			result.A /= 8;
			return result;
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x004C3F90 File Offset: 0x004C2190
		private float StripWidth(float progressOnStrip)
		{
			float num = Utils.GetLerpValue(0f, 0.06f + this.transitToDark * 0.01f, progressOnStrip, true);
			num = 1f - (1f - num) * (1f - num);
			return MathHelper.Lerp(24f + this.transitToDark * 16f, 8f, Utils.GetLerpValue(0f, 1f, progressOnStrip, true)) * num;
		}

		// Token: 0x040012E2 RID: 4834
		private static VertexStrip _vertexStrip = new VertexStrip();

		// Token: 0x040012E3 RID: 4835
		private float transitToDark;
	}
}
