using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Content.Sources;
using Terraria.ID;
using Terraria.ModLoader.Engine;

namespace Terraria.Audio
{
	// Token: 0x02000763 RID: 1891
	public class LegacyAudioSystem : IAudioSystem, IDisposable
	{
		// Token: 0x06004C6F RID: 19567 RVA: 0x0066D344 File Offset: 0x0066B544
		public void LoadFromSources()
		{
			List<IContentSource> fileSources = this.FileSources;
			for (int i = 0; i < this.AudioTracks.Length; i++)
			{
				string value;
				if (this.TrackNamesByIndex.TryGetValue(i, out value))
				{
					string assetPath = "Music" + Path.DirectorySeparatorChar.ToString() + value;
					IAudioTrack audioTrack = this.DefaultTrackByIndex[i];
					IAudioTrack audioTrack2 = audioTrack;
					IAudioTrack audioTrack3 = this.FindReplacementTrack(fileSources, assetPath);
					if (audioTrack3 != null)
					{
						audioTrack2 = audioTrack3;
					}
					if (this.AudioTracks[i] != audioTrack2)
					{
						this.AudioTracks[i].Stop(1);
					}
					if (this.AudioTracks[i] != audioTrack)
					{
						this.AudioTracks[i].Dispose();
					}
					this.AudioTracks[i] = audioTrack2;
				}
			}
		}

		// Token: 0x06004C70 RID: 19568 RVA: 0x0066D3F8 File Offset: 0x0066B5F8
		public void UseSources(List<IContentSource> sourcesFromLowestToHighest)
		{
			this.FileSources = sourcesFromLowestToHighest;
			this.LoadFromSources();
		}

		// Token: 0x06004C71 RID: 19569 RVA: 0x0066D408 File Offset: 0x0066B608
		public void Update()
		{
			if (!this.WaveBank.IsPrepared)
			{
				return;
			}
			for (int i = 0; i < this.AudioTracks.Length; i++)
			{
				if (this.AudioTracks[i] != null)
				{
					this.AudioTracks[i].Update();
				}
			}
		}

		// Token: 0x06004C72 RID: 19570 RVA: 0x0066D450 File Offset: 0x0066B650
		private IAudioTrack FindReplacementTrack(List<IContentSource> sources, string assetPath)
		{
			IAudioTrack audioTrack = null;
			for (int i = 0; i < sources.Count; i++)
			{
				IContentSource contentSource = sources[i];
				if (contentSource.HasAsset(assetPath))
				{
					string extension = contentSource.GetExtension(assetPath);
					string assetPathWithExtension = assetPath + extension;
					try
					{
						IAudioTrack audioTrack2 = null;
						if (!(extension == ".ogg"))
						{
							if (!(extension == ".wav"))
							{
								if (extension == ".mp3")
								{
									audioTrack2 = new MP3AudioTrack(contentSource.OpenStream(assetPathWithExtension));
								}
							}
							else
							{
								audioTrack2 = new WAVAudioTrack(contentSource.OpenStream(assetPathWithExtension));
							}
						}
						else
						{
							audioTrack2 = new OGGAudioTrack(contentSource.OpenStream(assetPathWithExtension));
						}
						if (audioTrack2 != null)
						{
							if (audioTrack != null)
							{
								audioTrack.Dispose();
							}
							audioTrack = audioTrack2;
						}
					}
					catch
					{
						string textToShow = "A resource pack failed to load " + assetPath + "!";
						Main.IssueReporter.AddReport(textToShow);
						Main.IssueReporterIndicator.AttemptLettingPlayerKnow();
					}
				}
			}
			return audioTrack;
		}

		// Token: 0x06004C73 RID: 19571 RVA: 0x0066D548 File Offset: 0x0066B748
		public LegacyAudioSystem()
		{
			TMLContentManager contentManager = (TMLContentManager)Main.instance.Content;
			this.Engine = new AudioEngine(contentManager.GetPath("TerrariaMusic.xgs"));
			FNALogging.PostAudioInit();
			this.SoundBank = new SoundBank(this.Engine, contentManager.GetPath("Sound Bank.xsb"));
			this.Engine.Update();
			this.WaveBank = new WaveBank(this.Engine, contentManager.GetPath("Wave Bank.xwb"), 0, 512);
			this.Engine.Update();
			this.AudioTracks = new IAudioTrack[Main.maxMusic];
			this.TrackNamesByIndex = new Dictionary<int, string>();
			this.DefaultTrackByIndex = new Dictionary<int, IAudioTrack>();
		}

