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
	// Token: 0x02000214 RID: 532
	public class TEDisplayDoll : TileEntity, IFixLoadedData
	{
		// Token: 0x06001E1D RID: 7709 RVA: 0x005081F4 File Offset: 0x005063F4
		public TEDisplayDoll()
		{
			this._items = new Item[8];
			for (int i = 0; i < this._items.Length; i++)
			{
				this._items[i] = new Item();
			}
			this._dyes = new Item[8];
			for (int j = 0; j < this._dyes.Length; j++)
			{
				this._dyes[j] = new Item();
			}
			this._dollPlayer = new Player();
			this._dollPlayer.hair = 15;
			this._dollPlayer.skinColor = Color.White;
			this._dollPlayer.skinVariant = 10;
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00508294 File Offset: 0x00506494
		public override void RegisterTileEntityID(int assignedID)
		{
			TEDisplayDoll._myEntityID = (byte)assignedID;
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x0050829D File Offset: 0x0050649D
		public override TileEntity GenerateInstance()
		{
			return new TEDisplayDoll();
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x005082A4 File Offset: 0x005064A4
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			int number = TEDisplayDoll.Place(x, y);
			NetMessage.SendData(86, -1, -1, null, number, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x005082D0 File Offset: 0x005064D0
		public static int Place(int x, int y)
		{
			TEDisplayDoll tedisplayDoll = new TEDisplayDoll();
			tedisplayDoll.Position = new Point16(x, y);
			tedisplayDoll.ID = TileEntity.AssignNewID();
			tedisplayDoll.type = TEDisplayDoll._myEntityID;
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByID[tedisplayDoll.ID] = tedisplayDoll;
				TileEntity.ByPosition[tedisplayDoll.Position] = tedisplayDoll;
			}
			return tedisplayDoll.ID;
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x0050835C File Offset: 0x0050655C
		public static int Hook_AfterPlacement(int x, int y, int type = 470, int style = 0, int direction = 1, int alternate = 0)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, x, y - 2, 2, 3, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, x, (float)(y - 2), (float)TEDisplayDoll._myEntityID, 0f, 0, 0, 0);
				return -1;
			}
			return TEDisplayDoll.Place(x, y - 2);
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x005083AC File Offset: 0x005065AC
		public static void Kill(int x, int y)
		{
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && tileEntity.type == TEDisplayDoll._myEntityID)
			{
				object entityCreationLock = TileEntity.EntityCreationLock;
				lock (entityCreationLock)
				{
					TileEntity.ByID.Remove(tileEntity.ID);
					TileEntity.ByPosition.Remove(new Point16(x, y));
				}
			}
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x0050842C File Offset: 0x0050662C
		public static int Find(int x, int y)
		{
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && tileEntity.type == TEDisplayDoll._myEntityID)
			{
				return tileEntity.ID;
			}
			return -1;
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x00508464 File Offset: 0x00506664
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			BitsByte bb = 0;
			bb[0] = !this._items[0].IsAir;
			bb[1] = !this._items[1].IsAir;
			bb[2] = !this._items[2].IsAir;
			bb[3] = !this._items[3].IsAir;
			bb[4] = !this._items[4].IsAir;
			bb[5] = !this._items[5].IsAir;
			bb[6] = !this._items[6].IsAir;
			bb[7] = !this._items[7].IsAir;
			BitsByte bb2 = 0;
			bb2[0] = !this._dyes[0].IsAir;
			bb2[1] = !this._dyes[1].IsAir;
			bb2[2] = !this._dyes[2].IsAir;
			bb2[3] = !this._dyes[3].IsAir;
			bb2[4] = !this._dyes[4].IsAir;
			bb2[5] = !this._dyes[5].IsAir;
			bb2[6] = !this._dyes[6].IsAir;
			bb2[7] = !this._dyes[7].IsAir;
			writer.Write(bb);
			writer.Write(bb2);
			for (int i = 0; i < 8; i++)
			{
				Item item = this._items[i];
				if (!item.IsAir)
				{
					writer.Write((short)item.netID);
					writer.Write(item.prefix);
					writer.Write((short)item.stack);
				}
			}
			for (int j = 0; j < 8; j++)
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

		// Token: 0x06001E26 RID: 7718 RVA: 0x005086A8 File Offset: 0x005068A8
		public override void ReadExtraData(BinaryReader reader, bool networkSend)
		{
			BitsByte bitsByte = reader.ReadByte();
			BitsByte bitsByte2 = reader.ReadByte();
			for (int i = 0; i < 8; i++)
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
			for (int j = 0; j < 8; j++)
			{
				this._dyes[j] = new Item();
				Item item2 = this._dyes[j];
				if (bitsByte2[j])
				{
					item2.netDefaults((int)reader.ReadInt16());
					item2.Prefix((int)reader.ReadByte());
					item2.stack = (int)reader.ReadInt16();
				}
			}
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0050877C File Offset: 0x0050697C
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
				this._items[1],
				" ",
				this._items[2]
			});
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x005087FC File Offset: 0x005069FC
		public static void Framing_CheckTile(int callX, int callY)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			Tile tileSafely = Framing.GetTileSafely(callX, callY);
			int num = callX - (int)(tileSafely.frameX / 18 % 2);
			int num2 = callY - (int)(tileSafely.frameY / 18 % 3);
			bool flag = false;
			for (int i = num; i < num + 2; i++)
			{
				for (int j = num2; j < num2 + 3; j++)
				{
					Tile tile = Main.tile[i, j];
					if (!tile.active() || tile.type != 470)
					{
						flag = true;
					}
				}
			}
			if (!WorldGen.SolidTileAllowBottomSlope(num, num2 + 3) || !WorldGen.SolidTileAllowBottomSlope(num + 1, num2 + 3))
			{
				flag = true;
			}
			if (flag)
			{
				TEDisplayDoll.Kill(num, num2);
				if (Main.tile[callX, callY].frameX / 72 != 1)
				{
					Item.NewItem(new EntitySource_TileBreak(num, num2), num * 16, num2 * 16, 32, 48, 498, 1, false, 0, false, false);
				}
				else
				{
					Item.NewItem(new EntitySource_TileBreak(num, num2), num * 16, num2 * 16, 32, 48, 1989, 1, false, 0, false, false);
				}
				WorldGen.destroyObject = true;
				for (int k = num; k < num + 2; k++)
				{
					for (int l = num2; l < num2 + 3; l++)
					{
						if (Main.tile[k, l].active() && Main.tile[k, l].type == 470)
						{
							WorldGen.KillTile(k, l, false, false, false);
						}
					}
				}
				WorldGen.destroyObject = false;
			}
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x0050897C File Offset: 0x00506B7C
		public void Draw(int tileLeftX, int tileTopY)
		{
			Player dollPlayer = this._dollPlayer;
			for (int i = 0; i < 8; i++)
			{
				dollPlayer.armor[i] = this._items[i];
				dollPlayer.dye[i] = this._dyes[i];
			}
			dollPlayer.direction = -1;
			dollPlayer.Male = true;
			Tile tileSafely = Framing.GetTileSafely(tileLeftX, tileTopY);
			if (tileSafely.frameX % 72 == 36)
			{
				dollPlayer.direction = 1;
			}
			if (tileSafely.frameX / 72 == 1)
			{
				dollPlayer.Male = false;
			}
			dollPlayer.isDisplayDollOrInanimate = true;
			dollPlayer.ResetEffects();
			dollPlayer.ResetVisibleAccessories();
			dollPlayer.UpdateDyes();
			dollPlayer.DisplayDollUpdate();
			dollPlayer.UpdateSocialShadow();
			dollPlayer.PlayerFrame();
			Vector2 position = new Vector2((float)(tileLeftX + 1), (float)(tileTopY + 3)) * 16f + new Vector2((float)(-(float)dollPlayer.width / 2), (float)(-(float)dollPlayer.height - 6));
			dollPlayer.position = position;
			dollPlayer.isFullbright = tileSafely.fullbrightBlock();
			dollPlayer.skinDyePacked = PlayerDrawHelper.PackShader((int)tileSafely.color(), PlayerDrawHelper.ShaderConfiguration.TilePaintID);
			Main.PlayerRenderer.DrawPlayer(Main.Camera, dollPlayer, dollPlayer.position, 0f, dollPlayer.fullRotationOrigin, 0f, 1f);
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x00508AAC File Offset: 0x00506CAC
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

		// Token: 0x06001E2B RID: 7723 RVA: 0x00508B28 File Offset: 0x00506D28
		public static void OnPlayerInteraction(Player player, int clickX, int clickY)
		{
			int num = clickX;
			if (Main.tile[num, clickY].frameX % 36 != 0)
			{
				num--;
			}
			int num2 = clickY - (int)(Main.tile[num, clickY].frameY / 18);
			int num3 = TEDisplayDoll.Find(num, num2);
			if (num3 != -1)
			{
				num2++;
				TEDisplayDoll.accessoryTargetSlot = 3;
				TileEntity.BasicOpenCloseInteraction(player, num, num2, num3);
			}
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x00508B8C File Offset: 0x00506D8C
		public override void OnInventoryDraw(Player player, SpriteBatch spriteBatch)
		{
			if (Main.tile[player.tileEntityAnchor.X, player.tileEntityAnchor.Y].type != 470)
			{
				player.tileEntityAnchor.Clear();
				Recipe.FindRecipes(false);
				return;
			}
			this.DrawInner(player, spriteBatch);
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x00508BE0 File Offset: 0x00506DE0
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
				if (context - 23 <= 2)
				{
					if (Main.player[Main.myPlayer].ItemSpace(item).CanTakeItemToPersonalInventory)
					{
						instruction = Lang.misc[68].Value;
						return true;
					}
				}
			}
			else if (TEDisplayDoll.FitsDisplayDoll(item))
			{
				instruction = Lang.misc[76].Value;
				return true;
			}
			return false;
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x00508C60 File Offset: 0x00506E60
		public override string GetItemGamepadInstructions(int slot = 0)
		{
			Item[] inv = this._items;
			int num = slot;
			int context = 23;
			if (slot >= 8)
			{
				num -= 8;
				inv = this._dyes;
				context = 25;
			}
			else if (slot >= 3)
			{
				inv = this._items;
				context = 24;
			}
			return ItemSlot.GetGamepadInstructions(inv, context, num);
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x00508CA3 File Offset: 0x00506EA3
		private void DrawInner(Player player, SpriteBatch spriteBatch)
		{
			Main.inventoryScale = 0.72f;
			this.DrawSlotPairSet(player, spriteBatch, 3, 0, 0f, 0.5f, 23);
			this.DrawSlotPairSet(player, spriteBatch, 5, 3, 3f, 0.5f, 24);
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x00508CDC File Offset: 0x00506EDC
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
						context = 25;
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

		// Token: 0x06001E31 RID: 7729 RVA: 0x00508DEC File Offset: 0x00506FEC
		public override bool OverrideItemSlotHover(Item[] inv, int context = 0, int slot = 0)
		{
			Item item = inv[slot];
			if (!item.IsAir && !inv[slot].favorited && context == 0 && TEDisplayDoll.FitsDisplayDoll(item))
			{
				Main.cursorOverride = 9;
				return true;
			}
			if (!item.IsAir && (context == 23 || context == 24 || context == 25) && Main.player[Main.myPlayer].ItemSpace(inv[slot]).CanTakeItemToPersonalInventory)
			{
				Main.cursorOverride = 8;
				return true;
			}
			return false;
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x00508E60 File Offset: 0x00507060
		public override bool OverrideItemSlotLeftClick(Item[] inv, int context = 0, int slot = 0)
		{
			if (!ItemSlot.ShiftInUse)
			{
				return false;
			}
			if (Main.cursorOverride == 9 && context == 0)
			{
				Item item = inv[slot];
				if (!item.IsAir && !item.favorited && TEDisplayDoll.FitsDisplayDoll(item))
				{
					return this.TryFitting(inv, context, slot, false);
				}
			}
			if ((Main.cursorOverride == 8 && context == 23) || context == 24 || context == 25)
			{
				inv[slot] = Main.player[Main.myPlayer].GetItem(Main.myPlayer, inv[slot], GetItemSettings.InventoryEntityToPlayerInventorySettings);
				if (Main.netMode == 1)
				{
					if (context == 25)
					{
						NetMessage.SendData(121, -1, -1, null, Main.myPlayer, (float)this.ID, (float)slot, 1f, 0, 0, 0);
					}
					else
					{
						NetMessage.SendData(121, -1, -1, null, Main.myPlayer, (float)this.ID, (float)slot, 0f, 0, 0, 0);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x00508F32 File Offset: 0x00507132
		public static bool FitsDisplayDoll(Item item)
		{
			return item.maxStack <= 1 && (item.headSlot > 0 || item.bodySlot > 0 || item.legSlot > 0 || item.accessory);
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x00508F64 File Offset: 0x00507164
		private bool TryFitting(Item[] inv, int context = 0, int slot = 0, bool justCheck = false)
		{
			Item item = inv[slot];
			int num = -1;
			if (item.headSlot > 0)
			{
				num = 0;
			}
			if (item.bodySlot > 0)
			{
				num = 1;
			}
			if (item.legSlot > 0)
			{
				num = 2;
			}
			if (item.accessory)
			{
				num = TEDisplayDoll.accessoryTargetSlot;
			}
			if (num == -1)
			{
				return false;
			}
			if (justCheck)
			{
				return true;
			}
			if (item.accessory)
			{
				TEDisplayDoll.accessoryTargetSlot++;
				if (TEDisplayDoll.accessoryTargetSlot >= 8)
				{
					TEDisplayDoll.accessoryTargetSlot = 3;
				}
				bool flag = ItemSlot.AccCheck(this._items, inv[slot], -1);
				for (int i = 3; i < 8; i++)
				{
					if (this._items[i].IsAir)
					{
						num = i;
						TEDisplayDoll.accessoryTargetSlot = i;
						break;
					}
				}
				for (int j = 3; j < 8; j++)
				{
					if (inv[slot].type == this._items[j].type || (flag && !ItemSlot.AccCheck(this._items, inv[slot], j)))
					{
						num = j;
						if (flag)
						{
							flag = false;
							break;
						}
					}
				}
				if (flag && num > -1)
				{
					return false;
				}
			}
			SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			Utils.Swap<Item>(ref this._items[num], ref inv[slot]);
			if (Main.netMode == 1)
			{
				NetMessage.SendData(121, -1, -1, null, Main.myPlayer, (float)this.ID, (float)num, 0f, 0, 0, 0);
			}
			return true;
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x005090B4 File Offset: 0x005072B4
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

		// Token: 0x06001E36 RID: 7734 RVA: 0x005090FC File Offset: 0x005072FC
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

		// Token: 0x06001E37 RID: 7735 RVA: 0x0050914C File Offset: 0x0050734C
		public override bool IsTileValidForEntity(int x, int y)
		{
			return Main.tile[x, y].active() && Main.tile[x, y].type == 470 && Main.tile[x, y].frameY == 0 && Main.tile[x, y].frameX % 36 == 0;
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x005091B0 File Offset: 0x005073B0
		public void SetInventoryFromMannequin(int headFrame, int shirtFrame, int legFrame)
		{
			headFrame /= 100;
			shirtFrame /= 100;
			legFrame /= 100;
			if (headFrame >= 0 && headFrame < Item.headType.Length)
			{
				this._items[0].SetDefaults(Item.headType[headFrame]);
			}
			if (shirtFrame >= 0 && shirtFrame < Item.bodyType.Length)
			{
				this._items[1].SetDefaults(Item.bodyType[shirtFrame]);
			}
			if (legFrame >= 0 && legFrame < Item.legType.Length)
			{
				this._items[2].SetDefaults(Item.legType[legFrame]);
			}
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00509238 File Offset: 0x00507438
		public static bool IsBreakable(int clickX, int clickY)
		{
			int num = clickX;
			if (Main.tile[num, clickY].frameX % 36 != 0)
			{
				num--;
			}
			int y = clickY - (int)(Main.tile[num, clickY].frameY / 18);
			int num2 = TEDisplayDoll.Find(num, y);
			return num2 == -1 || !(TileEntity.ByID[num2] as TEDisplayDoll).ContainsItems();
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x005092A0 File Offset: 0x005074A0
		public bool ContainsItems()
		{
			for (int i = 0; i < 8; i++)
			{
				if (!this._items[i].IsAir || !this._dyes[i].IsAir)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x005092DC File Offset: 0x005074DC
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

		// Token: 0x04004590 RID: 17808
		private static byte _myEntityID;

		// Token: 0x04004591 RID: 17809
		private const int MyTileID = 470;

		// Token: 0x04004592 RID: 17810
		private const int entityTileWidth = 2;

		// Token: 0x04004593 RID: 17811
		private const int entityTileHeight = 3;

		// Token: 0x04004594 RID: 17812
		private Player _dollPlayer;

		// Token: 0x04004595 RID: 17813
		private Item[] _items;

		// Token: 0x04004596 RID: 17814
		private Item[] _dyes;

		// Token: 0x04004597 RID: 17815
		private static int accessoryTargetSlot = 3;
	}
}
