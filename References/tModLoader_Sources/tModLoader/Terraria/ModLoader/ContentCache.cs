using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Container object for <see cref="T:Terraria.ModLoader.ILoadable" /> instances added for a <see cref="T:Terraria.ModLoader.Mod" />
	/// </summary>
	// Token: 0x02000152 RID: 338
	internal class ContentCache
	{
		// Token: 0x06001B88 RID: 7048 RVA: 0x004D0A45 File Offset: 0x004CEC45
		internal static void Unload()
		{
			ContentCache.contentLoadingFinished = false;
			ContentCache._cachedContentForAllMods.Clear();
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x004D0A58 File Offset: 0x004CEC58
		public static IEnumerable<T> GetContentForAllMods<T>() where T : ILoadable
		{
			IList cachedContent;
			if (ContentCache._cachedContentForAllMods.TryGetValue(typeof(T), out cachedContent))
			{
				return (IReadOnlyList<T>)cachedContent;
			}
			IEnumerable<T> query = ModLoader.Mods.SelectMany((Mod m) => m.GetContent<T>());
			if (!ContentCache.contentLoadingFinished)
			{
				return query;
			}
			IReadOnlyList<T> content = query.ToList<T>().AsReadOnly();
			ContentCache._cachedContentForAllMods[typeof(T)] = (IList)content;
			return content;
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x004D0ADE File Offset: 0x004CECDE
		internal ContentCache(Mod mod)
		{
			this._mod = mod;
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x004D0B03 File Offset: 0x004CED03
		internal void Add(ILoadable loadable)
		{
			this._content.Add(loadable);
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x004D0B11 File Offset: 0x004CED11
		public IEnumerable<ILoadable> GetContent()
		{
			return this._content.AsReadOnly();
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x004D0B20 File Offset: 0x004CED20
		public IEnumerable<T> GetContent<T>() where T : ILoadable
		{
			IList cachedContent;
			if (this._cachedContent.TryGetValue(typeof(T), out cachedContent))
			{
				return (IReadOnlyList<T>)cachedContent;
			}
			if (this._content.Count == 0)
			{
				return Enumerable.Empty<T>();
			}
			IEnumerable<T> query = this._content.OfType<T>();
			if (this._mod.loading)
			{
				return query;
			}
			IReadOnlyList<T> content = query.ToList<T>().AsReadOnly();
			this._cachedContent[typeof(T)] = (IList)content;
			return content;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x004D0BA3 File Offset: 0x004CEDA3
		internal void Clear()
		{
			this._content.Clear();
			this._cachedContent.Clear();
		}

		// Token: 0x040014BF RID: 5311
		private static readonly Dictionary<Type, IList> _cachedContentForAllMods = new Dictionary<Type, IList>();

		// Token: 0x040014C0 RID: 5312
		internal static bool contentLoadingFinished;

		// Token: 0x040014C1 RID: 5313
		private readonly Mod _mod;

		// Token: 0x040014C2 RID: 5314
		private readonly List<ILoadable> _content = new List<ILoadable>();

		// Token: 0x040014C3 RID: 5315
		private readonly Dictionary<Type, IList> _cachedContent = new Dictionary<Type, IList>();
	}
}
