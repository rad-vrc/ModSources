using System;
using System.IO;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x0200021A RID: 538
	public class TEItemFrame : TileEntity, IFixLoadedData
	{
		// Token: 0x06001E89 RID: 7817 RVA: 0x0050B654 File Offset: 0x00509854
		public override void RegisterTileEntityID(int assignedID)
		{
			TEItemFrame._myEntityID = (byte)assignedID;
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x0050B65D File Offset: 0x0050985D
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			TEItemFrame.NetPlaceEntity(x, y);
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x0050B668 File Offset: 0x00509868
		public static void NetPlaceEntity(int x, int y)
		{
			int number = TEItemFrame.Place(x, y);
			NetMessage.SendData(86, -1, -1, null, number, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x0050B694 File Offset: 0x00509894
		public override TileEntity GenerateInstance()
		{
			return new TEItemFrame();
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x0050B69B File Offset: 0x0050989B
		public TEItemFrame()
		{
			this.item = new Item();
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x0050B6B0 File Offset: 0x005098B0
		public static int Place(int x, int y)
		{
			TEItemFrame teitemFrame = new TEItemFrame();
			teitemFrame.Position = new Point16(x, y);
			teitemFrame.ID = TileEntity.AssignNewID();
			teitemFrame.type = TEItemFrame._myEntityID;
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByID[teitemFrame.ID] = teitemFrame;
				TileEntity.ByPosition[teitemFrame.Position] = teitemFrame;
			}
			return teitemFrame.ID;
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x0050B73C File Offset: 0x0050993C
		public override bool IsTileValidForEntity(int x, int y)
		{
			return TEItemFrame.ValidTile(x, y);
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x0050B748 File Offset: 0x00509948
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

		// Token: 0x06001E91 RID: 7825 RVA: 0x0050B790 File Offset: 0x00509990
		public static void Kill(int x, int y)
		{
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && tileEntity.type == TEItemFrame._myEntityID)
			{
				object entityCreationLock = TileEntity.EntityCreationLock;
				lock (entityCreationLock)
				{
					TileEntity.ByID.Remove(tileEntity.ID);
					TileEntity.ByPosition.Remove(new Point16(x, y));
				}
			}
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x0050B810 File Offset: 0x00509A10
		public static int Find(int x, int y)
		{
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && tileEntity.type == TEItemFrame._myEntityID)
			{
				return tileEntity.ID;
			}
			return -1;
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x0050B848 File Offset: 0x00509A48
		public static bool ValidTile(int x, int y)
		{
			return Main.tile[x, y].active() && Main.tile[x, y].type == 395 && Main.tile[x, y].frameY == 0 && Main.tile[x, y].frameX % 36 == 0;
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x0050B8AC File Offset: 0x00509AAC
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			writer.Write((short)this.item.netID);
			writer.Write(this.item.prefix);
			writer.Write((short)this.item.stack);
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x0050B8E4 File Offset: 0x00509AE4
		public override void ReadExtraData(BinaryReader reader, bool networkSend)
		{
			this.item = new Item();
			this.item.netDefaults((int)reader.ReadInt16());
			this.item.Prefix((int)reader.ReadByte());
			this.item.stack = (int)reader.ReadInt16();
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x0050B930 File Offset: 0x00509B30
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

		// Token: 0x06001E97 RID: 7831 RVA: 0x0050B988 File Offset: 0x00509B88
		public void DropItem()
		{
			if (Main.netMode != 1)
			{
				Item.NewItem(new EntitySource_TileBreak((int)this.Position.X, (int)this.Position.Y), (int)(this.Position.X * 16), (int)(this.Position.Y * 16), 32, 32, this.item.netID, 1, false, (int)this.item.prefix, false, false);
			}
			this.item = new Item();
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x0050BA04 File Offset: 0x00509C04
		public static void TryPlacing(int x, int y, int netid, int prefix, int stack)
		{
			WorldGen.RangeFrame(x, y, x + 2, y + 2);
			int num = TEItemFrame.Find(x, y);
			if (num == -1)
			{
				int num2 = Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 32, 32, 1, 1, false, 0, false, false);
				Main.item[num2].netDefaults(netid);
				Main.item[num2].Prefix(prefix);
				Main.item[num2].stack = stack;
				NetMessage.SendData(21, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			TEItemFrame teitemFrame = (TEItemFrame)TileEntity.ByID[num];
			if (teitemFrame.item.stack > 0)
			{
				teitemFrame.DropItem();
			}
			teitemFrame.item = new Item();
			teitemFrame.item.netDefaults(netid);
			teitemFrame.item.Prefix(prefix);
			teitemFrame.item.stack = stack;
			NetMessage.SendData(86, -1, -1, null, teitemFrame.ID, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x0050BB04 File Offset: 0x00509D04
		public static void OnPlayerInteraction(Player player, int clickX, int clickY)
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
			if (Main.tile[num, num2].frameX % 36 != 0)
			{
				num--;
			}
			if (Main.tile[num, num2].frameY % 36 != 0)
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

		// Token: 0x06001E9A RID: 7834 RVA: 0x0050BBDE File Offset: 0x00509DDE
		public static bool FitsItemFrame(Item i)
		{
			return i.stack > 0;
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x0050BBEC File Offset: 0x00509DEC
		public static void PlaceItemInFrame(Player player, int x, int y)
		{
			if (!player.ItemTimeIsZero)
			{
				return;
			}
			if (Main.tile[x, y].frameX % 36 != 0)
			{
				x--;
			}
			if (Main.tile[x, y].frameY % 36 != 0)
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
			}
			else
			{
				TEItemFrame.TryPlacing(x, y, player.inventory[player.selectedItem].netID, (int)player.inventory[player.selectedItem].prefix, 1);
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
			WorldGen.RangeFrame(x, y, x + 2, y + 2);
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x0050BD74 File Offset: 0x00509F74
		public void FixLoadedData()
		{
			this.item.FixAgainstExploit();
		}

		// Token: 0x040045B8 RID: 17848
		private static byte _myEntityID;

		// Token: 0x040045B9 RID: 17849
		public Item item;
	}
}
