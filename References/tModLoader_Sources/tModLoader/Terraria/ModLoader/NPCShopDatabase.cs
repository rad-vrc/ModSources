using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	// Token: 0x020001E3 RID: 483
	public static class NPCShopDatabase
	{
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060025AD RID: 9645 RVA: 0x004F1FDC File Offset: 0x004F01DC
		public static IEnumerable<AbstractNPCShop> AllShops
		{
			get
			{
				return NPCShopDatabase.npcShopByName.Values;
			}
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x004F1FE8 File Offset: 0x004F01E8
		internal static void AddShop(AbstractNPCShop shop)
		{
			NPCShopDatabase.npcShopByName.Add(shop.FullName, shop);
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x004F1FFB File Offset: 0x004F01FB
		public static bool TryGetNPCShop(string fullName, out AbstractNPCShop shop)
		{
			return NPCShopDatabase.npcShopByName.TryGetValue(fullName, out shop);
		}

		/// <summary>
		/// Gets a shop name (identifier) in the format matching <see cref="P:Terraria.ModLoader.AbstractNPCShop.FullName" /> <br />
		/// Can be used with <see cref="M:Terraria.ModLoader.NPCShopDatabase.TryGetNPCShop(System.String,Terraria.ModLoader.AbstractNPCShop@)" /> and <see cref="M:Terraria.ModLoader.GlobalNPC.ModifyActiveShop(Terraria.NPC,System.String,Terraria.Item[])" />
		/// </summary>
		/// <param name="npcType"></param>
		/// <param name="shopName"></param>
		/// <returns></returns>
		// Token: 0x060025B0 RID: 9648 RVA: 0x004F2009 File Offset: 0x004F0209
		public static string GetShopName(int npcType, string shopName = "Shop")
		{
			return ((npcType < (int)NPCID.Count) ? ("Terraria/" + NPCID.Search.GetName(npcType)) : NPCLoader.GetNPC(npcType).FullName) + "/" + shopName;
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x004F2040 File Offset: 0x004F0240
		public static string GetShopNameFromVanillaIndex(int index)
		{
			return NPCShopDatabase._vanillaShopNames[index];
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x004F204C File Offset: 0x004F024C
		public static void Initialize()
		{
			NPCShopDatabase.npcShopByName.Clear();
			NPCShopDatabase.NoPylons.Clear();
			NPCShopDatabase.RegisterVanillaNPCShops();
			for (int i = 0; i < NPCLoader.NPCCount; i++)
			{
				NPCLoader.AddShops(i);
			}
			foreach (NPCShop shop in NPCShopDatabase.AllShops.OfType<NPCShop>())
			{
				NPCLoader.ModifyShop(shop);
			}
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x004F20CC File Offset: 0x004F02CC
		internal static void FinishSetup()
		{
			foreach (AbstractNPCShop abstractNPCShop in NPCShopDatabase.AllShops)
			{
				abstractNPCShop.FinishSetup();
				foreach (AbstractNPCShop.Entry entry in abstractNPCShop.ActiveEntries)
				{
					entry.Item.material = ItemID.Sets.IsAMaterial[entry.Item.type];
				}
			}
			NPCShopDatabase.InitShopTestSystem();
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x004F216C File Offset: 0x004F036C
		private static void RegisterVanillaNPCShops()
		{
			NPCShopDatabase.NoPylons.Add(NPCShopDatabase.GetShopName(368, "Shop"));
			NPCShopDatabase.NoPylons.Add(NPCShopDatabase.GetShopName(453, "Shop"));
			NPCShopDatabase.NoPylons.Add(NPCShopDatabase.GetShopName(550, "Shop"));
			NPCShopDatabase.NoPylons.Add(NPCShopDatabase.GetShopName(142, "Shop"));
			NPCShopDatabase.RegisterMerchant();
			NPCShopDatabase.RegisterArmsDealer();
			NPCShopDatabase.RegisterDryad();
			NPCShopDatabase.RegisterBombGuy();
			NPCShopDatabase.RegisterClothier();
			NPCShopDatabase.RegisterGoblin();
			NPCShopDatabase.RegisterWizard();
			NPCShopDatabase.RegisterMechanic();
			NPCShopDatabase.RegisterSantaClaws();
			NPCShopDatabase.RegisterTruffle();
			NPCShopDatabase.RegisterSteampunker();
			NPCShopDatabase.RegisterDyeTrader();
			NPCShopDatabase.RegisterPartyGirl();
			NPCShopDatabase.RegisterCyborg();
			NPCShopDatabase.RegisterPainter();
			NPCShopDatabase.RegisterWitchDoctor();
			NPCShopDatabase.RegisterPirate();
			NPCShopDatabase.RegisterStylist();
			NPCShopDatabase.RegisterSkeletonMerchant();
			NPCShopDatabase.RegisterBartender();
			NPCShopDatabase.RegisterGolfer();
			NPCShopDatabase.RegisterZoologist();
			NPCShopDatabase.RegisterPrincess();
			NPCShopDatabase.RegisterTravellingMerchant();
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x004F2259 File Offset: 0x004F0459
		public static IEnumerable<NPCShop.Entry> GetPylonEntries()
		{
			Condition forestPylonCondition = new Condition("Conditions.ForestPylon", delegate()
			{
				if (Main.LocalPlayer.ZoneSnow || Main.LocalPlayer.ZoneDesert || Main.LocalPlayer.ZoneBeach || Main.LocalPlayer.ZoneJungle || Main.LocalPlayer.ZoneHallow || Main.LocalPlayer.ZoneGlowshroom)
				{
					return false;
				}
				if (!Main.remixWorld)
				{
					return (double)Main.LocalPlayer.Center.Y / 16.0 < Main.worldSurface;
				}
				return (double)Main.LocalPlayer.Center.Y / 16.0 > Main.rockLayer && Main.LocalPlayer.Center.Y / 16f < (float)(Main.maxTilesY - 350);
			});
			Condition cavernPylonCondition = new Condition("Conditions.UndergroundPylon", () => !Main.LocalPlayer.ZoneSnow && !Main.LocalPlayer.ZoneDesert && !Main.LocalPlayer.ZoneBeach && !Main.LocalPlayer.ZoneJungle && !Main.LocalPlayer.ZoneHallow && (Main.remixWorld || !Main.LocalPlayer.ZoneGlowshroom) && (double)Main.LocalPlayer.Center.Y / 16.0 >= Main.worldSurface);
			Condition oceanPylonCondition = new Condition("Conditions.InBeach", delegate()
			{
				bool flag4 = Main.LocalPlayer.ZoneBeach && (double)Main.LocalPlayer.position.Y < Main.worldSurface * 16.0;
				if (Main.remixWorld)
				{
					double num13 = (double)Main.LocalPlayer.position.X / 16.0;
					double num14 = (double)Main.LocalPlayer.position.Y / 16.0;
					flag4 |= ((num13 < (double)Main.maxTilesX * 0.43 || num13 > (double)Main.maxTilesX * 0.57) && num14 > Main.rockLayer && num14 < (double)(Main.maxTilesY - 350));
				}
				return flag4;
			});
			Condition mushroomPylonCondition = new Condition("Conditions.InGlowshroom", () => Main.LocalPlayer.ZoneGlowshroom && (!Main.remixWorld || !Main.LocalPlayer.ZoneUnderworldHeight));
			yield return new NPCShop.Entry(4876, new Condition[]
			{
				Condition.HappyEnoughToSellPylons,
				Condition.AnotherTownNPCNearby,
				Condition.NotInEvilBiome,
				forestPylonCondition
			}).OrderLast();
			yield return new NPCShop.Entry(4920, new Condition[]
			{
				Condition.HappyEnoughToSellPylons,
				Condition.AnotherTownNPCNearby,
				Condition.NotInEvilBiome,
				Condition.InSnow
			}).OrderLast();
			yield return new NPCShop.Entry(4919, new Condition[]
			{
				Condition.HappyEnoughToSellPylons,
				Condition.AnotherTownNPCNearby,
				Condition.NotInEvilBiome,
				Condition.InDesert
			}).OrderLast();
			yield return new NPCShop.Entry(4917, new Condition[]
			{
				Condition.HappyEnoughToSellPylons,
				Condition.AnotherTownNPCNearby,
				Condition.NotInEvilBiome,
				cavernPylonCondition
			}).OrderLast();
			yield return new NPCShop.Entry(4918, new Condition[]
			{
				Condition.HappyEnoughToSellPylons,
				Condition.AnotherTownNPCNearby,
				Condition.NotInEvilBiome,
				oceanPylonCondition
			}).OrderLast();
			yield return new NPCShop.Entry(4875, new Condition[]
			{
				Condition.HappyEnoughToSellPylons,
				Condition.AnotherTownNPCNearby,
				Condition.NotInEvilBiome,
				Condition.InJungle
			}).OrderLast();
			yield return new NPCShop.Entry(4916, new Condition[]
			{
				Condition.HappyEnoughToSellPylons,
				Condition.AnotherTownNPCNearby,
				Condition.NotInEvilBiome,
				Condition.InHallow
			}).OrderLast();
			yield return new NPCShop.Entry(4921, new Condition[]
			{
				Condition.HappyEnoughToSellPylons,
				Condition.AnotherTownNPCNearby,
				Condition.NotInEvilBiome,
				mushroomPylonCondition
			}).OrderLast();
			foreach (ModPylon modPylon in PylonLoader.modPylons)
			{
				NPCShop.Entry entry = modPylon.GetNPCShopEntry();
				if (entry != null)
				{
					yield return entry.OrderLast();
				}
			}
			IEnumerator<ModPylon> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x004F2264 File Offset: 0x004F0464
		private static void RegisterMerchant()
		{
			Condition carriesFlareGun = Condition.PlayerCarriesItem(930);
			Condition drumSetCondition = new Condition("Conditions.DownedB2B3HM", () => NPC.downedBoss2 || NPC.downedBoss3 || Main.hardMode);
			new NPCShop(17, "Shop").Add(88, Array.Empty<Condition>()).Add(87, Array.Empty<Condition>()).Add(35, Array.Empty<Condition>()).Add(1991, Array.Empty<Condition>()).Add(3509, Array.Empty<Condition>()).Add(3506, Array.Empty<Condition>()).Add(8, Array.Empty<Condition>()).Add(28, Array.Empty<Condition>()).Add(188, new Condition[]
			{
				Condition.Hardmode
			}).Add(110, Array.Empty<Condition>()).Add(189, new Condition[]
			{
				Condition.Hardmode
			}).Add(40, Array.Empty<Condition>()).Add(42, Array.Empty<Condition>()).Add(965, Array.Empty<Condition>()).Add(967, new Condition[]
			{
				Condition.InSnow
			}).Add(33, new Condition[]
			{
				Condition.InJungle
			}).Add(4074, new Condition[]
			{
				Condition.TimeDay,
				Condition.HappyWindyDay
			}).Add(279, new Condition[]
			{
				Condition.BloodMoon
			}).Add(282, new Condition[]
			{
				Condition.TimeNight
			}).Add(346, new Condition[]
			{
				Condition.DownedSkeletron
			}).Add(488, new Condition[]
			{
				Condition.Hardmode
			}).Add(931, new Condition[]
			{
				carriesFlareGun
			}).Add(1614, new Condition[]
			{
				carriesFlareGun
			}).Add(1786, Array.Empty<Condition>()).Add(1348, new Condition[]
			{
				Condition.Hardmode
			}).Add(3198, new Condition[]
			{
				Condition.Hardmode
			}).Add(4063, new Condition[]
			{
				drumSetCondition
			}).Add(4673, new Condition[]
			{
				drumSetCondition
			}).Add(3108, new Condition[]
			{
				Condition.PlayerCarriesItem(3107)
			}).Register();
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x004F24DC File Offset: 0x004F06DC
		private static void RegisterArmsDealer()
		{
			NPCShop npcshop = new NPCShop(19, "Shop").Add(97, Array.Empty<Condition>());
			int item = 278;
			Condition[] array = new Condition[2];
			array[0] = Condition.BloodMoonOrHardmode;
			array[1] = new Condition("Conditions.WorldGenSilver", () => WorldGen.SavedOreTiers.Silver == 9);
			NPCShop npcshop2 = npcshop.Add(item, array);
			int item2 = 4915;
			Condition[] array2 = new Condition[2];
			array2[0] = Condition.BloodMoonOrHardmode;
			array2[1] = new Condition("Conditions.WorldGenTungsten", () => WorldGen.SavedOreTiers.Silver == 168);
			NPCShop npcshop3 = npcshop2.Add(item2, array2);
			int item3 = 47;
			Condition[] array3 = new Condition[1];
			array3[0] = new Condition("Conditions.NightAfterEvilOrHardmode", () => (NPC.downedBoss2 && !Main.dayTime) || Main.hardMode);
			npcshop3.Add(item3, array3).Add(95, Array.Empty<Condition>()).Add(98, Array.Empty<Condition>()).Add(4703, new Condition[]
			{
				Condition.InGraveyard,
				Condition.DownedSkeletron
			}).Add(324, new Condition[]
			{
				Condition.TimeNight
			}).Add(534, new Condition[]
			{
				Condition.Hardmode
			}).Add(1432, new Condition[]
			{
				Condition.Hardmode
			}).Add(2177, new Condition[]
			{
				Condition.Hardmode
			}).Add(1261, new Condition[]
			{
				Condition.PlayerCarriesItem(1258)
			}).Add(1836, new Condition[]
			{
				Condition.PlayerCarriesItem(1835)
			}).Add(3108, new Condition[]
			{
				Condition.PlayerCarriesItem(3107)
			}).Add(1783, new Condition[]
			{
				Condition.PlayerCarriesItem(1782)
			}).Add(1785, new Condition[]
			{
				Condition.PlayerCarriesItem(1784)
			}).Add(1736, new Condition[]
			{
				Condition.Halloween
			}).Add(1737, new Condition[]
			{
				Condition.Halloween
			}).Add(1738, new Condition[]
			{
				Condition.Halloween
			}).Register();
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x004F2738 File Offset: 0x004F0938
		private static void RegisterDryad()
		{
			new NPCShop(20, "Shop").Add(2886, new Condition[]
			{
				Condition.BloodMoon,
				Condition.CrimsonWorld,
				Condition.NotRemixWorld
			}).Add(2171, new Condition[]
			{
				Condition.BloodMoon,
				Condition.CrimsonWorld
			}).Add(4508, new Condition[]
			{
				Condition.BloodMoon,
				Condition.CrimsonWorld
			}).Add(67, new Condition[]
			{
				Condition.BloodMoon,
				Condition.CorruptWorld,
				Condition.NotRemixWorld
			}).Add(59, new Condition[]
			{
				Condition.BloodMoon,
				Condition.CorruptWorld
			}).Add(4504, new Condition[]
			{
				Condition.BloodMoon,
				Condition.CorruptWorld
			}).Add(66, new Condition[]
			{
				Condition.NotBloodMoon,
				Condition.NotRemixWorld
			}).Add(62, new Condition[]
			{
				Condition.NotBloodMoon
			}).Add(63, new Condition[]
			{
				Condition.NotBloodMoon
			}).Add(745, new Condition[]
			{
				Condition.NotBloodMoon
			}).Add(2171, new Condition[]
			{
				Condition.Hardmode,
				Condition.InGraveyard,
				Condition.CorruptWorld
			}).Add(59, new Condition[]
			{
				Condition.Hardmode,
				Condition.InGraveyard,
				Condition.CrimsonWorld
			}).Add(27, Array.Empty<Condition>()).Add(5309, Array.Empty<Condition>()).Add(114, Array.Empty<Condition>()).Add(1828, Array.Empty<Condition>()).Add(747, Array.Empty<Condition>()).Add(746, new Condition[]
			{
				Condition.Hardmode
			}).Add(369, new Condition[]
			{
				Condition.Hardmode
			}).Add(4505, new Condition[]
			{
				Condition.Hardmode
			}).Add(5214, new Condition[]
			{
				Condition.InUnderworld
			}).Add(194, new Condition[]
			{
				Condition.NotInUnderworld,
				Condition.InGlowshroom
			}).Add(1853, new Condition[]
			{
				Condition.Halloween
			}).Add(1854, new Condition[]
			{
				Condition.Halloween
			}).Add(3215, new Condition[]
			{
				Condition.DownedKingSlime
			}).Add(3216, new Condition[]
			{
				Condition.DownedQueenBee
			}).Add(3219, new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).Add(3217, new Condition[]
			{
				Condition.DownedEaterOfWorlds
			}).Add(3218, new Condition[]
			{
				Condition.DownedBrainOfCthulhu
			}).Add(3220, new Condition[]
			{
				Condition.DownedSkeletron
			}).Add(3221, new Condition[]
			{
				Condition.DownedSkeletron
			}).Add(3222, new Condition[]
			{
				Condition.Hardmode
			}).Add(4047, Array.Empty<Condition>()).Add(4045, Array.Empty<Condition>()).Add(4044, Array.Empty<Condition>()).Add(4043, Array.Empty<Condition>()).Add(4042, Array.Empty<Condition>()).Add(4046, Array.Empty<Condition>()).Add(4041, Array.Empty<Condition>()).Add(4241, Array.Empty<Condition>()).Add(4048, Array.Empty<Condition>()).Add(4430, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhasesQuarter0
			}).Add(4431, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhasesQuarter0
			}).Add(4432, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhasesQuarter0
			}).Add(4433, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhasesQuarter1
			}).Add(4434, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhasesQuarter1
			}).Add(4435, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhasesQuarter1
			}).Add(4436, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhasesQuarter2
			}).Add(4437, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhasesQuarter2
			}).Add(4438, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhasesQuarter2
			}).Add(4439, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhasesQuarter3
			}).Add(4440, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhasesQuarter3
			}).Add(4441, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhasesQuarter3
			}).Register();
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x004F2C88 File Offset: 0x004F0E88
		private static void RegisterBombGuy()
		{
			new NPCShop(38, "Shop").Add(168, Array.Empty<Condition>()).Add(166, Array.Empty<Condition>()).Add(167, Array.Empty<Condition>()).Add(265, new Condition[]
			{
				Condition.Hardmode
			}).Add(937, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedPlantera,
				Condition.DownedPirates
			}).Add(1347, new Condition[]
			{
				Condition.Hardmode
			}).Add(4827, new Condition[]
			{
				Condition.PlayerCarriesItem(4827)
			}).Add(4824, new Condition[]
			{
				Condition.PlayerCarriesItem(4824)
			}).Add(4825, new Condition[]
			{
				Condition.PlayerCarriesItem(4825)
			}).Add(4826, new Condition[]
			{
				Condition.PlayerCarriesItem(4826)
			}).Register();
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x004F2DA0 File Offset: 0x004F0FA0
		private static void RegisterClothier()
		{
			Condition taxCollectorIsPresent = Condition.NpcIsPresent(441);
			new NPCShop(54, "Shop").Add(254, Array.Empty<Condition>()).Add(981, Array.Empty<Condition>()).Add(242, new Condition[]
			{
				Condition.TimeDay
			}).Add(245, new Condition[]
			{
				Condition.MoonPhaseFull
			}).Add(246, new Condition[]
			{
				Condition.MoonPhaseFull
			}).Add(1288, new Condition[]
			{
				Condition.MoonPhaseFull,
				Condition.TimeNight
			}).Add(1289, new Condition[]
			{
				Condition.MoonPhaseFull,
				Condition.TimeNight
			}).Add(325, new Condition[]
			{
				Condition.MoonPhaseWaningGibbous
			}).Add(326, new Condition[]
			{
				Condition.MoonPhaseWaningGibbous
			}).Add(269, Array.Empty<Condition>()).Add(270, Array.Empty<Condition>()).Add(271, Array.Empty<Condition>()).Add(503, new Condition[]
			{
				Condition.DownedClown
			}).Add(504, new Condition[]
			{
				Condition.DownedClown
			}).Add(505, new Condition[]
			{
				Condition.DownedClown
			}).Add(322, new Condition[]
			{
				Condition.BloodMoon
			}).Add(3362, new Condition[]
			{
				Condition.BloodMoon
			}).Add(3363, new Condition[]
			{
				Condition.BloodMoon
			}).Add(2856, new Condition[]
			{
				Condition.TimeDay,
				Condition.DownedCultist
			}).Add(2858, new Condition[]
			{
				Condition.TimeDay,
				Condition.DownedCultist
			}).Add(2857, new Condition[]
			{
				Condition.TimeNight,
				Condition.DownedCultist
			}).Add(2859, new Condition[]
			{
				Condition.TimeNight,
				Condition.DownedCultist
			}).Add(3242, new Condition[]
			{
				taxCollectorIsPresent
			}).Add(3243, new Condition[]
			{
				taxCollectorIsPresent
			}).Add(3244, new Condition[]
			{
				taxCollectorIsPresent
			}).Add(4685, new Condition[]
			{
				Condition.InGraveyard
			}).Add(4686, new Condition[]
			{
				Condition.InGraveyard
			}).Add(4704, new Condition[]
			{
				Condition.InGraveyard
			}).Add(4705, new Condition[]
			{
				Condition.InGraveyard
			}).Add(4706, new Condition[]
			{
				Condition.InGraveyard
			}).Add(4707, new Condition[]
			{
				Condition.InGraveyard
			}).Add(4708, new Condition[]
			{
				Condition.InGraveyard
			}).Add(4709, new Condition[]
			{
				Condition.InGraveyard
			}).Add(1429, new Condition[]
			{
				Condition.InSnow
			}).Add(1740, new Condition[]
			{
				Condition.Halloween
			}).Add(869, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhaseThirdQuarter
			}).Add(4994, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhaseWaningCrescent
			}).Add(4997, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhaseWaningCrescent
			}).Add(864, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhaseNew
			}).Add(865, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhaseNew
			}).Add(4995, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhaseWaxingCrescent
			}).Add(4998, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhaseWaxingCrescent
			}).Add(873, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhaseFirstQuarter
			}).Add(874, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhaseFirstQuarter
			}).Add(875, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhaseFirstQuarter
			}).Add(4996, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhaseWaxingGibbous
			}).Add(4999, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhaseWaxingGibbous
			}).Add(1275, new Condition[]
			{
				Condition.DownedFrostLegion,
				Condition.TimeDay
			}).Add(1276, new Condition[]
			{
				Condition.DownedFrostLegion,
				Condition.TimeNight
			}).Add(3246, new Condition[]
			{
				Condition.Halloween
			}).Add(3247, new Condition[]
			{
				Condition.Halloween
			}).Add(3730, new Condition[]
			{
				Condition.BirthdayParty
			}).Add(3731, new Condition[]
			{
				Condition.BirthdayParty
			}).Add(3733, new Condition[]
			{
				Condition.BirthdayParty
			}).Add(3734, new Condition[]
			{
				Condition.BirthdayParty
			}).Add(3735, new Condition[]
			{
				Condition.BirthdayParty
			}).Add(4744, new Condition[]
			{
				Condition.GolfScoreOver(2000)
			}).Add(5308, Array.Empty<Condition>()).Register();
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x004F339C File Offset: 0x004F159C
		private static void RegisterGoblin()
		{
			new NPCShop(107, "Shop").Add(128, Array.Empty<Condition>()).Add(486, Array.Empty<Condition>()).Add(398, Array.Empty<Condition>()).Add(84, Array.Empty<Condition>()).Add(407, Array.Empty<Condition>()).Add(161, Array.Empty<Condition>()).Add(5324, new Condition[]
			{
				Condition.Hardmode
			}).Register();
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x004F342C File Offset: 0x004F162C
		private static void RegisterWizard()
		{
			new NPCShop(108, "Shop").Add(487, Array.Empty<Condition>()).Add(496, Array.Empty<Condition>()).Add(500, Array.Empty<Condition>()).Add(507, Array.Empty<Condition>()).Add(508, Array.Empty<Condition>()).Add(531, Array.Empty<Condition>()).Add(149, Array.Empty<Condition>()).Add(576, Array.Empty<Condition>()).Add(3186, Array.Empty<Condition>()).Add(1739, new Condition[]
			{
				Condition.Halloween
			}).Register();
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x004F34EC File Offset: 0x004F16EC
		private static void RegisterMechanic()
		{
			new NPCShop(124, "Shop").Add(509, Array.Empty<Condition>()).Add(850, Array.Empty<Condition>()).Add(851, Array.Empty<Condition>()).Add(3612, Array.Empty<Condition>()).Add(510, Array.Empty<Condition>()).Add(530, Array.Empty<Condition>()).Add(513, Array.Empty<Condition>()).Add(538, Array.Empty<Condition>()).Add(529, Array.Empty<Condition>()).Add(541, Array.Empty<Condition>()).Add(542, Array.Empty<Condition>()).Add(543, Array.Empty<Condition>()).Add(852, Array.Empty<Condition>()).Add(853, Array.Empty<Condition>()).Add(4261, Array.Empty<Condition>()).Add(3707, Array.Empty<Condition>()).Add(2739, Array.Empty<Condition>()).Add(849, Array.Empty<Condition>()).Add(1263, Array.Empty<Condition>()).Add(3616, Array.Empty<Condition>()).Add(3725, Array.Empty<Condition>()).Add(2799, Array.Empty<Condition>()).Add(3619, Array.Empty<Condition>()).Add(3627, Array.Empty<Condition>()).Add(3629, Array.Empty<Condition>()).Add(585, Array.Empty<Condition>()).Add(584, Array.Empty<Condition>()).Add(583, Array.Empty<Condition>()).Add(4484, Array.Empty<Condition>()).Add(4485, Array.Empty<Condition>()).Add(2295, new Condition[]
			{
				Condition.NpcIsPresent(369),
				Condition.MoonPhasesOdd
			}).Register();
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x004F36F4 File Offset: 0x004F18F4
		private static void RegisterSantaClaws()
		{
			NPCShop shop = new NPCShop(142, "Shop").Add(588, Array.Empty<Condition>()).Add(589, Array.Empty<Condition>()).Add(590, Array.Empty<Condition>()).Add(597, Array.Empty<Condition>()).Add(598, Array.Empty<Condition>()).Add(596, Array.Empty<Condition>());
			for (int i = 1873; i <= 1905; i++)
			{
				shop.Add(i, Array.Empty<Condition>());
			}
			shop.Register();
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x004F3794 File Offset: 0x004F1994
		private static void RegisterTruffle()
		{
			new NPCShop(160, "Shop").Add(756, new Condition[]
			{
				Condition.DownedMechBossAny
			}).Add(787, new Condition[]
			{
				Condition.DownedMechBossAny
			}).Add(868, Array.Empty<Condition>()).Add(1551, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(1181, Array.Empty<Condition>()).Add(5231, Array.Empty<Condition>()).Add(783, new Condition[]
			{
				Condition.NotRemixWorld
			}).Register();
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x004F3844 File Offset: 0x004F1A44
		private static void RegisterSteampunker()
		{
			Condition steampunkerOutfitCondition = new Condition("Conditions.MoonPhasesHalf0OrPreHardmode", () => Condition.PreHardmode.IsMet() || Condition.MoonPhasesHalf0.IsMet());
			NPCShop npcshop = new NPCShop(178, "Shop").Add(779, new Condition[]
			{
				Condition.NotRemixWorld
			}).Add(839, new Condition[]
			{
				steampunkerOutfitCondition
			}).Add(840, new Condition[]
			{
				steampunkerOutfitCondition
			}).Add(841, new Condition[]
			{
				steampunkerOutfitCondition
			}).Add(748, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhasesHalf1
			}).Add(948, new Condition[]
			{
				Condition.DownedGolem
			}).Add(3623, new Condition[]
			{
				Condition.Hardmode
			}).Add(3603, Array.Empty<Condition>()).Add(3604, Array.Empty<Condition>()).Add(3607, Array.Empty<Condition>()).Add(3605, Array.Empty<Condition>()).Add(3606, Array.Empty<Condition>()).Add(3608, Array.Empty<Condition>()).Add(3618, Array.Empty<Condition>()).Add(3602, Array.Empty<Condition>()).Add(3663, Array.Empty<Condition>()).Add(3609, Array.Empty<Condition>()).Add(3610, Array.Empty<Condition>());
			int item = 995;
			Condition[] array = new Condition[1];
			array[0] = new Condition("Conditions.HardmodeFTW", () => Main.hardMode || !Main.getGoodWorld);
			npcshop.Add(item, array).Add(2203, new Condition[]
			{
				Condition.DownedEyeOfCthulhu,
				Condition.DownedEowOrBoc,
				Condition.DownedSkeletron
			}).Add(2193, new Condition[]
			{
				Condition.CrimsonWorld
			}).Add(4142, new Condition[]
			{
				Condition.CorruptWorld
			}).Add(2192, new Condition[]
			{
				Condition.InGraveyard
			}).Add(2204, new Condition[]
			{
				Condition.InJungle
			}).Add(2198, new Condition[]
			{
				Condition.InSnow
			}).Add(2197, new Condition[]
			{
				Condition.InSpace
			}).Add(2196, new Condition[]
			{
				Condition.PlayerCarriesItem(832)
			}).Add(784, new Condition[]
			{
				Condition.NotRemixWorld,
				Condition.EclipseOrBloodMoon,
				Condition.CrimsonWorld
			}).Add(782, new Condition[]
			{
				Condition.NotRemixWorld,
				Condition.EclipseOrBloodMoon,
				Condition.CorruptWorld
			}).Add(781, new Condition[]
			{
				Condition.NotRemixWorld,
				Condition.NotEclipseAndNotBloodMoon,
				Condition.InHallow
			}).Add(780, new Condition[]
			{
				Condition.NotRemixWorld,
				Condition.NotEclipseAndNotBloodMoon,
				Condition.NotInHallow
			}).Add(5392, new Condition[]
			{
				Condition.NotRemixWorld,
				Condition.DownedMoonLord
			}).Add(5393, new Condition[]
			{
				Condition.NotRemixWorld,
				Condition.DownedMoonLord
			}).Add(5394, new Condition[]
			{
				Condition.NotRemixWorld,
				Condition.DownedMoonLord
			}).Add(1344, new Condition[]
			{
				Condition.Hardmode
			}).Add(4472, new Condition[]
			{
				Condition.Hardmode
			}).Add(1742, new Condition[]
			{
				Condition.Halloween
			}).Register();
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x004F3C34 File Offset: 0x004F1E34
		private static void RegisterDyeTrader()
		{
			new NPCShop(207, "Shop").Add(1037, Array.Empty<Condition>()).Add(2874, Array.Empty<Condition>()).Add(1120, Array.Empty<Condition>()).Add(1969, new Condition[]
			{
				Condition.Multiplayer
			}).Add(3248, new Condition[]
			{
				Condition.Halloween
			}).Add(1741, new Condition[]
			{
				Condition.Halloween
			}).Add(2871, new Condition[]
			{
				Condition.MoonPhaseFull
			}).Add(2872, new Condition[]
			{
				Condition.MoonPhaseFull
			}).Add(4663, new Condition[]
			{
				Condition.BloodMoon
			}).Add(4662, new Condition[]
			{
				Condition.InGraveyard
			}).Register();
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x004F3D2C File Offset: 0x004F1F2C
		private static void RegisterPartyGirl()
		{
			new NPCShop(208, "Shop").Add(859, Array.Empty<Condition>()).Add(4743, new Condition[]
			{
				Condition.GolfScoreOver(500)
			}).Add(1000, Array.Empty<Condition>()).Add(1168, Array.Empty<Condition>()).Add(1449, new Condition[]
			{
				Condition.TimeDay
			}).Add(4552, new Condition[]
			{
				Condition.TimeNight
			}).Add(1345, Array.Empty<Condition>()).Add(1450, Array.Empty<Condition>()).Add(3253, Array.Empty<Condition>()).Add(4553, Array.Empty<Condition>()).Add(2700, Array.Empty<Condition>()).Add(2738, Array.Empty<Condition>()).Add(4470, Array.Empty<Condition>()).Add(4681, Array.Empty<Condition>()).Add(4682, new Condition[]
			{
				Condition.InGraveyard
			}).Add(4702, new Condition[]
			{
				Condition.LanternNight
			}).Add(3548, new Condition[]
			{
				Condition.PlayerCarriesItem(3548)
			}).Add(3369, new Condition[]
			{
				Condition.NpcIsPresent(229)
			}).Add(3546, new Condition[]
			{
				Condition.DownedGolem
			}).Add(3214, new Condition[]
			{
				Condition.Hardmode
			}).Add(2868, new Condition[]
			{
				Condition.Hardmode
			}).Add(970, new Condition[]
			{
				Condition.Hardmode
			}).Add(971, new Condition[]
			{
				Condition.Hardmode
			}).Add(972, new Condition[]
			{
				Condition.Hardmode
			}).Add(973, new Condition[]
			{
				Condition.Hardmode
			}).Add(4791, Array.Empty<Condition>()).Add(3747, Array.Empty<Condition>()).Add(3732, Array.Empty<Condition>()).Add(3742, Array.Empty<Condition>()).Add(3749, new Condition[]
			{
				Condition.BirthdayParty
			}).Add(3746, new Condition[]
			{
				Condition.BirthdayParty
			}).Add(3739, new Condition[]
			{
				Condition.BirthdayParty
			}).Add(3740, new Condition[]
			{
				Condition.BirthdayParty
			}).Add(3741, new Condition[]
			{
				Condition.BirthdayParty
			}).Add(3737, new Condition[]
			{
				Condition.BirthdayParty
			}).Add(3738, new Condition[]
			{
				Condition.BirthdayParty
			}).Add(3736, new Condition[]
			{
				Condition.BirthdayParty
			}).Add(3745, new Condition[]
			{
				Condition.BirthdayParty
			}).Add(3744, new Condition[]
			{
				Condition.BirthdayParty
			}).Add(3743, new Condition[]
			{
				Condition.BirthdayParty
			}).Register();
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x004F4098 File Offset: 0x004F2298
		private static void RegisterCyborg()
		{
			Condition portalGunStation = new Condition(Language.GetText("Conditions.PlayerCarriesItem2").WithFormatArgs(new object[]
			{
				Lang.GetItemName(3384),
				Lang.GetItemName(3664)
			}), () => Main.LocalPlayer.HasItem(3384) || Main.LocalPlayer.HasItem(3664));
			new NPCShop(209, "Shop").Add(771, Array.Empty<Condition>()).Add(772, new Condition[]
			{
				Condition.BloodMoon
			}).Add(773, new Condition[]
			{
				Condition.NightOrEclipse
			}).Add(774, new Condition[]
			{
				Condition.Eclipse
			}).Add(4445, new Condition[]
			{
				Condition.DownedMartians
			}).Add(4446, new Condition[]
			{
				Condition.DownedMartians,
				Condition.EclipseOrBloodMoon
			}).Add(4459, new Condition[]
			{
				Condition.Hardmode
			}).Add(760, new Condition[]
			{
				Condition.Hardmode
			}).Add(1346, new Condition[]
			{
				Condition.Hardmode
			}).Add(5451, new Condition[]
			{
				Condition.Hardmode
			}).Add(5452, new Condition[]
			{
				Condition.Hardmode
			}).Add(4409, new Condition[]
			{
				Condition.InGraveyard
			}).Add(4392, new Condition[]
			{
				Condition.InGraveyard
			}).Add(1743, new Condition[]
			{
				Condition.Halloween
			}).Add(1744, new Condition[]
			{
				Condition.Halloween
			}).Add(1745, new Condition[]
			{
				Condition.Halloween
			}).Add(2862, new Condition[]
			{
				Condition.DownedMartians
			}).Add(3109, new Condition[]
			{
				Condition.DownedMartians
			}).Add(3664, new Condition[]
			{
				portalGunStation
			}).Register();
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x004F42D0 File Offset: 0x004F24D0
		private static void RegisterPainter()
		{
			new NPCShop(227, "Shop").Add(1071, Array.Empty<Condition>()).Add(1072, Array.Empty<Condition>()).Add(1100, Array.Empty<Condition>()).Add(1073, Array.Empty<Condition>()).Add(1074, Array.Empty<Condition>()).Add(1075, Array.Empty<Condition>()).Add(1076, Array.Empty<Condition>()).Add(1077, Array.Empty<Condition>()).Add(1078, Array.Empty<Condition>()).Add(1079, Array.Empty<Condition>()).Add(1080, Array.Empty<Condition>()).Add(1081, Array.Empty<Condition>()).Add(1082, Array.Empty<Condition>()).Add(1083, Array.Empty<Condition>()).Add(1084, Array.Empty<Condition>()).Add(1097, Array.Empty<Condition>()).Add(1099, Array.Empty<Condition>()).Add(1098, Array.Empty<Condition>()).Add(1966, Array.Empty<Condition>()).Add(1967, new Condition[]
			{
				Condition.Hardmode
			}).Add(1968, new Condition[]
			{
				Condition.Hardmode
			}).Add(4668, new Condition[]
			{
				Condition.InGraveyard
			}).Add(5344, new Condition[]
			{
				Condition.InGraveyard,
				Condition.DownedPlantera
			}).Register();
			new NPCShop(227, "Decor").Add(1948, new Condition[]
			{
				Condition.Christmas
			}).Add(1949, new Condition[]
			{
				Condition.Christmas
			}).Add(1950, new Condition[]
			{
				Condition.Christmas
			}).Add(1951, new Condition[]
			{
				Condition.Christmas
			}).Add(1952, new Condition[]
			{
				Condition.Christmas
			}).Add(1953, new Condition[]
			{
				Condition.Christmas
			}).Add(1954, new Condition[]
			{
				Condition.Christmas
			}).Add(1955, new Condition[]
			{
				Condition.Christmas
			}).Add(1956, new Condition[]
			{
				Condition.Christmas
			}).Add(1957, new Condition[]
			{
				Condition.Christmas
			}).Add(2158, Array.Empty<Condition>()).Add(2159, Array.Empty<Condition>()).Add(2160, Array.Empty<Condition>()).Add(2008, Array.Empty<Condition>()).Add(2009, Array.Empty<Condition>()).Add(2010, Array.Empty<Condition>()).Add(2011, Array.Empty<Condition>()).Add(2012, Array.Empty<Condition>()).Add(2013, Array.Empty<Condition>()).Add(2014, Array.Empty<Condition>()).Add(1490, new Condition[]
			{
				Condition.NotInGraveyard
			}).Add(1481, new Condition[]
			{
				Condition.NotInGraveyard,
				Condition.MoonPhasesQuarter0
			}).Add(1482, new Condition[]
			{
				Condition.NotInGraveyard,
				Condition.MoonPhasesQuarter1
			}).Add(1483, new Condition[]
			{
				Condition.NotInGraveyard,
				Condition.MoonPhasesQuarter2
			}).Add(1484, new Condition[]
			{
				Condition.NotInGraveyard,
				Condition.MoonPhasesQuarter3
			}).Add(5245, new Condition[]
			{
				Condition.InShoppingZoneForest
			}).Add(1492, new Condition[]
			{
				Condition.InCrimson
			}).Add(1488, new Condition[]
			{
				Condition.InCorrupt
			}).Add(1489, new Condition[]
			{
				Condition.InHallow
			}).Add(1486, new Condition[]
			{
				Condition.InJungle
			}).Add(1487, new Condition[]
			{
				Condition.InSnow
			}).Add(1491, new Condition[]
			{
				Condition.InDesert
			}).Add(1493, new Condition[]
			{
				Condition.BloodMoon
			}).Add(1485, new Condition[]
			{
				Condition.NotInGraveyard,
				Condition.InSpace
			}).Add(1494, new Condition[]
			{
				Condition.NotInGraveyard,
				Condition.Hardmode,
				Condition.InSpace
			}).Add(5251, new Condition[]
			{
				Condition.Thunderstorm
			}).Add(4723, new Condition[]
			{
				Condition.InGraveyard
			}).Add(4724, new Condition[]
			{
				Condition.InGraveyard
			}).Add(4725, new Condition[]
			{
				Condition.InGraveyard
			}).Add(4726, new Condition[]
			{
				Condition.InGraveyard
			}).Add(4727, new Condition[]
			{
				Condition.InGraveyard
			}).Add(5257, new Condition[]
			{
				Condition.InGraveyard
			}).Add(4728, new Condition[]
			{
				Condition.InGraveyard
			}).Add(4729, new Condition[]
			{
				Condition.InGraveyard
			}).Register();
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x004F4888 File Offset: 0x004F2A88
		private static void RegisterWitchDoctor()
		{
			new NPCShop(228, "Shop").Add(1430, Array.Empty<Condition>()).Add(986, Array.Empty<Condition>()).Add(2999, new Condition[]
			{
				Condition.NpcIsPresent(108)
			}).Add(1158, new Condition[]
			{
				Condition.TimeNight
			}).Add(1159, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(1160, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(1161, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(1167, new Condition[]
			{
				Condition.DownedPlantera,
				Condition.InJungle
			}).Add(1339, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(1171, new Condition[]
			{
				Condition.Hardmode,
				Condition.InJungle
			}).Add(1162, new Condition[]
			{
				Condition.Hardmode,
				Condition.InJungle,
				Condition.TimeNight,
				Condition.DownedPlantera
			}).Add(909, Array.Empty<Condition>()).Add(910, Array.Empty<Condition>()).Add(940, Array.Empty<Condition>()).Add(941, Array.Empty<Condition>()).Add(942, Array.Empty<Condition>()).Add(943, Array.Empty<Condition>()).Add(944, Array.Empty<Condition>()).Add(945, Array.Empty<Condition>()).Add(4922, Array.Empty<Condition>()).Add(4417, Array.Empty<Condition>()).Add(1836, new Condition[]
			{
				Condition.PlayerCarriesItem(1835)
			}).Add(1261, new Condition[]
			{
				Condition.PlayerCarriesItem(1258)
			}).Add(1791, new Condition[]
			{
				Condition.Halloween
			}).Register();
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x004F4AB4 File Offset: 0x004F2CB4
		private static void RegisterPirate()
		{
			Condition beachCondition = new Condition("Conditions.InBeach", delegate()
			{
				int num6 = (int)((Main.screenPosition.X + (float)(Main.screenWidth / 2)) / 16f);
				return (double)(Main.screenPosition.Y / 16f) < Main.worldSurface + 10.0 && (num6 < 380 || num6 > Main.maxTilesX - 380);
			});
			new NPCShop(229, "Shop").Add(928, Array.Empty<Condition>()).Add(929, Array.Empty<Condition>()).Add(876, Array.Empty<Condition>()).Add(877, Array.Empty<Condition>()).Add(878, Array.Empty<Condition>()).Add(2434, Array.Empty<Condition>()).Add(1180, new Condition[]
			{
				beachCondition
			}).Add(1337, new Condition[]
			{
				Condition.NpcIsPresent(208),
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}).Register();
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x004F4B9C File Offset: 0x004F2D9C
		private static void RegisterStylist()
		{
			Condition maxLife = new Condition(Language.GetText("Conditions.AtleastXHealth").WithFormatArgs(new object[]
			{
				400
			}), () => Main.LocalPlayer.ConsumedLifeCrystals == 15);
			Condition maxMana = new Condition(Language.GetText("Conditions.AtleastXMana").WithFormatArgs(new object[]
			{
				200
			}), () => Main.LocalPlayer.ConsumedManaCrystals == 9);
			Condition moneyHair = new Condition("Conditions.PlatinumCoin", delegate()
			{
				long coinValue = 0L;
				for (int i = 0; i < 54; i++)
				{
					if (Main.LocalPlayer.inventory[i].type == 71)
					{
						coinValue += (long)Main.LocalPlayer.inventory[i].stack;
					}
					else if (Main.LocalPlayer.inventory[i].type == 72)
					{
						coinValue += (long)(Main.LocalPlayer.inventory[i].stack * 100);
					}
					else if (Main.LocalPlayer.inventory[i].type == 73)
					{
						coinValue += (long)(Main.LocalPlayer.inventory[i].stack * 10000);
					}
					else if (Main.LocalPlayer.inventory[i].type == 74)
					{
						coinValue += (long)(Main.LocalPlayer.inventory[i].stack * 1000000);
					}
					if (coinValue >= 1000000L)
					{
						return true;
					}
				}
				return false;
			});
			Condition timeHair = new Condition("Conditions.StyleMoon", () => Main.moonPhase % 2 == (!Main.dayTime).ToInt());
			Condition teamHair = new Condition("Conditions.OnTeam", () => Main.LocalPlayer.team != 0);
			new NPCShop(353, "Shop").Add(1990, Array.Empty<Condition>()).Add(1979, Array.Empty<Condition>()).Add(1977, new Condition[]
			{
				maxLife
			}).Add(1978, new Condition[]
			{
				maxMana
			}).Add(1980, new Condition[]
			{
				moneyHair
			}).Add(1981, new Condition[]
			{
				timeHair
			}).Add(1982, new Condition[]
			{
				teamHair
			}).Add(1983, new Condition[]
			{
				Condition.Hardmode
			}).Add(1984, new Condition[]
			{
				Condition.NpcIsPresent(208)
			}).Add(1985, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedTwins,
				Condition.DownedSkeletronPrime,
				Condition.DownedDestroyer
			}).Add(1986, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}).Add(2863, new Condition[]
			{
				Condition.DownedMartians
			}).Add(3259, new Condition[]
			{
				Condition.DownedMartians
			}).Add(5104, Array.Empty<Condition>()).Register();
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x004F4E14 File Offset: 0x004F3014
		private static void RegisterSkeletonMerchant()
		{
			Condition spelunkerGlowCondition = new Condition("Conditions.NightDayFullMoon", () => !Main.dayTime || Main.moonPhase == 0);
			Condition glowstickCondition = new Condition("Conditions.DaytimeNotFullMoon", () => Main.dayTime && Main.moonPhase != 0);
			Condition artisanCondition = new Condition("Conditions.NoAteLoaf", () => !Main.LocalPlayer.ateArtisanBread);
			Condition boneTorchCondition = new Condition("Conditions.Periodically", () => Main.time % 60.0 <= 30.0);
			Condition torchCondition = new Condition("Conditions.Periodically", () => Main.time % 60.0 > 30.0);
			new NPCShop(453, "Shop").Add(284, new Condition[]
			{
				Condition.MoonPhaseFull
			}).Add(946, new Condition[]
			{
				Condition.MoonPhaseWaningGibbous
			}).Add(3069, new Condition[]
			{
				Condition.MoonPhaseThirdQuarter,
				Condition.NotRemixWorld
			}).Add(517, new Condition[]
			{
				Condition.MoonPhaseThirdQuarter,
				Condition.RemixWorld
			}).Add(4341, new Condition[]
			{
				Condition.MoonPhaseWaningCrescent
			}).Add(285, new Condition[]
			{
				Condition.MoonPhaseNew
			}).Add(953, new Condition[]
			{
				Condition.MoonPhaseWaxingCrescent
			}).Add(3068, new Condition[]
			{
				Condition.MoonPhaseFirstQuarter
			}).Add(3084, new Condition[]
			{
				Condition.MoonPhaseWaxingGibbous
			}).Add(3001, new Condition[]
			{
				Condition.MoonPhasesEven
			}).Add(28, new Condition[]
			{
				Condition.MoonPhasesOdd
			}).Add(188, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhasesOdd
			}).Add(3002, new Condition[]
			{
				spelunkerGlowCondition
			}).Add(5377, new Condition[]
			{
				spelunkerGlowCondition,
				Condition.PlayerCarriesItem(930)
			}).Add(282, new Condition[]
			{
				glowstickCondition
			}).Add(3004, new Condition[]
			{
				boneTorchCondition
			}).Add(8, new Condition[]
			{
				torchCondition
			}).Add(3003, new Condition[]
			{
				Condition.MoonPhasesEvenQuarters
			}).Add(40, new Condition[]
			{
				Condition.MoonPhasesOddQuarters
			}).Add(3310, new Condition[]
			{
				Condition.MoonPhases04
			}).Add(3313, new Condition[]
			{
				Condition.MoonPhases15
			}).Add(3312, new Condition[]
			{
				Condition.MoonPhases26
			}).Add(3311, new Condition[]
			{
				Condition.MoonPhases37
			}).Add(166, Array.Empty<Condition>()).Add(965, Array.Empty<Condition>()).Add(3316, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhasesHalf0
			}).Add(3315, new Condition[]
			{
				Condition.Hardmode,
				Condition.MoonPhasesHalf1
			}).Add(3334, new Condition[]
			{
				Condition.Hardmode
			}).Add(3258, new Condition[]
			{
				Condition.Hardmode,
				Condition.BloodMoon
			}).Add(3043, new Condition[]
			{
				Condition.TimeNight,
				Condition.MoonPhaseFull
			}).Add(5326, new Condition[]
			{
				artisanCondition,
				Condition.MoonPhasesNearNew
			}).Register();
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x004F520C File Offset: 0x004F340C
		private static void RegisterBartender()
		{
			NPCShopDatabase.<>c__DisplayClass32_0 CS$<>8__locals1;
			CS$<>8__locals1.shop = new NPCShop(550, "Shop").AllowFillingLastSlot();
			CS$<>8__locals1.shop.Add(353, Array.Empty<Condition>());
			NPCShop shop = CS$<>8__locals1.shop;
			NPCShop.Entry[] array = new NPCShop.Entry[1];
			array[0] = new NPCShop.Entry(3828, Array.Empty<Condition>()).AddShopOpenedCallback(delegate(Item item, NPC npc)
			{
				if (NPC.downedGolemBoss)
				{
					item.shopCustomPrice = new int?(Item.buyPrice(0, 4, 0, 0));
					return;
				}
				if (NPC.downedMechBossAny)
				{
					item.shopCustomPrice = new int?(Item.buyPrice(0, 1, 0, 0));
					return;
				}
				item.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
			});
			shop.Add(array);
			CS$<>8__locals1.shop.Add(3816, Array.Empty<Condition>());
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3813, 50, Array.Empty<Condition>(), ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3800, 15, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3801, 15, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3802, 15, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3871, 50, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGolem
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3872, 50, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGolem
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3873, 50, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGolem
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3818, 5, Array.Empty<Condition>(), ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3824, 5, Array.Empty<Condition>(), ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3832, 5, Array.Empty<Condition>(), ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3829, 5, Array.Empty<Condition>(), ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3797, 15, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3798, 15, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3799, 15, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3874, 50, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGolem
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3875, 50, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGolem
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3876, 50, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGolem
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3819, 15, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3825, 15, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3833, 15, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3830, 15, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3803, 15, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3804, 15, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3805, 15, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3877, 50, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGolem
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3878, 50, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGolem
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3879, 50, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGolem
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3820, 60, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGolem
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3826, 60, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGolem
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3834, 60, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGolem
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3831, 60, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGolem
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3806, 15, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3807, 15, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3808, 15, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3880, 50, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGolem
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3881, 50, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGolem
			}, ref CS$<>8__locals1);
			NPCShopDatabase.<RegisterBartender>g__AddEntry|32_0(3882, 50, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGolem
			}, ref CS$<>8__locals1);
			CS$<>8__locals1.shop.Register();
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x004F5790 File Offset: 0x004F3990
		private static void RegisterGolfer()
		{
			Condition scoreOver500 = Condition.GolfScoreOver(500);
			Condition scoreOver501 = Condition.GolfScoreOver(1000);
			Condition scoreOver502 = Condition.GolfScoreOver(2000);
			new NPCShop(588, "Shop").Add(4587, Array.Empty<Condition>()).Add(4590, Array.Empty<Condition>()).Add(4589, Array.Empty<Condition>()).Add(4588, Array.Empty<Condition>()).Add(4083, Array.Empty<Condition>()).Add(4084, Array.Empty<Condition>()).Add(4085, Array.Empty<Condition>()).Add(4086, Array.Empty<Condition>()).Add(4087, Array.Empty<Condition>()).Add(4088, Array.Empty<Condition>()).Add(4039, new Condition[]
			{
				scoreOver500
			}).Add(4094, new Condition[]
			{
				scoreOver500
			}).Add(4093, new Condition[]
			{
				scoreOver500
			}).Add(4092, new Condition[]
			{
				scoreOver500
			}).Add(4089, Array.Empty<Condition>()).Add(3989, Array.Empty<Condition>()).Add(4095, Array.Empty<Condition>()).Add(4040, Array.Empty<Condition>()).Add(4319, Array.Empty<Condition>()).Add(4320, Array.Empty<Condition>()).Add(4591, new Condition[]
			{
				scoreOver501
			}).Add(4594, new Condition[]
			{
				scoreOver501
			}).Add(4593, new Condition[]
			{
				scoreOver501
			}).Add(4592, new Condition[]
			{
				scoreOver501
			}).Add(4135, Array.Empty<Condition>()).Add(4138, Array.Empty<Condition>()).Add(4136, Array.Empty<Condition>()).Add(4137, Array.Empty<Condition>()).Add(4049, Array.Empty<Condition>()).Add(4265, new Condition[]
			{
				scoreOver500
			}).Add(4595, new Condition[]
			{
				scoreOver502
			}).Add(4598, new Condition[]
			{
				scoreOver502
			}).Add(4597, new Condition[]
			{
				scoreOver502
			}).Add(4596, new Condition[]
			{
				scoreOver502
			}).Add(4264, new Condition[]
			{
				scoreOver502,
				Condition.DownedSkeletron
			}).Add(4599, new Condition[]
			{
				scoreOver500
			}).Add(4600, new Condition[]
			{
				scoreOver501
			}).Add(4601, new Condition[]
			{
				scoreOver502
			}).Add(4658, new Condition[]
			{
				scoreOver502,
				Condition.MoonPhasesQuarter0
			}).Add(4659, new Condition[]
			{
				scoreOver502,
				Condition.MoonPhasesQuarter1
			}).Add(4660, new Condition[]
			{
				scoreOver502,
				Condition.MoonPhasesQuarter2
			}).Add(4661, new Condition[]
			{
				scoreOver502,
				Condition.MoonPhasesQuarter3
			}).Register();
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x004F5ADC File Offset: 0x004F3CDC
		private static void RegisterZoologist()
		{
			NPCShop npcshop = new NPCShop(633, "Shop");
			int item = 4776;
			Condition[] array = new Condition[1];
			array[0] = new Condition("Conditions.BestiaryWinx", () => Chest.BestiaryGirl_IsFairyTorchAvailable());
			npcshop.Add(item, array).Add(4767, Array.Empty<Condition>()).Add(4759, Array.Empty<Condition>()).Add(5253, new Condition[]
			{
				Condition.MoonPhaseFull,
				Condition.TimeNight
			}).Add(4672, new Condition[]
			{
				Condition.BestiaryFilledPercent(10)
			}).Add(4829, Array.Empty<Condition>()).Add(4830, new Condition[]
			{
				Condition.BestiaryFilledPercent(25)
			}).Add(4910, new Condition[]
			{
				Condition.BestiaryFilledPercent(45)
			}).Add(4871, new Condition[]
			{
				Condition.BestiaryFilledPercent(30)
			}).Add(4907, new Condition[]
			{
				Condition.BestiaryFilledPercent(30)
			}).Add(4677, new Condition[]
			{
				Condition.DownedSolarPillar
			}).Add(4676, new Condition[]
			{
				Condition.BestiaryFilledPercent(10)
			}).Add(4762, new Condition[]
			{
				Condition.BestiaryFilledPercent(30)
			}).Add(4716, new Condition[]
			{
				Condition.BestiaryFilledPercent(25)
			}).Add(4785, new Condition[]
			{
				Condition.BestiaryFilledPercent(30)
			}).Add(4786, new Condition[]
			{
				Condition.BestiaryFilledPercent(30)
			}).Add(4787, new Condition[]
			{
				Condition.BestiaryFilledPercent(30)
			}).Add(4788, new Condition[]
			{
				Condition.BestiaryFilledPercent(30),
				Condition.Hardmode
			}).Add(4763, new Condition[]
			{
				Condition.BestiaryFilledPercent(35)
			}).Add(4955, new Condition[]
			{
				Condition.BestiaryFilledPercent(40)
			}).Add(4736, new Condition[]
			{
				Condition.Hardmode,
				Condition.BloodMoon
			}).Add(4701, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(4765, new Condition[]
			{
				Condition.BestiaryFilledPercent(50)
			}).Add(4766, new Condition[]
			{
				Condition.BestiaryFilledPercent(50)
			}).Add(5285, new Condition[]
			{
				Condition.BestiaryFilledPercent(50)
			}).Add(4777, new Condition[]
			{
				Condition.BestiaryFilledPercent(50)
			}).Add(4735, new Condition[]
			{
				Condition.BestiaryFilledPercent(70)
			}).Add(4951, new Condition[]
			{
				Condition.BestiaryFilledPercent(100)
			}).Add(4768, new Condition[]
			{
				Condition.MoonPhasesQuarter0
			}).Add(4769, new Condition[]
			{
				Condition.MoonPhasesQuarter0
			}).Add(4770, new Condition[]
			{
				Condition.MoonPhasesQuarter1
			}).Add(4771, new Condition[]
			{
				Condition.MoonPhasesQuarter1
			}).Add(4772, new Condition[]
			{
				Condition.MoonPhasesQuarter2
			}).Add(4773, new Condition[]
			{
				Condition.MoonPhasesQuarter2
			}).Add(4560, new Condition[]
			{
				Condition.MoonPhasesQuarter3
			}).Add(4775, new Condition[]
			{
				Condition.MoonPhasesQuarter3
			}).Register();
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x004F5EA8 File Offset: 0x004F40A8
		private static void RegisterPrincess()
		{
			NPCShop shop = new NPCShop(663, "Shop").Add(5071, Array.Empty<Condition>()).Add(5072, Array.Empty<Condition>()).Add(5073, Array.Empty<Condition>());
			for (int i = 5076; i <= 5087; i++)
			{
				shop.Add(i, Array.Empty<Condition>());
			}
			shop.Add(5310, Array.Empty<Condition>()).Add(5222, Array.Empty<Condition>()).Add(5228, Array.Empty<Condition>()).Add(5266, new Condition[]
			{
				Condition.DownedKingSlime,
				Condition.DownedQueenSlime
			}).Add(5044, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMoonLord
			}).Add(1309, new Condition[]
			{
				Condition.TenthAnniversaryWorld
			}).Add(1859, new Condition[]
			{
				Condition.TenthAnniversaryWorld
			}).Add(1358, new Condition[]
			{
				Condition.TenthAnniversaryWorld
			}).Add(857, new Condition[]
			{
				Condition.TenthAnniversaryWorld,
				Condition.InDesert
			}).Add(4144, new Condition[]
			{
				Condition.TenthAnniversaryWorld,
				Condition.BloodMoon
			}).Add(2584, new Condition[]
			{
				Condition.TenthAnniversaryWorld,
				Condition.Hardmode,
				Condition.DownedPirates,
				Condition.MoonPhasesQuarter0
			}).Add(854, new Condition[]
			{
				Condition.TenthAnniversaryWorld,
				Condition.Hardmode,
				Condition.DownedPirates,
				Condition.MoonPhasesQuarter1
			}).Add(855, new Condition[]
			{
				Condition.TenthAnniversaryWorld,
				Condition.Hardmode,
				Condition.DownedPirates,
				Condition.MoonPhasesQuarter2
			}).Add(905, new Condition[]
			{
				Condition.TenthAnniversaryWorld,
				Condition.Hardmode,
				Condition.DownedPirates,
				Condition.MoonPhasesQuarter3
			}).Add(5088, Array.Empty<Condition>()).Register();
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x004F60E0 File Offset: 0x004F42E0
		private static void RegisterTravellingMerchant()
		{
			new TravellingMerchantShop(368).AddInfoEntry(3309, Array.Empty<Condition>()).AddInfoEntry(3314, Array.Empty<Condition>()).AddInfoEntry(1987, Array.Empty<Condition>()).AddInfoEntry(2270, new Condition[]
			{
				Condition.Hardmode
			}).AddInfoEntry(4760, new Condition[]
			{
				Condition.Hardmode
			}).AddInfoEntry(2278, Array.Empty<Condition>()).AddInfoEntry(2271, Array.Empty<Condition>()).AddInfoEntry(2223, new Condition[]
			{
				Condition.DownedDestroyer,
				Condition.DownedTwins,
				Condition.DownedSkeletronPrime
			}).AddInfoEntry(2272, Array.Empty<Condition>()).AddInfoEntry(2276, Array.Empty<Condition>()).AddInfoEntry(2284, Array.Empty<Condition>()).AddInfoEntry(2285, Array.Empty<Condition>()).AddInfoEntry(2286, Array.Empty<Condition>()).AddInfoEntry(2287, Array.Empty<Condition>()).AddInfoEntry(4744, Array.Empty<Condition>()).AddInfoEntry(2296, new Condition[]
			{
				Condition.DownedSkeletron
			}).AddInfoEntry(3628, Array.Empty<Condition>()).AddInfoEntry(4091, new Condition[]
			{
				Condition.Hardmode
			}).AddInfoEntry(4603, Array.Empty<Condition>()).AddInfoEntry(4604, Array.Empty<Condition>()).AddInfoEntry(5297, Array.Empty<Condition>()).AddInfoEntry(4605, Array.Empty<Condition>()).AddInfoEntry(4550, Array.Empty<Condition>()).AddInfoEntry(2268, Array.Empty<Condition>()).AddInfoEntry(2269, new Condition[]
			{
				Condition.SmashedShadowOrb
			}).AddInfoEntry(1988, Array.Empty<Condition>()).AddInfoEntry(2275, Array.Empty<Condition>()).AddInfoEntry(2279, Array.Empty<Condition>()).AddInfoEntry(2277, Array.Empty<Condition>()).AddInfoEntry(4555, Array.Empty<Condition>()).AddInfoEntry(4321, Array.Empty<Condition>()).AddInfoEntry(4323, Array.Empty<Condition>()).AddInfoEntry(5390, Array.Empty<Condition>()).AddInfoEntry(4549, Array.Empty<Condition>()).AddInfoEntry(4561, Array.Empty<Condition>()).AddInfoEntry(4774, Array.Empty<Condition>()).AddInfoEntry(5136, Array.Empty<Condition>()).AddInfoEntry(5305, Array.Empty<Condition>()).AddInfoEntry(4562, Array.Empty<Condition>()).AddInfoEntry(4558, Array.Empty<Condition>()).AddInfoEntry(4559, Array.Empty<Condition>()).AddInfoEntry(4563, Array.Empty<Condition>()).AddInfoEntry(4666, Array.Empty<Condition>()).AddInfoEntry(4347, new Condition[]
			{
				Condition.DownedEarlygameBoss
			}).AddInfoEntry(4348, new Condition[]
			{
				Condition.Hardmode
			}).AddInfoEntry(3262, new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddInfoEntry(3284, new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddInfoEntry(2267, Array.Empty<Condition>()).AddInfoEntry(2214, Array.Empty<Condition>()).AddInfoEntry(2215, Array.Empty<Condition>()).AddInfoEntry(2216, Array.Empty<Condition>()).AddInfoEntry(2217, Array.Empty<Condition>()).AddInfoEntry(3624, Array.Empty<Condition>()).AddInfoEntry(671, new Condition[]
			{
				Condition.RemixWorld
			}).AddInfoEntry(2273, new Condition[]
			{
				Condition.NotRemixWorld
			}).AddInfoEntry(2274, Array.Empty<Condition>()).AddInfoEntry(2266, Array.Empty<Condition>()).AddInfoEntry(2281, Array.Empty<Condition>()).AddInfoEntry(2282, Array.Empty<Condition>()).AddInfoEntry(2283, Array.Empty<Condition>()).AddInfoEntry(2258, Array.Empty<Condition>()).AddInfoEntry(2242, Array.Empty<Condition>()).AddInfoEntry(2260, Array.Empty<Condition>()).AddInfoEntry(3637, Array.Empty<Condition>()).AddInfoEntry(4420, Array.Empty<Condition>()).AddInfoEntry(3119, Array.Empty<Condition>()).AddInfoEntry(3118, Array.Empty<Condition>()).AddInfoEntry(3099, Array.Empty<Condition>()).Register();
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x004F6574 File Offset: 0x004F4774
		private static void InitShopTestSystem()
		{
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x004F6584 File Offset: 0x004F4784
		internal static void Test()
		{
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x004F679C File Offset: 0x004F499C
		[CompilerGenerated]
		internal static void <RegisterBartender>g__AddEntry|32_0(int id, int price, Condition[] conditions, ref NPCShopDatabase.<>c__DisplayClass32_0 A_3)
		{
			A_3.shop.Add(new NPCShop.Entry[]
			{
				new NPCShop.Entry(new Item(id, 1, 0)
				{
					shopCustomPrice = new int?(price),
					shopSpecialCurrency = CustomCurrencyID.DefenderMedals
				}, conditions).ReserveSlot()
			});
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x004F67E8 File Offset: 0x004F49E8
		[CompilerGenerated]
		internal static string <Test>g__ChestToString|43_0(ref NPCShopDatabase.<>c__DisplayClass43_0 A_0)
		{
			return string.Join<int>(" ", from item in A_0.chest.item
			select item.type);
		}

		// Token: 0x040017C0 RID: 6080
		private static readonly Dictionary<string, AbstractNPCShop> npcShopByName = new Dictionary<string, AbstractNPCShop>();

		// Token: 0x040017C1 RID: 6081
		public static readonly ISet<string> NoPylons = new HashSet<string>();

		// Token: 0x040017C2 RID: 6082
		private static string[] _vanillaShopNames = new string[]
		{
			null,
			NPCShopDatabase.GetShopName(17, "Shop"),
			NPCShopDatabase.GetShopName(19, "Shop"),
			NPCShopDatabase.GetShopName(20, "Shop"),
			NPCShopDatabase.GetShopName(38, "Shop"),
			NPCShopDatabase.GetShopName(54, "Shop"),
			NPCShopDatabase.GetShopName(107, "Shop"),
			NPCShopDatabase.GetShopName(108, "Shop"),
			NPCShopDatabase.GetShopName(124, "Shop"),
			NPCShopDatabase.GetShopName(142, "Shop"),
			NPCShopDatabase.GetShopName(160, "Shop"),
			NPCShopDatabase.GetShopName(178, "Shop"),
			NPCShopDatabase.GetShopName(207, "Shop"),
			NPCShopDatabase.GetShopName(208, "Shop"),
			NPCShopDatabase.GetShopName(209, "Shop"),
			NPCShopDatabase.GetShopName(227, "Shop"),
			NPCShopDatabase.GetShopName(228, "Shop"),
			NPCShopDatabase.GetShopName(229, "Shop"),
			NPCShopDatabase.GetShopName(353, "Shop"),
			NPCShopDatabase.GetShopName(368, "Shop"),
			NPCShopDatabase.GetShopName(453, "Shop"),
			NPCShopDatabase.GetShopName(550, "Shop"),
			NPCShopDatabase.GetShopName(588, "Shop"),
			NPCShopDatabase.GetShopName(633, "Shop"),
			NPCShopDatabase.GetShopName(663, "Shop"),
			NPCShopDatabase.GetShopName(227, "Decor")
		};

		// Token: 0x040017C3 RID: 6083
		internal const bool TestingEnabled = false;

		// Token: 0x040017C4 RID: 6084
		private static List<NPCShopDatabase.ConditionTest> tests = new List<NPCShopDatabase.ConditionTest>();

		// Token: 0x040017C5 RID: 6085
		private static HashSet<string> mismatches = new HashSet<string>();

		// Token: 0x040017C6 RID: 6086
		private static readonly string TestFilePath = "TestedShopConditions.txt";

		// Token: 0x0200097E RID: 2430
		private class ConditionTest
		{
			// Token: 0x060054FC RID: 21756 RVA: 0x0069B0C8 File Offset: 0x006992C8
			public ConditionTest(IEnumerable<Condition> conditions)
			{
				this.Conditions = (from c in conditions
				orderby c.Description.Value
				select c).ToList<Condition>();
				this.Name = string.Join<LocalizedText>(" && ", from c in conditions
				select c.Description);
			}

			// Token: 0x170008E9 RID: 2281
			// (get) Token: 0x060054FD RID: 21757 RVA: 0x0069B140 File Offset: 0x00699340
			public string Status
			{
				get
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
					defaultInterpolatedStringHandler.AppendLiteral("[");
					defaultInterpolatedStringHandler.AppendFormatted<char>(this.Tested ? '#' : ' ');
					defaultInterpolatedStringHandler.AppendLiteral("] ");
					defaultInterpolatedStringHandler.AppendFormatted(this.Name);
					return defaultInterpolatedStringHandler.ToStringAndClear();
				}
			}

			// Token: 0x04006B5C RID: 27484
			public IEnumerable<Condition> Conditions;

			// Token: 0x04006B5D RID: 27485
			public string Name;

			// Token: 0x04006B5E RID: 27486
			public bool Tested;
		}
	}
}
