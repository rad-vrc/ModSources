using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x0200055F RID: 1375
	public class TEItemFrame : TileEntity, IFixLoadedData
	{
		// Token: 0x060040C5 RID: 16581 RVA: 0x005E2A46 File Offset: 0x005E0C46
		public override void RegisterTileEntityID(int assignedID)
		{
			TEItemFrame._myEntityID = (byte)assignedID;
		}

		// Token: 0x060040C6 RID: 16582 RVA: 0x005E2A4F File Offset: 0x005E0C4F
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			TEItemFrame.NetPlaceEntity(x, y);
		}

		// Token: 0x060040C7 RID: 16583 RVA: 0x005E2A58 File Offset: 0x005E0C58
		public static void NetPlaceEntity(int x, int y)
		{
			int number = TEItemFrame.Place(x, y);
			NetMessage.SendData(86, -1, -1, null, number, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x005E2A84 File Offset: 0x005E0C84
		public override TileEntity GenerateInstance()
		{
			return new TEItemFrame();
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x005E2A8B File Offset: 0x005E0C8B
		public TEItemFrame()
		{
			this.item = new Item();
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x005E2AA0 File Offset: 0x005E0CA0
		public static int Place(int x, int y)
		{
			TEItemFrame tEItemFrame = new TEItemFrame();
			tEItemFrame.Position = new Point16(x, y);
			tEItemFrame.ID = TileEntity.AssignNewID();
			tEItemFrame.type = TEItemFrame._myEntityID;
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByID[tEItemFrame.ID] = tEItemFrame;
				TileEntity.ByPosition[tEItemFrame.Position] = tEItemFrame;
			}
			return tEItemFrame.ID;
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x005E2B2C File Offset: 0x005E0D2C
		public override bool IsTileValidForEntity(int x, int y)
		{
			return TEItemFrame.ValidTile(x, y);
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x005E2B38 File Offset: 0x005E0D38
		public static int Hook_AfterPlacement(int x, int y, int type = 395, int style = 0, int direction = 1, int alternate = 0)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, x, y, 2, 2, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, x, (float)y, (float)TEItemFrame._myEntityID, 0f, 0, 0, 0);
				return -1;
			}
			return TEItemFrame.Place(x, y);
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x005E2B80 File Offset: 0x005E0D80
		public static void Kill(int x, int y)
		{
			TileEntity value;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out value) && value.type == TEItemFrame._myEntityID)
			{
				object entityCreationLock = TileEntity.EntityCreationLock;
				lock (entityCreationLock)
				{
					TileEntity.ByID.Remove(value.ID);
					TileEntity.ByPosition.Remove(new Point16(x, y));
				}
			}
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x005E2C00 File Offset: 0x005E0E00
		public static int Find(int x, int y)
		{
			TileEntity value;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out value) && value.type == TEItemFrame._myEntityID)
			{
				return value.ID;
			}
			return -1;
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x005E2C38 File Offset: 0x005E0E38
		public unsafe static bool ValidTile(int x, int y)
		{
			return Main.tile[x, y].active() && *Main.tile[x, y].type == 395 && *Main.tile[x, y].frameY == 0 && *Main.tile[x, y].frameX % 36 == 0;
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x005E2CAB File Offset: 0x005E0EAB
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			ItemIO.WriteShortVanillaID(this.item, writer);
			ItemIO.WriteByteVanillaPrefix(this.item, writer);
			writer.Write((short)this.item.stack);
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x005E2CD8 File Offset: 0x005E0ED8
		public override void ReadExtraData(BinaryReader reader, bool networkSend)
		{
			this.item = new Item();
			this.item.netDefaults((int)reader.ReadInt16());
			this.item.Prefix((int)reader.ReadByte());
			this.item.stack = (int)reader.ReadInt16();
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x005E2D24 File Offset: 0x005E0F24
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

		// Token: 0x060040D3 RID: 16595 RVA: 0x005E2D88 File Offset: 0x005E0F88
		public void DropItem()
		{
			if (Main.netMode != 1)
			{
				Item.NewItem(new EntitySource_TileBreak((int)this.Position.X, (int)this.Position.Y, null), (int)(this.Position.X * 16), (int)(this.Position.Y * 16), 32, 32, this.item, false, false, false);
			}
			this.item = new Item();
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x005E2DF4 File Offset: 0x005E0FF4
		public static void TryPlacing(int x, int y, Item item, int stack)
		{
			WorldGen.RangeFrame(x, y, x + 2, y + 2);
			int num = TEItemFrame.Find(x, y);
			if (num == -1)
			{
				Item.NewItem(new EntitySource_TileBreak(x, y, null), new Rectangle(x * 16, y * 16, 16, 16), item, false, false, false);
				return;
			}
			TEItemFrame tEItemFrame = (TEItemFrame)TileEntity.ByID[num];
			if (tEItemFrame.item.stack > 0)
			{
				tEItemFrame.DropItem();
			}
			tEItemFrame.item = ItemLoader.TransferWithLimit(item, stack);
			NetMessage.SendData(86, -1, -1, null, tEItemFrame.ID, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x005E2E8C File Offset: 0x005E108C
		public unsafe static void OnPlayerInteraction(Player player, int clickX, int clickY)
		{
			if (TEItemFrame.FitsItemFrame(player.inventory[player.selectedItem]) && !player.inventory[player.selectedItem].favorited)
			{
				player.GamepadEnableGrappleCooldown();
				TEItemFrame.PlaceItemInFrame(player, clickX, clickY);
				Recipe.FindRecipes(false);
				return;
			}
			int num = clickX;
			int num2 = clickY;
			if (*Main.tile[num, num2].frameX % 36 != 0)
			{
				num--;
			}
			if (*Main.tile[num, num2].frameY % 36 != 0)
			{
				num2--;
			}
			int num3 = TEItemFrame.Find(num, num2);
			if (num3 != -1 && ((TEItemFrame)TileEntity.ByID[num3]).item.stack > 0)
			{
				player.GamepadEnableGrappleCooldown();
				WorldGen.KillTile(clickX, clickY, true, false, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(17, -1, -1, null, 0, (float)num, (float)num2, 1f, 0, 0, 0);
				}
			}
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x005E2F6E File Offset: 0x005E116E
		public static bool FitsItemFrame(Item i)
		{
			return i.stack > 0;
		}

		// Token: 0x060040D7 RID: 16599 RVA: 0x005E2F7C File Offset: 0x005E117C
		public unsafe static void PlaceItemInFrame(Player player, int x, int y)
		{
			if (!player.ItemTimeIsZero)
			{
				return;
			}
			if (*Main.tile[x, y].frameX % 36 != 0)
			{
				x--;
			}
			if (*Main.tile[x, y].frameY % 36 != 0)
			{
				y--;
			}
			int num = TEItemFrame.Find(x, y);
			if (num == -1)
			{
				return;
			}
			if (((TEItemFrame)TileEntity.ByID[num]).item.stack > 0)
			{
				WorldGen.KillTile(x, y, true, false, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(17, -1, -1, null, 0, (float)Player.tileTargetX, (float)y, 1f, 0, 0, 0);
				}
			}
			if (Main.netMode == 1)
			{
				NetMessage.SendData(89, -1, -1, null, x, (float)y, (float)player.selectedItem, (float)player.whoAmI, 1, 0, 0);
				ItemLoader.TransferWithLimit(player.inventory[player.selectedItem], 1);
			}
			else
			{
				TEItemFrame.TryPlacing(x, y, player.inventory[player.selectedItem], 1);
			}
			if (player.selectedItem == 58)
			{
				Main.mouseItem = player.inventory[player.selectedItem].Clone();
			}
			player.releaseUseItem = false;
			player.mouseInterface = true;
			player.PlayDroppedItemAnimation(20);
			WorldGen.RangeFrame(x, y, x + 2, y + 2);
		}

		// Token: 0x060040D8 RID: 16600 RVA: 0x005E30BC File Offset: 0x005E12BC
		public void FixLoadedData()
		{
			this.item.FixAgainstExploit();
		}

		// Token: 0x060040D9 RID: 16601 RVA: 0x005E30C9 File Offset: 0x005E12C9
		public override void SaveData(TagCompound tag)
		{
			tag["item"] = ItemIO.Save(this.item);
		}

		// Token: 0x060040DA RID: 16602 RVA: 0x005E30E1 File Offset: 0x005E12E1
		public override void LoadData(TagCompound tag)
		{
			this.item = ItemIO.Load(tag.GetCompound("item"));
		}

		// Token: 0x060040DB RID: 16603 RVA: 0x005E30F9 File Offset: 0x005E12F9
		public override void NetSend(BinaryWriter writer)
		{
			ItemIO.Send(this.item, writer, true, false);
		}

		// Token: 0x060040DC RID: 16604 RVA: 0x005E3109 File Offset: 0x005E1309
		public override void NetReceive(BinaryReader reader)
		{
			this.item = ItemIO.Receive(reader, true, false);
		}

		// Token: 0x04005891 RID: 22673
		private static byte _myEntityID;

		// Token: 0x04005892 RID: 22674
		public Item item;
	}
}
