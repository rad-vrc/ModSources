using System;
using System.IO;
using System.Linq;
using System.Reflection;
using ReLogic.Content.Sources;
using Terraria.DataStructures;
using Terraria.ModLoader.Assets;
using Terraria.ModLoader.Default.Developer;
using Terraria.ModLoader.Default.Patreon;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002C7 RID: 711
	internal class ModLoaderMod : Mod
	{
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06002DA2 RID: 11682 RVA: 0x0052F6F8 File Offset: 0x0052D8F8
		public override string Name
		{
			get
			{
				return "ModLoader";
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06002DA3 RID: 11683 RVA: 0x0052F6FF File Offset: 0x0052D8FF
		public override Version Version
		{
			get
			{
				return BuildInfo.tMLVersion;
			}
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x0052F706 File Offset: 0x0052D906
		internal ModLoaderMod()
		{
			base.Side = ModSide.NoSync;
			base.DisplayName = "tModLoader";
			base.Code = Assembly.GetExecutingAssembly();
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x0052F72B File Offset: 0x0052D92B
		public override IContentSource CreateDefaultContentSource()
		{
			return new AssemblyResourcesContentSource(Assembly.GetExecutingAssembly(), "Terraria.ModLoader.Default.", null);
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x0052F740 File Offset: 0x0052D940
		public override void Load()
		{
			ModLoaderMod.PatronSets = (from t in base.GetContent<PatreonItem>()
			group t by t.InternalSetName into set
			select set.ToArray<PatreonItem>()).ToArray<PatreonItem[]>();
			ModLoaderMod.DeveloperSets = (from t in base.GetContent<DeveloperItem>()
			group t by t.InternalSetName into set
			select set.ToArray<DeveloperItem>()).ToArray<DeveloperItem[]>();
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x0052F7FD File Offset: 0x0052D9FD
		public override void Unload()
		{
			ModLoaderMod.PatronSets = null;
			ModLoaderMod.DeveloperSets = null;
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x0052F80C File Offset: 0x0052DA0C
		internal static bool TryGettingPatreonOrDevArmor(IEntitySource source, Player player)
		{
			if (Main.rand.NextBool(14))
			{
				int randomIndex = Main.rand.Next(ModLoaderMod.PatronSets.Length);
				foreach (PatreonItem patreonItem in ModLoaderMod.PatronSets[randomIndex])
				{
					player.QuickSpawnItem(source, patreonItem.Type, 1);
				}
				return true;
			}
			if (Main.rand.NextBool(50))
			{
				int randomIndex2 = Main.rand.Next(ModLoaderMod.DeveloperSets.Length);
				foreach (DeveloperItem developerItem in ModLoaderMod.DeveloperSets[randomIndex2])
				{
					player.QuickSpawnItem(source, developerItem.Type, 1);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x0052F8B8 File Offset: 0x0052DAB8
		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
			byte packetType = reader.ReadByte();
			if (packetType == 0)
			{
				ModAccessorySlotPlayer.NetHandler.HandlePacket(reader, whoAmI);
				return;
			}
			if (packetType != 1)
			{
				return;
			}
			ConsumedStatIncreasesPlayer.NetHandler.HandlePacket(reader, whoAmI);
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x0052F8E3 File Offset: 0x0052DAE3
		internal static ModPacket GetPacket(byte packetType)
		{
			ModPacket packet = ModContent.GetInstance<ModLoaderMod>().GetPacket(256);
			packet.Write(packetType);
			return packet;
		}

		// Token: 0x04001C5C RID: 7260
		private static PatreonItem[][] PatronSets;

		// Token: 0x04001C5D RID: 7261
		private static DeveloperItem[][] DeveloperSets;

		// Token: 0x04001C5E RID: 7262
		private const int ChanceToGetPatreonArmor = 14;

		// Token: 0x04001C5F RID: 7263
		private const int ChanceToGetDevArmor = 50;

		// Token: 0x04001C60 RID: 7264
		internal const byte AccessorySlotPacket = 0;

		// Token: 0x04001C61 RID: 7265
		internal const byte StatResourcesPacket = 1;
	}
}
