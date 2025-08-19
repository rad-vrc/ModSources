using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
	// Token: 0x02000636 RID: 1590
	public class TeamArmorShaderData : ArmorShaderData
	{
		// Token: 0x0600459F RID: 17823 RVA: 0x006147C0 File Offset: 0x006129C0
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

		// Token: 0x060045A0 RID: 17824 RVA: 0x00614838 File Offset: 0x00612A38
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

		// Token: 0x060045A1 RID: 17825 RVA: 0x00614888 File Offset: 0x00612A88
		public override ArmorShaderData GetSecondaryShader(Entity entity)
		{
			Player player = entity as Player;
			return TeamArmorShaderData.dustShaderData[player.team];
		}

		// Token: 0x04005B05 RID: 23301
		private static bool isInitialized;

		// Token: 0x04005B06 RID: 23302
		private static ArmorShaderData[] dustShaderData;
	}
}
