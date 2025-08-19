using System;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	// Token: 0x0200020B RID: 523
	public class HideAllWiresBuilderToggle : WireVisibilityBuilderToggle
	{
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x005095B5 File Offset: 0x005077B5
		public override int NumberOfStates
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x005095B8 File Offset: 0x005077B8
		public override string DisplayValue()
		{
			string text = "";
			int currentState = base.CurrentState;
			if (currentState != 0)
			{
				if (currentState == 1)
				{
					text = Language.GetTextValue("GameUI.WireModeNormal");
				}
			}
			else
			{
				text = Language.GetTextValue("GameUI.WireModeForced");
			}
			return text;
		}
	}
}
