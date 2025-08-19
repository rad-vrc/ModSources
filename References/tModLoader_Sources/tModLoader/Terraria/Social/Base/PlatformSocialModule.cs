using System;

namespace Terraria.Social.Base
{
	// Token: 0x02000100 RID: 256
	public abstract class PlatformSocialModule : ISocialModule
	{
		// Token: 0x060018E5 RID: 6373
		public abstract void Initialize();

		// Token: 0x060018E6 RID: 6374
		public abstract void Shutdown();
	}
}
