using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x0200055D RID: 1373
	public sealed class TEFoodPlatter : TileEntity, IFixLoadedData
	{
		// Token: 0x0600408B RID: 16523 RVA: 0x005E12FF File Offset: 0x005DF4FF
		public override void RegisterTileEntityID(int assignedID)
		{
			TEFoodPlatter._myEntityID = (byte)assignedID;
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x005E1308 File Offset: 0x005DF508
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			TEFoodPlatter.NetPlaceEntity(x, y);
		}

		// Token: 0x0600408D RID: 16525 RVA: 0x005E1314 File Offset: 0x005DF514
		public static void NetPlaceEntity(int x, int y)
		{
			int number = TEFoodPlatter.Place(x, y);
			NetMessage.SendData(86, -1, -1, null, number, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x0600408E RID: 16526 RVA: 0x005E1340 File Offset: 0x005DF540
		public override TileEntity GenerateInstance()
		{
			return new TEFoodPlatter();
		}

		// Token: 0x0600408F RID: 16527 RVA: 0x005E1347 File Offset: 0x005DF547
		public TEFoodPlatter()
		{
			this.item = new Item();
		}

		// Token: 0x06004090 RID: 16528 RVA: 0x005E135C File Offset: 0x005DF55C
		public static int Place(int x, int y)
		{
			TEFoodPlatter tEFoodPlatter = new TEFoodPlatter();
			tEFoodPlatter.Position = new Point16(x, y);
			tEFoodPlatter.ID = TileEntity.AssignNewID();
			tEFoodPlatter.type = TEFoodPlatter._myEntityID;
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByID[tEFoodPlatter.ID] = tEFoodPlatter;
				TileEntity.ByPosition[tEFoodPlatter.Position] = tEFoodPlatter;
			}
			return tEFoodPlatter.ID;
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x005E13E8 File Offset: 0x005DF5E8
		public override bool IsTileValidForEntity(int x, int y)
		{
			return TEFoodPlatter.ValidTile(x, y);
		}

		// Token: 0x06004092 RID: 16530 RVA: 0x005E13F4 File Offset: 0x005DF5F4
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

		// Token: 0x06004093 RID: 16531 RVA: 0x005E143C File Offset: 0x005DF63C
		public static void Kill(int x, int y)
		{
			TileEntity value;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out value) && value.type == TEFoodPlatter._myEntityID)
			{
				object entityCreationLock = TileEntity.EntityCreationLock;
				lock (entityCreationLock)
				{
					TileEntity.ByID.Remove(value.ID);
					TileEntity.ByPosition.Remove(new Point16(x, y));
				}
			}
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x005E14BC File Offset: 0x005DF6BC
		public static int Find(int x, int y)
		{
			TileEntity value;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out value) && value.type == TEFoodPlatter._myEntityID)
			{
				return value.ID;
			}
			return -1;
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x005E14F4 File Offset: 0x005DF6F4
		public unsafe static bool ValidTile(int x, int y)
		{
			return Main.tile[x, y].active() && *Main.tile[x, y].type == 520 && *Main.tile[x, y].frameY == 0;
		}

		// Token: 0x06004096 RID: 16534 RVA: 0x005E154D File Offset: 0x005DF74D
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			ItemIO.WriteShortVanillaID(this.item, writer);
			ItemIO.WriteByteVanillaPrefix(this.item, writer);
			writer.Write((short)this.item.stack);
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x005E157C File Offset: 0x005DF77C
		public override void ReadExtraData(BinaryReader reader, bool networkSend)
		{
			this.item = new Item();
			this.item.netDefaults((int)reader.ReadInt16());
			this.item.Prefix((int)reader.ReadByte());
			this.item.stack = (int)reader.ReadInt16();
		}

		// Token: 0x06004098 RID: 16536 RVA: 0x005E15C8 File Offset: 0x005DF7C8
		public override string ToString()
		{
			string[] array = new string[5];
			array[0] = this.Position.X.ToString();
			array[1] = "x  ";
			array[2] = this.Position.Y.ToString();
			array[3] = "y item: ";
			int num = 4;
			Item item = this.item;
			array[num] = ((item != null) ? item.ToString() : null);
			return string.Concat(array);
		}

		// Token: 0x06004099 RID: 16537 RVA: 0x005E162C File Offset: 0x005DF82C
		public void DropItem()
		{
			if (Main.netMode != 1)
			{
				Item.NewItem(new EntitySource_TileBreak((int)this.Position.X, (int)this.Position.Y, null), (int)(this.Position.X * 16), (int)(this.Position.Y * 16), 16, 16, this.item, false, false, false);
			}
			this.item = new Item();
		}

		// Token: 0x0600409A RID: 16538 RVA: 0x005E1698 File Offset: 0x005DF898
		public static void TryPlacing(int x, int y, Item item, int stack)
		{
			WorldGen.RangeFrame(x, y, x + 1, y + 1);
			int num = TEFoodPlatter.Find(x, y);
			if (num == -1)
			{
				Item.NewItem(new EntitySource_TileBreak(x, y, null), new Rectangle(x * 16, y * 16, 16, 16), item, false, false, false);
				return;
			}
			TEFoodPlatter tEFoodPlatter = (TEFoodPlatter)TileEntity.ByID[num];
			if (tEFoodPlatter.item.stack > 0)
			{
				tEFoodPlatter.DropItem();
			}
			tEFoodPlatter.item = ItemLoader.TransferWithLimit(item, stack);
			NetMessage.SendData(86, -1, -1, null, tEFoodPlatter.ID, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x005E1730 File Offset: 0x005DF930
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

		// Token: 0x0600409C RID: 16540 RVA: 0x005E17D2 File Offset: 0x005DF9D2
		public static bool FitsFoodPlatter(Item i)
		{
			return i.stack > 0 && ItemID.Sets.IsFood[i.type];
		}

		// Token: 0x0600409D RID: 16541 RVA: 0x005E17EC File Offset: 0x005DF9EC
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
				ItemLoader.TransferWithLimit(player.inventory[player.selectedItem], 1);
			}
			else
			{
				TEFoodPlatter.TryPlacing(x, y, player.inventory[player.selectedItem], 1);
			}
			if (player.selectedItem == 58)
			{
				Main.mouseItem = player.inventory[player.selectedItem].Clone();
			}
			player.releaseUseItem = false;
			player.mouseInterface = true;
			WorldGen.RangeFrame(x, y, x + 1, y + 1);
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x005E18E9 File Offset: 0x005DFAE9
		public void FixLoadedData()
		{
			this.item.FixAgainstExploit();
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x005E18F6 File Offset: 0x005DFAF6
		public override void SaveData(TagCompound tag)
		{
			tag["item"] = ItemIO.Save(this.item);
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x005E190E File Offset: 0x005DFB0E
		public override void LoadData(TagCompound tag)
		{
			this.item = ItemIO.Load(tag.GetCompound("item"));
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x005E1926 File Offset: 0x005DFB26
		public override void NetSend(BinaryWriter writer)
		{
			ItemIO.Send(this.item, writer, true, false);
		}

		// Token: 0x060040A2 RID: 16546 RVA: 0x005E1936 File Offset: 0x005DFB36
		public override void NetReceive(BinaryReader reader)
		{
			this.item = ItemIO.Receive(reader, true, false);
		}

		// Token: 0x04005887 RID: 22663
		private static byte _myEntityID;

		// Token: 0x04005888 RID: 22664
		public Item item;
	}
}
