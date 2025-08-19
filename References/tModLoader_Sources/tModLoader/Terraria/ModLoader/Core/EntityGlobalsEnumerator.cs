using System;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader.Core
{
	// Token: 0x02000356 RID: 854
	[CompilerFeatureRequired("RefStructs")]
	public ref struct EntityGlobalsEnumerator<TGlobal> where TGlobal : GlobalType<TGlobal>
	{
		// Token: 0x06002F97 RID: 12183 RVA: 0x005364F2 File Offset: 0x005346F2
		public EntityGlobalsEnumerator(TGlobal[] baseGlobals, TGlobal[] entityGlobals)
		{
			this.baseGlobals = ((entityGlobals == null) ? Array.Empty<TGlobal>() : baseGlobals);
			this.entityGlobals = entityGlobals;
			this.i = 0;
			this.current = default(TGlobal);
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x0053651F File Offset: 0x0053471F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityGlobalsEnumerator(IEntityWithGlobals<TGlobal> entity)
		{
			this = new EntityGlobalsEnumerator<TGlobal>(GlobalTypeLookups<TGlobal>.GetGlobalsForType(entity.Type), entity);
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x00536533 File Offset: 0x00534733
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityGlobalsEnumerator(TGlobal[] baseGlobals, IEntityWithGlobals<TGlobal> entity)
		{
			this = new EntityGlobalsEnumerator<TGlobal>(baseGlobals, entity.EntityGlobals.array);
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06002F9A RID: 12186 RVA: 0x00536547 File Offset: 0x00534747
		public TGlobal Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x00536550 File Offset: 0x00534750
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool MoveNext()
		{
			while (this.i < this.baseGlobals.Length)
			{
				TGlobal[] array = this.baseGlobals;
				int num = this.i;
				this.i = num + 1;
				this.current = array[num];
				short slot = this.current.PerEntityIndex;
				if (slot < 0)
				{
					return true;
				}
				this.current = this.entityGlobals[(int)slot];
				if (this.current != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x005365CA File Offset: 0x005347CA
		public EntityGlobalsEnumerator<TGlobal> GetEnumerator()
		{
			return this;
		}

		// Token: 0x04001CB3 RID: 7347
		private readonly TGlobal[] baseGlobals;

		// Token: 0x04001CB4 RID: 7348
		private readonly TGlobal[] entityGlobals;

		// Token: 0x04001CB5 RID: 7349
		private int i;

		// Token: 0x04001CB6 RID: 7350
		private TGlobal current;
	}
}
