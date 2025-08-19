using System;
using System.IO;
using System.Linq;
using System.Reflection;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.BuffChanges;
using QoLCompendium.Core.UI.Panels;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium
{
	// Token: 0x02000002 RID: 2
	public class QoLCompendium : Mod
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public override uint ExtraPlayerBuffSlots
		{
			get
			{
				return (uint)QoLCompendium.mainConfig.ExtraBuffSlots;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
		public override void PostSetupContent()
		{
			BuffSystem.DoBuffIntegration();
			Common.PostSetupTasks();
			LoadModSupport.PostSetupTasks();
			PermanentCalamityBuffUI.GetCalamityBuffData();
			PermanentMartinsOrderBuffUI.GetMartinsOrderBuffData();
			PermanentSOTSBuffUI.GetSOTSBuffData();
			PermanentSpiritClassicBuffUI.GetSpiritClassicBuffData();
			PermanentThoriumBuffUI.GetThoriumBuffData();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002086 File Offset: 0x00000286
		public override void Load()
		{
			On_Player.HandleBeingInChestRange += new On_Player.hook_HandleBeingInChestRange(this.ChestRange);
			On_WorldGen.moveRoom += new On_WorldGen.hook_moveRoom(this.WorldGen_moveRoom);
			QoLCompendium.instance = this;
			QoLCompendium.Instance = this;
			ModConditions.LoadSupportedMods();
			LoadModSupport.LoadTasks();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020C0 File Offset: 0x000002C0
		public override void Unload()
		{
			QoLCompendium.instance = null;
			QoLCompendium.Instance = null;
			QoLCompendium.mainConfig = null;
			QoLCompendium.mainClientConfig = null;
			QoLCompendium.itemConfig = null;
			QoLCompendium.shopConfig = null;
			QoLCompendium.tooltipConfig = null;
			On_Player.HandleBeingInChestRange -= new On_Player.hook_HandleBeingInChestRange(this.ChestRange);
			On_WorldGen.moveRoom -= new On_WorldGen.hook_moveRoom(this.WorldGen_moveRoom);
			Common.UnloadTasks();
			LoadModSupport.UnloadTasks();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002124 File Offset: 0x00000324
		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
			foreach (NPC npc in from n in Main.npc
			where n != null && n.active && n.townNPC && !n.homeless
			select n)
			{
				QoLCompendium.TownEntitiesTeleportToHome(npc, npc.homeTileX, npc.homeTileY);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021A0 File Offset: 0x000003A0
		private void WorldGen_moveRoom(On_WorldGen.orig_moveRoom orig, int x, int y, int n)
		{
			orig.Invoke(x, y, n);
			if (Main.npc.IndexInRange(n) && Main.npc[n] != null)
			{
				QoLCompendium.TownEntitiesTeleportToHome(Main.npc[n], Main.npc[n].homeTileX, Main.npc[n].homeTileY);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021F8 File Offset: 0x000003F8
		internal static void TownEntitiesTeleportToHome(NPC npc, int homeFloorX, int homeFloorY)
		{
			if (npc != null)
			{
				MethodInfo method = npc.GetType().GetMethod("AI_007_TownEntities_TeleportToHome", BindingFlags.Instance | BindingFlags.NonPublic, new Type[]
				{
					typeof(int),
					typeof(int)
				});
				if (method == null)
				{
					return;
				}
				method.Invoke(npc, new object[]
				{
					homeFloorX,
					homeFloorY
				});
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002260 File Offset: 0x00000460
		private void ChestRange(On_Player.orig_HandleBeingInChestRange orig, Player player)
		{
			int chest = player.chest;
			int? lastOpenedBank = QoLCompendium.LastOpenedBank;
			if (chest == lastOpenedBank.GetValueOrDefault() & lastOpenedBank != null)
			{
				return;
			}
			if (QoLCompendium.LastOpenedBank != null)
			{
				QoLCompendium.LastOpenedBank = null;
			}
			orig.Invoke(player);
		}

		// Token: 0x04000001 RID: 1
		public static Mod Instance;

		// Token: 0x04000002 RID: 2
		public static QoLCompendium instance;

		// Token: 0x04000003 RID: 3
		public static QoLCConfig mainConfig;

		// Token: 0x04000004 RID: 4
		public static QoLCConfig.MainClientConfig mainClientConfig;

		// Token: 0x04000005 RID: 5
		public static QoLCConfig.ItemConfig itemConfig;

		// Token: 0x04000006 RID: 6
		public static QoLCConfig.ShopConfig shopConfig;

		// Token: 0x04000007 RID: 7
		public static QoLCConfig.TooltipConfig tooltipConfig;

		// Token: 0x04000008 RID: 8
		public static int? LastOpenedBank;
	}
}
