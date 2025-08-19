using System;
using System.Collections.Generic;
using System.Diagnostics;
using Terraria.Utilities;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000076 RID: 118
	public class WorldGenerator
	{
		// Token: 0x06001177 RID: 4471 RVA: 0x0048DF18 File Offset: 0x0048C118
		public WorldGenerator(int seed, WorldGenConfiguration configuration)
		{
			this._seed = seed;
			this._configuration = configuration;
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0048DF39 File Offset: 0x0048C139
		public void Append(GenPass pass)
		{
			this._passes.Add(pass);
			this._totalLoadWeight += pass.Weight;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0048DF5C File Offset: 0x0048C15C
		public void GenerateWorld(GenerationProgress progress = null)
		{
			Stopwatch stopwatch = new Stopwatch();
			double num = 0.0;
			foreach (GenPass genPass in this._passes)
			{
				num += genPass.Weight;
			}
			if (progress == null)
			{
				progress = new GenerationProgress();
			}
			WorldGenerator.CurrentGenerationProgress = progress;
			progress.TotalWeight = num;
			foreach (GenPass genPass2 in this._passes)
			{
				WorldGen._genRand = new UnifiedRandom(this._seed);
				Main.rand = new UnifiedRandom(this._seed);
				stopwatch.Start();
				progress.Start(genPass2.Weight);
				genPass2.Apply(progress, this._configuration.GetPassConfiguration(genPass2.Name));
				progress.End();
				stopwatch.Reset();
			}
			WorldGenerator.CurrentGenerationProgress = null;
		}

		// Token: 0x04000FBA RID: 4026
		private readonly List<GenPass> _passes = new List<GenPass>();

		// Token: 0x04000FBB RID: 4027
		private double _totalLoadWeight;

		// Token: 0x04000FBC RID: 4028
		private readonly int _seed;

		// Token: 0x04000FBD RID: 4029
		private readonly WorldGenConfiguration _configuration;

		// Token: 0x04000FBE RID: 4030
		public static GenerationProgress CurrentGenerationProgress;
	}
}
