using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.Testing.ChatCommands
{
	// Token: 0x020000C7 RID: 199
	public class ArgumentListResult : IEnumerable<string>, IEnumerable
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x004B545B File Offset: 0x004B365B
		public int Count
		{
			get
			{
				return this._results.Count;
			}
		}

		// Token: 0x1700026A RID: 618
		public string this[int index]
		{
			get
			{
				return this._results[index];
			}
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x004B5476 File Offset: 0x004B3676
		public ArgumentListResult(IEnumerable<string> results)
		{
			this._results = results.ToList<string>();
			this.IsValid = true;
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x004B5491 File Offset: 0x004B3691
		private ArgumentListResult(bool isValid)
		{
			this._results = new List<string>();
			this.IsValid = isValid;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x004B54AB File Offset: 0x004B36AB
		public IEnumerator<string> GetEnumerator()
		{
			return this._results.GetEnumerator();
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x004B54BD File Offset: 0x004B36BD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040012B3 RID: 4787
		public static readonly ArgumentListResult Empty = new ArgumentListResult(true);

		// Token: 0x040012B4 RID: 4788
		public static readonly ArgumentListResult Invalid = new ArgumentListResult(false);

		// Token: 0x040012B5 RID: 4789
		public readonly bool IsValid;

		// Token: 0x040012B6 RID: 4790
		private readonly List<string> _results;
	}
}
