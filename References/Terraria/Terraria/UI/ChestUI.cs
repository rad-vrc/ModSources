using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI.Chat;
using Terraria.UI.Gamepad;

namespace Terraria.UI
{
	// Token: 0x02000092 RID: 146
	public class ChestUI
	{
		// Token: 0x0600124C RID: 4684 RVA: 0x00491B58 File Offset: 0x0048FD58
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

		// Token: 0x0600124D RID: 4685 RVA: 0x00491BF4 File Offset: 0x0048FDF4
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

		// Token: 0x0600124E RID: 4686 RVA: 0x00491CB0 File Offset: 0x0048FEB0
		private static void DrawName(SpriteBatch spritebatch)
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
					if (tile.type == 21)
					{
						text = Lang.chestType[(int)(tile.frameX / 36)].Value;
					}
					else if (tile.type == 467 && tile.frameX / 36 == 4)
					{
						text = Lang.GetItemNameValue(3988);
					}
					else if (tile.type == 467)
					{
						text = Lang.chestType2[(int)(tile.frameX / 36)].Value;
					}
					else if (tile.type == 88)
					{
						text = Lang.dresserType[(int)(tile.frameX / 54)].Value;
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
			Color baseColor = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			baseColor = Color.White * (1f - (255f - (float)Main.mouseTextColor) / 255f * 0.5f);
			baseColor.A = byte.MaxValue;
			int num;
			Utils.WordwrapString(text, FontAssets.MouseText.Value, 200, 1, out num);
			num++;
			for (int i = 0; i < num; i++)
			{
				ChatManager.DrawColorCodedStringWithShadow(spritebatch, FontAssets.MouseText.Value, text, new Vector2(504f, (float)(Main.instance.invBottom + i * 26)), baseColor, 0f, Vector2.Zero, Vector2.One, -1f, 1.5f);
			}
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00491FA0 File Offset: 0x004901A0
		private static void DrawButtons(SpriteBatch spritebatch)
		{
			for (int i = 0; i < ChestUI.ButtonID.Count; i++)
			{
				ChestUI.DrawButton(spritebatch, i, 506, Main.instance.invBottom + 40);
			}
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00491FD8 File Offset: 0x004901D8
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
				if (player.IsVoidVaultEnabled)
				{
					text = Language.GetTextValue("UI.ToggleBank4VacuumIsOn");
				}
				else
				{
					text = Language.GetTextValue("UI.ToggleBank4VacuumIsOff");
				}
				break;
			}
			Vector2 vector = FontAssets.MouseText.Value.MeasureString(text);
			Color baseColor = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor) * num2;
			baseColor = Color.White * 0.97f * (1f - (255f - (float)Main.mouseTextColor) / 255f * 0.5f);
			baseColor.A = byte.MaxValue;
			X += (int)(vector.X * num2 / 2f);
			bool flag = Utils.FloatIntersect((float)Main.mouseX, (float)Main.mouseY, 0f, 0f, (float)X - vector.X / 2f, (float)(Y - 12), vector.X, 24f);
			if (ChestUI.ButtonHovered[ID])
			{
				flag = Utils.FloatIntersect((float)Main.mouseX, (float)Main.mouseY, 0f, 0f, (float)X - vector.X / 2f - 10f, (float)(Y - 12), vector.X + 16f, 24f);
			}
			if (flag)
			{
				baseColor = Main.OurFavoriteColor;
			}
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, text, new Vector2((float)X, (float)Y), baseColor, 0f, vector / 2f, new Vector2(num2), -1f, 1.5f);
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
			if (!PlayerInput.IgnoreMouseInterface)
			{
				player.mouseInterface = true;
				if (!Main.mouseLeft || !Main.mouseLeftRelease)
				{
					return;
				}
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

		// Token: 0x06001251 RID: 4689 RVA: 0x0049247E File Offset: 0x0049067E
		private static void ToggleVacuum()
		{
			Player player = Main.player[Main.myPlayer];
			player.IsVoidVaultEnabled = !player.IsVoidVaultEnabled;
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0049249C File Offset: 0x0049069C
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

		// Token: 0x06001253 RID: 4691 RVA: 0x0049267C File Offset: 0x0049087C
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

		// Token: 0x06001254 RID: 4692 RVA: 0x00492910 File Offset: 0x00490B10
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
			for (int i = 49; i >= 10; i--)
			{
				if (player.inventory[i].stack > 0 && player.inventory[i].type > 0 && !player.inventory[i].favorited)
				{
					if (player.inventory[i].maxStack > 1)
					{
						for (int j = 0; j < 40; j++)
						{
							if (player.chest > -1)
							{
								Chest chest = Main.chest[player.chest];
								if (chest.item[j].stack < chest.item[j].maxStack && player.inventory[i].IsTheSameAs(chest.item[j]))
								{
									int num = player.inventory[i].stack;
									if (player.inventory[i].stack + chest.item[j].stack > chest.item[j].maxStack)
									{
										num = chest.item[j].maxStack - chest.item[j].stack;
									}
									player.inventory[i].stack -= num;
									chest.item[j].stack += num;
									SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
									if (player.inventory[i].stack <= 0)
									{
										player.inventory[i].SetDefaults(0);
										if (Main.netMode == 1)
										{
											NetMessage.SendData(32, -1, -1, null, player.chest, (float)j, 0f, 0f, 0, 0, 0);
											break;
										}
										break;
									}
									else
									{
										if (chest.item[j].type == 0)
										{
											chest.item[j] = player.inventory[i].Clone();
											player.inventory[i].SetDefaults(0);
										}
										if (Main.netMode == 1)
										{
											NetMessage.SendData(32, -1, -1, null, player.chest, (float)j, 0f, 0f, 0, 0, 0);
										}
									}
								}
							}
							else if (player.chest == -3)
							{
								if (player.bank2.item[j].stack < player.bank2.item[j].maxStack && player.inventory[i].IsTheSameAs(player.bank2.item[j]))
								{
									int num2 = player.inventory[i].stack;
									if (player.inventory[i].stack + player.bank2.item[j].stack > player.bank2.item[j].maxStack)
									{
										num2 = player.bank2.item[j].maxStack - player.bank2.item[j].stack;
									}
									player.inventory[i].stack -= num2;
									player.bank2.item[j].stack += num2;
									SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
									if (player.inventory[i].stack <= 0)
									{
										player.inventory[i].SetDefaults(0);
										break;
									}
									if (player.bank2.item[j].type == 0)
									{
										player.bank2.item[j] = player.inventory[i].Clone();
										player.inventory[i].SetDefaults(0);
									}
								}
							}
							else if (player.chest == -4)
							{
								if (player.bank3.item[j].stack < player.bank3.item[j].maxStack && player.inventory[i].IsTheSameAs(player.bank3.item[j]))
								{
									int num3 = player.inventory[i].stack;
									if (player.inventory[i].stack + player.bank3.item[j].stack > player.bank3.item[j].maxStack)
									{
										num3 = player.bank3.item[j].maxStack - player.bank3.item[j].stack;
									}
									player.inventory[i].stack -= num3;
									player.bank3.item[j].stack += num3;
									SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
									if (player.inventory[i].stack <= 0)
									{
										player.inventory[i].SetDefaults(0);
										break;
									}
									if (player.bank3.item[j].type == 0)
									{
										player.bank3.item[j] = player.inventory[i].Clone();
										player.inventory[i].SetDefaults(0);
									}
								}
							}
							else if (player.chest == -5)
							{
								if (player.bank4.item[j].stack < player.bank4.item[j].maxStack && player.inventory[i].IsTheSameAs(player.bank4.item[j]))
								{
									int num4 = player.inventory[i].stack;
									if (player.inventory[i].stack + player.bank4.item[j].stack > player.bank4.item[j].maxStack)
									{
										num4 = player.bank4.item[j].maxStack - player.bank4.item[j].stack;
									}
									player.inventory[i].stack -= num4;
									player.bank4.item[j].stack += num4;
									SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
									if (player.inventory[i].stack <= 0)
									{
										player.inventory[i].SetDefaults(0);
										break;
									}
									if (player.bank4.item[j].type == 0)
									{
										player.bank4.item[j] = player.inventory[i].Clone();
										player.inventory[i].SetDefaults(0);
									}
								}
							}
							else if (player.bank.item[j].stack < player.bank.item[j].maxStack && player.inventory[i].IsTheSameAs(player.bank.item[j]))
							{
								int num5 = player.inventory[i].stack;
								if (player.inventory[i].stack + player.bank.item[j].stack > player.bank.item[j].maxStack)
								{
									num5 = player.bank.item[j].maxStack - player.bank.item[j].stack;
								}
								player.inventory[i].stack -= num5;
								player.bank.item[j].stack += num5;
								SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
								if (player.inventory[i].stack <= 0)
								{
									player.inventory[i].SetDefaults(0);
									break;
								}
								if (player.bank.item[j].type == 0)
								{
									player.bank.item[j] = player.inventory[i].Clone();
									player.inventory[i].SetDefaults(0);
								}
							}
						}
					}
					if (player.inventory[i].stack > 0)
					{
						if (player.chest > -1)
						{
							int k = 0;
							while (k < 40)
							{
								if (Main.chest[player.chest].item[k].stack == 0)
								{
									SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
									Main.chest[player.chest].item[k] = player.inventory[i].Clone();
									player.inventory[i].SetDefaults(0);
									if (Main.netMode == 1)
									{
										NetMessage.SendData(32, -1, -1, null, player.chest, (float)k, 0f, 0f, 0, 0, 0);
										break;
									}
									break;
								}
								else
								{
									k++;
								}
							}
						}
						else if (player.chest == -3)
						{
							for (int l = 0; l < 40; l++)
							{
								if (player.bank2.item[l].stack == 0)
								{
									SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
									player.bank2.item[l] = player.inventory[i].Clone();
									player.inventory[i].SetDefaults(0);
									break;
								}
							}
						}
						else if (player.chest == -4)
						{
							for (int m = 0; m < 40; m++)
							{
								if (player.bank3.item[m].stack == 0)
								{
									SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
									player.bank3.item[m] = player.inventory[i].Clone();
									player.inventory[i].SetDefaults(0);
									break;
								}
							}
						}
						else if (player.chest == -5)
						{
							for (int n = 0; n < 40; n++)
							{
								if (player.bank4.item[n].stack == 0)
								{
									SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
									player.bank4.item[n] = player.inventory[i].Clone();
									player.inventory[i].SetDefaults(0);
									break;
								}
							}
						}
						else
						{
							for (int num6 = 0; num6 < 40; num6++)
							{
								if (player.bank.item[num6].stack == 0)
								{
									SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
									player.bank.item[num6] = player.inventory[i].Clone();
									player.inventory[i].SetDefaults(0);
									break;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00493404 File Offset: 0x00491604
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
				foreach (KeyValuePair<int, int> keyValuePair in dictionary)
				{
					if (keyValuePair.Value == netID && array[keyValuePair.Key].netID == netID)
					{
						int num4 = array[keyValuePair.Key].stack;
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
						if (canVisualizeTransfers)
						{
							Chest.VisualizeChestTransfer(center, containerWorldPosition, item[num3], num4);
						}
						item[num3].stack += num4;
						array[keyValuePair.Key].stack -= num4;
						if (array[keyValuePair.Key].stack == 0)
						{
							array[keyValuePair.Key].SetDefaults(0);
						}
						array2[num3] = true;
					}
				}
			}
			foreach (KeyValuePair<int, int> keyValuePair2 in dictionary)
			{
				if (array[keyValuePair2.Key].stack == 0)
				{
					list4.Add(keyValuePair2.Key);
				}
			}
			foreach (int key in list4)
			{
				dictionary.Remove(key);
			}
			for (int l = 0; l < list3.Count; l++)
			{
				int num6 = list3[l];
				bool flag = true;
				int num7 = item[num6].netID;
				if (num7 < 71 || num7 > 74)
				{
					foreach (KeyValuePair<int, int> keyValuePair3 in dictionary)
					{
						if ((keyValuePair3.Value == num7 && array[keyValuePair3.Key].netID == num7) || (flag && array[keyValuePair3.Key].stack > 0))
						{
							SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
							if (flag)
							{
								num7 = keyValuePair3.Value;
								item[num6] = array[keyValuePair3.Key];
								array[keyValuePair3.Key] = new Item();
								if (canVisualizeTransfers)
								{
									Chest.VisualizeChestTransfer(center, containerWorldPosition, item[num6], item[num6].stack);
								}
							}
							else
							{
								int num8 = array[keyValuePair3.Key].stack;
								int num9 = item[num6].maxStack - item[num6].stack;
								if (num9 == 0)
								{
									break;
								}
								if (num8 > num9)
								{
									num8 = num9;
								}
								if (canVisualizeTransfers)
								{
									Chest.VisualizeChestTransfer(center, containerWorldPosition, item[num6], num8);
								}
								item[num6].stack += num8;
								array[keyValuePair3.Key].stack -= num8;
								if (array[keyValuePair3.Key].stack == 0)
								{
									array[keyValuePair3.Key] = new Item();
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

		// Token: 0x06001256 RID: 4694 RVA: 0x00493ADC File Offset: 0x00491CDC
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

		// Token: 0x06001257 RID: 4695 RVA: 0x00493B0C File Offset: 0x00491D0C
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

		// Token: 0x06001258 RID: 4696 RVA: 0x00493B94 File Offset: 0x00491D94
		public static void RenameChestCancel()
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			Main.editChest = false;
			Main.npcChatText = string.Empty;
			Main.blockKey = Keys.Escape.ToString();
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00493BDC File Offset: 0x00491DDC
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
			for (int i = 57; i >= 0; i--)
			{
				if ((i < 50 || i >= 54) && (inventory[i].type < 71 || inventory[i].type > 74))
				{
					if (inventory[i].stack > 0 && inventory[i].maxStack > 1 && inventory[i].prefix == 0)
					{
						hashSet.Add(inventory[i].netID);
						if (inventory[i].stack < inventory[i].maxStack)
						{
							list.Add(i);
						}
					}
					else if (inventory[i].stack == 0 || inventory[i].netID == 0 || inventory[i].type == 0)
					{
						list2.Add(i);
					}
				}
			}
			bool flag = false;
			for (int j = 0; j < item.Length; j++)
			{
				if (item[j].stack >= 1 && item[j].prefix == 0 && hashSet.Contains(item[j].netID))
				{
					bool flag2 = false;
					for (int k = 0; k < list.Count; k++)
					{
						int num = list[k];
						int context = 0;
						if (num >= 50)
						{
							context = 2;
						}
						if (inventory[num].netID == item[j].netID && ItemSlot.PickItemMovementAction(inventory, context, num, item[j]) != -1)
						{
							int num2 = item[j].stack;
							if (inventory[num].maxStack - inventory[num].stack < num2)
							{
								num2 = inventory[num].maxStack - inventory[num].stack;
							}
							inventory[num].stack += num2;
							item[j].stack -= num2;
							flag = true;
							if (inventory[num].stack == inventory[num].maxStack)
							{
								if (Main.netMode == 1 && Main.player[Main.myPlayer].chest > -1)
								{
									NetMessage.SendData(32, -1, -1, null, Main.player[Main.myPlayer].chest, (float)j, 0f, 0f, 0, 0, 0);
								}
								list.RemoveAt(k);
								k--;
							}
							if (item[j].stack == 0)
							{
								item[j] = new Item();
								flag2 = true;
								if (Main.netMode == 1 && Main.player[Main.myPlayer].chest > -1)
								{
									NetMessage.SendData(32, -1, -1, null, Main.player[Main.myPlayer].chest, (float)j, 0f, 0f, 0, 0, 0);
									break;
								}
								break;
							}
						}
					}
					if (!flag2 && list2.Count > 0 && item[j].ammo != 0)
					{
						for (int l = 0; l < list2.Count; l++)
						{
							int context2 = 0;
							if (list2[l] >= 50)
							{
								context2 = 2;
							}
							if (ItemSlot.PickItemMovementAction(inventory, context2, list2[l], item[j]) != -1)
							{
								Utils.Swap<Item>(ref inventory[list2[l]], ref item[j]);
								if (Main.netMode == 1 && Main.player[Main.myPlayer].chest > -1)
								{
									NetMessage.SendData(32, -1, -1, null, Main.player[Main.myPlayer].chest, (float)j, 0f, 0f, 0, 0, 0);
								}
								list.Add(list2[l]);
								list2.RemoveAt(l);
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

		// Token: 0x0600125A RID: 4698 RVA: 0x00494024 File Offset: 0x00492224
		public static long MoveCoins(Item[] pInv, Item[] cInv, ContainerTransferContext context)
		{
			bool flag = false;
			int[] array = new int[4];
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			bool flag2 = false;
			int[] array2 = new int[40];
			bool flag3;
			long num = Utils.CoinsCount(out flag3, pInv, new int[0]);
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
					int n = 3;
					while (n >= 0)
					{
						if (array[n] > 0)
						{
							cInv[num6].SetDefaults(71 + n);
							cInv[num6].stack = array[n];
							if (cInv[num6].stack > cInv[num6].maxStack)
							{
								cInv[num6].stack = cInv[num6].maxStack;
							}
							array[n] -= cInv[num6].stack;
							array2[m] = -1;
							break;
						}
						if (array[n] == 0)
						{
							n--;
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
				int num7 = list2[0];
				int num8 = 3;
				while (num8 >= 0)
				{
					if (array[num8] > 0)
					{
						cInv[num7].SetDefaults(71 + num8);
						cInv[num7].stack = array[num8];
						if (cInv[num7].stack > cInv[num7].maxStack)
						{
							cInv[num7].stack = cInv[num7].maxStack;
						}
						array[num8] -= cInv[num7].stack;
						break;
					}
					if (array[num8] == 0)
					{
						num8--;
					}
				}
				if (Main.netMode == 1 && Main.player[Main.myPlayer].chest > -1)
				{
					NetMessage.SendData(32, -1, -1, null, Main.player[Main.myPlayer].chest, (float)list2[0], 0f, 0f, 0, 0, 0);
				}
				list2.RemoveAt(0);
			}
			int num9 = 3;
			while (num9 >= 0 && list.Count > 0)
			{
				int num10 = list[0];
				if (array[num9] > 0)
				{
					pInv[num10].SetDefaults(71 + num9);
					pInv[num10].stack = array[num9];
					if (pInv[num10].stack > pInv[num10].maxStack)
					{
						pInv[num10].stack = pInv[num10].maxStack;
					}
					array[num9] -= pInv[num10].stack;
					flag = false;
					list.RemoveAt(0);
				}
				if (array[num9] == 0)
				{
					num9--;
				}
			}
			if (flag)
			{
				SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			}
			bool flag4;
			long num11 = Utils.CoinsCount(out flag4, pInv, new int[0]);
			if (flag3 || flag4)
			{
				return 0L;
			}
			return num - num11;
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x004945BC File Offset: 0x004927BC
		public static bool TryPlacingInChest(Item I, bool justCheck, int itemSlotContext)
		{
			bool flag;
			Item[] array;
			ChestUI.GetContainerUsageInfo(out flag, out array);
			if (ChestUI.IsBlockedFromTransferIntoChest(I, array))
			{
				return false;
			}
			Player player = Main.player[Main.myPlayer];
			bool flag2 = false;
			if (I.maxStack > 1)
			{
				for (int i = 0; i < 40; i++)
				{
					if (array[i].stack < array[i].maxStack && I.IsTheSameAs(array[i]))
					{
						int num = I.stack;
						if (I.stack + array[i].stack > array[i].maxStack)
						{
							num = array[i].maxStack - array[i].stack;
						}
						if (justCheck)
						{
							flag2 = (flag2 || num > 0);
							break;
						}
						I.stack -= num;
						array[i].stack += num;
						SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
						if (I.stack <= 0)
						{
							I.SetDefaults(0);
							if (flag)
							{
								NetMessage.SendData(32, -1, -1, null, player.chest, (float)i, 0f, 0f, 0, 0, 0);
								break;
							}
							break;
						}
						else
						{
							if (array[i].type == 0)
							{
								array[i] = I.Clone();
								I.SetDefaults(0);
							}
							if (flag)
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
					if (array[j].stack == 0)
					{
						if (justCheck)
						{
							flag2 = true;
							break;
						}
						SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
						array[j] = I.Clone();
						I.SetDefaults(0);
						ItemSlot.AnnounceTransfer(new ItemSlot.ItemTransferInfo(array[j], 0, 3, 0));
						if (flag)
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
			return flag2;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x004947B4 File Offset: 0x004929B4
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

		// Token: 0x0600125D RID: 4701 RVA: 0x00494864 File Offset: 0x00492A64
		public static bool IsBlockedFromTransferIntoChest(Item item, Item[] container)
		{
			return (item.type == 3213 && item.favorited && container == Main.LocalPlayer.bank.item) || ((item.type == 4131 || item.type == 5325) && item.favorited && container == Main.LocalPlayer.bank4.item);
		}

		// Token: 0x04001014 RID: 4116
		public const float buttonScaleMinimum = 0.75f;

		// Token: 0x04001015 RID: 4117
		public const float buttonScaleMaximum = 1f;

		// Token: 0x04001016 RID: 4118
		public static float[] ButtonScale = new float[ChestUI.ButtonID.Count];

		// Token: 0x04001017 RID: 4119
		public static bool[] ButtonHovered = new bool[ChestUI.ButtonID.Count];

		// Token: 0x02000543 RID: 1347
		public class ButtonID
		{
			// Token: 0x04005867 RID: 22631
			public const int LootAll = 0;

			// Token: 0x04005868 RID: 22632
			public const int DepositAll = 1;

			// Token: 0x04005869 RID: 22633
			public const int QuickStack = 2;

			// Token: 0x0400586A RID: 22634
			public const int Restock = 3;

			// Token: 0x0400586B RID: 22635
			public const int Sort = 4;

			// Token: 0x0400586C RID: 22636
			public const int RenameChest = 5;

			// Token: 0x0400586D RID: 22637
			public const int RenameChestCancel = 6;

			// Token: 0x0400586E RID: 22638
			public const int ToggleVacuum = 7;

			// Token: 0x0400586F RID: 22639
			public static readonly int Count = 7;
		}
	}
}
