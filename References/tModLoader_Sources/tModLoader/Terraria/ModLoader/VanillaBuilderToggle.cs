using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Terraria.ModLoader
{
	// Token: 0x02000201 RID: 513
	[Autoload(false)]
	public abstract class VanillaBuilderToggle : BuilderToggle
	{
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060027D4 RID: 10196 RVA: 0x005091FC File Offset: 0x005073FC
		public override string Texture
		{
			get
			{
				return "Terraria/Images/UI/BuilderIcons";
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060027D5 RID: 10197 RVA: 0x00509203 File Offset: 0x00507403
		public override int NumberOfStates
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x00509206 File Offset: 0x00507406
		public override string DisplayValue()
		{
			return "";
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x00509210 File Offset: 0x00507410
		public override bool Draw(SpriteBatch spriteBatch, ref BuilderToggleDrawParams drawParams)
		{
			drawParams.Color = ((base.CurrentState == 0) ? Color.White : new Color(127, 127, 127));
			drawParams.Frame = ((base.Type < 10) ? new Rectangle(base.Type * 16, 16, 14, 14) : drawParams.Frame);
			return true;
		}
	}
}
