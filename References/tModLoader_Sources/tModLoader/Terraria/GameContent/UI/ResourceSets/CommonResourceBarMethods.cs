using System;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020004ED RID: 1261
	public class CommonResourceBarMethods
	{
		// Token: 0x06003D2B RID: 15659 RVA: 0x005C8B50 File Offset: 0x005C6D50
		public static void DrawLifeMouseOver()
		{
			if (!Main.mouseText)
			{
				Player localPlayer = Main.LocalPlayer;
				localPlayer.cursorItemIconEnabled = false;
				string text = localPlayer.statLife.ToString() + "/" + localPlayer.statLifeMax2.ToString();
				Main.instance.MouseTextHackZoom(text, null);
				Main.mouseText = true;
			}
		}

		// Token: 0x06003D2C RID: 15660 RVA: 0x005C8BA4 File Offset: 0x005C6DA4
		public static void DrawManaMouseOver()
		{
			if (!Main.mouseText)
			{
				Player localPlayer = Main.LocalPlayer;
				localPlayer.cursorItemIconEnabled = false;
				string text = localPlayer.statMana.ToString() + "/" + localPlayer.statManaMax2.ToString();
				Main.instance.MouseTextHackZoom(text, null);
				Main.mouseText = true;
			}
		}
	}
}
