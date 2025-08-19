using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;

namespace Terraria.Audio
{
	// Token: 0x02000488 RID: 1160
	public class SoundPlayer
	{
		// Token: 0x06002E2F RID: 11823 RVA: 0x005C34E0 File Offset: 0x005C16E0
		public SlotId Play(SoundStyle style, Vector2 position)
		{
			if (Main.dedServ || style == null || !style.IsTrackable)
			{
				return SlotId.Invalid;
			}
			if (Vector2.DistanceSquared(Main.screenPosition + new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2)), position) > 100000000f)
			{
				return SlotId.Invalid;
			}
			ActiveSound activeSound = new ActiveSound(style, position);
			return this._trackedSounds.Add(activeSound);
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x005C354C File Offset: 0x005C174C
		public SlotId PlayLooped(SoundStyle style, Vector2 position, ActiveSound.LoopedPlayCondition loopingCondition)
		{
			if (Main.dedServ || style == null || !style.IsTrackable)
			{
				return SlotId.Invalid;
			}
			if (Vector2.DistanceSquared(Main.screenPosition + new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2)), position) > 100000000f)
			{
				return SlotId.Invalid;
			}
			ActiveSound activeSound = new ActiveSound(style, position, loopingCondition);
			return this._trackedSounds.Add(activeSound);
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x005C35B8 File Offset: 0x005C17B8
		public void Reload()
		{
			this.StopAll();
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x005C35C0 File Offset: 0x005C17C0
		public SlotId Play(SoundStyle style)
		{
			if (Main.dedServ || style == null || !style.IsTrackable)
			{
				return SlotId.Invalid;
			}
			ActiveSound activeSound = new ActiveSound(style);
			return this._trackedSounds.Add(activeSound);
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x005C35F8 File Offset: 0x005C17F8
		public ActiveSound GetActiveSound(SlotId id)
		{
			if (!this._trackedSounds.Has(id))
			{
				return null;
			}
			return this._trackedSounds[id];
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x005C3618 File Offset: 0x005C1818
		public void PauseAll()
		{
			foreach (SlotVector<ActiveSound>.ItemPair itemPair in this._trackedSounds)
			{
				itemPair.Value.Pause();
			}
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x005C3668 File Offset: 0x005C1868
		public void ResumeAll()
		{
			foreach (SlotVector<ActiveSound>.ItemPair itemPair in this._trackedSounds)
			{
				itemPair.Value.Resume();
			}
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x005C36B8 File Offset: 0x005C18B8
		public void StopAll()
		{
			foreach (SlotVector<ActiveSound>.ItemPair itemPair in this._trackedSounds)
			{
				itemPair.Value.Stop();
			}
			this._trackedSounds.Clear();
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x005C3714 File Offset: 0x005C1914
		public void Update()
		{
			foreach (SlotVector<ActiveSound>.ItemPair itemPair in this._trackedSounds)
			{
				try
				{
					itemPair.Value.Update();
					if (!itemPair.Value.IsPlaying)
					{
						this._trackedSounds.Remove(itemPair.Id);
					}
				}
				catch
				{
					this._trackedSounds.Remove(itemPair.Id);
				}
			}
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x005C37A8 File Offset: 0x005C19A8
		public ActiveSound FindActiveSound(SoundStyle style)
		{
			foreach (SlotVector<ActiveSound>.ItemPair itemPair in this._trackedSounds)
			{
				if (itemPair.Value.Style == style)
				{
					return itemPair.Value;
				}
			}
			return null;
		}

		// Token: 0x040051BC RID: 20924
		private readonly SlotVector<ActiveSound> _trackedSounds = new SlotVector<ActiveSound>(4096);
	}
}
