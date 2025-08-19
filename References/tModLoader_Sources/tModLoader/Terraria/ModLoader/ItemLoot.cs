using System;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;

namespace Terraria.ModLoader
{
	/// <summary> Provides access to <see cref="T:Terraria.GameContent.ItemDropRules.ItemDropDatabase" />'s methods specific to this item type. </summary>
	// Token: 0x02000186 RID: 390
	public readonly struct ItemLoot : ILoot
	{
		// Token: 0x06001ECC RID: 7884 RVA: 0x004DD0B3 File Offset: 0x004DB2B3
		public ItemLoot(int itemType, ItemDropDatabase itemDropDatabase)
		{
			this.itemType = itemType;
			this.itemDropDatabase = itemDropDatabase;
		}

		/// <summary>
		/// <inheritdoc cref="M:Terraria.GameContent.ItemDropRules.ItemDropDatabase.GetRulesForItemID(System.Int32)" />
		/// </summary>
		/// <param name="includeGlobalDrops">Unused</param>
		// Token: 0x06001ECD RID: 7885 RVA: 0x004DD0C3 File Offset: 0x004DB2C3
		public List<IItemDropRule> Get(bool includeGlobalDrops = true)
		{
			return this.itemDropDatabase.GetRulesForItemID(this.itemType);
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x004DD0D6 File Offset: 0x004DB2D6
		public IItemDropRule Add(IItemDropRule entry)
		{
			return this.itemDropDatabase.RegisterToItem(this.itemType, entry);
		}

		/// <summary>
		/// Removes a specific <see cref="T:Terraria.GameContent.ItemDropRules.IItemDropRule" /> from this item type.
		/// <para /> Note that <paramref name="entry" /> must be an existing <see cref="T:Terraria.GameContent.ItemDropRules.IItemDropRule" /> instance retrieved from <see cref="M:Terraria.ModLoader.ItemLoot.Get(System.Boolean)" />, not a newly created instance.
		/// </summary>
		// Token: 0x06001ECF RID: 7887 RVA: 0x004DD0EA File Offset: 0x004DB2EA
		public IItemDropRule Remove(IItemDropRule entry)
		{
			return this.itemDropDatabase.RemoveFromItem(this.itemType, entry);
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x004DD100 File Offset: 0x004DB300
		public void RemoveWhere(Predicate<IItemDropRule> predicate, bool includeGlobalDrops = true)
		{
			foreach (IItemDropRule entry in this.Get(true))
			{
				if (predicate(entry))
				{
					this.Remove(entry);
				}
			}
		}

		// Token: 0x04001646 RID: 5702
		private readonly int itemType;

		// Token: 0x04001647 RID: 5703
		private readonly ItemDropDatabase itemDropDatabase;
	}
}
