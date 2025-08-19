using System;
using System.IO;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x02000212 RID: 530
	public class TEFoodPlatter : TileEntity, IFixLoadedData
	{
		// Token: 0x06001DF7 RID: 7671 RVA: 0x005075CB File Offset: 0x005057CB
		public override void RegisterTileEntityID(int assignedID)
		{
			TEFoodPlatter._myEntityID = (byte)assignedID;
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x005075D4 File Offset: 0x005057D4
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			TEFoodPlatter.NetPlaceEntity(x, y);
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x005075E0 File Offset: 0x005057E0
		public static void NetPlaceEntity(int x, int y)
		{
			int number = TEFoodPlatter.Place(x, y);
			NetMessage.SendData(86, -1, -1, null, number, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x0050760C File Offset: 0x0050580C
		public override TileEntity GenerateInstance()
		{
			return new TEFoodPlatter();
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x00507613 File Offset: 0x00505813
		public TEFoodPlatter()
		{
			this.item = new Item();
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x00507628 File Offset: 0x00505828
		public static int Place(int x, int y)
		{
			TEFoodPlatter tefoodPlatter = new TEFoodPlatter();
			tefoodPlatter.Position = new Point16(x, y);
			tefoodPlatter.ID = TileEntity.AssignNewID();
			tefoodPlatter.type = TEFoodPlatter._myEntityID;
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByID[tefoodPlatter.ID] = tefoodPlatter;
				TileEntity.ByPosition[tefoodPlatter.Position] = tefoodPlatter;
			}
			return tefoodPlatter.ID;
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x005076B4 File Offset: 0x005058B4
		public override bool IsTileValidForEntity(int x, int y)
		{
			return TEFoodPlatter.ValidTile(x, y);
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x005076C0 File Offset: 0x005058C0
		public static int Hook_AfterPlacement(int x, int y, int type = 520, int style = 0, int direction = 1, int alternate = 0)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, x, y, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, x, (float)y, (float)TEFoodPlatter._myEntityID, 0f, 0, 0, 0);
				return -1;
			}
			return TEFoodPlatter.Place(x, y);
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x00507708 File Offset: 0x00505908
		public static void Kill(int x, int y)
		{
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && tileEntity.type == TEFoodPlatter._myEntityID)
			{
				object entityCreationLock = TileEntity.EntityCreationLock;
				lock (entityCreationLock)
				{
					TileEntity.ByID.Remove(tileEntity.ID);
					TileEntity.ByPosition.Remove(new Point16(x, y));
				}
			}
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x00507788 File Offset: 0x00505988
		public static int Find(int x, int y)
		{
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && tileEntity.type == TEFoodPlatter._myEntityID)
			{
				return tileEntity.ID;
			}
			return -1;
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x005077C0 File Offset: 0x005059C0
		public static bool ValidTile(int x, int y)
		{
			return Main.tile[x, y].active() && Main.tile[x, y].type == 520 && Main.tile[x, y].frameY == 0;
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x0050780E File Offset: 0x00505A0E
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			writer.Write((short)this.item.netID);
			writer.Write(this.item.prefix);
			writer.Write((short)this.item.stack);
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x00507848 File Offset: 0x00505A48
		public override void ReadExtraData(BinaryReader reader, bool networkSend)
		{
			this.item = new Item();
			this.item.netDefaults((int)reader.ReadInt16());
			this.item.Prefix((int)reader.ReadByte());
			this.item.stack = (int)reader.ReadInt16();
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x00507894 File Offset: 0x00505A94
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Position.X,
				"x  ",
				this.Position.Y,
				"y item: ",
				this.item
			});
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x005078EC File Offset: 0x00505AEC
		public void DropItem()
		{
			if (Main.netMode != 1)
			{
				Item.NewItem(new EntitySource_TileBreak((int)this.Position.X, (int)this.Position.Y), (int)(this.Position.X * 16), (int)(this.Position.Y * 16), 16, 16, this.item.netID, 1, false, (int)this.item.prefix, false, false);
			}
			this.item = new Item();
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x00507968 File Offset: 0x00505B68
		public static void TryPlacing(int x, int y, int netid, int prefix, int stack)
		{
			WorldGen.RangeFrame(x, y, x + 1, y + 1);
			int num = TEFoodPlatter.Find(x, y);
			if (num == -1)
			{
				int num2 = Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 16, 16, 1, 1, false, 0, false, false);
				Main.item[num2].netDefaults(netid);
				Main.item[num2].Prefix(prefix);
				Main.item[num2].stack = stack;
				NetMessage.SendData(21, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			TEFoodPlatter tefoodPlatter = (TEFoodPlatter)TileEntity.ByID[num];
			if (tefoodPlatter.item.stack > 0)
			{
				tefoodPlatter.DropItem();
			}
			tefoodPlatter.item = new Item();
			tefoodPlatter.item.netDefaults(netid);
			tefoodPlatter.item.Prefix(prefix);
			tefoodPlatter.item.stack = stack;
			NetMessage.SendData(86, -1, -1, null, tefoodPlatter.ID, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x00507A68 File Offset: 0x00505C68
		public static void OnPlayerInteraction(Player player, int clickX, int clickY)
		{
			if (TEFoodPlatter.FitsFoodPlatter(player.inventory[player.selectedItem]) && !player.inventory[player.selectedItem].favorited)
			{
				player.GamepadEnableGrappleCooldown();
				TEFoodPlatter.PlaceItemInFrame(player, clickX, clickY);
				Recipe.FindRecipes(false);
				return;
			}
			int num = TEFoodPlatter.Find(clickX, clickY);
			if (num != -1 && ((TEFoodPlatter)TileEntity.ByID[num]).item.stack > 0)
			{
				player.GamepadEnableGrappleCooldown();
				WorldGen.KillTile(clickX, clickY, true, false, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(17, -1, -1, null, 0, (float)clickX, (float)clickY, 1f, 0, 0, 0);
				}
			}
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x00507B0E File Offset: 0x00505D0E
		public static bool FitsFoodPlatter(Item i)
		{
			return i.stack > 0 && ItemID.Sets.IsFood[i.type];
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x00507B28 File Offset: 0x00505D28
		public static void PlaceItemInFrame(Player player, int x, int y)
		{
			if (!player.ItemTimeIsZero)
			{
				return;
			}
			int num = TEFoodPlatter.Find(x, y);
			if (num == -1)
			{
				return;
			}
			if (((TEFoodPlatter)TileEntity.ByID[num]).item.stack > 0)
			{
				WorldGen.KillTile(x, y, true, false, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(17, -1, -1, null, 0, (float)Player.tileTargetX, (float)y, 1f, 0, 0, 0);
				}
			}
			if (Main.netMode == 1)
			{
				NetMessage.SendData(133, -1, -1, null, x, (float)y, (float)player.selectedItem, (float)player.whoAmI, 1, 0, 0);
			}
			else
			{
				TEFoodPlatter.TryPlacing(x, y, player.inventory[player.selectedItem].netID, (int)player.inventory[player.selectedItem].prefix, 1);
			}
			player.inventory[player.selectedItem].stack--;
			if (player.inventory[player.selectedItem].stack <= 0)
			{
				player.inventory[player.selectedItem].SetDefaults(0);
				Main.mouseItem.SetDefaults(0);
			}
			if (player.selectedItem == 58)
			{
				Main.mouseItem = player.inventory[player.selectedItem].Clone();
			}
			player.releaseUseItem = false;
			player.mouseInterface = true;
			WorldGen.RangeFrame(x, y, x + 1, y + 1);
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x00507C75 File Offset: 0x00505E75
		public void FixLoadedData()
		{
			this.item.FixAgainstExploit();
		}

		// Token: 0x0400458A RID: 17802
		private static byte _myEntityID;

		// Token: 0x0400458B RID: 17803
		public Item item;
	}
}
