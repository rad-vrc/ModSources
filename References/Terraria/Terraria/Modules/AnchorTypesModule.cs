using System;

namespace Terraria.Modules
{
	// Token: 0x02000055 RID: 85
	public class AnchorTypesModule
	{
		// Token: 0x0600111B RID: 4379 RVA: 0x0048C174 File Offset: 0x0048A374
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

		// Token: 0x04000ED9 RID: 3801
		public int[] tileValid;

		// Token: 0x04000EDA RID: 3802
		public int[] tileInvalid;

		// Token: 0x04000EDB RID: 3803
		public int[] tileAlternates;

		// Token: 0x04000EDC RID: 3804
		public int[] wallValid;
	}
}
