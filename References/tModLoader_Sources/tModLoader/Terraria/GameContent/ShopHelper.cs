using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terraria.GameContent
{
	// Token: 0x020004B5 RID: 1205
	public class ShopHelper
	{
		// Token: 0x060039D2 RID: 14802 RVA: 0x0059BF93 File Offset: 0x0059A193
		internal void ReinitializePersonalityDatabase()
		{
			this._database = new PersonalityDatabase();
			new PersonalityDatabasePopulator().Populate(this._database);
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x0059BFB0 File Offset: 0x0059A1B0
		public ShoppingSettings GetShoppingSettings(Player player, NPC npc)
		{
			ShoppingSettings result = new ShoppingSettings
			{
				PriceAdjustment = 1.0,
				HappinessReport = ""
			};
			this._currentNPCBeingTalkedTo = npc;
			this._currentPlayerTalking = player;
			this.ProcessMood(player, npc);
			result.PriceAdjustment = (double)this._currentPriceAdjustment;
			result.HappinessReport = this._currentHappiness;
			return result;
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x0059C018 File Offset: 0x0059A218
		private float GetSkeletonMerchantPrices(NPC npc)
		{
			float num = 1f;
			if (Main.moonPhase == 1 || Main.moonPhase == 7)
			{
				num = 1.1f;
			}
			if (Main.moonPhase == 2 || Main.moonPhase == 6)
			{
				num = 1.2f;
			}
			if (Main.moonPhase == 3 || Main.moonPhase == 5)
			{
				num = 1.3f;
			}
			if (Main.moonPhase == 4)
			{
				num = 1.4f;
			}
			if (Main.dayTime)
			{
				num += 0.1f;
			}
			return num;
		}

		// Token: 0x060039D5 RID: 14805 RVA: 0x0059C08C File Offset: 0x0059A28C
		private float GetTravelingMerchantPrices(NPC npc)
		{
			Vector2 vector = npc.Center / 16f;
			Vector2 value2;
			value2..ctor((float)Main.spawnTileX, (float)Main.spawnTileY);
			float num = Vector2.Distance(vector, value2) / (float)(Main.maxTilesX / 2);
			num = 1.5f - num;
			return (2f + num) / 3f;
		}

		// Token: 0x060039D6 RID: 14806 RVA: 0x0059C0E4 File Offset: 0x0059A2E4
		private void ProcessMood(Player player, NPC npc)
		{
			this._currentHappiness = "";
			this._currentPriceAdjustment = 1f;
			if (Main.remixWorld)
			{
				return;
			}
			if (npc.type == 368)
			{
				this._currentPriceAdjustment = 1f;
				return;
			}
			if (npc.type == 453)
			{
				this._currentPriceAdjustment = 1f;
				return;
			}
			if (NPCID.Sets.IsTownPet[npc.type])
			{
				return;
			}
			if (this.IsNotReallyTownNPC(npc))
			{
				this._currentPriceAdjustment = 1f;
				return;
			}
			if (this.RuinMoodIfHomeless(npc))
			{
				this._currentPriceAdjustment = 1000f;
			}
			else if (this.IsFarFromHome(npc))
			{
				this._currentPriceAdjustment = 1000f;
			}
			if (this.IsPlayerInEvilBiomes(player))
			{
				this._currentPriceAdjustment = 1000f;
			}
			int npcsWithinHouse;
			int npcsWithinVillage;
			List<NPC> nearbyResidentNPCs = this.GetNearbyResidentNPCs(npc, out npcsWithinHouse, out npcsWithinVillage);
			bool flag = true;
			float num = 1.05f;
			if (npc.type == 663)
			{
				flag = false;
				num = 1f;
				if (npcsWithinHouse < 2 && npcsWithinVillage < 2)
				{
					this.AddHappinessReportText("HateLonely", null, 0);
					this._currentPriceAdjustment = 1000f;
				}
			}
			if (npcsWithinHouse > 3)
			{
				for (int i = 3; i < npcsWithinHouse; i++)
				{
					this._currentPriceAdjustment *= num;
				}
				if (npcsWithinHouse > 6)
				{
					this.AddHappinessReportText("HateCrowded", null, 0);
				}
				else
				{
					this.AddHappinessReportText("DislikeCrowded", null, 0);
				}
			}
			if (flag && npcsWithinHouse <= 2 && npcsWithinVillage < 4)
			{
				this.AddHappinessReportText("LoveSpace", null, 0);
				this._currentPriceAdjustment *= 0.95f;
			}
			bool[] array = new bool[NPCLoader.NPCCount];
			foreach (NPC item in nearbyResidentNPCs)
			{
				array[item.type] = true;
			}
			HelperInfo info = new HelperInfo
			{
				player = player,
				npc = npc,
				NearbyNPCs = nearbyResidentNPCs,
				nearbyNPCsByType = array
			};
			PersonalityProfile personalityProfile;
			if (this._database.TryGetProfileByNPCID(npc.type, out personalityProfile))
			{
				foreach (IShopPersonalityTrait shopPersonalityTrait in personalityProfile.ShopModifiers)
				{
					shopPersonalityTrait.ModifyShopPrice(info, this);
				}
			}
			new AllPersonalitiesModifier().ModifyShopPrice(info, this);
			if (this._currentHappiness == "")
			{
				this.AddHappinessReportText("Content", null, 0);
			}
			this._currentPriceAdjustment = this.LimitAndRoundMultiplier(this._currentPriceAdjustment);
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x0059C374 File Offset: 0x0059A574
		private float LimitAndRoundMultiplier(float priceAdjustment)
		{
			priceAdjustment = MathHelper.Clamp(priceAdjustment, 0.75f, 1.5f);
			priceAdjustment = (float)Math.Round((double)(priceAdjustment * 100f)) / 100f;
			return priceAdjustment;
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x0059C39F File Offset: 0x0059A59F
		public static string BiomeNameByKey(string biomeNameKey)
		{
			return Language.GetTextValue(biomeNameKey.StartsWith("Mods.") ? biomeNameKey : ("TownNPCMoodBiomes." + biomeNameKey));
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x0059C3C4 File Offset: 0x0059A5C4
		private void AddHappinessReportText(string textKeyInCategory, object substitutes = null, int otherNPCType = 0)
		{
			string text = "TownNPCMood_" + NPCID.Search.GetName(this._currentNPCBeingTalkedTo.netID);
			if (textKeyInCategory == "Princess_LovesNPC")
			{
				text = ModContent.GetModNPC(otherNPCType).GetLocalizationKey("TownNPCMood");
			}
			else
			{
				ModNPC modNPC = this._currentNPCBeingTalkedTo.ModNPC;
				if (modNPC != null)
				{
					text = modNPC.GetLocalizationKey("TownNPCMood");
				}
			}
			if (this._currentNPCBeingTalkedTo.type == 633 && this._currentNPCBeingTalkedTo.altTexture == 2)
			{
				text += "Transformed";
			}
			string textValueWith = Language.GetTextValueWith(text + "." + textKeyInCategory, substitutes);
			this._currentHappiness = this._currentHappiness + textValueWith + " ";
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x0059C484 File Offset: 0x0059A684
		internal void ApplyNpcRelationshipEffect(int npcType, AffectionLevel affectionLevel)
		{
			if (affectionLevel == (AffectionLevel)0 || !Enum.IsDefined<AffectionLevel>(affectionLevel))
			{
				return;
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 1);
			defaultInterpolatedStringHandler.AppendFormatted<AffectionLevel>(affectionLevel);
			defaultInterpolatedStringHandler.AppendLiteral("NPC");
			this.AddHappinessReportText(defaultInterpolatedStringHandler.ToStringAndClear(), new
			{
				NPCName = NPC.GetFullnameByID(npcType)
			}, 0);
			this._currentPriceAdjustment *= NPCHappiness.AffectionLevelToPriceMultiplier[affectionLevel];
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x0059C4EC File Offset: 0x0059A6EC
		internal void ApplyBiomeRelationshipEffect(string biomeNameKey, AffectionLevel affectionLevel)
		{
			if (affectionLevel == (AffectionLevel)0 || !Enum.IsDefined<AffectionLevel>(affectionLevel))
			{
				return;
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
			defaultInterpolatedStringHandler.AppendFormatted<AffectionLevel>(affectionLevel);
			defaultInterpolatedStringHandler.AppendLiteral("Biome");
			this.AddHappinessReportText(defaultInterpolatedStringHandler.ToStringAndClear(), new
			{
				BiomeName = ShopHelper.BiomeNameByKey(biomeNameKey)
			}, 0);
			this._currentPriceAdjustment *= NPCHappiness.AffectionLevelToPriceMultiplier[affectionLevel];
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x0059C554 File Offset: 0x0059A754
		internal void LoveNPCByTypeName(int npcType)
		{
			if (npcType >= (int)NPCID.Count && this._currentNPCBeingTalkedTo.type == 663)
			{
				this.AddHappinessReportText("Princess_LovesNPC", new
				{
					NPCName = NPC.GetFullnameByID(npcType)
				}, npcType);
			}
			else
			{
				this.AddHappinessReportText("LoveNPC_" + NPCID.Search.GetName(npcType), new
				{
					NPCName = NPC.GetFullnameByID(npcType)
				}, 0);
			}
			this._currentPriceAdjustment *= NPCHappiness.AffectionLevelToPriceMultiplier[AffectionLevel.Love];
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x0059C5D4 File Offset: 0x0059A7D4
		internal void LikePrincess()
		{
			this.AddHappinessReportText("LikeNPC_Princess", new
			{
				NPCName = NPC.GetFullnameByID(663)
			}, 0);
			this._currentPriceAdjustment *= NPCHappiness.AffectionLevelToPriceMultiplier[AffectionLevel.Like];
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x0059C60C File Offset: 0x0059A80C
		private List<NPC> GetNearbyResidentNPCs(NPC npc, out int npcsWithinHouse, out int npcsWithinVillage)
		{
			List<NPC> list = new List<NPC>();
			npcsWithinHouse = 0;
			npcsWithinVillage = 0;
			Vector2 value;
			value..ctor((float)npc.homeTileX, (float)npc.homeTileY);
			if (npc.homeless)
			{
				value..ctor(npc.Center.X / 16f, npc.Center.Y / 16f);
			}
			for (int i = 0; i < 200; i++)
			{
				if (i != npc.whoAmI)
				{
					NPC nPC = Main.npc[i];
					if (nPC.active && nPC.townNPC && !this.IsNotReallyTownNPC(nPC) && !WorldGen.TownManager.CanNPCsLiveWithEachOther_ShopHelper(npc, nPC))
					{
						Vector2 value2;
						value2..ctor((float)nPC.homeTileX, (float)nPC.homeTileY);
						if (nPC.homeless)
						{
							value2 = nPC.Center / 16f;
						}
						float num = Vector2.Distance(value, value2);
						if (num < 25f)
						{
							list.Add(nPC);
							npcsWithinHouse++;
						}
						else if (num < 120f)
						{
							npcsWithinVillage++;
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x0059C71D File Offset: 0x0059A91D
		private bool RuinMoodIfHomeless(NPC npc)
		{
			if (npc.homeless)
			{
				this.AddHappinessReportText("NoHome", null, 0);
			}
			return npc.homeless;
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x0059C73C File Offset: 0x0059A93C
		private bool IsFarFromHome(NPC npc)
		{
			Vector2 vector = new Vector2((float)npc.homeTileX, (float)npc.homeTileY);
			Vector2 value2;
			value2..ctor(npc.Center.X / 16f, npc.Center.Y / 16f);
			if (Vector2.Distance(vector, value2) > 120f)
			{
				this.AddHappinessReportText("FarFromHome", null, 0);
				return true;
			}
			return false;
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x0059C7A4 File Offset: 0x0059A9A4
		private bool IsPlayerInEvilBiomes(Player player)
		{
			for (int i = 0; i < this._dangerousBiomes.Length; i++)
			{
				IShoppingBiome aShoppingBiome = this._dangerousBiomes[i];
				if (aShoppingBiome.IsInBiome(player))
				{
					this.AddHappinessReportText("HateBiome", new
					{
						BiomeName = ShopHelper.BiomeNameByKey(aShoppingBiome.NameKey)
					}, 0);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x0059C7F8 File Offset: 0x0059A9F8
		private bool IsNotReallyTownNPC(NPC npc)
		{
			int type = npc.type;
			return NPCID.Sets.NoTownNPCHappiness[type];
		}

		// Token: 0x04005284 RID: 21124
		public const float LowestPossiblePriceMultiplier = 0.75f;

		// Token: 0x04005285 RID: 21125
		public const float MaxHappinessAchievementPriceMultiplier = 0.82f;

		// Token: 0x04005286 RID: 21126
		public const float HighestPossiblePriceMultiplier = 1.5f;

		// Token: 0x04005287 RID: 21127
		private string _currentHappiness;

		// Token: 0x04005288 RID: 21128
		private float _currentPriceAdjustment;

		// Token: 0x04005289 RID: 21129
		private NPC _currentNPCBeingTalkedTo;

		// Token: 0x0400528A RID: 21130
		private Player _currentPlayerTalking;

		// Token: 0x0400528B RID: 21131
		internal PersonalityDatabase _database;

		// Token: 0x0400528C RID: 21132
		private IShoppingBiome[] _dangerousBiomes = new IShoppingBiome[]
		{
			new CorruptionBiome(),
			new CrimsonBiome(),
			new DungeonBiome()
		};

		// Token: 0x0400528D RID: 21133
		internal const float likeValue = 0.94f;

		// Token: 0x0400528E RID: 21134
		internal const float dislikeValue = 1.06f;

		// Token: 0x0400528F RID: 21135
		internal const float loveValue = 0.88f;

		// Token: 0x04005290 RID: 21136
		internal const float hateValue = 1.12f;
	}
}
