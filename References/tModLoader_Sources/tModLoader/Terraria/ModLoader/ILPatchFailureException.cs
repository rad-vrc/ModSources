using System;
using System.Runtime.CompilerServices;
using MonoMod.Cil;

namespace Terraria.ModLoader
{
	// Token: 0x020001D8 RID: 472
	public class ILPatchFailureException : Exception
	{
		// Token: 0x060024DA RID: 9434 RVA: 0x004EACB4 File Offset: 0x004E8EB4
		public ILPatchFailureException(Mod mod, ILContext il, Exception innerException)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 2);
			defaultInterpolatedStringHandler.AppendLiteral("Mod \"");
			defaultInterpolatedStringHandler.AppendFormatted(mod.Name);
			defaultInterpolatedStringHandler.AppendLiteral("\" failed to IL edit method \"");
			defaultInterpolatedStringHandler.AppendFormatted(il.Method.FullName);
			defaultInterpolatedStringHandler.AppendLiteral("\"");
			base..ctor(defaultInterpolatedStringHandler.ToStringAndClear(), innerException);
			MonoModHooks.DumpIL(mod, il);
		}
	}
}
