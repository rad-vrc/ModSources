using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Terraria.IO
{
	// Token: 0x020003E5 RID: 997
	[DebuggerDisplay("Version {Major}.{Minor}")]
	public struct ResourcePackVersion : IComparable, IComparable<ResourcePackVersion>
	{
		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06003445 RID: 13381 RVA: 0x005578F4 File Offset: 0x00555AF4
		// (set) Token: 0x06003446 RID: 13382 RVA: 0x005578FC File Offset: 0x00555AFC
		[JsonProperty("major")]
		public int Major { readonly get; private set; }

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06003447 RID: 13383 RVA: 0x00557905 File Offset: 0x00555B05
		// (set) Token: 0x06003448 RID: 13384 RVA: 0x0055790D File Offset: 0x00555B0D
		[JsonProperty("minor")]
		public int Minor { readonly get; private set; }

		// Token: 0x06003449 RID: 13385 RVA: 0x00557918 File Offset: 0x00555B18
		public static ResourcePackVersion Create(int major, int minor)
		{
			return new ResourcePackVersion
			{
				Major = major,
				Minor = minor
			};
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x0055793E File Offset: 0x00555B3E
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

		// Token: 0x0600344B RID: 13387 RVA: 0x0055796C File Offset: 0x00555B6C
		public int CompareTo(ResourcePackVersion other)
		{
			int num = this.Major.CompareTo(other.Major);
			if (num != 0)
			{
				return num;
			}
			return this.Minor.CompareTo(other.Minor);
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x005579A9 File Offset: 0x00555BA9
		public static bool operator ==(ResourcePackVersion lhs, ResourcePackVersion rhs)
		{
			return lhs.CompareTo(rhs) == 0;
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x005579B6 File Offset: 0x00555BB6
		public static bool operator !=(ResourcePackVersion lhs, ResourcePackVersion rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x005579C2 File Offset: 0x00555BC2
		public static bool operator <(ResourcePackVersion lhs, ResourcePackVersion rhs)
		{
			return lhs.CompareTo(rhs) < 0;
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x005579CF File Offset: 0x00555BCF
		public static bool operator >(ResourcePackVersion lhs, ResourcePackVersion rhs)
		{
			return lhs.CompareTo(rhs) > 0;
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x005579DC File Offset: 0x00555BDC
		public override bool Equals(object obj)
		{
			return obj is ResourcePackVersion && this.CompareTo((ResourcePackVersion)obj) == 0;
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x005579F8 File Offset: 0x00555BF8
		public override int GetHashCode()
		{
			long num3 = (long)this.Major;
			long num2 = (long)this.Minor;
			return (num3 << 32 | num2).GetHashCode();
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x00557A24 File Offset: 0x00555C24
		public string GetFormattedVersion()
		{
			return this.Major.ToString() + "." + this.Minor.ToString();
		}
	}
}
