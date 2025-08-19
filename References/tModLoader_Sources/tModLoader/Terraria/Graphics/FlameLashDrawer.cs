using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
	// Token: 0x02000439 RID: 1081
	public struct FlameLashDrawer
	{
		// Token: 0x060035A9 RID: 13737 RVA: 0x0057812C File Offset: 0x0057632C
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

		// Token: 0x060035AA RID: 13738 RVA: 0x00578224 File Offset: 0x00576424
		private Color StripColors(float progressOnStrip)
		{
			float lerpValue = Utils.GetLerpValue(0f - 0.1f * this.transitToDark, 0.7f - 0.2f * this.transitToDark, progressOnStrip, true);
			Color result = Color.Lerp(Color.Lerp(Color.White, Color.Orange, this.transitToDark * 0.5f), Color.Red, lerpValue) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, false));
			result.A /= 8;
			return result;
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x005782B4 File Offset: 0x005764B4
		private float StripWidth(float progressOnStrip)
		{
			float lerpValue = Utils.GetLerpValue(0f, 0.06f + this.transitToDark * 0.01f, progressOnStrip, true);
			lerpValue = 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(24f + this.transitToDark * 16f, 8f, Utils.GetLerpValue(0f, 1f, progressOnStrip, true)) * lerpValue;
		}

		// Token: 0x04004FD2 RID: 20434
		private static VertexStrip _vertexStrip = new VertexStrip();

		// Token: 0x04004FD3 RID: 20435
		private float transitToDark;
	}
}
