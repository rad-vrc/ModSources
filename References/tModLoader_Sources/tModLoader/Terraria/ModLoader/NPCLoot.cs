using System;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;

namespace Terraria.ModLoader
{
	/// <summary> Provides access to <see cref="T:Terraria.GameContent.ItemDropRules.ItemDropDatabase" />'s methods specific to this NPC type. </summary>
	// Token: 0x020001E0 RID: 480
	public readonly struct NPCLoot : ILoot
	{
		// Token: 0x06002582 RID: 9602 RVA: 0x004F1901 File Offset: 0x004EFB01
		public NPCLoot(int npcNetId, ItemDropDatabase itemDropDatabase)
		{
			this.npcNetId = npcNetId;
			this.itemDropDatabase = itemDropDatabase;
		}

		/// <inheritdoc cref="M:Terraria.GameContent.ItemDropRules.ItemDropDatabase.GetRulesForNPCID(System.Int32,System.Boolean)" />
		// Token: 0x06002583 RID: 9603 RVA: 0x004F1911 File Offset: 0x004EFB11
		public List<IItemDropRule> Get(bool includeGlobalDrops = true)
		{
			return this.itemDropDatabase.GetRulesForNPCID(this.npcNetId, includeGlobalDrops);
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x004F1925 File Offset: 0x004EFB25
		public IItemDropRule Add(IItemDropRule entry)
		{
			this.itemDropDatabase.RegisterToNPCNetId(this.npcNetId, entry);
			return entry;
		}

		/// <summary>
		/// Removes a specific <see cref="T:Terraria.GameContent.ItemDropRules.IItemDropRule" /> from this NPC type.
		/// <para /> Note that <paramref name="entry" /> must be an existing <see cref="T:Terraria.GameContent.ItemDropRules.IItemDropRule" /> instance retrieved from <see cref="M:Terraria.ModLoader.NPCLoot.Get(System.Boolean)" />, not a newly created instance.
		/// </summary>
		// Token: 0x06002585 RID: 9605 RVA: 0x004F193A File Offset: 0x004EFB3A
		public IItemDropRule Remove(IItemDropRule entry)
		{
			this.itemDropDatabase.RemoveFromNPCNetId(this.npcNetId, entry);
			return entry;
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x004F1950 File Offset: 0x004EFB50
		public void RemoveWhere(Predicate<IItemDropRule> predicate, bool includeGlobalDrops = true)
		{
			foreach (IItemDropRule entry in this.Get(includeGlobalDrops))
			{
				if (predicate(entry))
				{
					this.Remove(entry);
				}
			}
		}

		// Token: 0x040017BA RID: 6074
		private readonly int npcNetId;

		// Token: 0x040017BB RID: 6075
		private readonly ItemDropDatabase itemDropDatabase;
	}
}
