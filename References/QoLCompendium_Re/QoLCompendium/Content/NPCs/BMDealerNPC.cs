using System;
using System.Collections.Generic;
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
	// Token: 0x020001F8 RID: 504
	[AutoloadHead]
	public class BMDealerNPC : ModNPC
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00021782 File Offset: 0x0001F982
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Content/NPCs/BMDealerNPC";
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00021789 File Offset: 0x0001F989
		public override bool IsLoadingEnabled(Mod mod)
		{
			return QoLCompendium.mainConfig.BlackMarketDealerCanSpawn;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00021798 File Offset: 0x0001F998
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
			base.NPC.Happiness.SetBiomeAffection<SnowBiome>(AffectionLevel.Love).SetBiomeAffection<OceanBiome>(AffectionLevel.Like).SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike).SetNPCAffection(550, AffectionLevel.Love).SetNPCAffection(19, AffectionLevel.Like).SetNPCAffection(208, AffectionLevel.Dislike).SetNPCAffection(441, AffectionLevel.Hate);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000218A0 File Offset: 0x0001FAA0
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				new FlavorTextBestiaryInfoElement("He hails from a far away land to sell items that are difficult to get, but how did he even obtain them...")
			});
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x000218C8 File Offset: 0x0001FAC8
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
			base.NPC.HitSound = new SoundStyle?(SoundID.NPCHit1);
			base.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath1);
			base.NPC.knockBackResist = 0.5f;
			base.AnimationType = 22;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0002197F File Offset: 0x0001FB7F
		public override bool CanTownNPCSpawn(int numTownNPCs)
		{
			return QoLCompendium.mainConfig.BlackMarketDealerCanSpawn;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00021990 File Offset: 0x0001FB90
		public override List<string> SetNPCNameList()
		{
			List<string> list = new List<string>();
			list.Add("Bon");
			list.Add("Ned");
			list.Add("Jay");
			list.Add("Jack");
			list.Add("Jabon");
			return list;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x000219CE File Offset: 0x0001FBCE
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

		// Token: 0x06000B41 RID: 2881 RVA: 0x000219FF File Offset: 0x0001FBFF
		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 15;
			randExtraCooldown = 5;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00021A08 File Offset: 0x0001FC08
		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = 30;
			attackDelay = 1;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00021A11 File Offset: 0x0001FC11
		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 12f;
			randomOffset = 1f;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00021A24 File Offset: 0x0001FC24
		public override string GetChat()
		{
			string result;
			switch (Main.rand.Next(4))
			{
			case 0:
				result = "Illegal items are my specialty";
				break;
			case 1:
				result = "Hand over some money, and I'll hook you up";
				break;
			case 2:
				result = "Don't ask where I got this stuff";
				break;
			default:
				result = "Hey kid, want some items?";
				break;
			}
			return result;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00021A70 File Offset: 0x0001FC70
		public override void SetChatButtons(ref string button, ref string button2)
		{
			if (BMDealerNPC.shopNum == 0)
			{
				button = "Potions";
				BMDealerNPC.ShopName = "Potions";
			}
			else if (BMDealerNPC.shopNum == 1)
			{
				button = "Flasks, Stations & Foods";
				BMDealerNPC.ShopName = "Flasks, Stations & Foods";
			}
			else if (BMDealerNPC.shopNum == 2)
			{
				button = "Materials";
				BMDealerNPC.ShopName = "Materials";
			}
			else if (BMDealerNPC.shopNum == 3)
			{
				button = "Movement Accessories";
				BMDealerNPC.ShopName = "Movement Accessories";
			}
			else if (BMDealerNPC.shopNum == 4)
			{
				button = "Combat Accessories";
				BMDealerNPC.ShopName = "Combat Accessories";
			}
			else if (BMDealerNPC.shopNum == 5)
			{
				button = "Informative/Building Gear";
				BMDealerNPC.ShopName = "Informative/Building Gear";
			}
			else if (BMDealerNPC.shopNum == 6)
			{
				button = "Treasure Bags";
				BMDealerNPC.ShopName = "Treasure Bags";
			}
			else if (BMDealerNPC.shopNum == 7)
			{
				button = "Crates & Grab Bags";
				BMDealerNPC.ShopName = "Crates & Grab Bags";
			}
			else if (BMDealerNPC.shopNum == 8)
			{
				button = "Ores & Bars";
				BMDealerNPC.ShopName = "Ores & Bars";
			}
			else if (BMDealerNPC.shopNum == 9)
			{
				button = "Natural Blocks";
				BMDealerNPC.ShopName = "Natural Blocks";
			}
			else if (BMDealerNPC.shopNum == 10)
			{
				button = "Building Blocks";
				BMDealerNPC.ShopName = "Building Blocks";
			}
			else if (BMDealerNPC.shopNum == 11)
			{
				button = "Herbs & Plants";
				BMDealerNPC.ShopName = "Herbs & Plants";
			}
			else if (BMDealerNPC.shopNum == 12)
			{
				button = "Fish & Fishing Gear";
				BMDealerNPC.ShopName = "Fish & Fishing Gear";
			}
			else if (BMDealerNPC.shopNum == 13)
			{
				button = "Mounts & Hooks";
				BMDealerNPC.ShopName = "Mounts & Hooks";
			}
			else if (BMDealerNPC.shopNum == 14)
			{
				button = "Ammo";
				BMDealerNPC.ShopName = "Ammo";
			}
			button2 = "Shop Changer";
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00021C3A File Offset: 0x0001FE3A
		public override void OnChatButtonClicked(bool firstButton, ref string shop)
		{
			if (firstButton)
			{
				shop = BMDealerNPC.ShopName;
				BlackMarketDealerNPCUI.visible = false;
				return;
			}
			if (!BlackMarketDealerNPCUI.visible)
			{
				BlackMarketDealerNPCUI.timeStart = Main.GameUpdateCount;
			}
			BlackMarketDealerNPCUI.visible = true;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00021C64 File Offset: 0x0001FE64
		public override void AddShops()
		{
			new NPCShop(base.Type, "Potions").Add(new Item(2344, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(353, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, Array.Empty<Condition>()).Add(new Item(303, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToPurity
			}).Add(new Item(300, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Add(new Item(5211, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				Condition.DownedEyeOfCthulhu,
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(2325, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).Add(new Item(2324, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSky
			}).Add(new Item(2356, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(2329, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).Add(new Item(2346, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(295, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSky
			}).Add(new Item(2354, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(2327, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).Add(new Item(291, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(305, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSky
			}).Add(new Item(4479, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(2323, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Add(new Item(304, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(2348, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(297, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(292, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(2345, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).Add(new Item(294, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(293, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(2322, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(299, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToPurity
			}).Add(new Item(288, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(2347, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Add(new Item(678, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				Condition.ForTheWorthyWorld
			}).Add(new Item(289, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToPurity
			}).Add(new Item(2266, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, Array.Empty<Condition>()).Add(new Item(298, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToMushroom
			}).Add(new Item(2355, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(296, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(2328, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(290, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(301, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(2326, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDungeon
			}).Add(new Item(2359, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).Add(new Item(302, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(2349, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Register();
			new NPCShop(base.Type, "Flasks, Stations & Foods").Add(new Item(4624, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, Array.Empty<Condition>()).Add(new Item(4403, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, Array.Empty<Condition>()).Add(new Item(4022, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, Array.Empty<Condition>()).Add(new Item(1353, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedQueenBee,
				ModConditions.HasBeenToEvil
			}).Add(new Item(1354, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				Condition.DownedQueenBee,
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(1355, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedQueenBee
			}).Add(new Item(1356, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedQueenBee,
				ModConditions.HasBeenToEvil
			}).Add(new Item(1357, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				Condition.DownedQueenBee
			}).Add(new Item(1358, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				Condition.DownedQueenBee
			}).Add(new Item(1359, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				Condition.DownedQueenBee,
				ModConditions.HasBeenToJungle
			}).Add(new Item(1340, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				Condition.DownedQueenBee,
				ModConditions.HasBeenToJungle
			}).Add(new Item(28, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, Array.Empty<Condition>()).Add(new Item(188, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToMushroom
			}).Add(new Item(499, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).Add(new Item(3544, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.DownedLunarEvent
			}).Add(new Item(110, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, Array.Empty<Condition>()).Add(new Item(189, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToMushroom
			}).Add(new Item(500, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(2209, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).Add(new Item(227, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToMushroom
			}).Add(new Item(126, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, Array.Empty<Condition>()).Add(new Item(1134, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(3001, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				ModConditions.HasTalkedToSkeletonMerchant
			}).Add(new Item(2756, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, Array.Empty<Condition>()).Add(new Item(4870, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(2350, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, Array.Empty<Condition>()).Add(new Item(2351, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).Add(new Item(2997, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0))
			}, Array.Empty<Condition>()).Add(new Item(63, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(966, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(487, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).Add(new Item(2177, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(3198, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(2999, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).Add(new Item(3814, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				Condition.DownedOldOnesArmyAny
			}).Add(new Item(4276, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(3750, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1431, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).Add(new Item(1859, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(4609, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(3117, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(148, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).Add(new Item(5322, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Add(new Item(29, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(1291, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToJungle
			}).Add(new Item(109, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).Add(new Item(5337, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToAether
			}).Add(new Item(5339, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToAether
			}).Add(new Item(5338, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToAether
			}).Add(new Item(5342, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToAether
			}).Add(new Item(5341, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToAether
			}).Add(new Item(5340, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToAether
			}).Add(new Item(5343, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasTalkedToTravelingMerchant
			}).Add(new Item(5326, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasTalkedToSkeletonMerchant
			}).Add(new Item(4382, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).Add(new Item(5336, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToAether
			}).Register();
			new NPCShop(base.Type, "Materials").Add(new Item(3794, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToDesert
			}).Add(new Item(323, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(2218, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToTemple
			}).Add(new Item(2431, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedQueenBee,
				ModConditions.HasBeenToJungle
			}).Add(new Item(1811, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedPumpking
			}).Add(new Item(1119, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(236, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).Add(new Item(4413, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(254, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletron
			}).Add(new Item(154, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).Add(new Item(1517, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToDungeon
			}).Add(new Item(1520, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.DownedEclipse
			}).Add(new Item(1570, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.DownedEclipse
			}).Add(new Item(1611, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToJungle
			}).Add(new Item(522, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEvil
			}).Add(new Item(1116, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).Add(new Item(527, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEvil,
				ModConditions.HasBeenToDesert
			}).Add(new Item(1508, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToDungeon
			}).Add(new Item(1347, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(75, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).Add(new Item(320, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSky
			}).Add(new Item(1518, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(5070, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).Add(new Item(3783, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToDesert
			}).Add(new Item(2161, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSnow
			}).Add(new Item(23, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, Array.Empty<Condition>()).Add(new Item(1516, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSky
			}).Add(new Item(1348, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(255, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(118, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(1519, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSnow
			}).Add(new Item(1332, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEvil
			}).Add(new Item(324, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).Add(new Item(331, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(259, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Add(new Item(38, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).Add(new Item(528, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow,
				ModConditions.HasBeenToDesert
			}).Add(new Item(3356, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletronPrime,
				ModConditions.expertOrMaster
			}).Add(new Item(3355, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedDestroyer,
				ModConditions.expertOrMaster
			}).Add(new Item(3354, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedTwins,
				ModConditions.expertOrMaster
			}).Add(new Item(1346, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(3457, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedNebulaPillar
			}).Add(new Item(3111, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, Array.Empty<Condition>()).Add(new Item(4414, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(981, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletron
			}).Add(new Item(501, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).Add(new Item(1118, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(1115, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(68, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Add(new Item(86, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToEvil
			}).Add(new Item(319, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(225, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(2766, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToTemple
			}).Add(new Item(3458, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedSolarPillar
			}).Add(new Item(575, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSky
			}).Add(new Item(520, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(521, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEvil,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(548, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedDestroyer
			}).Add(new Item(549, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedTwins
			}).Add(new Item(547, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletronPrime
			}).Add(new Item(2607, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(1831, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedMourningWood
			}).Add(new Item(3459, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedStardustPillar
			}).Add(new Item(209, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(1521, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.HasBeenToJungle
			}).Add(new Item(362, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(1329, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToEvil
			}).Add(new Item(1328, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToJungle
			}).Add(new Item(526, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).Add(new Item(1330, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Add(new Item(1339, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(210, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(1117, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(3456, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedVortexPillar
			}).Add(new Item(4412, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(215, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(69, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Register();
			new NPCShop(base.Type, "Movement Accessories").Add(new Item(285, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(212, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(3225, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(987, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).Add(new Item(953, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(53, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(268, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(4055, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(187, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(4978, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSky
			}).Add(new Item(3017, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(934, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(2423, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(54, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(950, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).Add(new Item(4404, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(1303, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(906, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(158, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(5107, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Add(new Item(485, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.DownedBloodMoon
			}).Add(new Item(497, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.DownedEclipse
			}).Add(new Item(128, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedGoblinArmy
			}).Add(new Item(3200, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(857, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(159, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSky
			}).Add(new Item(975, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(977, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToDungeon
			}).Add(new Item(3201, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(863, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Register();
			new NPCShop(base.Type, "Combat Accessories").Add(new Item(885, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToJungle
			}).Add(new Item(3809, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedOldOnesArmyAny
			}).Add(new Item(886, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(935, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedMechBossAll
			}).Add(new Item(49, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(111, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Add(new Item(887, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(963, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToDungeon
			}).Add(new Item(3309, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(888, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(2219, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSky
			}).Add(new Item(156, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).Add(new Item(554, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(1301, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedGolem
			}).Add(new Item(1248, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToTemple
			}).Add(new Item(889, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(211, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(3016, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEvil
			}).Add(new Item(1253, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSnow
			}).Add(new Item(1921, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).Add(new Item(1167, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToJungle
			}).Add(new Item(1132, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedQueenBee,
				ModConditions.HasBeenToJungle
			}).Add(new Item(3811, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedOldOnesArmyT2
			}).Add(new Item(1321, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(1322, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(890, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).Add(new Item(3812, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedOldOnesArmyT2
			}).Add(new Item(900, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedMechBossAny,
				ModConditions.DownedEclipse
			}).Add(new Item(223, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(891, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).Add(new Item(1845, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedMourningWood
			}).Add(new Item(1323, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(193, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(938, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToDungeon
			}).Add(new Item(1290, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Add(new Item(535, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(3781, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(3015, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEvil
			}).Add(new Item(1158, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedQueenBee
			}).Add(new Item(491, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(1300, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToDungeon
			}).Add(new Item(216, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenThroughNight
			}).Add(new Item(3212, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.DownedBloodMoon,
				ModConditions.HasBeenToOcean
			}).Add(new Item(489, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(3810, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedOldOnesArmyAny
			}).Add(new Item(532, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(2998, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(899, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.HasBeenToTemple
			}).Add(new Item(536, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(893, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(892, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEvil
			}).Add(new Item(490, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(3306, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(3334, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Register();
			new NPCShop(base.Type, "Informative/Building Gear").Add(new Item(407, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1923, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(3061, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.NotDownedSkeletron
			}).Add(new Item(4056, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.NotDownedSkeletron,
				ModConditions.HasBeenToDesert
			}).Add(new Item(5126, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletron
			}).Add(new Item(3624, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasTalkedToTravelingMerchant
			}).Add(new Item(4409, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(1071, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.NotDownedPlantera
			}).Add(new Item(1072, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.NotDownedPlantera
			}).Add(new Item(1100, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.NotDownedPlantera
			}).Add(new Item(1543, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(1544, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(1545, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(4008, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(410, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(411, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(213, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(709, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(18, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(393, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(3084, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(3095, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).Add(new Item(3118, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(3119, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(3099, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(3102, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(3120, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(3037, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(3096, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(50, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(4263, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(4819, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(5358, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(1861, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).Add(new Item(854, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPirates
			}).Add(new Item(855, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPirates
			}).Add(new Item(3033, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPirates
			}).Add(new Item(3611, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).Add(new Item(3213, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.DownedBloodMoon
			}).Add(new Item(3068, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(5323, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(3090, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedKingSlime,
				Condition.InExpertMode
			}).Add(new Item(5043, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(327, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).Add(new Item(329, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).Add(new Item(1534, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToEvil
			}).Add(new Item(1535, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToEvil
			}).Add(new Item(4714, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToDesert
			}).Add(new Item(1537, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToSnow
			}).Add(new Item(1536, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToHallow
			}).Add(new Item(1533, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToJungle
			}).Add(new Item(1141, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToJungle
			}).Add(new Item(206, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(207, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(1128, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(3031, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4820, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(5302, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(5364, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedMoonLord,
				ModConditions.HasBeenToAether
			}).Add(new Item(3032, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(5303, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4872, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1299, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).Add(new Item(1786, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4049, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4346, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(3384, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedMoonLord
			}).Add(new Item(1326, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).Add(new Item(5335, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.ShimmerableAfterMoonLordOrSupremeCalamitas,
				ModConditions.HasBeenToAether
			}).Add(new Item(779, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedMechBossAny,
				Condition.NotDownedMoonLord
			}).Add(new Item(5134, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedMoonLord,
				ModConditions.HasBeenToAether
			}).Register();
			new NPCShop(base.Type, "Treasure Bags").Add(new Item(3318, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0))
			}, new Condition[]
			{
				Condition.DownedKingSlime,
				ModConditions.expertOrMaster
			}).Add(new Item(3319, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0))
			}, new Condition[]
			{
				Condition.DownedEyeOfCthulhu,
				ModConditions.expertOrMaster
			}).Add(new Item(3320, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.expertOrMaster
			}).Add(new Item(3321, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.expertOrMaster
			}).Add(new Item(3322, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0))
			}, new Condition[]
			{
				Condition.DownedQueenBee,
				ModConditions.expertOrMaster
			}).Add(new Item(3323, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.expertOrMaster
			}).Add(new Item(5111, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0))
			}, new Condition[]
			{
				Condition.DownedDeerclops,
				ModConditions.expertOrMaster
			}).Add(new Item(3324, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.expertOrMaster
			}).Add(new Item(4957, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0))
			}, new Condition[]
			{
				Condition.DownedQueenSlime,
				ModConditions.expertOrMaster
			}).Add(new Item(3325, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0))
			}, new Condition[]
			{
				Condition.DownedDestroyer,
				ModConditions.expertOrMaster
			}).Add(new Item(3326, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0))
			}, new Condition[]
			{
				Condition.DownedTwins,
				ModConditions.expertOrMaster
			}).Add(new Item(3327, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletronPrime,
				ModConditions.expertOrMaster
			}).Add(new Item(3328, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.expertOrMaster
			}).Add(new Item(3329, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0))
			}, new Condition[]
			{
				Condition.DownedGolem,
				ModConditions.expertOrMaster
			}).Add(new Item(3860, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0))
			}, new Condition[]
			{
				Condition.DownedOldOnesArmyT3,
				ModConditions.expertOrMaster
			}).Add(new Item(3330, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0))
			}, new Condition[]
			{
				Condition.DownedDukeFishron,
				ModConditions.expertOrMaster
			}).Add(new Item(4782, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0))
			}, new Condition[]
			{
				Condition.DownedEmpressOfLight,
				ModConditions.expertOrMaster
			}).Add(new Item(3332, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0))
			}, new Condition[]
			{
				Condition.DownedMoonLord,
				ModConditions.expertOrMaster
			}).Add(new Item(3817, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 1, 0, 0))
			}, new Condition[]
			{
				Condition.DownedOldOnesArmyAny
			}).Register();
			new NPCShop(base.Type, "Crates & Grab Bags").Add(new Item(2334, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(2335, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(2336, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(3208, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(3206, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSky
			}).Add(new Item(3203, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Add(new Item(3204, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Add(new Item(3207, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToHallow
			}).Add(new Item(3205, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).Add(new Item(4405, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).Add(new Item(4407, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(4877, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(5002, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(3979, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToOcean
			}).Add(new Item(3980, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToOcean
			}).Add(new Item(3981, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToOcean
			}).Add(new Item(3987, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToJungle
			}).Add(new Item(3985, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSky
			}).Add(new Item(3982, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEvil
			}).Add(new Item(3983, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToEvil
			}).Add(new Item(3986, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).Add(new Item(3984, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				Condition.DownedSkeletron,
				ModConditions.HasBeenToDungeon
			}).Add(new Item(4406, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToSnow
			}).Add(new Item(4408, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToDesert
			}).Add(new Item(4878, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(5003, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToOcean
			}).Add(new Item(1774, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1869, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, Array.Empty<Condition>()).Register();
			new NPCShop(base.Type, "Ores & Bars").Add(new Item(12, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(699, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(11, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(700, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(14, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(701, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(13, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(702, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(116, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc
			}).Add(new Item(56, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToEvil
			}).Add(new Item(880, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToEvil
			}).Add(new Item(174, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(364, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(1104, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(365, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(1105, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(366, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(1106, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(947, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedMechBossAll,
				ModConditions.HasBeenToJungle
			}).Add(new Item(3460, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0))
			}, new Condition[]
			{
				Condition.DownedMoonLord
			}).Add(new Item(20, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(703, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(22, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(704, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(21, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(705, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(19, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(706, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(117, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc
			}).Add(new Item(57, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToEvil
			}).Add(new Item(1257, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToEvil
			}).Add(new Item(175, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(381, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(1184, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(382, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(1191, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(391, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(1198, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(1225, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				Condition.DownedMechBossAny
			}).Add(new Item(1006, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				Condition.DownedMechBossAll,
				ModConditions.HasBeenToJungle
			}).Add(new Item(3261, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToDungeon
			}).Add(new Item(1552, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera,
				ModConditions.HasBeenToMushroom
			}).Add(new Item(3467, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				Condition.DownedMoonLord
			}).Add(new Item(502, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).Add(new Item(3380, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(999, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(181, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(182, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(179, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(178, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(177, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(180, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(4646, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(4640, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(4645, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(4643, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(4644, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(4642, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(4641, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(4400, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Register();
			new NPCShop(base.Type, "Natural Blocks").Add(new Item(9, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(2503, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(2504, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(620, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(619, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(911, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(5215, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(621, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(1729, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedMourningWood
			}).Add(new Item(2260, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(4564, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(4547, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(276, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1725, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1872, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(2, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(133, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(176, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(172, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(424, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1103, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(593, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(169, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(370, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1246, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(408, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(3, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(61, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(836, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(409, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(664, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(833, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(835, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(834, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(3086, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3081, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(173, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(5349, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3272, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3274, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3275, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3338, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(3271, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3276, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3277, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3339, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(3347, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(4090, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(751, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(765, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1124, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1125, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1127, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1727, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(150, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(933, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(832, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(3361, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(3360, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1129, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.DownedQueenBee
			}).Add(new Item(932, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletron
			}).Add(new Item(496, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(114, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).Add(new Item(5324, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(4460, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0))
			}, Array.Empty<Condition>()).Register();
			new NPCShop(base.Type, "Building Blocks").Add(new Item(129, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(2119, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(4962, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(131, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(607, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(594, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(883, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(414, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(413, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(192, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(609, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(4050, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(412, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(662, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(145, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(717, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3951, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3953, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(143, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(718, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(141, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(719, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(577, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).Add(new Item(2793, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).Add(new Item(3100, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc
			}).Add(new Item(214, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc
			}).Add(new Item(415, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(1589, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(416, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(1591, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(604, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(1593, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(2792, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedMechBossAll
			}).Add(new Item(2794, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(3461, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedMoonLord
			}).Add(new Item(5398, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(5439, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(5441, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(5442, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(5440, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(5443, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(5444, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(134, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedSkeletron
			}).Add(new Item(137, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedSkeletron
			}).Add(new Item(139, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedSkeletron
			}).Add(new Item(1101, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedGolem
			}).Add(new Item(4231, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedNebulaPillar
			}).Add(new Item(4229, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedSolarPillar
			}).Add(new Item(4232, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedStardustPillar
			}).Add(new Item(4230, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedVortexPillar
			}).Add(new Item(614, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(613, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(611, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(612, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3087, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3066, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(824, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(4392, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(2860, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedMartians
			}).Add(new Item(170, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(1976, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1970, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1975, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1973, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1974, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1972, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1971, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3621, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3633, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3634, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3635, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3636, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3637, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(3575, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedNebulaPillar
			}).Add(new Item(3573, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedSolarPillar
			}).Add(new Item(3576, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedStardustPillar
			}).Add(new Item(3574, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedVortexPillar
			}).Add(new Item(2261, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(2262, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(586, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(591, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(775, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedMechBossAny
			}).Add(new Item(2435, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(763, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(3955, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(4139, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(762, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedKingSlime
			}).Add(new Item(3113, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedKingSlime
			}).Add(new Item(2701, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(2751, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(2752, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(2753, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(2754, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(2755, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Register();
			new NPCShop(base.Type, "Herbs & Plants").Add(new Item(3093, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(315, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(313, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(307, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(316, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(310, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(318, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(312, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(314, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(308, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(2358, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(2357, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(317, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(311, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1828, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).Add(new Item(1107, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1108, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1109, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1110, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1111, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1112, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1113, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(1114, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(60, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Add(new Item(2887, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Add(new Item(5, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(183, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				ModConditions.HasBeenToMushroom
			}).Add(new Item(62, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).Add(new Item(195, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(194, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(59, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).Add(new Item(2171, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedEyeOfCthulhu
			}).Add(new Item(5214, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(369, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(4352, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(4350, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(4349, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(4354, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(4353, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(4351, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(4389, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(4377, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(5127, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(4378, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(5128, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(275, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(2626, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(2625, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(4071, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(4072, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(4073, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(27, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, Array.Empty<Condition>()).Add(new Item(5438, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10))
			}, new Condition[]
			{
				Condition.DownedSkeletron
			}).Register();
			new NPCShop(base.Type, "Fish & Fishing Gear").Add(new Item(3721, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.NotDownedEowOrBoc,
				ModConditions.HasBeenToOcean
			}).Add(new Item(5064, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToOcean
			}).Add(new Item(5139, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(2367, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(2368, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(2369, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(4325, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.DownedBloodMoon,
				ModConditions.HasBeenToOcean
			}).Add(new Item(2422, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToOcean
			}).Add(new Item(2294, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.AnglerQuestsFinishedOver(1),
				ModConditions.HasBeenToOcean
			}).Add(new Item(3183, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(4608, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(2303, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(2299, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).Add(new Item(2290, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, Array.Empty<Condition>()).Add(new Item(2317, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).Add(new Item(2305, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Add(new Item(2304, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSky
			}).Add(new Item(2313, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(2318, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Add(new Item(2312, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(4401, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(2306, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToSnow
			}).Add(new Item(2308, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(2319, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToEvil
			}).Add(new Item(2314, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(2302, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(2315, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc,
				ModConditions.HasBeenToUnderworld
			}).Add(new Item(2307, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).Add(new Item(2310, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				ModConditions.HasBeenToHallow
			}).Add(new Item(2301, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(4402, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToDesert
			}).Add(new Item(2298, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, Array.Empty<Condition>()).Add(new Item(2316, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(2309, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(2321, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToCavernsOrUnderground
			}).Add(new Item(2297, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, Array.Empty<Condition>()).Add(new Item(2300, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, Array.Empty<Condition>()).Add(new Item(2311, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(2674, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(2675, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(2676, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToOcean
			}).Add(new Item(4334, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(2156, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4335, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(2436, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(3194, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(3191, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1992, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(2007, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(2891, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4340, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(2893, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4362, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4419, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(2895, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(2740, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4336, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(2437, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(3192, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4845, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc
			}).Add(new Item(2001, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4361, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4847, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc
			}).Add(new Item(2004, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(4363, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4849, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc
			}).Add(new Item(1994, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4337, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(2438, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1995, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1996, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4338, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(2157, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(3193, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(2006, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(5132, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1998, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1999, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(2673, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(1997, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4418, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(2002, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4339, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(2000, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Register();
			new NPCShop(base.Type, "Mounts & Hooks").Add(new Item(2430, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedKingSlime
			}).Add(new Item(2502, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedQueenBee
			}).Add(new Item(2491, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(2428, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4791, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4264, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletron
			}).Add(new Item(4716, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4785, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4786, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4787, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4828, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc
			}).Add(new Item(3771, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(5130, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(3260, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(2429, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(4981, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedQueenSlime
			}).Add(new Item(1914, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedIceQueen
			}).Add(new Item(2771, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedMartians
			}).Add(new Item(2769, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedMartians
			}).Add(new Item(4444, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedMourningWood,
				Condition.InExpertMode
			}).Add(new Item(3367, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedDukeFishron,
				Condition.InExpertMode
			}).Add(new Item(2768, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedMoonLord
			}).Add(new Item(4796, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedOldOnesArmyT1,
				Condition.InMasterMode
			}).Add(new Item(4795, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode,
				Condition.InMasterMode
			}).Add(new Item(4792, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPirates,
				Condition.InMasterMode
			}).Add(new Item(4793, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedMourningWood,
				Condition.InMasterMode
			}).Add(new Item(4794, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedSantaNK1,
				Condition.InMasterMode
			}).Add(new Item(84, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1236, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1237, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1238, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1239, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4257, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1240, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1241, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(185, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				ModConditions.HasBeenToJungle
			}).Add(new Item(2585, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedKingSlime
			}).Add(new Item(2360, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(4759, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1800, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1915, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(939, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, Array.Empty<Condition>()).Add(new Item(1273, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedSkeletron
			}).Add(new Item(437, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(3020, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(3023, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(3022, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(4980, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedQueenSlime
			}).Add(new Item(3623, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedMechBossAny
			}).Add(new Item(3021, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(1829, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedMourningWood
			}).Add(new Item(1916, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedEverscream
			}).Add(new Item(2800, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedMartians
			}).Add(new Item(3572, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0))
			}, new Condition[]
			{
				Condition.DownedCultist
			}).Register();
			new NPCShop(base.Type, "Ammo").Add(new Item(97, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, Array.Empty<Condition>()).Add(new Item(278, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, Array.Empty<Condition>()).Add(new Item(4915, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, Array.Empty<Condition>()).Add(new Item(1349, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, Array.Empty<Condition>()).Add(new Item(234, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc
			}).Add(new Item(515, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(546, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(1335, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(1302, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedMechBossAny
			}).Add(new Item(1352, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(1351, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(1179, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedMechBossAll
			}).Add(new Item(1342, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(1350, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(3567, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedMoonLord
			}).Add(new Item(3104, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(40, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, Array.Empty<Condition>()).Add(new Item(41, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, Array.Empty<Condition>()).Add(new Item(988, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, Array.Empty<Condition>()).Add(new Item(51, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, Array.Empty<Condition>()).Add(new Item(47, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, Array.Empty<Condition>()).Add(new Item(3003, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				ModConditions.HasTalkedToSkeletonMerchant
			}).Add(new Item(5348, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				ModConditions.HasBeenToAether
			}).Add(new Item(265, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedEowOrBoc
			}).Add(new Item(545, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(1334, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(516, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(1235, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedMechBossAll
			}).Add(new Item(1341, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(3568, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedMoonLord
			}).Add(new Item(3103, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(771, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(772, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(773, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(774, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(4459, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(4447, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(4448, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(4449, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(4457, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(4458, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedPlantera
			}).Add(new Item(4445, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedMartians
			}).Add(new Item(4446, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedMartians
			}).Add(new Item(283, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, Array.Empty<Condition>()).Add(new Item(1310, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, Array.Empty<Condition>()).Add(new Item(3010, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(3011, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(3009, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(931, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, Array.Empty<Condition>()).Add(new Item(1614, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, Array.Empty<Condition>()).Add(new Item(5377, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, Array.Empty<Condition>()).Add(new Item(5380, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				ModConditions.HasBeenToAether
			}).Add(new Item(5378, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(5379, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.Hardmode
			}).Add(new Item(949, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, Array.Empty<Condition>()).Add(new Item(1261, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedGolem
			}).Add(new Item(1783, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedPumpking
			}).Add(new Item(1785, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedPumpking
			}).Add(new Item(1836, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedMourningWood
			}).Add(new Item(3108, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				ModConditions.DownedEclipse
			}).Add(new Item(780, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedMechBossAny
			}).Add(new Item(5392, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedMechBossAny
			}).Add(new Item(5393, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedMechBossAny
			}).Add(new Item(5394, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedMechBossAny
			}).Add(new Item(782, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedMechBossAny
			}).Add(new Item(784, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedMechBossAny
			}).Add(new Item(783, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedMechBossAny
			}).Add(new Item(781, 1, 0)
			{
				shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1))
			}, new Condition[]
			{
				Condition.DownedMechBossAny
			}).Register();
		}

		// Token: 0x0400004C RID: 76
		public static int shopNum;

		// Token: 0x0400004D RID: 77
		public static string ShopName;
	}
}
