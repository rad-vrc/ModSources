using System;
using System.Collections.Generic;
using Terraria.ID;

namespace Terraria.ModLoader
{
	// Token: 0x02000147 RID: 327
	public static class BiomeConversionLoader
	{
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x004CDD0A File Offset: 0x004CBF0A
		// (set) Token: 0x06001AD8 RID: 6872 RVA: 0x004CDD11 File Offset: 0x004CBF11
		internal static int BiomeConversionCount { get; private set; } = BiomeConversionID.Count;

		/// <summary>
		/// Gets the ModBiomeConversion instance with the given type. Returns null if no ModBiomeConversion with the given type exists.
		/// </summary>
		// Token: 0x06001AD9 RID: 6873 RVA: 0x004CDD19 File Offset: 0x004CBF19
		public static ModBiomeConversion GetBiomeConversion(int type)
		{
			if (type < BiomeConversionID.Count || type >= BiomeConversionLoader.BiomeConversionCount)
			{
				return null;
			}
			return BiomeConversionLoader.conversions[type - BiomeConversionID.Count];
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x004CDD3E File Offset: 0x004CBF3E
		internal static void ResizeArrays()
		{
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x004CDD40 File Offset: 0x004CBF40
		internal static void PostSetupContent()
		{
			foreach (ModBiomeConversion modBiomeConversion in BiomeConversionLoader.conversions)
			{
				modBiomeConversion.PostSetupContent();
			}
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x004CDD8C File Offset: 0x004CBF8C
		internal static void Unload()
		{
			BiomeConversionLoader.conversions.Clear();
			BiomeConversionLoader.BiomeConversionCount = BiomeConversionID.Count;
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x004CDDA2 File Offset: 0x004CBFA2
		public static int Register(ModBiomeConversion conversion)
		{
			int result = BiomeConversionLoader.BiomeConversionCount++;
			BiomeConversionLoader.conversions.Add(conversion);
			return result;
		}

		// Token: 0x04001483 RID: 5251
		internal static readonly IList<ModBiomeConversion> conversions = new List<ModBiomeConversion>();
	}
}
