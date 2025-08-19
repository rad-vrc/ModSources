using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
	// Token: 0x02000437 RID: 1079
	public struct EmpressBladeDrawer
	{
		// Token: 0x0600359F RID: 13727 RVA: 0x00577AAC File Offset: 0x00575CAC
		public void Draw(Projectile proj)
		{
			float num4 = proj.ai[1];
			MiscShaderData miscShaderData = GameShaders.Misc["EmpressBlade"];
			int num = 1;
			int num2 = 0;
			int num3 = 0;
			float w = 0.6f;
			miscShaderData.UseShaderSpecificData(new Vector4((float)num, (float)num2, (float)num3, w));
			miscShaderData.Apply(null);
			EmpressBladeDrawer._vertexStrip.PrepareStrip(proj.oldPos, proj.oldRot, new VertexStrip.StripColorFunction(this.StripColors), new VertexStrip.StripHalfWidthFunction(this.StripWidth), -Main.screenPosition + proj.Size / 2f, new int?(proj.oldPos.Length), true);
			EmpressBladeDrawer._vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x00577B94 File Offset: 0x00575D94
		private Color StripColors(float progressOnStrip)
		{
			Color result = Color.Lerp(this.ColorStart, this.ColorEnd, Utils.GetLerpValue(0f, 0.7f, progressOnStrip, true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, true));
			result.A /= 2;
			return result;
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x00577BF1 File Offset: 0x00575DF1
		private float StripWidth(float progressOnStrip)
		{
			return 36f;
		}

		// Token: 0x04004FC8 RID: 20424
		public const int TotalIllusions = 1;

		// Token: 0x04004FC9 RID: 20425
		public const int FramesPerImportantTrail = 60;

		// Token: 0x04004FCA RID: 20426
		private static VertexStrip _vertexStrip = new VertexStrip();

		// Token: 0x04004FCB RID: 20427
		public Color ColorStart;

		// Token: 0x04004FCC RID: 20428
		public Color ColorEnd;
	}
}
