using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	// Token: 0x02000206 RID: 518
	[Autoload(false)]
	public abstract class WireVisibilityBuilderToggle : VanillaBuilderToggle
	{
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060027E5 RID: 10213 RVA: 0x005093D0 File Offset: 0x005075D0
		public override int NumberOfStates
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x005093D3 File Offset: 0x005075D3
		public override bool Active()
		{
			return Main.player[Main.myPlayer].InfoAccMechShowWires;
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x005093E8 File Offset: 0x005075E8
		public override string DisplayValue()
		{
			string text = "";
			switch (base.Type)
			{
			case 4:
				text = Language.GetTextValue("Game.RedWires");
				break;
			case 5:
				text = Language.GetTextValue("Game.BlueWires");
				break;
			case 6:
				text = Language.GetTextValue("Game.GreenWires");
				break;
			case 7:
				text = Language.GetTextValue("Game.YellowWires");
				break;
			case 9:
				text = Language.GetTextValue("Game.Actuators");
				break;
			}
			string text2 = "";
			switch (base.CurrentState)
			{
			case 0:
				text2 = Language.GetTextValue("GameUI.Bright");
				break;
			case 1:
				text2 = Language.GetTextValue("GameUI.Normal");
				break;
			case 2:
				text2 = Language.GetTextValue("GameUI.Faded");
				break;
			case 3:
				Language.GetTextValue("GameUI.Hidden");
				break;
			}
			return text + ": " + text2;
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x005094C8 File Offset: 0x005076C8
		public override bool Draw(SpriteBatch spriteBatch, ref BuilderToggleDrawParams drawParams)
		{
			base.Draw(spriteBatch, ref drawParams);
			drawParams.Color = default(Color);
			switch (base.CurrentState)
			{
			case 0:
				drawParams.Color = Color.White;
				break;
			case 1:
				drawParams.Color = new Color(127, 127, 127);
				break;
			case 2:
				drawParams.Color = new Color(127, 127, 127).MultiplyRGBA(new Color(0.66f, 0.66f, 0.66f, 0.66f));
				break;
			case 3:
				drawParams.Color = new Color(127, 127, 127).MultiplyRGBA(new Color(0.33f, 0.33f, 0.33f, 0.33f));
				break;
			}
			return true;
		}
	}
}
