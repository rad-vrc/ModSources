using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent
{
	// Token: 0x020004C4 RID: 1220
	public struct VoidLensHelper
	{
		// Token: 0x06003A5E RID: 14942 RVA: 0x005A7F59 File Offset: 0x005A6159
		public VoidLensHelper(Projectile proj)
		{
			this._position = proj.Center;
			this._opacity = proj.Opacity;
			this._frameNumber = proj.frame;
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x005A7F80 File Offset: 0x005A6180
		public VoidLensHelper(Vector2 worldPosition, float opacity)
		{
			worldPosition.Y -= 2f;
			this._position = worldPosition;
			this._opacity = opacity;
			this._frameNumber = (int)(((float)Main.tileFrameCounter[491] + this._position.X + this._position.Y) % 40f) / 5;
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x005A7FDE File Offset: 0x005A61DE
		public void Update()
		{
			Lighting.AddLight(this._position, 0.4f, 0.2f, 0.9f);
			this.SpawnVoidLensDust();
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x005A8000 File Offset: 0x005A6200
		public void SpawnVoidLensDust()
		{
			if (Main.rand.Next(3) == 0)
			{
				if (Main.rand.Next(2) == 0)
				{
					Vector2 vector = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
					vector *= new Vector2(0.5f, 1f);
					Dust dust = Dust.NewDustDirect(this._position - vector * 30f, 0, 0, Utils.SelectRandom<int>(Main.rand, new int[]
					{
						86,
						88
					}), 0f, 0f, 0, default(Color), 1f);
					dust.noGravity = true;
					dust.noLightEmittence = true;
					dust.position = this._position - vector.SafeNormalize(Vector2.Zero) * (float)Main.rand.Next(10, 21);
					dust.velocity = vector.RotatedBy(1.5707963705062866, default(Vector2)) * 2f;
					dust.scale = 0.5f + Main.rand.NextFloat();
					dust.fadeIn = 0.5f;
					dust.customData = this;
					dust.position += dust.velocity * 10f;
					dust.velocity *= -1f;
					return;
				}
				Vector2 vector2 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
				vector2 *= new Vector2(0.5f, 1f);
				Dust dust2 = Dust.NewDustDirect(this._position - vector2 * 30f, 0, 0, Utils.SelectRandom<int>(Main.rand, new int[]
				{
					86,
					88
				}), 0f, 0f, 0, default(Color), 1f);
				dust2.noGravity = true;
				dust2.noLightEmittence = true;
				dust2.position = this._position - vector2.SafeNormalize(Vector2.Zero) * (float)Main.rand.Next(5, 10);
				dust2.velocity = vector2.RotatedBy(-1.5707963705062866, default(Vector2)) * 3f;
				dust2.scale = 0.5f + Main.rand.NextFloat();
				dust2.fadeIn = 0.5f;
				dust2.customData = this;
				dust2.position += dust2.velocity * 10f;
				dust2.velocity *= -1f;
			}
		}

		// Token: 0x06003A62 RID: 14946 RVA: 0x005A82D4 File Offset: 0x005A64D4
		public void DrawToDrawData(List<DrawData> drawDataList, int selectionMode)
		{
			Main.instance.LoadProjectile(734);
			Asset<Texture2D> asset = TextureAssets.Projectile[734];
			Rectangle rectangle = asset.Frame(1, 8, 0, this._frameNumber, 0, 0);
			Color color = Lighting.GetColor(this._position.ToTileCoordinates());
			color = Color.Lerp(color, Color.White, 0.5f);
			color *= this._opacity;
			DrawData drawData = new DrawData(asset.Value, this._position - Main.screenPosition, new Rectangle?(rectangle), color, 0f, rectangle.Size() / 2f, 1f, 0, 0f);
			drawDataList.Add(drawData);
			for (float num = 0f; num < 1f; num += 0.34f)
			{
				DrawData item = drawData;
				item.color = new Color(127, 50, 127, 0) * this._opacity;
				item.scale *= 1.1f;
				float x = (Main.GlobalTimeWrappedHourly / 5f * 6.2831855f).ToRotationVector2().X;
				item.color *= x * 0.1f + 0.3f;
				item.position += ((Main.GlobalTimeWrappedHourly / 5f + num) * 6.2831855f).ToRotationVector2() * (x * 1f + 2f);
				drawDataList.Add(item);
			}
			if (selectionMode != 0)
			{
				int num2 = (int)((color.R + color.G + color.B) / 3);
				if (num2 > 10)
				{
					Color selectionGlowColor = Colors.GetSelectionGlowColor(selectionMode == 2, num2);
					drawData = new DrawData(TextureAssets.Extra[93].Value, this._position - Main.screenPosition, new Rectangle?(rectangle), selectionGlowColor, 0f, rectangle.Size() / 2f, 1f, 0, 0f);
					drawDataList.Add(drawData);
				}
			}
		}

		// Token: 0x040053FC RID: 21500
		private readonly Vector2 _position;

		// Token: 0x040053FD RID: 21501
		private readonly float _opacity;

		// Token: 0x040053FE RID: 21502
		private readonly int _frameNumber;
	}
}
