using System;
using System.IO;
using Terraria.DataStructures;

namespace Terraria
{
	// Token: 0x0200003F RID: 63
	public class EquipmentLoadout : IFixLoadedData
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x002C0649 File Offset: 0x002BE849
		public EquipmentLoadout()
		{
			this.Armor = this.CreateItemArray(20);
			this.Dye = this.CreateItemArray(10);
			this.Hide = new bool[10];
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x002C067C File Offset: 0x002BE87C
		private Item[] CreateItemArray(int length)
		{
			Item[] array = new Item[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = new Item();
			}
			return array;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x002C06A8 File Offset: 0x002BE8A8
		public void Serialize(BinaryWriter writer)
		{
			ItemSerializationContext context = ItemSerializationContext.SavingAndLoading;
			for (int i = 0; i < this.Armor.Length; i++)
			{
				this.Armor[i].Serialize(writer, context);
			}
			for (int j = 0; j < this.Dye.Length; j++)
			{
				this.Dye[j].Serialize(writer, context);
			}
			for (int k = 0; k < this.Hide.Length; k++)
			{
				writer.Write(this.Hide[k]);
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x002C071C File Offset: 0x002BE91C
		public void Deserialize(BinaryReader reader, int gameVersion)
		{
			ItemSerializationContext context = ItemSerializationContext.SavingAndLoading;
			for (int i = 0; i < this.Armor.Length; i++)
			{
				this.Armor[i].DeserializeFrom(reader, context);
			}
			for (int j = 0; j < this.Dye.Length; j++)
			{
				this.Dye[j].DeserializeFrom(reader, context);
			}
			for (int k = 0; k < this.Hide.Length; k++)
			{
				this.Hide[k] = reader.ReadBoolean();
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x002C0790 File Offset: 0x002BE990
		public void Swap(Player player)
		{
			Item[] armor = player.armor;
			for (int i = 0; i < armor.Length; i++)
			{
				Utils.Swap<Item>(ref armor[i], ref this.Armor[i]);
			}
			Item[] dye = player.dye;
			for (int j = 0; j < dye.Length; j++)
			{
				Utils.Swap<Item>(ref dye[j], ref this.Dye[j]);
			}
			bool[] hideVisibleAccessory = player.hideVisibleAccessory;
			for (int k = 0; k < hideVisibleAccessory.Length; k++)
			{
				Utils.Swap<bool>(ref hideVisibleAccessory[k], ref this.Hide[k]);
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x002C0830 File Offset: 0x002BEA30
		public void TryDroppingItems(Player player, IEntitySource source)
		{
			for (int i = 0; i < this.Armor.Length; i++)
			{
				player.TryDroppingSingleItem(source, this.Armor[i]);
			}
			for (int j = 0; j < this.Dye.Length; j++)
			{
				player.TryDroppingSingleItem(source, this.Dye[j]);
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x002C0884 File Offset: 0x002BEA84
		public void FixLoadedData()
		{
			for (int i = 0; i < this.Armor.Length; i++)
			{
				this.Armor[i].FixAgainstExploit();
			}
			for (int j = 0; j < this.Dye.Length; j++)
			{
				this.Dye[j].FixAgainstExploit();
			}
			Player.FixLoadedData_EliminiateDuplicateAccessories(this.Armor);
		}

		// Token: 0x0400049B RID: 1179
		public Item[] Armor;

		// Token: 0x0400049C RID: 1180
		public Item[] Dye;

		// Token: 0x0400049D RID: 1181
		public bool[] Hide;
	}
}
