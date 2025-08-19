using System;
using System.Collections.Generic;

namespace Terraria.ID
{
	// Token: 0x020001B8 RID: 440
	public class SetFactory
	{
		// Token: 0x06001BA1 RID: 7073 RVA: 0x004EACDC File Offset: 0x004E8EDC
		public SetFactory(int size)
		{
			if (size == 0)
			{
				throw new ArgumentOutOfRangeException("size cannot be 0, the intializer for Count must run first");
			}
			this._size = size;
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x004EAD3C File Offset: 0x004E8F3C
		protected bool[] GetBoolBuffer()
		{
			object queueLock = this._queueLock;
			bool[] result;
			lock (queueLock)
			{
				if (this._boolBufferCache.Count == 0)
				{
					result = new bool[this._size];
				}
				else
				{
					result = this._boolBufferCache.Dequeue();
				}
			}
			return result;
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x004EADA0 File Offset: 0x004E8FA0
		protected int[] GetIntBuffer()
		{
			object queueLock = this._queueLock;
			int[] result;
			lock (queueLock)
			{
				if (this._intBufferCache.Count == 0)
				{
					result = new int[this._size];
				}
				else
				{
					result = this._intBufferCache.Dequeue();
				}
			}
			return result;
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x004EAE04 File Offset: 0x004E9004
		protected ushort[] GetUshortBuffer()
		{
			object queueLock = this._queueLock;
			ushort[] result;
			lock (queueLock)
			{
				if (this._ushortBufferCache.Count == 0)
				{
					result = new ushort[this._size];
				}
				else
				{
					result = this._ushortBufferCache.Dequeue();
				}
			}
			return result;
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x004EAE68 File Offset: 0x004E9068
		protected float[] GetFloatBuffer()
		{
			object queueLock = this._queueLock;
			float[] result;
			lock (queueLock)
			{
				if (this._floatBufferCache.Count == 0)
				{
					result = new float[this._size];
				}
				else
				{
					result = this._floatBufferCache.Dequeue();
				}
			}
			return result;
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x004EAECC File Offset: 0x004E90CC
		public void Recycle<T>(T[] buffer)
		{
			object queueLock = this._queueLock;
			lock (queueLock)
			{
				if (typeof(T).Equals(typeof(bool)))
				{
					this._boolBufferCache.Enqueue((bool[])buffer);
				}
				else if (typeof(T).Equals(typeof(int)))
				{
					this._intBufferCache.Enqueue((int[])buffer);
				}
			}
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x004EAF60 File Offset: 0x004E9160
		public bool[] CreateBoolSet(params int[] types)
		{
			return this.CreateBoolSet(false, types);
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x004EAF6C File Offset: 0x004E916C
		public bool[] CreateBoolSet(bool defaultState, params int[] types)
		{
			bool[] boolBuffer = this.GetBoolBuffer();
			for (int i = 0; i < boolBuffer.Length; i++)
			{
				boolBuffer[i] = defaultState;
			}
			for (int j = 0; j < types.Length; j++)
			{
				boolBuffer[types[j]] = !defaultState;
			}
			return boolBuffer;
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x004EAFAA File Offset: 0x004E91AA
		public int[] CreateIntSet(params int[] types)
		{
			return this.CreateIntSet(-1, types);
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x004EAFB4 File Offset: 0x004E91B4
		public int[] CreateIntSet(int defaultState, params int[] inputs)
		{
			if (inputs.Length % 2 != 0)
			{
				throw new Exception("You have a bad length for inputs on CreateArraySet");
			}
			int[] intBuffer = this.GetIntBuffer();
			for (int i = 0; i < intBuffer.Length; i++)
			{
				intBuffer[i] = defaultState;
			}
			for (int j = 0; j < inputs.Length; j += 2)
			{
				intBuffer[inputs[j]] = inputs[j + 1];
			}
			return intBuffer;
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x004EB008 File Offset: 0x004E9208
		public ushort[] CreateUshortSet(ushort defaultState, params ushort[] inputs)
		{
			if (inputs.Length % 2 != 0)
			{
				throw new Exception("You have a bad length for inputs on CreateArraySet");
			}
			ushort[] ushortBuffer = this.GetUshortBuffer();
			for (int i = 0; i < ushortBuffer.Length; i++)
			{
				ushortBuffer[i] = defaultState;
			}
			for (int j = 0; j < inputs.Length; j += 2)
			{
				ushortBuffer[(int)inputs[j]] = inputs[j + 1];
			}
			return ushortBuffer;
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x004EB05C File Offset: 0x004E925C
		public float[] CreateFloatSet(float defaultState, params float[] inputs)
		{
			if (inputs.Length % 2 != 0)
			{
				throw new Exception("You have a bad length for inputs on CreateArraySet");
			}
			float[] floatBuffer = this.GetFloatBuffer();
			for (int i = 0; i < floatBuffer.Length; i++)
			{
				floatBuffer[i] = defaultState;
			}
			for (int j = 0; j < inputs.Length; j += 2)
			{
				floatBuffer[(int)inputs[j]] = inputs[j + 1];
			}
			return floatBuffer;
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x004EB0B0 File Offset: 0x004E92B0
		public T[] CreateCustomSet<T>(T defaultState, params object[] inputs)
		{
			if (inputs.Length % 2 != 0)
			{
				throw new Exception("You have a bad length for inputs on CreateCustomSet");
			}
			T[] array = new T[this._size];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = defaultState;
			}
			if (inputs != null)
			{
				for (int j = 0; j < inputs.Length; j += 2)
				{
					T t;
					if (typeof(T).IsPrimitive)
					{
						t = (T)((object)inputs[j + 1]);
					}
					else if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
					{
						t = (T)((object)inputs[j + 1]);
					}
					else if (typeof(T).IsClass)
					{
						t = (T)((object)inputs[j + 1]);
					}
					else
					{
						t = (T)((object)Convert.ChangeType(inputs[j + 1], typeof(T)));
					}
					if (inputs[j] is ushort)
					{
						array[(int)((ushort)inputs[j])] = t;
					}
					else if (inputs[j] is int)
					{
						array[(int)inputs[j]] = t;
					}
					else
					{
						array[(int)((short)inputs[j])] = t;
					}
				}
			}
			return array;
		}

		// Token: 0x04001E0C RID: 7692
		protected int _size;

		// Token: 0x04001E0D RID: 7693
		private readonly Queue<int[]> _intBufferCache = new Queue<int[]>();

		// Token: 0x04001E0E RID: 7694
		private readonly Queue<ushort[]> _ushortBufferCache = new Queue<ushort[]>();

		// Token: 0x04001E0F RID: 7695
		private readonly Queue<bool[]> _boolBufferCache = new Queue<bool[]>();

		// Token: 0x04001E10 RID: 7696
		private readonly Queue<float[]> _floatBufferCache = new Queue<float[]>();

		// Token: 0x04001E11 RID: 7697
		private object _queueLock = new object();
	}
}
