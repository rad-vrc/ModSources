using System;

namespace Terraria.GameContent
{
	// Token: 0x020001FE RID: 510
	[Flags]
	public enum GameNotificationType
	{
		// Token: 0x04004418 RID: 17432
		None = 0,
		// Token: 0x04004419 RID: 17433
		Damage = 1,
		// Token: 0x0400441A RID: 17434
		SpawnOrDeath = 2,
		// Token: 0x0400441B RID: 17435
		WorldGen = 4,
		// Token: 0x0400441C RID: 17436
		All = 7
	}
}
