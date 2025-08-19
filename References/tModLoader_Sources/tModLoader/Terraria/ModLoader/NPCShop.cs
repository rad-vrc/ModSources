using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.ID;

namespace Terraria.ModLoader
{
	// Token: 0x020001E2 RID: 482
	public sealed class NPCShop : AbstractNPCShop
	{
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06002592 RID: 9618 RVA: 0x004F1A05 File Offset: 0x004EFC05
		public IReadOnlyList<NPCShop.Entry> Entries
		{
			get
			{
				return this._entries;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06002593 RID: 9619 RVA: 0x004F1A0D File Offset: 0x004EFC0D
		// (set) Token: 0x06002594 RID: 9620 RVA: 0x004F1A15 File Offset: 0x004EFC15
		public bool FillLastSlot { get; private set; }

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06002595 RID: 9621 RVA: 0x004F1A1E File Offset: 0x004EFC1E
		public IEnumerable<NPCShop.Entry> ActiveEntries
		{
			[PreserveBaseOverrides]
			get
			{
				return from e in this.Entries
				where !e.Disabled
				select e;
			}
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x004F1A4A File Offset: 0x004EFC4A
		public NPCShop(int npcType, string name = "Shop") : base(npcType, name)
		{
			this._entries = new List<NPCShop.Entry>();
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x004F1A60 File Offset: 0x004EFC60
		public NPCShop.Entry GetEntry(int item)
		{
			return this._entries.First((NPCShop.Entry x) => x.Item.type == item);
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x004F1A94 File Offset: 0x004EFC94
		public bool TryGetEntry(int item, out NPCShop.Entry entry)
		{
			int i = this._entries.FindIndex((NPCShop.Entry x) => x.Item.type == item);
			if (i == -1)
			{
				entry = null;
				return false;
			}
			entry = this._entries[i];
			return true;
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x004F1ADE File Offset: 0x004EFCDE
		public NPCShop AllowFillingLastSlot()
		{
			this.FillLastSlot = true;
			return this;
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x004F1AE8 File Offset: 0x004EFCE8
		public NPCShop Add(params NPCShop.Entry[] entries)
		{
			this._entries.AddRange(entries);
			return this;
		}

		/// <summary>
		/// <inheritdoc cref="M:Terraria.ModLoader.NPCShop.Add(System.Int32,Terraria.Condition[])" />
		/// <para /> This overload takes an <see cref="T:Terraria.Item" /> instance instead of just an item type. This Item can be customized prior to being registered. This is commonly used to provide a custom shop price or currency for this shop entry:
		/// <code>npcShop.Add(new Item(ItemID.MagicDagger) {
		/// 	shopCustomPrice = 2,
		/// 	shopSpecialCurrency = ExampleMod.ExampleCustomCurrencyId
		/// }, Condition.RemixWorld);
		/// </code>
		/// </summary>
		// Token: 0x0600259B RID: 9627 RVA: 0x004F1AF7 File Offset: 0x004EFCF7
		public NPCShop Add(Item item, params Condition[] condition)
		{
			return this.Add(new NPCShop.Entry[]
			{
				new NPCShop.Entry(item, condition)
			});
		}

		/// <summary> Adds the specified item with the provided conditions to this shop. If all of the conditions are satisfied, the item will be available in the shop. </summary>
		// Token: 0x0600259C RID: 9628 RVA: 0x004F1B0F File Offset: 0x004EFD0F
		public NPCShop Add(int item, params Condition[] condition)
		{
			return this.Add(new NPCShop.Entry[]
			{
				new NPCShop.Entry(item, condition)
			});
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.NPCShop.Add(System.Int32,Terraria.Condition[])" />
		// Token: 0x0600259D RID: 9629 RVA: 0x004F1B27 File Offset: 0x004EFD27
		public NPCShop Add<T>(params Condition[] condition) where T : ModItem
		{
			return this.Add(ModContent.ItemType<T>(), condition);
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x004F1B35 File Offset: 0x004EFD35
		private NPCShop InsertAt(NPCShop.Entry targetEntry, bool after, Item item, params Condition[] condition)
		{
			return this.Add(new NPCShop.Entry[]
			{
				new NPCShop.Entry(item, condition).SetOrdering(targetEntry, after)
			});
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x004F1B55 File Offset: 0x004EFD55
		private NPCShop InsertAt(int targetItem, bool after, Item item, params Condition[] condition)
		{
			return this.InsertAt(this.GetEntry(targetItem), after, item, condition);
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x004F1B68 File Offset: 0x004EFD68
		private NPCShop InsertAt(int targetItem, bool after, int item, params Condition[] condition)
		{
			return this.InsertAt(targetItem, after, ContentSamples.ItemsByType[item], condition);
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x004F1B7F File Offset: 0x004EFD7F
		public NPCShop InsertBefore(NPCShop.Entry targetEntry, Item item, params Condition[] condition)
		{
			return this.InsertAt(targetEntry, false, item, condition);
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x004F1B8B File Offset: 0x004EFD8B
		public NPCShop InsertBefore(int targetItem, Item item, params Condition[] condition)
		{
			return this.InsertAt(targetItem, false, item, condition);
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x004F1B97 File Offset: 0x004EFD97
		public NPCShop InsertBefore(int targetItem, int item, params Condition[] condition)
		{
			return this.InsertAt(targetItem, false, item, condition);
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x004F1BA3 File Offset: 0x004EFDA3
		public NPCShop InsertAfter(NPCShop.Entry targetEntry, Item item, params Condition[] condition)
		{
			return this.InsertAt(targetEntry, true, item, condition);
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x004F1BAF File Offset: 0x004EFDAF
		public NPCShop InsertAfter(int targetItem, Item item, params Condition[] condition)
		{
			return this.InsertAt(targetItem, true, item, condition);
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x004F1BBB File Offset: 0x004EFDBB
		public NPCShop InsertAfter(int targetItem, int item, params Condition[] condition)
		{
			return this.InsertAt(targetItem, true, item, condition);
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x004F1BC8 File Offset: 0x004EFDC8
		public override void FillShop(ICollection<Item> items, NPC npc)
		{
			foreach (NPCShop.Entry entry in this._entries)
			{
				if (!entry.Disabled)
				{
					Item item;
					if (entry.ConditionsMet())
					{
						item = entry.Item.Clone();
						entry.OnShopOpen(item, npc);
					}
					else
					{
						if (!entry.SlotReserved)
						{
							continue;
						}
						item = new Item(0, 1, 0);
					}
					items.Add(item);
				}
			}
		}

		/// <summary>
		/// Fills a shop array with the contents of this shop, evaluating conditions and running callbacks. <br />
		/// Does not fill the entire array if there are insufficient entries. <br />
		/// The last slot will be kept empty (null) if <see cref="P:Terraria.ModLoader.NPCShop.FillLastSlot" /> is false
		/// </summary>
		/// <param name="items">Array to be filled.</param>
		/// <param name="npc">The NPC the player is talking to, for <see cref="M:Terraria.ModLoader.NPCShop.Entry.OnShopOpen(Terraria.Item,Terraria.NPC)" /> calls.</param>
		/// <param name="overflow">True if some items were unable to fit in the provided array.</param>
		// Token: 0x060025A8 RID: 9640 RVA: 0x004F1C54 File Offset: 0x004EFE54
		public override void FillShop(Item[] items, NPC npc, out bool overflow)
		{
			overflow = false;
			int limit = this.FillLastSlot ? items.Length : (items.Length - 1);
			int i = 0;
			foreach (NPCShop.Entry entry in this._entries)
			{
				if (!entry.Disabled)
				{
					bool conditionsMet = entry.ConditionsMet();
					if (conditionsMet || entry.SlotReserved)
					{
						if (i == limit)
						{
							overflow = true;
							break;
						}
						Item item;
						if (conditionsMet)
						{
							item = entry.Item.Clone();
							entry.OnShopOpen(item, npc);
						}
						else
						{
							item = new Item(0, 1, 0);
						}
						items[i++] = item;
					}
				}
			}
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x004F1D0C File Offset: 0x004EFF0C
		public override void FinishSetup()
		{
			this.Sort();
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x004F1D14 File Offset: 0x004EFF14
		private void Sort()
		{
			List<NPCShop.Entry> toBeLast = (from x in this._entries
			where x.OrdersLast
			select x).ToList<NPCShop.Entry>();
			this._entries.RemoveAll((NPCShop.Entry x) => x.OrdersLast);
			this._entries.AddRange(toBeLast);
			this._entries = NPCShop.SortBeforeAfter<NPCShop.Entry>(this._entries, (NPCShop.Entry r) => r.Ordering);
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x004F1DB8 File Offset: 0x004EFFB8
		private static List<T> SortBeforeAfter<T>(IEnumerable<T> values, [TupleElementNames(new string[]
		{
			null,
			"after"
		})] Func<T, ValueTuple<T, bool>> func)
		{
			List<T> baseOrder = new List<T>();
			NPCShop.<>c__DisplayClass30_0<T> CS$<>8__locals1;
			CS$<>8__locals1.sortBefore = new Dictionary<T, List<T>>();
			CS$<>8__locals1.sortAfter = new Dictionary<T, List<T>>();
			foreach (T r in values)
			{
				ValueTuple<T, bool> valueTuple = func(r);
				T target = valueTuple.Item1;
				if (target != null)
				{
					if (valueTuple.Item2)
					{
						T target2 = target;
						List<T> after;
						if (!CS$<>8__locals1.sortAfter.TryGetValue(target2, out after))
						{
							after = (CS$<>8__locals1.sortAfter[target2] = new List<T>());
						}
						after.Add(r);
					}
					else
					{
						List<T> before;
						if (!CS$<>8__locals1.sortBefore.TryGetValue(target, out before))
						{
							before = (CS$<>8__locals1.sortBefore[target] = new List<T>());
						}
						before.Add(r);
					}
				}
				else
				{
					baseOrder.Add(r);
				}
			}
			if (CS$<>8__locals1.sortBefore.Count + CS$<>8__locals1.sortAfter.Count == 0)
			{
				return values.ToList<T>();
			}
			CS$<>8__locals1.sorted = new List<T>();
			foreach (T r2 in baseOrder)
			{
				NPCShop.<SortBeforeAfter>g__Sort|30_0<T>(r2, ref CS$<>8__locals1);
			}
			return CS$<>8__locals1.sorted;
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x004F1F28 File Offset: 0x004F0128
		[CompilerGenerated]
		internal static void <SortBeforeAfter>g__Sort|30_0<T>(T r, ref NPCShop.<>c__DisplayClass30_0<T> A_1)
		{
			List<T> before;
			if (A_1.sortBefore.TryGetValue(r, out before))
			{
				foreach (T r2 in before)
				{
					NPCShop.<SortBeforeAfter>g__Sort|30_0<T>(r2, ref A_1);
				}
			}
			A_1.sorted.Add(r);
			List<T> after;
			if (A_1.sortAfter.TryGetValue(r, out after))
			{
				foreach (T r3 in after)
				{
					NPCShop.<SortBeforeAfter>g__Sort|30_0<T>(r3, ref A_1);
				}
			}
		}

		// Token: 0x040017BE RID: 6078
		private List<NPCShop.Entry> _entries;

		// Token: 0x02000979 RID: 2425
		public new sealed class Entry : AbstractNPCShop.Entry
		{
			// Token: 0x170008E3 RID: 2275
			// (get) Token: 0x060054DC RID: 21724 RVA: 0x0069AEC6 File Offset: 0x006990C6
			public Item Item { get; }

			// Token: 0x170008E4 RID: 2276
			// (get) Token: 0x060054DD RID: 21725 RVA: 0x0069AECE File Offset: 0x006990CE
			public IEnumerable<Condition> Conditions
			{
				get
				{
					return this.conditions;
				}
			}

			// Token: 0x170008E5 RID: 2277
			// (get) Token: 0x060054DE RID: 21726 RVA: 0x0069AED6 File Offset: 0x006990D6
			// (set) Token: 0x060054DF RID: 21727 RVA: 0x0069AEDE File Offset: 0x006990DE
			[TupleElementNames(new string[]
			{
				"target",
				"after"
			})]
			internal ValueTuple<NPCShop.Entry, bool> Ordering { [return: TupleElementNames(new string[]
			{
				"target",
				"after"
			})] get; [param: TupleElementNames(new string[]
			{
				"target",
				"after"
			})] private set; } = new ValueTuple<NPCShop.Entry, bool>(null, false);

			// Token: 0x170008E6 RID: 2278
			// (get) Token: 0x060054E0 RID: 21728 RVA: 0x0069AEE7 File Offset: 0x006990E7
			// (set) Token: 0x060054E1 RID: 21729 RVA: 0x0069AEEF File Offset: 0x006990EF
			public bool Disabled { get; private set; }

			// Token: 0x170008E7 RID: 2279
			// (get) Token: 0x060054E2 RID: 21730 RVA: 0x0069AEF8 File Offset: 0x006990F8
			// (set) Token: 0x060054E3 RID: 21731 RVA: 0x0069AF00 File Offset: 0x00699100
			public bool OrdersLast { get; private set; }

			/// <inheritdoc cref="M:Terraria.ModLoader.NPCShop.Entry.ReserveSlot" />
			// Token: 0x170008E8 RID: 2280
			// (get) Token: 0x060054E4 RID: 21732 RVA: 0x0069AF09 File Offset: 0x00699109
			// (set) Token: 0x060054E5 RID: 21733 RVA: 0x0069AF11 File Offset: 0x00699111
			public bool SlotReserved { get; private set; }

			// Token: 0x060054E6 RID: 21734 RVA: 0x0069AF1A File Offset: 0x0069911A
			public Entry(int item, params Condition[] condition) : this(new Item(item, 1, 0), condition)
			{
			}

			// Token: 0x060054E7 RID: 21735 RVA: 0x0069AF2B File Offset: 0x0069912B
			public Entry(Item item, params Condition[] condition)
			{
				this.Disabled = false;
				this.Item = item;
				this.conditions = condition.ToList<Condition>();
			}

			// Token: 0x060054E8 RID: 21736 RVA: 0x0069AF5C File Offset: 0x0069915C
			internal NPCShop.Entry SetOrdering(NPCShop.Entry entry, bool after)
			{
				ArgumentNullException.ThrowIfNull(entry, "entry");
				this.Ordering = new ValueTuple<NPCShop.Entry, bool>(entry, after);
				NPCShop.Entry target = entry;
				while (target != this)
				{
					target = target.Ordering.Item1;
					if (target == null)
					{
						return this;
					}
				}
				throw new Exception("Entry ordering loop!");
			}

			// Token: 0x060054E9 RID: 21737 RVA: 0x0069AFA2 File Offset: 0x006991A2
			public NPCShop.Entry SortBefore(NPCShop.Entry target)
			{
				return this.SetOrdering(target, false);
			}

			// Token: 0x060054EA RID: 21738 RVA: 0x0069AFAC File Offset: 0x006991AC
			public NPCShop.Entry SortAfter(NPCShop.Entry target)
			{
				return this.SetOrdering(target, true);
			}

			// Token: 0x060054EB RID: 21739 RVA: 0x0069AFB6 File Offset: 0x006991B6
			public NPCShop.Entry AddCondition(Condition condition)
			{
				ArgumentNullException.ThrowIfNull(condition, "condition");
				this.conditions.Add(condition);
				return this;
			}

			// Token: 0x060054EC RID: 21740 RVA: 0x0069AFD0 File Offset: 0x006991D0
			public NPCShop.Entry OrderLast()
			{
				this.OrdersLast = true;
				return this;
			}

			// Token: 0x060054ED RID: 21741 RVA: 0x0069AFDA File Offset: 0x006991DA
			public NPCShop.Entry Disable()
			{
				this.Disabled = true;
				return this;
			}

			/// <summary>
			/// Reserves a slot for this entry even if its conditions are not met (<see cref="M:Terraria.ModLoader.NPCShop.Entry.ConditionsMet" />). This can be used to create a defined shop layout similar to the Tavernkeep shop.
			/// </summary>
			/// <returns></returns>
			// Token: 0x060054EE RID: 21742 RVA: 0x0069AFE4 File Offset: 0x006991E4
			public NPCShop.Entry ReserveSlot()
			{
				this.SlotReserved = true;
				return this;
			}

			// Token: 0x060054EF RID: 21743 RVA: 0x0069AFEE File Offset: 0x006991EE
			public NPCShop.Entry AddShopOpenedCallback(Action<Item, NPC> callback)
			{
				this.shopOpenedHooks = (Action<Item, NPC>)Delegate.Combine(this.shopOpenedHooks, callback);
				return this;
			}

			// Token: 0x060054F0 RID: 21744 RVA: 0x0069B008 File Offset: 0x00699208
			public void OnShopOpen(Item item, NPC npc)
			{
				Action<Item, NPC> action = this.shopOpenedHooks;
				if (action == null)
				{
					return;
				}
				action(item, npc);
			}

			// Token: 0x060054F1 RID: 21745 RVA: 0x0069B01C File Offset: 0x0069921C
			public bool ConditionsMet()
			{
				for (int i = 0; i < this.conditions.Count; i++)
				{
					if (!this.conditions[i].IsMet())
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x04006B4C RID: 27468
			private readonly List<Condition> conditions;

			// Token: 0x04006B4D RID: 27469
			private Action<Item, NPC> shopOpenedHooks;
		}
	}
}
