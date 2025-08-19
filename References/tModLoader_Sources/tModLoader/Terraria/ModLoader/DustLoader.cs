using System;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	// Token: 0x02000159 RID: 345
	public static class DustLoader
	{
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x004D12F7 File Offset: 0x004CF4F7
		// (set) Token: 0x06001BD4 RID: 7124 RVA: 0x004D12FE File Offset: 0x004CF4FE
		internal static int DustCount { get; private set; } = (int)DustID.Count;

		/// <summary>
		/// Gets the ModDust instance with the given type. Returns null if no ModDust with the given type exists.
		/// </summary>
		// Token: 0x06001BD5 RID: 7125 RVA: 0x004D1306 File Offset: 0x004CF506
		public static ModDust GetDust(int type)
		{
			if (type < (int)DustID.Count || type >= DustLoader.DustCount)
			{
				return null;
			}
			return DustLoader.dusts[type - (int)DustID.Count];
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x004D132B File Offset: 0x004CF52B
		internal static int ReserveDustID()
		{
			return DustLoader.DustCount++;
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x004D133C File Offset: 0x004CF53C
		internal static void ResizeArrays()
		{
			LoaderUtils.ResetStaticMembers(typeof(DustID), true);
			Array.Resize<bool>(ref ChildSafety.SafeDust, DustLoader.DustCount);
			for (int i = (int)DustID.Count; i < DustLoader.DustCount; i++)
			{
				ChildSafety.SafeDust[i] = true;
			}
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x004D1384 File Offset: 0x004CF584
		internal static void Unload()
		{
			DustLoader.dusts.Clear();
			DustLoader.DustCount = (int)DustID.Count;
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x004D139C File Offset: 0x004CF59C
		internal static void SetupDust(Dust dust)
		{
			ModDust modDust = DustLoader.GetDust(dust.type);
			if (modDust != null)
			{
				dust.frame.X = 0;
				dust.frame.Y = dust.frame.Y % 30;
				modDust.OnSpawn(dust);
			}
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x004D13DC File Offset: 0x004CF5DC
		internal static void SetupUpdateType(Dust dust)
		{
			ModDust modDust = DustLoader.GetDust(dust.type);
			if (modDust != null && modDust.UpdateType >= 0)
			{
				dust.realType = dust.type;
				dust.type = modDust.UpdateType;
			}
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x004D1419 File Offset: 0x004CF619
		internal static void TakeDownUpdateType(Dust dust)
		{
			if (dust.realType >= 0)
			{
				dust.type = dust.realType;
				dust.realType = -1;
			}
		}

		// Token: 0x040014EC RID: 5356
		internal static readonly IList<ModDust> dusts = new List<ModDust>();
	}
}
