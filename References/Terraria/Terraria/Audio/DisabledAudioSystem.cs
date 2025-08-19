using System;
using System.Collections;
using System.Collections.Generic;
using ReLogic.Content.Sources;

namespace Terraria.Audio
{
	// Token: 0x0200047C RID: 1148
	public class DisabledAudioSystem : IAudioSystem, IDisposable
	{
		// Token: 0x06002DB3 RID: 11699 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void LoadFromSources()
		{
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void UseSources(List<IContentSource> sources)
		{
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void Update()
		{
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void UpdateMisc()
		{
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x005BF3AD File Offset: 0x005BD5AD
		public IEnumerator PrepareWaveBank()
		{
			yield break;
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void LoadCue(int cueIndex, string cueName)
		{
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void PauseAll()
		{
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void ResumeAll()
		{
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void UpdateAmbientCueState(int i, bool gameIsActive, ref float trackVolume, float systemVolume)
		{
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void UpdateAmbientCueTowardStopping(int i, float stoppingSpeed, ref float trackVolume, float systemVolume)
		{
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		public bool IsTrackPlaying(int trackIndex)
		{
			return false;
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void UpdateCommonTrack(bool active, int i, float totalVolume, ref float tempFade)
		{
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void UpdateCommonTrackTowardStopping(int i, float totalVolume, ref float tempFade, bool isMainTrackAudible)
		{
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void UpdateAudioEngine()
		{
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void Dispose()
		{
		}
	}
}
