using System;

namespace Terraria.ModLoader
{
	// Token: 0x020001D1 RID: 465
	public abstract class ModType<TEntity> : ModType
	{
		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600249B RID: 9371 RVA: 0x004E9E4F File Offset: 0x004E804F
		// (set) Token: 0x0600249C RID: 9372 RVA: 0x004E9E57 File Offset: 0x004E8057
		public TEntity Entity { get; internal set; }

		// Token: 0x0600249D RID: 9373 RVA: 0x004E9E60 File Offset: 0x004E8060
		protected override void InitTemplateInstance()
		{
			this.Entity = this.CreateTemplateEntity();
		}

		// Token: 0x0600249E RID: 9374
		protected abstract TEntity CreateTemplateEntity();
	}
}
