using System;

namespace Terraria.Chat.Commands
{
	// Token: 0x0200046B RID: 1131
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
	public sealed class ChatCommandAttribute : Attribute
	{
		// Token: 0x06002D5B RID: 11611 RVA: 0x005BE5DD File Offset: 0x005BC7DD
		public ChatCommandAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x0400513B RID: 20795
		public readonly string Name;
	}
}
