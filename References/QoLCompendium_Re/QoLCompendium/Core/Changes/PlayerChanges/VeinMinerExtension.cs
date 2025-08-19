using System;
using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace QoLCompendium.Core.Changes.PlayerChanges
{
	// Token: 0x02000235 RID: 565
	public static class VeinMinerExtension
	{
		// Token: 0x06000D95 RID: 3477 RVA: 0x000691BE File Offset: 0x000673BE
		public static ILCursor EmitCall<T>(this ILCursor ilCursor, string memberName)
		{
			return ilCursor.Emit<T>(OpCodes.Call, memberName);
		}
	}
}
