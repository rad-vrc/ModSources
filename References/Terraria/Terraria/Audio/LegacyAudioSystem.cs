using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Content.Sources;

namespace Terraria.Audio
{
	// Token: 0x02000481 RID: 1153
	public class LegacyAudioSystem : IAudioSystem, IDisposable
	{
		// Token: 0x06002DEC RID: 11756 RVA: 0x005BF4BC File Offset: 0x005BD6BC
		public void LoadFromSources()
		{
			List<IContentSource> fileSources = this.FileSources;
			for (int i = 0; i < this.AudioTracks.Length; i++)
			{
				string str;
				if (this.TrackNamesByIndex.TryGetValue(i, out str))
				{
					string assetPath = "Music" + Path.DirectorySeparatorChar.ToString() + str;
					IAudioTrack audioTrack = this.DefaultTrackByIndex[i];
					IAudioTrack audioTrack2 = audioTrack;
					IAudioTrack audioTrack3 = this.FindReplacementTrack(fileSources, assetPath);
					if (audioTrack3 != null)
					{
						audioTrack2 = audioTrack3;
					}
					if (this.AudioTracks[i] != audioTrack2)
					{
						this.AudioTracks[i].Stop(AudioStopOptions.Immediate);
					}
					if (this.AudioTracks[i] != audioTrack)
					{
						this.AudioTracks[i].Dispose();
					}
					this.AudioTracks[i] = audioTrack2;
				}
			}
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x005BF574 File Offset: 0x005BD774
		public void UseSources(List<IContentSource> sourcesFromLowestToHighest)
		{
			this.FileSources = sourcesFromLowestToHighest;
			this.LoadFromSources();
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x005BF584 File Offset: 0x005BD784
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

		// Token: 0x06002DEF RID: 11759 RVA: 0x005BF5CC File Offset: 0x005BD7CC
		private IAudioTrack FindReplacementTrack(List<IContentSource> sources, string assetPath)
		{
			IAudioTrack audioTrack = null;
			for (int i = 0; i < sources.Count; i++)
			{
				IContentSource contentSource = sources[i];
				if (contentSource.HasAsset(assetPath))
				{
					string extension = contentSource.GetExtension(assetPath);
					try
					{
						IAudioTrack audioTrack2 = null;
						if (!(extension == ".ogg"))
						{
							if (!(extension == ".wav"))
							{
								if (extension == ".mp3")
								{
									audioTrack2 = new MP3AudioTrack(contentSource.OpenStream(assetPath));
								}
							}
							else
							{
								audioTrack2 = new WAVAudioTrack(contentSource.OpenStream(assetPath));
							}
						}
						else
						{
							audioTrack2 = new OGGAudioTrack(contentSource.OpenStream(assetPath));
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

		// Token: 0x06002DF0 RID: 11760 RVA: 0x005BF6B8 File Offset: 0x005BD8B8
		public LegacyAudioSystem()
		{
			this.Engine = new AudioEngine("Content\\TerrariaMusic.xgs");
			this.SoundBank = new SoundBank(this.Engine, "Content\\Sound Bank.xsb");
			this.Engine.Update();
			this.WaveBank = new WaveBank(this.Engine, "Content\\Wave Bank.xwb", 0, 512);
			this.Engine.Update();
			this.AudioTracks = new IAudioTrack[Main.maxMusic];
			this.TrackNamesByIndex = new Dictionary<int, string>();
			this.DefaultTrackByIndex = new Dictionary<int, IAudioTrack>();
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x005BF749 File Offset: 0x005BD949
		public IEnumerator PrepareWaveBank()
		{
			while (!this.WaveBank.IsPrepared)
			{
				this.Engine.Update();
				yield return null;
			}
			yield break;
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x005BF758 File Offset: 0x005BD958
		public void LoadCue(int cueIndex, string cueName)
		{
			CueAudioTrack cueAudioTrack = new CueAudioTrack(this.SoundBank, cueName);
			this.TrackNamesByIndex[cueIndex] = cueName;
			this.DefaultTrackByIndex[cueIndex] = cueAudioTrack;
			this.AudioTracks[cueIndex] = cueAudioTrack;
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x005BF795 File Offset: 0x005BD995
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

		// Token: 0x06002DF4 RID: 11764 RVA: 0x005BF7C4 File Offset: 0x005BD9C4
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

		// Token: 0x06002DF5 RID: 11765 RVA: 0x005BF84C File Offset: 0x005BDA4C
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

		// Token: 0x06002DF6 RID: 11766 RVA: 0x005BF8C8 File Offset: 0x005BDAC8
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
					this.AudioTracks[i].Stop(AudioStopOptions.Immediate);
					return;
				}
			}
			else
			{
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
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x005BF9A0 File Offset: 0x005BDBA0
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
				this.AudioTracks[i].Stop(AudioStopOptions.Immediate);
				return;
			}
			this.AudioTracks[i].SetVariable("Volume", trackVolume * systemVolume);
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x005BFA20 File Offset: 0x005BDC20
		public bool IsTrackPlaying(int trackIndex)
		{
			return this.WaveBank.IsPrepared && this.AudioTracks[trackIndex].IsPlaying;
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x005BFA40 File Offset: 0x005BDC40
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
					this.AudioTracks[i].SetVariable("Volume", totalVolume);
					this.AudioTracks[i].Play();
					return;
				}
			}
			else
			{
				this.AudioTracks[i].SetVariable("Volume", totalVolume);
			}
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x005BFAF8 File Offset: 0x005BDCF8
		public void UpdateCommonTrackTowardStopping(int i, float totalVolume, ref float tempFade, bool isMainTrackAudible)
		{
			if (!this.WaveBank.IsPrepared)
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
				this.AudioTracks[i].Stop(AudioStopOptions.Immediate);
				return;
			}
			this.AudioTracks[i].SetVariable("Volume", totalVolume);
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x005BFBA0 File Offset: 0x005BDDA0
		public void UpdateAudioEngine()
		{
			this.Engine.Update();
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x005BFBAD File Offset: 0x005BDDAD
		public void Dispose()
		{
			this.SoundBank.Dispose();
			this.WaveBank.Dispose();
			this.Engine.Dispose();
		}

		// Token: 0x0400515D RID: 20829
		public IAudioTrack[] AudioTracks;

		// Token: 0x0400515E RID: 20830
		public int MusicReplayDelay;

		// Token: 0x0400515F RID: 20831
		public AudioEngine Engine;

		// Token: 0x04005160 RID: 20832
		public SoundBank SoundBank;

		// Token: 0x04005161 RID: 20833
		public WaveBank WaveBank;

		// Token: 0x04005162 RID: 20834
		public Dictionary<int, string> TrackNamesByIndex;

		// Token: 0x04005163 RID: 20835
		public Dictionary<int, IAudioTrack> DefaultTrackByIndex;

		// Token: 0x04005164 RID: 20836
		public List<IContentSource> FileSources;
	}
}
