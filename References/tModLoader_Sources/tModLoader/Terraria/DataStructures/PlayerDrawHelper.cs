using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.DataStructures
{
	// Token: 0x02000725 RID: 1829
	public class PlayerDrawHelper
	{
		// Token: 0x06004A2C RID: 18988 RVA: 0x00655616 File Offset: 0x00653816
		public static int PackShader(int localShaderIndex, PlayerDrawHelper.ShaderConfiguration shaderType)
		{
			return (int)(localShaderIndex + shaderType * (PlayerDrawHelper.ShaderConfiguration)1000);
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x00655621 File Offset: 0x00653821
		public static void UnpackShader(int packedShaderIndex, out int localShaderIndex, out PlayerDrawHelper.ShaderConfiguration shaderType)
		{
			shaderType = (PlayerDrawHelper.ShaderConfiguration)(packedShaderIndex / 1000);
			localShaderIndex = packedShaderIndex % 1000;
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x00655638 File Offset: 0x00653838
		public static void SetShaderForData(Player player, int cHead, ref DrawData cdd)
		{
			int localShaderIndex;
			PlayerDrawHelper.ShaderConfiguration shaderType;
			PlayerDrawHelper.UnpackShader(cdd.shader, out localShaderIndex, out shaderType);
			switch (shaderType)
			{
			case PlayerDrawHelper.ShaderConfiguration.ArmorShader:
				GameShaders.Hair.Apply(0, player, new DrawData?(cdd));
				GameShaders.Armor.Apply(localShaderIndex, player, new DrawData?(cdd));
				return;
			case PlayerDrawHelper.ShaderConfiguration.HairShader:
				if (player.head == 0)
				{
					GameShaders.Hair.Apply(0, player, new DrawData?(cdd));
					GameShaders.Armor.Apply(cHead, player, new DrawData?(cdd));
					return;
				}
				GameShaders.Armor.Apply(0, player, new DrawData?(cdd));
				GameShaders.Hair.Apply((int)((short)localShaderIndex), player, new DrawData?(cdd));
				return;
			case PlayerDrawHelper.ShaderConfiguration.TileShader:
				Main.tileShader.CurrentTechnique.Passes[localShaderIndex].Apply();
				return;
			case PlayerDrawHelper.ShaderConfiguration.TilePaintID:
			{
				int index = Main.ConvertPaintIdToTileShaderIndex(localShaderIndex, false, false);
				Main.tileShader.CurrentTechnique.Passes[index].Apply();
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x04005FA9 RID: 24489
		public static Color DISPLAY_DOLL_DEFAULT_SKIN_COLOR = new Color(163, 121, 92);

		// Token: 0x02000D5B RID: 3419
		public enum ShaderConfiguration
		{
			// Token: 0x04007B86 RID: 31622
			ArmorShader,
			// Token: 0x04007B87 RID: 31623
			HairShader,
			// Token: 0x04007B88 RID: 31624
			TileShader,
			// Token: 0x04007B89 RID: 31625
			TilePaintID
		}
	}
}
