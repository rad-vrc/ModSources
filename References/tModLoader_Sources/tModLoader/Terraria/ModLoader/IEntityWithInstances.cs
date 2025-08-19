using System;

namespace Terraria.ModLoader
{
	// Token: 0x02000179 RID: 377
	public interface IEntityWithInstances<T>
	{
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001DF4 RID: 7668
		RefReadOnlyArray<T> Instances { get; }
	}
}
