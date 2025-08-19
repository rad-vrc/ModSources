using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Utilities;

namespace Terraria.Audio
{
	// Token: 0x0200048C RID: 1164
	public static class SoundEngine
	{
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06002E48 RID: 11848 RVA: 0x005C3A0B File Offset: 0x005C1C0B
		// (set) Token: 0x06002E49 RID: 11849 RVA: 0x005C3A12 File Offset: 0x005C1C12
		public static bool IsAudioSupported { get; private set; }

		// Token: 0x06002E4A RID: 11850 RVA: 0x005C3A1A File Offset: 0x005C1C1A
		public static void Initialize()
		{
			SoundEngine.IsAudioSupported = SoundEngine.TestAudioSupport();
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x005C3A26 File Offset: 0x005C1C26
		public static void Load(IServiceProvider services)
		{
			if (!SoundEngine.IsAudioSupported)
			{
				return;
			}
			SoundEngine.LegacySoundPlayer = new LegacySoundPlayer(services);
			SoundEngine.SoundPlayer = new SoundPlayer();
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x005C3A48 File Offset: 0x005C1C48
		public static void Update()
		{
			if (!SoundEngine.IsAudioSupported)
			{
				return;
			}
			if (Main.audioSystem != null)
			{
				Main.audioSystem.UpdateAudioEngine();
			}
			SoundInstanceGarbageCollector.Update();
			bool flag = (!Main.hasFocus || Main.gamePaused) && Main.netMode == 0;
			if (!SoundEngine.AreSoundsPaused && flag)
			{
				SoundEngine.SoundPlayer.PauseAll();
			}
			else if (SoundEngine.AreSoundsPaused && !flag)
			{
				SoundEngine.SoundPlayer.ResumeAll();
			}
			SoundEngine.AreSoundsPaused = flag;
			SoundEngine.SoundPlayer.Update();
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x005C3AC9 File Offset: 0x005C1CC9
		public static void Reload()
		{
			if (!SoundEngine.IsAudioSupported)
			{
				return;
			}
			if (SoundEngine.LegacySoundPlayer != null)
			{
				SoundEngine.LegacySoundPlayer.Reload();
			}
			if (SoundEngine.SoundPlayer != null)
			{
				SoundEngine.SoundPlayer.Reload();
			}
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x005C3AF5 File Offset: 0x005C1CF5
		public static void PlaySound(int type, Vector2 position, int style = 1)
		{
			SoundEngine.PlaySound(type, (int)position.X, (int)position.Y, style, 1f, 0f);
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x005C3B17 File Offset: 0x005C1D17
		public static SoundEffectInstance PlaySound(LegacySoundStyle type, Vector2 position)
		{
			return SoundEngine.PlaySound(type, (int)position.X, (int)position.Y);
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x005C3B2D File Offset: 0x005C1D2D
		public static SoundEffectInstance PlaySound(LegacySoundStyle type, int x = -1, int y = -1)
		{
			if (type == null)
			{
				return null;
			}
			return SoundEngine.PlaySound(type.SoundId, x, y, type.Style, type.Volume, type.GetRandomPitch());
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x005C3B53 File Offset: 0x005C1D53
		public static SoundEffectInstance PlaySound(int type, int x = -1, int y = -1, int Style = 1, float volumeScale = 1f, float pitchOffset = 0f)
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				return null;
			}
			return SoundEngine.LegacySoundPlayer.PlaySound(type, x, y, Style, volumeScale, pitchOffset);
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x005C3B77 File Offset: 0x005C1D77
		public static ActiveSound GetActiveSound(SlotId id)
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				return null;
			}
			return SoundEngine.SoundPlayer.GetActiveSound(id);
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x005C3B94 File Offset: 0x005C1D94
		public static SlotId PlayTrackedSound(SoundStyle style, Vector2 position)
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				return SlotId.Invalid;
			}
			return SoundEngine.SoundPlayer.Play(style, position);
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x005C3BB6 File Offset: 0x005C1DB6
		public static SlotId PlayTrackedLoopedSound(SoundStyle style, Vector2 position, ActiveSound.LoopedPlayCondition loopingCondition = null)
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				return SlotId.Invalid;
			}
			return SoundEngine.SoundPlayer.PlayLooped(style, position, loopingCondition);
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x005C3BD9 File Offset: 0x005C1DD9
		public static SlotId PlayTrackedSound(SoundStyle style)
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				return SlotId.Invalid;
			}
			return SoundEngine.SoundPlayer.Play(style);
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x005C3BFA File Offset: 0x005C1DFA
		public static void StopTrackedSounds()
		{
			if (!Main.dedServ && SoundEngine.IsAudioSupported)
			{
				SoundEngine.SoundPlayer.StopAll();
			}
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x005C3C14 File Offset: 0x005C1E14
		public static SoundEffect GetTrackableSoundByStyleId(int id)
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				return null;
			}
			return SoundEngine.LegacySoundPlayer.GetTrackableSoundByStyleId(id);
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x005C3C31 File Offset: 0x005C1E31
		public static void StopAmbientSounds()
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				return;
			}
			if (SoundEngine.LegacySoundPlayer != null)
			{
				SoundEngine.LegacySoundPlayer.StopAmbientSounds();
			}
		}

