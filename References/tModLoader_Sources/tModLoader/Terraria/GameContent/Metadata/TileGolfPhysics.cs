using System;
using Newtonsoft.Json;

namespace Terraria.GameContent.Metadata
{
	// Token: 0x020005DD RID: 1501
	public class TileGolfPhysics
	{
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x0600431B RID: 17179 RVA: 0x005FC66B File Offset: 0x005FA86B
		// (set) Token: 0x0600431C RID: 17180 RVA: 0x005FC673 File Offset: 0x005FA873
		[JsonProperty]
		public float DirectImpactDampening { get; private set; }

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x0600431D RID: 17181 RVA: 0x005FC67C File Offset: 0x005FA87C
		// (set) Token: 0x0600431E RID: 17182 RVA: 0x005FC684 File Offset: 0x005FA884
		[JsonProperty]
		public float SideImpactDampening { get; private set; }

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x0600431F RID: 17183 RVA: 0x005FC68D File Offset: 0x005FA88D
		// (set) Token: 0x06004320 RID: 17184 RVA: 0x005FC695 File Offset: 0x005FA895
		[JsonProperty]
		public float ClubImpactDampening { get; private set; }

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06004321 RID: 17185 RVA: 0x005FC69E File Offset: 0x005FA89E
		// (set) Token: 0x06004322 RID: 17186 RVA: 0x005FC6A6 File Offset: 0x005FA8A6
		[JsonProperty]
		public float PassThroughDampening { get; private set; }

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06004323 RID: 17187 RVA: 0x005FC6AF File Offset: 0x005FA8AF
		// (set) Token: 0x06004324 RID: 17188 RVA: 0x005FC6B7 File Offset: 0x005FA8B7
		[JsonProperty]
		public float ImpactDampeningResistanceEfficiency { get; private set; }
	}
}
