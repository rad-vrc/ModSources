using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	// Token: 0x02000177 RID: 375
	public static class HairLoader
	{
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x004D4D41 File Offset: 0x004D2F41
		public static int Count
		{
			get
			{
				return HairID.Count + HairLoader.hairs.Count;
			}
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x004D4D53 File Offset: 0x004D2F53
		internal static int Register(ModHair hair)
		{
			HairLoader.hairs.Add(hair);
			return HairLoader.Count - 1;
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x004D4D67 File Offset: 0x004D2F67
		public static ModHair GetHair(int type)
		{
			if (type < HairID.Count || type >= HairLoader.Count)
			{
				return null;
			}
			return HairLoader.hairs[type - HairID.Count];
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x004D4D8C File Offset: 0x004D2F8C
		public static void UpdateUnlocks(HairstyleUnlocksHelper unlocksHelper, bool inCharacterCreation)
		{
			foreach (ModHair hair in HairLoader.hairs)
			{
				bool flag;
				if (!inCharacterCreation)
				{
					flag = hair.GetUnlockConditions().All((Condition x) => x.IsMet());
				}
				else
				{
					flag = hair.AvailableDuringCharacterCreation;
				}
				if (flag)
				{
					unlocksHelper.AvailableHairstyles.Add(hair.Type);
				}
			}
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x004D4E1C File Offset: 0x004D301C
		internal static void ResizeArrays()
		{
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.PlayerHair, HairLoader.Count);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.PlayerHairAlt, HairLoader.Count);
			LoaderUtils.ResetStaticMembers(typeof(HairID), true);
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x004D4E4C File Offset: 0x004D304C
		internal static void Unload()
		{
			HairLoader.hairs.Clear();
		}

		// Token: 0x040015BD RID: 5565
		internal static readonly IList<ModHair> hairs = new List<ModHair>();
	}
}
