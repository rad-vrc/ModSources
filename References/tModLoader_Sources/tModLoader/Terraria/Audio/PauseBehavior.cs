using System;

namespace Terraria.Audio
{
	/// <inheritdoc cref="P:Terraria.Audio.SoundStyle.PauseBehavior" />
	// Token: 0x0200076D RID: 1901
	public enum PauseBehavior
	{
		/// <summary> This sound will keep playing even when the game is paused. </summary>
		// Token: 0x04006120 RID: 24864
		KeepPlaying,
		/// <summary> This sound will pause when the game is paused or unfocused and resume once the game is resumed. </summary>
		// Token: 0x04006121 RID: 24865
		PauseWithGame,
		/// <summary> This sound will stop when the game is paused or unfocused. </summary>
		// Token: 0x04006122 RID: 24866
		StopWhenGamePaused
	}
}
