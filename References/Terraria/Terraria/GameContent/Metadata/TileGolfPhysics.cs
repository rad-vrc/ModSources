using System;
using Newtonsoft.Json;

namespace Terraria.GameContent.Metadata
{
	// Token: 0x0200020F RID: 527
	public class TileGolfPhysics
	{
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x00507428 File Offset: 0x00505628
		// (set) Token: 0x06001DE6 RID: 7654 RVA: 0x00507430 File Offset: 0x00505630
		[JsonProperty]
		public float DirectImpactDampening { get; private set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x00507439 File Offset: 0x00505639
		// (set) Token: 0x06001DE8 RID: 7656 RVA: 0x00507441 File Offset: 0x00505641
		[JsonProperty]
		public float SideImpactDampening { get; private set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x0050744A File Offset: 0x0050564A
		// (set) Token: 0x06001DEA RID: 7658 RVA: 0x00507452 File Offset: 0x00505652
		[JsonProperty]
		public float ClubImpactDampening { get; private set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x0050745B File Offset: 0x0050565B
		// (set) Token: 0x06001DEC RID: 7660 RVA: 0x00507463 File Offset: 0x00505663
		[JsonProperty]
		public float PassThroughDampening { get; private set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06001DED RID: 7661 RVA: 0x0050746C File Offset: 0x0050566C
		// (set) Token: 0x06001DEE RID: 7662 RVA: 0x00507474 File Offset: 0x00505674
		[JsonProperty]
		public float ImpactDampeningResistanceEfficiency { get; private set; }
	}
}
