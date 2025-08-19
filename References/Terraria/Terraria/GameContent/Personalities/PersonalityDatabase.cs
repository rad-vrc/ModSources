using System;
using System.Collections.Generic;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020003C5 RID: 965
	public class PersonalityDatabase
	{
		// Token: 0x06002A78 RID: 10872 RVA: 0x00599651 File Offset: 0x00597851
		public PersonalityDatabase()
		{
			this._personalityProfiles = new Dictionary<int, PersonalityProfile>();
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x0059966F File Offset: 0x0059786F
		public void Register(int npcId, IShopPersonalityTrait trait)
		{
			if (!this._personalityProfiles.ContainsKey(npcId))
			{
				this._personalityProfiles[npcId] = new PersonalityProfile();
			}
			this._personalityProfiles[npcId].ShopModifiers.Add(trait);
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x005996A8 File Offset: 0x005978A8
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

		// Token: 0x06002A7B RID: 10875 RVA: 0x005996D8 File Offset: 0x005978D8
		public PersonalityProfile GetByNPCID(int npcId)
		{
			PersonalityProfile result;
			if (this._personalityProfiles.TryGetValue(npcId, out result))
			{
				return result;
			}
			return this._trashEntry;
		}

		// Token: 0x04004D37 RID: 19767
		private Dictionary<int, PersonalityProfile> _personalityProfiles;

		// Token: 0x04004D38 RID: 19768
		private PersonalityProfile _trashEntry = new PersonalityProfile();
	}
}
