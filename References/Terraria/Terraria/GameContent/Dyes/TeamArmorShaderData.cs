using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
	// Token: 0x0200021F RID: 543
	public class TeamArmorShaderData : ArmorShaderData
	{
		// Token: 0x06001EB8 RID: 7864 RVA: 0x0050C478 File Offset: 0x0050A678
		public TeamArmorShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
		{
			if (!TeamArmorShaderData.isInitialized)
			{
				TeamArmorShaderData.isInitialized = true;
				TeamArmorShaderData.dustShaderData = new ArmorShaderData[Main.teamColor.Length];
				for (int i = 1; i < Main.teamColor.Length; i++)
				{
					TeamArmorShaderData.dustShaderData[i] = new ArmorShaderData(shader, passName).UseColor(Main.teamColor[i]);
				}
				TeamArmorShaderData.dustShaderData[0] = new ArmorShaderData(shader, "Default");
			}
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x0050C4F0 File Offset: 0x0050A6F0
		public override void Apply(Entity entity, DrawData? drawData)
		{
			Player player = entity as Player;
			if (player == null || player.team == 0)
			{
				TeamArmorShaderData.dustShaderData[0].Apply(player, drawData);
				return;
			}
			base.UseColor(Main.teamColor[player.team]);
			base.Apply(player, drawData);
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x0050C540 File Offset: 0x0050A740
		public override ArmorShaderData GetSecondaryShader(Entity entity)
		{
			Player player = entity as Player;
			return TeamArmorShaderData.dustShaderData[player.team];
		}

		// Token: 0x040045BF RID: 17855
		private static bool isInitialized;

		// Token: 0x040045C0 RID: 17856
		private static ArmorShaderData[] dustShaderData;
	}
}
