using System;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This is an enum of all possible types of extra mount textures for custom mounts.
	/// <br /> The enum's keys are used in default texture autoloading lookup paths, which can be overriden in <see cref="M:Terraria.ModLoader.ModMount.GetExtraTexture(Terraria.ModLoader.MountTextureType)" />.
	/// </summary>
	// Token: 0x020001DA RID: 474
	public enum MountTextureType
	{
		// Token: 0x04001745 RID: 5957
		Back,
		// Token: 0x04001746 RID: 5958
		BackGlow,
		// Token: 0x04001747 RID: 5959
		BackExtra,
		// Token: 0x04001748 RID: 5960
		BackExtraGlow,
		// Token: 0x04001749 RID: 5961
		Front,
		// Token: 0x0400174A RID: 5962
		FrontGlow,
		// Token: 0x0400174B RID: 5963
		FrontExtra,
		// Token: 0x0400174C RID: 5964
		FrontExtraGlow
	}
}
