using System;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000679 RID: 1657
	public struct BestiaryUICollectionInfo
	{
		// Token: 0x04005BF0 RID: 23536
		public BestiaryEntry OwnerEntry;

		/// <summary>
		/// Indicates the extent to how unlocked this bestiary entry is. Use this to determine how detailed of information to provide in a <see cref="T:Terraria.GameContent.Bestiary.IBestiaryInfoElement" />. The <see href="https://terraria.wiki.gg/wiki/Bestiary">Bestiary page on the wiki</see> has more information.
		/// </summary>
		// Token: 0x04005BF1 RID: 23537
		public BestiaryEntryUnlockState UnlockState;
	}
}
