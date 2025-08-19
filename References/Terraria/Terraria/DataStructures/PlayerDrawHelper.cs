using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.DataStructures
{
	// Token: 0x0200044F RID: 1103
	public class PlayerDrawHelper
	{
		// Token: 0x06002C0A RID: 11274 RVA: 0x005A0924 File Offset: 0x0059EB24
		public static int PackShader(int localShaderIndex, PlayerDrawHelper.ShaderConfiguration shaderType)
		{
			return (int)(localShaderIndex + shaderType * (PlayerDrawHelper.ShaderConfiguration)1000);
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x005A092F File Offset: 0x0059EB2F
		public static void UnpackShader(int packedShaderIndex, out int localShaderIndex, out PlayerDrawHelper.ShaderConfiguration shaderType)
		{
			shaderType = (PlayerDrawHelper.ShaderConfiguration)(packedShaderIndex / 1000);
			localShaderIndex = packedShaderIndex % 1000;
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x005A0944 File Offset: 0x0059EB44
		public static void SetShaderForData(Player player, int cHead, ref DrawData cdd)
		{
			int num;
			PlayerDrawHelper.ShaderConfiguration shaderConfiguration;
			PlayerDrawHelper.UnpackShader(cdd.shader, out num, out shaderConfiguration);
			switch (shaderConfiguration)
			{
			case PlayerDrawHelper.ShaderConfiguration.ArmorShader:
				GameShaders.Hair.Apply(0, player, new DrawData?(cdd));
				GameShaders.Armor.Apply(num, player, new DrawData?(cdd));
				return;
			case PlayerDrawHelper.ShaderConfiguration.HairShader:
				if (player.head == 0)
				{
					GameShaders.Hair.Apply(0, player, new DrawData?(cdd));
					GameShaders.Armor.Apply(cHead, player, new DrawData?(cdd));
					return;
				}
				GameShaders.Armor.Apply(0, player, new DrawData?(cdd));
				GameShaders.Hair.Apply((short)num, player, new DrawData?(cdd));
				return;
			case PlayerDrawHelper.ShaderConfiguration.TileShader:
				Main.tileShader.CurrentTechnique.Passes[num].Apply();
				return;
			case PlayerDrawHelper.ShaderConfiguration.TilePaintID:
			{
				int index = Main.ConvertPaintIdToTileShaderIndex(num, false, false);
				Main.tileShader.CurrentTechnique.Passes[index].Apply();
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x04005029 RID: 20521
		public static Color DISPLAY_DOLL_DEFAULT_SKIN_COLOR = new Color(163, 121, 92);

		// Token: 0x0200077D RID: 1917
		public enum ShaderConfiguration
		{
			// Token: 0x0400649D RID: 25757
			ArmorShader,
			// Token: 0x0400649E RID: 25758
			HairShader,
			// Token: 0x0400649F RID: 25759
			TileShader,
			// Token: 0x040064A0 RID: 25760
			TilePaintID
		}
	}
}
