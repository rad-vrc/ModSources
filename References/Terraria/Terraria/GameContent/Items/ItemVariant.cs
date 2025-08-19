using System;
using Terraria.Localization;

namespace Terraria.GameContent.Items
{
	// Token: 0x020003D8 RID: 984
	public class ItemVariant
	{
		// Token: 0x06002A9E RID: 10910 RVA: 0x0059A545 File Offset: 0x00598745
		public ItemVariant(NetworkText description)
		{
			this.Description = description;
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x0059A554 File Offset: 0x00598754
		public override string ToString()
		{
			return this.Description.ToString();
		}

		// Token: 0x04004D45 RID: 19781
		public readonly NetworkText Description;
	}
}
