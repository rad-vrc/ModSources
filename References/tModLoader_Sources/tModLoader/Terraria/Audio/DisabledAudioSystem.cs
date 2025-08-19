using System;
using System.Collections;
using System.Collections.Generic;
using ReLogic.Content.Sources;

namespace Terraria.Audio
{
	// Token: 0x0200075F RID: 1887
	public class DisabledAudioSystem : IAudioSystem, IDisposable
	{
		// Token: 0x06004C43 RID: 19523 RVA: 0x0066D26E File Offset: 0x0066B46E
		public void LoadFromSources()
		{
		}

		// Token: 0x06004C44 RID: 19524 RVA: 0x0066D270 File Offset: 0x0066B470
		public void UseSources(List<IContentSource> sources)
		{
		}

		// Token: 0x06004C45 RID: 19525 RVA: 0x0066D272 File Offset: 0x0066B472
		public void Update()
		{
		}

		// Token: 0x06004C46 RID: 19526 RVA: 0x0066D274 File Offset: 0x0066B474
		public void UpdateMisc()
		{
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x0066D276 File Offset: 0x0066B476
		public IEnumerator PrepareWaveBank()
		{
			yield break;
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x0066D27E File Offset: 0x0066B47E
		public void LoadCue(int cueIndex, string cueName)
		{
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x0066D280 File Offset: 0x0066B480
		public void PauseAll()
		{
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x0066D282 File Offset: 0x0066B482
		public void ResumeAll()
		{
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x0066D284 File Offset: 0x0066B484
		public void UpdateAmbientCueState(int i, bool gameIsActive, ref float trackVolume, float systemVolume)
		{
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x0066D286 File Offset: 0x0066B486
		public void UpdateAmbientCueTowardStopping(int i, float stoppingSpeed, ref float trackVolume, float systemVolume)
		{
		}

		// Token: 0x06004C4D RID: 19533 RVA: 0x0066D288 File Offset: 0x0066B488
		public bool IsTrackPlaying(int trackIndex)
		{
			return false;
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x0066D28B File Offset: 0x0066B48B
		public void UpdateCommonTrack(bool active, int i, float totalVolume, ref float tempFade)
		{
		}

		// Token: 0x06004C4F RID: 19535 RVA: 0x0066D28D File Offset: 0x0066B48D
		public void UpdateCommonTrackTowardStopping(int i, float totalVolume, ref float tempFade, bool isMainTrackAudible)
		{
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x0066D28F File Offset: 0x0066B48F
		public void UpdateAudioEngine()
		{
		}

		// Token: 0x06004C51 RID: 19537 RVA: 0x0066D291 File Offset: 0x0066B491
		public void Dispose()
		{
		}
	}
}
