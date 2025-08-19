using System;
using System.Collections.Generic;

namespace Terraria.ModLoader.Exceptions
{
	// Token: 0x020002A6 RID: 678
	public class MultipleException : AggregateException
	{
		// Token: 0x06002CF0 RID: 11504 RVA: 0x0052B31A File Offset: 0x0052951A
		public MultipleException(IEnumerable<Exception> exceptions) : this(MultipleException.DefaultMessage, exceptions)
		{
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x0052B328 File Offset: 0x00529528
		public MultipleException(string message, IEnumerable<Exception> exceptions) : base(exceptions)
		{
			this._message = message;
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06002CF2 RID: 11506 RVA: 0x0052B338 File Offset: 0x00529538
		public override string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x04001C14 RID: 7188
		public static readonly string DefaultMessage = "Multiple errors occurred.";

		// Token: 0x04001C15 RID: 7189
		private readonly string _message;
	}
}
