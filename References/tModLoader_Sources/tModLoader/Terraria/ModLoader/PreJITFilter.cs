using System;
using System.Linq;
using System.Reflection;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Allows custom JIT logic to be applied to classes in this mod.
	/// </summary>
	// Token: 0x02000187 RID: 391
	public class PreJITFilter
	{
		// Token: 0x06001ED1 RID: 7889 RVA: 0x004DD160 File Offset: 0x004DB360
		public virtual bool ShouldJIT(MemberInfo member)
		{
			return member.GetCustomAttributes<MemberJitAttribute>().All((MemberJitAttribute a) => a.ShouldJIT(member));
		}
	}
}
