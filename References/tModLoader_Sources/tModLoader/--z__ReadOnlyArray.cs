using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x0200077C RID: 1916
internal sealed class <>z__ReadOnlyArray<T> : IEnumerable, ICollection, IList, IEnumerable<!0>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection<T>, IList<T>
{
	// Token: 0x06004D58 RID: 19800 RVA: 0x0067303F File Offset: 0x0067123F
	public <>z__ReadOnlyArray(T[] items)
	{
		this._items = items;
	}

	// Token: 0x06004D59 RID: 19801 RVA: 0x0067304E File Offset: 0x0067124E
	[return: Nullable(1)]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this._items.GetEnumerator();
	}

	// Token: 0x17000883 RID: 2179
	// (get) Token: 0x06004D5A RID: 19802 RVA: 0x0067305B File Offset: 0x0067125B
	int ICollection.Count
	{
		get
		{
			return this._items.Length;
		}
	}

	// Token: 0x17000884 RID: 2180
	// (get) Token: 0x06004D5B RID: 19803 RVA: 0x00673065 File Offset: 0x00671265
	bool ICollection.IsSynchronized
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000885 RID: 2181
	// (get) Token: 0x06004D5C RID: 19804 RVA: 0x00673068 File Offset: 0x00671268
	object ICollection.SyncRoot
	{
		[return: Nullable(1)]
		get
		{
			return this;
		}
	}

	// Token: 0x06004D5D RID: 19805 RVA: 0x0067306B File Offset: 0x0067126B
	void ICollection.CopyTo([Nullable(1)] Array array, int index)
	{
		this._items.CopyTo(array, index);
	}

	// Token: 0x17000886 RID: 2182
	object IList.this[int index]
	{
		[return: Nullable(2)]
		get
		{
			return this._items[index];
		}
		[param: Nullable(2)]
		set
		{
			throw new NotSupportedException();
		}
	}

	// Token: 0x17000887 RID: 2183
	// (get) Token: 0x06004D60 RID: 19808 RVA: 0x00673094 File Offset: 0x00671294
	bool IList.IsFixedSize
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000888 RID: 2184
	// (get) Token: 0x06004D61 RID: 19809 RVA: 0x00673097 File Offset: 0x00671297
	bool IList.IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06004D62 RID: 19810 RVA: 0x0067309A File Offset: 0x0067129A
	int IList.Add([Nullable(2)] object value)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06004D63 RID: 19811 RVA: 0x006730A1 File Offset: 0x006712A1
	void IList.Clear()
	{
		throw new NotSupportedException();
	}

	// Token: 0x06004D64 RID: 19812 RVA: 0x006730A8 File Offset: 0x006712A8
	bool IList.Contains([Nullable(2)] object value)
	{
		return this._items.Contains(value);
	}

	// Token: 0x06004D65 RID: 19813 RVA: 0x006730B6 File Offset: 0x006712B6
	int IList.IndexOf([Nullable(2)] object value)
	{
		return this._items.IndexOf(value);
	}

	// Token: 0x06004D66 RID: 19814 RVA: 0x006730C4 File Offset: 0x006712C4
	void IList.Insert(int index, [Nullable(2)] object value)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06004D67 RID: 19815 RVA: 0x006730CB File Offset: 0x006712CB
	void IList.Remove([Nullable(2)] object value)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06004D68 RID: 19816 RVA: 0x006730D2 File Offset: 0x006712D2
	void IList.RemoveAt(int index)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06004D69 RID: 19817 RVA: 0x006730D9 File Offset: 0x006712D9
	[return: Nullable(new byte[]
	{
		1,
		0
	})]
	IEnumerator<T> IEnumerable<!0>.GetEnumerator()
	{
		return this._items.GetEnumerator();
	}

	// Token: 0x17000889 RID: 2185
	// (get) Token: 0x06004D6A RID: 19818 RVA: 0x006730E6 File Offset: 0x006712E6
	int IReadOnlyCollection<!0>.Count
	{
		get
		{
			return this._items.Length;
		}
	}

	// Token: 0x1700088A RID: 2186
	T IReadOnlyList<!0>.this[int index]
	{
		get
		{
			return this._items[index];
		}
	}

	// Token: 0x1700088B RID: 2187
	// (get) Token: 0x06004D6C RID: 19820 RVA: 0x006730FE File Offset: 0x006712FE
	int ICollection<!0>.Count
	{
		get
		{
			return this._items.Length;
		}
	}

	// Token: 0x1700088C RID: 2188
	// (get) Token: 0x06004D6D RID: 19821 RVA: 0x00673108 File Offset: 0x00671308
	bool ICollection<!0>.IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06004D6E RID: 19822 RVA: 0x0067310B File Offset: 0x0067130B
	void ICollection<!0>.Add(T item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06004D6F RID: 19823 RVA: 0x00673112 File Offset: 0x00671312
	void ICollection<!0>.Clear()
	{
		throw new NotSupportedException();
	}

	// Token: 0x06004D70 RID: 19824 RVA: 0x00673119 File Offset: 0x00671319
	bool ICollection<!0>.Contains(T item)
	{
		return this._items.Contains(item);
	}

	// Token: 0x06004D71 RID: 19825 RVA: 0x00673127 File Offset: 0x00671327
	void ICollection<!0>.CopyTo([Nullable(new byte[]
	{
		1,
		0
	})] T[] array, int arrayIndex)
	{
		this._items.CopyTo(array, arrayIndex);
	}

	// Token: 0x06004D72 RID: 19826 RVA: 0x00673136 File Offset: 0x00671336
	bool ICollection<!0>.Remove(T item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x1700088D RID: 2189
	T IList<!0>.this[int index]
	{
		get
		{
			return this._items[index];
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	// Token: 0x06004D75 RID: 19829 RVA: 0x00673152 File Offset: 0x00671352
	int IList<!0>.IndexOf(T item)
	{
		return this._items.IndexOf(item);
	}

	// Token: 0x06004D76 RID: 19830 RVA: 0x00673160 File Offset: 0x00671360
	void IList<!0>.Insert(int index, T item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06004D77 RID: 19831 RVA: 0x00673167 File Offset: 0x00671367
	void IList<!0>.RemoveAt(int index)
	{
		throw new NotSupportedException();
	}

	// Token: 0x0400657E RID: 25982
	[CompilerGenerated]
	private readonly T[] _items;
}
