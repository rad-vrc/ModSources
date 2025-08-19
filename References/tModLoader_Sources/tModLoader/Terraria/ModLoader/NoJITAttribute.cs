using System;
using System.Reflection;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Excludes a class, method, or property from the load time JIT. 
	/// </summary>
	// Token: 0x02000189 RID: 393
	public class NoJITAttribute : MemberJitAttribute
	{
		// Token: 0x06001ED5 RID: 7893 RVA: 0x004DD1A9 File Offset: 0x004DB3A9
		public override bool ShouldJIT(MemberInfo member)
		{
			return false;
		}
	}
}
