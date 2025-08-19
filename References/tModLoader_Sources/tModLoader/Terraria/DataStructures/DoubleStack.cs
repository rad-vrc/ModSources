using System;

namespace Terraria.DataStructures
{
	// Token: 0x020006DA RID: 1754
	public class DoubleStack<T1>
	{
		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06004922 RID: 18722 RVA: 0x0064CB0E File Offset: 0x0064AD0E
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x0064CB18 File Offset: 0x0064AD18
		public DoubleStack(int segmentSize = 1024, int initialSize = 0)
		{
			if (segmentSize < 16)
			{
				segmentSize = 16;
			}
			this._start = segmentSize / 2;
			this._end = this._start;
			this._size = 0;
			this._segmentShiftPosition = segmentSize + this._start;
			initialSize += this._start;
			int num = initialSize / segmentSize + 1;
			this._segmentList = new T1[num][];
			for (int i = 0; i < num; i++)
			{
				this._segmentList[i] = new T1[segmentSize];
			}
			this._segmentSize = segmentSize;
			this._segmentCount = num;
			this._last = this._segmentSize * this._segmentCount - 1;
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x0064CBB8 File Offset: 0x0064ADB8
		public void PushFront(T1 front)
		{
			if (this._start == 0)
			{
				T1[][] array = new T1[this._segmentCount + 1][];
				for (int i = 0; i < this._segmentCount; i++)
				{
					array[i + 1] = this._segmentList[i];
				}
				array[0] = new T1[this._segmentSize];
				this._segmentList = array;
				this._segmentCount++;
				this._start += this._segmentSize;
				this._end += this._segmentSize;
				this._last += this._segmentSize;
			}
			this._start--;
			T1[] array2 = this._segmentList[this._start / this._segmentSize];
			int num = this._start % this._segmentSize;
			array2[num] = front;
			this._size++;
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x0064CCA0 File Offset: 0x0064AEA0
		public T1 PopFront()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException("The DoubleStack is empty.");
			}
			T1[] array2 = this._segmentList[this._start / this._segmentSize];
			int num = this._start % this._segmentSize;
			T1 result = array2[num];
			array2[num] = default(T1);
			this._start++;
			this._size--;
			if (this._start >= this._segmentShiftPosition)
			{
				T1[] array = this._segmentList[0];
				for (int i = 0; i < this._segmentCount - 1; i++)
				{
					this._segmentList[i] = this._segmentList[i + 1];
				}
				this._segmentList[this._segmentCount - 1] = array;
				this._start -= this._segmentSize;
				this._end -= this._segmentSize;
			}
			if (this._size == 0)
			{
				this._start = this._segmentSize / 2;
				this._end = this._start;
			}
			return result;
		}

		// Token: 0x06004926 RID: 18726 RVA: 0x0064CDB0 File Offset: 0x0064AFB0
		public T1 PeekFront()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException("The DoubleStack is empty.");
			}
			T1[] array = this._segmentList[this._start / this._segmentSize];
			int num = this._start % this._segmentSize;
			return array[num];
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x0064CDF8 File Offset: 0x0064AFF8
		public void PushBack(T1 back)
		{
			if (this._end == this._last)
			{
				T1[][] array = new T1[this._segmentCount + 1][];
				for (int i = 0; i < this._segmentCount; i++)
				{
					array[i] = this._segmentList[i];
				}
				array[this._segmentCount] = new T1[this._segmentSize];
				this._segmentCount++;
				this._segmentList = array;
				this._last += this._segmentSize;
			}
			T1[] array2 = this._segmentList[this._end / this._segmentSize];
			int num = this._end % this._segmentSize;
			array2[num] = back;
			this._end++;
			this._size++;
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x0064CEC0 File Offset: 0x0064B0C0
		public T1 PopBack()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException("The DoubleStack is empty.");
			}
			T1[] array = this._segmentList[this._end / this._segmentSize];
			int num = this._end % this._segmentSize;
			T1 result = array[num];
			array[num] = default(T1);
			this._end--;
			this._size--;
			if (this._size == 0)
			{
				this._start = this._segmentSize / 2;
				this._end = this._start;
			}
			return result;
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x0064CF58 File Offset: 0x0064B158
		public T1 PeekBack()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException("The DoubleStack is empty.");
			}
			T1[] array = this._segmentList[this._end / this._segmentSize];
			int num = this._end % this._segmentSize;
			return array[num];
		}

		// Token: 0x0600492A RID: 18730 RVA: 0x0064CFA0 File Offset: 0x0064B1A0
		public void Clear(bool quickClear = false)
		{
			if (!quickClear)
			{
				for (int i = 0; i < this._segmentCount; i++)
				{
					Array.Clear(this._segmentList[i], 0, this._segmentSize);
				}
			}
			this._start = this._segmentSize / 2;
			this._end = this._start;
			this._size = 0;
		}

		// Token: 0x04005E87 RID: 24199
		private T1[][] _segmentList;

		// Token: 0x04005E88 RID: 24200
		private readonly int _segmentSize;

		// Token: 0x04005E89 RID: 24201
		private int _segmentCount;

		// Token: 0x04005E8A RID: 24202
		private readonly int _segmentShiftPosition;

		// Token: 0x04005E8B RID: 24203
		private int _start;

		// Token: 0x04005E8C RID: 24204
		private int _end;

		// Token: 0x04005E8D RID: 24205
		private int _size;

		// Token: 0x04005E8E RID: 24206
		private int _last;
	}
}
