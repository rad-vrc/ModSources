using System;
using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002D1 RID: 721
	public class UnloadedGlobalItem : GlobalItem
	{
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06002DDC RID: 11740 RVA: 0x005305F7 File Offset: 0x0052E7F7
		// (set) Token: 0x06002DDD RID: 11741 RVA: 0x005305FF File Offset: 0x0052E7FF
		[CloneByReference]
		public string ModPrefixMod { get; internal set; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x00530608 File Offset: 0x0052E808
		// (set) Token: 0x06002DDF RID: 11743 RVA: 0x00530610 File Offset: 0x0052E810
		[CloneByReference]
		public string ModPrefixName { get; internal set; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x00530619 File Offset: 0x0052E819
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x0053061C File Offset: 0x0052E81C
		public override void SaveData(Item item, TagCompound tag)
		{
			throw new NotSupportedException("UnloadedGlobalItem data is meant to be flattened and saved transparently via ItemIO");
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x00530628 File Offset: 0x0052E828
		public override void LoadData(Item item, TagCompound tag)
		{
			if (tag.ContainsKey("modData"))
			{
				ItemIO.LoadGlobals(item, tag.GetList<TagCompound>("modData"));
			}
		}

		// Token: 0x04001C66 RID: 7270
		[CloneByReference]
		internal IList<TagCompound> data = new List<TagCompound>();
	}
}
