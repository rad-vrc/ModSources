using System;
using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002D2 RID: 722
	public class UnloadedGlobalNPC : GlobalNPC
	{
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x0053065B File Offset: 0x0052E85B
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x0053065E File Offset: 0x0052E85E
		public override bool NeedSaving(NPC npc)
		{
			return this.data.Count > 0;
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x0053066E File Offset: 0x0052E86E
		public override void SaveData(NPC npc, TagCompound tag)
		{
			throw new NotSupportedException("UnloadedGlobalNPC data is meant to be flattened and saved transparently via ItemIO");
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x0053067A File Offset: 0x0052E87A
		public override void LoadData(NPC npc, TagCompound tag)
		{
		}

		// Token: 0x04001C69 RID: 7273
		internal IList<TagCompound> data = new List<TagCompound>();
	}
}
