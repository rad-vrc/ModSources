using System;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020003B7 RID: 951
	public class CommonResourceBarMethods
	{
		// Token: 0x06002A14 RID: 10772 RVA: 0x0059724C File Offset: 0x0059544C
		public static void DrawLifeMouseOver()
		{
			if (!Main.mouseText)
			{
				Player localPlayer = Main.LocalPlayer;
				localPlayer.cursorItemIconEnabled = false;
				string text = localPlayer.statLife + "/" + localPlayer.statLifeMax2;
				Main.instance.MouseTextHackZoom(text, null);
				Main.mouseText = true;
			}
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x005972A0 File Offset: 0x005954A0
		public static void DrawManaMouseOver()
		{
			if (!Main.mouseText)
			{
				Player localPlayer = Main.LocalPlayer;
				localPlayer.cursorItemIconEnabled = false;
				string text = localPlayer.statMana + "/" + localPlayer.statManaMax2;
				Main.instance.MouseTextHackZoom(text, null);
				Main.mouseText = true;
			}
		}
	}
}
