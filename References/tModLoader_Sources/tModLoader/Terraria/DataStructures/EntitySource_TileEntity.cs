using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used by vanilla for training dummies
	/// </summary>
	// Token: 0x02000702 RID: 1794
	[NullableContext(1)]
	[Nullable(0)]
	public class EntitySource_TileEntity : IEntitySource
	{
		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06004988 RID: 18824 RVA: 0x0064DA0D File Offset: 0x0064BC0D
		public TileEntity TileEntity { get; }

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06004989 RID: 18825 RVA: 0x0064DA15 File Offset: 0x0064BC15
		[Nullable(2)]
		public string Context { [NullableContext(2)] get; }

		// Token: 0x0600498A RID: 18826 RVA: 0x0064DA1D File Offset: 0x0064BC1D
		public EntitySource_TileEntity(TileEntity tileEntity, [Nullable(2)] string context = null)
		{
			this.TileEntity = tileEntity;
			this.Context = context;
		}
	}
}
