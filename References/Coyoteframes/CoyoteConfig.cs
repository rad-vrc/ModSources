using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Coyoteframes
{
	// Token: 0x02000004 RID: 4
	public class CoyoteConfig : ModConfig
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000021E3 File Offset: 0x000003E3
		public override ConfigScope Mode
		{
			get
			{
				return ConfigScope.ClientSide;
			}
		}

		// Token: 0x04000004 RID: 4
		[Header("Messages")]
		[DefaultValue(true)]
		public bool WelcomeMessageToggle;

		// Token: 0x04000005 RID: 5
		[Header("Frames")]
		[DefaultValue(9)]
		public int CoyoteTimeFramesDefault;
	}
}
