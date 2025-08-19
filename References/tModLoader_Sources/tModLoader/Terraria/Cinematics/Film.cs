using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Terraria.Cinematics
{
	// Token: 0x02000745 RID: 1861
	public class Film
	{
		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06004B94 RID: 19348 RVA: 0x0066B881 File Offset: 0x00669A81
		public int Frame
		{
			get
			{
				return this._frame;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06004B95 RID: 19349 RVA: 0x0066B889 File Offset: 0x00669A89
		public int FrameCount
		{
			get
			{
				return this._frameCount;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06004B96 RID: 19350 RVA: 0x0066B891 File Offset: 0x00669A91
		public int AppendPoint
		{
			get
			{
				return this._nextSequenceAppendTime;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06004B97 RID: 19351 RVA: 0x0066B899 File Offset: 0x00669A99
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x0066B8A1 File Offset: 0x00669AA1
		public void AddSequence(int start, int duration, FrameEvent frameEvent)
		{
			this._sequences.Add(new Film.Sequence(frameEvent, start, duration));
			this._nextSequenceAppendTime = Math.Max(this._nextSequenceAppendTime, start + duration);
			this._frameCount = Math.Max(this._frameCount, start + duration);
		}

		// Token: 0x06004B99 RID: 19353 RVA: 0x0066B8DE File Offset: 0x00669ADE
		public void AppendSequence(int duration, FrameEvent frameEvent)
		{
			this.AddSequence(this._nextSequenceAppendTime, duration, frameEvent);
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x0066B8F0 File Offset: 0x00669AF0
		public void AddSequences(int start, int duration, params FrameEvent[] frameEvents)
		{
			foreach (FrameEvent frameEvent in frameEvents)
			{
				this.AddSequence(start, duration, frameEvent);
			}
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x0066B91C File Offset: 0x00669B1C
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

		// Token: 0x06004B9C RID: 19356 RVA: 0x0066B97F File Offset: 0x00669B7F
		public void AppendEmptySequence(int duration)
		{
			int nextSequenceAppendTime = this._nextSequenceAppendTime;
			FrameEvent frameEvent;
			if ((frameEvent = Film.<>O.<0>__EmptyFrameEvent) == null)
			{
				frameEvent = (Film.<>O.<0>__EmptyFrameEvent = new FrameEvent(Film.EmptyFrameEvent));
			}
			this.AddSequence(nextSequenceAppendTime, duration, frameEvent);
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x0066B9A9 File Offset: 0x00669BA9
		public void AppendKeyFrame(FrameEvent frameEvent)
		{
			this.AddKeyFrame(this._nextSequenceAppendTime, frameEvent);
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x0066B9B8 File Offset: 0x00669BB8
		public void AppendKeyFrames(params FrameEvent[] frameEvents)
		{
			int nextSequenceAppendTime = this._nextSequenceAppendTime;
			foreach (FrameEvent frameEvent in frameEvents)
			{
				this._sequences.Add(new Film.Sequence(frameEvent, nextSequenceAppendTime, 1));
			}
			this._frameCount = Math.Max(this._frameCount, nextSequenceAppendTime + 1);
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x0066BA07 File Offset: 0x00669C07
		public void AddKeyFrame(int frame, FrameEvent frameEvent)
		{
			this._sequences.Add(new Film.Sequence(frameEvent, frame, 1));
			this._frameCount = Math.Max(this._frameCount, frame + 1);
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x0066BA30 File Offset: 0x00669C30
		public void AddKeyFrames(int frame, params FrameEvent[] frameEvents)
		{
			foreach (FrameEvent frameEvent in frameEvents)
			{
				this.AddKeyFrame(frame, frameEvent);
			}
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x0066BA5C File Offset: 0x00669C5C
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

		// Token: 0x06004BA2 RID: 19362 RVA: 0x0066BB10 File Offset: 0x00669D10
		public virtual void OnBegin()
		{
			this._isActive = true;
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x0066BB19 File Offset: 0x00669D19
		public virtual void OnEnd()
		{
			this._isActive = false;
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x0066BB22 File Offset: 0x00669D22
		private static void EmptyFrameEvent(FrameEventData evt)
		{
		}

		// Token: 0x0400608B RID: 24715
		private int _frame;

		// Token: 0x0400608C RID: 24716
		private int _frameCount;

		// Token: 0x0400608D RID: 24717
		private int _nextSequenceAppendTime;

		// Token: 0x0400608E RID: 24718
		private bool _isActive;

		// Token: 0x0400608F RID: 24719
		private List<Film.Sequence> _sequences = new List<Film.Sequence>();

		// Token: 0x02000D67 RID: 3431
		private class Sequence
		{
			// Token: 0x170009A0 RID: 2464
			// (get) Token: 0x0600640A RID: 25610 RVA: 0x006D9C9D File Offset: 0x006D7E9D
			public FrameEvent Event
			{
				get
				{
					return this._frameEvent;
				}
			}

			// Token: 0x170009A1 RID: 2465
			// (get) Token: 0x0600640B RID: 25611 RVA: 0x006D9CA5 File Offset: 0x006D7EA5
			public int Duration
			{
				get
				{
					return this._duration;
				}
			}

			// Token: 0x170009A2 RID: 2466
			// (get) Token: 0x0600640C RID: 25612 RVA: 0x006D9CAD File Offset: 0x006D7EAD
			public int Start
			{
				get
				{
					return this._start;
				}
			}

			// Token: 0x0600640D RID: 25613 RVA: 0x006D9CB5 File Offset: 0x006D7EB5
			public Sequence(FrameEvent frameEvent, int start, int duration)
			{
				this._frameEvent = frameEvent;
				this._start = start;
				this._duration = duration;
			}

			// Token: 0x04007B96 RID: 31638
			private FrameEvent _frameEvent;

			// Token: 0x04007B97 RID: 31639
			private int _duration;

			// Token: 0x04007B98 RID: 31640
			private int _start;
		}

		// Token: 0x02000D68 RID: 3432
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007B99 RID: 31641
			public static FrameEvent <0>__EmptyFrameEvent;
		}
	}
}
