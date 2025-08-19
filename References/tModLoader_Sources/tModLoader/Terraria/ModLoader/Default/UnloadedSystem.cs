using System;
using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002DA RID: 730
	[LegacyName(new string[]
	{
		"UnloadedWorld"
	})]
	public class UnloadedSystem : ModSystem
	{
		// Token: 0x06002E0B RID: 11787 RVA: 0x00530CE0 File Offset: 0x0052EEE0
		public override void ClearWorld()
		{
			this.data = new List<TagCompound>();
			this.unloadedNPCs = new List<TagCompound>();
			this.unloadedKillCounts = new List<TagCompound>();
			this.unloadedBestiaryKills = new List<TagCompound>();
			this.unloadedBestiarySights = new List<TagCompound>();
			this.unloadedBestiaryChats = new List<TagCompound>();
			TileIO.ClearWorld();
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x00530D34 File Offset: 0x0052EF34
		public override void Unload()
		{
			TileIO.ResetUnloadedTypes();
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x00530D3C File Offset: 0x0052EF3C
		public override void SaveWorldData(TagCompound tag)
		{
			tag["list"] = this.data;
			tag["unloadedNPCs"] = this.unloadedNPCs;
			tag["unloadedKillCounts"] = this.unloadedKillCounts;
			tag["unloadedBestiaryKills"] = this.unloadedBestiaryKills;
			tag["unloadedBestiarySights"] = this.unloadedBestiarySights;
			tag["unloadedBestiaryChats"] = this.unloadedBestiaryChats;
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x00530DB0 File Offset: 0x0052EFB0
		public override void LoadWorldData(TagCompound tag)
		{
			WorldIO.LoadModData(tag.GetList<TagCompound>("list"));
			WorldIO.LoadNPCs(tag.GetList<TagCompound>("unloadedNPCs"));
			WorldIO.LoadNPCKillCounts(tag.GetList<TagCompound>("unloadedKillCounts"));
			WorldIO.LoadNPCBestiaryKills(tag.GetList<TagCompound>("unloadedBestiaryKills"));
			WorldIO.LoadNPCBestiarySights(tag.GetList<TagCompound>("unloadedBestiarySights"));
			WorldIO.LoadNPCBestiaryChats(tag.GetList<TagCompound>("unloadedBestiaryChats"));
		}

		// Token: 0x04001C6F RID: 7279
		internal IList<TagCompound> data;

		// Token: 0x04001C70 RID: 7280
		internal IList<TagCompound> unloadedNPCs;

		// Token: 0x04001C71 RID: 7281
		internal IList<TagCompound> unloadedKillCounts;

		// Token: 0x04001C72 RID: 7282
		internal IList<TagCompound> unloadedBestiaryKills;

		// Token: 0x04001C73 RID: 7283
		internal IList<TagCompound> unloadedBestiarySights;

		// Token: 0x04001C74 RID: 7284
		internal IList<TagCompound> unloadedBestiaryChats;
	}
}
