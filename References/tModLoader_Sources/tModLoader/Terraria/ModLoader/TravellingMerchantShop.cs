using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Terraria.ID;

namespace Terraria.ModLoader
{
	// Token: 0x020001E4 RID: 484
	public class TravellingMerchantShop : AbstractNPCShop
	{
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x004F6823 File Offset: 0x004F4A23
		public override IEnumerable<AbstractNPCShop.Entry> ActiveEntries
		{
			get
			{
				return this._entries;
			}
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x004F682B File Offset: 0x004F4A2B
		public TravellingMerchantShop(int npcType) : base(npcType, "Shop")
		{
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x004F6844 File Offset: 0x004F4A44
		public TravellingMerchantShop AddInfoEntry(Item item, params Condition[] conditions)
		{
			this._entries.Add(new TravellingMerchantShop.Entry(item, conditions.ToList<Condition>()));
			return this;
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x004F685E File Offset: 0x004F4A5E
		public TravellingMerchantShop AddInfoEntry(int item, params Condition[] conditions)
		{
			return this.AddInfoEntry(ContentSamples.ItemsByType[item], conditions);
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x004F6874 File Offset: 0x004F4A74
		public override void FillShop(ICollection<Item> items, NPC npc)
		{
			foreach (int itemId in Main.travelShop)
			{
				if (itemId != 0)
				{
					items.Add(new Item(itemId, 1, 0));
				}
			}
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x004F68AC File Offset: 0x004F4AAC
		public override void FillShop(Item[] items, NPC npc, out bool overflow)
		{
			overflow = false;
			int i = 0;
			foreach (int itemId in Main.travelShop)
			{
				if (itemId != 0)
				{
					items[i++] = new Item(itemId, 1, 0);
				}
			}
		}

		// Token: 0x040017C7 RID: 6087
		private List<TravellingMerchantShop.Entry> _entries = new List<TravellingMerchantShop.Entry>();

		// Token: 0x02000985 RID: 2437
		private new class Entry : AbstractNPCShop.Entry, IEquatable<TravellingMerchantShop.Entry>
		{
			// Token: 0x06005529 RID: 21801 RVA: 0x0069BC1B File Offset: 0x00699E1B
			public Entry(Item Item, IEnumerable<Condition> Conditions)
			{
				this.Item = Item;
				this.Conditions = Conditions;
				base..ctor();
			}

			// Token: 0x170008EC RID: 2284
			// (get) Token: 0x0600552A RID: 21802 RVA: 0x0069BC31 File Offset: 0x00699E31
			[Nullable(1)]
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[NullableContext(1)]
				[CompilerGenerated]
				get
				{
					return typeof(TravellingMerchantShop.Entry);
				}
			}

			// Token: 0x170008ED RID: 2285
			// (get) Token: 0x0600552B RID: 21803 RVA: 0x0069BC3D File Offset: 0x00699E3D
			// (set) Token: 0x0600552C RID: 21804 RVA: 0x0069BC45 File Offset: 0x00699E45
			public Item Item { get; set; }

			// Token: 0x170008EE RID: 2286
			// (get) Token: 0x0600552D RID: 21805 RVA: 0x0069BC4E File Offset: 0x00699E4E
			// (set) Token: 0x0600552E RID: 21806 RVA: 0x0069BC56 File Offset: 0x00699E56
			public IEnumerable<Condition> Conditions { get; set; }

			// Token: 0x0600552F RID: 21807 RVA: 0x0069BC60 File Offset: 0x00699E60
			[NullableContext(1)]
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Entry");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x06005530 RID: 21808 RVA: 0x0069BCAC File Offset: 0x00699EAC
			[NullableContext(1)]
			[CompilerGenerated]
			protected virtual bool PrintMembers(StringBuilder builder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				builder.Append("Item = ");
				builder.Append(this.Item);
				builder.Append(", Conditions = ");
				builder.Append(this.Conditions);
				return true;
			}

			// Token: 0x06005531 RID: 21809 RVA: 0x0069BCE6 File Offset: 0x00699EE6
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator !=(TravellingMerchantShop.Entry left, TravellingMerchantShop.Entry right)
			{
				return !(left == right);
			}

			// Token: 0x06005532 RID: 21810 RVA: 0x0069BCF2 File Offset: 0x00699EF2
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator ==(TravellingMerchantShop.Entry left, TravellingMerchantShop.Entry right)
			{
				return left == right || (left != null && left.Equals(right));
			}

			// Token: 0x06005533 RID: 21811 RVA: 0x0069BD06 File Offset: 0x00699F06
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Item>.Default.GetHashCode(this.<Item>k__BackingField)) * -1521134295 + EqualityComparer<IEnumerable<Condition>>.Default.GetHashCode(this.<Conditions>k__BackingField);
			}

			// Token: 0x06005534 RID: 21812 RVA: 0x0069BD46 File Offset: 0x00699F46
			[NullableContext(2)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as TravellingMerchantShop.Entry);
			}

			// Token: 0x06005535 RID: 21813 RVA: 0x0069BD54 File Offset: 0x00699F54
			[NullableContext(2)]
			[CompilerGenerated]
			public virtual bool Equals(TravellingMerchantShop.Entry other)
			{
				return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Item>.Default.Equals(this.<Item>k__BackingField, other.<Item>k__BackingField) && EqualityComparer<IEnumerable<Condition>>.Default.Equals(this.<Conditions>k__BackingField, other.<Conditions>k__BackingField));
			}

			// Token: 0x06005537 RID: 21815 RVA: 0x0069BDB5 File Offset: 0x00699FB5
			[CompilerGenerated]
			protected Entry([Nullable(1)] TravellingMerchantShop.Entry original)
			{
				this.Item = original.<Item>k__BackingField;
				this.Conditions = original.<Conditions>k__BackingField;
			}

			// Token: 0x06005538 RID: 21816 RVA: 0x0069BDD5 File Offset: 0x00699FD5
			[CompilerGenerated]
			public void Deconstruct(out Item Item, out IEnumerable<Condition> Conditions)
			{
				Item = this.Item;
				Conditions = this.Conditions;
			}
		}
	}
}
