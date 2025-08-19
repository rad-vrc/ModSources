using System;

namespace Terraria.ModLoader
{
	// Token: 0x020001C2 RID: 450
	public enum PrefixCategory
	{
		/// <summary>
		/// Can modify the size of the weapon
		/// </summary>
		// Token: 0x04001718 RID: 5912
		Melee,
		/// <summary>
		/// Can modify the shoot speed of the weapon
		/// </summary>
		// Token: 0x04001719 RID: 5913
		Ranged,
		/// <summary>
		/// Can modify the mana usage of the weapon
		/// </summary>
		// Token: 0x0400171A RID: 5914
		Magic,
		// Token: 0x0400171B RID: 5915
		AnyWeapon,
		// Token: 0x0400171C RID: 5916
		Accessory,
		/// <summary>
		/// Will not appear by default. Useful as prefixes for your own damage type.
		/// </summary>
		// Token: 0x0400171D RID: 5917
		Custom
	}
}
