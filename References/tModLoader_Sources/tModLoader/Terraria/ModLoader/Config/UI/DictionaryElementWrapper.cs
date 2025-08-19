using System;
using System.Collections;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x0200039F RID: 927
	internal class DictionaryElementWrapper<K, V> : IDictionaryElementWrapper
	{
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06003202 RID: 12802 RVA: 0x00541CC6 File Offset: 0x0053FEC6
		// (set) Token: 0x06003203 RID: 12803 RVA: 0x00541CD0 File Offset: 0x0053FED0
		public K Key
		{
			get
			{
				return this._key;
			}
			set
			{
				if (!this.dictionary.Contains(value))
				{
					this.dictionary.Remove(this._key);
					this._key = value;
					this.dictionary.Add(this._key, this._value);
				}
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06003204 RID: 12804 RVA: 0x00541D2E File Offset: 0x0053FF2E
		// (set) Token: 0x06003205 RID: 12805 RVA: 0x00541D36 File Offset: 0x0053FF36
		public V Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this.dictionary[this.Key] = value;
				this._value = value;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06003206 RID: 12806 RVA: 0x00541D5B File Offset: 0x0053FF5B
		object IDictionaryElementWrapper.Key
		{
			get
			{
				return this.Key;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06003207 RID: 12807 RVA: 0x00541D68 File Offset: 0x0053FF68
		object IDictionaryElementWrapper.Value
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x00541D75 File Offset: 0x0053FF75
		public DictionaryElementWrapper(K key, V value, IDictionary dictionary)
		{
			this.dictionary = dictionary;
			this._key = key;
			this._value = value;
		}

		// Token: 0x04001D82 RID: 7554
		private readonly IDictionary dictionary;

		// Token: 0x04001D83 RID: 7555
		private K _key;

		// Token: 0x04001D84 RID: 7556
		private V _value;
	}
}
