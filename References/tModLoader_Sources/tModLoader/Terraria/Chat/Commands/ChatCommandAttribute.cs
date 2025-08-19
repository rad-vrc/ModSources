using System;

namespace Terraria.Chat.Commands
{
	// Token: 0x0200074F RID: 1871
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
	public sealed class ChatCommandAttribute : Attribute
	{
		// Token: 0x06004BE5 RID: 19429 RVA: 0x0066C4CB File Offset: 0x0066A6CB
		public ChatCommandAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x0400609E RID: 24734
		public readonly string Name;
	}
}
