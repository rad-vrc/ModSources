using System;

namespace Terraria.Cinematics
{
	// Token: 0x02000463 RID: 1123
	public struct FrameEventData
	{
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06002D12 RID: 11538 RVA: 0x005BDB59 File Offset: 0x005BBD59
		public int AbsoluteFrame
		{
			get
			{
				return this._absoluteFrame;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06002D13 RID: 11539 RVA: 0x005BDB61 File Offset: 0x005BBD61
		public int Start
		{
			get
			{
				return this._start;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06002D14 RID: 11540 RVA: 0x005BDB69 File Offset: 0x005BBD69
		public int Duration
		{
			get
			{
				return this._duration;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06002D15 RID: 11541 RVA: 0x005BDB71 File Offset: 0x005BBD71
		public int Frame
		{
			get
			{
				return this._absoluteFrame - this._start;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06002D16 RID: 11542 RVA: 0x005BDB80 File Offset: 0x005BBD80
		public bool IsFirstFrame
		{
			get
			{
				return this._start == this._absoluteFrame;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06002D17 RID: 11543 RVA: 0x005BDB90 File Offset: 0x005BBD90
		public bool IsLastFrame
		{
			get
			{
				return this.Remaining == 0;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06002D18 RID: 11544 RVA: 0x005BDB9B File Offset: 0x005BBD9B
		public int Remaining
		{
			get
			{
				return this._start + this._duration - this._absoluteFrame - 1;
			}
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x005BDBB3 File Offset: 0x005BBDB3
		public FrameEventData(int absoluteFrame, int start, int duration)
		{
			this._absoluteFrame = absoluteFrame;
			this._start = start;
			this._duration = duration;
		}

		// Token: 0x0400512A RID: 20778
		private int _absoluteFrame;

		// Token: 0x0400512B RID: 20779
		private int _start;

		// Token: 0x0400512C RID: 20780
		private int _duration;
	}
}
