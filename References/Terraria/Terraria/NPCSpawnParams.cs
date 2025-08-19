using System;
using Terraria.DataStructures;

namespace Terraria
{
	// Token: 0x02000039 RID: 57
	public struct NPCSpawnParams
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x0014AE7C File Offset: 0x0014907C
		public NPCSpawnParams WithScale(float scaleOverride)
		{
			return new NPCSpawnParams
			{
				sizeScaleOverride = new float?(scaleOverride),
				playerCountForMultiplayerDifficultyOverride = this.playerCountForMultiplayerDifficultyOverride,
				gameModeData = this.gameModeData,
				strengthMultiplierOverride = this.strengthMultiplierOverride
			};
		}

		// Token: 0x040002BD RID: 701
		public float? sizeScaleOverride;

		// Token: 0x040002BE RID: 702
		public int? playerCountForMultiplayerDifficultyOverride;

		// Token: 0x040002BF RID: 703
		public GameModeData gameModeData;

		// Token: 0x040002C0 RID: 704
		public float? strengthMultiplierOverride;
	}
}
