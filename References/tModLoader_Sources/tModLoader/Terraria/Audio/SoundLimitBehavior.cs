using System;

namespace Terraria.Audio
{
	/// <inheritdoc cref="P:Terraria.Audio.SoundStyle.SoundLimitBehavior" />
	// Token: 0x0200076C RID: 1900
	public enum SoundLimitBehavior
	{
		/// <summary> When the sound limit is reached, no sound instance will be started. </summary>
		// Token: 0x0400611D RID: 24861
		IgnoreNew,
		/// <summary> When the sound limit is reached, a currently playing sound will be stopped and a new sound instance will be started. </summary>
		// Token: 0x0400611E RID: 24862
		ReplaceOldest
	}
}
