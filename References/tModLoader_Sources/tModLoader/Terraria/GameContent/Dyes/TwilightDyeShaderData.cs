using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
	// Token: 0x02000637 RID: 1591
	public class TwilightDyeShaderData : ArmorShaderData
	{
		// Token: 0x060045A2 RID: 17826 RVA: 0x006148A8 File Offset: 0x00612AA8
		public TwilightDyeShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
		{
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x006148B4 File Offset: 0x00612AB4
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
