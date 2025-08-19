using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
	// Token: 0x0200021D RID: 541
	public class TwilightDyeShaderData : ArmorShaderData
	{
		// Token: 0x06001EB3 RID: 7859 RVA: 0x0050C37E File Offset: 0x0050A57E
		public TwilightDyeShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
		{
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x0050C388 File Offset: 0x0050A588
		public override void Apply(Entity entity, DrawData? drawData)
		{
			if (drawData != null)
			{
				Player player = entity as Player;
				if (player != null && !player.isDisplayDollOrInanimate && !player.isHatRackDoll)
				{
					base.UseTargetPosition(Main.screenPosition + drawData.Value.position);
				}
				else if (entity is Projectile)
				{
					base.UseTargetPosition(Main.screenPosition + drawData.Value.position);
				}
				else
				{
					base.UseTargetPosition(drawData.Value.position);
				}
			}
			base.Apply(entity, drawData);
		}
	}
}
