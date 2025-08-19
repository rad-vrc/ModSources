using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	// Token: 0x0200020E RID: 526
	public class TorchBiomeBuilderToggle : VanillaBuilderToggle
	{
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060027FA RID: 10234 RVA: 0x00509724 File Offset: 0x00507924
		public override string Texture
		{
			get
			{
				return "Terraria/Images/Extra_211";
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060027FB RID: 10235 RVA: 0x0050972B File Offset: 0x0050792B
		public override string HoverTexture
		{
			get
			{
				return "Terraria/Images/Extra_211";
			}
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x00509732 File Offset: 0x00507932
		public override bool Active()
		{
			return Main.player[Main.myPlayer].unlockedBiomeTorches;
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x00509744 File Offset: 0x00507944
		public override string DisplayValue()
		{
			string text = "";
			int currentState = base.CurrentState;
			if (currentState != 0)
			{
				if (currentState == 1)
				{
					text = Language.GetTextValue("GameUI.TorchTypeSwapperOff");
				}
			}
			else
			{
				text = Language.GetTextValue("GameUI.TorchTypeSwapperOn");
			}
			return text;
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x00509780 File Offset: 0x00507980
		public override bool Draw(SpriteBatch spriteBatch, ref BuilderToggleDrawParams drawParams)
		{
			drawParams.Color = Color.White;
			drawParams.Frame = drawParams.Texture.Frame(4, 1, (base.CurrentState == 0) ? 1 : 0, 0, 0, 0);
			drawParams.Position += new Vector2(1f, 0f);
			return true;
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x005097E0 File Offset: 0x005079E0
		public override bool DrawHover(SpriteBatch spriteBatch, ref BuilderToggleDrawParams drawParams)
		{
			drawParams.Frame = drawParams.Texture.Frame(4, 1, (base.CurrentState == 0) ? 3 : 2, 0, 0, 0);
			drawParams.Position += new Vector2(1f, 0f);
			drawParams.Scale = 0.9f;
			return true;
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x00509840 File Offset: 0x00507A40
		public override bool OnLeftClick(ref SoundStyle? sound)
		{
			sound = new SoundStyle?(SoundID.Unlock);
			return true;
		}
	}
}
