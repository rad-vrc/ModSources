using System;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Excludes a class from JIT and also from autoloading. Annotate classes that inherit from classes from <see href="https://github.com/tModLoader/tModLoader/wiki/Expert-Cross-Mod-Content#weak-references-aka-weakreferences-expert">weakly referenced mods</see> with this attribute to prevent the game from attempting to autoload the class, which would cause load errors otherwise. See <see href="https://github.com/tModLoader/tModLoader/wiki/JIT-Exception#weak-references">this wiki page</see> for more information.
	/// </summary>
	// Token: 0x0200015E RID: 350
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ExtendsFromModAttribute : Attribute
	{
		// Token: 0x06001C14 RID: 7188 RVA: 0x004D2751 File Offset: 0x004D0951
		public ExtendsFromModAttribute(params string[] names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			this.Names = names;
		}

		// Token: 0x0400150B RID: 5387
		public readonly string[] Names;
	}
}
