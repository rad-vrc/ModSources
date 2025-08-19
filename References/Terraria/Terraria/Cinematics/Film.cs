using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.Cinematics
{
	// Token: 0x02000465 RID: 1125
	public class Film
	{
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06002D1E RID: 11550 RVA: 0x005BDBCA File Offset: 0x005BBDCA
		public int Frame
		{
			get
			{
				return this._frame;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06002D1F RID: 11551 RVA: 0x005BDBD2 File Offset: 0x005BBDD2
		public int FrameCount
		{
			get
			{
				return this._frameCount;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06002D20 RID: 11552 RVA: 0x005BDBDA File Offset: 0x005BBDDA
		public int AppendPoint
		{
			get
			{
				return this._nextSequenceAppendTime;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06002D21 RID: 11553 RVA: 0x005BDBE2 File Offset: 0x005BBDE2
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x005BDBEA File Offset: 0x005BBDEA
		public void AddSequence(int start, int duration, FrameEvent frameEvent)
		{
			this._sequences.Add(new Film.Sequence(frameEvent, start, duration));
			this._nextSequenceAppendTime = Math.Max(this._nextSequenceAppendTime, start + duration);
			this._frameCount = Math.Max(this._frameCount, start + duration);
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x005BDC27 File Offset: 0x005BBE27
		public void AppendSequence(int duration, FrameEvent frameEvent)
		{
			this.AddSequence(this._nextSequenceAppendTime, duration, frameEvent);
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x005BDC38 File Offset: 0x005BBE38
		public void AddSequences(int start, int duration, params FrameEvent[] frameEvents)
		{
			foreach (FrameEvent frameEvent in frameEvents)
			{
				this.AddSequence(start, duration, frameEvent);
			}
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x005BDC64 File Offset: 0x005BBE64
		public void AppendSequences(int duration, params FrameEvent[] frameEvents)
		{
			int nextSequenceAppendTime = this._nextSequenceAppendTime;
			foreach (FrameEvent frameEvent in frameEvents)
			{
				this._sequences.Add(new Film.Sequence(frameEvent, nextSequenceAppendTime, duration));
				this._nextSequenceAppendTime = Math.Max(this._nextSequenceAppendTime, nextSequenceAppendTime + duration);
				this._frameCount = Math.Max(this._frameCount, nextSequenceAppendTime + duration);
			}
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x005BDCC7 File Offset: 0x005BBEC7
		public void AppendEmptySequence(int duration)
		{
			this.AddSequence(this._nextSequenceAppendTime, duration, new FrameEvent(Film.EmptyFrameEvent));
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x005BDCE2 File Offset: 0x005BBEE2
		public void AppendKeyFrame(FrameEvent frameEvent)
		{
			this.AddKeyFrame(this._nextSequenceAppendTime, frameEvent);
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x005BDCF4 File Offset: 0x005BBEF4
		public void AppendKeyFrames(params FrameEvent[] frameEvents)
		{
			int nextSequenceAppendTime = this._nextSequenceAppendTime;
			foreach (FrameEvent frameEvent in frameEvents)
			{
				this._sequences.Add(new Film.Sequence(frameEvent, nextSequenceAppendTime, 1));
			}
			this._frameCount = Math.Max(this._frameCount, nextSequenceAppendTime + 1);
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x005BDD43 File Offset: 0x005BBF43
		public void AddKeyFrame(int frame, FrameEvent frameEvent)
		{
			this._sequences.Add(new Film.Sequence(frameEvent, frame, 1));
			this._frameCount = Math.Max(this._frameCount, frame + 1);
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x005BDD6C File Offset: 0x005BBF6C
		public void AddKeyFrames(int frame, params FrameEvent[] frameEvents)
		{
			foreach (FrameEvent frameEvent in frameEvents)
			{
				this.AddKeyFrame(frame, frameEvent);
			}
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x005BDD98 File Offset: 0x005BBF98
		public bool OnUpdate(GameTime gameTime)
		{
			if (this._sequences.Count == 0)
			{
				return false;
			}
			foreach (Film.Sequence sequence in this._sequences)
			{
				int num = this._frame - sequence.Start;
				if (num >= 0 && num < sequence.Duration)
				{
					sequence.Event(new FrameEventData(this._frame, sequence.Start, sequence.Duration));
				}
			}
			int num2 = this._frame + 1;
			this._frame = num2;
			return num2 != this._frameCount;
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x005BDE4C File Offset: 0x005BC04C
		public virtual void OnBegin()
		{
			this._isActive = true;
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x005BDE55 File Offset: 0x005BC055
		public virtual void OnEnd()
		{
			this._isActive = false;
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		private static void EmptyFrameEvent(FrameEventData evt)
		{
		}

		// Token: 0x0400512D RID: 20781
		private int _frame;

		// Token: 0x0400512E RID: 20782
		private int _frameCount;

		// Token: 0x0400512F RID: 20783
		private int _nextSequenceAppendTime;

		// Token: 0x04005130 RID: 20784
		private bool _isActive;

		// Token: 0x04005131 RID: 20785
		private List<Film.Sequence> _sequences = new List<Film.Sequence>();

		// Token: 0x0200077E RID: 1918
		private class Sequence
		{
			// Token: 0x17000414 RID: 1044
			// (get) Token: 0x0600391A RID: 14618 RVA: 0x006153D1 File Offset: 0x006135D1
			public FrameEvent Event
			{
				get
				{
					return this._frameEvent;
				}
			}

			// Token: 0x17000415 RID: 1045
			// (get) Token: 0x0600391B RID: 14619 RVA: 0x006153D9 File Offset: 0x006135D9
			public int Duration
			{
				get
				{
					return this._duration;
				}
			}

			// Token: 0x17000416 RID: 1046
			// (get) Token: 0x0600391C RID: 14620 RVA: 0x006153E1 File Offset: 0x006135E1
			public int Start
			{
				get
				{
					return this._start;
				}
			}

			// Token: 0x0600391D RID: 14621 RVA: 0x006153E9 File Offset: 0x006135E9
			public Sequence(FrameEvent frameEvent, int start, int duration)
			{
				this._frameEvent = frameEvent;
				this._start = start;
				this._duration = duration;
			}

			// Token: 0x040064A1 RID: 25761
			private FrameEvent _frameEvent;

			// Token: 0x040064A2 RID: 25762
			private int _duration;

			// Token: 0x040064A3 RID: 25763
			private int _start;
		}
	}
}
