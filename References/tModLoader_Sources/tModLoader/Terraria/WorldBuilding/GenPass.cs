using System;
using Terraria.IO;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000075 RID: 117
	public abstract class GenPass : GenBase
	{
		// Token: 0x060013E2 RID: 5090 RVA: 0x0049F826 File Offset: 0x0049DA26
		public GenPass(string name, double loadWeight)
		{
			this.Name = name;
			this.Weight = loadWeight;
		}

		// Token: 0x060013E3 RID: 5091
		protected abstract void ApplyPass(GenerationProgress progress, GameConfiguration configuration);

		// Token: 0x060013E4 RID: 5092 RVA: 0x0049F843 File Offset: 0x0049DA43
		public void Apply(GenerationProgress progress, GameConfiguration configuration)
		{
			this.ApplyPass(progress, configuration);
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0049F84D File Offset: 0x0049DA4D
		// (set) Token: 0x060013E6 RID: 5094 RVA: 0x0049F855 File Offset: 0x0049DA55
		public bool Enabled { get; private set; } = true;

		// Token: 0x060013E7 RID: 5095 RVA: 0x0049F85E File Offset: 0x0049DA5E
		public void Disable()
		{
			this.Enabled = false;
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0049F867 File Offset: 0x0049DA67
		internal void Reset()
		{
			this.Enabled = true;
		}

		// Token: 0x04000FEB RID: 4075
		public string Name;

		// Token: 0x04000FEC RID: 4076
		public double Weight;
	}
}
