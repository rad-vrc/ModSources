using System;

namespace Terraria.Cinematics
{
	// Token: 0x02000747 RID: 1863
	public struct FrameEventData
	{
		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06004BAA RID: 19370 RVA: 0x0066BB37 File Offset: 0x00669D37
		public int AbsoluteFrame
		{
			get
			{
				return this._absoluteFrame;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06004BAB RID: 19371 RVA: 0x0066BB3F File Offset: 0x00669D3F
		public int Start
		{
			get
			{
				return this._start;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06004BAC RID: 19372 RVA: 0x0066BB47 File Offset: 0x00669D47
		public int Duration
		{
			get
			{
				return this._duration;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06004BAD RID: 19373 RVA: 0x0066BB4F File Offset: 0x00669D4F
		public int Frame
		{
			get
			{
				return this._absoluteFrame - this._start;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06004BAE RID: 19374 RVA: 0x0066BB5E File Offset: 0x00669D5E
		public bool IsFirstFrame
		{
			get
			{
				return this._start == this._absoluteFrame;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06004BAF RID: 19375 RVA: 0x0066BB6E File Offset: 0x00669D6E
		public bool IsLastFrame
		{
			get
			{
				return this.Remaining == 0;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06004BB0 RID: 19376 RVA: 0x0066BB79 File Offset: 0x00669D79
		public int Remaining
		{
			get
			{
				return this._start + this._duration - this._absoluteFrame - 1;
			}
		}

		// Token: 0x06004BB1 RID: 19377 RVA: 0x0066BB91 File Offset: 0x00669D91
		public FrameEventData(int absoluteFrame, int start, int duration)
		{
			this._absoluteFrame = absoluteFrame;
			this._start = start;
			this._duration = duration;
		}

		// Token: 0x04006090 RID: 24720
		private int _absoluteFrame;

		// Token: 0x04006091 RID: 24721
		private int _start;

		// Token: 0x04006092 RID: 24722
		private int _duration;
	}
}
