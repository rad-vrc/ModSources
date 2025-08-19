using System;

namespace Terraria.ModLoader.Core
{
	// Token: 0x02000360 RID: 864
	internal class ModMemoryUsage
	{
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06002FF8 RID: 12280 RVA: 0x00537E70 File Offset: 0x00536070
		internal long total
		{
			get
			{
				return this.managed + this.code + this.sounds + this.textures;
			}
		}

		// Token: 0x04001CD9 RID: 7385
		internal long managed;

		// Token: 0x04001CDA RID: 7386
		internal long sounds;

		// Token: 0x04001CDB RID: 7387
		internal long textures;

		// Token: 0x04001CDC RID: 7388
		internal long code;
	}
}
