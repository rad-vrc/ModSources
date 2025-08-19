using System;
using System.Collections.Generic;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020005C0 RID: 1472
	public class PersonalityDatabase
	{
		// Token: 0x060042BA RID: 17082 RVA: 0x005FA1F8 File Offset: 0x005F83F8
		public PersonalityDatabase()
		{
			this._personalityProfiles = new Dictionary<int, PersonalityProfile>();
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x005FA20B File Offset: 0x005F840B
		public void Register(int npcId, IShopPersonalityTrait trait)
		{
			if (!this._personalityProfiles.ContainsKey(npcId))
			{
				this._personalityProfiles[npcId] = new PersonalityProfile();
			}
			this._personalityProfiles[npcId].ShopModifiers.Add(trait);
		}

		// Token: 0x060042BC RID: 17084 RVA: 0x005FA244 File Offset: 0x005F8444
		public void Register(IShopPersonalityTrait trait, params int[] npcIds)
		{
			for (int i = 0; i < npcIds.Length; i++)
			{
				this.Register(trait, new int[]
				{
					npcIds[i]
				});
			}
		}

		// Token: 0x060042BD RID: 17085 RVA: 0x005FA272 File Offset: 0x005F8472
		public bool TryGetProfileByNPCID(int npcId, out PersonalityProfile profile)
		{
			if (this._personalityProfiles.TryGetValue(npcId, out profile))
			{
				return true;
			}
			profile = null;
			return false;
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x005FA28C File Offset: 0x005F848C
		public PersonalityProfile GetOrCreateProfileByNPCID(int npcId)
		{
			PersonalityProfile value;
			if (!this._personalityProfiles.TryGetValue(npcId, out value))
			{
				value = (this._personalityProfiles[npcId] = new PersonalityProfile());
			}
			return value;
		}

		// Token: 0x040059CE RID: 22990
		private Dictionary<int, PersonalityProfile> _personalityProfiles;
	}
}
