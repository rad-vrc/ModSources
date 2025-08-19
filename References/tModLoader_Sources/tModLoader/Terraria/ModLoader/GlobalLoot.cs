using System;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;

namespace Terraria.ModLoader
{
	/// <summary> This readonly struct is a simple shortcut for modifying global drop rules in an <see cref="T:Terraria.GameContent.ItemDropRules.ItemDropDatabase" />. </summary>
	// Token: 0x0200016D RID: 365
	public readonly struct GlobalLoot : ILoot
	{
		// Token: 0x06001D0E RID: 7438 RVA: 0x004D3DA7 File Offset: 0x004D1FA7
		public GlobalLoot(ItemDropDatabase itemDropDatabase)
		{
			this.itemDropDatabase = itemDropDatabase;
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x004D3DB0 File Offset: 0x004D1FB0
		public List<IItemDropRule> Get(bool unusedParam = true)
		{
			return new List<IItemDropRule>(this.itemDropDatabase._globalEntries);
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x004D3DC2 File Offset: 0x004D1FC2
		public IItemDropRule Add(IItemDropRule entry)
		{
			return this.itemDropDatabase.RegisterToGlobal(entry);
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x004D3DD0 File Offset: 0x004D1FD0
		public IItemDropRule Remove(IItemDropRule entry)
		{
			this.itemDropDatabase._globalEntries.Remove(entry);
			return entry;
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x004D3DE8 File Offset: 0x004D1FE8
		public void RemoveWhere(Predicate<IItemDropRule> predicate, bool unusedParam = true)
		{
			List<IItemDropRule> list = this.itemDropDatabase._globalEntries;
			for (int i = 0; i < list.Count; i++)
			{
				IItemDropRule entry = list[i];
				if (predicate(entry))
				{
					list.RemoveAt(i--);
				}
			}
		}

		// Token: 0x040015B6 RID: 5558
		private readonly ItemDropDatabase itemDropDatabase;
	}
}
