using System;

namespace Terraria.Modules
{
	// Token: 0x0200012E RID: 302
	public class AnchorTypesModule
	{
		// Token: 0x06001A7A RID: 6778 RVA: 0x004CB970 File Offset: 0x004C9B70
		public AnchorTypesModule(AnchorTypesModule copyFrom = null)
		{
			if (copyFrom == null)
			{
				this.tileValid = null;
				this.tileInvalid = null;
				this.tileAlternates = null;
				this.wallValid = null;
				return;
			}
			if (copyFrom.tileValid == null)
			{
				this.tileValid = null;
			}
			else
			{
				this.tileValid = new int[copyFrom.tileValid.Length];
				Array.Copy(copyFrom.tileValid, this.tileValid, this.tileValid.Length);
			}
			if (copyFrom.tileInvalid == null)
			{
				this.tileInvalid = null;
			}
			else
			{
				this.tileInvalid = new int[copyFrom.tileInvalid.Length];
				Array.Copy(copyFrom.tileInvalid, this.tileInvalid, this.tileInvalid.Length);
			}
			if (copyFrom.tileAlternates == null)
			{
				this.tileAlternates = null;
			}
			else
			{
				this.tileAlternates = new int[copyFrom.tileAlternates.Length];
				Array.Copy(copyFrom.tileAlternates, this.tileAlternates, this.tileAlternates.Length);
			}
			if (copyFrom.wallValid == null)
			{
				this.wallValid = null;
				return;
			}
			this.wallValid = new int[copyFrom.wallValid.Length];
			Array.Copy(copyFrom.wallValid, this.wallValid, this.wallValid.Length);
		}

		// Token: 0x04001438 RID: 5176
		public int[] tileValid;

		// Token: 0x04001439 RID: 5177
		public int[] tileInvalid;

		// Token: 0x0400143A RID: 5178
		public int[] tileAlternates;

		// Token: 0x0400143B RID: 5179
		public int[] wallValid;
	}
}
