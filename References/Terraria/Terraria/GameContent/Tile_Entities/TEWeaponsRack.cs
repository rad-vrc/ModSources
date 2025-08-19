using System;
using System.IO;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x02000218 RID: 536
	public class TEWeaponsRack : TileEntity, IFixLoadedData
	{
		// Token: 0x06001E5B RID: 7771 RVA: 0x0050A243 File Offset: 0x00508443
		public TEWeaponsRack()
		{
			this.item = new Item();
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x0050A256 File Offset: 0x00508456
		public override void RegisterTileEntityID(int assignedID)
		{
			TEWeaponsRack._myEntityID = (byte)assignedID;
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x0050A25F File Offset: 0x0050845F
		public override TileEntity GenerateInstance()
		{
			return new TEWeaponsRack();
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x0050A266 File Offset: 0x00508466
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			TEWeaponsRack.NetPlaceEntity(x, y);
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x0050A270 File Offset: 0x00508470
		public static void NetPlaceEntity(int x, int y)
		{
			int number = TEWeaponsRack.Place(x, y);
			NetMessage.SendData(86, -1, -1, null, number, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x0050A29C File Offset: 0x0050849C
		public override bool IsTileValidForEntity(int x, int y)
		{
			return TEWeaponsRack.ValidTile(x, y);
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x0050A2A8 File Offset: 0x005084A8
		public static bool ValidTile(int x, int y)
		{
			return Main.tile[x, y].active() && Main.tile[x, y].type == 471 && Main.tile[x, y].frameY == 0 && Main.tile[x, y].frameX % 54 == 0;
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x0050A30C File Offset: 0x0050850C
		public static int Place(int x, int y)
		{
			TEWeaponsRack teweaponsRack = new TEWeaponsRack();
			teweaponsRack.Position = new Point16(x, y);
			teweaponsRack.ID = TileEntity.AssignNewID();
			teweaponsRack.type = TEWeaponsRack._myEntityID;
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByID[teweaponsRack.ID] = teweaponsRack;
				TileEntity.ByPosition[teweaponsRack.Position] = teweaponsRack;
			}
			return teweaponsRack.ID;
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x0050A398 File Offset: 0x00508598
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

		// Token: 0x06001E64 RID: 7780 RVA: 0x0050A3E0 File Offset: 0x005085E0
		public static void Kill(int x, int y)
		{
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && tileEntity.type == TEWeaponsRack._myEntityID)
			{
				object entityCreationLock = TileEntity.EntityCreationLock;
				lock (entityCreationLock)
				{
					TileEntity.ByID.Remove(tileEntity.ID);
					TileEntity.ByPosition.Remove(new Point16(x, y));
				}
			}
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x0050A460 File Offset: 0x00508660
		public static int Find(int x, int y)
		{
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && tileEntity.type == TEWeaponsRack._myEntityID)
			{
				return tileEntity.ID;
			}
			return -1;
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x0050A497 File Offset: 0x00508697
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			writer.Write((short)this.item.netID);
			writer.Write(this.item.prefix);
			writer.Write((short)this.item.stack);
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x0050A4D0 File Offset: 0x005086D0
		public override void ReadExtraData(BinaryReader reader, bool networkSend)
		{
			this.item = new Item();
			this.item.netDefaults((int)reader.ReadInt16());
			this.item.Prefix((int)reader.ReadByte());
			this.item.stack = (int)reader.ReadInt16();
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x0050A51C File Offset: 0x0050871C
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

		// Token: 0x06001E69 RID: 7785 RVA: 0x0050A574 File Offset: 0x00508774
		public static void Framing_CheckTile(int callX, int callY)
		{
			int num = 3;
			int num2 = 3;
			if (WorldGen.destroyObject)
			{
				return;
			}
			Tile tileSafely = Framing.GetTileSafely(callX, callY);
			int num3 = callX - (int)(tileSafely.frameX / 18) % num;
			int num4 = callY - (int)(tileSafely.frameY / 18) % num2;
			bool flag = false;
			for (int i = num3; i < num3 + num; i++)
			{
				for (int j = num4; j < num4 + num2; j++)
				{
					Tile tile = Main.tile[i, j];
					if (!tile.active() || tile.type != 471 || tile.wall == 0)
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
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
				for (int k = num3; k < num3 + num; k++)
				{
					for (int l = num4; l < num4 + num2; l++)
					{
						if (Main.tile[k, l].active() && Main.tile[k, l].type == 471)
						{
							WorldGen.KillTile(k, l, false, false, false);
						}
					}
				}
				Item.NewItem(new EntitySource_TileBreak(num3, num4), num3 * 16, num4 * 16, 48, 48, 2699, 1, false, 0, false, false);
				TEWeaponsRack.Kill(num3, num4);
				WorldGen.destroyObject = false;
			}
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x0050A708 File Offset: 0x00508908
		public void DropItem()
		{
			if (Main.netMode != 1)
			{
				Item.NewItem(new EntitySource_TileBreak((int)this.Position.X, (int)this.Position.Y), (int)(this.Position.X * 16), (int)(this.Position.Y * 16), 32, 32, this.item.netID, 1, false, (int)this.item.prefix, false, false);
			}
			this.item = new Item();
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x0050A784 File Offset: 0x00508984
		public static void TryPlacing(int x, int y, int netid, int prefix, int stack)
		{
			WorldGen.RangeFrame(x, y, x + 3, y + 3);
			int num = TEWeaponsRack.Find(x, y);
			if (num == -1)
			{
				int num2 = Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 32, 32, 1, 1, false, 0, false, false);
				Main.item[num2].netDefaults(netid);
				Main.item[num2].Prefix(prefix);
				Main.item[num2].stack = stack;
				NetMessage.SendData(21, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			TEWeaponsRack teweaponsRack = (TEWeaponsRack)TileEntity.ByID[num];
			if (teweaponsRack.item.stack > 0)
			{
				teweaponsRack.DropItem();
			}
			teweaponsRack.item = new Item();
			teweaponsRack.item.netDefaults(netid);
			teweaponsRack.item.Prefix(prefix);
			teweaponsRack.item.stack = stack;
			NetMessage.SendData(86, -1, -1, null, teweaponsRack.ID, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x0050A884 File Offset: 0x00508A84
		public static void OnPlayerInteraction(Player player, int clickX, int clickY)
		{
			if (TEWeaponsRack.FitsWeaponFrame(player.inventory[player.selectedItem]) && !player.inventory[player.selectedItem].favorited)
			{
				player.GamepadEnableGrappleCooldown();
				TEWeaponsRack.PlaceItemInFrame(player, clickX, clickY);
				Recipe.FindRecipes(false);
				return;
			}
			int num = clickX - (int)(Main.tile[clickX, clickY].frameX % 54 / 18);
			int num2 = clickY - (int)(Main.tile[num, clickY].frameY % 54 / 18);
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

		// Token: 0x06001E6D RID: 7789 RVA: 0x0050A95E File Offset: 0x00508B5E
		public static bool FitsWeaponFrame(Item i)
		{
			return (!i.IsAir && (i.fishingPole > 0 || ItemID.Sets.CanBePlacedOnWeaponRacks[i.type])) || (i.damage > 0 && i.useStyle != 0 && i.stack > 0);
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x0050A9A0 File Offset: 0x00508BA0
		private static void PlaceItemInFrame(Player player, int x, int y)
		{
			if (!player.ItemTimeIsZero)
			{
				return;
			}
			x -= (int)(Main.tile[x, y].frameX % 54 / 18);
			y -= (int)(Main.tile[x, y].frameY % 54 / 18);
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
			}
			else
			{
				TEWeaponsRack.TryPlacing(x, y, player.inventory[player.selectedItem].netID, (int)player.inventory[player.selectedItem].prefix, 1);
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
			player.PlayDroppedItemAnimation(20);
			WorldGen.RangeFrame(x, y, x + 3, y + 3);
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x0050AB28 File Offset: 0x00508D28
		public void FixLoadedData()
		{
			this.item.FixAgainstExploit();
		}

		// Token: 0x040045AC RID: 17836
		private static byte _myEntityID;

		// Token: 0x040045AD RID: 17837
		public Item item;

		// Token: 0x040045AE RID: 17838
		private const int MyTileID = 471;
	}
}
