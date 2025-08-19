using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
	// Token: 0x020000EF RID: 239
	public struct LightDiscDrawer
	{
		// Token: 0x060015C1 RID: 5569 RVA: 0x004C3CE4 File Offset: 0x004C1EE4
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

		// Token: 0x060015C2 RID: 5570 RVA: 0x004C3DB0 File Offset: 0x004C1FB0
		private Color StripColors(float progressOnStrip)
		{
			float num = 1f - progressOnStrip;
			Color result = new Color(48, 63, 150) * (num * num * num * num) * 0.5f;
			result.A = 0;
			return result;
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x004C3DF3 File Offset: 0x004C1FF3
		private float StripWidth(float progressOnStrip)
		{
			return 16f;
		}

		// Token: 0x040012E1 RID: 4833
		private static VertexStrip _vertexStrip = new VertexStrip();
	}
}
