using System;
using System.Collections.Generic;
using Terraria.GameContent.UI;

namespace Terraria.ModLoader
{
	// Token: 0x020001EF RID: 495
	public static class RarityLoader
	{
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060026E1 RID: 9953 RVA: 0x00501583 File Offset: 0x004FF783
		public static int RarityCount
		{
			get
			{
				return 12 + RarityLoader.rarities.Count;
			}
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x00501592 File Offset: 0x004FF792
		internal static int Add(ModRarity rarity)
		{
			RarityLoader.rarities.Add(rarity);
			return RarityLoader.RarityCount - 1;
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x005015A6 File Offset: 0x004FF7A6
		internal static void FinishSetup()
		{
			ItemRarity.Initialize();
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x005015AD File Offset: 0x004FF7AD
		public static ModRarity GetRarity(int type)
		{
			if (type < 12 || type >= RarityLoader.RarityCount)
			{
				return null;
			}
			return RarityLoader.rarities[type - 12];
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x005015CC File Offset: 0x004FF7CC
		internal static void Unload()
		{
			RarityLoader.rarities.Clear();
			ItemRarity.Initialize();
		}

		// Token: 0x04001898 RID: 6296
		internal static readonly List<ModRarity> rarities = new List<ModRarity>();
	}
}
