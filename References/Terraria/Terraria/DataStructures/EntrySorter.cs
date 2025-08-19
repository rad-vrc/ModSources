using System;
using System.Collections.Generic;
using Terraria.Localization;

namespace Terraria.DataStructures
{
	// Token: 0x02000403 RID: 1027
	public class EntrySorter<TEntryType, TStepType> : IComparer<TEntryType> where TEntryType : new() where TStepType : IEntrySortStep<TEntryType>
	{
		// Token: 0x06002B05 RID: 11013 RVA: 0x0059D651 File Offset: 0x0059B851
		public void AddSortSteps(List<TStepType> sortSteps)
		{
			this.Steps.AddRange(sortSteps);
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x0059D660 File Offset: 0x0059B860
		public int Compare(TEntryType x, TEntryType y)
		{
			int num = 0;
			if (this._prioritizedStep != -1)
			{
				TStepType tstepType = this.Steps[this._prioritizedStep];
				num = tstepType.Compare(x, y);
				if (num != 0)
				{
					return num;
				}
			}
			for (int i = 0; i < this.Steps.Count; i++)
			{
				if (i != this._prioritizedStep)
				{
					TStepType tstepType = this.Steps[i];
					num = tstepType.Compare(x, y);
					if (num != 0)
					{
						return num;
					}
				}
			}
			return num;
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x0059D6E1 File Offset: 0x0059B8E1
		public void SetPrioritizedStepIndex(int index)
		{
			this._prioritizedStep = index;
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x0059D6EC File Offset: 0x0059B8EC
		public string GetDisplayName()
		{
			TStepType tstepType = this.Steps[this._prioritizedStep];
			return Language.GetTextValue(tstepType.GetDisplayNameKey());
		}

		// Token: 0x04004F42 RID: 20290
		public List<TStepType> Steps = new List<TStepType>();

		// Token: 0x04004F43 RID: 20291
		private int _prioritizedStep;
	}
}
