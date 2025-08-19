using System;
using Terraria.GameContent.Drawing;

namespace Terraria.GameContent
{
	// Token: 0x020001CB RID: 459
	public struct ShimmerUnstuckHelper
	{
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x004F05BD File Offset: 0x004EE7BD
		public bool ShouldUnstuck
		{
			get
			{
				return this.IndefiniteProtectionActive || this.TimeLeftUnstuck > 0;
			}
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x004F05D4 File Offset: 0x004EE7D4
		public void Update(Player player)
		{
			bool flag = !player.shimmering && !player.shimmerWet;
			if (flag)
			{
				this.IndefiniteProtectionActive = false;
			}
			if (this.TimeLeftUnstuck > 0 && !flag)
			{
				this.StartUnstuck();
			}
			if (this.IndefiniteProtectionActive)
			{
				return;
			}
			if (this.TimeLeftUnstuck > 0)
			{
				this.TimeLeftUnstuck--;
				if (this.TimeLeftUnstuck == 0)
				{
					ParticleOrchestrator.BroadcastOrRequestParticleSpawn(ParticleOrchestraType.ShimmerTownNPC, new ParticleOrchestraSettings
					{
						PositionInWorld = player.Bottom
					});
				}
			}
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x004F0657 File Offset: 0x004EE857
		public void StartUnstuck()
		{
			this.IndefiniteProtectionActive = true;
			this.TimeLeftUnstuck = 120;
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x004F0668 File Offset: 0x004EE868
		public void Clear()
		{
			this.IndefiniteProtectionActive = false;
			this.TimeLeftUnstuck = 0;
		}

		// Token: 0x04004351 RID: 17233
		public int TimeLeftUnstuck;

		// Token: 0x04004352 RID: 17234
		public bool IndefiniteProtectionActive;
	}
}
