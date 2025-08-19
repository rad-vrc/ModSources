using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	// Token: 0x0200020D RID: 525
	public class BlockSwapBuilderToggle : VanillaBuilderToggle
	{
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060027F2 RID: 10226 RVA: 0x00509604 File Offset: 0x00507804
		public override string Texture
		{
			get
			{
				return "Terraria/Images/UI/BlockReplace_0";
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060027F3 RID: 10227 RVA: 0x0050960B File Offset: 0x0050780B
		public override string HoverTexture
		{
			get
			{
				return "Terraria/Images/UI/BlockReplace_0";
			}
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x00509612 File Offset: 0x00507812
		public override bool Active()
		{
			return true;
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x00509618 File Offset: 0x00507818
		public override string DisplayValue()
		{
			string text = "";
			int currentState = base.CurrentState;
			if (currentState != 0)
			{
				if (currentState == 1)
				{
					text = Language.GetTextValue("GameUI.BlockReplacerOff");
				}
			}
			else
			{
				text = Language.GetTextValue("GameUI.BlockReplacerOn");
			}
			return text;
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x00509654 File Offset: 0x00507854
		public override bool Draw(SpriteBatch spriteBatch, ref BuilderToggleDrawParams drawParams)
		{
			drawParams.Color = Color.White;
			drawParams.Frame = drawParams.Texture.Frame(3, 1, (base.CurrentState != 0) ? 1 : 0, 0, 0, 0);
			drawParams.Position += new Vector2(1f, 0f);
			return true;
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x005096B4 File Offset: 0x005078B4
		public override bool DrawHover(SpriteBatch spriteBatch, ref BuilderToggleDrawParams drawParams)
		{
			drawParams.Frame = drawParams.Texture.Frame(3, 1, 2, 0, 0, 0);
			drawParams.Position += new Vector2(1f, 0f);
			drawParams.Scale = 0.9f;
			return true;
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x00509709 File Offset: 0x00507909
		public override bool OnLeftClick(ref SoundStyle? sound)
		{
			sound = new SoundStyle?(SoundID.Unlock);
			return true;
		}
	}
}
