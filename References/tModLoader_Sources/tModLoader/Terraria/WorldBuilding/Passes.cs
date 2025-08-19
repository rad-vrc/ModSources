using System;
using Terraria.IO;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200007E RID: 126
	public static class Passes
	{
		// Token: 0x02000846 RID: 2118
		public class Clear : GenPass
		{
			// Token: 0x060050EE RID: 20718 RVA: 0x006954F1 File Offset: 0x006936F1
			public Clear() : base("clear", 1.0)
			{
			}

			// Token: 0x060050EF RID: 20719 RVA: 0x00695507 File Offset: 0x00693707
			protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
			{
				GenBase._tiles.ClearEverything();
			}
		}

		// Token: 0x02000847 RID: 2119
		public class ScatterCustom : GenPass
		{
			// Token: 0x060050F0 RID: 20720 RVA: 0x00695513 File Offset: 0x00693713
			public ScatterCustom(string name, double loadWeight, int count, GenBase.CustomPerUnitAction perUnit = null) : base(name, loadWeight)
			{
				this._perUnit = perUnit;
				this._count = count;
			}

			// Token: 0x060050F1 RID: 20721 RVA: 0x0069552C File Offset: 0x0069372C
			public void SetCustomAction(GenBase.CustomPerUnitAction perUnit)
			{
				this._perUnit = perUnit;
			}

			// Token: 0x060050F2 RID: 20722 RVA: 0x00695538 File Offset: 0x00693738
			protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
			{
				int num = this._count;
				while (num > 0)
				{
					if (this._perUnit(GenBase._random.Next(1, GenBase._worldWidth), GenBase._random.Next(1, GenBase._worldHeight), Array.Empty<object>()))
					{
						num--;
					}
				}
			}

			// Token: 0x040068B9 RID: 26809
			private GenBase.CustomPerUnitAction _perUnit;

			// Token: 0x040068BA RID: 26810
			private int _count;
		}
	}
}
