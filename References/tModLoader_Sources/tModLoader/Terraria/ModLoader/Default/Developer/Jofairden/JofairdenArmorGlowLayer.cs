using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.ModLoader.Default.Developer.Jofairden
{
	// Token: 0x02000349 RID: 841
	internal abstract class JofairdenArmorGlowLayer : JofairdenArmorDrawLayer
	{
		// Token: 0x06002F43 RID: 12099 RVA: 0x00533BE4 File Offset: 0x00531DE4
		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			DrawDataInfo drawDataInfo = this.GetData(drawInfo);
			Player drawPlayer = drawInfo.drawPlayer;
			JofairdenArmorEffectPlayer modPlayer = drawPlayer.GetModPlayer<JofairdenArmorEffectPlayer>();
			SpriteEffects effects = 0;
			if (drawPlayer.direction == -1)
			{
				effects |= 1;
			}
			if (drawPlayer.gravDir == -1f)
			{
				effects |= 2;
			}
			DrawData data = new DrawData(drawDataInfo.Texture, drawDataInfo.Position, drawDataInfo.Frame, Color.White * Main.essScale * modPlayer.LayerStrength, drawDataInfo.Rotation, drawDataInfo.Origin, 1f, effects, 0f);
			if (modPlayer.HasAura)
			{
				int value = JofairdenArmorDrawLayer.ShaderId.GetValueOrDefault();
				if (JofairdenArmorDrawLayer.ShaderId == null)
				{
					value = GameShaders.Armor.GetShaderIdFromItemId(2870);
					JofairdenArmorDrawLayer.ShaderId = new int?(value);
				}
				data.shader = JofairdenArmorDrawLayer.ShaderId.Value;
			}
			drawInfo.DrawDataCache.Add(data);
		}
	}
}
