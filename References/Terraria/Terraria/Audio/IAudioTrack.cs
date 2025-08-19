using System;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x0200047F RID: 1151
	public interface IAudioTrack : IDisposable
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06002DD6 RID: 11734
		bool IsPlaying { get; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06002DD7 RID: 11735
		bool IsStopped { get; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06002DD8 RID: 11736
		bool IsPaused { get; }

		// Token: 0x06002DD9 RID: 11737
		void Stop(AudioStopOptions options);

		// Token: 0x06002DDA RID: 11738
		void Play();

		// Token: 0x06002DDB RID: 11739
		void Pause();

		// Token: 0x06002DDC RID: 11740
		void SetVariable(string variableName, float value);

		// Token: 0x06002DDD RID: 11741
		void Resume();

		// Token: 0x06002DDE RID: 11742
		void Reuse();

		// Token: 0x06002DDF RID: 11743
		void Update();
	}
}
