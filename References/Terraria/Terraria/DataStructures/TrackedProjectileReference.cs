using System;
using System.IO;

namespace Terraria.DataStructures
{
	// Token: 0x02000417 RID: 1047
	public struct TrackedProjectileReference
	{
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x0059E336 File Offset: 0x0059C536
		// (set) Token: 0x06002B64 RID: 11108 RVA: 0x0059E33E File Offset: 0x0059C53E
		public int ProjectileLocalIndex { get; private set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06002B65 RID: 11109 RVA: 0x0059E347 File Offset: 0x0059C547
		// (set) Token: 0x06002B66 RID: 11110 RVA: 0x0059E34F File Offset: 0x0059C54F
		public int ProjectileOwnerIndex { get; private set; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06002B67 RID: 11111 RVA: 0x0059E358 File Offset: 0x0059C558
		// (set) Token: 0x06002B68 RID: 11112 RVA: 0x0059E360 File Offset: 0x0059C560
		public int ProjectileIdentity { get; private set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06002B69 RID: 11113 RVA: 0x0059E369 File Offset: 0x0059C569
		// (set) Token: 0x06002B6A RID: 11114 RVA: 0x0059E371 File Offset: 0x0059C571
		public int ProjectileType { get; private set; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06002B6B RID: 11115 RVA: 0x0059E37A File Offset: 0x0059C57A
		// (set) Token: 0x06002B6C RID: 11116 RVA: 0x0059E382 File Offset: 0x0059C582
		public bool IsTrackingSomething { get; private set; }

		// Token: 0x06002B6D RID: 11117 RVA: 0x0059E38B File Offset: 0x0059C58B
		public void Set(Projectile proj)
		{
			this.ProjectileLocalIndex = proj.whoAmI;
			this.ProjectileOwnerIndex = proj.owner;
			this.ProjectileIdentity = proj.identity;
			this.ProjectileType = proj.type;
			this.IsTrackingSomething = true;
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x0059E3C4 File Offset: 0x0059C5C4
		public void Clear()
		{
			this.ProjectileLocalIndex = -1;
			this.ProjectileOwnerIndex = -1;
			this.ProjectileIdentity = -1;
			this.ProjectileType = -1;
			this.IsTrackingSomething = false;
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x0059E3E9 File Offset: 0x0059C5E9
		public void Write(BinaryWriter writer)
		{
			writer.Write((short)this.ProjectileOwnerIndex);
			if (this.ProjectileOwnerIndex == -1)
			{
				return;
			}
			writer.Write((short)this.ProjectileIdentity);
			writer.Write((short)this.ProjectileType);
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x0059E41C File Offset: 0x0059C61C
		public bool IsTracking(Projectile proj)
		{
			return proj.whoAmI == this.ProjectileLocalIndex;
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x0059E42C File Offset: 0x0059C62C
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

		// Token: 0x06002B72 RID: 11122 RVA: 0x0059E474 File Offset: 0x0059C674
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

		// Token: 0x06002B73 RID: 11123 RVA: 0x0059E4C0 File Offset: 0x0059C6C0
		public override bool Equals(object obj)
		{
			if (!(obj is TrackedProjectileReference))
			{
				return false;
			}
			TrackedProjectileReference other = (TrackedProjectileReference)obj;
			return this.Equals(other);
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x0059E4E5 File Offset: 0x0059C6E5
		public bool Equals(TrackedProjectileReference other)
		{
			return this.ProjectileLocalIndex == other.ProjectileLocalIndex && this.ProjectileOwnerIndex == other.ProjectileOwnerIndex && this.ProjectileIdentity == other.ProjectileIdentity && this.ProjectileType == other.ProjectileType;
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x0059E525 File Offset: 0x0059C725
		public override int GetHashCode()
		{
			return ((this.ProjectileLocalIndex * 397 ^ this.ProjectileOwnerIndex) * 397 ^ this.ProjectileIdentity) * 397 ^ this.ProjectileType;
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x0059E554 File Offset: 0x0059C754
		public static bool operator ==(TrackedProjectileReference c1, TrackedProjectileReference c2)
		{
			return c1.Equals(c2);
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x0059E55E File Offset: 0x0059C75E
		public static bool operator !=(TrackedProjectileReference c1, TrackedProjectileReference c2)
		{
			return !c1.Equals(c2);
		}
	}
}
