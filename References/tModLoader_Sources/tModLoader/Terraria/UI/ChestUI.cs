using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using Terraria.UI.Gamepad;

namespace Terraria.UI
{
	// Token: 0x0200009E RID: 158
	public class ChestUI
	{
		// Token: 0x060014C5 RID: 5317 RVA: 0x004A3CC4 File Offset: 0x004A1EC4
		public static void UpdateHover(int ID, bool hovering)
		{
			if (hovering)
			{
				if (!ChestUI.ButtonHovered[ID])
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
				ChestUI.ButtonHovered[ID] = true;
				ChestUI.ButtonScale[ID] += 0.05f;
				if (ChestUI.ButtonScale[ID] > 1f)
				{
					ChestUI.ButtonScale[ID] = 1f;
					return;
				}
			}
			else
			{
				ChestUI.ButtonHovered[ID] = false;
				ChestUI.ButtonScale[ID] -= 0.05f;
				if (ChestUI.ButtonScale[ID] < 0.75f)
				{
					ChestUI.ButtonScale[ID] = 0.75f;
				}
			}
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x004A3D60 File Offset: 0x004A1F60
		public static void Draw(SpriteBatch spritebatch)
		{
			if (Main.player[Main.myPlayer].chest != -1 && !Main.recBigList)
			{
				Main.inventoryScale = 0.755f;
				if (Utils.FloatIntersect((float)Main.mouseX, (float)Main.mouseY, 0f, 0f, 73f, (float)Main.instance.invBottom, 560f * Main.inventoryScale, 224f * Main.inventoryScale))
				{
					Main.player[Main.myPlayer].mouseInterface = true;
				}
				ChestUI.DrawName(spritebatch);
				ChestUI.DrawButtons(spritebatch);
				ChestUI.DrawSlots(spritebatch);
				return;
			}
			for (int i = 0; i < ChestUI.ButtonID.Count; i++)
			{
				ChestUI.ButtonScale[i] = 0.75f;
				ChestUI.ButtonHovered[i] = false;
			}
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x004A3E1C File Offset: 0x004A201C
		private unsafe static void DrawName(SpriteBatch spritebatch)
		{
			Player player = Main.player[Main.myPlayer];
			string text = string.Empty;
			if (Main.editChest)
			{
				text = Main.npcChatText;
				Main.instance.textBlinkerCount++;
				if (Main.instance.textBlinkerCount >= 20)
				{
					if (Main.instance.textBlinkerState == 0)
					{
						Main.instance.textBlinkerState = 1;
					}
					else
					{
						Main.instance.textBlinkerState = 0;
					}
					Main.instance.textBlinkerCount = 0;
				}
				if (Main.instance.textBlinkerState == 1)
				{
					text += "|";
				}
				Main.instance.DrawWindowsIMEPanel(new Vector2(120f, 518f), 0f);
			}
			else if (player.chest > -1)
			{
				if (Main.chest[player.chest] == null)
				{
					Main.chest[player.chest] = new Chest(false);
				}
				Chest chest = Main.chest[player.chest];
				if (chest.name != "")
				{
					text = chest.name;
				}
				else
				{
					Tile tile = Main.tile[player.chestX, player.chestY];
					if (*tile.type == 21)
					{
						text = Lang.chestType[(int)(*tile.frameX / 36)].Value;
					}
					else if (*tile.type == 467 && *tile.frameX / 36 == 4)
					{
						text = Lang.GetItemNameValue(3988);
					}
					else if (*tile.type == 467)
					{
						text = Lang.chestType2[(int)(*tile.frameX / 36)].Value;
					}
					else if (*tile.type == 88)
					{
						text = Lang.dresserType[(int)(*tile.frameX / 54)].Value;
					}
					else if (TileID.Sets.BasicChest[(int)(*Main.tile[player.chestX, player.chestY].type)] || TileID.Sets.BasicDresser[(int)(*Main.tile[player.chestX, player.chestY].type)])
					{
						text = TileLoader.DefaultContainerName((int)(*tile.type), (int)(*tile.TileFrameX), (int)(*tile.TileFrameY));
					}
				}
			}
			else if (player.chest == -2)
			{
				text = Lang.inter[32].Value;
			}
			else if (player.chest == -3)
			{
				text = Lang.inter[33].Value;
			}
			else if (player.chest == -4)
			{
				text = Lang.GetItemNameValue(3813);
			}
			else if (player.chest == -5)
			{
				text = Lang.GetItemNameValue(4076);
			}
			Color color;
			color..ctor((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			color = Color.White * (1f - (255f - (float)Main.mouseTextColor) / 255f * 0.5f);
			color.A = byte.MaxValue;
			int lineAmount;
			Utils.WordwrapString(text, FontAssets.MouseText.Value, 200, 1, out lineAmount);
			lineAmount++;
			for (int i = 0; i < lineAmount; i++)
			{
				ChatManager.DrawColorCodedStringWithShadow(spritebatch, FontAssets.MouseText.Value, text, new Vector2(504f, (float)(Main.instance.invBottom + i * 26)), color, 0f, Vector2.Zero, Vector2.One, -1f, 1.5f);
			}
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x004A4188 File Offset: 0x004A2388
		private static void DrawButtons(SpriteBatch spritebatch)
		{
			for (int i = 0; i < ChestUI.ButtonID.Count; i++)
			{
				ChestUI.DrawButton(spritebatch, i, 506, Main.instance.invBottom + 40);
			}
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x004A41C0 File Offset: 0x004A23C0
		private static void DrawButton(SpriteBatch spriteBatch, int ID, int X, int Y)
		{
			Player player = Main.player[Main.myPlayer];
			if ((ID == 5 && player.chest < -1) || (ID == 6 && !Main.editChest))
			{
				ChestUI.UpdateHover(ID, false);
				return;
			}
			if (ID == 7 && player.chest != -5)
			{
				ChestUI.UpdateHover(ID, false);
				return;
			}
			int num = ID;
			if (ID == 7)
			{
				num = 5;
			}
			Y += num * 26;
			float num2 = ChestUI.ButtonScale[ID];
			string text = "";
			switch (ID)
			{
			case 0:
				text = Lang.inter[29].Value;
				break;
			case 1:
				text = Lang.inter[30].Value;
				break;
			case 2:
				text = Lang.inter[31].Value;
				break;
			case 3:
				text = Lang.inter[82].Value;
				break;
			case 4:
				text = Lang.inter[122].Value;
				break;
			case 5:
				text = Lang.inter[Main.editChest ? 47 : 61].Value;
				break;
			case 6:
				text = Lang.inter[63].Value;
				break;
			case 7:
				text = ((!player.IsVoidVaultEnabled) ? Language.GetTextValue("UI.ToggleBank4VacuumIsOff") : Language.GetTextValue("UI.ToggleBank4VacuumIsOn"));
				break;
			}
			Vector2 vector = FontAssets.MouseText.Value.MeasureString(text);
			Color color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor) * num2;
			color = Color.White * 0.97f * (1f - (255f - (float)Main.mouseTextColor) / 255f * 0.5f);
			color.A = byte.MaxValue;
			X += (int)(vector.X * num2 / 2f);
			bool flag = Utils.FloatIntersect((float)Main.mouseX, (float)Main.mouseY, 0f, 0f, (float)X - vector.X / 2f, (float)(Y - 12), vector.X, 24f);
			if (ChestUI.ButtonHovered[ID])
			{
				flag = Utils.FloatIntersect((float)Main.mouseX, (float)Main.mouseY, 0f, 0f, (float)X - vector.X / 2f - 10f, (float)(Y - 12), vector.X + 16f, 24f);
			}
			if (flag)
			{
				color = Main.OurFavoriteColor;
			}
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, text, new Vector2((float)X, (float)Y), color, 0f, vector / 2f, new Vector2(num2), -1f, 1.5f);
			vector *= num2;
			switch (ID)
			{
			case 0:
				UILinkPointNavigator.SetPosition(500, new Vector2((float)X - vector.X * num2 / 2f * 0.8f, (float)Y));
				break;
			case 1:
				UILinkPointNavigator.SetPosition(501, new Vector2((float)X - vector.X * num2 / 2f * 0.8f, (float)Y));
				break;
			case 2:
				UILinkPointNavigator.SetPosition(502, new Vector2((float)X - vector.X * num2 / 2f * 0.8f, (float)Y));
				break;
			case 3:
				UILinkPointNavigator.SetPosition(503, new Vector2((float)X - vector.X * num2 / 2f * 0.8f, (float)Y));
				break;
			case 4:
				UILinkPointNavigator.SetPosition(505, new Vector2((float)X - vector.X * num2 / 2f * 0.8f, (float)Y));
				break;
			case 5:
				UILinkPointNavigator.SetPosition(504, new Vector2((float)X, (float)Y));
				break;
			case 6:
				UILinkPointNavigator.SetPosition(504, new Vector2((float)X, (float)Y));
				break;
			case 7:
				UILinkPointNavigator.SetPosition(506, new Vector2((float)X - vector.X * num2 / 2f * 0.8f, (float)Y));
				break;
			}
			if (!flag)
			{
				ChestUI.UpdateHover(ID, false);
				return;
			}
			ChestUI.UpdateHover(ID, true);
			if (PlayerInput.IgnoreMouseInterface)
			{
				return;
			}
			player.mouseInterface = true;
			if (Main.mouseLeft && Main.mouseLeftRelease)
			{
				switch (ID)
				{
				case 0:
					ChestUI.LootAll();
					break;
				case 1:
					ChestUI.DepositAll(ContainerTransferContext.FromUnknown(player));
					break;
				case 2:
					ChestUI.QuickStack(ContainerTransferContext.FromUnknown(player), false);
					break;
				case 3:
					ChestUI.Restock();
					break;
				case 4:
					ItemSorting.SortChest();
					break;
				case 5:
					ChestUI.RenameChest();
					break;
				case 6:
					ChestUI.RenameChestCancel();
					break;
				case 7:
					ChestUI.ToggleVacuum();
					break;
				}
				Recipe.FindRecipes(false);
			}
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x004A4662 File Offset: 0x004A2862
		private static void ToggleVacuum()
		{
			Player player = Main.player[Main.myPlayer];
			player.IsVoidVaultEnabled = !player.IsVoidVaultEnabled;
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x004A4680 File Offset: 0x004A2880
		private static void DrawSlots(SpriteBatch spriteBatch)
		{
			Player player = Main.player[Main.myPlayer];
			int context = 0;
			Item[] inv = null;
			if (player.chest > -1)
			{
				context = 3;
				inv = Main.chest[player.chest].item;
			}
			if (player.chest == -2)
			{
				context = 4;
				inv = player.bank.item;
			}
			if (player.chest == -3)
			{
				context = 4;
				inv = player.bank2.item;
			}
			if (player.chest == -4)
			{
				context = 4;
				inv = player.bank3.item;
			}
			if (player.chest == -5)
			{
				context = 32;
				inv = player.bank4.item;
			}
			Main.inventoryScale = 0.755f;
			if (Utils.FloatIntersect((float)Main.mouseX, (float)Main.mouseY, 0f, 0f, 73f, (float)Main.instance.invBottom, 560f * Main.inventoryScale, 224f * Main.inventoryScale) && !PlayerInput.IgnoreMouseInterface)
			{
				player.mouseInterface = true;
			}
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					int num = (int)(73f + (float)(i * 56) * Main.inventoryScale);
					int num2 = (int)((float)Main.instance.invBottom + (float)(j * 56) * Main.inventoryScale);
					int slot = i + j * 10;
					new Color(100, 100, 100, 100);
					if (Utils.FloatIntersect((float)Main.mouseX, (float)Main.mouseY, 0f, 0f, (float)num, (float)num2, (float)TextureAssets.InventoryBack.Width() * Main.inventoryScale, (float)TextureAssets.InventoryBack.Height() * Main.inventoryScale) && !PlayerInput.IgnoreMouseInterface)
					{
						player.mouseInterface = true;
						ItemSlot.Handle(inv, context, slot);
					}
					ItemSlot.Draw(spriteBatch, inv, context, slot, new Vector2((float)num, (float)num2), default(Color));
				}
			}
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x004A4860 File Offset: 0x004A2A60
		public static void LootAll()
		{
			GetItemSettings lootAllSettings = GetItemSettings.LootAllSettings;
			Player player = Main.player[Main.myPlayer];
			if (player.chest > -1)
			{
				GetItemSettings lootAllSettingsRegularChest = GetItemSettings.LootAllSettingsRegularChest;
				Chest chest = Main.chest[player.chest];
				for (int i = 0; i < 40; i++)
				{
					if (chest.item[i].type > 0)
					{
						chest.item[i].position = player.Center;
						chest.item[i] = player.GetItem(Main.myPlayer, chest.item[i], lootAllSettingsRegularChest);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(32, -1, -1, null, player.chest, (float)i, 0f, 0f, 0, 0, 0);
						}
					}
				}
				return;
			}
			if (player.chest == -3)
			{
				for (int j = 0; j < 40; j++)
				{
					if (player.bank2.item[j].type > 0)
					{
						player.bank2.item[j].position = player.Center;
						player.bank2.item[j] = player.GetItem(Main.myPlayer, player.bank2.item[j], lootAllSettings);
					}
				}
				return;
			}
			if (player.chest == -4)
			{
				for (int k = 0; k < 40; k++)
				{
					if (player.bank3.item[k].type > 0)
					{
						player.bank3.item[k].position = player.Center;
						player.bank3.item[k] = player.GetItem(Main.myPlayer, player.bank3.item[k], lootAllSettings);
					}
				}
				return;
			}
			if (player.chest == -5)
			{
				for (int l = 0; l < 40; l++)
				{
					if (player.bank4.item[l].type > 0 && !player.bank4.item[l].favorited)
					{
						player.bank4.item[l].position = player.Center;
						player.bank4.item[l] = player.GetItem(Main.myPlayer, player.bank4.item[l], lootAllSettings);
					}
				}
				return;
			}
			for (int m = 0; m < 40; m++)
			{
				if (player.bank.item[m].type > 0)
				{
					player.bank.item[m].position = player.Center;
					player.bank.item[m] = player.GetItem(Main.myPlayer, player.bank.item[m], lootAllSettings);
				}
			}
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x004A4AF4 File Offset: 0x004A2CF4
		public static void DepositAll(ContainerTransferContext context)
		{
			Player player = Main.player[Main.myPlayer];
			if (player.chest > -1)
			{
				ChestUI.MoveCoins(player.inventory, Main.chest[player.chest].item, context);
			}
			else if (player.chest == -3)
			{
				ChestUI.MoveCoins(player.inventory, player.bank2.item, context);
			}
			else if (player.chest == -4)
			{
				ChestUI.MoveCoins(player.inventory, player.bank3.item, context);
			}
			else if (player.chest == -5)
			{
				ChestUI.MoveCoins(player.inventory, player.bank4.item, context);
			}
			else
			{
				ChestUI.MoveCoins(player.inventory, player.bank.item, context);
			}
			for (int num = 49; num >= 10; num--)
			{
				if (player.inventory[num].stack > 0 && player.inventory[num].type > 0 && !player.inventory[num].favorited)
				{
					if (player.inventory[num].maxStack > 1)
					{
						for (int i = 0; i < 40; i++)
						{
							int num2;
							if (player.chest > -1)
							{
								Chest chest = Main.chest[player.chest];
								if (chest.item[i].stack < chest.item[i].maxStack && player.inventory[num].IsTheSameAs(chest.item[i]) && ItemLoader.TryStackItems(chest.item[i], player.inventory[num], out num2, false))
								{
									SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
									if (player.inventory[num].stack <= 0)
									{
										player.inventory[num].SetDefaults(0);
										if (Main.netMode == 1)
										{
											NetMessage.SendData(32, -1, -1, null, player.chest, (float)i, 0f, 0f, 0, 0, 0);
											break;
										}
										break;
									}
									else
									{
										if (chest.item[i].type == 0)
										{
											chest.item[i] = player.inventory[num].Clone();
											player.inventory[num].SetDefaults(0);
										}
										if (Main.netMode == 1)
										{
											NetMessage.SendData(32, -1, -1, null, player.chest, (float)i, 0f, 0f, 0, 0, 0);
										}
									}
								}
							}
							else if (player.chest == -3)
							{
								if (player.bank2.item[i].stack < player.bank2.item[i].maxStack && player.inventory[num].IsTheSameAs(player.bank2.item[i]) && ItemLoader.TryStackItems(player.bank2.item[i], player.inventory[num], out num2, false))
								{
									SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
									if (player.inventory[num].stack <= 0)
									{
										player.inventory[num].SetDefaults(0);
										break;
									}
									if (player.bank2.item[i].type == 0)
									{
										player.bank2.item[i] = player.inventory[num].Clone();
										player.inventory[num].SetDefaults(0);
									}
								}
							}
							else if (player.chest == -4)
							{
								if (player.bank3.item[i].stack < player.bank3.item[i].maxStack && player.inventory[num].IsTheSameAs(player.bank3.item[i]) && ItemLoader.TryStackItems(player.bank3.item[i], player.inventory[num], out num2, false))
								{
									SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
									if (player.inventory[num].stack <= 0)
									{
										player.inventory[num].SetDefaults(0);
										break;
									}
									if (player.bank3.item[i].type == 0)
									{
										player.bank3.item[i] = player.inventory[num].Clone();
										player.inventory[num].SetDefaults(0);
									}
								}
							}
							else if (player.chest == -5)
							{
								if (player.bank4.item[i].stack < player.bank4.item[i].maxStack && player.inventory[num].IsTheSameAs(player.bank4.item[i]) && ItemLoader.TryStackItems(player.bank4.item[i], player.inventory[num], out num2, false))
								{
									SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
									if (player.inventory[num].stack <= 0)
									{
										player.inventory[num].SetDefaults(0);
										break;
									}
									if (player.bank4.item[i].type == 0)
									{
										player.bank4.item[i] = player.inventory[num].Clone();
										player.inventory[num].SetDefaults(0);
									}
								}
							}
							else if (player.bank.item[i].stack < player.bank.item[i].maxStack && player.inventory[num].IsTheSameAs(player.bank.item[i]) && ItemLoader.TryStackItems(player.bank.item[i], player.inventory[num], out num2, false))
							{
								SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
								if (player.inventory[num].stack <= 0)
								{
									player.inventory[num].SetDefaults(0);
									break;
								}
								if (player.bank.item[i].type == 0)
								{
									player.bank.item[i] = player.inventory[num].Clone();
									player.inventory[num].SetDefaults(0);
								}
							}
						}
					}
					if (player.inventory[num].stack > 0)
					{
						if (player.chest > -1)
						{
							int j = 0;
							while (j < 40)
							{
								if (Main.chest[player.chest].item[j].stack == 0)
								{
									SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
									Main.chest[player.chest].item[j] = player.inventory[num].Clone();
									player.inventory[num].SetDefaults(0);
									if (Main.netMode == 1)
									{
										NetMessage.SendData(32, -1, -1, null, player.chest, (float)j, 0f, 0f, 0, 0, 0);
										break;
									}
									break;
								}
								else
								{
									j++;
								}
							}
						}
						else if (player.chest == -3)
						{
							for (int k = 0; k < 40; k++)
							{
								if (player.bank2.item[k].stack == 0)
								{
									SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
									player.bank2.item[k] = player.inventory[num].Clone();
									player.inventory[num].SetDefaults(0);
									break;
								}
							}
						}
						else if (player.chest == -4)
						{
							for (int l = 0; l < 40; l++)
							{
								if (player.bank3.item[l].stack == 0)
								{
									SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
									player.bank3.item[l] = player.inventory[num].Clone();
									player.inventory[num].SetDefaults(0);
									break;
								}
							}
						}
						else if (player.chest == -5)
						{
							for (int m = 0; m < 40; m++)
							{
								if (player.bank4.item[m].stack == 0)
								{
									SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
									player.bank4.item[m] = player.inventory[num].Clone();
									player.inventory[num].SetDefaults(0);
									break;
								}
							}
						}
						else
						{
							for (int n = 0; n < 40; n++)
							{
								if (player.bank.item[n].stack == 0)
								{
									SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
									player.bank.item[n] = player.inventory[num].Clone();
									player.inventory[num].SetDefaults(0);
									break;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x004A539C File Offset: 0x004A359C
		public static void QuickStack(ContainerTransferContext context, bool voidStack = false)
		{
			Player player = Main.player[Main.myPlayer];
			Item[] array = player.inventory;
			if (voidStack)
			{
				array = player.bank4.item;
			}
			Vector2 center = player.Center;
			Vector2 containerWorldPosition = context.GetContainerWorldPosition();
			bool canVisualizeTransfers = context.CanVisualizeTransfers;
			if (!voidStack && player.chest == -5)
			{
				long coinsMoved = ChestUI.MoveCoins(array, player.bank4.item, context);
				if (canVisualizeTransfers)
				{
					Chest.VisualizeChestTransfer_CoinsBatch(center, containerWorldPosition, coinsMoved);
				}
			}
			else if (player.chest == -4)
			{
				long coinsMoved2 = ChestUI.MoveCoins(array, player.bank3.item, context);
				if (canVisualizeTransfers)
				{
					Chest.VisualizeChestTransfer_CoinsBatch(center, containerWorldPosition, coinsMoved2);
				}
			}
			else if (player.chest == -3)
			{
				long coinsMoved3 = ChestUI.MoveCoins(array, player.bank2.item, context);
				if (canVisualizeTransfers)
				{
					Chest.VisualizeChestTransfer_CoinsBatch(center, containerWorldPosition, coinsMoved3);
				}
			}
			else if (player.chest == -2)
			{
				long coinsMoved4 = ChestUI.MoveCoins(array, player.bank.item, context);
				if (canVisualizeTransfers)
				{
					Chest.VisualizeChestTransfer_CoinsBatch(center, containerWorldPosition, coinsMoved4);
				}
			}
			Item[] item = player.bank.item;
			if (player.chest > -1)
			{
				item = Main.chest[player.chest].item;
			}
			else if (player.chest == -2)
			{
				item = player.bank.item;
			}
			else if (player.chest == -3)
			{
				item = player.bank2.item;
			}
			else if (player.chest == -4)
			{
				item = player.bank3.item;
			}
			else if (!voidStack && player.chest == -5)
			{
				item = player.bank4.item;
			}
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			List<int> list3 = new List<int>();
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			List<int> list4 = new List<int>();
			bool[] array2 = new bool[item.Length];
			for (int i = 0; i < 40; i++)
			{
				if (item[i].type > 0 && item[i].stack > 0 && (item[i].type < 71 || item[i].type > 74))
				{
					list2.Add(i);
					list.Add(item[i].netID);
				}
				if (item[i].type == 0 || item[i].stack <= 0)
				{
					list3.Add(i);
				}
			}
			int num = 50;
			int num2 = 10;
			if (player.chest <= -2)
			{
				num += 4;
			}
			if (voidStack)
			{
				num2 = 0;
				num = 40;
			}
			for (int j = num2; j < num; j++)
			{
				if (list.Contains(array[j].netID) && !array[j].favorited)
				{
					dictionary.Add(j, array[j].netID);
				}
			}
			for (int k = 0; k < list2.Count; k++)
			{
				int num3 = list2[k];
				int netID = item[num3].netID;
				foreach (KeyValuePair<int, int> item2 in dictionary)
				{
					if (item2.Value == netID && array[item2.Key].netID == netID)
					{
						int num4 = array[item2.Key].stack;
						int num5 = item[num3].maxStack - item[num3].stack;
						if (num5 == 0)
						{
							break;
						}
						if (num4 > num5)
						{
							num4 = num5;
						}
						SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
						ItemLoader.TryStackItems(item[num3], array[item2.Key], out num4, false);
						if (canVisualizeTransfers && num4 > 0)
						{
							Chest.VisualizeChestTransfer(center, containerWorldPosition, item[num3], num4);
						}
						array2[num3] = true;
					}
				}
			}
			foreach (KeyValuePair<int, int> item3 in dictionary)
			{
				if (array[item3.Key].stack == 0)
				{
					list4.Add(item3.Key);
				}
			}
			foreach (int item4 in list4)
			{
				dictionary.Remove(item4);
			}
			for (int l = 0; l < list3.Count; l++)
			{
				int num6 = list3[l];
				bool flag = true;
				int num7 = item[num6].netID;
				if (num7 < 71 || num7 > 74)
				{
					foreach (KeyValuePair<int, int> item5 in dictionary)
					{
						if ((item5.Value == num7 && array[item5.Key].netID == num7) || (flag && array[item5.Key].stack > 0))
						{
							SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
							if (flag)
							{
								num7 = item5.Value;
								item[num6] = array[item5.Key];
								array[item5.Key] = new Item();
								if (canVisualizeTransfers)
								{
									Chest.VisualizeChestTransfer(center, containerWorldPosition, item[num6], item[num6].stack);
								}
							}
							else
							{
								int num8 = array[item5.Key].stack;
								int num9 = item[num6].maxStack - item[num6].stack;
								if (num9 == 0)
								{
									break;
								}
								if (num8 > num9)
								{
									num8 = num9;
								}
								ItemLoader.TryStackItems(item[num6], array[item5.Key], out num8, false);
								if (canVisualizeTransfers && num8 > 0)
								{
									Chest.VisualizeChestTransfer(center, containerWorldPosition, item[num6], num8);
								}
								if (array[item5.Key].stack == 0)
								{
									array[item5.Key] = new Item();
								}
							}
							array2[num6] = true;
							flag = false;
						}
					}
				}
			}
			if (Main.netMode == 1 && player.chest >= 0)
			{
				for (int m = 0; m < array2.Length; m++)
				{
					NetMessage.SendData(32, -1, -1, null, player.chest, (float)m, 0f, 0f, 0, 0, 0);
				}
			}
			list.Clear();
			list2.Clear();
			list3.Clear();
			dictionary.Clear();
			list4.Clear();
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x004A5A30 File Offset: 0x004A3C30
		public static void RenameChest()
		{
			Player player = Main.player[Main.myPlayer];
			if (!Main.editChest)
			{
				IngameFancyUI.OpenVirtualKeyboard(2);
				return;
			}
			ChestUI.RenameChestSubmit(player);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x004A5A60 File Offset: 0x004A3C60
		public static void RenameChestSubmit(Player player)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			Main.editChest = false;
			int chest = player.chest;
			if (chest < 0)
			{
				return;
			}
			if (Main.npcChatText == Main.defaultChestName)
			{
				Main.npcChatText = "";
			}
			if (Main.chest[chest].name != Main.npcChatText)
			{
				Main.chest[chest].name = Main.npcChatText;
				if (Main.netMode == 1)
				{
					player.editedChestName = true;
				}
			}
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x004A5AE8 File Offset: 0x004A3CE8
		public static void RenameChestCancel()
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			Main.editChest = false;
			Main.npcChatText = string.Empty;
			Main.blockKey = 27.ToString();
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x004A5B30 File Offset: 0x004A3D30
		public static void Restock()
		{
			Player player = Main.player[Main.myPlayer];
			Item[] inventory = player.inventory;
			Item[] item = player.bank.item;
			if (player.chest > -1)
			{
				item = Main.chest[player.chest].item;
			}
			else if (player.chest == -2)
			{
				item = player.bank.item;
			}
			else if (player.chest == -3)
			{
				item = player.bank2.item;
			}
			else if (player.chest == -4)
			{
				item = player.bank3.item;
			}
			else if (player.chest == -5)
			{
				item = player.bank4.item;
			}
			HashSet<int> hashSet = new HashSet<int>();
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			for (int num = 57; num >= 0; num--)
			{
				if ((num < 50 || num >= 54) && (inventory[num].type < 71 || inventory[num].type > 74))
				{
					if (inventory[num].stack > 0 && inventory[num].maxStack > 1)
					{
						hashSet.Add(inventory[num].netID);
						if (inventory[num].stack < inventory[num].maxStack)
						{
							list.Add(num);
						}
					}
					else if (inventory[num].stack == 0 || inventory[num].netID == 0 || inventory[num].type == 0)
					{
						list2.Add(num);
					}
				}
			}
			bool flag = false;
			for (int i = 0; i < item.Length; i++)
			{
				if (item[i].stack >= 1 && hashSet.Contains(item[i].netID))
				{
					bool flag2 = false;
					for (int j = 0; j < list.Count; j++)
					{
						int num2 = list[j];
						int context = 0;
						if (num2 >= 50)
						{
							context = 2;
						}
						int num3;
						if (inventory[num2].netID == item[i].netID && ItemSlot.PickItemMovementAction(inventory, context, num2, item[i]) != -1 && ItemLoader.TryStackItems(inventory[num2], item[i], out num3, false))
						{
							flag = true;
							if (inventory[num2].stack == inventory[num2].maxStack)
							{
								if (Main.netMode == 1 && Main.player[Main.myPlayer].chest > -1)
								{
									NetMessage.SendData(32, -1, -1, null, Main.player[Main.myPlayer].chest, (float)i, 0f, 0f, 0, 0, 0);
								}
								list.RemoveAt(j);
								j--;
							}
							if (item[i].stack == 0)
							{
								item[i] = new Item();
								flag2 = true;
								if (Main.netMode == 1 && Main.player[Main.myPlayer].chest > -1)
								{
									NetMessage.SendData(32, -1, -1, null, Main.player[Main.myPlayer].chest, (float)i, 0f, 0f, 0, 0, 0);
									break;
								}
								break;
							}
						}
					}
					if (!flag2 && list2.Count > 0 && item[i].ammo != 0)
					{
						for (int k = 0; k < list2.Count; k++)
						{
							int context2 = 0;
							if (list2[k] >= 50)
							{
								context2 = 2;
							}
							if (ItemSlot.PickItemMovementAction(inventory, context2, list2[k], item[i]) != -1)
							{
								Utils.Swap<Item>(ref inventory[list2[k]], ref item[i]);
								if (Main.netMode == 1 && Main.player[Main.myPlayer].chest > -1)
								{
									NetMessage.SendData(32, -1, -1, null, Main.player[Main.myPlayer].chest, (float)i, 0f, 0f, 0, 0, 0);
								}
								list.Add(list2[k]);
								list2.RemoveAt(k);
								flag = true;
								break;
							}
						}
					}
				}
			}
			if (flag)
			{
				SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			}
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x004A5F1C File Offset: 0x004A411C
		public static long MoveCoins(Item[] pInv, Item[] cInv, ContainerTransferContext context)
		{
			bool flag = false;
			int[] array = new int[4];
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			bool flag2 = false;
			int[] array2 = new int[40];
			bool overFlowing;
			long num = Utils.CoinsCount(out overFlowing, pInv, Array.Empty<int>());
			for (int i = 0; i < cInv.Length; i++)
			{
				array2[i] = -1;
				if (cInv[i].stack < 1 || cInv[i].type < 1)
				{
					list2.Add(i);
					cInv[i] = new Item();
				}
				if (cInv[i] != null && cInv[i].stack > 0)
				{
					int num2 = 0;
					if (cInv[i].type == 71)
					{
						num2 = 1;
					}
					if (cInv[i].type == 72)
					{
						num2 = 2;
					}
					if (cInv[i].type == 73)
					{
						num2 = 3;
					}
					if (cInv[i].type == 74)
					{
						num2 = 4;
					}
					array2[i] = num2 - 1;
					if (num2 > 0)
					{
						array[num2 - 1] += cInv[i].stack;
						list2.Add(i);
						cInv[i] = new Item();
						flag2 = true;
					}
				}
			}
			if (!flag2)
			{
				return 0L;
			}
			for (int j = 0; j < pInv.Length; j++)
			{
				if (j != 58 && pInv[j] != null && pInv[j].stack > 0 && !pInv[j].favorited)
				{
					int num3 = 0;
					if (pInv[j].type == 71)
					{
						num3 = 1;
					}
					if (pInv[j].type == 72)
					{
						num3 = 2;
					}
					if (pInv[j].type == 73)
					{
						num3 = 3;
					}
					if (pInv[j].type == 74)
					{
						num3 = 4;
					}
					if (num3 > 0)
					{
						flag = true;
						array[num3 - 1] += pInv[j].stack;
						list.Add(j);
						pInv[j] = new Item();
					}
				}
			}
			for (int k = 0; k < 3; k++)
			{
				while (array[k] >= 100)
				{
					array[k] -= 100;
					array[k + 1]++;
				}
			}
			for (int l = 0; l < 40; l++)
			{
				if (array2[l] >= 0 && cInv[l].type == 0)
				{
					int num4 = l;
					int num5 = array2[l];
					if (array[num5] > 0)
					{
						cInv[num4].SetDefaults(71 + num5);
						cInv[num4].stack = array[num5];
						if (cInv[num4].stack > cInv[num4].maxStack)
						{
							cInv[num4].stack = cInv[num4].maxStack;
						}
						array[num5] -= cInv[num4].stack;
						array2[l] = -1;
					}
					if (Main.netMode == 1 && Main.player[Main.myPlayer].chest > -1)
					{
						NetMessage.SendData(32, -1, -1, null, Main.player[Main.myPlayer].chest, (float)num4, 0f, 0f, 0, 0, 0);
					}
					list2.Remove(num4);
				}
			}
			for (int m = 0; m < 40; m++)
			{
				if (array2[m] >= 0 && cInv[m].type == 0)
				{
					int num6 = m;
					int num7 = 3;
					while (num7 >= 0)
					{
						if (array[num7] > 0)
						{
							cInv[num6].SetDefaults(71 + num7);
							cInv[num6].stack = array[num7];
							if (cInv[num6].stack > cInv[num6].maxStack)
							{
								cInv[num6].stack = cInv[num6].maxStack;
							}
							array[num7] -= cInv[num6].stack;
							array2[m] = -1;
							break;
						}
						if (array[num7] == 0)
						{
							num7--;
						}
					}
					if (Main.netMode == 1 && Main.player[Main.myPlayer].chest > -1)
					{
						NetMessage.SendData(32, -1, -1, null, Main.player[Main.myPlayer].chest, (float)num6, 0f, 0f, 0, 0, 0);
					}
					list2.Remove(num6);
				}
			}
			while (list2.Count > 0)
			{
				int num8 = list2[0];
				int num9 = 3;
				while (num9 >= 0)
				{
					if (array[num9] > 0)
					{
						cInv[num8].SetDefaults(71 + num9);
						cInv[num8].stack = array[num9];
						if (cInv[num8].stack > cInv[num8].maxStack)
						{
							cInv[num8].stack = cInv[num8].maxStack;
						}
						array[num9] -= cInv[num8].stack;
						break;
					}
					if (array[num9] == 0)
					{
						num9--;
					}
				}
				if (Main.netMode == 1 && Main.player[Main.myPlayer].chest > -1)
				{
					NetMessage.SendData(32, -1, -1, null, Main.player[Main.myPlayer].chest, (float)list2[0], 0f, 0f, 0, 0, 0);
				}
				list2.RemoveAt(0);
			}
			int num10 = 3;
			while (num10 >= 0 && list.Count > 0)
			{
				int num11 = list[0];
				if (array[num10] > 0)
				{
					pInv[num11].SetDefaults(71 + num10);
					pInv[num11].stack = array[num10];
					if (pInv[num11].stack > pInv[num11].maxStack)
					{
						pInv[num11].stack = pInv[num11].maxStack;
					}
					array[num10] -= pInv[num11].stack;
					flag = false;
					list.RemoveAt(0);
				}
				if (array[num10] == 0)
				{
					num10--;
				}
			}
			if (flag)
			{
				SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			}
			bool overFlowing2;
			long num12 = Utils.CoinsCount(out overFlowing2, pInv, Array.Empty<int>());
			if (overFlowing || overFlowing2)
			{
				return 0L;
			}
			return num - num12;
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x004A64B0 File Offset: 0x004A46B0
		public static bool TryPlacingInChest(Item I, bool justCheck, int itemSlotContext)
		{
			bool sync;
			Item[] chestinv;
			ChestUI.GetContainerUsageInfo(out sync, out chestinv);
			if (ChestUI.IsBlockedFromTransferIntoChest(I, chestinv))
			{
				return false;
			}
			Player player = Main.player[Main.myPlayer];
			bool flag = false;
			if (I.maxStack > 1)
			{
				for (int i = 0; i < 40; i++)
				{
					if (chestinv[i].stack < chestinv[i].maxStack && I.IsTheSameAs(chestinv[i]) && ItemLoader.CanStack(chestinv[i], I))
					{
						int num = I.stack;
						if (I.stack + chestinv[i].stack > chestinv[i].maxStack)
						{
							num = chestinv[i].maxStack - chestinv[i].stack;
						}
						if (justCheck)
						{
							flag = (flag || num > 0);
							break;
						}
						int num2;
						ItemLoader.StackItems(chestinv[i], I, out num2, false, null);
						SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
						if (I.stack <= 0)
						{
							I.SetDefaults(0);
							if (sync)
							{
								NetMessage.SendData(32, -1, -1, null, player.chest, (float)i, 0f, 0f, 0, 0, 0);
								break;
							}
							break;
						}
						else
						{
							if (chestinv[i].type == 0)
							{
								chestinv[i] = I.Clone();
								I.SetDefaults(0);
							}
							if (sync)
							{
								NetMessage.SendData(32, -1, -1, null, player.chest, (float)i, 0f, 0f, 0, 0, 0);
							}
						}
					}
				}
			}
			if (I.stack > 0)
			{
				int j = 0;
				while (j < 40)
				{
					if (chestinv[j].stack == 0)
					{
						if (justCheck)
						{
							flag = true;
							break;
						}
						SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
						chestinv[j] = I.Clone();
						I.SetDefaults(0);
						ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(chestinv[j], 0, 3, 0));
						if (sync)
						{
							NetMessage.SendData(32, -1, -1, null, player.chest, (float)j, 0f, 0f, 0, 0, 0);
							break;
						}
						break;
					}
					else
					{
						j++;
					}
				}
			}
			return flag;
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x004A66AC File Offset: 0x004A48AC
		public static void GetContainerUsageInfo(out bool sync, out Item[] chestinv)
		{
			sync = false;
			Player player = Main.player[Main.myPlayer];
			chestinv = player.bank.item;
			if (player.chest > -1)
			{
				chestinv = Main.chest[player.chest].item;
				sync = (Main.netMode == 1);
				return;
			}
			if (player.chest == -2)
			{
				chestinv = player.bank.item;
				return;
			}
			if (player.chest == -3)
			{
				chestinv = player.bank2.item;
				return;
			}
			if (player.chest == -4)
			{
				chestinv = player.bank3.item;
				return;
			}
			if (player.chest == -5)
			{
				chestinv = player.bank4.item;
			}
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x004A675C File Offset: 0x004A495C
		public static bool IsBlockedFromTransferIntoChest(Item item, Item[] container)
		{
			return (item.type == 3213 && item.favorited && container == Main.LocalPlayer.bank.item) || ((item.type == 4131 || item.type == 5325) && item.favorited && container == Main.LocalPlayer.bank4.item);
		}

		// Token: 0x040010DB RID: 4315
		public const float buttonScaleMinimum = 0.75f;

		// Token: 0x040010DC RID: 4316
		public const float buttonScaleMaximum = 1f;

		// Token: 0x040010DD RID: 4317
		public static float[] ButtonScale = new float[ChestUI.ButtonID.Count];

		// Token: 0x040010DE RID: 4318
		public static bool[] ButtonHovered = new bool[ChestUI.ButtonID.Count];

		// Token: 0x02000864 RID: 2148
		public class ButtonID
		{
			// Token: 0x0400690E RID: 26894
			public const int LootAll = 0;

			// Token: 0x0400690F RID: 26895
			public const int DepositAll = 1;

			// Token: 0x04006910 RID: 26896
			public const int QuickStack = 2;

			// Token: 0x04006911 RID: 26897
			public const int Restock = 3;

			// Token: 0x04006912 RID: 26898
			public const int Sort = 4;

			// Token: 0x04006913 RID: 26899
			public const int RenameChest = 5;

			// Token: 0x04006914 RID: 26900
			public const int RenameChestCancel = 6;

			// Token: 0x04006915 RID: 26901
			public const int ToggleVacuum = 7;

			// Token: 0x04006916 RID: 26902
			public static readonly int Count = 7;
		}
	}
}