		// Token: 0x06004C74 RID: 19572 RVA: 0x0066D600 File Offset: 0x0066B800
		public IEnumerator PrepareWaveBank()
		{
			while (!this.WaveBank.IsPrepared)
			{
				this.Engine.Update();
				yield return null;
			}
			yield break;
		}

		// Token: 0x06004C75 RID: 19573 RVA: 0x0066D60F File Offset: 0x0066B80F
		internal Cue GetCueInternal(string cueName)
		{
			return this.SoundBank.GetCue(cueName);
		}

		// Token: 0x06004C76 RID: 19574 RVA: 0x0066D620 File Offset: 0x0066B820
		public void LoadCue(int cueIndex, string cueName)
		{
			CueAudioTrack cueAudioTrack = new CueAudioTrack(this.SoundBank, cueName);
			this.TrackNamesByIndex[cueIndex] = cueName;
			this.DefaultTrackByIndex[cueIndex] = cueAudioTrack;
			this.AudioTracks[cueIndex] = cueAudioTrack;
		}

		// Token: 0x06004C77 RID: 19575 RVA: 0x0066D65D File Offset: 0x0066B85D
		public void UpdateMisc()
		{
			if (Main.curMusic != Main.newMusic)
			{
				this.MusicReplayDelay = 0;
			}
			if (this.MusicReplayDelay > 0)
			{
				this.MusicReplayDelay--;
			}
		}

