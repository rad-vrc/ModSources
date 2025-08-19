using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used when NPCs or other entities are caught by things like bug nets. Normally converting the caught entity into an item.
	/// </summary>
	// Token: 0x020006E3 RID: 1763
	[NullableContext(1)]
	[Nullable(0)]
	public class EntitySource_Caught : EntitySource_Parent
	{
		/// <summary>
		/// The entity which performed the act of catching.<br /><br />
		/// In vanilla, this is a <see cref="T:Terraria.Player" />.
		/// </summary>
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x0600494A RID: 18762 RVA: 0x0064D766 File Offset: 0x0064B966
		public Entity Catcher { get; }

		// Token: 0x0600494B RID: 18763 RVA: 0x0064D76E File Offset: 0x0064B96E
		public EntitySource_Caught(Entity catcher, Entity caught, [Nullable(2)] string context = null) : base(caught, context)
		{
			this.Catcher = catcher;
		}
	}
}
