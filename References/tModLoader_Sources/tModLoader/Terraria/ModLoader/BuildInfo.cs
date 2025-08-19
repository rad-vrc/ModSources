using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader
{
	// Token: 0x0200014D RID: 333
	public static class BuildInfo
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x004CF7EE File Offset: 0x004CD9EE
		public static bool IsStable
		{
			get
			{
				return BuildInfo.Purpose == BuildInfo.BuildPurpose.Stable;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x004CF7F8 File Offset: 0x004CD9F8
		public static bool IsPreview
		{
			get
			{
				return BuildInfo.Purpose == BuildInfo.BuildPurpose.Preview;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x004CF802 File Offset: 0x004CDA02
		public static bool IsDev
		{
			get
			{
				return BuildInfo.Purpose == BuildInfo.BuildPurpose.Dev;
			}
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x004CF80C File Offset: 0x004CDA0C
		static BuildInfo()
		{
			string[] array = BuildInfo.BuildIdentifier.Substring(BuildInfo.BuildIdentifier.IndexOf('+') + 1).Split('|', StringSplitOptions.None);
			int i = 0;
			BuildInfo.tMLVersion = new Version(array[i++]);
			BuildInfo.stableVersion = new Version(array[i++]);
			BuildInfo.BranchName = array[i++];
			Enum.TryParse<BuildInfo.BuildPurpose>(array[i++], true, out BuildInfo.Purpose);
			BuildInfo.CommitSHA = array[i++];
			BuildInfo.BuildDate = DateTime.FromBinary(long.Parse(array[i++])).ToLocalTime();
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 1);
			defaultInterpolatedStringHandler.AppendLiteral("tModLoader v");
			defaultInterpolatedStringHandler.AppendFormatted<Version>(BuildInfo.tMLVersion);
			BuildInfo.versionedName = defaultInterpolatedStringHandler.ToStringAndClear();
			string[] branchNameBlacklist = new string[]
			{
				"unknown",
				"stable",
				"preview",
				"1.4.3-Legacy"
			};
			if (!string.IsNullOrEmpty(BuildInfo.BranchName) && !branchNameBlacklist.Contains(BuildInfo.BranchName))
			{
				BuildInfo.versionedName = BuildInfo.versionedName + " " + BuildInfo.BranchName;
			}
			if (BuildInfo.Purpose != BuildInfo.BuildPurpose.Stable)
			{
				string str = BuildInfo.versionedName;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted<BuildInfo.BuildPurpose>(BuildInfo.Purpose);
				BuildInfo.versionedName = str + defaultInterpolatedStringHandler.ToStringAndClear();
			}
			BuildInfo.versionTag = BuildInfo.versionedName.Substring("tModLoader ".Length).Replace(' ', '-').ToLower();
			BuildInfo.versionedNameDevFriendly = BuildInfo.versionedName;
			if (BuildInfo.CommitSHA != "unknown")
			{
				BuildInfo.versionedNameDevFriendly = BuildInfo.versionedNameDevFriendly + " " + BuildInfo.CommitSHA.Substring(0, 8);
			}
			string str2 = BuildInfo.versionedNameDevFriendly;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
			defaultInterpolatedStringHandler.AppendLiteral(", built ");
			defaultInterpolatedStringHandler.AppendFormatted<DateTime>(BuildInfo.BuildDate, "g");
			BuildInfo.versionedNameDevFriendly = str2 + defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x040014B1 RID: 5297
		public static readonly string BuildIdentifier = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

		// Token: 0x040014B2 RID: 5298
		public static readonly Version tMLVersion;

		/// <summary>The Major.Minor version of the stable release at the time this build was created.</summary>
		// Token: 0x040014B3 RID: 5299
		public static readonly Version stableVersion;

		// Token: 0x040014B4 RID: 5300
		public static readonly BuildInfo.BuildPurpose Purpose;

		// Token: 0x040014B5 RID: 5301
		public static readonly string BranchName;

		// Token: 0x040014B6 RID: 5302
		public static readonly string CommitSHA;

		/// <summary>
		/// local time, for display purposes
		/// </summary>
		// Token: 0x040014B7 RID: 5303
		public static readonly DateTime BuildDate;

		// Token: 0x040014B8 RID: 5304
		public static readonly string versionedName;

		// Token: 0x040014B9 RID: 5305
		public static readonly string versionTag;

		// Token: 0x040014BA RID: 5306
		public static readonly string versionedNameDevFriendly;

		// Token: 0x020008BB RID: 2235
		public enum BuildPurpose
		{
			// Token: 0x04006A55 RID: 27221
			Dev,
			// Token: 0x04006A56 RID: 27222
			Preview,
			// Token: 0x04006A57 RID: 27223
			Stable
		}
	}
}
