using System;
using System.IO;

namespace Terraria.DataStructures
{
	// Token: 0x0200073D RID: 1853
	public struct TrackedProjectileReference
	{
		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06004B41 RID: 19265 RVA: 0x00669CD8 File Offset: 0x00667ED8
		// (set) Token: 0x06004B42 RID: 19266 RVA: 0x00669CE0 File Offset: 0x00667EE0
		public int ProjectileLocalIndex { readonly get; private set; }

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06004B43 RID: 19267 RVA: 0x00669CE9 File Offset: 0x00667EE9
		// (set) Token: 0x06004B44 RID: 19268 RVA: 0x00669CF1 File Offset: 0x00667EF1
		public int ProjectileOwnerIndex { readonly get; private set; }

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06004B45 RID: 19269 RVA: 0x00669CFA File Offset: 0x00667EFA
		// (set) Token: 0x06004B46 RID: 19270 RVA: 0x00669D02 File Offset: 0x00667F02
		public int ProjectileIdentity { readonly get; private set; }

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06004B47 RID: 19271 RVA: 0x00669D0B File Offset: 0x00667F0B
		// (set) Token: 0x06004B48 RID: 19272 RVA: 0x00669D13 File Offset: 0x00667F13
		public int ProjectileType { readonly get; private set; }

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06004B49 RID: 19273 RVA: 0x00669D1C File Offset: 0x00667F1C
		// (set) Token: 0x06004B4A RID: 19274 RVA: 0x00669D24 File Offset: 0x00667F24
		public bool IsTrackingSomething { readonly get; private set; }

		// Token: 0x06004B4B RID: 19275 RVA: 0x00669D2D File Offset: 0x00667F2D
		public void Set(Projectile proj)
		{
			this.ProjectileLocalIndex = proj.whoAmI;
			this.ProjectileOwnerIndex = proj.owner;
			this.ProjectileIdentity = proj.identity;
			this.ProjectileType = proj.type;
			this.IsTrackingSomething = true;
		}

		// Token: 0x06004B4C RID: 19276 RVA: 0x00669D66 File Offset: 0x00667F66
		public void Clear()
		{
			this.ProjectileLocalIndex = -1;
			this.ProjectileOwnerIndex = -1;
			this.ProjectileIdentity = -1;
			this.ProjectileType = -1;
			this.IsTrackingSomething = false;
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x00669D8B File Offset: 0x00667F8B
		public void Write(BinaryWriter writer)
		{
			writer.Write((short)this.ProjectileOwnerIndex);
			if (this.ProjectileOwnerIndex != -1)
			{
				writer.Write((short)this.ProjectileIdentity);
				writer.Write((short)this.ProjectileType);
			}
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x00669DBD File Offset: 0x00667FBD
		public bool IsTracking(Projectile proj)
		{
			return proj.whoAmI == this.ProjectileLocalIndex;
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x00669DD0 File Offset: 0x00667FD0
		public void TryReading(BinaryReader reader)
		{
			int num = (int)reader.ReadInt16();
			if (num == -1)
			{
				this.Clear();
				return;
			}
			int expectedIdentity = (int)reader.ReadInt16();
			int expectedType = (int)reader.ReadInt16();
			Projectile projectile = this.FindMatchingProjectile(num, expectedIdentity, expectedType);
			if (projectile == null)
			{
				this.Clear();
				return;
			}
			this.Set(projectile);
		}

		// Token: 0x06004B50 RID: 19280 RVA: 0x00669E18 File Offset: 0x00668018
		private Projectile FindMatchingProjectile(int expectedOwner, int expectedIdentity, int expectedType)
		{
			if (expectedOwner == -1)
			{
				return null;
			}
			for (int i = 0; i < 1000; i++)
			{
				Projectile projectile = Main.projectile[i];
				if (projectile.type == expectedType && projectile.owner == expectedOwner && projectile.identity == expectedIdentity)
				{
					return projectile;
				}
			}
			return null;
		}

		// Token: 0x06004B51 RID: 19281 RVA: 0x00669E64 File Offset: 0x00668064
		public override bool Equals(object obj)
		{
			if (obj is TrackedProjectileReference)
			{
				TrackedProjectileReference other = (TrackedProjectileReference)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06004B52 RID: 19282 RVA: 0x00669E8B File Offset: 0x0066808B
		public bool Equals(TrackedProjectileReference other)
		{
			return this.ProjectileLocalIndex == other.ProjectileLocalIndex && this.ProjectileOwnerIndex == other.ProjectileOwnerIndex && this.ProjectileIdentity == other.ProjectileIdentity && this.ProjectileType == other.ProjectileType;
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x00669ECB File Offset: 0x006680CB
		public override int GetHashCode()
		{
			return ((this.ProjectileLocalIndex * 397 ^ this.ProjectileOwnerIndex) * 397 ^ this.ProjectileIdentity) * 397 ^ this.ProjectileType;
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x00669EFA File Offset: 0x006680FA
		public static bool operator ==(TrackedProjectileReference c1, TrackedProjectileReference c2)
		{
			return c1.Equals(c2);
		}

		// Token: 0x06004B55 RID: 19285 RVA: 0x00669F04 File Offset: 0x00668104
		public static bool operator !=(TrackedProjectileReference c1, TrackedProjectileReference c2)
		{
			return !c1.Equals(c2);
		}
	}
}
