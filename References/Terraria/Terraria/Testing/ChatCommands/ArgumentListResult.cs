using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.Testing.ChatCommands
{
	// Token: 0x020000A8 RID: 168
	public class ArgumentListResult : IEnumerable<string>, IEnumerable
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x0049FBB7 File Offset: 0x0049DDB7
		public int Count
		{
			get
			{
				return this._results.Count;
			}
		}

		// Token: 0x170001BD RID: 445
		public string this[int index]
		{
			get
			{
				return this._results[index];
			}
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0049FBD2 File Offset: 0x0049DDD2
		public ArgumentListResult(IEnumerable<string> results)
		{
			this._results = results.ToList<string>();
			this.IsValid = true;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0049FBED File Offset: 0x0049DDED
		private ArgumentListResult(bool isValid)
		{
			this._results = new List<string>();
			this.IsValid = isValid;
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0049FC07 File Offset: 0x0049DE07
		public IEnumerator<string> GetEnumerator()
		{
			return this._results.GetEnumerator();
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0049FC19 File Offset: 0x0049DE19
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040011AD RID: 4525
		public static readonly ArgumentListResult Empty = new ArgumentListResult(true);

		// Token: 0x040011AE RID: 4526
		public static readonly ArgumentListResult Invalid = new ArgumentListResult(false);

		// Token: 0x040011AF RID: 4527
		public readonly bool IsValid;

		// Token: 0x040011B0 RID: 4528
		private readonly List<string> _results;
	}
}
