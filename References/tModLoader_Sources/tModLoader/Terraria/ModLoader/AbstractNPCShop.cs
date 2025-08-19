using System;
using System.Collections.Generic;

namespace Terraria.ModLoader
{
	// Token: 0x020001E1 RID: 481
	public abstract class AbstractNPCShop
	{
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06002587 RID: 9607 RVA: 0x004F19B0 File Offset: 0x004EFBB0
		// (set) Token: 0x06002588 RID: 9608 RVA: 0x004F19B8 File Offset: 0x004EFBB8
		public int NpcType { get; private set; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06002589 RID: 9609 RVA: 0x004F19C1 File Offset: 0x004EFBC1
		// (set) Token: 0x0600258A RID: 9610 RVA: 0x004F19C9 File Offset: 0x004EFBC9
		public string Name { get; private set; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x004F19D2 File Offset: 0x004EFBD2
		public string FullName
		{
			get
			{
				return NPCShopDatabase.GetShopName(this.NpcType, this.Name);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600258C RID: 9612
		public abstract IEnumerable<AbstractNPCShop.Entry> ActiveEntries { get; }

		// Token: 0x0600258D RID: 9613 RVA: 0x004F19E5 File Offset: 0x004EFBE5
		public AbstractNPCShop(int npcType, string name = "Shop")
		{
			this.NpcType = npcType;
			this.Name = name;
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x004F19FB File Offset: 0x004EFBFB
		public void Register()
		{
			NPCShopDatabase.AddShop(this);
		}

		/// <summary>
		/// Unbounded variant of <see cref="M:Terraria.ModLoader.AbstractNPCShop.FillShop(Terraria.Item[],Terraria.NPC,System.Boolean@)" />, for future forwards compatibility with tabbed or scrolling shops.
		/// </summary>
		/// <param name="items">The collection to be filled</param>
		/// <param name="npc">The NPC the player is talking to</param>
		// Token: 0x0600258F RID: 9615
		public abstract void FillShop(ICollection<Item> items, NPC npc);

		/// <summary>
		/// Fills a shop array with the contents of this shop, evaluating conditions and running callbacks.
		/// </summary>
		/// <param name="items">Array to be filled.</param>
		/// <param name="npc">The NPC the player is talking to</param>
		/// <param name="overflow">True if some items were unable to fit in the provided array</param>
		// Token: 0x06002590 RID: 9616
		public abstract void FillShop(Item[] items, NPC npc, out bool overflow);

		// Token: 0x06002591 RID: 9617 RVA: 0x004F1A03 File Offset: 0x004EFC03
		public virtual void FinishSetup()
		{
		}

		// Token: 0x02000978 RID: 2424
		public interface Entry
		{
			// Token: 0x170008E1 RID: 2273
			// (get) Token: 0x060054DA RID: 21722
			Item Item { get; }

			// Token: 0x170008E2 RID: 2274
			// (get) Token: 0x060054DB RID: 21723
			IEnumerable<Condition> Conditions { get; }
		}
	}
}
