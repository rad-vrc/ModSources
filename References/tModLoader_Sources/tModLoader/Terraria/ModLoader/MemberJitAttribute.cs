using System;
using System.Reflection;

namespace Terraria.ModLoader
{
	// Token: 0x02000188 RID: 392
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = true)]
	public abstract class MemberJitAttribute : Attribute
	{
		// Token: 0x06001ED3 RID: 7891 RVA: 0x004DD19E File Offset: 0x004DB39E
		public virtual bool ShouldJIT(MemberInfo member)
		{
			return true;
		}
	}
}
