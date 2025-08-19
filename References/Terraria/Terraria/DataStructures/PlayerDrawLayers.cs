﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.GameContent.Liquid;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.DataStructures
{
	// Token: 0x02000453 RID: 1107
	public static class PlayerDrawLayers
	{
		// Token: 0x06002C16 RID: 11286 RVA: 0x005A6E3E File Offset: 0x005A503E
		public static void DrawPlayer_extra_TorsoPlus(ref PlayerDrawSet drawinfo)
		{
			drawinfo.Position.Y = drawinfo.Position.Y + drawinfo.torsoOffset;
			drawinfo.ItemLocation.Y = drawinfo.ItemLocation.Y + drawinfo.torsoOffset;
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x005A6E6A File Offset: 0x005A506A
		public static void DrawPlayer_extra_TorsoMinus(ref PlayerDrawSet drawinfo)
		{
			drawinfo.Position.Y = drawinfo.Position.Y - drawinfo.torsoOffset;
			drawinfo.ItemLocation.Y = drawinfo.ItemLocation.Y - drawinfo.torsoOffset;
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x005A6E96 File Offset: 0x005A5096
		public static void DrawPlayer_extra_MountPlus(ref PlayerDrawSet drawinfo)
		{
			drawinfo.Position.Y = drawinfo.Position.Y + (float)((int)drawinfo.mountOffSet / 2);
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x005A6EB1 File Offset: 0x005A50B1
		public static void DrawPlayer_extra_MountMinus(ref PlayerDrawSet drawinfo)
		{
			drawinfo.Position.Y = drawinfo.Position.Y - (float)((int)drawinfo.mountOffSet / 2);
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x005A6ECC File Offset: 0x005A50CC
		public static void DrawCompositeArmorPiece(ref PlayerDrawSet drawinfo, CompositePlayerDrawContext context, DrawData data)
		{
			drawinfo.DrawDataCache.Add(data);
			switch (context)
			{
			case CompositePlayerDrawContext.BackShoulder:
			case CompositePlayerDrawContext.BackArm:
			case CompositePlayerDrawContext.FrontArm:
			case CompositePlayerDrawContext.FrontShoulder:
				if (drawinfo.armGlowColor.PackedValue > 0U)
				{
					DrawData drawData = data;
					drawData.color = drawinfo.armGlowColor;
					Rectangle value = drawData.sourceRect.Value;
					value.Y += 224;
					drawData.sourceRect = new Rectangle?(value);
					if (drawinfo.drawPlayer.body == 227)
					{
						Vector2 position = drawData.position;
						for (int i = 0; i < 2; i++)
						{
							Vector2 value2 = new Vector2((float)Main.rand.Next(-10, 10) * 0.125f, (float)Main.rand.Next(-10, 10) * 0.125f);
							drawData.position = position + value2;
							if (i == 0)
							{
								drawinfo.DrawDataCache.Add(drawData);
							}
						}
					}
					drawinfo.DrawDataCache.Add(drawData);
				}
				break;
			case CompositePlayerDrawContext.Torso:
				if (drawinfo.bodyGlowColor.PackedValue > 0U)
				{
					DrawData drawData2 = data;
					drawData2.color = drawinfo.bodyGlowColor;
					Rectangle value3 = drawData2.sourceRect.Value;
					value3.Y += 224;
					drawData2.sourceRect = new Rectangle?(value3);
					if (drawinfo.drawPlayer.body == 227)
					{
						Vector2 position2 = drawData2.position;
						for (int j = 0; j < 2; j++)
						{
							Vector2 value4 = new Vector2((float)Main.rand.Next(-10, 10) * 0.125f, (float)Main.rand.Next(-10, 10) * 0.125f);
							drawData2.position = position2 + value4;
							if (j == 0)
							{
								drawinfo.DrawDataCache.Add(drawData2);
							}
						}
					}
					drawinfo.DrawDataCache.Add(drawData2);
				}
				break;
			}
			if (context == CompositePlayerDrawContext.FrontShoulder && drawinfo.drawPlayer.head == 269)
			{
				Vector2 position3 = drawinfo.helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect;
				DrawData item = new DrawData(TextureAssets.Extra[214].Value, position3, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cHead;
				drawinfo.DrawDataCache.Add(item);
				item = new DrawData(TextureAssets.GlowMask[308].Value, position3, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cHead;
				drawinfo.DrawDataCache.Add(item);
			}
			if (context == CompositePlayerDrawContext.FrontArm && drawinfo.drawPlayer.body == 205)
			{
				Color color = new Color(100, 100, 100, 0);
				ulong num = (ulong)((long)(drawinfo.drawPlayer.miscCounter / 4));
				int num2 = 4;
				for (int k = 0; k < num2; k++)
				{
					float num3 = (float)Utils.RandomInt(ref num, -10, 11) * 0.2f;
					float num4 = (float)Utils.RandomInt(ref num, -10, 1) * 0.15f;
					DrawData item2 = data;
					Rectangle value5 = item2.sourceRect.Value;
					value5.Y += 224;
					item2.sourceRect = new Rectangle?(value5);
					item2.position.X = item2.position.X + num3;
					item2.position.Y = item2.position.Y + num4;
					item2.color = color;
					drawinfo.DrawDataCache.Add(item2);
				}
			}
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x005A7324 File Offset: 0x005A5524
		public static void DrawPlayer_01_BackHair(ref PlayerDrawSet drawinfo)
		{
			if (!drawinfo.hideHair && drawinfo.backHairDraw)
			{
				Vector2 position = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + drawinfo.hairOffset;
				if (drawinfo.drawPlayer.head == -1 || drawinfo.fullHair || drawinfo.drawsBackHairWithoutHeadgear)
				{
					DrawData item = new DrawData(TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, position, new Rectangle?(drawinfo.hairBackFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.hairDyePacked;
					drawinfo.DrawDataCache.Add(item);
					return;
				}
				if (drawinfo.hatHair)
				{
					DrawData item = new DrawData(TextureAssets.PlayerHairAlt[drawinfo.drawPlayer.hair].Value, position, new Rectangle?(drawinfo.hairBackFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.hairDyePacked;
					drawinfo.DrawDataCache.Add(item);
				}
			}
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x005A74E4 File Offset: 0x005A56E4
		public static void DrawPlayer_02_MountBehindPlayer(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.mount.Active)
			{
				PlayerDrawLayers.DrawMeowcartTrail(ref drawinfo);
				PlayerDrawLayers.DrawTiedBalloons(ref drawinfo);
				drawinfo.drawPlayer.mount.Draw(drawinfo.DrawDataCache, 0, drawinfo.drawPlayer, drawinfo.Position, drawinfo.colorMount, drawinfo.playerEffect, drawinfo.shadow);
				drawinfo.drawPlayer.mount.Draw(drawinfo.DrawDataCache, 1, drawinfo.drawPlayer, drawinfo.Position, drawinfo.colorMount, drawinfo.playerEffect, drawinfo.shadow);
			}
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x005A757C File Offset: 0x005A577C
		public static void DrawPlayer_03_Carpet(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.carpetFrame >= 0)
			{
				Color colorArmorLegs = drawinfo.colorArmorLegs;
				float num = 0f;
				if (drawinfo.drawPlayer.gravDir == -1f)
				{
					num = 10f;
				}
				DrawData item = new DrawData(TextureAssets.FlyingCarpet.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)(drawinfo.drawPlayer.height / 2) + 28f * drawinfo.drawPlayer.gravDir + num))), new Rectangle?(new Rectangle(0, TextureAssets.FlyingCarpet.Height() / 6 * drawinfo.drawPlayer.carpetFrame, TextureAssets.FlyingCarpet.Width(), TextureAssets.FlyingCarpet.Height() / 6)), colorArmorLegs, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.FlyingCarpet.Width() / 2), (float)(TextureAssets.FlyingCarpet.Height() / 8)), 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cCarpet;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x005A76C4 File Offset: 0x005A58C4
		public static void DrawPlayer_03_PortableStool(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.portableStoolInfo.IsInUse)
			{
				Texture2D value = TextureAssets.Extra[102].Value;
				Vector2 position = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height + 28f)));
				Rectangle rectangle = value.Frame(1, 1, 0, 0, 0, 0);
				Vector2 origin = rectangle.Size() * new Vector2(0.5f, 1f);
				DrawData item = new DrawData(value, position, new Rectangle?(rectangle), drawinfo.colorArmorLegs, drawinfo.drawPlayer.bodyRotation, origin, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cPortableStool;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x005A77C4 File Offset: 0x005A59C4
		public static void DrawPlayer_04_ElectrifiedDebuffBack(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.electrified && drawinfo.shadow == 0f)
			{
				Texture2D value = TextureAssets.GlowMask[25].Value;
				int num = drawinfo.drawPlayer.miscCounter / 5;
				for (int i = 0; i < 2; i++)
				{
					num %= 7;
					if (num <= 1 || num >= 5)
					{
						DrawData item = new DrawData(value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(new Rectangle(0, num * value.Height / 7, value.Width, value.Height / 7)), drawinfo.colorElectricity, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(value.Width / 2), (float)(value.Height / 14)), 1f, drawinfo.playerEffect, 0f);
						drawinfo.DrawDataCache.Add(item);
					}
					num += 3;
				}
			}
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x005A7960 File Offset: 0x005A5B60
		public static void DrawPlayer_05_ForbiddenSetRing(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.setForbidden && drawinfo.shadow == 0f)
			{
				Color color = Color.Lerp(drawinfo.colorArmorBody, Color.White, 0.7f);
				Texture2D value = TextureAssets.Extra[74].Value;
				Texture2D value2 = TextureAssets.GlowMask[217].Value;
				bool flag = !drawinfo.drawPlayer.setForbiddenCooldownLocked;
				int num = (int)(((float)drawinfo.drawPlayer.miscCounter / 300f * 6.2831855f).ToRotationVector2().Y * 6f);
				float num2 = ((float)drawinfo.drawPlayer.miscCounter / 75f * 6.2831855f).ToRotationVector2().X * 4f;
				Color color2 = new Color(80, 70, 40, 0) * (num2 / 8f + 0.5f) * 0.8f;
				if (!flag)
				{
					num = 0;
					num2 = 2f;
					color2 = new Color(80, 70, 40, 0) * 0.3f;
					color = color.MultiplyRGB(new Color(0.5f, 0.5f, 1f));
				}
				Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
				int num3 = 10;
				int num4 = 20;
				if (drawinfo.drawPlayer.head == 238)
				{
					num3 += 4;
					num4 += 4;
				}
				vector += new Vector2((float)(-(float)drawinfo.drawPlayer.direction * num3), (float)(-(float)num4) * drawinfo.drawPlayer.gravDir + (float)num * drawinfo.drawPlayer.gravDir);
				DrawData item = new DrawData(value, vector, null, color, drawinfo.drawPlayer.bodyRotation, value.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cBody;
				drawinfo.DrawDataCache.Add(item);
				for (float num5 = 0f; num5 < 4f; num5 += 1f)
				{
					item = new DrawData(value2, vector + (num5 * 1.5707964f).ToRotationVector2() * num2, null, color2, drawinfo.drawPlayer.bodyRotation, value.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
				}
			}
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x005A7C90 File Offset: 0x005A5E90
		public static void DrawPlayer_01_3_BackHead(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.head < 0 || drawinfo.drawPlayer.head >= ArmorIDs.Head.Count)
			{
				return;
			}
			int num = ArmorIDs.Head.Sets.FrontToBackID[drawinfo.drawPlayer.head];
			if (num < 0)
			{
				return;
			}
			Vector2 helmetOffset = drawinfo.helmetOffset;
			DrawData item = new DrawData(TextureAssets.ArmorHead[num].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
			item.shader = drawinfo.cHead;
			drawinfo.DrawDataCache.Add(item);
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x005A7DE0 File Offset: 0x005A5FE0
		public static void DrawPlayer_01_2_JimsCloak(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.legs == 60 && !drawinfo.isSitting && !drawinfo.drawPlayer.invis && (!PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo) || drawinfo.drawPlayer.wearsRobe))
			{
				DrawData item = new DrawData(TextureAssets.Extra[153].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cLegs;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x005A7F34 File Offset: 0x005A6134
		public static void DrawPlayer_05_2_SafemanSun(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.head == 238 && drawinfo.shadow == 0f)
			{
				Color color = Color.Lerp(drawinfo.colorArmorBody, Color.White, 0.7f);
				Texture2D value = TextureAssets.Extra[152].Value;
				Texture2D value2 = TextureAssets.Extra[152].Value;
				int num = (int)(((float)drawinfo.drawPlayer.miscCounter / 300f * 6.2831855f).ToRotationVector2().Y * 6f);
				float num2 = ((float)drawinfo.drawPlayer.miscCounter / 75f * 6.2831855f).ToRotationVector2().X * 4f;
				Color color2 = new Color(80, 70, 40, 0) * (num2 / 8f + 0.5f) * 0.8f;
				Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
				int num3 = 8;
				int num4 = 20;
				num3 += 4;
				num4 += 4;
				vector += new Vector2((float)(-(float)drawinfo.drawPlayer.direction * num3), (float)(-(float)num4) * drawinfo.drawPlayer.gravDir + (float)num * drawinfo.drawPlayer.gravDir);
				DrawData item = new DrawData(value, vector, null, color, drawinfo.drawPlayer.bodyRotation, value.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cHead;
				drawinfo.DrawDataCache.Add(item);
				for (float num5 = 0f; num5 < 4f; num5 += 1f)
				{
					item = new DrawData(value2, vector + (num5 * 1.5707964f).ToRotationVector2() * num2, null, color2, drawinfo.drawPlayer.bodyRotation, value.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cHead;
					drawinfo.DrawDataCache.Add(item);
				}
			}
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x005A821C File Offset: 0x005A641C
		public static void DrawPlayer_06_WebbedDebuffBack(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.webbed && drawinfo.shadow == 0f && drawinfo.drawPlayer.velocity.Y != 0f)
			{
				Color color = drawinfo.colorArmorBody * 0.75f;
				Texture2D value = TextureAssets.Extra[32].Value;
				DrawData item = new DrawData(value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), null, color, drawinfo.drawPlayer.bodyRotation, value.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x005A8388 File Offset: 0x005A6588
		public static void DrawPlayer_07_LeinforsHairShampoo(ref PlayerDrawSet drawinfo)
		{
			if (!drawinfo.drawPlayer.leinforsHair || (!drawinfo.fullHair && !drawinfo.hatHair && !drawinfo.drawsBackHairWithoutHeadgear && drawinfo.drawPlayer.head != -1 && drawinfo.drawPlayer.head != 0) || drawinfo.drawPlayer.hair == 12 || drawinfo.shadow != 0f || Main.rgbToHsl(drawinfo.colorHead).Z <= 0.2f)
			{
				return;
			}
			if (Main.rand.Next(20) == 0 && !drawinfo.hatHair)
			{
				Rectangle rectangle = Utils.CenteredRectangle(drawinfo.Position + drawinfo.drawPlayer.Size / 2f + new Vector2(0f, drawinfo.drawPlayer.gravDir * -20f), new Vector2(20f, 14f));
				int num = Dust.NewDust(rectangle.TopLeft(), rectangle.Width, rectangle.Height, 204, 0f, 0f, 150, default(Color), 0.3f);
				Main.dust[num].fadeIn = 1f;
				Main.dust[num].velocity *= 0.1f;
				Main.dust[num].noLight = true;
				Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(drawinfo.drawPlayer.cLeinShampoo, drawinfo.drawPlayer);
				drawinfo.DustCache.Add(num);
			}
			if (Main.rand.Next(40) == 0 && drawinfo.hatHair)
			{
				Rectangle rectangle2 = Utils.CenteredRectangle(drawinfo.Position + drawinfo.drawPlayer.Size / 2f + new Vector2((float)(drawinfo.drawPlayer.direction * -10), drawinfo.drawPlayer.gravDir * -10f), new Vector2(5f, 5f));
				int num2 = Dust.NewDust(rectangle2.TopLeft(), rectangle2.Width, rectangle2.Height, 204, 0f, 0f, 150, default(Color), 0.3f);
				Main.dust[num2].fadeIn = 1f;
				Main.dust[num2].velocity *= 0.1f;
				Main.dust[num2].noLight = true;
				Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(drawinfo.drawPlayer.cLeinShampoo, drawinfo.drawPlayer);
				drawinfo.DustCache.Add(num2);
			}
			if (drawinfo.drawPlayer.velocity.X != 0f && drawinfo.backHairDraw && Main.rand.Next(15) == 0)
			{
				Rectangle rectangle3 = Utils.CenteredRectangle(drawinfo.Position + drawinfo.drawPlayer.Size / 2f + new Vector2((float)(drawinfo.drawPlayer.direction * -14), 0f), new Vector2(4f, 30f));
				int num3 = Dust.NewDust(rectangle3.TopLeft(), rectangle3.Width, rectangle3.Height, 204, 0f, 0f, 150, default(Color), 0.3f);
				Main.dust[num3].fadeIn = 1f;
				Main.dust[num3].velocity *= 0.1f;
				Main.dust[num3].noLight = true;
				Main.dust[num3].shader = GameShaders.Armor.GetSecondaryShader(drawinfo.drawPlayer.cLeinShampoo, drawinfo.drawPlayer);
				drawinfo.DustCache.Add(num3);
			}
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x005A8783 File Offset: 0x005A6983
		public static bool DrawPlayer_08_PlayerVisuallyHasFullArmorSet(PlayerDrawSet drawinfo, int head, int body, int legs)
		{
			return drawinfo.drawPlayer.head == head && drawinfo.drawPlayer.body == body && drawinfo.drawPlayer.legs == legs;
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x005A87B4 File Offset: 0x005A69B4
		public static void DrawPlayer_08_Backpacks(ref PlayerDrawSet drawinfo)
		{
			if (PlayerDrawLayers.DrawPlayer_08_PlayerVisuallyHasFullArmorSet(drawinfo, 266, 235, 218))
			{
				Vector2 vector = new Vector2(-2f + -2f * drawinfo.drawPlayer.Directions.X, 0f) + drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2));
				vector = vector.Floor();
				Texture2D value = TextureAssets.Extra[212].Value;
				Rectangle value2 = value.Frame(1, 5, 0, drawinfo.drawPlayer.miscCounter % 25 / 5, 0, 0);
				Color color = drawinfo.drawPlayer.GetImmuneAlphaPure(new Color(250, 250, 250, 200), drawinfo.shadow);
				color *= drawinfo.drawPlayer.stealth;
				DrawData item = new DrawData(value, vector, new Rectangle?(value2), color, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cBody;
				drawinfo.DrawDataCache.Add(item);
			}
			if (PlayerDrawLayers.DrawPlayer_08_PlayerVisuallyHasFullArmorSet(drawinfo, 268, 237, 222))
			{
				Vector2 vector2 = new Vector2(-9f + 1f * drawinfo.drawPlayer.Directions.X, -4f * drawinfo.drawPlayer.Directions.Y) + drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2));
				vector2 = vector2.Floor();
				Texture2D value3 = TextureAssets.Extra[213].Value;
				Rectangle value4 = value3.Frame(1, 5, 0, drawinfo.drawPlayer.miscCounter % 25 / 5, 0, 0);
				Color color2 = drawinfo.drawPlayer.GetImmuneAlphaPure(new Color(250, 250, 250, 200), drawinfo.shadow);
				color2 *= drawinfo.drawPlayer.stealth;
				DrawData item = new DrawData(value3, vector2, new Rectangle?(value4), color2, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cBody;
				drawinfo.DrawDataCache.Add(item);
			}
			if (drawinfo.heldItem.type == 4818 && drawinfo.drawPlayer.ownedProjectileCounts[902] == 0)
			{
				int num = 8;
				Vector2 value5 = new Vector2(0f, 8f);
				Vector2 vector3 = drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0f, -4f) + value5;
				vector3 = vector3.Floor();
				DrawData item = new DrawData(TextureAssets.BackPack[num].Value, vector3, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
			}
			if (drawinfo.drawPlayer.backpack > 0 && (int)drawinfo.drawPlayer.backpack < ArmorIDs.Back.Count && !drawinfo.drawPlayer.mount.Active)
			{
				Vector2 value6 = new Vector2(0f, 8f);
				Vector2 vector4 = drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0f, -4f) + value6;
				vector4 = vector4.Floor();
				DrawData item = new DrawData(TextureAssets.AccBack[(int)drawinfo.drawPlayer.backpack].Value, vector4, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cBackpack;
				drawinfo.DrawDataCache.Add(item);
				return;
			}
			if (drawinfo.heldItem.type == 1178 || drawinfo.heldItem.type == 779 || drawinfo.heldItem.type == 5134 || drawinfo.heldItem.type == 1295 || drawinfo.heldItem.type == 1910 || drawinfo.drawPlayer.turtleArmor || drawinfo.drawPlayer.body == 106 || drawinfo.drawPlayer.body == 170)
			{
				int type = drawinfo.heldItem.type;
				int num2 = 1;
				float num3 = -4f;
				float num4 = -8f;
				int shader = 0;
				if (drawinfo.drawPlayer.turtleArmor)
				{
					num2 = 4;
					shader = drawinfo.cBody;
				}
				else if (drawinfo.drawPlayer.body == 106)
				{
					num2 = 6;
					shader = drawinfo.cBody;
				}
				else if (drawinfo.drawPlayer.body == 170)
				{
					num2 = 7;
					shader = drawinfo.cBody;
				}
				else if (type == 1178)
				{
					num2 = 1;
				}
				else if (type == 779)
				{
					num2 = 2;
				}
				else if (type == 5134)
				{
					num2 = 9;
				}
				else if (type == 1295)
				{
					num2 = 3;
				}
				else if (type == 1910)
				{
					num2 = 5;
				}
				Vector2 value7 = new Vector2(0f, 8f);
				Vector2 vector5 = drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0f, -4f) + value7;
				vector5 = vector5.Floor();
				Vector2 vector6 = drawinfo.Position - Main.screenPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2((-9f + num3) * (float)drawinfo.drawPlayer.direction, (2f + num4) * drawinfo.drawPlayer.gravDir) + value7;
				vector6 = vector6.Floor();
				DrawData item;
				if (num2 == 7)
				{
					item = new DrawData(TextureAssets.BackPack[num2].Value, vector5, new Rectangle?(new Rectangle(0, drawinfo.drawPlayer.bodyFrame.Y, TextureAssets.BackPack[num2].Width(), drawinfo.drawPlayer.bodyFrame.Height)), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float)TextureAssets.BackPack[num2].Width() * 0.5f, drawinfo.bodyVect.Y), 1f, drawinfo.playerEffect, 0f);
					item.shader = shader;
					drawinfo.DrawDataCache.Add(item);
					return;
				}
				if (num2 == 4 || num2 == 6)
				{
					item = new DrawData(TextureAssets.BackPack[num2].Value, vector5, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = shader;
					drawinfo.DrawDataCache.Add(item);
					return;
				}
				item = new DrawData(TextureAssets.BackPack[num2].Value, vector6, new Rectangle?(new Rectangle(0, 0, TextureAssets.BackPack[num2].Width(), TextureAssets.BackPack[num2].Height())), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.BackPack[num2].Width() / 2), (float)(TextureAssets.BackPack[num2].Height() / 2)), 1f, drawinfo.playerEffect, 0f);
				item.shader = shader;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x005A9104 File Offset: 0x005A7304
		public static void DrawPlayer_08_1_Tails(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.tail > 0 && (int)drawinfo.drawPlayer.tail < ArmorIDs.Back.Count && !drawinfo.drawPlayer.mount.Active)
			{
				Vector2 zero = Vector2.Zero;
				if (drawinfo.isSitting)
				{
					zero.Y += -2f;
				}
				Vector2 value = new Vector2(0f, 8f);
				Vector2 vector = zero + drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0f, -4f) + value;
				vector = vector.Floor();
				DrawData item = new DrawData(TextureAssets.AccBack[(int)drawinfo.drawPlayer.tail].Value, vector, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cTail;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x005A9268 File Offset: 0x005A7468
		public static void DrawPlayer_10_BackAcc(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.back > 0 && (int)drawinfo.drawPlayer.back < ArmorIDs.Back.Count)
			{
				if (drawinfo.drawPlayer.front >= 1 && drawinfo.drawPlayer.front <= 4)
				{
					int num = drawinfo.drawPlayer.bodyFrame.Y / 56;
					if (num < 1 || num > 5)
					{
						drawinfo.armorAdjust = 10;
					}
					else
					{
						if (drawinfo.drawPlayer.front == 1)
						{
							drawinfo.armorAdjust = 0;
						}
						if (drawinfo.drawPlayer.front == 2)
						{
							drawinfo.armorAdjust = 8;
						}
						if (drawinfo.drawPlayer.front == 3)
						{
							drawinfo.armorAdjust = 0;
						}
						if (drawinfo.drawPlayer.front == 4)
						{
							drawinfo.armorAdjust = 8;
						}
					}
				}
				Vector2 zero = Vector2.Zero;
				Vector2 value = new Vector2(0f, 8f);
				Vector2 vector = zero + drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0f, -4f) + value;
				vector = vector.Floor();
				DrawData item = new DrawData(TextureAssets.AccBack[(int)drawinfo.drawPlayer.back].Value, vector, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cBack;
				drawinfo.DrawDataCache.Add(item);
				if (drawinfo.drawPlayer.back == 36)
				{
					Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
					Rectangle value2 = bodyFrame;
					value2.Width = 2;
					int num2 = 0;
					int num3 = bodyFrame.Width / 2;
					int num4 = 2;
					if (drawinfo.playerEffect.HasFlag(SpriteEffects.FlipHorizontally))
					{
						num2 = bodyFrame.Width - 2;
						num4 = -2;
					}
					for (int i = 0; i < num3; i++)
					{
						value2.X = bodyFrame.X + 2 * i;
						Color color = drawinfo.drawPlayer.GetImmuneAlpha(LiquidRenderer.GetShimmerGlitterColor(true, (float)i / 16f, 0f), drawinfo.shadow);
						color *= (float)drawinfo.colorArmorBody.A / 255f;
						item = new DrawData(TextureAssets.GlowMask[332].Value, vector + new Vector2((float)(num2 + i * num4), 0f), new Rectangle?(value2), color, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cBack;
						drawinfo.DrawDataCache.Add(item);
					}
				}
			}
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x005A9578 File Offset: 0x005A7778
		public static void DrawPlayer_09_Wings(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.dead || drawinfo.hideEntirePlayer)
			{
				return;
			}
			Vector2 directions = drawinfo.drawPlayer.Directions;
			Vector2 vector = drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.Size / 2f;
			Vector2 value = new Vector2(0f, 7f);
			vector = drawinfo.Position - Main.screenPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + value;
			if (drawinfo.drawPlayer.wings > 0)
			{
				Main.instance.LoadWings(drawinfo.drawPlayer.wings);
				if (drawinfo.drawPlayer.wings == 22)
				{
					if (drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated())
					{
						Main.instance.LoadItemFlames(1866);
						Color colorArmorBody = drawinfo.colorArmorBody;
						int num = 26;
						int num2 = -9;
						Vector2 vector2 = vector + new Vector2((float)num2, (float)num) * directions;
						DrawData item;
						if (drawinfo.shadow == 0f && drawinfo.drawPlayer.grappling[0] == -1)
						{
							for (int i = 0; i < 7; i++)
							{
								Color color = new Color(250 - i * 10, 250 - i * 10, 250 - i * 10, 150 - i * 10);
								Vector2 vector3 = new Vector2((float)Main.rand.Next(-10, 11) * 0.2f, (float)Main.rand.Next(-10, 11) * 0.2f);
								drawinfo.stealth *= drawinfo.stealth;
								drawinfo.stealth *= 1f - drawinfo.shadow;
								color = new Color((int)((float)color.R * drawinfo.stealth), (int)((float)color.G * drawinfo.stealth), (int)((float)color.B * drawinfo.stealth), (int)((float)color.A * drawinfo.stealth));
								vector3.X = drawinfo.drawPlayer.itemFlamePos[i].X;
								vector3.Y = -drawinfo.drawPlayer.itemFlamePos[i].Y;
								vector3 *= 0.5f;
								Vector2 position = (vector2 + vector3).Floor();
								item = new DrawData(TextureAssets.ItemFlame[1866].Value, position, new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7 - 2)), color, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 14)), 1f, drawinfo.playerEffect, 0f);
								item.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(item);
							}
						}
						item = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vector2.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7)), colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 14)), 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(item);
						return;
					}
				}
				else if (drawinfo.drawPlayer.wings == 28)
				{
					if (drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated())
					{
						Color colorArmorBody2 = drawinfo.colorArmorBody;
						Vector2 value2 = new Vector2(0f, 19f);
						Vector2 vec = vector + value2 * directions;
						Texture2D value3 = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
						Rectangle rectangle = value3.Frame(1, 4, 0, drawinfo.drawPlayer.miscCounter / 5 % 4, 0, 0);
						rectangle.Width -= 2;
						rectangle.Height -= 2;
						DrawData item = new DrawData(value3, vec.Floor(), new Rectangle?(rectangle), Color.Lerp(colorArmorBody2, Color.White, 1f), drawinfo.drawPlayer.bodyRotation, rectangle.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(item);
						value3 = TextureAssets.Extra[38].Value;
						item = new DrawData(value3, vec.Floor(), new Rectangle?(rectangle), Color.Lerp(colorArmorBody2, Color.White, 0.5f), drawinfo.drawPlayer.bodyRotation, rectangle.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(item);
						return;
					}
				}
				else if (drawinfo.drawPlayer.wings == 45)
				{
					if (drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated())
					{
						PlayerDrawLayers.DrawStarboardRainbowTrail(ref drawinfo, vector, directions);
						Color value4 = new Color(255, 255, 255, 255);
						int num3 = 22;
						int num4 = 0;
						Vector2 vec2 = vector + new Vector2((float)num4, (float)num3) * directions;
						Color color2 = value4 * (1f - drawinfo.shadow);
						DrawData item = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vec2.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 6 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 6)), color2, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 12)), 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(item);
						if (drawinfo.shadow == 0f)
						{
							float num5 = ((float)drawinfo.drawPlayer.miscCounter / 75f * 6.2831855f).ToRotationVector2().X * 4f;
							Color color3 = new Color(70, 70, 70, 0) * (num5 / 8f + 0.5f) * 0.4f;
							for (float num6 = 0f; num6 < 6.2831855f; num6 += 1.5707964f)
							{
								item = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vec2.Floor() + num6.ToRotationVector2() * num5, new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 6 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 6)), color3, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 12)), 1f, drawinfo.playerEffect, 0f);
								item.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(item);
							}
							return;
						}
					}
				}
				else if (drawinfo.drawPlayer.wings == 34)
				{
					if (drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated())
					{
						drawinfo.stealth *= drawinfo.stealth;
						drawinfo.stealth *= 1f - drawinfo.shadow;
						Color color4 = new Color((int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(100f * drawinfo.stealth));
						Vector2 value5 = new Vector2(0f, 0f);
						Texture2D value6 = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
						Vector2 vec3 = drawinfo.Position + drawinfo.drawPlayer.Size / 2f - Main.screenPosition + value5 * drawinfo.drawPlayer.Directions - Vector2.UnitX * (float)drawinfo.drawPlayer.direction * 4f;
						Rectangle rectangle2 = value6.Frame(1, 6, 0, drawinfo.drawPlayer.wingFrame, 0, 0);
						rectangle2.Width -= 2;
						rectangle2.Height -= 2;
						DrawData item = new DrawData(value6, vec3.Floor(), new Rectangle?(rectangle2), color4, drawinfo.drawPlayer.bodyRotation, rectangle2.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(item);
						return;
					}
				}
				else
				{
					if (drawinfo.drawPlayer.wings == 40)
					{
						drawinfo.stealth *= drawinfo.stealth;
						drawinfo.stealth *= 1f - drawinfo.shadow;
						Color color5 = new Color((int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(100f * drawinfo.stealth));
						Vector2 value7 = new Vector2(-4f, 0f);
						Texture2D value8 = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
						Vector2 vector4 = vector + value7 * directions;
						for (int j = 0; j < 1; j++)
						{
							SpriteEffects spriteEffects = drawinfo.playerEffect;
							Vector2 scale = new Vector2(1f);
							Vector2 zero = Vector2.Zero;
							zero.X = (float)(drawinfo.drawPlayer.direction * 3);
							if (j == 1)
							{
								spriteEffects ^= SpriteEffects.FlipHorizontally;
								scale = new Vector2(0.7f, 1f);
								zero.X += (float)(-(float)drawinfo.drawPlayer.direction) * 6f;
							}
							Vector2 value9 = drawinfo.drawPlayer.velocity * -1.5f;
							int num7 = 0;
							int num8 = 8;
							float num9 = 4f;
							if (drawinfo.drawPlayer.velocity.Y == 0f)
							{
								num7 = 8;
								num8 = 14;
								num9 = 3f;
							}
							for (int k = num7; k < num8; k++)
							{
								Vector2 vector5 = vector4;
								Rectangle rectangle3 = value8.Frame(1, 14, 0, k, 0, 0);
								rectangle3.Width -= 2;
								rectangle3.Height -= 2;
								int num10 = (k - num7) % (int)num9;
								Vector2 value10 = new Vector2(0f, 0.5f).RotatedBy((double)((drawinfo.drawPlayer.miscCounterNormalized * (2f + (float)num10) + (float)num10 * 0.5f + (float)j * 1.3f) * 6.2831855f), default(Vector2)) * (float)(num10 + 1);
								vector5 += value10;
								vector5 += value9 * ((float)num10 / num9);
								vector5 += zero;
								DrawData item = new DrawData(value8, vector5.Floor(), new Rectangle?(rectangle3), color5, drawinfo.drawPlayer.bodyRotation, rectangle3.Size() / 2f, scale, spriteEffects, 0f);
								item.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(item);
							}
						}
						return;
					}
					if (drawinfo.drawPlayer.wings == 39)
					{
						if (drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated())
						{
							drawinfo.stealth *= drawinfo.stealth;
							drawinfo.stealth *= 1f - drawinfo.shadow;
							Color colorArmorBody3 = drawinfo.colorArmorBody;
							Vector2 value11 = new Vector2(-6f, -7f);
							Texture2D value12 = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
							Vector2 vec4 = vector + value11 * directions;
							Rectangle rectangle4 = value12.Frame(1, 6, 0, drawinfo.drawPlayer.wingFrame, 0, 0);
							rectangle4.Width -= 2;
							rectangle4.Height -= 2;
							DrawData item = new DrawData(value12, vec4.Floor(), new Rectangle?(rectangle4), colorArmorBody3, drawinfo.drawPlayer.bodyRotation, rectangle4.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
							item.shader = drawinfo.cWings;
							drawinfo.DrawDataCache.Add(item);
							return;
						}
					}
					else
					{
						int num11 = 0;
						int num12 = 0;
						int num13 = 4;
						if (drawinfo.drawPlayer.wings == 43)
						{
							num12 = -5;
							num11 = -7;
							num13 = 7;
						}
						else if (drawinfo.drawPlayer.wings == 44)
						{
							num13 = 7;
						}
						else if (drawinfo.drawPlayer.wings == 5)
						{
							num12 = 4;
							num11 -= 4;
						}
						else if (drawinfo.drawPlayer.wings == 27)
						{
							num12 = 4;
						}
						Color color6 = drawinfo.colorArmorBody;
						if (drawinfo.drawPlayer.wings == 9 || drawinfo.drawPlayer.wings == 29)
						{
							drawinfo.stealth *= drawinfo.stealth;
							drawinfo.stealth *= 1f - drawinfo.shadow;
							color6 = new Color((int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(100f * drawinfo.stealth));
						}
						if (drawinfo.drawPlayer.wings == 10)
						{
							drawinfo.stealth *= drawinfo.stealth;
							drawinfo.stealth *= 1f - drawinfo.shadow;
							color6 = new Color((int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(175f * drawinfo.stealth));
						}
						if (drawinfo.drawPlayer.wings == 11 && color6.A > Main.gFade)
						{
							color6.A = Main.gFade;
						}
						if (drawinfo.drawPlayer.wings == 31)
						{
							color6.A = (byte)(220f * drawinfo.stealth);
						}
						if (drawinfo.drawPlayer.wings == 32)
						{
							color6.A = (byte)(127f * drawinfo.stealth);
						}
						if (drawinfo.drawPlayer.wings == 6)
						{
							color6.A = (byte)(160f * drawinfo.stealth);
							color6 *= 0.9f;
						}
						Vector2 vector6 = vector + new Vector2((float)(num12 - 9), (float)(num11 + 2)) * directions;
						DrawData item = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vector6.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num13 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num13)), color6, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num13 / 2)), 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(item);
						if (drawinfo.drawPlayer.wings == 43 && drawinfo.shadow == 0f)
						{
							Vector2 value13 = vector6;
							Vector2 origin = new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num13 / 2));
							Rectangle value14 = new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num13 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num13);
							for (int l = 0; l < 2; l++)
							{
								Vector2 value15 = new Vector2((float)Main.rand.Next(-10, 10) * 0.125f, (float)Main.rand.Next(-10, 10) * 0.125f);
								item = new DrawData(TextureAssets.GlowMask[272].Value, value13 + value15, new Rectangle?(value14), new Color(230, 230, 230, 60), drawinfo.drawPlayer.bodyRotation, origin, 1f, drawinfo.playerEffect, 0f);
								item.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(item);
							}
						}
						if (drawinfo.drawPlayer.wings == 23)
						{
							drawinfo.stealth *= drawinfo.stealth;
							drawinfo.stealth *= 1f - drawinfo.shadow;
							color6 = new Color((int)(200f * drawinfo.stealth), (int)(200f * drawinfo.stealth), (int)(200f * drawinfo.stealth), (int)(200f * drawinfo.stealth));
							item = new DrawData(TextureAssets.Flames[8].Value, vector6.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), color6, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
							item.shader = drawinfo.cWings;
							drawinfo.DrawDataCache.Add(item);
							return;
						}
						if (drawinfo.drawPlayer.wings == 27)
						{
							item = new DrawData(TextureAssets.GlowMask[92].Value, vector6.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color(255, 255, 255, 127) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
							item.shader = drawinfo.cWings;
							drawinfo.DrawDataCache.Add(item);
							return;
						}
						if (drawinfo.drawPlayer.wings == 44)
						{
							PlayerRainbowWingsTextureContent playerRainbowWings = TextureAssets.RenderTargets.PlayerRainbowWings;
							playerRainbowWings.Request();
							if (playerRainbowWings.IsReady)
							{
								RenderTarget2D target = playerRainbowWings.GetTarget();
								item = new DrawData(target, vector6.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7)), new Color(255, 255, 255, 255) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 14)), 1f, drawinfo.playerEffect, 0f);
								item.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(item);
								return;
							}
						}
						else
						{
							if (drawinfo.drawPlayer.wings == 30)
							{
								item = new DrawData(TextureAssets.GlowMask[181].Value, vector6.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color(255, 255, 255, 127) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
								item.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(item);
								return;
							}
							if (drawinfo.drawPlayer.wings == 38)
							{
								Color color7 = drawinfo.ArkhalisColor * drawinfo.stealth * (1f - drawinfo.shadow);
								item = new DrawData(TextureAssets.GlowMask[251].Value, vector6.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), color7, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
								item.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(item);
								for (int m = drawinfo.drawPlayer.shadowPos.Length - 2; m >= 0; m--)
								{
									Color value16 = color7;
									value16.A = 0;
									value16 *= MathHelper.Lerp(1f, 0f, (float)m / 3f);
									value16 *= 0.1f;
									Vector2 value17 = drawinfo.drawPlayer.shadowPos[m] - drawinfo.drawPlayer.position;
									for (float num14 = 0f; num14 < 1f; num14 += 0.01f)
									{
										Vector2 value18 = new Vector2(2f, 0f).RotatedBy((double)(num14 / 0.04f * 6.2831855f), default(Vector2));
										item = new DrawData(TextureAssets.GlowMask[251].Value, value18 + value17 * num14 + vector6, new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), value16 * (1f - num14), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
										item.shader = drawinfo.cWings;
										drawinfo.DrawDataCache.Add(item);
									}
								}
								return;
							}
							if (drawinfo.drawPlayer.wings == 29)
							{
								item = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vector6.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color(255, 255, 255, 0) * drawinfo.stealth * (1f - drawinfo.shadow) * 0.5f, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1.06f, drawinfo.playerEffect, 0f);
								item.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(item);
								return;
							}
							if (drawinfo.drawPlayer.wings == 36)
							{
								item = new DrawData(TextureAssets.GlowMask[213].Value, vector6.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color(255, 255, 255, 0) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1.06f, drawinfo.playerEffect, 0f);
								item.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(item);
								Vector2 spinningpoint = new Vector2(0f, 2f - drawinfo.shadow * 2f);
								for (int n = 0; n < 4; n++)
								{
									item = new DrawData(TextureAssets.GlowMask[213].Value, spinningpoint.RotatedBy((double)(1.5707964f * (float)n), default(Vector2)) + vector6.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color(127, 127, 127, 127) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
									item.shader = drawinfo.cWings;
									drawinfo.DrawDataCache.Add(item);
								}
								return;
							}
							if (drawinfo.drawPlayer.wings == 31)
							{
								Color value19 = new Color(255, 255, 255, 0);
								value19 = Color.Lerp(Color.HotPink, Color.Crimson, (float)Math.Cos((double)(6.2831855f * ((float)drawinfo.drawPlayer.miscCounter / 100f))) * 0.4f + 0.5f);
								value19.A = 0;
								for (int num15 = 0; num15 < 4; num15++)
								{
									Vector2 value20 = new Vector2((float)Math.Cos((double)(6.2831855f * ((float)drawinfo.drawPlayer.miscCounter / 60f))) * 0.5f + 0.5f, 0f).RotatedBy((double)((float)num15 * 1.5707964f), default(Vector2)) * 1f;
									item = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vector6.Floor() + value20, new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), value19 * drawinfo.stealth * (1f - drawinfo.shadow) * 0.5f, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
									item.shader = drawinfo.cWings;
									drawinfo.DrawDataCache.Add(item);
								}
								item = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vector6.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), value19 * drawinfo.stealth * (1f - drawinfo.shadow) * 1f, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
								item.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(item);
								return;
							}
							if (drawinfo.drawPlayer.wings == 32)
							{
								item = new DrawData(TextureAssets.GlowMask[183].Value, vector6.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color(255, 255, 255, 0) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1.06f, drawinfo.playerEffect, 0f);
								item.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(item);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x005AB824 File Offset: 0x005A9A24
		public static void DrawPlayer_12_1_BalloonFronts(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.balloonFront > 0 && (int)drawinfo.drawPlayer.balloonFront < ArmorIDs.Balloon.Count)
			{
				DrawData item;
				if (ArmorIDs.Balloon.Sets.UsesTorsoFraming[(int)drawinfo.drawPlayer.balloonFront])
				{
					item = new DrawData(TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloonFront].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + drawinfo.bodyVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cBalloonFront;
					drawinfo.DrawDataCache.Add(item);
					return;
				}
				int num = (!Main.hasFocus || (Main.ingameOptionsWindow && Main.autoPause)) ? 0 : (DateTime.Now.Millisecond % 800 / 200);
				Vector2 vector = Main.OffsetsPlayerOffhand[drawinfo.drawPlayer.bodyFrame.Y / 56];
				if (drawinfo.drawPlayer.direction != 1)
				{
					vector.X = (float)drawinfo.drawPlayer.width - vector.X;
				}
				if (drawinfo.drawPlayer.gravDir != 1f)
				{
					vector.Y -= (float)drawinfo.drawPlayer.height;
				}
				Vector2 value = new Vector2(0f, 8f) + new Vector2(0f, 6f);
				Vector2 vector2 = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + vector.X)), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + vector.Y * drawinfo.drawPlayer.gravDir)));
				vector2 = drawinfo.Position - Main.screenPosition + vector * new Vector2(1f, drawinfo.drawPlayer.gravDir) + new Vector2(0f, (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height)) + value;
				vector2 = vector2.Floor();
				item = new DrawData(TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloonFront].Value, vector2, new Rectangle?(new Rectangle(0, TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloonFront].Height() / 4 * num, TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloonFront].Width(), TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloonFront].Height() / 4)), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(26 + drawinfo.drawPlayer.direction * 4), 28f + drawinfo.drawPlayer.gravDir * 6f), 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cBalloonFront;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x005ABBD0 File Offset: 0x005A9DD0
		public static void DrawPlayer_11_Balloons(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.balloon > 0 && (int)drawinfo.drawPlayer.balloon < ArmorIDs.Balloon.Count)
			{
				DrawData item;
				if (ArmorIDs.Balloon.Sets.UsesTorsoFraming[(int)drawinfo.drawPlayer.balloon])
				{
					item = new DrawData(TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloon].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + drawinfo.bodyVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cBalloon;
					drawinfo.DrawDataCache.Add(item);
					return;
				}
				int num = (!Main.hasFocus || (Main.ingameOptionsWindow && Main.autoPause)) ? 0 : (DateTime.Now.Millisecond % 800 / 200);
				Vector2 vector = Main.OffsetsPlayerOffhand[drawinfo.drawPlayer.bodyFrame.Y / 56];
				if (drawinfo.drawPlayer.direction != 1)
				{
					vector.X = (float)drawinfo.drawPlayer.width - vector.X;
				}
				if (drawinfo.drawPlayer.gravDir != 1f)
				{
					vector.Y -= (float)drawinfo.drawPlayer.height;
				}
				Vector2 value = new Vector2(0f, 8f) + new Vector2(0f, 6f);
				Vector2 vector2 = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + vector.X)), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + vector.Y * drawinfo.drawPlayer.gravDir)));
				vector2 = drawinfo.Position - Main.screenPosition + vector * new Vector2(1f, drawinfo.drawPlayer.gravDir) + new Vector2(0f, (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height)) + value;
				vector2 = vector2.Floor();
				item = new DrawData(TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloon].Value, vector2, new Rectangle?(new Rectangle(0, TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloon].Height() / 4 * num, TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloon].Width(), TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloon].Height() / 4)), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(26 + drawinfo.drawPlayer.direction * 4), 28f + drawinfo.drawPlayer.gravDir * 6f), 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cBalloon;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x005ABF7C File Offset: 0x005AA17C
		public static void DrawPlayer_12_Skin(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.usesCompositeTorso)
			{
				PlayerDrawLayers.DrawPlayer_12_Skin_Composite(ref drawinfo);
				return;
			}
			if (drawinfo.isSitting)
			{
				drawinfo.hidesBottomSkin = true;
			}
			if (!drawinfo.hidesTopSkin)
			{
				drawinfo.Position.Y = drawinfo.Position.Y + drawinfo.torsoOffset;
				DrawData item = new DrawData(TextureAssets.Players[drawinfo.skinVar, 3].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.skinDyePacked
				};
				drawinfo.DrawDataCache.Add(item);
				drawinfo.Position.Y = drawinfo.Position.Y - drawinfo.torsoOffset;
			}
			if (!drawinfo.hidesBottomSkin && !PlayerDrawLayers.IsBottomOverridden(ref drawinfo))
			{
				if (drawinfo.isSitting)
				{
					PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.Players[drawinfo.skinVar, 10].Value, drawinfo.colorLegs, 0, false);
					return;
				}
				DrawData item = new DrawData(TextureAssets.Players[drawinfo.skinVar, 10].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorLegs, drawinfo.drawPlayer.legRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x005AC26A File Offset: 0x005AA46A
		public static bool IsBottomOverridden(ref PlayerDrawSet drawinfo)
		{
			return PlayerDrawLayers.ShouldOverrideLegs_CheckPants(ref drawinfo) || PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo);
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x005AC284 File Offset: 0x005AA484
		public static bool ShouldOverrideLegs_CheckPants(ref PlayerDrawSet drawinfo)
		{
			int legs = drawinfo.drawPlayer.legs;
			if (legs <= 140)
			{
				if (legs <= 106)
				{
					if (legs != 67 && legs != 106)
					{
						return false;
					}
				}
				else if (legs != 138 && legs != 140)
				{
					return false;
				}
			}
			else if (legs <= 217)
			{
				if (legs != 143 && legs != 217)
				{
					return false;
				}
			}
			else if (legs != 222 && legs != 226 && legs != 228)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x005AC300 File Offset: 0x005AA500
		public static bool ShouldOverrideLegs_CheckShoes(ref PlayerDrawSet drawinfo)
		{
			sbyte shoe = drawinfo.drawPlayer.shoe;
			return shoe == 15;
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x005AC324 File Offset: 0x005AA524
		public static void DrawPlayer_12_Skin_Composite(ref PlayerDrawSet drawinfo)
		{
			if (!drawinfo.hidesTopSkin && !drawinfo.drawPlayer.invis)
			{
				Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
				vector.Y += drawinfo.torsoOffset;
				Vector2 value = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
				value.Y -= 2f;
				vector += value * (float)(-(float)drawinfo.playerEffect.HasFlag(SpriteEffects.FlipVertically).ToDirectionInt());
				float bodyRotation = drawinfo.drawPlayer.bodyRotation;
				Vector2 value2 = vector;
				Vector2 value3 = vector;
				Vector2 bodyVect = drawinfo.bodyVect;
				Vector2 value4 = drawinfo.bodyVect;
				Vector2 compositeOffset_BackArm = PlayerDrawLayers.GetCompositeOffset_BackArm(ref drawinfo);
				value2 += compositeOffset_BackArm;
				bodyVect + compositeOffset_BackArm;
				Vector2 compositeOffset_FrontArm = PlayerDrawLayers.GetCompositeOffset_FrontArm(ref drawinfo);
				value4 += compositeOffset_FrontArm;
				value3 + compositeOffset_FrontArm;
				DrawData drawData;
				if (drawinfo.drawFloatingTube)
				{
					List<DrawData> drawDataCache = drawinfo.DrawDataCache;
					drawData = new DrawData(TextureAssets.Extra[105].Value, vector, new Rectangle?(new Rectangle(0, 0, 40, 56)), drawinfo.floatingTubeColor, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
					{
						shader = drawinfo.cFloatingTube
					};
					drawDataCache.Add(drawData);
				}
				List<DrawData> drawDataCache2 = drawinfo.DrawDataCache;
				drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 3].Value, vector, new Rectangle?(drawinfo.compTorsoFrame), drawinfo.colorBodySkin, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.skinDyePacked
				};
				drawDataCache2.Add(drawData);
			}
			if (!drawinfo.hidesBottomSkin && !drawinfo.drawPlayer.invis && !PlayerDrawLayers.IsBottomOverridden(ref drawinfo))
			{
				if (drawinfo.isSitting)
				{
					PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.Players[drawinfo.skinVar, 10].Value, drawinfo.colorLegs, drawinfo.skinDyePacked, false);
				}
				else
				{
					DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 10].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorLegs, drawinfo.drawPlayer.legRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
					{
						shader = drawinfo.skinDyePacked
					};
					DrawData item = drawData;
					drawinfo.DrawDataCache.Add(item);
				}
			}
			PlayerDrawLayers.DrawPlayer_12_SkinComposite_BackArmShirt(ref drawinfo);
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x005AC72C File Offset: 0x005AA92C
		public static void DrawPlayer_12_SkinComposite_BackArmShirt(ref PlayerDrawSet drawinfo)
		{
			Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
			Vector2 value = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
			value.Y -= 2f;
			vector += value * (float)(-(float)drawinfo.playerEffect.HasFlag(SpriteEffects.FlipVertically).ToDirectionInt());
			vector.Y += drawinfo.torsoOffset;
			float bodyRotation = drawinfo.drawPlayer.bodyRotation;
			Vector2 vector2 = vector;
			Vector2 vector3 = vector;
			Vector2 vector4 = drawinfo.bodyVect;
			Vector2 compositeOffset_BackArm = PlayerDrawLayers.GetCompositeOffset_BackArm(ref drawinfo);
			vector2 += compositeOffset_BackArm;
			vector3 += drawinfo.backShoulderOffset;
			vector4 += compositeOffset_BackArm;
			float rotation = bodyRotation + drawinfo.compositeBackArmRotation;
			bool flag = !drawinfo.drawPlayer.invis;
			bool flag2 = !drawinfo.drawPlayer.invis;
			bool flag3 = drawinfo.drawPlayer.body > 0 && drawinfo.drawPlayer.body < ArmorIDs.Body.Count;
			bool flag4 = !drawinfo.hidesTopSkin;
			bool flag5 = false;
			if (flag3)
			{
				flag &= drawinfo.missingHand;
				if (flag2 && drawinfo.missingArm)
				{
					if (flag4)
					{
						List<DrawData> drawDataCache = drawinfo.DrawDataCache;
						DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, vector2, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorBodySkin, rotation, vector4, 1f, drawinfo.playerEffect, 0f)
						{
							shader = drawinfo.skinDyePacked
						};
						drawDataCache.Add(drawData);
					}
					if (!flag5 && flag4)
					{
						List<DrawData> drawDataCache2 = drawinfo.DrawDataCache;
						DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 5].Value, vector2, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorBodySkin, rotation, vector4, 1f, drawinfo.playerEffect, 0f)
						{
							shader = drawinfo.skinDyePacked
						};
						drawDataCache2.Add(drawData);
						flag5 = true;
					}
					flag2 = false;
				}
				if (!drawinfo.drawPlayer.invis || PlayerDrawLayers.IsArmorDrawnWhenInvisible(drawinfo.drawPlayer.body))
				{
					Texture2D value2 = TextureAssets.ArmorBodyComposite[drawinfo.drawPlayer.body].Value;
					DrawData drawData;
					if (!drawinfo.hideCompositeShoulders)
					{
						CompositePlayerDrawContext context = CompositePlayerDrawContext.BackShoulder;
						drawData = new DrawData(value2, vector3, new Rectangle?(drawinfo.compBackShoulderFrame), drawinfo.colorArmorBody, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
						{
							shader = drawinfo.cBody
						};
						PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, context, drawData);
					}
					PlayerDrawLayers.DrawPlayer_12_1_BalloonFronts(ref drawinfo);
					CompositePlayerDrawContext context2 = CompositePlayerDrawContext.BackArm;
					drawData = new DrawData(value2, vector2, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorArmorBody, rotation, vector4, 1f, drawinfo.playerEffect, 0f)
					{
						shader = drawinfo.cBody
					};
					PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, context2, drawData);
				}
			}
			if (flag)
			{
				if (flag4)
				{
					if (flag2)
					{
						List<DrawData> drawDataCache3 = drawinfo.DrawDataCache;
						DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, vector2, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorBodySkin, rotation, vector4, 1f, drawinfo.playerEffect, 0f)
						{
							shader = drawinfo.skinDyePacked
						};
						drawDataCache3.Add(drawData);
					}
					if (!flag5 && flag4)
					{
						List<DrawData> drawDataCache4 = drawinfo.DrawDataCache;
						DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 5].Value, vector2, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorBodySkin, rotation, vector4, 1f, drawinfo.playerEffect, 0f)
						{
							shader = drawinfo.skinDyePacked
						};
						drawDataCache4.Add(drawData);
					}
				}
				if (!flag3)
				{
					drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 8].Value, vector2, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorUnderShirt, rotation, vector4, 1f, drawinfo.playerEffect, 0f));
					PlayerDrawLayers.DrawPlayer_12_1_BalloonFronts(ref drawinfo);
					drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 13].Value, vector2, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorShirt, rotation, vector4, 1f, drawinfo.playerEffect, 0f));
				}
			}
			if (drawinfo.drawPlayer.handoff > 0 && (int)drawinfo.drawPlayer.handoff < ArmorIDs.HandOff.Count)
			{
				Texture2D value3 = TextureAssets.AccHandsOffComposite[(int)drawinfo.drawPlayer.handoff].Value;
				CompositePlayerDrawContext context3 = CompositePlayerDrawContext.BackArmAccessory;
				DrawData drawData = new DrawData(value3, vector2, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorArmorBody, rotation, vector4, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.cHandOff
				};
				PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, context3, drawData);
			}
			if (drawinfo.drawPlayer.drawingFootball)
			{
				Main.instance.LoadProjectile(861);
				Texture2D value4 = TextureAssets.Projectile[861].Value;
				Rectangle rectangle = value4.Frame(1, 4, 0, 0, 0, 0);
				Vector2 origin = rectangle.Size() / 2f;
				Vector2 position = vector2 + new Vector2((float)(drawinfo.drawPlayer.direction * -2), drawinfo.drawPlayer.gravDir * 4f);
				drawinfo.DrawDataCache.Add(new DrawData(value4, position, new Rectangle?(rectangle), drawinfo.colorArmorBody, bodyRotation + 0.7853982f * (float)drawinfo.drawPlayer.direction, origin, 0.8f, drawinfo.playerEffect, 0f));
			}
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x005ACD9C File Offset: 0x005AAF9C
		public static void DrawPlayer_13_Leggings(ref PlayerDrawSet drawinfo)
		{
			Vector2 legsOffset = drawinfo.legsOffset;
			if (drawinfo.drawPlayer.legs == 169)
			{
				return;
			}
			if (drawinfo.isSitting && drawinfo.drawPlayer.legs != 140 && drawinfo.drawPlayer.legs != 217)
			{
				if (drawinfo.drawPlayer.legs > 0 && drawinfo.drawPlayer.legs < ArmorIDs.Legs.Count && (!PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo) || drawinfo.drawPlayer.wearsRobe))
				{
					if (!drawinfo.drawPlayer.invis)
					{
						PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.ArmorLeg[drawinfo.drawPlayer.legs].Value, drawinfo.colorArmorLegs, drawinfo.cLegs, false);
						if (drawinfo.legsGlowMask != -1)
						{
							PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.GlowMask[drawinfo.legsGlowMask].Value, drawinfo.legsGlowColor, drawinfo.cLegs, false);
							return;
						}
					}
				}
				else if (!drawinfo.drawPlayer.invis && !PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo))
				{
					PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.Players[drawinfo.skinVar, 11].Value, drawinfo.colorPants, 0, false);
					PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.Players[drawinfo.skinVar, 12].Value, drawinfo.colorShoes, 0, false);
				}
				return;
			}
			if (drawinfo.drawPlayer.legs == 140)
			{
				if (!drawinfo.drawPlayer.invis && !drawinfo.drawPlayer.mount.Active)
				{
					Texture2D value = TextureAssets.Extra[73].Value;
					bool flag = drawinfo.drawPlayer.legFrame.Y != drawinfo.drawPlayer.legFrame.Height || Main.gameMenu;
					int num = drawinfo.drawPlayer.miscCounter / 3 % 8;
					if (flag)
					{
						num = drawinfo.drawPlayer.miscCounter / 4 % 8;
					}
					Rectangle rectangle = new Rectangle(18 * flag.ToInt(), num * 26, 16, 24);
					float num2 = 12f;
					if (drawinfo.drawPlayer.bodyFrame.Height != 0)
					{
						num2 = 12f - Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height].Y;
					}
					if (drawinfo.drawPlayer.Directions.Y == -1f)
					{
						num2 -= 6f;
					}
					Vector2 scale = new Vector2(1f, 1f);
					Vector2 value2 = drawinfo.Position + drawinfo.drawPlayer.Size * new Vector2(0.5f, 0.5f + 0.5f * drawinfo.drawPlayer.gravDir);
					int direction = drawinfo.drawPlayer.direction;
					Vector2 vector = value2 + new Vector2((float)0, -num2 * drawinfo.drawPlayer.gravDir) - Main.screenPosition + drawinfo.drawPlayer.legPosition;
					if (drawinfo.isSitting)
					{
						vector.Y += drawinfo.seatYOffset;
					}
					vector += legsOffset;
					vector = vector.Floor();
					DrawData item = new DrawData(value, vector, new Rectangle?(rectangle), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, rectangle.Size() * new Vector2(0.5f, 0.5f - drawinfo.drawPlayer.gravDir * 0.5f), scale, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cLegs;
					drawinfo.DrawDataCache.Add(item);
					return;
				}
			}
			else if (drawinfo.drawPlayer.legs > 0 && drawinfo.drawPlayer.legs < ArmorIDs.Legs.Count && (!PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo) || drawinfo.drawPlayer.wearsRobe))
			{
				if (!drawinfo.drawPlayer.invis)
				{
					DrawData item = new DrawData(TextureAssets.ArmorLeg[drawinfo.drawPlayer.legs].Value, legsOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cLegs;
					drawinfo.DrawDataCache.Add(item);
					if (drawinfo.legsGlowMask != -1)
					{
						if (drawinfo.legsGlowMask == 274)
						{
							for (int i = 0; i < 2; i++)
							{
								Vector2 value3 = new Vector2((float)Main.rand.Next(-10, 10) * 0.125f, (float)Main.rand.Next(-10, 10) * 0.125f);
								item = new DrawData(TextureAssets.GlowMask[drawinfo.legsGlowMask].Value, legsOffset + value3 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.legsGlowColor, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
								item.shader = drawinfo.cLegs;
								drawinfo.DrawDataCache.Add(item);
							}
							return;
						}
						item = new DrawData(TextureAssets.GlowMask[drawinfo.legsGlowMask].Value, legsOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.legsGlowColor, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cLegs;
						drawinfo.DrawDataCache.Add(item);
						return;
					}
				}
			}
			else if (!drawinfo.drawPlayer.invis && !PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo))
			{
				DrawData item = new DrawData(TextureAssets.Players[drawinfo.skinVar, 11].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorPants, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
				item = new DrawData(TextureAssets.Players[drawinfo.skinVar, 12].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorShoes, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x005AD730 File Offset: 0x005AB930
		private static void DrawSittingLongCoats(ref PlayerDrawSet drawinfo, int specialLegCoat, Texture2D textureToDraw, Color matchingColor, int shaderIndex = 0, bool glowmask = false)
		{
			Vector2 legsOffset = drawinfo.legsOffset;
			Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect;
			Rectangle legFrame = drawinfo.drawPlayer.legFrame;
			vector += legsOffset;
			vector.X += (float)(2 * drawinfo.drawPlayer.direction);
			legFrame.Y = legFrame.Height * 5;
			if (specialLegCoat == 160 || specialLegCoat == 173)
			{
				legFrame = drawinfo.drawPlayer.legFrame;
			}
			DrawData item = new DrawData(textureToDraw, vector, new Rectangle?(legFrame), matchingColor, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
			item.shader = shaderIndex;
			drawinfo.DrawDataCache.Add(item);
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x005AD87C File Offset: 0x005ABA7C
		private static void DrawSittingLegs(ref PlayerDrawSet drawinfo, Texture2D textureToDraw, Color matchingColor, int shaderIndex = 0, bool glowmask = false)
		{
			Vector2 legsOffset = drawinfo.legsOffset;
			Vector2 value = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect;
			Rectangle legFrame = drawinfo.drawPlayer.legFrame;
			value.Y -= 2f;
			value.Y += drawinfo.seatYOffset;
			value += legsOffset;
			int num = 2;
			int num2 = 42;
			int num3 = 2;
			int num4 = 2;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			bool flag = drawinfo.drawPlayer.legs == 101 || drawinfo.drawPlayer.legs == 102 || drawinfo.drawPlayer.legs == 118 || drawinfo.drawPlayer.legs == 99;
			if (drawinfo.drawPlayer.wearsRobe && !flag)
			{
				num = 0;
				num4 = 0;
				num2 = 6;
				value.Y += 4f;
				legFrame.Y = legFrame.Height * 5;
			}
			int legs = drawinfo.drawPlayer.legs;
			if (legs <= 143)
			{
				if (legs != 106)
				{
					if (legs == 132)
					{
						num = -2;
						num7 = 2;
						goto IL_2AC;
					}
					if (legs != 143)
					{
						goto IL_2AC;
					}
				}
			}
			else if (legs <= 210)
			{
				if (legs - 193 > 1)
				{
					if (legs != 210)
					{
						goto IL_2AC;
					}
					if (glowmask)
					{
						Vector2 value2 = new Vector2((float)Main.rand.Next(-10, 10) * 0.125f, (float)Main.rand.Next(-10, 10) * 0.125f);
						value += value2;
						goto IL_2AC;
					}
					goto IL_2AC;
				}
				else
				{
					if (drawinfo.drawPlayer.body == 218)
					{
						num = -2;
						num7 = 2;
						value.Y += 2f;
						goto IL_2AC;
					}
					goto IL_2AC;
				}
			}
			else
			{
				if (legs - 214 <= 2)
				{
					num = -6;
					num4 = 2;
					num5 = 2;
					num3 = 4;
					num2 = 6;
					legFrame = drawinfo.drawPlayer.legFrame;
					value.Y += 2f;
					goto IL_2AC;
				}
				if (legs != 226)
				{
					goto IL_2AC;
				}
			}
			num = 0;
			num4 = 0;
			num2 = 6;
			value.Y += 4f;
			legFrame.Y = legFrame.Height * 5;
			IL_2AC:
			for (int i = num3; i >= 0; i--)
			{
				Vector2 position = value + new Vector2((float)num, 2f) * new Vector2((float)drawinfo.drawPlayer.direction, 1f);
				Rectangle value3 = legFrame;
				value3.Y += i * 2;
				value3.Y += num2;
				value3.Height -= num2;
				value3.Height -= i * 2;
				if (i != num3)
				{
					value3.Height = 2;
				}
				position.X += (float)(drawinfo.drawPlayer.direction * num4 * i + num6 * drawinfo.drawPlayer.direction);
				if (i != 0)
				{
					position.X += (float)(num7 * drawinfo.drawPlayer.direction);
				}
				position.Y += (float)num2;
				position.Y += (float)num5;
				DrawData item = new DrawData(textureToDraw, position, new Rectangle?(value3), matchingColor, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = shaderIndex;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x005ADC74 File Offset: 0x005ABE74
		public static void DrawPlayer_14_Shoes(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.shoe > 0 && (int)drawinfo.drawPlayer.shoe < ArmorIDs.Shoe.Count && !PlayerDrawLayers.ShouldOverrideLegs_CheckPants(ref drawinfo))
			{
				int num = drawinfo.cShoe;
				if (drawinfo.drawPlayer.shoe == 22 || drawinfo.drawPlayer.shoe == 23)
				{
					num = drawinfo.cFlameWaker;
				}
				if (drawinfo.isSitting)
				{
					PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.AccShoes[(int)drawinfo.drawPlayer.shoe].Value, drawinfo.colorArmorLegs, num, false);
					return;
				}
				DrawData item = new DrawData(TextureAssets.AccShoes[(int)drawinfo.drawPlayer.shoe].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = num;
				drawinfo.DrawDataCache.Add(item);
				if (drawinfo.drawPlayer.shoe == 25 || drawinfo.drawPlayer.shoe == 26)
				{
					PlayerDrawLayers.DrawPlayer_14_2_GlassSlipperSparkles(ref drawinfo);
				}
			}
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x005ADE30 File Offset: 0x005AC030
		public static void DrawPlayer_14_2_GlassSlipperSparkles(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.shadow != 0f)
			{
				return;
			}
			if (Main.rand.Next(60) == 0)
			{
				Rectangle rectangle = Utils.CenteredRectangle(drawinfo.Position + drawinfo.drawPlayer.Size / 2f + new Vector2(0f, drawinfo.drawPlayer.gravDir * 16f), new Vector2(20f, 8f));
				int num = Dust.NewDust(rectangle.TopLeft(), rectangle.Width, rectangle.Height, 204, 0f, 0f, 150, default(Color), 0.3f);
				Main.dust[num].fadeIn = 1f;
				Main.dust[num].velocity *= 0.1f;
				Main.dust[num].noLight = true;
				Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(drawinfo.drawPlayer.cShoe, drawinfo.drawPlayer);
				drawinfo.DustCache.Add(num);
			}
			if (drawinfo.drawPlayer.velocity.X != 0f && Main.rand.Next(10) == 0)
			{
				Rectangle rectangle2 = Utils.CenteredRectangle(drawinfo.Position + drawinfo.drawPlayer.Size / 2f + new Vector2((float)(drawinfo.drawPlayer.direction * -2), drawinfo.drawPlayer.gravDir * 16f), new Vector2(6f, 8f));
				int num2 = Dust.NewDust(rectangle2.TopLeft(), rectangle2.Width, rectangle2.Height, 204, 0f, 0f, 150, default(Color), 0.3f);
				Main.dust[num2].fadeIn = 1f;
				Main.dust[num2].velocity *= 0.1f;
				Main.dust[num2].noLight = true;
				Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(drawinfo.drawPlayer.cShoe, drawinfo.drawPlayer);
				drawinfo.DustCache.Add(num2);
			}
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x005AE090 File Offset: 0x005AC290
		public static void DrawPlayer_15_SkinLongCoat(ref PlayerDrawSet drawinfo)
		{
			if ((drawinfo.skinVar == 3 || drawinfo.skinVar == 8 || drawinfo.skinVar == 7) && (drawinfo.drawPlayer.body <= 0 || drawinfo.drawPlayer.body >= ArmorIDs.Body.Count) && !drawinfo.drawPlayer.invis)
			{
				if (drawinfo.isSitting)
				{
					PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.Players[drawinfo.skinVar, 14].Value, drawinfo.colorShirt, 0, false);
					return;
				}
				DrawData item = new DrawData(TextureAssets.Players[drawinfo.skinVar, 14].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorShirt, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x005AE21C File Offset: 0x005AC41C
		public static void DrawPlayer_16_ArmorLongCoat(ref PlayerDrawSet drawinfo)
		{
			int num = -1;
			int body = drawinfo.drawPlayer.body;
			if (body <= 182)
			{
				if (body <= 73)
				{
					if (body != 52)
					{
						if (body != 53)
						{
							if (body == 73)
							{
								num = 170;
							}
						}
						else if (drawinfo.drawPlayer.Male)
						{
							num = 175;
						}
						else
						{
							num = 176;
						}
					}
					else if (drawinfo.drawPlayer.Male)
					{
						num = 171;
					}
					else
					{
						num = 172;
					}
				}
				else if (body <= 89)
				{
					if (body != 81)
					{
						if (body == 89)
						{
							num = 186;
						}
					}
					else
					{
						num = 169;
					}
				}
				else if (body != 168)
				{
					if (body == 182)
					{
						num = 163;
					}
				}
				else
				{
					num = 164;
				}
			}
			else if (body <= 218)
			{
				if (body != 187)
				{
					switch (body)
					{
					case 198:
						num = 162;
						break;
					case 199:
					case 203:
					case 204:
					case 206:
					case 208:
						break;
					case 200:
						num = 149;
						break;
					case 201:
						num = 150;
						break;
					case 202:
						num = 151;
						break;
					case 205:
						num = 174;
						break;
					case 207:
						num = 161;
						break;
					case 209:
						num = 160;
						break;
					case 210:
						if (drawinfo.drawPlayer.Male)
						{
							num = 178;
						}
						else
						{
							num = 177;
						}
						break;
					case 211:
						if (drawinfo.drawPlayer.Male)
						{
							num = 182;
						}
						else
						{
							num = 181;
						}
						break;
					default:
						if (body == 218)
						{
							num = 195;
						}
						break;
					}
				}
				else
				{
					num = 173;
				}
			}
			else if (body <= 225)
			{
				if (body != 222)
				{
					if (body == 225)
					{
						num = 206;
					}
				}
				else if (drawinfo.drawPlayer.Male)
				{
					num = 201;
				}
				else
				{
					num = 200;
				}
			}
			else if (body != 236)
			{
				if (body == 237)
				{
					num = 223;
				}
			}
			else
			{
				num = 221;
			}
			if (num != -1)
			{
				Main.instance.LoadArmorLegs(num);
				if (drawinfo.isSitting && num != 195)
				{
					PlayerDrawLayers.DrawSittingLongCoats(ref drawinfo, num, TextureAssets.ArmorLeg[num].Value, drawinfo.colorArmorBody, drawinfo.cBody, false);
					return;
				}
				DrawData item = new DrawData(TextureAssets.ArmorLeg[num].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cBody;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x005AE5C0 File Offset: 0x005AC7C0
		public static void DrawPlayer_17_Torso(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.usesCompositeTorso)
			{
				PlayerDrawLayers.DrawPlayer_17_TorsoComposite(ref drawinfo);
				return;
			}
			if (drawinfo.drawPlayer.body > 0 && drawinfo.drawPlayer.body < ArmorIDs.Body.Count)
			{
				Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
				int num = drawinfo.armorAdjust;
				bodyFrame.X += num;
				bodyFrame.Width -= num;
				if (drawinfo.drawPlayer.direction == -1)
				{
					num = 0;
				}
				if (!drawinfo.drawPlayer.invis || (drawinfo.drawPlayer.body != 21 && drawinfo.drawPlayer.body != 22))
				{
					Texture2D value;
					if (!drawinfo.drawPlayer.Male)
					{
						value = TextureAssets.FemaleBody[drawinfo.drawPlayer.body].Value;
					}
					else
					{
						value = TextureAssets.ArmorBody[drawinfo.drawPlayer.body].Value;
					}
					DrawData item = new DrawData(value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2)) + num), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cBody;
					drawinfo.DrawDataCache.Add(item);
					if (drawinfo.bodyGlowMask != -1)
					{
						item = new DrawData(TextureAssets.GlowMask[drawinfo.bodyGlowMask].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2)) + num), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(bodyFrame), drawinfo.bodyGlowColor, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cBody;
						drawinfo.DrawDataCache.Add(item);
					}
				}
				if (drawinfo.missingHand && !drawinfo.drawPlayer.invis)
				{
					DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 5].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
					{
						shader = drawinfo.skinDyePacked
					};
					DrawData item = drawData;
					drawinfo.DrawDataCache.Add(item);
					return;
				}
			}
			else if (!drawinfo.drawPlayer.invis)
			{
				DrawData item;
				if (!drawinfo.drawPlayer.Male)
				{
					item = new DrawData(TextureAssets.Players[drawinfo.skinVar, 4].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorUnderShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
					item = new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
				}
				else
				{
					item = new DrawData(TextureAssets.Players[drawinfo.skinVar, 4].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorUnderShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
					item = new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
				}
				DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 5].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.skinDyePacked
				};
				item = drawData;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x005AEFF0 File Offset: 0x005AD1F0
		public static void DrawPlayer_17_TorsoComposite(ref PlayerDrawSet drawinfo)
		{
			Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
			Vector2 value = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
			value.Y -= 2f;
			vector += value * (float)(-(float)drawinfo.playerEffect.HasFlag(SpriteEffects.FlipVertically).ToDirectionInt());
			float bodyRotation = drawinfo.drawPlayer.bodyRotation;
			Vector2 value2 = vector;
			Vector2 value3 = drawinfo.bodyVect;
			Vector2 compositeOffset_BackArm = PlayerDrawLayers.GetCompositeOffset_BackArm(ref drawinfo);
			value2 + compositeOffset_BackArm;
			value3 += compositeOffset_BackArm;
			if (drawinfo.drawPlayer.body > 0 && drawinfo.drawPlayer.body < ArmorIDs.Body.Count)
			{
				if (!drawinfo.drawPlayer.invis || PlayerDrawLayers.IsArmorDrawnWhenInvisible(drawinfo.drawPlayer.body))
				{
					Texture2D value4 = TextureAssets.ArmorBodyComposite[drawinfo.drawPlayer.body].Value;
					CompositePlayerDrawContext context = CompositePlayerDrawContext.Torso;
					DrawData drawData = new DrawData(value4, vector, new Rectangle?(drawinfo.compTorsoFrame), drawinfo.colorArmorBody, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
					{
						shader = drawinfo.cBody
					};
					PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, context, drawData);
				}
			}
			else if (!drawinfo.drawPlayer.invis)
			{
				drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 4].Value, vector, new Rectangle?(drawinfo.compBackShoulderFrame), drawinfo.colorUnderShirt, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f));
				drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, vector, new Rectangle?(drawinfo.compBackShoulderFrame), drawinfo.colorShirt, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f));
				drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 4].Value, vector, new Rectangle?(drawinfo.compTorsoFrame), drawinfo.colorUnderShirt, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f));
				drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, vector, new Rectangle?(drawinfo.compTorsoFrame), drawinfo.colorShirt, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f));
			}
			if (drawinfo.drawFloatingTube)
			{
				List<DrawData> drawDataCache = drawinfo.DrawDataCache;
				DrawData drawData = new DrawData(TextureAssets.Extra[105].Value, vector, new Rectangle?(new Rectangle(0, 56, 40, 56)), drawinfo.floatingTubeColor, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.cFloatingTube
				};
				drawDataCache.Add(drawData);
			}
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x005AF3AC File Offset: 0x005AD5AC
		public static void DrawPlayer_18_OffhandAcc(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.usesCompositeBackHandAcc)
			{
				return;
			}
			if (drawinfo.drawPlayer.handoff > 0 && (int)drawinfo.drawPlayer.handoff < ArmorIDs.HandOff.Count)
			{
				DrawData item = new DrawData(TextureAssets.AccHandsOff[(int)drawinfo.drawPlayer.handoff].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cHandOff;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x005AF514 File Offset: 0x005AD714
		public static void DrawPlayer_JimsDroneRadio(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.HeldItem.type == 5451 && drawinfo.drawPlayer.itemAnimation == 0)
			{
				Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
				Texture2D value = TextureAssets.Extra[261].Value;
				DrawData item = new DrawData(value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2)) + drawinfo.drawPlayer.direction * 2), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f + 14f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(bodyFrame), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cWaist;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x005AF688 File Offset: 0x005AD888
		public static void DrawPlayer_19_WaistAcc(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.waist > 0 && (int)drawinfo.drawPlayer.waist < ArmorIDs.Waist.Count)
			{
				Rectangle value = drawinfo.drawPlayer.legFrame;
				if (ArmorIDs.Waist.Sets.UsesTorsoFraming[(int)drawinfo.drawPlayer.waist])
				{
					value = drawinfo.drawPlayer.bodyFrame;
				}
				DrawData item = new DrawData(TextureAssets.AccWaist[(int)drawinfo.drawPlayer.waist].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(value), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cWaist;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x005AF7E4 File Offset: 0x005AD9E4
		public static void DrawPlayer_20_NeckAcc(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.neck > 0 && (int)drawinfo.drawPlayer.neck < ArmorIDs.Neck.Count)
			{
				DrawData item = new DrawData(TextureAssets.AccNeck[(int)drawinfo.drawPlayer.neck].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cNeck;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x005AF944 File Offset: 0x005ADB44
		public static void DrawPlayer_21_Head(ref PlayerDrawSet drawinfo)
		{
			Vector2 helmetOffset = drawinfo.helmetOffset;
			PlayerDrawLayers.DrawPlayer_21_Head_TheFace(ref drawinfo);
			bool flag = drawinfo.drawPlayer.head == 14 || drawinfo.drawPlayer.head == 56 || drawinfo.drawPlayer.head == 114 || drawinfo.drawPlayer.head == 158 || drawinfo.drawPlayer.head == 69 || drawinfo.drawPlayer.head == 180;
			bool flag2 = drawinfo.drawPlayer.head == 28;
			bool flag3 = drawinfo.drawPlayer.head == 39 || drawinfo.drawPlayer.head == 38;
			Vector2 value = new Vector2((float)(-(float)drawinfo.drawPlayer.bodyFrame.Width / 2 + drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height + 4));
			Vector2 vector = (drawinfo.Position - Main.screenPosition + value).Floor() + drawinfo.drawPlayer.headPosition + drawinfo.headVect;
			if (drawinfo.playerEffect.HasFlag(SpriteEffects.FlipVertically))
			{
				int num = drawinfo.drawPlayer.bodyFrame.Height - drawinfo.hairFrontFrame.Height;
				vector.Y += (float)num;
			}
			vector += drawinfo.hairOffset;
			if (drawinfo.fullHair)
			{
				Color color = drawinfo.colorArmorHead;
				int shader = drawinfo.cHead;
				if (ArmorIDs.Head.Sets.UseSkinColor[drawinfo.drawPlayer.head])
				{
					if (drawinfo.drawPlayer.isDisplayDollOrInanimate)
					{
						color = drawinfo.colorDisplayDollSkin;
					}
					else
					{
						color = drawinfo.colorHead;
					}
					shader = drawinfo.skinDyePacked;
				}
				DrawData item = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = shader;
				drawinfo.DrawDataCache.Add(item);
				if (!drawinfo.drawPlayer.invis)
				{
					item = new DrawData(TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, vector, new Rectangle?(drawinfo.hairFrontFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.hairDyePacked;
					drawinfo.DrawDataCache.Add(item);
				}
			}
			if (drawinfo.hatHair && !drawinfo.drawPlayer.invis)
			{
				DrawData item = new DrawData(TextureAssets.PlayerHairAlt[drawinfo.drawPlayer.hair].Value, vector, new Rectangle?(drawinfo.hairFrontFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.hairDyePacked;
				drawinfo.DrawDataCache.Add(item);
			}
			if (drawinfo.drawPlayer.head == 270)
			{
				Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
				bodyFrame.Width += 2;
				DrawData item = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cHead;
				drawinfo.DrawDataCache.Add(item);
				item = new DrawData(TextureAssets.GlowMask[drawinfo.headGlowMask].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cHead;
				drawinfo.DrawDataCache.Add(item);
			}
			else if (flag)
			{
				Rectangle bodyFrame2 = drawinfo.drawPlayer.bodyFrame;
				Vector2 headVect = drawinfo.headVect;
				if (drawinfo.drawPlayer.gravDir == 1f)
				{
					if (bodyFrame2.Y != 0)
					{
						bodyFrame2.Y -= 2;
						headVect.Y += 2f;
					}
					bodyFrame2.Height -= 8;
				}
				else if (bodyFrame2.Y != 0)
				{
					bodyFrame2.Y -= 2;
					headVect.Y -= 10f;
					bodyFrame2.Height -= 8;
				}
				Color color2 = drawinfo.colorArmorHead;
				int shader2 = drawinfo.cHead;
				if (ArmorIDs.Head.Sets.UseSkinColor[drawinfo.drawPlayer.head])
				{
					if (drawinfo.drawPlayer.isDisplayDollOrInanimate)
					{
						color2 = drawinfo.colorDisplayDollSkin;
					}
					else
					{
						color2 = drawinfo.colorHead;
					}
					shader2 = drawinfo.skinDyePacked;
				}
				DrawData item = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame2), color2, drawinfo.drawPlayer.headRotation, headVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = shader2;
				drawinfo.DrawDataCache.Add(item);
			}
			else if (drawinfo.drawPlayer.head == 259)
			{
				int verticalFrames = 27;
				Texture2D value2 = TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value;
				Rectangle rectangle = value2.Frame(1, verticalFrames, 0, drawinfo.drawPlayer.rabbitOrderFrame.DisplayFrame, 0, 0);
				Vector2 origin = rectangle.Size() / 2f;
				int num2 = drawinfo.drawPlayer.babyBird.ToInt();
				Vector2 value3 = PlayerDrawLayers.DrawPlayer_21_Head_GetSpecialHatDrawPosition(ref drawinfo, ref helmetOffset, new Vector2((float)(1 + num2 * 2), (float)(-26 + drawinfo.drawPlayer.babyBird.ToInt() * -6)));
				int hatStacks = PlayerDrawLayers.GetHatStacks(ref drawinfo, 4955);
				float num3 = 0.05235988f;
				float num4 = num3 * drawinfo.drawPlayer.position.X % 6.2831855f;
				for (int i = hatStacks - 1; i >= 0; i--)
				{
					float x = Vector2.UnitY.RotatedBy((double)(num4 + num3 * (float)i), default(Vector2)).X * ((float)i / 30f) * 2f - (float)(i * 2 * drawinfo.drawPlayer.direction);
					DrawData item = new DrawData(value2, value3 + new Vector2(x, (float)(i * -14) * drawinfo.drawPlayer.gravDir), new Rectangle?(rectangle), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, origin, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cHead;
					drawinfo.DrawDataCache.Add(item);
				}
				if (!drawinfo.drawPlayer.invis)
				{
					DrawData item = new DrawData(TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, vector, new Rectangle?(drawinfo.hairFrontFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.hairDyePacked;
					drawinfo.DrawDataCache.Add(item);
				}
			}
			else if (drawinfo.drawPlayer.head > 0 && drawinfo.drawPlayer.head < ArmorIDs.Head.Count && !flag2)
			{
				if (!drawinfo.drawPlayer.invis || !flag3)
				{
					if (drawinfo.drawPlayer.head == 13)
					{
						int hatStacks2 = PlayerDrawLayers.GetHatStacks(ref drawinfo, 205);
						float num5 = 0.05235988f;
						float num6 = num5 * drawinfo.drawPlayer.position.X % 6.2831855f;
						for (int j = 0; j < hatStacks2; j++)
						{
							float num7 = Vector2.UnitY.RotatedBy((double)(num6 + num5 * (float)j), default(Vector2)).X * ((float)j / 30f) * 2f;
							DrawData item = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))) + num7, (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f - (float)(4 * j)))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
							item.shader = drawinfo.cHead;
							drawinfo.DrawDataCache.Add(item);
						}
					}
					else if (drawinfo.drawPlayer.head == 265)
					{
						int verticalFrames2 = 6;
						Texture2D value4 = TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value;
						Rectangle rectangle2 = value4.Frame(1, verticalFrames2, 0, drawinfo.drawPlayer.rabbitOrderFrame.DisplayFrame, 0, 0);
						Vector2 origin2 = rectangle2.Size() / 2f;
						Vector2 value5 = PlayerDrawLayers.DrawPlayer_21_Head_GetSpecialHatDrawPosition(ref drawinfo, ref helmetOffset, new Vector2(0f, -9f));
						int hatStacks3 = PlayerDrawLayers.GetHatStacks(ref drawinfo, 5004);
						float num8 = 0.05235988f;
						float num9 = num8 * drawinfo.drawPlayer.position.X % 6.2831855f;
						int num10 = hatStacks3 * 4 + 2;
						int num11 = 0;
						bool flag4 = (Main.GlobalTimeWrappedHourly + 180f) % 600f < 60f;
						for (int k = num10 - 1; k >= 0; k--)
						{
							int num12 = 0;
							if (k == num10 - 1)
							{
								rectangle2.Y = 0;
								num12 = 2;
							}
							else if (k == 0)
							{
								rectangle2.Y = rectangle2.Height * 5;
							}
							else
							{
								rectangle2.Y = rectangle2.Height * (num11++ % 4 + 1);
							}
							if (rectangle2.Y != rectangle2.Height * 3 || !flag4)
							{
								float x2 = Vector2.UnitY.RotatedBy((double)(num9 + num8 * (float)k), default(Vector2)).X * ((float)k / 10f) * 4f - (float)k * 0.1f * (float)drawinfo.drawPlayer.direction;
								DrawData item = new DrawData(value4, value5 + new Vector2(x2, (float)(k * -4 + num12) * drawinfo.drawPlayer.gravDir), new Rectangle?(rectangle2), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, origin2, 1f, drawinfo.playerEffect, 0f);
								item.shader = drawinfo.cHead;
								drawinfo.DrawDataCache.Add(item);
							}
						}
					}
					else
					{
						Rectangle bodyFrame3 = drawinfo.drawPlayer.bodyFrame;
						Vector2 headVect2 = drawinfo.headVect;
						if (drawinfo.drawPlayer.gravDir == 1f)
						{
							bodyFrame3.Height -= 4;
						}
						else
						{
							headVect2.Y -= 4f;
							bodyFrame3.Height -= 4;
						}
						Color color3 = drawinfo.colorArmorHead;
						int shader3 = drawinfo.cHead;
						if (ArmorIDs.Head.Sets.UseSkinColor[drawinfo.drawPlayer.head])
						{
							if (drawinfo.drawPlayer.isDisplayDollOrInanimate)
							{
								color3 = drawinfo.colorDisplayDollSkin;
							}
							else
							{
								color3 = drawinfo.colorHead;
							}
							shader3 = drawinfo.skinDyePacked;
						}
						DrawData item = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame3), color3, drawinfo.drawPlayer.headRotation, headVect2, 1f, drawinfo.playerEffect, 0f);
						item.shader = shader3;
						drawinfo.DrawDataCache.Add(item);
						if (drawinfo.headGlowMask != -1)
						{
							if (drawinfo.headGlowMask == 309)
							{
								int num13 = PlayerDrawLayers.DrawPlayer_Head_GetTVScreen(drawinfo.drawPlayer);
								if (num13 != 0)
								{
									int num14 = 0;
									num14 += drawinfo.drawPlayer.bodyFrame.Y / 56;
									if (num14 >= Main.OffsetsPlayerHeadgear.Length)
									{
										num14 = 0;
									}
									Vector2 vector2 = Main.OffsetsPlayerHeadgear[num14];
									vector2.Y -= 2f;
									vector2 *= (float)(-(float)drawinfo.playerEffect.HasFlag(SpriteEffects.FlipVertically).ToDirectionInt());
									Texture2D value6 = TextureAssets.GlowMask[drawinfo.headGlowMask].Value;
									int frameY = drawinfo.drawPlayer.miscCounter % 20 / 5;
									if (num13 == 5)
									{
										frameY = 0;
										if (drawinfo.drawPlayer.eyeHelper.EyeFrameToShow > 0)
										{
											frameY = 2;
										}
									}
									Rectangle value7 = value6.Frame(6, 4, num13, frameY, -2, 0);
									item = new DrawData(value6, vector2 + helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(value7), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
									item.shader = drawinfo.cHead;
									drawinfo.DrawDataCache.Add(item);
								}
							}
							else if (drawinfo.headGlowMask == 273)
							{
								for (int l = 0; l < 2; l++)
								{
									Vector2 value8 = new Vector2((float)Main.rand.Next(-10, 10) * 0.125f, (float)Main.rand.Next(-10, 10) * 0.125f);
									item = new DrawData(TextureAssets.GlowMask[drawinfo.headGlowMask].Value, value8 + helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame3), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, headVect2, 1f, drawinfo.playerEffect, 0f);
									item.shader = drawinfo.cHead;
									drawinfo.DrawDataCache.Add(item);
								}
							}
							else
							{
								item = new DrawData(TextureAssets.GlowMask[drawinfo.headGlowMask].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame3), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, headVect2, 1f, drawinfo.playerEffect, 0f);
								item.shader = drawinfo.cHead;
								drawinfo.DrawDataCache.Add(item);
							}
						}
						if (drawinfo.drawPlayer.head == 211)
						{
							Color color4 = new Color(100, 100, 100, 0);
							ulong num15 = (ulong)((long)(drawinfo.drawPlayer.miscCounter / 4 + 100));
							int num16 = 4;
							for (int m = 0; m < num16; m++)
							{
								float x3 = (float)Utils.RandomInt(ref num15, -10, 11) * 0.2f;
								float y = (float)Utils.RandomInt(ref num15, -14, 1) * 0.15f;
								item = new DrawData(TextureAssets.GlowMask[241].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + new Vector2(x3, y), new Rectangle?(drawinfo.drawPlayer.bodyFrame), color4, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
								item.shader = drawinfo.cHead;
								drawinfo.DrawDataCache.Add(item);
							}
						}
					}
				}
			}
			else if (!drawinfo.drawPlayer.invis && (drawinfo.drawPlayer.face < 0 || !ArmorIDs.Face.Sets.PreventHairDraw[(int)drawinfo.drawPlayer.face]))
			{
				DrawData item = new DrawData(TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, vector, new Rectangle?(drawinfo.hairFrontFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.hairDyePacked;
				drawinfo.DrawDataCache.Add(item);
			}
			if (drawinfo.drawPlayer.beard > 0 && (drawinfo.drawPlayer.head < 0 || !ArmorIDs.Head.Sets.PreventBeardDraw[drawinfo.drawPlayer.head]))
			{
				Vector2 beardDrawOffsetFromHelmet = drawinfo.drawPlayer.GetBeardDrawOffsetFromHelmet();
				Color color5 = drawinfo.colorArmorHead;
				if (ArmorIDs.Beard.Sets.UseHairColor[(int)drawinfo.drawPlayer.beard])
				{
					color5 = drawinfo.colorHair;
				}
				DrawData item = new DrawData(TextureAssets.AccBeard[(int)drawinfo.drawPlayer.beard].Value, beardDrawOffsetFromHelmet + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), color5, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cBeard;
				drawinfo.DrawDataCache.Add(item);
			}
			if (drawinfo.drawPlayer.head == 205)
			{
				DrawData item = new DrawData(TextureAssets.Extra[77].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.skinDyePacked
				};
				drawinfo.DrawDataCache.Add(item);
			}
			if (drawinfo.drawPlayer.head == 214 && !drawinfo.drawPlayer.invis)
			{
				Rectangle bodyFrame4 = drawinfo.drawPlayer.bodyFrame;
				bodyFrame4.Y = 0;
				float num17 = (float)drawinfo.drawPlayer.miscCounter / 300f;
				Color color6 = new Color(0, 0, 0, 0);
				float num18 = 0.8f;
				float num19 = 0.9f;
				if (num17 >= num18)
				{
					color6 = Color.Lerp(Color.Transparent, new Color(200, 200, 200, 0), Utils.GetLerpValue(num18, num19, num17, true));
				}
				if (num17 >= num19)
				{
					color6 = Color.Lerp(Color.Transparent, new Color(200, 200, 200, 0), Utils.GetLerpValue(1f, num19, num17, true));
				}
				color6 *= drawinfo.stealth * (1f - drawinfo.shadow);
				DrawData item = new DrawData(TextureAssets.Extra[90].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect - Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height], new Rectangle?(bodyFrame4), color6, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
			}
			if (drawinfo.drawPlayer.head == 137)
			{
				DrawData item = new DrawData(TextureAssets.JackHat.Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), new Color(255, 255, 255, 255), drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
				for (int n = 0; n < 7; n++)
				{
					Color color7 = new Color(110 - n * 10, 110 - n * 10, 110 - n * 10, 110 - n * 10);
					Vector2 vector3 = new Vector2((float)Main.rand.Next(-10, 11) * 0.2f, (float)Main.rand.Next(-10, 11) * 0.2f);
					vector3.X = drawinfo.drawPlayer.itemFlamePos[n].X;
					vector3.Y = drawinfo.drawPlayer.itemFlamePos[n].Y;
					vector3 *= 0.5f;
					item = new DrawData(TextureAssets.JackHat.Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + vector3, new Rectangle?(drawinfo.drawPlayer.bodyFrame), color7, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
				}
			}
			if (drawinfo.drawPlayer.babyBird)
			{
				Rectangle bodyFrame5 = drawinfo.drawPlayer.bodyFrame;
				bodyFrame5.Y = 0;
				Vector2 value9 = Vector2.Zero;
				Color color8 = drawinfo.colorArmorHead;
				if (drawinfo.drawPlayer.mount.Active && drawinfo.drawPlayer.mount.Type == 52)
				{
					Vector2 mountedCenter = drawinfo.drawPlayer.MountedCenter;
					color8 = drawinfo.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)mountedCenter.X / 16, (int)mountedCenter.Y / 16, Color.White), drawinfo.shadow);
					value9 = new Vector2(0f, 6f) * drawinfo.drawPlayer.Directions;
				}
				DrawData item = new DrawData(TextureAssets.Extra[100].Value, value9 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height] * drawinfo.drawPlayer.gravDir, new Rectangle?(bodyFrame5), color8, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x005B184C File Offset: 0x005AFA4C
		public static int DrawPlayer_Head_GetTVScreen(Player plr)
		{
			if (NPC.AnyDanger(false, false))
			{
				return 4;
			}
			if (plr.statLife <= plr.statLifeMax2 / 4)
			{
				return 1;
			}
			if (plr.ZoneCorrupt || plr.ZoneCrimson || plr.ZoneGraveyard)
			{
				return 0;
			}
			if (plr.wet)
			{
				return 2;
			}
			if (plr.townNPCs >= 3f || BirthdayParty.PartyIsUp || LanternNight.LanternsUp)
			{
				return 5;
			}
			return 3;
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x005B18B8 File Offset: 0x005AFAB8
		private static int GetHatStacks(ref PlayerDrawSet drawinfo, int hatItemId)
		{
			int num = 0;
			int num2 = 0;
			if (drawinfo.drawPlayer.armor[num2] != null && drawinfo.drawPlayer.armor[num2].type == hatItemId && drawinfo.drawPlayer.armor[num2].stack > 0)
			{
				num += drawinfo.drawPlayer.armor[num2].stack;
			}
			num2 = 10;
			if (drawinfo.drawPlayer.armor[num2] != null && drawinfo.drawPlayer.armor[num2].type == hatItemId && drawinfo.drawPlayer.armor[num2].stack > 0)
			{
				num += drawinfo.drawPlayer.armor[num2].stack;
			}
			if (num > 2)
			{
				num = 2;
			}
			return num;
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x005B1970 File Offset: 0x005AFB70
		private static Vector2 DrawPlayer_21_Head_GetSpecialHatDrawPosition(ref PlayerDrawSet drawinfo, ref Vector2 helmetOffset, Vector2 hatOffset)
		{
			Vector2 value = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height] * drawinfo.drawPlayer.Directions;
			Vector2 vector = drawinfo.Position - Main.screenPosition + helmetOffset + new Vector2((float)(-(float)drawinfo.drawPlayer.bodyFrame.Width / 2 + drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height + 4)) + hatOffset * drawinfo.drawPlayer.Directions + value;
			vector = vector.Floor();
			vector += drawinfo.drawPlayer.headPosition + drawinfo.headVect;
			if (drawinfo.drawPlayer.gravDir == -1f)
			{
				vector.Y += 12f;
			}
			vector = vector.Floor();
			return vector;
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x005B1A8C File Offset: 0x005AFC8C
		private static void DrawPlayer_21_Head_TheFace(ref PlayerDrawSet drawinfo)
		{
			bool flag = drawinfo.drawPlayer.head == 38 || drawinfo.drawPlayer.head == 135 || drawinfo.drawPlayer.head == 269;
			if (!flag && drawinfo.drawPlayer.faceHead > 0 && drawinfo.drawPlayer.faceHead < ArmorIDs.Face.Count)
			{
				Vector2 faceHeadOffsetFromHelmet = drawinfo.drawPlayer.GetFaceHeadOffsetFromHelmet();
				DrawData item = new DrawData(TextureAssets.AccFace[(int)drawinfo.drawPlayer.faceHead].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + faceHeadOffsetFromHelmet, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cFaceHead;
				drawinfo.DrawDataCache.Add(item);
				if (drawinfo.drawPlayer.face > 0 && ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int)drawinfo.drawPlayer.face])
				{
					float num = 0f;
					if (drawinfo.drawPlayer.face == 5)
					{
						sbyte faceHead = drawinfo.drawPlayer.faceHead;
						if (faceHead - 10 <= 3)
						{
							num = (float)(2 * drawinfo.drawPlayer.direction);
						}
					}
					item = new DrawData(TextureAssets.AccFace[(int)drawinfo.drawPlayer.face].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))) + num, (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cFace;
					drawinfo.DrawDataCache.Add(item);
					return;
				}
			}
			else if (!drawinfo.drawPlayer.invis && !flag)
			{
				DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 0].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.skinDyePacked
				};
				DrawData item = drawData;
				drawinfo.DrawDataCache.Add(item);
				item = new DrawData(TextureAssets.Players[drawinfo.skinVar, 1].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorEyeWhites, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
				item = new DrawData(TextureAssets.Players[drawinfo.skinVar, 2].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorEyes, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
				Asset<Texture2D> asset = TextureAssets.Players[drawinfo.skinVar, 15];
				if (asset.IsLoaded)
				{
					Vector2 vector = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
					vector.Y -= 2f;
					vector *= (float)(-(float)drawinfo.playerEffect.HasFlag(SpriteEffects.FlipVertically).ToDirectionInt());
					Rectangle value = asset.Frame(1, 3, 0, drawinfo.drawPlayer.eyeHelper.EyeFrameToShow, 0, 0);
					drawData = new DrawData(asset.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + vector, new Rectangle?(value), drawinfo.colorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f)
					{
						shader = drawinfo.skinDyePacked
					};
					item = drawData;
					drawinfo.DrawDataCache.Add(item);
				}
				if (drawinfo.drawPlayer.yoraiz0rDarkness)
				{
					drawData = new DrawData(TextureAssets.Extra[67].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f)
					{
						shader = drawinfo.skinDyePacked
					};
					item = drawData;
					drawinfo.DrawDataCache.Add(item);
				}
				if (drawinfo.drawPlayer.face > 0 && ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int)drawinfo.drawPlayer.face])
				{
					item = new DrawData(TextureAssets.AccFace[(int)drawinfo.drawPlayer.face].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cFace;
					drawinfo.DrawDataCache.Add(item);
				}
			}
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x005B245C File Offset: 0x005B065C
		public static void DrawPlayer_21_1_Magiluminescence(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.shadow == 0f && drawinfo.drawPlayer.neck == 11 && !drawinfo.hideEntirePlayer)
			{
				Color colorArmorBody = drawinfo.colorArmorBody;
				Color color = new Color(140, 140, 35, 12);
				float amount = (float)(colorArmorBody.R + colorArmorBody.G + colorArmorBody.B) / 3f / 255f;
				color = Color.Lerp(color, Color.Transparent, amount);
				if (color == Color.Transparent)
				{
					return;
				}
				DrawData item = new DrawData(TextureAssets.GlowMask[310].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), color, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cNeck;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x005B2614 File Offset: 0x005B0814
		public static void DrawPlayer_22_FaceAcc(ref PlayerDrawSet drawinfo)
		{
			Vector2 vector = Vector2.Zero;
			if (drawinfo.drawPlayer.mount.Active && drawinfo.drawPlayer.mount.Type == 52)
			{
				vector = new Vector2(28f, -2f);
			}
			vector *= drawinfo.drawPlayer.Directions;
			if (drawinfo.drawPlayer.face > 0 && drawinfo.drawPlayer.face < ArmorIDs.Face.Count && !ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int)drawinfo.drawPlayer.face])
			{
				Vector2 value = Vector2.Zero;
				if (drawinfo.drawPlayer.face == 19)
				{
					value = new Vector2(0f, -6f) * drawinfo.drawPlayer.Directions;
				}
				DrawData item = new DrawData(TextureAssets.AccFace[(int)drawinfo.drawPlayer.face].Value, value + vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cFace;
				drawinfo.DrawDataCache.Add(item);
			}
			if (drawinfo.drawPlayer.faceFlower > 0 && drawinfo.drawPlayer.faceFlower < ArmorIDs.Face.Count)
			{
				DrawData item = new DrawData(TextureAssets.AccFace[(int)drawinfo.drawPlayer.faceFlower].Value, vector + drawinfo.helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cFaceFlower;
				drawinfo.DrawDataCache.Add(item);
			}
			if (drawinfo.drawUnicornHorn)
			{
				DrawData item = new DrawData(TextureAssets.Extra[143].Value, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cUnicornHorn;
				drawinfo.DrawDataCache.Add(item);
			}
			if (drawinfo.drawAngelHalo)
			{
				Color color = drawinfo.drawPlayer.GetImmuneAlphaPure(new Color(200, 200, 200, 150), drawinfo.shadow);
				color *= drawinfo.drawPlayer.stealth;
				Main.instance.LoadAccFace(7);
				DrawData item = new DrawData(TextureAssets.AccFace[7].Value, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cAngelHalo;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x005B2B90 File Offset: 0x005B0D90
		public static void DrawTiedBalloons(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.mount.Type != 34)
			{
				return;
			}
			Texture2D value = TextureAssets.Extra[141].Value;
			Vector2 value2 = new Vector2(0f, 4f);
			Color colorMount = drawinfo.colorMount;
			int frameY = (int)(Main.GlobalTimeWrappedHourly * 3f + drawinfo.drawPlayer.position.X / 50f) % 3;
			Rectangle rectangle = value.Frame(1, 3, 0, frameY, 0, 0);
			Vector2 origin = new Vector2((float)(rectangle.Width / 2), (float)rectangle.Height);
			float rotation = -drawinfo.drawPlayer.velocity.X * 0.1f - drawinfo.drawPlayer.fullRotation;
			DrawData item = new DrawData(value, drawinfo.drawPlayer.MountedCenter + value2 - Main.screenPosition, new Rectangle?(rectangle), colorMount, rotation, origin, 1f, drawinfo.playerEffect, 0f);
			drawinfo.DrawDataCache.Add(item);
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x005B2C9C File Offset: 0x005B0E9C
		public static void DrawStarboardRainbowTrail(ref PlayerDrawSet drawinfo, Vector2 commonWingPosPreFloor, Vector2 dirsVec)
		{
			if (drawinfo.shadow != 0f)
			{
				return;
			}
			int num = Math.Min(drawinfo.drawPlayer.availableAdvancedShadowsCount - 1, 30);
			float num2 = 0f;
			for (int i = num; i > 0; i--)
			{
				EntityShadowInfo advancedShadow = drawinfo.drawPlayer.GetAdvancedShadow(i);
				EntityShadowInfo advancedShadow2 = drawinfo.drawPlayer.GetAdvancedShadow(i - 1);
				num2 += Vector2.Distance(advancedShadow.Position, advancedShadow2.Position);
			}
			float scale = MathHelper.Clamp(num2 / 160f, 0f, 1f);
			Main.instance.LoadProjectile(250);
			Texture2D value = TextureAssets.Projectile[250].Value;
			float x = 1.7f;
			Vector2 origin = new Vector2((float)(value.Width / 2), (float)(value.Height / 2));
			new Vector2((float)drawinfo.drawPlayer.width, (float)drawinfo.drawPlayer.height) / 2f;
			Color white = Color.White;
			white.A = 64;
			Vector2 value2 = drawinfo.drawPlayer.DefaultSize * new Vector2(0.5f, 1f) + new Vector2(0f, -4f);
			if (dirsVec.Y < 0f)
			{
				value2 = drawinfo.drawPlayer.DefaultSize * new Vector2(0.5f, 0f) + new Vector2(0f, 4f);
			}
			for (int j = num; j > 0; j--)
			{
				EntityShadowInfo advancedShadow3 = drawinfo.drawPlayer.GetAdvancedShadow(j);
				EntityShadowInfo advancedShadow4 = drawinfo.drawPlayer.GetAdvancedShadow(j - 1);
				Vector2 vector = advancedShadow3.Position + value2 + advancedShadow3.HeadgearOffset;
				Vector2 vector2 = advancedShadow4.Position + value2 + advancedShadow4.HeadgearOffset;
				vector = drawinfo.drawPlayer.RotatedRelativePoint(vector, true, false);
				vector2 = drawinfo.drawPlayer.RotatedRelativePoint(vector2, true, false);
				float rotation = (vector2 - vector).ToRotation() - 1.5707964f;
				rotation = 1.5707964f * (float)drawinfo.drawPlayer.direction;
				float num3 = Math.Abs(vector2.X - vector.X);
				Vector2 scale2 = new Vector2(x, num3 / (float)value.Height);
				float num4 = 1f - (float)j / (float)num;
				num4 *= num4;
				num4 *= Utils.GetLerpValue(0f, 4f, num3, true);
				num4 *= 0.5f;
				num4 *= num4;
				Color color = white * num4 * scale;
				if (!(color == Color.Transparent))
				{
					DrawData item = new DrawData(value, vector - Main.screenPosition, null, color, rotation, origin, scale2, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cWings;
					drawinfo.DrawDataCache.Add(item);
					for (float num5 = 0.25f; num5 < 1f; num5 += 0.25f)
					{
						item = new DrawData(value, Vector2.Lerp(vector, vector2, num5) - Main.screenPosition, null, color, rotation, origin, scale2, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(item);
					}
				}
			}
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x005B301C File Offset: 0x005B121C
		public static void DrawMeowcartTrail(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.mount.Type != 33)
			{
				return;
			}
			if (drawinfo.shadow > 0f)
			{
				return;
			}
			int num = Math.Min(drawinfo.drawPlayer.availableAdvancedShadowsCount - 1, 20);
			float num2 = 0f;
			for (int i = num; i > 0; i--)
			{
				EntityShadowInfo advancedShadow = drawinfo.drawPlayer.GetAdvancedShadow(i);
				EntityShadowInfo advancedShadow2 = drawinfo.drawPlayer.GetAdvancedShadow(i - 1);
				num2 += Vector2.Distance(advancedShadow.Position, advancedShadow2.Position);
			}
			float scale = MathHelper.Clamp(num2 / 160f, 0f, 1f);
			Main.instance.LoadProjectile(250);
			Texture2D value = TextureAssets.Projectile[250].Value;
			float x = 1.5f;
			Vector2 origin = new Vector2((float)(value.Width / 2), 0f);
			Vector2 value2 = new Vector2((float)drawinfo.drawPlayer.width, (float)drawinfo.drawPlayer.height) / 2f;
			Vector2 value3 = new Vector2((float)(-(float)drawinfo.drawPlayer.direction * 10), 15f);
			Color white = Color.White;
			white.A = 127;
			Vector2 value4 = value2 + value3;
			value4 = Vector2.Zero;
			Vector2 value5 = drawinfo.drawPlayer.RotatedRelativePoint(drawinfo.drawPlayer.Center + value4 + value3, false, true) - drawinfo.drawPlayer.position;
			for (int j = num; j > 0; j--)
			{
				EntityShadowInfo advancedShadow3 = drawinfo.drawPlayer.GetAdvancedShadow(j);
				ref EntityShadowInfo advancedShadow4 = drawinfo.drawPlayer.GetAdvancedShadow(j - 1);
				Vector2 vector = advancedShadow3.Position + value4;
				Vector2 vector2 = advancedShadow4.Position + value4;
				vector += value5;
				vector2 += value5;
				vector = drawinfo.drawPlayer.RotatedRelativePoint(vector, true, false);
				vector2 = drawinfo.drawPlayer.RotatedRelativePoint(vector2, true, false);
				float rotation = (vector2 - vector).ToRotation() - 1.5707964f;
				float num3 = Vector2.Distance(vector, vector2);
				Vector2 scale2 = new Vector2(x, num3 / (float)value.Height);
				float num4 = 1f - (float)j / (float)num;
				num4 *= num4;
				Color color = white * num4 * scale;
				DrawData item = new DrawData(value, vector - Main.screenPosition, null, color, rotation, origin, scale2, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x005B32C0 File Offset: 0x005B14C0
		public static void DrawPlayer_23_MountFront(ref PlayerDrawSet drawinfo)
		{
			if (!drawinfo.drawPlayer.mount.Active)
			{
				return;
			}
			drawinfo.drawPlayer.mount.Draw(drawinfo.DrawDataCache, 2, drawinfo.drawPlayer, drawinfo.Position, drawinfo.colorMount, drawinfo.playerEffect, drawinfo.shadow);
			drawinfo.drawPlayer.mount.Draw(drawinfo.DrawDataCache, 3, drawinfo.drawPlayer, drawinfo.Position, drawinfo.colorMount, drawinfo.playerEffect, drawinfo.shadow);
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x005B334C File Offset: 0x005B154C
		public static void DrawPlayer_24_Pulley(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.pulley && drawinfo.drawPlayer.itemAnimation == 0)
			{
				DrawData item;
				if (drawinfo.drawPlayer.pulleyDir == 2)
				{
					int num = -25;
					int num2 = 0;
					float rotation = 0f;
					item = new DrawData(TextureAssets.Pulley.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2) - (float)(9 * drawinfo.drawPlayer.direction)) + num2 * drawinfo.drawPlayer.direction), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)(drawinfo.drawPlayer.height / 2) + 2f * drawinfo.drawPlayer.gravDir + (float)num * drawinfo.drawPlayer.gravDir))), new Rectangle?(new Rectangle(0, TextureAssets.Pulley.Height() / 2 * drawinfo.drawPlayer.pulleyFrame, TextureAssets.Pulley.Width(), TextureAssets.Pulley.Height() / 2)), drawinfo.colorArmorHead, rotation, new Vector2((float)(TextureAssets.Pulley.Width() / 2), (float)(TextureAssets.Pulley.Height() / 4)), 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
					return;
				}
				int num3 = -26;
				int num4 = 10;
				float rotation2 = 0.35f * (float)(-(float)drawinfo.drawPlayer.direction);
				item = new DrawData(TextureAssets.Pulley.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2) - (float)(9 * drawinfo.drawPlayer.direction)) + num4 * drawinfo.drawPlayer.direction), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)(drawinfo.drawPlayer.height / 2) + 2f * drawinfo.drawPlayer.gravDir + (float)num3 * drawinfo.drawPlayer.gravDir))), new Rectangle?(new Rectangle(0, TextureAssets.Pulley.Height() / 2 * drawinfo.drawPlayer.pulleyFrame, TextureAssets.Pulley.Width(), TextureAssets.Pulley.Height() / 2)), drawinfo.colorArmorHead, rotation2, new Vector2((float)(TextureAssets.Pulley.Width() / 2), (float)(TextureAssets.Pulley.Height() / 4)), 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x005B35F4 File Offset: 0x005B17F4
		public static void DrawPlayer_25_Shield(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.shield > 0 && (int)drawinfo.drawPlayer.shield < ArmorIDs.Shield.Count)
			{
				Vector2 zero = Vector2.Zero;
				if (drawinfo.drawPlayer.shieldRaised)
				{
					zero.Y -= 4f * drawinfo.drawPlayer.gravDir;
				}
				Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
				Vector2 zero2 = Vector2.Zero;
				Vector2 bodyVect = drawinfo.bodyVect;
				if (bodyFrame.Width != TextureAssets.AccShield[(int)drawinfo.drawPlayer.shield].Value.Width)
				{
					bodyFrame.Width = TextureAssets.AccShield[(int)drawinfo.drawPlayer.shield].Value.Width;
					bodyVect.X += (float)(bodyFrame.Width - TextureAssets.AccShield[(int)drawinfo.drawPlayer.shield].Value.Width);
					if (drawinfo.playerEffect.HasFlag(SpriteEffects.FlipHorizontally))
					{
						bodyVect.X = (float)bodyFrame.Width - bodyVect.X;
					}
				}
				DrawData item;
				if (drawinfo.drawPlayer.shieldRaised)
				{
					float num = (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f));
					float x = 2.5f + 1.5f * num;
					Color color = drawinfo.colorArmorBody;
					color.A = 0;
					color *= 0.45f - num * 0.15f;
					for (float num2 = 0f; num2 < 4f; num2 += 1f)
					{
						item = new DrawData(TextureAssets.AccShield[(int)drawinfo.drawPlayer.shield].Value, zero2 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)) + zero + new Vector2(x, 0f).RotatedBy((double)(num2 / 4f * 6.2831855f), default(Vector2)), new Rectangle?(bodyFrame), color, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cShield;
						drawinfo.DrawDataCache.Add(item);
					}
				}
				item = new DrawData(TextureAssets.AccShield[(int)drawinfo.drawPlayer.shield].Value, zero2 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)) + zero, new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cShield;
				drawinfo.DrawDataCache.Add(item);
				if (drawinfo.drawPlayer.shieldRaised)
				{
					Color color2 = drawinfo.colorArmorBody;
					float num3 = (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 3.1415927f));
					color2.A = (byte)((float)color2.A * (0.5f + 0.5f * num3));
					color2 *= 0.5f + 0.5f * num3;
					item = new DrawData(TextureAssets.AccShield[(int)drawinfo.drawPlayer.shield].Value, zero2 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)) + zero, new Rectangle?(bodyFrame), color2, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cShield;
				}
				if (drawinfo.drawPlayer.shieldRaised && drawinfo.drawPlayer.shieldParryTimeLeft > 0)
				{
					float num4 = (float)drawinfo.drawPlayer.shieldParryTimeLeft / 20f;
					float num5 = 1.5f * num4;
					Vector2 vector = zero2 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)) + zero;
					Color color3 = drawinfo.colorArmorBody;
					float num6 = 1f;
					Vector2 value = drawinfo.Position + drawinfo.drawPlayer.Size / 2f - Main.screenPosition;
					Vector2 value2 = vector - value;
					vector += value2 * num5;
					num6 += num5;
					color3.A = (byte)((float)color3.A * (1f - num4));
					color3 *= 1f - num4;
					item = new DrawData(TextureAssets.AccShield[(int)drawinfo.drawPlayer.shield].Value, vector, new Rectangle?(bodyFrame), color3, drawinfo.drawPlayer.bodyRotation, bodyVect, num6, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cShield;
					drawinfo.DrawDataCache.Add(item);
				}
				if (drawinfo.drawPlayer.mount.Cart)
				{
					drawinfo.DrawDataCache.Reverse(drawinfo.DrawDataCache.Count - 2, 2);
				}
			}
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x005B3D4C File Offset: 0x005B1F4C
		public static void DrawPlayer_26_SolarShield(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.solarShields > 0 && drawinfo.shadow == 0f && !drawinfo.drawPlayer.dead)
			{
				Texture2D value = TextureAssets.Extra[61 + drawinfo.drawPlayer.solarShields - 1].Value;
				Color color = new Color(255, 255, 255, 127);
				float num = (drawinfo.drawPlayer.solarShieldPos[0] * new Vector2(1f, 0.5f)).ToRotation();
				if (drawinfo.drawPlayer.direction == -1)
				{
					num += 3.1415927f;
				}
				num += 0.06283186f * (float)drawinfo.drawPlayer.direction;
				DrawData item = new DrawData(value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)(drawinfo.drawPlayer.height / 2)))) + drawinfo.drawPlayer.solarShieldPos[0], null, color, num, value.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cBody;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x005B3ECC File Offset: 0x005B20CC
		public static void DrawPlayer_27_HeldItem(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.JustDroppedAnItem)
			{
				return;
			}
			if (drawinfo.drawPlayer.heldProj >= 0 && drawinfo.shadow == 0f && !drawinfo.heldProjOverHand)
			{
				drawinfo.projectileDrawPosition = drawinfo.DrawDataCache.Count;
			}
			Item heldItem = drawinfo.heldItem;
			int num = heldItem.type;
			if (drawinfo.drawPlayer.UsingBiomeTorches)
			{
				if (num != 8)
				{
					if (num == 966)
					{
						num = drawinfo.drawPlayer.BiomeCampfireHoldStyle(num);
					}
				}
				else
				{
					num = drawinfo.drawPlayer.BiomeTorchHoldStyle(num);
				}
			}
			float adjustedItemScale = drawinfo.drawPlayer.GetAdjustedItemScale(heldItem);
			Main.instance.LoadItem(num);
			Texture2D value = TextureAssets.Item[num].Value;
			Vector2 vector = new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y)));
			Rectangle itemDrawFrame = drawinfo.drawPlayer.GetItemDrawFrame(num);
			drawinfo.itemColor = Lighting.GetColor((int)((double)drawinfo.Position.X + (double)drawinfo.drawPlayer.width * 0.5) / 16, (int)(((double)drawinfo.Position.Y + (double)drawinfo.drawPlayer.height * 0.5) / 16.0));
			if (num == 678)
			{
				drawinfo.itemColor = Color.White;
			}
			if (drawinfo.drawPlayer.shroomiteStealth && heldItem.ranged)
			{
				float num2 = drawinfo.drawPlayer.stealth;
				if ((double)num2 < 0.03)
				{
					num2 = 0.03f;
				}
				float num3 = (1f + num2 * 10f) / 11f;
				drawinfo.itemColor = new Color((int)((byte)((float)drawinfo.itemColor.R * num2)), (int)((byte)((float)drawinfo.itemColor.G * num2)), (int)((byte)((float)drawinfo.itemColor.B * num3)), (int)((byte)((float)drawinfo.itemColor.A * num2)));
			}
			if (drawinfo.drawPlayer.setVortex && heldItem.ranged)
			{
				float num4 = drawinfo.drawPlayer.stealth;
				if ((double)num4 < 0.03)
				{
					num4 = 0.03f;
				}
				float num5 = (1f + num4 * 10f) / 11f;
				drawinfo.itemColor = drawinfo.itemColor.MultiplyRGBA(new Color(Vector4.Lerp(Vector4.One, new Vector4(0f, 0.12f, 0.16f, 0f), 1f - num4)));
			}
			bool flag = drawinfo.drawPlayer.itemAnimation > 0 && heldItem.useStyle != 0;
			bool flag2 = heldItem.holdStyle != 0 && !drawinfo.drawPlayer.pulley;
			if (!drawinfo.drawPlayer.CanVisuallyHoldItem(heldItem))
			{
				flag2 = false;
			}
			if (drawinfo.shadow == 0f && !drawinfo.drawPlayer.frozen && (flag || flag2) && num > 0 && !drawinfo.drawPlayer.dead && !heldItem.noUseGraphic && (!drawinfo.drawPlayer.wet || !heldItem.noWet) && (!drawinfo.drawPlayer.happyFunTorchTime || drawinfo.drawPlayer.inventory[drawinfo.drawPlayer.selectedItem].createTile != 4 || drawinfo.drawPlayer.itemAnimation != 0))
			{
				string name = drawinfo.drawPlayer.name;
				Color color = new Color(250, 250, 250, heldItem.alpha);
				Vector2 value2 = Vector2.Zero;
				if (num <= 426)
				{
					if (num <= 104)
					{
						if (num == 46)
						{
							float amount = Utils.Remap(drawinfo.itemColor.ToVector3().Length() / 1.731f, 0.3f, 0.5f, 1f, 0f, true);
							color = Color.Lerp(Color.Transparent, new Color(255, 255, 255, 127) * 0.7f, amount);
							goto IL_518;
						}
						if (num != 104)
						{
							goto IL_518;
						}
					}
					else
					{
						if (num == 204)
						{
							value2 = new Vector2(4f, -6f) * drawinfo.drawPlayer.Directions;
							goto IL_518;
						}
						if (num != 426)
						{
							goto IL_518;
						}
						goto IL_448;
					}
				}
				else if (num <= 1506)
				{
					if (num != 797 && num != 1506)
					{
						goto IL_518;
					}
					goto IL_448;
				}
				else
				{
					if (num == 3349)
					{
						value2 = new Vector2(2f, -2f) * drawinfo.drawPlayer.Directions;
						goto IL_518;
					}
					if (num - 5094 > 1)
					{
						if (num - 5096 > 1)
						{
							goto IL_518;
						}
						goto IL_448;
					}
				}
				value2 = new Vector2(4f, -4f) * drawinfo.drawPlayer.Directions;
				goto IL_518;
				IL_448:
				value2 = new Vector2(6f, -6f) * drawinfo.drawPlayer.Directions;
				IL_518:
				if (num == 3823)
				{
					value2 = new Vector2((float)(7 * drawinfo.drawPlayer.direction), -7f * drawinfo.drawPlayer.gravDir);
				}
				if (num == 3827)
				{
					value2 = new Vector2((float)(13 * drawinfo.drawPlayer.direction), -13f * drawinfo.drawPlayer.gravDir);
					color = heldItem.GetAlpha(drawinfo.itemColor);
					color = Color.Lerp(color, Color.White, 0.6f);
					color.A = 66;
				}
				Vector2 vector2 = new Vector2((float)itemDrawFrame.Width * 0.5f - (float)itemDrawFrame.Width * 0.5f * (float)drawinfo.drawPlayer.direction, (float)itemDrawFrame.Height);
				if (heldItem.useStyle == 9 && drawinfo.drawPlayer.itemAnimation > 0)
				{
					Vector2 vector3 = new Vector2(0.5f, 0.4f);
					if (heldItem.type == 5009 || heldItem.type == 5042)
					{
						vector3 = new Vector2(0.26f, 0.5f);
						if (drawinfo.drawPlayer.direction == -1)
						{
							vector3.X = 1f - vector3.X;
						}
					}
					vector2 = itemDrawFrame.Size() * vector3;
				}
				if (drawinfo.drawPlayer.gravDir == -1f)
				{
					vector2.Y = (float)itemDrawFrame.Height - vector2.Y;
				}
				vector2 += value2;
				float num6 = drawinfo.drawPlayer.itemRotation;
				if (heldItem.useStyle == 8)
				{
					float x = vector.X;
					int direction = drawinfo.drawPlayer.direction;
					vector.X = x - (float)0;
					num6 -= 1.5707964f * (float)drawinfo.drawPlayer.direction;
					vector2.Y = 2f;
					vector2.X += (float)(2 * drawinfo.drawPlayer.direction);
				}
				if (num == 425 || num == 507)
				{
					if (drawinfo.drawPlayer.gravDir == 1f)
					{
						if (drawinfo.drawPlayer.direction == 1)
						{
							drawinfo.itemEffect = SpriteEffects.FlipVertically;
						}
						else
						{
							drawinfo.itemEffect = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
						}
					}
					else if (drawinfo.drawPlayer.direction == 1)
					{
						drawinfo.itemEffect = SpriteEffects.None;
					}
					else
					{
						drawinfo.itemEffect = SpriteEffects.FlipHorizontally;
					}
				}
				if ((num == 946 || num == 4707) && num6 != 0f)
				{
					vector.Y -= 22f * drawinfo.drawPlayer.gravDir;
					num6 = -1.57f * (float)(-(float)drawinfo.drawPlayer.direction) * drawinfo.drawPlayer.gravDir;
				}
				ItemSlot.GetItemLight(ref drawinfo.itemColor, heldItem, false);
				if (num == 3476)
				{
					Texture2D value3 = TextureAssets.Extra[64].Value;
					Rectangle rectangle = value3.Frame(1, 9, 0, drawinfo.drawPlayer.miscCounter % 54 / 6, 0, 0);
					Vector2 value4 = new Vector2((float)(rectangle.Width / 2 * drawinfo.drawPlayer.direction), 0f);
					Vector2 origin = rectangle.Size() / 2f;
					DrawData item = new DrawData(value3, (drawinfo.ItemLocation - Main.screenPosition + value4).Floor(), new Rectangle?(rectangle), heldItem.GetAlpha(drawinfo.itemColor).MultiplyRGBA(new Color(new Vector4(0.5f, 0.5f, 0.5f, 0.8f))), drawinfo.drawPlayer.itemRotation, origin, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
					value3 = TextureAssets.GlowMask[195].Value;
					item = new DrawData(value3, (drawinfo.ItemLocation - Main.screenPosition + value4).Floor(), new Rectangle?(rectangle), new Color(250, 250, 250, heldItem.alpha) * 0.5f, drawinfo.drawPlayer.itemRotation, origin, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
					return;
				}
				if (num == 4049)
				{
					Texture2D value5 = TextureAssets.Extra[92].Value;
					Rectangle rectangle2 = value5.Frame(1, 4, 0, drawinfo.drawPlayer.miscCounter % 20 / 5, 0, 0);
					Vector2 vector4 = new Vector2((float)(rectangle2.Width / 2 * drawinfo.drawPlayer.direction), 0f);
					vector4 += new Vector2((float)(-10 * drawinfo.drawPlayer.direction), 8f * drawinfo.drawPlayer.gravDir);
					Vector2 origin2 = rectangle2.Size() / 2f;
					DrawData item = new DrawData(value5, (drawinfo.ItemLocation - Main.screenPosition + vector4).Floor(), new Rectangle?(rectangle2), heldItem.GetAlpha(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, origin2, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
					return;
				}
				if (num == 3779)
				{
					Texture2D texture2D = value;
					Rectangle rectangle3 = texture2D.Frame(1, 1, 0, 0, 0, 0);
					Vector2 value6 = new Vector2((float)(rectangle3.Width / 2 * drawinfo.drawPlayer.direction), 0f);
					Vector2 origin3 = rectangle3.Size() / 2f;
					float num7 = ((float)drawinfo.drawPlayer.miscCounter / 75f * 6.2831855f).ToRotationVector2().X * 1f + 0f;
					Color color2 = new Color(120, 40, 222, 0) * (num7 / 2f * 0.3f + 0.85f) * 0.5f;
					num7 = 2f;
					DrawData item;
					for (float num8 = 0f; num8 < 4f; num8 += 1f)
					{
						item = new DrawData(TextureAssets.GlowMask[218].Value, (drawinfo.ItemLocation - Main.screenPosition + value6).Floor() + (num8 * 1.5707964f).ToRotationVector2() * num7, new Rectangle?(rectangle3), color2, drawinfo.drawPlayer.itemRotation, origin3, adjustedItemScale, drawinfo.itemEffect, 0f);
						drawinfo.DrawDataCache.Add(item);
					}
					item = new DrawData(texture2D, (drawinfo.ItemLocation - Main.screenPosition + value6).Floor(), new Rectangle?(rectangle3), heldItem.GetAlpha(drawinfo.itemColor).MultiplyRGBA(new Color(new Vector4(0.5f, 0.5f, 0.5f, 0.8f))), drawinfo.drawPlayer.itemRotation, origin3, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
					return;
				}
				if (heldItem.useStyle == 5)
				{
					if (Item.staff[num])
					{
						float num9 = drawinfo.drawPlayer.itemRotation + 0.785f * (float)drawinfo.drawPlayer.direction;
						float num10 = 0f;
						float num11 = 0f;
						Vector2 zero = new Vector2(0f, (float)itemDrawFrame.Height);
						if (num == 3210)
						{
							num10 = (float)(8 * -(float)drawinfo.drawPlayer.direction);
							num11 = (float)(2 * (int)drawinfo.drawPlayer.gravDir);
						}
						if (num == 3870)
						{
							Vector2 vector5 = (drawinfo.drawPlayer.itemRotation + 0.7853982f * (float)drawinfo.drawPlayer.direction).ToRotationVector2() * new Vector2((float)(-(float)drawinfo.drawPlayer.direction) * 1.5f, drawinfo.drawPlayer.gravDir) * 3f;
							num10 = (float)((int)vector5.X);
							num11 = (float)((int)vector5.Y);
						}
						if (num == 3787)
						{
							num11 = (float)((int)((float)(8 * (int)drawinfo.drawPlayer.gravDir) * (float)Math.Cos((double)num9)));
						}
						if (num == 3209)
						{
							Vector2 vector6 = (new Vector2(-8f, 0f) * drawinfo.drawPlayer.Directions).RotatedBy((double)drawinfo.drawPlayer.itemRotation, default(Vector2));
							num10 = vector6.X;
							num11 = vector6.Y;
						}
						if (drawinfo.drawPlayer.gravDir == -1f)
						{
							if (drawinfo.drawPlayer.direction == -1)
							{
								num9 += 1.57f;
								zero = new Vector2((float)itemDrawFrame.Width, 0f);
								num10 -= (float)itemDrawFrame.Width;
							}
							else
							{
								num9 -= 1.57f;
								zero = Vector2.Zero;
							}
						}
						else if (drawinfo.drawPlayer.direction == -1)
						{
							zero = new Vector2((float)itemDrawFrame.Width, (float)itemDrawFrame.Height);
							num10 -= (float)itemDrawFrame.Width;
						}
						DrawData item = new DrawData(value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + zero.X + num10)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + num11))), new Rectangle?(itemDrawFrame), heldItem.GetAlpha(drawinfo.itemColor), num9, zero, adjustedItemScale, drawinfo.itemEffect, 0f);
						drawinfo.DrawDataCache.Add(item);
						if (num == 3870)
						{
							item = new DrawData(TextureAssets.GlowMask[238].Value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + zero.X + num10)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + num11))), new Rectangle?(itemDrawFrame), new Color(255, 255, 255, 127), num9, zero, adjustedItemScale, drawinfo.itemEffect, 0f);
							drawinfo.DrawDataCache.Add(item);
							return;
						}
					}
					else
					{
						DrawData item;
						if (num == 5118)
						{
							float rotation = drawinfo.drawPlayer.itemRotation + 1.57f * (float)drawinfo.drawPlayer.direction;
							Vector2 vector7 = new Vector2((float)itemDrawFrame.Width * 0.5f, (float)itemDrawFrame.Height * 0.5f);
							Vector2 origin4 = new Vector2((float)itemDrawFrame.Width * 0.5f, (float)itemDrawFrame.Height);
							Vector2 vector8 = new Vector2(10f, 4f) * drawinfo.drawPlayer.Directions;
							vector8 = vector8.RotatedBy((double)drawinfo.drawPlayer.itemRotation, default(Vector2));
							item = new DrawData(value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + vector7.X + vector8.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + vector7.Y + vector8.Y))), new Rectangle?(itemDrawFrame), heldItem.GetAlpha(drawinfo.itemColor), rotation, origin4, adjustedItemScale, drawinfo.itemEffect, 0f);
							drawinfo.DrawDataCache.Add(item);
							return;
						}
						Vector2 vector9 = new Vector2((float)(itemDrawFrame.Width / 2), (float)(itemDrawFrame.Height / 2));
						Vector2 vector10 = Main.DrawPlayerItemPos(drawinfo.drawPlayer.gravDir, num);
						int num12 = (int)vector10.X;
						vector9.Y = vector10.Y;
						Vector2 origin5 = new Vector2((float)(-(float)num12), (float)(itemDrawFrame.Height / 2));
						if (drawinfo.drawPlayer.direction == -1)
						{
							origin5 = new Vector2((float)(itemDrawFrame.Width + num12), (float)(itemDrawFrame.Height / 2));
						}
						item = new DrawData(value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + vector9.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + vector9.Y))), new Rectangle?(itemDrawFrame), heldItem.GetAlpha(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, origin5, adjustedItemScale, drawinfo.itemEffect, 0f);
						drawinfo.DrawDataCache.Add(item);
						if (heldItem.color != default(Color))
						{
							item = new DrawData(value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + vector9.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + vector9.Y))), new Rectangle?(itemDrawFrame), heldItem.GetColor(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, origin5, adjustedItemScale, drawinfo.itemEffect, 0f);
							drawinfo.DrawDataCache.Add(item);
						}
						if (heldItem.glowMask != -1)
						{
							item = new DrawData(TextureAssets.GlowMask[(int)heldItem.glowMask].Value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + vector9.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + vector9.Y))), new Rectangle?(itemDrawFrame), new Color(250, 250, 250, heldItem.alpha), drawinfo.drawPlayer.itemRotation, origin5, adjustedItemScale, drawinfo.itemEffect, 0f);
							drawinfo.DrawDataCache.Add(item);
						}
						if (num == 3788)
						{
							float num13 = ((float)drawinfo.drawPlayer.miscCounter / 75f * 6.2831855f).ToRotationVector2().X * 1f + 0f;
							Color color3 = new Color(80, 40, 252, 0) * (num13 / 2f * 0.3f + 0.85f) * 0.5f;
							for (float num14 = 0f; num14 < 4f; num14 += 1f)
							{
								item = new DrawData(TextureAssets.GlowMask[220].Value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + vector9.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + vector9.Y))) + (num14 * 1.5707964f + drawinfo.drawPlayer.itemRotation).ToRotationVector2() * num13, null, color3, drawinfo.drawPlayer.itemRotation, origin5, adjustedItemScale, drawinfo.itemEffect, 0f);
								drawinfo.DrawDataCache.Add(item);
							}
							return;
						}
					}
				}
				else if (drawinfo.drawPlayer.gravDir == -1f)
				{
					DrawData item = new DrawData(value, vector, new Rectangle?(itemDrawFrame), heldItem.GetAlpha(drawinfo.itemColor), num6, vector2, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
					if (heldItem.color != default(Color))
					{
						item = new DrawData(value, vector, new Rectangle?(itemDrawFrame), heldItem.GetColor(drawinfo.itemColor), num6, vector2, adjustedItemScale, drawinfo.itemEffect, 0f);
						drawinfo.DrawDataCache.Add(item);
					}
					if (heldItem.glowMask != -1)
					{
						item = new DrawData(TextureAssets.GlowMask[(int)heldItem.glowMask].Value, vector, new Rectangle?(itemDrawFrame), new Color(250, 250, 250, heldItem.alpha), num6, vector2, adjustedItemScale, drawinfo.itemEffect, 0f);
						drawinfo.DrawDataCache.Add(item);
						return;
					}
				}
				else
				{
					DrawData item = new DrawData(value, vector, new Rectangle?(itemDrawFrame), heldItem.GetAlpha(drawinfo.itemColor), num6, vector2, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
					if (heldItem.color != default(Color))
					{
						item = new DrawData(value, vector, new Rectangle?(itemDrawFrame), heldItem.GetColor(drawinfo.itemColor), num6, vector2, adjustedItemScale, drawinfo.itemEffect, 0f);
						drawinfo.DrawDataCache.Add(item);
					}
					if (heldItem.glowMask != -1)
					{
						item = new DrawData(TextureAssets.GlowMask[(int)heldItem.glowMask].Value, vector, new Rectangle?(itemDrawFrame), color, num6, vector2, adjustedItemScale, drawinfo.itemEffect, 0f);
						drawinfo.DrawDataCache.Add(item);
					}
					if (heldItem.flame && drawinfo.shadow == 0f)
					{
						try
						{
							Main.instance.LoadItemFlames(num);
							if (TextureAssets.ItemFlame[num].IsLoaded)
							{
								Color color4 = new Color(100, 100, 100, 0);
								int num15 = 7;
								float num16 = 1f;
								float num17 = 0f;
								if (num <= 4952)
								{
									if (num != 3045)
									{
										if (num == 4952)
										{
											num15 = 3;
											num16 = 0.6f;
											color4 = new Color(50, 50, 50, 0);
										}
									}
									else
									{
										color4 = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
									}
								}
								else if (num != 5293)
								{
									if (num != 5322)
									{
										if (num == 5353)
										{
											color4 = new Color(255, 255, 255, 200);
										}
									}
									else
									{
										color4 = new Color(100, 100, 100, 150);
										num17 = (float)(-2 * drawinfo.drawPlayer.direction);
									}
								}
								else
								{
									color4 = new Color(50, 50, 100, 20);
								}
								for (int i = 0; i < num15; i++)
								{
									float num18 = drawinfo.drawPlayer.itemFlamePos[i].X * adjustedItemScale * num16;
									float num19 = drawinfo.drawPlayer.itemFlamePos[i].Y * adjustedItemScale * num16;
									item = new DrawData(TextureAssets.ItemFlame[num].Value, new Vector2((float)((int)(vector.X + num18 + num17)), (float)((int)(vector.Y + num19))), new Rectangle?(itemDrawFrame), color4, num6, vector2, adjustedItemScale, drawinfo.itemEffect, 0f);
									drawinfo.DrawDataCache.Add(item);
								}
							}
						}
						catch
						{
						}
					}
				}
			}
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x005B568C File Offset: 0x005B388C
		public static void DrawPlayer_28_ArmOverItem(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.usesCompositeTorso)
			{
				PlayerDrawLayers.DrawPlayer_28_ArmOverItemComposite(ref drawinfo);
				return;
			}
			if (drawinfo.drawPlayer.body > 0 && drawinfo.drawPlayer.body < ArmorIDs.Body.Count)
			{
				Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
				int num = drawinfo.armorAdjust;
				bodyFrame.X += num;
				bodyFrame.Width -= num;
				if (drawinfo.drawPlayer.direction == -1)
				{
					num = 0;
				}
				if (!drawinfo.drawPlayer.invis || (drawinfo.drawPlayer.body != 21 && drawinfo.drawPlayer.body != 22))
				{
					DrawData item;
					if (drawinfo.missingHand && !drawinfo.drawPlayer.invis)
					{
						int body = drawinfo.drawPlayer.body;
						DrawData drawData;
						if (drawinfo.missingArm)
						{
							drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
							{
								shader = drawinfo.skinDyePacked
							};
							item = drawData;
							drawinfo.DrawDataCache.Add(item);
						}
						drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 9].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
						{
							shader = drawinfo.skinDyePacked
						};
						item = drawData;
						drawinfo.DrawDataCache.Add(item);
					}
					item = new DrawData(TextureAssets.ArmorArm[drawinfo.drawPlayer.body].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2)) + num), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cBody;
					drawinfo.DrawDataCache.Add(item);
					if (drawinfo.armGlowMask != -1)
					{
						item = new DrawData(TextureAssets.GlowMask[drawinfo.armGlowMask].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2)) + num), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(bodyFrame), drawinfo.armGlowColor, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cBody;
						drawinfo.DrawDataCache.Add(item);
					}
					if (drawinfo.drawPlayer.body == 205)
					{
						Color color = new Color(100, 100, 100, 0);
						ulong num2 = (ulong)((long)(drawinfo.drawPlayer.miscCounter / 4));
						int num3 = 4;
						for (int i = 0; i < num3; i++)
						{
							float num4 = (float)Utils.RandomInt(ref num2, -10, 11) * 0.2f;
							float num5 = (float)Utils.RandomInt(ref num2, -10, 1) * 0.15f;
							item = new DrawData(TextureAssets.GlowMask[240].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2)) + num), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + num4, (float)(drawinfo.drawPlayer.bodyFrame.Height / 2) + num5), new Rectangle?(bodyFrame), color, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
							item.shader = drawinfo.cBody;
							drawinfo.DrawDataCache.Add(item);
						}
						return;
					}
				}
			}
			else if (!drawinfo.drawPlayer.invis)
			{
				DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.skinDyePacked
				};
				DrawData item = drawData;
				drawinfo.DrawDataCache.Add(item);
				item = new DrawData(TextureAssets.Players[drawinfo.skinVar, 8].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorUnderShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
				item = new DrawData(TextureAssets.Players[drawinfo.skinVar, 13].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x005B611C File Offset: 0x005B431C
		public static void DrawPlayer_28_ArmOverItemComposite(ref PlayerDrawSet drawinfo)
		{
			Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
			Vector2 value = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
			value.Y -= 2f;
			vector += value * (float)(-(float)drawinfo.playerEffect.HasFlag(SpriteEffects.FlipVertically).ToDirectionInt());
			float bodyRotation = drawinfo.drawPlayer.bodyRotation;
			float rotation = drawinfo.drawPlayer.bodyRotation + drawinfo.compositeFrontArmRotation;
			Vector2 vector2 = drawinfo.bodyVect;
			Vector2 compositeOffset_FrontArm = PlayerDrawLayers.GetCompositeOffset_FrontArm(ref drawinfo);
			vector2 += compositeOffset_FrontArm;
			vector += compositeOffset_FrontArm;
			Vector2 position = vector + drawinfo.frontShoulderOffset;
			if (drawinfo.compFrontArmFrame.X / drawinfo.compFrontArmFrame.Width >= 7)
			{
				vector += new Vector2((float)(drawinfo.playerEffect.HasFlag(SpriteEffects.FlipHorizontally) ? -1 : 1), (float)(drawinfo.playerEffect.HasFlag(SpriteEffects.FlipVertically) ? -1 : 1));
			}
			bool invis = drawinfo.drawPlayer.invis;
			bool flag = drawinfo.drawPlayer.body > 0 && drawinfo.drawPlayer.body < ArmorIDs.Body.Count;
			int num = drawinfo.compShoulderOverFrontArm ? 1 : 0;
			int num2 = drawinfo.compShoulderOverFrontArm ? 0 : 1;
			int num3 = drawinfo.compShoulderOverFrontArm ? 0 : 1;
			bool flag2 = !drawinfo.hidesTopSkin;
			if (flag)
			{
				if (!drawinfo.drawPlayer.invis || PlayerDrawLayers.IsArmorDrawnWhenInvisible(drawinfo.drawPlayer.body))
				{
					Texture2D value2 = TextureAssets.ArmorBodyComposite[drawinfo.drawPlayer.body].Value;
					for (int i = 0; i < 2; i++)
					{
						if (!drawinfo.drawPlayer.invis && i == num3 && flag2)
						{
							if (drawinfo.missingArm)
							{
								List<DrawData> drawDataCache = drawinfo.DrawDataCache;
								DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorBodySkin, rotation, vector2, 1f, drawinfo.playerEffect, 0f)
								{
									shader = drawinfo.skinDyePacked
								};
								drawDataCache.Add(drawData);
							}
							if (drawinfo.missingHand)
							{
								List<DrawData> drawDataCache2 = drawinfo.DrawDataCache;
								DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 9].Value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorBodySkin, rotation, vector2, 1f, drawinfo.playerEffect, 0f)
								{
									shader = drawinfo.skinDyePacked
								};
								drawDataCache2.Add(drawData);
							}
						}
						if (i == num && !drawinfo.hideCompositeShoulders)
						{
							CompositePlayerDrawContext context = CompositePlayerDrawContext.FrontShoulder;
							DrawData drawData = new DrawData(value2, position, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorArmorBody, bodyRotation, vector2, 1f, drawinfo.playerEffect, 0f)
							{
								shader = drawinfo.cBody
							};
							PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, context, drawData);
						}
						if (i == num2)
						{
							CompositePlayerDrawContext context2 = CompositePlayerDrawContext.FrontArm;
							DrawData drawData = new DrawData(value2, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorArmorBody, rotation, vector2, 1f, drawinfo.playerEffect, 0f)
							{
								shader = drawinfo.cBody
							};
							PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, context2, drawData);
						}
					}
				}
			}
			else if (!drawinfo.drawPlayer.invis)
			{
				for (int j = 0; j < 2; j++)
				{
					if (j == num)
					{
						if (flag2)
						{
							List<DrawData> drawDataCache3 = drawinfo.DrawDataCache;
							DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, position, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorBodySkin, bodyRotation, vector2, 1f, drawinfo.playerEffect, 0f)
							{
								shader = drawinfo.skinDyePacked
							};
							drawDataCache3.Add(drawData);
						}
						drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 8].Value, position, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorUnderShirt, bodyRotation, vector2, 1f, drawinfo.playerEffect, 0f));
						drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 13].Value, position, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorShirt, bodyRotation, vector2, 1f, drawinfo.playerEffect, 0f));
						drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, position, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorShirt, bodyRotation, vector2, 1f, drawinfo.playerEffect, 0f));
						if (drawinfo.drawPlayer.head == 269)
						{
							Vector2 position2 = drawinfo.helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect;
							DrawData item = new DrawData(TextureAssets.Extra[214].Value, position2, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
							item.shader = drawinfo.cHead;
							drawinfo.DrawDataCache.Add(item);
							item = new DrawData(TextureAssets.GlowMask[308].Value, position2, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
							item.shader = drawinfo.cHead;
							drawinfo.DrawDataCache.Add(item);
						}
					}
					if (j == num2)
					{
						if (flag2)
						{
							List<DrawData> drawDataCache4 = drawinfo.DrawDataCache;
							DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorBodySkin, rotation, vector2, 1f, drawinfo.playerEffect, 0f)
							{
								shader = drawinfo.skinDyePacked
							};
							drawDataCache4.Add(drawData);
						}
						drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 8].Value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorUnderShirt, rotation, vector2, 1f, drawinfo.playerEffect, 0f));
						drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 13].Value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorShirt, rotation, vector2, 1f, drawinfo.playerEffect, 0f));
						drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorShirt, rotation, vector2, 1f, drawinfo.playerEffect, 0f));
					}
				}
			}
			if (drawinfo.drawPlayer.handon > 0 && (int)drawinfo.drawPlayer.handon < ArmorIDs.HandOn.Count)
			{
				Texture2D value3 = TextureAssets.AccHandsOnComposite[(int)drawinfo.drawPlayer.handon].Value;
				CompositePlayerDrawContext context3 = CompositePlayerDrawContext.FrontArmAccessory;
				DrawData drawData = new DrawData(value3, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorArmorBody, rotation, vector2, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.cHandOn
				};
				PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, context3, drawData);
			}
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x005B6A0C File Offset: 0x005B4C0C
		public static void DrawPlayer_29_OnhandAcc(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.usesCompositeFrontHandAcc)
			{
				return;
			}
			if (drawinfo.drawPlayer.handon > 0 && (int)drawinfo.drawPlayer.handon < ArmorIDs.HandOn.Count)
			{
				DrawData item = new DrawData(TextureAssets.AccHandsOn[(int)drawinfo.drawPlayer.handon].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cHandOn;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x005B6B74 File Offset: 0x005B4D74
		public static void DrawPlayer_30_BladedGlove(ref PlayerDrawSet drawinfo)
		{
			Item heldItem = drawinfo.heldItem;
			if (heldItem.type > -1 && Item.claw[heldItem.type] && drawinfo.shadow == 0f)
			{
				Main.instance.LoadItem(heldItem.type);
				Asset<Texture2D> asset = TextureAssets.Item[heldItem.type];
				if (!drawinfo.drawPlayer.frozen && (drawinfo.drawPlayer.itemAnimation > 0 || (heldItem.holdStyle != 0 && !drawinfo.drawPlayer.pulley)) && heldItem.type > 0 && !drawinfo.drawPlayer.dead && !heldItem.noUseGraphic && (!drawinfo.drawPlayer.wet || !heldItem.noWet))
				{
					DrawData item;
					if (drawinfo.drawPlayer.gravDir == -1f)
					{
						item = new DrawData(asset.Value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, asset.Width(), asset.Height())), heldItem.GetAlpha(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, new Vector2((float)asset.Width() * 0.5f - (float)asset.Width() * 0.5f * (float)drawinfo.drawPlayer.direction, 0f), drawinfo.drawPlayer.GetAdjustedItemScale(heldItem), drawinfo.itemEffect, 0f);
						drawinfo.DrawDataCache.Add(item);
						return;
					}
					item = new DrawData(asset.Value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, asset.Width(), asset.Height())), heldItem.GetAlpha(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, new Vector2((float)asset.Width() * 0.5f - (float)asset.Width() * 0.5f * (float)drawinfo.drawPlayer.direction, (float)asset.Height()), drawinfo.drawPlayer.GetAdjustedItemScale(heldItem), drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
				}
			}
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x005B6DEE File Offset: 0x005B4FEE
		public static void DrawPlayer_31_ProjectileOverArm(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.heldProj >= 0 && drawinfo.shadow == 0f && drawinfo.heldProjOverHand)
			{
				drawinfo.projectileDrawPosition = drawinfo.DrawDataCache.Count;
			}
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x005B6E24 File Offset: 0x005B5024
		public static void DrawPlayer_32_FrontAcc(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.front > 0 && (int)drawinfo.drawPlayer.front < ArmorIDs.Front.Count && !drawinfo.drawPlayer.mount.Active)
			{
				Vector2 zero = Vector2.Zero;
				DrawData item = new DrawData(TextureAssets.AccFront[(int)drawinfo.drawPlayer.front].Value, zero + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cFront;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x005B6FA4 File Offset: 0x005B51A4
		public static void DrawPlayer_32_FrontAcc_FrontPart(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.front > 0 && (int)drawinfo.drawPlayer.front < ArmorIDs.Front.Count)
			{
				Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
				int num = bodyFrame.Width / 2;
				bodyFrame.Width -= num;
				Vector2 bodyVect = drawinfo.bodyVect;
				if (drawinfo.playerEffect.HasFlag(SpriteEffects.FlipHorizontally))
				{
					bodyVect.X -= (float)num;
				}
				Vector2 vector = Vector2.Zero + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
				DrawData item = new DrawData(TextureAssets.AccFront[(int)drawinfo.drawPlayer.front].Value, vector, new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cFront;
				drawinfo.DrawDataCache.Add(item);
				if (drawinfo.drawPlayer.front == 12)
				{
					Rectangle rectangle = bodyFrame;
					Rectangle value = rectangle;
					value.Width = 2;
					int num2 = 0;
					int num3 = rectangle.Width / 2;
					int num4 = 2;
					if (drawinfo.playerEffect.HasFlag(SpriteEffects.FlipHorizontally))
					{
						num2 = rectangle.Width - 2;
						num4 = -2;
					}
					for (int i = 0; i < num3; i++)
					{
						value.X = rectangle.X + 2 * i;
						Color color = drawinfo.drawPlayer.GetImmuneAlpha(LiquidRenderer.GetShimmerGlitterColor(true, (float)i / 16f, 0f), drawinfo.shadow);
						color *= (float)drawinfo.colorArmorBody.A / 255f;
						item = new DrawData(TextureAssets.GlowMask[331].Value, vector + new Vector2((float)(num2 + i * num4), 0f), new Rectangle?(value), color, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cFront;
						drawinfo.DrawDataCache.Add(item);
					}
				}
			}
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x005B7284 File Offset: 0x005B5484
		public static void DrawPlayer_32_FrontAcc_BackPart(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.front > 0 && (int)drawinfo.drawPlayer.front < ArmorIDs.Front.Count)
			{
				Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
				int num = bodyFrame.Width / 2;
				bodyFrame.Width -= num;
				bodyFrame.X += num;
				Vector2 bodyVect = drawinfo.bodyVect;
				if (!drawinfo.playerEffect.HasFlag(SpriteEffects.FlipHorizontally))
				{
					bodyVect.X -= (float)num;
				}
				Vector2 vector = Vector2.Zero + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
				DrawData item = new DrawData(TextureAssets.AccFront[(int)drawinfo.drawPlayer.front].Value, vector, new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cFront;
				drawinfo.DrawDataCache.Add(item);
				if (drawinfo.drawPlayer.front == 12)
				{
					Rectangle rectangle = bodyFrame;
					Rectangle value = rectangle;
					value.Width = 2;
					int num2 = 0;
					int num3 = rectangle.Width / 2;
					int num4 = 2;
					if (drawinfo.playerEffect.HasFlag(SpriteEffects.FlipHorizontally))
					{
						num2 = rectangle.Width - 2;
						num4 = -2;
					}
					for (int i = 0; i < num3; i++)
					{
						value.X = rectangle.X + 2 * i;
						Color color = drawinfo.drawPlayer.GetImmuneAlpha(LiquidRenderer.GetShimmerGlitterColor(true, (float)i / 16f, 0f), drawinfo.shadow);
						color *= (float)drawinfo.colorArmorBody.A / 255f;
						item = new DrawData(TextureAssets.GlowMask[331].Value, vector + new Vector2((float)(num2 + i * num4), 0f), new Rectangle?(value), color, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cFront;
						drawinfo.DrawDataCache.Add(item);
					}
				}
			}
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x005B7570 File Offset: 0x005B5770
		public static void DrawPlayer_33_FrozenOrWebbedDebuff(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.shimmering)
			{
				return;
			}
			if (drawinfo.drawPlayer.frozen && drawinfo.shadow == 0f)
			{
				Color colorArmorBody = drawinfo.colorArmorBody;
				colorArmorBody.R = (byte)((double)colorArmorBody.R * 0.55);
				colorArmorBody.G = (byte)((double)colorArmorBody.G * 0.55);
				colorArmorBody.B = (byte)((double)colorArmorBody.B * 0.55);
				colorArmorBody.A = (byte)((double)colorArmorBody.A * 0.55);
				DrawData item = new DrawData(TextureAssets.Frozen.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(new Rectangle(0, 0, TextureAssets.Frozen.Width(), TextureAssets.Frozen.Height())), colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Frozen.Width() / 2), (float)(TextureAssets.Frozen.Height() / 2)), 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
				return;
			}
			if (drawinfo.drawPlayer.webbed && drawinfo.shadow == 0f && drawinfo.drawPlayer.velocity.Y == 0f)
			{
				Color color = drawinfo.colorArmorBody * 0.75f;
				Texture2D value = TextureAssets.Extra[31].Value;
				int num = drawinfo.drawPlayer.height / 2;
				DrawData item = new DrawData(value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f + (float)num))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), null, color, drawinfo.drawPlayer.bodyRotation, value.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x005B78C4 File Offset: 0x005B5AC4
		public static void DrawPlayer_34_ElectrifiedDebuffFront(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.electrified && drawinfo.shadow == 0f)
			{
				Texture2D value = TextureAssets.GlowMask[25].Value;
				int num = drawinfo.drawPlayer.miscCounter / 5;
				for (int i = 0; i < 2; i++)
				{
					num %= 7;
					if (num > 1 && num < 5)
					{
						DrawData item = new DrawData(value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(new Rectangle(0, num * value.Height / 7, value.Width, value.Height / 7)), drawinfo.colorElectricity, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(value.Width / 2), (float)(value.Height / 14)), 1f, drawinfo.playerEffect, 0f);
						drawinfo.DrawDataCache.Add(item);
					}
					num += 3;
				}
			}
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x005B7A64 File Offset: 0x005B5C64
		public static void DrawPlayer_35_IceBarrier(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.iceBarrier && drawinfo.shadow == 0f)
			{
				int num = TextureAssets.IceBarrier.Height() / 12;
				Color white = Color.White;
				DrawData item = new DrawData(TextureAssets.IceBarrier.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(new Rectangle(0, num * (int)drawinfo.drawPlayer.iceBarrierFrame, TextureAssets.IceBarrier.Width(), num)), white, 0f, new Vector2((float)(TextureAssets.Frozen.Width() / 2), (float)(TextureAssets.Frozen.Height() / 2)), 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x005B7BD8 File Offset: 0x005B5DD8
		public static void DrawPlayer_36_CTG(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.shadow == 0f && drawinfo.drawPlayer.ownedLargeGems > 0)
			{
				bool flag = false;
				BitsByte ownedLargeGems = drawinfo.drawPlayer.ownedLargeGems;
				float num = 0f;
				for (int i = 0; i < 7; i++)
				{
					if (ownedLargeGems[i])
					{
						num += 1f;
					}
				}
				float num2 = 1f - num * 0.06f;
				float num3 = (num - 1f) * 4f;
				switch ((int)num)
				{
				case 2:
					num3 += 10f;
					break;
				case 3:
					num3 += 8f;
					break;
				case 4:
					num3 += 6f;
					break;
				case 5:
					num3 += 6f;
					break;
				case 6:
					num3 += 2f;
					break;
				case 7:
					num3 += 0f;
					break;
				}
				float num4 = (float)drawinfo.drawPlayer.miscCounter / 300f * 6.2831855f;
				if (num > 0f)
				{
					float num5 = 6.2831855f / num;
					float num6 = 0f;
					Vector2 one = new Vector2(1.3f, 0.65f);
					if (!flag)
					{
						one = Vector2.One;
					}
					List<DrawData> list = new List<DrawData>();
					for (int j = 0; j < 7; j++)
					{
						if (!ownedLargeGems[j])
						{
							num6 += 1f;
						}
						else
						{
							Vector2 vector = (num4 + num5 * ((float)j - num6)).ToRotationVector2();
							float num7 = num2;
							if (flag)
							{
								num7 = MathHelper.Lerp(num2 * 0.7f, 1f, vector.Y / 2f + 0.5f);
							}
							Texture2D value = TextureAssets.Gem[j].Value;
							DrawData item = new DrawData(value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - 80f))) + vector * one * num3, null, new Color(250, 250, 250, (int)(Main.mouseTextColor / 2)), 0f, value.Size() / 2f, ((float)Main.mouseTextColor / 1000f + 0.8f) * num7, SpriteEffects.None, 0f);
							list.Add(item);
						}
					}
					if (flag)
					{
						list.Sort(new Comparison<DrawData>(DelegateMethods.CompareDrawSorterByYScale));
					}
					drawinfo.DrawDataCache.AddRange(list);
				}
			}
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x005B7E9C File Offset: 0x005B609C
		public static void DrawPlayer_37_BeetleBuff(ref PlayerDrawSet drawinfo)
		{
			if ((drawinfo.drawPlayer.beetleOffense || drawinfo.drawPlayer.beetleDefense) && drawinfo.shadow == 0f)
			{
				for (int i = 0; i < drawinfo.drawPlayer.beetleOrbs; i++)
				{
					DrawData item;
					for (int j = 0; j < 5; j++)
					{
						Color colorArmorBody = drawinfo.colorArmorBody;
						float num = (float)j * 0.1f;
						num = 0.5f - num;
						colorArmorBody.R = (byte)((float)colorArmorBody.R * num);
						colorArmorBody.G = (byte)((float)colorArmorBody.G * num);
						colorArmorBody.B = (byte)((float)colorArmorBody.B * num);
						colorArmorBody.A = (byte)((float)colorArmorBody.A * num);
						Vector2 value = -drawinfo.drawPlayer.beetleVel[i] * (float)j;
						item = new DrawData(TextureAssets.Beetle.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)(drawinfo.drawPlayer.height / 2)))) + drawinfo.drawPlayer.beetlePos[i] + value, new Rectangle?(new Rectangle(0, TextureAssets.Beetle.Height() / 3 * drawinfo.drawPlayer.beetleFrame + 1, TextureAssets.Beetle.Width(), TextureAssets.Beetle.Height() / 3 - 2)), colorArmorBody, 0f, new Vector2((float)(TextureAssets.Beetle.Width() / 2), (float)(TextureAssets.Beetle.Height() / 6)), 1f, drawinfo.playerEffect, 0f);
						drawinfo.DrawDataCache.Add(item);
					}
					item = new DrawData(TextureAssets.Beetle.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)(drawinfo.drawPlayer.height / 2)))) + drawinfo.drawPlayer.beetlePos[i], new Rectangle?(new Rectangle(0, TextureAssets.Beetle.Height() / 3 * drawinfo.drawPlayer.beetleFrame + 1, TextureAssets.Beetle.Width(), TextureAssets.Beetle.Height() / 3 - 2)), drawinfo.colorArmorBody, 0f, new Vector2((float)(TextureAssets.Beetle.Width() / 2), (float)(TextureAssets.Beetle.Height() / 6)), 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
				}
			}
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x005B8188 File Offset: 0x005B6388
		public static void DrawPlayer_38_EyebrellaCloud(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.eyebrellaCloud && drawinfo.shadow == 0f)
			{
				Texture2D value = TextureAssets.Projectile[238].Value;
				int frameY = drawinfo.drawPlayer.miscCounter % 18 / 6;
				Rectangle rectangle = value.Frame(1, 6, 0, frameY, 0, 0);
				Vector2 origin = new Vector2((float)(rectangle.Width / 2), (float)(rectangle.Height / 2));
				Vector2 value2 = new Vector2(0f, -70f);
				Vector2 vector = drawinfo.drawPlayer.MountedCenter - new Vector2(0f, (float)drawinfo.drawPlayer.height * 0.5f) + value2 - Main.screenPosition;
				Color color = Lighting.GetColor((drawinfo.drawPlayer.Top + new Vector2(0f, -30f)).ToTileCoordinates());
				int num = 170;
				int b;
				int r;
				int g = r = (b = num);
				if ((int)color.R < num)
				{
					r = (int)color.R;
				}
				if ((int)color.G < num)
				{
					g = (int)color.G;
				}
				if ((int)color.B < num)
				{
					b = (int)color.B;
				}
				Color color2 = new Color(r, g, b, 100);
				float num2 = (float)(drawinfo.drawPlayer.miscCounter % 50) / 50f;
				float num3 = 3f;
				DrawData item;
				for (int i = 0; i < 2; i++)
				{
					Vector2 value3 = new Vector2((i == 0) ? (-num3) : num3, 0f).RotatedBy((double)(num2 * 6.2831855f * ((i == 0) ? 1f : -1f)), default(Vector2));
					item = new DrawData(value, vector + value3, new Rectangle?(rectangle), color2 * 0.65f, 0f, origin, 1f, (drawinfo.drawPlayer.gravDir == -1f) ? SpriteEffects.FlipVertically : SpriteEffects.None, 0f);
					item.shader = drawinfo.cHead;
					item.ignorePlayerRotation = true;
					drawinfo.DrawDataCache.Add(item);
				}
				item = new DrawData(value, vector, new Rectangle?(rectangle), color2, 0f, origin, 1f, (drawinfo.drawPlayer.gravDir == -1f) ? SpriteEffects.FlipVertically : SpriteEffects.None, 0f);
				item.shader = drawinfo.cHead;
				item.ignorePlayerRotation = true;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x005B8410 File Offset: 0x005B6610
		private static Vector2 GetCompositeOffset_BackArm(ref PlayerDrawSet drawinfo)
		{
			return new Vector2((float)(6 * (drawinfo.playerEffect.HasFlag(SpriteEffects.FlipHorizontally) ? -1 : 1)), (float)(2 * (drawinfo.playerEffect.HasFlag(SpriteEffects.FlipVertically) ? -1 : 1)));
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x005B8460 File Offset: 0x005B6660
		private static Vector2 GetCompositeOffset_FrontArm(ref PlayerDrawSet drawinfo)
		{
			return new Vector2((float)(-5 * (drawinfo.playerEffect.HasFlag(SpriteEffects.FlipHorizontally) ? -1 : 1)), 0f);
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x005B848C File Offset: 0x005B668C
		public static void DrawPlayer_TransformDrawData(ref PlayerDrawSet drawinfo)
		{
			float rotation = drawinfo.rotation;
			Vector2 value = drawinfo.Position - Main.screenPosition + drawinfo.rotationOrigin;
			Vector2 value2 = drawinfo.drawPlayer.position + drawinfo.rotationOrigin;
			Matrix matrix = Matrix.CreateRotationZ(drawinfo.rotation);
			for (int i = 0; i < drawinfo.DustCache.Count; i++)
			{
				Vector2 vector = Main.dust[drawinfo.DustCache[i]].position - value2;
				vector = Vector2.Transform(vector, matrix);
				Main.dust[drawinfo.DustCache[i]].position = vector + value2;
			}
			for (int j = 0; j < drawinfo.GoreCache.Count; j++)
			{
				Vector2 vector2 = Main.gore[drawinfo.GoreCache[j]].position - value2;
				vector2 = Vector2.Transform(vector2, matrix);
				Main.gore[drawinfo.GoreCache[j]].position = vector2 + value2;
			}
			for (int k = 0; k < drawinfo.DrawDataCache.Count; k++)
			{
				DrawData drawData = drawinfo.DrawDataCache[k];
				if (!drawData.ignorePlayerRotation)
				{
					Vector2 vector3 = drawData.position - value;
					vector3 = Vector2.Transform(vector3, matrix);
					drawData.position = vector3 + value;
					drawData.rotation += drawinfo.rotation;
					drawinfo.DrawDataCache[k] = drawData;
				}
			}
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x005B8624 File Offset: 0x005B6824
		public static void DrawPlayer_ScaleDrawData(ref PlayerDrawSet drawinfo, float scale)
		{
			if (scale == 1f)
			{
				return;
			}
			Vector2 vector = drawinfo.Position + drawinfo.drawPlayer.Size * new Vector2(0.5f, 1f) - Main.screenPosition;
			for (int i = 0; i < drawinfo.DrawDataCache.Count; i++)
			{
				DrawData drawData = drawinfo.DrawDataCache[i];
				Vector2 value = drawData.position - vector;
				drawData.position = vector + value * scale;
				drawData.scale *= scale;
				drawinfo.DrawDataCache[i] = drawData;
			}
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x005B86D8 File Offset: 0x005B68D8
		public static void DrawPlayer_AddSelectionGlow(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.selectionGlowColor == Color.Transparent)
			{
				return;
			}
			Color selectionGlowColor = drawinfo.selectionGlowColor;
			List<DrawData> list = new List<DrawData>();
			list.AddRange(PlayerDrawLayers.GetFlatColoredCloneData(ref drawinfo, new Vector2(0f, -2f), selectionGlowColor));
			list.AddRange(PlayerDrawLayers.GetFlatColoredCloneData(ref drawinfo, new Vector2(0f, 2f), selectionGlowColor));
			list.AddRange(PlayerDrawLayers.GetFlatColoredCloneData(ref drawinfo, new Vector2(2f, 0f), selectionGlowColor));
			list.AddRange(PlayerDrawLayers.GetFlatColoredCloneData(ref drawinfo, new Vector2(-2f, 0f), selectionGlowColor));
			list.AddRange(drawinfo.DrawDataCache);
			drawinfo.DrawDataCache = list;
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x005B8788 File Offset: 0x005B6988
		public static void DrawPlayer_MakeIntoFirstFractalAfterImage(ref PlayerDrawSet drawinfo)
		{
			if (!drawinfo.drawPlayer.isFirstFractalAfterImage)
			{
				if (drawinfo.drawPlayer.HeldItem.type == 4722)
				{
					bool flag = drawinfo.drawPlayer.itemAnimation > 0;
				}
				return;
			}
			for (int i = 0; i < drawinfo.DrawDataCache.Count; i++)
			{
				DrawData value = drawinfo.DrawDataCache[i];
				value.color *= drawinfo.drawPlayer.firstFractalAfterImageOpacity;
				value.color.A = (byte)((float)value.color.A * 0.8f);
				drawinfo.DrawDataCache[i] = value;
			}
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x005B8840 File Offset: 0x005B6A40
		public static void DrawPlayer_RenderAllLayers(ref PlayerDrawSet drawinfo)
		{
			List<DrawData> drawDataCache = drawinfo.DrawDataCache;
			if (PlayerDrawLayers.spriteBuffer == null)
			{
				PlayerDrawLayers.spriteBuffer = new SpriteDrawBuffer(Main.graphics.GraphicsDevice, 200);
			}
			else
			{
				PlayerDrawLayers.spriteBuffer.CheckGraphicsDevice(Main.graphics.GraphicsDevice);
			}
			foreach (DrawData drawData in drawDataCache)
			{
				if (drawData.texture != null)
				{
					drawData.Draw(PlayerDrawLayers.spriteBuffer);
				}
			}
			PlayerDrawLayers.spriteBuffer.UploadAndBind();
			DrawData drawData2 = default(DrawData);
			int num = 0;
			for (int i = 0; i <= drawDataCache.Count; i++)
			{
				if (drawinfo.projectileDrawPosition == i)
				{
					if (drawData2.shader != 0)
					{
						Main.pixelShader.CurrentTechnique.Passes[0].Apply();
					}
					PlayerDrawLayers.spriteBuffer.Unbind();
					PlayerDrawLayers.DrawHeldProj(drawinfo, Main.projectile[drawinfo.drawPlayer.heldProj]);
					PlayerDrawLayers.spriteBuffer.Bind();
				}
				if (i != drawDataCache.Count)
				{
					drawData2 = drawDataCache[i];
					if (drawData2.sourceRect == null)
					{
						drawData2.sourceRect = new Rectangle?(drawData2.texture.Frame(1, 1, 0, 0, 0, 0));
					}
					PlayerDrawHelper.SetShaderForData(drawinfo.drawPlayer, drawinfo.cHead, ref drawData2);
					if (drawData2.texture != null)
					{
						PlayerDrawLayers.spriteBuffer.DrawSingle(num++);
					}
				}
			}
			PlayerDrawLayers.spriteBuffer.Unbind();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x005B89F4 File Offset: 0x005B6BF4
		private static void DrawHeldProj(PlayerDrawSet drawinfo, Projectile proj)
		{
			if (!ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[proj.type])
			{
				proj.gfxOffY = drawinfo.drawPlayer.gfxOffY;
			}
			try
			{
				Main.instance.DrawProjDirect(proj);
			}
			catch
			{
				proj.active = false;
			}
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x005B8A48 File Offset: 0x005B6C48
		public static void DrawPlayer_RenderAllLayersSlow(ref PlayerDrawSet drawinfo)
		{
			int num = -1;
			List<DrawData> drawDataCache = drawinfo.DrawDataCache;
			Effect pixelShader = Main.pixelShader;
			Projectile[] projectile = Main.projectile;
			SpriteBatch spriteBatch = Main.spriteBatch;
			for (int i = 0; i <= drawDataCache.Count; i++)
			{
				if (drawinfo.projectileDrawPosition == i)
				{
					if (!ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[projectile[drawinfo.drawPlayer.heldProj].type])
					{
						projectile[drawinfo.drawPlayer.heldProj].gfxOffY = drawinfo.drawPlayer.gfxOffY;
					}
					if (num != 0)
					{
						pixelShader.CurrentTechnique.Passes[0].Apply();
						num = 0;
					}
					try
					{
						Main.instance.DrawProj(drawinfo.drawPlayer.heldProj);
					}
					catch
					{
						projectile[drawinfo.drawPlayer.heldProj].active = false;
					}
				}
				if (i != drawDataCache.Count)
				{
					DrawData drawData = drawDataCache[i];
					if (drawData.sourceRect == null)
					{
						drawData.sourceRect = new Rectangle?(drawData.texture.Frame(1, 1, 0, 0, 0, 0));
					}
					PlayerDrawHelper.SetShaderForData(drawinfo.drawPlayer, drawinfo.cHead, ref drawData);
					num = drawData.shader;
					if (drawData.texture != null)
					{
						drawData.Draw(spriteBatch);
					}
				}
			}
			pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x005B8BAC File Offset: 0x005B6DAC
		public static void DrawPlayer_DrawSelectionRect(ref PlayerDrawSet drawinfo)
		{
			Vector2 value;
			Vector2 value2;
			SpriteRenderTargetHelper.GetDrawBoundary(drawinfo.DrawDataCache, out value, out value2);
			Utils.DrawRect(Main.spriteBatch, value + Main.screenPosition, value2 + Main.screenPosition, Color.White);
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x005B8BED File Offset: 0x005B6DED
		private static bool IsArmorDrawnWhenInvisible(int torsoID)
		{
			return torsoID - 21 > 1;
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x005B8BFC File Offset: 0x005B6DFC
		private static DrawData[] GetFlatColoredCloneData(ref PlayerDrawSet drawinfo, Vector2 offset, Color color)
		{
			int colorOnlyShaderIndex = ContentSamples.CommonlyUsedContentSamples.ColorOnlyShaderIndex;
			DrawData[] array = new DrawData[drawinfo.DrawDataCache.Count];
			for (int i = 0; i < drawinfo.DrawDataCache.Count; i++)
			{
				DrawData drawData = drawinfo.DrawDataCache[i];
				drawData.position += offset;
				drawData.shader = colorOnlyShaderIndex;
				drawData.color = color;
				array[i] = drawData;
			}
			return array;
		}

		// Token: 0x040050AC RID: 20652
		private const int DEFAULT_MAX_SPRITES = 200;

		// Token: 0x040050AD RID: 20653
		private static SpriteDrawBuffer spriteBuffer;
	}
}
