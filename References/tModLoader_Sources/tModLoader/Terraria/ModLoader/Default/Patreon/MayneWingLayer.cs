using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000310 RID: 784
	internal class MayneWingLayer : PlayerDrawLayer
	{
		// Token: 0x06002E97 RID: 11927 RVA: 0x0053202F File Offset: 0x0053022F
		public override PlayerDrawLayer.Position GetDefaultPosition()
		{
			return new PlayerDrawLayer.AfterParent(PlayerDrawLayers.Wings);
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x0053203B File Offset: 0x0053023B
		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			return drawInfo.drawPlayer.wings == EquipLoader.GetEquipSlot(base.Mod, "Mayne_Wings", EquipType.Wings);
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x0053205C File Offset: 0x0053025C
		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			if (drawInfo.drawPlayer.dead)
			{
				return;
			}
			DrawData? wingData = null;
			foreach (DrawData data in drawInfo.DrawDataCache)
			{
				if (data.texture == ModContent.Request<Texture2D>("ModLoader/Patreon.Mayne_Wings_Wings", 1).Value)
				{
					wingData = new DrawData?(data);
				}
			}
			if (wingData != null)
			{
				Texture2D value = ModContent.Request<Texture2D>("ModLoader/Patreon.Mayne_Wings_Wings_Glow", 1).Value;
				Color color = Color.White * drawInfo.stealth * (1f - drawInfo.shadow);
				DrawData glow = new DrawData(value, wingData.Value.position, wingData.Value.sourceRect, color, wingData.Value.rotation, wingData.Value.origin, wingData.Value.scale, wingData.Value.effect, 0f);
				glow.shader = wingData.Value.shader;
				drawInfo.DrawDataCache.Add(glow);
			}
		}
	}
}
