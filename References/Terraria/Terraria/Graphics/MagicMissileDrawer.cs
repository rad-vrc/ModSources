using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
	// Token: 0x020000EE RID: 238
	public struct MagicMissileDrawer
	{
		// Token: 0x060015BD RID: 5565 RVA: 0x004C3B7C File Offset: 0x004C1D7C
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

		// Token: 0x060015BE RID: 5566 RVA: 0x004C3C48 File Offset: 0x004C1E48
		private Color StripColors(float progressOnStrip)
		{
			Color result = Color.Lerp(Color.White, Color.Violet, Utils.GetLerpValue(0f, 0.7f, progressOnStrip, true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, false));
			result.A /= 2;
			return result;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x004C3CA3 File Offset: 0x004C1EA3
		private float StripWidth(float progressOnStrip)
		{
			return MathHelper.Lerp(26f, 32f, Utils.GetLerpValue(0f, 0.2f, progressOnStrip, true)) * Utils.GetLerpValue(0f, 0.07f, progressOnStrip, true);
		}

		// Token: 0x040012E0 RID: 4832
		private static VertexStrip _vertexStrip = new VertexStrip();
	}
}
