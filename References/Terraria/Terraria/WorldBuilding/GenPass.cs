using System;
using Terraria.IO;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000068 RID: 104
	public abstract class GenPass : GenBase
	{
		// Token: 0x06001142 RID: 4418 RVA: 0x0048CA1A File Offset: 0x0048AC1A
		public GenPass(string name, double loadWeight)
		{
			this.Name = name;
			this.Weight = loadWeight;
		}

		// Token: 0x06001143 RID: 4419
		protected abstract void ApplyPass(GenerationProgress progress, GameConfiguration configuration);

		// Token: 0x06001144 RID: 4420 RVA: 0x0048CA30 File Offset: 0x0048AC30
		public void Apply(GenerationProgress progress, GameConfiguration configuration)
		{
			this.ApplyPass(progress, configuration);
		}

		// Token: 0x04000F0F RID: 3855
		public string Name;

		// Token: 0x04000F10 RID: 3856
		public double Weight;
	}
}
