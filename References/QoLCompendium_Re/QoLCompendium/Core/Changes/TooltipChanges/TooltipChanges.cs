using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Humanizer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.TooltipChanges
{
	// Token: 0x02000217 RID: 535
	public class TooltipChanges : GlobalItem
	{
		// Token: 0x06000D0A RID: 3338 RVA: 0x00065351 File Offset: 0x00063551
		public override void SetStaticDefaults()
		{
			TooltipChanges._shimmerItemDisplay = new Item();
			TooltipChanges._shimmerNPCDisplay = new NPC();
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00065368 File Offset: 0x00063568
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine fav = tooltips.Find((TooltipLine l) => l.Name == "Favorite");
			TooltipLine favDescr = tooltips.Find((TooltipLine l) => l.Name == "FavoriteDesc");
			TooltipLine oneDropLogo = tooltips.Find((TooltipLine l) => l.Name == "OneDropLogo");
			TooltipLine id = tooltips.Find((TooltipLine l) => l.Mod == "AfterYM");
			tooltips.Remove(id);
			if (QoLCompendium.tooltipConfig.NoFavoriteTooltip)
			{
				tooltips.Remove(fav);
				tooltips.Remove(favDescr);
			}
			if (QoLCompendium.tooltipConfig.ShimmerableTooltip)
			{
				this.ShimmmerableTooltips(item, tooltips);
			}
			if (QoLCompendium.tooltipConfig.WorksInBanksTooltip && QoLCompendium.mainConfig.UtilityAccessoriesWorkInBanks)
			{
				this.WorksInBankTooltip(item, tooltips);
			}
			if (QoLCompendium.tooltipConfig.WingStatsTooltips)
			{
				this.WingStatsTooltip(item, tooltips);
			}
			if (QoLCompendium.tooltipConfig.HookStatsTooltips)
			{
				this.HookStatsTooltip(item, tooltips);
			}
			if (QoLCompendium.tooltipConfig.UsedPermanentUpgradeTooltip)
			{
				this.UsedPermanentUpgrade(item, tooltips);
			}
			if (QoLCompendium.tooltipConfig.AmmoTooltip)
			{
				this.AmmoTooltip(item, tooltips);
			}
			if (QoLCompendium.tooltipConfig.ActiveTooltip)
			{
				this.ActiveTooltip(item, tooltips);
			}
			if (QoLCompendium.tooltipConfig.NoYoyoTooltip)
			{
				tooltips.Remove(oneDropLogo);
			}
			if (QoLCompendium.tooltipConfig.FromModTooltip)
			{
				TooltipChanges.ItemModTooltip(item, tooltips);
			}
			if (QoLCompendium.tooltipConfig.ClassTagTooltip)
			{
				TooltipChanges.ItemClassTooltip(item, tooltips);
			}
			if (ModConditions.thoriumLoaded && ModConditions.exhaustionDisablerLoaded)
			{
				TooltipChanges.RemoveExhaustionToolTip(item, tooltips);
			}
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00065518 File Offset: 0x00063718
		public void ShimmmerableTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (!item.CanShimmer())
			{
				return;
			}
			int countsAsIem = ItemID.Sets.ShimmerCountsAsItem[item.type];
			int type = (countsAsIem != -1) ? countsAsIem : item.type;
			int transformsToItem = ItemID.Sets.ShimmerTransformToItem[type];
			int npcID = -1;
			if (type == 4986 && !NPC.unlockedSlimeRainbowSpawn && !QoLCompendium.mainConfig.NoTownSlimes)
			{
				npcID = 681;
			}
			else if (item.makeNPC > 0)
			{
				int npc = NPCID.Sets.ShimmerTransformToNPC[item.makeNPC];
				npcID = ((npc != -1) ? npc : item.makeNPC);
			}
			else if (type == 3461)
			{
				short num;
				switch (Main.GetMoonPhase())
				{
				case MoonPhase.Full:
					num = 5408;
					goto IL_FE;
				case MoonPhase.ThreeQuartersAtLeft:
					num = 5401;
					goto IL_FE;
				case MoonPhase.HalfAtLeft:
					num = 5403;
					goto IL_FE;
				case MoonPhase.QuarterAtLeft:
					num = 5402;
					goto IL_FE;
				case MoonPhase.QuarterAtRight:
					num = 5407;
					goto IL_FE;
				case MoonPhase.HalfAtRight:
					num = 5405;
					goto IL_FE;
				case MoonPhase.ThreeQuartersAtRight:
					num = 5404;
					goto IL_FE;
				}
				num = 5406;
				IL_FE:
				transformsToItem = (int)num;
			}
			else if (item.createTile == 139)
			{
				transformsToItem = 576;
			}
			string shimmerTextValue = Common.GetTooltipValue("Shimmerable", Array.Empty<object>());
			if (transformsToItem != -1)
			{
				TooltipChanges._shimmerItemDisplay.SetDefaults(transformsToItem);
				shimmerTextValue = Common.GetTooltipValue("ShimmerableIntoItem", new object[]
				{
					transformsToItem,
					TooltipChanges._shimmerItemDisplay.Name
				});
			}
			else if (npcID != -1)
			{
				TooltipChanges._shimmerNPCDisplay.SetDefaults(npcID, default(NPCSpawnParams));
				shimmerTextValue = Common.GetTooltipValue("ShimmerableIntoNPC", new object[]
				{
					TooltipChanges._shimmerNPCDisplay.GivenOrTypeName
				});
			}
			else
			{
				int coinLuck = ItemID.Sets.CoinLuckValue[type];
				if (coinLuck <= 0)
				{
					return;
				}
				string suffix = "ShimmerCoinLuck";
				object[] array = new object[1];
				int num2 = 0;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(1, 1);
				defaultInterpolatedStringHandler.AppendLiteral("+");
				defaultInterpolatedStringHandler.AppendFormatted<int>(coinLuck, "##,###");
				array[num2] = defaultInterpolatedStringHandler.ToStringAndClear();
				shimmerTextValue = Common.GetTooltipValue(suffix, array);
			}
			TooltipLine tooltipLine = new TooltipLine(base.Mod, "ShimmerInfo", shimmerTextValue)
			{
				OverrideColor = new Color?(Color.Plum)
			};
			Common.AddLastTooltip(tooltips, tooltipLine);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0006573C File Offset: 0x0006393C
		public void WorksInBankTooltip(Item item, List<TooltipLine> tooltips)
		{
			if (!Common.BankItems.Contains(item.type))
			{
				return;
			}
			TooltipLine tooltipLine = new TooltipLine(base.Mod, "WorksInBanks", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.WorksInBanks"))
			{
				OverrideColor = new Color?(Color.Gray)
			};
			Common.AddLastTooltip(tooltips, tooltipLine);
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00065790 File Offset: 0x00063990
		public unsafe void WingStatsTooltip(Item item, List<TooltipLine> tooltips)
		{
			int wingsID = item.wingSlot;
			if (wingsID != -1 && !item.social)
			{
				if (ModConditions.calamityLoaded && item.type <= (int)ItemID.Count)
				{
					return;
				}
				if (ModConditions.calamityLoaded)
				{
					int num = 15;
					List<int> list = new List<int>(num);
					CollectionsMarshal.SetCount<int>(list, num);
					Span<int> span = CollectionsMarshal.AsSpan<int>(list);
					int num2 = 0;
					*span[num2] = Common.GetModItem(ModConditions.calamityMod, "AureateBooster");
					num2++;
					*span[num2] = Common.GetModItem(ModConditions.calamityMod, "DrewsWings");
					num2++;
					*span[num2] = Common.GetModItem(ModConditions.calamityMod, "ElysianWings");
					num2++;
					*span[num2] = Common.GetModItem(ModConditions.calamityMod, "ExodusWings");
					num2++;
					*span[num2] = Common.GetModItem(ModConditions.calamityMod, "HadalMantle");
					num2++;
					*span[num2] = Common.GetModItem(ModConditions.calamityMod, "HadarianWings");
					num2++;
					*span[num2] = Common.GetModItem(ModConditions.calamityMod, "MOAB");
					num2++;
					*span[num2] = Common.GetModItem(ModConditions.calamityMod, "SilvaWings");
					num2++;
					*span[num2] = Common.GetModItem(ModConditions.calamityMod, "SkylineWings");
					num2++;
					*span[num2] = Common.GetModItem(ModConditions.calamityMod, "SoulofCryogen");
					num2++;
					*span[num2] = Common.GetModItem(ModConditions.calamityMod, "StarlightWings");
					num2++;
					*span[num2] = Common.GetModItem(ModConditions.calamityMod, "TarragonWings");
					num2++;
					*span[num2] = Common.GetModItem(ModConditions.calamityMod, "TracersCelestial");
					num2++;
					*span[num2] = Common.GetModItem(ModConditions.calamityMod, "TracersElysian");
					num2++;
					*span[num2] = Common.GetModItem(ModConditions.calamityMod, "TracersSeraph");
					num2++;
					if (list.Contains(item.type))
					{
						return;
					}
				}
				if (ModConditions.fargosSoulsLoaded && (item.type == Common.GetModItem(ModConditions.fargosSoulsMod, "FlightMasterySoul") || item.type == Common.GetModItem(ModConditions.fargosSoulsMod, "DimensionSoul") || item.type == Common.GetModItem(ModConditions.fargosSoulsMod, "EternitySoul")))
				{
					return;
				}
				if (ModConditions.wrathOfTheGodsLoaded && item.type == Common.GetModItem(ModConditions.wrathOfTheGodsMod, "DivineWings"))
				{
					return;
				}
				WingStats wingStats = ArmorIDs.Wing.Sets.Stats[wingsID];
				float flyTime = (float)wingStats.FlyTime / 60f;
				TooltipLine equip = tooltips.Find((TooltipLine l) => l.Name == "Equipable");
				TooltipLine flightTime = new TooltipLine(base.Mod, "FlightTime", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.FlightTime", flyTime.ToString("0.##")));
				TooltipLine horizontalSpeed = new TooltipLine(base.Mod, "HorizontalSpeed", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.HorizontalSpeed", wingStats.AccRunSpeedOverride.ToString("~0.##")));
				TooltipLine verticalSpeedMul = new TooltipLine(base.Mod, "VerticalSpeedMul", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.VerticalSpeedMul", wingStats.AccRunAccelerationMult.ToString("~0.##")));
				tooltips.Insert(tooltips.IndexOf(equip) + 1, flightTime);
				tooltips.Insert(tooltips.IndexOf(flightTime) + 1, horizontalSpeed);
				tooltips.Insert(tooltips.IndexOf(horizontalSpeed) + 1, verticalSpeedMul);
			}
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00065B38 File Offset: 0x00063D38
		public void HookStatsTooltip(Item item, List<TooltipLine> tooltips)
		{
			if (!ModConditions.calamityLoaded)
			{
				if (item.type == 84)
				{
					this.CreateVanillaHookTooltip(18.75f, 11.5f, tooltips);
				}
				if (item.type == 1236)
				{
					this.CreateVanillaHookTooltip(18.75f, 10f, tooltips);
				}
				if (item.type == 4759)
				{
					this.CreateVanillaHookTooltip(19f, 11.5f, tooltips);
				}
				if (item.type == 1237)
				{
					this.CreateVanillaHookTooltip(20.625f, 10.5f, tooltips);
				}
				if (item.type == 1238)
				{
					this.CreateVanillaHookTooltip(22.5f, 11f, tooltips);
				}
				if (item.type == 1239)
				{
					this.CreateVanillaHookTooltip(24.375f, 11.5f, tooltips);
				}
				if (item.type == 1240)
				{
					this.CreateVanillaHookTooltip(26.25f, 12f, tooltips);
				}
				if (item.type == 4257)
				{
					this.CreateVanillaHookTooltip(27.5f, 12.5f, tooltips);
				}
				if (item.type == 1241)
				{
					this.CreateVanillaHookTooltip(29.125f, 12.5f, tooltips);
				}
				if (item.type == 939)
				{
					this.CreateVanillaHookTooltip(22.625f, 10f, tooltips);
				}
				if (item.type == 1273)
				{
					this.CreateVanillaHookTooltip(21.875f, 15f, tooltips);
				}
				if (item.type == 2585)
				{
					this.CreateVanillaHookTooltip(18.75f, 13f, tooltips);
				}
				if (item.type == 2360)
				{
					this.CreateVanillaHookTooltip(25f, 13f, tooltips);
				}
				if (item.type == 185)
				{
					this.CreateVanillaHookTooltip(28f, 13f, tooltips);
				}
				if (item.type == 1800)
				{
					this.CreateVanillaHookTooltip(31.25f, 13.5f, tooltips);
				}
				if (item.type == 1915)
				{
					this.CreateVanillaHookTooltip(25f, 11.5f, tooltips);
				}
				if (item.type == 437)
				{
					this.CreateVanillaHookTooltip(27.5f, 14f, tooltips);
				}
				if (item.type == 4980)
				{
					this.CreateVanillaHookTooltip(30f, 16f, tooltips);
				}
				if (item.type == 3021)
				{
					this.CreateVanillaHookTooltip(30f, 16f, tooltips);
				}
				if (item.type == 3022)
				{
					this.CreateVanillaHookTooltip(30f, 15f, tooltips);
				}
				if (item.type == 3023)
				{
					this.CreateVanillaHookTooltip(30f, 15f, tooltips);
				}
				if (item.type == 3020)
				{
					this.CreateVanillaHookTooltip(30f, 15f, tooltips);
				}
				if (item.type == 2800)
				{
					this.CreateVanillaHookTooltip(31.25f, 14f, tooltips);
				}
				if (item.type == 1829)
				{
					this.CreateVanillaHookTooltip(34.375f, 15.5f, tooltips);
				}
				if (item.type == 1916)
				{
					this.CreateVanillaHookTooltip(34.375f, 15.5f, tooltips);
				}
				if (item.type == 3572)
				{
					this.CreateVanillaHookTooltip(34.375f, 18f, tooltips);
				}
				if (item.type == 3623)
				{
					this.CreateVanillaHookTooltip(37.5f, 16f, tooltips);
				}
			}
			float hookSpeed = 11f;
			if (ModConditions.calamityLoaded && (item.type == Common.GetModItem(ModConditions.calamityMod, "SerpentsBite") || item.type == Common.GetModItem(ModConditions.calamityMod, "BobbitHook")))
			{
				return;
			}
			if (item.shoot != 0 && Main.projHook[item.shoot] && item.type > (int)ItemID.Count)
			{
				ProjectileLoader.GetProjectile(item.shoot).GrapplePullSpeed(Main.CurrentPlayer, ref hookSpeed);
				float hookReach = ProjectileLoader.GetProjectile(item.shoot).GrappleRange() / 16f;
				TooltipLine equip = tooltips.Find((TooltipLine l) => l.Name == "Equipable");
				TooltipLine reach = new TooltipLine(base.Mod, "HookReach", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.HookReach", hookReach));
				TooltipLine pullSpeed = new TooltipLine(base.Mod, "HookPullSpeed", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.HookPullSpeed", hookSpeed));
				tooltips.Insert(tooltips.IndexOf(equip) + 1, reach);
				tooltips.Insert(tooltips.IndexOf(reach) + 1, pullSpeed);
			}
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00065F9C File Offset: 0x0006419C
		public void CreateVanillaHookTooltip(float hookReach, float hookSpeed, List<TooltipLine> tooltips)
		{
			TooltipLine equip = tooltips.Find((TooltipLine l) => l.Name == "Equipable");
			TooltipLine reach = new TooltipLine(base.Mod, "HookReach", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.HookReach", hookReach));
			TooltipLine pullSpeed = new TooltipLine(base.Mod, "HookPullSpeed", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.HookPullSpeed", hookSpeed));
			tooltips.Insert(tooltips.IndexOf(equip) + 1, reach);
			tooltips.Insert(tooltips.IndexOf(reach) + 1, pullSpeed);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00066034 File Offset: 0x00064234
		public void UsedPermanentUpgrade(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine tooltipLine = new TooltipLine(base.Mod, "UsedItem", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.UsedItem"))
			{
				OverrideColor = new Color?(Color.LightGreen)
			};
			if (item.type == 29)
			{
				tooltipLine.Text = Common.GetTooltipValue("UsedItemCountable", new object[]
				{
					Main.LocalPlayer.ConsumedLifeCrystals,
					15
				});
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == 1291)
			{
				tooltipLine.Text = Common.GetTooltipValue("UsedItemCountable", new object[]
				{
					Main.LocalPlayer.ConsumedLifeFruit,
					20
				});
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == 109)
			{
				tooltipLine.Text = Common.GetTooltipValue("UsedItemCountable", new object[]
				{
					Main.LocalPlayer.ConsumedManaCrystals,
					9
				});
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == 5337 && Main.LocalPlayer.usedAegisCrystal)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == 5339 && Main.LocalPlayer.usedArcaneCrystal)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == 5338 && Main.LocalPlayer.usedAegisFruit)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == 5342 && Main.LocalPlayer.usedAmbrosia)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == 5341 && Main.LocalPlayer.usedGummyWorm)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == 5340 && Main.LocalPlayer.usedGalaxyPearl)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == 5343 && NPC.peddlersSatchelWasUsed)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == 5326 && Main.LocalPlayer.ateArtisanBread)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == 4382 && NPC.combatBookWasUsed)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == 5336 && NPC.combatBookVolumeTwoWasUsed)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == 5043 && Main.LocalPlayer.unlockedBiomeTorches)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == 5289 && Main.LocalPlayer.unlockedSuperCart)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == 3335 && Main.LocalPlayer.CanDemonHeartAccessoryBeShown())
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x000662CC File Offset: 0x000644CC
		public void AmmoTooltip(Item item, List<TooltipLine> tooltips)
		{
			if (item.useAmmo != AmmoID.None)
			{
				Item displayItem = new Item(item.useAmmo, 1, 0);
				TooltipLine tooltipLine = new TooltipLine(base.Mod, "UseAmmo", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.UseAmmo"))
				{
					Text = Common.GetTooltipValue("UseAmmo", new object[]
					{
						item.useAmmo,
						displayItem.Name
					})
				};
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00066348 File Offset: 0x00064548
		public void ActiveTooltip(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine tooltipActive = new TooltipLine(base.Mod, "Active", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.Active"))
			{
				OverrideColor = new Color?(Common.ColorSwap(Color.Lime, Color.YellowGreen, 3f))
			};
			TooltipLine tooltipActiveBuff = new TooltipLine(base.Mod, "ActiveBuff", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.ActiveBuff"))
			{
				OverrideColor = new Color?(Common.ColorSwap(Color.Lime, Color.YellowGreen, 3f))
			};
			if (Main.LocalPlayer.GetModPlayer<QoLCPlayer>().activeItems.Contains(item.type))
			{
				Common.AddLastTooltip(tooltips, tooltipActive);
			}
			else
			{
				tooltips.Remove(tooltipActive);
			}
			if (Main.LocalPlayer.GetModPlayer<QoLCPlayer>().activeBuffItems.Contains(item.type))
			{
				Common.AddLastTooltip(tooltips, tooltipActiveBuff);
				return;
			}
			tooltips.Remove(tooltipActiveBuff);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00066424 File Offset: 0x00064624
		public static void ItemDisabledTooltip(Item item, List<TooltipLine> tooltips, bool configOn)
		{
			TooltipLine name = tooltips.Find((TooltipLine l) => l.Name == "ItemName");
			if (!configOn)
			{
				TooltipLine tooltipLine = name;
				tooltipLine.Text = tooltipLine.Text + " " + Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.ItemDisabled");
				name.OverrideColor = new Color?(Color.Red);
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0006648C File Offset: 0x0006468C
		public static void RemoveExhaustionToolTip(Item item, List<TooltipLine> tooltips)
		{
			foreach (TooltipLine tip in tooltips)
			{
				if ((item.type > (int)ItemID.Count && item.ModItem.Mod == ModConditions.thoriumMod && tip.Text == "Overuse of this weapon exhausts you, massively reducing its damage") || tip.Text == "Killing enemies recovers some of your exhaustion")
				{
					tip.Hide();
				}
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0006651C File Offset: 0x0006471C
		public static void ItemClassTooltip(Item item, List<TooltipLine> tooltips)
		{
			if (item.pick > 0 || item.hammer > 0 || item.axe > 0 || (item.damage > 0 && item.createTile > -1 && !item.IsCurrency))
			{
				tooltips.Insert(1, new TooltipLine(QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.Tool")));
			}
			if (item.CountsAsClass(DamageClass.Melee) && item.pick <= 0 && item.axe <= 0 && item.hammer <= 0 && item.createTile == -1 && !item.accessory)
			{
				tooltips.Insert(1, new TooltipLine(QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.WarriorClass")));
			}
			if (item.CountsAsClass(DamageClass.Ranged) && !item.IsCurrency && !item.accessory)
			{
				tooltips.Insert(1, new TooltipLine(QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.RangerClass")));
			}
			if (item.CountsAsClass(DamageClass.Magic) && item.type != 167 && item.damage > 0 && !item.accessory)
			{
				tooltips.Insert(1, new TooltipLine(QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.SorcererClass")));
			}
			if (item.CountsAsClass(DamageClass.Summon) && !item.accessory)
			{
				if (!ProjectileID.Sets.IsAWhip[item.shoot])
				{
					tooltips.Insert(1, new TooltipLine(QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.SummonerClass")));
				}
				else
				{
					tooltips.Insert(1, new TooltipLine(QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.SummonerClassWhip")));
				}
			}
			if (item.CountsAsClass(DamageClass.Throwing) && !item.accessory && !ModConditions.thoriumLoaded && !item.CountsAsClass(Common.GetModDamageClass(ModConditions.calamityMod, "RogueDamageClass")))
			{
				tooltips.Insert(1, new TooltipLine(QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.ThrowerClass")));
			}
			if (item.CountsAsClass(DamageClass.Generic) && !item.accessory)
			{
				tooltips.Insert(1, new TooltipLine(QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.Classless")));
			}
			if (item.CountsAsClass(Common.GetModDamageClass(ModConditions.calamityMod, "RogueDamageClass")) && ModConditions.calamityLoaded && !item.accessory)
			{
				tooltips.Insert(1, new TooltipLine(QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.RogueClass")));
			}
			if (item.CountsAsClass(Common.GetModDamageClass(ModConditions.ruptureMod, "DruidDamageClass")) && ModConditions.ruptureLoaded && !item.accessory)
			{
				tooltips.Insert(1, new TooltipLine(QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.DruidClass")));
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x000667D8 File Offset: 0x000649D8
		public static void ItemModTooltip(Item item, List<TooltipLine> tooltips)
		{
			if (item.ModItem != null)
			{
				TooltipLine name = tooltips.Find((TooltipLine l) => l.Name == "ItemName");
				TooltipLine tooltipFromMod = new TooltipLine(QoLCompendium.instance, "FromMod", StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.FromMod"), new object[]
				{
					item.ModItem.Mod.DisplayName
				}))
				{
					OverrideColor = new Color?(Common.ColorSwap(Color.AliceBlue, Color.Azure, 1f))
				};
				tooltips.AddAfter(name, tooltipFromMod);
			}
		}

		// Token: 0x04000582 RID: 1410
		private static Item _shimmerItemDisplay;

		// Token: 0x04000583 RID: 1411
		private static NPC _shimmerNPCDisplay;
	}
}
