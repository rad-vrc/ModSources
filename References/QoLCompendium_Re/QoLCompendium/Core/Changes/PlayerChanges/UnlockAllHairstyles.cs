using System;
using System.Runtime.CompilerServices;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.PlayerChanges
{
	// Token: 0x02000232 RID: 562
	public class UnlockAllHairstyles : ModSystem
	{
		// Token: 0x06000D7B RID: 3451 RVA: 0x00068B74 File Offset: 0x00066D74
		public override void Load()
		{
			On_HairstyleUnlocksHelper.hook_ListWarrantsRemake hook_ListWarrantsRemake;
			if ((hook_ListWarrantsRemake = UnlockAllHairstyles.<>O.<0>__RebuildPatch) == null)
			{
				hook_ListWarrantsRemake = (UnlockAllHairstyles.<>O.<0>__RebuildPatch = new On_HairstyleUnlocksHelper.hook_ListWarrantsRemake(UnlockAllHairstyles.RebuildPatch));
			}
			On_HairstyleUnlocksHelper.ListWarrantsRemake += hook_ListWarrantsRemake;
			On_HairstyleUnlocksHelper.hook_RebuildList hook_RebuildList;
			if ((hook_RebuildList = UnlockAllHairstyles.<>O.<1>__UnlockPatch) == null)
			{
				hook_RebuildList = (UnlockAllHairstyles.<>O.<1>__UnlockPatch = new On_HairstyleUnlocksHelper.hook_RebuildList(UnlockAllHairstyles.UnlockPatch));
			}
			On_HairstyleUnlocksHelper.RebuildList += hook_RebuildList;
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00068BC4 File Offset: 0x00066DC4
		public override void Unload()
		{
			On_HairstyleUnlocksHelper.hook_ListWarrantsRemake hook_ListWarrantsRemake;
			if ((hook_ListWarrantsRemake = UnlockAllHairstyles.<>O.<0>__RebuildPatch) == null)
			{
				hook_ListWarrantsRemake = (UnlockAllHairstyles.<>O.<0>__RebuildPatch = new On_HairstyleUnlocksHelper.hook_ListWarrantsRemake(UnlockAllHairstyles.RebuildPatch));
			}
			On_HairstyleUnlocksHelper.ListWarrantsRemake -= hook_ListWarrantsRemake;
			On_HairstyleUnlocksHelper.hook_RebuildList hook_RebuildList;
			if ((hook_RebuildList = UnlockAllHairstyles.<>O.<1>__UnlockPatch) == null)
			{
				hook_RebuildList = (UnlockAllHairstyles.<>O.<1>__UnlockPatch = new On_HairstyleUnlocksHelper.hook_RebuildList(UnlockAllHairstyles.UnlockPatch));
			}
			On_HairstyleUnlocksHelper.RebuildList -= hook_RebuildList;
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00068C11 File Offset: 0x00066E11
		private static bool RebuildPatch(On_HairstyleUnlocksHelper.orig_ListWarrantsRemake orig, HairstyleUnlocksHelper self)
		{
			if (!QoLCompendium.mainConfig.AllHairStylesAvailable)
			{
				return false;
			}
			if (!UnlockAllHairstyles._rebuilt)
			{
				UnlockAllHairstyles._rebuilt = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00068C34 File Offset: 0x00066E34
		private static void UnlockPatch(On_HairstyleUnlocksHelper.orig_RebuildList orig, HairstyleUnlocksHelper self)
		{
			if (!QoLCompendium.mainConfig.AllHairStylesAvailable)
			{
				return;
			}
			self.AvailableHairstyles.Clear();
			for (int i = 0; i < TextureAssets.PlayerHair.Length; i++)
			{
				self.AvailableHairstyles.Add(i);
			}
		}

		// Token: 0x0400059E RID: 1438
		private static bool _rebuilt;

		// Token: 0x02000549 RID: 1353
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000EF3 RID: 3827
			public static On_HairstyleUnlocksHelper.hook_ListWarrantsRemake <0>__RebuildPatch;

			// Token: 0x04000EF4 RID: 3828
			public static On_HairstyleUnlocksHelper.hook_RebuildList <1>__UnlockPatch;
		}
	}
}
