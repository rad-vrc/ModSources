using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using log4net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Utilities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terraria.Audio
{
	// Token: 0x02000769 RID: 1897
	[NullableContext(2)]
	[Nullable(0)]
	public static class SoundEngine
	{
		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06004CA5 RID: 19621 RVA: 0x006708A3 File Offset: 0x0066EAA3
		// (set) Token: 0x06004CA6 RID: 19622 RVA: 0x006708AA File Offset: 0x0066EAAA
		public static bool IsAudioSupported { get; private set; }

		// Token: 0x06004CA7 RID: 19623 RVA: 0x006708B2 File Offset: 0x0066EAB2
		public static void Initialize()
		{
			SoundEngine.IsAudioSupported = SoundEngine.TestAudioSupport();
			if (!SoundEngine.IsAudioSupported)
			{
				Utils.ShowFancyErrorMessage(Language.GetTextValue("tModLoader.AudioNotSupported"), 10002, null);
			}
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x006708DA File Offset: 0x0066EADA
		[NullableContext(0)]
		public static void Load(IServiceProvider services)
		{
			if (SoundEngine.IsAudioSupported)
			{
				SoundEngine.LegacySoundPlayer = new LegacySoundPlayer(services);
				SoundEngine.SoundPlayer = new SoundPlayer();
			}
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x006708F8 File Offset: 0x0066EAF8
		public static void Update()
		{
			if (SoundEngine.IsAudioSupported)
			{
				if (Main.audioSystem != null)
				{
					Main.audioSystem.UpdateAudioEngine();
				}
				SoundInstanceGarbageCollector.Update();
				bool flag = (!Main.hasFocus || Main.gamePaused) && Main.netMode == 0;
				if (!SoundEngine.AreSoundsPaused && flag)
				{
					SoundEngine.SoundPlayer.PauseOrStopAll();
				}
				else if (SoundEngine.AreSoundsPaused && !flag)
				{
					SoundEngine.SoundPlayer.ResumeAll();
				}
				SoundEngine.AreSoundsPaused = flag;
				SoundEngine.SoundPlayer.Update();
			}
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x00670978 File Offset: 0x0066EB78
		public static void Reload()
		{
			if (SoundEngine.IsAudioSupported)
			{
				if (SoundEngine.LegacySoundPlayer != null)
				{
					SoundEngine.LegacySoundPlayer.Reload();
				}
				if (SoundEngine.SoundPlayer != null)
				{
					SoundEngine.SoundPlayer.Reload();
				}
			}
		}

		// Token: 0x06004CAB RID: 19627 RVA: 0x006709A3 File Offset: 0x0066EBA3
		public static void StopTrackedSounds()
		{
			if (!Main.dedServ && SoundEngine.IsAudioSupported)
			{
				SoundEngine.SoundPlayer.StopAll();
			}
		}

		// Token: 0x06004CAC RID: 19628 RVA: 0x006709BD File Offset: 0x0066EBBD
		public static void StopAmbientSounds()
		{
			if (!Main.dedServ && SoundEngine.IsAudioSupported && SoundEngine.LegacySoundPlayer != null)
			{
				SoundEngine.LegacySoundPlayer.StopAmbientSounds();
			}
		}

		// Token: 0x06004CAD RID: 19629 RVA: 0x006709DE File Offset: 0x0066EBDE
		[NullableContext(0)]
		public static ActiveSound FindActiveSound(in SoundStyle style)
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				return null;
			}
			return SoundEngine.SoundPlayer.FindActiveSound(style);
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x006709FC File Offset: 0x0066EBFC
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
				using (MemoryStream stream = new MemoryStream(buffer))
				{
					SoundEffect.FromStream(stream);
				}
			}
			catch (NoAudioHardwareException)
			{
				Logging.tML.Warn("No audio hardware found. Disabling all audio.");
				return false;
			}
			catch
			{
				return false;
			}
			return true;
		}

		/// <inheritdoc cref="M:Terraria.Audio.SoundEngine.PlaySound(Terraria.Audio.SoundStyle@,System.Nullable{Microsoft.Xna.Framework.Vector2},Terraria.Audio.SoundUpdateCallback)" />
		/// <summary>
		/// Attempts to play a sound style with the provided sound style (if it's not null), and returns a valid <see cref="T:ReLogic.Utilities.SlotId" /> handle to it on success.
		/// </summary>
		// Token: 0x06004CAF RID: 19631 RVA: 0x00670A80 File Offset: 0x0066EC80
		public static SlotId PlaySound(in SoundStyle? style, Vector2? position = null, SoundUpdateCallback updateCallback = null)
		{
			if (style == null)
			{
				return SlotId.Invalid;
			}
			SoundStyle value = style.Value;
			return SoundEngine.PlaySound(value, position, updateCallback);
		}

		/// <summary>
		/// Attempts to play a sound with the provided sound style, and returns a valid <see cref="T:ReLogic.Utilities.SlotId" /> handle to it on success.
		/// </summary>
		/// <param name="style"> The sound style that describes everything about the played sound. </param>
		/// <param name="position"> An optional 2D position to play the sound at. When null, this sound will be heard everywhere. </param>
		/// <param name="updateCallback"> A callback for customizing the behavior of the created sound instance, like tying its existence to a projectile using <see cref="T:Terraria.Audio.ProjectileAudioTracker" />. </param>
		// Token: 0x06004CB0 RID: 19632 RVA: 0x00670AAB File Offset: 0x0066ECAB
		public static SlotId PlaySound(in SoundStyle style, Vector2? position = null, SoundUpdateCallback updateCallback = null)
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				return SlotId.Invalid;
			}
			return SoundEngine.SoundPlayer.Play(style, position, updateCallback);
		}

		/// <inheritdoc cref="M:Terraria.Audio.SoundPlayer.TryGetActiveSound(ReLogic.Utilities.SlotId,Terraria.Audio.ActiveSound@)" />
		// Token: 0x06004CB1 RID: 19633 RVA: 0x00670ACE File Offset: 0x0066ECCE
		public static bool TryGetActiveSound(SlotId slotId, [NotNullWhen(true)] out ActiveSound result)
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				result = null;
				return false;
			}
			return SoundEngine.SoundPlayer.TryGetActiveSound(slotId, out result);
		}

		// Token: 0x06004CB2 RID: 19634 RVA: 0x00670AF0 File Offset: 0x0066ECF0
		internal static SoundEffectInstance PlaySound(SoundStyle? style, Vector2? position = null)
		{
			SlotId slotId = SoundEngine.PlaySound(style, position, null);
			if (!slotId.IsValid)
			{
				return null;
			}
			ActiveSound activeSound = SoundEngine.GetActiveSound(slotId);
			if (activeSound == null)
			{
				return null;
			}
			return activeSound.Sound;
		}

		// Token: 0x06004CB3 RID: 19635 RVA: 0x00670B23 File Offset: 0x0066ED23
		internal static SoundEffectInstance PlaySound(SoundStyle? type, int x, int y)
		{
			return SoundEngine.PlaySound(type, SoundEngine.XYToOptionalPosition(x, y));
		}

		// Token: 0x06004CB4 RID: 19636 RVA: 0x00670B32 File Offset: 0x0066ED32
		internal static void PlaySound(int type, Vector2 position, int style = 1)
		{
			SoundEngine.PlaySound(type, (int)position.X, (int)position.Y, style, 1f, 0f);
		}

		// Token: 0x06004CB5 RID: 19637 RVA: 0x00670B54 File Offset: 0x0066ED54
		internal static SoundEffectInstance PlaySound(int type, int x = -1, int y = -1, int Style = 1, float volumeScale = 1f, float pitchOffset = 0f)
		{
			SoundStyle soundStyle;
			if (!SoundID.TryGetLegacyStyle(type, Style, out soundStyle))
			{
				ILog tML = Logging.tML;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(48, 2);
				defaultInterpolatedStringHandler.AppendLiteral("Failed to get legacy sound style for (");
				defaultInterpolatedStringHandler.AppendFormatted<int>(type);
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(Style);
				defaultInterpolatedStringHandler.AppendLiteral(") input.");
				tML.Warn(defaultInterpolatedStringHandler.ToStringAndClear());
				return null;
			}
			SoundStyle soundStyle2 = soundStyle;
			soundStyle2.Volume = soundStyle.Volume * volumeScale;
			soundStyle2.Pitch = soundStyle.Pitch + pitchOffset;
			soundStyle = soundStyle2;
			SlotId slotId = SoundEngine.PlaySound(soundStyle, SoundEngine.XYToOptionalPosition(x, y), null);
			if (!slotId.IsValid)
			{
				return null;
			}
			ActiveSound activeSound = SoundEngine.GetActiveSound(slotId);
			if (activeSound == null)
			{
				return null;
			}
			return activeSound.Sound;
		}

		// Token: 0x06004CB6 RID: 19638 RVA: 0x00670C10 File Offset: 0x0066EE10
		internal static SlotId PlayTrackedSound(in SoundStyle style, Vector2? position = null)
		{
			SoundStyle soundStyle = style;
			soundStyle.PauseBehavior = PauseBehavior.StopWhenGamePaused;
			return SoundEngine.PlaySound(soundStyle, position, null);
		}

		// Token: 0x06004CB7 RID: 19639 RVA: 0x00670C38 File Offset: 0x0066EE38
		internal static SlotId PlayTrackedLoopedSound(in SoundStyle style, Vector2 position, Func<bool> loopingCondition = null)
		{
			SoundStyle soundStyle = style;
			soundStyle.PauseBehavior = PauseBehavior.StopWhenGamePaused;
			return SoundEngine.PlaySound(soundStyle, new Vector2?(position), (ActiveSound _) => loopingCondition());
		}

		// Token: 0x06004CB8 RID: 19640 RVA: 0x00670C7C File Offset: 0x0066EE7C
		internal static ActiveSound GetActiveSound(SlotId slotId)
		{
			ActiveSound result;
			if (!SoundEngine.TryGetActiveSound(slotId, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06004CB9 RID: 19641 RVA: 0x00670C98 File Offset: 0x0066EE98
		private static Vector2? XYToOptionalPosition(int x, int y)
		{
			if (x == -1 && y == -1)
			{
				return null;
			}
			return new Vector2?(new Vector2((float)x, (float)y));
		}

		// Token: 0x04006116 RID: 24854
		[Nullable(0)]
		internal static LegacySoundPlayer LegacySoundPlayer;

		// Token: 0x04006117 RID: 24855
		[Nullable(0)]
		public static SoundPlayer SoundPlayer;

		// Token: 0x04006118 RID: 24856
		public static bool AreSoundsPaused;
	}
}
