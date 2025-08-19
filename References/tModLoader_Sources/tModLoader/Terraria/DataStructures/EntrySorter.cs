using System;
using System.Collections.Generic;
using Terraria.Localization;

namespace Terraria.DataStructures
{
	// Token: 0x02000709 RID: 1801
	public class EntrySorter<TEntryType, TStepType> : IComparer<TEntryType> where TEntryType : new() where TStepType : IEntrySortStep<TEntryType>
	{
		// Token: 0x0600499B RID: 18843 RVA: 0x0064DC49 File Offset: 0x0064BE49
		public void AddSortSteps(List<TStepType> sortSteps)
		{
			this.Steps.AddRange(sortSteps);
		}

		// Token: 0x0600499C RID: 18844 RVA: 0x0064DC58 File Offset: 0x0064BE58
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

		// Token: 0x0600499D RID: 18845 RVA: 0x0064DCD9 File Offset: 0x0064BED9
		public void SetPrioritizedStepIndex(int index)
		{
			this._prioritizedStep = index;
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x0064DCE4 File Offset: 0x0064BEE4
		public string GetDisplayName()
		{
			TStepType tstepType = this.Steps[this._prioritizedStep];
			return Language.GetTextValue(tstepType.GetDisplayNameKey());
		}

		// Token: 0x04005ED3 RID: 24275
		public List<TStepType> Steps = new List<TStepType>();

		// Token: 0x04005ED4 RID: 24276
		private int _prioritizedStep;
	}
}
