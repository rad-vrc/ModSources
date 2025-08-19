using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Excludes a class, method, or property from the <see href="https://github.com/tModLoader/tModLoader/wiki/JIT-Exception#what-are-jit-exceptions">load time JIT</see> unless the specified mods are also loaded. Use this on any member which directly references a Type from a <see href="https://github.com/tModLoader/tModLoader/wiki/Expert-Cross-Mod-Content#weak-references-aka-weakreferences-expert">weakly referenced mod</see> that might not be present. See <see href="https://github.com/tModLoader/tModLoader/wiki/JIT-Exception#weak-references">this wiki page</see> for more information.
	/// </summary>
	// Token: 0x0200018A RID: 394
	public sealed class JITWhenModsEnabledAttribute : MemberJitAttribute
	{
		// Token: 0x06001ED7 RID: 7895 RVA: 0x004DD1B4 File Offset: 0x004DB3B4
		public JITWhenModsEnabledAttribute(params string[] names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			this.Names = names;
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x004DD1D2 File Offset: 0x004DB3D2
		public override bool ShouldJIT(MemberInfo member)
		{
			IEnumerable<string> names = this.Names;
			Func<string, bool> predicate;
			if ((predicate = JITWhenModsEnabledAttribute.<>O.<0>__HasMod) == null)
			{
				predicate = (JITWhenModsEnabledAttribute.<>O.<0>__HasMod = new Func<string, bool>(ModLoader.HasMod));
			}
			return names.All(predicate);
		}

		// Token: 0x04001648 RID: 5704
		public readonly string[] Names;

		// Token: 0x020008FE RID: 2302
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006A8C RID: 27276
			public static Func<string, bool> <0>__HasMod;
		}
	}
}
