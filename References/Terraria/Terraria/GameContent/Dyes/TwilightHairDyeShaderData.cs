using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
	// Token: 0x0200021C RID: 540
	public class TwilightHairDyeShaderData : HairShaderData
	{
		// Token: 0x06001EB1 RID: 7857 RVA: 0x0050C344 File Offset: 0x0050A544
		public TwilightHairDyeShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
		{
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x0050C34E File Offset: 0x0050A54E
		public override void Apply(Player player, DrawData? drawData = null)
		{
			if (drawData != null)
			{
				base.UseTargetPosition(Main.screenPosition + drawData.Value.position);
			}
			base.Apply(player, drawData);
		}
	}
}
