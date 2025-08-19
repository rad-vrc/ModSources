using System;

namespace Terraria.GameContent.Ambience
{
	// Token: 0x020006B5 RID: 1717
	public class AmbientSkyDrawCache
	{
		// Token: 0x0600489D RID: 18589 RVA: 0x0064A6F0 File Offset: 0x006488F0
		public void SetUnderworldInfo(int drawIndex, float scale)
		{
			this.Underworld[drawIndex] = new AmbientSkyDrawCache.UnderworldCache
			{
				Scale = scale
			};
		}

		// Token: 0x0600489E RID: 18590 RVA: 0x0064A71C File Offset: 0x0064891C
		public void SetOceanLineInfo(float yScreenPosition, float oceanOpacity)
		{
			this.OceanLineInfo = new AmbientSkyDrawCache.OceanLineCache
			{
				YScreenPosition = yScreenPosition,
				OceanOpacity = oceanOpacity
			};
		}

		// Token: 0x04005C4F RID: 23631
		public static AmbientSkyDrawCache Instance = new AmbientSkyDrawCache();

		// Token: 0x04005C50 RID: 23632
		public AmbientSkyDrawCache.UnderworldCache[] Underworld = new AmbientSkyDrawCache.UnderworldCache[5];

		// Token: 0x04005C51 RID: 23633
		public AmbientSkyDrawCache.OceanLineCache OceanLineInfo;

		// Token: 0x02000D4C RID: 3404
		public struct UnderworldCache
		{
			// Token: 0x04007B79 RID: 31609
			public float Scale;
		}

		// Token: 0x02000D4D RID: 3405
		public struct OceanLineCache
		{
			// Token: 0x04007B7A RID: 31610
			public float YScreenPosition;

			// Token: 0x04007B7B RID: 31611
			public float OceanOpacity;
		}
	}
}
