using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.GameContent
{
	// Token: 0x020004BE RID: 1214
	public class TownNPCProfiles
	{
		// Token: 0x06003A31 RID: 14897 RVA: 0x005A62D0 File Offset: 0x005A44D0
		public bool GetProfile(NPC npc, out ITownNPCProfile profile)
		{
			this._townNPCProfiles.TryGetValue(npc.type, out profile);
			NPCLoader.ModifyTownNPCProfile(npc, ref profile);
			if (!Main.dedServ)
			{
				ITownNPCProfile townNPCProfile = profile;
				if (townNPCProfile != null)
				{
					Action wait = townNPCProfile.GetTextureNPCShouldUse(npc).Wait;
					if (wait != null)
					{
						wait();
					}
				}
			}
			return profile != null;
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x005A6324 File Offset: 0x005A4524
		internal void ResetTexturesAccordingToVanillaProfiles()
		{
			foreach (KeyValuePair<int, ITownNPCProfile> pair in this._townNPCProfiles)
			{
				TextureAssets.Npc[pair.Key] = pair.Value.GetTextureNPCShouldUse(ContentSamples.NpcsByNetId[pair.Key]);
			}
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x005A639C File Offset: 0x005A459C
		public static ITownNPCProfile LegacyWithSimpleShimmer(string subPath, int headIdNormal, int headIdShimmered, bool uniquePartyTexture = true, bool uniquePartyTextureShimmered = true)
		{
			return new Profiles.StackedNPCProfile(new ITownNPCProfile[]
			{
				new Profiles.LegacyNPCProfile("Images/TownNPCs/" + subPath, headIdNormal, true, uniquePartyTexture),
				new Profiles.LegacyNPCProfile("Images/TownNPCs/Shimmered/" + subPath, headIdShimmered, true, uniquePartyTextureShimmered)
			});
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x005A63D6 File Offset: 0x005A45D6
		public static ITownNPCProfile TransformableWithSimpleShimmer(string subPath, int headIdNormal, int headIdShimmered, bool uniqueCreditTexture = true, bool uniqueCreditTextureShimmered = true)
		{
			return new Profiles.StackedNPCProfile(new ITownNPCProfile[]
			{
				new Profiles.TransformableNPCProfile("Images/TownNPCs/" + subPath, headIdNormal, uniqueCreditTexture),
				new Profiles.TransformableNPCProfile("Images/TownNPCs/Shimmered/" + subPath, headIdShimmered, uniqueCreditTextureShimmered)
			});
		}

		/// <summary> Retrieves the head index of this town NPC, accounting for shimmer and other variants. Returns -1 if no head index found. (See also <see cref="F:Terraria.GameContent.TextureAssets.NpcHead" />, <see cref="T:Terraria.ModLoader.AutoloadHead" />, <see cref="M:Terraria.ModLoader.Mod.AddNPCHeadTexture(System.Int32,System.String)" />) </summary>
		// Token: 0x06003A35 RID: 14901 RVA: 0x005A6410 File Offset: 0x005A4610
		public static int GetHeadIndexSafe(NPC npc)
		{
			ITownNPCProfile profile;
			if (TownNPCProfiles.Instance.GetProfile(npc, out profile))
			{
				return profile.GetHeadTextureIndex(npc);
			}
			return NPC.TypeToDefaultHeadIndex(npc.type);
		}

		// Token: 0x040053CC RID: 21452
		private const string DefaultNPCFileFolderPath = "Images/TownNPCs/";

		// Token: 0x040053CD RID: 21453
		private const string ShimmeredNPCFileFolderPath = "Images/TownNPCs/Shimmered/";

		// Token: 0x040053CE RID: 21454
		private static readonly int[] CatHeadIDs = new int[]
		{
			27,
			28,
			29,
			30,
			31,
			32
		};

		// Token: 0x040053CF RID: 21455
		private static readonly int[] DogHeadIDs = new int[]
		{
			33,
			34,
			35,
			36,
			37,
			38
		};

		// Token: 0x040053D0 RID: 21456
		private static readonly int[] BunnyHeadIDs = new int[]
		{
			39,
			40,
			41,
			42,
			43,
			44
		};

		// Token: 0x040053D1 RID: 21457
		private static readonly int[] SlimeHeadIDs = new int[]
		{
			46,
			47,
			48,
			49,
			50,
			51,
			52,
			53
		};

		// Token: 0x040053D2 RID: 21458
		private Dictionary<int, ITownNPCProfile> _townNPCProfiles = new Dictionary<int, ITownNPCProfile>
		{
			{
				22,
				TownNPCProfiles.LegacyWithSimpleShimmer("Guide", 1, 72, false, false)
			},
			{
				20,
				TownNPCProfiles.LegacyWithSimpleShimmer("Dryad", 5, 73, false, false)
			},
			{
				19,
				TownNPCProfiles.LegacyWithSimpleShimmer("ArmsDealer", 6, 74, false, false)
			},
			{
				107,
				TownNPCProfiles.LegacyWithSimpleShimmer("GoblinTinkerer", 9, 75, false, false)
			},
			{
				160,
				TownNPCProfiles.LegacyWithSimpleShimmer("Truffle", 12, 76, false, false)
			},
			{
				208,
				TownNPCProfiles.LegacyWithSimpleShimmer("PartyGirl", 15, 77, false, false)
			},
			{
				228,
				TownNPCProfiles.LegacyWithSimpleShimmer("WitchDoctor", 18, 78, false, false)
			},
			{
				550,
				TownNPCProfiles.LegacyWithSimpleShimmer("Tavernkeep", 24, 79, false, false)
			},
			{
				369,
				TownNPCProfiles.LegacyWithSimpleShimmer("Angler", 22, 55, true, false)
			},
			{
				54,
				TownNPCProfiles.LegacyWithSimpleShimmer("Clothier", 7, 57, true, false)
			},
			{
				209,
				TownNPCProfiles.LegacyWithSimpleShimmer("Cyborg", 16, 58, true, true)
			},
			{
				38,
				TownNPCProfiles.LegacyWithSimpleShimmer("Demolitionist", 4, 59, true, true)
			},
			{
				207,
				TownNPCProfiles.LegacyWithSimpleShimmer("DyeTrader", 14, 60, true, true)
			},
			{
				588,
				TownNPCProfiles.LegacyWithSimpleShimmer("Golfer", 25, 61, true, false)
			},
			{
				124,
				TownNPCProfiles.LegacyWithSimpleShimmer("Mechanic", 8, 62, true, true)
			},
			{
				17,
				TownNPCProfiles.LegacyWithSimpleShimmer("Merchant", 2, 63, true, true)
			},
			{
				18,
				TownNPCProfiles.LegacyWithSimpleShimmer("Nurse", 3, 64, true, true)
			},
			{
				227,
				TownNPCProfiles.LegacyWithSimpleShimmer("Painter", 17, 65, true, false)
			},
			{
				229,
				TownNPCProfiles.LegacyWithSimpleShimmer("Pirate", 19, 66, true, true)
			},
			{
				142,
				TownNPCProfiles.LegacyWithSimpleShimmer("Santa", 11, 67, true, true)
			},
			{
				178,
				TownNPCProfiles.LegacyWithSimpleShimmer("Steampunker", 13, 68, true, false)
			},
			{
				353,
				TownNPCProfiles.LegacyWithSimpleShimmer("Stylist", 20, 69, true, true)
			},
			{
				441,
				TownNPCProfiles.LegacyWithSimpleShimmer("TaxCollector", 23, 70, true, true)
			},
			{
				108,
				TownNPCProfiles.LegacyWithSimpleShimmer("Wizard", 10, 71, true, true)
			},
			{
				663,
				TownNPCProfiles.LegacyWithSimpleShimmer("Princess", 45, 54, true, true)
			},
			{
				633,
				TownNPCProfiles.TransformableWithSimpleShimmer("BestiaryGirl", 26, 56, true, false)
			},
			{
				37,
				TownNPCProfiles.LegacyWithSimpleShimmer("OldMan", -1, -1, false, false)
			},
			{
				453,
				TownNPCProfiles.LegacyWithSimpleShimmer("SkeletonMerchant", -1, -1, true, true)
			},
			{
				368,
				TownNPCProfiles.LegacyWithSimpleShimmer("TravelingMerchant", 21, 80, true, true)
			},
			{
				637,
				new Profiles.VariantNPCProfile("Images/TownNPCs/Cat", "Cat", TownNPCProfiles.CatHeadIDs, new string[]
				{
					"Siamese",
					"Black",
					"OrangeTabby",
					"RussianBlue",
					"Silver",
					"White"
				})
			},
			{
				638,
				new Profiles.VariantNPCProfile("Images/TownNPCs/Dog", "Dog", TownNPCProfiles.DogHeadIDs, new string[]
				{
					"Labrador",
					"PitBull",
					"Beagle",
					"Corgi",
					"Dalmation",
					"Husky"
				})
			},
			{
				656,
				new Profiles.VariantNPCProfile("Images/TownNPCs/Bunny", "Bunny", TownNPCProfiles.BunnyHeadIDs, new string[]
				{
					"White",
					"Angora",
					"Dutch",
					"Flemish",
					"Lop",
					"Silver"
				})
			},
			{
				670,
				new Profiles.LegacyNPCProfile("Images/TownNPCs/SlimeBlue", 46, true, false)
			},
			{
				678,
				new Profiles.LegacyNPCProfile("Images/TownNPCs/SlimeGreen", 47, true, true)
			},
			{
				679,
				new Profiles.LegacyNPCProfile("Images/TownNPCs/SlimeOld", 48, true, true)
			},
			{
				680,
				new Profiles.LegacyNPCProfile("Images/TownNPCs/SlimePurple", 49, true, true)
			},
			{
				681,
				new Profiles.LegacyNPCProfile("Images/TownNPCs/SlimeRainbow", 50, true, true)
			},
			{
				682,
				new Profiles.LegacyNPCProfile("Images/TownNPCs/SlimeRed", 51, true, true)
			},
			{
				683,
				new Profiles.LegacyNPCProfile("Images/TownNPCs/SlimeYellow", 52, true, true)
			},
			{
				684,
				new Profiles.LegacyNPCProfile("Images/TownNPCs/SlimeCopper", 53, true, true)
			}
		};

		// Token: 0x040053D3 RID: 21459
		public static TownNPCProfiles Instance = new TownNPCProfiles();
	}
}
