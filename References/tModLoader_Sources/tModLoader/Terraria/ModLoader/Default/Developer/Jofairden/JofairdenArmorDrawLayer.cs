using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Terraria.ModLoader.Default.Developer.Jofairden
{
	// Token: 0x02000348 RID: 840
	internal abstract class JofairdenArmorDrawLayer : PlayerDrawLayer
	{
		// Token: 0x06002F3D RID: 12093
		public abstract DrawDataInfo GetData(PlayerDrawSet info);

		// Token: 0x06002F3E RID: 12094 RVA: 0x00533914 File Offset: 0x00531B14
		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			Player player = drawInfo.drawPlayer;
			return drawInfo.shadow == 0f && !player.invis && player.GetModPlayer<JofairdenArmorEffectPlayer>().LayerStrength > 0f;
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x00533954 File Offset: 0x00531B54
		public static DrawDataInfo GetHeadDrawDataInfo(PlayerDrawSet drawInfo, Texture2D texture)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			Vector2 pos = drawPlayer.headPosition + drawInfo.headVect + new Vector2((float)((int)(drawInfo.Position.X + (float)drawPlayer.width / 2f - (float)drawPlayer.bodyFrame.Width / 2f - Main.screenPosition.X)), (float)((int)(drawInfo.Position.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f - Main.screenPosition.Y)));
			return new DrawDataInfo
			{
				Position = pos,
				Frame = new Rectangle?(drawPlayer.bodyFrame),
				Origin = drawInfo.headVect,
				Rotation = drawPlayer.headRotation,
				Texture = texture
			};
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x00533A2C File Offset: 0x00531C2C
		public static DrawDataInfo GetBodyDrawDataInfo(PlayerDrawSet drawInfo, Texture2D texture)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			Vector2 pos = drawPlayer.bodyPosition + drawInfo.bodyVect + new Vector2((float)((int)(drawInfo.Position.X - Main.screenPosition.X - (float)drawPlayer.bodyFrame.Width / 2f + (float)drawPlayer.width / 2f)), (float)((int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)));
			return new DrawDataInfo
			{
				Position = pos,
				Frame = new Rectangle?(drawPlayer.bodyFrame),
				Origin = drawInfo.bodyVect,
				Rotation = drawPlayer.bodyRotation,
				Texture = texture
			};
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x00533B04 File Offset: 0x00531D04
		public static DrawDataInfo GetLegDrawDataInfo(PlayerDrawSet drawInfo, Texture2D texture)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			Vector2 pos = drawPlayer.legPosition + drawInfo.legVect + new Vector2((float)((int)(drawInfo.Position.X - Main.screenPosition.X - (float)drawPlayer.legFrame.Width / 2f + (float)drawPlayer.width / 2f)), (float)((int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f)));
			return new DrawDataInfo
			{
				Position = pos,
				Frame = new Rectangle?(drawPlayer.legFrame),
				Origin = drawInfo.legVect,
				Rotation = drawPlayer.legRotation,
				Texture = texture
			};
		}

		// Token: 0x04001C8E RID: 7310
		protected static int? ShaderId;
	}
}
