using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Combination of <see cref="T:Terraria.DataStructures.IEntitySource_OnHurt" /> and <see cref="T:Terraria.DataStructures.IEntitySource_WithStatsFromItem" /> <br /><br />
	///
	/// Used for on-hurt accessories in vanilla (Star Cloak, Brain of Confusion etc). <br />
	/// Modders should be aware that it is <b>not</b> necessary to use this instead of <see cref="T:Terraria.DataStructures.EntitySource_OnHurt" /> <br />
	/// The combination with <see cref="T:Terraria.DataStructures.IEntitySource_WithStatsFromItem" /> will only have an effect if the item has damage/crit/armor pen stats which vanilla accessories do not <br />
	/// Some mods may wish to transfer other accessory bonuses from the item to spawned projectiles.
	/// </summary>
	// Token: 0x020006ED RID: 1773
	[NullableContext(1)]
	[Nullable(0)]
	public class EntitySource_ItemUse_OnHurt : EntitySource_ItemUse, IEntitySource_OnHurt
	{
		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x0600495F RID: 18783 RVA: 0x0064D84C File Offset: 0x0064BA4C
		[Nullable(2)]
		public Entity Attacker { [NullableContext(2)] get; }

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06004960 RID: 18784 RVA: 0x0064D854 File Offset: 0x0064BA54
		public Entity Victim
		{
			get
			{
				return base.Entity;
			}
		}

		// Token: 0x06004961 RID: 18785 RVA: 0x0064D85C File Offset: 0x0064BA5C
		public EntitySource_ItemUse_OnHurt(Player player, Item item, [Nullable(2)] Entity attacker, [Nullable(2)] string context = null) : base(player, item, context)
		{
			this.Attacker = attacker;
		}
	}
}
