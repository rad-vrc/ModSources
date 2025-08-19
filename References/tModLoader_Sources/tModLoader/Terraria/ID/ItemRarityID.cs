using System;
using ReLogic.Reflection;

namespace Terraria.ID
{
	/// <summary>Enumerates the values used with Item.rare</summary>
	// Token: 0x0200040F RID: 1039
	public static class ItemRarityID
	{
		/// <summary>Minus thirteen (-13)<br />Master: Fiery Red<br />Flag: item.master</summary>
		// Token: 0x04003D81 RID: 15745
		public const int Master = -13;

		/// <summary>Minus twelve (-12)<br />Expert: Rainbow<br />Flag: item.expert</summary>
		// Token: 0x04003D82 RID: 15746
		public const int Expert = -12;

		/// <summary>Minus eleven (-11)<br />Quest: Amber<br />Flag: item.quest</summary>
		// Token: 0x04003D83 RID: 15747
		public const int Quest = -11;

		/// <summary>Minus one (-1)</summary>
		// Token: 0x04003D84 RID: 15748
		public const int Gray = -1;

		/// <summary>Zero (0)</summary>
		// Token: 0x04003D85 RID: 15749
		public const int White = 0;

		/// <summary>One (1)</summary>
		// Token: 0x04003D86 RID: 15750
		public const int Blue = 1;

		/// <summary>Two (2)</summary>
		// Token: 0x04003D87 RID: 15751
		public const int Green = 2;

		/// <summary>Three (3)</summary>
		// Token: 0x04003D88 RID: 15752
		public const int Orange = 3;

		/// <summary>Four (4)</summary>
		// Token: 0x04003D89 RID: 15753
		public const int LightRed = 4;

		/// <summary>Five (5)</summary>
		// Token: 0x04003D8A RID: 15754
		public const int Pink = 5;

		/// <summary>Six (6)</summary>
		// Token: 0x04003D8B RID: 15755
		public const int LightPurple = 6;

		/// <summary>Seven (7)</summary>
		// Token: 0x04003D8C RID: 15756
		public const int Lime = 7;

		/// <summary>Eight (8)</summary>
		// Token: 0x04003D8D RID: 15757
		public const int Yellow = 8;

		/// <summary>Nine (9)</summary>
		// Token: 0x04003D8E RID: 15758
		public const int Cyan = 9;

		/// <summary>Ten (10)</summary>
		// Token: 0x04003D8F RID: 15759
		public const int Red = 10;

		/// <summary>Eleven (11)</summary>
		// Token: 0x04003D90 RID: 15760
		public const int Purple = 11;

		// Token: 0x04003D91 RID: 15761
		public const int Count = 12;

		// Token: 0x04003D92 RID: 15762
		public static readonly IdDictionary Search = IdDictionary.Create(typeof(ItemRarityID), typeof(int));
	}
}
