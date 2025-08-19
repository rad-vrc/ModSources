using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x02000563 RID: 1379
	public sealed class TEWeaponsRack : TileEntity, IFixLoadedData
	{
		// Token: 0x0600411C RID: 16668 RVA: 0x005E4888 File Offset: 0x005E2A88
		public TEWeaponsRack()
		{
			this.item = new Item();
		}

		// Token: 0x0600411D RID: 16669 RVA: 0x005E489B File Offset: 0x005E2A9B
		public override void RegisterTileEntityID(int assignedID)
		{
			TEWeaponsRack._myEntityID = (byte)assignedID;
		}

		// Token: 0x0600411E RID: 16670 RVA: 0x005E48A4 File Offset: 0x005E2AA4
		public override TileEntity GenerateInstance()
		{
			return new TEWeaponsRack();
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x005E48AB File Offset: 0x005E2AAB
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			TEWeaponsRack.NetPlaceEntity(x, y);
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x005E48B4 File Offset: 0x005E2AB4
		public static void NetPlaceEntity(int x, int y)
		{
			int number = TEWeaponsRack.Place(x, y);
			NetMessage.SendData(86, -1, -1, null, number, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x005E48E0 File Offset: 0x005E2AE0
		public override bool IsTileValidForEntity(int x, int y)
		{
			return TEWeaponsRack.ValidTile(x, y);
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x005E48EC File Offset: 0x005E2AEC
		public unsafe static bool ValidTile(int x, int y)
		{
			return Main.tile[x, y].active() && *Main.tile[x, y].type == 471 && *Main.tile[x, y].frameY == 0 && *Main.tile[x, y].frameX % 54 == 0;
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x005E4960 File Offset: 0x005E2B60
		public static int Place(int x, int y)
		{
			TEWeaponsRack tEWeaponsRack = new TEWeaponsRack();
			tEWeaponsRack.Position = new Point16(x, y);
			tEWeaponsRack.ID = TileEntity.AssignNewID();
			tEWeaponsRack.type = TEWeaponsRack._myEntityID;
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByID[tEWeaponsRack.ID] = tEWeaponsRack;
				TileEntity.ByPosition[tEWeaponsRack.Position] = tEWeaponsRack;
			}
			return tEWeaponsRack.ID;
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x005E49EC File Offset: 0x005E2BEC
		public static int Hook_AfterPlacement(int x, int y, int type = 471, int style = 0, int direction = 1, int alternate = 0)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, x, y, 3, 3, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, x, (float)y, (float)TEWeaponsRack._myEntityID, 0f, 0, 0, 0);
				return -1;
			}
			return TEWeaponsRack.Place(x, y);
		}

		// Token: 0x06004125 RID: 16677 RVA: 0x005E4A34 File Offset: 0x005E2C34
		public static void Kill(int x, int y)
		{
			TileEntity value;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out value) && value.type == TEWeaponsRack._myEntityID)
			{
				object entityCreationLock = TileEntity.EntityCreationLock;
				lock (entityCreationLock)
				{
					TileEntity.ByID.Remove(value.ID);
					TileEntity.ByPosition.Remove(new Point16(x, y));
				}
			}
		}

		// Token: 0x06004126 RID: 16678 RVA: 0x005E4AB4 File Offset: 0x005E2CB4
		public static int Find(int x, int y)
		{
			TileEntity value;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out value) && value.type == TEWeaponsRack._myEntityID)
			{
				return value.ID;
			}
			return -1;
		}

		// Token: 0x06004127 RID: 16679 RVA: 0x005E4AEB File Offset: 0x005E2CEB
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			ItemIO.WriteShortVanillaID(this.item, writer);
			ItemIO.WriteByteVanillaPrefix(this.item, writer);
			writer.Write((short)this.item.stack);
		}

		// Token: 0x06004128 RID: 16680 RVA: 0x005E4B18 File Offset: 0x005E2D18
		public override void ReadExtraData(BinaryReader reader, bool networkSend)
		{
			this.item = new Item();
			this.item.netDefaults((int)reader.ReadInt16());
			this.item.Prefix((int)reader.ReadByte());
			this.item.stack = (int)reader.ReadInt16();
		}

		// Token: 0x06004129 RID: 16681 RVA: 0x005E4B64 File Offset: 0x005E2D64
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

		// Token: 0x0600412A RID: 16682 RVA: 0x005E4BC8 File Offset: 0x005E2DC8
		public unsafe static void Framing_CheckTile(int callX, int callY)
		{
			int num = 3;
			int num2 = 3;
			if (WorldGen.destroyObject)
			{
				return;
			}
			Tile tileSafely = Framing.GetTileSafely(callX, callY);
			int num3 = callX - (int)(*tileSafely.frameX / 18) % num;
			int num4 = callY - (int)(*tileSafely.frameY / 18) % num2;
			bool flag = false;
			for (int i = num3; i < num3 + num; i++)
			{
				for (int j = num4; j < num4 + num2; j++)
				{
					Tile tile = Main.tile[i, j];
					if (!tile.active() || *tile.type != 471 || *tile.wall == 0)
					{
						flag = true;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			int num5 = TEWeaponsRack.Find(num3, num4);
			if (num5 != -1 && ((TEWeaponsRack)TileEntity.ByID[num5]).item.stack > 0)
			{
				((TEWeaponsRack)TileEntity.ByID[num5]).DropItem();
				if (Main.netMode != 2)
				{
					Main.LocalPlayer.InterruptItemUsageIfOverTile(471);
				}
			}
			WorldGen.destroyObject = true;
			bool drop = TileLoader.Drop(callX, callY, 471, true);
			for (int k = num3; k < num3 + num; k++)
			{
				for (int l = num4; l < num4 + num2; l++)
				{
					if (Main.tile[k, l].active() && *Main.tile[k, l].type == 471)
					{
						WorldGen.KillTile(k, l, false, false, false);
					}
				}
			}
			if (drop)
			{
				Item.NewItem(new EntitySource_TileBreak(num3, num4, null), num3 * 16, num4 * 16, 48, 48, 2699, 1, false, 0, false, false);
			}
			TEWeaponsRack.Kill(num3, num4);
			WorldGen.destroyObject = false;
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x005E4D7C File Offset: 0x005E2F7C
		public void DropItem()
		{
			if (Main.netMode != 1)
			{
				Item.NewItem(new EntitySource_TileBreak((int)this.Position.X, (int)this.Position.Y, null), (int)(this.Position.X * 16), (int)(this.Position.Y * 16), 32, 32, this.item, false, false, false);
			}
			this.item = new Item();
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x005E4DE8 File Offset: 0x005E2FE8
		public static void TryPlacing(int x, int y, Item item, int stack)
		{
			WorldGen.RangeFrame(x, y, x + 3, y + 3);
			int num = TEWeaponsRack.Find(x, y);
			if (num == -1)
			{
				Item.NewItem(new EntitySource_TileBreak(x, y, null), new Rectangle(x * 16, y * 16, 16, 16), item, false, false, false);
				return;
			}
			TEWeaponsRack tEWeaponsRack = (TEWeaponsRack)TileEntity.ByID[num];
			if (tEWeaponsRack.item.stack > 0)
			{
				tEWeaponsRack.DropItem();
			}
			tEWeaponsRack.item = ItemLoader.TransferWithLimit(item, stack);
			NetMessage.SendData(86, -1, -1, null, tEWeaponsRack.ID, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x0600412D RID: 16685 RVA: 0x005E4E80 File Offset: 0x005E3080
		public unsafe static void OnPlayerInteraction(Player player, int clickX, int clickY)
		{
			if (TEWeaponsRack.FitsWeaponFrame(player.inventory[player.selectedItem]) && !player.inventory[player.selectedItem].favorited)
			{
				player.GamepadEnableGrappleCooldown();
				TEWeaponsRack.PlaceItemInFrame(player, clickX, clickY);
				Recipe.FindRecipes(false);
				return;
			}
			int num = clickX - (int)(*Main.tile[clickX, clickY].frameX % 54 / 18);
			int num2 = clickY - (int)(*Main.tile[num, clickY].frameY % 54 / 18);
			int num3 = TEWeaponsRack.Find(num, num2);
			if (num3 != -1 && ((TEWeaponsRack)TileEntity.ByID[num3]).item.stack > 0)
			{
				player.GamepadEnableGrappleCooldown();
				WorldGen.KillTile(num, num2, true, false, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(17, -1, -1, null, 0, (float)num, (float)num2, 1f, 0, 0, 0);
				}
			}
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x005E4F62 File Offset: 0x005E3162
		public static bool FitsWeaponFrame(Item i)
		{
			return (!i.IsAir && (i.fishingPole > 0 || ItemID.Sets.CanBePlacedOnWeaponRacks[i.type])) || (i.damage > 0 && i.useStyle != 0 && i.stack > 0);
		}

		// Token: 0x0600412F RID: 16687 RVA: 0x005E4FA4 File Offset: 0x005E31A4
		private unsafe static void PlaceItemInFrame(Player player, int x, int y)
		{
			if (!player.ItemTimeIsZero)
			{
				return;
			}
			x -= (int)(*Main.tile[x, y].frameX % 54 / 18);
			y -= (int)(*Main.tile[x, y].frameY % 54 / 18);
			int num = TEWeaponsRack.Find(x, y);
			if (num == -1)
			{
				return;
			}
			if (((TEWeaponsRack)TileEntity.ByID[num]).item.stack > 0)
			{
				WorldGen.KillTile(x, y, true, false, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(17, -1, -1, null, 0, (float)Player.tileTargetX, (float)y, 1f, 0, 0, 0);
				}
			}
			if (Main.netMode == 1)
			{
				NetMessage.SendData(123, -1, -1, null, x, (float)y, (float)player.selectedItem, (float)player.whoAmI, 1, 0, 0);
				ItemLoader.TransferWithLimit(player.inventory[player.selectedItem], 1);
			}
			else
			{
				TEWeaponsRack.TryPlacing(x, y, player.inventory[player.selectedItem], 1);
			}
			if (player.selectedItem == 58)
			{
				Main.mouseItem = player.inventory[player.selectedItem].Clone();
			}
			player.releaseUseItem = false;
			player.mouseInterface = true;
			player.PlayDroppedItemAnimation(20);
			WorldGen.RangeFrame(x, y, x + 3, y + 3);
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x005E50E4 File Offset: 0x005E32E4
		public void FixLoadedData()
		{
			this.item.FixAgainstExploit();
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x005E50F1 File Offset: 0x005E32F1
		public override void SaveData(TagCompound tag)
		{
			tag["item"] = ItemIO.Save(this.item);
		}

		// Token: 0x06004132 RID: 16690 RVA: 0x005E5109 File Offset: 0x005E3309
		public override void LoadData(TagCompound tag)
		{
			this.item = ItemIO.Load(tag.GetCompound("item"));
		}

		// Token: 0x06004133 RID: 16691 RVA: 0x005E5121 File Offset: 0x005E3321
		public override void NetSend(BinaryWriter writer)
		{
			ItemIO.Send(this.item, writer, true, false);
		}

		// Token: 0x06004134 RID: 16692 RVA: 0x005E5131 File Offset: 0x005E3331
		public override void NetReceive(BinaryReader reader)
		{
			this.item = ItemIO.Receive(reader, true, false);
		}

		// Token: 0x040058A4 RID: 22692
		private static byte _myEntityID;

		// Token: 0x040058A5 RID: 22693
		public Item item;

		// Token: 0x040058A6 RID: 22694
		private const int MyTileID = 471;
	}
}
