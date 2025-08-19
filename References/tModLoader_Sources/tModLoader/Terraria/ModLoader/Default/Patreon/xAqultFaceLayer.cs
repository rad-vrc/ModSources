using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200033B RID: 827
	internal class xAqultFaceLayer : PlayerDrawLayer
	{
		// Token: 0x06002F14 RID: 12052 RVA: 0x005331F8 File Offset: 0x005313F8
		public override PlayerDrawLayer.Position GetDefaultPosition()
		{
			return new PlayerDrawLayer.AfterParent(PlayerDrawLayers.Head);
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x00533204 File Offset: 0x00531404
		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			return drawInfo.drawPlayer.face == EquipLoader.GetEquipSlot(base.Mod, "xAqult_Lens", EquipType.Face) || drawInfo.drawPlayer.face == EquipLoader.GetEquipSlot(base.Mod, "xAqult_Lens_Blue", EquipType.Face);
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x00533254 File Offset: 0x00531454
		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			int insertIndex = -1;
			for (int i = 0; i < drawInfo.DrawDataCache.Count; i++)
			{
				if (drawInfo.DrawDataCache[i].texture == TextureAssets.Players[drawInfo.skinVar, 2].Value)
				{
					insertIndex = i + 1;
					break;
				}
			}
			if (insertIndex < 0)
			{
				return;
			}
			for (int j = insertIndex; j < drawInfo.DrawDataCache.Count; j++)
			{
				DrawData data = drawInfo.DrawDataCache[j];
				if (data.texture == ModContent.Request<Texture2D>("ModLoader/Patreon.xAqult_Lens_Face", 1).Value)
				{
					drawInfo.DrawDataCache.RemoveAt(j);
					drawInfo.DrawDataCache.Insert(insertIndex, data);
					data.texture = ModContent.Request<Texture2D>("ModLoader/Patreon.xAqult_Lens_Face_Glow", 1).Value;
					data.color = drawInfo.drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
					drawInfo.DrawDataCache.Insert(insertIndex + 1, data);
					return;
				}
				if (data.texture == ModContent.Request<Texture2D>("ModLoader/Patreon.xAqult_Lens_Blue_Face", 1).Value)
				{
					drawInfo.DrawDataCache.RemoveAt(j);
					drawInfo.DrawDataCache.Insert(insertIndex, data);
					data.texture = ModContent.Request<Texture2D>("ModLoader/Patreon.xAqult_Lens_Blue_Face_Glow", 1).Value;
					data.color = drawInfo.drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
					drawInfo.DrawDataCache.Insert(insertIndex + 1, data);
					return;
				}
			}
		}
	}
}
