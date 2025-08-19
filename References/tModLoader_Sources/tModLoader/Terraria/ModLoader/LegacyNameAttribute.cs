using System;
using System.Collections.Generic;
using System.Reflection;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Allows for types to be registered with legacy/alias names for lookup via <see cref="M:Terraria.ModLoader.ModContent.Find``1(System.String)" /> and similar methods.
	/// <br />When manually loading content, use <see cref="M:Terraria.ModLoader.ModTypeLookup`1.RegisterLegacyNames(`0,System.String[])" /> instead.
	/// </summary>
	// Token: 0x0200018C RID: 396
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class LegacyNameAttribute : Attribute
	{
		// Token: 0x06001EE0 RID: 7904 RVA: 0x004DD29E File Offset: 0x004DB49E
		public LegacyNameAttribute(params string[] names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			this.Names = names;
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x004DD2BC File Offset: 0x004DB4BC
		public static IEnumerable<string> GetLegacyNamesOfType(Type type)
		{
			foreach (LegacyNameAttribute attribute in type.GetCustomAttributes(false))
			{
				foreach (string legacyName in attribute.Names)
				{
					yield return legacyName;
				}
				string[] array = null;
			}
			IEnumerator<LegacyNameAttribute> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0400164A RID: 5706
		public readonly string[] Names;
	}
}
