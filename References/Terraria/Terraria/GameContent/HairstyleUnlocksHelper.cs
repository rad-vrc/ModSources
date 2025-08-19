using System;
using System.Collections.Generic;

namespace Terraria.GameContent
{
	// Token: 0x020001D2 RID: 466
	public class HairstyleUnlocksHelper
	{
		// Token: 0x06001C06 RID: 7174 RVA: 0x004F1158 File Offset: 0x004EF358
		public void UpdateUnlocks()
		{
			if (!this.ListWarrantsRemake())
			{
				return;
			}
			this.RebuildList();
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x004F116C File Offset: 0x004EF36C
		private bool ListWarrantsRemake()
		{
			bool flag = NPC.downedMartians && !Main.gameMenu;
			bool flag2 = NPC.downedMoonlord && !Main.gameMenu;
			bool flag3 = NPC.downedPlantBoss && !Main.gameMenu;
			bool flag4 = Main.hairWindow && !Main.gameMenu;
			bool gameMenu = Main.gameMenu;
			bool result = false;
			if (this._defeatedMartians != flag || this._defeatedMoonlord != flag2 || this._defeatedPlantera != flag3 || this._isAtStylist != flag4 || this._isAtCharacterCreation != gameMenu)
			{
				result = true;
			}
			this._defeatedMartians = flag;
			this._defeatedMoonlord = flag2;
			this._defeatedPlantera = flag3;
			this._isAtStylist = flag4;
			this._isAtCharacterCreation = gameMenu;
			return result;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x004F1228 File Offset: 0x004EF428
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
		}

		// Token: 0x0400435E RID: 17246
		public List<int> AvailableHairstyles = new List<int>();

		// Token: 0x0400435F RID: 17247
		private bool _defeatedMartians;

		// Token: 0x04004360 RID: 17248
		private bool _defeatedMoonlord;

		// Token: 0x04004361 RID: 17249
		private bool _defeatedPlantera;

		// Token: 0x04004362 RID: 17250
		private bool _isAtStylist;

		// Token: 0x04004363 RID: 17251
		private bool _isAtCharacterCreation;
	}
}
