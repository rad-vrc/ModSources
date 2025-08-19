using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ReLogic.Content.Sources;

namespace Terraria.ModLoader.Assets
{
	// Token: 0x020003C4 RID: 964
	public sealed class AssemblyResourcesContentSource : ContentSource
	{
		// Token: 0x0600331B RID: 13083 RVA: 0x00548F32 File Offset: 0x00547132
		[Obsolete]
		private AssemblyResourcesContentSource(Assembly assembly, string rootPath) : this(assembly, rootPath, null)
		{
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x00548F40 File Offset: 0x00547140
		public AssemblyResourcesContentSource(Assembly assembly, string rootPath = null, IEnumerable<string> excludedStartingPaths = null)
		{
			this.assembly = assembly;
			if (excludedStartingPaths == null)
			{
				excludedStartingPaths = Enumerable.Empty<string>();
			}
			IEnumerable<string> resourceNames = assembly.GetManifestResourceNames();
			using (IEnumerator<string> enumerator = (excludedStartingPaths ?? Enumerable.Empty<string>()).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string startingPath = enumerator.Current;
					resourceNames = from p in resourceNames
					where !p.StartsWith(startingPath)
					select p;
				}
			}
			if (rootPath != null)
			{
				resourceNames = from p in resourceNames
				where p.StartsWith(rootPath)
				select p.Substring(rootPath.Length);
			}
			this.rootPath = (rootPath ?? "");
			base.SetAssetNames(resourceNames);
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x0054901C File Offset: 0x0054721C
		public override Stream OpenStream(string assetName)
		{
			return this.assembly.GetManifestResourceStream(this.rootPath + assetName);
		}

		// Token: 0x04001DF2 RID: 7666
		private readonly string rootPath;

		// Token: 0x04001DF3 RID: 7667
		private readonly Assembly assembly;
	}
}
