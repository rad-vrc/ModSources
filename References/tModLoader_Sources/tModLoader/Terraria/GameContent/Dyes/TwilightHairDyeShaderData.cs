using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
	// Token: 0x02000638 RID: 1592
	public class TwilightHairDyeShaderData : HairShaderData
	{
		// Token: 0x060045A4 RID: 17828 RVA: 0x00614945 File Offset: 0x00612B45
		public TwilightHairDyeShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
		{
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x0061494F File Offset: 0x00612B4F
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
