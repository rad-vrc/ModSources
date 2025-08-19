using System;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria
{
	// Token: 0x02000038 RID: 56
	public class Recipe
	{
		// Token: 0x060003F4 RID: 1012 RVA: 0x0010D554 File Offset: 0x0010B754
		public void RequireGroup(string name)
		{
			int num;
			if (RecipeGroup.recipeGroupIDs.TryGetValue(name, out num))
			{
				for (int i = 0; i < Recipe.maxRequirements; i++)
				{
					if (this.acceptedGroups[i] == -1)
					{
						this.acceptedGroups[i] = num;
						return;
					}
				}
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0010D598 File Offset: 0x0010B798
		public void RequireGroup(int id)
		{
			for (int i = 0; i < Recipe.maxRequirements; i++)
			{
				if (this.acceptedGroups[i] == -1)
				{
					this.acceptedGroups[i] = id;
					return;
				}
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0010D5CC File Offset: 0x0010B7CC
		public bool ProcessGroupsForText(int type, out string theText)
		{
			for (int i = 0; i < Recipe.maxRequirements; i++)
			{
				int num = this.acceptedGroups[i];
				if (num == -1)
				{
					break;
				}
				if (RecipeGroup.recipeGroups[num].ValidItems.Contains(type))
				{
					theText = RecipeGroup.recipeGroups[num].GetText();
					return true;
				}
			}
			theText = "";
			return false;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0010D630 File Offset: 0x0010B830
		public bool AcceptsGroup(int groupId)
		{
			for (int i = 0; i < Recipe.maxRequirements; i++)
			{
				int num = this.acceptedGroups[i];
				if (num == -1)
				{
					break;
				}
				if (num == groupId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0010D664 File Offset: 0x0010B864
		public bool AcceptedByItemGroups(int invType, int reqType)
		{
			for (int i = 0; i < Recipe.maxRequirements; i++)
			{
				int num = this.acceptedGroups[i];
				if (num == -1)
				{
					break;
				}
				if (RecipeGroup.recipeGroups[num].ValidItems.Contains(invType) && RecipeGroup.recipeGroups[num].ValidItems.Contains(reqType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0010D6C4 File Offset: 0x0010B8C4
		public Item AddCustomShimmerResult(int itemType, int itemStack = 1)
		{
			if (this.customShimmerResults == null)
			{
				this.customShimmerResults = new List<Item>();
			}
			Item item = new Item();
			item.SetDefaults(itemType);
			item.stack = itemStack;
			this.customShimmerResults.Add(item);
			return item;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0010D708 File Offset: 0x0010B908
		public Recipe()
		{
			for (int i = 0; i < Recipe.maxRequirements; i++)
			{
				this.requiredItem[i] = new Item();
				this.requiredTile[i] = -1;
				this.acceptedGroups[i] = -1;
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0010D798 File Offset: 0x0010B998
		public void Create()
		{
			for (int i = 0; i < Recipe.maxRequirements; i++)
			{
				Item item = this.requiredItem[i];
				if (item.type == 0)
				{
					break;
				}
				int num = item.stack;
				if (this.alchemy && Main.player[Main.myPlayer].alchemyTable)
				{
					if (num > 1)
					{
						int num2 = 0;
						for (int j = 0; j < num; j++)
						{
							if (Main.rand.Next(3) == 0)
							{
								num2++;
							}
						}
						num -= num2;
					}
					else if (Main.rand.Next(3) == 0)
					{
						num = 0;
					}
				}
				if (num > 0)
				{
					Item[] array = Main.player[Main.myPlayer].inventory;
					for (int k = 0; k < 58; k++)
					{
						Item item2 = array[k];
						if (num <= 0)
						{
							break;
						}
						if (item2.IsTheSameAs(item) || this.useWood(item2.type, item.type) || this.useSand(item2.type, item.type) || this.useFragment(item2.type, item.type) || this.useIronBar(item2.type, item.type) || this.usePressurePlate(item2.type, item.type) || this.AcceptedByItemGroups(item2.type, item.type))
						{
							if (item2.stack > num)
							{
								item2.stack -= num;
								num = 0;
							}
							else
							{
								num -= item2.stack;
								array[k] = new Item();
							}
						}
					}
					if (Main.player[Main.myPlayer].chest != -1)
					{
						if (Main.player[Main.myPlayer].chest > -1)
						{
							array = Main.chest[Main.player[Main.myPlayer].chest].item;
						}
						else if (Main.player[Main.myPlayer].chest == -2)
						{
							array = Main.player[Main.myPlayer].bank.item;
						}
						else if (Main.player[Main.myPlayer].chest == -3)
						{
							array = Main.player[Main.myPlayer].bank2.item;
						}
						else if (Main.player[Main.myPlayer].chest == -4)
						{
							array = Main.player[Main.myPlayer].bank3.item;
						}
						else if (Main.player[Main.myPlayer].chest == -5)
						{
							array = Main.player[Main.myPlayer].bank4.item;
						}
						for (int l = 0; l < 40; l++)
						{
							Item item2 = array[l];
							if (num <= 0)
							{
								break;
							}
							if (item2.IsTheSameAs(item) || this.useWood(item2.type, item.type) || this.useSand(item2.type, item.type) || this.useIronBar(item2.type, item.type) || this.usePressurePlate(item2.type, item.type) || this.useFragment(item2.type, item.type) || this.AcceptedByItemGroups(item2.type, item.type))
							{
								if (item2.stack > num)
								{
									item2.stack -= num;
									if (Main.netMode == 1 && Main.player[Main.myPlayer].chest >= 0)
									{
										NetMessage.SendData(32, -1, -1, null, Main.player[Main.myPlayer].chest, (float)l, 0f, 0f, 0, 0, 0);
									}
									num = 0;
								}
								else
								{
									num -= item2.stack;
									array[l] = new Item();
									if (Main.netMode == 1 && Main.player[Main.myPlayer].chest >= 0)
									{
										NetMessage.SendData(32, -1, -1, null, Main.player[Main.myPlayer].chest, (float)l, 0f, 0f, 0, 0, 0);
									}
								}
							}
						}
					}
					if (Main.player[Main.myPlayer].useVoidBag() && Main.player[Main.myPlayer].chest != -5)
					{
						array = Main.player[Main.myPlayer].bank4.item;
						for (int m = 0; m < 40; m++)
						{
							Item item2 = array[m];
							if (num <= 0)
							{
								break;
							}
							if (item2.IsTheSameAs(item) || this.useWood(item2.type, item.type) || this.useSand(item2.type, item.type) || this.useIronBar(item2.type, item.type) || this.usePressurePlate(item2.type, item.type) || this.useFragment(item2.type, item.type) || this.AcceptedByItemGroups(item2.type, item.type))
							{
								if (item2.stack > num)
								{
									item2.stack -= num;
									if (Main.netMode == 1 && Main.player[Main.myPlayer].chest >= 0)
									{
										NetMessage.SendData(32, -1, -1, null, Main.player[Main.myPlayer].chest, (float)m, 0f, 0f, 0, 0, 0);
									}
									num = 0;
								}
								else
								{
									num -= item2.stack;
									array[m] = new Item();
									if (Main.netMode == 1 && Main.player[Main.myPlayer].chest >= 0)
									{
										NetMessage.SendData(32, -1, -1, null, Main.player[Main.myPlayer].chest, (float)m, 0f, 0f, 0, 0, 0);
									}
								}
							}
						}
					}
				}
			}
			AchievementsHelper.NotifyItemCraft(this);
			AchievementsHelper.NotifyItemPickup(Main.player[Main.myPlayer], this.createItem);
			Recipe.FindRecipes(false);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0010DD54 File Offset: 0x0010BF54
		public bool useWood(int invType, int reqType)
		{
			if (!this.anyWood)
			{
				return false;
			}
			if (reqType <= 911)
			{
				if (reqType == 9 || reqType - 619 <= 2 || reqType == 911)
				{
					goto IL_47;
				}
			}
			else if (reqType == 1729 || reqType - 2503 <= 1 || reqType == 5215)
			{
				goto IL_47;
			}
			return false;
			IL_47:
			if (invType <= 911)
			{
				if (invType == 9 || invType - 619 <= 2 || invType == 911)
				{
					return true;
				}
			}
			else if (invType == 1729 || invType - 2503 <= 1 || invType == 5215)
			{
				return true;
			}
			return false;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0010DDE6 File Offset: 0x0010BFE6
		public bool useIronBar(int invType, int reqType)
		{
			return this.anyIronBar && (reqType == 22 || reqType == 704) && (invType == 22 || invType == 704);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0010DE14 File Offset: 0x0010C014
		public bool useSand(int invType, int reqType)
		{
			return (reqType == 169 || reqType == 408 || reqType == 1246 || reqType == 370 || reqType == 3272 || reqType == 3338 || reqType == 3274 || reqType == 3275) && (this.anySand && (invType == 169 || invType == 408 || invType == 1246 || invType == 370 || invType == 3272 || invType == 3338 || invType == 3274 || invType == 3275));
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0010DEB0 File Offset: 0x0010C0B0
		public bool useFragment(int invType, int reqType)
		{
			return (reqType == 3458 || reqType == 3456 || reqType == 3457 || reqType == 3459) && (this.anyFragment && (invType == 3458 || invType == 3456 || invType == 3457 || invType == 3459));
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0010DF0C File Offset: 0x0010C10C
		public bool usePressurePlate(int invType, int reqType)
		{
			if (!this.anyPressurePlate)
			{
				return false;
			}
			if (reqType <= 543)
			{
				if (reqType == 529 || reqType - 541 <= 2)
				{
					goto IL_42;
				}
			}
			else if (reqType - 852 <= 1 || reqType == 1151 || reqType == 4261)
			{
				goto IL_42;
			}
			return false;
			IL_42:
			if (invType <= 543)
			{
				if (invType == 529 || invType - 541 <= 2)
				{
					return true;
				}
			}
			else if (invType - 852 <= 1 || invType == 1151 || invType == 4261)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0010DF94 File Offset: 0x0010C194
		public static void GetThroughDelayedFindRecipes()
		{
			if (!Recipe._hasDelayedFindRecipes)
			{
				return;
			}
			Recipe._hasDelayedFindRecipes = false;
			Recipe.FindRecipes(false);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0010DFAC File Offset: 0x0010C1AC
		public static void FindRecipes(bool canDelayCheck = false)
		{
			if (canDelayCheck)
			{
				Recipe._hasDelayedFindRecipes = true;
				return;
			}
			int oldRecipe = Main.availableRecipe[Main.focusRecipe];
			float focusY = Main.availableRecipeY[Main.focusRecipe];
			Recipe.ClearAvailableRecipes();
			if (!Main.guideItem.IsAir && Main.guideItem.Name != "")
			{
				Recipe.CollectGuideRecipes();
				Recipe.TryRefocusingRecipe(oldRecipe);
				Recipe.VisuallyRepositionRecipes(focusY);
				return;
			}
			Player localPlayer = Main.LocalPlayer;
			Recipe.CollectItemsToCraftWithFrom(localPlayer);
			for (int i = 0; i < Recipe.maxRecipes; i++)
			{
				Recipe recipe = Main.recipe[i];
				if (recipe.createItem.type == 0)
				{
					break;
				}
				if (Recipe.PlayerMeetsTileRequirements(localPlayer, recipe) && Recipe.PlayerMeetsEnvironmentConditions(localPlayer, recipe) && Recipe.CollectedEnoughItemsToCraftRecipeNew(recipe))
				{
					Recipe.AddToAvailableRecipes(i);
				}
			}
			Recipe.TryRefocusingRecipe(oldRecipe);
			Recipe.VisuallyRepositionRecipes(focusY);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0010E079 File Offset: 0x0010C279
		private static void AddToAvailableRecipes(int recipeIndex)
		{
			Main.availableRecipe[Main.numAvailableRecipes] = recipeIndex;
			Main.numAvailableRecipes++;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0010E094 File Offset: 0x0010C294
		public static bool CollectedEnoughItemsToCraftRecipeOld(Recipe tempRec)
		{
			for (int i = 0; i < Recipe.maxRequirements; i++)
			{
				Item item = tempRec.requiredItem[i];
				if (item.type == 0)
				{
					break;
				}
				int num = item.stack;
				bool flag = false;
				foreach (int num2 in Recipe._ownedItems.Keys)
				{
					if (tempRec.useWood(num2, item.type) || tempRec.useSand(num2, item.type) || tempRec.useIronBar(num2, item.type) || tempRec.useFragment(num2, item.type) || tempRec.usePressurePlate(num2, item.type) || tempRec.AcceptedByItemGroups(num2, item.type))
					{
						num -= Recipe._ownedItems[num2];
						flag = true;
					}
				}
				if (!flag && Recipe._ownedItems.ContainsKey(item.netID))
				{
					num -= Recipe._ownedItems[item.netID];
				}
				if (num > 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0010E1BC File Offset: 0x0010C3BC
		public static bool CollectedEnoughItemsToCraftRecipeNew(Recipe tempRec)
		{
			for (int i = 0; i < Recipe.maxRequirements; i++)
			{
				Recipe.RequiredItemEntry requiredItemEntry = tempRec.requiredItemQuickLookup[i];
				if (requiredItemEntry.itemIdOrRecipeGroup == 0)
				{
					break;
				}
				int num;
				if (!Recipe._ownedItems.TryGetValue(requiredItemEntry.itemIdOrRecipeGroup, out num))
				{
					return false;
				}
				if (num < requiredItemEntry.stack)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0010E210 File Offset: 0x0010C410
		private static bool PlayerMeetsEnvironmentConditions(Player player, Recipe tempRec)
		{
			bool flag = !tempRec.needWater || player.adjWater || player.adjTile[172];
			bool flag2 = !tempRec.needHoney || tempRec.needHoney == player.adjHoney;
			bool flag3 = !tempRec.needLava || tempRec.needLava == player.adjLava;
			bool flag4 = !tempRec.needSnowBiome || player.ZoneSnow;
			bool flag5 = !tempRec.needGraveyardBiome || player.ZoneGraveyard;
			bool flag6 = !tempRec.needEverythingSeed || (Main.remixWorld && Main.getGoodWorld);
			return flag && flag2 && flag3 && flag4 && flag5 && flag6;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0010E2BC File Offset: 0x0010C4BC
		private static bool PlayerMeetsTileRequirements(Player player, Recipe tempRec)
		{
			int num = 0;
			while (num < Recipe.maxRequirements && tempRec.requiredTile[num] != -1)
			{
				if (!player.adjTile[tempRec.requiredTile[num]])
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0010E2F8 File Offset: 0x0010C4F8
		private static void CollectItemsToCraftWithFrom(Player player)
		{
			Recipe._ownedItems.Clear();
			Recipe.CollectItems(player.inventory, 58);
			if (player.useVoidBag() && player.chest != -5)
			{
				Recipe.CollectItems(player.bank4.item, 40);
			}
			if (player.chest != -1)
			{
				Item[] currentInventory = null;
				if (player.chest > -1)
				{
					currentInventory = Main.chest[player.chest].item;
				}
				else if (player.chest == -2)
				{
					currentInventory = player.bank.item;
				}
				else if (player.chest == -3)
				{
					currentInventory = player.bank2.item;
				}
				else if (player.chest == -4)
				{
					currentInventory = player.bank3.item;
				}
				else if (player.chest == -5)
				{
					currentInventory = player.bank4.item;
				}
				Recipe.CollectItems(currentInventory, 40);
			}
			Recipe.AddFakeCountsForItemGroups();
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0010E3D8 File Offset: 0x0010C5D8
		private static void AddFakeCountsForItemGroups()
		{
			foreach (RecipeGroup recipeGroup in RecipeGroup.recipeGroups.Values)
			{
				int groupFakeItemId = recipeGroup.GetGroupFakeItemId();
				Recipe._ownedItems[groupFakeItemId] = recipeGroup.CountUsableItems(Recipe._ownedItems);
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0010E448 File Offset: 0x0010C648
		private static void CollectItems(Item[] currentInventory, int slotCap)
		{
			for (int i = 0; i < slotCap; i++)
			{
				Item item = currentInventory[i];
				if (item.stack > 0)
				{
					int num = item.stack;
					int num2;
					if (Recipe._ownedItems.TryGetValue(item.netID, out num2))
					{
						num += num2;
					}
					Recipe._ownedItems[item.netID] = num;
				}
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0010E4A0 File Offset: 0x0010C6A0
		private static void CollectGuideRecipes()
		{
			int type = Main.guideItem.type;
			for (int i = 0; i < Recipe.maxRecipes; i++)
			{
				Recipe recipe = Main.recipe[i];
				if (recipe.createItem.type == 0)
				{
					break;
				}
				for (int j = 0; j < Recipe.maxRequirements; j++)
				{
					Item item = recipe.requiredItem[j];
					if (item.type == 0)
					{
						break;
					}
					if (Main.guideItem.IsTheSameAs(item) || recipe.useWood(type, item.type) || recipe.useSand(type, item.type) || recipe.useIronBar(type, item.type) || recipe.useFragment(type, item.type) || recipe.AcceptedByItemGroups(type, item.type) || recipe.usePressurePlate(type, item.type))
					{
						Main.availableRecipe[Main.numAvailableRecipes] = i;
						Main.numAvailableRecipes++;
						break;
					}
				}
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0010E59C File Offset: 0x0010C79C
		public static void ClearAvailableRecipes()
		{
			for (int i = 0; i < Recipe.maxRecipes; i++)
			{
				Main.availableRecipe[i] = 0;
			}
			Main.numAvailableRecipes = 0;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0010E5C8 File Offset: 0x0010C7C8
		private static void VisuallyRepositionRecipes(float focusY)
		{
			float num = Main.availableRecipeY[Main.focusRecipe] - focusY;
			for (int i = 0; i < Recipe.maxRecipes; i++)
			{
				Main.availableRecipeY[i] -= num;
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0010E604 File Offset: 0x0010C804
		private static void TryRefocusingRecipe(int oldRecipe)
		{
			for (int i = 0; i < Main.numAvailableRecipes; i++)
			{
				if (oldRecipe == Main.availableRecipe[i])
				{
					Main.focusRecipe = i;
					break;
				}
			}
			if (Main.focusRecipe >= Main.numAvailableRecipes)
			{
				Main.focusRecipe = Main.numAvailableRecipes - 1;
			}
			if (Main.focusRecipe < 0)
			{
				Main.focusRecipe = 0;
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0010E65C File Offset: 0x0010C85C
		public static void SetupRecipeGroups()
		{
			RecipeGroup rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Lang.GetNPCNameValue(74), new int[]
			{
				2015,
				2016,
				2017
			});
			RecipeGroupID.Birds = RecipeGroup.RegisterGroup("Birds", rec);
			rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Lang.GetNPCNameValue(367), new int[]
			{
				2157,
				2156
			});
			RecipeGroupID.Scorpions = RecipeGroup.RegisterGroup("Scorpions", rec);
			rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Lang.GetNPCNameValue(299), new int[]
			{
				2018,
				3563
			});
			RecipeGroupID.Squirrels = RecipeGroup.RegisterGroup("Squirrels", rec);
			rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Lang.misc[85].Value, new int[]
			{
				3194,
				3192,
				3193
			});
			RecipeGroupID.Bugs = RecipeGroup.RegisterGroup("Bugs", rec);
			rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Lang.misc[86].Value, new int[]
			{
				2123,
				2122
			});
			RecipeGroupID.Ducks = RecipeGroup.RegisterGroup("Ducks", rec);
			rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Lang.misc[87].Value, new int[]
			{
				1998,
				2001,
				1994,
				1995,
				1996,
				1999,
				1997,
				2000
			});
			RecipeGroupID.Butterflies = RecipeGroup.RegisterGroup("Butterflies", rec);
			rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Lang.misc[88].Value, new int[]
			{
				1992,
				2004
			});
			RecipeGroupID.Fireflies = RecipeGroup.RegisterGroup("Fireflies", rec);
			rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Lang.misc[95].Value, new int[]
			{
				2006,
				2007
			});
			RecipeGroupID.Snails = RecipeGroup.RegisterGroup("Snails", rec);
			rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Lang.misc[105].Value, new int[]
			{
				4334,
				4335,
				4336,
				4338,
				4339,
				4337
			});
			RecipeGroupID.Dragonflies = RecipeGroup.RegisterGroup("Dragonflies", rec);
			rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Lang.GetNPCNameValue(616), new int[]
			{
				4464,
				4465
			});
			RecipeGroupID.Turtles = RecipeGroup.RegisterGroup("Turtles", rec);
			rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.Macaw"), new int[]
			{
				5212,
				5300
			});
			RecipeGroupID.Macaws = RecipeGroup.RegisterGroup("Macaws", rec);
			rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.Cockatiel"), new int[]
			{
				5312,
				5313
			});
			RecipeGroupID.Cockatiels = RecipeGroup.RegisterGroup("Cockatiels", rec);
			rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.CloudBalloon"), new int[]
			{
				399,
				1250
			});
			RecipeGroupID.CloudBalloons = RecipeGroup.RegisterGroup("Cloud Balloons", rec);
			rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.BlizzardBalloon"), new int[]
			{
				1163,
				1251
			});
			RecipeGroupID.BlizzardBalloons = RecipeGroup.RegisterGroup("Blizzard Balloons", rec);
			rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.SandstormBalloon"), new int[]
			{
				983,
				1252
			});
			RecipeGroupID.SandstormBalloons = RecipeGroup.RegisterGroup("Sandstorm Balloons", rec);
			rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.CritterGuides"), new int[]
			{
				4767,
				5453
			});
			RecipeGroupID.CritterGuides = RecipeGroup.RegisterGroup("Guide to Critter Companionship", rec);
			rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.NatureGuides"), new int[]
			{
				5309,
				5454
			});
			RecipeGroupID.NatureGuides = RecipeGroup.RegisterGroup("Guide to Nature Preservation", rec);
			rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.Fruit"), new int[]
			{
				4009,
				4282,
				4283,
				4284,
				4285,
				4286,
				4287,
				4288,
				4289,
				4290,
				4291,
				4292,
				4293,
				4294,
				4295,
				4296,
				4297,
				5277,
				5278
			});
			RecipeGroupID.Fruit = RecipeGroup.RegisterGroup("Fruit", rec);
			rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.Balloon"), new int[]
			{
				3738,
				3736,
				3737
			});
			RecipeGroupID.Balloons = RecipeGroup.RegisterGroup("Balloons", rec);
			rec = new RecipeGroup(() => "replaceme wood", new int[]
			{
				9,
				619,
				620,
				621,
				911,
				1729,
				2504,
				2503,
				5215
			});
			RecipeGroupID.Wood = RecipeGroup.RegisterGroup("Wood", rec);
			rec = new RecipeGroup(() => "replaceme sand", new int[]
			{
				169,
				408,
				1246,
				370,
				3272,
				3338,
				3274,
				3275
			});
			RecipeGroupID.Sand = RecipeGroup.RegisterGroup("Sand", rec);
			rec = new RecipeGroup(() => "replaceme ironbar", new int[]
			{
				22,
				704
			});
			RecipeGroupID.IronBar = RecipeGroup.RegisterGroup("IronBar", rec);
			rec = new RecipeGroup(() => "replaceme fragment", new int[]
			{
				3458,
				3456,
				3457,
				3459
			});
			RecipeGroupID.Fragment = RecipeGroup.RegisterGroup("Fragment", rec);
			rec = new RecipeGroup(() => "replaceme pressureplate", new int[]
			{
				852,
				543,
				542,
				541,
				1151,
				529,
				853,
				4261
			});
			RecipeGroupID.PressurePlate = RecipeGroup.RegisterGroup("PressurePlate", rec);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0010ED40 File Offset: 0x0010CF40
		public static void UpdateItemVariants()
		{
			for (int i = 0; i < Recipe.maxRecipes; i++)
			{
				Recipe recipe = Main.recipe[i];
				recipe.createItem.Refresh(true);
				Item[] array = recipe.requiredItem;
				for (int j = 0; j < array.Length; j++)
				{
					array[j].Refresh(true);
				}
			}
			if (Main.remixWorld && Main.getGoodWorld)
			{
				ItemID.Sets.IsAMaterial[544] = true;
				ItemID.Sets.IsAMaterial[556] = true;
				ItemID.Sets.IsAMaterial[557] = true;
				return;
			}
			ItemID.Sets.IsAMaterial[544] = false;
			ItemID.Sets.IsAMaterial[556] = false;
			ItemID.Sets.IsAMaterial[557] = false;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0010EDE4 File Offset: 0x0010CFE4
		public static void SetupRecipes()
		{
			int num = 5;
			int stack = 2;
			Recipe.SetupRecipeGroups();
			Recipe.currentRecipe.createItem.SetDefaults(8);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(23);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(974);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(664);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(974);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(833);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(974);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(834);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(974);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(835);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4383);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3272);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4383);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3274);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4383);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3275);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4383);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3338);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4384);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(275);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4385);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(61);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4385);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(833);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4385);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3274);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4386);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(836);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4386);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(835);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4386);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3275);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4387);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(409);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4387);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(834);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4387);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3338);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4388);
			Recipe.currentRecipe.createItem.stack = 25;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(331);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5293);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(183);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3114);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3111);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(433);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(173);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(523);
			Recipe.currentRecipe.createItem.stack = 33;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 33;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(522);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1333);
			Recipe.currentRecipe.createItem.stack = 33;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 33;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1332);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3045);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(662);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(427);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(177);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(428);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(178);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(429);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(179);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(432);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(180);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(430);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(181);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(431);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(182);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1245);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(999);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5378);
			Recipe.currentRecipe.createItem.stack = 33;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(931);
			Recipe.currentRecipe.requiredItem[0].stack = 33;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(522);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5379);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(931);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(662);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5377);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(931);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3002);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(966);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3048);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(974);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3047);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(433);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3046);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(523);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3049);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1333);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3050);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3045);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3723);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2274);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3724);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3004);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4689);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4383);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4690);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4384);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4691);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4385);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4692);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4386);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4693);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4387);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4694);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4388);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5299);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(5293);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5357);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(5353);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2751);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2701);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(522);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2752);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2701);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(433);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2753);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2701);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2754);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2701);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1332);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2755);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2701);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2274);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(985);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(965);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3005);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2996);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3078);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(150);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3080);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3078);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3077);
			Recipe.currentRecipe.createItem.stack = 30;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3079);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3077);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2590);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(353);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(23);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(8);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1130);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(168);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2431);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2586);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(168);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(23);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(235);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(166);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(23);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2896);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(167);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(23);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3116);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(168);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3111);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3115);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(166);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3111);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3547);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(167);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3111);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4423);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(166);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3380);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4908);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(166);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2);
			Recipe.currentRecipe.requiredItem[1].stack = 25;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4909);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4908);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(23);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4909);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(235);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2);
			Recipe.currentRecipe.requiredItem[1].stack = 25;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1338);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2019);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(167);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(286);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(282);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(23);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3112);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(282);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3111);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3191);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2002);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(75);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2243);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2244);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(351);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(353);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(351);
			Recipe.currentRecipe.requiredTile[0] = 94;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(357);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(261);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3195);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3192);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3193);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3194);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1787);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5092);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				68,
				8
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.corruption = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5092);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				1330,
				8
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.crimson = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5093);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2121);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4033);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2121
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4032);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2123
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4032);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2122
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4032);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4374
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4031);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2015
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4031);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2017
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4031);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2016
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4031);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2205
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4031);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4359
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4024);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2018
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4024);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3563
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4014);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2019
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4019);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2006
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4022);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2889
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4022);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2890
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4022);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2892
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4022);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2894
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4022);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3564
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4022);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2893
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4022);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2895
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4022);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2891
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4022);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4274
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4022);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4340
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4022);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4362
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4022);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4482
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4022);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4419
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2425);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2290);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2425);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2297);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2425);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2299);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2427);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2300);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2427);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2298);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2427);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2301);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2427);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4401);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2426);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2316);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4403);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4402);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5009);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 622;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4614);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4009);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(31);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4617);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4283);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(31);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(593);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4615);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4023);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(31);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4616);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4291);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(31);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4618);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4293);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(31);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4619);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4294);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4287);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(31);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4620);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4292);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4294);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(31);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4621);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4285);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4296);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(31);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4622);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4284);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4289);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(31);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4624);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4009);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(31);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Fruit);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4625);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4009);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(356);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Fruit);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4623);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4297);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4288);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(31);
			Recipe.currentRecipe.requiredTile[0] = 96;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4034);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2303,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4034);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2317,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4034);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2305,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4034);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2304,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4034);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2313,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4034);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2318,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4034);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2312,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4034);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2306,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4034);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2319,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4034);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2314,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4034);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2302,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4034);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2315,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4034);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2307,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4034);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2310,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4034);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2309,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4034);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2321,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4034);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2311,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				96
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(968);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(967);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(31);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(126);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(31);
			Recipe.currentRecipe.needWater = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1134);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(31);
			Recipe.currentRecipe.needHoney = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(422);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(501);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(369);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(423);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(370);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(59);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3477);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1246);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(2171);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(28);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(23);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(31);
			Recipe.currentRecipe.requiredItem[2].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(227);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3111);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(31);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(188);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(28);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(183);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(499);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(501);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(502);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3544);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(499);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3457);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(3459);
			Recipe.currentRecipe.requiredItem[4].SetDefaults(3456);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(189);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(110);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(183);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2209);
			Recipe.currentRecipe.createItem.stack = 15;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(500);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(75);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(502);
			Recipe.currentRecipe.requiredItem[2].stack = 3;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(526);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2756);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(313);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(314);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(315);
			Recipe.currentRecipe.requiredItem[4].SetDefaults(317);
			Recipe.currentRecipe.requiredItem[5].SetDefaults(316);
			Recipe.currentRecipe.requiredItem[6].SetDefaults(2358);
			Recipe.currentRecipe.requiredItem[7].SetDefaults(318);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5099);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(313);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(314);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(315);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(317);
			Recipe.currentRecipe.requiredItem[4].SetDefaults(316);
			Recipe.currentRecipe.requiredItem[5].SetDefaults(2358);
			Recipe.currentRecipe.requiredItem[6].SetDefaults(318);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(288);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(318);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(317);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(173);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(289);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(313);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(5);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(290);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(315);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(276);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(291);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(317);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(275);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(292);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(313);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(11);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(292);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(313);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(700);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(293);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(314);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(313);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(75);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(294);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(314);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(316);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(75);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(295);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(313);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(315);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(320);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(296);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(315);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(314);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(13);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(296);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(315);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(314);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(702);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(297);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(315);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(314);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(298);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(313);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(183);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(299);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(313);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(315);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(300);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(316);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(68);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(300);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(316);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1330);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(301);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(316);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(276);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(302);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(317);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(319);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(303);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(313);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(38);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(304);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(313);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(315);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(319);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(305);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(318);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(316);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(315);
			Recipe.currentRecipe.requiredItem[4].SetDefaults(320);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2354);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1127);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(317);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2356);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(999);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(314);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(2358);
			Recipe.currentRecipe.requiredItem[4].SetDefaults(317);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2325);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(315);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(2358);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(314);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2326);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(316);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(2358);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2329);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2358);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(150);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2355);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(317);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(275);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2322);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(323);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(315);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2327);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2358);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(317);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2323);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2305);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(313);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2324);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2304);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(313);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2328);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2311);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(314);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2344);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2313);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(314);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2345);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2310);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(314);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(2358);
			Recipe.currentRecipe.requiredItem[4].SetDefaults(317);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2346);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2303);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(315);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2348);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2312);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(2315);
			Recipe.currentRecipe.requiredItem[2].stack = 2;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(318);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2347);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2319);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(316);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2349);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2318);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(316);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2350);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2309);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(313);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2997);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2309);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(315);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2351);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2317);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(315);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(318);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2352);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2307);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(2358);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2353);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2321);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(316);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2359);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2306);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(2358);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4477);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(317);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(4361);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(4412);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4478);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(317);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(4361);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(4413);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4479);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(317);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(4361);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(4414);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4870);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2350);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2315);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5211);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				126,
				1,
				318,
				1,
				315,
				1,
				314,
				1,
				62,
				5
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				13
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1359);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(209);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 243;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1354);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(174);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 243;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1340);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1339);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 243;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1355);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1348);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 243;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1356);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1332);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 243;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1353);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(522);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 243;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1357);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1346);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 243;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1358);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1345);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 243;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5068);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(5070);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[2].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5068);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(5070);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[2].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5069);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5070);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5069);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5070);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5120);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5070);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(56);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(38);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.currentRecipe.corruption = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5120);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5070);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(880);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(38);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.currentRecipe.crimson = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3092);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(520);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3091);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(521);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(949);
			Recipe.currentRecipe.createItem.stack = 15;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(593);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3103);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(40);
			Recipe.currentRecipe.requiredItem[0].stack = 3996;
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3104);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(97);
			Recipe.currentRecipe.requiredItem[0].stack = 3996;
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(40);
			Recipe.currentRecipe.createItem.stack = 25;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(41);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(40);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(988);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(40);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(974);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(51);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(40);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(75);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(47);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(40);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(69);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(47);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(40);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1330);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(265);
			Recipe.currentRecipe.createItem.stack = 100;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(40);
			Recipe.currentRecipe.requiredItem[0].stack = 100;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(175);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(545);
			Recipe.currentRecipe.createItem.stack = 150;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(40);
			Recipe.currentRecipe.requiredItem[0].stack = 150;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(522);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1334);
			Recipe.currentRecipe.createItem.stack = 150;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(40);
			Recipe.currentRecipe.requiredItem[0].stack = 150;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1332);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(516);
			Recipe.currentRecipe.createItem.stack = 200;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(40);
			Recipe.currentRecipe.requiredItem[0].stack = 200;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(501);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(526);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1341);
			Recipe.currentRecipe.createItem.stack = 35;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(40);
			Recipe.currentRecipe.requiredItem[0].stack = 35;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1339);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1235);
			Recipe.currentRecipe.createItem.stack = 150;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1310);
			Recipe.currentRecipe.createItem.stack = 100;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(209);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3009);
			Recipe.currentRecipe.createItem.stack = 100;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(502);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3010);
			Recipe.currentRecipe.createItem.stack = 100;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(522);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3011);
			Recipe.currentRecipe.createItem.stack = 100;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1332);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(234);
			Recipe.currentRecipe.createItem.stack = 70;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(97);
			Recipe.currentRecipe.requiredItem[0].stack = 70;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(117);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(278);
			Recipe.currentRecipe.createItem.stack = 70;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(97);
			Recipe.currentRecipe.requiredItem[0].stack = 70;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(21);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.AddCustomShimmerResult(97, 70);
			Recipe.currentRecipe.AddCustomShimmerResult(14, 1);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4915);
			Recipe.currentRecipe.createItem.stack = 70;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(97);
			Recipe.currentRecipe.requiredItem[0].stack = 70;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(705);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.AddCustomShimmerResult(97, 70);
			Recipe.currentRecipe.AddCustomShimmerResult(701, 1);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(515);
			Recipe.currentRecipe.createItem.stack = 100;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(97);
			Recipe.currentRecipe.requiredItem[0].stack = 100;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(502);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(546);
			Recipe.currentRecipe.createItem.stack = 150;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(97);
			Recipe.currentRecipe.requiredItem[0].stack = 150;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(522);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1335);
			Recipe.currentRecipe.createItem.stack = 150;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(97);
			Recipe.currentRecipe.requiredItem[0].stack = 150;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1332);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1179);
			Recipe.currentRecipe.createItem.stack = 60;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(97);
			Recipe.currentRecipe.requiredItem[0].stack = 60;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1006);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1302);
			Recipe.currentRecipe.createItem.stack = 50;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1432);
			Recipe.currentRecipe.requiredItem[0].stack = 50;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1344);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1349);
			Recipe.currentRecipe.createItem.stack = 50;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1432);
			Recipe.currentRecipe.requiredItem[0].stack = 50;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1345);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1350);
			Recipe.currentRecipe.createItem.stack = 50;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1432);
			Recipe.currentRecipe.requiredItem[0].stack = 50;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1346);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1351);
			Recipe.currentRecipe.createItem.stack = 50;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1432);
			Recipe.currentRecipe.requiredItem[0].stack = 50;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1347);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1352);
			Recipe.currentRecipe.createItem.stack = 50;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1432);
			Recipe.currentRecipe.requiredItem[0].stack = 50;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1348);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1342);
			Recipe.currentRecipe.createItem.stack = 50;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1432);
			Recipe.currentRecipe.requiredItem[0].stack = 50;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1339);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4447);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4459);
			Recipe.currentRecipe.needWater = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4448);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4459);
			Recipe.currentRecipe.needLava = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4449);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4459);
			Recipe.currentRecipe.needHoney = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4459);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4447);
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4459);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4448);
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4459);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4449);
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4824);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4827);
			Recipe.currentRecipe.needWater = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4825);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4827);
			Recipe.currentRecipe.needLava = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4826);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4827);
			Recipe.currentRecipe.needHoney = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4827);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4824);
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4827);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4825);
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4827);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4826);
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4457);
			Recipe.currentRecipe.createItem.stack = 100;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(773);
			Recipe.currentRecipe.requiredItem[0].stack = 100;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1552);
			Recipe.currentRecipe.requiredTile[0] = 247;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4458);
			Recipe.currentRecipe.createItem.stack = 100;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(774);
			Recipe.currentRecipe.requiredItem[0].stack = 100;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1552);
			Recipe.currentRecipe.requiredTile[0] = 247;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(67);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(60);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2886);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2887);
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5438);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5395);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(172);
			Recipe.currentRecipe.requiredItem[2].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 13;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(287);
			Recipe.currentRecipe.createItem.stack = 50;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(279);
			Recipe.currentRecipe.requiredItem[0].stack = 50;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(67);
			Recipe.currentRecipe.corruption = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(287);
			Recipe.currentRecipe.createItem.stack = 50;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(279);
			Recipe.currentRecipe.requiredItem[0].stack = 50;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2886);
			Recipe.currentRecipe.crimson = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(94);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4416);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(129);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2518);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2566);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(632);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(631);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(913);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(633);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2744);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1702);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1796);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1818);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(634);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2549);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2581);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(762);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2627);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2628);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(824);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2629);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2630);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1457);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(192);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3144);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3100);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3146);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3087);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3145);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3066);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2822);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2860);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3903);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3234);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3945);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				1
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3905);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3906);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1101);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3907);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(763);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3957);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3955);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3908);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(664);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4159);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				1
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4180);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				1
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4201);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				1
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4222);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				1
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4311);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				1
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4580);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				1
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5162);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				1
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5183);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				1
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5204);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				1
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5292);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4392,
				1
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1384);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				134,
				1
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1386);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				137,
				1
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1385);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				139,
				1
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1389);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1388);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(145);
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1418);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1387);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(717);
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1431);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(31);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(75);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1993);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(31);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1992);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2005);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(31);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2004);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4848);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(31);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4847);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5351);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(31);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(5350);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4695);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(31);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(520);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4696);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(31);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(521);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4697);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(31);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(575);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4698);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(31);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(549);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4699);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(31);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(548);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4700);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(31);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(547);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1859);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(29);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(85);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(344);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(342);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(341);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(347);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3782);
			Recipe.currentRecipe.createItem.stack = 3;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3186);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(169);
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.currentRecipe.anySand = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3182);
			Recipe.currentRecipe.createItem.stack = 1;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3186);
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.currentRecipe.needWater = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3184);
			Recipe.currentRecipe.createItem.stack = 1;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3186);
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.currentRecipe.needLava = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3185);
			Recipe.currentRecipe.createItem.stack = 1;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3186);
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.currentRecipe.needHoney = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2693);
			Recipe.currentRecipe.createItem.stack = 1;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.needWater = true;
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2169);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2693);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2694);
			Recipe.currentRecipe.createItem.stack = 1;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.needLava = true;
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2170);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2694);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2787);
			Recipe.currentRecipe.createItem.stack = 1;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.needHoney = true;
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2788);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2787);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3754);
			Recipe.currentRecipe.createItem.stack = 1;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(169);
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3752);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3754);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3755);
			Recipe.currentRecipe.createItem.stack = 1;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(593);
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3753);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3755);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4277);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(75);
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4279);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4277);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4278);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(75);
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4280);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4278);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5291);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4392);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4490);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4640);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4491);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4641);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4492);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4642);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4493);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4643);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4494);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4644);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4495);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4645);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4647);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4646);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4496);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4349);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4497);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4350);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4498);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4351);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4499);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4352);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4500);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4353);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4503);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(150);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4529);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(62);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4531);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(62);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4530);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(195);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4532);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(195);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3340);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3272,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3341);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3274,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3342);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3275,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3343);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3338,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3344);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3276,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3345);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3277,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3346);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3339,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3348);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3347,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(663);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(662);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2695);
			Recipe.currentRecipe.createItem.stack = 1;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1345);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2696);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2695);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2697);
			Recipe.currentRecipe.createItem.stack = 1;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1345);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2698);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2697);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3748);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3743);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3744);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3745);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4090);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2625);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4090);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4071);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4090);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4072);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4090);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4073);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4090);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2626);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4090);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(275);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(148);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(105);
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.currentRecipe.needWater = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(148);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(713);
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.currentRecipe.needWater = true;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5322);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(57);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.currentRecipe.corruption = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5322);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1257);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.currentRecipe.crimson = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5323);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4767);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(5309);
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.CritterGuides);
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.NatureGuides);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5295);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(213);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3506);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(331);
			Recipe.currentRecipe.requiredItem[2].stack = 12;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(210);
			Recipe.currentRecipe.requiredItem[3].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5295);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(213);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3500);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(331);
			Recipe.currentRecipe.requiredItem[2].stack = 12;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(210);
			Recipe.currentRecipe.requiredItem[3].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(752);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(751);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(222);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(133);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(350);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(133);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(356);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(133);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4326);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(133);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(169);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.currentRecipe.anySand = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(392);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1267);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(392);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(181);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1268);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(392);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(180);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1269);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(392);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(177);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1270);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(392);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(179);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1271);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(392);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(178);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4260);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(392);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(999);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1272);
			Recipe.currentRecipe.createItem.stack = 50;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(392);
			Recipe.currentRecipe.requiredItem[0].stack = 50;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(181);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(180);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(177);
			Recipe.currentRecipe.requiredItem[4].SetDefaults(179);
			Recipe.currentRecipe.requiredItem[5].SetDefaults(178);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1970);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(181);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			int defaults = 1970;
			int defaults2 = 2679;
			int defaults3 = 2680;
			Recipe.currentRecipe.createItem.SetDefaults(defaults2);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(defaults3);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1971);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(180);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			defaults = 1971;
			defaults2 = 2689;
			defaults3 = 2690;
			Recipe.currentRecipe.createItem.SetDefaults(defaults2);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(defaults3);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1972);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(177);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			defaults = 1972;
			defaults2 = 2687;
			defaults3 = 2688;
			Recipe.currentRecipe.createItem.SetDefaults(defaults2);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(defaults3);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1973);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(179);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			defaults = 1973;
			defaults2 = 2683;
			defaults3 = 2684;
			Recipe.currentRecipe.createItem.SetDefaults(defaults2);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(defaults3);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1974);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(178);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			defaults = 1974;
			defaults2 = 2685;
			defaults3 = 2686;
			Recipe.currentRecipe.createItem.SetDefaults(defaults2);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(defaults3);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1975);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(182);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			defaults = 1975;
			defaults2 = 2681;
			defaults3 = 2682;
			Recipe.currentRecipe.createItem.SetDefaults(defaults2);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(defaults3);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1976);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(999);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			defaults = 1976;
			defaults2 = 2677;
			defaults3 = 2678;
			Recipe.currentRecipe.createItem.SetDefaults(defaults2);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(defaults3);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4238);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(134);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4239);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(137);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4240);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(139);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(135);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(134);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1379);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(134);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1378);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(134);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(138);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(137);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1383);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(137);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1382);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(137);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(140);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(139);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1381);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(139);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1380);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(139);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2119);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2433);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2119);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4962);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2120);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(169);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3272);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(169);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2);
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3271);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(169);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2173);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(12);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2432);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2173);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2692);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(699);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2691);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2692);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(775);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(23);
			Recipe.currentRecipe.requiredTile[0] = 217;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1102);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1101);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(129);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(130);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(129);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4415);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(129);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(131);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(133);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(132);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(131);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(145);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(12);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(146);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(145);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3951);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(11);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3952);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3951);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3953);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(700);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3954);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3953);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(144);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(143);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(143);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(14);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(142);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(141);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(141);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(13);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(717);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(699);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(720);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(717);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(721);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(718);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(718);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(701);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(722);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(719);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(719);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(702);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(214);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(174);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3067);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(214);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4533);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				174,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4534);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				174,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4535);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				174,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4536);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				174,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(192);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(173);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(330);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(192);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4507);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				173,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(606);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(577);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(594);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(593);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(595);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(594);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4489);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				593,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(883);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4506);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				664,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(884);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(883);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(587);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(586);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(592);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(591);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(607);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(169);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(608);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(607);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4051);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3271);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4053);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4051);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3273);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3271,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4565);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4564);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4547);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4564);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4564);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4547);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4548);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4547);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(412);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(409);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(417);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(412);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4488);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				409,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4525);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				409,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4526);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				409,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4527);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				409,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4528);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				409,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(609);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(61);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(610);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(609);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4486);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(61);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4513);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				61,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4514);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				61,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4515);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				61,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4516);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				61,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4050);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(836);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4052);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4050);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4509);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				836,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4517);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				836,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4518);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				836,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4519);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				836,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4520);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				836,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(413);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(172);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(418);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(413);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(414);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(176);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(419);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(414);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(611);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(424);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(133);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(615);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(611);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(612);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(424);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(169);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(616);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(612);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(613);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(424);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(255);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(617);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(613);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(614);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(424);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(618);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(614);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3100);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(116);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3101);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3100);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2793);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(880);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(836);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2790);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2793);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(415);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(364);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(420);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(415);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(416);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(365);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(421);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(416);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(604);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(366);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(605);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(604);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1589);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1104);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1590);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1589);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1591);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1105);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1592);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1591);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1593);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1106);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1594);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1593);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2792);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(947);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2789);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2792);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2794);
			Recipe.currentRecipe.createItem.stack = 25;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1552);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2791);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2794);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3461);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3460);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3472);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3461);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5409);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5401);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5410);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5402);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5411);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5403);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5412);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5404);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5413);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5405);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5414);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5406);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5415);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5407);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5416);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5408);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5418);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5417);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5420);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5419);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5422);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5421);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5424);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5423);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5426);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5425);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5428);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5427);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5436);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5435);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5434);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5433);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5432);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5431);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5430);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5429);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5439);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4354);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(133);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5440);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4389);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(133);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5441);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4377);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(133);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5442);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4378);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(133);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5443);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5127);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(133);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5444);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5128);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(133);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5397);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5349);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5398);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5349);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5399);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5398);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5445);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5439);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5446);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5440);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5447);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5441);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5448);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5442);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5449);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5443);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5450);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5444);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3573);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3574);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3456);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3575);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3457);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3576);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3459);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3234);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(502);
			Recipe.currentRecipe.requiredTile[0] = 133;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3238);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3234);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2435);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(275);
			Recipe.currentRecipe.needWater = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5306);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(275);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(2625);
			Recipe.currentRecipe.needWater = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5307);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5306);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5396);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5395);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(577);
			Recipe.currentRecipe.createItem.stack = 5;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(56);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(61);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(176);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2);
			Recipe.currentRecipe.needWater = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4487);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				176,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(30);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4501);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4510);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4511);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4521);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4522);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4523);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4524);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(26);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4502);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4512);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4537);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4538);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4539);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4540);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1723);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3584);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(93);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(623);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(622);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(927);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(624);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2505);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2506);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5216);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5215);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(764);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1726);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1728);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1727);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1730);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3751);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2861);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2860);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3760);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3736);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3761);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3737);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3762);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3738);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3239);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3240);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(25);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(34);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(48);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2827);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(32);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(36);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(333);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(224);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(334);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(354);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2519);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2520);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2521);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2536);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2522);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2523);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2524);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2525);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2526);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2601);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2528);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2529);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2533);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2530);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2531);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2527);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2850);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4118);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2504,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2532);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2534);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4717);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2552);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2553);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2554);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2555);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(974);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2556);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(974);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2557);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2558);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(974);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2559);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2560);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2561);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2562);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2563);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(974);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2564);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(974);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2565);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(858);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2852);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4119);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2503,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(677);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(673);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4718);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2597);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(651);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(629);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(626);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2829);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(639);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(636);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(642);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(645);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(648);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2026);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2077);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2050);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2038);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2060);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2087);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2098);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2399);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4097);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				620,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(650);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(628);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2593);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(625);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2828);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(638);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(635);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(641);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(644);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(647);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2021);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2073);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2033);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2046);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2056);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2083);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2093);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2398);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4096);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				619,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2604);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(912);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(915);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(914);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2835);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(917);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(916);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(919);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(920);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2127);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2136);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(918);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2142);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2150);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2146);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2132);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2154);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2401);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4105);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				911,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2602);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(652);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(630);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(627);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2830);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(640);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(637);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(643);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(646);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(649);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2027);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2078);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2039);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2051);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2061);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2088);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2099);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2400);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4098);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				621,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.AddAshWoodFurnitureArmorAndItems();
			Recipe.currentRecipe.createItem.SetDefaults(4721);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2537);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2538);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2539);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2540);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2541);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2542);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(810);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2543);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2544);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2599);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(818);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2545);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2547);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2546);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2548);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2413);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2851);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4103);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				183,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2550);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(814);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2567);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2568);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2569);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2570);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2571);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2572);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2573);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2574);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2575);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2576);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2577);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2578);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2579);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2580);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2582);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2853);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4120);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				762,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				220
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2583);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(815);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3159);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3100);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3162);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3100);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3165);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3100);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3168);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3100);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3171);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3100);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3174);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3100);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3177);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3100);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3180);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3100);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3126);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3100);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3129);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3100);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3132);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3100);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3135);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3100);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3138);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3100);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3141);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3100);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3150);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3100);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3147);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3100);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4141);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3100,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3153);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3100);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3156);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3100);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3081);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3083);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3066);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3082);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3081);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4554);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3160);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3163);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3166);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3169);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3172);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3175);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3178);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3181);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3127);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3130);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3133);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3136);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3139);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3142);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3151);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3148);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4123);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3066,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3154);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3157);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3066);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3086);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3089);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3087);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3088);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3086);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4719);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3161);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3164);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3167);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3170);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3173);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3176);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3179);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3125);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3128);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3131);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3134);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3137);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3140);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3143);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3152);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3149);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4122);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3087,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3155);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3158);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3087);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2810);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2860);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2811);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2860);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2817);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2860);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2825);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2860);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2818);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2860);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2812);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2860);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2813);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2860);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2814);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2860);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2809);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(2860);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2815);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2860);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2816);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2860);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2819);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2860);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2820);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2860);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2821);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2860);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2823);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2860);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2855);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2860);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4121);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2860,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2824);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2860);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2826);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2860);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			int defaults4 = 3234;
			Recipe.currentRecipe.createItem.SetDefaults(3895);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults4);
			Recipe.currentRecipe.requiredItem[0].stack = 35;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3897);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults4);
			Recipe.currentRecipe.requiredItem[0].stack = 40;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3917);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults4);
			Recipe.currentRecipe.requiredItem[0].stack = 50;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3893);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults4);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3890);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults4);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3889);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults4);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3894);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults4);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3884);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults4);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3898);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(defaults4);
			Recipe.currentRecipe.requiredItem[1].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3888);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults4);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3911);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults4);
			Recipe.currentRecipe.requiredItem[0].stack = 40;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3892);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(defaults4);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3891);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults4);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3915);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(defaults4);
			Recipe.currentRecipe.requiredItem[1].stack = 40;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3918);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults4);
			Recipe.currentRecipe.requiredItem[0].stack = (int)((float)num * 2.5f);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4124);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3234,
				15
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3896);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults4);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3920);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults4);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3909);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(defaults4);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.AddRecipe();
			Recipe.AddSpiderFurniture();
			Recipe.AddLesionFurniture();
			Recipe.AddSolarFurniture();
			Recipe.AddVortexFurniture();
			Recipe.AddNebulaFurniture();
			Recipe.AddStardustFurniture();
			Recipe.AddSandstoneFurniture();
			Recipe.AddBambooFurniture();
			Recipe.AddCoralFurniture();
			Recipe.AddBalloonFurniture();
			Recipe.currentRecipe.createItem.SetDefaults(171);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4710);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1447);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2210);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2211);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2212);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2213);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2507);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2508);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5217);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5215);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1448);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(704);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2333);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4424);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4667);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4564);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			for (int i = 3665; i <= 3704; i++)
			{
				Recipe.currentRecipe.createItem.SetDefaults(i);
				Recipe.currentRecipe.requiredItem[0].SetDefaults(ItemID.Sets.TextureCopyLoad[i]);
				Recipe.currentRecipe.requiredItem[1].SetDefaults(530);
				Recipe.currentRecipe.requiredItem[1].stack = 10;
				Recipe.currentRecipe.requiredTile[0] = 283;
				Recipe.AddRecipe();
			}
			int[,] array = new int[,]
			{
				{
					3886,
					3884
				},
				{
					3887,
					3885
				},
				{
					3950,
					3939
				},
				{
					3976,
					3965
				},
				{
					4164,
					4153
				},
				{
					4185,
					4174
				},
				{
					4206,
					4195
				},
				{
					4227,
					4216
				},
				{
					4266,
					4265
				},
				{
					4268,
					4267
				},
				{
					4585,
					4574
				},
				{
					4713,
					4712
				},
				{
					5167,
					5156
				},
				{
					5188,
					5177
				},
				{
					5209,
					5198
				}
			};
			for (int j = 0; j < array.GetLength(0); j++)
			{
				Recipe.currentRecipe.createItem.SetDefaults(array[j, 0]);
				Recipe.currentRecipe.requiredItem[0].SetDefaults(array[j, 1]);
				Recipe.currentRecipe.requiredItem[1].SetDefaults(530);
				Recipe.currentRecipe.requiredItem[1].stack = 10;
				Recipe.currentRecipe.requiredTile[0] = 283;
				Recipe.AddRecipe();
			}
			Recipe.currentRecipe.createItem.SetDefaults(2340);
			Recipe.currentRecipe.createItem.stack = 50;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2492);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2340);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(542);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyPressurePlate = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(479);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(480);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3202);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1727);
			Recipe.currentRecipe.requiredItem[1].stack = 50;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(498);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1989);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3977);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2699);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3270);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5137);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5132,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5138);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5132,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(343);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 9;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(359);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(352);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5008);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(133);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[1].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(332);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			for (int k = 2114; k <= 2118; k++)
			{
				Recipe.currentRecipe.createItem.SetDefaults(k);
				Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
				Recipe.currentRecipe.requiredItem[0].stack = 12;
				Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
				Recipe.currentRecipe.requiredItem[1].stack = 3;
				Recipe.currentRecipe.requiredTile[0] = 106;
				Recipe.currentRecipe.anyWood = true;
				Recipe.currentRecipe.anyIronBar = true;
				Recipe.AddRecipe();
			}
			Recipe.currentRecipe.createItem.SetDefaults(1706);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1714);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1715);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(335);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2397);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(363);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5012);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5011);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 99;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5147);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3069);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(974);
			Recipe.currentRecipe.requiredItem[1].stack = 99;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(55);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(284);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(75);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5298);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(55);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4764);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(670);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2289);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(727);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(728);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(729);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(24);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 7;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(196);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(39);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3278);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(150);
			Recipe.currentRecipe.requiredItem[1].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3283);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3278);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(502);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(520);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(733);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(734);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(735);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2509);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2510);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2511);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2745);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 7;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2746);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2747);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2503);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2512);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2513);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2514);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2517);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 7;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2516);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2515);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2504);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(656);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 7;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(657);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(658);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(730);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(731);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(732);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(653);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 7;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(654);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(655);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(619);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(924);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(925);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(926);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(921);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 7;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(922);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(923);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(911);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(736);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(737);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(738);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(659);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 7;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(660);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(661);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(621);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1832);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[0].stack = 200;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1833);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[0].stack = 300;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1834);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[0].stack = 250;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2263);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2264);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2265);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2228);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2230);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3916);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3912);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3919);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2849);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4117);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2260,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2259);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2229);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2231);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2237);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2233);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2232);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2226);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2236);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2224);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2225);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2227);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2235);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2234);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2260);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2596);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(806);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(831);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(819);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3914);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2833);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(829);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2139);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2245);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2135);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2126);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2145);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2153);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2141);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2131);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2149);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2636);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2633);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 304;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4099);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				9,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				304
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2239);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 302;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1703);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 302;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2748);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 302;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1709);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 302;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2639);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 302;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2842);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 302;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1713);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 302;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1719);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 302;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2254);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 302;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2025);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 302;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2075);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 302;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2037);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 302;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2048);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 302;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2065);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 302;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2085);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 302;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2097);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 302;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2414);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 302;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4112);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				170,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				302
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2632);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 302;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1127);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1707);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1711);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2844);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1717);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2251);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1721);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2255);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2124);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2249);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2240);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2023);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2035);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2648);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2058);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2095);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2129);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2257);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2395);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2411);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 308;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4113);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				1125,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				308
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1924);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1872);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1925);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1872);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1926);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1872);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(824);
			Recipe.currentRecipe.createItem.stack = 25;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(75);
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(825);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(824);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(826);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2606);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(838);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3899);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[2].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(837);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2834);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(830);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2029);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2070);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2080);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2042);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2053);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2063);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2090);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2102);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2384);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2394);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2410);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2631);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(824);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4104);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				824,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				305
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(765);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(751);
			Recipe.currentRecipe.needWater = true;
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3756);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(751);
			Recipe.currentRecipe.needSnowBiome = true;
			Recipe.currentRecipe.requiredTile[0] = 305;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2595);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1101);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 303;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1143);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1101);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 303;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1137);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1101);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 303;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2836);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1101);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 303;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1144);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1101);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 303;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1142);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1101);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 303;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2030);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1101);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 303;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2069);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1101);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 303;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2079);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1101);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 303;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2041);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1101);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 303;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2052);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1101);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 303;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2062);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1101);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 303;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2089);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1101);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 303;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2101);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1101);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 303;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2385);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1101);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 303;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2396);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1101);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 303;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2416);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1101);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 303;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4106);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				1101,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				303
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2848);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 306;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2248);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 306;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2635);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 306;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2252);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 306;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2031);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 306;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2247);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 306;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2068);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 306;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2076);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 306;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(681);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 306;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2594);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 306;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2044);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 306;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3913);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 306;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2040);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(974);
			Recipe.currentRecipe.requiredTile[0] = 306;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2049);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(974);
			Recipe.currentRecipe.requiredTile[0] = 306;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2059);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(974);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 306;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2086);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(974);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 306;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2100);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(974);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 306;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2288);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 306;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4111);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				664,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				306
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1708);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 307;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2649);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 307;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2655);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 307;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1712);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 307;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2638);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 307;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2845);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 307;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1718);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 307;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2253);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 307;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1722);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 307;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2256);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 307;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2125);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 307;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2250);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 307;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2241);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 307;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2024);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 307;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2036);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 307;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2096);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 307;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2130);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 307;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2412);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 307;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4114);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				1344,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				307
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(894);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(895);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(896);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(881);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(882);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(750);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(816);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(807);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2616);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2592);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(812);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2020);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2066);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2072);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2032);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2045);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2055);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2082);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2092);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2382);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2392);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2408);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2854);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2743);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4100);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				276,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2661);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2669);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2670);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2668);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2603);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1793);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2637);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1792);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2656);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2619);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2671);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2846);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1794);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1795);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1813);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2643);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2641);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1808);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2054);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4115);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				1725,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1812);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2415);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1731);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1732);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1733);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2605);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1815);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1814);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2620);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2847);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1816);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1817);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2028);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2071);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2081);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2043);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2650);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2064);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2091);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2103);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2383);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2393);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2409);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1729);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 106;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4116);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				1729,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(763);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(836);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 218;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(770);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(763);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2598);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(763);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 301;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(817);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(763);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 301;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2640);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(763);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 301;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(809);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(763);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 301;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2617);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(763);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 301;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(813);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(763);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 301;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2832);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(763);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 301;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(828);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(763);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 301;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2246);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(763);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 301;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2022);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(763);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 301;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2067);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(763);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 301;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2074);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(763);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 301;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2034);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(763);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 301;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2047);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(763);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 301;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2057);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(763);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 301;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2084);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(763);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 301;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2094);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(763);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 301;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2634);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(763);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 301;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4102);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				763,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				301
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(762);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(23);
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(769);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(762);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(767);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(762);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(664);
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3113);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3111);
			Recipe.currentRecipe.requiredTile[0] = 220;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1126);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1124);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(768);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(820);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2615);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2591);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(808);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(811);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2831);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(827);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2138);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2140);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2128);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2144);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2152);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2134);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2148);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2381);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 19;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(149);
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2391);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4101);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				154,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				300
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2407);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = num;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[1].stack = stack;
			Recipe.currentRecipe.requiredTile[0] = 300;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2618);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(173);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(174);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[2].stack = 2;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2840);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(173);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(174);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4110);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				173,
				4,
				174,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2613);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(139);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2839);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(139);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4109);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				139,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2614);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(134);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2837);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(134);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4107);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				134,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2612);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(137);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2838);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(137);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4108);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				137,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(361);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(362);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(150);
			Recipe.currentRecipe.requiredItem[0].stack = 7;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(337);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(338);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(339);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(340);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(255);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(195);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(247);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(255);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(248);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(255);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(249);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(255);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3773);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3794);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3774);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3794);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3775);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3794);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(240);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(254);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(241);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(254);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4132);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(981);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4133);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(981);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4134);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(981);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4128);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(254);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4129);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(254);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4130);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(254);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4652);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(255);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4653);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(255);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4654);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(255);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5045);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1330);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5046);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1330);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1050);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5047);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1330);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1050);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5048);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(133);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1992);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5049);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1992);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5050);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1992);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5051);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5052);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(68);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1050);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5053);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(68);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1050);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5054);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(313);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5055);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5056);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5057);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(275);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(75);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5058);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(75);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1037);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5060);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(75);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1037);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5061);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5062);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1016);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5063);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1016);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5102);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5103);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5115);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5116);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(262);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3266);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(173);
			Recipe.currentRecipe.requiredItem[1].stack = 20;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(86);
			Recipe.currentRecipe.requiredItem[2].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 77;
			Recipe.currentRecipe.corruption = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3267);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(173);
			Recipe.currentRecipe.requiredItem[1].stack = 20;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(86);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 77;
			Recipe.currentRecipe.corruption = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3268);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(173);
			Recipe.currentRecipe.requiredItem[1].stack = 20;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(86);
			Recipe.currentRecipe.requiredItem[2].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 77;
			Recipe.currentRecipe.corruption = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3266);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(173);
			Recipe.currentRecipe.requiredItem[1].stack = 20;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1329);
			Recipe.currentRecipe.requiredItem[2].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 77;
			Recipe.currentRecipe.crimson = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3267);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(173);
			Recipe.currentRecipe.requiredItem[1].stack = 20;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1329);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 77;
			Recipe.currentRecipe.crimson = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3268);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(173);
			Recipe.currentRecipe.requiredItem[1].stack = 20;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1329);
			Recipe.currentRecipe.requiredItem[2].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 77;
			Recipe.currentRecipe.crimson = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1282);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(262);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(181);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1283);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(262);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(180);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1284);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(262);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(177);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1285);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(262);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(179);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1286);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(262);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(178);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1287);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(262);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(182);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4256);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(262);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(999);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4242);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3989,
				1,
				1050,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4243);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3989,
				1,
				1015,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4244);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3989,
				1,
				2874,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4245);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3989,
				1,
				1013,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4246);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3989,
				1,
				1011,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4247);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3989,
				1,
				1010,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4248);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3989,
				1,
				1008,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4249);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3989,
				1,
				1018,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4250);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3989,
				1,
				1016,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4251);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3989,
				1,
				1007,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4252);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3989,
				1,
				1014,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4253);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3989,
				1,
				1012,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4254);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3989,
				1,
				1017,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4255);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3989,
				1,
				1009,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3306);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(150);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredTile[0] = 86;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3293);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3306);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1007);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3294);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3306);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1008);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3295);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3306);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1009);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3296);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3306);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1010);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3297);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3306);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1011);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3298);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3306);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1012);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3299);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3306);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1013);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3300);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3306);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1014);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3301);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3306);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1015);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3302);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3306);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1016);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3303);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3306);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1017);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3304);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3306);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1018);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3308);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3306);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3305);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3306);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2874);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3307);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3306);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1066);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				228
			});
			Recipe.AddRecipe();
			for (int l = 3309; l <= 3314; l++)
			{
				Recipe.currentRecipe.createItem.SetDefaults(3366);
				Recipe.currentRecipe.requiredItem[0].SetDefaults(3306);
				Recipe.currentRecipe.requiredItem[1].SetDefaults(3334);
				Recipe.currentRecipe.requiredItem[2].SetDefaults(l);
				Recipe.currentRecipe.requiredTile[0] = 114;
				Recipe.AddRecipe();
			}
			Recipe.currentRecipe.createItem.SetDefaults(259);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(68);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(252);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(259);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(253);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(259);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(978);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(803);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(981);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(979);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(804);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(981);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(980);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(805);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(981);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3365);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(129);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4075);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				22,
				4
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				16
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4064);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				9,
				12
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4065);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				9,
				12,
				225,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4859);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4858);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(313);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4860);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4858);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(314);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4861);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4858);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(317);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4862);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4858);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2358);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4863);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4858);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(315);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4864);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4858);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(316);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4865);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4858);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(316);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4866);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4858);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(318);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4867);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4858);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4852);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(27);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(181);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4851);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(27);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(180);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4853);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(27);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(177);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4854);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(27);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(179);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4855);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(27);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(178);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4856);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(27);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(182);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4857);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(27);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(999);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4869);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4868);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3364);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(129);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[2].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(33);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[2].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(360);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 50;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			for (int m = 0; m < 36; m++)
			{
				Recipe.currentRecipe.createItem.SetDefaults(2702 + m);
				Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
				Recipe.currentRecipe.requiredItem[0].stack = 50;
				Recipe.currentRecipe.requiredTile[0] = 283;
				Recipe.AddRecipe();
			}
			int[,] array2 = new int[,]
			{
				{
					444,
					-1,
					261
				},
				{
					3653,
					-1,
					2002
				},
				{
					3651,
					0,
					-1
				},
				{
					3652,
					0,
					-1
				},
				{
					3654,
					0,
					-1
				},
				{
					3655,
					0,
					-1
				},
				{
					3656,
					0,
					-1
				},
				{
					3658,
					-1,
					2003
				},
				{
					3659,
					0,
					-1
				},
				{
					3660,
					-1,
					2205
				},
				{
					3661,
					-1,
					2121
				},
				{
					3662,
					0,
					-1
				},
				{
					445,
					-1,
					2019
				},
				{
					464,
					0,
					-1
				},
				{
					3657,
					-1,
					2740
				},
				{
					4342,
					0,
					-1
				},
				{
					4360,
					-1,
					4359
				},
				{
					4397,
					-1,
					4395
				},
				{
					4466,
					0,
					-1
				},
				{
					5317,
					0,
					-1
				},
				{
					5318,
					-1,
					5311
				},
				{
					5319,
					0,
					-1
				}
			};
			array2[2, 1] = RecipeGroupID.Squirrels;
			array2[3, 1] = RecipeGroupID.Butterflies;
			array2[4, 1] = RecipeGroupID.Fireflies;
			array2[5, 1] = RecipeGroupID.Scorpions;
			array2[6, 1] = RecipeGroupID.Snails;
			array2[8, 1] = RecipeGroupID.Ducks;
			array2[11, 1] = RecipeGroupID.Bugs;
			array2[13, 1] = RecipeGroupID.Birds;
			array2[15, 1] = RecipeGroupID.Dragonflies;
			array2[18, 1] = RecipeGroupID.Turtles;
			array2[19, 1] = RecipeGroupID.Macaws;
			array2[21, 1] = RecipeGroupID.Cockatiels;
			int[,] array3 = array2;
			for (int n = 0; n < array3.GetLength(0); n++)
			{
				Recipe.currentRecipe.createItem.SetDefaults(array3[n, 0]);
				int num2 = 0;
				Recipe.currentRecipe.requiredItem[num2].SetDefaults(3);
				Recipe.currentRecipe.requiredItem[num2].stack = 50;
				int num3 = array3[n, 1];
				if (num3 != -1)
				{
					RecipeGroup recipeGroup = RecipeGroup.recipeGroups[num3];
					Recipe.currentRecipe.requiredItem[++num2].SetDefaults(recipeGroup.IconicItemId);
					Recipe.currentRecipe.requiredItem[num2].stack = 5;
					Recipe.currentRecipe.RequireGroup(num3);
				}
				int num4 = array3[n, 2];
				if (num4 != -1)
				{
					Recipe.currentRecipe.requiredItem[++num2].SetDefaults(num4);
					Recipe.currentRecipe.requiredItem[num2].stack = 5;
				}
				Recipe.currentRecipe.requiredTile[0] = 283;
				Recipe.currentRecipe.needGraveyardBiome = true;
				Recipe.AddRecipe();
			}
			Recipe.currentRecipe.createItem.SetDefaults(20);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(12);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3509);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(20);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.AddCustomShimmerResult(12, 1);
			Recipe.currentRecipe.AddCustomShimmerResult(9, 1);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3506);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(20);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.AddCustomShimmerResult(12, 1);
			Recipe.currentRecipe.AddCustomShimmerResult(9, 1);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3505);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(20);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3508);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(20);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3507);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(20);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.AddCustomShimmerResult(12, 1);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3504);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(20);
			Recipe.currentRecipe.requiredItem[0].stack = 7;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(739);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(20);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(181);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(89);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(20);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(80);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(20);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(76);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(20);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(15);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(20);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 14;
			Recipe.currentRecipe.requiredTile[1] = 15;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(106);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(20);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(703);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(699);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3503);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(703);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3500);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(703);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3499);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(703);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3502);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(703);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3501);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(703);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3498);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(703);
			Recipe.currentRecipe.requiredItem[0].stack = 7;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(740);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(703);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(180);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(687);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(703);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(688);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(703);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(689);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(703);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(707);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(703);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 14;
			Recipe.currentRecipe.requiredTile[1] = 15;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(710);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(703);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(11);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(35);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2291);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(10);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(7);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4711);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(6);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(99);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 7;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(90);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(81);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(77);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(704);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(700);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(716);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(704);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3497);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(704);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3494);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(704);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3493);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(704);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3496);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(704);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3495);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(704);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3492);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(704);
			Recipe.currentRecipe.requiredItem[0].stack = 7;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(690);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(704);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(691);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(704);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(692);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(704);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(205);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5364);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3031);
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5304);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3032);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4872);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(5303);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1140);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1139);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(704);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2172);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2194);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(348);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(336);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(358);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4127);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				182,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4731);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(358);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1570);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2841);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(206);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(345);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(85);
			Recipe.currentRecipe.createItem.stack = 15;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5328);
			Recipe.currentRecipe.createItem.stack = 1;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(85);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4422);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(21);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(14);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3515);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(21);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3512);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(21);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3511);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(21);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3514);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(21);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3513);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(21);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3510);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(21);
			Recipe.currentRecipe.requiredItem[0].stack = 7;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(741);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(21);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(177);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(91);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(21);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(82);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(21);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(78);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(21);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(16);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(21);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 14;
			Recipe.currentRecipe.requiredTile[1] = 15;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(107);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(21);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(705);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(701);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3491);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(705);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3488);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(705);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3487);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(705);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3490);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(705);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3489);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(705);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3486);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(705);
			Recipe.currentRecipe.requiredItem[0].stack = 7;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(742);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(705);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(179);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(693);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(705);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(694);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(705);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(695);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(705);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(708);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(705);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 14;
			Recipe.currentRecipe.requiredTile[1] = 15;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(711);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(705);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(19);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(13);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3521);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3518);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3517);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3520);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3519);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3516);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[0].stack = 7;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(743);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(178);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(92);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(83);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(79);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(17);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 14;
			Recipe.currentRecipe.requiredTile[1] = 15;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(264);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(178);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(108);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(105);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3117);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3114);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(349);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(706);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(702);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3485);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3482);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3481);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyWood = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3484);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3483);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3480);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[0].stack = 7;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(744);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(182);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(696);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(697);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(698);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(709);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 14;
			Recipe.currentRecipe.requiredTile[1] = 15;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(715);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(178);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(712);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(85);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(713);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3117);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3114);
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(714);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(8);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(355);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[1].stack = 30;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(355);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[1].stack = 30;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(57);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(56);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2293);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(57);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(44);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(57);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(45);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(57);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(46);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(57);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(102);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(57);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(86);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(101);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(57);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(86);
			Recipe.currentRecipe.requiredItem[1].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(100);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(57);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(86);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(103);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(57);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(86);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(104);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(57);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(86);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3279);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(57);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5107);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(57);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(180);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.corruption = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1257);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(880);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2421);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1257);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(796);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1257);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(799);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1257);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(795);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1257);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(792);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1257);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1329);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(793);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1257);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1329);
			Recipe.currentRecipe.requiredItem[1].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(794);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1257);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1329);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(798);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1257);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1329);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(797);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1257);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1329);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(801);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1257);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1329);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3280);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1257);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5107);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1257);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(180);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.crimson = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(84);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(85);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(118);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1236);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(181);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1237);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(180);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1238);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(177);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1239);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(179);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1240);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(178);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1241);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(182);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4257);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(999);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1522);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(181);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1523);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(180);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1524);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(177);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1525);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(179);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1526);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(178);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1527);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(182);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3643);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(999);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3648);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(181);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3647);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(180);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3646);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(179);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3645);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(177);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3644);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(178);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3649);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(182);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3650);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(999);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(117);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(116);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(198);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(117);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(177);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(199);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(117);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(178);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(200);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(117);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(179);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(201);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(117);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(181);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(202);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(117);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(182);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(203);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(117);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(180);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4258);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(117);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(999);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3764);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(198);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(502);
			Recipe.currentRecipe.requiredItem[1].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3765);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(199);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(502);
			Recipe.currentRecipe.requiredItem[1].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3766);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(200);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(502);
			Recipe.currentRecipe.requiredItem[1].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3767);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(201);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(502);
			Recipe.currentRecipe.requiredItem[1].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3768);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(202);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(502);
			Recipe.currentRecipe.requiredItem[1].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3769);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(203);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(502);
			Recipe.currentRecipe.requiredItem[1].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4259);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4258);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(502);
			Recipe.currentRecipe.requiredItem[1].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(204);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(117);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(127);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(117);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(197);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(98);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(117);
			Recipe.currentRecipe.requiredItem[1].stack = 20;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(75);
			Recipe.currentRecipe.requiredItem[2].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(123);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(117);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(124);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(117);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(125);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(117);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4059);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3380,
				12,
				9,
				4
			});
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				16
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3378);
			Recipe.currentRecipe.createItem.stack = 20;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3380);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3379);
			Recipe.currentRecipe.createItem.stack = 30;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3380);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3377);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3380);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(999);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3374);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3380);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3375);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3380);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3376);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3380);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(151);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 40;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(150);
			Recipe.currentRecipe.requiredItem[1].stack = 40;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(152);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 60;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(150);
			Recipe.currentRecipe.requiredItem[1].stack = 50;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(153);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 50;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(150);
			Recipe.currentRecipe.requiredItem[1].stack = 45;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5074);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 90;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(150);
			Recipe.currentRecipe.requiredItem[1].stack = 55;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(190);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(209);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(331);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(210);
			Recipe.currentRecipe.requiredItem[2].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(191);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(331);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(209);
			Recipe.currentRecipe.requiredItem[1].stack = 9;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(185);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(331);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(210);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3281);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(620);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(209);
			Recipe.currentRecipe.requiredItem[1].stack = 12;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(210);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(331);
			Recipe.currentRecipe.requiredItem[3].stack = 9;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4913);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(209);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(210);
			Recipe.currentRecipe.requiredItem[1].stack = 3;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(331);
			Recipe.currentRecipe.requiredItem[2].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(228);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(331);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(229);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(331);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(209);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(230);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(331);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(210);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2364);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2431);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2361);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2431);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2362);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2431);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2363);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2431);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5294);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2431);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(175);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(174);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(173);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 77;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(119);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(175);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(55);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(120);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(175);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(121);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(175);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(122);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(175);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(217);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(175);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(219);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(175);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(164);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2365);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(175);
			Recipe.currentRecipe.requiredItem[0].stack = 17;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(231);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(175);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(232);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(175);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(233);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(175);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4821);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(175);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1991);
			Recipe.currentRecipe.requiredTile[0] = 77;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5129);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3484,
				1,
				5132,
				5
			});
			Recipe.currentRecipe.needHoney = true;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5129);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3520,
				1,
				5132,
				5
			});
			Recipe.currentRecipe.needHoney = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(273);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(46);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(155);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(190);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(121);
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.currentRecipe.corruption = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(273);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(795);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(155);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(190);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(121);
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.currentRecipe.crimson = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(675);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(273);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(547);
			Recipe.currentRecipe.requiredItem[1].stack = 20;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(548);
			Recipe.currentRecipe.requiredItem[2].stack = 20;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(549);
			Recipe.currentRecipe.requiredItem[3].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(674);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(368);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[1].stack = 24;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(757);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(675);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(674);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1570);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4956);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(757);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3063);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3065);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(2880);
			Recipe.currentRecipe.requiredItem[4].SetDefaults(1826);
			Recipe.currentRecipe.requiredItem[5].SetDefaults(3018);
			Recipe.currentRecipe.requiredItem[6].SetDefaults(65);
			Recipe.currentRecipe.requiredItem[7].SetDefaults(1123);
			Recipe.currentRecipe.requiredItem[8].SetDefaults(989);
			Recipe.currentRecipe.requiredItem[9].SetDefaults(3507);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(389);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(527);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(528);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(521);
			Recipe.currentRecipe.requiredItem[2].stack = 7;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(520);
			Recipe.currentRecipe.requiredItem[3].stack = 7;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(381);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(364);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(372);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(381);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(373);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(381);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(371);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(381);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(374);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(381);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(375);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(381);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(385);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(381);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(776);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(381);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(383);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(381);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(991);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(381);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(435);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(381);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(483);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(381);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(537);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(381);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1184);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1104);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1205);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1184);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1206);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1184);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1207);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1184);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1208);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1184);
			Recipe.currentRecipe.requiredItem[0].stack = 24;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1209);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1184);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1189);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1184);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1188);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1184);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1190);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1184);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1222);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1184);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1187);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1184);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1185);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1184);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1186);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1184);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(382);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(365);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(377);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(382);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(378);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(382);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(376);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(382);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(379);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(382);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(380);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(382);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(386);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(382);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(777);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(382);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(384);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(382);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(992);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(382);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(436);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(382);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(484);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(382);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(390);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(382);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(525);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(382);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1191);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1105);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1210);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1191);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1211);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1191);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1212);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1191);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1213);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1191);
			Recipe.currentRecipe.requiredItem[0].stack = 24;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1214);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1191);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1196);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1191);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1195);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1191);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1197);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1191);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1223);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1191);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1194);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1191);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1192);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1191);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1193);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1191);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1220);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1191);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(366);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 133;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(401);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(402);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(400);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(403);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].stack = 24;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(404);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(388);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(778);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(387);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(993);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(481);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(482);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(406);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(524);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(366);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(221);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1106);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 133;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1215);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].stack = 13;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1216);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].stack = 13;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1217);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].stack = 13;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1218);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].stack = 26;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1219);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1203);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1202);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1204);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].stack = 13;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1224);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].stack = 13;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1201);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].stack = 13;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1199);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].stack = 13;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1200);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].stack = 13;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1221);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1106);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(221);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2551);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2607);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2366);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2607);
			Recipe.currentRecipe.requiredItem[0].stack = 24;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2370);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2607);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2371);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2607);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2372);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2607);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(684);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2161);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(685);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2161);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(686);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2161);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(684);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2161);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(685);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2161);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(686);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2161);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4911);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(520);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(521);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(2161);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3788);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(534);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(527);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(521);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3787);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(113);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(528);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(520);
			Recipe.currentRecipe.requiredItem[2].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3779);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3795);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3783);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(521);
			Recipe.currentRecipe.requiredItem[2].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3776);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3783);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3777);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3783);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3778);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(391);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3783);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3776);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3783);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3777);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3783);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3778);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1198);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3783);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(559);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(553);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(558);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4873);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(551);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 24;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(552);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4896);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4897);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4898);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4899);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4900);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 24;
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4901);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(579);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(547);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(548);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(549);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(990);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(547);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(548);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(549);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(578);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(368);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(550);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4790);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4678);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4060);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(197);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(947);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 133;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1002);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1001);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1003);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1004);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 24;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1005);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1231);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1230);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1232);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1233);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1262);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1234);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1229);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1227);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1226);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1228);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2188);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1308);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[1].stack = 14;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5296);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(997);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1316);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1328);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1317);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 24;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1328);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1318);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1328);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2199);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2218);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1316);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2200);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2218);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1317);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2201);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2218);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1317);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2202);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2218);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1318);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1552);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(183);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 247;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1546);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1552);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1547);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1552);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1548);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1552);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1549);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1552);
			Recipe.currentRecipe.requiredItem[0].stack = 24;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1550);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1552);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2176);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1552);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3261);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1508);
			Recipe.currentRecipe.requiredTile[0] = 133;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2189);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3261);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1503);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3261);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1504);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3261);
			Recipe.currentRecipe.requiredItem[0].stack = 24;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1505);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3261);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1506);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3261);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1507);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3261);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1543);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3261);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1544);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3261);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1545);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3261);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3460);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3567);
			Recipe.currentRecipe.createItem.stack = 333;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3467);
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3568);
			Recipe.currentRecipe.createItem.stack = 333;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3467);
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4318);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3460);
			Recipe.currentRecipe.requiredItem[0].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3572);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3457);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3459);
			Recipe.currentRecipe.requiredItem[2].stack = 6;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(3456);
			Recipe.currentRecipe.requiredItem[3].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3456);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3457);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3459);
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3539);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2763);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2764);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2765);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2786);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2784);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3522);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3473);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3543);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3456);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3457);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3459);
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3536);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3456);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2757);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3456);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2758);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3456);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2759);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3456);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2776);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3456);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2774);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3456);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3523);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3456);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3475);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3456);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3540);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3456);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3457);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3456);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3459);
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3537);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3457);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2760);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3457);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2761);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3457);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2762);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3457);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2781);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3457);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2779);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3457);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3524);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3457);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3476);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3457);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3542);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3457);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3459);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3456);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3457);
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3538);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3459);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3381);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3459);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3382);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3459);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3383);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3459);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3466);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3459);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3464);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3459);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3525);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3459);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3474);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3459);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3531);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3459);
			Recipe.currentRecipe.requiredItem[0].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(533);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(98);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(324);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(319);
			Recipe.currentRecipe.requiredItem[2].stack = 5;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(548);
			Recipe.currentRecipe.requiredItem[3].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(561);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(520);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(548);
			Recipe.currentRecipe.requiredItem[2].stack = 25;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(506);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(324);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(547);
			Recipe.currentRecipe.requiredItem[2].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2535);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(236);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(38);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[2].stack = 12;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(549);
			Recipe.currentRecipe.requiredItem[3].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(494);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(508);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(502);
			Recipe.currentRecipe.requiredItem[1].stack = 20;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(521);
			Recipe.currentRecipe.requiredItem[2].stack = 8;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(549);
			Recipe.currentRecipe.requiredItem[3].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(425);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(507);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(501);
			Recipe.currentRecipe.requiredItem[1].stack = 25;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(520);
			Recipe.currentRecipe.requiredItem[2].stack = 8;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(549);
			Recipe.currentRecipe.requiredItem[3].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2343);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5125);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2343,
				1,
				215,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				16
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5288);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5125,
				1,
				4731,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				16
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4468);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2343);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2218);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4451);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2343);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1522);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4452);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2343);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1523);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4453);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2343);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1524);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4454);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2343);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1525);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4455);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2343);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1526);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4456);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2343);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1527);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4467);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2343);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3643);
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4745);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(68);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.currentRecipe.corruption = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4745);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(9);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1330);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.currentRecipe.crimson = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5289);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3354);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3355);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3356);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2768);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[0].stack = 40;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1006);
			Recipe.currentRecipe.requiredItem[1].stack = 40;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1552);
			Recipe.currentRecipe.requiredItem[2].stack = 40;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(3261);
			Recipe.currentRecipe.requiredItem[3].stack = 40;
			Recipe.currentRecipe.requiredItem[4].SetDefaults(175);
			Recipe.currentRecipe.requiredItem[4].stack = 40;
			Recipe.currentRecipe.requiredItem[5].SetDefaults(117);
			Recipe.currentRecipe.requiredItem[5].stack = 40;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5131);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4797);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4960);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(495);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(502);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(526);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(501);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(520);
			Recipe.currentRecipe.requiredItem[3].stack = 8;
			Recipe.currentRecipe.requiredItem[4].SetDefaults(549);
			Recipe.currentRecipe.requiredItem[4].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(493);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(320);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(575);
			Recipe.currentRecipe.requiredItem[1].stack = 20;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(520);
			Recipe.currentRecipe.requiredItem[2].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(492);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(320);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(575);
			Recipe.currentRecipe.requiredItem[1].stack = 20;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(521);
			Recipe.currentRecipe.requiredItem[2].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(761);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(575);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(501);
			Recipe.currentRecipe.requiredItem[1].stack = 99;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(785);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(575);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1516);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(749);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(575);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1611);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(786);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(575);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1517);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(821);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(575);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1518);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(822);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(575);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1519);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1165);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(575);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1520);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1515);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(575);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1521);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1797);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(575);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1811);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1830);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(575);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1831);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(823);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(575);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3261);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2280);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(575);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2218);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1866);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(575);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1552);
			Recipe.currentRecipe.requiredItem[1].stack = 18;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3468);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3469);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3456);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3470);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3457);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3471);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3459);
			Recipe.currentRecipe.requiredItem[0].stack = 14;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(250);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(261);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(126);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4398);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4373);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(126);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2439);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2436);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(126);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2440);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2437);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(126);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2441);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2438);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(126);
			Recipe.AddRecipe();
			for (int num5 = 0; num5 < 8; num5++)
			{
				Recipe.currentRecipe.createItem.SetDefaults(2178 + num5);
				Recipe.currentRecipe.requiredItem[0].SetDefaults(1994 + num5);
				Recipe.currentRecipe.requiredItem[0].stack = 1;
				Recipe.currentRecipe.requiredItem[1].SetDefaults(31);
				Recipe.AddRecipe();
			}
			for (int num6 = 4327; num6 <= 4332; num6++)
			{
				Recipe.currentRecipe.createItem.SetDefaults(num6);
				Recipe.currentRecipe.requiredItem[0].SetDefaults(num6 - 4327 + 4334);
				Recipe.currentRecipe.requiredItem[0].stack = 1;
				Recipe.currentRecipe.requiredItem[1].SetDefaults(31);
				Recipe.AddRecipe();
			}
			Recipe.currentRecipe.createItem.SetDefaults(5133);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(5132);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4846);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(31);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4845);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4964);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(31);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4961);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4655);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(31);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4068);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4656);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(31);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4069);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4657);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(31);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4070);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 16;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2162);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2019);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2163);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2018);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3565);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3563);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2206);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2205);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2165);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2123);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2164);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2122);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2166);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2015);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2167);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2016);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2168);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2017);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5213);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(5212);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5301);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(5300);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5314);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(5311);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5315);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(5312);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5316);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(5313);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2190);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2121);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2174);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2006);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2175);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2007);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2186);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2157);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2187);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2156);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2191);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2003);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4376);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4375);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2207);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2002);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4364);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4363);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2741);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2740);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4380);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4361);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4461);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4464);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4462);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4465);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4473);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4374);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4474);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4359);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4475);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4418);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4481);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4480);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4396);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4395);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4850);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4849);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4963);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2673);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4882);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4838);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4883);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4839);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4884);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4840);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4885);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4841);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4886);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4842);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4887);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4843);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4888);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4844);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4889);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4831);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4890);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4832);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4891);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4833);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4892);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4834);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4893);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4835);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4894);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4836);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4895);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4837);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4483);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4482);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4476);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4419);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4275);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4274);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(126);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3072);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2891);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(31);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4333);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4340);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(31);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3070);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2889);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3071);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2890);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3566);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3564);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3073);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2892);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3074);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2893);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3075);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2894);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3076);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2895);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4399);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4362);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3254);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3191);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3255);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3194);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3256);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3192);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3257);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2208);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3193);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1085);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1073);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1086);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1074);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1087);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1075);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1088);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1076);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1089);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1077);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1090);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1078);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1091);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1079);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1092);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1080);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1093);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1081);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1094);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1082);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1095);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1083);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1096);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1084);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1007);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1115);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1008);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1114);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1009);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1110);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1010);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1112);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1011);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1108);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1012);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1107);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1013);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1116);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1014);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1109);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1015);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1111);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1016);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1118);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1017);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1117);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1018);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1113);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1050);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1119);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1031);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1007);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1008);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1009);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1033);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1009);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1010);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1011);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1035);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1013);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1014);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1015);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1063);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1031);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1064);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1033);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1065);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1035);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1032);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1031);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1034);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1033);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1036);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1035);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3550);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1031);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3551);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1033);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3552);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1035);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1068);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1008);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1009);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1010);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1069);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1012);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1013);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1014);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1070);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1016);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1017);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1018);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1066);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1068);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1069);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1070);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1067);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1066);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1019);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1007);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1020);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1008);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1021);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1009);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1022);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1010);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1023);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1011);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1024);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1012);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1025);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1013);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1026);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1014);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1027);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1015);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1028);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1016);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1029);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1017);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1030);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1018);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2875);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2874);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1038);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1007);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1039);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1008);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1040);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1009);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1041);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1010);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1042);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1011);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1043);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1012);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1044);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1013);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1045);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1014);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1046);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1015);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1047);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1016);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1048);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1017);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1049);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1018);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2876);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2874);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1051);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1007);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1052);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1008);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1053);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1009);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1054);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1010);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1055);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1011);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1056);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1012);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1057);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1013);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1058);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1014);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1059);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1015);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1060);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1016);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1061);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1017);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1062);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1018);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2877);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2874);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1037);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3559);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1037);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3557);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1037);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1050);
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3558);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1037);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3562);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3561);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3111);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3535);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3533);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(502);
			Recipe.currentRecipe.requiredItem[1].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3526);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3528);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3456);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3527);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3457);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3529);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3459);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3530);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(126);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3467);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 228;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2750);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(117);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(501);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(520);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(394);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(187);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(268);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1860);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(394);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1303);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1861);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1860);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(950);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(395);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(17);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(18);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(393);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(395);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(709);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(18);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(393);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3036);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3120);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3037);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3096);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3121);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3102);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3099);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3119);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3122);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3084);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3118);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3095);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3123);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(395);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3036);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3121);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(3122);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(50);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(19);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(182);
			Recipe.currentRecipe.requiredItem[2].stack = 3;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				17
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(50);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(170);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(706);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(182);
			Recipe.currentRecipe.requiredItem[2].stack = 3;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				17
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3124);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3123);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(50);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3124);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3123);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3199);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5437);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3124);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4263);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(4819);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4000);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				555,
				1,
				2219,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4001);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				555,
				1,
				532,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3996);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2423,
				1,
				976,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3994);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2423,
				1,
				187,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3995);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3994,
				1,
				976,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3995);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3996,
				1,
				187,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3997);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				938,
				1,
				1253,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3998);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				938,
				1,
				3016,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4008);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				88,
				1,
				3109,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3992);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				897,
				1,
				3016,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4006);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				1321,
				1,
				3015,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4005);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				1858,
				1,
				3015,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3991);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				555,
				1,
				3015,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4007);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3212,
				1,
				1132,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3990);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3200,
				1,
				2423,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4002);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				1321,
				1,
				1322,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3993);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				405,
				1,
				3017,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(396);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(158);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(193);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(397);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(156);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(193);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1613);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(397);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1612);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1724);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(53);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(215);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(399);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(53);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(159);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1163);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(987);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(159);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(983);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(857);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(159);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1863);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1724);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(159);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3241);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3201);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3225);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1250);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(399);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(158);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1251);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1163);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(158);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1252);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(983);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(158);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3250);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1863);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(158);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3251);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1249);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(158);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3252);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3241);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(158);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1164);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(399);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1163);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(983);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5331);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1164);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(158);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5331);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(399);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1163);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(983);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(158);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5331);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1250);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1163);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(983);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.BlizzardBalloons);
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.SandstormBalloons);
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5331);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1251);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(399);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(983);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.CloudBalloons);
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.SandstormBalloons);
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5331);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1252);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(399);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1163);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.CloudBalloons);
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.BlizzardBalloons);
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1249);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(159);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1132);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(857);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(53);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3783);
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(987);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(53);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2161);
			Recipe.currentRecipe.requiredTile[0] = 125;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(405);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(54);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(128);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(405);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1579);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(128);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(405);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3200);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(128);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(405);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4055);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(128);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(898);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(405);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(212);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(285);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1862);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(898);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(950);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5000);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1862);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(908);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(193);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(173);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3999);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				193,
				1,
				906,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4004);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				193,
				1,
				1323,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4003);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4004,
				1,
				906,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4003);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3999,
				1,
				1323,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4003);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3999,
				1,
				4004,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4038);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				906,
				1,
				193,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(907);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(863);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(193);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(908);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(907);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(906);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1323);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(908);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4038,
				1,
				863,
				1,
				1323,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(908);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4038,
				1,
				907,
				1,
				1323,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(908);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4003,
				1,
				907,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(908);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4003,
				1,
				863,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4874);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4822,
				1,
				405,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(555);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(223);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(189);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3034);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3033);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(855);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3035);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3034);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(854);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(897);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(536);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(211);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(936);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(897);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(935);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1343);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1322);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(936);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1864);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1845);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1167);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(982);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(111);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(49);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1595);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(982);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(216);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2221);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2219);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1595);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2220);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2219);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(935);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(860);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(49);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(535);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1865);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(899);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(900);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(861);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(485);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(497);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3110);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1865);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(861);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(862);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(532);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(554);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1247);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1132);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(532);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1578);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1132);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1290);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(976);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(953);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(975);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(984);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(976);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(977);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(963);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3061);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2214);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2215);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(2216);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(2217);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5126);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3061);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4056);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(5010);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(4341);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3721);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2373);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2375);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(2374);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5064);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3721);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4881);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5140);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5139);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(75);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5144);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5140);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4389);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5142);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5140);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4377);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5141);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5140);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4354);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5146);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5140);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(5128);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5145);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5140);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(5127);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5143);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5140);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4378);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(901);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(886);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(892);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(902);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(887);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(885);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(903);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(889);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(893);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(904);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(890);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(891);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5354);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(888);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3781);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1612);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(901);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(902);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(903);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(904);
			Recipe.currentRecipe.requiredItem[4].SetDefaults(5354);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(935);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(490);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(548);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(549);
			Recipe.currentRecipe.requiredItem[2].stack = 5;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(547);
			Recipe.currentRecipe.requiredItem[3].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(935);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(491);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(548);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(549);
			Recipe.currentRecipe.requiredItem[2].stack = 5;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(547);
			Recipe.currentRecipe.requiredItem[3].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(935);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(489);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(548);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(549);
			Recipe.currentRecipe.requiredItem[2].stack = 5;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(547);
			Recipe.currentRecipe.requiredItem[3].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(935);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2998);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(548);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(549);
			Recipe.currentRecipe.requiredItem[2].stack = 5;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(547);
			Recipe.currentRecipe.requiredItem[3].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1301);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(935);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1248);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1858);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1300);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1301);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(518);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(531);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(502);
			Recipe.currentRecipe.requiredItem[1].stack = 20;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(520);
			Recipe.currentRecipe.requiredItem[2].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 101;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(519);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(531);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(522);
			Recipe.currentRecipe.requiredItem[1].stack = 20;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(521);
			Recipe.currentRecipe.requiredItem[2].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 101;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1336);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(531);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1332);
			Recipe.currentRecipe.requiredItem[1].stack = 20;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(521);
			Recipe.currentRecipe.requiredItem[2].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 101;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(37);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(38);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.requiredTile[1] = 15;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(266);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(324);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(323);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(180);
			Recipe.currentRecipe.requiredItem[2].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 17;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(237);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(236);
			Recipe.currentRecipe.requiredItem[0].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.currentRecipe.requiredTile[1] = 15;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(109);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(75);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3625);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(509);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(851);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(850);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(3612);
			Recipe.currentRecipe.requiredItem[4].SetDefaults(510);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3611);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3625);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3619);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(2799);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[3].stack = 60;
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3620);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(849);
			Recipe.currentRecipe.requiredItem[0].stack = 50;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 10;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(511);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(530);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(512);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(26);
			Recipe.currentRecipe.requiredItem[0].stack = 4;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(530);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3617);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(171);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 4;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[2].stack = 4;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(581);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(582);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[0].stack = 10;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(583);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(17);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(583);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(709);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(584);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(16);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(584);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(708);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(585);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(15);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(585);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(707);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 16;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3632);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(542);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.currentRecipe.anyPressurePlate = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3630);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(542);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.currentRecipe.anyPressurePlate = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3626);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(542);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.currentRecipe.anyPressurePlate = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3631);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(542);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.currentRecipe.anyPressurePlate = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3613);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(520);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3614);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(521);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3615);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(549);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3726);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3182);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3727);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3184);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3728);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3185);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3729);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1344);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3182);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3184);
			Recipe.currentRecipe.requiredItem[2].stack = 1;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(3185);
			Recipe.currentRecipe.requiredItem[3].stack = 1;
			Recipe.currentRecipe.requiredItem[4].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[4].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5135);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(539);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(2607);
			Recipe.currentRecipe.requiredItem[1].stack = 2;
			Recipe.currentRecipe.requiredTile[0] = 18;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(580);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(167);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(530);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5327);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(167);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(343);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(540);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5383);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				540,
				1,
				3111,
				5
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				283
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5384);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				540,
				1,
				29,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				283
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4390);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(276);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5066);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1124);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5067);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3271);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(323);
			Recipe.currentRecipe.requiredItem[1].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5286);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				29,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				283
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5287);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				109,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				283
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5320);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				28,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				283
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5321);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				110,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				283
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5345);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4392);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(530);
			Recipe.currentRecipe.requiredItem[1].stack = 6;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(129);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3393);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3391,
				1,
				3392,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				86
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3391);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3392,
				1,
				3393,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				86
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3392);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3391,
				1,
				3393,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				86
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4391);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(664);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1290);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				111,
				1,
				29,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(111);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				1290,
				1,
				109,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				114
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2193);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4142,
				1,
				521,
				10
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4142);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				2193,
				1,
				521,
				10
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4355);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3);
			Recipe.currentRecipe.requiredItem[0].stack = 50;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(540);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4640);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(181);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4641);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(180);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4642);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(177);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4643);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(179);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4644);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(178);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4645);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(182);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4646);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(999);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3);
			Recipe.currentRecipe.requiredTile[0] = 283;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(565);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(562);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(563);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(564);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(566);
			Recipe.currentRecipe.requiredItem[4].SetDefaults(567);
			Recipe.currentRecipe.requiredItem[5].SetDefaults(568);
			Recipe.currentRecipe.requiredItem[6].SetDefaults(569);
			Recipe.currentRecipe.requiredItem[7].SetDefaults(570);
			Recipe.currentRecipe.requiredItem[8].SetDefaults(571);
			Recipe.currentRecipe.requiredItem[9].SetDefaults(572);
			Recipe.currentRecipe.requiredItem[10].SetDefaults(573);
			Recipe.currentRecipe.requiredItem[11].SetDefaults(574);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4356);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(4078);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(4080);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(4081);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(4082);
			Recipe.currentRecipe.requiredItem[4].SetDefaults(4357);
			Recipe.currentRecipe.requiredItem[5].SetDefaults(4358);
			Recipe.currentRecipe.requiredItem[6].SetDefaults(4421);
			Recipe.currentRecipe.requiredItem[7].SetDefaults(4606);
			Recipe.currentRecipe.requiredItem[8].SetDefaults(5006);
			Recipe.currentRecipe.requiredItem[9].SetDefaults(4979);
			Recipe.currentRecipe.requiredItem[10].SetDefaults(4985);
			Recipe.currentRecipe.requiredItem[11].SetDefaults(4990);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4992);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1603);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1602);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(4079);
			Recipe.currentRecipe.requiredItem[3].SetDefaults(4077);
			Recipe.currentRecipe.requiredItem[4].SetDefaults(1607);
			Recipe.currentRecipe.requiredItem[5].SetDefaults(4991);
			Recipe.currentRecipe.requiredTile[0] = 114;
			Recipe.currentRecipe.needGraveyardBiome = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4237);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				562,
				1,
				2860,
				25
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(43);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(38);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4131);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(331);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(86);
			Recipe.currentRecipe.requiredItem[2].stack = 30;
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.currentRecipe.corruption = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4131);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(331);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1329);
			Recipe.currentRecipe.requiredItem[2].stack = 30;
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.currentRecipe.crimson = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4076);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(331);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1329);
			Recipe.currentRecipe.requiredItem[2].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.currentRecipe.crimson = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4076);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 15;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(331);
			Recipe.currentRecipe.requiredItem[1].stack = 8;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(86);
			Recipe.currentRecipe.requiredItem[2].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.currentRecipe.corruption = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(70);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(67);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(68);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1331);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2886);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1330);
			Recipe.currentRecipe.requiredItem[1].stack = 15;
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1133);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1125);
			Recipe.currentRecipe.requiredItem[0].stack = 5;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(209);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1124);
			Recipe.currentRecipe.requiredItem[2].stack = 5;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(1134);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(560);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(23);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(264);
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(560);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(23);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(715);
			Recipe.currentRecipe.requiredTile[0] = 26;
			Recipe.currentRecipe.notDecraftable = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(544);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(38);
			Recipe.currentRecipe.requiredItem[0].stack = 3;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(520);
			Recipe.currentRecipe.requiredItem[2].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(556);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(68);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(521);
			Recipe.currentRecipe.requiredItem[2].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(556);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1330);
			Recipe.currentRecipe.requiredItem[0].stack = 6;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(521);
			Recipe.currentRecipe.requiredItem[2].stack = 6;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(557);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(154);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(22);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(520);
			Recipe.currentRecipe.requiredItem[2].stack = 3;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(521);
			Recipe.currentRecipe.requiredItem[3].stack = 3;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5334);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(544);
			Recipe.currentRecipe.requiredItem[1].SetDefaults(557);
			Recipe.currentRecipe.requiredItem[2].SetDefaults(556);
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.currentRecipe.needEverythingSeed = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1844);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(1725);
			Recipe.currentRecipe.requiredItem[0].stack = 30;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1508);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(1225);
			Recipe.currentRecipe.requiredItem[2].stack = 10;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(1958);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(225);
			Recipe.currentRecipe.requiredItem[0].stack = 20;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(1508);
			Recipe.currentRecipe.requiredItem[1].stack = 5;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(547);
			Recipe.currentRecipe.requiredItem[2].stack = 5;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(2767);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(2766);
			Recipe.currentRecipe.requiredItem[0].stack = 8;
			Recipe.currentRecipe.requiredTile[0] = 134;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3601);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(3458);
			Recipe.currentRecipe.requiredItem[0].stack = 12;
			Recipe.currentRecipe.requiredItem[1].SetDefaults(3456);
			Recipe.currentRecipe.requiredItem[1].stack = 12;
			Recipe.currentRecipe.requiredItem[2].SetDefaults(3457);
			Recipe.currentRecipe.requiredItem[2].stack = 12;
			Recipe.currentRecipe.requiredItem[3].SetDefaults(3459);
			Recipe.currentRecipe.requiredItem[3].stack = 12;
			Recipe.currentRecipe.requiredTile[0] = 412;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5104);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5105);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5104);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5106);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5105);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(5106);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(71);
			Recipe.currentRecipe.createItem.stack = 100;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(72);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(72);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(71);
			Recipe.currentRecipe.requiredItem[0].stack = 100;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(72);
			Recipe.currentRecipe.createItem.stack = 100;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(73);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(73);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(72);
			Recipe.currentRecipe.requiredItem[0].stack = 100;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(73);
			Recipe.currentRecipe.createItem.stack = 100;
			Recipe.currentRecipe.requiredItem[0].SetDefaults(74);
			Recipe.currentRecipe.requiredItem[0].stack = 1;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(74);
			Recipe.currentRecipe.requiredItem[0].SetDefaults(73);
			Recipe.currentRecipe.requiredItem[0].stack = 100;
			Recipe.AddRecipe();
			Recipe.CreateReverseWallRecipes();
			Recipe.CreateReversePlatformRecipes();
			Recipe.UpdateWhichItemsAreMaterials();
			Recipe.UpdateWhichItemsAreCrafted();
			Recipe.UpdateMaterialFieldForAllRecipes();
			Recipe.ReplaceItemUseFlagsWithRecipeGroups();
			Recipe.CreateRequiredItemQuickLookups();
			ShimmerTransforms.UpdateRecipeSets();
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x001460D8 File Offset: 0x001442D8
		private static void ReplaceItemUseFlagsWithRecipeGroups()
		{
			for (int i = 0; i < Recipe.numRecipes; i++)
			{
				Recipe recipe = Main.recipe[i];
				recipe.ReplaceItemUseFlagWithGroup(ref recipe.anyWood, RecipeGroupID.Wood);
				recipe.ReplaceItemUseFlagWithGroup(ref recipe.anySand, RecipeGroupID.Sand);
				recipe.ReplaceItemUseFlagWithGroup(ref recipe.anyPressurePlate, RecipeGroupID.PressurePlate);
				recipe.ReplaceItemUseFlagWithGroup(ref recipe.anyIronBar, RecipeGroupID.IronBar);
				recipe.ReplaceItemUseFlagWithGroup(ref recipe.anyFragment, RecipeGroupID.Fragment);
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00146150 File Offset: 0x00144350
		private void ReplaceItemUseFlagWithGroup(ref bool flag, int groupId)
		{
			if (flag)
			{
				this.RequireGroup(groupId);
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00146160 File Offset: 0x00144360
		private static void CreateRequiredItemQuickLookups()
		{
			for (int i = 0; i < Recipe.numRecipes; i++)
			{
				Recipe recipe = Main.recipe[i];
				for (int j = 0; j < Recipe.maxRequirements; j++)
				{
					Item item = recipe.requiredItem[j];
					if (item.IsAir)
					{
						break;
					}
					Recipe.RequiredItemEntry requiredItemEntry = new Recipe.RequiredItemEntry
					{
						itemIdOrRecipeGroup = item.type,
						stack = item.stack
					};
					foreach (int num in recipe.acceptedGroups)
					{
						if (num < 0)
						{
							break;
						}
						RecipeGroup recipeGroup = RecipeGroup.recipeGroups[num];
						if (recipeGroup.ValidItems.Contains(item.type))
						{
							requiredItemEntry.itemIdOrRecipeGroup = recipeGroup.GetGroupFakeItemId();
						}
					}
					recipe.requiredItemQuickLookup[j] = requiredItemEntry;
				}
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00146240 File Offset: 0x00144440
		private static void UpdateMaterialFieldForAllRecipes()
		{
			for (int i = 0; i < Recipe.numRecipes; i++)
			{
				int num = 0;
				while (Main.recipe[i].requiredItem[num].type > 0)
				{
					Main.recipe[i].requiredItem[num].material = ItemID.Sets.IsAMaterial[Main.recipe[i].requiredItem[num].type];
					num++;
				}
				Main.recipe[i].createItem.material = ItemID.Sets.IsAMaterial[Main.recipe[i].createItem.type];
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x001462D0 File Offset: 0x001444D0
		public static void UpdateWhichItemsAreMaterials()
		{
			for (int i = 0; i < (int)ItemID.Count; i++)
			{
				Item item = new Item();
				item.SetDefaults(i, true, null);
				item.checkMat();
				ItemID.Sets.IsAMaterial[i] = item.material;
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00146310 File Offset: 0x00144510
		public static void UpdateWhichItemsAreCrafted()
		{
			for (int i = 0; i < Recipe.numRecipes; i++)
			{
				if (!Main.recipe[i].notDecraftable)
				{
					ItemID.Sets.IsCrafted[Main.recipe[i].createItem.type] = i;
				}
				if (Main.recipe[i].crimson)
				{
					ItemID.Sets.IsCraftedCrimson[Main.recipe[i].createItem.type] = i;
				}
				if (Main.recipe[i].corruption)
				{
					ItemID.Sets.IsCraftedCorruption[Main.recipe[i].createItem.type] = i;
				}
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x001463A0 File Offset: 0x001445A0
		private static void AddSolarFurniture()
		{
			Recipe.currentRecipe.createItem.SetDefaults(4229);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3,
				10,
				3458,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4233);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4145);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				14
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4146);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				15,
				225,
				5
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4147);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				20,
				149,
				10
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4148);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				16
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4149);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				5,
				8,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4150);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				4,
				8,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4151);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				4
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4152);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				4,
				8,
				4,
				85,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4153);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				8,
				22,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4154);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				10,
				22,
				3,
				170,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4155);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4156);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				8,
				1,
				4229,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4157);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				6,
				8,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4158);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				15,
				154,
				4,
				149,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4160);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				6,
				206,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4161);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				5,
				225,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4162);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				8
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4163);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				10
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4165);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4229,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00146A28 File Offset: 0x00144C28
		private static void AddVortexFurniture()
		{
			Recipe.currentRecipe.createItem.SetDefaults(4230);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3,
				10,
				3456,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4234);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4166);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				14
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4167);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				15,
				225,
				5
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4168);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				20,
				149,
				10
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4169);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				16
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4170);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				5,
				8,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4171);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				4,
				8,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4172);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				4
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4173);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				4,
				8,
				4,
				85,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4174);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				8,
				22,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4175);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				10,
				22,
				3,
				170,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4176);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4177);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				8,
				1,
				4230,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4178);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				6,
				8,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4179);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				15,
				154,
				4,
				149,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4181);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				6,
				206,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4182);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				5,
				225,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4183);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				8
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4184);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				10
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4186);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4230,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x001470B0 File Offset: 0x001452B0
		private static void AddNebulaFurniture()
		{
			Recipe.currentRecipe.createItem.SetDefaults(4231);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3,
				10,
				3457,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4235);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4187);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				14
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4188);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				15,
				225,
				5
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4189);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				20,
				149,
				10
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4190);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				16
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4191);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				5,
				8,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4192);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				4,
				8,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4193);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				4
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4194);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				4,
				8,
				4,
				85,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4195);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				8,
				22,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4196);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				10,
				22,
				3,
				170,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4197);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4198);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				8,
				1,
				4231,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4199);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				6,
				8,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4200);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				15,
				154,
				4,
				149,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4202);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				6,
				206,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4203);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				5,
				225,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4204);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				8
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4205);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				10
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4207);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4231,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00147738 File Offset: 0x00145938
		private static void AddStardustFurniture()
		{
			Recipe.currentRecipe.createItem.SetDefaults(4232);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3,
				10,
				3459,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4236);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4208);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				14
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4209);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				15,
				225,
				5
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4210);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				20,
				149,
				10
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4211);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				16
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4212);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				5,
				8,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4213);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				4,
				8,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4214);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				4
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4215);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				4,
				8,
				4,
				85,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4216);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				8,
				22,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4217);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				10,
				22,
				3,
				170,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4218);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4219);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				8,
				1,
				4232,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4220);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				6,
				8,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4221);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				15,
				154,
				4,
				149,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4223);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				6,
				206,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4224);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				5,
				225,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4225);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				8
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4226);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				10
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4228);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4232,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				412
			});
			Recipe.AddRecipe();
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00147DC0 File Offset: 0x00145FC0
		private static void AddSpiderFurniture()
		{
			Recipe.currentRecipe.createItem.SetDefaults(4139);
			Recipe.currentRecipe.createItem.stack = 10;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				150,
				10,
				2607,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4140);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3931);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				14
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3932);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				15,
				225,
				5
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3933);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				20,
				149,
				10
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3934);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				16
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3935);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				5,
				8,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3936);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				4,
				8,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3937);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				4
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3938);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				4,
				8,
				4,
				85,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				16
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3939);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				8,
				22,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3940);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				10,
				22,
				3,
				170,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3941);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3942);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				8,
				1,
				4139,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3943);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				6,
				8,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3944);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				15,
				154,
				4,
				149,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3946);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				6,
				206,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3947);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				5,
				225,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3948);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				8
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3949);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				10
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4125);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4139,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x001483F8 File Offset: 0x001465F8
		private static void AddLesionFurniture()
		{
			int num = 3955;
			Recipe.currentRecipe.createItem.SetDefaults(3955);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				61,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				218
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3975);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				num,
				10
			});
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3956);
			Recipe.currentRecipe.createItem.stack = 4;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3955,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3967);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				num,
				6
			});
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3963);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				num,
				4
			});
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3965);
			Recipe recipe = Recipe.currentRecipe;
			int[] array = new int[]
			{
				0,
				8,
				22,
				2
			};
			array[0] = num;
			recipe.SetIngredients(array);
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3974);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				num,
				8
			});
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3972);
			Recipe recipe2 = Recipe.currentRecipe;
			int[] array2 = new int[]
			{
				0,
				6,
				206,
				1
			};
			array2[0] = num;
			recipe2.SetIngredients(array2);
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3970);
			Recipe recipe3 = Recipe.currentRecipe;
			int[] array3 = new int[]
			{
				0,
				6,
				8,
				1
			};
			array3[0] = num;
			recipe3.SetIngredients(array3);
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3962);
			Recipe recipe4 = Recipe.currentRecipe;
			int[] array4 = new int[]
			{
				0,
				4,
				8,
				1
			};
			array4[0] = num;
			recipe4.SetIngredients(array4);
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3969);
			Recipe recipe5 = Recipe.currentRecipe;
			int[] array5 = new int[]
			{
				0,
				3,
				8,
				1
			};
			array5[0] = num;
			recipe5.SetIngredients(array5);
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3961);
			Recipe recipe6 = Recipe.currentRecipe;
			int[] array6 = new int[]
			{
				0,
				5,
				8,
				3
			};
			array6[0] = num;
			recipe6.SetIngredients(array6);
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3959);
			Recipe recipe7 = Recipe.currentRecipe;
			int[] array7 = new int[]
			{
				0,
				15,
				225,
				5
			};
			array7[0] = num;
			recipe7.SetIngredients(array7);
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3968);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				num,
				16
			});
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3960);
			Recipe recipe8 = Recipe.currentRecipe;
			int[] array8 = new int[]
			{
				0,
				20,
				149,
				10
			};
			array8[0] = num;
			recipe8.SetIngredients(array8);
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3966);
			Recipe recipe9 = Recipe.currentRecipe;
			int[] array9 = new int[]
			{
				22,
				3,
				170,
				6,
				0,
				10
			};
			array9[4] = num;
			recipe9.SetIngredients(array9);
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3973);
			Recipe recipe10 = Recipe.currentRecipe;
			int[] array10 = new int[]
			{
				0,
				5,
				225,
				2
			};
			array10[0] = num;
			recipe10.SetIngredients(array10);
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3971);
			Recipe recipe11 = Recipe.currentRecipe;
			int[] array11 = new int[]
			{
				154,
				4,
				0,
				15,
				149,
				1
			};
			array11[2] = num;
			recipe11.SetIngredients(array11);
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3958);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				num,
				14
			});
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(3964);
			Recipe recipe12 = Recipe.currentRecipe;
			int[] array12 = new int[]
			{
				0,
				4,
				8,
				4,
				85,
				1
			};
			array12[0] = num;
			recipe12.SetIngredients(array12);
			Recipe.currentRecipe.anyWood = true;
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4126);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3955,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				499
			});
			Recipe.AddRecipe();
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00148B54 File Offset: 0x00146D54
		private static void AddSandstoneFurniture()
		{
			Recipe.currentRecipe.createItem.SetDefaults(4720);
			Recipe.currentRecipe.createItem.stack = 2;
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4298);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				14
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4299);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				15,
				225,
				5
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4300);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				20,
				149,
				10
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4301);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				16
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4302);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				5,
				8,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4303);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				4,
				8,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4304);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				4
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4305);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				4,
				8,
				4,
				85,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				16
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4267);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				8,
				22,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4306);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				10,
				22,
				3,
				170,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4307);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4308);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				8,
				1,
				4051,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4309);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				6,
				8,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4310);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				15,
				154,
				4,
				149,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4312);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				6,
				206,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4313);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				5,
				225,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4314);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				8
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4315);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				10
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4316);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4051,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00149134 File Offset: 0x00147334
		private static void AddBambooFurniture()
		{
			Recipe.currentRecipe.createItem.SetDefaults(4566);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				14
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4567);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				15,
				225,
				5
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4568);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				20,
				149,
				10
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4569);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				16
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4570);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				5,
				8,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4571);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				4,
				8,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4572);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				4
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4573);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				4,
				8,
				4,
				85,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				16
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4574);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				8,
				22,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4575);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				10,
				22,
				3,
				170,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4576);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4577);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				8,
				1,
				4564,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4578);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				6,
				8,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4579);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				15,
				154,
				4,
				149,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4581);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				6,
				206,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4582);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				5,
				225,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4583);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				8
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4584);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				10
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(4586);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				4564,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x001496B8 File Offset: 0x001478B8
		private static void AddCoralFurniture()
		{
			Recipe.currentRecipe.createItem.SetDefaults(5148);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				14
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5149);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				15,
				225,
				5
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5150);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				20,
				149,
				10
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5151);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				16
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5152);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				5,
				8,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5153);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				4,
				8,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5154);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				4
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5155);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				4,
				8,
				4,
				85,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				16
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5156);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				8,
				22,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5157);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				10,
				22,
				3,
				170,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5158);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5159);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				8,
				1,
				5306,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5160);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				6,
				8,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5161);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				15,
				154,
				4,
				149,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5163);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				6,
				206,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5164);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				5,
				225,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5165);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				8
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5166);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				10
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5168);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5306,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00149C3C File Offset: 0x00147E3C
		private static void AddBalloonFurniture()
		{
			Recipe.currentRecipe.createItem.SetDefaults(5169);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				14
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5170);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				15,
				225,
				5
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5171);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				20,
				149,
				10
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5172);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				16
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5173);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				5,
				8,
				3
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5174);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				4,
				8,
				1
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5175);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				4
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5176);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				4,
				8,
				4,
				85,
				1
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				16
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5177);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				8,
				22,
				2
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5178);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				10,
				22,
				3,
				170,
				6
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5179);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				6
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5180);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				8,
				1,
				3738,
				3
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5181);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				6,
				8,
				1
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5182);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				15,
				154,
				4,
				149,
				1
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5184);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				6,
				206,
				1
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5185);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				5,
				225,
				2
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5186);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				8
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5187);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				10
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5189);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				3738,
				6
			});
			Recipe.currentRecipe.RequireGroup(RecipeGroupID.Balloons);
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0014A2DC File Offset: 0x001484DC
		private static void AddAshWoodFurnitureArmorAndItems()
		{
			Recipe.currentRecipe.createItem.SetDefaults(5279);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				20
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5280);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				30
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5281);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				25
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5284);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				7
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5283);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				8
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5282);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				10
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5190);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				14
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5191);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				15,
				225,
				5
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5192);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				20,
				149,
				10
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5193);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				16
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5194);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				5,
				8,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5195);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				4,
				8,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5196);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				4
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5197);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				4,
				8,
				4,
				85,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				16
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5198);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				8,
				22,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5199);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				10,
				22,
				3,
				170,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.currentRecipe.anyIronBar = true;
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5200);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5201);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				8,
				1,
				5215,
				3
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5202);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				6,
				8,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5203);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				15,
				154,
				4,
				149,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5205);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				6,
				206,
				1
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5206);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				5,
				225,
				2
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5207);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				8
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				18
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5208);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				10
			});
			Recipe.AddRecipe();
			Recipe.currentRecipe.createItem.SetDefaults(5210);
			Recipe.currentRecipe.SetIngredients(new int[]
			{
				5215,
				6
			});
			Recipe.currentRecipe.SetCraftingStation(new int[]
			{
				106
			});
			Recipe.AddRecipe();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0014AA20 File Offset: 0x00148C20
		private static void CreateReversePlatformRecipes()
		{
			int num = Recipe.numRecipes;
			for (int i = 0; i < num; i++)
			{
				if (Main.recipe[i].createItem.createTile >= 0 && TileID.Sets.Platforms[Main.recipe[i].createItem.createTile] && Main.recipe[i].requiredItem[1].type == 0)
				{
					Recipe.currentRecipe.createItem.SetDefaults(Main.recipe[i].requiredItem[0].type);
					Recipe.currentRecipe.createItem.stack = Main.recipe[i].requiredItem[0].stack;
					Recipe.currentRecipe.requiredItem[0].SetDefaults(Main.recipe[i].createItem.type);
					Recipe.currentRecipe.requiredItem[0].stack = Main.recipe[i].createItem.stack;
					for (int j = 0; j < Recipe.currentRecipe.requiredTile.Length; j++)
					{
						Recipe.currentRecipe.requiredTile[j] = Main.recipe[i].requiredTile[j];
					}
					Recipe.AddRecipe();
					Recipe recipe = Main.recipe[Recipe.numRecipes - 1];
					for (int k = Recipe.numRecipes - 2; k > i; k--)
					{
						Main.recipe[k + 1] = Main.recipe[k];
					}
					Main.recipe[i + 1] = recipe;
					Main.recipe[i + 1].notDecraftable = true;
				}
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0014ABA0 File Offset: 0x00148DA0
		private static void CreateReverseWallRecipes()
		{
			int num = Recipe.numRecipes;
			for (int i = 0; i < num; i++)
			{
				if (Main.recipe[i].createItem.createWall > 0 && Main.recipe[i].requiredItem[1].type == 0 && Main.recipe[i].requiredItem[0].createWall == -1)
				{
					Recipe.currentRecipe.createItem.SetDefaults(Main.recipe[i].requiredItem[0].type);
					Recipe.currentRecipe.createItem.stack = Main.recipe[i].requiredItem[0].stack;
					Recipe.currentRecipe.requiredItem[0].SetDefaults(Main.recipe[i].createItem.type);
					Recipe.currentRecipe.requiredItem[0].stack = Main.recipe[i].createItem.stack;
					for (int j = 0; j < Recipe.currentRecipe.requiredTile.Length; j++)
					{
						Recipe.currentRecipe.requiredTile[j] = Main.recipe[i].requiredTile[j];
					}
					Recipe.AddRecipe();
					Recipe recipe = Main.recipe[Recipe.numRecipes - 1];
					for (int k = Recipe.numRecipes - 2; k > i; k--)
					{
						Main.recipe[k + 1] = Main.recipe[k];
					}
					Main.recipe[i + 1] = recipe;
					Main.recipe[i + 1].notDecraftable = true;
				}
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0014AD1C File Offset: 0x00148F1C
		public void SetIngredients(params int[] ingredients)
		{
			if (ingredients.Length == 1)
			{
				ingredients = new int[]
				{
					ingredients[0],
					1
				};
			}
			if (ingredients.Length % 2 != 0)
			{
				throw new Exception("Bad ingredients amount");
			}
			for (int i = 0; i < ingredients.Length; i += 2)
			{
				int num = i / 2;
				this.requiredItem[num].SetDefaults(ingredients[i]);
				this.requiredItem[num].stack = ingredients[i + 1];
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0014AD88 File Offset: 0x00148F88
		public void SetCraftingStation(params int[] tileIDs)
		{
			for (int i = 0; i < tileIDs.Length; i++)
			{
				this.requiredTile[i] = tileIDs[i];
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0014ADB0 File Offset: 0x00148FB0
		private static void AddRecipe()
		{
			if (Recipe.currentRecipe.requiredTile[0] == 13)
			{
				Recipe.currentRecipe.alchemy = true;
			}
			Main.recipe[Recipe.numRecipes] = Recipe.currentRecipe;
			Recipe.currentRecipe = new Recipe();
			Recipe.numRecipes++;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0014ADFE File Offset: 0x00148FFE
		public static int GetRequiredTileStyle(int tileID)
		{
			if (tileID != 26)
			{
				return 0;
			}
			if (!WorldGen.crimson)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0014AE14 File Offset: 0x00149014
		public bool ContainsIngredient(int itemType)
		{
			foreach (Recipe.RequiredItemEntry requiredItemEntry in this.requiredItemQuickLookup)
			{
				if (requiredItemEntry.itemIdOrRecipeGroup == 0)
				{
					break;
				}
				if (requiredItemEntry.itemIdOrRecipeGroup == itemType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040002A2 RID: 674
		public static int maxRequirements = 15;

		// Token: 0x040002A3 RID: 675
		public static int maxRecipes = 3000;

		// Token: 0x040002A4 RID: 676
		public static int numRecipes;

		// Token: 0x040002A5 RID: 677
		private static Recipe currentRecipe = new Recipe();

		// Token: 0x040002A6 RID: 678
		public Item createItem = new Item();

		// Token: 0x040002A7 RID: 679
		public Item[] requiredItem = new Item[Recipe.maxRequirements];

		// Token: 0x040002A8 RID: 680
		public int[] requiredTile = new int[Recipe.maxRequirements];

		// Token: 0x040002A9 RID: 681
		public int[] acceptedGroups = new int[Recipe.maxRequirements];

		// Token: 0x040002AA RID: 682
		private Recipe.RequiredItemEntry[] requiredItemQuickLookup = new Recipe.RequiredItemEntry[Recipe.maxRequirements];

		// Token: 0x040002AB RID: 683
		public List<Item> customShimmerResults;

		// Token: 0x040002AC RID: 684
		public bool needHoney;

		// Token: 0x040002AD RID: 685
		public bool needWater;

		// Token: 0x040002AE RID: 686
		public bool needLava;

		// Token: 0x040002AF RID: 687
		public bool anyWood;

		// Token: 0x040002B0 RID: 688
		public bool anyIronBar;

		// Token: 0x040002B1 RID: 689
		public bool anyPressurePlate;

		// Token: 0x040002B2 RID: 690
		public bool anySand;

		// Token: 0x040002B3 RID: 691
		public bool anyFragment;

		// Token: 0x040002B4 RID: 692
		public bool alchemy;

		// Token: 0x040002B5 RID: 693
		public bool needSnowBiome;

		// Token: 0x040002B6 RID: 694
		public bool needGraveyardBiome;

		// Token: 0x040002B7 RID: 695
		public bool needEverythingSeed;

		// Token: 0x040002B8 RID: 696
		public bool notDecraftable;

		// Token: 0x040002B9 RID: 697
		public bool crimson;

		// Token: 0x040002BA RID: 698
		public bool corruption;

		// Token: 0x040002BB RID: 699
		private static bool _hasDelayedFindRecipes;

		// Token: 0x040002BC RID: 700
		private static Dictionary<int, int> _ownedItems = new Dictionary<int, int>();

		// Token: 0x020004B6 RID: 1206
		private struct RequiredItemEntry
		{
			// Token: 0x0400564C RID: 22092
			public int itemIdOrRecipeGroup;

			// Token: 0x0400564D RID: 22093
			public int stack;
		}
	}
}
