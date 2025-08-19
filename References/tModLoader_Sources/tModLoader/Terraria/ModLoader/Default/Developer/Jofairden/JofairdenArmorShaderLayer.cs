using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.ModLoader.Default.Developer.Jofairden
{
	// Token: 0x0200034A RID: 842
	internal abstract class JofairdenArmorShaderLayer : JofairdenArmorDrawLayer
	{
		// Token: 0x06002F45 RID: 12101 RVA: 0x00533CD8 File Offset: 0x00531ED8
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
			DrawData data = new DrawData(drawDataInfo.Texture, drawDataInfo.Position, drawDataInfo.Frame, Color.White * Main.essScale * modPlayer.LayerStrength * modPlayer.ShaderStrength, drawDataInfo.Rotation, drawDataInfo.Origin, 1f, effects, 0f);
			JofairdenArmorShaderLayer.BeginShaderBatch(Main.spriteBatch);
			int value = JofairdenArmorDrawLayer.ShaderId.GetValueOrDefault();
			if (JofairdenArmorDrawLayer.ShaderId == null)
			{
				value = GameShaders.Armor.GetShaderIdFromItemId(2870);
				JofairdenArmorDrawLayer.ShaderId = new int?(value);
			}
			GameShaders.Armor.Apply(JofairdenArmorDrawLayer.ShaderId.Value, drawPlayer, new DrawData?(data));
			Vector2 centerPos = data.position;
			for (int i = 0; i < 8; i++)
			{
				data.position = centerPos + JofairdenArmorShaderLayer.GetDrawOffset(i);
				data.Draw(Main.spriteBatch);
			}
			data.position = centerPos;
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x00533E14 File Offset: 0x00532014
		protected static Vector2 GetDrawOffset(int i)
		{
			return new Vector2(0f, 2f).RotatedBy((double)((float)i / 8f * 6.2831855f), default(Vector2));
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x00533E50 File Offset: 0x00532050
		private static void BeginShaderBatch(SpriteBatch batch)
		{
			batch.End();
			RasterizerState rasterizerState = (Main.LocalPlayer.gravDir == 1f) ? RasterizerState.CullCounterClockwise : RasterizerState.CullClockwise;
			batch.Begin(1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, rasterizerState, null, Main.GameViewMatrix.TransformationMatrix);
		}

		// Token: 0x04001C8F RID: 7311
		public const int ShaderNumSegments = 8;

		// Token: 0x04001C90 RID: 7312
		public const int ShaderDrawOffset = 2;
	}
}
