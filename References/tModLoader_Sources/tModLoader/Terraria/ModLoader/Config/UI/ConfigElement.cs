using System;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x02000399 RID: 921
	public abstract class ConfigElement<T> : ConfigElement
	{
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06003198 RID: 12696 RVA: 0x005408F2 File Offset: 0x0053EAF2
		// (set) Token: 0x06003199 RID: 12697 RVA: 0x005408FF File Offset: 0x0053EAFF
		protected virtual T Value
		{
			get
			{
				return (T)((object)this.GetObject());
			}
			set
			{
				this.SetObject(value);
			}
		}
	}
}
