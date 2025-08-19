using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Terraria.IO
{
	// Token: 0x020000D5 RID: 213
	[DebuggerDisplay("Version {Major}.{Minor}")]
	public struct ResourcePackVersion : IComparable, IComparable<ResourcePackVersion>
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x004AE574 File Offset: 0x004AC774
		// (set) Token: 0x060014BF RID: 5311 RVA: 0x004AE57C File Offset: 0x004AC77C
		[JsonProperty("major")]
		public int Major { get; private set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x004AE585 File Offset: 0x004AC785
		// (set) Token: 0x060014C1 RID: 5313 RVA: 0x004AE58D File Offset: 0x004AC78D
		[JsonProperty("minor")]
		public int Minor { get; private set; }

		// Token: 0x060014C2 RID: 5314 RVA: 0x004AE598 File Offset: 0x004AC798
		public static ResourcePackVersion Create(int major, int minor)
		{
			return new ResourcePackVersion
			{
				Major = major,
				Minor = minor
			};
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x004AE5BE File Offset: 0x004AC7BE
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is ResourcePackVersion))
			{
				throw new ArgumentException("A RatingInformation object is required for comparison.", "obj");
			}
			return this.CompareTo((ResourcePackVersion)obj);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x004AE5EC File Offset: 0x004AC7EC
		public int CompareTo(ResourcePackVersion other)
		{
			int num = this.Major.CompareTo(other.Major);
			if (num != 0)
			{
				return num;
			}
			return this.Minor.CompareTo(other.Minor);
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x004AE629 File Offset: 0x004AC829
		public static bool operator ==(ResourcePackVersion lhs, ResourcePackVersion rhs)
		{
			return lhs.CompareTo(rhs) == 0;
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x004AE636 File Offset: 0x004AC836
		public static bool operator !=(ResourcePackVersion lhs, ResourcePackVersion rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x004AE642 File Offset: 0x004AC842
		public static bool operator <(ResourcePackVersion lhs, ResourcePackVersion rhs)
		{
			return lhs.CompareTo(rhs) < 0;
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x004AE64F File Offset: 0x004AC84F
		public static bool operator >(ResourcePackVersion lhs, ResourcePackVersion rhs)
		{
			return lhs.CompareTo(rhs) > 0;
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x004AE65C File Offset: 0x004AC85C
		public override bool Equals(object obj)
		{
			return obj is ResourcePackVersion && this.CompareTo((ResourcePackVersion)obj) == 0;
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x004AE678 File Offset: 0x004AC878
		public override int GetHashCode()
		{
			long num = (long)this.Major;
			long num2 = (long)this.Minor;
			return (num << 32 | num2).GetHashCode();
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x004AE6A1 File Offset: 0x004AC8A1
		public string GetFormattedVersion()
		{
			return this.Major + "." + this.Minor;
		}
	}
}
