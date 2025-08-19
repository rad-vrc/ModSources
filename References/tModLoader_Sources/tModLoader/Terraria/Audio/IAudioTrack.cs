using System;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x02000762 RID: 1890
	public interface IAudioTrack : IDisposable
	{
		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06004C65 RID: 19557
		bool IsPlaying { get; }

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06004C66 RID: 19558
		bool IsStopped { get; }

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06004C67 RID: 19559
		bool IsPaused { get; }

		// Token: 0x06004C68 RID: 19560
		void Stop(AudioStopOptions options);

		// Token: 0x06004C69 RID: 19561
		void Play();

		// Token: 0x06004C6A RID: 19562
		void Pause();

		// Token: 0x06004C6B RID: 19563
		void SetVariable(string variableName, float value);

		// Token: 0x06004C6C RID: 19564
		void Resume();

		// Token: 0x06004C6D RID: 19565
		void Reuse();

		// Token: 0x06004C6E RID: 19566
		void Update();
	}
}
