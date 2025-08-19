using System;
using Terraria.DataStructures;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200033A RID: 826
	internal class xAqultPlayer : ModPlayer
	{
		// Token: 0x06002F12 RID: 12050 RVA: 0x00533150 File Offset: 0x00531350
		public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
		{
			if (base.Player.head == EquipLoader.GetEquipSlot(base.Mod, "xAqult_Mask", EquipType.Head) && base.Player.face < 0)
			{
				drawInfo.drawPlayer.face = EquipLoader.GetEquipSlot(base.Mod, "xAqult_Lens", EquipType.Face);
			}
			if (base.Player.face == EquipLoader.GetEquipSlot(base.Mod, "xAqult_Lens", EquipType.Face) && base.Player.direction == -1)
			{
				drawInfo.drawPlayer.face = EquipLoader.GetEquipSlot(base.Mod, "xAqult_Lens_Blue", EquipType.Face);
			}
		}
	}
}
