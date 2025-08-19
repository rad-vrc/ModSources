using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace Terraria.GameContent
{
	// Token: 0x0200049C RID: 1180
	public class HairstyleUnlocksHelper
	{
		// Token: 0x06003933 RID: 14643 RVA: 0x00597395 File Offset: 0x00595595
		public void UpdateUnlocks()
		{
			if (this.ListWarrantsRemake())
			{
				this.RebuildList();
			}
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x005973A8 File Offset: 0x005955A8
		private bool ListWarrantsRemake()
		{
			bool flag = NPC.downedMartians && !Main.gameMenu;
			bool flag2 = NPC.downedMoonlord && !Main.gameMenu;
			bool flag3 = NPC.downedPlantBoss && !Main.gameMenu;
			bool flag4 = Main.hairWindow && !Main.gameMenu;
			bool gameMenu = Main.gameMenu;
			if (this._defeatedMartians == flag && this._defeatedMoonlord == flag2 && this._defeatedPlantera == flag3 && this._isAtStylist == flag4)
			{
				bool isAtCharacterCreation = this._isAtCharacterCreation;
			}
			this._defeatedMartians = flag;
			this._defeatedMoonlord = flag2;
			this._defeatedPlantera = flag3;
			this._isAtStylist = flag4;
			this._isAtCharacterCreation = gameMenu;
			return true;
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x0059745C File Offset: 0x0059565C
		private void RebuildList()
		{
			List<int> availableHairstyles = this.AvailableHairstyles;
			availableHairstyles.Clear();
			if (this._isAtCharacterCreation || this._isAtStylist)
			{
				for (int i = 0; i < 51; i++)
				{
					availableHairstyles.Add(i);
				}
				availableHairstyles.Add(136);
				availableHairstyles.Add(137);
				availableHairstyles.Add(138);
				availableHairstyles.Add(139);
				availableHairstyles.Add(140);
				availableHairstyles.Add(141);
				availableHairstyles.Add(142);
				availableHairstyles.Add(143);
				availableHairstyles.Add(144);
				availableHairstyles.Add(147);
				availableHairstyles.Add(148);
				availableHairstyles.Add(149);
				availableHairstyles.Add(150);
				availableHairstyles.Add(151);
				availableHairstyles.Add(154);
				availableHairstyles.Add(155);
				availableHairstyles.Add(157);
				availableHairstyles.Add(158);
				availableHairstyles.Add(161);
			}
			for (int j = 51; j < 123; j++)
			{
				availableHairstyles.Add(j);
			}
			availableHairstyles.Add(134);
			availableHairstyles.Add(135);
			availableHairstyles.Add(146);
			availableHairstyles.Add(152);
			availableHairstyles.Add(153);
			availableHairstyles.Add(156);
			availableHairstyles.Add(159);
			availableHairstyles.Add(160);
			if (this._defeatedPlantera)
			{
				availableHairstyles.Add(162);
				availableHairstyles.Add(164);
				availableHairstyles.Add(163);
				availableHairstyles.Add(145);
			}
			if (this._defeatedMartians)
			{
				availableHairstyles.AddRange(new int[]
				{
					132,
					131,
					130,
					129,
					128,
					127,
					126,
					125,
					124,
					123
				});
			}
			if (this._defeatedMartians && this._defeatedMoonlord)
			{
				availableHairstyles.Add(133);
			}
			HairLoader.UpdateUnlocks(this, this._isAtCharacterCreation);
		}

		// Token: 0x04005250 RID: 21072
		public List<int> AvailableHairstyles = new List<int>();

		// Token: 0x04005251 RID: 21073
		private bool _defeatedMartians;

		// Token: 0x04005252 RID: 21074
		private bool _defeatedMoonlord;

		// Token: 0x04005253 RID: 21075
		private bool _defeatedPlantera;

		// Token: 0x04005254 RID: 21076
		private bool _isAtStylist;

		// Token: 0x04005255 RID: 21077
		private bool _isAtCharacterCreation;
	}
}
