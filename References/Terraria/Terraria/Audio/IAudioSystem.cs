using System;
using System.Collections;
using System.Collections.Generic;
using ReLogic.Content.Sources;

namespace Terraria.Audio
{
	// Token: 0x0200047E RID: 1150
	public interface IAudioSystem : IDisposable
	{
		// Token: 0x06002DC8 RID: 11720
		void LoadCue(int cueIndex, string cueName);

		// Token: 0x06002DC9 RID: 11721
		void PauseAll();

		// Token: 0x06002DCA RID: 11722
		void ResumeAll();

		// Token: 0x06002DCB RID: 11723
		void UpdateMisc();

		// Token: 0x06002DCC RID: 11724
		void UpdateAudioEngine();

		// Token: 0x06002DCD RID: 11725
		void UpdateAmbientCueState(int i, bool gameIsActive, ref float trackVolume, float systemVolume);

		// Token: 0x06002DCE RID: 11726
		void UpdateAmbientCueTowardStopping(int i, float stoppingSpeed, ref float trackVolume, float systemVolume);

		// Token: 0x06002DCF RID: 11727
		void UpdateCommonTrack(bool active, int i, float totalVolume, ref float tempFade);

		// Token: 0x06002DD0 RID: 11728
		void UpdateCommonTrackTowardStopping(int i, float totalVolume, ref float tempFade, bool isMainTrackAudible);

		// Token: 0x06002DD1 RID: 11729
		bool IsTrackPlaying(int trackIndex);

		// Token: 0x06002DD2 RID: 11730
		void UseSources(List<IContentSource> sources);

		// Token: 0x06002DD3 RID: 11731
		IEnumerator PrepareWaveBank();

		// Token: 0x06002DD4 RID: 11732
		void LoadFromSources();

		// Token: 0x06002DD5 RID: 11733
		void Update();
	}
}
