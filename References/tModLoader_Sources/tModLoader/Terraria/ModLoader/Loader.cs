using System;

namespace Terraria.ModLoader
{
	/// <summary> Serves as a highest-level base for loaders. </summary>
	// Token: 0x0200018D RID: 397
	public abstract class Loader : ILoader
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x004DD2CC File Offset: 0x004DB4CC
		// (set) Token: 0x06001EE3 RID: 7907 RVA: 0x004DD2D4 File Offset: 0x004DB4D4
		public int VanillaCount { get; set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x004DD2DD File Offset: 0x004DB4DD
		// (set) Token: 0x06001EE5 RID: 7909 RVA: 0x004DD2E5 File Offset: 0x004DB4E5
		internal int TotalCount { get; set; }

		/// <summary>
		/// Initializes the loader based on the vanilla count of the ModType.
		/// </summary>
		// Token: 0x06001EE6 RID: 7910 RVA: 0x004DD2EE File Offset: 0x004DB4EE
		internal void Initialize(int vanillaCount)
		{
			this.VanillaCount = vanillaCount;
			this.TotalCount = vanillaCount;
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x004DD300 File Offset: 0x004DB500
		protected int Reserve()
		{
			int totalCount = this.TotalCount;
			this.TotalCount = totalCount + 1;
			return totalCount;
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x004DD31E File Offset: 0x004DB51E
		internal virtual void ResizeArrays()
		{
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x004DD320 File Offset: 0x004DB520
		internal virtual void Unload()
		{
			this.TotalCount = this.VanillaCount;
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x004DD32E File Offset: 0x004DB52E
		void ILoader.ResizeArrays()
		{
			this.ResizeArrays();
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x004DD336 File Offset: 0x004DB536
		void ILoader.Unload()
		{
			this.Unload();
		}
	}
}
