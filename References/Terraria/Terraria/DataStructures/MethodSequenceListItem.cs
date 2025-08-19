using System;
using System.Collections.Generic;

namespace Terraria.DataStructures
{
	// Token: 0x0200044C RID: 1100
	public class MethodSequenceListItem
	{
		// Token: 0x06002BF3 RID: 11251 RVA: 0x005A0262 File Offset: 0x0059E462
		public MethodSequenceListItem(string name, Func<bool> method, MethodSequenceListItem parent = null)
		{
			this.Name = name;
			this.Method = method;
			this.Parent = parent;
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x005A027F File Offset: 0x0059E47F
		public bool ShouldAct(List<MethodSequenceListItem> sequence)
		{
			return !this.Skip && sequence.Contains(this) && (this.Parent == null || this.Parent.ShouldAct(sequence));
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x005A02AC File Offset: 0x0059E4AC
		public bool Act()
		{
			return this.Method();
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x005A02BC File Offset: 0x0059E4BC
		public static void ExecuteSequence(List<MethodSequenceListItem> sequence)
		{
			foreach (MethodSequenceListItem methodSequenceListItem in sequence)
			{
				if (methodSequenceListItem.ShouldAct(sequence) && !methodSequenceListItem.Act())
				{
					break;
				}
			}
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x005A031C File Offset: 0x0059E51C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"name: ",
				this.Name,
				" skip: ",
				this.Skip.ToString(),
				" parent: ",
				this.Parent
			});
		}

		// Token: 0x04005017 RID: 20503
		public string Name;

		// Token: 0x04005018 RID: 20504
		public MethodSequenceListItem Parent;

		// Token: 0x04005019 RID: 20505
		public Func<bool> Method;

		// Token: 0x0400501A RID: 20506
		public bool Skip;
	}
}
