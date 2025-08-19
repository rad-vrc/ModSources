using System;

namespace Terraria.Audio
{
	/// <summary>
	/// <see cref="T:Terraria.Audio.ActiveSound" />'s update callback.
	/// <br /> Returning false here will force the sound to end abruptly.
	/// <br /> Tip: Use <see cref="M:Terraria.Audio.ProjectileAudioTracker.IsActiveAndInGame" /> to tie sounds to projectiles.
	/// </summary>
	/// <param name="soundInstance"> The sound object instance. </param>
	/// <returns> Whether the sound effect should continue to play. </returns>
	// Token: 0x0200075B RID: 1883
	// (Invoke) Token: 0x06004C14 RID: 19476
	public delegate bool SoundUpdateCallback(ActiveSound soundInstance);
}
