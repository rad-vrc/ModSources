using System;
using System.Collections.Generic;

namespace Terraria.ModLoader.Utilities
{
	// Token: 0x02000237 RID: 567
	internal static class NPCSpawnHelper
	{
		// Token: 0x060028BF RID: 10431 RVA: 0x0050D61C File Offset: 0x0050B81C
		internal static void Reset()
		{
			foreach (SpawnCondition spawnCondition in NPCSpawnHelper.conditions)
			{
				spawnCondition.Reset();
			}
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x0050D66C File Offset: 0x0050B86C
		internal static void DoChecks(NPCSpawnInfo info)
		{
			float weight = 1f;
			foreach (SpawnCondition spawnCondition in NPCSpawnHelper.conditions)
			{
				spawnCondition.Check(info, ref weight);
				if ((double)Math.Abs(weight) < 5E-06)
				{
					break;
				}
			}
		}

		// Token: 0x04001957 RID: 6487
		internal static List<SpawnCondition> conditions = new List<SpawnCondition>();
	}
}
