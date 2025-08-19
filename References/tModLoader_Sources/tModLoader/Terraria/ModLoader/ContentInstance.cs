using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	// Token: 0x02000153 RID: 339
	public static class ContentInstance
	{
		// Token: 0x06001B90 RID: 7056 RVA: 0x004D0BC7 File Offset: 0x004CEDC7
		static ContentInstance()
		{
			TypeCaching.OnClear += ContentInstance.Clear;
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x004D0BE4 File Offset: 0x004CEDE4
		private static ContentInstance.ContentEntry Factory(Type t)
		{
			return new ContentInstance.ContentEntry();
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x004D0BEB File Offset: 0x004CEDEB
		internal static void Link(Type t, Action<object, IEnumerable> update)
		{
			ConcurrentDictionary<Type, ContentInstance.ContentEntry> concurrentDictionary = ContentInstance.contentByType;
			Func<Type, ContentInstance.ContentEntry> valueFactory;
			if ((valueFactory = ContentInstance.<>O.<0>__Factory) == null)
			{
				valueFactory = (ContentInstance.<>O.<0>__Factory = new Func<Type, ContentInstance.ContentEntry>(ContentInstance.Factory));
			}
			concurrentDictionary.GetOrAdd(t, valueFactory).Link(update);
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x004D0C19 File Offset: 0x004CEE19
		public static void Register(object obj)
		{
			ConcurrentDictionary<Type, ContentInstance.ContentEntry> concurrentDictionary = ContentInstance.contentByType;
			Type type = obj.GetType();
			Func<Type, ContentInstance.ContentEntry> valueFactory;
			if ((valueFactory = ContentInstance.<>O.<0>__Factory) == null)
			{
				valueFactory = (ContentInstance.<>O.<0>__Factory = new Func<Type, ContentInstance.ContentEntry>(ContentInstance.Factory));
			}
			concurrentDictionary.GetOrAdd(type, valueFactory).Register(obj);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x004D0C4C File Offset: 0x004CEE4C
		private static void Clear()
		{
			foreach (KeyValuePair<Type, ContentInstance.ContentEntry> entry in ContentInstance.contentByType)
			{
				entry.Value.Clear();
				if (entry.Key.Assembly != typeof(ContentInstance.ContentEntry).Assembly)
				{
					ContentInstance.ContentEntry contentEntry;
					ContentInstance.contentByType.TryRemove(entry.Key, out contentEntry);
				}
			}
		}

		// Token: 0x040014C4 RID: 5316
		private static ConcurrentDictionary<Type, ContentInstance.ContentEntry> contentByType = new ConcurrentDictionary<Type, ContentInstance.ContentEntry>();

		// Token: 0x020008C6 RID: 2246
		private class ContentEntry
		{
			// Token: 0x06005255 RID: 21077 RVA: 0x00698B30 File Offset: 0x00696D30
			public void Register(object obj)
			{
				lock (this)
				{
					if (this.instances != null)
					{
						this.instances.Add(obj);
					}
					else if (this.instance != null)
					{
						this.instances = new List<object>
						{
							this.instance,
							obj
						};
						this.instance = null;
					}
					else
					{
						this.instance = obj;
					}
					Action<object, IEnumerable> action = this.staticUpdate;
					if (action != null)
					{
						action(this.instance, this.instances);
					}
				}
			}

			// Token: 0x06005256 RID: 21078 RVA: 0x00698BD0 File Offset: 0x00696DD0
			public void Link(Action<object, IEnumerable> update)
			{
				lock (this)
				{
					this.staticUpdate = update;
					update(this.instance, this.instances);
				}
			}

			// Token: 0x06005257 RID: 21079 RVA: 0x00698C20 File Offset: 0x00696E20
			public void Clear()
			{
				lock (this)
				{
					this.instance = null;
					this.instances = null;
					Action<object, IEnumerable> action = this.staticUpdate;
					if (action != null)
					{
						action(this.instance, this.instances);
					}
				}
			}

			// Token: 0x04006A65 RID: 27237
			private object instance;

			// Token: 0x04006A66 RID: 27238
			private List<object> instances;

			// Token: 0x04006A67 RID: 27239
			private Action<object, IEnumerable> staticUpdate;
		}

		// Token: 0x020008C7 RID: 2247
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006A68 RID: 27240
			[Nullable(new byte[]
			{
				0,
				1,
				0
			})]
			public static Func<Type, ContentInstance.ContentEntry> <0>__Factory;
		}
	}
}
