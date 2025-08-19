using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;

namespace Terraria.ModLoader.Engine
{
	// Token: 0x020002AC RID: 684
	internal static class EffectsTracker
	{
		// Token: 0x06002CFE RID: 11518 RVA: 0x0052B430 File Offset: 0x00529630
		internal static void CacheVanillaState()
		{
			EffectsTracker.KeyCaches = new EffectsTracker.KeyCache[]
			{
				EffectsTracker.KeyCache.Create<string, Filter>(Filters.Scene._effects),
				EffectsTracker.KeyCache.Create<string, CustomSky>(SkyManager.Instance._effects),
				EffectsTracker.KeyCache.Create<string, Overlay>(Overlays.Scene._effects),
				EffectsTracker.KeyCache.Create<string, Overlay>(Overlays.FilterFallback._effects),
				EffectsTracker.KeyCache.Create<string, MiscShaderData>(GameShaders.Misc)
			};
			EffectsTracker.vanillaArmorShaderCount = GameShaders.Armor._shaderDataCount;
			EffectsTracker.vanillaHairShaderCount = GameShaders.Hair._shaderDataCount;
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x0052B4BC File Offset: 0x005296BC
		internal static void RemoveModEffects()
		{
			if (EffectsTracker.KeyCaches == null)
			{
				return;
			}
			EffectsTracker.KeyCache[] keyCaches = EffectsTracker.KeyCaches;
			for (int i = 0; i < keyCaches.Length; i++)
			{
				keyCaches[i].Reset();
			}
			EffectsTracker.KeyCaches = null;
			EffectsTracker.ResetShaderDataSet<ArmorShaderData, int, int>(EffectsTracker.vanillaArmorShaderCount, ref GameShaders.Armor._shaderDataCount, ref GameShaders.Armor._shaderData, ref GameShaders.Armor._shaderLookupDictionary);
			EffectsTracker.ResetShaderDataSet<HairShaderData, int, int>(EffectsTracker.vanillaHairShaderCount, ref GameShaders.Hair._shaderDataCount, ref GameShaders.Hair._shaderData, ref GameShaders.Hair._shaderLookupDictionary);
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x0052B544 File Offset: 0x00529744
		private static void ResetShaderDataSet<T, U, V>(int vanillaShaderCount, ref U shaderDataCount, ref List<T> shaderData, ref Dictionary<int, V> shaderLookupDictionary)
		{
			shaderDataCount = (U)((object)Convert.ChangeType(vanillaShaderCount, typeof(U)));
			shaderData.RemoveRange(vanillaShaderCount, shaderData.Count - vanillaShaderCount);
			V shaderLookupLimit = (V)((object)Convert.ChangeType(vanillaShaderCount, typeof(V)));
			foreach (KeyValuePair<int, V> entry in shaderLookupDictionary.ToArray<KeyValuePair<int, V>>())
			{
				if (Comparer<V>.Default.Compare(entry.Value, shaderLookupLimit) > 0)
				{
					shaderLookupDictionary.Remove(entry.Key);
				}
			}
		}

		// Token: 0x04001C18 RID: 7192
		private static EffectsTracker.KeyCache[] KeyCaches;

		// Token: 0x04001C19 RID: 7193
		private static int vanillaArmorShaderCount;

		// Token: 0x04001C1A RID: 7194
		internal static int vanillaHairShaderCount;

		// Token: 0x02000A51 RID: 2641
		private abstract class KeyCache
		{
			// Token: 0x0600587A RID: 22650
			public abstract void Reset();

			// Token: 0x0600587B RID: 22651 RVA: 0x0069FE36 File Offset: 0x0069E036
			public static EffectsTracker.KeyCache Create<K, V>(IDictionary<K, V> dict)
			{
				return new EffectsTracker.KeyCache<K, V>(dict);
			}
		}

		// Token: 0x02000A52 RID: 2642
		private class KeyCache<K, V> : EffectsTracker.KeyCache
		{
			// Token: 0x0600587D RID: 22653 RVA: 0x0069FE46 File Offset: 0x0069E046
			public KeyCache(IDictionary<K, V> dict)
			{
				this.dict = dict;
				this.keys = new HashSet<K>(dict.Keys);
			}

			// Token: 0x0600587E RID: 22654 RVA: 0x0069FE68 File Offset: 0x0069E068
			public override void Reset()
			{
				foreach (K i in this.dict.Keys.ToArray<K>())
				{
					if (!this.keys.Contains(i))
					{
						this.dict.Remove(i);
					}
				}
			}

			// Token: 0x04006CE2 RID: 27874
			public IDictionary<K, V> dict;

			// Token: 0x04006CE3 RID: 27875
			public HashSet<K> keys;
		}
	}
}
