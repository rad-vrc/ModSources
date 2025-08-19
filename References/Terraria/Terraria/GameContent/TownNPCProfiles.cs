﻿using System;
using System.Collections.Generic;

namespace Terraria.GameContent
{
	// Token: 0x020001EE RID: 494
	public class TownNPCProfiles
	{
		// Token: 0x06001CD3 RID: 7379 RVA: 0x004FDB7E File Offset: 0x004FBD7E
		public bool GetProfile(int npcId, out ITownNPCProfile profile)
		{
			return this._townNPCProfiles.TryGetValue(npcId, out profile);
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x004FDB8D File Offset: 0x004FBD8D
		public static ITownNPCProfile LegacyWithSimpleShimmer(string subPath, int headIdNormal, int headIdShimmered, bool uniquePartyTexture = true, bool uniquePartyTextureShimmered = true)
		{
			return new Profiles.StackedNPCProfile(new ITownNPCProfile[]
			{
				new Profiles.LegacyNPCProfile("Images/TownNPCs/" + subPath, headIdNormal, true, uniquePartyTexture),
				new Profiles.LegacyNPCProfile("Images/TownNPCs/Shimmered/" + subPath, headIdShimmered, true, uniquePartyTextureShimmered)
			});
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x004FDBC7 File Offset: 0x004FBDC7
		public static ITownNPCProfile TransformableWithSimpleShimmer(string subPath, int headIdNormal, int headIdShimmered, bool uniqueCreditTexture = true, bool uniqueCreditTextureShimmered = true)
		{
			return new Profiles.StackedNPCProfile(new ITownNPCProfile[]
			{
				new Profiles.TransformableNPCProfile("Images/TownNPCs/" + subPath, headIdNormal, uniqueCreditTexture),
				new Profiles.TransformableNPCProfile("Images/TownNPCs/Shimmered/" + subPath, headIdShimmered, uniqueCreditTextureShimmered)
			});
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x004FDC00 File Offset: 0x004FBE00
		public static int GetHeadIndexSafe(NPC npc)
		{
			ITownNPCProfile townNPCProfile;
			if (TownNPCProfiles.Instance.GetProfile(npc.type, out townNPCProfile))
			{
				return townNPCProfile.GetHeadTextureIndex(npc);
			}
			return NPC.TypeToDefaultHeadIndex(npc.type);
		}

		// Token: 0x040043D5 RID: 17365
		private const string DefaultNPCFileFolderPath = "Images/TownNPCs/";

		// Token: 0x040043D6 RID: 17366
		private const string ShimmeredNPCFileFolderPath = "Images/TownNPCs/Shimmered/";

		// Token: 0x040043D7 RID: 17367
		private static readonly int[] CatHeadIDs = new int[]
		{
			27,
			28,
			29,
			30,
			31,
			32
		};

		// Token: 0x040043D8 RID: 17368
		private static readonly int[] DogHeadIDs = new int[]
		{
			33,
			34,
			35,
			36,
			37,
			38
		};

		// Token: 0x040043D9 RID: 17369
		private static readonly int[] BunnyHeadIDs = new int[]
		{
			39,
			40,
			41,
			42,
			43,
			44
		};

		// Token: 0x040043DA RID: 17370
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

		// Token: 0x040043DB RID: 17371
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

		// Token: 0x040043DC RID: 17372
		public static TownNPCProfiles Instance = new TownNPCProfiles();
	}
}
