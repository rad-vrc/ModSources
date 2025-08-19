using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// This object encapsulates context information about the source of a particular spawning event of an Item/Projectile/NPC/etc. Aids in facilitating many modding situations and used in various OnSpawn hooks.<br />
	/// The <see href="https://github.com/tModLoader/tModLoader/wiki/IEntitySource">IEntitySource Guide</see> teaches how and why to use this.
	/// </summary>
	// Token: 0x02000710 RID: 1808
	[NullableContext(2)]
	public interface IEntitySource
	{
		/// <summary>
		/// Additional context identifier, particularly useful for set bonuses or accessory affects. See <see cref="T:Terraria.ID.ItemSourceID" /> and <see cref="T:Terraria.ID.ProjectileSourceID" /> for vanilla uses
		/// </summary>
		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060049CE RID: 18894
		string Context { get; }

		// Token: 0x060049CF RID: 18895 RVA: 0x0064E51B File Offset: 0x0064C71B
		internal static IEntitySource GetGoreFallback()
		{
			return IEntitySource._goreFallback;
		}

		// Token: 0x060049D0 RID: 18896 RVA: 0x0064E522 File Offset: 0x0064C722
		[NullableContext(1)]
		internal static IEntitySource.FallbackSourceRef PushFallback(IEntitySource source)
		{
			return new IEntitySource.FallbackSourceRef(source);
		}

		// Token: 0x04005F04 RID: 24324
		private static IEntitySource _goreFallback;

		// Token: 0x02000D5A RID: 3418
		[NullableContext(0)]
		internal struct FallbackSourceRef : IDisposable
		{
			// Token: 0x060063E7 RID: 25575 RVA: 0x006D9A60 File Offset: 0x006D7C60
			[NullableContext(1)]
			public FallbackSourceRef(IEntitySource source)
			{
				this.prev = IEntitySource._goreFallback;
				IEntitySource._goreFallback = source;
			}

			// Token: 0x060063E8 RID: 25576 RVA: 0x006D9A73 File Offset: 0x006D7C73
			public void Dispose()
			{
				IEntitySource._goreFallback = this.prev;
			}

			// Token: 0x04007B84 RID: 31620
			[Nullable(2)]
			private IEntitySource prev;
		}
	}
}
