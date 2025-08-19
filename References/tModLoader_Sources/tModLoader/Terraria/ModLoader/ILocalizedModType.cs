using System;

namespace Terraria.ModLoader
{
	// Token: 0x0200017C RID: 380
	public interface ILocalizedModType : IModType
	{
		/// <summary>
		/// The category used by this modded content for use in localization keys. Localization keys follow the pattern of "Mods.{ModName}.{Category}.{ContentName}.{DataName}". The <see href="https://github.com/tModLoader/tModLoader/wiki/Localization#modtype-and-ilocalizedmodtype">Localization wiki page</see> explains how custom <see cref="T:Terraria.ModLoader.ModType" /> classes can utilize this.
		/// </summary>
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001DF9 RID: 7673
		string LocalizationCategory { get; }
	}
}
