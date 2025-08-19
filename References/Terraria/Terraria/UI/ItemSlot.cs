using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.Chat;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI.Chat;
using Terraria.UI.Gamepad;

namespace Terraria.UI
{
	// Token: 0x02000093 RID: 147
	public class ItemSlot
	{
		// Token: 0x06001260 RID: 4704 RVA: 0x004948F4 File Offset: 0x00492AF4
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

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x00494B2E File Offset: 0x00492D2E
		public static bool ShiftInUse
		{
			get
			{
				return Main.keyState.PressingShift() || ItemSlot.ShiftForcedOn;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x00494B43 File Offset: 0x00492D43
		public static bool ControlInUse
		{
			get
			{
				return Main.keyState.PressingControl();
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x00494B4F File Offset: 0x00492D4F
		public static bool NotUsingGamepad
		{
			get
			{
				return !PlayerInput.UsingGamepad;
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06001264 RID: 4708 RVA: 0x00494B5C File Offset: 0x00492D5C
		// (remove) Token: 0x06001265 RID: 4709 RVA: 0x00494B90 File Offset: 0x00492D90
		public static event ItemSlot.ItemTransferEvent OnItemTransferred;

		// Token: 0x06001266 RID: 4710 RVA: 0x00494BC3 File Offset: 0x00492DC3
		public static void AnnounceTransfer(ItemSlot.ItemTransferInfo info)
		{
			if (ItemSlot.OnItemTransferred != null)
			{
				ItemSlot.OnItemTransferred(info);
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00494BD8 File Offset: 0x00492DD8
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

		// Token: 0x06001268 RID: 4712 RVA: 0x00494C30 File Offset: 0x00492E30
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

		// Token: 0x06001269 RID: 4713 RVA: 0x00494CEC File Offset: 0x00492EEC
		public static void Handle(ref Item inv, int context = 0)
		{
			ItemSlot.singleSlotArray[0] = inv;
			ItemSlot.Handle(ItemSlot.singleSlotArray, context, 0);
			inv = ItemSlot.singleSlotArray[0];
			Recipe.FindRecipes(false);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00494D12 File Offset: 0x00492F12
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

		// Token: 0x0600126B RID: 4715 RVA: 0x00494D48 File Offset: 0x00492F48
		public static void OverrideHover(ref Item inv, int context = 0)
		{
			ItemSlot.singleSlotArray[0] = inv;
			ItemSlot.OverrideHover(ItemSlot.singleSlotArray, context, 0);
			inv = ItemSlot.singleSlotArray[0];
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		public static bool isEquipLocked(int type)
		{
			return false;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00494D68 File Offset: 0x00492F68
		public static void OverrideHover(Item[] inv, int context = 0, int slot = 0)
		{
			Item item = inv[slot];
			if (!PlayerInput.UsingGamepad)
			{
				UILinkPointNavigator.SuggestUsage(ItemSlot.GetGamepadPointForSlot(inv, context, slot));
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
								goto IL_4D1;
							}
							Main.cursorOverride = 6;
							goto IL_4D1;
						case 3:
						case 4:
						case 7:
							break;
						case 5:
						case 6:
							goto IL_4D1;
						default:
							if (context != 32)
							{
								goto IL_4D1;
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
						case 1:
						case 2:
							if (context == 0 && Main.CreativeMenu.IsShowingResearchMenu())
							{
								Main.cursorOverride = 9;
							}
							else if (context == 0 && Main.InReforgeMenu)
							{
								if (item.maxStack == 1 && item.Prefix(-3))
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
							if (item.maxStack == 1 && item.Prefix(-3))
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
			IL_4D1:
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

		// Token: 0x0600126E RID: 4718 RVA: 0x004952A8 File Offset: 0x004934A8
		private static bool OverrideLeftClick(Item[] inv, int context = 0, int slot = 0)
		{
			if (context == 10 && ItemSlot.isEquipLocked(inv[slot].type))
			{
				return true;
			}
			if (Main.LocalPlayer.tileEntityAnchor.IsInValidUseTileEntity() && Main.LocalPlayer.tileEntityAnchor.GetTileEntity().OverrideItemSlotLeftClick(inv, context, slot))
			{
				return true;
			}
			Item item = inv[slot];
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
				if (!ItemSlot.canFavoriteAt[context])
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
					Item item2 = new Item();
					item2.SetDefaults(inv[slot].netID);
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

		// Token: 0x0600126F RID: 4719 RVA: 0x00495533 File Offset: 0x00493733
		public static void LeftClick(ref Item inv, int context = 0)
		{
			ItemSlot.singleSlotArray[0] = inv;
			ItemSlot.LeftClick(ItemSlot.singleSlotArray, context, 0);
			inv = ItemSlot.singleSlotArray[0];
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00495554 File Offset: 0x00493754
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
				if (ItemSlot.LeftClick_SellOrTrash(inv, context, slot))
				{
					return;
				}
				if (player.itemAnimation != 0 || player.itemTime != 0)
				{
					return;
				}
			}
			int num = ItemSlot.PickItemMovementAction(inv, context, slot, Main.mouseItem);
			if (num != 3 && !flag)
			{
				return;
			}
			if (num == 0)
			{
				if (context == 6 && Main.mouseItem.type != 0)
				{
					inv[slot].SetDefaults(0);
				}
				if (context != 11 || inv[slot].FitsAccessoryVanitySlot)
				{
					Utils.Swap<Item>(ref inv[slot], ref Main.mouseItem);
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
						if (context <= 17)
						{
							if (context == 0)
							{
								AchievementsHelper.NotifyItemPickup(player, inv[slot]);
								goto IL_130;
							}
							if (context - 8 > 4 && context - 16 > 1)
							{
								goto IL_130;
							}
						}
						else if (context != 25 && context != 27 && context != 33)
						{
							goto IL_130;
						}
						AchievementsHelper.HandleOnEquip(player, inv[slot], context);
					}
					IL_130:
					if (inv[slot].type == 0 || inv[slot].stack < 1)
					{
						inv[slot] = new Item();
					}
					if (Main.mouseItem.IsTheSameAs(inv[slot]))
					{
						Utils.Swap<bool>(ref inv[slot].favorited, ref Main.mouseItem.favorited);
						if (inv[slot].stack != inv[slot].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
						{
							if (Main.mouseItem.stack + inv[slot].stack <= Main.mouseItem.maxStack)
							{
								inv[slot].stack += Main.mouseItem.stack;
								Main.mouseItem.stack = 0;
								ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(inv[slot], 21, context, inv[slot].stack));
							}
							else
							{
								int num2 = Main.mouseItem.maxStack - inv[slot].stack;
								inv[slot].stack += num2;
								Main.mouseItem.stack -= num2;
								ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(inv[slot], 21, context, num2));
							}
						}
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
			}
			else if (num == 1)
			{
				if (Main.mouseItem.stack == 1 && Main.mouseItem.type > 0 && inv[slot].type > 0 && inv[slot].IsNotTheSameAs(Main.mouseItem) && (context != 11 || Main.mouseItem.FitsAccessoryVanitySlot))
				{
					Utils.Swap<Item>(ref inv[slot], ref Main.mouseItem);
					SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
					if (inv[slot].stack > 0)
					{
						if (context <= 17)
						{
							if (context == 0)
							{
								AchievementsHelper.NotifyItemPickup(player, inv[slot]);
								goto IL_59D;
							}
							if (context - 8 > 4 && context - 16 > 1)
							{
								goto IL_59D;
							}
						}
						else if (context != 25 && context != 27 && context != 33)
						{
							goto IL_59D;
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
					if (Main.mouseItem.stack == 1)
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
					else
					{
						Main.mouseItem.stack--;
						inv[slot].SetDefaults(Main.mouseItem.type);
						Recipe.FindRecipes(false);
						SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
					}
					if (inv[slot].stack > 0)
					{
						if (context <= 17)
						{
							if (context == 0)
							{
								AchievementsHelper.NotifyItemPickup(player, inv[slot]);
								goto IL_59D;
							}
							if (context - 8 > 4 && context - 16 > 1)
							{
								goto IL_59D;
							}
						}
						else if (context != 25 && context != 27 && context != 33)
						{
							goto IL_59D;
						}
						AchievementsHelper.HandleOnEquip(player, inv[slot], context);
					}
				}
				IL_59D:
				if ((context == 23 || context == 24) && Main.netMode == 1)
				{
					NetMessage.SendData(121, -1, -1, null, Main.myPlayer, (float)player.tileEntityAnchor.interactEntityID, (float)slot, 0f, 0, 0, 0);
				}
				if (context == 26 && Main.netMode == 1)
				{
					NetMessage.SendData(124, -1, -1, null, Main.myPlayer, (float)player.tileEntityAnchor.interactEntityID, (float)slot, 0f, 0, 0, 0);
				}
			}
			else if (num == 2)
			{
				if (Main.mouseItem.stack == 1 && Main.mouseItem.dye > 0 && inv[slot].type > 0 && inv[slot].type != Main.mouseItem.type)
				{
					Utils.Swap<Item>(ref inv[slot], ref Main.mouseItem);
					SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
					if (inv[slot].stack > 0)
					{
						if (context <= 17)
						{
							if (context == 0)
							{
								AchievementsHelper.NotifyItemPickup(player, inv[slot]);
								goto IL_8B8;
							}
							if (context - 8 > 4 && context - 16 > 1)
							{
								goto IL_8B8;
							}
						}
						else if (context != 25 && context != 27 && context != 33)
						{
							goto IL_8B8;
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
					if (Main.mouseItem.stack == 1)
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
					else
					{
						Main.mouseItem.stack--;
						inv[slot].SetDefaults(Main.mouseItem.type);
						Recipe.FindRecipes(false);
						SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
					}
					if (inv[slot].stack > 0)
					{
						if (context <= 17)
						{
							if (context == 0)
							{
								AchievementsHelper.NotifyItemPickup(player, inv[slot]);
								goto IL_8B8;
							}
							if (context - 8 > 4 && context - 16 > 1)
							{
								goto IL_8B8;
							}
						}
						else if (context != 25 && context != 27 && context != 33)
						{
							goto IL_8B8;
						}
						AchievementsHelper.HandleOnEquip(player, inv[slot], context);
					}
				}
				IL_8B8:
				if (context == 25 && Main.netMode == 1)
				{
					NetMessage.SendData(121, -1, -1, null, Main.myPlayer, (float)player.tileEntityAnchor.interactEntityID, (float)slot, 1f, 0, 0, 0);
				}
				if (context == 27 && Main.netMode == 1)
				{
					NetMessage.SendData(124, -1, -1, null, Main.myPlayer, (float)player.tileEntityAnchor.interactEntityID, (float)slot, 1f, 0, 0, 0);
				}
			}
			else if (num == 3)
			{
				ItemSlot.HandleShopSlot(inv, slot, false, true);
			}
			else if (num == 4)
			{
				Chest chest = Main.instance.shop[Main.npcShop];
				if (player.SellItem(Main.mouseItem, -1))
				{
					chest.AddItemToShop(Main.mouseItem);
					Main.mouseItem.SetDefaults(0);
					SoundEngine.PlaySound(18, -1, -1, 1, 1f, 0f);
					ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(inv[slot], 21, 15, 0));
				}
				else if (Main.mouseItem.value == 0)
				{
					chest.AddItemToShop(Main.mouseItem);
					Main.mouseItem.SetDefaults(0);
					SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
					ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(inv[slot], 21, 15, 0));
				}
				Recipe.FindRecipes(false);
				Main.stackSplit = 9999;
			}
			else if (num == 5 && Main.mouseItem.IsAir)
			{
				SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
				Main.mouseItem.SetDefaults(inv[slot].netID);
				Main.mouseItem.stack = Main.mouseItem.maxStack;
				Main.mouseItem.OnCreated(new JourneyDuplicationItemCreationContext());
				ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(inv[slot], 29, 21, 0));
			}
			if (context > 2 && context != 5 && context != 32)
			{
				inv[slot].favorited = false;
			}
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00495FDD File Offset: 0x004941DD
		private static bool DisableTrashing()
		{
			return ItemSlot.Options.DisableLeftShiftTrashCan && !PlayerInput.SteamDeckIsUsed;
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00495FF0 File Offset: 0x004941F0
		private static bool LeftClick_SellOrTrash(Item[] inv, int context, int slot)
		{
			bool flag = false;
			bool result = false;
			if (ItemSlot.NotUsingGamepad && ItemSlot.Options.DisableLeftShiftTrashCan)
			{
				if (!ItemSlot.Options.DisableQuickTrash)
				{
					if (context <= 4 || context == 7 || context == 32)
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
				if (context <= 4 || context == 32)
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

		// Token: 0x06001273 RID: 4723 RVA: 0x0049607C File Offset: 0x0049427C
		private static void SellOrTrash(Item[] inv, int context, int slot)
		{
			Player player = Main.player[Main.myPlayer];
			if (inv[slot].type > 0)
			{
				if (Main.npcShop > 0 && !inv[slot].favorited)
				{
					Chest chest = Main.instance.shop[Main.npcShop];
					if (inv[slot].type < 71 || inv[slot].type > 74)
					{
						if (player.SellItem(inv[slot], -1))
						{
							chest.AddItemToShop(inv[slot]);
							ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(inv[slot], context, 15, 0));
							inv[slot].TurnToAir(false);
							SoundEngine.PlaySound(18, -1, -1, 1, 1f, 0f);
							Recipe.FindRecipes(false);
							return;
						}
						if (inv[slot].value == 0)
						{
							chest.AddItemToShop(inv[slot]);
							ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(inv[slot], context, 15, 0));
							inv[slot].TurnToAir(false);
							SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
							Recipe.FindRecipes(false);
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
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x004961F8 File Offset: 0x004943F8
		private static string GetOverrideInstructions(Item[] inv, int context, int slot)
		{
			Player player = Main.player[Main.myPlayer];
			TileEntity tileEntity = player.tileEntityAnchor.GetTileEntity();
			string result;
			if (tileEntity != null && tileEntity.TryGetItemGamepadOverrideInstructions(inv, context, slot, out result))
			{
				return result;
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

		// Token: 0x06001275 RID: 4725 RVA: 0x00496480 File Offset: 0x00494680
		public static int PickItemMovementAction(Item[] inv, int context, int slot, Item checkItem)
		{
			Player player = Main.player[Main.myPlayer];
			int result = -1;
			if (context == 0)
			{
				result = 0;
			}
			else if (context == 1)
			{
				if (checkItem.type == 0 || checkItem.type == 71 || checkItem.type == 72 || checkItem.type == 73 || checkItem.type == 74)
				{
					result = 0;
				}
			}
			else if (context == 2)
			{
				if (checkItem.FitsAmmoSlot())
				{
					result = 0;
				}
			}
			else if (context == 3)
			{
				result = 0;
			}
			else if (context == 4 || context == 32)
			{
				bool flag;
				Item[] container;
				ChestUI.GetContainerUsageInfo(out flag, out container);
				if (!ChestUI.IsBlockedFromTransferIntoChest(checkItem, container))
				{
					result = 0;
				}
			}
			else if (context == 5)
			{
				if (checkItem.Prefix(-3) || checkItem.type == 0)
				{
					result = 0;
				}
			}
			else if (context == 6)
			{
				result = 0;
			}
			else if (context == 7)
			{
				if (checkItem.material || checkItem.type == 0)
				{
					result = 0;
				}
			}
			else if (context == 8)
			{
				if (checkItem.type == 0 || (checkItem.headSlot > -1 && slot == 0) || (checkItem.bodySlot > -1 && slot == 1) || (checkItem.legSlot > -1 && slot == 2))
				{
					result = 1;
				}
			}
			else if (context == 23)
			{
				if (checkItem.type == 0 || (checkItem.headSlot > 0 && slot == 0) || (checkItem.bodySlot > 0 && slot == 1) || (checkItem.legSlot > 0 && slot == 2))
				{
					result = 1;
				}
			}
			else if (context == 26)
			{
				if (checkItem.type == 0 || checkItem.headSlot > 0)
				{
					result = 1;
				}
			}
			else if (context == 9)
			{
				if (checkItem.type == 0 || (checkItem.headSlot > -1 && slot == 10) || (checkItem.bodySlot > -1 && slot == 11) || (checkItem.legSlot > -1 && slot == 12))
				{
					result = 1;
				}
			}
			else if (context == 10)
			{
				if (checkItem.type == 0 || (checkItem.accessory && !ItemSlot.AccCheck(Main.LocalPlayer.armor, checkItem, slot)))
				{
					result = 1;
				}
			}
			else if (context == 24)
			{
				if (checkItem.type == 0 || (checkItem.accessory && !ItemSlot.AccCheck(inv, checkItem, slot)))
				{
					result = 1;
				}
			}
			else if (context == 11)
			{
				if (checkItem.type == 0 || (checkItem.accessory && !ItemSlot.AccCheck(Main.LocalPlayer.armor, checkItem, slot)))
				{
					result = 1;
				}
			}
			else if (context == 12 || context == 25 || context == 27 || context == 33)
			{
				result = 2;
			}
			else if (context == 15)
			{
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
			}
			else if (context == 16)
			{
				if (checkItem.type == 0 || Main.projHook[checkItem.shoot])
				{
					result = 1;
				}
			}
			else if (context == 17)
			{
				if (checkItem.type == 0 || (checkItem.mountType != -1 && !MountID.Sets.Cart[checkItem.mountType]))
				{
					result = 1;
				}
			}
			else if (context == 19)
			{
				if (checkItem.type == 0 || (checkItem.buffType > 0 && Main.vanityPet[checkItem.buffType] && !Main.lightPet[checkItem.buffType]))
				{
					result = 1;
				}
			}
			else if (context == 18)
			{
				if (checkItem.type == 0 || (checkItem.mountType != -1 && MountID.Sets.Cart[checkItem.mountType]))
				{
					result = 1;
				}
			}
			else if (context == 20)
			{
				if (checkItem.type == 0 || (checkItem.buffType > 0 && Main.lightPet[checkItem.buffType]))
				{
					result = 1;
				}
			}
			else if (context == 29 && checkItem.type == 0 && inv[slot].type > 0)
			{
				result = 5;
			}
			if (context == 30)
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00496899 File Offset: 0x00494A99
		public static void RightClick(ref Item inv, int context = 0)
		{
			ItemSlot.singleSlotArray[0] = inv;
			ItemSlot.RightClick(ItemSlot.singleSlotArray, context, 0);
			inv = ItemSlot.singleSlotArray[0];
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x004968BC File Offset: 0x00494ABC
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
			if (context == 0 && ItemID.Sets.OpenableBag[inv[slot].type])
			{
				if (Main.mouseRightRelease)
				{
					ItemSlot.TryOpenContainer(inv[slot], player);
					return;
				}
			}
			else if (context == 9 || context == 11)
			{
				if (Main.mouseRightRelease)
				{
					ItemSlot.SwapVanityEquip(inv, context, slot, player);
					return;
				}
			}
			else if (context == 12 || context == 25 || context == 27 || context == 33)
			{
				if (Main.mouseRightRelease)
				{
					ItemSlot.TryPickupDyeToCursor(context, inv, slot, player);
					return;
				}
			}
			else if ((context == 0 || context == 4 || context == 32 || context == 3) && inv[slot].maxStack == 1)
			{
				if (Main.mouseRightRelease)
				{
					ItemSlot.SwapEquip(inv, context, slot);
					return;
				}
			}
			else if (Main.stackSplit <= 1)
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
						if ((Main.mouseItem.IsTheSameAs(inv[slot]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
						{
							ItemSlot.PickupItemIntoMouse(inv, context, slot, player);
							SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
							ItemSlot.RefreshStackSplitCooldown();
						}
					}
				}
			}
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00496A7C File Offset: 0x00494C7C
		public static void PickupItemIntoMouse(Item[] inv, int context, int slot, Player player)
		{
			if (Main.mouseItem.type == 0)
			{
				Main.mouseItem = inv[slot].Clone();
				if (context == 29)
				{
					Main.mouseItem.SetDefaults(Main.mouseItem.type);
					Main.mouseItem.OnCreated(new JourneyDuplicationItemCreationContext());
				}
				Main.mouseItem.stack = 0;
				if (inv[slot].favorited && inv[slot].stack == 1)
				{
					Main.mouseItem.favorited = true;
				}
				else
				{
					Main.mouseItem.favorited = false;
				}
				ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(inv[slot], context, 21, 0));
			}
			Main.mouseItem.stack++;
			if (context != 29)
			{
				inv[slot].stack--;
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

		// Token: 0x06001279 RID: 4729 RVA: 0x00496C4B File Offset: 0x00494E4B
		public static void RefreshStackSplitCooldown()
		{
			if (Main.stackSplit == 0)
			{
				Main.stackSplit = 30;
				return;
			}
			Main.stackSplit = Main.stackDelay;
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00496C68 File Offset: 0x00494E68
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
			else
			{
				if (item.type != 599 && item.type != 600 && item.type != 601)
				{
					return;
				}
				player.OpenLegacyPresent(item.type);
			}
			item.stack--;
			if (item.stack == 0)
			{
				item.SetDefaults(0);
			}
			SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			Main.stackSplit = 30;
			Main.mouseRightRelease = false;
			Recipe.FindRecipes(false);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00496E10 File Offset: 0x00495010
		private static void SwapVanityEquip(Item[] inv, int context, int slot, Player player)
		{
			if (Main.npcShop > 0)
			{
				return;
			}
			if ((inv[slot].type > 0 && inv[slot].stack > 0) || (inv[slot - 10].type > 0 && inv[slot - 10].stack > 0))
			{
				Item item = inv[slot - 10];
				bool flag = context != 11 || item.FitsAccessoryVanitySlot || item.IsAir;
				if (flag && context == 11 && inv[slot].wingSlot > 0)
				{
					for (int i = 3; i < 10; i++)
					{
						if (inv[i].wingSlot > 0 && i != slot - 10)
						{
							flag = false;
						}
					}
				}
				if (flag)
				{
					Utils.Swap<Item>(ref inv[slot], ref inv[slot - 10]);
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
			}
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00496F28 File Offset: 0x00495128
		private static void TryPickupDyeToCursor(int context, Item[] inv, int slot, Player player)
		{
			bool flag = false;
			if (!flag && ((Main.mouseItem.stack < Main.mouseItem.maxStack && Main.mouseItem.type > 0) || Main.mouseItem.IsAir) && inv[slot].type > 0 && (Main.mouseItem.type == inv[slot].type || Main.mouseItem.IsAir))
			{
				flag = true;
				if (Main.mouseItem.IsAir)
				{
					Main.mouseItem = inv[slot].Clone();
				}
				else
				{
					Main.mouseItem.stack++;
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

		// Token: 0x0600127D RID: 4733 RVA: 0x0049704C File Offset: 0x0049524C
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

		// Token: 0x0600127E RID: 4734 RVA: 0x004972F4 File Offset: 0x004954F4
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

		// Token: 0x0600127F RID: 4735 RVA: 0x00497388 File Offset: 0x00495588
		private static void HandleShopSlot(Item[] inv, int slot, bool rightClickIsValid, bool leftClickIsValid)
		{
			if (Main.cursorOverride == 2)
			{
				return;
			}
			Chest chest = Main.instance.shop[Main.npcShop];
			bool flag = (Main.mouseRight && rightClickIsValid) || (Main.mouseLeft && leftClickIsValid);
			if (Main.stackSplit <= 1 && flag && inv[slot].type > 0 && (Main.mouseItem.IsTheSameAs(inv[slot]) || Main.mouseItem.type == 0))
			{
				int num = Main.superFastStack + 1;
				Player localPlayer = Main.LocalPlayer;
				for (int i = 0; i < num; i++)
				{
					if (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0)
					{
						long num2;
						long price;
						localPlayer.GetItemExpectedPrice(inv[slot], out num2, out price);
						if (localPlayer.BuyItem(price, inv[slot].shopSpecialCurrency) && inv[slot].stack > 0)
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
							if (Main.mouseItem.type == 0)
							{
								Main.mouseItem.netDefaults(inv[slot].netID);
								if (inv[slot].prefix != 0)
								{
									Main.mouseItem.Prefix((int)inv[slot].prefix);
								}
								Main.mouseItem.stack = 0;
							}
							if (!inv[slot].buyOnce)
							{
								Main.shopSellbackHelper.Add(inv[slot]);
							}
							Main.mouseItem.stack++;
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
						}
					}
				}
			}
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00497563 File Offset: 0x00495763
		public static void Draw(SpriteBatch spriteBatch, ref Item inv, int context, Vector2 position, Color lightColor = default(Color))
		{
			ItemSlot.singleSlotArray[0] = inv;
			ItemSlot.Draw(spriteBatch, ItemSlot.singleSlotArray, context, 0, position, lightColor);
			inv = ItemSlot.singleSlotArray[0];
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00497588 File Offset: 0x00495788
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
				if (num == 1)
				{
					color2 = color2.MultiplyRGBA(new Color(num3, num3 / 2f, num3 / 2f));
				}
				else
				{
					color2 = color2.MultiplyRGBA(new Color(num3 / 2f, num3, num3 / 2f));
				}
			}
			else if (context == 0 && slot < 10)
			{
				value = TextureAssets.InventoryBack9.Value;
			}
			else if (context == 28)
			{
				value = TextureAssets.InventoryBack7.Value;
				color2 = Color.White;
			}
			else if (context == 16 || context == 17 || context == 19 || context == 18 || context == 20)
			{
				value = TextureAssets.InventoryBack3.Value;
			}
			else if (context == 10 || context == 8)
			{
				value = TextureAssets.InventoryBack13.Value;
				color2 = ItemSlot.GetColorByLoadout(slot, context);
			}
			else if (context == 24 || context == 23 || context == 26)
			{
				value = TextureAssets.InventoryBack8.Value;
			}
			else if (context == 11 || context == 9)
			{
				value = TextureAssets.InventoryBack13.Value;
				color2 = ItemSlot.GetColorByLoadout(slot, context);
			}
			else if (context == 25 || context == 27 || context == 33)
			{
				value = TextureAssets.InventoryBack12.Value;
			}
			else if (context == 12)
			{
				value = TextureAssets.InventoryBack13.Value;
				color2 = ItemSlot.GetColorByLoadout(slot, context);
			}
			else if (context == 3)
			{
				value = TextureAssets.InventoryBack5.Value;
			}
			else if (context == 4 || context == 32)
			{
				value = TextureAssets.InventoryBack2.Value;
			}
			else if (context == 7 || context == 5)
			{
				value = TextureAssets.InventoryBack4.Value;
			}
			else if (context == 6)
			{
				value = TextureAssets.InventoryBack7.Value;
			}
			else if (context == 13)
			{
				byte b = 200;
				if (slot == Main.player[Main.myPlayer].selectedItem)
				{
					value = TextureAssets.InventoryBack14.Value;
					b = byte.MaxValue;
				}
				color2 = new Color((int)b, (int)b, (int)b, (int)b);
			}
			else if (context == 14 || context == 21)
			{
				flag2 = true;
			}
			else if (context == 15)
			{
				value = TextureAssets.InventoryBack6.Value;
			}
			else if (context == 29)
			{
				color2 = new Color(53, 69, 127, 255);
				value = TextureAssets.InventoryBack18.Value;
			}
			else if (context == 30)
			{
				flag2 = !flag;
			}
			else if (context == 22)
			{
				value = TextureAssets.InventoryBack4.Value;
				if (ItemSlot.DrawGoldBGForCraftingMaterial)
				{
					ItemSlot.DrawGoldBGForCraftingMaterial = false;
					value = TextureAssets.InventoryBack14.Value;
					float num4 = (float)color2.A / 255f;
					if (num4 < 0.7f)
					{
						num4 = Utils.GetLerpValue(0f, 0.7f, num4, true);
					}
					else
					{
						num4 = 1f;
					}
					color2 = Color.White * num4;
				}
			}
			if ((context == 0 || context == 2) && ItemSlot.inventoryGlowTime[slot] > 0 && !inv[slot].favorited && !inv[slot].IsAir)
			{
				float scale = Main.invAlpha / 255f;
				Color value2 = new Color(63, 65, 151, 255) * scale;
				Color value3 = Main.hslToRgb(ItemSlot.inventoryGlowHue[slot], 1f, 0.5f, byte.MaxValue) * scale;
				float num5 = (float)ItemSlot.inventoryGlowTime[slot] / 300f;
				num5 *= num5;
				color2 = Color.Lerp(value2, value3, num5 / 2f);
				value = TextureAssets.InventoryBack13.Value;
			}
			if ((context == 4 || context == 32 || context == 3) && ItemSlot.inventoryGlowTimeChest[slot] > 0 && !inv[slot].favorited && !inv[slot].IsAir)
			{
				float scale2 = Main.invAlpha / 255f;
				Color value4 = new Color(130, 62, 102, 255) * scale2;
				if (context == 3)
				{
					value4 = new Color(104, 52, 52, 255) * scale2;
				}
				Color value5 = Main.hslToRgb(ItemSlot.inventoryGlowHueChest[slot], 1f, 0.5f, byte.MaxValue) * scale2;
				float num6 = (float)ItemSlot.inventoryGlowTimeChest[slot] / 300f;
				num6 *= num6;
				color2 = Color.Lerp(value4, value5, num6 / 2f);
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
				spriteBatch.Draw(value, position, null, color2, 0f, default(Vector2), inventoryScale, SpriteEffects.None, 0f);
			}
			int num7 = -1;
			switch (context)
			{
			case 8:
			case 23:
				if (slot == 0)
				{
					num7 = 0;
				}
				if (slot == 1)
				{
					num7 = 6;
				}
				if (slot == 2)
				{
					num7 = 12;
				}
				break;
			case 9:
				if (slot == 10)
				{
					num7 = 3;
				}
				if (slot == 11)
				{
					num7 = 9;
				}
				if (slot == 12)
				{
					num7 = 15;
				}
				break;
			case 10:
			case 24:
				num7 = 11;
				break;
			case 11:
				num7 = 2;
				break;
			case 12:
			case 25:
			case 27:
			case 33:
				num7 = 1;
				break;
			case 16:
				num7 = 4;
				break;
			case 17:
				num7 = 13;
				break;
			case 18:
				num7 = 7;
				break;
			case 19:
				num7 = 10;
				break;
			case 20:
				num7 = 17;
				break;
			case 26:
				num7 = 0;
				break;
			}
			if ((item.type <= 0 || item.stack <= 0) && num7 != -1)
			{
				Texture2D value6 = TextureAssets.Extra[54].Value;
				Rectangle rectangle = value6.Frame(3, 6, num7 % 3, num7 / 3, 0, 0);
				rectangle.Width -= 2;
				rectangle.Height -= 2;
				spriteBatch.Draw(value6, position + value.Size() / 2f * inventoryScale, new Rectangle?(rectangle), Color.White * 0.35f, 0f, rectangle.Size() / 2f, inventoryScale, SpriteEffects.None, 0f);
			}
			Vector2 vector = value.Size() * inventoryScale;
			if (item.type > 0 && item.stack > 0)
			{
				float scale3 = ItemSlot.DrawItemIcon(item, context, spriteBatch, position + vector / 2f, inventoryScale, 32f, color);
				if (ItemID.Sets.TrapSigned[item.type])
				{
					spriteBatch.Draw(TextureAssets.Wire.Value, position + new Vector2(40f, 40f) * inventoryScale, new Rectangle?(new Rectangle(4, 58, 8, 8)), color, 0f, new Vector2(4f), 1f, SpriteEffects.None, 0f);
				}
				if (ItemID.Sets.DrawUnsafeIndicator[item.type])
				{
					Vector2 value7 = new Vector2(-4f, -4f) * inventoryScale;
					Texture2D value8 = TextureAssets.Extra[258].Value;
					Rectangle rectangle2 = value8.Frame(1, 1, 0, 0, 0, 0);
					spriteBatch.Draw(value8, position + value7 + new Vector2(40f, 40f) * inventoryScale, new Rectangle?(rectangle2), color, 0f, rectangle2.Size() / 2f, 1f, SpriteEffects.None, 0f);
				}
				if (item.type == 5324 || item.type == 5329 || item.type == 5330)
				{
					Vector2 value9 = new Vector2(2f, -6f) * inventoryScale;
					int type = item.type;
					if (type != 5324)
					{
						if (type != 5329)
						{
							if (type == 5330)
							{
								Texture2D value10 = TextureAssets.Extra[257].Value;
								Rectangle rectangle3 = value10.Frame(3, 1, 0, 0, 0, 0);
								spriteBatch.Draw(value10, position + value9 + new Vector2(40f, 40f) * inventoryScale, new Rectangle?(rectangle3), color, 0f, rectangle3.Size() / 2f, 1f, SpriteEffects.None, 0f);
							}
						}
						else
						{
							Texture2D value11 = TextureAssets.Extra[257].Value;
							Rectangle rectangle4 = value11.Frame(3, 1, 1, 0, 0, 0);
							spriteBatch.Draw(value11, position + value9 + new Vector2(40f, 40f) * inventoryScale, new Rectangle?(rectangle4), color, 0f, rectangle4.Size() / 2f, 1f, SpriteEffects.None, 0f);
						}
					}
					else
					{
						Texture2D value12 = TextureAssets.Extra[257].Value;
						Rectangle rectangle5 = value12.Frame(3, 1, 2, 0, 0, 0);
						spriteBatch.Draw(value12, position + value9 + new Vector2(40f, 40f) * inventoryScale, new Rectangle?(rectangle5), color, 0f, rectangle5.Size() / 2f, 1f, SpriteEffects.None, 0f);
					}
				}
				if (item.stack > 1)
				{
					ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, item.stack.ToString(), position + new Vector2(10f, 26f) * inventoryScale, color, 0f, Vector2.Zero, new Vector2(inventoryScale), -1f, inventoryScale);
				}
				int num8 = -1;
				if (context == 13)
				{
					if (item.DD2Summon)
					{
						for (int i = 0; i < 58; i++)
						{
							if (inv[i].type == 3822)
							{
								num8 += inv[i].stack;
							}
						}
						if (num8 >= 0)
						{
							num8++;
						}
					}
					if (item.useAmmo > 0)
					{
						int useAmmo = item.useAmmo;
						num8 = 0;
						for (int j = 0; j < 58; j++)
						{
							if (inv[j].ammo == useAmmo)
							{
								num8 += inv[j].stack;
							}
						}
					}
					if (item.fishingPole > 0)
					{
						num8 = 0;
						for (int k = 0; k < 58; k++)
						{
							if (inv[k].bait > 0)
							{
								num8 += inv[k].stack;
							}
						}
					}
					if (item.tileWand > 0)
					{
						int tileWand = item.tileWand;
						num8 = 0;
						for (int l = 0; l < 58; l++)
						{
							if (inv[l].type == tileWand)
							{
								num8 += inv[l].stack;
							}
						}
					}
					if (item.type == 509 || item.type == 851 || item.type == 850 || item.type == 3612 || item.type == 3625 || item.type == 3611)
					{
						num8 = 0;
						for (int m = 0; m < 58; m++)
						{
							if (inv[m].type == 530)
							{
								num8 += inv[m].stack;
							}
						}
					}
				}
				if (num8 != -1)
				{
					ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, num8.ToString(), position + new Vector2(8f, 30f) * inventoryScale, color, 0f, Vector2.Zero, new Vector2(inventoryScale * 0.8f), -1f, inventoryScale);
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
					spriteBatch.Draw(TextureAssets.Cd.Value, position2, null, color3, 0f, default(Vector2), scale3, SpriteEffects.None, 0f);
				}
				if ((context == 10 || context == 18) && item.expertOnly && !Main.expertMode)
				{
					Vector2 position3 = position + value.Size() * inventoryScale / 2f - TextureAssets.Cd.Value.Size() * inventoryScale / 2f;
					Color white = Color.White;
					spriteBatch.Draw(TextureAssets.Cd.Value, position3, null, white, 0f, default(Vector2), scale3, SpriteEffects.None, 0f);
				}
			}
			else if (context == 6)
			{
				Texture2D value13 = TextureAssets.Trash.Value;
				Vector2 position4 = position + value.Size() * inventoryScale / 2f - value13.Size() * inventoryScale / 2f;
				spriteBatch.Draw(value13, position4, null, new Color(100, 100, 100, 100), 0f, default(Vector2), inventoryScale, SpriteEffects.None, 0f);
			}
			if (context == 0 && slot < 10)
			{
				float num9 = inventoryScale;
				string text2 = string.Concat(slot + 1);
				if (text2 == "10")
				{
					text2 = "0";
				}
				Color baseColor = Main.inventoryBack;
				int num10 = 0;
				if (Main.player[Main.myPlayer].selectedItem == slot)
				{
					baseColor = Color.White;
					baseColor.A = 200;
					num10 -= 2;
					num9 *= 1.4f;
				}
				ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text2, position + new Vector2(6f, (float)(4 + num10)) * inventoryScale, baseColor, 0f, Vector2.Zero, new Vector2(inventoryScale), -1f, inventoryScale);
			}
			if (gamepadPointForSlot != -1)
			{
				UILinkPointNavigator.SetPosition(gamepadPointForSlot, position + vector * 0.75f);
			}
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00498638 File Offset: 0x00496838
		public static Color GetColorByLoadout(int slot, int context)
		{
			Color color = Color.White;
			Color color2;
			if (ItemSlot.TryGetSlotColor(Main.LocalPlayer.CurrentLoadoutIndex, context, out color2))
			{
				color = color2;
			}
			Color value = new Color(color.ToVector4() * Main.inventoryBack.ToVector4());
			float num = Utils.Remap((float)(Main.timeForVisualEffects - ItemSlot._lastTimeForVisualEffectsThatLoadoutWasChanged), 0f, 30f, 0.5f, 0f, true);
			return Color.Lerp(value, Color.White, num * num * num);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x004986B2 File Offset: 0x004968B2
		public static void RecordLoadoutChange()
		{
			ItemSlot._lastTimeForVisualEffectsThatLoadoutWasChanged = Main.timeForVisualEffects;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x004986C0 File Offset: 0x004968C0
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

		// Token: 0x06001285 RID: 4741 RVA: 0x00498721 File Offset: 0x00496921
		public static float ShiftHueByLoadout(float hue, int loadoutIndex)
		{
			return (hue + (float)loadoutIndex / 8f) % 1f;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00498733 File Offset: 0x00496933
		public static Color GetLoadoutColor(int loadoutIndex)
		{
			return Main.hslToRgb(ItemSlot.ShiftHueByLoadout(0.41f, loadoutIndex), 0.7f, 0.5f, byte.MaxValue);
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00498754 File Offset: 0x00496954
		public static float DrawItemIcon(Item item, int context, SpriteBatch spriteBatch, Vector2 screenPositionForItemCenter, float scale, float sizeLimit, Color environmentColor)
		{
			int num = item.type;
			if (num - 5358 <= 3 && context == 31)
			{
				num = 5437;
			}
			Main.instance.LoadItem(num);
			Texture2D value = TextureAssets.Item[num].Value;
			Rectangle rectangle;
			if (Main.itemAnimations[num] != null)
			{
				rectangle = Main.itemAnimations[num].GetFrame(value, -1);
			}
			else
			{
				rectangle = value.Frame(1, 1, 0, 0, 0, 0);
			}
			Color newColor;
			float num2;
			ItemSlot.DrawItem_GetColorAndScale(item, scale, ref environmentColor, sizeLimit, ref rectangle, out newColor, out num2);
			SpriteEffects effects = SpriteEffects.None;
			Vector2 origin = rectangle.Size() / 2f;
			spriteBatch.Draw(value, screenPositionForItemCenter, new Rectangle?(rectangle), item.GetAlpha(newColor), 0f, origin, num2, effects, 0f);
			if (item.color != Color.Transparent)
			{
				Color newColor2 = environmentColor;
				if (context == 13)
				{
					newColor2.A = byte.MaxValue;
				}
				spriteBatch.Draw(value, screenPositionForItemCenter, new Rectangle?(rectangle), item.GetColor(newColor2), 0f, origin, num2, effects, 0f);
			}
			if (num - 5140 > 5)
			{
				if (num == 5146)
				{
					Texture2D value2 = TextureAssets.GlowMask[324].Value;
					Color color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
					spriteBatch.Draw(value2, screenPositionForItemCenter, new Rectangle?(rectangle), color, 0f, origin, num2, effects, 0f);
				}
			}
			else
			{
				Texture2D value3 = TextureAssets.GlowMask[(int)item.glowMask].Value;
				Color white = Color.White;
				spriteBatch.Draw(value3, screenPositionForItemCenter, new Rectangle?(rectangle), white, 0f, origin, num2, effects, 0f);
			}
			return num2;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x004988F4 File Offset: 0x00496AF4
		public static void DrawItem_GetColorAndScale(Item item, float scale, ref Color currentWhite, float sizeLimit, ref Rectangle frame, out Color itemLight, out float finalDrawScale)
		{
			itemLight = currentWhite;
			float num = 1f;
			ItemSlot.GetItemLight(ref itemLight, ref num, item, false);
			float num2 = 1f;
			if ((float)frame.Width > sizeLimit || (float)frame.Height > sizeLimit)
			{
				if (frame.Width > frame.Height)
				{
					num2 = sizeLimit / (float)frame.Width;
				}
				else
				{
					num2 = sizeLimit / (float)frame.Height;
				}
			}
			finalDrawScale = scale * num2 * num;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0049896C File Offset: 0x00496B6C
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
				int num = slot;
				if (num % 10 == 9 && !localPlayer.CanDemonHeartAccessoryBeShown())
				{
					num--;
				}
				result = 100 + num;
				break;
			}
			case 12:
				if (inv == localPlayer.dye)
				{
					int num2 = slot;
					if (num2 % 10 == 9 && !localPlayer.CanDemonHeartAccessoryBeShown())
					{
						num2--;
					}
					result = 120 + num2;
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

		// Token: 0x0600128A RID: 4746 RVA: 0x00498B6E File Offset: 0x00496D6E
		public static void MouseHover(int context = 0)
		{
			ItemSlot.singleSlotArray[0] = Main.HoverItem;
			ItemSlot.MouseHover(ItemSlot.singleSlotArray, context, 0);
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00498B88 File Offset: 0x00496D88
		public static void MouseHover(ref Item inv, int context = 0)
		{
			ItemSlot.singleSlotArray[0] = inv;
			ItemSlot.MouseHover(ItemSlot.singleSlotArray, context, 0);
			inv = ItemSlot.singleSlotArray[0];
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x00498BA8 File Offset: 0x00496DA8
		public static void MouseHover(Item[] inv, int context = 0, int slot = 0)
		{
			if (context == 6 && Main.hoverItemName == null)
			{
				Main.hoverItemName = Lang.inter[3].Value;
			}
			if (inv[slot].type > 0 && inv[slot].stack > 0)
			{
				ItemSlot._customCurrencyForSavings = inv[slot].shopSpecialCurrency;
				Main.hoverItemName = inv[slot].Name;
				if (inv[slot].stack > 1)
				{
					Main.hoverItemName = string.Concat(new object[]
					{
						Main.hoverItemName,
						" (",
						inv[slot].stack,
						")"
					});
				}
				Main.HoverItem = inv[slot].Clone();
				Main.HoverItem.tooltipContext = context;
				if (context == 8 && slot <= 2)
				{
					Main.HoverItem.wornArmor = true;
					return;
				}
				if (context == 11 || context == 9)
				{
					Main.HoverItem.social = true;
					return;
				}
				if (context == 15)
				{
					Main.HoverItem.buy = true;
					return;
				}
			}
			else
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
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00498E17 File Offset: 0x00497017
		public static void SwapEquip(ref Item inv, int context = 0)
		{
			ItemSlot.singleSlotArray[0] = inv;
			ItemSlot.SwapEquip(ItemSlot.singleSlotArray, context, 0);
			inv = ItemSlot.singleSlotArray[0];
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00498E38 File Offset: 0x00497038
		public static void SwapEquip(Item[] inv, int context, int slot)
		{
			Player player = Main.player[Main.myPlayer];
			if (ItemSlot.isEquipLocked(inv[slot].type))
			{
				return;
			}
			if (inv[slot].IsAir)
			{
				return;
			}
			if (inv[slot].dye > 0)
			{
				bool flag;
				inv[slot] = ItemSlot.DyeSwap(inv[slot], out flag);
				if (flag)
				{
					Main.EquipPageSelected = 0;
					AchievementsHelper.HandleOnEquip(player, inv[slot], 12);
				}
			}
			else if (Main.projHook[inv[slot].shoot])
			{
				bool flag;
				inv[slot] = ItemSlot.EquipSwap(inv[slot], player.miscEquips, 4, out flag);
				if (flag)
				{
					Main.EquipPageSelected = 2;
					AchievementsHelper.HandleOnEquip(player, inv[slot], 16);
				}
			}
			else if (inv[slot].mountType != -1 && !MountID.Sets.Cart[inv[slot].mountType])
			{
				bool flag;
				inv[slot] = ItemSlot.EquipSwap(inv[slot], player.miscEquips, 3, out flag);
				if (flag)
				{
					Main.EquipPageSelected = 2;
					AchievementsHelper.HandleOnEquip(player, inv[slot], 17);
				}
			}
			else if (inv[slot].mountType != -1 && MountID.Sets.Cart[inv[slot].mountType])
			{
				bool flag;
				inv[slot] = ItemSlot.EquipSwap(inv[slot], player.miscEquips, 2, out flag);
				if (flag)
				{
					Main.EquipPageSelected = 2;
				}
			}
			else if (inv[slot].buffType > 0 && Main.lightPet[inv[slot].buffType])
			{
				bool flag;
				inv[slot] = ItemSlot.EquipSwap(inv[slot], player.miscEquips, 1, out flag);
				if (flag)
				{
					Main.EquipPageSelected = 2;
				}
			}
			else if (inv[slot].buffType > 0 && Main.vanityPet[inv[slot].buffType])
			{
				bool flag;
				inv[slot] = ItemSlot.EquipSwap(inv[slot], player.miscEquips, 0, out flag);
				if (flag)
				{
					Main.EquipPageSelected = 2;
				}
			}
			else
			{
				Item item = inv[slot];
				bool flag;
				inv[slot] = ItemSlot.ArmorSwap(inv[slot], out flag);
				if (flag)
				{
					Main.EquipPageSelected = 0;
					AchievementsHelper.HandleOnEquip(player, item, item.accessory ? 10 : 8);
				}
			}
			Recipe.FindRecipes(false);
			if (context == 3 && Main.netMode == 1)
			{
				NetMessage.SendData(32, -1, -1, null, player.chest, (float)slot, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0049903E File Offset: 0x0049723E
		public static bool Equippable(ref Item inv, int context = 0)
		{
			ItemSlot.singleSlotArray[0] = inv;
			bool result = ItemSlot.Equippable(ItemSlot.singleSlotArray, context, 0);
			inv = ItemSlot.singleSlotArray[0];
			return result;
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00499060 File Offset: 0x00497260
		public static bool Equippable(Item[] inv, int context, int slot)
		{
			Player player = Main.player[Main.myPlayer];
			return inv[slot].dye > 0 || Main.projHook[inv[slot].shoot] || inv[slot].mountType != -1 || (inv[slot].buffType > 0 && Main.lightPet[inv[slot].buffType]) || (inv[slot].buffType > 0 && Main.vanityPet[inv[slot].buffType]) || inv[slot].headSlot >= 0 || inv[slot].bodySlot >= 0 || inv[slot].legSlot >= 0 || inv[slot].accessory;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00499104 File Offset: 0x00497304
		public static bool IsMiscEquipment(Item item)
		{
			return item.mountType != -1 || (item.buffType > 0 && Main.lightPet[item.buffType]) || (item.buffType > 0 && Main.vanityPet[item.buffType]) || Main.projHook[item.shoot];
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00499158 File Offset: 0x00497358
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

		// Token: 0x06001293 RID: 4755 RVA: 0x004991F8 File Offset: 0x004973F8
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
			if (ItemSlot.dyeSlotCount >= 10)
			{
				ItemSlot.dyeSlotCount = 0;
			}
			if (ItemSlot.dyeSlotCount < 0)
			{
				ItemSlot.dyeSlotCount = 9;
			}
			Item result = player.dye[ItemSlot.dyeSlotCount].Clone();
			player.dye[ItemSlot.dyeSlotCount] = item.Clone();
			ItemSlot.dyeSlotCount++;
			if (ItemSlot.dyeSlotCount >= 10)
			{
				ItemSlot.accSlotToSwapTo = 0;
			}
			SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			Recipe.FindRecipes(false);
			success = true;
			return result;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x004992C0 File Offset: 0x004974C0
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
			else if (item.accessory)
			{
				int num2 = 3;
				for (int i = 3; i < 10; i++)
				{
					if (player.IsItemSlotUnlockedAndUsable(i))
					{
						num2 = i;
						if (player.armor[i].type == 0)
						{
							ItemSlot.accSlotToSwapTo = i - 3;
							break;
						}
					}
				}
				for (int j = 0; j < player.armor.Length; j++)
				{
					if (item.IsTheSameAs(player.armor[j]))
					{
						ItemSlot.accSlotToSwapTo = j - 3;
					}
					if (j < 10 && item.wingSlot > 0 && player.armor[j].wingSlot > 0)
					{
						ItemSlot.accSlotToSwapTo = j - 3;
					}
				}
				if (ItemSlot.accSlotToSwapTo > num2)
				{
					return item;
				}
				if (ItemSlot.accSlotToSwapTo < 0)
				{
					ItemSlot.accSlotToSwapTo = num2 - 3;
				}
				int num3 = 3 + ItemSlot.accSlotToSwapTo;
				if (ItemSlot.isEquipLocked(player.armor[num3].type))
				{
					return item;
				}
				for (int k = 0; k < player.armor.Length; k++)
				{
					if (item.IsTheSameAs(player.armor[k]))
					{
						num3 = k;
					}
				}
				result = player.armor[num3].Clone();
				player.armor[num3] = item.Clone();
				ItemSlot.accSlotToSwapTo = 0;
			}
			SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			Recipe.FindRecipes(false);
			success = true;
			return result;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x004994FC File Offset: 0x004976FC
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

		// Token: 0x06001296 RID: 4758 RVA: 0x00499550 File Offset: 0x00497750
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
						int num = coinsArray[3 - i];
					}
					Vector2 vector = new Vector2(shopx + ChatManager.GetStringSize(FontAssets.MouseText.Value, text, Vector2.One, -1f).X + (float)(24 * i) + 45f, shopy + 50f);
					sb.Draw(TextureAssets.Item[74 - i].Value, vector, null, Color.White, 0f, TextureAssets.Item[74 - i].Value.Size() / 2f, 1f, SpriteEffects.None, 0f);
					Utils.DrawBorderStringFourWay(sb, FontAssets.ItemStack.Value, coinsArray[3 - i].ToString(), vector.X - 11f, vector.Y, Color.White, Color.Black, new Vector2(0.3f), 0.75f);
				}
				return;
			}
			for (int j = 0; j < 4; j++)
			{
				Main.instance.LoadItem(74 - j);
				int num2 = (j == 0 && coinsArray[3 - j] > 99) ? -6 : 0;
				sb.Draw(TextureAssets.Item[74 - j].Value, new Vector2(shopx + 11f + (float)(24 * j), shopy + 75f), null, Color.White, 0f, TextureAssets.Item[74 - j].Value.Size() / 2f, 1f, SpriteEffects.None, 0f);
				Utils.DrawBorderStringFourWay(sb, FontAssets.ItemStack.Value, coinsArray[3 - j].ToString(), shopx + (float)(24 * j) + (float)num2, shopy + 75f, Color.White, Color.Black, new Vector2(0.3f), 0.75f);
			}
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00499790 File Offset: 0x00497990
		public static void DrawSavings(SpriteBatch sb, float shopx, float shopy, bool horizontal = false)
		{
			Player player = Main.player[Main.myPlayer];
			if (ItemSlot._customCurrencyForSavings != -1)
			{
				CustomCurrencyManager.DrawSavings(sb, ItemSlot._customCurrencyForSavings, shopx, shopy, horizontal);
				return;
			}
			bool flag;
			long num = Utils.CoinsCount(out flag, player.bank.item, new int[0]);
			long num2 = Utils.CoinsCount(out flag, player.bank2.item, new int[0]);
			long num3 = Utils.CoinsCount(out flag, player.bank3.item, new int[0]);
			long num4 = Utils.CoinsCount(out flag, player.bank4.item, new int[0]);
			long num5 = Utils.CoinsCombineStacks(out flag, new long[]
			{
				num,
				num2,
				num3,
				num4
			});
			if (num5 > 0L)
			{
				Texture2D texture;
				Rectangle r;
				Main.GetItemDrawFrame(4076, out texture, out r);
				Texture2D texture2;
				Rectangle r2;
				Main.GetItemDrawFrame(3813, out texture2, out r2);
				Texture2D texture2D;
				Rectangle rectangle;
				Main.GetItemDrawFrame(346, out texture2D, out rectangle);
				Texture2D texture2D2;
				Rectangle rectangle2;
				Main.GetItemDrawFrame(87, out texture2D2, out rectangle2);
				if (num4 > 0L)
				{
					sb.Draw(texture, Utils.CenteredRectangle(new Vector2(shopx + 92f, shopy + 45f), r.Size() * 0.65f), null, Color.White);
				}
				if (num3 > 0L)
				{
					sb.Draw(texture2, Utils.CenteredRectangle(new Vector2(shopx + 92f, shopy + 45f), r2.Size() * 0.65f), null, Color.White);
				}
				if (num2 > 0L)
				{
					sb.Draw(texture2D, Utils.CenteredRectangle(new Vector2(shopx + 80f, shopy + 50f), texture2D.Size() * 0.65f), null, Color.White);
				}
				if (num > 0L)
				{
					sb.Draw(texture2D2, Utils.CenteredRectangle(new Vector2(shopx + 70f, shopy + 60f), texture2D2.Size() * 0.65f), null, Color.White);
				}
				ItemSlot.DrawMoney(sb, Lang.inter[66].Value, shopx, shopy, Utils.CoinsSplit(num5), horizontal);
			}
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x004999B8 File Offset: 0x00497BB8
		public static void GetItemLight(ref Color currentColor, Item item, bool outInTheWorld = false)
		{
			float num = 1f;
			ItemSlot.GetItemLight(ref currentColor, ref num, item, outInTheWorld);
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x004999D8 File Offset: 0x00497BD8
		public static void GetItemLight(ref Color currentColor, int type, bool outInTheWorld = false)
		{
			float num = 1f;
			ItemSlot.GetItemLight(ref currentColor, ref num, type, outInTheWorld);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x004999F6 File Offset: 0x00497BF6
		public static void GetItemLight(ref Color currentColor, ref float scale, Item item, bool outInTheWorld = false)
		{
			ItemSlot.GetItemLight(ref currentColor, ref scale, item.type, outInTheWorld);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00499A08 File Offset: 0x00497C08
		public static Color GetItemLight(ref Color currentColor, ref float scale, int type, bool outInTheWorld = false)
		{
			if (type < 0 || type > (int)ItemID.Count)
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

		// Token: 0x0600129C RID: 4764 RVA: 0x00499B7C File Offset: 0x00497D7C
		public static void DrawRadialCircular(SpriteBatch sb, Vector2 position, Player.SelectionRadial radial, Item[] items)
		{
			ItemSlot.CircularRadialOpacity = MathHelper.Clamp(ItemSlot.CircularRadialOpacity + ((PlayerInput.UsingGamepad && PlayerInput.Triggers.Current.RadialHotbar) ? 0.25f : -0.15f), 0f, 1f);
			if (ItemSlot.CircularRadialOpacity == 0f)
			{
				return;
			}
			Texture2D value = TextureAssets.HotbarRadial[2].Value;
			float scale = ItemSlot.CircularRadialOpacity * 0.9f;
			float num = ItemSlot.CircularRadialOpacity * 1f;
			float num2 = (float)Main.mouseTextColor / 255f;
			float num3 = 1f - (1f - num2) * (1f - num2);
			num3 *= 0.785f;
			Color value2 = Color.White * num3 * scale;
			value = TextureAssets.HotbarRadial[1].Value;
			float num4 = 6.2831855f / (float)radial.RadialCount;
			float num5 = -1.5707964f;
			for (int i = 0; i < radial.RadialCount; i++)
			{
				int num6 = radial.Bindings[i];
				Vector2 value3 = new Vector2(150f, 0f).RotatedBy((double)(num5 + num4 * (float)i), default(Vector2)) * num;
				float num7 = 0.85f;
				if (radial.SelectedBinding == i)
				{
					num7 = 1.7f;
				}
				sb.Draw(value, position + value3, null, value2 * num7, 0f, value.Size() / 2f, num * num7, SpriteEffects.None, 0f);
				if (num6 != -1)
				{
					float inventoryScale = Main.inventoryScale;
					Main.inventoryScale = num * num7;
					ItemSlot.Draw(sb, items, 14, num6, position + value3 + new Vector2(-26f * num * num7), Color.White);
					Main.inventoryScale = inventoryScale;
				}
			}
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00499D54 File Offset: 0x00497F54
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
			float scale = ItemSlot.QuicksRadialOpacity * 0.9f;
			float num = ItemSlot.QuicksRadialOpacity * 1f;
			float num2 = (float)Main.mouseTextColor / 255f;
			float num3 = 1f - (1f - num2) * (1f - num2);
			num3 *= 0.785f;
			Color value3 = Color.White * num3 * scale;
			float num4 = 6.2831855f / (float)player.QuicksRadial.RadialCount;
			float num5 = -1.5707964f;
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
				Item item5 = item4;
				if (i == 1)
				{
					item5 = item;
				}
				if (i == 2)
				{
					item5 = item3;
				}
				if (i == 3)
				{
					item5 = item2;
				}
				int num6 = player.QuicksRadial.Bindings[i];
				Vector2 value4 = new Vector2(120f, 0f).RotatedBy((double)(num5 + num4 * (float)i), default(Vector2)) * num;
				float num7 = 0.85f;
				if (player.QuicksRadial.SelectedBinding == i)
				{
					num7 = 1.7f;
				}
				sb.Draw(value, position + value4, null, value3 * num7, 0f, value.Size() / 2f, num * num7 * 1.3f, SpriteEffects.None, 0f);
				float inventoryScale = Main.inventoryScale;
				Main.inventoryScale = num * num7;
				ItemSlot.Draw(sb, ref item5, 14, position + value4 + new Vector2(-26f * num * num7), Color.White);
				Main.inventoryScale = inventoryScale;
				sb.Draw(value2, position + value4 + new Vector2(34f, 20f) * 0.85f * num * num7, null, value3 * num7, 0f, value.Size() / 2f, num * num7 * 1.3f, SpriteEffects.None, 0f);
			}
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0049A050 File Offset: 0x00498250
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
			sb.Draw(value, position, null, color, 0f, value.Size() / 2f, Main.inventoryScale, SpriteEffects.None, 0f);
			for (int i = 0; i < 4; i++)
			{
				int num3 = player.DpadRadial.Bindings[i];
				if (num3 != -1)
				{
					ItemSlot.Draw(sb, player.inventory, 14, num3, position + new Vector2((float)(value.Width / 3), 0f).RotatedBy((double)(-1.5707964f + 1.5707964f * (float)i), default(Vector2)) + new Vector2(-26f * Main.inventoryScale), Color.White);
				}
			}
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0049A18A File Offset: 0x0049838A
		public static string GetGamepadInstructions(ref Item inv, int context = 0)
		{
			ItemSlot.singleSlotArray[0] = inv;
			string gamepadInstructions = ItemSlot.GetGamepadInstructions(ItemSlot.singleSlotArray, context, 0);
			inv = ItemSlot.singleSlotArray[0];
			return gamepadInstructions;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0049A1AA File Offset: 0x004983AA
		public static bool CanExecuteCommand()
		{
			return PlayerInput.AllowExecutionOfGamepadInstructions;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0049A1B4 File Offset: 0x004983B4
		public static string GetGamepadInstructions(Item[] inv, int context = 0, int slot = 0)
		{
			Player player = Main.player[Main.myPlayer];
			string text = "";
			if (inv == null || inv[slot] == null || Main.mouseItem == null)
			{
				return text;
			}
			if (context == 0 || context == 1 || context == 2)
			{
				if (inv[slot].type > 0 && inv[slot].stack > 0)
				{
					if (Main.mouseItem.type > 0)
					{
						text += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
						if (inv[slot].type == Main.mouseItem.type && Main.mouseItem.stack < inv[slot].maxStack && inv[slot].maxStack > 1)
						{
							text += PlayerInput.BuildCommand(Lang.misc[55].Value, false, new List<string>[]
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
						text += PlayerInput.BuildCommand(Lang.misc[54].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
						if (inv[slot].maxStack > 1)
						{
							text += PlayerInput.BuildCommand(Lang.misc[55].Value, false, new List<string>[]
							{
								PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
							});
						}
					}
					if (inv[slot].maxStack == 1 && ItemSlot.Equippable(inv, context, slot))
					{
						text += PlayerInput.BuildCommand(Lang.misc[67].Value, false, new List<string>[]
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
					text += PlayerInput.BuildCommand(Lang.misc[83].Value, false, new List<string>[]
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
					text += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
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
						text += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
						if (inv[slot].type == Main.mouseItem.type && Main.mouseItem.stack < inv[slot].maxStack && inv[slot].maxStack > 1)
						{
							text += PlayerInput.BuildCommand(Lang.misc[55].Value, false, new List<string>[]
							{
								PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
							});
						}
					}
					else
					{
						text += PlayerInput.BuildCommand(Lang.misc[54].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
						if (inv[slot].maxStack > 1)
						{
							text += PlayerInput.BuildCommand(Lang.misc[55].Value, false, new List<string>[]
							{
								PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
							});
						}
					}
					if (inv[slot].maxStack == 1 && ItemSlot.Equippable(inv, context, slot))
					{
						text += PlayerInput.BuildCommand(Lang.misc[67].Value, false, new List<string>[]
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
						text += PlayerInput.BuildCommand(Lang.misc[83].Value, false, new List<string>[]
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
					text += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
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
							text += PlayerInput.BuildCommand(Lang.misc[91].Value, false, new List<string>[]
							{
								PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
							});
						}
					}
					else
					{
						text += PlayerInput.BuildCommand(Lang.misc[90].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"],
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
						});
					}
				}
				else if (Main.mouseItem.type > 0)
				{
					text += PlayerInput.BuildCommand(Lang.misc[92].Value, false, new List<string>[]
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
							text += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
							{
								PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
							});
						}
					}
					else if (context != 8 || !ItemSlot.isEquipLocked(inv[slot].type))
					{
						text += PlayerInput.BuildCommand(Lang.misc[54].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
					}
					if (context == 8 && slot >= 3)
					{
						bool flag = player.hideVisibleAccessory[slot];
						text += PlayerInput.BuildCommand(Lang.misc[flag ? 77 : 78].Value, false, new List<string>[]
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
						text += PlayerInput.BuildCommand(Lang.misc[flag2 ? 77 : 78].Value, false, new List<string>[]
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
						text += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
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
						text += PlayerInput.BuildCommand(Lang.misc[flag3 ? 77 : 78].Value, false, new List<string>[]
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
						text += PlayerInput.BuildCommand(Lang.misc[flag4 ? 77 : 78].Value, false, new List<string>[]
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
			if (context == 12 || context == 25 || context == 27 || context == 33)
			{
				if (inv[slot].type > 0 && inv[slot].stack > 0)
				{
					if (Main.mouseItem.type > 0)
					{
						if (Main.mouseItem.dye > 0)
						{
							text += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
							{
								PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
							});
						}
					}
					else
					{
						text += PlayerInput.BuildCommand(Lang.misc[54].Value, false, new List<string>[]
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
								bool flag5 = player.hideVisibleAccessory[slot];
								text += PlayerInput.BuildCommand(Lang.misc[flag5 ? 77 : 78].Value, false, new List<string>[]
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
								bool flag6 = player.hideMisc[slot];
								text += PlayerInput.BuildCommand(Lang.misc[flag6 ? 77 : 78].Value, false, new List<string>[]
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
					text += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
					});
				}
				return text;
			}
			if (context == 18)
			{
				if (inv[slot].type > 0 && inv[slot].stack > 0)
				{
					if (Main.mouseItem.type > 0)
					{
						if (Main.mouseItem.dye > 0)
						{
							text += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
							{
								PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
							});
						}
					}
					else
					{
						text += PlayerInput.BuildCommand(Lang.misc[54].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
					}
				}
				else if (Main.mouseItem.type > 0 && Main.mouseItem.dye > 0)
				{
					text += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
					});
				}
				bool enabledSuperCart = player.enabledSuperCart;
				text += PlayerInput.BuildCommand(Language.GetTextValue((!enabledSuperCart) ? "UI.EnableSuperCart" : "UI.DisableSuperCart"), false, new List<string>[]
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
				return text;
			}
			if (context == 6)
			{
				if (inv[slot].type > 0 && inv[slot].stack > 0)
				{
					if (Main.mouseItem.type > 0)
					{
						text += PlayerInput.BuildCommand(Lang.misc[74].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
					}
					else
					{
						text += PlayerInput.BuildCommand(Lang.misc[54].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
					}
				}
				else if (Main.mouseItem.type > 0)
				{
					text += PlayerInput.BuildCommand(Lang.misc[74].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
					});
				}
				return text;
			}
			if (context == 5 || context == 7)
			{
				bool flag7 = false;
				if (context == 5)
				{
					flag7 = (Main.mouseItem.Prefix(-3) || Main.mouseItem.type == 0);
				}
				if (context == 7)
				{
					flag7 = Main.mouseItem.material;
				}
				if (inv[slot].type > 0 && inv[slot].stack > 0)
				{
					if (Main.mouseItem.type > 0)
					{
						if (flag7)
						{
							text += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
							{
								PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
							});
						}
					}
					else
					{
						text += PlayerInput.BuildCommand(Lang.misc[54].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
					}
				}
				else if (Main.mouseItem.type > 0 && flag7)
				{
					text += PlayerInput.BuildCommand(Lang.misc[65].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
					});
				}
				return text;
			}
			string overrideInstructions = ItemSlot.GetOverrideInstructions(inv, context, slot);
			bool flag8 = Main.mouseItem.type > 0 && (context == 0 || context == 1 || context == 2 || context == 6 || context == 15 || context == 7 || context == 4 || context == 32 || context == 3);
			if (context != 8 || !ItemSlot.isEquipLocked(inv[slot].type))
			{
				if (flag8 && string.IsNullOrEmpty(overrideInstructions))
				{
					text += PlayerInput.BuildCommand(Lang.inter[121].Value, false, new List<string>[]
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
						text += PlayerInput.BuildCommand(overrideInstructions, false, new List<string>[]
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
			if (!ItemSlot.TryEnteringFastUseMode(inv, context, slot, player, ref text))
			{
				ItemSlot.TryEnteringBuildingMode(inv, context, slot, player, ref text);
			}
			return text;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0049B59B File Offset: 0x0049979B
		private static bool CanDoSimulatedClickAction()
		{
			return !PlayerInput.SteamDeckIsUsed || UILinkPointNavigator.InUse;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0049B5AC File Offset: 0x004997AC
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
					if (num == 1)
					{
						PlayerInput.TryEnteringFastUseModeForMouseItem();
					}
					else if (num == 2)
					{
						PlayerInput.TryEnteringFastUseModeForInventorySlot(slot);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0049B654 File Offset: 0x00499854
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

		// Token: 0x060012A5 RID: 4773 RVA: 0x0049B7C2 File Offset: 0x004999C2
		public static bool IsABuildingItem(Item item)
		{
			return item.type > 0 && item.stack > 0 && item.useStyle != 0 && item.useTime > 0;
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0049B7EC File Offset: 0x004999EC
		public static void SelectEquipPage(Item item)
		{
			Main.EquipPage = -1;
			if (item.IsAir)
			{
				return;
			}
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

		// Token: 0x04001018 RID: 4120
		public static bool DrawGoldBGForCraftingMaterial = false;

		// Token: 0x04001019 RID: 4121
		public static bool ShiftForcedOn;

		// Token: 0x0400101B RID: 4123
		private static Item[] singleSlotArray = new Item[1];

		// Token: 0x0400101C RID: 4124
		private static bool[] canFavoriteAt = new bool[ItemSlot.Context.Count];

		// Token: 0x0400101D RID: 4125
		private static bool[] canShareAt = new bool[ItemSlot.Context.Count];

		// Token: 0x0400101E RID: 4126
		private static float[] inventoryGlowHue = new float[58];

		// Token: 0x0400101F RID: 4127
		private static int[] inventoryGlowTime = new int[58];

		// Token: 0x04001020 RID: 4128
		private static float[] inventoryGlowHueChest = new float[58];

		// Token: 0x04001021 RID: 4129
		private static int[] inventoryGlowTimeChest = new int[58];

		// Token: 0x04001022 RID: 4130
		private static int _customCurrencyForSavings = -1;

		// Token: 0x04001023 RID: 4131
		public static bool forceClearGlowsOnChest = false;

		// Token: 0x04001024 RID: 4132
		private static double _lastTimeForVisualEffectsThatLoadoutWasChanged;

		// Token: 0x04001025 RID: 4133
		private static Color[,] LoadoutSlotColors;

		// Token: 0x04001026 RID: 4134
		private static int dyeSlotCount;

		// Token: 0x04001027 RID: 4135
		private static int accSlotToSwapTo;

		// Token: 0x04001028 RID: 4136
		public static float CircularRadialOpacity;

		// Token: 0x04001029 RID: 4137
		public static float QuicksRadialOpacity;

		// Token: 0x02000544 RID: 1348
		public class Options
		{
			// Token: 0x04005870 RID: 22640
			public static bool DisableLeftShiftTrashCan = true;

			// Token: 0x04005871 RID: 22641
			public static bool DisableQuickTrash = false;

			// Token: 0x04005872 RID: 22642
			public static bool HighlightNewItems = true;
		}

		// Token: 0x02000545 RID: 1349
		public class Context
		{
			// Token: 0x04005873 RID: 22643
			public const int InventoryItem = 0;

			// Token: 0x04005874 RID: 22644
			public const int InventoryCoin = 1;

			// Token: 0x04005875 RID: 22645
			public const int InventoryAmmo = 2;

			// Token: 0x04005876 RID: 22646
			public const int ChestItem = 3;

			// Token: 0x04005877 RID: 22647
			public const int BankItem = 4;

			// Token: 0x04005878 RID: 22648
			public const int PrefixItem = 5;

			// Token: 0x04005879 RID: 22649
			public const int TrashItem = 6;

			// Token: 0x0400587A RID: 22650
			public const int GuideItem = 7;

			// Token: 0x0400587B RID: 22651
			public const int EquipArmor = 8;

			// Token: 0x0400587C RID: 22652
			public const int EquipArmorVanity = 9;

			// Token: 0x0400587D RID: 22653
			public const int EquipAccessory = 10;

			// Token: 0x0400587E RID: 22654
			public const int EquipAccessoryVanity = 11;

			// Token: 0x0400587F RID: 22655
			public const int EquipDye = 12;

			// Token: 0x04005880 RID: 22656
			public const int HotbarItem = 13;

			// Token: 0x04005881 RID: 22657
			public const int ChatItem = 14;

			// Token: 0x04005882 RID: 22658
			public const int ShopItem = 15;

			// Token: 0x04005883 RID: 22659
			public const int EquipGrapple = 16;

			// Token: 0x04005884 RID: 22660
			public const int EquipMount = 17;

			// Token: 0x04005885 RID: 22661
			public const int EquipMinecart = 18;

			// Token: 0x04005886 RID: 22662
			public const int EquipPet = 19;

			// Token: 0x04005887 RID: 22663
			public const int EquipLight = 20;

			// Token: 0x04005888 RID: 22664
			public const int MouseItem = 21;

			// Token: 0x04005889 RID: 22665
			public const int CraftingMaterial = 22;

			// Token: 0x0400588A RID: 22666
			public const int DisplayDollArmor = 23;

			// Token: 0x0400588B RID: 22667
			public const int DisplayDollAccessory = 24;

			// Token: 0x0400588C RID: 22668
			public const int DisplayDollDye = 25;

			// Token: 0x0400588D RID: 22669
			public const int HatRackHat = 26;

			// Token: 0x0400588E RID: 22670
			public const int HatRackDye = 27;

			// Token: 0x0400588F RID: 22671
			public const int GoldDebug = 28;

			// Token: 0x04005890 RID: 22672
			public const int CreativeInfinite = 29;

			// Token: 0x04005891 RID: 22673
			public const int CreativeSacrifice = 30;

			// Token: 0x04005892 RID: 22674
			public const int InWorld = 31;

			// Token: 0x04005893 RID: 22675
			public const int VoidItem = 32;

			// Token: 0x04005894 RID: 22676
			public const int EquipMiscDye = 33;

			// Token: 0x04005895 RID: 22677
			public static readonly int Count = 34;
		}

		// Token: 0x02000546 RID: 1350
		public struct ItemTransferInfo
		{
			// Token: 0x060030E2 RID: 12514 RVA: 0x005E4B03 File Offset: 0x005E2D03
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

			// Token: 0x04005896 RID: 22678
			public int ItemType;

			// Token: 0x04005897 RID: 22679
			public int TransferAmount;

			// Token: 0x04005898 RID: 22680
			public int FromContenxt;

			// Token: 0x04005899 RID: 22681
			public int ToContext;
		}

		// Token: 0x02000547 RID: 1351
		// (Invoke) Token: 0x060030E4 RID: 12516
		public delegate void ItemTransferEvent(ItemSlot.ItemTransferInfo info);
	}
}
