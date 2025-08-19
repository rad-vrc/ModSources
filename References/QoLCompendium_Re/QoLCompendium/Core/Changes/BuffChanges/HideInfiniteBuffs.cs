using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.BuffChanges
{
	// Token: 0x02000262 RID: 610
	public class HideInfiniteBuffs : GlobalBuff
	{
		// Token: 0x06000E29 RID: 3625 RVA: 0x00071F28 File Offset: 0x00070128
		public override bool PreDraw(SpriteBatch spriteBatch, int type, int buffIndex, ref BuffDrawParams drawParams)
		{
			if (!QoLCompendium.mainConfig.HideBuffs)
			{
				return base.PreDraw(spriteBatch, type, buffIndex, ref drawParams);
			}
			if (!HideInfiniteBuffs.HideBuffTypes(type))
			{
				return base.PreDraw(spriteBatch, type, buffIndex, ref drawParams);
			}
			drawParams.TextPosition = new Vector2(-100f);
			drawParams.Position = new Vector2(-100f);
			drawParams.MouseRectangle = Rectangle.Empty;
			return false;
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00071F90 File Offset: 0x00070190
		public static bool HideBuffTypes(int type)
		{
			return Main.LocalPlayer.GetModPlayer<QoLCPlayer>().activeBuffs.Contains(type);
		}
	}
}
