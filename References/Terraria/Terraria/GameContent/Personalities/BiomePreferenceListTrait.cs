using System;
using System.Collections;
using System.Collections.Generic;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020003C3 RID: 963
	public class BiomePreferenceListTrait : IShopPersonalityTrait, IEnumerable<BiomePreferenceListTrait.BiomePreference>, IEnumerable
	{
		// Token: 0x06002A6F RID: 10863 RVA: 0x005994DA File Offset: 0x005976DA
		public BiomePreferenceListTrait()
		{
			this._preferences = new List<BiomePreferenceListTrait.BiomePreference>();
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x005994ED File Offset: 0x005976ED
		public void Add(BiomePreferenceListTrait.BiomePreference preference)
		{
			this._preferences.Add(preference);
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x005994FB File Offset: 0x005976FB
		public void Add(AffectionLevel level, AShoppingBiome biome)
		{
			this._preferences.Add(new BiomePreferenceListTrait.BiomePreference(level, biome));
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x00599510 File Offset: 0x00597710
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

		// Token: 0x06002A73 RID: 10867 RVA: 0x00599574 File Offset: 0x00597774
		private void ApplyPreference(BiomePreferenceListTrait.BiomePreference preference, HelperInfo info, ShopHelper shopHelperInstance)
		{
			string nameKey = preference.Biome.NameKey;
			AffectionLevel affection = preference.Affection;
			if (affection <= AffectionLevel.Dislike)
			{
				if (affection != AffectionLevel.Hate)
				{
					if (affection != AffectionLevel.Dislike)
					{
						return;
					}
					shopHelperInstance.DislikeBiome(nameKey);
					return;
				}
				else
				{
					shopHelperInstance.HateBiome(nameKey);
				}
			}
			else
			{
				if (affection == AffectionLevel.Like)
				{
					shopHelperInstance.LikeBiome(nameKey);
					return;
				}
				if (affection == AffectionLevel.Love)
				{
					shopHelperInstance.LoveBiome(nameKey);
					return;
				}
			}
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x005995CD File Offset: 0x005977CD
		public IEnumerator<BiomePreferenceListTrait.BiomePreference> GetEnumerator()
		{
			return this._preferences.GetEnumerator();
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x005995CD File Offset: 0x005977CD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._preferences.GetEnumerator();
		}

		// Token: 0x04004D34 RID: 19764
		private List<BiomePreferenceListTrait.BiomePreference> _preferences;

		// Token: 0x02000761 RID: 1889
		public class BiomePreference
		{
			// Token: 0x060038D6 RID: 14550 RVA: 0x006146ED File Offset: 0x006128ED
			public BiomePreference(AffectionLevel affection, AShoppingBiome biome)
			{
				this.Affection = affection;
				this.Biome = biome;
			}

			// Token: 0x04006456 RID: 25686
			public AffectionLevel Affection;

			// Token: 0x04006457 RID: 25687
			public AShoppingBiome Biome;
		}
	}
}
