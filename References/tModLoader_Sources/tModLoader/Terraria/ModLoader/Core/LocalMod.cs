using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader.Core
{
	// Token: 0x0200035F RID: 863
	[DebuggerDisplay("{DetailedInfo}")]
	internal class LocalMod
	{
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06002FEE RID: 12270 RVA: 0x00537D03 File Offset: 0x00535F03
		public string Name
		{
			get
			{
				return this.modFile.Name;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06002FEF RID: 12271 RVA: 0x00537D10 File Offset: 0x00535F10
		public string DisplayName
		{
			get
			{
				if (!string.IsNullOrEmpty(this.properties.displayName))
				{
					return this.properties.displayName;
				}
				return this.Name;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06002FF0 RID: 12272 RVA: 0x00537D36 File Offset: 0x00535F36
		public Version Version
		{
			get
			{
				return this.properties.version;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06002FF1 RID: 12273 RVA: 0x00537D43 File Offset: 0x00535F43
		public Version tModLoaderVersion
		{
			get
			{
				return this.properties.buildVersion;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06002FF2 RID: 12274 RVA: 0x00537D50 File Offset: 0x00535F50
		// (set) Token: 0x06002FF3 RID: 12275 RVA: 0x00537D5D File Offset: 0x00535F5D
		public bool Enabled
		{
			get
			{
				return ModLoader.IsEnabled(this.Name);
			}
			set
			{
				ModLoader.SetModEnabled(this.Name, value);
			}
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x00537D6B File Offset: 0x00535F6B
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06002FF5 RID: 12277 RVA: 0x00537D74 File Offset: 0x00535F74
		public string DetailedInfo
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 4);
				defaultInterpolatedStringHandler.AppendFormatted(this.Name);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted<Version>(this.Version);
				defaultInterpolatedStringHandler.AppendLiteral(" for tML ");
				defaultInterpolatedStringHandler.AppendFormatted<Version>(this.tModLoaderVersion);
				defaultInterpolatedStringHandler.AppendLiteral(" from ");
				defaultInterpolatedStringHandler.AppendFormatted<ModLocation>(this.location);
				return defaultInterpolatedStringHandler.ToStringAndClear() + ((Path.GetFileNameWithoutExtension(this.modFile.path) != this.Name) ? (" (" + Path.GetFileName(this.modFile.path) + ")") : "");
			}
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x00537E32 File Offset: 0x00536032
		public LocalMod(ModLocation location, TmodFile modFile, BuildProperties properties)
		{
			this.location = location;
			this.modFile = modFile;
			this.properties = properties;
			this.DisplayNameClean = Utils.CleanChatTags(this.DisplayName);
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x00537E60 File Offset: 0x00536060
		public LocalMod(ModLocation location, TmodFile modFile) : this(location, modFile, BuildProperties.ReadModFile(modFile))
		{
		}

		// Token: 0x04001CD4 RID: 7380
		public readonly ModLocation location;

		// Token: 0x04001CD5 RID: 7381
		public readonly TmodFile modFile;

		// Token: 0x04001CD6 RID: 7382
		public readonly BuildProperties properties;

		// Token: 0x04001CD7 RID: 7383
		public DateTime lastModified;

		// Token: 0x04001CD8 RID: 7384
		public readonly string DisplayNameClean;
	}
}
