using System;

namespace Terraria.GameContent
{
	// Token: 0x0200049B RID: 1179
	[Flags]
	public enum GameNotificationType
	{
		// Token: 0x0400524B RID: 21067
		None = 0,
		// Token: 0x0400524C RID: 21068
		Damage = 1,
		// Token: 0x0400524D RID: 21069
		SpawnOrDeath = 2,
		// Token: 0x0400524E RID: 21070
		WorldGen = 4,
		// Token: 0x0400524F RID: 21071
		All = 7
	}
}
