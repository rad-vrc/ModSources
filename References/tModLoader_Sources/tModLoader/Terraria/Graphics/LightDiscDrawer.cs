using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
	// Token: 0x0200043A RID: 1082
	public struct LightDiscDrawer
	{
		// Token: 0x060035AD RID: 13741 RVA: 0x00578334 File Offset: 0x00576534
		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["LightDisc"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(2f);
			miscShaderData.Apply(null);
			LightDiscDrawer._vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, new VertexStrip.StripColorFunction(this.StripColors), new VertexStrip.StripHalfWidthFunction(this.StripWidth), -Main.screenPosition + proj.Size / 2f, false, true);
			LightDiscDrawer._vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x00578400 File Offset: 0x00576600
		private Color StripColors(float progressOnStrip)
		{
			float num = 1f - progressOnStrip;
			Color result = new Color(48, 63, 150) * (num * num * num * num) * 0.5f;
			result.A = 0;
			return result;
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x00578443 File Offset: 0x00576643
		private float StripWidth(float progressOnStrip)
		{
			return 16f;
		}

		// Token: 0x04004FD4 RID: 20436
		private static VertexStrip _vertexStrip = new VertexStrip();
	}
}
