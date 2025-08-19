using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent
{
	// Token: 0x020001E7 RID: 487
	public class ShopHelper
	{
		// Token: 0x06001C7C RID: 7292 RVA: 0x004F4490 File Offset: 0x004F2690
		public ShopHelper()
		{
			this._database = new PersonalityDatabase();
			new PersonalityDatabasePopulator().Populate(this._database);
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x004F44E4 File Offset: 0x004F26E4
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

		// Token: 0x06001C7E RID: 7294 RVA: 0x004F454C File Offset: 0x004F274C
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

		// Token: 0x06001C7F RID: 7295 RVA: 0x004F45C0 File Offset: 0x004F27C0
		private float GetTravelingMerchantPrices(NPC npc)
		{
			Vector2 value = npc.Center / 16f;
			Vector2 value2 = new Vector2((float)Main.spawnTileX, (float)Main.spawnTileY);
			float num = Vector2.Distance(value, value2) / (float)(Main.maxTilesX / 2);
			num = 1.5f - num;
			return (2f + num) / 3f;
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x004F4618 File Offset: 0x004F2818
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
			int num;
			int num2;
			List<NPC> nearbyResidentNPCs = this.GetNearbyResidentNPCs(npc, out num, out num2);
			bool flag = true;
			bool flag2 = true;
			float num3 = 1.05f;
			if (npc.type == 663)
			{
				flag = false;
				num3 = 1f;
				if (num < 2 && num2 < 2)
				{
					this.AddHappinessReportText("HateLonely", null);
					this._currentPriceAdjustment = 1000f;
				}
			}
			if (flag2 && num > 3)
			{
				for (int i = 3; i < num; i++)
				{
					this._currentPriceAdjustment *= num3;
				}
				if (num > 6)
				{
					this.AddHappinessReportText("HateCrowded", null);
				}
				else
				{
					this.AddHappinessReportText("DislikeCrowded", null);
				}
			}
			if (flag && num <= 2 && num2 < 4)
			{
				this.AddHappinessReportText("LoveSpace", null);
				this._currentPriceAdjustment *= 0.95f;
			}
			bool[] array = new bool[(int)NPCID.Count];
			foreach (NPC npc2 in nearbyResidentNPCs)
			{
				array[npc2.type] = true;
			}
			HelperInfo info = new HelperInfo
			{
				player = player,
				npc = npc,
				NearbyNPCs = nearbyResidentNPCs,
				nearbyNPCsByType = array
			};
			foreach (IShopPersonalityTrait shopPersonalityTrait in this._database.GetByNPCID(npc.type).ShopModifiers)
			{
				shopPersonalityTrait.ModifyShopPrice(info, this);
			}
			new AllPersonalitiesModifier().ModifyShopPrice(info, this);
			if (this._currentHappiness == "")
			{
				this.AddHappinessReportText("Content", null);
			}
			this._currentPriceAdjustment = this.LimitAndRoundMultiplier(this._currentPriceAdjustment);
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x004F48A0 File Offset: 0x004F2AA0
		private float LimitAndRoundMultiplier(float priceAdjustment)
		{
			priceAdjustment = MathHelper.Clamp(priceAdjustment, 0.75f, 1.5f);
			priceAdjustment = (float)Math.Round((double)(priceAdjustment * 100f)) / 100f;
			return priceAdjustment;
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x004F48CB File Offset: 0x004F2ACB
		private static string BiomeNameByKey(string biomeNameKey)
		{
			return Language.GetTextValue("TownNPCMoodBiomes." + biomeNameKey);
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x004F48E0 File Offset: 0x004F2AE0
		private void AddHappinessReportText(string textKeyInCategory, object substitutes = null)
		{
			string str = "TownNPCMood_" + NPCID.Search.GetName(this._currentNPCBeingTalkedTo.netID);
			if (this._currentNPCBeingTalkedTo.type == 633 && this._currentNPCBeingTalkedTo.altTexture == 2)
			{
				str += "Transformed";
			}
			string textValueWith = Language.GetTextValueWith(str + "." + textKeyInCategory, substitutes);
			this._currentHappiness = this._currentHappiness + textValueWith + " ";
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x004F4963 File Offset: 0x004F2B63
		public void LikeBiome(string nameKey)
		{
			this.AddHappinessReportText("LikeBiome", new
			{
				BiomeName = ShopHelper.BiomeNameByKey(nameKey)
			});
			this._currentPriceAdjustment *= 0.94f;
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x004F498D File Offset: 0x004F2B8D
		public void LoveBiome(string nameKey)
		{
			this.AddHappinessReportText("LoveBiome", new
			{
				BiomeName = ShopHelper.BiomeNameByKey(nameKey)
			});
			this._currentPriceAdjustment *= 0.88f;
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x004F49B7 File Offset: 0x004F2BB7
		public void DislikeBiome(string nameKey)
		{
			this.AddHappinessReportText("DislikeBiome", new
			{
				BiomeName = ShopHelper.BiomeNameByKey(nameKey)
			});
			this._currentPriceAdjustment *= 1.06f;
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x004F49E1 File Offset: 0x004F2BE1
		public void HateBiome(string nameKey)
		{
			this.AddHappinessReportText("HateBiome", new
			{
				BiomeName = ShopHelper.BiomeNameByKey(nameKey)
			});
			this._currentPriceAdjustment *= 1.12f;
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x004F4A0B File Offset: 0x004F2C0B
		public void LikeNPC(int npcType)
		{
			this.AddHappinessReportText("LikeNPC", new
			{
				NPCName = NPC.GetFullnameByID(npcType)
			});
			this._currentPriceAdjustment *= 0.94f;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x004F4A35 File Offset: 0x004F2C35
		public void LoveNPCByTypeName(int npcType)
		{
			this.AddHappinessReportText("LoveNPC_" + NPCID.Search.GetName(npcType), new
			{
				NPCName = NPC.GetFullnameByID(npcType)
			});
			this._currentPriceAdjustment *= 0.88f;
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x004F4A6F File Offset: 0x004F2C6F
		public void LikePrincess()
		{
			this.AddHappinessReportText("LikeNPC_Princess", new
			{
				NPCName = NPC.GetFullnameByID(663)
			});
			this._currentPriceAdjustment *= 0.94f;
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x004F4A9D File Offset: 0x004F2C9D
		public void LoveNPC(int npcType)
		{
			this.AddHappinessReportText("LoveNPC", new
			{
				NPCName = NPC.GetFullnameByID(npcType)
			});
			this._currentPriceAdjustment *= 0.88f;
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x004F4AC7 File Offset: 0x004F2CC7
		public void DislikeNPC(int npcType)
		{
			this.AddHappinessReportText("DislikeNPC", new
			{
				NPCName = NPC.GetFullnameByID(npcType)
			});
			this._currentPriceAdjustment *= 1.06f;
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x004F4AF1 File Offset: 0x004F2CF1
		public void HateNPC(int npcType)
		{
			this.AddHappinessReportText("HateNPC", new
			{
				NPCName = NPC.GetFullnameByID(npcType)
			});
			this._currentPriceAdjustment *= 1.12f;
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x004F4B1C File Offset: 0x004F2D1C
		private List<NPC> GetNearbyResidentNPCs(NPC npc, out int npcsWithinHouse, out int npcsWithinVillage)
		{
			List<NPC> list = new List<NPC>();
			npcsWithinHouse = 0;
			npcsWithinVillage = 0;
			Vector2 value = new Vector2((float)npc.homeTileX, (float)npc.homeTileY);
			if (npc.homeless)
			{
				value = new Vector2(npc.Center.X / 16f, npc.Center.Y / 16f);
			}
			for (int i = 0; i < 200; i++)
			{
				if (i != npc.whoAmI)
				{
					NPC npc2 = Main.npc[i];
					if (npc2.active && npc2.townNPC && !this.IsNotReallyTownNPC(npc2) && !WorldGen.TownManager.CanNPCsLiveWithEachOther_ShopHelper(npc, npc2))
					{
						Vector2 value2 = new Vector2((float)npc2.homeTileX, (float)npc2.homeTileY);
						if (npc2.homeless)
						{
							value2 = npc2.Center / 16f;
						}
						float num = Vector2.Distance(value, value2);
						if (num < 25f)
						{
							list.Add(npc2);
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

		// Token: 0x06001C8F RID: 7311 RVA: 0x004F4C2D File Offset: 0x004F2E2D
		private bool RuinMoodIfHomeless(NPC npc)
		{
			if (npc.homeless)
			{
				this.AddHappinessReportText("NoHome", null);
			}
			return npc.homeless;
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x004F4C4C File Offset: 0x004F2E4C
		private bool IsFarFromHome(NPC npc)
		{
			Vector2 value = new Vector2((float)npc.homeTileX, (float)npc.homeTileY);
			Vector2 value2 = new Vector2(npc.Center.X / 16f, npc.Center.Y / 16f);
			if (Vector2.Distance(value, value2) > 120f)
			{
				this.AddHappinessReportText("FarFromHome", null);
				return true;
			}
			return false;
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x004F4CB4 File Offset: 0x004F2EB4
		private bool IsPlayerInEvilBiomes(Player player)
		{
			for (int i = 0; i < this._dangerousBiomes.Length; i++)
			{
				AShoppingBiome ashoppingBiome = this._dangerousBiomes[i];
				if (ashoppingBiome.IsInBiome(player))
				{
					this.AddHappinessReportText("HateBiome", new
					{
						BiomeName = ShopHelper.BiomeNameByKey(ashoppingBiome.NameKey)
					});
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x004F4D04 File Offset: 0x004F2F04
		private bool IsNotReallyTownNPC(NPC npc)
		{
			int type = npc.type;
			return type == 37 || type == 368 || type == 453;
		}

		// Token: 0x040043AC RID: 17324
		public const float LowestPossiblePriceMultiplier = 0.75f;

		// Token: 0x040043AD RID: 17325
		public const float MaxHappinessAchievementPriceMultiplier = 0.82f;

		// Token: 0x040043AE RID: 17326
		public const float HighestPossiblePriceMultiplier = 1.5f;

		// Token: 0x040043AF RID: 17327
		private string _currentHappiness;

		// Token: 0x040043B0 RID: 17328
		private float _currentPriceAdjustment;

		// Token: 0x040043B1 RID: 17329
		private NPC _currentNPCBeingTalkedTo;

		// Token: 0x040043B2 RID: 17330
		private Player _currentPlayerTalking;

		// Token: 0x040043B3 RID: 17331
		private PersonalityDatabase _database;

		// Token: 0x040043B4 RID: 17332
		private AShoppingBiome[] _dangerousBiomes = new AShoppingBiome[]
		{
			new CorruptionBiome(),
			new CrimsonBiome(),
			new DungeonBiome()
		};

		// Token: 0x040043B5 RID: 17333
		private const float likeValue = 0.94f;

		// Token: 0x040043B6 RID: 17334
		private const float dislikeValue = 1.06f;

		// Token: 0x040043B7 RID: 17335
		private const float loveValue = 0.88f;

		// Token: 0x040043B8 RID: 17336
		private const float hateValue = 1.12f;
	}
}