		// Token: 0x06002E59 RID: 11865 RVA: 0x005C3C53 File Offset: 0x005C1E53
		public static ActiveSound FindActiveSound(SoundStyle style)
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				return null;
			}
			return SoundEngine.SoundPlayer.FindActiveSound(style);
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x005C3C70 File Offset: 0x005C1E70
		private static bool TestAudioSupport()
		{
			byte[] buffer = new byte[]
			{
				82,
				73,
				70,
				70,
				158,
				0,
				0,
				0,
				87,
				65,
				86,
				69,
				102,
				109,
				116,
				32,
				16,
				0,
				0,
				0,
				1,
				0,
				1,
				0,
				68,
				172,
				0,
				0,
				136,
				88,
				1,
				0,
				2,
				0,
				16,
				0,
				76,
				73,
				83,
				84,
				26,
				0,
				0,
				0,
				73,
				78,
				70,
				79,
				73,
				83,
				70,
				84,
				14,
				0,
				0,
				0,
				76,
				97,
				118,
				102,
				53,
				54,
				46,
				52,
				48,
				46,
				49,
				48,
				49,
				0,
				100,
				97,
				116,
				97,
				88,
				0,
				0,
				0,
				0,
				0,
				126,
				4,
				240,
				8,
				64,
				13,
				95,
				17,
				67,
				21,
				217,
				24,
				23,
				28,
				240,
				30,
				94,
				33,
				84,
				35,
				208,
				36,
				204,
				37,
				71,
				38,
				64,
				38,
				183,
				37,
				180,
				36,
				58,
				35,
				79,
				33,
				1,
				31,
				86,
				28,
				92,
				25,
				37,
				22,
				185,
				18,
				42,
				15,
				134,
				11,
				222,
				7,
				68,
				4,
				196,
				0,
				112,
				253,
				86,
				250,
				132,
				247,
				6,
				245,
				230,
				242,
				47,
				241,
				232,
				239,
				25,
				239,
				194,
				238,
				231,
				238,
				139,
				239,
				169,
				240,
				61,
				242,
				67,
				244,
				180,
				246
			};
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(buffer))
				{
					SoundEffect.FromStream(memoryStream);
				}
			}
			catch (NoAudioHardwareException)
			{
				Console.WriteLine("No audio hardware found. Disabling all audio.");
				return false;
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x040051C9 RID: 20937
		public static LegacySoundPlayer LegacySoundPlayer;

		// Token: 0x040051CA RID: 20938
		public static SoundPlayer SoundPlayer;

		// Token: 0x040051CB RID: 20939
		public static bool AreSoundsPaused;
	}
}
