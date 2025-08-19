using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.Chat;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.UI.Chat;
using Terraria.UI.Gamepad;

namespace Terraria.UI
{
	// Token: 0x020000AA RID: 170
	public class ItemSlot
	{
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x004A72FB File Offset: 0x004A54FB
		public static bool ShiftInUse
		{
			get
			{
				return Main.keyState.PressingShift() || ItemSlot.ShiftForcedOn;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x004A7310 File Offset: 0x004A5510
		public static bool ControlInUse
		{
			get
			{
				return Main.keyState.PressingControl();
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x004A731C File Offset: 0x004A551C
		public static bool NotUsingGamepad
		{
			get
			{
				return !PlayerInput.UsingGamepad;
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06001517 RID: 5399 RVA: 0x004A7328 File Offset: 0x004A5528
		// (remove) Token: 0x06001518 RID: 5400 RVA: 0x004A735C File Offset: 0x004A555C
		public static event ItemSlot.ItemTransferEvent OnItemTransferred;

		// Token: 0x06001519 RID: 5401 RVA: 0x004A7390 File Offset: 0x004A5590
		static ItemSlot()
		{
			Color[,] array = new Color[3, 3];
			array[0, 0] = new Color(50, 106, 64);
			array[0, 1] = new Color(46, 106, 98);
			array[0, 2] = new Color(45, 85, 105);
			array[1, 0] = new Color(35, 106, 126);
			array[1, 1] = new Color(50, 89, 140);
			array[1, 2] = new Color(57, 70, 128);
			array[2, 0] = new Color(122, 63, 83);
			array[2, 1] = new Color(104, 46, 85);
			array[2, 2] = new Color(84, 37, 87);
			ItemSlot.LoadoutSlotColors = array;
			ItemSlot.canFavoriteAt[0] = true;
			ItemSlot.canFavoriteAt[1] = true;
			ItemSlot.canFavoriteAt[2] = true;
			ItemSlot.canFavoriteAt[32] = true;
			ItemSlot.canShareAt[15] = true;
			ItemSlot.canShareAt[4] = true;
			ItemSlot.canShareAt[32] = true;
			ItemSlot.canShareAt[5] = true;
			ItemSlot.canShareAt[6] = true;
			ItemSlot.canShareAt[7] = true;
			ItemSlot.canShareAt[27] = true;
			ItemSlot.canShareAt[26] = true;
			ItemSlot.canShareAt[23] = true;
			ItemSlot.canShareAt[24] = true;
			ItemSlot.canShareAt[25] = true;
			ItemSlot.canShareAt[22] = true;
			ItemSlot.canShareAt[3] = true;
			ItemSlot.canShareAt[8] = true;
			ItemSlot.canShareAt[9] = true;
			ItemSlot.canShareAt[10] = true;
			ItemSlot.canShareAt[11] = true;
			ItemSlot.canShareAt[12] = true;
			ItemSlot.canShareAt[33] = true;
			ItemSlot.canShareAt[16] = true;
			ItemSlot.canShareAt[20] = true;
			ItemSlot.canShareAt[18] = true;
			ItemSlot.canShareAt[19] = true;
			ItemSlot.canShareAt[17] = true;
			ItemSlot.canShareAt[29] = true;
			ItemSlot.canShareAt[30] = true;
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x004A75CA File Offset: 0x004A57CA
		public static void AnnounceTransfer(ItemSlot.ItemTransferInfo info)
		{
			if (ItemSlot.OnItemTransferred != null)
			{
				ItemSlot.OnItemTransferred(info);
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x004A75E0 File Offset: 0x004A57E0
		public static void SetGlow(int index, float hue, bool chest)
		{
			if (!chest)
			{
				ItemSlot.inventoryGlowTime[index] = 300;
				ItemSlot.inventoryGlowHue[index] = hue;
				return;
			}
			if (hue < 0f)
			{
				ItemSlot.inventoryGlowTimeChest[index] = 0;
				ItemSlot.inventoryGlowHueChest[index] = 0f;
				return;
			}
			ItemSlot.inventoryGlowTimeChest[index] = 300;
			ItemSlot.inventoryGlowHueChest[index] = hue;
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x004A7638 File Offset: 0x004A5838
		public static void UpdateInterface()
		{
			if (!Main.playerInventory || Main.player[Main.myPlayer].talkNPC == -1)
			{
				ItemSlot._customCurrencyForSavings = -1;
			}
			for (int i = 0; i < ItemSlot.inventoryGlowTime.Length; i++)
			{
				if (ItemSlot.inventoryGlowTime[i] > 0)
				{
					ItemSlot.inventoryGlowTime[i]--;
					if (ItemSlot.inventoryGlowTime[i] == 0)
					{
						ItemSlot.inventoryGlowHue[i] = 0f;
					}
				}
			}
			for (int j = 0; j < ItemSlot.inventoryGlowTimeChest.Length; j++)
			{
				if (ItemSlot.inventoryGlowTimeChest[j] > 0)
				{
					ItemSlot.inventoryGlowTimeChest[j]--;
					if (ItemSlot.inventoryGlowTimeChest[j] == 0 || ItemSlot.forceClearGlowsOnChest)
					{
						ItemSlot.inventoryGlowHueChest[j] = 0f;
					}
				}
			}
			ItemSlot.forceClearGlowsOnChest = false;
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x004A76F4 File Offset: 0x004A58F4
		public static void Handle(ref Item inv, int context = 0)
		{
			ItemSlot.singleSlotArray[0] = inv;
			ItemSlot.Handle(ItemSlot.singleSlotArray, context, 0);
			inv = ItemSlot.singleSlotArray[0];
			Recipe.FindRecipes(false);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x004A771A File Offset: 0x004A591A
		public static void Handle(Item[] inv, int context = 0, int slot = 0)
		{
			ItemSlot.OverrideHover(inv, context, slot);
			ItemSlot.LeftClick(inv, context, slot);
			ItemSlot.RightClick(inv, context, slot);
			if (Main.mouseLeftRelease && Main.mouseLeft)
			{
				Recipe.FindRecipes(false);
			}
			ItemSlot.MouseHover(inv, context, slot);
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x004A7750 File Offset: 0x004A5950
		public static void OverrideHover(ref Item inv, int context = 0)
		{
			ItemSlot.singleSlotArray[0] = inv;
			ItemSlot.OverrideHover(ItemSlot.singleSlotArray, context, 0);
			inv = ItemSlot.singleSlotArray[0];
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x004A7770 File Offset: 0x004A5970
		public static bool isEquipLocked(int type)
		{
			return false;
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x004A7774 File Offset: 0x004A5974
		public static void OverrideHover(Item[] inv, int context = 0, int slot = 0)
		{
			Item item = inv[slot];
			if (!PlayerInput.UsingGamepad)
			{
				UILinkPointNavigator.SuggestUsage(ItemSlot.GetGamepadPointForSlot(inv, context, slot));
			}
			if (PlayerLoader.HoverSlot(Main.player[Main.myPlayer], inv, context, slot))
			{
				return;
			}
			bool shiftForcedOn = ItemSlot.ShiftForcedOn;
			if (ItemSlot.NotUsingGamepad && ItemSlot.Options.DisableLeftShiftTrashCan && !shiftForcedOn)
			{
				if (ItemSlot.ControlInUse && !ItemSlot.Options.DisableQuickTrash)
				{
					if (item.type > 0 && item.stack > 0 && !inv[slot].favorited)
					{
						switch (context)
						{
						case 0:
						case 1:
						case 2:
							if (Main.npcShop > 0 && !item.favorited)
							{
								Main.cursorOverride = 10;
								goto IL_4CC;
							}
							Main.cursorOverride = 6;
							goto IL_4CC;
						case 3:
						case 4:
						case 7:
							break;
						case 5:
						case 6:
							goto IL_4CC;
						default:
							if (context != 32)
							{
								goto IL_4CC;
							}
							break;
						}
						if (Main.player[Main.myPlayer].ItemSpace(item).CanTakeItemToPersonalInventory)
						{
							Main.cursorOverride = 6;
						}
					}
				}
				else if (ItemSlot.ShiftInUse)
				{
					bool flag = false;
					if (Main.LocalPlayer.tileEntityAnchor.IsInValidUseTileEntity())
					{
						flag = Main.LocalPlayer.tileEntityAnchor.GetTileEntity().OverrideItemSlotHover(inv, context, slot);
					}
					if (item.type > 0 && item.stack > 0 && !inv[slot].favorited && !flag)
					{
						switch (context)
						{
						case 0:
							if (Main.CreativeMenu.IsShowingResearchMenu())
							{
								Main.cursorOverride = 9;
								goto IL_4CC;
							}
							break;
						case 1:
						case 2:
							break;
						case 3:
						case 4:
						case 32:
							if (Main.player[Main.myPlayer].ItemSpace(item).CanTakeItemToPersonalInventory)
							{
								Main.cursorOverride = 8;
								goto IL_4CC;
							}
							goto IL_4CC;
						case 5:
						case 7:
						case 8:
						case 9:
						case 10:
						case 11:
						case 12:
						case 16:
						case 17:
						case 18:
						case 19:
						case 20:
						case 23:
						case 24:
						case 25:
						case 26:
						case 27:
						case 29:
						case 33:
							if (Main.player[Main.myPlayer].ItemSpace(inv[slot]).CanTakeItemToPersonalInventory)
							{
								Main.cursorOverride = 7;
								goto IL_4CC;
							}
							goto IL_4CC;
						case 6:
						case 13:
						case 14:
						case 15:
						case 21:
						case 22:
						case 28:
						case 30:
						case 31:
							goto IL_4CC;
						default:
							goto IL_4CC;
						}
						if (context == 0 && Main.InReforgeMenu)
						{
							if (item.Prefix(-3))
							{
								Main.cursorOverride = 9;
							}
						}
						else if (context == 0 && Main.InGuideCraftMenu)
						{
							if (item.material)
							{
								Main.cursorOverride = 9;
							}
						}
						else if (Main.player[Main.myPlayer].chest != -1 && ChestUI.TryPlacingInChest(item, true, context))
						{
							Main.cursorOverride = 9;
						}
					}
				}
			}
			else if (ItemSlot.ShiftInUse)
			{
				bool flag2 = false;
				if (Main.LocalPlayer.tileEntityAnchor.IsInValidUseTileEntity())
				{
					flag2 = Main.LocalPlayer.tileEntityAnchor.GetTileEntity().OverrideItemSlotHover(inv, context, slot);
				}
				if (item.type > 0 && item.stack > 0 && !inv[slot].favorited && !flag2)
				{
					switch (context)
					{
					case 0:
					case 1:
					case 2:
						if (Main.npcShop > 0 && !item.favorited)
						{
							if (!ItemSlot.Options.DisableQuickTrash)
							{
								Main.cursorOverride = 10;
							}
						}
						else if (context == 0 && Main.CreativeMenu.IsShowingResearchMenu())
						{
							Main.cursorOverride = 9;
						}
						else if (context == 0 && Main.InReforgeMenu)
						{
							if (item.Prefix(-3))
							{
								Main.cursorOverride = 9;
							}
						}
						else if (context == 0 && Main.InGuideCraftMenu)
						{
							if (item.material)
							{
								Main.cursorOverride = 9;
							}
						}
						else if (Main.player[Main.myPlayer].chest != -1)
						{
							if (ChestUI.TryPlacingInChest(item, true, context))
							{
								Main.cursorOverride = 9;
							}
						}
						else if (!ItemSlot.Options.DisableQuickTrash)
						{
							Main.cursorOverride = 6;
						}
						break;
					case 3:
					case 4:
					case 32:
						if (Main.player[Main.myPlayer].ItemSpace(item).CanTakeItemToPersonalInventory)
						{
							Main.cursorOverride = 8;
						}
						break;
					case 5:
					case 7:
					case 8:
					case 9:
					case 10:
					case 11:
					case 12:
					case 16:
					case 17:
					case 18:
					case 19:
					case 20:
					case 23:
					case 24:
					case 25:
					case 26:
					case 27:
					case 29:
					case 33:
						if (Main.player[Main.myPlayer].ItemSpace(inv[slot]).CanTakeItemToPersonalInventory)
						{
							Main.cursorOverride = 7;
						}
						break;
					}
				}
			}
			IL_4CC:
			if (Main.keyState.IsKeyDown(Main.FavoriteKey) && (ItemSlot.canFavoriteAt[context] || (Main.drawingPlayerChat && ItemSlot.canShareAt[context])))
			{
				if (item.type > 0 && item.stack > 0 && Main.drawingPlayerChat)
				{
					Main.cursorOverride = 2;
					return;
				}
				if (item.type > 0 && item.stack > 0)
				{
					Main.cursorOverride = 3;
				}
			}
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x004A7CB0 File Offset: 0x004A5EB0
		private static bool OverrideLeftClick(Item[] inv, int context = 0, int slot = 0)
		{
			if (Math.Abs(context) == 10 && ItemSlot.isEquipLocked(inv[slot].type))
			{
				return true;
			}
			if (Main.LocalPlayer.tileEntityAnchor.IsInValidUseTileEntity() && Main.LocalPlayer.tileEntityAnchor.GetTileEntity().OverrideItemSlotLeftClick(inv, context, slot))
			{
				return true;
			}
			Item item = inv[slot];
			if (ItemSlot.ShiftInUse && PlayerLoader.ShiftClickSlot(Main.player[Main.myPlayer], inv, context, slot))
			{
				return true;
			}
			if (Main.cursorOverride == 2)
			{
				if (ChatManager.AddChatText(FontAssets.MouseText.Value, ItemTagHandler.GenerateTag(item), Vector2.One))
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
				return true;
			}
			if (Main.cursorOverride == 3)
			{
				if (!ItemSlot.canFavoriteAt[Math.Abs(context)])
				{
					return false;
				}
				item.favorited = !item.favorited;
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				return true;
			}
			else if (Main.cursorOverride == 7)
			{
				if (context == 29)
				{
					Item item2 = inv[slot].Clone();
					item2.stack = item2.maxStack;
					item2.OnCreated(new JourneyDuplicationItemCreationContext());
					item2 = Main.player[Main.myPlayer].GetItem(Main.myPlayer, item2, GetItemSettings.InventoryEntityToPlayerInventorySettings);
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					return true;
				}
				inv[slot] = Main.player[Main.myPlayer].GetItem(Main.myPlayer, inv[slot], GetItemSettings.InventoryEntityToPlayerInventorySettings);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				return true;
			}
			else
			{
				if (Main.cursorOverride == 8)
				{
					inv[slot] = Main.player[Main.myPlayer].GetItem(Main.myPlayer, inv[slot], GetItemSettings.InventoryEntityToPlayerInventorySettings);
					if (Main.player[Main.myPlayer].chest > -1)
					{
						NetMessage.SendData(32, -1, -1, null, Main.player[Main.myPlayer].chest, (float)slot, 0f, 0f, 0, 0, 0);
					}
					return true;
				}
				if (Main.cursorOverride == 9)
				{
					if (Main.CreativeMenu.IsShowingResearchMenu())
					{
						Main.CreativeMenu.SwapItem(ref inv[slot]);
						SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
						Main.CreativeMenu.SacrificeItemInSacrificeSlot();
					}
					else if (Main.InReforgeMenu)
					{
						Utils.Swap<Item>(ref inv[slot], ref Main.reforgeItem);
						SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
					}
					else if (Main.InGuideCraftMenu)
					{
						Utils.Swap<Item>(ref inv[slot], ref Main.guideItem);
						Recipe.FindRecipes(false);
						SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
					}
					else
					{
						ChestUI.TryPlacingInChest(inv[slot], false, context);
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x004A7F58 File Offset: 0x004A6158
		public static void LeftClick(ref Item inv, int context = 0)
		{
			ItemSlot.singleSlotArray[0] = inv;
			ItemSlot.LeftClick(ItemSlot.singleSlotArray, context, 0);
			inv = ItemSlot.singleSlotArray[0];
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x004A7F78 File Offset: 0x004A6178
		private static bool IsAccessoryContext(int context)
		{
			int num = Math.Abs(context);
			return num - 10 <= 1;
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x004A7F9C File Offset: 0x004A619C
		public static void LeftClick(Item[] inv, int context = 0, int slot = 0)
		{
			Player player = Main.player[Main.myPlayer];
			bool flag = Main.mouseLeftRelease && Main.mouseLeft;
			if (flag)
			{
				if (ItemSlot.OverrideLeftClick(inv, context, slot))
				{
					return;
				}
				inv[slot].newAndShiny = false;
				if (ItemSlot.LeftClick_SellOrTrash(inv, context, slot) || player.itemAnimation != 0 || player.itemTime != 0)
				{
					return;
				}
			}
			int num = ItemSlot.PickItemMovementAction(inv, context, slot, Main.mouseItem);
			if (num != 3 && !flag)
			{
				return;
			}
			switch (num)
			{
			case 0:
				if (context == 6 && Main.mouseItem.type != 0)
				{
					inv[slot].SetDefaults(0);
				}
				if ((!ItemSlot.IsAccessoryContext(context) || ItemLoader.CanEquipAccessory(inv[slot], slot, context < 0)) && (context != 11 || inv[slot].FitsAccessoryVanitySlot) && (context >= 0 || LoaderManager.Get<AccessorySlotLoader>().CanAcceptItem(slot, inv[slot], context)))
				{
					if (Main.mouseItem.maxStack <= 1 || inv[slot].type != Main.mouseItem.type || inv[slot].stack == inv[slot].maxStack || Main.mouseItem.stack == Main.mouseItem.maxStack)
					{
						Utils.Swap<Item>(ref inv[slot], ref Main.mouseItem);
					}
					if (inv[slot].stack > 0)
					{
						ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(inv[slot], 21, context, inv[slot].stack));
					}
					else
					{
						ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(Main.mouseItem, context, 21, Main.mouseItem.stack));
					}
					if (inv[slot].stack > 0)
					{
						int num2 = Math.Abs(context);
						if (num2 <= 17)
						{
							if (num2 == 0)
							{
								AchievementsHelper.NotifyItemPickup(player, inv[slot]);
								goto IL_1CE;
							}
							if (num2 - 8 > 4 && num2 - 16 > 1)
							{
								goto IL_1CE;
							}
						}
						else if (num2 != 25 && num2 != 27 && num2 != 33)
						{
							goto IL_1CE;
						}
						AchievementsHelper.HandleOnEquip(player, inv[slot], context);
					}
					IL_1CE:
					if (inv[slot].type == 0 || inv[slot].stack < 1)
					{
						inv[slot] = new Item();
					}
					int numTransfered;
					if (Main.mouseItem.IsTheSameAs(inv[slot]) && inv[slot].stack != inv[slot].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack && ItemLoader.TryStackItems(inv[slot], Main.mouseItem, out numTransfered, false))
					{
						ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(inv[slot], 21, context, numTransfered));
					}
					if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
					{
						Main.mouseItem = new Item();
					}
					if (Main.mouseItem.type > 0 || inv[slot].type > 0)
					{
						Recipe.FindRecipes(false);
						SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
					}
					if (context == 3 && Main.netMode == 1)
					{
						NetMessage.SendData(32, -1, -1, null, player.chest, (float)slot, 0f, 0f, 0, 0, 0);
					}
				}
				break;
			case 1:
				if (Main.mouseItem.stack == 1 && Main.mouseItem.type > 0 && inv[slot].type > 0 && inv[slot].IsNotTheSameAs(Main.mouseItem) && (context != 11 || Main.mouseItem.FitsAccessoryVanitySlot))
				{
					if ((ItemSlot.IsAccessoryContext(context) && !ItemLoader.CanEquipAccessory(Main.mouseItem, slot, context < 0)) || (Math.Abs(context) == 11 && !Main.mouseItem.FitsAccessoryVanitySlot) || (context < 0 && !LoaderManager.Get<AccessorySlotLoader>().CanAcceptItem(slot, Main.mouseItem, context)))
					{
						break;
					}
					Utils.Swap<Item>(ref inv[slot], ref Main.mouseItem);
					SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
					if (inv[slot].stack > 0)
					{
						int num2 = Math.Abs(context);
						if (num2 <= 17)
						{
							if (num2 == 0)
							{
								AchievementsHelper.NotifyItemPickup(player, inv[slot]);
								goto IL_598;
							}
							if (num2 - 8 > 4 && num2 - 16 > 1)
							{
								goto IL_598;
							}
						}
						else if (num2 != 25 && num2 != 27 && num2 != 33)
						{
							goto IL_598;
						}
						AchievementsHelper.HandleOnEquip(player, inv[slot], context);
					}
				}
				else if (Main.mouseItem.type == 0 && inv[slot].type > 0)
				{
					Utils.Swap<Item>(ref inv[slot], ref Main.mouseItem);
					if (inv[slot].type == 0 || inv[slot].stack < 1)
					{
						inv[slot] = new Item();
					}
					if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
					{
						Main.mouseItem = new Item();
					}
					if (Main.mouseItem.type > 0 || inv[slot].type > 0)
					{
						Recipe.FindRecipes(false);
						SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
					}
				}
				else if (Main.mouseItem.type > 0 && inv[slot].type == 0 && (context != 11 || Main.mouseItem.FitsAccessoryVanitySlot))
				{
					if ((ItemSlot.IsAccessoryContext(context) && !ItemLoader.CanEquipAccessory(Main.mouseItem, slot, context < 0)) || (Math.Abs(context) == 11 && !Main.mouseItem.FitsAccessoryVanitySlot) || (context < 0 && !LoaderManager.Get<AccessorySlotLoader>().CanAcceptItem(slot, Main.mouseItem, context)))
					{
						break;
					}
					inv[slot] = ItemLoader.TransferWithLimit(Main.mouseItem, 1);
					Recipe.FindRecipes(false);
					SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
					if (inv[slot].stack > 0)
					{
						int num2 = Math.Abs(context);
						if (num2 <= 17)
						{
							if (num2 == 0)
							{
								AchievementsHelper.NotifyItemPickup(player, inv[slot]);
								goto IL_598;
							}
							if (num2 - 8 > 4 && num2 - 16 > 1)
							{
								goto IL_598;
							}
						}
						else if (num2 != 25 && num2 != 27 && num2 != 33)
						{
							goto IL_598;
						}
						AchievementsHelper.HandleOnEquip(player, inv[slot], context);
					}
				}
				IL_598:
				if ((context == 23 || context == 24) && Main.netMode == 1)
				{
					NetMessage.SendData(121, -1, -1, null, Main.myPlayer, (float)player.tileEntityAnchor.interactEntityID, (float)slot, 0f, 0, 0, 0);
				}
				if (context == 26 && Main.netMode == 1)
				{
					NetMessage.SendData(124, -1, -1, null, Main.myPlayer, (float)player.tileEntityAnchor.interactEntityID, (float)slot, 0f, 0, 0, 0);
				}
				break;
			case 2:
				if (Main.mouseItem.stack == 1 && Main.mouseItem.dye > 0 && inv[slot].type > 0 && inv[slot].type != Main.mouseItem.type)
				{
					Utils.Swap<Item>(ref inv[slot], ref Main.mouseItem);
					SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
					if (inv[slot].stack > 0)
					{
						int num2 = Math.Abs(context);
						if (num2 <= 17)
						{
							if (num2 == 0)
							{
								AchievementsHelper.NotifyItemPickup(player, inv[slot]);
								goto IL_80F;
							}
							if (num2 - 8 > 4 && num2 - 16 > 1)
							{
								goto IL_80F;
							}
						}
						else if (num2 != 25 && num2 != 27 && num2 != 33)
						{
							goto IL_80F;
						}
						AchievementsHelper.HandleOnEquip(player, inv[slot], context);
					}
				}
				else if (Main.mouseItem.type == 0 && inv[slot].type > 0)
				{
					Utils.Swap<Item>(ref inv[slot], ref Main.mouseItem);
					if (inv[slot].type == 0 || inv[slot].stack < 1)
					{
						inv[slot] = new Item();
					}
					if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
					{
						Main.mouseItem = new Item();
					}
					if (Main.mouseItem.type > 0 || inv[slot].type > 0)
					{
						Recipe.FindRecipes(false);
						SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
					}
				}
				else if (Main.mouseItem.dye > 0 && inv[slot].type == 0)
				{
					inv[slot] = ItemLoader.TransferWithLimit(Main.mouseItem, 1);
					Recipe.FindRecipes(false);
					SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
					if (inv[slot].stack > 0)
					{
						int num2 = Math.Abs(context);
						if (num2 <= 17)
						{
							if (num2 == 0)
							{
								AchievementsHelper.NotifyItemPickup(player, inv[slot]);
								goto IL_80F;
							}
							if (num2 - 8 > 4 && num2 - 16 > 1)
							{
								goto IL_80F;
							}
						}
						else if (num2 != 25 && num2 != 27 && num2 != 33)
						{
							goto IL_80F;
						}
						AchievementsHelper.HandleOnEquip(player, inv[slot], context);
					}
				}
				IL_80F:
				if (context == 25 && Main.netMode == 1)
				{
					NetMessage.SendData(121, -1, -1, null, Main.myPlayer, (float)player.tileEntityAnchor.interactEntityID, (float)slot, 1f, 0, 0, 0);
				}
				if (context == 27 && Main.netMode == 1)
				{
					NetMessage.SendData(124, -1, -1, null, Main.myPlayer, (float)player.tileEntityAnchor.interactEntityID, (float)slot, 1f, 0, 0, 0);
				}
				break;
			case 3:
				ItemSlot.HandleShopSlot(inv, slot, false, true);
				break;
			case 4:
				if (PlayerLoader.CanSellItem(player, player.TalkNPC, inv, Main.mouseItem))
				{
					Chest chest = Main.instance.shop[Main.npcShop];
					if (player.SellItem(Main.mouseItem, -1))
					{
						int soldItemIndex = chest.AddItemToShop(Main.mouseItem);
						Main.mouseItem.SetDefaults(0);
						SoundEngine.PlaySound(18, -1, -1, 1, 1f, 0f);
						ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(inv[slot], 21, 15, 0));
						PlayerLoader.PostSellItem(player, player.TalkNPC, chest.item, chest.item[soldItemIndex]);
					}
					else if (Main.mouseItem.value == 0)
					{
						int soldItemIndex2 = chest.AddItemToShop(Main.mouseItem);
						Main.mouseItem.SetDefaults(0);
						SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
						ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(inv[slot], 21, 15, 0));
						PlayerLoader.PostSellItem(player, player.TalkNPC, chest.item, chest.item[soldItemIndex2]);
					}
					Recipe.FindRecipes(false);
					Main.stackSplit = 9999;
				}
				break;
			case 5:
				if (Main.mouseItem.IsAir)
				{
					SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
					Main.mouseItem = inv[slot].Clone();
					Main.mouseItem.stack = Main.mouseItem.maxStack;
					Main.mouseItem.OnCreated(new JourneyDuplicationItemCreationContext());
					ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(inv[slot], 29, 21, 0));
				}
				break;
			}
			if (context > 2 && context != 5 && context != 32)
			{
				inv[slot].favorited = false;
			}
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x004A89BD File Offset: 0x004A6BBD
		private static bool DisableTrashing()
		{
			return ItemSlot.Options.DisableLeftShiftTrashCan && !PlayerInput.SteamDeckIsUsed;
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x004A89D0 File Offset: 0x004A6BD0
		private static bool LeftClick_SellOrTrash(Item[] inv, int context, int slot)
		{
			bool flag = false;
			bool result = false;
			if (ItemSlot.NotUsingGamepad && ItemSlot.Options.DisableLeftShiftTrashCan)
			{
				if (!ItemSlot.Options.DisableQuickTrash)
				{
					if ((context <= 4 && context >= 0) || context == 7 || context == 32)
					{
						flag = true;
					}
					if (ItemSlot.ControlInUse && flag)
					{
						ItemSlot.SellOrTrash(inv, context, slot);
						result = true;
					}
				}
			}
			else
			{
				if ((context <= 4 && context >= 0) || context == 32)
				{
					flag = (Main.player[Main.myPlayer].chest == -1);
				}
				if (ItemSlot.ShiftInUse && flag && (!ItemSlot.NotUsingGamepad || !ItemSlot.Options.DisableQuickTrash))
				{
					ItemSlot.SellOrTrash(inv, context, slot);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x004A8A64 File Offset: 0x004A6C64
		public static void SellOrTrash(Item[] inv, int context, int slot)
		{
			Player player = Main.player[Main.myPlayer];
			if (inv[slot].type <= 0)
			{
				return;
			}
			if (Main.npcShop > 0 && !inv[slot].favorited)
			{
				Chest chest = Main.instance.shop[Main.npcShop];
				if ((inv[slot].type < 71 || inv[slot].type > 74) && PlayerLoader.CanSellItem(player, player.TalkNPC, chest.item, inv[slot]))
				{
					if (player.SellItem(inv[slot], -1))
					{
						ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(inv[slot], context, 15, 0));
						int soldItemIndex = chest.AddItemToShop(inv[slot]);
						inv[slot].TurnToAir(false);
						SoundEngine.PlaySound(18, -1, -1, 1, 1f, 0f);
						Recipe.FindRecipes(false);
						PlayerLoader.PostSellItem(player, player.TalkNPC, chest.item, chest.item[soldItemIndex]);
						return;
					}
					if (inv[slot].value == 0)
					{
						ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(inv[slot], context, 15, 0));
						int soldItemIndex2 = chest.AddItemToShop(inv[slot]);
						inv[slot].TurnToAir(false);
						SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
						Recipe.FindRecipes(false);
						PlayerLoader.PostSellItem(player, player.TalkNPC, chest.item, chest.item[soldItemIndex2]);
						return;
					}
				}
			}
			else if (!inv[slot].favorited)
			{
				SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
				player.trashItem = inv[slot].Clone();
				ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(player.trashItem, context, 6, 0));
				inv[slot].TurnToAir(false);
				if (context == 3 && Main.netMode == 1)
				{
					NetMessage.SendData(32, -1, -1, null, player.chest, (float)slot, 0f, 0f, 0, 0, 0);
				}
				Recipe.FindRecipes(false);
			}
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x004A8C2C File Offset: 0x004A6E2C
		private static string GetOverrideInstructions(Item[] inv, int context, int slot)
		{
			Player player = Main.player[Main.myPlayer];
			TileEntity tileEntity = player.tileEntityAnchor.GetTileEntity();
			string instruction;
			if (tileEntity != null && tileEntity.TryGetItemGamepadOverrideInstructions(inv, context, slot, out instruction))
			{
				return instruction;
			}
			if (inv[slot].type > 0 && inv[slot].stack > 0)
			{
				if (!inv[slot].favorited)
				{
					switch (context)
					{
					case 0:
					case 1:
					case 2:
						if (Main.npcShop > 0 && !inv[slot].favorited)
						{
							return Lang.misc[75].Value;
						}
						if (Main.player[Main.myPlayer].chest != -1)
						{
							if (ChestUI.TryPlacingInChest(inv[slot], true, context))
							{
								return Lang.misc[76].Value;
							}
						}
						else if (Main.InGuideCraftMenu && inv[slot].material)
						{
							return Lang.misc[76].Value;
						}
						if (Main.mouseItem.type > 0 && (context == 0 || context == 1 || context == 2 || context == 6 || context == 15 || context == 7 || context == 4 || context == 32 || context == 3))
						{
							return null;
						}
						return Lang.misc[74].Value;
					case 3:
					case 4:
					case 32:
						if (Main.player[Main.myPlayer].ItemSpace(inv[slot]).CanTakeItemToPersonalInventory)
						{
							return Lang.misc[76].Value;
						}
						break;
					case 5:
					case 8:
					case 9:
					case 10:
					case 11:
					case 12:
					case 16:
					case 17:
					case 18:
					case 19:
					case 20:
					case 25:
					case 27:
					case 33:
						if (Main.player[Main.myPlayer].ItemSpace(inv[slot]).CanTakeItemToPersonalInventory)
						{
							return Lang.misc[68].Value;
						}
						break;
					}
				}
				bool flag = false;
				if (context <= 4 || context == 32)
				{
					flag = (player.chest == -1);
				}
				if (flag)
				{
					if (Main.npcShop > 0 && !inv[slot].favorited)
					{
						Chest chest = Main.instance.shop[Main.npcShop];
						if (inv[slot].type >= 71 && inv[slot].type <= 74)
						{
							return "";
						}
						return Lang.misc[75].Value;
					}
					else if (!inv[slot].favorited)
					{
						return Lang.misc[74].Value;
					}
				}
			}
			return "";
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x004A8EAC File Offset: 0x004A70AC
		public static int PickItemMovementAction(Item[] inv, int context, int slot, Item checkItem)
		{
			Player player = Main.player[Main.myPlayer];
			int result = -1;
			switch (context)
			{
			case -12:
			case 12:
			case 25:
			case 27:
			case 33:
				result = 2;
				break;
			case -11:
			case -10:
				if (checkItem.type == 0 || (checkItem.accessory && LoaderManager.Get<AccessorySlotLoader>().ModSlotCheck(checkItem, slot, context)))
				{
					result = 1;
				}
				break;
			case 0:
				result = 0;
				break;
			case 1:
				if (checkItem.type == 0 || checkItem.type == 71 || checkItem.type == 72 || checkItem.type == 73 || checkItem.type == 74)
				{
					result = 0;
				}
				break;
			case 2:
				if (checkItem.FitsAmmoSlot())
				{
					result = 0;
				}
				break;
			case 3:
				result = 0;
				break;
			case 4:
			case 32:
			{
				bool flag;
				Item[] chestinv;
				ChestUI.GetContainerUsageInfo(out flag, out chestinv);
				if (!ChestUI.IsBlockedFromTransferIntoChest(checkItem, chestinv))
				{
					result = 0;
				}
				break;
			}
			case 5:
				if (checkItem.Prefix(-3) || checkItem.type == 0)
				{
					result = 0;
				}
				break;
			case 6:
				result = 0;
				break;
			case 7:
				if (checkItem.material || checkItem.type == 0)
				{
					result = 0;
				}
				break;
			case 8:
				if (checkItem.type == 0 || (checkItem.headSlot > -1 && slot == 0) || (checkItem.bodySlot > -1 && slot == 1) || (checkItem.legSlot > -1 && slot == 2))
				{
					result = 1;
				}
				break;
			case 9:
				if (checkItem.type == 0 || (checkItem.headSlot > -1 && slot == 10) || (checkItem.bodySlot > -1 && slot == 11) || (checkItem.legSlot > -1 && slot == 12))
				{
					result = 1;
				}
				break;
			case 10:
				if (checkItem.type == 0 || (checkItem.accessory && !ItemSlot.AccCheck_ForLocalPlayer(Main.LocalPlayer.armor.Concat(AccessorySlotLoader.ModSlotPlayer(Main.LocalPlayer).exAccessorySlot).ToArray<Item>(), checkItem, slot)))
				{
					result = 1;
				}
				break;
			case 11:
				if (checkItem.type == 0 || (checkItem.accessory && !ItemSlot.AccCheck_ForLocalPlayer(Main.LocalPlayer.armor.Concat(AccessorySlotLoader.ModSlotPlayer(Main.LocalPlayer).exAccessorySlot).ToArray<Item>(), checkItem, slot)))
				{
					result = 1;
				}
				break;
			case 15:
				if (checkItem.type == 0 && inv[slot].type > 0)
				{
					result = 3;
				}
				else if (checkItem.type == inv[slot].type && checkItem.type > 0 && checkItem.stack < checkItem.maxStack && inv[slot].stack > 0)
				{
					result = 3;
				}
				else if (inv[slot].type == 0 && checkItem.type > 0 && (checkItem.type < 71 || checkItem.type > 74))
				{
					result = 4;
				}
				break;
			case 16:
				if (checkItem.type == 0 || Main.projHook[checkItem.shoot])
				{
					result = 1;
				}
				break;
			case 17:
				if (checkItem.type == 0 || (checkItem.mountType != -1 && !MountID.Sets.Cart[checkItem.mountType]))
				{
					result = 1;
				}
				break;
			case 18:
				if (checkItem.type == 0 || (checkItem.mountType != -1 && MountID.Sets.Cart[checkItem.mountType]))
				{
					result = 1;
				}
				break;
			case 19:
				if (checkItem.type == 0 || (checkItem.buffType > 0 && Main.vanityPet[checkItem.buffType] && !Main.lightPet[checkItem.buffType]))
				{
					result = 1;
				}
				break;
			case 20:
				if (checkItem.type == 0 || (checkItem.buffType > 0 && Main.lightPet[checkItem.buffType]))
				{
					result = 1;
				}
				break;
			case 23:
				if (checkItem.type == 0 || (checkItem.headSlot > 0 && slot == 0) || (checkItem.bodySlot > 0 && slot == 1) || (checkItem.legSlot > 0 && slot == 2))
				{
					result = 1;
				}
				break;
			case 24:
				if (checkItem.type == 0 || (checkItem.accessory && !ItemSlot.AccCheck(inv, checkItem, slot)))
				{
					result = 1;
				}
				break;
			case 26:
				if (checkItem.type == 0 || checkItem.headSlot > 0)
				{
					result = 1;
				}
				break;
			case 29:
				if (checkItem.type == 0 && inv[slot].type > 0)
				{
					result = 5;
				}
				break;
			}
			if (context == 30)
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x004A9366 File Offset: 0x004A7566
		public static void RightClick(ref Item inv, int context = 0)
		{
			ItemSlot.singleSlotArray[0] = inv;
			ItemSlot.RightClick(ItemSlot.singleSlotArray, context, 0);
			inv = ItemSlot.singleSlotArray[0];
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x004A9388 File Offset: 0x004A7588
		public static void RightClick(Item[] inv, int context = 0, int slot = 0)
		{
			Player player = Main.player[Main.myPlayer];
			inv[slot].newAndShiny = false;
			if (player.itemAnimation > 0)
			{
				return;
			}
			if (context == 15)
			{
				ItemSlot.HandleShopSlot(inv, slot, true, false);
				return;
			}
			if (!Main.mouseRight)
			{
				return;
			}
			if (context == 0 && Main.mouseRightRelease)
			{
				ItemSlot.TryItemSwap(inv[slot]);
			}
			if (context == 0 && ItemLoader.CanRightClick(inv[slot]))
			{
				if (Main.mouseRightRelease)
				{
					if (Main.ItemDropsDB.GetRulesForItemID(inv[slot].type).Any<IItemDropRule>())
					{
						ItemSlot.TryOpenContainer(inv[slot], player);
						return;
					}
					ItemLoader.RightClick(inv[slot], player);
				}
				return;
			}
			int num2 = Math.Abs(context);
			if (num2 <= 12)
			{
				if (num2 == 0 || num2 - 3 <= 1)
				{
					goto IL_FE;
				}
				switch (num2)
				{
				case 9:
				case 11:
					if (Main.mouseRightRelease)
					{
						ItemSlot.SwapVanityEquip(inv, context, slot, player);
						return;
					}
					return;
				case 10:
					goto IL_11C;
				case 12:
					break;
				default:
					goto IL_11C;
				}
			}
			else if (num2 <= 27)
			{
				if (num2 != 25 && num2 != 27)
				{
					goto IL_11C;
				}
			}
			else
			{
				if (num2 == 32)
				{
					goto IL_FE;
				}
				if (num2 != 33)
				{
					goto IL_11C;
				}
			}
			if (Main.mouseRightRelease)
			{
				ItemSlot.TryPickupDyeToCursor(context, inv, slot, player);
				return;
			}
			return;
			IL_FE:
			if (inv[slot].maxStack == 1)
			{
				if (Main.mouseRightRelease)
				{
					ItemSlot.SwapEquip(inv, context, slot);
					return;
				}
				return;
			}
			IL_11C:
			if (Main.stackSplit <= 1)
			{
				bool flag = true;
				bool flag2 = inv[slot].maxStack <= 1 && inv[slot].stack <= 1;
				if (context == 0 && flag2)
				{
					flag = false;
				}
				if (context == 3 && flag2)
				{
					flag = false;
				}
				if (context == 4 && flag2)
				{
					flag = false;
				}
				if (context == 32 && flag2)
				{
					flag = false;
				}
				if (flag)
				{
					int num = Main.superFastStack + 1;
					for (int i = 0; i < num; i++)
					{
						if (((Main.mouseItem.IsTheSameAs(inv[slot]) && ItemLoader.CanStack(Main.mouseItem, inv[slot])) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
						{
							ItemSlot.PickupItemIntoMouse(inv, context, slot, player);
							SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
							ItemSlot.RefreshStackSplitCooldown();
						}
					}
				}
			}
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x004A9594 File Offset: 0x004A7794
		public static void PickupItemIntoMouse(Item[] inv, int context, int slot, Player player)
		{
			if (Main.mouseItem.type == 0)
			{
				if (context == 29)
				{
					Main.mouseItem = inv[slot].Clone();
					Main.mouseItem.OnCreated(new JourneyDuplicationItemCreationContext());
				}
				else
				{
					Main.mouseItem = ItemLoader.TransferWithLimit(inv[slot], 1);
				}
				ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(inv[slot], context, 21, 0));
			}
			else
			{
				int num;
				ItemLoader.StackItems(Main.mouseItem, inv[slot], out num, context == 29, new int?(1));
			}
			if (inv[slot].stack <= 0)
			{
				inv[slot] = new Item();
			}
			Recipe.FindRecipes(false);
			if (context == 3 && Main.netMode == 1)
			{
				NetMessage.SendData(32, -1, -1, null, player.chest, (float)slot, 0f, 0f, 0, 0, 0);
			}
			if ((context == 23 || context == 24) && Main.netMode == 1)
			{
				NetMessage.SendData(121, -1, -1, null, Main.myPlayer, (float)player.tileEntityAnchor.interactEntityID, (float)slot, 0f, 0, 0, 0);
			}
			if (context == 25 && Main.netMode == 1)
			{
				NetMessage.SendData(121, -1, -1, null, Main.myPlayer, (float)player.tileEntityAnchor.interactEntityID, (float)slot, 1f, 0, 0, 0);
			}
			if (context == 26 && Main.netMode == 1)
			{
				NetMessage.SendData(124, -1, -1, null, Main.myPlayer, (float)player.tileEntityAnchor.interactEntityID, (float)slot, 0f, 0, 0, 0);
			}
			if (context == 27 && Main.netMode == 1)
			{
				NetMessage.SendData(124, -1, -1, null, Main.myPlayer, (float)player.tileEntityAnchor.interactEntityID, (float)slot, 1f, 0, 0, 0);
			}
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x004A971C File Offset: 0x004A791C
		public static void RefreshStackSplitCooldown()
		{
			if (Main.stackSplit == 0)
			{
				Main.stackSplit = 30;
				return;
			}
			Main.stackSplit = Main.stackDelay;
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x004A9738 File Offset: 0x004A7938
		private static void TryOpenContainer(Item item, Player player)
		{
			if (ItemID.Sets.BossBag[item.type])
			{
				player.OpenBossBag(item.type);
			}
			else if (ItemID.Sets.IsFishingCrate[item.type])
			{
				player.OpenFishingCrate(item.type);
			}
			else if (item.type == 3093)
			{
				player.OpenHerbBag(3093);
			}
			else if (item.type == 4345)
			{
				player.OpenCanofWorms(item.type);
			}
			else if (item.type == 4410)
			{
				player.OpenOyster(item.type);
			}
			else if (item.type == 1774)
			{
				player.OpenGoodieBag(1774);
			}
			else if (item.type == 3085)
			{
				if (!player.ConsumeItem(327, false, true))
				{
					return;
				}
				player.OpenLockBox(3085);
			}
			else if (item.type == 4879)
			{
				if (!player.HasItemInInventoryOrOpenVoidBag(329))
				{
					return;
				}
				player.OpenShadowLockbox(4879);
			}
			else if (item.type == 1869)
			{
				player.OpenPresent(1869);
			}
			else if (item.type == 599 && item.type == 600 && item.type == 601)
			{
				player.OpenLegacyPresent(item.type);
			}
			else
			{
				player.DropFromItem(item.type);
			}
			ItemLoader.RightClickCallHooks(item, player);
			if (ItemLoader.ConsumeItem(item, player))
			{
				item.stack--;
			}
			if (item.stack == 0)
			{
				item.SetDefaults(0);
			}
			SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			Main.stackSplit = 30;
			Main.mouseRightRelease = false;
			Recipe.FindRecipes(false);
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x004A9900 File Offset: 0x004A7B00
		private static void SwapVanityEquip(Item[] inv, int context, int slot, Player player)
		{
			int tSlot = slot - inv.Length / 2;
			if (Main.npcShop > 0 || ((inv[slot].type <= 0 || inv[slot].stack <= 0) && (inv[tSlot].type <= 0 || inv[tSlot].stack <= 0)))
			{
				return;
			}
			Item item = inv[tSlot];
			bool flag = context != 11 || item.FitsAccessoryVanitySlot || item.IsAir;
			if (flag && Math.Abs(context) == 11)
			{
				Item[] accessories = AccessorySlotLoader.ModSlotPlayer(player).exAccessorySlot;
				for (int invNum = 0; invNum < 2; invNum++)
				{
					Item[] checkInv = player.armor;
					int startIndex = 3;
					if (invNum == 1)
					{
						checkInv = accessories;
						startIndex = 0;
					}
					for (int i = startIndex; i < checkInv.Length / 2; i++)
					{
						if ((context != 11 || invNum != 0 || i != tSlot) && (context != -11 || invNum != 1 || i != tSlot) && ((inv[slot].wingSlot > 0 && checkInv[i].wingSlot > 0) || !ItemLoader.CanAccessoryBeEquippedWith(inv[slot], checkInv[i])))
						{
							flag = false;
						}
					}
				}
			}
			if (!flag || !ItemLoader.CanEquipAccessory(inv[slot], tSlot, context < 0))
			{
				return;
			}
			Utils.Swap<Item>(ref inv[slot], ref inv[tSlot]);
			SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			Recipe.FindRecipes(false);
			if (inv[slot].stack > 0)
			{
				if (context <= 17)
				{
					if (context == 0)
					{
						AchievementsHelper.NotifyItemPickup(player, inv[slot]);
						return;
					}
					if (context - 8 > 4 && context - 16 > 1)
					{
						return;
					}
				}
				else if (context != 25 && context != 27 && context != 33)
				{
					return;
				}
				AchievementsHelper.HandleOnEquip(player, inv[slot], context);
			}
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x004A9A8C File Offset: 0x004A7C8C
		private static void TryPickupDyeToCursor(int context, Item[] inv, int slot, Player player)
		{
			bool flag = false;
			if (!flag && ((Main.mouseItem.stack < Main.mouseItem.maxStack && Main.mouseItem.type > 0 && ItemLoader.CanStack(Main.mouseItem, inv[slot])) || Main.mouseItem.IsAir) && inv[slot].type > 0 && (Main.mouseItem.type == inv[slot].type || Main.mouseItem.IsAir))
			{
				flag = true;
				if (Main.mouseItem.IsAir)
				{
					Main.mouseItem = inv[slot].Clone();
				}
				else
				{
					int num;
					ItemLoader.StackItems(Main.mouseItem, inv[slot], out num, false, null);
				}
				inv[slot].SetDefaults(0);
				SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			}
			if (flag)
			{
				if (context == 25 && Main.netMode == 1)
				{
					NetMessage.SendData(121, -1, -1, null, Main.myPlayer, (float)player.tileEntityAnchor.interactEntityID, (float)slot, 1f, 0, 0, 0);
				}
				if (context == 27 && Main.netMode == 1)
				{
					NetMessage.SendData(124, -1, -1, null, Main.myPlayer, (float)player.tileEntityAnchor.interactEntityID, (float)slot, 1f, 0, 0, 0);
				}
			}
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x004A9BC4 File Offset: 0x004A7DC4
		private static void TryItemSwap(Item item)
		{
			int type = item.type;
			if (type > 5309)
			{
				if (type <= 5361)
				{
					switch (type)
					{
					case 5323:
						break;
					case 5324:
						item.ChangeItemType(5329);
						ItemSlot.AfterItemSwap(type, item.type);
						return;
					case 5325:
						goto IL_D2;
					case 5326:
					case 5327:
					case 5328:
						return;
					case 5329:
						item.ChangeItemType(5330);
						ItemSlot.AfterItemSwap(type, item.type);
						return;
					case 5330:
						item.ChangeItemType(5324);
						ItemSlot.AfterItemSwap(type, item.type);
						return;
					default:
						switch (type)
						{
						case 5358:
							item.ChangeItemType(5360);
							ItemSlot.AfterItemSwap(type, item.type);
							return;
						case 5359:
							item.ChangeItemType(5358);
							ItemSlot.AfterItemSwap(type, item.type);
							return;
						case 5360:
							item.ChangeItemType(5361);
							ItemSlot.AfterItemSwap(type, item.type);
							return;
						case 5361:
							item.ChangeItemType(5359);
							ItemSlot.AfterItemSwap(type, item.type);
							return;
						default:
							return;
						}
						break;
					}
				}
				else
				{
					if (type == 5391)
					{
						goto IL_172;
					}
					if (type == 5437)
					{
						item.ChangeItemType(5358);
						ItemSlot.AfterItemSwap(type, item.type);
						return;
					}
					switch (type)
					{
					case 5453:
						goto IL_1CA;
					case 5454:
						goto IL_1F6;
					case 5455:
						break;
					default:
						return;
					}
				}
				item.ChangeItemType((item.type == 5323) ? 5455 : 5323);
				ItemSlot.AfterItemSwap(type, item.type);
				return;
			}
			if (type <= 4346)
			{
				if (type != 4131)
				{
					if (type != 4346)
					{
						return;
					}
					goto IL_172;
				}
			}
			else
			{
				if (type == 4767)
				{
					goto IL_1CA;
				}
				if (type - 5059 <= 1)
				{
					item.ChangeItemType((item.type == 5059) ? 5060 : 5059);
					ItemSlot.AfterItemSwap(type, item.type);
					return;
				}
				if (type != 5309)
				{
					return;
				}
				goto IL_1F6;
			}
			IL_D2:
			item.ChangeItemType((item.type == 5325) ? 4131 : 5325);
			ItemSlot.AfterItemSwap(type, item.type);
			return;
			IL_172:
			item.ChangeItemType((item.type == 4346) ? 5391 : 4346);
			ItemSlot.AfterItemSwap(type, item.type);
			return;
			IL_1CA:
			item.ChangeItemType((item.type == 4767) ? 5453 : 4767);
			ItemSlot.AfterItemSwap(type, item.type);
			return;
			IL_1F6:
			item.ChangeItemType((item.type == 5309) ? 5454 : 5309);
			ItemSlot.AfterItemSwap(type, item.type);
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x004A9E6C File Offset: 0x004A806C
		private static void AfterItemSwap(int oldType, int newType)
		{
			if (newType == 5324 || newType == 5329 || newType == 5330 || newType == 4346 || newType == 5391 || newType == 5358 || newType == 5361 || newType == 5360 || newType == 5359)
			{
				SoundEngine.PlaySound(22, -1, -1, 1, 1f, 0f);
			}
			else
			{
				SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			}
			Main.stackSplit = 30;
			Main.mouseRightRelease = false;
			Recipe.FindRecipes(false);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x004A9F00 File Offset: 0x004A8100
		private static void HandleShopSlot(Item[] inv, int slot, bool rightClickIsValid, bool leftClickIsValid)
		{
			if (Main.cursorOverride == 2)
			{
				return;
			}
			Chest chest = Main.instance.shop[Main.npcShop];
			bool flag = (Main.mouseRight && rightClickIsValid) || (Main.mouseLeft && leftClickIsValid);
			if (Main.stackSplit > 1 || !flag || inv[slot].type <= 0 || ((!Main.mouseItem.IsTheSameAs(inv[slot]) || !ItemLoader.CanStack(Main.mouseItem, inv[slot])) && Main.mouseItem.type != 0))
			{
				return;
			}
			int num = Main.superFastStack + 1;
			Player localPlayer = Main.LocalPlayer;
			for (int i = 0; i < num; i++)
			{
				if (PlayerLoader.CanBuyItem(localPlayer, localPlayer.TalkNPC, inv, inv[slot]) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
				{
					long num2;
					long calcForBuying;
					localPlayer.GetItemExpectedPrice(inv[slot], out num2, out calcForBuying);
					if (localPlayer.BuyItem(calcForBuying, inv[slot].shopSpecialCurrency) && inv[slot].stack > 0)
					{
						if (i == 0)
						{
							if (inv[slot].value > 0)
							{
								SoundEngine.PlaySound(18, -1, -1, 1, 1f, 0f);
							}
							else
							{
								SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
							}
						}
						Item boughtItem = inv[slot].Clone();
						boughtItem.buyOnce = false;
						boughtItem.isAShopItem = false;
						if (boughtItem.shopSpecialCurrency != -1)
						{
							boughtItem.shopSpecialCurrency = -1;
							boughtItem.shopCustomPrice = null;
						}
						boughtItem.stack = 1;
						boughtItem.OnCreated(new BuyItemCreationContext(Main.mouseItem, localPlayer.TalkNPC));
						if (Main.mouseItem.type == 0)
						{
							Main.mouseItem = boughtItem;
						}
						else
						{
							int num3;
							ItemLoader.StackItems(Main.mouseItem, inv[slot], out num3, true, new int?(1));
						}
						if (!inv[slot].buyOnce)
						{
							Main.shopSellbackHelper.Add(inv[slot]);
						}
						ItemSlot.RefreshStackSplitCooldown();
						if (inv[slot].buyOnce)
						{
							Item item = inv[slot];
							int num3 = item.stack - 1;
							item.stack = num3;
							if (num3 <= 0)
							{
								inv[slot].SetDefaults(0);
							}
						}
						ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(Main.mouseItem, 15, 21, 0));
						PlayerLoader.PostBuyItem(localPlayer, localPlayer.TalkNPC, inv, Main.mouseItem);
					}
				}
			}
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x004AA134 File Offset: 0x004A8334
		public static void Draw(SpriteBatch spriteBatch, ref Item inv, int context, Vector2 position, Color lightColor = default(Color))
		{
			ItemSlot.singleSlotArray[0] = inv;
			ItemSlot.Draw(spriteBatch, ItemSlot.singleSlotArray, context, 0, position, lightColor);
			inv = ItemSlot.singleSlotArray[0];
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x004AA158 File Offset: 0x004A8358
		public static void Draw(SpriteBatch spriteBatch, Item[] inv, int context, int slot, Vector2 position, Color lightColor = default(Color))
		{
			Player player = Main.player[Main.myPlayer];
			Item item = inv[slot];
			float inventoryScale = Main.inventoryScale;
			Color color = Color.White;
			if (lightColor != Color.Transparent)
			{
				color = lightColor;
			}
			bool flag = false;
			int num = 0;
			int gamepadPointForSlot = ItemSlot.GetGamepadPointForSlot(inv, context, slot);
			if (PlayerInput.UsingGamepadUI)
			{
				flag = (UILinkPointNavigator.CurrentPoint == gamepadPointForSlot);
				if (PlayerInput.SettingsForUI.PreventHighlightsForGamepad)
				{
					flag = false;
				}
				if (context == 0)
				{
					num = player.DpadRadial.GetDrawMode(slot);
					if (num > 0 && !PlayerInput.CurrentProfile.UsingDpadHotbar())
					{
						num = 0;
					}
				}
			}
			Texture2D value = TextureAssets.InventoryBack.Value;
			Color color2 = Main.inventoryBack;
			bool flag2 = false;
			bool highlightThingsForMouse = PlayerInput.SettingsForUI.HighlightThingsForMouse;
			if (item.type > 0 && item.stack > 0 && item.favorited && context != 13 && context != 21 && context != 22 && context != 14)
			{
				value = TextureAssets.InventoryBack10.Value;
				if (context == 32)
				{
					value = TextureAssets.InventoryBack19.Value;
				}
			}
			else if (item.type > 0 && item.stack > 0 && ItemSlot.Options.HighlightNewItems && item.newAndShiny && context != 13 && context != 21 && context != 14 && context != 22)
			{
				value = TextureAssets.InventoryBack15.Value;
				float num2 = (float)Main.mouseTextColor / 255f;
				num2 = num2 * 0.2f + 0.8f;
				color2 = color2.MultiplyRGBA(new Color(num2, num2, num2));
			}
			else if (!highlightThingsForMouse && item.type > 0 && item.stack > 0 && num != 0 && context != 13 && context != 21 && context != 22)
			{
				value = TextureAssets.InventoryBack15.Value;
				float num3 = (float)Main.mouseTextColor / 255f;
				num3 = num3 * 0.2f + 0.8f;
				color2 = ((num != 1) ? color2.MultiplyRGBA(new Color(num3 / 2f, num3, num3 / 2f)) : color2.MultiplyRGBA(new Color(num3, num3 / 2f, num3 / 2f)));
			}
			else if (context == 0 && slot < 10)
			{
				value = TextureAssets.InventoryBack9.Value;
			}
			else
			{
				switch (context)
				{
				case -12:
				case -11:
				case -10:
				{
					AccessorySlotLoader accessorySlotLoader = LoaderManager.Get<AccessorySlotLoader>();
					ValueTuple<Texture2D, bool> backgroundTexture = accessorySlotLoader.GetBackgroundTexture(slot, context);
					value = backgroundTexture.Item1;
					if (!backgroundTexture.Item2)
					{
						color2 = ItemSlot.GetColorByLoadout(slot, Math.Abs(context));
					}
					accessorySlotLoader.Get(slot).BackgroundDrawColor(accessorySlotLoader.ContextToEnum(context), ref color2);
					break;
				}
				case 3:
					value = TextureAssets.InventoryBack5.Value;
					break;
				case 4:
				case 32:
					value = TextureAssets.InventoryBack2.Value;
					break;
				case 5:
				case 7:
					value = TextureAssets.InventoryBack4.Value;
					break;
				case 6:
					value = TextureAssets.InventoryBack7.Value;
					break;
				case 8:
				case 10:
					value = TextureAssets.InventoryBack13.Value;
					color2 = ItemSlot.GetColorByLoadout(slot, context);
					break;
				case 9:
				case 11:
					value = TextureAssets.InventoryBack13.Value;
					color2 = ItemSlot.GetColorByLoadout(slot, context);
					break;
				case 12:
					value = TextureAssets.InventoryBack13.Value;
					color2 = ItemSlot.GetColorByLoadout(slot, context);
					break;
				case 13:
				{
					byte b = 200;
					if (slot == Main.player[Main.myPlayer].selectedItem)
					{
						value = TextureAssets.InventoryBack14.Value;
						b = byte.MaxValue;
					}
					color2..ctor((int)b, (int)b, (int)b, (int)b);
					break;
				}
				case 14:
				case 21:
					flag2 = true;
					break;
				case 15:
					value = TextureAssets.InventoryBack6.Value;
					break;
				case 16:
				case 17:
				case 18:
				case 19:
				case 20:
					value = TextureAssets.InventoryBack3.Value;
					break;
				case 22:
					value = TextureAssets.InventoryBack4.Value;
					if (ItemSlot.DrawGoldBGForCraftingMaterial)
					{
						ItemSlot.DrawGoldBGForCraftingMaterial = false;
						value = TextureAssets.InventoryBack14.Value;
						float num4 = (float)color2.A / 255f;
						num4 = ((num4 >= 0.7f) ? 1f : Utils.GetLerpValue(0f, 0.7f, num4, true));
						color2 = Color.White * num4;
					}
					break;
				case 23:
				case 24:
				case 26:
					value = TextureAssets.InventoryBack8.Value;
					break;
				case 25:
				case 27:
				case 33:
					value = TextureAssets.InventoryBack12.Value;
					break;
				case 28:
					value = TextureAssets.InventoryBack7.Value;
					color2 = Color.White;
					break;
				case 29:
					color2..ctor(53, 69, 127, 255);
					value = TextureAssets.InventoryBack18.Value;
					break;
				case 30:
					flag2 = !flag;
					break;
				}
			}
			if ((context == 0 || context == 2) && ItemSlot.inventoryGlowTime[slot] > 0 && !inv[slot].favorited && !inv[slot].IsAir)
			{
				float num5 = Main.invAlpha / 255f;
				Color color4 = new Color(63, 65, 151, 255) * num5;
				Color value2 = Main.hslToRgb(ItemSlot.inventoryGlowHue[slot], 1f, 0.5f, byte.MaxValue) * num5;
				float num6 = (float)ItemSlot.inventoryGlowTime[slot] / 300f;
				num6 *= num6;
				color2 = Color.Lerp(color4, value2, num6 / 2f);
				value = TextureAssets.InventoryBack13.Value;
			}
			if ((context == 4 || context == 32 || context == 3) && ItemSlot.inventoryGlowTimeChest[slot] > 0 && !inv[slot].favorited && !inv[slot].IsAir)
			{
				float num7 = Main.invAlpha / 255f;
				Color value3 = new Color(130, 62, 102, 255) * num7;
				if (context == 3)
				{
					value3 = new Color(104, 52, 52, 255) * num7;
				}
				Color value4 = Main.hslToRgb(ItemSlot.inventoryGlowHueChest[slot], 1f, 0.5f, byte.MaxValue) * num7;
				float num8 = (float)ItemSlot.inventoryGlowTimeChest[slot] / 300f;
				num8 *= num8;
				color2 = Color.Lerp(value3, value4, num8 / 2f);
				value = TextureAssets.InventoryBack13.Value;
			}
			if (flag)
			{
				value = TextureAssets.InventoryBack14.Value;
				color2 = Color.White;
				if (item.favorited)
				{
					value = TextureAssets.InventoryBack17.Value;
				}
			}
			if (context == 28 && Main.MouseScreen.Between(position, position + value.Size() * inventoryScale) && !player.mouseInterface)
			{
				value = TextureAssets.InventoryBack14.Value;
				color2 = Color.White;
			}
			if (!flag2)
			{
				spriteBatch.Draw(value, position, null, color2, 0f, default(Vector2), inventoryScale, 0, 0f);
			}
			int num9 = -1;
			switch (context)
			{
			case -12:
				num9 = 1;
				break;
			case -11:
				num9 = 2;
				break;
			case -10:
				num9 = 11;
				break;
			default:
				switch (context)
				{
				case 8:
				case 23:
					if (slot == 0)
					{
						num9 = 0;
					}
					if (slot == 1)
					{
						num9 = 6;
					}
					if (slot == 2)
					{
						num9 = 12;
					}
					break;
				case 9:
					if (slot == 10)
					{
						num9 = 3;
					}
					if (slot == 11)
					{
						num9 = 9;
					}
					if (slot == 12)
					{
						num9 = 15;
					}
					break;
				case 10:
				case 24:
					num9 = 11;
					break;
				case 11:
					num9 = 2;
					break;
				case 12:
				case 25:
				case 27:
				case 33:
					num9 = 1;
					break;
				case 16:
					num9 = 4;
					break;
				case 17:
					num9 = 13;
					break;
				case 18:
					num9 = 7;
					break;
				case 19:
					num9 = 10;
					break;
				case 20:
					num9 = 17;
					break;
				case 26:
					num9 = 0;
					break;
				}
				break;
			}
			if ((item.type <= 0 || item.stack <= 0) && num9 != -1)
			{
				Texture2D value5 = TextureAssets.Extra[54].Value;
				Rectangle rectangle = value5.Frame(3, 6, num9 % 3, num9 / 3, 0, 0);
				rectangle.Width -= 2;
				rectangle.Height -= 2;
				bool flag3 = context - -12 <= 2;
				if (flag3)
				{
					LoaderManager.Get<AccessorySlotLoader>().DrawSlotTexture(value5, position + value.Size() / 2f * inventoryScale, rectangle, Color.White * 0.35f, 0f, rectangle.Size() / 2f, inventoryScale, 0, 0f, slot, context);
				}
				else
				{
					spriteBatch.Draw(value5, position + value.Size() / 2f * inventoryScale, new Rectangle?(rectangle), Color.White * 0.35f, 0f, rectangle.Size() / 2f, inventoryScale, 0, 0f);
				}
			}
			Vector2 vector = value.Size() * inventoryScale;
			if (item.type > 0 && item.stack > 0)
			{
				float scale = ItemSlot.DrawItemIcon(item, context, spriteBatch, position + vector / 2f, inventoryScale, 32f, color);
				if (ItemID.Sets.TrapSigned[item.type])
				{
					spriteBatch.Draw(TextureAssets.Wire.Value, position + new Vector2(40f, 40f) * inventoryScale, new Rectangle?(new Rectangle(4, 58, 8, 8)), color, 0f, new Vector2(4f), 1f, 0, 0f);
				}
				if (ItemID.Sets.DrawUnsafeIndicator[item.type])
				{
					Vector2 vector2 = new Vector2(-4f, -4f) * inventoryScale;
					Texture2D value6 = TextureAssets.Extra[258].Value;
					Rectangle rectangle2 = value6.Frame(1, 1, 0, 0, 0, 0);
					spriteBatch.Draw(value6, position + vector2 + new Vector2(40f, 40f) * inventoryScale, new Rectangle?(rectangle2), color, 0f, rectangle2.Size() / 2f, 1f, 0, 0f);
				}
				if (item.type == 5324 || item.type == 5329 || item.type == 5330)
				{
					Vector2 vector3 = new Vector2(2f, -6f) * inventoryScale;
					int type = item.type;
					if (type != 5324)
					{
						if (type != 5329)
						{
							if (type == 5330)
							{
								Texture2D value7 = TextureAssets.Extra[257].Value;
								Rectangle rectangle3 = value7.Frame(3, 1, 0, 0, 0, 0);
								spriteBatch.Draw(value7, position + vector3 + new Vector2(40f, 40f) * inventoryScale, new Rectangle?(rectangle3), color, 0f, rectangle3.Size() / 2f, 1f, 0, 0f);
							}
						}
						else
						{
							Texture2D value8 = TextureAssets.Extra[257].Value;
							Rectangle rectangle4 = value8.Frame(3, 1, 1, 0, 0, 0);
							spriteBatch.Draw(value8, position + vector3 + new Vector2(40f, 40f) * inventoryScale, new Rectangle?(rectangle4), color, 0f, rectangle4.Size() / 2f, 1f, 0, 0f);
						}
					}
					else
					{
						Texture2D value9 = TextureAssets.Extra[257].Value;
						Rectangle rectangle5 = value9.Frame(3, 1, 2, 0, 0, 0);
						spriteBatch.Draw(value9, position + vector3 + new Vector2(40f, 40f) * inventoryScale, new Rectangle?(rectangle5), color, 0f, rectangle5.Size() / 2f, 1f, 0, 0f);
					}
				}
				if (item.stack > 1)
				{
					ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, item.stack.ToString(), position + new Vector2(10f, 26f) * inventoryScale, color, 0f, Vector2.Zero, new Vector2(inventoryScale), -1f, inventoryScale);
				}
				int num10 = -1;
				if (context == 13)
				{
					if (item.DD2Summon)
					{
						for (int i = 0; i < 58; i++)
						{
							if (inv[i].type == 3822)
							{
								num10 += inv[i].stack;
							}
						}
						if (num10 >= 0)
						{
							num10++;
						}
					}
					if (item.useAmmo > 0)
					{
						int useAmmo = item.useAmmo;
						num10 = 0;
						for (int j = 0; j < 58; j++)
						{
							if (inv[j].stack > 0 && ItemLoader.CanChooseAmmo(item, inv[j], player))
							{
								num10 += inv[j].stack;
							}
						}
					}
					if (item.fishingPole > 0)
					{
						num10 = 0;
						for (int k = 0; k < 58; k++)
						{
							if (inv[k].bait > 0)
							{
								num10 += inv[k].stack;
							}
						}
					}
					if (item.tileWand > 0)
					{
						int tileWand = item.tileWand;
						num10 = 0;
						for (int l = 0; l < 58; l++)
						{
							if (inv[l].type == tileWand)
							{
								num10 += inv[l].stack;
							}
						}
					}
					if (item.type == 509 || item.type == 851 || item.type == 850 || item.type == 3612 || item.type == 3625 || item.type == 3611)
					{
						num10 = 0;
						for (int m = 0; m < 58; m++)
						{
							if (inv[m].type == 530)
							{
								num10 += inv[m].stack;
							}
						}
					}
				}
				if (num10 != -1)
				{
					ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, num10.ToString(), position + new Vector2(8f, 30f) * inventoryScale, color, 0f, Vector2.Zero, new Vector2(inventoryScale * 0.8f), -1f, inventoryScale);
				}
				if (context == 13)
				{
					string text = string.Concat(slot + 1);
					if (text == "10")
					{
						text = "0";
					}
					ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text, position + new Vector2(8f, 4f) * inventoryScale, color, 0f, Vector2.Zero, new Vector2(inventoryScale), -1f, inventoryScale);
				}
				if (context == 13 && item.potion)
				{
					Vector2 position2 = position + value.Size() * inventoryScale / 2f - TextureAssets.Cd.Value.Size() * inventoryScale / 2f;
					Color color3 = item.GetAlpha(color) * ((float)player.potionDelay / (float)player.potionDelayTime);
					spriteBatch.Draw(TextureAssets.Cd.Value, position2, null, color3, 0f, default(Vector2), scale, 0, 0f);
				}
				if ((Math.Abs(context) == 10 || context == 18) && ((item.expertOnly && !Main.expertMode) || (item.masterOnly && !Main.masterMode)))
				{
					Vector2 position3 = position + value.Size() * inventoryScale / 2f - TextureAssets.Cd.Value.Size() * inventoryScale / 2f;
					Color white = Color.White;
					spriteBatch.Draw(TextureAssets.Cd.Value, position3, null, white, 0f, default(Vector2), scale, 0, 0f);
				}
			}
			else if (context == 6)
			{
				Texture2D value10 = TextureAssets.Trash.Value;
				Vector2 position4 = position + value.Size() * inventoryScale / 2f - value10.Size() * inventoryScale / 2f;
				spriteBatch.Draw(value10, position4, null, new Color(100, 100, 100, 100), 0f, default(Vector2), inventoryScale, 0, 0f);
			}
			if (context == 0 && slot < 10)
			{
				float num11 = inventoryScale;
				string text2 = string.Concat(slot + 1);
				if (text2 == "10")
				{
					text2 = "0";
				}
				Color baseColor = Main.inventoryBack;
				int num12 = 0;
				if (Main.player[Main.myPlayer].selectedItem == slot)
				{
					baseColor = Color.White;
					baseColor.A = 200;
					num12 -= 2;
					num11 *= 1.4f;
				}
				ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text2, position + new Vector2(6f, (float)(4 + num12)) * inventoryScale, baseColor, 0f, Vector2.Zero, new Vector2(inventoryScale), -1f, inventoryScale);
			}
			if (gamepadPointForSlot != -1)
			{
				UILinkPointNavigator.SetPosition(gamepadPointForSlot, position + vector * 0.75f);
			}
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x004AB330 File Offset: 0x004A9530
		public static Color GetColorByLoadout(int slot, int context)
		{
			Color color = Color.White;
			Color color2;
			if (ItemSlot.TryGetSlotColor(Main.LocalPlayer.CurrentLoadoutIndex, context, out color2))
			{
				color = color2;
			}
			Color color3 = new Color(color.ToVector4() * Main.inventoryBack.ToVector4());
			float num = Utils.Remap((float)(Main.timeForVisualEffects - ItemSlot._lastTimeForVisualEffectsThatLoadoutWasChanged), 0f, 30f, 0.5f, 0f, true);
			return Color.Lerp(color3, Color.White, num * num * num);
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x004AB3AA File Offset: 0x004A95AA
		public static void RecordLoadoutChange()
		{
			ItemSlot._lastTimeForVisualEffectsThatLoadoutWasChanged = Main.timeForVisualEffects;
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x004AB3B8 File Offset: 0x004A95B8
		public static bool TryGetSlotColor(int loadoutIndex, int context, out Color color)
		{
			color = default(Color);
			if (loadoutIndex < 0 || loadoutIndex >= 3)
			{
				return false;
			}
			int num = -1;
			switch (context)
			{
			case 8:
			case 10:
				num = 0;
				break;
			case 9:
			case 11:
				num = 1;
				break;
			case 12:
				num = 2;
				break;
			}
			if (num == -1)
			{
				return false;
			}
			color = ItemSlot.LoadoutSlotColors[loadoutIndex, num];
			return true;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x004AB419 File Offset: 0x004A9619
		public static float ShiftHueByLoadout(float hue, int loadoutIndex)
		{
			return (hue + (float)loadoutIndex / 8f) % 1f;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x004AB42B File Offset: 0x004A962B
		public static Color GetLoadoutColor(int loadoutIndex)
		{
			return Main.hslToRgb(ItemSlot.ShiftHueByLoadout(0.41f, loadoutIndex), 0.7f, 0.5f, byte.MaxValue);
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x004AB44C File Offset: 0x004A964C
		public static float DrawItemIcon(Item item, int context, SpriteBatch spriteBatch, Vector2 screenPositionForItemCenter, float scale, float sizeLimit, Color environmentColor)
		{
			int num = item.type;
			if (num - 5358 <= 3 && context == 31)
			{
				num = 5437;
			}
			Main.instance.LoadItem(num);
			Texture2D value = TextureAssets.Item[num].Value;
			Rectangle frame = (Main.itemAnimations[num] == null) ? value.Frame(1, 1, 0, 0, 0, 0) : Main.itemAnimations[num].GetFrame(value, -1);
			Color itemLight;
			float finalDrawScale;
			ItemSlot.DrawItem_GetColorAndScale(item, scale, ref environmentColor, sizeLimit, ref frame, out itemLight, out finalDrawScale);
			SpriteEffects effects = 0;
			Vector2 origin = frame.Size() / 2f;
			if (ItemLoader.PreDrawInInventory(item, spriteBatch, screenPositionForItemCenter, frame, item.GetAlpha(itemLight), item.GetColor(environmentColor), origin, finalDrawScale))
			{
				spriteBatch.Draw(value, screenPositionForItemCenter, new Rectangle?(frame), item.GetAlpha(itemLight), 0f, origin, finalDrawScale, effects, 0f);
				if (item.color != Color.Transparent)
				{
					Color newColor = environmentColor;
					if (context == 13)
					{
						newColor.A = byte.MaxValue;
					}
					spriteBatch.Draw(value, screenPositionForItemCenter, new Rectangle?(frame), item.GetColor(newColor), 0f, origin, finalDrawScale, effects, 0f);
				}
				if (num - 5140 > 5)
				{
					if (num == 5146)
					{
						Texture2D value2 = TextureAssets.GlowMask[324].Value;
						Color color;
						color..ctor(Main.DiscoR, Main.DiscoG, Main.DiscoB);
						spriteBatch.Draw(value2, screenPositionForItemCenter, new Rectangle?(frame), color, 0f, origin, finalDrawScale, effects, 0f);
					}
				}
				else
				{
					Texture2D value3 = TextureAssets.GlowMask[(int)item.glowMask].Value;
					Color white = Color.White;
					spriteBatch.Draw(value3, screenPositionForItemCenter, new Rectangle?(frame), white, 0f, origin, finalDrawScale, effects, 0f);
				}
			}
			ItemLoader.PostDrawInInventory(item, spriteBatch, screenPositionForItemCenter, frame, item.GetAlpha(itemLight), item.GetColor(environmentColor), origin, finalDrawScale);
			return finalDrawScale;
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x004AB628 File Offset: 0x004A9828
		public static void DrawItem_GetColorAndScale(Item item, float scale, ref Color currentWhite, float sizeLimit, ref Rectangle frame, out Color itemLight, out float finalDrawScale)
		{
			itemLight = currentWhite;
			float scale2 = 1f;
			ItemSlot.GetItemLight(ref itemLight, ref scale2, item, false);
			float num = 1f;
			if ((float)frame.Width > sizeLimit || (float)frame.Height > sizeLimit)
			{
				num = ((frame.Width <= frame.Height) ? (sizeLimit / (float)frame.Height) : (sizeLimit / (float)frame.Width));
			}
			finalDrawScale = scale * num * scale2;
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x004AB6A0 File Offset: 0x004A98A0
		private static int GetGamepadPointForSlot(Item[] inv, int context, int slot)
		{
			Player localPlayer = Main.LocalPlayer;
			int result = -1;
			switch (context)
			{
			case 0:
			case 1:
			case 2:
				result = slot;
				break;
			case 3:
			case 4:
			case 32:
				result = 400 + slot;
				break;
			case 5:
				result = 303;
				break;
			case 6:
				result = 300;
				break;
			case 7:
				result = 1500;
				break;
			case 8:
			case 9:
			case 10:
			case 11:
			{
				int num2 = slot;
				if (num2 % 10 == 9 && !localPlayer.CanDemonHeartAccessoryBeShown())
				{
					num2--;
				}
				result = 100 + num2;
				break;
			}
			case 12:
				if (inv == localPlayer.dye)
				{
					int num3 = slot;
					if (num3 % 10 == 9 && !localPlayer.CanDemonHeartAccessoryBeShown())
					{
						num3--;
					}
					result = 120 + num3;
				}
				break;
			case 15:
				result = 2700 + slot;
				break;
			case 16:
				result = 184;
				break;
			case 17:
				result = 183;
				break;
			case 18:
				result = 182;
				break;
			case 19:
				result = 180;
				break;
			case 20:
				result = 181;
				break;
			case 22:
				if (UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeBig != -1)
				{
					result = 700 + UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeBig;
				}
				if (UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeSmall != -1)
				{
					result = 1500 + UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeSmall + 1;
				}
				break;
			case 23:
				result = 5100 + slot;
				break;
			case 24:
				result = 5100 + slot;
				break;
			case 25:
				result = 5108 + slot;
				break;
			case 26:
				result = 5000 + slot;
				break;
			case 27:
				result = 5002 + slot;
				break;
			case 29:
				result = 3000 + slot;
				if (UILinkPointNavigator.Shortcuts.CREATIVE_ItemSlotShouldHighlightAsSelected)
				{
					result = UILinkPointNavigator.CurrentPoint;
				}
				break;
			case 30:
				result = 15000 + slot;
				break;
			case 33:
				if (inv == localPlayer.miscDyes)
				{
					result = 185 + slot;
				}
				break;
			}
			return result;
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x004AB89E File Offset: 0x004A9A9E
		public static void MouseHover(int context = 0)
		{
			ItemSlot.singleSlotArray[0] = Main.HoverItem;
			ItemSlot.MouseHover(ItemSlot.singleSlotArray, context, 0);
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x004AB8B8 File Offset: 0x004A9AB8
		public static void MouseHover(ref Item inv, int context = 0)
		{
			ItemSlot.singleSlotArray[0] = inv;
			ItemSlot.MouseHover(ItemSlot.singleSlotArray, context, 0);
			inv = ItemSlot.singleSlotArray[0];
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x004AB8D8 File Offset: 0x004A9AD8
		public static void MouseHover(Item[] inv, int context = 0, int slot = 0)
		{
			if (context == 6 && Main.hoverItemName == null)
			{
				Main.hoverItemName = Lang.inter[3].Value;
			}
			if (inv[slot].type <= 0 || inv[slot].stack <= 0)
			{
				if (context == 10 || context == 11 || context == 24)
				{
					Main.hoverItemName = Lang.inter[9].Value;
				}
				if (context == 11)
				{
					Main.hoverItemName = Lang.inter[11].Value + " " + Main.hoverItemName;
				}
				if (context == 8 || context == 9 || context == 23 || context == 26)
				{
					if (slot == 0 || slot == 10 || context == 26)
					{
						Main.hoverItemName = Lang.inter[12].Value;
					}
					else if (slot == 1 || slot == 11)
					{
						Main.hoverItemName = Lang.inter[13].Value;
					}
					else if (slot == 2 || slot == 12)
					{
						Main.hoverItemName = Lang.inter[14].Value;
					}
					else if (slot >= 10)
					{
						Main.hoverItemName = Lang.inter[11].Value + " " + Main.hoverItemName;
					}
				}
				if (context == 12 || context == 25 || context == 27 || context == 33)
				{
					Main.hoverItemName = Lang.inter[57].Value;
				}
				if (context == 16)
				{
					Main.hoverItemName = Lang.inter[90].Value;
				}
				if (context == 17)
				{
					Main.hoverItemName = Lang.inter[91].Value;
				}
				if (context == 19)
				{
					Main.hoverItemName = Lang.inter[92].Value;
				}
				if (context == 18)
				{
					Main.hoverItemName = Lang.inter[93].Value;
				}
				if (context == 20)
				{
					Main.hoverItemName = Lang.inter[94].Value;
				}
				return;
			}
			ItemSlot._customCurrencyForSavings = inv[slot].shopSpecialCurrency;
			Main.hoverItemName = inv[slot].Name;
			if (inv[slot].stack > 1)
			{
				Main.hoverItemName = Main.hoverItemName + " (" + inv[slot].stack.ToString() + ")";
			}
			Main.HoverItem = inv[slot].Clone();
			Main.HoverItem.tooltipContext = context;
			if (context == 8 && slot <= 2)
			{
				Main.HoverItem.wornArmor = true;
				return;
			}
			if (context == 9 || context == 11)
			{
				Main.HoverItem.social = true;
				return;
			}
			if (context != 15)
			{
				return;
			}
			Main.HoverItem.buy = true;
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x004ABB33 File Offset: 0x004A9D33
		public static void SwapEquip(ref Item inv, int context = 0)
		{
			ItemSlot.singleSlotArray[0] = inv;
			ItemSlot.SwapEquip(ItemSlot.singleSlotArray, context, 0);
			inv = ItemSlot.singleSlotArray[0];
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x004ABB54 File Offset: 0x004A9D54
		public static void SwapEquip(Item[] inv, int context, int slot)
		{
			Player player = Main.player[Main.myPlayer];
			if (ItemSlot.isEquipLocked(inv[slot].type) || inv[slot].IsAir)
			{
				return;
			}
			if (inv[slot].dye > 0)
			{
				bool success;
				inv[slot] = ItemSlot.DyeSwap(inv[slot], out success);
				if (success)
				{
					Main.EquipPageSelected = 0;
					AchievementsHelper.HandleOnEquip(player, inv[slot], 12);
				}
			}
			else if (Main.projHook[inv[slot].shoot])
			{
				bool success;
				inv[slot] = ItemSlot.EquipSwap(inv[slot], player.miscEquips, 4, out success);
				if (success)
				{
					Main.EquipPageSelected = 2;
					AchievementsHelper.HandleOnEquip(player, inv[slot], 16);
				}
			}
			else if (inv[slot].mountType != -1 && !MountID.Sets.Cart[inv[slot].mountType])
			{
				bool success;
				inv[slot] = ItemSlot.EquipSwap(inv[slot], player.miscEquips, 3, out success);
				if (success)
				{
					Main.EquipPageSelected = 2;
					AchievementsHelper.HandleOnEquip(player, inv[slot], 17);
				}
			}
			else if (inv[slot].mountType != -1 && MountID.Sets.Cart[inv[slot].mountType])
			{
				bool success;
				inv[slot] = ItemSlot.EquipSwap(inv[slot], player.miscEquips, 2, out success);
				if (success)
				{
					Main.EquipPageSelected = 2;
				}
			}
			else if (inv[slot].buffType > 0 && Main.lightPet[inv[slot].buffType])
			{
				bool success;
				inv[slot] = ItemSlot.EquipSwap(inv[slot], player.miscEquips, 1, out success);
				if (success)
				{
					Main.EquipPageSelected = 2;
				}
			}
			else if (inv[slot].buffType > 0 && Main.vanityPet[inv[slot].buffType])
			{
				bool success;
				inv[slot] = ItemSlot.EquipSwap(inv[slot], player.miscEquips, 0, out success);
				if (success)
				{
					Main.EquipPageSelected = 2;
				}
			}
			else
			{
				Item item = inv[slot];
				bool success;
				inv[slot] = ItemSlot.ArmorSwap(inv[slot], out success);
				if (success)
				{
					Main.EquipPageSelected = 0;
					AchievementsHelper.HandleOnEquip(player, item, (item.accessory ? 10 : 8) * Math.Sign(context));
				}
			}
			Recipe.FindRecipes(false);
			if (context == 3 && Main.netMode == 1)
			{
				NetMessage.SendData(32, -1, -1, null, player.chest, (float)slot, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x004ABD60 File Offset: 0x004A9F60
		public static bool Equippable(ref Item inv, int context = 0)
		{
			ItemSlot.singleSlotArray[0] = inv;
			bool result = ItemSlot.Equippable(ItemSlot.singleSlotArray, context, 0);
			inv = ItemSlot.singleSlotArray[0];
			return result;
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x004ABD80 File Offset: 0x004A9F80
		public static bool Equippable(Item[] inv, int context, int slot)
		{
			Player player = Main.player[Main.myPlayer];
			return inv[slot].dye > 0 || Main.projHook[inv[slot].shoot] || inv[slot].mountType != -1 || (inv[slot].buffType > 0 && Main.lightPet[inv[slot].buffType]) || (inv[slot].buffType > 0 && Main.vanityPet[inv[slot].buffType]) || inv[slot].headSlot >= 0 || inv[slot].bodySlot >= 0 || inv[slot].legSlot >= 0 || inv[slot].accessory;
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x004ABE24 File Offset: 0x004AA024
		public static bool IsMiscEquipment(Item item)
		{
			return item.mountType != -1 || (item.buffType > 0 && Main.lightPet[item.buffType]) || (item.buffType > 0 && Main.vanityPet[item.buffType]) || Main.projHook[item.shoot];
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x004ABE78 File Offset: 0x004AA078
		public static bool AccCheck(Item[] itemCollection, Item item, int slot)
		{
			if (ItemSlot.isEquipLocked(item.type))
			{
				return true;
			}
			if (slot != -1)
			{
				if (itemCollection[slot].IsTheSameAs(item))
				{
					return false;
				}
				if (itemCollection[slot].wingSlot > 0 && item.wingSlot > 0)
				{
					return false;
				}
			}
			for (int i = 0; i < itemCollection.Length; i++)
			{
				if (slot < 10 && i < 10)
				{
					if (item.wingSlot > 0 && itemCollection[i].wingSlot > 0)
					{
						return true;
					}
					if (slot >= 10 && i >= 10 && item.wingSlot > 0 && itemCollection[i].wingSlot > 0)
					{
						return true;
					}
				}
				if (item.IsTheSameAs(itemCollection[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x004ABF18 File Offset: 0x004AA118
		private static Item DyeSwap(Item item, out bool success)
		{
			success = false;
			if (item.dye <= 0)
			{
				return item;
			}
			Player player = Main.player[Main.myPlayer];
			for (int i = 0; i < 10; i++)
			{
				if (player.dye[i].type == 0)
				{
					ItemSlot.dyeSlotCount = i;
					break;
				}
			}
			Item item2;
			if (ItemSlot.dyeSlotCount >= 10)
			{
				item2 = ItemSlot.ModSlotDyeSwap(item, out success);
				if (success)
				{
					return item2;
				}
				ItemSlot.dyeSlotCount = 0;
			}
			if (ItemSlot.dyeSlotCount < 0)
			{
				ItemSlot.dyeSlotCount = 9;
			}
			item2 = player.dye[ItemSlot.dyeSlotCount].Clone();
			player.dye[ItemSlot.dyeSlotCount] = item.Clone();
			ItemSlot.dyeSlotCount++;
			if (ItemSlot.dyeSlotCount >= 10)
			{
				ItemSlot.accSlotToSwapTo = 0;
			}
			SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			Recipe.FindRecipes(false);
			success = true;
			return item2;
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x004ABFF0 File Offset: 0x004AA1F0
		private static Item ArmorSwap(Item item, out bool success)
		{
			success = false;
			if (item.stack < 1)
			{
				return item;
			}
			if (item.headSlot == -1 && item.bodySlot == -1 && item.legSlot == -1 && !item.accessory)
			{
				return item;
			}
			Player player = Main.player[Main.myPlayer];
			int num = (item.vanity && !item.accessory) ? 10 : 0;
			item.favorited = false;
			Item result = item;
			if (item.headSlot != -1)
			{
				result = player.armor[num].Clone();
				player.armor[num] = item.Clone();
			}
			else if (item.bodySlot != -1)
			{
				result = player.armor[num + 1].Clone();
				player.armor[num + 1] = item.Clone();
			}
			else if (item.legSlot != -1)
			{
				result = player.armor[num + 2].Clone();
				player.armor[num + 2] = item.Clone();
			}
			else if (item.accessory && !ItemSlot.AccessorySwap(player, item, ref result))
			{
				return result;
			}
			SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			Recipe.FindRecipes(false);
			success = true;
			return result;
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x004AC10C File Offset: 0x004AA30C
		private static Item EquipSwap(Item item, Item[] inv, int slot, out bool success)
		{
			success = false;
			Player player = Main.player[Main.myPlayer];
			item.favorited = false;
			Item result = inv[slot].Clone();
			inv[slot] = item.Clone();
			SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			Recipe.FindRecipes(false);
			success = true;
			return result;
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x004AC160 File Offset: 0x004AA360
		public static void DrawMoney(SpriteBatch sb, string text, float shopx, float shopy, int[] coinsArray, bool horizontal = false)
		{
			Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, text, shopx, shopy + 40f, Color.White * ((float)Main.mouseTextColor / 255f), Color.Black, Vector2.Zero, 1f);
			if (horizontal)
			{
				for (int i = 0; i < 4; i++)
				{
					Main.instance.LoadItem(74 - i);
					if (i == 0)
					{
						int num2 = coinsArray[3 - i];
					}
					Vector2 position;
					position..ctor(shopx + ChatManager.GetStringSize(FontAssets.MouseText.Value, text, Vector2.One, -1f).X + (float)(24 * i) + 45f, shopy + 50f);
					sb.Draw(TextureAssets.Item[74 - i].Value, position, null, Color.White, 0f, TextureAssets.Item[74 - i].Value.Size() / 2f, 1f, 0, 0f);
					Utils.DrawBorderStringFourWay(sb, FontAssets.ItemStack.Value, coinsArray[3 - i].ToString(), position.X - 11f, position.Y, Color.White, Color.Black, new Vector2(0.3f), 0.75f);
				}
				return;
			}
			for (int j = 0; j < 4; j++)
			{
				Main.instance.LoadItem(74 - j);
				int num = (j == 0 && coinsArray[3 - j] > 99) ? -6 : 0;
				sb.Draw(TextureAssets.Item[74 - j].Value, new Vector2(shopx + 11f + (float)(24 * j), shopy + 75f), null, Color.White, 0f, TextureAssets.Item[74 - j].Value.Size() / 2f, 1f, 0, 0f);
				Utils.DrawBorderStringFourWay(sb, FontAssets.ItemStack.Value, coinsArray[3 - j].ToString(), shopx + (float)(24 * j) + (float)num, shopy + 75f, Color.White, Color.Black, new Vector2(0.3f), 0.75f);
			}
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x004AC39C File Offset: 0x004AA59C
		public static void DrawSavings(SpriteBatch sb, float shopx, float shopy, bool horizontal = false)
		{
			Player player = Main.player[Main.myPlayer];
			if (ItemSlot._customCurrencyForSavings != -1)
			{
				CustomCurrencyManager.DrawSavings(sb, ItemSlot._customCurrencyForSavings, shopx, shopy, horizontal);
				return;
			}
			bool overFlowing;
			long num = Utils.CoinsCount(out overFlowing, player.bank.item, Array.Empty<int>());
			long num2 = Utils.CoinsCount(out overFlowing, player.bank2.item, Array.Empty<int>());
			long num3 = Utils.CoinsCount(out overFlowing, player.bank3.item, Array.Empty<int>());
			long num4 = Utils.CoinsCount(out overFlowing, player.bank4.item, Array.Empty<int>());
			long num5 = Utils.CoinsCombineStacks(out overFlowing, new long[]
			{
				num,
				num2,
				num3,
				num4
			});
			if (num5 > 0L)
			{
				Texture2D itemTexture;
				Rectangle itemFrame;
				Main.GetItemDrawFrame(4076, out itemTexture, out itemFrame);
				Texture2D itemTexture2;
				Rectangle itemFrame2;
				Main.GetItemDrawFrame(3813, out itemTexture2, out itemFrame2);
				Texture2D itemTexture3;
				Rectangle rectangle;
				Main.GetItemDrawFrame(346, out itemTexture3, out rectangle);
				Texture2D itemTexture4;
				Main.GetItemDrawFrame(87, out itemTexture4, out rectangle);
				if (num4 > 0L)
				{
					sb.Draw(itemTexture, Utils.CenteredRectangle(new Vector2(shopx + 92f, shopy + 45f), itemFrame.Size() * 0.65f), null, Color.White);
				}
				if (num3 > 0L)
				{
					sb.Draw(itemTexture2, Utils.CenteredRectangle(new Vector2(shopx + 92f, shopy + 45f), itemFrame2.Size() * 0.65f), null, Color.White);
				}
				if (num2 > 0L)
				{
					sb.Draw(itemTexture3, Utils.CenteredRectangle(new Vector2(shopx + 80f, shopy + 50f), itemTexture3.Size() * 0.65f), null, Color.White);
				}
				if (num > 0L)
				{
					sb.Draw(itemTexture4, Utils.CenteredRectangle(new Vector2(shopx + 70f, shopy + 60f), itemTexture4.Size() * 0.65f), null, Color.White);
				}
				ItemSlot.DrawMoney(sb, Lang.inter[66].Value, shopx, shopy, Utils.CoinsSplit(num5), horizontal);
			}
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x004AC5C0 File Offset: 0x004AA7C0
		public static void GetItemLight(ref Color currentColor, Item item, bool outInTheWorld = false)
		{
			float scale = 1f;
			ItemSlot.GetItemLight(ref currentColor, ref scale, item, outInTheWorld);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x004AC5E0 File Offset: 0x004AA7E0
		public static void GetItemLight(ref Color currentColor, int type, bool outInTheWorld = false)
		{
			float scale = 1f;
			ItemSlot.GetItemLight(ref currentColor, ref scale, type, outInTheWorld);
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x004AC5FE File Offset: 0x004AA7FE
		public static void GetItemLight(ref Color currentColor, ref float scale, Item item, bool outInTheWorld = false)
		{
			ItemSlot.GetItemLight(ref currentColor, ref scale, item.type, outInTheWorld);
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x004AC610 File Offset: 0x004AA810
		public static Color GetItemLight(ref Color currentColor, ref float scale, int type, bool outInTheWorld = false)
		{
			if (type < 0)
			{
				return currentColor;
			}
			if (type == 662 || type == 663 || type == 5444 || type == 5450)
			{
				currentColor.R = (byte)Main.DiscoR;
				currentColor.G = (byte)Main.DiscoG;
				currentColor.B = (byte)Main.DiscoB;
				currentColor.A = byte.MaxValue;
			}
			if (type == 5128)
			{
				currentColor.R = (byte)Main.DiscoR;
				currentColor.G = (byte)Main.DiscoG;
				currentColor.B = (byte)Main.DiscoB;
				currentColor.A = byte.MaxValue;
			}
			else if (ItemID.Sets.ItemIconPulse[type])
			{
				scale = Main.essScale;
				currentColor.R = (byte)((float)currentColor.R * scale);
				currentColor.G = (byte)((float)currentColor.G * scale);
				currentColor.B = (byte)((float)currentColor.B * scale);
				currentColor.A = (byte)((float)currentColor.A * scale);
			}
			else if (type == 58 || type == 184 || type == 4143)
			{
				scale = Main.essScale * 0.25f + 0.75f;
				currentColor.R = (byte)((float)currentColor.R * scale);
				currentColor.G = (byte)((float)currentColor.G * scale);
				currentColor.B = (byte)((float)currentColor.B * scale);
				currentColor.A = (byte)((float)currentColor.A * scale);
			}
			return currentColor;
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x004AC77C File Offset: 0x004AA97C
		public static void DrawRadialCircular(SpriteBatch sb, Vector2 position, Player.SelectionRadial radial, Item[] items)
		{
			ItemSlot.CircularRadialOpacity = MathHelper.Clamp(ItemSlot.CircularRadialOpacity + ((PlayerInput.UsingGamepad && PlayerInput.Triggers.Current.RadialHotbar) ? 0.25f : -0.15f), 0f, 1f);
			if (ItemSlot.CircularRadialOpacity == 0f)
			{
				return;
			}
			Texture2D value = TextureAssets.HotbarRadial[2].Value;
			float num = ItemSlot.CircularRadialOpacity * 0.9f;
			float num2 = ItemSlot.CircularRadialOpacity * 1f;
			float num3 = (float)Main.mouseTextColor / 255f;
			float num4 = 1f - (1f - num3) * (1f - num3);
			num4 *= 0.785f;
			Color color = Color.White * num4 * num;
			value = TextureAssets.HotbarRadial[1].Value;
			float num5 = 6.2831855f / (float)radial.RadialCount;
			float num6 = -1.5707964f;
			for (int i = 0; i < radial.RadialCount; i++)
			{
				int num7 = radial.Bindings[i];
				Vector2 vector = new Vector2(150f, 0f).RotatedBy((double)(num6 + num5 * (float)i), default(Vector2)) * num2;
				float num8 = 0.85f;
				if (radial.SelectedBinding == i)
				{
					num8 = 1.7f;
				}
				sb.Draw(value, position + vector, null, color * num8, 0f, value.Size() / 2f, num2 * num8, 0, 0f);
				if (num7 != -1)
				{
					float inventoryScale = Main.inventoryScale;
					Main.inventoryScale = num2 * num8;
					ItemSlot.Draw(sb, items, 14, num7, position + vector + new Vector2(-26f * num2 * num8), Color.White);
					Main.inventoryScale = inventoryScale;
				}
			}
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x004AC954 File Offset: 0x004AAB54
		public static void DrawRadialQuicks(SpriteBatch sb, Vector2 position)
		{
			ItemSlot.QuicksRadialOpacity = MathHelper.Clamp(ItemSlot.QuicksRadialOpacity + ((PlayerInput.UsingGamepad && PlayerInput.Triggers.Current.RadialQuickbar) ? 0.25f : -0.15f), 0f, 1f);
			if (ItemSlot.QuicksRadialOpacity == 0f)
			{
				return;
			}
			Player player = Main.player[Main.myPlayer];
			Texture2D value = TextureAssets.HotbarRadial[2].Value;
			Texture2D value2 = TextureAssets.QuicksIcon.Value;
			float num = ItemSlot.QuicksRadialOpacity * 0.9f;
			float num2 = ItemSlot.QuicksRadialOpacity * 1f;
			float num3 = (float)Main.mouseTextColor / 255f;
			float num4 = 1f - (1f - num3) * (1f - num3);
			num4 *= 0.785f;
			Color color = Color.White * num4 * num;
			float num5 = 6.2831855f / (float)player.QuicksRadial.RadialCount;
			float num6 = -1.5707964f;
			Item item = player.QuickHeal_GetItemToUse();
			Item item2 = player.QuickMana_GetItemToUse();
			Item item3 = null;
			Item item4 = null;
			if (item == null)
			{
				item = new Item();
				item.SetDefaults(28);
			}
			if (item2 == null)
			{
				item2 = new Item();
				item2.SetDefaults(110);
			}
			if (item3 == null)
			{
				item3 = new Item();
				item3.SetDefaults(292);
			}
			if (item4 == null)
			{
				item4 = new Item();
				item4.SetDefaults(2428);
			}
			for (int i = 0; i < player.QuicksRadial.RadialCount; i++)
			{
				Item inv = item4;
				if (i == 1)
				{
					inv = item;
				}
				if (i == 2)
				{
					inv = item3;
				}
				if (i == 3)
				{
					inv = item2;
				}
				int num8 = player.QuicksRadial.Bindings[i];
				Vector2 vector = new Vector2(120f, 0f).RotatedBy((double)(num6 + num5 * (float)i), default(Vector2)) * num2;
				float num7 = 0.85f;
				if (player.QuicksRadial.SelectedBinding == i)
				{
					num7 = 1.7f;
				}
				sb.Draw(value, position + vector, null, color * num7, 0f, value.Size() / 2f, num2 * num7 * 1.3f, 0, 0f);
				float inventoryScale = Main.inventoryScale;
				Main.inventoryScale = num2 * num7;
				ItemSlot.Draw(sb, ref inv, 14, position + vector + new Vector2(-26f * num2 * num7), Color.White);
				Main.inventoryScale = inventoryScale;
				sb.Draw(value2, position + vector + new Vector2(34f, 20f) * 0.85f * num2 * num7, null, color * num7, 0f, value.Size() / 2f, num2 * num7 * 1.3f, 0, 0f);
			}
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x004ACC50 File Offset: 0x004AAE50
		public static void DrawRadialDpad(SpriteBatch sb, Vector2 position)
		{
			if (!PlayerInput.UsingGamepad || !PlayerInput.CurrentProfile.UsingDpadHotbar())
			{
				return;
			}
			Player player = Main.player[Main.myPlayer];
			if (player.chest != -1)
			{
				return;
			}
			Texture2D value = TextureAssets.HotbarRadial[0].Value;
			float num = (float)Main.mouseTextColor / 255f;
			float num2 = 1f - (1f - num) * (1f - num);
			num2 *= 0.785f;
			Color color = Color.White * num2;
			sb.Draw(value, position, null, color, 0f, value.Size() / 2f, Main.inventoryScale, 0, 0f);
			for (int i = 0; i < 4; i++)
			{
				int num3 = player.DpadRadial.Bindings[i];
				if (num3 != -1)
				{
					ItemSlot.Draw(sb, player.inventory, 14, num3, position + new Vector2((float)(value.Width / 3), 0f).RotatedBy((double)(-1.5707964f + 1.5707964f * (float)i), default(Vector2)) + new Vector2(-26f * Main.inventoryScale), Color.White);
				}
			}
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x004ACD8A File Offset: 0x004AAF8A
		public static string GetGamepadInstructions(ref Item inv, int context = 0)
		{
			ItemSlot.singleSlotArray[0] = inv;
			string gamepadInstructions = ItemSlot.GetGamepadInstructions(ItemSlot.singleSlotArray, context, 0);
			inv = ItemSlot.singleSlotArray[0];
			return gamepadInstructions;
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x004ACDAA File Offset: 0x004AAFAA
		public static bool CanExecuteCommand()
		{
			return PlayerInput.AllowExecutionOfGamepadInstructions;
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x004ACDB4 File Offset: 0x004AAFB4
		public static string GetGamepadInstructions(Item[] inv, int context = 0, int slot = 0)
		{
			Player player = Main.player[Main.myPlayer];
			string s = "";
			if (inv == null || inv[slot] == null || Main.mouseItem == null)
			{
				return s;
			}
			if (context == 0 || context == 1 || context == 2)
			{
				if (inv[slot].type > 0 && inv[slot].stack > 0)
				{
					if (Main.mouseItem.type > 0)
					{
						s += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
						if (inv[slot].type == Main.mouseItem.type && Main.mouseItem.stack < inv[slot].maxStack && inv[slot].maxStack > 1)
						{
							s += PlayerInput.BuildCommand(Lang.misc[55].Value, false, new List<string>[]
							{
								PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
							});
						}
					}
					else
					{
						if (context == 0 && player.chest == -1 && PlayerInput.AllowExecutionOfGamepadInstructions)
						{
							player.DpadRadial.ChangeBinding(slot);
						}
						s += PlayerInput.BuildCommand(Lang.misc[54].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
						if (inv[slot].maxStack > 1)
						{
							s += PlayerInput.BuildCommand(Lang.misc[55].Value, false, new List<string>[]
							{
								PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
							});
						}
					}
					if (inv[slot].maxStack == 1 && ItemSlot.Equippable(inv, context, slot))
					{
						s += PlayerInput.BuildCommand(Lang.misc[67].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"]
						});
						if (ItemSlot.CanExecuteCommand() && PlayerInput.Triggers.JustPressed.Grapple)
						{
							ItemSlot.SwapEquip(inv, context, slot);
							PlayerInput.LockGamepadButtons("Grapple");
							PlayerInput.SettingsForUI.TryRevertingToMouseMode();
						}
					}
					s += PlayerInput.BuildCommand(Lang.misc[83].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["SmartCursor"]
					});
					if (ItemSlot.CanExecuteCommand() && PlayerInput.Triggers.JustPressed.SmartCursor)
					{
						inv[slot].favorited = !inv[slot].favorited;
						PlayerInput.LockGamepadButtons("SmartCursor");
						PlayerInput.SettingsForUI.TryRevertingToMouseMode();
					}
				}
				else if (Main.mouseItem.type > 0)
				{
					s += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
					});
				}
			}
			if (context == 3 || context == 4 || context == 32)
			{
				if (inv[slot].type > 0 && inv[slot].stack > 0)
				{
					if (Main.mouseItem.type > 0)
					{
						s += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
						if (inv[slot].type == Main.mouseItem.type && Main.mouseItem.stack < inv[slot].maxStack && inv[slot].maxStack > 1)
						{
							s += PlayerInput.BuildCommand(Lang.misc[55].Value, false, new List<string>[]
							{
								PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
							});
						}
					}
					else
					{
						s += PlayerInput.BuildCommand(Lang.misc[54].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
						if (inv[slot].maxStack > 1)
						{
							s += PlayerInput.BuildCommand(Lang.misc[55].Value, false, new List<string>[]
							{
								PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
							});
						}
					}
					if (inv[slot].maxStack == 1 && ItemSlot.Equippable(inv, context, slot))
					{
						s += PlayerInput.BuildCommand(Lang.misc[67].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"]
						});
						if (ItemSlot.CanExecuteCommand() && PlayerInput.Triggers.JustPressed.Grapple)
						{
							ItemSlot.SwapEquip(inv, context, slot);
							PlayerInput.LockGamepadButtons("Grapple");
							PlayerInput.SettingsForUI.TryRevertingToMouseMode();
						}
					}
					if (context == 32)
					{
						s += PlayerInput.BuildCommand(Lang.misc[83].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["SmartCursor"]
						});
						if (ItemSlot.CanExecuteCommand() && PlayerInput.Triggers.JustPressed.SmartCursor)
						{
							inv[slot].favorited = !inv[slot].favorited;
							PlayerInput.LockGamepadButtons("SmartCursor");
							PlayerInput.SettingsForUI.TryRevertingToMouseMode();
						}
					}
				}
				else if (Main.mouseItem.type > 0)
				{
					s += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
					});
				}
			}
			if (context == 15)
			{
				if (inv[slot].type > 0 && inv[slot].stack > 0)
				{
					if (Main.mouseItem.type > 0)
					{
						if (inv[slot].type == Main.mouseItem.type && Main.mouseItem.stack < inv[slot].maxStack && inv[slot].maxStack > 1)
						{
							s += PlayerInput.BuildCommand(Lang.misc[91].Value, false, new List<string>[]
							{
								PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
							});
						}
					}
					else
					{
						s += PlayerInput.BuildCommand(Lang.misc[90].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"],
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
						});
					}
				}
				else if (Main.mouseItem.type > 0)
				{
					s += PlayerInput.BuildCommand(Lang.misc[92].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
					});
				}
			}
			if (context == 8 || context == 9 || context == 16 || context == 17 || context == 18 || context == 19 || context == 20)
			{
				if (inv[slot].type > 0 && inv[slot].stack > 0)
				{
					if (Main.mouseItem.type > 0)
					{
						if (ItemSlot.Equippable(ref Main.mouseItem, context))
						{
							s += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
							{
								PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
							});
						}
					}
					else if (context != 8 || !ItemSlot.isEquipLocked(inv[slot].type))
					{
						s += PlayerInput.BuildCommand(Lang.misc[54].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
					}
					if (context == 8 && slot >= 3)
					{
						bool flag = player.hideVisibleAccessory[slot];
						s += PlayerInput.BuildCommand(Lang.misc[flag ? 77 : 78].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"]
						});
						if (ItemSlot.CanExecuteCommand() && PlayerInput.Triggers.JustPressed.Grapple)
						{
							player.hideVisibleAccessory[slot] = !player.hideVisibleAccessory[slot];
							SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
							if (Main.netMode == 1)
							{
								NetMessage.SendData(4, -1, -1, null, Main.myPlayer, 0f, 0f, 0f, 0, 0, 0);
							}
							PlayerInput.LockGamepadButtons("Grapple");
							PlayerInput.SettingsForUI.TryRevertingToMouseMode();
						}
					}
					if ((context == 16 || context == 17 || context == 18 || context == 19 || context == 20) && slot < 2)
					{
						bool flag2 = player.hideMisc[slot];
						s += PlayerInput.BuildCommand(Lang.misc[flag2 ? 77 : 78].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"]
						});
						if (ItemSlot.CanExecuteCommand() && PlayerInput.Triggers.JustPressed.Grapple)
						{
							if (slot == 0)
							{
								player.TogglePet();
							}
							if (slot == 1)
							{
								player.ToggleLight();
							}
							SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
							if (Main.netMode == 1)
							{
								NetMessage.SendData(4, -1, -1, null, Main.myPlayer, 0f, 0f, 0f, 0, 0, 0);
							}
							PlayerInput.LockGamepadButtons("Grapple");
							PlayerInput.SettingsForUI.TryRevertingToMouseMode();
						}
					}
				}
				else
				{
					if (Main.mouseItem.type > 0 && ItemSlot.Equippable(ref Main.mouseItem, context))
					{
						s += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
					}
					if (context == 8 && slot >= 3)
					{
						int num = slot;
						if (num % 10 == 8 && !player.CanDemonHeartAccessoryBeShown())
						{
							num++;
						}
						bool flag3 = player.hideVisibleAccessory[num];
						s += PlayerInput.BuildCommand(Lang.misc[flag3 ? 77 : 78].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"]
						});
						if (ItemSlot.CanExecuteCommand() && PlayerInput.Triggers.JustPressed.Grapple)
						{
							player.hideVisibleAccessory[num] = !player.hideVisibleAccessory[num];
							SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
							if (Main.netMode == 1)
							{
								NetMessage.SendData(4, -1, -1, null, Main.myPlayer, 0f, 0f, 0f, 0, 0, 0);
							}
							PlayerInput.LockGamepadButtons("Grapple");
							PlayerInput.SettingsForUI.TryRevertingToMouseMode();
						}
					}
					if ((context == 16 || context == 17 || context == 18 || context == 19 || context == 20) && slot < 2)
					{
						bool flag4 = player.hideMisc[slot];
						s += PlayerInput.BuildCommand(Lang.misc[flag4 ? 77 : 78].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"]
						});
						if (ItemSlot.CanExecuteCommand() && PlayerInput.Triggers.JustPressed.Grapple)
						{
							if (slot == 0)
							{
								player.TogglePet();
							}
							if (slot == 1)
							{
								player.ToggleLight();
							}
							Main.mouseLeftRelease = false;
							SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
							if (Main.netMode == 1)
							{
								NetMessage.SendData(4, -1, -1, null, Main.myPlayer, 0f, 0f, 0f, 0, 0, 0);
							}
							PlayerInput.LockGamepadButtons("Grapple");
							PlayerInput.SettingsForUI.TryRevertingToMouseMode();
						}
					}
				}
			}
			if (context <= 18)
			{
				switch (context)
				{
				case 5:
				case 7:
				{
					bool flag5 = false;
					if (context == 5)
					{
						flag5 = (Main.mouseItem.Prefix(-3) || Main.mouseItem.type == 0);
					}
					if (context == 7)
					{
						flag5 = Main.mouseItem.material;
					}
					if (inv[slot].type > 0 && inv[slot].stack > 0)
					{
						if (Main.mouseItem.type > 0)
						{
							if (flag5)
							{
								s += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
								{
									PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
								});
							}
						}
						else
						{
							s += PlayerInput.BuildCommand(Lang.misc[54].Value, false, new List<string>[]
							{
								PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
							});
						}
					}
					else if (Main.mouseItem.type > 0 && flag5)
					{
						s += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
					}
					return s;
				}
				case 6:
					if (inv[slot].type > 0 && inv[slot].stack > 0)
					{
						s = ((Main.mouseItem.type <= 0) ? (s + PlayerInput.BuildCommand(Lang.misc[54].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						})) : (s + PlayerInput.BuildCommand(Lang.misc[74].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						})));
					}
					else if (Main.mouseItem.type > 0)
					{
						s += PlayerInput.BuildCommand(Lang.misc[74].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
					}
					return s;
				default:
					if (context != 12)
					{
						if (context != 18)
						{
							goto IL_124D;
						}
						if (inv[slot].type > 0 && inv[slot].stack > 0)
						{
							if (Main.mouseItem.type > 0)
							{
								if (Main.mouseItem.dye > 0)
								{
									s += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
									{
										PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
									});
								}
							}
							else
							{
								s += PlayerInput.BuildCommand(Lang.misc[54].Value, false, new List<string>[]
								{
									PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
								});
							}
						}
						else if (Main.mouseItem.type > 0 && Main.mouseItem.dye > 0)
						{
							s += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
							{
								PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
							});
						}
						bool enabledSuperCart = player.enabledSuperCart;
						s += PlayerInput.BuildCommand(Language.GetTextValue((!enabledSuperCart) ? "UI.EnableSuperCart" : "UI.DisableSuperCart"), false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"]
						});
						if (ItemSlot.CanExecuteCommand() && PlayerInput.Triggers.JustPressed.Grapple)
						{
							player.enabledSuperCart = !player.enabledSuperCart;
							SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
							if (Main.netMode == 1)
							{
								NetMessage.SendData(4, -1, -1, null, Main.myPlayer, 0f, 0f, 0f, 0, 0, 0);
							}
							PlayerInput.LockGamepadButtons("Grapple");
							PlayerInput.SettingsForUI.TryRevertingToMouseMode();
						}
						return s;
					}
					break;
				}
			}
			else if (context != 25 && context != 27 && context != 33)
			{
				goto IL_124D;
			}
			if (inv[slot].type > 0 && inv[slot].stack > 0)
			{
				if (Main.mouseItem.type > 0)
				{
					if (Main.mouseItem.dye > 0)
					{
						s += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
					}
				}
				else
				{
					s += PlayerInput.BuildCommand(Lang.misc[54].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
					});
				}
				if (context == 12 || context == 25 || context == 27 || context == 33)
				{
					int num2 = -1;
					if (inv == player.dye)
					{
						num2 = slot;
					}
					if (inv == player.miscDyes)
					{
						num2 = 10 + slot;
					}
					if (num2 != -1)
					{
						if (num2 < 10)
						{
							bool flag6 = player.hideVisibleAccessory[slot];
							s += PlayerInput.BuildCommand(Lang.misc[flag6 ? 77 : 78].Value, false, new List<string>[]
							{
								PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"]
							});
							if (ItemSlot.CanExecuteCommand() && PlayerInput.Triggers.JustPressed.Grapple)
							{
								player.hideVisibleAccessory[slot] = !player.hideVisibleAccessory[slot];
								SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
								if (Main.netMode == 1)
								{
									NetMessage.SendData(4, -1, -1, null, Main.myPlayer, 0f, 0f, 0f, 0, 0, 0);
								}
								PlayerInput.LockGamepadButtons("Grapple");
								PlayerInput.SettingsForUI.TryRevertingToMouseMode();
							}
						}
						else
						{
							bool flag7 = player.hideMisc[slot];
							s += PlayerInput.BuildCommand(Lang.misc[flag7 ? 77 : 78].Value, false, new List<string>[]
							{
								PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"]
							});
							if (ItemSlot.CanExecuteCommand() && PlayerInput.Triggers.JustPressed.Grapple)
							{
								player.hideMisc[slot] = !player.hideMisc[slot];
								SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
								if (Main.netMode == 1)
								{
									NetMessage.SendData(4, -1, -1, null, Main.myPlayer, 0f, 0f, 0f, 0, 0, 0);
								}
								PlayerInput.LockGamepadButtons("Grapple");
								PlayerInput.SettingsForUI.TryRevertingToMouseMode();
							}
						}
					}
				}
			}
			else if (Main.mouseItem.type > 0 && Main.mouseItem.dye > 0)
			{
				s += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
				});
			}
			return s;
			IL_124D:
			string overrideInstructions = ItemSlot.GetOverrideInstructions(inv, context, slot);
			bool flag8 = Main.mouseItem.type > 0 && (context == 0 || context == 1 || context == 2 || context == 6 || context == 15 || context == 7 || context == 4 || context == 32 || context == 3);
			if (context != 8 || !ItemSlot.isEquipLocked(inv[slot].type))
			{
				if (flag8 && string.IsNullOrEmpty(overrideInstructions))
				{
					s += PlayerInput.BuildCommand(Lang.inter[121].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["SmartSelect"]
					});
					if (ItemSlot.CanExecuteCommand() && PlayerInput.Triggers.JustPressed.SmartSelect)
					{
						player.DropSelectedItem();
						PlayerInput.LockGamepadButtons("SmartSelect");
						PlayerInput.SettingsForUI.TryRevertingToMouseMode();
					}
				}
				else if (!string.IsNullOrEmpty(overrideInstructions))
				{
					ItemSlot.ShiftForcedOn = true;
					int cursorOverride = Main.cursorOverride;
					ItemSlot.OverrideHover(inv, context, slot);
					if (-1 != Main.cursorOverride)
					{
						s += PlayerInput.BuildCommand(overrideInstructions, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["SmartSelect"]
						});
						if (ItemSlot.CanDoSimulatedClickAction() && ItemSlot.CanExecuteCommand() && PlayerInput.Triggers.JustPressed.SmartSelect)
						{
							bool mouseLeft = Main.mouseLeft;
							Main.mouseLeft = true;
							ItemSlot.LeftClick(inv, context, slot);
							Main.mouseLeft = mouseLeft;
							PlayerInput.LockGamepadButtons("SmartSelect");
							PlayerInput.SettingsForUI.TryRevertingToMouseMode();
						}
					}
					Main.cursorOverride = cursorOverride;
					ItemSlot.ShiftForcedOn = false;
				}
			}
			if (!ItemSlot.TryEnteringFastUseMode(inv, context, slot, player, ref s))
			{
				ItemSlot.TryEnteringBuildingMode(inv, context, slot, player, ref s);
			}
			return s;
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x004AE1A6 File Offset: 0x004AC3A6
		private static bool CanDoSimulatedClickAction()
		{
			return !PlayerInput.SteamDeckIsUsed || UILinkPointNavigator.InUse;
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x004AE1B8 File Offset: 0x004AC3B8
		private static bool TryEnteringFastUseMode(Item[] inv, int context, int slot, Player player, ref string s)
		{
			int num = 0;
			if (Main.mouseItem.CanBeQuickUsed)
			{
				num = 1;
			}
			if (num == 0 && Main.mouseItem.stack <= 0 && context == 0 && inv[slot].CanBeQuickUsed)
			{
				num = 2;
			}
			if (num > 0)
			{
				s += PlayerInput.BuildCommand(Language.GetTextValue("UI.QuickUseItem"), false, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["QuickMount"]
				});
				if (ItemSlot.CanExecuteCommand() && PlayerInput.Triggers.JustPressed.QuickMount)
				{
					if (num != 1)
					{
						if (num == 2)
						{
							PlayerInput.TryEnteringFastUseModeForInventorySlot(slot);
						}
					}
					else
					{
						PlayerInput.TryEnteringFastUseModeForMouseItem();
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x004AE264 File Offset: 0x004AC464
		private static bool TryEnteringBuildingMode(Item[] inv, int context, int slot, Player player, ref string s)
		{
			int num = 0;
			if (ItemSlot.IsABuildingItem(Main.mouseItem))
			{
				num = 1;
			}
			if (num == 0 && Main.mouseItem.stack <= 0 && context == 0 && ItemSlot.IsABuildingItem(inv[slot]))
			{
				num = 2;
			}
			if (num > 0)
			{
				Item item = Main.mouseItem;
				if (num == 1)
				{
					item = Main.mouseItem;
				}
				if (num == 2)
				{
					item = inv[slot];
				}
				if (num != 1 || player.ItemSpace(item).CanTakeItemToPersonalInventory)
				{
					if (item.damage > 0 && item.ammo == 0)
					{
						s += PlayerInput.BuildCommand(Lang.misc[60].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["QuickMount"]
						});
					}
					else if (item.createTile >= 0 || item.createWall > 0)
					{
						s += PlayerInput.BuildCommand(Lang.misc[61].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["QuickMount"]
						});
					}
					else
					{
						s += PlayerInput.BuildCommand(Lang.misc[63].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["QuickMount"]
						});
					}
					if (ItemSlot.CanExecuteCommand() && PlayerInput.Triggers.JustPressed.QuickMount)
					{
						PlayerInput.EnterBuildingMode();
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x004AE3D2 File Offset: 0x004AC5D2
		public static bool IsABuildingItem(Item item)
		{
			return item.type > 0 && item.stack > 0 && item.useStyle != 0 && item.useTime > 0;
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x004AE3FC File Offset: 0x004AC5FC
		public static void SelectEquipPage(Item item)
		{
			Main.EquipPage = -1;
			if (!item.IsAir)
			{
				if (Main.projHook[item.shoot])
				{
					Main.EquipPage = 2;
					return;
				}
				if (item.mountType != -1)
				{
					Main.EquipPage = 2;
					return;
				}
				if (item.buffType > 0 && Main.vanityPet[item.buffType])
				{
					Main.EquipPage = 2;
					return;
				}
				if (item.buffType > 0 && Main.lightPet[item.buffType])
				{
					Main.EquipPage = 2;
					return;
				}
				if (item.dye > 0 && Main.EquipPageSelected == 1)
				{
					Main.EquipPage = 0;
					return;
				}
				if (item.legSlot != -1 || item.headSlot != -1 || item.bodySlot != -1 || item.accessory)
				{
					Main.EquipPage = 0;
				}
			}
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x004AE4BC File Offset: 0x004AC6BC
		private static bool AccessorySwap(Player player, Item item, ref Item result)
		{
			ItemSlot.accSlotToSwapTo = -1;
			AccessorySlotLoader accLoader = LoaderManager.Get<AccessorySlotLoader>();
			Item[] accessories = AccessorySlotLoader.ModSlotPlayer(player).exAccessorySlot;
			for (int i = 3; i < 10; i++)
			{
				if (player.IsItemSlotUnlockedAndUsable(i) && player.armor[i].type == 0 && ItemLoader.CanEquipAccessory(item, i, false))
				{
					ItemSlot.accSlotToSwapTo = i - 3;
					break;
				}
			}
			if (ItemSlot.accSlotToSwapTo < 0)
			{
				for (int j = 0; j < accessories.Length / 2; j++)
				{
					if (accLoader.ModdedIsSpecificItemSlotUnlockedAndUsable(j, player, false) && accessories[j].type == 0 && accLoader.CanAcceptItem(j, item, -10) && ItemLoader.CanEquipAccessory(item, j, true))
					{
						ItemSlot.accSlotToSwapTo = j + 20;
						break;
					}
				}
			}
			accLoader.ModifyDefaultSwapSlot(item, ref ItemSlot.accSlotToSwapTo);
			for (int k = 0; k < player.armor.Length; k++)
			{
				if (item.IsTheSameAs(player.armor[k]) && ItemLoader.CanEquipAccessory(item, k, false))
				{
					ItemSlot.accSlotToSwapTo = k - 3;
				}
				if (k < 10 && ((item.wingSlot > 0 && player.armor[k].wingSlot > 0) || !ItemLoader.CanAccessoryBeEquippedWith(player.armor[k], item)) && ItemLoader.CanEquipAccessory(item, k, false))
				{
					ItemSlot.accSlotToSwapTo = k - 3;
				}
			}
			for (int l = 0; l < accessories.Length; l++)
			{
				if (item.IsTheSameAs(accessories[l]) && accLoader.CanAcceptItem(l, item, (l < accessories.Length / 2) ? -10 : -11) && ItemLoader.CanEquipAccessory(item, l, true))
				{
					ItemSlot.accSlotToSwapTo = l + 20;
				}
				if (l < accLoader.list.Count && ((item.wingSlot > 0 && accessories[l].wingSlot > 0) || !ItemLoader.CanAccessoryBeEquippedWith(accessories[l], item)) && accLoader.CanAcceptItem(l, item, (l < accessories.Length / 2) ? -10 : -11) && ItemLoader.CanEquipAccessory(item, l, true))
				{
					ItemSlot.accSlotToSwapTo = l + 20;
				}
			}
			if (ItemSlot.accSlotToSwapTo == -1 && !ItemLoader.CanEquipAccessory(item, 0, false))
			{
				return false;
			}
			ItemSlot.accSlotToSwapTo = Math.Max(ItemSlot.accSlotToSwapTo, 0);
			if (ItemSlot.accSlotToSwapTo >= 20)
			{
				int num3 = ItemSlot.accSlotToSwapTo - 20;
				if (ItemSlot.isEquipLocked(accessories[num3].type))
				{
					result = item;
					return false;
				}
				if (!accLoader.ModSlotCheck(item, num3, -10))
				{
					return false;
				}
				result = accessories[num3].Clone();
				accessories[num3] = item.Clone();
			}
			else
			{
				int num4 = 3 + ItemSlot.accSlotToSwapTo;
				if (ItemSlot.isEquipLocked(player.armor[num4].type))
				{
					result = item;
					return false;
				}
				result = player.armor[num4].Clone();
				player.armor[num4] = item.Clone();
			}
			return true;
		}

		/// <summary>
		/// Alters the ItemSlot.DyeSwap method for modded slots;
		/// Unfortunately, I (Solxan) couldn't ever get ItemSlot.DyeSwap invoked so pretty sure this and its vanilla code is defunct.
		/// Here in case someone proves my statement wrong later.
		/// </summary>
		// Token: 0x0600155D RID: 5469 RVA: 0x004AE75C File Offset: 0x004AC95C
		private static Item ModSlotDyeSwap(Item item, out bool success)
		{
			ModAccessorySlotPlayer msPlayer = AccessorySlotLoader.ModSlotPlayer(Main.LocalPlayer);
			int dyeSlotCount = 0;
			Item[] dyes = msPlayer.exDyesAccessory;
			for (int i = 0; i < dyeSlotCount; i++)
			{
				if (dyes[i].type == 0)
				{
					dyeSlotCount = i;
					break;
				}
			}
			if (dyeSlotCount >= msPlayer.SlotCount)
			{
				success = false;
				return item;
			}
			Item item2 = dyes[dyeSlotCount].Clone();
			dyes[dyeSlotCount] = item.Clone();
			SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			Recipe.FindRecipes(false);
			success = true;
			return item2;
		}

		/// <inheritdoc cref="M:Terraria.UI.ItemSlot.AccCheck_ForPlayer(Terraria.Player,Terraria.Item[],Terraria.Item,System.Int32)" />
		// Token: 0x0600155E RID: 5470 RVA: 0x004AE7DD File Offset: 0x004AC9DD
		internal static bool AccCheck_ForLocalPlayer(Item[] itemCollection, Item item, int slot)
		{
			return ItemSlot.AccCheck_ForPlayer(Main.LocalPlayer, itemCollection, item, slot);
		}

		/// <summary>
		/// Checks if placing <paramref name="item" /> into index <paramref name="slot" /> of <paramref name="itemCollection" /> works or not. <paramref name="itemCollection" /> corresponds to the <paramref name="player" />'s accessories. 
		/// <br /><br /> This is a replacement for <see cref="M:Terraria.UI.ItemSlot.AccCheck(Terraria.Item[],Terraria.Item,System.Int32)" /> that takes into account player-specific checks and hooks.
		/// <br /><br /> Returns true if can't equip and false if placing the accessory is ok. 
		/// </summary>
		// Token: 0x0600155F RID: 5471 RVA: 0x004AE7EC File Offset: 0x004AC9EC
		internal static bool AccCheck_ForPlayer(Player player, Item[] itemCollection, Item item, int slot)
		{
			if (ItemSlot.isEquipLocked(item.type))
			{
				return true;
			}
			if (slot != -1)
			{
				if (itemCollection[slot].IsTheSameAs(item))
				{
					return false;
				}
				if ((itemCollection[slot].wingSlot > 0 && item.wingSlot > 0) || !ItemLoader.CanAccessoryBeEquippedWith(player, itemCollection[slot], item))
				{
					return !ItemLoader.CanEquipAccessory(player, item, (slot >= 20) ? (slot - 20) : slot, slot >= 20);
				}
			}
			int modCount = AccessorySlotLoader.ModSlotPlayer(player).SlotCount;
			bool targetVanity = (slot >= 20 && slot >= modCount + 20) || (slot < 20 && slot >= 10);
			for (int i = targetVanity ? 13 : 3; i < (targetVanity ? 20 : 10); i++)
			{
				if ((!targetVanity && item.wingSlot > 0 && itemCollection[i].wingSlot > 0) || !ItemLoader.CanAccessoryBeEquippedWith(player, itemCollection[i], item))
				{
					return true;
				}
			}
			for (int j = (targetVanity ? modCount : 0) + 20; j < (targetVanity ? (modCount * 2) : modCount) + 20; j++)
			{
				if ((!targetVanity && item.wingSlot > 0 && itemCollection[j].wingSlot > 0) || !ItemLoader.CanAccessoryBeEquippedWith(player, itemCollection[j], item))
				{
					return true;
				}
			}
			for (int k = 0; k < itemCollection.Length; k++)
			{
				if (item.IsTheSameAs(itemCollection[k]))
				{
					return true;
				}
			}
			return !ItemLoader.CanEquipAccessory(player, item, (slot >= 20) ? (slot - 20) : slot, slot >= 20);
		}

		// Token: 0x040010E8 RID: 4328
		public static bool DrawGoldBGForCraftingMaterial = false;

		// Token: 0x040010E9 RID: 4329
		public static bool ShiftForcedOn;

		// Token: 0x040010EA RID: 4330
		internal static Item[] singleSlotArray = new Item[1];

		// Token: 0x040010EB RID: 4331
		private static bool[] canFavoriteAt = new bool[ItemSlot.Context.Count];

		// Token: 0x040010EC RID: 4332
		private static bool[] canShareAt = new bool[ItemSlot.Context.Count];

		// Token: 0x040010ED RID: 4333
		private static float[] inventoryGlowHue = new float[58];

		// Token: 0x040010EE RID: 4334
		private static int[] inventoryGlowTime = new int[58];

		// Token: 0x040010EF RID: 4335
		private static float[] inventoryGlowHueChest = new float[58];

		// Token: 0x040010F0 RID: 4336
		private static int[] inventoryGlowTimeChest = new int[58];

		// Token: 0x040010F1 RID: 4337
		private static int _customCurrencyForSavings = -1;

		// Token: 0x040010F2 RID: 4338
		public static bool forceClearGlowsOnChest = false;

		// Token: 0x040010F3 RID: 4339
		private static double _lastTimeForVisualEffectsThatLoadoutWasChanged;

		// Token: 0x040010F4 RID: 4340
		private static Color[,] LoadoutSlotColors;

		// Token: 0x040010F5 RID: 4341
		private static int dyeSlotCount;

		// Token: 0x040010F6 RID: 4342
		private static int accSlotToSwapTo;

		// Token: 0x040010F7 RID: 4343
		public static float CircularRadialOpacity;

		// Token: 0x040010F8 RID: 4344
		public static float QuicksRadialOpacity;

		// Token: 0x02000869 RID: 2153
		public class Options
		{
			// Token: 0x04006930 RID: 26928
			public static bool DisableLeftShiftTrashCan = true;

			// Token: 0x04006931 RID: 26929
			public static bool DisableQuickTrash = false;

			// Token: 0x04006932 RID: 26930
			public static bool HighlightNewItems = true;
		}

		// Token: 0x0200086A RID: 2154
		public class Context
		{
			// Token: 0x04006933 RID: 26931
			public const int ModdedAccessorySlot = -10;

			// Token: 0x04006934 RID: 26932
			public const int ModdedVanityAccessorySlot = -11;

			// Token: 0x04006935 RID: 26933
			public const int ModdedDyeSlot = -12;

			// Token: 0x04006936 RID: 26934
			public const int InventoryItem = 0;

			// Token: 0x04006937 RID: 26935
			public const int InventoryCoin = 1;

			// Token: 0x04006938 RID: 26936
			public const int InventoryAmmo = 2;

			// Token: 0x04006939 RID: 26937
			public const int ChestItem = 3;

			// Token: 0x0400693A RID: 26938
			public const int BankItem = 4;

			// Token: 0x0400693B RID: 26939
			public const int PrefixItem = 5;

			// Token: 0x0400693C RID: 26940
			public const int TrashItem = 6;

			// Token: 0x0400693D RID: 26941
			public const int GuideItem = 7;

			// Token: 0x0400693E RID: 26942
			public const int EquipArmor = 8;

			// Token: 0x0400693F RID: 26943
			public const int EquipArmorVanity = 9;

			// Token: 0x04006940 RID: 26944
			public const int EquipAccessory = 10;

			// Token: 0x04006941 RID: 26945
			public const int EquipAccessoryVanity = 11;

			// Token: 0x04006942 RID: 26946
			public const int EquipDye = 12;

			// Token: 0x04006943 RID: 26947
			public const int HotbarItem = 13;

			// Token: 0x04006944 RID: 26948
			public const int ChatItem = 14;

			// Token: 0x04006945 RID: 26949
			public const int ShopItem = 15;

			// Token: 0x04006946 RID: 26950
			public const int EquipGrapple = 16;

			// Token: 0x04006947 RID: 26951
			public const int EquipMount = 17;

			// Token: 0x04006948 RID: 26952
			public const int EquipMinecart = 18;

			// Token: 0x04006949 RID: 26953
			public const int EquipPet = 19;

			// Token: 0x0400694A RID: 26954
			public const int EquipLight = 20;

			// Token: 0x0400694B RID: 26955
			public const int MouseItem = 21;

			// Token: 0x0400694C RID: 26956
			public const int CraftingMaterial = 22;

			// Token: 0x0400694D RID: 26957
			public const int DisplayDollArmor = 23;

			// Token: 0x0400694E RID: 26958
			public const int DisplayDollAccessory = 24;

			// Token: 0x0400694F RID: 26959
			public const int DisplayDollDye = 25;

			// Token: 0x04006950 RID: 26960
			public const int HatRackHat = 26;

			// Token: 0x04006951 RID: 26961
			public const int HatRackDye = 27;

			// Token: 0x04006952 RID: 26962
			public const int GoldDebug = 28;

			// Token: 0x04006953 RID: 26963
			public const int CreativeInfinite = 29;

			// Token: 0x04006954 RID: 26964
			public const int CreativeSacrifice = 30;

			// Token: 0x04006955 RID: 26965
			public const int InWorld = 31;

			// Token: 0x04006956 RID: 26966
			public const int VoidItem = 32;

			// Token: 0x04006957 RID: 26967
			public const int EquipMiscDye = 33;

			// Token: 0x04006958 RID: 26968
			public static readonly int Count = 34;
		}

		// Token: 0x0200086B RID: 2155
		public struct ItemTransferInfo
		{
			// Token: 0x06005160 RID: 20832 RVA: 0x00696E9C File Offset: 0x0069509C
			public ItemTransferInfo(Item itemAfter, int fromContext, int toContext, int transferAmount = 0)
			{
				this.ItemType = itemAfter.type;
				this.TransferAmount = itemAfter.stack;
				if (transferAmount != 0)
				{
					this.TransferAmount = transferAmount;
				}
				this.FromContenxt = fromContext;
				this.ToContext = toContext;
			}

			// Token: 0x04006959 RID: 26969
			public int ItemType;

			// Token: 0x0400695A RID: 26970
			public int TransferAmount;

			// Token: 0x0400695B RID: 26971
			public int FromContenxt;

			// Token: 0x0400695C RID: 26972
			public int ToContext;
		}

		// Token: 0x0200086C RID: 2156
		// (Invoke) Token: 0x06005162 RID: 20834
		public delegate void ItemTransferEvent(ItemSlot.ItemTransferInfo info);
	}
}
