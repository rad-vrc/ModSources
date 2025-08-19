using System;
using System.IO;
using Terraria.DataStructures;

namespace Terraria
{
	// Token: 0x0200002C RID: 44
	public class EquipmentLoadout : IFixLoadedData
	{
		// Token: 0x060001FB RID: 507 RVA: 0x00021A5E File Offset: 0x0001FC5E
		public EquipmentLoadout()
		{
			this.Armor = this.CreateItemArray(20);
			this.Dye = this.CreateItemArray(10);
			this.Hide = new bool[10];
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00021A90 File Offset: 0x0001FC90
		private Item[] CreateItemArray(int length)
		{
			Item[] array = new Item[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = new Item();
			}
			return array;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00021ABC File Offset: 0x0001FCBC
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

		// Token: 0x060001FE RID: 510 RVA: 0x00021B30 File Offset: 0x0001FD30
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

		// Token: 0x060001FF RID: 511 RVA: 0x00021BA4 File Offset: 0x0001FDA4
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

		// Token: 0x06000200 RID: 512 RVA: 0x00021C44 File Offset: 0x0001FE44
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

		// Token: 0x06000201 RID: 513 RVA: 0x00021C98 File Offset: 0x0001FE98
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

		// Token: 0x040001C9 RID: 457
		public Item[] Armor;

		// Token: 0x040001CA RID: 458
		public Item[] Dye;

		// Token: 0x040001CB RID: 459
		public bool[] Hide;
	}
}
