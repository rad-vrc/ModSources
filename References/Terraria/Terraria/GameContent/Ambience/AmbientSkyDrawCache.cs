using System;

namespace Terraria.GameContent.Ambience
{
	// Token: 0x0200032E RID: 814
	public class AmbientSkyDrawCache
	{
		// Token: 0x060024EC RID: 9452 RVA: 0x00566530 File Offset: 0x00564730
		public void SetUnderworldInfo(int drawIndex, float scale)
		{
			this.Underworld[drawIndex] = new AmbientSkyDrawCache.UnderworldCache
			{
				Scale = scale
			};
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x0056655C File Offset: 0x0056475C
		public void SetOceanLineInfo(float yScreenPosition, float oceanOpacity)
		{
			this.OceanLineInfo = new AmbientSkyDrawCache.OceanLineCache
			{
				YScreenPosition = yScreenPosition,
				OceanOpacity = oceanOpacity
			};
		}

		// Token: 0x040048E1 RID: 18657
		public static AmbientSkyDrawCache Instance = new AmbientSkyDrawCache();

		// Token: 0x040048E2 RID: 18658
		public AmbientSkyDrawCache.UnderworldCache[] Underworld = new AmbientSkyDrawCache.UnderworldCache[5];

		// Token: 0x040048E3 RID: 18659
		public AmbientSkyDrawCache.OceanLineCache OceanLineInfo;

		// Token: 0x02000718 RID: 1816
		public struct UnderworldCache
		{
			// Token: 0x0400632A RID: 25386
			public float Scale;
		}

		// Token: 0x02000719 RID: 1817
		public struct OceanLineCache
		{
			// Token: 0x0400632B RID: 25387
			public float YScreenPosition;

			// Token: 0x0400632C RID: 25388
			public float OceanOpacity;
		}
	}
}
