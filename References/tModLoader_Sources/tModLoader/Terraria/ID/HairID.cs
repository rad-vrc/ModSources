using System;
using ReLogic.Reflection;
using Terraria.ModLoader;

namespace Terraria.ID
{
	// Token: 0x02000408 RID: 1032
	public class HairID
	{
		// Token: 0x040027DF RID: 10207
		public static readonly int Count = 165;

		// Token: 0x040027E0 RID: 10208
		public static IdDictionary Search = IdDictionary.Create<HairID, int>();

		// Token: 0x02000B53 RID: 2899
		public class Sets
		{
			// Token: 0x04007420 RID: 29728
			public static SetFactory Factory = new SetFactory(HairLoader.Count, "HairID", HairID.Search);

			/// <summary>
			/// If <see langword="true" /> for a given <strong><see cref="F:Terraria.Player.hair" /></strong> value, then that hair will additionally draw behind the player's back using <see cref="F:Terraria.DataStructures.PlayerDrawSet.hairBackFrame" />. Set this to prevent long hair from drawing in front of capes.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			/// <remarks>
			/// Back hair is drawn using <see cref="F:Terraria.DataStructures.PlayerDrawLayers.HairBack" />.
			/// </remarks>
			// Token: 0x04007421 RID: 29729
			public static bool[] DrawBackHair = HairID.Sets.Factory.CreateBoolSet(new int[]
			{
				51,
				52,
				53,
				54,
				55,
				64,
				65,
				66,
				67,
				68,
				69,
				70,
				71,
				72,
				73,
				78,
				79,
				80,
				81,
				82,
				83,
				84,
				85,
				86,
				87,
				90,
				91,
				92,
				93,
				94,
				95,
				96,
				97,
				98,
				99,
				101,
				102,
				103,
				105,
				106,
				107,
				108,
				109,
				110,
				111,
				113,
				114,
				115,
				133,
				134,
				146,
				162,
				6
			});
		}
	}
}
