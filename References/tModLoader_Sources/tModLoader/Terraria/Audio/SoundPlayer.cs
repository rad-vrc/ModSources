using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Utilities;

namespace Terraria.Audio
{
	// Token: 0x0200076B RID: 1899
	[NullableContext(2)]
	[Nullable(0)]
	public class SoundPlayer
	{
		// Token: 0x06004CBD RID: 19645 RVA: 0x00670D60 File Offset: 0x0066EF60
		public SlotId Play(in SoundStyle style, Vector2? position = null, SoundUpdateCallback updateCallback = null)
		{
			if (Main.dedServ)
			{
				return SlotId.Invalid;
			}
			if (position != null && Vector2.DistanceSquared(Main.screenPosition + new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2)), position.Value) > 100000000f)
			{
				return SlotId.Invalid;
			}
			if (style.PlayOnlyIfFocused && !Main.hasFocus)
			{
				return SlotId.Invalid;
			}
			if (!Program.IsMainThread)
			{
				SoundStyle styleCopy = style;
				return Main.RunOnMainThread<SlotId>(() => this.Play_Inner(styleCopy, position, updateCallback)).GetAwaiter().GetResult();
			}
			return this.Play_Inner(style, position, updateCallback);
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x00670E38 File Offset: 0x0066F038
		private SlotId Play_Inner(in SoundStyle style, Vector2? position, SoundUpdateCallback updateCallback)
		{
			SoundStyle soundStyle = style;
			SoundStyle chosenStyle = soundStyle.WithSelectedVariant(null);
			int maxInstances = chosenStyle.MaxInstances;
			if (maxInstances > 0)
			{
				int attempts = chosenStyle.LimitsArePerVariant ? (chosenStyle.RerollAttempts + 1) : 1;
				int attempt = 0;
				while (attempt < attempts)
				{
					bool tryAgain = false;
					int instanceCount = 0;
					foreach (SlotVector<ActiveSound>.ItemPair itemPair in this._trackedSounds)
					{
						ActiveSound activeSound = itemPair.Value;
						if (activeSound.IsPlaying && chosenStyle.IsTheSameAs(activeSound.Style) && ++instanceCount >= maxInstances)
						{
							if (attempt + 1 < attempts)
							{
								tryAgain = true;
								soundStyle = style;
								chosenStyle = soundStyle.WithSelectedVariant(null);
								break;
							}
							if (chosenStyle.SoundLimitBehavior != SoundLimitBehavior.ReplaceOldest)
							{
								return SlotId.Invalid;
							}
							SoundEffectInstance sound = activeSound.Sound;
							if (sound != null)
							{
								sound.Stop(true);
							}
						}
					}
					if (tryAgain)
					{
						attempt++;
						continue;
					}
					break;
				}
			}
			if (chosenStyle.UsesMusicPitch)
			{
				chosenStyle.Pitch += Main.musicPitch;
			}
			ActiveSound value = new ActiveSound(chosenStyle, position, updateCallback);
			return this._trackedSounds.Add(value);
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x00670F94 File Offset: 0x0066F194
		public void Reload()
		{
			this.StopAll();
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x00670F9C File Offset: 0x0066F19C
		internal ActiveSound GetActiveSound(SlotId id)
		{
			ActiveSound result;
			if (!this.TryGetActiveSound(id, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x00670FB8 File Offset: 0x0066F1B8
		public void PauseAll()
		{
			foreach (SlotVector<ActiveSound>.ItemPair itemPair in this._trackedSounds)
			{
				itemPair.Value.Pause();
			}
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x00671008 File Offset: 0x0066F208
		public void PauseOrStopAll()
		{
			foreach (SlotVector<ActiveSound>.ItemPair item in this._trackedSounds)
			{
				switch (item.Value.Style.PauseBehavior)
				{
				case PauseBehavior.PauseWithGame:
					item.Value.Pause();
					break;
				case PauseBehavior.StopWhenGamePaused:
					item.Value.Stop();
					break;
				}
			}
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x00671090 File Offset: 0x0066F290
		public void ResumeAll()
		{
			foreach (SlotVector<ActiveSound>.ItemPair itemPair in this._trackedSounds)
			{
				itemPair.Value.Resume();
			}
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x006710E0 File Offset: 0x0066F2E0
		public void StopAll()
		{
			foreach (SlotVector<ActiveSound>.ItemPair itemPair in this._trackedSounds)
			{
				itemPair.Value.Stop();
			}
			this._trackedSounds.Clear();
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x0067113C File Offset: 0x0066F33C
		public void Update()
		{
			foreach (SlotVector<ActiveSound>.ItemPair item in this._trackedSounds)
			{
				try
				{
					item.Value.Update();
					if (!item.Value.IsPlayingOrPaused)
					{
						this._trackedSounds.Remove(item.Id);
					}
				}
				catch
				{
					this._trackedSounds.Remove(item.Id);
				}
			}
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x006711D0 File Offset: 0x0066F3D0
		public ActiveSound FindActiveSound(in SoundStyle style)
		{
			foreach (SlotVector<ActiveSound>.ItemPair item in this._trackedSounds)
			{
				if (item.Value.Style == style)
				{
					return item.Value;
				}
			}
			return null;
		}

		/// <summary>
		/// Safely attempts to get a currently playing <see cref="T:Terraria.Audio.ActiveSound" /> instance, tied to the provided <see cref="T:ReLogic.Utilities.SlotId" />.
		/// </summary>
		// Token: 0x06004CC7 RID: 19655 RVA: 0x0067123C File Offset: 0x0066F43C
		public bool TryGetActiveSound(SlotId id, [NotNullWhen(true)] out ActiveSound result)
		{
			return this._trackedSounds.TryGetValue(id, ref result);
		}

		/// <summary>
		/// Stops all sounds matching the provided <see cref="T:Terraria.Audio.SoundStyle" />. Use <see cref="M:Terraria.Audio.SoundPlayer.StopAll(Terraria.Audio.SoundStyle@,System.Int32)" /> instead if stopping just a specific variant is desired.
		/// </summary>
		// Token: 0x06004CC8 RID: 19656 RVA: 0x0067124C File Offset: 0x0066F44C
		public void StopAll(in SoundStyle style)
		{
			List<SlotVector<ActiveSound>.ItemPair> stopped = new List<SlotVector<ActiveSound>.ItemPair>();
			foreach (SlotVector<ActiveSound>.ItemPair item in this._trackedSounds)
			{
				SoundStyle soundStyle = style;
				if (soundStyle.IsVariantOf(item.Value.Style))
				{
					item.Value.Stop();
					stopped.Add(item);
				}
			}
			foreach (SlotVector<ActiveSound>.ItemPair item2 in stopped)
			{
				this._trackedSounds.Remove(item2.Id);
			}
		}

		/// <summary>
		/// Stops all sounds matching the provided <see cref="T:Terraria.Audio.SoundStyle" /> and variant choice. Use <see cref="M:Terraria.Audio.SoundPlayer.StopAll(Terraria.Audio.SoundStyle@)" /> instead if stopping all variants is desired.
		/// </summary>
		// Token: 0x06004CC9 RID: 19657 RVA: 0x00671310 File Offset: 0x0066F510
		public void StopAll(in SoundStyle style, int variant)
		{
			SoundStyle soundStyle = style;
			soundStyle.SelectedVariant = new int?(variant);
			SoundStyle checkStyle = soundStyle;
			List<SlotVector<ActiveSound>.ItemPair> stopped = new List<SlotVector<ActiveSound>.ItemPair>();
			foreach (SlotVector<ActiveSound>.ItemPair item in this._trackedSounds)
			{
				if (checkStyle.IsTheSameAs(item.Value.Style))
				{
					item.Value.Stop();
					stopped.Add(item);
				}
			}
			foreach (SlotVector<ActiveSound>.ItemPair item2 in stopped)
			{
				this._trackedSounds.Remove(item2.Id);
			}
		}

		// Token: 0x0400611B RID: 24859
		[Nullable(1)]
		private readonly SlotVector<ActiveSound> _trackedSounds = new SlotVector<ActiveSound>(4096);
	}
}
