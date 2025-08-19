using System;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Magnets
{
	// Token: 0x020001AD RID: 429
	public class MagnetPlayer : ModPlayer
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x0001BDF4 File Offset: 0x00019FF4
		public override void Initialize()
		{
			this.Reset();
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0001BDF4 File Offset: 0x00019FF4
		public override void ResetEffects()
		{
			this.Reset();
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001BDF4 File Offset: 0x00019FF4
		public override void UpdateDead()
		{
			this.Reset();
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001BDFC File Offset: 0x00019FFC
		private void Reset()
		{
			this.BaseMagnet = false;
			this.HellstoneMagnet = false;
			this.SoulMagnet = false;
			this.SpectreMagnet = false;
			this.LunarMagnet = false;
		}

		// Token: 0x04000046 RID: 70
		public bool BaseMagnet;

		// Token: 0x04000047 RID: 71
		public bool HellstoneMagnet;

		// Token: 0x04000048 RID: 72
		public bool SoulMagnet;

		// Token: 0x04000049 RID: 73
		public bool SpectreMagnet;

		// Token: 0x0400004A RID: 74
		public bool LunarMagnet;
	}
}
