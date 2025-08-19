using System;
using Terraria.GameContent.Drawing;

namespace Terraria.GameContent
{
	// Token: 0x020004B4 RID: 1204
	public struct ShimmerUnstuckHelper
	{
		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060039CD RID: 14797 RVA: 0x0059BEAD File Offset: 0x0059A0AD
		public bool ShouldUnstuck
		{
			get
			{
				return this.IndefiniteProtectionActive || this.TimeLeftUnstuck > 0;
			}
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x0059BEC4 File Offset: 0x0059A0C4
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
			if (!this.IndefiniteProtectionActive && this.TimeLeftUnstuck > 0)
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

		// Token: 0x060039CF RID: 14799 RVA: 0x0059BF46 File Offset: 0x0059A146
		public void StartUnstuck()
		{
			this.IndefiniteProtectionActive = true;
			this.TimeLeftUnstuck = 120;
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x0059BF57 File Offset: 0x0059A157
		public void Clear()
		{
			this.IndefiniteProtectionActive = false;
			this.TimeLeftUnstuck = 0;
		}

		// Token: 0x04005282 RID: 21122
		public int TimeLeftUnstuck;

		// Token: 0x04005283 RID: 21123
		public bool IndefiniteProtectionActive;
	}
}
