using System;
using System.Collections.Generic;
using System.Diagnostics;
using Terraria.Localization;
using Terraria.Utilities;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000086 RID: 134
	public class WorldGenerator
	{
		// Token: 0x06001420 RID: 5152 RVA: 0x004A0EA4 File Offset: 0x0049F0A4
		public WorldGenerator(int seed, WorldGenConfiguration configuration)
		{
			this._seed = seed;
			this._configuration = configuration;
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x004A0EC5 File Offset: 0x0049F0C5
		public void Append(GenPass pass)
		{
			this._passes.Add(pass);
			this._totalLoadWeight += pass.Weight;
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x004A0EE8 File Offset: 0x0049F0E8
		public void GenerateWorld(GenerationProgress progress = null)
		{
			Stopwatch stopwatch = new Stopwatch();
			double num = 0.0;
			foreach (GenPass pass in this._passes)
			{
				num += pass.Weight;
			}
			if (progress == null)
			{
				progress = new GenerationProgress();
			}
			WorldGenerator.CurrentGenerationProgress = progress;
			progress.TotalWeight = num;
			foreach (GenPass pass2 in this._passes)
			{
				WorldGen._genRand = new UnifiedRandom(this._seed);
				Main.rand = new UnifiedRandom(this._seed);
				stopwatch.Start();
				progress.Start(pass2.Weight);
				try
				{
					pass2.Apply(progress, this._configuration.GetPassConfiguration(pass2.Name));
				}
				catch (Exception e)
				{
					Utils.ShowFancyErrorMessage(string.Join("\n", new object[]
					{
						Language.GetTextValue("tModLoader.WorldGenError"),
						pass2.Name,
						e
					}), 0, null);
					throw;
				}
				progress.End();
				stopwatch.Reset();
			}
			WorldGenerator.CurrentGenerationProgress = null;
		}

		// Token: 0x04001099 RID: 4249
		internal readonly List<GenPass> _passes = new List<GenPass>();

		// Token: 0x0400109A RID: 4250
		internal double _totalLoadWeight;

		// Token: 0x0400109B RID: 4251
		internal readonly int _seed;

		// Token: 0x0400109C RID: 4252
		private readonly WorldGenConfiguration _configuration;

		// Token: 0x0400109D RID: 4253
		public static GenerationProgress CurrentGenerationProgress;
	}
}
