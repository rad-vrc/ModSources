using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.WorldChanges
{
	// Token: 0x02000211 RID: 529
	public class DisableMeteors : ModSystem
	{
		// Token: 0x06000CF7 RID: 3319 RVA: 0x00064DFF File Offset: 0x00062FFF
		public override void Load()
		{
			On_WorldGen.dropMeteor += new On_WorldGen.hook_dropMeteor(this.StopMeteor);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00064E12 File Offset: 0x00063012
		public override void Unload()
		{
			On_WorldGen.dropMeteor -= new On_WorldGen.hook_dropMeteor(this.StopMeteor);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00064E28 File Offset: 0x00063028
		private void StopMeteor(On_WorldGen.orig_dropMeteor orig)
		{
			if (!QoLCompendium.mainConfig.NoMeteorSpawns)
			{
				orig.Invoke();
				return;
			}
			ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.QoLCompendium.Messages.MeteorStopped", Array.Empty<object>()), Color.White, -1);
			int activePlayerCount = (from player in Main.player
			where player.active
			select player).ToArray<Player>().Length;
			for (int i = 0; i < Main.player.Length; i++)
			{
				Player player2 = Main.player[i];
				if (player2.active)
				{
					int amount = Main.rand.Next(400, 500) / activePlayerCount;
					player2.QuickSpawnItem(player2.GetSource_FromThis(null), 116, amount);
					ChatHelper.SendChatMessageToClient(NetworkText.FromKey("Mods.QoLCompendium.Messages.MeteoriteGiven", new object[]
					{
						amount
					}), Color.White, i);
				}
			}
		}
	}
}
