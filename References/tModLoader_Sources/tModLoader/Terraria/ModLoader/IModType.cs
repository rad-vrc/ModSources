using System;

namespace Terraria.ModLoader
{
	// Token: 0x0200017F RID: 383
	public interface IModType
	{
		/// <summary>
		///  The mod this belongs to.
		///  </summary>
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001E01 RID: 7681
		Mod Mod { get; }

		/// <summary>
		/// The internal name of this instance.
		/// </summary>
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001E02 RID: 7682
		string Name { get; }

		/// <summary>
		/// =&gt; $"{Mod.Name}/{Name}"
		/// </summary>
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001E03 RID: 7683
		string FullName { get; }
	}
}
