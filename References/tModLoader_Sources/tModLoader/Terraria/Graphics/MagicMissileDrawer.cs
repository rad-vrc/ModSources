using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
	// Token: 0x0200043B RID: 1083
	public struct MagicMissileDrawer
	{
		// Token: 0x060035B1 RID: 13745 RVA: 0x00578458 File Offset: 0x00576658
		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["MagicMissile"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(2f);
			miscShaderData.Apply(null);
			MagicMissileDrawer._vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, new VertexStrip.StripColorFunction(this.StripColors), new VertexStrip.StripHalfWidthFunction(this.StripWidth), -Main.screenPosition + proj.Size / 2f, false, true);
			MagicMissileDrawer._vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x00578524 File Offset: 0x00576724
		private Color StripColors(float progressOnStrip)
		{
			Color result = Color.Lerp(Color.White, Color.Violet, Utils.GetLerpValue(0f, 0.7f, progressOnStrip, true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, false));
			result.A /= 2;
			return result;
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x0057857F File Offset: 0x0057677F
		private float StripWidth(float progressOnStrip)
		{
			return MathHelper.Lerp(26f, 32f, Utils.GetLerpValue(0f, 0.2f, progressOnStrip, true)) * Utils.GetLerpValue(0f, 0.07f, progressOnStrip, true);
		}

		// Token: 0x04004FD5 RID: 20437
		private static VertexStrip _vertexStrip = new VertexStrip();
	}
}
