using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
	// Token: 0x020000ED RID: 237
	public struct EmpressBladeDrawer
	{
		// Token: 0x060015B9 RID: 5561 RVA: 0x004C3A24 File Offset: 0x004C1C24
		public void Draw(Projectile proj)
		{
			float num = proj.ai[1];
			MiscShaderData miscShaderData = GameShaders.Misc["EmpressBlade"];
			int num2 = 1;
			int num3 = 0;
			int num4 = 0;
			float w = 0.6f;
			miscShaderData.UseShaderSpecificData(new Vector4((float)num2, (float)num3, (float)num4, w));
			miscShaderData.Apply(null);
			EmpressBladeDrawer._vertexStrip.PrepareStrip(proj.oldPos, proj.oldRot, new VertexStrip.StripColorFunction(this.StripColors), new VertexStrip.StripHalfWidthFunction(this.StripWidth), -Main.screenPosition + proj.Size / 2f, new int?(proj.oldPos.Length), true);
			EmpressBladeDrawer._vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x004C3B0C File Offset: 0x004C1D0C
		private Color StripColors(float progressOnStrip)
		{
			Color result = Color.Lerp(this.ColorStart, this.ColorEnd, Utils.GetLerpValue(0f, 0.7f, progressOnStrip, true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, true));
			result.A /= 2;
			return result;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x004C3B69 File Offset: 0x004C1D69
		private float StripWidth(float progressOnStrip)
		{
			return 36f;
		}

		// Token: 0x040012DB RID: 4827
		public const int TotalIllusions = 1;

		// Token: 0x040012DC RID: 4828
		public const int FramesPerImportantTrail = 60;

		// Token: 0x040012DD RID: 4829
		private static VertexStrip _vertexStrip = new VertexStrip();

		// Token: 0x040012DE RID: 4830
		public Color ColorStart;

		// Token: 0x040012DF RID: 4831
		public Color ColorEnd;
	}
}
