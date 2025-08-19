using System;
using Terraria.DataStructures;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002F9 RID: 761
	internal class FrostyPlayer : ModPlayer
	{
		// Token: 0x06002E58 RID: 11864 RVA: 0x0053165B File Offset: 0x0052F85B
		public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
		{
			if (drawInfo.drawPlayer.body == EquipLoader.GetEquipSlot(base.Mod, "Frosty_Hoodie", EquipType.Body))
			{
				drawInfo.colorArmorBody = drawInfo.colorUnderShirt;
			}
		}
	}
}
