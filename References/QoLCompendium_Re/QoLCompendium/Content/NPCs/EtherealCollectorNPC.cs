using System;
using System.Collections.Generic;
using QoLCompendium.Content.Items.Dedicated;
using QoLCompendium.Core;
using QoLCompendium.Core.UI.Panels;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.NPCs
{
	// Token: 0x020001F9 RID: 505
	[AutoloadHead]
	public class EtherealCollectorNPC : ModNPC
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x0002C58B File Offset: 0x0002A78B
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Content/NPCs/EtherealCollectorNPC";
			}
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0002C592 File Offset: 0x0002A792
		public override bool IsLoadingEnabled(Mod mod)
		{
			return QoLCompendium.mainConfig.EtherealCollectorCanSpawn;
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0002C5A0 File Offset: 0x0002A7A0
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[base.NPC.type] = 26;
			NPCID.Sets.ExtraFramesCount[base.NPC.type] = 9;
			NPCID.Sets.AttackFrameCount[base.NPC.type] = 5;
			NPCID.Sets.DangerDetectRange[base.NPC.type] = 800;
			NPCID.Sets.AttackType[base.NPC.type] = 0;
			NPCID.Sets.AttackTime[base.NPC.type] = 90;
			NPCID.Sets.AttackAverageChance[base.NPC.type] = 30;
			NPCID.Sets.HatOffsetY[base.NPC.type] = 4;
			base.NPC.Happiness.SetBiomeAffection<SnowBiome>(AffectionLevel.Love).SetBiomeAffection<UndergroundBiome>(AffectionLevel.Like).SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike).SetNPCAffection(20, AffectionLevel.Love).SetNPCAffection(550, AffectionLevel.Like).SetNPCAffection(108, AffectionLevel.Dislike).SetNPCAffection(441, AffectionLevel.Hate);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002C6A5 File Offset: 0x0002A8A5
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				new FlavorTextBestiaryInfoElement("He arrived to sell his wares, but where did he come from?")
			});
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002C6D0 File Offset: 0x0002A8D0
		public override void SetDefaults()
		{
			base.NPC.townNPC = true;
			base.NPC.friendly = true;
			base.NPC.width = 18;
			base.NPC.height = 48;
			base.NPC.aiStyle = 7;
			base.NPC.damage = 15;
			base.NPC.defense = 25;
			base.NPC.lifeMax = 500;
			base.NPC.alpha = 100;
			base.NPC.HitSound = new SoundStyle?(SoundID.NPCHit1);
			base.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath1);
			base.NPC.knockBackResist = 0.5f;
			base.AnimationType = 22;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002C794 File Offset: 0x0002A994
		public override bool CanTownNPCSpawn(int numTownNPCs)
		{
			return QoLCompendium.mainConfig.EtherealCollectorCanSpawn;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002C7A5 File Offset: 0x0002A9A5
		public override List<string> SetNPCNameList()
		{
			List<string> list = new List<string>();
			list.Add("Spiri");
			list.Add("Lumen");
			list.Add("Dexter");
			list.Add("Geist");
			list.Add("Sullivan");
			return list;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x000219CE File Offset: 0x0001FBCE
		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			if (!Main.hardMode)
			{
				damage = 10;
			}
			if (!NPC.downedMoonlord && Main.hardMode)
			{
				damage = 25;
			}
			if (NPC.downedMoonlord)
			{
				damage = 100;
			}
			knockback = 8f;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000219FF File Offset: 0x0001FBFF
		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 15;
			randExtraCooldown = 5;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002C7E3 File Offset: 0x0002A9E3
		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = 118;
			attackDelay = 1;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00021A11 File Offset: 0x0001FC11
		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 12f;
			randomOffset = 1f;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0002C7EC File Offset: 0x0002A9EC
		public override string GetChat()
		{
			string text;
			switch (Main.rand.Next(4))
			{
			case 0:
				text = "I won't tell you where I'm from";
				break;
			case 1:
				text = "The more you progress, the more I materialize for you";
				break;
			case 2:
				text = "Why don't you try to farm your gear sometimes?";
				break;
			default:
				text = "If one of the townsfolk dies, I'll gladly sell their items!";
				break;
			}
			string result = text;
			if (Main.LocalPlayer.HasItem(3513) && QoLCompendium.itemConfig.DedicatedItems)
			{
				result = "That Silver Shortsword is looking mighty fine! Take some burgers for showing me this beauty!";
			}
			return result;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0002C860 File Offset: 0x0002AA60
		public override void SetChatButtons(ref string button, ref string button2)
		{
			if (EtherealCollectorNPC.shopNum == 0)
			{
				button = "Modded Potions";
				EtherealCollectorNPC.ShopName = "Modded Potions";
			}
			else if (EtherealCollectorNPC.shopNum == 1)
			{
				button = "Modded Flasks, Stations & Foods";
				EtherealCollectorNPC.ShopName = "Modded Flasks, Stations & Foods";
			}
			else if (EtherealCollectorNPC.shopNum == 2)
			{
				button = "Modded Materials (Mods: A-M)";
				EtherealCollectorNPC.ShopName = "Modded Materials";
			}
			else if (EtherealCollectorNPC.shopNum == 3)
			{
				button = "Modded Materials (Mods: N-Z)";
				EtherealCollectorNPC.ShopName = "Modded Materials 2";
			}
			else if (EtherealCollectorNPC.shopNum == 4)
			{
				button = "Modded Treasure Bags";
				EtherealCollectorNPC.ShopName = "Modded Treasure Bags";
			}
			else if (EtherealCollectorNPC.shopNum == 5)
			{
				button = "Modded Crates & Grab Bags";
				EtherealCollectorNPC.ShopName = "Modded Crates & Grab Bags";
			}
			else if (EtherealCollectorNPC.shopNum == 6)
			{
				button = "Modded Ores & Bars";
				EtherealCollectorNPC.ShopName = "Modded Ores & Bars";
			}
			else if (EtherealCollectorNPC.shopNum == 7)
			{
				button = "Modded Natural Blocks";
				EtherealCollectorNPC.ShopName = "Modded Natural Blocks";
			}
			else if (EtherealCollectorNPC.shopNum == 8)
			{
				button = "Modded Building Blocks";
				EtherealCollectorNPC.ShopName = "Modded Building Blocks";
			}
			else if (EtherealCollectorNPC.shopNum == 9)
			{
				button = "Modded Herbs & Plants";
				EtherealCollectorNPC.ShopName = "Modded Herbs & Plants";
			}
			else if (EtherealCollectorNPC.shopNum == 10)
			{
				button = "Modded Fish & Fishing Gear";
				EtherealCollectorNPC.ShopName = "Modded Fish & Fishing Gear";
			}
			if (Main.LocalPlayer.HasItem(3513) && QoLCompendium.itemConfig.DedicatedItems)
			{
				button2 = "Get some burgers";
				return;
			}
			button2 = "Shop Changer";
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0002C9D4 File Offset: 0x0002ABD4
		public override void OnChatButtonClicked(bool firstButton, ref string shop)
		{
			if (firstButton)
			{
				shop = EtherealCollectorNPC.ShopName;
				EtherealCollectorNPCUI.visible = false;
				return;
			}
			if (Main.LocalPlayer.HasItem(3513) && QoLCompendium.itemConfig.DedicatedItems)
			{
				Item.NewItem(null, Main.LocalPlayer.getRect(), ModContent.ItemType<Burger>(), 100, false, 0, false, false);
				return;
			}
			if (!EtherealCollectorNPCUI.visible)
			{
				EtherealCollectorNPCUI.timeStart = Main.GameUpdateCount;
			}
			EtherealCollectorNPCUI.visible = true;
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0002CA44 File Offset: 0x0002AC44
		public override void AddShops()
		{
			new NPCShop(base.Type, "Modded Potions").AddModItemToShop(ModConditions.aequusMod, "BloodthirstPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.aequusMod, "FrostPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.aequusMod, "ManathirstPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.aequusMod, "MercerTonic", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.aequusMod, "NeutronYogurt", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedGlimmer
			}).AddModItemToShop(ModConditions.aequusMod, "NoonPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.aequusMod, "SentryPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.aequusMod, "VeinminerPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.afkpetsMod, "AstralwalkerPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.afkpetsMod, "BarkSkinPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.afkpetsMod, "BeetJuice", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.afkpetsMod, "BottledFlaxOil", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.afkpetsMod, "CarrotJuice", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.afkpetsMod, "CoronaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.afkpetsMod, "DragonheartElixir", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedOldOnesArmyT3,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.afkpetsMod, "FarmingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.afkpetsMod, "IroncladPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.afkpetsMod, "JumpmansMightyBrew", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.afkpetsMod, "KheprisSacrificialElixir", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToTemple
			}).AddModItemToShop(ModConditions.afkpetsMod, "LeafLoversBrew", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.afkpetsMod, "PhantomBloodPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon,
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.afkpetsMod, "RedBullPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.afkpetsMod, "StarSealerPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.afkpetsMod, "StinguardPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEverscream,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.assortedCrazyThingsMod, "EnhancedHunterPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedSoulHarvester,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.awfulGarbageMod, "Chirumiru", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).AddModItemToShop(ModConditions.awfulGarbageMod, "ReachPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedEyeOfTheStorm
			}).AddModItemToShop(ModConditions.awfulGarbageMod, "ScepterCapacityPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.blocksThrowerMod, "DexterityPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.blocksThrowerMod, "StealthPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedEclipse
			}).AddModItemToShop(ModConditions.buffariaMod, "AquaAffinityPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.buffariaMod, "BeePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.buffariaMod, "BoostPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.buffariaMod, "BreakerPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.buffariaMod, "CavernPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.buffariaMod, "CorruptionPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.buffariaMod, "CrimsonPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.buffariaMod, "CrimtaneRegenerationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.buffariaMod, "DemoniteProtectionPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.buffariaMod, "DesertPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.buffariaMod, "FastfallPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.buffariaMod, "FastHandsPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.buffariaMod, "FirestarterPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.buffariaMod, "FlightPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.buffariaMod, "ForestPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.buffariaMod, "HallowPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.buffariaMod, "HellPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.buffariaMod, "HolyPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.buffariaMod, "HoppingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.buffariaMod, "HyperPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.buffariaMod, "ImpactPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.buffariaMod, "InfestedPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToMushroom
			}).AddModItemToShop(ModConditions.buffariaMod, "JunglePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.buffariaMod, "ManaforcePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.buffariaMod, "OceanPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.buffariaMod, "PalladiumRegenerationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.buffariaMod, "PhoenixPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.buffariaMod, "RadarPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.buffariaMod, "SentryPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.buffariaMod, "ShockPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.buffariaMod, "ShredderPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.buffariaMod, "SnowPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.buffariaMod, "SoftfallPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.buffariaMod, "StabbingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.buffariaMod, "SteadfastPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.buffariaMod, "UnflinchingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.buffariaMod, "WisdomPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).AddModItemToShop(ModConditions.buffariaMod, "AgilityPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.buffariaMod, "BurningPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.buffariaMod, "CreationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.buffariaMod, "DoubleRegenerationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.buffariaMod, "GoliathPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.buffariaMod, "MermaidPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.buffariaMod, "SuperLuckPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.buffariaMod, "SurvivalPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.buffariaMod, "TerminatorPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.buffariaMod, "UltimaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.buffariaMod, "VisionaryPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.buffariaMod, "WarfarePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.buffariaMod, "WayfarerPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.buffariaMod, "WeightlessPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "AnechoicCoating", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToSulphurSea
			}).AddModItemToShop(ModConditions.calamityMod, "AstralInjection", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedAstrumAureus,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.calamityMod, "AureusCell", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedAstrumAureus,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.calamityMod, "BoundingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.calamityMod, "CalciumPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.calamityMod, "CeaselessHungerPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedCeaselessVoid
			}).AddModItemToShop(ModConditions.calamityMod, "GravityNormalizerPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedAstrumAureus,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.calamityMod, "PhotosynthesisPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.calamityMod, "PotionofOmniscience", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.calamityMod, "ShadowPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).AddModItemToShop(ModConditions.calamityMod, "SoaringPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.calamityMod, "SulphurskinPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToSulphurSea
			}).AddModItemToShop(ModConditions.calamityMod, "TeslaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedPerforatorsOrHiveMind,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.calamityMod, "ZenPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedSlimeGod
			}).AddModItemToShop(ModConditions.calamityMod, "ZergPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedSlimeGod
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "Meth", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSunkenSea
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "CrabLeaves", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedCrabulon
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "Coke", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.captureDiscsClassMod, "AffinityPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.captureDiscsClassMod, "SynergyPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.catalystMod, "AstraJelly", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.catalystMod, "Lean", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedAstrumAureus,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.cerebralMod, "AlertnessPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.cerebralMod, "DefenderPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.cerebralMod, "FocusPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.cerebralMod, "GravityPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.cerebralMod, "NebulaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.cerebralMod, "PulsarPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.cerebralMod, "SentryPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.cerebralMod, "SolarPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.cerebralMod, "StardustPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.cerebralMod, "StellarwindPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.cerebralMod, "VeilPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.cerebralMod, "VortexPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.cerebralMod, "WillPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.clamityAddonMod, "SupremeLuckPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedClamitas
			}).AddModItemToShop(ModConditions.clamityAddonMod, "TitanScalePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedGolem
			}).AddModItemToShop(ModConditions.ruptureMod, "DeadshotPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGoblinArmy
			}).AddModItemToShop(ModConditions.ruptureMod, "StrongArmPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.clickerClassMod, "InfluencePotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.consolariaMod, "Wiesnbrau", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.depthsMod, "CrystalSkinPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.depthsMod, "SilverSpherePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "DisgustingGoop", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasTalkedToSkeletonMerchant
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "MRE", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasTalkedToTravelingMerchant
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "KiStimulant", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.edorbisMod, "CapacityPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.edorbisMod, "EnergyRegenerationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedGlaciation
			}).AddModItemToShop(ModConditions.calamityEntropyMod, "SoyMilk", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.calamityEntropyMod, "YharimsStimulants", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.everjadeMod, "HeartbeatPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.everjadeMod, "SpinePotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.excelsiorMod, "SweetPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.excelsiorMod, "UncoagulatingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AuraPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BloodCastPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "CloverPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ForceFieldPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "FuryPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToAvalonEvilBiomes
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "GamblerPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "GauntletPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "GPSPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "HeartsickPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LeapingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "MagnetPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToAvalonEvilBiomes
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "NinjaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu,
				ModConditions.HasBeenToAvalonEvilBiomes
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "RoguePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ShadowPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ShockwavePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "StarbrightPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "StrengthPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "SupersonicPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "TimeShiftPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "TitanskinPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedDestroyer
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "VisionPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "WisdomPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvAmmoReservationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvArcheryPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvAuraPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvBattlePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvBloodCastPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvBuilderPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvCalmingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvCratePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvDangersensePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvEndurancePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvFeatherfallPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvFishingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvFlipperPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvForceFieldPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvFuryPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvGauntletPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvGillsPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvGPSPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvGravitationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvHeartreachPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvHeartsickPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvHunterPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvInfernoPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvInvisibilityPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvIronskinPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvLeapingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvLifeforcePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvLuckPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvMagicPowerPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvMagnetPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvManaRegenerationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvMiningPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvNightOwlPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvNinjaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvObsidianSkinPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvRagePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvRegenerationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvRoguePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvShadowPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvShinePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvShockwavePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvSonarPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvSpelunkerPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvStarbrightPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvStrengthPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvSummoningPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvSupersonicPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvSwiftnessPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvThornsPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvTimeShiftPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvTitanPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvTitanskinPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvWarmthPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvWaterWalkingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvWisdomPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AdvWrathPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.fargosSoulsMod, "RabiesShot", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "SanityPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.gerdsLabMod, "ExterminationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.gerdsLabMod, "ManaBarrierPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.gerdsLabMod, "RavagingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.gerdsLabMod, "SpitePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "FlightPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedMaterealizer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "HastePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedMaterealizer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "ReanimationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedMaterealizer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "YangPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "YinPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "FluorescentBerry", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedDiver,
				ModConditions.HasBeenToHomewardAbyss
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "FluorescentBerryCan", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedDiver,
				ModConditions.HasBeenToHomewardAbyss
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "ForbiddenFruit", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow,
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "ForbiddenFruitCan", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow,
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "Iodine", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToHomewardAbyss
			}).AddModItemToShop(ModConditions.martainsOrderMod, "BlackHolePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "DefenderPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "EmpowermentPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.martainsOrderMod, "SummonSpeedPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "HastePotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "InvincibilityPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedGolem
			}).AddModItemToShop(ModConditions.martainsOrderMod, "ShieldingPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "ShooterPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.martainsOrderMod, "SoulPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "CasterPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.martainsOrderMod, "StarreachPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "SugarBottle", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "SweepPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.martainsOrderMod, "ThrowerPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.martainsOrderMod, "WhipperPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.martainsOrderMod, "BleedPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "BrokenArmorPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "ConfusionPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "DarknessPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "DazePotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "ManasickPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "PetrifyPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "PoisonPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "SicknessPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "SilencePotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "SlowPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "WeaknessPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "SlimeBucket", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.medialRiftMod, "Abomnishrum", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.medialRiftMod, "BackwardsPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedQueenSlime
			}).AddModItemToShop(ModConditions.medialRiftMod, "BrilliantBeerBoot", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.medialRiftMod, "DazzlingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.medialRiftMod, "ExpiredMatterSyringe", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.medialRiftMod, "GlobalPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedQueenSlime
			}).AddModItemToShop(ModConditions.medialRiftMod, "MatterSyringe", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.medialRiftMod, "OverclockedBackwardsPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedQueenSlime
			}).AddModItemToShop(ModConditions.medialRiftMod, "StrangeBeer", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.medialRiftMod, "TimeCrasherPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.medialRiftMod, "ViriumPillBottle", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.medialRiftMod, "VsyncedMeth", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.medialRiftMod, "VsyncPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.polaritiesMod, "PiercingPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.polaritiesMod, "TolerancePotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.redemptionMod, "CharismaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedThorn,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.redemptionMod, "HydraCorrosionPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToWasteland
			}).AddModItemToShop(ModConditions.redemptionMod, "SkirmishPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToWasteland
			}).AddModItemToShop(ModConditions.redemptionMod, "VendettaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedThorn,
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.redemptionMod, "VigourousPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedNebuleus
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "AbyssalTonic", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedTidalSpirit,
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "AssassinationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToPlanetarium
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "BlazingTonic", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedInfernoSpirit,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "BlightfulTonic", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedNatureSpirit
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "BluefirePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "BrittlePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DoubleVisionPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToPyramid
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "EtherealTonic", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedLux,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "GlacialTonic", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedPermafrostSpirit,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "HarmonyPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "HereticTonic", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedEvilSpirit,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "NightmarePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "RipplePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "RoughskinPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToPyramid
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "SeismicTonic", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedEarthenSpirit,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "SoulAccessPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToPyramid
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "StarlightTonic", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedAdvisor,
				ModConditions.HasBeenToPlanetarium
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "VibePotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "SentryPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "DayStatsPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "ExtraEndurancePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedAraghur
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "NightStatsPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "ComboPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "FlariumInfernoImmunityPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedAraghur
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "NeilShinePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "NeilCritDamagePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "NeilBossDamagePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedAbaddon,
				ModConditions.DownedLostSiblings
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "FastFallPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "HolyShinePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "MoonlightPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedLostSiblings
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "NightmarePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "ThrownPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "NeilPolarizePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedAbaddon,
				ModConditions.DownedLostSiblings
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "SuppressionPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedLostSiblings
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "ThrownSpeedPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "FewerSpawnsPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "NeilSpeedPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "ExtraFishPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "JumpBoostPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedJensenTheGrandHarpy
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "SandstormWindImmunityPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.sloomeMod, "YharmStim", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedYharon
			}).AddModItemToShop(ModConditions.spiritMod, "PinkPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.spiritMod, "MirrorCoat", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.spiritMod, "RunePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToSpirit
			}).AddModItemToShop(ModConditions.spiritMod, "FlightPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.spiritMod, "SoulPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToSpirit
			}).AddModItemToShop(ModConditions.spiritMod, "SpiritPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToSpirit
			}).AddModItemToShop(ModConditions.spiritMod, "MushroomPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToMushroom
			}).AddModItemToShop(ModConditions.spiritMod, "StarPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToSpirit
			}).AddModItemToShop(ModConditions.spiritMod, "TurtlePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToSpirit
			}).AddModItemToShop(ModConditions.spiritMod, "BismitePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.spiritMod, "DoubleJumpPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.starlightRiverMod, "PotionForest", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.stormsAdditionsMod, "BeetlePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToTemple
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "FruitHeartPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "GunPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "HeartPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "ShroomitePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToMushroom
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "SpectrePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "SpookyPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedPlantera,
				Condition.DownedMourningWood
			}).AddModItemToShop(ModConditions.stramsClassesMod, "RetributionPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.stramsClassesMod, "StealthPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.stramsClassesMod, "StrengthPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.supernovaMod, "QuarionPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.terrorbornMod, "AdrenalinePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.terrorbornMod, "AerodynamicPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedTidalTitan,
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.terrorbornMod, "BloodFlowPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon,
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.terrorbornMod, "DarkbloodPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.terrorbornMod, "SinducementPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.terrorbornMod, "StarpowerPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).AddModItemToShop(ModConditions.terrorbornMod, "VampirismPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon,
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.thoriumMod, "AquaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToAquaticDepths
			}).AddModItemToShop(ModConditions.thoriumMod, "ArcanePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.thoriumMod, "ArtilleryPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.thoriumMod, "AssassinPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.thoriumMod, "BatRepellent", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon,
				ModConditions.DownedPatchWerk
			}).AddModItemToShop(ModConditions.thoriumMod, "BloodPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.thoriumMod, "BouncingFlamePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.thoriumMod, "CactusFruit", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedGrandThunderBird,
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.thoriumMod, "ConflagrationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.thoriumMod, "CreativityPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedGrandThunderBird,
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.thoriumMod, "EarwormPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "FishRepellent", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon,
				ModConditions.DownedPatchWerk
			}).AddModItemToShop(ModConditions.thoriumMod, "FrenzyPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToAquaticDepths
			}).AddModItemToShop(ModConditions.thoriumMod, "GlowingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToAquaticDepths
			}).AddModItemToShop(ModConditions.thoriumMod, "HolyPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.thoriumMod, "HydrationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToAquaticDepths
			}).AddModItemToShop(ModConditions.thoriumMod, "InsectRepellent", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon,
				ModConditions.DownedPatchWerk
			}).AddModItemToShop(ModConditions.thoriumMod, "InspirationReachPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.thoriumMod, "KineticPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.thoriumMod, "SkeletonRepellent", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon,
				ModConditions.DownedPatchWerk
			}).AddModItemToShop(ModConditions.thoriumMod, "WarmongerPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.thoriumMod, "ZombieRepellent", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon,
				ModConditions.DownedPatchWerk
			}).AddModItemToShop(ModConditions.thoriumBossReworkMod, "DeathsingerPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToAquaticDepths
			}).AddModItemToShop(ModConditions.thoriumBossReworkMod, "InspirationRegenerationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.uhtricMod, "PotionOfTheHealthy", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.uhtricMod, "PuncturePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.universeOfSwordsMod, "MeleePowerPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.universeOfSwordsMod, "NordMead", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.universeOfSwordsMod, "Skooma", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedPumpking
			}).AddModItemToShop(ModConditions.valhallaMod, "DartPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.valhallaMod, "SentryPlusPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.valhallaMod, "AuraPlusPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.vitalityMod, "ArmorPiercingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.vitalityMod, "LeapingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.vitalityMod, "TranquillityPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.vitalityMod, "TravelsensePotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.vitalityMod, "WarriorPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.zylonMod, "BloodiedVial", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.zylonMod, "FeralChemicals", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.zylonMod, "FloaterPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.zylonMod, "GalePotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.zylonMod, "HeavyHitterPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.zylonMod, "ManareachPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.zylonMod, "NeutroninaBottle", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedLunarEvent
			}).AddModItemToShop(ModConditions.zylonMod, "StealthPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).Register();
			new NPCShop(base.Type, "Modded Flasks, Stations & Foods").AddModItemToShop(ModConditions.aequusMod, "Baguette", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.aequusMod, "SpicyEel", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.aequusMod, "PalePufferfish", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.aequusMod, "PotionOfResurrection", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.aequusMod, "LesserRestorationPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.aequusMod, "GreaterRestorationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.aequusMod, "SuperRestorationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedLunarEvent
			}).AddModItemToShop(ModConditions.afkpetsMod, "FlaskofBlood", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.afkpetsMod, "FlaskofNature", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				Condition.DownedMechBossAll,
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.afkpetsMod, "FlaskofShadowflames", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				Condition.DownedGoblinArmy
			}).AddModItemToShop(ModConditions.afkpetsMod, "JellyfishJam", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.afkpetsMod, "OliveOilBottle", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee
			}).AddModItemToShop(ModConditions.assortedCrazyThingsMod, "EmpowermentFlask", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedSoulHarvester,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.blocksThrowerMod, "ThrowingBoard", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.buffariaMod, "BetsyPhial", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				Condition.DownedOldOnesArmyT3
			}).AddModItemToShop(ModConditions.buffariaMod, "BleedingPhial", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.buffariaMod, "ConfusedPhial", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.buffariaMod, "CursedFlamesPhial", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.buffariaMod, "DaybreakPhial", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.buffariaMod, "ElectricityPhial", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.buffariaMod, "ExplosivePhial", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.buffariaMod, "FirePhial", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee
			}).AddModItemToShop(ModConditions.buffariaMod, "FrostburnPhial", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee
			}).AddModItemToShop(ModConditions.buffariaMod, "GoldPhial", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.buffariaMod, "IchorPhial", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.buffariaMod, "PoisonPhial", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee
			}).AddModItemToShop(ModConditions.buffariaMod, "ShadowflamePhial", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.buffariaMod, "VenomPhial", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.buffariaMod, "LegendaryPhial", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.buffariaMod, "LesserHoneyedHealingPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.buffariaMod, "HoneyedHealingPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.buffariaMod, "GreaterHoneyedHealingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.buffariaMod, "SuperHoneyedHealingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedLunarEvent
			}).AddModItemToShop(ModConditions.buffariaMod, "LesserRestorationPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.buffariaMod, "GreaterRestorationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.buffariaMod, "SuperRestorationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedLunarEvent
			}).AddModItemToShop(ModConditions.buffariaMod, "LesserScalingHealingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.buffariaMod, "ScalingHealingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.buffariaMod, "GreaterScalingHealingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.buffariaMod, "SuperScalingHealingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.buffariaMod, "LesserScalingManaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.buffariaMod, "ScalingManaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.buffariaMod, "GreaterScalingManaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.buffariaMod, "SuperScalingManaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.calamityMod, "Baguette", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.calamityMod, "HadalStew", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToAbyss
			}).AddModItemToShop(ModConditions.calamityMod, "FlaskOfBrimstone", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedCalamitasClone
			}).AddModItemToShop(ModConditions.calamityMod, "FlaskOfCrumbling", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.calamityMod, "FlaskOfHolyFlames", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMoonLord,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.calamityMod, "CorruptionEffigy", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.calamityMod, "CrimsonEffigy", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.calamityMod, "EffigyOfDecay", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				ModConditions.DownedAcidRain1,
				ModConditions.HasBeenToSulphurSea
			}).AddModItemToShop(ModConditions.calamityMod, "TranquilityCandle", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.calamityMod, "ChaosCandle", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.calamityMod, "ResilientCandle", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "SpitefulCandle", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "WeightlessCandle", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "VigorousCandle", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "Bloodfin", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedProvidence,
				ModConditions.HasBeenToCrags
			}).AddModItemToShop(ModConditions.calamityMod, "SupremeHealingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedProvidence
			}).AddModItemToShop(ModConditions.calamityMod, "SupremeManaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.calamityMod, "OmegaHealingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedDevourerOfGods
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "AstralEffigy", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "HallowEffigy", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.captureDiscsClassMod, "DiscCharger", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.cerebralMod, "LesserPsychosisPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.cerebralMod, "PsychosisPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToMushroom
			}).AddModItemToShop(ModConditions.cerebralMod, "GreaterPsychosisPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.cerebralMod, "SuperPsychosisPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.cerebralMod, "EngineerToolkit", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.clamityAddonMod, "ExoBaguette", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedExoMechs
			}).AddModItemToShop(ModConditions.ruptureMod, "TrinketRack", Item.buyPrice(0, 15, 0, 0)).AddModItemToShop(ModConditions.clickerClassMod, "DesktopComputer", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.clickerClassMod, "HeavenlyChip", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToAether
			}).AddModItemToShop(ModConditions.depthsMod, "FlaskofMercury", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "KiPotion1", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "KiPotion2", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "KiPotion3", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "KiPotion4", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "KiPotion5", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.draedonExpansionMod, "FlaskOfElectricity", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee
			}).AddModItemToShop(ModConditions.edorbisMod, "LesserEnergyPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.edorbisMod, "EnergyPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.edorbisMod, "GreaterEnergyPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.edorbisMod, "SuperEnergyPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.calamityEntropyMod, "VoidCandle", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedCruiser
			}).AddModItemToShop(ModConditions.everjadeMod, "JasmineTea", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.everjadeMod, "SpringWater", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.excelsiorMod, "FlaskofEnergy", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.excelsiorMod, "FrostfireFlask", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				ModConditions.DownedNiflheim
			}).AddModItemToShop(ModConditions.excelsiorMod, "MapleSyrup", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "FlaskOfPathogens", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToAvalonEvilBiomes
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "InvincibilityPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "RejuvenationPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LesserStaminaPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "StaminaPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "GreaterStaminaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "SuperStaminaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "StaminaCrystal", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "EnergyCrystal", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToAether
			}).AddModItemToShop(ModConditions.fargosMutantMod, "Omnistation", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.fargosMutantMod, "Omnistation2", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.fargosMutantMod, "Semistation", Item.buyPrice(0, 15, 0, 0)).AddModItemToShop(ModConditions.gerdsLabMod, "Medkit", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "DivineFireFlask", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "PlagueFlask", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "SteelFlask", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "Americano", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				ModConditions.DownedOverwatcher,
				ModConditions.DownedMaterealizer,
				ModConditions.DownedLifebringer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "Cappuccino", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				ModConditions.DownedOverwatcher,
				ModConditions.DownedMaterealizer,
				ModConditions.DownedLifebringer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "Latte", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				ModConditions.DownedOverwatcher,
				ModConditions.DownedMaterealizer,
				ModConditions.DownedLifebringer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "Mocha", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				ModConditions.DownedOverwatcher,
				ModConditions.DownedMaterealizer,
				ModConditions.DownedLifebringer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "AirHandcanon", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				ModConditions.DownedOverwatcher,
				ModConditions.DownedMaterealizer,
				ModConditions.DownedLifebringer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "GreatCrystal", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				ModConditions.DownedOverwatcher,
				ModConditions.DownedMaterealizer,
				ModConditions.DownedLifebringer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "HotCase", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				ModConditions.DownedOverwatcher,
				ModConditions.DownedMaterealizer,
				ModConditions.DownedLifebringer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "WhimInABottle", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				ModConditions.DownedOverwatcher,
				ModConditions.DownedMaterealizer,
				ModConditions.DownedLifebringer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "BushOfLife", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				ModConditions.DownedLifebringer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "LifeLantern", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "UltraHealingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "UltraManaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow
			}).AddModItemToShop(ModConditions.martainsOrderMod, "Endopill", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "Exopill", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "HerbInfusion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "Bisque", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "EnergyDrink", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "FastFood", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "FreshSalad", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "GlowingSalad", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "HerbTea", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "MashedPotatoes", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "Noodles", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "Smoothie", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "WaterBottle", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "NaturePotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "HyperHealingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "HyperManaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "LesserHurtPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "LesserMoilPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "HurtPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "MoilPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "GreaterHurtPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.martainsOrderMod, "GreaterMoilPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.martainsOrderMod, "SuperHurtPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.martainsOrderMod, "SuperMoilPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.martainsOrderMod, "HyperHurtPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "HyperMoilPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "SporeFarm", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.martainsOrderMod, "FirstAidTreatments", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.martainsOrderMod, "FishOfSpirit", Item.buyPrice(0, 15, 0, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "FishOfPurity", Item.buyPrice(0, 15, 0, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "FishOfWrath", Item.buyPrice(0, 15, 0, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "ShimmerFish", Item.buyPrice(0, 15, 0, 0)).AddModItemToShop(ModConditions.redemptionMod, "EvilJelly", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedKeeper
			}).AddModItemToShop(ModConditions.redemptionMod, "BileFlask", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToWasteland
			}).AddModItemToShop(ModConditions.redemptionMod, "NitroglycerineFlask", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.redemptionMod, "EnergyStation", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.DownedMechBossAll,
				ModConditions.HasBeenToLab
			}).AddModItemToShop(ModConditions.redemptionMod, "RevivalPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.redemptionMod, "CrystalSerum", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.redemptionMod, "RadiationPill", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.redemptionMod, "MedicKit", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedPatientZero
			}).AddModItemToShop(ModConditions.redemptionMod, "GalaxyHeart", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedNebuleus
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "AlmondMilk", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "AvocadoSoup", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "Chocolate", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedPirates
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "CoconutMilk", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "CookedMushroom", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "CursedCaviar", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToPyramid
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DigitalCornSyrup", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToPlanetarium
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "FoulConcoction", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "StrawberryIcecream", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DigitalDisplay", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToPlanetarium
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "MegaHealingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedAraghur
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "MegaManaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedAraghur
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "AsthraltiteHealingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedErazor
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "AsthraltiteManaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedErazor
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "AhristaHealingPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedErazor
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "KrysteaManaPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedErazor
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "FruitLantern", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "ManaFruit", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.spiritMod, "Candy", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.spiritMod, "ChocolateBar", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.spiritMod, "HealthCandy", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.spiritMod, "Lollipop", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.spiritMod, "ManaCandy", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.spiritMod, "MysteryCandy", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.spiritMod, "Taffy", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.spiritMod, "CoilEnergizerItem", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.DownedGoblinArmy
			}).AddModItemToShop(ModConditions.spiritMod, "SunPot", Item.buyPrice(0, 15, 0, 0)).AddModItemToShop(ModConditions.spiritMod, "TheCouch", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.spiritMod, "MoonJelly", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedJellyDeluge
			}).AddModItemToShop(ModConditions.spiritMod, "MoonJellyDonut", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedJellyDeluge,
				ModConditions.DownedMysticMoon
			}).AddModItemToShop(ModConditions.starlightRiverMod, "LesserBarrierPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.starlightRiverMod, "RegularBarrierPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedGlassweaver,
				ModConditions.HasBeenToVitricDesert
			}).AddModItemToShop(ModConditions.starlightRiverMod, "GreaterBarrierPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.thoriumMod, "DeepFreezeCoatingItem", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.thoriumMod, "ExplosiveCoatingItem", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.thoriumMod, "GorgonCoatingItem", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "SporeCoatingItem", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				ModConditions.HasBeenToMushroom
			}).AddModItemToShop(ModConditions.thoriumMod, "ToxicCoatingItem", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedQueenBee,
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.thoriumMod, "Altar", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				ModConditions.DownedBuriedChampion
			}).AddModItemToShop(ModConditions.thoriumMod, "ArenaMastersBrazier", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.thoriumMod, "ConductorsStand", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.thoriumMod, "NinjaRack", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.thoriumMod, "TrueArenaMastersBrazier", Item.buyPrice(0, 15, 0, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.thoriumMod, "Mistletoe", Item.buyPrice(0, 15, 0, 0)).AddModItemToShop(ModConditions.thoriumMod, "Jelly", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.thoriumMod, "LifeWater", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.thoriumMod, "MidnightOil", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.thoriumMod, "SpringWater", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToAquaticDepths
			}).AddModItemToShop(ModConditions.thoriumMod, "WaterChestnut", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.HasBeenToAquaticDepths
			}).AddModItemToShop(ModConditions.vitalityMod, "LifeJuice", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.zylonMod, "LesserRestorationPotion", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.zylonMod, "GreaterRestorationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.zylonMod, "SuperRestorationPotion", Item.buyPrice(0, 0, 75, 0), new Condition[]
			{
				ModConditions.DownedLunarEvent
			}).AddModItemToShop(ModConditions.zylonMod, "Tent", Item.buyPrice(0, 0, 75, 0)).AddModItemToShop(ModConditions.zylonMod, "Cabin", Item.buyPrice(0, 0, 75, 0)).Register();
			new NPCShop(base.Type, "Modded Materials").AddModItemToShop(ModConditions.aequusMod, "AtmosphericEnergy", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedGaleStreams,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.aequusMod, "AquaticEnergy", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedCrabson,
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.aequusMod, "BloodyTearstone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.aequusMod, "StariteMaterial", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedGlimmer
			}).AddModItemToShop(ModConditions.aequusMod, "CosmicEnergy", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedGlimmer
			}).AddModItemToShop(ModConditions.aequusMod, "DemonicEnergy", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDemonSiege,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.aequusMod, "Fluorescence", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedRedSprite,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.aequusMod, "FrozenTear", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSpaceSquid,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.aequusMod, "FrozenTechnology", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.aequusMod, "GlowLichen", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.aequusMod, "Hexoplasm", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.aequusMod, "OrganicEnergy", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.aequusMod, "PossessedShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.aequusMod, "UltimateEnergy", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.afkpetsMod, "Bakamite", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedIceQueen
			}).AddModItemToShop(ModConditions.afkpetsMod, "Bitcoin", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.afkpetsMod, "BlueThread", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.afkpetsMod, "BoilingBlood", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.afkpetsMod, "BrokenDollParts", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.afkpetsMod, "BrokenRealityPieceA", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDrFetus
			}).AddModItemToShop(ModConditions.afkpetsMod, "BrokenRealityPieceB", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAncientTrio
			}).AddModItemToShop(ModConditions.afkpetsMod, "BrokenRealityPieceD", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.afkpetsMod, "Circuit", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.afkpetsMod, "DragonsBreath", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedOldOnesArmyT3
			}).AddModItemToShop(ModConditions.afkpetsMod, "EncryptedScroll", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.afkpetsMod, "Flax", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.afkpetsMod, "HolographicShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.afkpetsMod, "KeyPiece1", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.afkpetsMod, "KeyPiece2", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.afkpetsMod, "KeyPiece3", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.afkpetsMod, "MolecularStabilizer", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.afkpetsMod, "Paper", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.afkpetsMod, "Pine", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEverscream
			}).AddModItemToShop(ModConditions.afkpetsMod, "PlantBox", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.afkpetsMod, "PouchofPhoenixAsh", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLavalGolem
			}).AddModItemToShop(ModConditions.afkpetsMod, "PurpleThread", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.afkpetsMod, "RedThread", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.afkpetsMod, "RainbowVial", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.afkpetsMod, "ScrapMetal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSATLA
			}).AddModItemToShop(ModConditions.afkpetsMod, "ShadeCinder", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGoblinArmy
			}).AddModItemToShop(ModConditions.amuletOfManyMinionsMod, "GraniteSpark", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.amuletOfManyMinionsMod, "InertCombatPetFriendshipBow", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.amuletOfManyMinionsMod, "GuideHair", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarEvent
			}).AddModItemToShop(ModConditions.assortedCrazyThingsMod, "ChunkysEyeItem", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.assortedCrazyThingsMod, "CaughtDungeonSoulFreed", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSoulHarvester,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.assortedCrazyThingsMod, "DesiccatedLeather", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSoulHarvester,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.assortedCrazyThingsMod, "DroneParts", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedDestroyer
			}).AddModItemToShop(ModConditions.assortedCrazyThingsMod, "MeatballsEyeItem", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.awfulGarbageMod, "FrostShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.awfulGarbageMod, "Garbage", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.awfulGarbageMod, "SpiderLeg", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.awfulGarbageMod, "SpiritItem", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.awfulGarbageMod, "StormEssence", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedEyeOfTheStorm
			}).AddModItemToShop(ModConditions.blocksArsenalMod, "AncientSpiritCatalyst", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.blocksArsenalMod, "ChargedGranite", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.blocksArsenalMod, "ForgottenHeroShards", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.blocksArsenalMod, "LegendJewel", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.blocksArsenalMod, "LostWhip", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.blocksArsenalMod, "PhantasmalJewel", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.blocksArtificerMod, "MartianScrap", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMartians
			}).AddModItemToShop(ModConditions.blocksArtificerMod, "NovaFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.blocksCoreBossMod, "CrimsonShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.blocksCoreBossMod, "ShadowShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.blocksThrowerMod, "ShatteredFlake", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedIceQueen
			}).AddModItemToShop(ModConditions.blocksThrowerMod, "UmbraCloth", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedEclipse
			}).AddModItemToShop(ModConditions.blocksThrowerMod, "VoidFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.bombusApisMod, "PhotonFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.bombusApisMod, "Pollen", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.calamityMod, "AncientBoneDust", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.calamityMod, "ArmoredShell", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedStormWeaver
			}).AddModItemToShop(ModConditions.calamityMod, "AscendantSpiritEssence", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDevourerOfGods
			}).AddModItemToShop(ModConditions.calamityMod, "AshesofAnnihilation", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSupremeCalamitas
			}).AddModItemToShop(ModConditions.calamityMod, "AshesofCalamity", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedCalamitasClone
			}).AddModItemToShop(ModConditions.calamityMod, "BeetleJuice", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.calamityMod, "BlightedGel", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.calamityMod, "BloodOrb", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.calamityMod, "BloodSample", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPerforators
			}).AddModItemToShop(ModConditions.calamityMod, "Bloodstone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedProvidence
			}).AddModItemToShop(ModConditions.calamityMod, "BloodstoneCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedProvidence
			}).AddModItemToShop(ModConditions.calamityMod, "CoreofCalamity", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.DownedCalamitasClone
			}).AddModItemToShop(ModConditions.calamityMod, "CoreofEleum", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.calamityMod, "CoreofHavoc", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.calamityMod, "CoreofSunlight", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.calamityMod, "CorrodedFossil", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAcidRain2
			}).AddModItemToShop(ModConditions.calamityMod, "DarkPlasma", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedCeaselessVoid
			}).AddModItemToShop(ModConditions.calamityMod, "DarksunFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDevourerOfGods
			}).AddModItemToShop(ModConditions.calamityMod, "DepthCells", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedCalamitasClone
			}).AddModItemToShop(ModConditions.calamityMod, "DemonicBoneAsh", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.calamityMod, "DivineGeode", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedProvidence
			}).AddModItemToShop(ModConditions.calamityMod, "DraedonPowerCell", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.calamityMod, "DubiousPlating", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.calamityMod, "EffulgentFeather", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDragonfolly
			}).AddModItemToShop(ModConditions.calamityMod, "EndothermicEnergy", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDevourerOfGods
			}).AddModItemToShop(ModConditions.calamityMod, "EnergyCore", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.calamityMod, "EssenceofChaos", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCrags
			}).AddModItemToShop(ModConditions.calamityMod, "EssenceofEleum", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.calamityMod, "EssenceofSunlight", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.calamityMod, "ExoPrism", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedExoMechs
			}).AddModItemToShop(ModConditions.calamityMod, "GalacticaSingularity", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.calamityMod, "GrandScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedGreatSandShark
			}).AddModItemToShop(ModConditions.calamityMod, "InfectedArmorPlating", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPlaguebringerGoliath
			}).AddModItemToShop(ModConditions.calamityMod, "LabSeekingMechanism", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.calamityMod, "LivingShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.calamityMod, "Lumenyl", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedCalamitasClone
			}).AddModItemToShop(ModConditions.calamityMod, "MeldBlob", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.calamityMod, "MeldConstruct", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.calamityMod, "MiracleMatter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedExoMechs
			}).AddModItemToShop(ModConditions.calamityMod, "MolluskHusk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.DownedGiantClam
			}).AddModItemToShop(ModConditions.calamityMod, "MurkyPaste", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.calamityMod, "MysteriousCircuitry", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.calamityMod, "Necroplasm", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.calamityMod, "NightmareFuel", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDevourerOfGods
			}).AddModItemToShop(ModConditions.calamityMod, "PearlShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDesertScourge
			}).AddModItemToShop(ModConditions.calamityMod, "PlagueCellCanister", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem
			}).AddModItemToShop(ModConditions.calamityMod, "PlasmaDriveCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.calamityMod, "PurifiedGel", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSlimeGod
			}).AddModItemToShop(ModConditions.calamityMod, "ReaperTooth", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPolterghast
			}).AddModItemToShop(ModConditions.calamityMod, "RottenMatter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedHiveMind
			}).AddModItemToShop(ModConditions.calamityMod, "RuinousSoul", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPolterghast
			}).AddModItemToShop(ModConditions.calamityMod, "SeaRemains", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDesertScourge
			}).AddModItemToShop(ModConditions.calamityMod, "SolarVeil", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.calamityMod, "StarblightSoot", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.calamityMod, "StormlionMandible", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.calamityMod, "SulphuricScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAcidRain1
			}).AddModItemToShop(ModConditions.calamityMod, "SuspiciousScrap", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.calamityMod, "TitanHeart", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.calamityMod, "TrapperBulb", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.calamityMod, "TwistingNether", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSignus
			}).AddModItemToShop(ModConditions.calamityMod, "UnholyCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.calamityMod, "UnholyEssence", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.calamityMod, "WulfrumMetalScrap", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.calamityMod, "YharonSoulFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedYharon
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "ConquestFragment", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "CoreofBabil", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "CoyoteVenom", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "CrocodileScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLeviathanAndAnahita
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "DesertFeather", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDragonfolly
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "EssenceofBabil", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "EssenceofLaw", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "EssenceofZot", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "GrandioseGland", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "GreenDemonHead", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedEclipse
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "OrnateCloth", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDragonfolly
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "ParchedScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDesertScourge
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "SoulFlux", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "SubnauticalPlate", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedEidolonWyrm
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "Theswordisinsidethecore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "UnholyBloodCells", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedCalamitasClone
			}).AddModItemToShop(ModConditions.calamityEntropyMod, "VoidScales", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedCruiser
			}).AddModItemToShop(ModConditions.calamityEntropyMod, "WyrmTooth", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedEidolonWyrm
			}).AddModItemToShop(ModConditions.calamityOverhaulMod, "MurasamaMould", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.calamityOverhaulMod, "DarkMatterBall", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedExoMechs,
				ModConditions.DownedSupremeCalamitas
			}).AddModItemToShop(ModConditions.calamityOverhaulMod, "DecayParticles", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedExoMechs,
				ModConditions.DownedSupremeCalamitas
			}).AddModItemToShop(ModConditions.calamityOverhaulMod, "DecaySubstance", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedExoMechs,
				ModConditions.DownedSupremeCalamitas
			}).AddModItemToShop(ModConditions.calamityOverhaulMod, "DissipationSubstance", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedExoMechs,
				ModConditions.DownedSupremeCalamitas
			}).AddModItemToShop(ModConditions.calamityOverhaulMod, "Flint", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.calamityOverhaulMod, "InfiniteStick", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedExoMechs,
				ModConditions.DownedSupremeCalamitas
			}).AddModItemToShop(ModConditions.calamityOverhaulMod, "InfinityCatalyst", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBossRush,
				ModConditions.DownedExoMechs,
				ModConditions.DownedSupremeCalamitas
			}).AddModItemToShop(ModConditions.calamityOverhaulMod, "Pebble", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.calamityOverhaulMod, "SpectralMatter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedExoMechs,
				ModConditions.DownedSupremeCalamitas
			}).AddModItemToShop(ModConditions.cerebralMod, "BetsyCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedOldOnesArmyT3
			}).AddModItemToShop(ModConditions.cerebralMod, "BlankEmblem", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.cerebralMod, "BrownDwarfFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.cerebralMod, "ChestCrafter", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.cerebralMod, "CommonCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedKingSlime
			}).AddModItemToShop(ModConditions.cerebralMod, "CorruptionMimicCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.cerebralMod, "CrimsonHeartCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.SmashedShadowOrb
			}).AddModItemToShop(ModConditions.cerebralMod, "CrimsonMimicCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.cerebralMod, "DeerclopsCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedDeerclops
			}).AddModItemToShop(ModConditions.cerebralMod, "DukeFishronCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedDukeFishron
			}).AddModItemToShop(ModConditions.cerebralMod, "DungeonChestCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.cerebralMod, "EmpressofLightCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEmpressOfLight
			}).AddModItemToShop(ModConditions.cerebralMod, "EnchantedCrafter", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.cerebralMod, "EpicCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.cerebralMod, "EyeofCthulhuCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.cerebralMod, "FrostLegionCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedFrostLegion
			}).AddModItemToShop(ModConditions.cerebralMod, "FrozenChestCrafter", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.cerebralMod, "GoldenChestCrafter", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.cerebralMod, "GolemCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem
			}).AddModItemToShop(ModConditions.cerebralMod, "GraniteChestCrafter", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.cerebralMod, "GravityFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.cerebralMod, "HallowedMimicCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.cerebralMod, "IceMimicCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.cerebralMod, "IvyChestCrafter", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.cerebralMod, "KingSlimeCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedKingSlime
			}).AddModItemToShop(ModConditions.cerebralMod, "LegendaryCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem
			}).AddModItemToShop(ModConditions.cerebralMod, "LihzardChestCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.cerebralMod, "LivingWoodChestCrafter", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.cerebralMod, "MarbleChestCrafter", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.cerebralMod, "MartianMadnessCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMartians
			}).AddModItemToShop(ModConditions.cerebralMod, "MimicCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.cerebralMod, "MoonLordCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.cerebralMod, "MushroomChestCrafter", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.cerebralMod, "NightCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedDeerclops
			}).AddModItemToShop(ModConditions.cerebralMod, "PirateInvasionCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPirates
			}).AddModItemToShop(ModConditions.cerebralMod, "PlanteraCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.cerebralMod, "PulsarFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.cerebralMod, "QueenSlimeCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedQueenSlime
			}).AddModItemToShop(ModConditions.cerebralMod, "RareCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedQueenSlime
			}).AddModItemToShop(ModConditions.cerebralMod, "RelicCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.cerebralMod, "SandstoneChestCrafter", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.cerebralMod, "ShadowChestCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.cerebralMod, "ShadowflameCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.cerebralMod, "ShadowOrbCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.SmashedShadowOrb
			}).AddModItemToShop(ModConditions.cerebralMod, "SkywareChestCrafter", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.cerebralMod, "StellarwindFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.cerebralMod, "UncommonCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.cerebralMod, "VeilFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.cerebralMod, "WallofFleshCrafter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.cerebralMod, "WaterChestCrafter", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.cerebralMod, "WebCoveredChestCrafter", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.cerebralMod, "WyvernScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.clamityAddonMod, "ClamitousPearl", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedClamitas
			}).AddModItemToShop(ModConditions.clamityAddonMod, "CoreOfFlame", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.clamityAddonMod, "EssenceOfFlame", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.clamityAddonMod, "HuskOfCalamity", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedClamitas
			}).AddModItemToShop(ModConditions.ruptureMod, "RiftFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.clickerClassMod, "MiceFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "CanofMeat", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "CookieDough", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToConfectionOrHallow
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "SoulofDelight", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToConfectionOrHallow
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "SoulofSpite", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "Sprinkles", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToConfectionOrHallow
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "YumDrop", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToConfectionOrHallow
			}).AddModItemToShop(ModConditions.consolariaMod, "RainbowPiece", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.consolariaMod, "SoulofBlight", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOcram
			}).AddModItemToShop(ModConditions.consolariaMod, "WhiteThread", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.coraliteMod, "AncientCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedEclipse
			}).AddModItemToShop(ModConditions.coraliteMod, "AncientGemstone", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.coraliteMod, "BloodyOrb", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.coraliteMod, "ChippedBlade", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.coraliteMod, "ClearGemstone", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.coraliteMod, "CrystallineMagike", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.coraliteMod, "DukeFishronSkin", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedDukeFishron
			}).AddModItemToShop(ModConditions.coraliteMod, "EmpyrosPowder", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.coraliteMod, "FlyingSnakeFeather", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.coraliteMod, "FragmentsOfLight", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEmpressOfLight
			}).AddModItemToShop(ModConditions.coraliteMod, "FrostCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBabyIceDragon
			}).AddModItemToShop(ModConditions.coraliteMod, "HeatanInABottle", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.coraliteMod, "IcicleCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBabyIceDragon
			}).AddModItemToShop(ModConditions.coraliteMod, "LeafStone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.coraliteMod, "MagicalPowder", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.coraliteMod, "MagicCrystal", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.coraliteMod, "RemainsOfSamurai", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.coraliteMod, "RustedShield", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedEclipse
			}).AddModItemToShop(ModConditions.coraliteMod, "ShadowCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.coraliteMod, "ShadowEnergy", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.coraliteMod, "SoulOfDeveloper", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.coraliteMod, "WoodStick", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.coraliteMod, "ZapCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedThunderveinDragon
			}).AddModItemToShop(ModConditions.depthsMod, "DiamondDust", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.depthsMod, "Ember", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.depthsMod, "Geode", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "AngerKiCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "AstralEssentia", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedQueenBee
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "CalmKiCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "DemonicSoul", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "DivineThreads", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "EarthenShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "EmptyNecklace", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "KatchinScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedDukeFishron
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "PridefulKiCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "PureKiCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "RadiantFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedCultist,
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "RefinedMetal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "SatanicCloth", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "ScrapMetal", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "SkeletalEssence", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "SoulofEntity", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "StableKiCrystal", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "BetsyScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedOldOnesArmyT3
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Broken_Hero_GunParts", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "CometFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedCreation
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "CorruptShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Cosmic_Essence", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Crimson_Stone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Divine_Fragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Duskbulb", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDestruction
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Enkin", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDestruction
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "CosmicFabric", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDestruction
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "GenocideCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Hallow_Stone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Hell_Stone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Ice_Stone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "InfinityCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDestruction,
				ModConditions.DownedCreation
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "InfinityGeode", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedCreation
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Jungle_Stone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "LunarSilk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Purity_Stone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Relic_Fragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Sand_Stone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "SingularityCatalyst", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedCreation
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Stardust", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDestruction
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "SunstruckEssence", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedGalahis
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Tungqua", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedCreation
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Underground_Stone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Water_Stone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Wyvernscale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.edorbisMod, "Battery", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.edorbisMod, "BlobMatter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.edorbisMod, "BrokenDamocles", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedGlaciation
			}).AddModItemToShop(ModConditions.edorbisMod, "CentauriumBattery", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.edorbisMod, "ChlorophyteWire", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.edorbisMod, "DarkDust", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.edorbisMod, "DiscordShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.edorbisMod, "ElectrostaticSilk", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.edorbisMod, "FishronScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedDukeFishron
			}).AddModItemToShop(ModConditions.edorbisMod, "GlaciationPlating", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedGlaciation
			}).AddModItemToShop(ModConditions.edorbisMod, "HighTechSalvages", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.edorbisMod, "MetalSalvages", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGoblinArmy
			}).AddModItemToShop(ModConditions.edorbisMod, "MetalWire", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.edorbisMod, "SoulOfFight", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedCursePreacher
			}).AddModItemToShop(ModConditions.everjadeMod, "JadeChunk", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.everjadeMod, "LotusFiber", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.everjadeMod, "SpineItem", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.everjadeMod, "JasmineFlower", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exaltMod, "DragonScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.exaltMod, "IceCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.exaltMod, "Leaf", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exaltMod, "Membrane", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exaltMod, "Paper", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exaltMod, "Remnant", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedIceLich
			}).AddModItemToShop(ModConditions.exaltMod, "TwistedFlesh", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon,
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.exaltMod, "Vescon", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.excelsiorMod, "BlackholeFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.excelsiorMod, "Blood", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.excelsiorMod, "DarksteelScraps", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.excelsiorMod, "ElegantFlower", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.excelsiorMod, "EnergizedGranite", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.excelsiorMod, "ShatteredHeartbeat", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.excelsiorMod, "StellarPlating", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedStellarStarship
			}).AddModItemToShop(ModConditions.excelsiorMod, "WyvernScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Beak", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BlankScroll", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BloodshotLens", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Booger", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAvalonEvilBosses
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BottledLava", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BrokenVigilanteTome", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.DownedEclipse
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "WildMushroom", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ChaosDust", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "DesertFeather", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDesertBeak
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "FakeFourLeafClover", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "FleshyTendril", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "FourLeafClover", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "GhostintheMachine", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPhantasm
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LifeDew", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "MosquitoProboscis", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Pathogen", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToAvalonEvilBiomes
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "RottenFlesh", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "SoulofDelight", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "SoulofIce", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "SulphurCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "YuckyBit", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToAvalonEvilBiomes
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ArcaneShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BlastShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BreezeShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "CoreShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "DemonicShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "EarthShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ElementShard", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "CorruptShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToAvalonEvilBiomes
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "FireShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "FrigidShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "FrostShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "SacredShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "TornadoShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "TorrentShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ToxinShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "UndeadShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "VenomShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "WaterShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "WickedShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToAvalonEvilBiomes
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "CarbonSteel", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "DewofHerbs", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "DewOrb", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "DragonOrb", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ElementDiamond", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ElementDust", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "FineLumber", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Gravel", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "MysticalClaw", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "MysticalTomePage", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "MysticalTotem", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "RubybeadHerb", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Sandstone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ScrollofTome", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "StrongVenom", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.fargosSoulsMod, "AbomEnergy", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAbominationn
			}).AddModItemToShop(ModConditions.fargosSoulsMod, "DeviatingEnergy", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDeviantt
			}).AddModItemToShop(ModConditions.fargosSoulsMod, "Eridanium", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedEridanus
			}).AddModItemToShop(ModConditions.fargosSoulsMod, "EternalEnergy", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedMutant
			}).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "Cryogem", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "FrostjawShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "Hemotite", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToDread
			}).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "NeophyteShard", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "TerrorCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToDread
			}).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "VivifiedWood", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "WeirdlyColoredPetal", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.gameTerrariaMod, "ChargedScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedHornlitz
			}).AddModItemToShop(ModConditions.gameTerrariaMod, "InertPiece", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.gameTerrariaMod, "SoldierMaterial", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.gensokyoMod, "KappaTech", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.gensokyoMod, "YamawaroTech", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.gensokyoMod, "PointItem", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.gensokyoMod, "PowerItem", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLilyWhite
			}).AddModItemToShop(ModConditions.gerdsLabMod, "AlloyBox", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.gerdsLabMod, "AncientInfraRedPlating", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAcheron
			}).AddModItemToShop(ModConditions.gerdsLabMod, "HardmodeUpgradeCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.gerdsLabMod, "SpecialUpgradeCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.gerdsLabMod, "BossUpgradeCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.gerdsLabMod, "UpgradeCrystal", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.gerdsLabMod, "InfraRedBar", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAcheron
			}).AddModItemToShop(ModConditions.gerdsLabMod, "InfraRedCrystalShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAcheron
			}).AddModItemToShop(ModConditions.gerdsLabMod, "PrimeEnhancementModule", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.gerdsLabMod, "PrimePlating", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.gerdsLabMod, "ScrapFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAcheron
			}).AddModItemToShop(ModConditions.heartbeatariaMod, "DreadFangs", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.heartbeatariaMod, "FilthySap", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.heartbeatariaMod, "FusionModule", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.heartbeatariaMod, "GelOfCthulhu", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.heartbeatariaMod, "GreenScales", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.heartbeatariaMod, "MagmaShell", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "AbyssFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDiver
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "AnglerCoin", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "AnglerGoldCoin", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "Blood", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "CoffeeBean_1", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOverwatcher,
				ModConditions.DownedMaterealizer,
				ModConditions.DownedLifebringer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "CoffeeBean_2", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOverwatcher,
				ModConditions.DownedMaterealizer,
				ModConditions.DownedLifebringer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "DenseIcicle", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "DivineShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "EssenceofBright", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSon
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "EssenceofLife", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "EssenceofMatter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "EssenceofNothingness", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedScarabBelief
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "EssenceofTime", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "EssenceofDeath", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedWorldsEndWhale
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "FluorescentFibre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToHomewardAbyss
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "JungleDewdrop", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "NetherStar", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "SolarFlareScoria", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSunSlimeGod
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "SoulofBlight", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedMotherbrain
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "SpiralTissue", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "SteelFeather", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "SunlightGel", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "CoffeeBean_3", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOverwatcher,
				ModConditions.DownedMaterealizer,
				ModConditions.DownedLifebringer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "TankOfThePastCave", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOrdealAny
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "TankOfThePastCorruption", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOrdealAny
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "TankOfThePastCrimson", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOrdealAny
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "TankOfThePastDesert", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOrdealAny
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "TankOfThePastForest", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOrdealAny
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "TankOfThePastHallow", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOrdealAny
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "TankOfThePastJungle", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOrdealAny
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "TankOfThePastSky", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOrdealAny
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "TankOfThePastSnowland", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOrdealAny
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "TankOfThePastUnderworld", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOrdealAny
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "TrueJungleSpore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "CoffeeBean_4", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOverwatcher,
				ModConditions.DownedMaterealizer,
				ModConditions.DownedLifebringer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "WillToCorrode", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOverwatcher,
				ModConditions.DownedMaterealizer,
				ModConditions.DownedLifebringer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "WillToCrown", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOverwatcher,
				ModConditions.DownedMaterealizer,
				ModConditions.DownedLifebringer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "WillToGrow", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOverwatcher,
				ModConditions.DownedMaterealizer,
				ModConditions.DownedLifebringer
			}).AddModItemToShop(ModConditions.huntOfTheOldGodMod, "ChromaticMass", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedGoozma
			}).AddModItemToShop(ModConditions.lunarVeilMod, "AlcadizMetal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedCommanderGintzia
			}).AddModItemToShop(ModConditions.lunarVeilMod, "AlcaricMush", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.lunarVeilMod, "AuroreanStarI", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSTARBOMBER
			}).AddModItemToShop(ModConditions.lunarVeilMod, "BlankRune", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.lunarVeilMod, "BasicGunParts", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "BrokenTech", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToGovheilCastle
			}).AddModItemToShop(ModConditions.lunarVeilMod, "CondensedDirt", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "GraftedSoul", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.lunarVeilMod, "ConvulgingMater", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "DarkEssence", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.lunarVeilMod, "DreadFoil", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDreadMire
			}).AddModItemToShop(ModConditions.lunarVeilMod, "Bagitem", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "DustedSilk", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "EldritchSoul", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.lunarVeilMod, "EmptyMoonflameLantern", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "AlcadizScrap", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "FlowerBatch", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "Ivythorn", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "MetallicOmniSource", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "MiracleThread", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSylia
			}).AddModItemToShop(ModConditions.lunarVeilMod, "MoltenScrap", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "MorrowVine", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "Morrowshroom", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "Mushroom", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "PearlescentScrap", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedVerlia
			}).AddModItemToShop(ModConditions.lunarVeilMod, "RippedFabric", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.lunarVeilMod, "Cinderscrap", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "STARCORE", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSTARBOMBER
			}).AddModItemToShop(ModConditions.lunarVeilMod, "Starrdew", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.lunarVeilMod, "StarSilk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.lunarVeilMod, "Stick", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "TerrorFragments", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.lunarVeilMod, "UnknownCircuitry", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "VirulentPlating", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.lunarVeilMod, "VoidLantern", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSingularityFragment
			}).AddModItemToShop(ModConditions.lunarVeilMod, "WanderingFlame", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPumpkinJack
			}).AddModItemToShop(ModConditions.lunarVeilMod, "WinterbornShard", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.magicStorageMod, "ShadowDiamond", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.magicStorageMod, "RadiantJewel", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.martainsOrderMod, "AstralFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.martainsOrderMod, "WillowFiber", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "BionicMass", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.martainsOrderMod, "BrokenSundialShards", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.martainsOrderMod, "BrownBark", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "CamoCloth", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.martainsOrderMod, "ChromaFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.martainsOrderMod, "CursedIchor", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedCarnagePillar
			}).AddModItemToShop(ModConditions.martainsOrderMod, "DustyGarment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.martainsOrderMod, "ForestEssence", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "FractalFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.martainsOrderMod, "IceCubes", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBritzz
			}).AddModItemToShop(ModConditions.martainsOrderMod, "IceCrystal", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "JungleBattery", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedJungleDefenders
			}).AddModItemToShop(ModConditions.martainsOrderMod, "LunarConglomeration", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.martainsOrderMod, "LunarLens", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedTriplets
			}).AddModItemToShop(ModConditions.martainsOrderMod, "MachinedTechCap", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedTheAlchemist
			}).AddModItemToShop(ModConditions.martainsOrderMod, "MarineChemicals", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "NaiveSoul", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "ReptileScales", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "Shadowflame", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGoblinArmy
			}).AddModItemToShop(ModConditions.martainsOrderMod, "VoidSoul", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "TealGel", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPrinceSlime
			}).AddModItemToShop(ModConditions.martainsOrderMod, "TectonicPlate", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "TimeFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.martainsOrderMod, "VoidCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "VoidScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "WhiteGel", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPrinceSlime
			}).AddModItemToShop(ModConditions.martainsOrderMod, "WoodSplinter", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.mechReworkMod, "SoulofFreight", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSt4sys
			}).AddModItemToShop(ModConditions.mechReworkMod, "SoulofPlight", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedTerminator
			}).AddModItemToShop(ModConditions.mechReworkMod, "SoulofDight", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedCaretaker
			}).AddModItemToShop(ModConditions.mechReworkMod, "SoulofTight", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSiegeEngine
			}).AddModItemToShop(ModConditions.medialRiftMod, "ArtificialFin", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSuperVoltaicMotherSlime
			}).AddModItemToShop(ModConditions.medialRiftMod, "ArtificialScrap", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSuperVoltaicMotherSlime
			}).AddModItemToShop(ModConditions.medialRiftMod, "BlankSpell", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.medialRiftMod, "BrilliantAmpule", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.medialRiftMod, "GlimmerGel", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedQueenSlime
			}).AddModItemToShop(ModConditions.medialRiftMod, "MatterSpikes", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.medialRiftMod, "PrimordialAlloy", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.medialRiftMod, "StarsCloud", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.medialRiftMod, "ViriumMatter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.medialRiftMod, "WaterStar", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.metroidMod, "EnergyShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedTorizo
			}).AddModItemToShop(ModConditions.metroidMod, "FrozenCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedIceQueen
			}).AddModItemToShop(ModConditions.metroidMod, "GravityFlare", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPhantoon
			}).AddModItemToShop(ModConditions.metroidMod, "KraidTissue", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedKraid
			}).AddModItemToShop(ModConditions.metroidMod, "NightmareCoreX", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedNightmare
			}).AddModItemToShop(ModConditions.metroidMod, "NightmareCoreXFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedNightmare
			}).AddModItemToShop(ModConditions.metroidMod, "PurePhazon", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOmegaPirate
			}).AddModItemToShop(ModConditions.metroidMod, "SerrisCoreX", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSerris
			}).AddModItemToShop(ModConditions.metroidMod, "UnknownPlasmaBeam", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedKraid
			}).Register();
			new NPCShop(base.Type, "Modded Materials 2").AddModItemToShop(ModConditions.polaritiesMod, "AlkalineFluid", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.polaritiesMod, "CongealedBrine", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.polaritiesMod, "EvilDNA", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedEsophage
			}).AddModItemToShop(ModConditions.polaritiesMod, "LimestoneCarapace", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.polaritiesMod, "Rattle", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.polaritiesMod, "SaltCrystals", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.polaritiesMod, "SerpentScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.polaritiesMod, "StormChunk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedStormCloudfish
			}).AddModItemToShop(ModConditions.polaritiesMod, "Tentacle", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.polaritiesMod, "VenomGland", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.polaritiesMod, "WandererPlating", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedConvectiveWanderer
			}).AddModItemToShop(ModConditions.projectZeroMod, "FlanricCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.projectZeroMod, "PureSoulofPower", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedArmagem
			}).AddModItemToShop(ModConditions.projectZeroMod, "ScarletFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.projectZeroMod, "SlushofFate", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.projectZeroMod, "SpaceCatalyst", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarEvent
			}).AddModItemToShop(ModConditions.qwertyMod, "CraftingRune", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedRuneGhost
			}).AddModItemToShop(ModConditions.qwertyMod, "Etims", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedNoehtnap
			}).AddModItemToShop(ModConditions.qwertyMod, "FortressHarpyBeak", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSkyFortress
			}).AddModItemToShop(ModConditions.qwertyMod, "HydraScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedHydra
			}).AddModItemToShop(ModConditions.qwertyMod, "InvaderPlating", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedInvaders
			}).AddModItemToShop(ModConditions.qwertyMod, "CaeliteCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDivineLight
			}).AddModItemToShop(ModConditions.qwertyMod, "SoulOfHeight", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedInvaders
			}).AddModItemToShop(ModConditions.redemptionMod, "AIChip", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.redemptionMod, "Archcloth", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.redemptionMod, "CarbonMyofibre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.redemptionMod, "Capacitator", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.redemptionMod, "CoastScarabShell", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.redemptionMod, "CyberPlating", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedKingSlayerIII
			}).AddModItemToShop(ModConditions.redemptionMod, "EmptyMutagen", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.redemptionMod, "GildedStar", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAncientDeityDuo
			}).AddModItemToShop(ModConditions.redemptionMod, "GrimShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedKeeper
			}).AddModItemToShop(ModConditions.redemptionMod, "LifeFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedNebuleus
			}).AddModItemToShop(ModConditions.redemptionMod, "LivingTwig", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.redemptionMod, "LostSoul", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.redemptionMod, "MoonflareFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).AddModItemToShop(ModConditions.redemptionMod, "RoboBrain", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOmegaObliterator
			}).AddModItemToShop(ModConditions.redemptionMod, "OphosNotes", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem
			}).AddModItemToShop(ModConditions.redemptionMod, "OmegaPowerCell", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOmegaGigapora
			}).AddModItemToShop(ModConditions.redemptionMod, "Plating", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.redemptionMod, "ZweihanderFragment2", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.redemptionMod, "ZweihanderFragment1", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.redemptionMod, "ToxicBile", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToWasteland
			}).AddModItemToShop(ModConditions.redemptionMod, "TreeBugShell", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.remnantsMod, "exoticgold", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPirates
			}).AddModItemToShop(ModConditions.remnantsMod, "coinmoldgold", Item.buyPrice(0, 1, 0, 0)).AddModItemToShop(ModConditions.remnantsMod, "coinmoldplatinum", Item.buyPrice(1, 0, 0, 0)).AddModItemToShop(ModConditions.remnantsMod, "metalscraps", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "CorrosiveGel", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPutridPinky
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "CursedMatter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPharaohsCurse,
				ModConditions.HasBeenToPyramid
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DissolvingAether", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAdvisor,
				ModConditions.HasBeenToPlanetarium
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DissolvingAurora", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPermafrostSpirit,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DissolvingBrilliance", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLux,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DissolvingDeluge", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedTidalSpirit,
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DissolvingEarth", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedEarthenSpirit,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DissolvingNature", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedNatureSpirit
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DissolvingNether", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedInfernoSpirit,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DissolvingUmbra", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedEvilSpirit,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "FragmentOfChaos", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "FragmentOfEarth", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "FragmentOfEvil", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "FragmentOfInferno", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "FragmentOfNature", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "FragmentOfOtherworld", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToPlanetarium
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "FragmentOfPermafrost", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "FragmentOfTide", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "GlowNylon", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedGlowmoth
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "HelicopterParts", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEverscream,
				Condition.DownedSantaNK1,
				Condition.DownedIceQueen
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "Peanut", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "SanguiteBar", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSubspaceSerpent
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "SkipShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPharaohsCurse
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "SkipSoul", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPharaohsCurse
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "Snakeskin", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToPyramid
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "SoulOfPlight", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPolaris
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "SoulResidue", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToPyramid
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "TwilightGel", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToPlanetarium
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "TwilightShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAdvisor
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "VialofAcid", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPutridPinky
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "TabletOfWeaponMastery", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedErazor
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "AsthraltitePowerCell", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedErazor
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "BlazePowder", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedZombiePiglinBrute
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "BlazeRod", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedZombiePiglinBrute
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "Blightbone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedFlamingPumpkin,
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "ArcticFur", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDecree
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "DesertCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "DreadFlame", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedFlamingPumpkin
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "EldritchSpark", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "EmberOfOmen", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedNihilus
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "ForgottenCrown", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "ForgottenCrystalBattery", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "ForgottenTablet", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "FieryExtract", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAraghur
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "FlareEssentia", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "FlariumShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAraghur
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "FragmentBlight", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAraghur
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "FragmentHatred", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAraghur
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "FrigidOoze", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "LifeEmber", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "LuminousEnergy", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLostSiblings
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "MarstechChip", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedMartianSaucer
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "SoulOfTide", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedDukeFishron
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "JellyNebula", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "PrimordialRune", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPrimordia
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "FragmentQuasar", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "JellyQuasar", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "TabletFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedErazor
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "DarkMist", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "SpaceJunk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "JellySolar", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "JellyStardust", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "GrandHarpyFeather", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedJensenTheGrandHarpy
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "TraceOfChaos", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "Tumblestick", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "VenomSample", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAraneas
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "GoldenFeather", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedHarpyQueenRaynare
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "JellyVortex", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "BirdFeather", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.spiritMod, "Rune", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToSpirit
			}).AddModItemToShop(ModConditions.spiritMod, "ArcaneGeyser", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAtlas
			}).AddModItemToShop(ModConditions.spiritMod, "MoonStone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedMysticMoon
			}).AddModItemToShop(ModConditions.spiritMod, "BismiteCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.spiritMod, "Brightbulb", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.spiritMod, "Chitin", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedScarabeus
			}).AddModItemToShop(ModConditions.spiritMod, "DeepCascadeShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.spiritMod, "SynthMaterial", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGoblinArmy
			}).AddModItemToShop(ModConditions.spiritMod, "DuskStone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDusking
			}).AddModItemToShop(ModConditions.spiritMod, "DreamstrideEssence", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.spiritMod, "EmptyCodex", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.spiritMod, "StarEnergy", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.spiritMod, "FrigidFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.spiritMod, "GranitechMaterial", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.spiritMod, "HeartScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.spiritMod, "IridescentScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.spiritMod, "NetherCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToSpirit
			}).AddModItemToShop(ModConditions.spiritMod, "OldLeather", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.spiritMod, "CarvedRock", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.spiritMod, "InfernalAppendage", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedInfernon
			}).AddModItemToShop(ModConditions.spiritMod, "TribalScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedTide
			}).AddModItemToShop(ModConditions.spiritMod, "SoulShred", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToSpirit
			}).AddModItemToShop(ModConditions.spiritMod, "StrippedBamboo", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.spiritMod, "SulfurDeposit", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.spiritMod, "TechDrive", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGoblinArmy
			}).AddModItemToShop(ModConditions.spookyMod, "ArteryPiece", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOrroBoro,
				ModConditions.HasBeenToEyeValley
			}).AddModItemToShop(ModConditions.spookyMod, "RottenChunk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedRotGourd,
				ModConditions.HasBeenToSpookyForest
			}).AddModItemToShop(ModConditions.spookyMod, "CreepyChunk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToEyeValley
			}).AddModItemToShop(ModConditions.spookyMod, "PlantChunk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDaffodil,
				ModConditions.HasBeenToCatacombs
			}).AddModItemToShop(ModConditions.spookyMod, "SentientHeart", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedMoco,
				ModConditions.HasBeenToEyeValley
			}).AddModItemToShop(ModConditions.spookyMod, "SnotGlob", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedMoco,
				ModConditions.HasBeenToEyeValley
			}).AddModItemToShop(ModConditions.spookyMod, "SpiderChitin", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSpiderCave
			}).AddModItemToShop(ModConditions.spookyMod, "SpookyGlowshroom", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSpookyUnderground
			}).AddModItemToShop(ModConditions.starlightRiverMod, "SandstoneChunk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToVitricDesert
			}).AddModItemToShop(ModConditions.starlightRiverMod, "AncientGear", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.starlightRiverMod, "Astroscrap", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedGlassweaver,
				ModConditions.HasBeenToVitricDesert
			}).AddModItemToShop(ModConditions.starlightRiverMod, "BasicInfusion", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedGlassweaver,
				ModConditions.HasBeenToVitricDesert
			}).AddModItemToShop(ModConditions.starlightRiverMod, "ChargedMagnet", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.starlightRiverMod, "ExoticGeodeArtifactItem", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.starlightRiverMod, "GlassIdol", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedGlassweaver,
				ModConditions.HasBeenToVitricDesert
			}).AddModItemToShop(ModConditions.starlightRiverMod, "GloomGel", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.starlightRiverMod, "InertStaff", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.starlightRiverMod, "LivingBlood", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.starlightRiverMod, "MagmaCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedCeiros,
				ModConditions.HasBeenToVitricTemple
			}).AddModItemToShop(ModConditions.starlightRiverMod, "Slimeberry", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.starlightRiverMod, "StaminaGel", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedGlassweaver,
				ModConditions.HasBeenToVitricDesert
			}).AddModItemToShop(ModConditions.starlightRiverMod, "VengefulSpirit", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.starlightRiverMod, "VitricOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedGlassweaver,
				ModConditions.HasBeenToVitricDesert
			}).AddModItemToShop(ModConditions.starsAboveMod, "BandedTenebrium", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.starsAboveMod, "EnigmaticDust", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedVagrantofSpace
			}).AddModItemToShop(ModConditions.starsAboveMod, "InertShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.starsAboveMod, "PrismaticCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedVagrantofSpace
			}).AddModItemToShop(ModConditions.starsAboveMod, "StellarRemnant", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "SpaceRock", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "BloodDrop", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "CrackedHeart", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "ChaosShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "DerplingShell", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "IceOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "DesertOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "GraniteCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "SoulFire", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAll,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "BlueCloth", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "SantankScrap", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSantaNK1
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "RedSilk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.stramsClassesMod, "FineSilk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.stramsClassesMod, "IceSpike", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.stramsClassesMod, "ReinforcedPlate", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.supernovaMod, "BloodShards", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.supernovaMod, "BoneFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBloodweaver
			}).AddModItemToShop(ModConditions.supernovaMod, "FirearmManual", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.supernovaMod, "GoldenRingMold", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.supernovaMod, "QuarionShard", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.supernovaMod, "Rime", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedFlyingTerror
			}).AddModItemToShop(ModConditions.supernovaMod, "TerrorTuft", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedFlyingTerror
			}).AddModItemToShop(ModConditions.terrorbornMod, "DreadfulEssence", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.terrorbornMod, "FusionFragment", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.terrorbornMod, "HellbornEssence", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.terrorbornMod, "HexingEssence", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.terrorbornMod, "IncendiusAlloy", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.terrorbornMod, "NoxiousScale", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.terrorbornMod, "SanguineFang", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.terrorbornMod, "ShellFragments", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.terrorbornMod, "SoulOfPlight", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.terrorbornMod, "TerrorSample", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.terrorbornMod, "ThunderShard", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.terrorbornMod, "TorturedEssence", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.terrorbornMod, "TarOfHunger", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.thoriumMod, "AbyssalChitin", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToAquaticDepths
			}).AddModItemToShop(ModConditions.thoriumMod, "BioMatter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.thoriumMod, "BirdTalon", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.thoriumMod, "Blood", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.thoriumMod, "BloomWeave", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.thoriumMod, "BrokenHeroFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.DownedEclipse
			}).AddModItemToShop(ModConditions.thoriumMod, "BronzeFragments", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "CelestialFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.thoriumMod, "CeruleanMorel", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToMushroom
			}).AddModItemToShop(ModConditions.thoriumMod, "CursedCloth", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLich
			}).AddModItemToShop(ModConditions.thoriumMod, "DarkMatter", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.thoriumMod, "DeathEssence", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPrimordials
			}).AddModItemToShop(ModConditions.thoriumMod, "DemonBloodShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.thoriumMod, "DepthScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToAquaticDepths
			}).AddModItemToShop(ModConditions.thoriumMod, "DreadSoul", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.thoriumMod, "Geode", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.thoriumMod, "GraniteEnergyCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "GreenDragonScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.thoriumMod, "HallowedCharm", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.thoriumMod, "HolyKnightsAlloy", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.thoriumMod, "IcyShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.thoriumMod, "InfernoEssence", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPrimordials
			}).AddModItemToShop(ModConditions.thoriumMod, "LifeCell", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.thoriumMod, "LivingLeaf", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.thoriumMod, "OceanEssence", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPrimordials
			}).AddModItemToShop(ModConditions.thoriumMod, "Petal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.thoriumMod, "PharaohsBreath", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.thoriumMod, "PurityShards", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.thoriumMod, "ShootingStarFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.thoriumMod, "StrangeAlienTech", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.thoriumMod, "StrangePlating", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.thoriumMod, "SolarPebble", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToTemple
			}).AddModItemToShop(ModConditions.thoriumMod, "SoulofPlight", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.thoriumMod, "SpiritDroplet", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.thoriumMod, "TerrariumCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.thoriumMod, "UnfathomableFlesh", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.thoriumMod, "UnholyShards", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.thoriumMod, "WhiteDwarfFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.traeMod, "DriedRose", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.traeMod, "IceQueenJewel", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedIceQueen
			}).AddModItemToShop(ModConditions.traeMod, "GraniteBattery", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedGraniteOvergrowth,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.traeMod, "LuminiteFeather", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.traeMod, "MagicalAsh", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.traeMod, "ObsidianScale", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.traeMod, "SalamanderTail", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.uhtricMod, "AncientLeaf", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.uhtricMod, "Charcoal", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.uhtricMod, "CrabChitin", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.uhtricMod, "DiseasedBlood", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.uhtricMod, "GalacticGel", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.uhtricMod, "NibblersEye", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.uhtricMod, "PufferSpike", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.uhtricMod, "RoachShell", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.uhtricMod, "WaspLegs", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.universeOfSwordsMod, "SwordOfPower", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.universeOfSwordsMod, "LunarOrb", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.universeOfSwordsMod, "MartianSaucerCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMartians
			}).AddModItemToShop(ModConditions.universeOfSwordsMod, "ScarletFlareCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.universeOfSwordsMod, "SwordMatter", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.valhallaMod, "GargoyleFeather", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.valhallaMod, "BrokenGlaive", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.DownedEclipse
			}).AddModItemToShop(ModConditions.valhallaMod, "BrokenGranitbur", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.valhallaMod, "BrokenPigronWing", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.valhallaMod, "BrokenSpear", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.DownedEclipse
			}).AddModItemToShop(ModConditions.valhallaMod, "DamagedBook", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.valhallaMod, "EvilIngot", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.valhallaMod, "HiveIngot", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem
			}).AddModItemToShop(ModConditions.valhallaMod, "JadeCloth", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedYurnero
			}).AddModItemToShop(ModConditions.valhallaMod, "PureGoldChunk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPirates
			}).AddModItemToShop(ModConditions.valhallaMod, "ToxicGel", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.verdantMod, "ApotheoticSoul", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAll,
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.verdantMod, "Lightbulb", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.verdantMod, "LushLeaf", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.verdantMod, "MysteriaClump", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.verdantMod, "PinkPetal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.verdantMod, "PuffMaterial", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.verdantMod, "RedPetal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.verdantMod, "WisplantItem", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.verdantMod, "YellowBulb", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.vitalityMod, "ArcaneGoldShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.vitalityMod, "Argonite", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.vitalityMod, "BloodCandy", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.vitalityMod, "BloodSac", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedMosquitoMonarch
			}).AddModItemToShop(ModConditions.vitalityMod, "BloodVial", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.vitalityMod, "BrokenScubaHelmet", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).AddModItemToShop(ModConditions.vitalityMod, "BrokenScubaLeggings", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).AddModItemToShop(ModConditions.vitalityMod, "BrokenScubaSuit", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).AddModItemToShop(ModConditions.vitalityMod, "BronzeShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.vitalityMod, "ChaosCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.vitalityMod, "ChaosDust", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.vitalityMod, "Charcoal", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.vitalityMod, "CloudVapor", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedStormCloud
			}).AddModItemToShop(ModConditions.vitalityMod, "DarkLeather", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.vitalityMod, "Ectosoul", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPaladinSpirit,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.vitalityMod, "EquityCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPaladinSpirit,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.vitalityMod, "EssenceofFire", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.vitalityMod, "EssenceofFrost", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.vitalityMod, "ForbiddenFeather", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.vitalityMod, "GlacialChunk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.vitalityMod, "GlowingGranitePowder", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.vitalityMod, "Glowshroom", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToMushroom
			}).AddModItemToShop(ModConditions.vitalityMod, "Heliumite", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.vitalityMod, "Kryptonite", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.vitalityMod, "Lavanite", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.vitalityMod, "LivingStick", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.vitalityMod, "MartianScrap", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMartians
			}).AddModItemToShop(ModConditions.vitalityMod, "Neonite", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.vitalityMod, "Paper", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.vitalityMod, "PurifiedSpore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.vitalityMod, "SanguineVial", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.vitalityMod, "ShiverFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEverscream,
				Condition.DownedSantaNK1,
				Condition.DownedIceQueen
			}).AddModItemToShop(ModConditions.vitalityMod, "SoulofVitality", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.vitalityMod, "TornPage", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.vitalityMod, "Xenonite", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.wayfairContentMod, "LivingLeaf", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.wayfairContentMod, "MagicShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedManaflora
			}).AddModItemToShop(ModConditions.wayfairContentMod, "ParafalconFeather", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.wayfairContentMod, "VultureFeather", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.zylonMod, "AdeniteCrumbles", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAdeneb
			}).AddModItemToShop(ModConditions.zylonMod, "BloodDroplet", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDreadnautilus
			}).AddModItemToShop(ModConditions.zylonMod, "BloodySpiderLeg", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.zylonMod, "Cerussite", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.zylonMod, "CocoaBeans", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.zylonMod, "EerieBell", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedEldritchJellyfish
			}).AddModItemToShop(ModConditions.zylonMod, "ElementalGoop", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.zylonMod, "EnchantedIceCube", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.zylonMod, "FantasticalFinality", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSaburRex
			}).AddModItemToShop(ModConditions.zylonMod, "GloryPetals", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.zylonMod, "GrahamCracker", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.zylonMod, "LivingBranch", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.zylonMod, "MetallicBell", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.zylonMod, "NeutronFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLunarEvent
			}).AddModItemToShop(ModConditions.zylonMod, "ObeliskShard", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.zylonMod, "OnyxShard", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.zylonMod, "Oozeberry", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.zylonMod, "OtherworldlyFang", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedEldritchJellyfish
			}).AddModItemToShop(ModConditions.zylonMod, "Smore", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.zylonMod, "SearedStone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAdeneb
			}).AddModItemToShop(ModConditions.zylonMod, "SlimyCore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedKingSlime
			}).AddModItemToShop(ModConditions.zylonMod, "SpeckledStardust", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.zylonMod, "SpectralFairyDust", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.zylonMod, "TabooEssence", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.zylonMod, "WindEssence", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.zylonMod, "WolfPelt", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).Register();
			new NPCShop(base.Type, "Modded Treasure Bags").AddModItemToShop(ModConditions.aequusMod, "CrabsonBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCrabson,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.aequusMod, "OmegaStariteBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedOmegaStarite,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.aequusMod, "DustDevilBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDustDevil,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.afkpetsMod, "LeatherBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSlayerOfEvil,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.afkpetsMod, "SATLA001TreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSATLA,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.afkpetsMod, "DrFetusTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDrFetus,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.afkpetsMod, "SlimesLastHopeTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSlimesHope,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.afkpetsMod, "PoliticianSlimeTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPoliticianSlime,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.afkpetsMod, "Inventory", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAncientTrio,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.afkpetsMod, "LavalGolemTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedLavalGolem,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.afkpetsMod, "AntonyPouch", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAntony,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.afkpetsMod, "BunnyZeppelinPouch", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedBunnyZeppelin,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.afkpetsMod, "OkikuPouch", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedOkiku,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.afkpetsMod, "RoyalHarpyAirForcePouch", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedHarpyAirforce,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.afkpetsMod, "IsaacPouch", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedIsaac,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.afkpetsMod, "AncientGuardianPouch", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAncientGuardian,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.afkpetsMod, "HeroicSlimePouch", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedHeroicSlime,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.afkpetsMod, "HolographicSlimePouch", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedHoloSlime,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.afkpetsMod, "SecurityBotPouch", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSecurityBot,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.afkpetsMod, "UndeadChefPouch", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedUndeadChef,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.afkpetsMod, "GuardianofFrostPouch", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedGuardianOfFrost,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.assortedCrazyThingsMod, "HarvesterTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSoulHarvester,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.awfulGarbageMod, "TreeToadBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedTreeToad,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.awfulGarbageMod, "SeseKitsugaiBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSeseKitsugai,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.awfulGarbageMod, "EyeOfTheStormBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedEyeOfTheStorm,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.awfulGarbageMod, "FrigidiusBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedFrigidius,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.blocksCoreBossMod, "CoreBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCoreBoss,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.blocksCoreBossMod, "CoreBossBagCrim", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCoreBoss,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "DesertScourgeBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDesertScourge,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "CrabulonBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCrabulon,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "HiveMindBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedHiveMind,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "PerforatorBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPerforators,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "SlimeGodBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSlimeGod,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "CryogenBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCryogen,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "AquaticScourgeBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAquaticScourge,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "BrimstoneWaifuBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedBrimstoneElemental,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "CalamitasCloneBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCalamitasClone,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "LeviathanBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedLeviathanAndAnahita,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "AstrumAureusBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAstrumAureus,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "PlaguebringerGoliathBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPlaguebringerGoliath,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "RavagerBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedRavager,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "AstrumDeusBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAstrumDeus,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "DragonfollyBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDragonfolly,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "ProvidenceBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedProvidence,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "PolterghastBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPolterghast,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "CeaselessVoidBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCeaselessVoid,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "StormWeaverBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedStormWeaver,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "SignusBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSignus,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "OldDukeBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedOldDuke,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "DevourerofGodsBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDevourerOfGods,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "YharonBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedYharon,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "DraedonBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedExoMechs,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityMod, "CalamitasCoffer", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSupremeCalamitas,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityEntropyMod, "LuminarisBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedLuminaris,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityEntropyMod, "ProphetBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedProphet,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityEntropyMod, "NihilityTwinBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedNihilityTwin,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.calamityEntropyMod, "CruiserBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCruiser,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.catalystMod, "AstrageldonBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAstrageldon,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.clamityAddonMod, "PyrogenBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPyrogen,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.clamityAddonMod, "WoBTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedWallOfBronze,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.consolariaMod, "LepusBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedLepus,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.consolariaMod, "TurkorBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedTurkor,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.consolariaMod, "OcramBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedOcram,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.coraliteMod, "RediancieBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedRediancie,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.coraliteMod, "BabyIceDragonBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedBabyIceDragon,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.coraliteMod, "SlimeEmperorSoulBox", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSlimeEmperor,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.coraliteMod, "BloodiancieBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedBloodiancie,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.coraliteMod, "ThunderveinDragonBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedThunderveinDragon,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.coraliteMod, "GriefSeed", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedNightmarePlantera,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.depthsMod, "ChasmeBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedChasme,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "GalahisBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedGalahis,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "CreationBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCreation,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "DestructionBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDestruction,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.edorbisMod, "BlightKingBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedBlightKing,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.edorbisMod, "GardenerBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedGardener,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.edorbisMod, "GlaciationBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedGlaciation,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.edorbisMod, "HocBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedHandOfCthulhu,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.edorbisMod, "CursedLordBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCursePreacher,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.exaltMod, "EffulgenceBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedEffulgence,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.exaltMod, "IceLichBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedIceLich,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.excelsiorMod, "BagNiflheim", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedNiflheim,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.excelsiorMod, "BagStarship", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedStellarStarship,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BacteriumPrimeBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedBacteriumPrime,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "DesertBeakBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDesertBeak,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "KingStingBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedKingSting,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "PhantasmBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPhantasm,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.fargosSoulsMod, "TrojanSquirrelBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedTrojanSquirrel,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.fargosSoulsMod, "CursedCoffinBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCursedCoffin,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.fargosSoulsMod, "DeviBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDeviantt,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.fargosSoulsMod, "BanishedBaronBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedBanishedBaron,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.fargosSoulsMod, "LifelightBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedLifelight,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.fargosSoulsMod, "CosmosBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedEridanus,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.fargosSoulsMod, "AbomBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAbominationn,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.fargosSoulsMod, "MutantBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedMutant,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "AlphaFrostjawBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAlphaFrostjaw,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "SanguineElementalBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSanguineElemental,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gameTerrariaMod, "SnowDonBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSnowDon,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "LilyWhiteBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedLilyWhite,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "RumiaBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedRumia,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "EternityLarvaBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedEternityLarva,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "NazrinBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedNazrin,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "HinaKagiyamaBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedHinaKagiyama,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "SekibankiBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSekibanki,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "SeiranBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSeiran,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "NitoriKawashiroBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedNitoriKawashiro,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "MedicineMelancholyBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedMedicineMelancholy,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "CirnoBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCirno,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "MinamitsuMurasaBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedMinamitsuMurasa,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "AliceMargatroidBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAliceMargatroid,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "SakuyaIzayoiBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSakuyaIzayoi,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "SeijaKijinBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSeijaKijin,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "MayumiJoutouguuBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedMayumiJoutouguu,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "ToyosatomimiNoMikoBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedToyosatomimiNoMiko,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "KaguyaHouraisanBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedKaguyaHouraisan,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "UtsuhoReiujiBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedUtsuhoReiuji,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "TenshiHinanawiBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedTenshiHinanawi,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gensokyoMod, "KisumeBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedKisume,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gerdsLabMod, "MagmaEyeTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedMagmaEye,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gerdsLabMod, "JackTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedJack,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.gerdsLabMod, "AcheronTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAcheron,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "MarquisMoonsquidTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedMarquisMoonsquid,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "PriestessRodTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPriestessRod,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "DiverTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDiver,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "TheMotherbrainTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedMotherbrain,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "WallofShadowTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "SlimeGodTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSunSlimeGod,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "TheOverwatcherTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedOverwatcher,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "TheLifebringerTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedLifebringer,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "TheMaterealizerTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedMaterealizer,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "ScarabBeliefTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedScarabBelief,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "EverlastingFallingWhaleTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedWorldsEndWhale,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "TheSonTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSon,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.huntOfTheOldGodMod, "TreasureBucket", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedGoozma,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.huntOfTheOldGodMod, "TreasureTrunk", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedGoozma,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.infernumMod, "BereftVassalBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedBereftVassal,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.lunarVeilMod, "GintziaBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCommanderGintzia,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.lunarVeilMod, "SunStalkerBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSunStalker,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.lunarVeilMod, "JackoBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPumpkinJack,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.lunarVeilMod, "DaedusBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedForgottenPuppetDaedus,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.lunarVeilMod, "DreadmireBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDreadMire,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.lunarVeilMod, "SingularityBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSingularityFragment,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.lunarVeilMod, "VerliaBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedVerlia,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.lunarVeilMod, "GothiviaBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedIrradia,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.lunarVeilMod, "SyliaBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSylia,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.lunarVeilMod, "FenixBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedFenix,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.lunarVeilMod, "TreasureBoxWater", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedWaterJellyfishOrWaterCogwork,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.lunarVeilMod, "TreasureBoxTrap", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCogworkOrSparn,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.lunarVeilMod, "TreasureBoxFire", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedBlazingSerpentOrPandorasFlamebox,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.martainsOrderMod, "BritzzBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedBritzz,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.martainsOrderMod, "AlchemistBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedTheAlchemist,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.martainsOrderMod, "VoidDiggerBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedVoidDigger,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.martainsOrderMod, "PrinceSlimeBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPrinceSlime,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.martainsOrderMod, "TripletsBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedTriplets,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.martainsOrderMod, "JungleDefendersBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedJungleDefenders,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.mechReworkMod, "MechclopsBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSt4sys,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.mechReworkMod, "TerminatorBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedTerminator,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.mechReworkMod, "CaretakerBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCaretaker,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.mechReworkMod, "SiegeEngineBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSiegeEngine,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.metroidMod, "TorizoBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedTorizo,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.metroidMod, "SerrisBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSerris,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.metroidMod, "KraidBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedKraid,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.metroidMod, "PhantoonBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPhantoon,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.metroidMod, "OmegaPirateBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedOmegaPirate,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.metroidMod, "NightmareBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedNightmare,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.metroidMod, "GoldenTorizoBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedGoldenTorizo,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.ophioidMod, "TreasureBagOphioid", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedOphiofly,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.polaritiesMod, "StormCloudfishBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedStormCloudfish,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.polaritiesMod, "StarConstructBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedStarConstruct,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.polaritiesMod, "GigabatBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedGigabat,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.polaritiesMod, "SunPixieBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSunPixie,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.polaritiesMod, "EsophageBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedEsophage,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.polaritiesMod, "ConvectiveWandererBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedConvectiveWanderer,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.projectZeroMod, "ForestGuardianBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedForestGuardian,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.projectZeroMod, "CryoGuardianBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCryoGuardian,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.projectZeroMod, "PrimordialWormBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPrimordialWorm,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.projectZeroMod, "HellGuardianBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedTheGuardianOfHell,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.projectZeroMod, "VoidBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedVoid,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.projectZeroMod, "ArmagemBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedArmagem,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.qwertyMod, "TundraBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPolarExterminator,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.qwertyMod, "FortressBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDivineLight,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.qwertyMod, "AncientMachineBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAncientMachine,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.qwertyMod, "NoehtnapBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedNoehtnap,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.qwertyMod, "HydraBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedHydra,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.qwertyMod, "BladeBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedImperious,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.qwertyMod, "RuneGhostBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedRuneGhost,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.qwertyMod, "BattleshipBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedInvaderBattleship,
				ModConditions.DownedInvaderNoehtnap,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.qwertyMod, "B4Bag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedOLORD,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.redemptionMod, "ThornBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedThorn,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.redemptionMod, "ErhanBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedErhan,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.redemptionMod, "KeeperBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedKeeper,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.redemptionMod, "SoIBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSeedOfInfection,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.redemptionMod, "SlayerBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedKingSlayerIII,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.redemptionMod, "OmegaCleaverBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedOmegaCleaver,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.redemptionMod, "OmegaGigaporaBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedOmegaGigapora,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.redemptionMod, "OmegaOblitBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedOmegaObliterator,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.redemptionMod, "PZBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPatientZero,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.redemptionMod, "AkkaBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAkka,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.redemptionMod, "UkkoBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedUkko,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.redemptionMod, "NebBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedNebuleus,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "GlowmothBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedGlowmoth,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "PinkyBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPutridPinky,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "CurseBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPharaohsCurse,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "TheAdvisorBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAdvisor,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "PolarisBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPolaris,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "LuxBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedLux,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "SubspaceBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSubspaceSerpent,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.sloomeMod, "Bag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedExodygen,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "DecreeBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDecree,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "RalnekBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedFlamingPumpkin,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "BruteBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedZombiePiglinBrute,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "JensenBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedJensenTheGrandHarpy,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "AraneasBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAraneas,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "RaynareBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedHarpyQueenRaynare,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "PrimordiaBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPrimordia,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "AbaddonBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAbaddon,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "AraghurBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAraghur,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "LostSiblingsBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedLostSiblings,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "ErazorBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedErazor,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "NihilusBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedNihilus,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.spiritMod, "BagOScarabs", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedScarabeus,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.spiritMod, "MJWBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedMoonJellyWizard,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.spiritMod, "ReachBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedVinewrathBane,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.spiritMod, "FlyerBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAncientAvian,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.spiritMod, "SteamRaiderBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedStarplateVoyager,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.spiritMod, "InfernonBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedInfernon,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.spiritMod, "DuskingBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDusking,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.spiritMod, "AtlasBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAtlas,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.spookyMod, "BossBagRotGourd", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedRotGourd,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.spookyMod, "BossBagSpookySpirit", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSpookySpirit,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.spookyMod, "BossBagMoco", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedMoco,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.spookyMod, "BossBagDaffodil", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDaffodil,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.spookyMod, "BossBagOrroboro", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedOrroBoro,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.spookyMod, "BossBagBigBone", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedBigBone,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.starlightRiverMod, "SquidBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAuroracle,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.starlightRiverMod, "VitricBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCeiros,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.starsAboveMod, "VagrantBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedVagrantofSpace,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.starsAboveMod, "ThespianBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedThespian,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.starsAboveMod, "DioskouroiBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDioskouroi,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.starsAboveMod, "NalhaunBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedNalhaun,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.starsAboveMod, "PenthBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPenthesilea,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.starsAboveMod, "ArbitrationBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedArbitration,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.starsAboveMod, "WarriorBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedWarriorOfLight,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.starsAboveMod, "TsukiBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedTsukiyomi,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "AridBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAncientHusk,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "StormBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedOverloadedScandrone,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "UltimateBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPainbringer,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.supernovaMod, "FlyingTerrorBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedFlyingTerror,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.terrorbornMod, "II_TreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedInfectedIncarnate,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.terrorbornMod, "TT_TreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedTidalTitan,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.terrorbornMod, "DS_TreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDunestock,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.terrorbornMod, "HC_TreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedHexedConstructor,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.terrorbornMod, "SC_TreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedShadowcrawler,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.terrorbornMod, "PI_TreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPrototypeI,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.thoriumMod, "TheGrandThunderBirdTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedGrandThunderBird,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.thoriumMod, "DarkMageTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				Condition.DownedOldOnesArmyAny,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.thoriumMod, "QueenJellyfishTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedQueenJellyfish,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.thoriumMod, "ViscountTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedViscount,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.thoriumMod, "GraniteEnergyStormTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedGraniteEnergyStorm,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.thoriumMod, "BuriedChampionTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedBuriedChampion,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.thoriumMod, "StarScouterTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedStarScouter,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.thoriumMod, "FlyingDutchmanTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				Condition.DownedPirates,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.thoriumMod, "BoreanStriderTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedBoreanStrider,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.thoriumMod, "FallenBeholderTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedFallenBeholder,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.thoriumMod, "OgreTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				Condition.DownedOldOnesArmyT2,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.thoriumMod, "LichTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedLich,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.thoriumMod, "ForgottenOneTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedForgottenOne,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.thoriumMod, "MartianSaucerTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedMartianSaucer,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.thoriumMod, "LunaticCultistTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				Condition.DownedCultist,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.thoriumMod, "ThePrimordialsTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPrimordials,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.traeMod, "DreadBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDreadnautilus,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.traeMod, "BeholderBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedBeholder,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.uhtricMod, "DredgerBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDredger,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.uhtricMod, "CharcoolSnowmanBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCharcoolSnowman,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.uhtricMod, "CosmicMenaceBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedCosmicMenace,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.universeOfSwordsMod, "SwordBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedEvilFlyingBlade,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.valhallaMod, "PirateSquidBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedColossalCarnage,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.valhallaMod, "EmperorBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedYurnero,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.vitalityMod, "StormCloudBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedStormCloud,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.vitalityMod, "GrandAntlionBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedGrandAntlion,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.vitalityMod, "GemstoneElementalBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedGemstoneElemental,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.vitalityMod, "MoonlightDragonflyBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedMoonlightDragonfly,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.vitalityMod, "DreadnaughtBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDreadnaught,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.vitalityMod, "MosquitoMonarchBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedMosquitoMonarch,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.vitalityMod, "AnarchulesBeetleBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAnarchulesBeetle,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.vitalityMod, "ChaosbringerBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedChaosbringer,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.vitalityMod, "PaladinSpiritBossBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedPaladinSpirit,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.wayfairContentMod, "LightblossomTreasureBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedManaflora,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.zylonMod, "DirtballBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedDirtball,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.zylonMod, "MetelordBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedMetelord,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.zylonMod, "AdenebBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedAdeneb,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.zylonMod, "JellyBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedEldritchJellyfish,
				ModConditions.expertOrMaster
			}).AddModItemToShop(ModConditions.zylonMod, "SaburBag", Item.buyPrice(0, 25, 0, 0), new Condition[]
			{
				ModConditions.DownedSaburRex,
				ModConditions.expertOrMaster
			}).Register();
			new NPCShop(base.Type, "Modded Crates & Grab Bags").AddModItemToShop(ModConditions.aequusMod, "CrabCreviceCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToCrabCrevice
			}).AddModItemToShop(ModConditions.aequusMod, "CrabCreviceCrateHard", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCrabCrevice
			}).AddModItemToShop(ModConditions.calamityMod, "StarterBag", Item.buyPrice(0, 5, 0, 0)).AddModItemToShop(ModConditions.calamityMod, "MonolithCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.calamityMod, "AstralCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.calamityMod, "SlagCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToCrags
			}).AddModItemToShop(ModConditions.calamityMod, "BrimstoneCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCrags
			}).AddModItemToShop(ModConditions.calamityMod, "SulphurousCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToSulphurSea
			}).AddModItemToShop(ModConditions.calamityMod, "HydrothermalCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSulphurSea
			}).AddModItemToShop(ModConditions.calamityMod, "EutrophicCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToSunkenSea
			}).AddModItemToShop(ModConditions.calamityMod, "PrismCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSunkenSea
			}).AddModItemToShop(ModConditions.calamityMod, "SulphuricTreasure", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToSulphurSea
			}).AddModItemToShop(ModConditions.calamityMod, "AbyssalTreasure", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToAbyss
			}).AddModItemToShop(ModConditions.calamityMod, "FleshyGeode", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.DownedRavager
			}).AddModItemToShop(ModConditions.calamityMod, "NecromanticGeode", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.DownedProvidence
			}).AddModItemToShop(ModConditions.calamityMod, "SandyAnglingKit", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.DownedDesertScourge
			}).AddModItemToShop(ModConditions.calamityMod, "BleachedAnglingKit", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.DownedAquaticScourge
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "BananaSplitCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToConfectionOrHallow
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "ConfectionCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToConfectionOrHallow
			}).AddModItemToShop(ModConditions.depthsMod, "QuartzCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.depthsMod, "ArqueriteCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.depthsMod, "QuartzLockBox", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.everjadeMod, "SpringCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToJadeLake
			}).AddModItemToShop(ModConditions.everjadeMod, "DragonCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToJadeLake
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ContagionCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToAvalonEvilBiomes
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "PlagueCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToAvalonEvilBiomes
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "ForeverCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.DownedWallOfShadow
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "CountdownCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.DownedWallOfShadow,
				ModConditions.DownedOverwatcher
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "LivingCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.DownedWallOfShadow
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "MembraneCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.DownedWallOfShadow,
				ModConditions.DownedLifebringer
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "CubistCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.DownedWallOfShadow
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "CubeCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.DownedWallOfShadow,
				ModConditions.DownedMaterealizer
			}).AddModItemToShop(ModConditions.martainsOrderMod, "CavernCrate", Item.buyPrice(0, 5, 0, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "GrottoCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.martainsOrderMod, "CoralBag", Item.buyPrice(0, 5, 0, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "FishBarBag", Item.buyPrice(0, 5, 0, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "HoneyBag", Item.buyPrice(0, 5, 0, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "MoltenBag", Item.buyPrice(0, 5, 0, 0)).AddModItemToShop(ModConditions.polaritiesMod, "SaltCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.redemptionMod, "PetrifiedCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToWasteland
			}).AddModItemToShop(ModConditions.redemptionMod, "LabCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.DownedMechBossAll,
				ModConditions.HasBeenToLab
			}).AddModItemToShop(ModConditions.redemptionMod, "LabCrate2", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.DownedMoonLord,
				ModConditions.HasBeenToLab
			}).AddModItemToShop(ModConditions.redemptionMod, "ScrapMetal", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.DownedMechBossAll,
				ModConditions.HasBeenToLab
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "PyramidCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToPyramid
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "JarOfSouls", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.DownedAdvisor
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "FlariumCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.DownedAraghur
			}).AddModItemToShop(ModConditions.spiritMod, "ReachCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToBriar
			}).AddModItemToShop(ModConditions.spiritMod, "BriarCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToBriar
			}).AddModItemToShop(ModConditions.spiritMod, "SpiritCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToSpirit
			}).AddModItemToShop(ModConditions.spiritMod, "FishCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.spiritMod, "PirateCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.DownedPirates
			}).AddModItemToShop(ModConditions.spookyMod, "SpookyCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToSpookyForest
			}).AddModItemToShop(ModConditions.spookyMod, "SpookyHellCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToEyeValley
			}).AddModItemToShop(ModConditions.spookyMod, "SpookyHellCrate2", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEyeValley
			}).AddModItemToShop(ModConditions.spookyMod, "CatacombCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToCatacombs
			}).AddModItemToShop(ModConditions.spookyMod, "CatacombCrate2", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCatacombs
			}).AddModItemToShop(ModConditions.thoriumMod, "AquaticDepthsCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToAquaticDepths
			}).AddModItemToShop(ModConditions.thoriumMod, "AbyssalCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToAquaticDepths
			}).AddModItemToShop(ModConditions.thoriumMod, "ScarletCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.thoriumMod, "SinisterCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.thoriumMod, "StrangeCrate", Item.buyPrice(0, 5, 0, 0)).AddModItemToShop(ModConditions.thoriumMod, "WondrousCrate", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.verdantMod, "LushWoodCrateItem", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.verdantMod, "MysteriaCrateItem", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.zylonMod, "BagofFruits", Item.buyPrice(0, 5, 0, 0), new Condition[]
			{
				ModConditions.DownedDirtball
			}).Register();
			new NPCShop(base.Type, "Modded Ores & Bars").AddModItemToShop(ModConditions.aequusMod, "PearlShardBlack", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCrabCrevice
			}).AddModItemToShop(ModConditions.aequusMod, "SoulGem", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDemonSiege
			}).AddModItemToShop(ModConditions.aequusMod, "MonoGem", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.aequusMod, "OmniGem", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToAether
			}).AddModItemToShop(ModConditions.aequusMod, "PearlShardPink", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCrabCrevice
			}).AddModItemToShop(ModConditions.aequusMod, "SoulGemFilled", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDemonSiege
			}).AddModItemToShop(ModConditions.aequusMod, "PearlShardWhite", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCrabCrevice
			}).AddModItemToShop(ModConditions.afkpetsMod, "NetheriteOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLavalGolem
			}).AddModItemToShop(ModConditions.afkpetsMod, "NetheriteBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedLavalGolem
			}).AddModItemToShop(ModConditions.awfulGarbageMod, "FrigidiumOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.awfulGarbageMod, "FrigidiumBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.calamityMod, "AerialiteOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedPerforatorsOrHiveMind,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.calamityMod, "AstralOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAstrumDeus,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.calamityMod, "AuricOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedYharon,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.calamityMod, "CryonicOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedCryogen,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.calamityMod, "ExodiumCluster", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.calamityMod, "HallowedOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAll,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.calamityMod, "InfernalSuevite", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToCrags
			}).AddModItemToShop(ModConditions.calamityMod, "PerennialOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.calamityMod, "PrismShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSunkenSea
			}).AddModItemToShop(ModConditions.calamityMod, "ScoriaOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToAbyss
			}).AddModItemToShop(ModConditions.calamityMod, "SeaPrism", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSunkenSea
			}).AddModItemToShop(ModConditions.calamityMod, "UelibloomOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedProvidence,
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.calamityMod, "AerialiteBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedPerforatorsOrHiveMind,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.calamityMod, "AstralBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedAstrumDeus,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.calamityMod, "AuricBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedYharon,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.calamityMod, "CosmiliteBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedDevourerOfGods
			}).AddModItemToShop(ModConditions.calamityMod, "CryonicBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedCryogen,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.calamityMod, "LifeAlloy", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedGolem
			}).AddModItemToShop(ModConditions.calamityMod, "PerennialBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.calamityMod, "ScoriaBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToAbyss
			}).AddModItemToShop(ModConditions.calamityMod, "ShadowspecBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedSupremeCalamitas,
				ModConditions.DownedExoMechs
			}).AddModItemToShop(ModConditions.calamityMod, "UelibloomBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedProvidence,
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "LifeOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "AlloyBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedPerforatorsOrHiveMind
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "ElementalBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "EssentialEssenceBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedAstrumDeus
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "HauntedBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedPolterghast
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "YharimBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedSupremeCalamitas
			}).AddModItemToShop(ModConditions.calamityEntropyMod, "VoidOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedCruiser
			}).AddModItemToShop(ModConditions.calamityEntropyMod, "VoidBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedCruiser
			}).AddModItemToShop(ModConditions.calamityOverhaulMod, "BlackMatterStick", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedExoMechs,
				ModConditions.DownedSupremeCalamitas
			}).AddModItemToShop(ModConditions.calamityOverhaulMod, "InfiniteIngot", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedBossRush,
				ModConditions.DownedExoMechs,
				ModConditions.DownedSupremeCalamitas
			}).AddModItemToShop(ModConditions.calamityOverhaulMod, "PestilenceIngot", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedPlaguebringerGoliath
			}).AddModItemToShop(ModConditions.catalystMod, "MetanovaOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAstrageldon,
				ModConditions.DownedAstrumDeus,
				Condition.DownedCultist,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.catalystMod, "MetanovaBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedAstrageldon,
				ModConditions.DownedAstrumDeus,
				Condition.DownedCultist,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.clamityAddonMod, "FrozenHellstone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedWallOfBronze
			}).AddModItemToShop(ModConditions.clamityAddonMod, "EnchantedMetal", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedWallOfBronze
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "HallowedOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToConfectionOrHallow
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "NeapoliniteOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToConfectionOrHallow
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "NeapoliniteBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToConfectionOrHallow
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "Saccharite", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToConfectionOrHallow
			}).AddModItemToShop(ModConditions.coraliteMod, "BloodJade", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedBloodiancie
			}).AddModItemToShop(ModConditions.coraliteMod, "RedJade", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedRediancie
			}).AddModItemToShop(ModConditions.coraliteMod, "GlistentBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.coraliteMod, "SteelBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.depthsMod, "ArqueriteOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.depthsMod, "ArqueriteBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.depthsMod, "Onyx", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.depthsMod, "AmethystShalestoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "DiamondShalestoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "EmeraldShalestoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "OnyxShalestoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "OnyxStoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "RubyShalestoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "SapphireShalestoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "TopazShalestoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "ArcaniumOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Moonstone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Skystone_Ore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "VarsiumCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Ashen_Ore", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "UniversiumOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Arcanium_Bar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "SkystoneBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "Ashen_Bar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.echoesOfTheAncientsMod, "UniversiumBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.edorbisMod, "CentauriumOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem
			}).AddModItemToShop(ModConditions.edorbisMod, "KelviniteOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.edorbisMod, "Lithium", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.edorbisMod, "AtlantiumBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedHandOfCthulhu
			}).AddModItemToShop(ModConditions.edorbisMod, "CentauriumBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedGolem
			}).AddModItemToShop(ModConditions.edorbisMod, "KelviniteBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.edorbisMod, "SteampunkAlloy", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.edorbisMod, "ThermiteBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.DownedCursePreacher
			}).AddModItemToShop(ModConditions.exaltMod, "TitanicOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.exaltMod, "TitanicBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.excelsiorMod, "PurifiedStone", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.excelsiorMod, "DarksteelOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.excelsiorMod, "GlacialOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedNiflheim
			}).AddModItemToShop(ModConditions.excelsiorMod, "SkylineOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.excelsiorMod, "MysticCrystal", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.excelsiorMod, "DarksteelBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.excelsiorMod, "GlacialBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedNiflheim
			}).AddModItemToShop(ModConditions.excelsiorMod, "SkylineBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.excelsiorMod, "HyperionCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BacciliteOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BismuthOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Boltstone", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BronzeOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "CaesiumOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "DurataniumOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "HallowedOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Heartstone", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "IridiumOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDesertBeak
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "NaquadahOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "NickelOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "OsmiumOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDesertBeak
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "RhodiumOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedDesertBeak
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Starstone", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Sulphur", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "TroxiniumOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "XanthophyteOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ZincOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BacciliteBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BismuthBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BronzeBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "CaesiumBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "DurataniumBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "EnchantedBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "IridiumBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedDesertBeak
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "NaquadahBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "NickelBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "OsmiumBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedDesertBeak
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "RhodiumBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedDesertBeak
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "TroxiniumBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "XanthophyteBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ZincBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Peridot", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Tourmaline", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Zircon", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "PeridotStoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "TourmalineStoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ZirconStoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "ZilliumOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "BrassBar", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "IndustrialWaxBar", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "ZilliumBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "Aquamarine", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "Chalcedony", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "Peridot", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "RoseQuartz", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.homewardJourneyMod, "Onyx", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToHomewardAbyss
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "HauntedOnyx", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToHomewardAbyss
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "CubistOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "DeepOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToHomewardAbyss
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "FinalOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLifebringer,
				ModConditions.DownedMaterealizer,
				ModConditions.DownedOverwatcher
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "EternalOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow,
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "LivingOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow,
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "CubistBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "DeepBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToHomewardAbyss
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "FinalBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedLifebringer,
				ModConditions.DownedMaterealizer,
				ModConditions.DownedOverwatcher
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "EternalBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow,
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "LivingBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedWallOfShadow,
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.homewardJourneyMod, "AbyssalChunk", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedDiver,
				ModConditions.HasBeenToHomewardAbyss
			}).AddModItemToShop(ModConditions.lunarVeilMod, "ArncharChunk", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "FrileOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "KaleidoscopicInk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.lunarVeilMod, "LostScrap", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "OldWeddingRing", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "VerianOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.lunarVeilMod, "ArnchaliteBar", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "ArtisanBar", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.lunarVeilMod, "FrileBar", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "GintzlMetal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedGintzeArmy
			}).AddModItemToShop(ModConditions.lunarVeilMod, "GrailBar", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "SingulariumBar", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.lunarVeilMod, "SpacialDistortionFragments", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSingularityFragment
			}).AddModItemToShop(ModConditions.lunarVeilMod, "VerianBar", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.martainsOrderMod, "AquaRock", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.martainsOrderMod, "Charcoal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.martainsOrderMod, "FishyumOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "HallowedDebris", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.martainsOrderMod, "MagmaRock", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.martainsOrderMod, "MagnesiumOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "MercuryOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "NadirScraps", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "OreFiber", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "PlutoniumOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "TantalumOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.martainsOrderMod, "UraniumOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.martainsOrderMod, "WitherDebris", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.martainsOrderMod, "ZincOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.martainsOrderMod, "FoolMaterial", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "FishBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "FishyumBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.martainsOrderMod, "MagnesiumBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "MercuryBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "PlutoniumBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "TantalumBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.martainsOrderMod, "UraniumBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.martainsOrderMod, "WitherBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.martainsOrderMod, "Martinite", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.martainsOrderMod, "ZincBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.martainsOrderMod, "Carbonite", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "Garnet", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.martainsOrderMod, "Graphite", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "Hematite", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "Jacinth", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "Maxixe", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "Peridot", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "CarboniteStoneBlock", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "GraphiteStoneBlock", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "HematiteStoneBlock", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "JacinthStoneBlock", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "MaxixeStoneBlock", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "PeridotStoneBlock", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.metroidMod, "ChoziteOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedTorizo
			}).AddModItemToShop(ModConditions.metroidMod, "Phazon", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.metroidMod, "ChoziteBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedTorizo
			}).AddModItemToShop(ModConditions.metroidMod, "PhazonBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.polaritiesMod, "MantellarOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.polaritiesMod, "MantellarBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.polaritiesMod, "SunplateBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedStarConstruct
			}).AddModItemToShop(ModConditions.projectZeroMod, "DarkOre", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.projectZeroMod, "ElectroOre", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.projectZeroMod, "DarkBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.projectZeroMod, "ElectroBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.qwertyMod, "LuneOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.qwertyMod, "RhuthiniumOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.qwertyMod, "CaeliteBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedDivineLight,
				ModConditions.HasBeenToSkyFortress
			}).AddModItemToShop(ModConditions.qwertyMod, "LuneBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.qwertyMod, "RhuthiniumBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.redemptionMod, "CorruptedXenomite", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedOmegaCleaver
			}).AddModItemToShop(ModConditions.redemptionMod, "DragonLeadOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.redemptionMod, "GathicCryoCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.redemptionMod, "GraveSteelShards", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.redemptionMod, "Plutonium", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord,
				ModConditions.HasBeenToLab
			}).AddModItemToShop(ModConditions.redemptionMod, "RawXenium", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMoonLord,
				ModConditions.HasBeenToLab
			}).AddModItemToShop(ModConditions.redemptionMod, "Uranium", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToLab
			}).AddModItemToShop(ModConditions.redemptionMod, "Xenomite", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToLab
			}).AddModItemToShop(ModConditions.redemptionMod, "XenomiteShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedSeedOfInfection
			}).AddModItemToShop(ModConditions.redemptionMod, "GraveSteelAlloy", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.redemptionMod, "DragonLeadAlloy", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.redemptionMod, "MoltenScrap", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.redemptionMod, "PureIronAlloy", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.redemptionMod, "XeniumAlloy", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedMoonLord,
				ModConditions.HasBeenToLab
			}).AddModItemToShop(ModConditions.redemptionMod, "OreBomb", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.redemptionMod, "OrePowder", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.remnantsMod, "starore", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "FrigidIce", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "PhaseOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedLux,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "VibrantOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "AbsoluteBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedPolaris
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "AncientSteelBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedGoblinArmy
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "FrigidBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "HardlightAlloy", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedAdvisor,
				ModConditions.HasBeenToPlanetarium
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "PhaseBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedLux,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "StarlightAlloy", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedAdvisor,
				ModConditions.HasBeenToPlanetarium
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "OtherworldlyAlloy", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedAdvisor,
				ModConditions.HasBeenToPlanetarium
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "VibrantBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "RoyalRubyShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToPyramid,
				ModConditions.DownedPharaohsCurse
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "RubyKeystone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToPyramid,
				ModConditions.DownedPharaohsCurse
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "TaintedKeystoneShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToPyramid,
				ModConditions.DownedPharaohsCurse
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "TaintedKeystone", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToPyramid,
				ModConditions.DownedPharaohsCurse
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "FlariumCrystal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAraghur
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "OblivionOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "RawBismuth", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "RawLapis", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "VenomiteOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedAraneas
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "DraconiumAlloy", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedAraghur
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "SnowBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedQueenBee
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "LihzahrdBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "OblivionBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "RefinedBismuth", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "RefinedLapis", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "TiridiumBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedLostSiblings
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "VenomiteBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedAraneas
			}).AddModItemToShop(ModConditions.spiritMod, "MarbleChunk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.spiritMod, "CosmiliteShard", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedStarplateVoyager,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.spiritMod, "CryoliteOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.spiritMod, "GraniteChunk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.spiritMod, "FloranOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToBriar
			}).AddModItemToShop(ModConditions.spiritMod, "SpiritOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToSpirit
			}).AddModItemToShop(ModConditions.spiritMod, "CryoliteBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.spiritMod, "FloranBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.HasBeenToBriar
			}).AddModItemToShop(ModConditions.spiritMod, "SpiritBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToSpirit
			}).AddModItemToShop(ModConditions.starlightRiverMod, "AuroraIceItem", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToAuroracleTemple
			}).AddModItemToShop(ModConditions.starlightRiverMod, "MoonstoneOreItem", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.starlightRiverMod, "AuroraIceBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.HasBeenToAuroracleTemple
			}).AddModItemToShop(ModConditions.starlightRiverMod, "MoonstoneBarItem", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "SpaceRockBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "DesertBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.stormsAdditionsMod, "IceBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.supernovaMod, "ZirconiumOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.supernovaMod, "VerglasBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedQueenBee
			}).AddModItemToShop(ModConditions.supernovaMod, "ZirconiumBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.terrorbornMod, "AzuriteOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.terrorbornMod, "DeimosteelOreItem", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.terrorbornMod, "NovagoldOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.terrorbornMod, "SkullmoundOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.terrorbornMod, "AzuriteBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.terrorbornMod, "PlasmaliumBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.terrorbornMod, "DeimosteelBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.terrorbornMod, "NovagoldBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.terrorbornMod, "SkullmoundBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.terrorbornMod, "PyroclasticGemstone", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.thoriumMod, "Aquaite", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToAquaticDepths
			}).AddModItemToShop(ModConditions.thoriumMod, "IllumiteChunk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "LifeQuartz", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "LodeStoneChunk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedFallenBeholder,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "SmoothCoal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "ThoriumOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "ValadiumChunk", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedFallenBeholder,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "AquaiteBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToAquaticDepths
			}).AddModItemToShop(ModConditions.thoriumMod, "aDarksteelAlloy", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).AddModItemToShop(ModConditions.thoriumMod, "IllumiteIngot", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "LodeStoneIngot", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedFallenBeholder,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "SandstoneIngot", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedGrandThunderBird,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "ThoriumBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "TitanicBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.thoriumMod, "ValadiumIngot", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedFallenBeholder,
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "Aquamarine", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "Opal", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "AquamarineStoneBlock", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "OpalStoneBlock", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.thoriumMod, "AmberMarineBlock", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedQueenJellyfish
			}).AddModItemToShop(ModConditions.thoriumMod, "AmethystMarineBlock", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedQueenJellyfish
			}).AddModItemToShop(ModConditions.thoriumMod, "AquamarineMarineBlock", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedQueenJellyfish
			}).AddModItemToShop(ModConditions.thoriumMod, "DiamondMarineBlock", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedQueenJellyfish,
				ModConditions.DownedQueenJellyfish
			}).AddModItemToShop(ModConditions.thoriumMod, "EmeraldMarineBlock", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedQueenJellyfish
			}).AddModItemToShop(ModConditions.thoriumMod, "OpalMarineBlock", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedQueenJellyfish
			}).AddModItemToShop(ModConditions.thoriumMod, "RubyMarineBlock", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedQueenJellyfish
			}).AddModItemToShop(ModConditions.thoriumMod, "SapphireMarineBlock", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedQueenJellyfish
			}).AddModItemToShop(ModConditions.thoriumMod, "TopazMarineBlock", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedQueenJellyfish
			}).AddModItemToShop(ModConditions.uhtricMod, "CaelestisOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.uhtricMod, "CaelestisBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.universeOfSwordsMod, "DamascusOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.universeOfSwordsMod, "DamascusBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.universeOfSwordsMod, "Orichalcon", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.valhallaMod, "CorrodeOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.valhallaMod, "JadeFragment", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.DownedYurnero
			}).AddModItemToShop(ModConditions.valhallaMod, "TarOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.valhallaMod, "ValhalliteOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.valhallaMod, "CorrodeBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.valhallaMod, "HardenedGlass", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.valhallaMod, "ValhalliteBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.verdantMod, "AquamarineItem", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground,
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.verdantMod, "GreenCrystalItem", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedEyeOfCthulhu,
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.verdantMod, "EmbeddedStoneAquamarineItem", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground,
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.verdantMod, "EmbeddedAquamarineItem", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground,
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.vitalityMod, "ArcticOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.vitalityMod, "Bloodrock", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.vitalityMod, "GeraniumOre", Item.buyPrice(0, 0, 25, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.vitalityMod, "AnarchyBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedAnarchulesBeetle
			}).AddModItemToShop(ModConditions.vitalityMod, "ArcaneGoldBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.vitalityMod, "ArcticBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.vitalityMod, "BloodrockBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.vitalityMod, "BronzeAlloy", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.vitalityMod, "ChaosBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedChaosbringer
			}).AddModItemToShop(ModConditions.vitalityMod, "DriedBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				ModConditions.DownedGrandAntlion
			}).AddModItemToShop(ModConditions.vitalityMod, "GeraniumBar", Item.buyPrice(0, 0, 50, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.vitalityMod, "GlowingGraniteBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.vitalityMod, "SteelAlloy", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.vitalityMod, "PurifiedBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.zylonMod, "CarnalliteOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.zylonMod, "HaxoniteOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.zylonMod, "ZincOre", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.zylonMod, "JadeStoneBlock", Item.buyPrice(0, 0, 25, 0)).AddModItemToShop(ModConditions.zylonMod, "CarnalliteBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.zylonMod, "HaxoniteBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.zylonMod, "ZincBar", Item.buyPrice(0, 0, 50, 0)).AddModItemToShop(ModConditions.zylonMod, "Jade", Item.buyPrice(0, 0, 25, 0)).Register();
			new NPCShop(base.Type, "Modded Natural Blocks").AddModItemToShop(ModConditions.arbourMod, "BirchWoodBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.aequusMod, "SedimentaryRockItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.aequusMod, "MeadowWood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityMod, "AbyssGravel", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.calamityMod, "Acidwood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityMod, "AstralClay", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "AstralDirt", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "AstralIce", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "AstralMonolith", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "AstralSand", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "AstralSandstone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "AstralSnow", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "AstralStone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "BrimstoneSlag", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.calamityMod, "CelestialRemains", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "EutrophicSand", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityMod, "HardenedAstralSand", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "HardenedSulphurousSandstone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityMod, "PyreMantleMolten", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.calamityMod, "Navystone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityMod, "NovaeSlag", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "PlantyMush", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityMod, "PyreMantle", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.calamityMod, "ScorchedBone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.calamityMod, "ScorchedRemains", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.calamityMod, "SulphurousSand", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityMod, "SulphurousSandstone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityMod, "SulphurousShale", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityMod, "VernalSoil", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityMod, "Voidstone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "CosmiliteSlag", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedDevourerOfGods
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "Grimesand", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "MeldGunk", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "OvergrownPlaguedStone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPlaguebringerGoliath
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "PlaguedClay", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPlaguebringerGoliath
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "PlaguedGrass", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPlaguebringerGoliath
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "PlaguedHive", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPlaguebringerGoliath
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "PlaguedMud", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPlaguebringerGoliath
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "PlaguedPipe", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPlaguebringerGoliath
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "PlaguedSand", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPlaguebringerGoliath
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "PlaguedSilt", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPlaguebringerGoliath
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "PlaguedStone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPlaguebringerGoliath
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "Sporezol", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPlaguebringerGoliath
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "AstralClay", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "AstralDirt", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "AstralIce", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "AstralSand", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "AstralSandstone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "AstralSnow", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "AstralHardenedSand", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "AstralTreeWood", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "Xenostone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.clamityAddonMod, "FrozenAsh", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedWallOfBronze
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "BlueFairyFloss", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "ChocolateFudge", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "CookieBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "CreamBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "Creamsand", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "Creamsandstone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "Creamstone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "CreamWood", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "HardenedCreamsand", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "OrangeIce", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "PinkFairyFloss", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "PipBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "PurpleFairyFloss", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.coraliteMod, "Basalt", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.coraliteMod, "CrystalBasalt", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.coraliteMod, "GelFiber", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.coraliteMod, "HardBasalt", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.coraliteMod, "MagicCrystalBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "NightWood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "PetrifiedWood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "Quartz", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "ShaleBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "Shalestone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.edorbisMod, "Nightwood", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).AddModItemToShop(ModConditions.everjadeMod, "HardenedJadeSandItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.everjadeMod, "JadeSandItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.everjadeMod, "JadeSandstoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exaltMod, "Basalt", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.excelsiorMod, "CompactDirt", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.excelsiorMod, "Darkslate", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ApocalyptusWood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BasaltBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BasaltPillar", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BlastedStone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BleachedEbony", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Bramble", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BrimstoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ChunkstoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Coughwood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "CrystalStoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "CrystalStoneCrystal", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "GreenIceBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "HardenedSnotsandBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LoamBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "NestBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ResistantWood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "SepsisBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "SnotsandBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "SnotsandstoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "YellowIceBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "DreadClayBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToDread
			}).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "DreadDirtBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToDread
			}).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "DreadIceBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToDread
			}).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "DreadSand", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToDread
			}).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "DreadsandstoneBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToDread
			}).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "DreadSnowBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToDread
			}).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "DreadstoneBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToDread
			}).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "Dreadwood", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToDread
			}).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "HardenedDreadsandBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToDread
			}).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "HemotiteChunk", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "BeanstalkBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "BlinkrootBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "BlueFlowerBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "Chalchum", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "DaybloomBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "DeathweedBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "FertileDirt", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "FireblossomBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "Floralwood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "KelpBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "Loam", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "Mantilum", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "MoondustBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "MoonglowBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "OrangeFlowerBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "Pallasite", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "PurpleFlowerBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "RedFlowerBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "SeashellBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "ShiverthornBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "StarDirt", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "StarlightOre", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "Starsand", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "Starstone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "Starwood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "Veridanite", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "Vinestone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "WaterleafBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "YellowFlowerBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.homewardJourneyMod, "AbyssStone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.lunarVeilMod, "AcidicalDirtBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.lunarVeilMod, "CathediteGrassBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.lunarVeilMod, "CarianWood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.lunarVeilMod, "OvermorrowDirt", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.lunarVeilMod, "OvermorrowWood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.lunarVeilMod, "StarbloomDirtBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.lunarVeilMod, "VerianWood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.lunarVeilMod, "VeriplantGrassBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "AridStone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "BirchWood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "BlackIceBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "ClayStone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "CoalBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "CoreBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.martainsOrderMod, "CorruptedHerb", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "CorruptHerbBale", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "CrimsonHerb", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "CrimsonHerbBale", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "CrystalStone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "CutBirch", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "DesertWood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "DryDirt", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "DryHerb", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "DryHerbBale", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "GelidStone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "GlowySpores", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "HallowHerb", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.martainsOrderMod, "HallowHerbBale", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.martainsOrderMod, "HardStone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "HardenedVoidsandBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "Herb", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "HerbBale", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "Hollowwood", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "IvyStone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "LivingShadowflameBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedGoblinArmy
			}).AddModItemToShop(ModConditions.martainsOrderMod, "LivingWitherBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.martainsOrderMod, "MireMudBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "SandySnow", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "SnowySand", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "SturdyCore", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.martainsOrderMod, "SturdySoil", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "SturdyStone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "ThornyBush", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "VoidHerb", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "VoidHerbBale", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "VoidsandBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "VoidsandstoneBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "VoidstoneBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "WhiteSlimeBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPrinceSlime
			}).AddModItemToShop(ModConditions.medialRiftMod, "Fissurestone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.medialRiftMod, "LucidGlass", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.medialRiftMod, "SiltRock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.medialRiftMod, "Stellarock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.medialRiftMod, "SummitGrass", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.medialRiftMod, "Summitstone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.medialRiftMod, "VertexGrass", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.medialRiftMod, "VertexRock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).AddModItemToShop(ModConditions.metroidMod, "BrinstoneTile", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.metroidMod, "GreenBrinstarRootsTile", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.metroidMod, "NorfairBubbleSM", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.metroidMod, "NorfairBubbleZM", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.metroidMod, "RedBrinstone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.metroidMod, "TanglevineTile", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.polaritiesMod, "Limestone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.polaritiesMod, "RockSalt", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.polaritiesMod, "Salt", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.redemptionMod, "AncientDirt", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.redemptionMod, "Asteroid", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.redemptionMod, "ElderWood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.redemptionMod, "GathicColdstone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.redemptionMod, "GathicFroststone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.redemptionMod, "GathicGladestone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.redemptionMod, "GathicStone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.redemptionMod, "GloomMushroom", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.redemptionMod, "IrradiatedClay", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.redemptionMod, "IrradiatedCrimstone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.redemptionMod, "IrradiatedDirt", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.redemptionMod, "IrradiatedEbonstone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.redemptionMod, "IrradiatedHardenedSand", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.redemptionMod, "IrradiatedIce", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.redemptionMod, "IrradiatedSand", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.redemptionMod, "IrradiatedSandstone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.redemptionMod, "IrradiatedSnow", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.redemptionMod, "IrradiatedStone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.redemptionMod, "PetrifiedWood", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.redemptionMod, "PlantMatter", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.remnantsMod, "elderdirt", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.remnantsMod, "elderwood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.remnantsMod, "flesh", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.remnantsMod, "hardstone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.remnantsMod, "elderstone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.remnantsMod, "lavastone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.remnantsMod, "poisonrock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "CharredWood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "CursedTumor", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPharaohsCurse
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "Evostone", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "GlowSilk", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedGlowmoth
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "Wormwood", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedKingSlime
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "SootBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "CinderDirt", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "Emberbark", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "HardenedScorchedSand", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "EmberRock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "ScorchedSand", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "ScorchedSandstone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "ThermalRack", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "Tuff", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "Weaverstone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAraneas
			}).AddModItemToShop(ModConditions.spiritMod, "AsteroidBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "AzureGemBlockItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "Black_Stone_Item", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "BlastStoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "BloodyFoliageItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "CaveDirtItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "CinderstoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "CorruptMassItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "CorruptOvergrowthItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "CorruptPustuleItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "CorruptTendrilItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "CrackedDirtItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "CragstoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "CreepingIce", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "CrimsonBlisterItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "CrimsonPustuleItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "CrimsonScabItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "CrumblingDirtItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "DarkFoliageItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "DriftwoodTileItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "SpiritWoodItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.spiritMod, "AncientBark", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "FloweryFoliageItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "ForestFoliageItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "FracturedStoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "SpiritStoneItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.spiritMod, "HallowCavernstoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "HallowCrystallineItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "HallowFoliageItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "HallowPrismstoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "HallowShardstoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "HiveBlockAltItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "IvyStoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "JungleFoliageItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "KrampusHornBlockItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "LayeredDirtItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "LeafyDirtItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "MagmastoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "MottledStoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "ObsidianBlockItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "OldStoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "OutlandStoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "RockyDirtItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "RoughDirtItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "RuinstoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "RuneBlockItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "ScrapItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "SmolderingRockItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "SpaceJunkItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "SpiritDirtItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.spiritMod, "SpiritIceItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.spiritMod, "SpiritSandItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.spiritMod, "HalloweenGrass", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "StalactiteStoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "ReachGrass", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "VinestoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "WavyDirtItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "WornStoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spookyMod, "DampSoilItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToSpiderCave
			}).AddModItemToShop(ModConditions.spookyMod, "EyeBlockItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToEyeValley
			}).AddModItemToShop(ModConditions.spookyMod, "LivingFleshItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToEyeValley
			}).AddModItemToShop(ModConditions.spookyMod, "SpookyStoneItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToSpookyForest
			}).AddModItemToShop(ModConditions.spookyMod, "SpookyWoodItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToSpookyForest
			}).AddModItemToShop(ModConditions.spookyMod, "SpookyMushItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToEyeValley
			}).AddModItemToShop(ModConditions.spookyMod, "RootWoodItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToSpiderCave
			}).AddModItemToShop(ModConditions.spookyMod, "WebBlockItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToSpiderCave
			}).AddModItemToShop(ModConditions.spookyMod, "SpookyDirtItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToSpookyForest
			}).AddModItemToShop(ModConditions.spookyMod, "CemeteryStoneItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToCemetery
			}).AddModItemToShop(ModConditions.spookyMod, "CemeteryDirtItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToCemetery
			}).AddModItemToShop(ModConditions.spookyMod, "ValleyStoneItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToEyeValley
			}).AddModItemToShop(ModConditions.starlightRiverMod, "AncientSandstoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.starlightRiverMod, "VitricCactusItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.starlightRiverMod, "LeafOvergrowItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.starlightRiverMod, "VitricSandItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.starlightRiverMod, "OvergrowthLivingWoodItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.starlightRiverMod, "PalestoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.starlightRiverMod, "PermafrostIceItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.starlightRiverMod, "StoneOvergrowItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.starlightRiverMod, "VitricSoftSandItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.starlightRiverMod, "VitricSandPlainItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.starlightRiverMod, "SpringstoneItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.terrorbornMod, "DeimostoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.terrorbornMod, "KindlingSoilBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.terrorbornMod, "PyroclasticCloudBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.terrorbornMod, "PyroclasticRaincloudBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.thoriumMod, "BrackishClump", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.thoriumMod, "EvergreenBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEverscream,
				Condition.DownedSantaNK1,
				Condition.DownedIceQueen
			}).AddModItemToShop(ModConditions.thoriumMod, "GingerbreadBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEverscream,
				Condition.DownedSantaNK1,
				Condition.DownedIceQueen
			}).AddModItemToShop(ModConditions.thoriumMod, "MarineBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.thoriumMod, "MossyMarineBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.thoriumMod, "Permafrost", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEverscream,
				Condition.DownedSantaNK1,
				Condition.DownedIceQueen
			}).AddModItemToShop(ModConditions.thoriumMod, "SugarCookieBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEverscream,
				Condition.DownedSantaNK1,
				Condition.DownedIceQueen
			}).AddModItemToShop(ModConditions.thoriumMod, "YewWood", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedGoblinArmy
			}).AddModItemToShop(ModConditions.valhallaMod, "LivingSnow", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.valhallaMod, "LivingSnowBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.valhallaMod, "WildRootDirt", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.valhallaMod, "Sinstone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.valhallaMod, "TarBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.valhallaMod, "WildRoot", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.verdantMod, "BackslateTileItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.verdantMod, "PuffBlockItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.verdantMod, "LightbulbLeafItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.verdantMod, "VerdantGrassBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.verdantMod, "LushSoilBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.verdantMod, "MysteriaFluffItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.verdantMod, "MysteriaWood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.verdantMod, "PinkPetalBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.verdantMod, "RedPetalBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.verdantMod, "SnailShellBlockItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.verdantMod, "ThornBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.verdantMod, "VerdantWoodBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.verdantMod, "LivingLushWoodWand", Item.buyPrice(0, 1, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToVerdant
			}).Register();
			new NPCShop(base.Type, "Modded Building Blocks").AddModItemToShop(ModConditions.aequusMod, "AncientAntiGravityBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedOmegaStarite
			}).AddModItemToShop(ModConditions.aequusMod, "AncientGravityBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedOmegaStarite
			}).AddModItemToShop(ModConditions.aequusMod, "AntiGravityBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedOmegaStarite
			}).AddModItemToShop(ModConditions.aequusMod, "EmancipationGrill", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedOmegaStarite
			}).AddModItemToShop(ModConditions.aequusMod, "GravityBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedOmegaStarite
			}).AddModItemToShop(ModConditions.aequusMod, "PhysicsBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedOmegaStarite
			}).AddModItemToShop(ModConditions.aequusMod, "RadonMossBrickItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.calamityMod, "AerialiteBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPerforatorsOrHiveMind
			}).AddModItemToShop(ModConditions.calamityMod, "AshenAccentSlab", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedBrimstoneElemental
			}).AddModItemToShop(ModConditions.calamityMod, "AshenSlab", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedBrimstoneElemental
			}).AddModItemToShop(ModConditions.calamityMod, "AstralBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAstrumDeus
			}).AddModItemToShop(ModConditions.calamityMod, "BrimstoneSlab", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedBrimstoneElemental
			}).AddModItemToShop(ModConditions.calamityMod, "Cinderplate", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "CosmiliteBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedDevourerOfGods
			}).AddModItemToShop(ModConditions.calamityMod, "CryonicBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedCryogen
			}).AddModItemToShop(ModConditions.calamityMod, "Elumplate", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "EutrophicGlass", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityMod, "ExoPlating", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedExoMechs
			}).AddModItemToShop(ModConditions.calamityMod, "ExoPrismPanel", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedExoMechs
			}).AddModItemToShop(ModConditions.calamityMod, "Havocplate", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "HazardChevronPanels", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityMod, "LaboratoryPanels", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityMod, "LaboratoryPipePlating", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityMod, "LaboratoryPlating", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityMod, "Navyplate", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "OccultBrickItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedSupremeCalamitas
			}).AddModItemToShop(ModConditions.calamityMod, "Onyxplate", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityMod, "OtherworldlyStone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedCeaselessVoid,
				ModConditions.DownedStormWeaver,
				ModConditions.DownedSignus
			}).AddModItemToShop(ModConditions.calamityMod, "PerennialBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.calamityMod, "PlaguedContainmentBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPlaguebringerGoliath
			}).AddModItemToShop(ModConditions.calamityMod, "Plagueplate", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPlaguebringerGoliath
			}).AddModItemToShop(ModConditions.calamityMod, "ProfanedCrystal", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedProvidence
			}).AddModItemToShop(ModConditions.calamityMod, "ProfanedRock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedProvidence
			}).AddModItemToShop(ModConditions.calamityMod, "ProfanedSlab", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedProvidence
			}).AddModItemToShop(ModConditions.calamityMod, "RunicProfanedBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedProvidence
			}).AddModItemToShop(ModConditions.calamityMod, "RustedPipes", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityMod, "RustedPlating", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityMod, "SeaPrismBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityMod, "ScoriaBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedGolem
			}).AddModItemToShop(ModConditions.calamityMod, "SilvaCrystal", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedDragonfolly
			}).AddModItemToShop(ModConditions.calamityMod, "SmoothAbyssGravel", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.calamityMod, "SmoothBrimstoneSlag", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedBrimstoneElemental
			}).AddModItemToShop(ModConditions.calamityMod, "SmoothVoidstone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.calamityMod, "StatigelBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedSlimeGod
			}).AddModItemToShop(ModConditions.calamityMod, "StratusBricks", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPolterghast
			}).AddModItemToShop(ModConditions.calamityMod, "UelibloomBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedProvidence
			}).AddModItemToShop(ModConditions.calamityMod, "VoidstoneSlab", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.calamityMod, "WulfrumPlating", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "Aeroplate", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "Bloodplate", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "AstralBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "AstralPearlBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "AstralPlating", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "AuricBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedYharon
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "AzufreSludge", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedOldDuke
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "BlightedEggBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "Bloodstone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedProvidence
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "BloodstoneBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedProvidence
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "ChiseledBloodstone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedProvidence
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "EidolicSlab", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPolterghast
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "FrostflakeBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedCryogen
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "HallowedBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "MeldBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "Necrostone", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedRavager
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "PhantowaxBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPolterghast
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "PolishedAstralMonolith", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "PolishedXenomonolith", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "ShadowBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedExoMechs,
				ModConditions.DownedSupremeCalamitas
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "ThanatosPlating", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedExoMechs
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "ThanatosPlatingVent", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedExoMechs
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "WulfrumPlating", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.catalystMod, "Astrogel", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAstrageldon
			}).AddModItemToShop(ModConditions.catalystMod, "MetanovaRealBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAstrageldon
			}).AddModItemToShop(ModConditions.catalystMod, "MetanovaBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAstrageldon
			}).AddModItemToShop(ModConditions.cerebralMod, "GravityBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.cerebralMod, "GravityFragmentBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.cerebralMod, "PulsarBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.cerebralMod, "PulsarFragmentBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.cerebralMod, "StellarwindBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.cerebralMod, "StellarwindFragmentBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.cerebralMod, "VeilBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.cerebralMod, "VeilFragmentBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.clamityAddonMod, "Pyroplate", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.ruptureMod, "RiftBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.ruptureMod, "RiftFragmentBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.clickerClassMod, "MiceBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "CreamstoneBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "HallowedBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "NeapoliniteBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "PastryBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "SacchariteBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "ShellBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "SherbetBricks", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "YumBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.coraliteMod, "MagicCrystalBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.coraliteMod, "ShadowBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.coraliteMod, "ShadowImaginaryBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.coraliteMod, "ShadowQuadrel", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.coraliteMod, "ThunderStoneBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedThunderveinDragon
			}).AddModItemToShop(ModConditions.depthsMod, "AncientShadowBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "ArqueriteBricks", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "BlackGemspark", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "LivingFog", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "MercuryMossBricks", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "QuartzBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "QuartzBricks", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "QuartzPillar", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "ShadowBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "ShaleBricks", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "SilverfallBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.dragonBallTerrariaMod, "RadiantFragmentBlockItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedCultist,
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.edorbisMod, "KelviniteBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.edorbisMod, "ThermiteBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.DownedCursePreacher
			}).AddModItemToShop(ModConditions.exaltMod, "BasaltBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.excelsiorMod, "GlacialBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedNiflheim
			}).AddModItemToShop(ModConditions.excelsiorMod, "BlackholeBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.excelsiorMod, "DecoratedPurityBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.excelsiorMod, "CheckerItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.excelsiorMod, "ChiseledDarkslate", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.excelsiorMod, "DarkslateBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.excelsiorMod, "DarksteelBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.excelsiorMod, "GlacialBrickNew", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedNiflheim
			}).AddModItemToShop(ModConditions.excelsiorMod, "HyperionLampBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.excelsiorMod, "SkylineBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.excelsiorMod, "StellarBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedStellarStarship
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AncientOrangeBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AncientPurpleBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "AncientYellowBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BacciliteBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAvalonEvilBosses
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BismuthBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BloodiedSpike", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BoltstoneBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BrambleSpike", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BronzeBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BronzePlating", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ChunkstoneBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "CrackedOrangeBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "CrackedPurpleBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "CrackedYellowBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "DemonSpikeScale", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "DurataniumPipe", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "DuskplateBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "FallenStarBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "HallowedBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "HeartstoneBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ImperviousBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "IridiumBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedDesertBeak
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LoamstoneBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "MoonplateBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "NastySpike", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "NickelBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "OrangeBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "OsmiumBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedDesertBeak
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "PeridotGemsparkBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "PurpleBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "RhodiumBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedDesertBeak
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "SkyBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "StarstoneBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "TourmalineGemsparkBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "TuhrtlBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedGolem
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "VenomSpike", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "YellowBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ZincBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ZirconGemsparkBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.fracturesOfPenumbraMod, "Hemobrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "AliceRose", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "AmberBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "AmethystBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "AppleSpiceCakeBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "AquamarineBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "AquamarineGemsparkBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "BlueFlowerBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "CarrotCakeBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "ChalcedonyBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "ChalcedonyGemsparkBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "ChalchumBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "ChocolateBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "CoconutCakeBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "ConstellationBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "CottonCandyBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "DiamondBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "EmeraldBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "ForestCakeBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "GardenBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "IndustrialWaxBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "IronBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "LaminatedFlooring", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "LeadBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "ManaStarAltBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "ManaStarBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "MantilumBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "MilkChocolateBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "MoonsetterBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "MoonsetterPlate", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "NaniteBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "OrangeFlowerBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "PallasiteBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "PerfectlyGenericBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "PeridotBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "PeridotGemsparkBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "PurpleFlowerBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "RedFlowerBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "RoseQuartzBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "RoseQuartzGemsparkBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "RubyBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "SapphireBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "SilkCarpet", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "SoulFlightBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "SoulFrightBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "SoulLightBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "SoulMightBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "SoulNightBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "SoulSightBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "StarlightBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "StarsandstoneBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "StarshineBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "StarshineBlockAlt", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "StarshineBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "StarstoneBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "StrawberryShortCakeBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "SweetBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "TopazBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "VeridaniteBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "VinestoneBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "WhiteChocolateBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "WickerBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "WickerBlock2", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "WickerBlock2", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "YellowFlowerBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "ZilliumBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.homewardJourneyMod, "AbyssBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.lunarVeilMod, "CathediteBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.lunarVeilMod, "GovheilBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.lunarVeilMod, "GovheilCastleBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.lunarVeilMod, "HuntiacBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.lunarVeilMod, "StarbloomBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.lunarVeilMod, "Stelleanbrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.lunarVeilMod, "VerianRuneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "AccursedBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "AstralBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.martainsOrderMod, "AstralFragmentBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.martainsOrderMod, "Biosolid", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "CamoBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.martainsOrderMod, "CarboniteGemsparkBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "ChromaBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.martainsOrderMod, "ChromaFragmentBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.martainsOrderMod, "CyanToyBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "EclipseplateBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "Electronics", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.martainsOrderMod, "FishyumBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "FractalBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.martainsOrderMod, "FractalFragmentBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.martainsOrderMod, "GarnetGemsparkBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.martainsOrderMod, "GoldenBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedPirates
			}).AddModItemToShop(ModConditions.martainsOrderMod, "Graphene", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMartians
			}).AddModItemToShop(ModConditions.martainsOrderMod, "GreenToyBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "HematiteGemsparkBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "IceCrystalBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "JacinthGemsparkBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "MagnesiumBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "MaxixeGemsparkBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "MercuryBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "MoonPlate", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "PackedIce", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "Paper", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "PeridotGemsparkBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "PinkToyBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "Plastic", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "PlasticBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "ScaleBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "StarplateBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.martainsOrderMod, "TantalumBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.martainsOrderMod, "TimeBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.martainsOrderMod, "TimeFragmentBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.martainsOrderMod, "VoidstoneBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "ZincBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.metroidMod, "ArcticPlating", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.metroidMod, "ChozoBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.metroidMod, "ChozoBrickNatural", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.metroidMod, "ExtraArcticPlating", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.metroidMod, "ItemPedestalTile", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.metroidMod, "ItemRoomTile", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.metroidMod, "NorfairBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.metroidMod, "NorfairBrickNatural", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.metroidMod, "TourianPipe", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.metroidMod, "TourianPipeAccent", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.metroidMod, "TourianPipeAccentNatural", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.metroidMod, "TourianPipeNatural", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.polaritiesMod, "GlowingLimestoneBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.polaritiesMod, "HaliteBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.polaritiesMod, "LimestoneBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.polaritiesMod, "SaltBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.qwertyMod, "ChiselledFortressBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.qwertyMod, "ReverseSand", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.qwertyMod, "DnasBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.qwertyMod, "FakeFortressBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.qwertyMod, "FortressBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.qwertyMod, "FortressPillar", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.redemptionMod, "Cyberscrap", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedKingSlayerIII
			}).AddModItemToShop(ModConditions.redemptionMod, "GathicColdstoneBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.redemptionMod, "GathicFroststoneBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.redemptionMod, "GathicGladestoneBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.redemptionMod, "GathicStoneBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.redemptionMod, "LabPlating", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.redemptionMod, "MetalSupportBeam", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMoonLord
			}).AddModItemToShop(ModConditions.redemptionMod, "NiricPipe", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedGolem
			}).AddModItemToShop(ModConditions.remnantsMod, "labtiles", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.remnantsMod, "industurialbeam", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.remnantsMod, "goldenbrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.remnantsMod, "hardstonebrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.remnantsMod, "hardstonetiles", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.remnantsMod, "seabrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.remnantsMod, "vaultbrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.remnantsMod, "vaultpipe", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "RoyalGoldBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPharaohsCurse
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DissolvingAuroraBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPermafrostSpirit
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "AvaritianPlating", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedOtherworldlyConstruct
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DissolvingBrillianceBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLux
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "ChaosPlating", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedChaosConstruct
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "CursedHiveBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPharaohsCurse
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DarkShingles", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DissolvingDelugeBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedTidalSpirit
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DullPlating", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedOtherworldlyConstruct
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DissolvingEarthBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedEarthenSpirit
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "EarthenPlating", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedEarthenConstruct
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "EvilPlating", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedEvilConstruct
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "EvostoneBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "FrigidBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "HardIceBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPolaris
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "HardlightBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedOtherworldlyConstruct
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "InfernoPlating", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedInfernoConstruct
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DissolvingNatureBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedNatureSpirit
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "NaturePlating", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedNatureConstruct
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DissolvingNetherBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedInfernoSpirit
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "OtherworldPlating", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedOtherworldlyConstruct
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "OvergrownPyramidBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAll
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "PermafrostPlating", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPermafrostConstruct
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DissolvingAetherBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAdvisor
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "PyramidBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPharaohsCurse
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "PyramidSlab", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPharaohsCurse
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "PyramidRubble", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPharaohsCurse
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "RuinedPyramidBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedPharaohsCurse
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "TidePlating", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedTidalConstruct
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "UltimatePlating", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedChaosConstruct,
				ModConditions.DownedEarthenConstruct,
				ModConditions.DownedEvilConstruct,
				ModConditions.DownedInfernoConstruct,
				ModConditions.DownedNatureConstruct,
				ModConditions.DownedOtherworldlyConstruct,
				ModConditions.DownedPermafrostConstruct,
				ModConditions.DownedTidalConstruct
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "DissolvingUmbraBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedEvilSpirit
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "VibrantBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "BricksAsthral", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedErazor
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "BricksEmber", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "FrostbitePanel", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedDecree
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "OblivionBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "MossShrineBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "RunedShrineBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "QuasarFragmentBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedCultist
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "ShrineBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "ShrineLight", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "ShrineShingles", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "BricksThermal", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.spiritMod, "AcidBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.spiritMod, "NeonBlockBlueItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedGoblinArmy
			}).AddModItemToShop(ModConditions.spiritMod, "NeonBlockGreenItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedGoblinArmy
			}).AddModItemToShop(ModConditions.spiritMod, "TechBlockItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedGoblinArmy
			}).AddModItemToShop(ModConditions.spiritMod, "NeonBlockPurpleItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedGoblinArmy
			}).AddModItemToShop(ModConditions.spiritMod, "NeonBlockRedItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedGoblinArmy
			}).AddModItemToShop(ModConditions.spiritMod, "SepulchreBrickTwoItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "SepulchreBrickItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "NeonBlockYellowItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedGoblinArmy
			}).AddModItemToShop(ModConditions.spookyMod, "CatacombBrick2Item", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToCatacombs
			}).AddModItemToShop(ModConditions.spookyMod, "GildedBrickItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToCatacombs
			}).AddModItemToShop(ModConditions.spookyMod, "CatacombBrick1Item", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToCatacombs
			}).AddModItemToShop(ModConditions.spookyMod, "SpookyStoneBricksItem", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToSpookyUnderground
			}).AddModItemToShop(ModConditions.starlightRiverMod, "TempleBrickItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.starlightRiverMod, "AncientSandstoneTileItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.starlightRiverMod, "GreenhouseGlassItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.starlightRiverMod, "BrickOvergrowItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.starlightRiverMod, "SkeletonBrickItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.starlightRiverMod, "VitricBrickItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.starlightRiverMod, "VitricGlassItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.terrorbornMod, "IncendiaryBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.terrorbornMod, "IncendiaryMachineryBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.terrorbornMod, "IncendiaryPipe", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.terrorbornMod, "SmoothDeimostoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.thoriumMod, "AquamarineGemsparkBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.thoriumMod, "ScaleBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.thoriumMod, "BloodstainedBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.thoriumMod, "BookshelfBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.thoriumMod, "CelestialBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.thoriumMod, "CelestialFragmentBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.thoriumMod, "CheckeredBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.thoriumMod, "CursedBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLich
			}).AddModItemToShop(ModConditions.thoriumMod, "CutSandstoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.thoriumMod, "CutSandstoneBlockSlab", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.thoriumMod, "CutStoneBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.thoriumMod, "CutStoneBlockSlab", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.thoriumMod, "GlowingMarineBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.thoriumMod, "IllumiteBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.thoriumMod, "LodestoneSlab", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedFallenBeholder
			}).AddModItemToShop(ModConditions.thoriumMod, "MediciteBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.thoriumMod, "NagaBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.thoriumMod, "OpalGemsparkBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.thoriumMod, "OrnateBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.thoriumMod, "PlateSlab", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.thoriumMod, "PotionShelfBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.thoriumMod, "RefinedMarineBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.thoriumMod, "ScarletBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.thoriumMod, "ShadyBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedPlantera
			}).AddModItemToShop(ModConditions.thoriumMod, "ShootingStarBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.thoriumMod, "ShootingStarFragmentBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.thoriumMod, "SmoothWood", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.thoriumMod, "ThoriumBrickBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.thoriumMod, "ThoriumBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.thoriumMod, "ValadiumPlating", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedFallenBeholder
			}).AddModItemToShop(ModConditions.thoriumMod, "WhiteDwarfBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.thoriumMod, "WhiteDwarfFragmentBlock", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLunarPillarAny
			}).AddModItemToShop(ModConditions.valhallaMod, "BurntCobble", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.valhallaMod, "CorrodeBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.valhallaMod, "HardGlassBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.valhallaMod, "HellstoneRoof", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.valhallaMod, "SinstoneBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc
			}).AddModItemToShop(ModConditions.valhallaMod, "Terracotta", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.valhallaMod, "ValhalliteBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.verdantMod, "GemsparkAquamarineItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.verdantMod, "LushWoodPlankBlock", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.verdantMod, "OvergrownBrickItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.verdantMod, "TrimmedOvergrownBrickItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.vitalityMod, "ArcticBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAny
			}).AddModItemToShop(ModConditions.vitalityMod, "BronzeBrick", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.vitalityMod, "GeraniumBrick", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedSkeletron
			}).Register();
			new NPCShop(base.Type, "Modded Herbs & Plants").AddModItemToShop(ModConditions.arbourMod, "ArborGrassSeeds", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.arbourMod, "MicrobirchAcorn", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.aequusMod, "MeadowGrass", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.aequusMod, "ManaclePollen", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedDemonSiege
			}).AddModItemToShop(ModConditions.aequusMod, "ManacleSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedDemonSiege
			}).AddModItemToShop(ModConditions.aequusMod, "MistralPollen", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedDustDevil
			}).AddModItemToShop(ModConditions.aequusMod, "MistralSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedDustDevil
			}).AddModItemToShop(ModConditions.aequusMod, "MoonflowerPollen", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedOmegaStarite
			}).AddModItemToShop(ModConditions.aequusMod, "MoonflowerSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedOmegaStarite
			}).AddModItemToShop(ModConditions.aequusMod, "MorayPollen", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedCrabson
			}).AddModItemToShop(ModConditions.aequusMod, "MoraySeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedCrabson
			}).AddModItemToShop(ModConditions.aequusMod, "RadonMoss", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.calamityMod, "AstralGrassSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.calamityMod, "CinderBlossomSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToCrags
			}).AddModItemToShop(ModConditions.calamityMod, "SpineSapling", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToCrags
			}).AddModItemToShop(ModConditions.calamityCommunityRemixMod, "Cosmichid", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedDevourerOfGods
			}).AddModItemToShop(ModConditions.calamityVanitiesMod, "AstralGrass", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.confectionRebakedMod, "CreamBeans", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToConfectionOrHallow
			}).AddModItemToShop(ModConditions.coraliteMod, "SlimeSapling", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.depthsMod, "MercuryMoss", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.depthsMod, "ShadowShrub", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.depthsMod, "ShadowShrubSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.everjadeMod, "JadeGrassSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToJadeLake
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Barfbush", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BarfbushSeeds", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Bloodberry", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "BloodberrySeeds", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Holybird", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "HolybirdSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Sweetstem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "SweetstemSeeds", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeBarfbush", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeBarfbushSeed", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeBlinkroot", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeBlinkrootSeed", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeBloodberry", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeBloodberrySeed", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeDaybloom", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeDaybloomSeed", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeDeathweed", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeDeathweedSeed", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeFireblossom", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeFireblossomSeed", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeHolybird", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeHolybirdSeed", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeMoonglow", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeMoonglowSeed", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeShiverthorn", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeShiverthornSeed", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeSweetstem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeSweetstemSeed", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeWaterleaf", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "LargeWaterleafSeed", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ContagionSeeds", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "VirulentMushroom", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "VirulentPowder", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "PeridotGemcorn", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "TourmalineGemcorn", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "ZirconGemcorn", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "CelestialGrassSeed", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.furnitureFoodAndFunMod, "GardenGrassSeed", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.gensokyoMod, "EssenceOfSpring", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedLilyWhite
			}).AddModItemToShop(ModConditions.redemptionMod, "AncientGrassSeeds", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.redemptionMod, "IrradiatedCorruptGrassSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToWasteland
			}).AddModItemToShop(ModConditions.redemptionMod, "IrradiatedCrimsonGrassSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToWasteland
			}).AddModItemToShop(ModConditions.redemptionMod, "IrradiatedGrassSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToWasteland
			}).AddModItemToShop(ModConditions.redemptionMod, "AnglonicMysticBlossom", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedThorn
			}).AddModItemToShop(ModConditions.redemptionMod, "Nightshade", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedThorn
			}).AddModItemToShop(ModConditions.redemptionMod, "NightshadeSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedThorn
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "Enduflora", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "EndufloraSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "Illumifern", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "IllumifernSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "Welkinbell", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "WelkinbellSeeds", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "CloudstalkItem", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "CloudstalkSeed", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "EnchantedLeaf", Item.buyPrice(0, 0, 0, 10)).AddModItemToShop(ModConditions.spiritMod, "GlowRoot", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToMushroom
			}).AddModItemToShop(ModConditions.spiritMod, "Kelp", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).AddModItemToShop(ModConditions.spiritMod, "SoulBloom", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToSpirit
			}).AddModItemToShop(ModConditions.spiritMod, "SoulSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToSpirit
			}).AddModItemToShop(ModConditions.spiritMod, "BriarGrassSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToBriar
			}).AddModItemToShop(ModConditions.spookyMod, "DampGrassSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToSpiderCave
			}).AddModItemToShop(ModConditions.spookyMod, "CemeteryGrassSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToCemetery
			}).AddModItemToShop(ModConditions.spookyMod, "SpookyMushGrassSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToEyeValley
			}).AddModItemToShop(ModConditions.spookyMod, "SpookySeedsGreen", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToSpookyForest
			}).AddModItemToShop(ModConditions.spookyMod, "SpookySeedsOrange", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToSpookyForest
			}).AddModItemToShop(ModConditions.spookyMod, "MushroomMossSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToSpookyUnderground
			}).AddModItemToShop(ModConditions.spookyMod, "EyeSeed", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToEyeValley
			}).AddModItemToShop(ModConditions.thoriumMod, "MarineKelp", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToAquaticDepths
			}).AddModItemToShop(ModConditions.thoriumMod, "MarineKelpSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToAquaticDepths
			}).AddModItemToShop(ModConditions.verdantMod, "LightbulbSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.verdantMod, "LushGrassSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.verdantMod, "MysteriaAcorn", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.verdantMod, "WisplantSeeds", Item.buyPrice(0, 0, 0, 10), new Condition[]
			{
				ModConditions.HasBeenToVerdant
			}).AddModItemToShop(ModConditions.wayfairContentMod, "Dirtroot", Item.buyPrice(0, 0, 0, 10)).Register();
			new NPCShop(base.Type, "Modded Fish & Fishing Gear").AddModItemToShop(ModConditions.aequusMod, "AnglerBroadcaster", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.aequusMod, "ArgonFish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.aequusMod, "Blobfish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.DownedGlimmer
			}).AddModItemToShop(ModConditions.aequusMod, "CrabDaughter", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToCrabCrevice
			}).AddModItemToShop(ModConditions.aequusMod, "Depthscale", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.aequusMod, "GoreFish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.DownedDemonSiege
			}).AddModItemToShop(ModConditions.aequusMod, "IcebergFish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.aequusMod, "KryptonFish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.aequusMod, "Leecheel", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.aequusMod, "RadonFish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.aequusMod, "ShimmerFish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.aequusMod, "HeatFish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.aequusMod, "XenonFish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.aequusMod, "CrateBait", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.aequusMod, "CursedPopper", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.aequusMod, "IchorPopper", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.aequusMod, "LegendberryBait", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.aequusMod, "MysticPopper", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.aequusMod, "Omnibait", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.aequusMod, "XenonBait", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.afkpetsMod, "FishermansPride", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.afkpetsMod, "PortableSonar", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				Condition.DownedSkeletron
			}).AddModItemToShop(ModConditions.afkpetsMod, "ArcherFish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.afkpetsMod, "Barkfin", Item.buyPrice(0, 0, 10, 0)).AddModItemToShop(ModConditions.afkpetsMod, "DragonShrimp", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.afkpetsMod, "HorseshoeScarab", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.afkpetsMod, "MushoWar", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.Hardmode
			}).AddModItemToShop(ModConditions.afkpetsMod, "Stargazer", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.afkpetsMod, "Sunfish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.afkpetsMod, "UraeEel", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToDesert
			}).AddModItemToShop(ModConditions.afkpetsMod, "HoneyBee", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).AddModItemToShop(ModConditions.afkpetsMod, "IceFairy", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.calamityMod, "AlluringBait", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToSulphurSea
			}).AddModItemToShop(ModConditions.calamityMod, "EnchantedPearl", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				ModConditions.DownedDesertScourge,
				ModConditions.HasBeenToSunkenSea
			}).AddModItemToShop(ModConditions.calamityMod, "SupremeBaitTackleBoxFishingStation", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSunkenSea
			}).AddModItemToShop(ModConditions.calamityMod, "AldebaranAlewife", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.calamityMod, "Bloodfin", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.DownedProvidence,
				ModConditions.HasBeenToCrags
			}).AddModItemToShop(ModConditions.calamityMod, "CharredLasher", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToCrags
			}).AddModItemToShop(ModConditions.calamityMod, "CoastalDemonfish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToCrags
			}).AddModItemToShop(ModConditions.calamityMod, "CragBullhead", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToCrags
			}).AddModItemToShop(ModConditions.calamityMod, "EnchantedStarfish", Item.buyPrice(0, 0, 10, 0)).AddModItemToShop(ModConditions.calamityMod, "FishofEleum", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSnow
			}).AddModItemToShop(ModConditions.calamityMod, "FishofFlight", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.calamityMod, "FishofLight", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).AddModItemToShop(ModConditions.calamityMod, "FishofNight", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEvil
			}).AddModItemToShop(ModConditions.calamityMod, "GlimmeringGemfish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).AddModItemToShop(ModConditions.calamityMod, "Gorecodile", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).AddModItemToShop(ModConditions.calamityMod, "GreenwaveLoach", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToSunkenSea
			}).AddModItemToShop(ModConditions.calamityMod, "Havocfish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.calamityMod, "PrismaticGuppy", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToSunkenSea
			}).AddModItemToShop(ModConditions.calamityMod, "ProcyonidPrawn", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.calamityMod, "Shadowfish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenThroughNight,
				ModConditions.HasBeenToCrags
			}).AddModItemToShop(ModConditions.calamityMod, "StuffedFish", Item.buyPrice(0, 0, 10, 0)).AddModItemToShop(ModConditions.calamityMod, "SunbeamFish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.calamityMod, "SunkenSailfish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToSunkenSea
			}).AddModItemToShop(ModConditions.calamityMod, "TwinklingPollox", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.calamityMod, "ArcturusAstroidean", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.calamityMod, "BabyGhostBellItem", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToSunkenSea
			}).AddModItemToShop(ModConditions.calamityMod, "BloodwormItem", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				ModConditions.DownedPolterghast,
				ModConditions.HasBeenToSulphurSea
			}).AddModItemToShop(ModConditions.calamityMod, "GrandMarquisBait", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.calamityMod, "SeaMinnowItem", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToSunkenSea
			}).AddModItemToShop(ModConditions.calamityMod, "TwinklerItem", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToAstral
			}).AddModItemToShop(ModConditions.depthsMod, "QuicksilverproofFishingHook", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.depthsMod, "QuicksilverproofTackleBag", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.depthsMod, "MercuryMossFishingBobber", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.depthsMod, "SilverLiner", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.depthsMod, "QuicksilverproofBugNet", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.depthsMod, "QuartzFeeder", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.depthsMod, "ShadowFightingFish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.depthsMod, "AlbinoRat", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.depthsMod, "EnchantedNightmareWorm", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.depthsMod, "QuartzCrawler", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.depthsMod, "QuicksilverBucket", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToDepthsOrUnderworld
			}).AddModItemToShop(ModConditions.everjadeMod, "KoiItem", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToJadeLake
			}).AddModItemToShop(ModConditions.everjadeMod, "JadeWaterfallBucket", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				ModConditions.HasBeenToJadeLake
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "Ickfish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToAvalonEvilBiomes
			}).AddModItemToShop(ModConditions.exxoAvalonOriginsMod, "SicklyTrout", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToAvalonEvilBiomes
			}).AddModItemToShop(ModConditions.martainsOrderMod, "TrapFish", Item.buyPrice(0, 0, 10, 0)).AddModItemToShop(ModConditions.martainsOrderMod, "VoidGill", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.DownedVoidDigger
			}).AddModItemToShop(ModConditions.martainsOrderMod, "Wormfish", Item.buyPrice(0, 0, 10, 0)).AddModItemToShop(ModConditions.redemptionMod, "BloatedTrout", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToWasteland
			}).AddModItemToShop(ModConditions.redemptionMod, "ToxicGlooper", Item.buyPrice(0, 2, 0, 0), new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToWasteland
			}).AddModItemToShop(ModConditions.redemptionMod, "AntiJohnSnailItem", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.redemptionMod, "CoastScarabItem", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.redemptionMod, "FlyBait", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.redemptionMod, "GrandLarvaBait", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.redemptionMod, "JohnSnailItem", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.redemptionMod, "KabucraItem", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.redemptionMod, "SandskinSpiderItem", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.redemptionMod, "SpiderSwarmerItem", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.redemptionMod, "TreeBugItem", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "Blazefury", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.shadowsOfAbaddonMod, "FieryFin", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.DownedAbaddon
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "Curgeon", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToPyramid
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "PhantomFish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToPyramid
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "TinyPlanetFish", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				ModConditions.HasBeenToSky
			}).AddModItemToShop(ModConditions.secretsOfTheShadowsMod, "SeaSnake", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToPyramid
			}).AddModItemToShop(ModConditions.spiritMod, "FisheyeGem", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.spiritMod, "KoiTotem", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.spiritMod, "MimicRepellent", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.spiritMod, "ReachFishingCatch", Item.buyPrice(0, 0, 10, 0)).AddModItemToShop(ModConditions.spiritMod, "SpiritKoi", Item.buyPrice(0, 0, 10, 0)).AddModItemToShop(ModConditions.thoriumMod, "HightechSonarDevice", Item.buyPrice(0, 2, 0, 0)).AddModItemToShop(ModConditions.thoriumMod, "FlamingCrackGut", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.thoriumMod, "MagmaGill", Item.buyPrice(0, 0, 10, 0), new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToUnderworld
			}).AddModItemToShop(ModConditions.zylonMod, "PaintedGlassTetra", Item.buyPrice(0, 0, 10, 0)).Register();
		}

		// Token: 0x0400004E RID: 78
		public static int shopNum;

		// Token: 0x0400004F RID: 79
		public static string ShopName;
	}
}
