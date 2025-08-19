using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x020003A2 RID: 930
[CompilerGenerated]
internal sealed class <>z__ReadOnlySingleElementList<T> : IEnumerable, ICollection, IList, IEnumerable<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection<T>, IList<T>
{
	// Token: 0x0600136F RID: 4975 RVA: 0x0008EC36 File Offset: 0x0008CE36
	public <>z__ReadOnlySingleElementList(T item)
	{
		this._item = item;
	}

	// Token: 0x06001370 RID: 4976 RVA: 0x0008EC45 File Offset: 0x0008CE45
	[return: Nullable(1)]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return new <>z__ReadOnlySingleElementList<T>.Enumerator(this._item);
	}

	// Token: 0x17000231 RID: 561
	// (get) Token: 0x06001371 RID: 4977 RVA: 0x00002430 File Offset: 0x00000630
	int ICollection.Count
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000232 RID: 562
	// (get) Token: 0x06001372 RID: 4978 RVA: 0x0000404D File Offset: 0x0000224D
	bool ICollection.IsSynchronized
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000233 RID: 563
	// (get) Token: 0x06001373 RID: 4979 RVA: 0x0008EC52 File Offset: 0x0008CE52
	object ICollection.SyncRoot
	{
		[return: Nullable(1)]
		get
		{
			return this;
		}
	}

	// Token: 0x06001374 RID: 4980 RVA: 0x0008EC55 File Offset: 0x0008CE55
	void ICollection.CopyTo([Nullable(1)] Array array, int index)
	{
		array.SetValue(this._item, index);
	}

	// Token: 0x17000234 RID: 564
	object IList.this[int index]
	{
		[return: Nullable(2)]
		get
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException();
			}
			return this._item;
		}
		[param: Nullable(2)]
		set
		{
			throw new NotSupportedException();
		}
	}

	// Token: 0x17000235 RID: 565
	// (get) Token: 0x06001377 RID: 4983 RVA: 0x00002430 File Offset: 0x00000630
	bool IList.IsFixedSize
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000236 RID: 566
	// (get) Token: 0x06001378 RID: 4984 RVA: 0x00002430 File Offset: 0x00000630
	bool IList.IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06001379 RID: 4985 RVA: 0x0008EC7F File Offset: 0x0008CE7F
	int IList.Add([Nullable(2)] object value)
	{
		throw new NotSupportedException();
	}

	// Token: 0x0600137A RID: 4986 RVA: 0x0008EC7F File Offset: 0x0008CE7F
	void IList.Clear()
	{
		throw new NotSupportedException();
	}

	// Token: 0x0600137B RID: 4987 RVA: 0x0008EC86 File Offset: 0x0008CE86
	bool IList.Contains([Nullable(2)] object value)
	{
		return EqualityComparer<T>.Default.Equals(this._item, (T)((object)value));
	}

	// Token: 0x0600137C RID: 4988 RVA: 0x0008EC9E File Offset: 0x0008CE9E
	int IList.IndexOf([Nullable(2)] object value)
	{
		if (!EqualityComparer<T>.Default.Equals(this._item, (T)((object)value)))
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x0600137D RID: 4989 RVA: 0x0008EC7F File Offset: 0x0008CE7F
	void IList.Insert(int index, [Nullable(2)] object value)
	{
		throw new NotSupportedException();
	}

	// Token: 0x0600137E RID: 4990 RVA: 0x0008EC7F File Offset: 0x0008CE7F
	void IList.Remove([Nullable(2)] object value)
	{
		throw new NotSupportedException();
	}

	// Token: 0x0600137F RID: 4991 RVA: 0x0008EC7F File Offset: 0x0008CE7F
	void IList.RemoveAt(int index)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06001380 RID: 4992 RVA: 0x0008EC45 File Offset: 0x0008CE45
	[return: Nullable(new byte[]
	{
		1,
		0
	})]
	IEnumerator<T> IEnumerable<!0>.GetEnumerator()
	{
		return new <>z__ReadOnlySingleElementList<T>.Enumerator(this._item);
	}

	// Token: 0x17000237 RID: 567
	// (get) Token: 0x06001381 RID: 4993 RVA: 0x00002430 File Offset: 0x00000630
	int IReadOnlyCollection<!0>.Count
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000238 RID: 568
	T IReadOnlyList<!0>.this[int index]
	{
		get
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException();
			}
			return this._item;
		}
	}

	// Token: 0x17000239 RID: 569
	// (get) Token: 0x06001383 RID: 4995 RVA: 0x00002430 File Offset: 0x00000630
	int ICollection<!0>.Count
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x1700023A RID: 570
	// (get) Token: 0x06001384 RID: 4996 RVA: 0x00002430 File Offset: 0x00000630
	bool ICollection<!0>.IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06001385 RID: 4997 RVA: 0x0008EC7F File Offset: 0x0008CE7F
	void ICollection<!0>.Add(T item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06001386 RID: 4998 RVA: 0x0008EC7F File Offset: 0x0008CE7F
	void ICollection<!0>.Clear()
	{
		throw new NotSupportedException();
	}

	// Token: 0x06001387 RID: 4999 RVA: 0x0008ECCC File Offset: 0x0008CECC
	bool ICollection<!0>.Contains(T item)
	{
		return EqualityComparer<T>.Default.Equals(this._item, item);
	}

	// Token: 0x06001388 RID: 5000 RVA: 0x0008ECDF File Offset: 0x0008CEDF
	void ICollection<!0>.CopyTo([Nullable(new byte[]
	{
		1,
		0
	})] T[] array, int arrayIndex)
	{
		array[arrayIndex] = this._item;
	}

	// Token: 0x06001389 RID: 5001 RVA: 0x0008EC7F File Offset: 0x0008CE7F
	bool ICollection<!0>.Remove(T item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x1700023B RID: 571
	T IList<!0>.this[int index]
	{
		get
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException();
			}
			return this._item;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	// Token: 0x0600138C RID: 5004 RVA: 0x0008ECEE File Offset: 0x0008CEEE
	int IList<!0>.IndexOf(T item)
	{
		if (!EqualityComparer<T>.Default.Equals(this._item, item))
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x0600138D RID: 5005 RVA: 0x0008EC7F File Offset: 0x0008CE7F
	void IList<!0>.Insert(int index, T item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x0600138E RID: 5006 RVA: 0x0008EC7F File Offset: 0x0008CE7F
	void IList<!0>.RemoveAt(int index)
	{
		throw new NotSupportedException();
	}

	// Token: 0x040007AA RID: 1962
	[CompilerGenerated]
	private readonly T _item;

	// Token: 0x02000578 RID: 1400
	private sealed class Enumerator : IDisposable, IEnumerator, IEnumerator<T>
	{
		// Token: 0x06001CF1 RID: 7409 RVA: 0x00092DAD File Offset: 0x00090FAD
		public Enumerator(T item)
		{
			this.System.Collections.Generic.IEnumerator<T>.Current = item;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x00092DBC File Offset: 0x00090FBC
		object IEnumerator.Current
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x00092DC9 File Offset: 0x00090FC9
		T IEnumerator<!0>.Current
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x00092DD4 File Offset: 0x00090FD4
		bool IEnumerator.MoveNext()
		{
			return !this._moveNextCalled && (this._moveNextCalled = true);
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x00092DF5 File Offset: 0x00090FF5
		void IEnumerator.Reset()
		{
			this._moveNextCalled = false;
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		void IDisposable.Dispose()
		{
		}

		// Token: 0x04000FD9 RID: 4057
		[CompilerGenerated]
		private readonly T _item;

		// Token: 0x04000FDA RID: 4058
		[CompilerGenerated]
		private bool _moveNextCalled;
	}
}
