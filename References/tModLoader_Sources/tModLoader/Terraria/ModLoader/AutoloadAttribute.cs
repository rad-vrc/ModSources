using System;
using System.Linq;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Allows for customizing if the annotated Type should be automatically loaded.
	/// <para /> True to always autoload, which is the default behavior, or false to prevent autoloading this Type.
	/// <para /> It is also possible to dictate if autoloading should conditionally only happen on a client or a server by setting <see cref="P:Terraria.ModLoader.AutoloadAttribute.Side" /> to <see cref="F:Terraria.ModLoader.ModSide.Client" /> or <see cref="F:Terraria.ModLoader.ModSide.Server" />. One use of this is to prevent graphics related classes from loading on the server.
	/// <para /> Note that content with a non-default constructor or marked as abstract will automatically not be autoloaded, so this attribute is not needed for those.
	/// </summary>
	// Token: 0x0200013F RID: 319
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public sealed class AutoloadAttribute : Attribute
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x004CD120 File Offset: 0x004CB320
		// (set) Token: 0x06001AB6 RID: 6838 RVA: 0x004CD128 File Offset: 0x004CB328
		public ModSide Side { get; set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x004CD131 File Offset: 0x004CB331
		public bool NeedsAutoloading
		{
			get
			{
				return this.Value && ModOrganizer.LoadSide(this.Side);
			}
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x004CD148 File Offset: 0x004CB348
		public AutoloadAttribute(bool value = true)
		{
			this.Value = value;
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x004CD157 File Offset: 0x004CB357
		public static AutoloadAttribute GetValue(Type type)
		{
			return ((AutoloadAttribute)type.GetCustomAttributes(typeof(AutoloadAttribute), true).FirstOrDefault<object>()) ?? AutoloadAttribute.Default;
		}

		// Token: 0x04001475 RID: 5237
		private static readonly AutoloadAttribute Default = new AutoloadAttribute(true);

		// Token: 0x04001476 RID: 5238
		public readonly bool Value;
	}
}