		// Token: 0x06004C78 RID: 19576 RVA: 0x0066D68C File Offset: 0x0066B88C
		public void PauseAll()
		{
			if (!this.WaveBank.IsPrepared)
			{
				return;
			}
			float[] musicFade = Main.musicFade;
			for (int i = 0; i < this.AudioTracks.Length; i++)
			{
				if (this.AudioTracks[i] != null && !this.AudioTracks[i].IsPaused && this.AudioTracks[i].IsPlaying && musicFade[i] > 0f)
				{
					try
					{
						this.AudioTracks[i].Pause();
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x06004C79 RID: 19577 RVA: 0x0066D714 File Offset: 0x0066B914
		public void ResumeAll()
		{
			if (!this.WaveBank.IsPrepared)
			{
				return;
			}
			float[] musicFade = Main.musicFade;
			for (int i = 0; i < this.AudioTracks.Length; i++)
			{
				if (this.AudioTracks[i] != null && this.AudioTracks[i].IsPaused && musicFade[i] > 0f)
				{
					try
					{
						this.AudioTracks[i].Resume();
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x06004C7A RID: 19578 RVA: 0x0066D790 File Offset: 0x0066B990
		public void UpdateAmbientCueState(int i, bool gameIsActive, ref float trackVolume, float systemVolume)
		{
			if (!this.WaveBank.IsPrepared)
			{
				return;
			}
			if (systemVolume == 0f)
			{
				if (this.AudioTracks[i].IsPlaying)
				{
					this.AudioTracks[i].Stop(1);
				}
				return;
			}
			if (!this.AudioTracks[i].IsPlaying)
			{
				this.AudioTracks[i].Reuse();
				this.AudioTracks[i].Play();
				this.AudioTracks[i].SetVariable("Volume", trackVolume * systemVolume);
				return;
			}
			if (this.AudioTracks[i].IsPaused && gameIsActive)
			{
				this.AudioTracks[i].Resume();
				return;
			}
			trackVolume += 0.005f;
			if (trackVolume > 1f)
			{
				trackVolume = 1f;
			}
			this.AudioTracks[i].SetVariable("Volume", trackVolume * systemVolume);
		}

		// Token: 0x06004C7B RID: 19579 RVA: 0x0066D864 File Offset: 0x0066BA64
		public void UpdateAmbientCueTowardStopping(int i, float stoppingSpeed, ref float trackVolume, float systemVolume)
		{
			if (!this.WaveBank.IsPrepared)
			{
				return;
			}
			if (!this.AudioTracks[i].IsPlaying)
			{
				trackVolume = 0f;
				return;
			}
			if (trackVolume > 0f)
			{
				trackVolume -= stoppingSpeed;
				if (trackVolume < 0f)
				{
					trackVolume = 0f;
				}
			}
			if (trackVolume <= 0f)
			{
				this.AudioTracks[i].Stop(1);
				return;
			}
			this.AudioTracks[i].SetVariable("Volume", trackVolume * systemVolume);
		}

		// Token: 0x06004C7C RID: 19580 RVA: 0x0066D8E4 File Offset: 0x0066BAE4
		public bool IsTrackPlaying(int trackIndex)
		{
			return this.WaveBank.IsPrepared && this.AudioTracks[trackIndex].IsPlaying;
		}

		// Token: 0x06004C7D RID: 19581 RVA: 0x0066D904 File Offset: 0x0066BB04
		public void UpdateCommonTrack(bool active, int i, float totalVolume, ref float tempFade)
		{
			if (!this.WaveBank.IsPrepared)
			{
				return;
			}
			tempFade += 0.005f;
			if (tempFade > 1f)
			{
				tempFade = 1f;
			}
			if (!this.AudioTracks[i].IsPlaying && active)
			{
				if (this.MusicReplayDelay == 0)
				{
					if (Main.SettingMusicReplayDelayEnabled)
					{
						this.MusicReplayDelay = Main.rand.Next(14400, 21601);
					}
					this.AudioTracks[i].Reuse();
					this.AudioTracks[i].SetVariable(MusicID.Sets.SkipsVolumeRemap[i] ? "VolumeDirect" : "Volume", totalVolume);
					this.AudioTracks[i].Play();
					return;
				}
			}
			else
			{
				this.AudioTracks[i].SetVariable(MusicID.Sets.SkipsVolumeRemap[i] ? "VolumeDirect" : "Volume", totalVolume);
			}
		}

		// Token: 0x06004C7E RID: 19582 RVA: 0x0066D9E0 File Offset: 0x0066BBE0
		public void UpdateCommonTrackTowardStopping(int i, float totalVolume, ref float tempFade, bool isMainTrackAudible)
		{
			if (!this.WaveBank.IsPrepared || this.AudioTracks[i] == null)
			{
				return;
			}
			if (!this.AudioTracks[i].IsPlaying && this.AudioTracks[i].IsStopped)
			{
				tempFade = 0f;
				return;
			}
			if (isMainTrackAudible)
			{
				tempFade -= 0.005f;
			}
			else if (Main.curMusic == 0)
			{
				tempFade = 0f;
			}
			if (tempFade <= 0f)
			{
				tempFade = 0f;
				this.AudioTracks[i].SetVariable("Volume", 0f);
				this.AudioTracks[i].Stop(1);
				return;
			}
			this.AudioTracks[i].SetVariable(MusicID.Sets.SkipsVolumeRemap[i] ? "VolumeDirect" : "Volume", totalVolume);
		}

		// Token: 0x06004C7F RID: 19583 RVA: 0x0066DAA2 File Offset: 0x0066BCA2
		public void UpdateAudioEngine()
		{
			this.Engine.Update();
		}

		// Token: 0x06004C80 RID: 19584 RVA: 0x0066DAAF File Offset: 0x0066BCAF
		public void Dispose()
		{
			this.SoundBank.Dispose();
			this.WaveBank.Dispose();
			this.Engine.Dispose();
		}

		// Token: 0x040060B9 RID: 24761
		public IAudioTrack[] AudioTracks;

		// Token: 0x040060BA RID: 24762
		public int MusicReplayDelay;

		// Token: 0x040060BB RID: 24763
		public AudioEngine Engine;

		// Token: 0x040060BC RID: 24764
		public SoundBank SoundBank;

		// Token: 0x040060BD RID: 24765
		public WaveBank WaveBank;

		// Token: 0x040060BE RID: 24766
		public Dictionary<int, string> TrackNamesByIndex;

		// Token: 0x040060BF RID: 24767
		public Dictionary<int, IAudioTrack> DefaultTrackByIndex;

		// Token: 0x040060C0 RID: 24768
		public List<IContentSource> FileSources;
	}
}
