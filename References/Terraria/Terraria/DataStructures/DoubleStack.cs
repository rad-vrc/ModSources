using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000446 RID: 1094
	public class DoubleStack<T1>
	{
		// Token: 0x06002BD8 RID: 11224 RVA: 0x0059F7A8 File Offset: 0x0059D9A8
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

		// Token: 0x06002BD9 RID: 11225 RVA: 0x0059F848 File Offset: 0x0059DA48
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

		// Token: 0x06002BDA RID: 11226 RVA: 0x0059F930 File Offset: 0x0059DB30
		public T1 PopFront()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException("The DoubleStack is empty.");
			}
			T1[] array = this._segmentList[this._start / this._segmentSize];
			int num = this._start % this._segmentSize;
			T1 result = array[num];
			array[num] = default(T1);
			this._start++;
			this._size--;
			if (this._start >= this._segmentShiftPosition)
			{
				T1[] array2 = this._segmentList[0];
				for (int i = 0; i < this._segmentCount - 1; i++)
				{
					this._segmentList[i] = this._segmentList[i + 1];
				}
				this._segmentList[this._segmentCount - 1] = array2;
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

		// Token: 0x06002BDB RID: 11227 RVA: 0x0059FA40 File Offset: 0x0059DC40
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

		// Token: 0x06002BDC RID: 11228 RVA: 0x0059FA88 File Offset: 0x0059DC88
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

		// Token: 0x06002BDD RID: 11229 RVA: 0x0059FB50 File Offset: 0x0059DD50
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

		// Token: 0x06002BDE RID: 11230 RVA: 0x0059FBE8 File Offset: 0x0059DDE8
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

		// Token: 0x06002BDF RID: 11231 RVA: 0x0059FC30 File Offset: 0x0059DE30
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

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06002BE0 RID: 11232 RVA: 0x0059FC86 File Offset: 0x0059DE86
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x04004FF8 RID: 20472
		private T1[][] _segmentList;

		// Token: 0x04004FF9 RID: 20473
		private readonly int _segmentSize;

		// Token: 0x04004FFA RID: 20474
		private int _segmentCount;

		// Token: 0x04004FFB RID: 20475
		private readonly int _segmentShiftPosition;

		// Token: 0x04004FFC RID: 20476
		private int _start;

		// Token: 0x04004FFD RID: 20477
		private int _end;

		// Token: 0x04004FFE RID: 20478
		private int _size;

		// Token: 0x04004FFF RID: 20479
		private int _last;
	}
}
