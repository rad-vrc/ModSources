using System;
using System.Collections.Generic;

namespace Terraria.DataStructures
{
	// Token: 0x0200071C RID: 1820
	public class MethodSequenceListItem
	{
		// Token: 0x060049DF RID: 18911 RVA: 0x0064E5B0 File Offset: 0x0064C7B0
		public MethodSequenceListItem(string name, Func<bool> method, MethodSequenceListItem parent = null)
		{
			this.Name = name;
			this.Method = method;
			this.Parent = parent;
		}

		// Token: 0x060049E0 RID: 18912 RVA: 0x0064E5CD File Offset: 0x0064C7CD
		public bool ShouldAct(List<MethodSequenceListItem> sequence)
		{
			return !this.Skip && sequence.Contains(this) && (this.Parent == null || this.Parent.ShouldAct(sequence));
		}

		// Token: 0x060049E1 RID: 18913 RVA: 0x0064E5FA File Offset: 0x0064C7FA
		public bool Act()
		{
			return this.Method();
		}

		// Token: 0x060049E2 RID: 18914 RVA: 0x0064E608 File Offset: 0x0064C808
		public static void ExecuteSequence(List<MethodSequenceListItem> sequence)
		{
			foreach (MethodSequenceListItem item in sequence)
			{
				if (item.ShouldAct(sequence) && !item.Act())
				{
					break;
				}
			}
		}

		// Token: 0x060049E3 RID: 18915 RVA: 0x0064E664 File Offset: 0x0064C864
		public override string ToString()
		{
			string[] array = new string[6];
			array[0] = "name: ";
			array[1] = this.Name;
			array[2] = " skip: ";
			array[3] = this.Skip.ToString();
			array[4] = " parent: ";
			int num = 5;
			MethodSequenceListItem parent = this.Parent;
			array[num] = ((parent != null) ? parent.ToString() : null);
			return string.Concat(array);
		}

		// Token: 0x04005F0D RID: 24333
		public string Name;

		// Token: 0x04005F0E RID: 24334
		public MethodSequenceListItem Parent;

		// Token: 0x04005F0F RID: 24335
		public Func<bool> Method;

		// Token: 0x04005F10 RID: 24336
		public bool Skip;
	}
}
