using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x02000216 RID: 534
	public class TEHatRack : TileEntity, IFixLoadedData
	{
		// Token: 0x06001E3D RID: 7741 RVA: 0x0050932C File Offset: 0x0050752C
		public TEHatRack()
		{
			this._items = new Item[2];
			for (int i = 0; i < this._items.Length; i++)
			{
				this._items[i] = new Item();
			}
			this._dyes = new Item[2];
			for (int j = 0; j < this._dyes.Length; j++)
			{
				this._dyes[j] = new Item();
			}
			this._dollPlayer = new Player();
			this._dollPlayer.hair = 15;
			this._dollPlayer.skinColor = Color.White;
			this._dollPlayer.skinVariant = 10;
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x005093CC File Offset: 0x005075CC
		public override void RegisterTileEntityID(int assignedID)
		{
			TEHatRack._myEntityID = (byte)assignedID;
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x005093D5 File Offset: 0x005075D5
		public override TileEntity GenerateInstance()
		{
			return new TEHatRack();
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x005093DC File Offset: 0x005075DC
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			int number = TEHatRack.Place(x, y);
			NetMessage.SendData(86, -1, -1, null, number, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x00509408 File Offset: 0x00507608
		public static int Place(int x, int y)
		{
			TEHatRack tehatRack = new TEHatRack();
			tehatRack.Position = new Point16(x, y);
			tehatRack.ID = TileEntity.AssignNewID();
			tehatRack.type = TEHatRack._myEntityID;
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByID[tehatRack.ID] = tehatRack;
				TileEntity.ByPosition[tehatRack.Position] = tehatRack;
			}
			return tehatRack.ID;
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x00509494 File Offset: 0x00507694
		public static int Hook_AfterPlacement(int x, int y, int type = 475, int style = 0, int direction = 1, int alternate = 0)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, x - 1, y - 3, 3, 4, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, x + -1, (float)(y + -3), (float)TEHatRack._myEntityID, 0f, 0, 0, 0);
				return -1;
			}
			return TEHatRack.Place(x + -1, y + -3);
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x005094EC File Offset: 0x005076EC
		public static void Kill(int x, int y)
		{
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && tileEntity.type == TEHatRack._myEntityID)
			{
				object entityCreationLock = TileEntity.EntityCreationLock;
				lock (entityCreationLock)
				{
					TileEntity.ByID.Remove(tileEntity.ID);
					TileEntity.ByPosition.Remove(new Point16(x, y));
				}
			}
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x0050956C File Offset: 0x0050776C
		public static int Find(int x, int y)
		{
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && tileEntity.type == TEHatRack._myEntityID)
			{
				return tileEntity.ID;
			}
			return -1;
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x005095A4 File Offset: 0x005077A4
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			BitsByte bb = 0;
			bb[0] = !this._items[0].IsAir;
			bb[1] = !this._items[1].IsAir;
			bb[2] = !this._dyes[0].IsAir;
			bb[3] = !this._dyes[1].IsAir;
			writer.Write(bb);
			for (int i = 0; i < 2; i++)
			{
				Item item = this._items[i];
				if (!item.IsAir)
				{
					writer.Write((short)item.netID);
					writer.Write(item.prefix);
					writer.Write((short)item.stack);
				}
			}
			for (int j = 0; j < 2; j++)
			{
				Item item2 = this._dyes[j];
				if (!item2.IsAir)
				{
					writer.Write((short)item2.netID);
					writer.Write(item2.prefix);
					writer.Write((short)item2.stack);
				}
			}
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x005096B0 File Offset: 0x005078B0
		public override void ReadExtraData(BinaryReader reader, bool networkSend)
		{
			BitsByte bitsByte = reader.ReadByte();
			for (int i = 0; i < 2; i++)
			{
				this._items[i] = new Item();
				Item item = this._items[i];
				if (bitsByte[i])
				{
					item.netDefaults((int)reader.ReadInt16());
					item.Prefix((int)reader.ReadByte());
					item.stack = (int)reader.ReadInt16();
				}
			}
			for (int j = 0; j < 2; j++)
			{
				this._dyes[j] = new Item();
				Item item2 = this._dyes[j];
				if (bitsByte[j + 2])
				{
					item2.netDefaults((int)reader.ReadInt16());
					item2.Prefix((int)reader.ReadByte());
					item2.stack = (int)reader.ReadInt16();
				}
			}
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x00509774 File Offset: 0x00507974
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Position.X,
				"x  ",
				this.Position.Y,
				"y item: ",
				this._items[0],
				" ",
				this._items[1]
			});
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x005097E0 File Offset: 0x005079E0
		public static void Framing_CheckTile(int callX, int callY)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			Tile tileSafely = Framing.GetTileSafely(callX, callY);
			int num = callX - (int)(tileSafely.frameX / 18 % 3);
			int num2 = callY - (int)(tileSafely.frameY / 18 % 4);
			bool flag = false;
			for (int i = num; i < num + 3; i++)
			{
				for (int j = num2; j < num2 + 4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (!tile.active() || tile.type != 475)
					{
						flag = true;
					}
				}
			}
			if (!WorldGen.SolidTileAllowBottomSlope(num, num2 + 4) || !WorldGen.SolidTileAllowBottomSlope(num + 1, num2 + 4) || !WorldGen.SolidTileAllowBottomSlope(num + 2, num2 + 4))
			{
				flag = true;
			}
			if (flag)
			{
				TEHatRack.Kill(num, num2);
				Item.NewItem(new EntitySource_TileBreak(num, num2), num * 16, num2 * 16, 48, 64, 3977, 1, false, 0, false, false);
				WorldGen.destroyObject = true;
				for (int k = num; k < num + 3; k++)
				{
					for (int l = num2; l < num2 + 4; l++)
					{
						if (Main.tile[k, l].active() && Main.tile[k, l].type == 475)
						{
							WorldGen.KillTile(k, l, false, false, false);
						}
					}
				}
				WorldGen.destroyObject = false;
			}
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x00509930 File Offset: 0x00507B30
		public void Draw(int tileLeftX, int tileTopY)
		{
			Player dollPlayer = this._dollPlayer;
			dollPlayer.direction = -1;
			dollPlayer.Male = true;
			if (Framing.GetTileSafely(tileLeftX, tileTopY).frameX % 216 == 54)
			{
				dollPlayer.direction = 1;
			}
			dollPlayer.isDisplayDollOrInanimate = true;
			dollPlayer.isHatRackDoll = true;
			dollPlayer.armor[0] = this._items[0];
			dollPlayer.dye[0] = this._dyes[0];
			dollPlayer.ResetEffects();
			dollPlayer.ResetVisibleAccessories();
			dollPlayer.invis = true;
			dollPlayer.UpdateDyes();
			dollPlayer.DisplayDollUpdate();
			dollPlayer.PlayerFrame();
			Vector2 value = new Vector2((float)tileLeftX + 1.5f, (float)(tileTopY + 4)) * 16f;
			dollPlayer.direction *= -1;
			Vector2 value2 = new Vector2((float)(-(float)dollPlayer.width / 2), (float)(-(float)dollPlayer.height - 6)) + new Vector2((float)(dollPlayer.direction * 14), -2f);
			dollPlayer.position = value + value2;
			Main.PlayerRenderer.DrawPlayer(Main.Camera, dollPlayer, dollPlayer.position, 0f, dollPlayer.fullRotationOrigin, 0f, 1f);
			dollPlayer.armor[0] = this._items[1];
			dollPlayer.dye[0] = this._dyes[1];
			dollPlayer.ResetEffects();
			dollPlayer.ResetVisibleAccessories();
			dollPlayer.invis = true;
			dollPlayer.UpdateDyes();
			dollPlayer.DisplayDollUpdate();
			dollPlayer.skipAnimatingValuesInPlayerFrame = true;
			dollPlayer.PlayerFrame();
			dollPlayer.skipAnimatingValuesInPlayerFrame = false;
			dollPlayer.direction *= -1;
			value2 = new Vector2((float)(-(float)dollPlayer.width / 2), (float)(-(float)dollPlayer.height - 6)) + new Vector2((float)(dollPlayer.direction * 12), 16f);
			dollPlayer.position = value + value2;
			Main.PlayerRenderer.DrawPlayer(Main.Camera, dollPlayer, dollPlayer.position, 0f, dollPlayer.fullRotationOrigin, 0f, 1f);
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x00509B24 File Offset: 0x00507D24
		public override string GetItemGamepadInstructions(int slot = 0)
		{
			Item[] inv = this._items;
			int num = slot;
			int context = 26;
			if (slot >= 2)
			{
				num -= 2;
				inv = this._dyes;
				context = 27;
			}
			return ItemSlot.GetGamepadInstructions(inv, context, num);
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x00509B58 File Offset: 0x00507D58
		public override bool TryGetItemGamepadOverrideInstructions(Item[] inv, int context, int slot, out string instruction)
		{
			instruction = "";
			Item item = inv[slot];
			if (item.IsAir || item.favorited)
			{
				return false;
			}
			if (context != 0)
			{
				if (context - 26 <= 1)
				{
					if (Main.player[Main.myPlayer].ItemSpace(item).CanTakeItemToPersonalInventory)
					{
						instruction = Lang.misc[68].Value;
						return true;
					}
				}
			}
			else if (TEHatRack.FitsHatRack(item))
			{
				instruction = Lang.misc[76].Value;
				return true;
			}
			return false;
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x00509BD8 File Offset: 0x00507DD8
		public override void OnPlayerUpdate(Player player)
		{
			if (!player.InInteractionRange(player.tileEntityAnchor.X, player.tileEntityAnchor.Y, TileReachCheckSettings.Simple) || player.chest != -1 || player.talkNPC != -1)
			{
				if (player.chest == -1 && player.talkNPC == -1)
				{
					SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				}
				player.tileEntityAnchor.Clear();
				Recipe.FindRecipes(false);
			}
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x00509C54 File Offset: 0x00507E54
		public static void OnPlayerInteraction(Player player, int clickX, int clickY)
		{
			int num = clickX - (int)(Main.tile[clickX, clickY].frameX % 54 / 18);
			int num2 = clickY - (int)(Main.tile[num, clickY].frameY / 18);
			int num3 = TEHatRack.Find(num, num2);
			if (num3 != -1)
			{
				num2++;
				num++;
				TileEntity.BasicOpenCloseInteraction(player, num, num2, num3);
			}
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x00509CB4 File Offset: 0x00507EB4
		public override void OnInventoryDraw(Player player, SpriteBatch spriteBatch)
		{
			if (Main.tile[player.tileEntityAnchor.X, player.tileEntityAnchor.Y].type != 475)
			{
				player.tileEntityAnchor.Clear();
				Recipe.FindRecipes(false);
				return;
			}
			this.DrawInner(player, spriteBatch);
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x00509D07 File Offset: 0x00507F07
		private void DrawInner(Player player, SpriteBatch spriteBatch)
		{
			Main.inventoryScale = 0.72f;
			this.DrawSlotPairSet(player, spriteBatch, 2, 0, 3.5f, 0.5f, 26);
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x00509D2C File Offset: 0x00507F2C
		private void DrawSlotPairSet(Player player, SpriteBatch spriteBatch, int slotsToShowLine, int slotsArrayOffset, float offsetX, float offsetY, int inventoryContextTarget)
		{
			Item[] inv = this._items;
			for (int i = 0; i < slotsToShowLine; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					int num = (int)(73f + ((float)i + offsetX) * 56f * Main.inventoryScale);
					int num2 = (int)((float)Main.instance.invBottom + ((float)j + offsetY) * 56f * Main.inventoryScale);
					int context;
					if (j == 0)
					{
						inv = this._items;
						context = inventoryContextTarget;
					}
					else
					{
						inv = this._dyes;
						context = 27;
					}
					if (Utils.FloatIntersect((float)Main.mouseX, (float)Main.mouseY, 0f, 0f, (float)num, (float)num2, (float)TextureAssets.InventoryBack.Width() * Main.inventoryScale, (float)TextureAssets.InventoryBack.Height() * Main.inventoryScale) && !PlayerInput.IgnoreMouseInterface)
					{
						player.mouseInterface = true;
						ItemSlot.Handle(inv, context, i + slotsArrayOffset);
					}
					ItemSlot.Draw(spriteBatch, inv, context, i + slotsArrayOffset, new Vector2((float)num, (float)num2), default(Color));
				}
			}
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x00509E3C File Offset: 0x0050803C
		public override bool OverrideItemSlotHover(Item[] inv, int context = 0, int slot = 0)
		{
			Item item = inv[slot];
			if (!item.IsAir && !inv[slot].favorited && context == 0 && TEHatRack.FitsHatRack(item))
			{
				Main.cursorOverride = 9;
				return true;
			}
			if (!item.IsAir && (context == 26 || context == 27) && Main.player[Main.myPlayer].ItemSpace(inv[slot]).CanTakeItemToPersonalInventory)
			{
				Main.cursorOverride = 8;
				return true;
			}
			return false;
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x00509EAC File Offset: 0x005080AC
		public override bool OverrideItemSlotLeftClick(Item[] inv, int context = 0, int slot = 0)
		{
			if (!ItemSlot.ShiftInUse)
			{
				return false;
			}
			if (Main.cursorOverride == 9 && context == 0)
			{
				Item item = inv[slot];
				if (Main.cursorOverride == 9 && !item.IsAir && !item.favorited && context == 0 && TEHatRack.FitsHatRack(item))
				{
					return this.TryFitting(inv, context, slot, false);
				}
			}
			if ((Main.cursorOverride == 8 && context == 23) || context == 26 || context == 27)
			{
				inv[slot] = Main.player[Main.myPlayer].GetItem(Main.myPlayer, inv[slot], GetItemSettings.InventoryEntityToPlayerInventorySettings);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(124, -1, -1, null, Main.myPlayer, (float)this.ID, (float)slot, 0f, 0, 0, 0);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x00509F63 File Offset: 0x00508163
		public static bool FitsHatRack(Item item)
		{
			return item.maxStack <= 1 && item.headSlot > 0;
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x00509F7C File Offset: 0x0050817C
		private bool TryFitting(Item[] inv, int context = 0, int slot = 0, bool justCheck = false)
		{
			if (!TEHatRack.FitsHatRack(inv[slot]))
			{
				return false;
			}
			if (justCheck)
			{
				return true;
			}
			int num = TEHatRack.hatTargetSlot;
			TEHatRack.hatTargetSlot++;
			for (int i = 0; i < 2; i++)
			{
				if (this._items[i].IsAir)
				{
					num = i;
					TEHatRack.hatTargetSlot = i + 1;
					break;
				}
			}
			for (int j = 0; j < 2; j++)
			{
				if (inv[slot].type == this._items[j].type)
				{
					num = j;
				}
			}
			if (TEHatRack.hatTargetSlot >= 2)
			{
				TEHatRack.hatTargetSlot = 0;
			}
			SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			Utils.Swap<Item>(ref this._items[num], ref inv[slot]);
			if (Main.netMode == 1)
			{
				NetMessage.SendData(124, -1, -1, null, Main.myPlayer, (float)this.ID, (float)num, 0f, 0, 0, 0);
			}
			return true;
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x0050A05C File Offset: 0x0050825C
		public void WriteItem(int itemIndex, BinaryWriter writer, bool dye)
		{
			Item item = this._items[itemIndex];
			if (dye)
			{
				item = this._dyes[itemIndex];
			}
			writer.Write((ushort)item.netID);
			writer.Write((ushort)item.stack);
			writer.Write(item.prefix);
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x0050A0A4 File Offset: 0x005082A4
		public void ReadItem(int itemIndex, BinaryReader reader, bool dye)
		{
			int defaults = (int)reader.ReadUInt16();
			int stack = (int)reader.ReadUInt16();
			int prefixWeWant = (int)reader.ReadByte();
			Item item = this._items[itemIndex];
			if (dye)
			{
				item = this._dyes[itemIndex];
			}
			item.SetDefaults(defaults);
			item.stack = stack;
			item.Prefix(prefixWeWant);
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x0050A0F4 File Offset: 0x005082F4
		public override bool IsTileValidForEntity(int x, int y)
		{
			return Main.tile[x, y].active() && Main.tile[x, y].type == 475 && Main.tile[x, y].frameY == 0 && Main.tile[x, y].frameX % 54 == 0;
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x0050A158 File Offset: 0x00508358
		public static bool IsBreakable(int clickX, int clickY)
		{
			int num = clickX - (int)(Main.tile[clickX, clickY].frameX % 54 / 18);
			int y = clickY - (int)(Main.tile[num, clickY].frameY / 18);
			int num2 = TEHatRack.Find(num, y);
			return num2 == -1 || !(TileEntity.ByID[num2] as TEHatRack).ContainsItems();
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x0050A1C0 File Offset: 0x005083C0
		public bool ContainsItems()
		{
			for (int i = 0; i < 2; i++)
			{
				if (!this._items[i].IsAir || !this._dyes[i].IsAir)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x0050A1FC File Offset: 0x005083FC
		public void FixLoadedData()
		{
			Item[] array = this._items;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].FixAgainstExploit();
			}
			array = this._dyes;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].FixAgainstExploit();
			}
		}

		// Token: 0x040045A1 RID: 17825
		private static byte _myEntityID;

		// Token: 0x040045A2 RID: 17826
		private const int MyTileID = 475;

		// Token: 0x040045A3 RID: 17827
		private const int entityTileWidth = 3;

		// Token: 0x040045A4 RID: 17828
		private const int entityTileHeight = 4;

		// Token: 0x040045A5 RID: 17829
		private Player _dollPlayer;

		// Token: 0x040045A6 RID: 17830
		private Item[] _items;

		// Token: 0x040045A7 RID: 17831
		private Item[] _dyes;

		// Token: 0x040045A8 RID: 17832
		private static int hatTargetSlot;
	}
}
