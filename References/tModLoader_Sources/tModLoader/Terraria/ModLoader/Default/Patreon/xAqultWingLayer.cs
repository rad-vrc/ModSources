using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200033C RID: 828
	internal class xAqultWingLayer : PlayerDrawLayer
	{
		// Token: 0x06002F18 RID: 12056 RVA: 0x005333C8 File Offset: 0x005315C8
		public override PlayerDrawLayer.Position GetDefaultPosition()
		{
			return new PlayerDrawLayer.AfterParent(PlayerDrawLayers.Wings);
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x005333D4 File Offset: 0x005315D4
		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			return drawInfo.drawPlayer.wings == EquipLoader.GetEquipSlot(base.Mod, "xAqult_Wings", EquipType.Wings);
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x005333F8 File Offset: 0x005315F8
		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			if (drawInfo.drawPlayer.dead)
			{
				return;
			}
			DrawData? wingData = null;
			foreach (DrawData data in drawInfo.DrawDataCache)
			{
				if (data.texture == ModContent.Request<Texture2D>("ModLoader/Patreon.xAqult_Wings_Wings", 1).Value)
				{
					wingData = new DrawData?(data);
				}
			}
			if (wingData != null)
			{
				Texture2D value = ModContent.Request<Texture2D>("ModLoader/Patreon.xAqult_Wings_Wings_Glow", 1).Value;
				Color color = Color.White * drawInfo.stealth * (1f - drawInfo.shadow);
				DrawData glow = new DrawData(value, wingData.Value.position, wingData.Value.sourceRect, color, wingData.Value.rotation, wingData.Value.origin, wingData.Value.scale, wingData.Value.effect, 0f);
				glow.shader = wingData.Value.shader;
				drawInfo.DrawDataCache.Add(glow);
			}
		}
	}
}
