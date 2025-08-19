using System;
using System.Collections;
using System.Collections.Generic;
using ReLogic.Content.Sources;

namespace Terraria.Audio
{
	// Token: 0x02000761 RID: 1889
	public interface IAudioSystem : IDisposable
	{
		// Token: 0x06004C57 RID: 19543
		void LoadCue(int cueIndex, string cueName);

		// Token: 0x06004C58 RID: 19544
		void PauseAll();

		// Token: 0x06004C59 RID: 19545
		void ResumeAll();

		// Token: 0x06004C5A RID: 19546
		void UpdateMisc();

		// Token: 0x06004C5B RID: 19547
		void UpdateAudioEngine();

		// Token: 0x06004C5C RID: 19548
		void UpdateAmbientCueState(int i, bool gameIsActive, ref float trackVolume, float systemVolume);

		// Token: 0x06004C5D RID: 19549
		void UpdateAmbientCueTowardStopping(int i, float stoppingSpeed, ref float trackVolume, float systemVolume);

		// Token: 0x06004C5E RID: 19550
		void UpdateCommonTrack(bool active, int i, float totalVolume, ref float tempFade);

		// Token: 0x06004C5F RID: 19551
		void UpdateCommonTrackTowardStopping(int i, float totalVolume, ref float tempFade, bool isMainTrackAudible);

		// Token: 0x06004C60 RID: 19552
		bool IsTrackPlaying(int trackIndex);

		// Token: 0x06004C61 RID: 19553
		void UseSources(List<IContentSource> sources);

		// Token: 0x06004C62 RID: 19554
		IEnumerator PrepareWaveBank();

		// Token: 0x06004C63 RID: 19555
		void LoadFromSources();

		// Token: 0x06004C64 RID: 19556
		void Update();
	}
}
