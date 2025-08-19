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
	// Token: 0x0200055E RID: 1374
	public sealed class TEHatRack : TileEntity, IFixLoadedData
	{
		// Token: 0x060040A3 RID: 16547 RVA: 0x005E1948 File Offset: 0x005DFB48
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

		// Token: 0x060040A4 RID: 16548 RVA: 0x005E19E8 File Offset: 0x005DFBE8
		public override void RegisterTileEntityID(int assignedID)
		{
			TEHatRack._myEntityID = (byte)assignedID;
		}

		// Token: 0x060040A5 RID: 16549 RVA: 0x005E19F1 File Offset: 0x005DFBF1
		public override TileEntity GenerateInstance()
		{
			return new TEHatRack();
		}

		// Token: 0x060040A6 RID: 16550 RVA: 0x005E19F8 File Offset: 0x005DFBF8
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			int number = TEHatRack.Place(x, y);
			NetMessage.SendData(86, -1, -1, null, number, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x060040A7 RID: 16551 RVA: 0x005E1A24 File Offset: 0x005DFC24
		public static int Place(int x, int y)
		{
			TEHatRack tEHatRack = new TEHatRack();
			tEHatRack.Position = new Point16(x, y);
			tEHatRack.ID = TileEntity.AssignNewID();
			tEHatRack.type = TEHatRack._myEntityID;
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByID[tEHatRack.ID] = tEHatRack;
				TileEntity.ByPosition[tEHatRack.Position] = tEHatRack;
			}
			return tEHatRack.ID;
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x005E1AB0 File Offset: 0x005DFCB0
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

		// Token: 0x060040A9 RID: 16553 RVA: 0x005E1B08 File Offset: 0x005DFD08
		public static void Kill(int x, int y)
		{
			TileEntity value;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out value) && value.type == TEHatRack._myEntityID)
			{
				object entityCreationLock = TileEntity.EntityCreationLock;
				lock (entityCreationLock)
				{
					TileEntity.ByID.Remove(value.ID);
					TileEntity.ByPosition.Remove(new Point16(x, y));
				}
			}
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x005E1B88 File Offset: 0x005DFD88
		public static int Find(int x, int y)
		{
			TileEntity value;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out value) && value.type == TEHatRack._myEntityID)
			{
				return value.ID;
			}
			return -1;
		}

		// Token: 0x060040AB RID: 16555 RVA: 0x005E1BC0 File Offset: 0x005DFDC0
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			BitsByte bitsByte = 0;
			bitsByte[0] = !this._items[0].IsAir;
			bitsByte[1] = !this._items[1].IsAir;
			bitsByte[2] = !this._dyes[0].IsAir;
			bitsByte[3] = !this._dyes[1].IsAir;
			writer.Write(bitsByte);
			for (int i = 0; i < 2; i++)
			{
				Item item = this._items[i];
				if (!item.IsAir)
				{
					ItemIO.WriteShortVanillaID(item, writer);
					ItemIO.WriteByteVanillaPrefix(item, writer);
					writer.Write((short)item.stack);
				}
			}
			for (int j = 0; j < 2; j++)
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

		// Token: 0x060040AC RID: 16556 RVA: 0x005E1CB8 File Offset: 0x005DFEB8
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

		// Token: 0x060040AD RID: 16557 RVA: 0x005E1D7C File Offset: 0x005DFF7C
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

		// Token: 0x060040AE RID: 16558 RVA: 0x005E1DE8 File Offset: 0x005DFFE8
		public unsafe static void Framing_CheckTile(int callX, int callY)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			Tile tileSafely = Framing.GetTileSafely(callX, callY);
			int num = callX - (int)(*tileSafely.frameX / 18 % 3);
			int num2 = callY - (int)(*tileSafely.frameY / 18 % 4);
			bool flag = false;
			for (int i = num; i < num + 3; i++)
			{
				for (int j = num2; j < num2 + 4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (!tile.active() || *tile.type != 475)
					{
						flag = true;
					}
				}
			}
			if (!WorldGen.SolidTileAllowBottomSlope(num, num2 + 4) || !WorldGen.SolidTileAllowBottomSlope(num + 1, num2 + 4) || !WorldGen.SolidTileAllowBottomSlope(num + 2, num2 + 4))
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			TEHatRack.Kill(num, num2);
			if (TileLoader.Drop(callX, callY, 475, true))
			{
				Item.NewItem(new EntitySource_TileBreak(num, num2, null), num * 16, num2 * 16, 48, 64, 3977, 1, false, 0, false, false);
			}
			WorldGen.destroyObject = true;
			for (int k = num; k < num + 3; k++)
			{
				for (int l = num2; l < num2 + 4; l++)
				{
					if (Main.tile[k, l].active() && *Main.tile[k, l].type == 475)
					{
						WorldGen.KillTile(k, l, false, false, false);
					}
				}
			}
			WorldGen.destroyObject = false;
		}

		// Token: 0x060040AF RID: 16559 RVA: 0x005E1F54 File Offset: 0x005E0154
		public unsafe void Draw(int tileLeftX, int tileTopY)
		{
			Player dollPlayer = this._dollPlayer;
			dollPlayer.direction = -1;
			dollPlayer.Male = true;
			if (*Framing.GetTileSafely(tileLeftX, tileTopY).frameX % 216 == 54)
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
			Vector2 vector = new Vector2((float)tileLeftX + 1.5f, (float)(tileTopY + 4)) * 16f;
			dollPlayer.direction *= -1;
			Vector2 vector2 = new Vector2((float)(-(float)dollPlayer.width / 2), (float)(-(float)dollPlayer.height - 6)) + new Vector2((float)(dollPlayer.direction * 14), -2f);
			dollPlayer.position = vector + vector2;
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
			vector2 = new Vector2((float)(-(float)dollPlayer.width / 2), (float)(-(float)dollPlayer.height - 6)) + new Vector2((float)(dollPlayer.direction * 12), 16f);
			dollPlayer.position = vector + vector2;
			Main.PlayerRenderer.DrawPlayer(Main.Camera, dollPlayer, dollPlayer.position, 0f, dollPlayer.fullRotationOrigin, 0f, 1f);
		}

		// Token: 0x060040B0 RID: 16560 RVA: 0x005E214C File Offset: 0x005E034C
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

		// Token: 0x060040B1 RID: 16561 RVA: 0x005E2180 File Offset: 0x005E0380
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

		// Token: 0x060040B2 RID: 16562 RVA: 0x005E2200 File Offset: 0x005E0400
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

		// Token: 0x060040B3 RID: 16563 RVA: 0x005E227C File Offset: 0x005E047C
		public unsafe static void OnPlayerInteraction(Player player, int clickX, int clickY)
		{
			int num = clickX - (int)(*Main.tile[clickX, clickY].frameX % 54 / 18);
			int num2 = clickY - (int)(*Main.tile[num, clickY].frameY / 18);
			int num3 = TEHatRack.Find(num, num2);
			if (num3 != -1)
			{
				num2++;
				num++;
				TileEntity.BasicOpenCloseInteraction(player, num, num2, num3);
			}
		}

		// Token: 0x060040B4 RID: 16564 RVA: 0x005E22E4 File Offset: 0x005E04E4
		public unsafe override void OnInventoryDraw(Player player, SpriteBatch spriteBatch)
		{
			if (*Main.tile[player.tileEntityAnchor.X, player.tileEntityAnchor.Y].type != 475)
			{
				player.tileEntityAnchor.Clear();
				Recipe.FindRecipes(false);
				return;
			}
			this.DrawInner(player, spriteBatch);
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x005E233B File Offset: 0x005E053B
		private void DrawInner(Player player, SpriteBatch spriteBatch)
		{
			Main.inventoryScale = 0.72f;
			this.DrawSlotPairSet(player, spriteBatch, 2, 0, 3.5f, 0.5f, 26);
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x005E2360 File Offset: 0x005E0560
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
						num4 = 27;
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

		// Token: 0x060040B7 RID: 16567 RVA: 0x005E2470 File Offset: 0x005E0670
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

		// Token: 0x060040B8 RID: 16568 RVA: 0x005E24E0 File Offset: 0x005E06E0
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

		// Token: 0x060040B9 RID: 16569 RVA: 0x005E2597 File Offset: 0x005E0797
		public static bool FitsHatRack(Item item)
		{
			return item.maxStack <= 1 && item.headSlot > 0;
		}

		// Token: 0x060040BA RID: 16570 RVA: 0x005E25B0 File Offset: 0x005E07B0
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

		// Token: 0x060040BB RID: 16571 RVA: 0x005E2690 File Offset: 0x005E0890
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

		// Token: 0x060040BC RID: 16572 RVA: 0x005E26D8 File Offset: 0x005E08D8
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

		// Token: 0x060040BD RID: 16573 RVA: 0x005E2728 File Offset: 0x005E0928
		public unsafe override bool IsTileValidForEntity(int x, int y)
		{
			return Main.tile[x, y].active() && *Main.tile[x, y].type == 475 && *Main.tile[x, y].frameY == 0 && *Main.tile[x, y].frameX % 54 == 0;
		}

		// Token: 0x060040BE RID: 16574 RVA: 0x005E279C File Offset: 0x005E099C
		public unsafe static bool IsBreakable(int clickX, int clickY)
		{
			int num = clickX - (int)(*Main.tile[clickX, clickY].frameX % 54 / 18);
			int num2 = clickY - (int)(*Main.tile[num, clickY].frameY / 18);
			int num3 = TEHatRack.Find(num, num2);
			return num3 == -1 || !(TileEntity.ByID[num3] as TEHatRack).ContainsItems();
		}

		// Token: 0x060040BF RID: 16575 RVA: 0x005E280C File Offset: 0x005E0A0C
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

		// Token: 0x060040C0 RID: 16576 RVA: 0x005E2848 File Offset: 0x005E0A48
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

		// Token: 0x060040C1 RID: 16577 RVA: 0x005E288F File Offset: 0x005E0A8F
		public override void SaveData(TagCompound tag)
		{
			tag["items"] = PlayerIO.SaveInventory(this._items);
			tag["dyes"] = PlayerIO.SaveInventory(this._dyes);
		}

		// Token: 0x060040C2 RID: 16578 RVA: 0x005E28BD File Offset: 0x005E0ABD
		public override void LoadData(TagCompound tag)
		{
			PlayerIO.LoadInventory(this._items, tag.GetList<TagCompound>("items"));
			PlayerIO.LoadInventory(this._dyes, tag.GetList<TagCompound>("dyes"));
		}

		// Token: 0x060040C3 RID: 16579 RVA: 0x005E28EC File Offset: 0x005E0AEC
		public override void NetSend(BinaryWriter writer)
		{
			BitsByte itemsBits = default(BitsByte);
			for (int i = 0; i < this._items.Length; i++)
			{
				itemsBits[i] = !this._items[i].IsAir;
				itemsBits[i + this._items.Length] = !this._dyes[i].IsAir;
			}
			writer.Write(itemsBits);
			for (int j = 0; j < this._items.Length; j++)
			{
				Item item = this._items[j];
				if (!item.IsAir)
				{
					ItemIO.Send(item, writer, true, false);
				}
			}
			for (int k = 0; k < this._dyes.Length; k++)
			{
				Item dye = this._dyes[k];
				if (!dye.IsAir)
				{
					ItemIO.Send(dye, writer, true, false);
				}
			}
		}

		// Token: 0x060040C4 RID: 16580 RVA: 0x005E29BC File Offset: 0x005E0BBC
		public override void NetReceive(BinaryReader reader)
		{
			BitsByte presentItems = reader.ReadByte();
			for (int i = 0; i < this._items.Length; i++)
			{
				this._items[i] = (presentItems[i] ? ItemIO.Receive(reader, true, false) : new Item());
			}
			for (int j = 0; j < this._dyes.Length; j++)
			{
				this._dyes[j] = (presentItems[j + this._items.Length] ? ItemIO.Receive(reader, true, false) : new Item());
			}
		}

		// Token: 0x04005889 RID: 22665
		private static byte _myEntityID;

		// Token: 0x0400588A RID: 22666
		private const int MyTileID = 475;

		// Token: 0x0400588B RID: 22667
		private const int entityTileWidth = 3;

		// Token: 0x0400588C RID: 22668
		private const int entityTileHeight = 4;

		// Token: 0x0400588D RID: 22669
		private Player _dollPlayer;

		// Token: 0x0400588E RID: 22670
		private Item[] _items;

		// Token: 0x0400588F RID: 22671
		private Item[] _dyes;

		// Token: 0x04005890 RID: 22672
		private static int hatTargetSlot;
	}
}
