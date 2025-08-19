using System;
using System.Collections;
using System.Collections.Generic;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020005B3 RID: 1459
	public class BiomePreferenceListTrait : IShopPersonalityTrait, IEnumerable<BiomePreferenceListTrait.BiomePreference>, IEnumerable
	{
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x0600429D RID: 17053 RVA: 0x005F9FEC File Offset: 0x005F81EC
		public List<BiomePreferenceListTrait.BiomePreference> Preferences
		{
			get
			{
				return this._preferences;
			}
		}

		// Token: 0x0600429E RID: 17054 RVA: 0x005F9FF4 File Offset: 0x005F81F4
		public BiomePreferenceListTrait()
		{
			this._preferences = new List<BiomePreferenceListTrait.BiomePreference>();
		}

		// Token: 0x0600429F RID: 17055 RVA: 0x005FA007 File Offset: 0x005F8207
		public void Add(BiomePreferenceListTrait.BiomePreference preference)
		{
			this._preferences.Add(preference);
		}

		// Token: 0x060042A0 RID: 17056 RVA: 0x005FA015 File Offset: 0x005F8215
		public void Add(AffectionLevel level, IShoppingBiome biome)
		{
			this._preferences.Add(new BiomePreferenceListTrait.BiomePreference(level, biome));
		}

		// Token: 0x060042A1 RID: 17057 RVA: 0x005FA02C File Offset: 0x005F822C
		public void ModifyShopPrice(HelperInfo info, ShopHelper shopHelperInstance)
		{
			BiomePreferenceListTrait.BiomePreference biomePreference = null;
			for (int i = 0; i < this._preferences.Count; i++)
			{
				BiomePreferenceListTrait.BiomePreference biomePreference2 = this._preferences[i];
				if (biomePreference2.Biome.IsInBiome(info.player) && (biomePreference == null || biomePreference.Affection < biomePreference2.Affection))
				{
					biomePreference = biomePreference2;
				}
			}
			if (biomePreference != null)
			{
				this.ApplyPreference(biomePreference, info, shopHelperInstance);
			}
		}

		// Token: 0x060042A2 RID: 17058 RVA: 0x005FA090 File Offset: 0x005F8290
		private void ApplyPreference(BiomePreferenceListTrait.BiomePreference preference, HelperInfo info, ShopHelper shopHelperInstance)
		{
			string nameKey = preference.Biome.NameKey;
			shopHelperInstance.ApplyBiomeRelationshipEffect(nameKey, preference.Affection);
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x005FA0B6 File Offset: 0x005F82B6
		public IEnumerator<BiomePreferenceListTrait.BiomePreference> GetEnumerator()
		{
			return this._preferences.GetEnumerator();
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x005FA0C8 File Offset: 0x005F82C8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._preferences.GetEnumerator();
		}

		// Token: 0x040059C7 RID: 22983
		private List<BiomePreferenceListTrait.BiomePreference> _preferences;

		// Token: 0x02000C65 RID: 3173
		public class BiomePreference
		{
			// Token: 0x06005FE9 RID: 24553 RVA: 0x006D1A33 File Offset: 0x006CFC33
			public BiomePreference(AffectionLevel affection, IShoppingBiome biome)
			{
				this.Affection = affection;
				this.Biome = biome;
			}

			// Token: 0x04007982 RID: 31106
			public AffectionLevel Affection;

			// Token: 0x04007983 RID: 31107
			public IShoppingBiome Biome;
		}
	}
}
