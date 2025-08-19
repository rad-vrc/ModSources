using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using log4net;
using Microsoft.Xna.Framework.Content;

namespace Terraria.ModLoader.Engine
{
	// Token: 0x020002BC RID: 700
	internal class TMLContentManager : ContentManager
	{
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06002D58 RID: 11608 RVA: 0x0052DC66 File Offset: 0x0052BE66
		public IEnumerable<string> RootDirectories
		{
			get
			{
				if (this.overrideContentManager != null)
				{
					yield return this.overrideContentManager.RootDirectory;
				}
				yield return base.RootDirectory;
				yield break;
			}
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x0052DC76 File Offset: 0x0052BE76
		public TMLContentManager(IServiceProvider serviceProvider, string rootDirectory, TMLContentManager overrideContentManager) : base(serviceProvider, rootDirectory)
		{
			TMLContentManager.TryFixFileCasings(rootDirectory);
			this.overrideContentManager = overrideContentManager;
			this.<.ctor>g__CacheImagePaths|5_0(rootDirectory);
			if (overrideContentManager != null)
			{
				this.<.ctor>g__CacheImagePaths|5_0(overrideContentManager.RootDirectory);
			}
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x0052DCB4 File Offset: 0x0052BEB4
		protected override Stream OpenStream(string assetName)
		{
			if (!assetName.StartsWith("tmod:"))
			{
				if (this.overrideContentManager != null && File.Exists(Path.Combine(this.overrideContentManager.RootDirectory, assetName + ".xnb")))
				{
					try
					{
						using (new Logging.QuietExceptionHandle())
						{
							return this.overrideContentManager.OpenStream(assetName);
						}
					}
					catch
					{
					}
				}
				return base.OpenStream(assetName);
			}
			if (!assetName.EndsWith(".xnb"))
			{
				assetName += ".xnb";
			}
			return ModContent.OpenRead(assetName, false);
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x0052DD60 File Offset: 0x0052BF60
		public override T Load<T>(string assetName)
		{
			if (assetName.StartsWith("tmod:"))
			{
				return base.ReadAsset<T>(assetName, null);
			}
			this.loadedAssets++;
			if (this.loadedAssets % 1000 == 0)
			{
				ILog terraria = Logging.Terraria;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(22, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Loaded ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.loadedAssets);
				defaultInterpolatedStringHandler.AppendLiteral(" vanilla assets");
				terraria.Info(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return base.Load<T>(assetName);
		}

		/// <summary> Returns a path to the provided relative asset path, prioritizing overrides in the alternate content manager. Throws exceptions on failure. </summary>
		// Token: 0x06002D5C RID: 11612 RVA: 0x0052DDE8 File Offset: 0x0052BFE8
		public string GetPath(string asset)
		{
			string result;
			if (!this.TryGetPath(asset, out result))
			{
				throw new FileNotFoundException("Unable to find asset '" + asset + "'.");
			}
			return result;
		}

		/// <summary> Safely attempts to get a path to the provided relative asset path, prioritizing overrides in the alternate content manager. </summary>
		// Token: 0x06002D5D RID: 11613 RVA: 0x0052DE18 File Offset: 0x0052C018
		public bool TryGetPath(string asset, out string result)
		{
			if (this.overrideContentManager != null && this.overrideContentManager.TryGetPath(asset, out result))
			{
				return true;
			}
			string path = Path.Combine(base.RootDirectory, asset);
			result = (File.Exists(path) ? path : null);
			return result != null;
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x0052DE5E File Offset: 0x0052C05E
		public bool ImageExists(string assetName)
		{
			return this.ExistingImages.Contains(assetName);
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x0052DE6C File Offset: 0x0052C06C
		private static void TryFixFileCasings(string rootDirectory)
		{
			string[] array = new string[]
			{
				"Images/NPC_517.xnb",
				"Images/Gore_240.xnb",
				"Images/Projectile_179.xnb",
				"Images/Projectile_189.xnb",
				"Images/Projectile_618.xnb",
				"Images/Tiles_650.xnb",
				"Images/Item_2648.xnb"
			};
			int i = 0;
			while (i < array.Length)
			{
				string problematicAsset = array[i];
				string expectedName = Path.GetFileName(problematicAsset);
				string expectedFullPath = Path.Combine(rootDirectory, problematicAsset);
				FileInfo faultyAssetInfo = new FileInfo(Path.Combine(rootDirectory, problematicAsset));
				string actualFullPath;
				if (faultyAssetInfo.Exists)
				{
					FileSystemInfo assetInfo = faultyAssetInfo.Directory.EnumerateFileSystemInfos(faultyAssetInfo.Name).First<FileSystemInfo>();
					if (!(expectedName == assetInfo.Name))
					{
						actualFullPath = assetInfo.FullName;
						goto IL_101;
					}
				}
				else
				{
					FileSystemInfo assetInfo2 = faultyAssetInfo.Directory.EnumerateFileSystemInfos().FirstOrDefault((FileSystemInfo p) => p.Name.Equals(expectedName, StringComparison.InvariantCultureIgnoreCase));
					if (assetInfo2 != null)
					{
						actualFullPath = assetInfo2.FullName;
						goto IL_101;
					}
					Logging.tML.Info("An expected vanilla asset is missing: (from " + rootDirectory + ") " + problematicAsset);
				}
				IL_16C:
				i++;
				continue;
				IL_101:
				string relativeActualPath = Path.GetRelativePath(rootDirectory, actualFullPath);
				ILog tML = Logging.tML;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(59, 3);
				defaultInterpolatedStringHandler.AppendLiteral("Found vanilla asset with wrong case, renaming: (from ");
				defaultInterpolatedStringHandler.AppendFormatted(rootDirectory);
				defaultInterpolatedStringHandler.AppendLiteral(") ");
				defaultInterpolatedStringHandler.AppendFormatted(relativeActualPath);
				defaultInterpolatedStringHandler.AppendLiteral(" -> ");
				defaultInterpolatedStringHandler.AppendFormatted(problematicAsset);
				tML.Info(defaultInterpolatedStringHandler.ToStringAndClear());
				File.Move(actualFullPath, expectedFullPath);
				goto IL_16C;
			}
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x0052DFF4 File Offset: 0x0052C1F4
		[CompilerGenerated]
		private void <.ctor>g__CacheImagePaths|5_0(string path)
		{
			string basePath = Path.Combine(path, "Images");
			foreach (string file in Directory.EnumerateFiles(basePath, "*.xnb", SearchOption.AllDirectories))
			{
				this.ExistingImages.Add(Path.GetFileNameWithoutExtension(file.Remove(0, basePath.Length + 1)));
			}
		}

		// Token: 0x04001C4E RID: 7246
		internal readonly TMLContentManager overrideContentManager;

		// Token: 0x04001C4F RID: 7247
		private readonly HashSet<string> ExistingImages = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x04001C50 RID: 7248
		private int loadedAssets;
	}
}
