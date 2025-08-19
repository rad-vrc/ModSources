using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.DataStructures
{
	/// <summary>
	/// <b>Unused:</b> Replaced by <see cref="F:Terraria.ID.NPCID.Sets.SpecificDebuffImmunity" />, <see cref="F:Terraria.ID.NPCID.Sets.ImmuneToAllBuffs" />, and <see cref="F:Terraria.ID.NPCID.Sets.ImmuneToRegularBuffs" /><br /><br />
	/// Determines the default debuff immunities of an <see cref="T:Terraria.NPC" />.
	/// </summary>
	// Token: 0x0200071E RID: 1822
	public class NPCDebuffImmunityData
	{
		/// <summary>
		/// Sets up <see cref="F:Terraria.NPC.buffImmune" /> to be immune to the stored buffs.
		/// </summary>
		/// <param name="npc">The NPC to apply immunities to.</param>
		// Token: 0x060049E9 RID: 18921 RVA: 0x0064E7FC File Offset: 0x0064C9FC
		public void ApplyToNPC(NPC npc)
		{
			if (this.ImmuneToWhips || this.ImmuneToAllBuffsThatAreNotWhips)
			{
				for (int i = 1; i < BuffLoader.BuffCount; i++)
				{
					bool flag = BuffID.Sets.IsATagBuff[i];
					bool flag2 = false;
					flag2 |= (flag && this.ImmuneToWhips);
					flag2 |= (!flag && this.ImmuneToAllBuffsThatAreNotWhips);
					npc.buffImmune[i] = flag2;
				}
			}
			if (this.SpecificallyImmuneTo != null)
			{
				for (int j = 0; j < this.SpecificallyImmuneTo.Length; j++)
				{
					int num = this.SpecificallyImmuneTo[j];
					npc.buffImmune[num] = true;
				}
			}
		}

		/// <summary>
		/// If <see langword="true" />, this NPC type (<see cref="F:Terraria.NPC.type" />) will be immune to all tag debuffs (<see cref="F:Terraria.ID.BuffID.Sets.IsATagBuff" />).
		/// </summary>
		// Token: 0x04005F17 RID: 24343
		public bool ImmuneToWhips;

		/// <summary>
		/// If <see langword="true" />, this NPC type (<see cref="F:Terraria.NPC.type" />) will be immune to all non-tag debuffs (<see cref="F:Terraria.ID.BuffID.Sets.IsATagBuff" />).
		/// </summary>
		// Token: 0x04005F18 RID: 24344
		public bool ImmuneToAllBuffsThatAreNotWhips;

		/// <summary>
		/// This NPC type (<see cref="F:Terraria.NPC.type" />) will be immune to all <see cref="T:Terraria.ID.BuffID" />s in this array.
		/// </summary>
		// Token: 0x04005F19 RID: 24345
		public int[] SpecificallyImmuneTo;
	}
}
