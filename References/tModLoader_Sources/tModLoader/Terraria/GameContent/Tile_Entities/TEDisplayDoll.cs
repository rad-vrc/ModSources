using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x0200055C RID: 1372
	public sealed class TEDisplayDoll : TileEntity, IFixLoadedData
	{
		// Token: 0x06004067 RID: 16487 RVA: 0x005E0030 File Offset: 0x005DE230
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

		// Token: 0x06004068 RID: 16488 RVA: 0x005E00D0 File Offset: 0x005DE2D0
		public override void RegisterTileEntityID(int assignedID)
		{
			TEDisplayDoll._myEntityID = (byte)assignedID;
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x005E00D9 File Offset: 0x005DE2D9
		public override TileEntity GenerateInstance()
		{
			return new TEDisplayDoll();
		}

		// Token: 0x0600406A RID: 16490 RVA: 0x005E00E0 File Offset: 0x005DE2E0
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			int number = TEDisplayDoll.Place(x, y);
			NetMessage.SendData(86, -1, -1, null, number, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x0600406B RID: 16491 RVA: 0x005E010C File Offset: 0x005DE30C
		public static int Place(int x, int y)
		{
			TEDisplayDoll tEDisplayDoll = new TEDisplayDoll();
			tEDisplayDoll.Position = new Point16(x, y);
			tEDisplayDoll.ID = TileEntity.AssignNewID();
			tEDisplayDoll.type = TEDisplayDoll._myEntityID;
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByID[tEDisplayDoll.ID] = tEDisplayDoll;
				TileEntity.ByPosition[tEDisplayDoll.Position] = tEDisplayDoll;
			}
			return tEDisplayDoll.ID;
		}

		// Token: 0x0600406C RID: 16492 RVA: 0x005E0198 File Offset: 0x005DE398
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

		// Token: 0x0600406D RID: 16493 RVA: 0x005E01E8 File Offset: 0x005DE3E8
		public static void Kill(int x, int y)
		{
			TileEntity value;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out value) && value.type == TEDisplayDoll._myEntityID)
			{
				object entityCreationLock = TileEntity.EntityCreationLock;
				lock (entityCreationLock)
				{
					TileEntity.ByID.Remove(value.ID);
					TileEntity.ByPosition.Remove(new Point16(x, y));
				}
			}
		}

		// Token: 0x0600406E RID: 16494 RVA: 0x005E0268 File Offset: 0x005DE468
		public static int Find(int x, int y)
		{
			TileEntity value;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out value) && value.type == TEDisplayDoll._myEntityID)
			{
				return value.ID;
			}
			return -1;
		}

		// Token: 0x0600406F RID: 16495 RVA: 0x005E02A0 File Offset: 0x005DE4A0
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			BitsByte bitsByte = 0;
			bitsByte[0] = !this._items[0].IsAir;
			bitsByte[1] = !this._items[1].IsAir;
			bitsByte[2] = !this._items[2].IsAir;
			bitsByte[3] = !this._items[3].IsAir;
			bitsByte[4] = !this._items[4].IsAir;
			bitsByte[5] = !this._items[5].IsAir;
			bitsByte[6] = !this._items[6].IsAir;
			bitsByte[7] = !this._items[7].IsAir;
			BitsByte bitsByte2 = 0;
			bitsByte2[0] = !this._dyes[0].IsAir;
			bitsByte2[1] = !this._dyes[1].IsAir;
			bitsByte2[2] = !this._dyes[2].IsAir;
			bitsByte2[3] = !this._dyes[3].IsAir;
			bitsByte2[4] = !this._dyes[4].IsAir;
			bitsByte2[5] = !this._dyes[5].IsAir;
			bitsByte2[6] = !this._dyes[6].IsAir;
			bitsByte2[7] = !this._dyes[7].IsAir;
			writer.Write(bitsByte);
			writer.Write(bitsByte2);
			for (int i = 0; i < 8; i++)
			{
				Item item = this._items[i];
				if (!item.IsAir)
				{
					ItemIO.WriteShortVanillaID(item, writer);
					ItemIO.WriteByteVanillaPrefix(item, writer);
					writer.Write((short)item.stack);
				}
			}
			for (int j = 0; j < 8; j++)
			{
				Item item2 = this._dyes[j];
				if (!item2.IsAir)
				{
					ItemIO.WriteShortVanillaID(item2, writer);
					ItemIO.WriteByteVanillaPrefix(item2, writer);
					writer.Write((short)item2.stack);
				}
			}
		}

		// Token: 0x06004070 RID: 16496 RVA: 0x005E04D0 File Offset: 0x005DE6D0
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

		// Token: 0x06004071 RID: 16497 RVA: 0x005E05A4 File Offset: 0x005DE7A4
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

		// Token: 0x06004072 RID: 16498 RVA: 0x005E0624 File Offset: 0x005DE824
		public unsafe static void Framing_CheckTile(int callX, int callY)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			Tile tileSafely = Framing.GetTileSafely(callX, callY);
			int num = callX - (int)(*tileSafely.frameX / 18 % 2);
			int num2 = callY - (int)(*tileSafely.frameY / 18 % 3);
			bool flag = false;
			for (int i = num; i < num + 2; i++)
			{
				for (int j = num2; j < num2 + 3; j++)
				{
					Tile tile = Main.tile[i, j];
					if (!tile.active() || *tile.type != 470)
					{
						flag = true;
					}
				}
			}
			if (!WorldGen.SolidTileAllowBottomSlope(num, num2 + 3) || !WorldGen.SolidTileAllowBottomSlope(num + 1, num2 + 3))
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			TEDisplayDoll.Kill(num, num2);
			if (TileLoader.Drop(callX, callY, 470, true))
			{
				if (*Main.tile[callX, callY].frameX / 72 != 1)
				{
					Item.NewItem(new EntitySource_TileBreak(num, num2, null), num * 16, num2 * 16, 32, 48, 498, 1, false, 0, false, false);
				}
				else
				{
					Item.NewItem(new EntitySource_TileBreak(num, num2, null), num * 16, num2 * 16, 32, 48, 1989, 1, false, 0, false, false);
				}
			}
			WorldGen.destroyObject = true;
			for (int k = num; k < num + 2; k++)
			{
				for (int l = num2; l < num2 + 3; l++)
				{
					if (Main.tile[k, l].active() && *Main.tile[k, l].type == 470)
					{
						WorldGen.KillTile(k, l, false, false, false);
					}
				}
			}
			WorldGen.destroyObject = false;
		}

		// Token: 0x06004073 RID: 16499 RVA: 0x005E07C4 File Offset: 0x005DE9C4
		public unsafe void Draw(int tileLeftX, int tileTopY)
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
			if (*tileSafely.frameX % 72 == 36)
			{
				dollPlayer.direction = 1;
			}
			if (*tileSafely.frameX / 72 == 1)
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

		// Token: 0x06004074 RID: 16500 RVA: 0x005E08F8 File Offset: 0x005DEAF8
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

		// Token: 0x06004075 RID: 16501 RVA: 0x005E0974 File Offset: 0x005DEB74
		public unsafe static void OnPlayerInteraction(Player player, int clickX, int clickY)
		{
			int num = clickX;
			if (*Main.tile[num, clickY].frameX % 36 != 0)
			{
				num--;
			}
			int num2 = clickY - (int)(*Main.tile[num, clickY].frameY / 18);
			int num3 = TEDisplayDoll.Find(num, num2);
			if (num3 != -1)
			{
				num2++;
				TEDisplayDoll.accessoryTargetSlot = 3;
				TileEntity.BasicOpenCloseInteraction(player, num, num2, num3);
			}
		}

		// Token: 0x06004076 RID: 16502 RVA: 0x005E09E0 File Offset: 0x005DEBE0
		public unsafe override void OnInventoryDraw(Player player, SpriteBatch spriteBatch)
		{
			if (*Main.tile[player.tileEntityAnchor.X, player.tileEntityAnchor.Y].type != 470)
			{
				player.tileEntityAnchor.Clear();
				Recipe.FindRecipes(false);
				return;
			}
			this.DrawInner(player, spriteBatch);
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x005E0A38 File Offset: 0x005DEC38
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

		// Token: 0x06004078 RID: 16504 RVA: 0x005E0AB8 File Offset: 0x005DECB8
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

		// Token: 0x06004079 RID: 16505 RVA: 0x005E0AFB File Offset: 0x005DECFB
		private void DrawInner(Player player, SpriteBatch spriteBatch)
		{
			Main.inventoryScale = 0.72f;
			this.DrawSlotPairSet(player, spriteBatch, 3, 0, 0f, 0.5f, 23);
			this.DrawSlotPairSet(player, spriteBatch, 5, 3, 3f, 0.5f, 24);
		}

		// Token: 0x0600407A RID: 16506 RVA: 0x005E0B34 File Offset: 0x005DED34
		private void DrawSlotPairSet(Player player, SpriteBatch spriteBatch, int slotsToShowLine, int slotsArrayOffset, float offsetX, float offsetY, int inventoryContextTarget)
		{
			Item[] items = this._items;
			for (int i = 0; i < slotsToShowLine; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					int num2 = (int)(73f + ((float)i + offsetX) * 56f * Main.inventoryScale);
					int num3 = (int)((float)Main.instance.invBottom + ((float)j + offsetY) * 56f * Main.inventoryScale);
					int num4;
					if (j == 0)
					{
						items = this._items;
						num4 = inventoryContextTarget;
					}
					else
					{
						items = this._dyes;
						num4 = 25;
					}
					if (Utils.FloatIntersect((float)Main.mouseX, (float)Main.mouseY, 0f, 0f, (float)num2, (float)num3, (float)TextureAssets.InventoryBack.Width() * Main.inventoryScale, (float)TextureAssets.InventoryBack.Height() * Main.inventoryScale) && !PlayerInput.IgnoreMouseInterface)
					{
						player.mouseInterface = true;
						ItemSlot.Handle(items, num4, i + slotsArrayOffset);
					}
					ItemSlot.Draw(spriteBatch, items, num4, i + slotsArrayOffset, new Vector2((float)num2, (float)num3), default(Color));
				}
			}
		}

		// Token: 0x0600407B RID: 16507 RVA: 0x005E0C44 File Offset: 0x005DEE44
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

		// Token: 0x0600407C RID: 16508 RVA: 0x005E0CB8 File Offset: 0x005DEEB8
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

		// Token: 0x0600407D RID: 16509 RVA: 0x005E0D8A File Offset: 0x005DEF8A
		public static bool FitsDisplayDoll(Item item)
		{
			return item.maxStack <= 1 && (item.headSlot > 0 || item.bodySlot > 0 || item.legSlot > 0 || item.accessory);
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x005E0DBC File Offset: 0x005DEFBC
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

		// Token: 0x0600407F RID: 16511 RVA: 0x005E0F0C File Offset: 0x005DF10C
		public void WriteItem(int itemIndex, BinaryWriter writer, bool dye)
		{
			Item item = this._items[itemIndex];
			if (dye)
			{
				item = this._dyes[itemIndex];
			}
			ItemIO.Send(item, writer, true, false);
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x005E0F38 File Offset: 0x005DF138
		public void ReadItem(int itemIndex, BinaryReader reader, bool dye)
		{
			Item item = (dye ? this._dyes : this._items)[itemIndex];
			ItemIO.Receive(item, reader, true, false);
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x005E0F64 File Offset: 0x005DF164
		public unsafe override bool IsTileValidForEntity(int x, int y)
		{
			return Main.tile[x, y].active() && *Main.tile[x, y].type == 470 && *Main.tile[x, y].frameY == 0 && *Main.tile[x, y].frameX % 36 == 0;
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x005E0FD8 File Offset: 0x005DF1D8
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

		// Token: 0x06004083 RID: 16515 RVA: 0x005E1060 File Offset: 0x005DF260
		public unsafe static bool IsBreakable(int clickX, int clickY)
		{
			int num = clickX;
			if (*Main.tile[num, clickY].frameX % 36 != 0)
			{
				num--;
			}
			int num2 = clickY - (int)(*Main.tile[num, clickY].frameY / 18);
			int num3 = TEDisplayDoll.Find(num, num2);
			return num3 == -1 || !(TileEntity.ByID[num3] as TEDisplayDoll).ContainsItems();
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x005E10D0 File Offset: 0x005DF2D0
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

		// Token: 0x06004085 RID: 16517 RVA: 0x005E110C File Offset: 0x005DF30C
		public void FixLoadedData()
		{
			Item[] items = this._items;
			for (int i = 0; i < items.Length; i++)
			{
				items[i].FixAgainstExploit();
			}
			items = this._dyes;
			for (int j = 0; j < items.Length; j++)
			{
				items[j].FixAgainstExploit();
			}
		}

		// Token: 0x06004086 RID: 16518 RVA: 0x005E1153 File Offset: 0x005DF353
		public override void SaveData(TagCompound tag)
		{
			tag["items"] = PlayerIO.SaveInventory(this._items);
			tag["dyes"] = PlayerIO.SaveInventory(this._dyes);
		}

		// Token: 0x06004087 RID: 16519 RVA: 0x005E1181 File Offset: 0x005DF381
		public override void LoadData(TagCompound tag)
		{
			PlayerIO.LoadInventory(this._items, tag.GetList<TagCompound>("items"));
			PlayerIO.LoadInventory(this._dyes, tag.GetList<TagCompound>("dyes"));
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x005E11B0 File Offset: 0x005DF3B0
		public override void NetSend(BinaryWriter writer)
		{
			BitsByte itemsBits = default(BitsByte);
			BitsByte dyesBits = default(BitsByte);
			for (int i = 0; i < 8; i++)
			{
				itemsBits[i] = !this._items[i].IsAir;
				dyesBits[i] = !this._dyes[i].IsAir;
			}
			writer.Write(itemsBits);
			writer.Write(dyesBits);
			for (int j = 0; j < 8; j++)
			{
				Item item = this._items[j];
				if (!item.IsAir)
				{
					ItemIO.Send(item, writer, true, false);
				}
			}
			for (int k = 0; k < 8; k++)
			{
				Item dye = this._dyes[k];
				if (!dye.IsAir)
				{
					ItemIO.Send(dye, writer, true, false);
				}
			}
		}

		// Token: 0x06004089 RID: 16521 RVA: 0x005E1278 File Offset: 0x005DF478
		public override void NetReceive(BinaryReader reader)
		{
			BitsByte presentItems = reader.ReadByte();
			BitsByte presentDyes = reader.ReadByte();
			for (int i = 0; i < 8; i++)
			{
				this._items[i] = (presentItems[i] ? ItemIO.Receive(reader, true, false) : new Item());
			}
			for (int j = 0; j < 8; j++)
			{
				this._dyes[j] = (presentDyes[j] ? ItemIO.Receive(reader, true, false) : new Item());
			}
		}

		// Token: 0x0400587F RID: 22655
		private static byte _myEntityID;

		// Token: 0x04005880 RID: 22656
		private const int MyTileID = 470;

		// Token: 0x04005881 RID: 22657
		private const int entityTileWidth = 2;

		// Token: 0x04005882 RID: 22658
		private const int entityTileHeight = 3;

		// Token: 0x04005883 RID: 22659
		private Player _dollPlayer;

		// Token: 0x04005884 RID: 22660
		private Item[] _items;

		// Token: 0x04005885 RID: 22661
		private Item[] _dyes;

		// Token: 0x04005886 RID: 22662
		private static int accessoryTargetSlot = 3;
	}
}
