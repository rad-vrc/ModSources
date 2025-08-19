using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.Personalities;
using Terraria.ID;

namespace Terraria.ModLoader
{
	/// <summary> This struct provides access to an NPC type's NPC &amp; Biome relationships. </summary>
	// Token: 0x020001DD RID: 477
	public readonly struct NPCHappiness
	{
		// Token: 0x06002507 RID: 9479 RVA: 0x004EB833 File Offset: 0x004E9A33
		private NPCHappiness(int npcType)
		{
			this.NpcType = npcType;
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x004EB83C File Offset: 0x004E9A3C
		public NPCHappiness SetNPCAffection<T>(AffectionLevel affectionLevel) where T : ModNPC
		{
			return this.SetNPCAffection(ModContent.GetInstance<T>().Type, affectionLevel);
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x004EB854 File Offset: 0x004E9A54
		public NPCHappiness SetNPCAffection(int npcId, AffectionLevel affectionLevel)
		{
			List<IShopPersonalityTrait> shopModifiers = Main.ShopHelper._database.GetOrCreateProfileByNPCID(this.NpcType).ShopModifiers;
			NPCPreferenceTrait existingEntry = (NPCPreferenceTrait)shopModifiers.SingleOrDefault(delegate(IShopPersonalityTrait t)
			{
				NPCPreferenceTrait npcPreference = t as NPCPreferenceTrait;
				return npcPreference != null && npcPreference.NpcId == npcId;
			});
			if (existingEntry == null)
			{
				shopModifiers.Add(new NPCPreferenceTrait
				{
					NpcId = npcId,
					Level = affectionLevel
				});
				return this;
			}
			if (affectionLevel == (AffectionLevel)0)
			{
				shopModifiers.Remove(existingEntry);
				return this;
			}
			existingEntry.Level = affectionLevel;
			return this;
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x004EB8E8 File Offset: 0x004E9AE8
		public NPCHappiness SetBiomeAffection<T>(AffectionLevel affectionLevel) where T : class, ILoadable, IShoppingBiome
		{
			return this.SetBiomeAffection(ModContent.GetInstance<T>(), affectionLevel);
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x004EB8FC File Offset: 0x004E9AFC
		public NPCHappiness SetBiomeAffection(IShoppingBiome biome, AffectionLevel affectionLevel)
		{
			List<IShopPersonalityTrait> shopModifiers = Main.ShopHelper._database.GetOrCreateProfileByNPCID(this.NpcType).ShopModifiers;
			bool removal = affectionLevel == (AffectionLevel)0;
			BiomePreferenceListTrait biomePreferenceList = (BiomePreferenceListTrait)shopModifiers.SingleOrDefault((IShopPersonalityTrait t) => t is BiomePreferenceListTrait);
			if (biomePreferenceList == null)
			{
				if (removal)
				{
					return this;
				}
				shopModifiers.Add(biomePreferenceList = new BiomePreferenceListTrait());
			}
			List<BiomePreferenceListTrait.BiomePreference> biomePreferences = biomePreferenceList.Preferences;
			BiomePreferenceListTrait.BiomePreference existingEntry = biomePreferenceList.SingleOrDefault((BiomePreferenceListTrait.BiomePreference p) => p.Biome == biome);
			if (existingEntry == null)
			{
				biomePreferenceList.Add(affectionLevel, biome);
				return this;
			}
			if (removal)
			{
				biomePreferences.Remove(existingEntry);
				return this;
			}
			existingEntry.Affection = affectionLevel;
			return this;
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x004EB9D1 File Offset: 0x004E9BD1
		public static NPCHappiness Get(int npcType)
		{
			return new NPCHappiness(npcType);
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x004EB9DC File Offset: 0x004E9BDC
		internal static void RegisterVanillaNpcRelationships()
		{
			for (int i = 0; i < (int)NPCID.Count; i++)
			{
				AllPersonalitiesModifier.RegisterVanillaNpcRelationships(ContentSamples.NpcsByNetId[i]);
			}
		}

		/// <summary> Allows you to modify the shop price multipliers associated with a (biome/npc type) relationship level. </summary>
		// Token: 0x04001757 RID: 5975
		public static readonly Dictionary<AffectionLevel, float> AffectionLevelToPriceMultiplier = new Dictionary<AffectionLevel, float>
		{
			{
				AffectionLevel.Hate,
				1.12f
			},
			{
				AffectionLevel.Dislike,
				1.06f
			},
			{
				AffectionLevel.Like,
				0.94f
			},
			{
				AffectionLevel.Love,
				0.88f
			}
		};

		// Token: 0x04001758 RID: 5976
		public readonly int NpcType;
	}
}
