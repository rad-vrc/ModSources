using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.GameContent.Liquid;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terraria.DataStructures
{
	// Token: 0x02000726 RID: 1830
	public static class PlayerDrawLayers
	{
		// Token: 0x06004A31 RID: 18993 RVA: 0x0065575C File Offset: 0x0065395C
		public static void DrawPlayer_extra_TorsoPlus(ref PlayerDrawSet drawinfo)
		{
			drawinfo.Position.Y = drawinfo.Position.Y + drawinfo.torsoOffset;
			drawinfo.ItemLocation.Y = drawinfo.ItemLocation.Y + drawinfo.torsoOffset;
		}

		// Token: 0x06004A32 RID: 18994 RVA: 0x00655788 File Offset: 0x00653988
		public static void DrawPlayer_extra_TorsoMinus(ref PlayerDrawSet drawinfo)
		{
			drawinfo.Position.Y = drawinfo.Position.Y - drawinfo.torsoOffset;
			drawinfo.ItemLocation.Y = drawinfo.ItemLocation.Y - drawinfo.torsoOffset;
		}

		// Token: 0x06004A33 RID: 18995 RVA: 0x006557B4 File Offset: 0x006539B4
		public static void DrawPlayer_extra_MountPlus(ref PlayerDrawSet drawinfo)
		{
			drawinfo.Position.Y = drawinfo.Position.Y + (float)((int)drawinfo.mountOffSet / 2);
		}

		// Token: 0x06004A34 RID: 18996 RVA: 0x006557CF File Offset: 0x006539CF
		public static void DrawPlayer_extra_MountMinus(ref PlayerDrawSet drawinfo)
		{
			drawinfo.Position.Y = drawinfo.Position.Y - (float)((int)drawinfo.mountOffSet / 2);
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x006557EC File Offset: 0x006539EC
		public static void DrawCompositeArmorPiece(ref PlayerDrawSet drawinfo, CompositePlayerDrawContext context, DrawData data)
		{
			drawinfo.DrawDataCache.Add(data);
			switch (context)
			{
			case CompositePlayerDrawContext.BackShoulder:
			case CompositePlayerDrawContext.BackArm:
			case CompositePlayerDrawContext.FrontArm:
			case CompositePlayerDrawContext.FrontShoulder:
				if (drawinfo.armGlowColor.PackedValue != 0U)
				{
					DrawData item2 = data;
					item2.color = drawinfo.armGlowColor;
					Rectangle value2 = item2.sourceRect.Value;
					value2.Y += 224;
					item2.sourceRect = new Rectangle?(value2);
					if (drawinfo.drawPlayer.body == 227)
					{
						Vector2 position2 = item2.position;
						for (int i = 0; i < 2; i++)
						{
							Vector2 vector2;
							vector2..ctor((float)Main.rand.Next(-10, 10) * 0.125f, (float)Main.rand.Next(-10, 10) * 0.125f);
							item2.position = position2 + vector2;
							if (i == 0)
							{
								drawinfo.DrawDataCache.Add(item2);
							}
						}
					}
					drawinfo.DrawDataCache.Add(item2);
				}
				break;
			case CompositePlayerDrawContext.Torso:
				if (drawinfo.bodyGlowColor.PackedValue != 0U)
				{
					DrawData item3 = data;
					item3.color = drawinfo.bodyGlowColor;
					Rectangle value3 = item3.sourceRect.Value;
					value3.Y += 224;
					item3.sourceRect = new Rectangle?(value3);
					if (drawinfo.drawPlayer.body == 227)
					{
						Vector2 position3 = item3.position;
						for (int j = 0; j < 2; j++)
						{
							Vector2 vector3;
							vector3..ctor((float)Main.rand.Next(-10, 10) * 0.125f, (float)Main.rand.Next(-10, 10) * 0.125f);
							item3.position = position3 + vector3;
							if (j == 0)
							{
								drawinfo.DrawDataCache.Add(item3);
							}
						}
					}
					drawinfo.DrawDataCache.Add(item3);
				}
				break;
			}
			if (context == CompositePlayerDrawContext.FrontShoulder && drawinfo.drawPlayer.head == 269)
			{
				Vector2 position4 = drawinfo.helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect;
				DrawData item4 = new DrawData(TextureAssets.Extra[214].Value, position4, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item4.shader = drawinfo.cHead;
				drawinfo.DrawDataCache.Add(item4);
				item4 = new DrawData(TextureAssets.GlowMask[308].Value, position4, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item4.shader = drawinfo.cHead;
				drawinfo.DrawDataCache.Add(item4);
			}
			if (context == CompositePlayerDrawContext.FrontArm && drawinfo.drawPlayer.body == 205)
			{
				Color color;
				color..ctor(100, 100, 100, 0);
				ulong seed = (ulong)((long)(drawinfo.drawPlayer.miscCounter / 4));
				int num = 4;
				for (int k = 0; k < num; k++)
				{
					float num2 = (float)Utils.RandomInt(ref seed, -10, 11) * 0.2f;
					float num3 = (float)Utils.RandomInt(ref seed, -10, 1) * 0.15f;
					DrawData item5 = data;
					Rectangle value4 = item5.sourceRect.Value;
					value4.Y += 224;
					item5.sourceRect = new Rectangle?(value4);
					item5.position.X = item5.position.X + num2;
					item5.position.Y = item5.position.Y + num3;
					item5.color = color;
					drawinfo.DrawDataCache.Add(item5);
				}
			}
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x00655C44 File Offset: 0x00653E44
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
					DrawData item2 = new DrawData(TextureAssets.PlayerHairAlt[drawinfo.drawPlayer.hair].Value, position, new Rectangle?(drawinfo.hairBackFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					item2.shader = drawinfo.hairDyePacked;
					drawinfo.DrawDataCache.Add(item2);
				}
			}
		}

		// Token: 0x06004A37 RID: 18999 RVA: 0x00655E04 File Offset: 0x00654004
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

		// Token: 0x06004A38 RID: 19000 RVA: 0x00655E9C File Offset: 0x0065409C
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

		// Token: 0x06004A39 RID: 19001 RVA: 0x00655FE4 File Offset: 0x006541E4
		public static void DrawPlayer_03_PortableStool(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.portableStoolInfo.IsInUse)
			{
				Texture2D value = TextureAssets.Extra[102].Value;
				Vector2 position;
				position..ctor((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height + 28f)));
				Rectangle rectangle = value.Frame(1, 1, 0, 0, 0, 0);
				Vector2 origin = rectangle.Size() * new Vector2(0.5f, 1f);
				DrawData item = new DrawData(value, position, new Rectangle?(rectangle), drawinfo.colorArmorLegs, drawinfo.drawPlayer.bodyRotation, origin, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cPortableStool;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06004A3A RID: 19002 RVA: 0x006560E0 File Offset: 0x006542E0
		public static void DrawPlayer_04_ElectrifiedDebuffBack(ref PlayerDrawSet drawinfo)
		{
			if (!drawinfo.drawPlayer.electrified || drawinfo.shadow != 0f)
			{
				return;
			}
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

		// Token: 0x06004A3B RID: 19003 RVA: 0x00656278 File Offset: 0x00654478
		public static void DrawPlayer_05_ForbiddenSetRing(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.setForbidden && drawinfo.shadow == 0f)
			{
				Color color = Color.Lerp(drawinfo.colorArmorBody, Color.White, 0.7f);
				Texture2D value = TextureAssets.Extra[74].Value;
				Texture2D value2 = TextureAssets.GlowMask[217].Value;
				bool flag = !drawinfo.drawPlayer.setForbiddenCooldownLocked;
				int num2 = (int)(((float)drawinfo.drawPlayer.miscCounter / 300f * 6.2831855f).ToRotationVector2().Y * 6f);
				float num3 = ((float)drawinfo.drawPlayer.miscCounter / 75f * 6.2831855f).ToRotationVector2().X * 4f;
				Color color2 = new Color(80, 70, 40, 0) * (num3 / 8f + 0.5f) * 0.8f;
				if (!flag)
				{
					num2 = 0;
					num3 = 2f;
					color2 = new Color(80, 70, 40, 0) * 0.3f;
					color = color.MultiplyRGB(new Color(0.5f, 0.5f, 1f));
				}
				Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
				int num4 = 10;
				int num5 = 20;
				if (drawinfo.drawPlayer.head == 238)
				{
					num4 += 4;
					num5 += 4;
				}
				vector += new Vector2((float)(-(float)drawinfo.drawPlayer.direction * num4), (float)(-(float)num5) * drawinfo.drawPlayer.gravDir + (float)num2 * drawinfo.drawPlayer.gravDir);
				DrawData item = new DrawData(value, vector, null, color, drawinfo.drawPlayer.bodyRotation, value.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cBody;
				drawinfo.DrawDataCache.Add(item);
				for (float num6 = 0f; num6 < 4f; num6 += 1f)
				{
					item = new DrawData(value2, vector + (num6 * 1.5707964f).ToRotationVector2() * num3, null, color2, drawinfo.drawPlayer.bodyRotation, value.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
				}
			}
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x006565A8 File Offset: 0x006547A8
		public static void DrawPlayer_01_3_BackHead(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.head >= 0)
			{
				int num = ArmorIDs.Head.Sets.FrontToBackID[drawinfo.drawPlayer.head];
				if (num >= 0)
				{
					Vector2 helmetOffset = drawinfo.helmetOffset;
					DrawData item = new DrawData(TextureAssets.ArmorHead[num].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cHead;
					drawinfo.DrawDataCache.Add(item);
				}
			}
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x006566E8 File Offset: 0x006548E8
		public static void DrawPlayer_01_2_JimsCloak(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.legs == 60 && !drawinfo.isSitting && !drawinfo.drawPlayer.invis && (!PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo) || drawinfo.drawPlayer.wearsRobe))
			{
				DrawData item = new DrawData(TextureAssets.Extra[153].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cLegs;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x0065683C File Offset: 0x00654A3C
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

		// Token: 0x06004A3F RID: 19007 RVA: 0x00656B24 File Offset: 0x00654D24
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

		// Token: 0x06004A40 RID: 19008 RVA: 0x00656C90 File Offset: 0x00654E90
		public static void DrawPlayer_07_LeinforsHairShampoo(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.headOnlyRender)
			{
				return;
			}
			if (drawinfo.drawPlayer.leinforsHair && (drawinfo.fullHair || drawinfo.hatHair || drawinfo.drawsBackHairWithoutHeadgear || drawinfo.drawPlayer.head == -1 || drawinfo.drawPlayer.head == 0) && drawinfo.drawPlayer.hair != 12 && drawinfo.shadow == 0f && Main.rgbToHsl(drawinfo.colorHead).Z > 0.2f)
			{
				if (Main.rand.Next(20) == 0 && !drawinfo.hatHair)
				{
					Rectangle r = Utils.CenteredRectangle(drawinfo.Position + drawinfo.drawPlayer.Size / 2f + new Vector2(0f, drawinfo.drawPlayer.gravDir * -20f), new Vector2(20f, 14f));
					int num = Dust.NewDust(r.TopLeft(), r.Width, r.Height, 204, 0f, 0f, 150, default(Color), 0.3f);
					Main.dust[num].fadeIn = 1f;
					Main.dust[num].velocity *= 0.1f;
					Main.dust[num].noLight = true;
					Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(drawinfo.drawPlayer.cLeinShampoo, drawinfo.drawPlayer);
					drawinfo.DustCache.Add(num);
				}
				if (Main.rand.Next(40) == 0 && drawinfo.hatHair)
				{
					Rectangle r2 = Utils.CenteredRectangle(drawinfo.Position + drawinfo.drawPlayer.Size / 2f + new Vector2((float)(drawinfo.drawPlayer.direction * -10), drawinfo.drawPlayer.gravDir * -10f), new Vector2(5f, 5f));
					int num2 = Dust.NewDust(r2.TopLeft(), r2.Width, r2.Height, 204, 0f, 0f, 150, default(Color), 0.3f);
					Main.dust[num2].fadeIn = 1f;
					Main.dust[num2].velocity *= 0.1f;
					Main.dust[num2].noLight = true;
					Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(drawinfo.drawPlayer.cLeinShampoo, drawinfo.drawPlayer);
					drawinfo.DustCache.Add(num2);
				}
				if (drawinfo.drawPlayer.velocity.X != 0f && drawinfo.backHairDraw && Main.rand.Next(15) == 0)
				{
					Rectangle r3 = Utils.CenteredRectangle(drawinfo.Position + drawinfo.drawPlayer.Size / 2f + new Vector2((float)(drawinfo.drawPlayer.direction * -14), 0f), new Vector2(4f, 30f));
					int num3 = Dust.NewDust(r3.TopLeft(), r3.Width, r3.Height, 204, 0f, 0f, 150, default(Color), 0.3f);
					Main.dust[num3].fadeIn = 1f;
					Main.dust[num3].velocity *= 0.1f;
					Main.dust[num3].noLight = true;
					Main.dust[num3].shader = GameShaders.Armor.GetSecondaryShader(drawinfo.drawPlayer.cLeinShampoo, drawinfo.drawPlayer);
					drawinfo.DustCache.Add(num3);
				}
			}
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x006570A2 File Offset: 0x006552A2
		public static bool DrawPlayer_08_PlayerVisuallyHasFullArmorSet(PlayerDrawSet drawinfo, int head, int body, int legs)
		{
			return drawinfo.drawPlayer.head == head && drawinfo.drawPlayer.body == body && drawinfo.drawPlayer.legs == legs;
		}

		// Token: 0x06004A42 RID: 19010 RVA: 0x006570D0 File Offset: 0x006552D0
		public static void DrawPlayer_08_Backpacks(ref PlayerDrawSet drawinfo)
		{
			if (PlayerDrawLayers.DrawPlayer_08_PlayerVisuallyHasFullArmorSet(drawinfo, 266, 235, 218))
			{
				Vector2 vec = new Vector2(-2f + -2f * drawinfo.drawPlayer.Directions.X, 0f) + drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2));
				vec = vec.Floor();
				Texture2D value = TextureAssets.Extra[212].Value;
				Rectangle value2 = value.Frame(1, 5, 0, drawinfo.drawPlayer.miscCounter % 25 / 5, 0, 0);
				Color immuneAlphaPure = drawinfo.drawPlayer.GetImmuneAlphaPure(new Color(250, 250, 250, 200), drawinfo.shadow);
				immuneAlphaPure *= drawinfo.drawPlayer.stealth;
				DrawData item = new DrawData(value, vec, new Rectangle?(value2), immuneAlphaPure, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cBody;
				drawinfo.DrawDataCache.Add(item);
			}
			if (PlayerDrawLayers.DrawPlayer_08_PlayerVisuallyHasFullArmorSet(drawinfo, 268, 237, 222))
			{
				Vector2 vec2 = new Vector2(-9f + 1f * drawinfo.drawPlayer.Directions.X, -4f * drawinfo.drawPlayer.Directions.Y) + drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2));
				vec2 = vec2.Floor();
				Texture2D value3 = TextureAssets.Extra[213].Value;
				Rectangle value4 = value3.Frame(1, 5, 0, drawinfo.drawPlayer.miscCounter % 25 / 5, 0, 0);
				Color immuneAlphaPure2 = drawinfo.drawPlayer.GetImmuneAlphaPure(new Color(250, 250, 250, 200), drawinfo.shadow);
				immuneAlphaPure2 *= drawinfo.drawPlayer.stealth;
				DrawData item2 = new DrawData(value3, vec2, new Rectangle?(value4), immuneAlphaPure2, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item2.shader = drawinfo.cBody;
				drawinfo.DrawDataCache.Add(item2);
			}
			if (drawinfo.heldItem.type == 4818 && drawinfo.drawPlayer.ownedProjectileCounts[902] == 0)
			{
				int num = 8;
				Vector2 vector;
				vector..ctor(0f, 8f);
				Vector2 vec3 = drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0f, -4f) + vector;
				vec3 = vec3.Floor();
				DrawData item3 = new DrawData(TextureAssets.BackPack[num].Value, vec3, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item3);
			}
			if (drawinfo.drawPlayer.backpack > 0 && !drawinfo.drawPlayer.mount.Active)
			{
				Vector2 vector2;
				vector2..ctor(0f, 8f);
				Vector2 vec4 = drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0f, -4f) + vector2;
				vec4 = vec4.Floor();
				DrawData item4 = new DrawData(TextureAssets.AccBack[drawinfo.drawPlayer.backpack].Value, vec4, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item4.shader = drawinfo.cBackpack;
				drawinfo.DrawDataCache.Add(item4);
				return;
			}
			if (drawinfo.heldItem.type != 1178 && drawinfo.heldItem.type != 779 && drawinfo.heldItem.type != 5134 && drawinfo.heldItem.type != 1295 && drawinfo.heldItem.type != 1910 && !drawinfo.drawPlayer.turtleArmor && drawinfo.drawPlayer.body != 106 && drawinfo.drawPlayer.body != 170)
			{
				return;
			}
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
			else if (type <= 1178)
			{
				if (type != 779)
				{
					if (type == 1178)
					{
						num2 = 1;
					}
				}
				else
				{
					num2 = 2;
				}
			}
			else if (type != 1295)
			{
				if (type != 1910)
				{
					if (type == 5134)
					{
						num2 = 9;
					}
				}
				else
				{
					num2 = 5;
				}
			}
			else
			{
				num2 = 3;
			}
			Vector2 vector3;
			vector3..ctor(0f, 8f);
			Vector2 vec5 = drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0f, -4f) + vector3;
			vec5 = vec5.Floor();
			Vector2 vec6 = drawinfo.Position - Main.screenPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2((-9f + num3) * (float)drawinfo.drawPlayer.direction, (2f + num4) * drawinfo.drawPlayer.gravDir) + vector3;
			vec6 = vec6.Floor();
			switch (num2)
			{
			case 4:
			case 6:
			{
				DrawData item5 = new DrawData(TextureAssets.BackPack[num2].Value, vec5, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item5.shader = shader;
				drawinfo.DrawDataCache.Add(item5);
				return;
			}
			case 7:
			{
				DrawData item6 = new DrawData(TextureAssets.BackPack[num2].Value, vec5, new Rectangle?(new Rectangle(0, drawinfo.drawPlayer.bodyFrame.Y, TextureAssets.BackPack[num2].Width(), drawinfo.drawPlayer.bodyFrame.Height)), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float)TextureAssets.BackPack[num2].Width() * 0.5f, drawinfo.bodyVect.Y), 1f, drawinfo.playerEffect, 0f);
				item6.shader = shader;
				drawinfo.DrawDataCache.Add(item6);
				return;
			}
			}
			DrawData item7 = new DrawData(TextureAssets.BackPack[num2].Value, vec6, new Rectangle?(new Rectangle(0, 0, TextureAssets.BackPack[num2].Width(), TextureAssets.BackPack[num2].Height())), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.BackPack[num2].Width() / 2), (float)(TextureAssets.BackPack[num2].Height() / 2)), 1f, drawinfo.playerEffect, 0f);
			item7.shader = shader;
			drawinfo.DrawDataCache.Add(item7);
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x00657A24 File Offset: 0x00655C24
		public static void DrawPlayer_08_1_Tails(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.tail > 0 && !drawinfo.drawPlayer.mount.Active)
			{
				Vector2 zero = Vector2.Zero;
				if (drawinfo.isSitting)
				{
					zero.Y += -2f;
				}
				Vector2 vector;
				vector..ctor(0f, 8f);
				Vector2 vec = zero + drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0f, -4f) + vector;
				vec = vec.Floor();
				DrawData item = new DrawData(TextureAssets.AccBack[drawinfo.drawPlayer.tail].Value, vec, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cTail;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x00657B74 File Offset: 0x00655D74
		public static void DrawPlayer_10_BackAcc(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.back <= 0)
			{
				return;
			}
			if (drawinfo.drawPlayer.front >= 1)
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
			Vector2 vector;
			vector..ctor(0f, 8f);
			Vector2 vec = zero + drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0f, -4f) + vector;
			vec = vec.Floor();
			DrawData item = new DrawData(TextureAssets.AccBack[drawinfo.drawPlayer.back].Value, vec, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
			item.shader = drawinfo.cBack;
			drawinfo.DrawDataCache.Add(item);
			if (drawinfo.drawPlayer.back == 36)
			{
				Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
				Rectangle value = bodyFrame;
				value.Width = 2;
				int num2 = 0;
				int num3 = bodyFrame.Width / 2;
				int num4 = 2;
				if (drawinfo.playerEffect.HasFlag(1))
				{
					num2 = bodyFrame.Width - 2;
					num4 = -2;
				}
				for (int i = 0; i < num3; i++)
				{
					value.X = bodyFrame.X + 2 * i;
					Color immuneAlpha = drawinfo.drawPlayer.GetImmuneAlpha(LiquidRenderer.GetShimmerGlitterColor(true, (float)i / 16f, 0f), drawinfo.shadow);
					immuneAlpha *= (float)drawinfo.colorArmorBody.A / 255f;
					item = new DrawData(TextureAssets.GlowMask[332].Value, vec + new Vector2((float)(num2 + i * num4), 0f), new Rectangle?(value), immuneAlpha, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cBack;
					drawinfo.DrawDataCache.Add(item);
				}
			}
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x00657E5C File Offset: 0x0065605C
		public static void DrawPlayer_09_Wings(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.dead || drawinfo.hideEntirePlayer)
			{
				return;
			}
			Vector2 directions = drawinfo.drawPlayer.Directions;
			Vector2 vector = drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.Size / 2f;
			Vector2 vector2;
			vector2..ctor(0f, 7f);
			vector = drawinfo.Position - Main.screenPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + vector2;
			if (drawinfo.drawPlayer.wings <= 0)
			{
				return;
			}
			Main.instance.LoadWings(drawinfo.drawPlayer.wings);
			if (drawinfo.drawPlayer.wings == 22)
			{
				if (!drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated())
				{
					return;
				}
				Main.instance.LoadItemFlames(1866);
				Color colorArmorBody = drawinfo.colorArmorBody;
				int num = 26;
				int num2 = -9;
				Vector2 vector3 = vector + new Vector2((float)num2, (float)num) * directions;
				DrawData item;
				if (drawinfo.shadow == 0f && drawinfo.drawPlayer.grappling[0] == -1)
				{
					for (int i = 0; i < 7; i++)
					{
						Color color;
						color..ctor(250 - i * 10, 250 - i * 10, 250 - i * 10, 150 - i * 10);
						Vector2 vector4;
						vector4..ctor((float)Main.rand.Next(-10, 11) * 0.2f, (float)Main.rand.Next(-10, 11) * 0.2f);
						drawinfo.stealth *= drawinfo.stealth;
						drawinfo.stealth *= 1f - drawinfo.shadow;
						color..ctor((int)((float)color.R * drawinfo.stealth), (int)((float)color.G * drawinfo.stealth), (int)((float)color.B * drawinfo.stealth), (int)((float)color.A * drawinfo.stealth));
						vector4.X = drawinfo.drawPlayer.itemFlamePos[i].X;
						vector4.Y = 0f - drawinfo.drawPlayer.itemFlamePos[i].Y;
						vector4 *= 0.5f;
						Vector2 position = (vector3 + vector4).Floor();
						item = new DrawData(TextureAssets.ItemFlame[1866].Value, position, new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7 - 2)), color, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 14)), 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(item);
					}
				}
				item = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vector3.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7)), colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 14)), 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cWings;
				drawinfo.DrawDataCache.Add(item);
				return;
			}
			else
			{
				if (drawinfo.drawPlayer.wings == 28)
				{
					if (drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated())
					{
						Color colorArmorBody2 = drawinfo.colorArmorBody;
						Vector2 vector5;
						vector5..ctor(0f, 19f);
						Vector2 vec = vector + vector5 * directions;
						Texture2D value = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
						Rectangle rectangle = value.Frame(1, 4, 0, drawinfo.drawPlayer.miscCounter / 5 % 4, 0, 0);
						rectangle.Width -= 2;
						rectangle.Height -= 2;
						DrawData item = new DrawData(value, vec.Floor(), new Rectangle?(rectangle), Color.Lerp(colorArmorBody2, Color.White, 1f), drawinfo.drawPlayer.bodyRotation, rectangle.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(item);
						value = TextureAssets.Extra[38].Value;
						item = new DrawData(value, vec.Floor(), new Rectangle?(rectangle), Color.Lerp(colorArmorBody2, Color.White, 0.5f), drawinfo.drawPlayer.bodyRotation, rectangle.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(item);
					}
					return;
				}
				if (drawinfo.drawPlayer.wings == 45)
				{
					if (!drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated())
					{
						return;
					}
					PlayerDrawLayers.DrawStarboardRainbowTrail(ref drawinfo, vector, directions);
					Color color10 = new Color(255, 255, 255, 255);
					int num3 = 22;
					int num4 = 0;
					Vector2 vec2 = vector + new Vector2((float)num4, (float)num3) * directions;
					Color color2 = color10 * (1f - drawinfo.shadow);
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
					}
					return;
				}
				else
				{
					DrawData item;
					if (drawinfo.drawPlayer.wings == 34)
					{
						if (drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated())
						{
							drawinfo.stealth *= drawinfo.stealth;
							drawinfo.stealth *= 1f - drawinfo.shadow;
							Color color4;
							color4..ctor((int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(100f * drawinfo.stealth));
							Vector2 vector6;
							vector6..ctor(0f, 0f);
							Texture2D value2 = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
							Vector2 vec3 = drawinfo.Position + drawinfo.drawPlayer.Size / 2f - Main.screenPosition + vector6 * drawinfo.drawPlayer.Directions - Vector2.UnitX * (float)drawinfo.drawPlayer.direction * 4f;
							Rectangle rectangle2 = value2.Frame(1, 6, 0, drawinfo.drawPlayer.wingFrame, 0, 0);
							rectangle2.Width -= 2;
							rectangle2.Height -= 2;
							item = new DrawData(value2, vec3.Floor(), new Rectangle?(rectangle2), color4, drawinfo.drawPlayer.bodyRotation, rectangle2.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
							item.shader = drawinfo.cWings;
							drawinfo.DrawDataCache.Add(item);
						}
						return;
					}
					if (drawinfo.drawPlayer.wings == 40)
					{
						drawinfo.stealth *= drawinfo.stealth;
						drawinfo.stealth *= 1f - drawinfo.shadow;
						Color color5;
						color5..ctor((int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(100f * drawinfo.stealth));
						Vector2 vector7;
						vector7..ctor(-4f, 0f);
						Texture2D value3 = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
						Vector2 vector8 = vector + vector7 * directions;
						for (int j = 0; j < 1; j++)
						{
							SpriteEffects spriteEffects = drawinfo.playerEffect;
							Vector2 scale;
							scale..ctor(1f);
							Vector2 zero = Vector2.Zero;
							zero.X = (float)(drawinfo.drawPlayer.direction * 3);
							if (j == 1)
							{
								spriteEffects ^= 1;
								scale..ctor(0.7f, 1f);
								zero.X += (float)(-(float)drawinfo.drawPlayer.direction) * 6f;
							}
							Vector2 vector9 = drawinfo.drawPlayer.velocity * -1.5f;
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
								Vector2 vec4 = vector8;
								Rectangle rectangle3 = value3.Frame(1, 14, 0, k, 0, 0);
								rectangle3.Width -= 2;
								rectangle3.Height -= 2;
								int num10 = (k - num7) % (int)num9;
								Vector2 vector10 = new Vector2(0f, 0.5f).RotatedBy((double)((drawinfo.drawPlayer.miscCounterNormalized * (2f + (float)num10) + (float)num10 * 0.5f + (float)j * 1.3f) * 6.2831855f), default(Vector2)) * (float)(num10 + 1);
								vec4 += vector10;
								vec4 += vector9 * ((float)num10 / num9);
								vec4 += zero;
								item = new DrawData(value3, vec4.Floor(), new Rectangle?(rectangle3), color5, drawinfo.drawPlayer.bodyRotation, rectangle3.Size() / 2f, scale, spriteEffects, 0f);
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
							Vector2 vector11;
							vector11..ctor(-6f, -7f);
							Texture2D value4 = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
							Vector2 vec5 = vector + vector11 * directions;
							Rectangle rectangle4 = value4.Frame(1, 6, 0, drawinfo.drawPlayer.wingFrame, 0, 0);
							rectangle4.Width -= 2;
							rectangle4.Height -= 2;
							item = new DrawData(value4, vec5.Floor(), new Rectangle?(rectangle4), colorArmorBody3, drawinfo.drawPlayer.bodyRotation, rectangle4.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
							item.shader = drawinfo.cWings;
							drawinfo.DrawDataCache.Add(item);
						}
						return;
					}
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
						color6..ctor((int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(100f * drawinfo.stealth));
					}
					if (drawinfo.drawPlayer.wings == 10)
					{
						drawinfo.stealth *= drawinfo.stealth;
						drawinfo.stealth *= 1f - drawinfo.shadow;
						color6..ctor((int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(175f * drawinfo.stealth));
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
					Vector2 vector12 = vector + new Vector2((float)(num12 - 9), (float)(num11 + 2)) * directions;
					item = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vector12.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num13 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num13)), color6, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num13 / 2)), 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cWings;
					drawinfo.DrawDataCache.Add(item);
					if (drawinfo.drawPlayer.wings == 43 && drawinfo.shadow == 0f)
					{
						Vector2 vector13 = vector12;
						Vector2 origin;
						origin..ctor((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num13 / 2));
						Rectangle value5;
						value5..ctor(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num13 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num13);
						for (int l = 0; l < 2; l++)
						{
							Vector2 position2 = vector13 + new Vector2((float)Main.rand.Next(-10, 10) * 0.125f, (float)Main.rand.Next(-10, 10) * 0.125f);
							item = new DrawData(TextureAssets.GlowMask[272].Value, position2, new Rectangle?(value5), new Color(230, 230, 230, 60), drawinfo.drawPlayer.bodyRotation, origin, 1f, drawinfo.playerEffect, 0f);
							item.shader = drawinfo.cWings;
							drawinfo.DrawDataCache.Add(item);
						}
					}
					if (drawinfo.drawPlayer.wings == 23)
					{
						drawinfo.stealth *= drawinfo.stealth;
						drawinfo.stealth *= 1f - drawinfo.shadow;
						Color color11;
						color11..ctor((int)(200f * drawinfo.stealth), (int)(200f * drawinfo.stealth), (int)(200f * drawinfo.stealth), (int)(200f * drawinfo.stealth));
						item = new DrawData(TextureAssets.Flames[8].Value, vector12.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), color11, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(item);
						return;
					}
					if (drawinfo.drawPlayer.wings == 27)
					{
						item = new DrawData(TextureAssets.GlowMask[92].Value, vector12.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color(255, 255, 255, 127) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
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
							item = new DrawData(target, vector12.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7)), new Color(255, 255, 255, 255) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 14)), 1f, drawinfo.playerEffect, 0f);
							item.shader = drawinfo.cWings;
							drawinfo.DrawDataCache.Add(item);
							return;
						}
					}
					else
					{
						if (drawinfo.drawPlayer.wings == 30)
						{
							item = new DrawData(TextureAssets.GlowMask[181].Value, vector12.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color(255, 255, 255, 127) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
							item.shader = drawinfo.cWings;
							drawinfo.DrawDataCache.Add(item);
							return;
						}
						if (drawinfo.drawPlayer.wings == 38)
						{
							Color color7 = drawinfo.ArkhalisColor * drawinfo.stealth * (1f - drawinfo.shadow);
							item = new DrawData(TextureAssets.GlowMask[251].Value, vector12.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), color7, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
							item.shader = drawinfo.cWings;
							drawinfo.DrawDataCache.Add(item);
							for (int num14 = drawinfo.drawPlayer.shadowPos.Length - 2; num14 >= 0; num14--)
							{
								Color color8 = color7;
								color8.A = 0;
								color8 *= MathHelper.Lerp(1f, 0f, (float)num14 / 3f);
								color8 *= 0.1f;
								Vector2 vector14 = drawinfo.drawPlayer.shadowPos[num14] - drawinfo.drawPlayer.position;
								for (float num15 = 0f; num15 < 1f; num15 += 0.01f)
								{
									Vector2 vector15 = new Vector2(2f, 0f).RotatedBy((double)(num15 / 0.04f * 6.2831855f), default(Vector2));
									item = new DrawData(TextureAssets.GlowMask[251].Value, vector15 + vector14 * num15 + vector12, new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), color8 * (1f - num15), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
									item.shader = drawinfo.cWings;
									drawinfo.DrawDataCache.Add(item);
								}
							}
							return;
						}
						if (drawinfo.drawPlayer.wings == 29)
						{
							item = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vector12.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color(255, 255, 255, 0) * drawinfo.stealth * (1f - drawinfo.shadow) * 0.5f, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1.06f, drawinfo.playerEffect, 0f);
							item.shader = drawinfo.cWings;
							drawinfo.DrawDataCache.Add(item);
							return;
						}
						if (drawinfo.drawPlayer.wings == 36)
						{
							item = new DrawData(TextureAssets.GlowMask[213].Value, vector12.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color(255, 255, 255, 0) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1.06f, drawinfo.playerEffect, 0f);
							item.shader = drawinfo.cWings;
							drawinfo.DrawDataCache.Add(item);
							Vector2 spinningpoint;
							spinningpoint..ctor(0f, 2f - drawinfo.shadow * 2f);
							for (int m = 0; m < 4; m++)
							{
								item = new DrawData(TextureAssets.GlowMask[213].Value, spinningpoint.RotatedBy((double)(1.5707964f * (float)m), default(Vector2)) + vector12.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color(127, 127, 127, 127) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
								item.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(item);
							}
							return;
						}
						if (drawinfo.drawPlayer.wings == 31)
						{
							Color color9;
							color9..ctor(255, 255, 255, 0);
							color9 = Color.Lerp(Color.HotPink, Color.Crimson, (float)Math.Cos((double)(6.2831855f * ((float)drawinfo.drawPlayer.miscCounter / 100f))) * 0.4f + 0.5f);
							color9.A = 0;
							for (int n = 0; n < 4; n++)
							{
								Vector2 vector16 = new Vector2((float)Math.Cos((double)(6.2831855f * ((float)drawinfo.drawPlayer.miscCounter / 60f))) * 0.5f + 0.5f, 0f).RotatedBy((double)((float)n * 1.5707964f), default(Vector2)) * 1f;
								item = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vector12.Floor() + vector16, new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), color9 * drawinfo.stealth * (1f - drawinfo.shadow) * 0.5f, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
								item.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(item);
							}
							item = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vector12.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), color9 * drawinfo.stealth * (1f - drawinfo.shadow) * 1f, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
							item.shader = drawinfo.cWings;
							drawinfo.DrawDataCache.Add(item);
							return;
						}
						if (drawinfo.drawPlayer.wings == 32)
						{
							item = new DrawData(TextureAssets.GlowMask[183].Value, vector12.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color(255, 255, 255, 0) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1.06f, drawinfo.playerEffect, 0f);
							item.shader = drawinfo.cWings;
							drawinfo.DrawDataCache.Add(item);
						}
					}
					return;
				}
			}
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x0065A108 File Offset: 0x00658308
		public static void DrawPlayer_12_1_BalloonFronts(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.balloonFront <= 0)
			{
				return;
			}
			DrawData item;
			if (ArmorIDs.Balloon.Sets.UsesTorsoFraming[drawinfo.drawPlayer.balloonFront])
			{
				item = new DrawData(TextureAssets.AccBalloon[drawinfo.drawPlayer.balloonFront].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + drawinfo.bodyVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cBalloonFront;
				drawinfo.DrawDataCache.Add(item);
				return;
			}
			int num = (Main.hasFocus && (!Main.ingameOptionsWindow || !Main.autoPause)) ? (DateTime.Now.Millisecond % 800 / 200) : 0;
			Vector2 vector = Main.OffsetsPlayerOffhand[drawinfo.drawPlayer.bodyFrame.Y / 56];
			if (drawinfo.drawPlayer.direction != 1)
			{
				vector.X = (float)drawinfo.drawPlayer.width - vector.X;
			}
			if (drawinfo.drawPlayer.gravDir != 1f)
			{
				vector.Y -= (float)drawinfo.drawPlayer.height;
			}
			Vector2 vector2 = new Vector2(0f, 8f) + new Vector2(0f, 6f);
			Vector2 vector3;
			vector3..ctor((float)((int)(drawinfo.Position.X - Main.screenPosition.X + vector.X)), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + vector.Y * drawinfo.drawPlayer.gravDir)));
			vector3 = drawinfo.Position - Main.screenPosition + vector * new Vector2(1f, drawinfo.drawPlayer.gravDir) + new Vector2(0f, (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height)) + vector2;
			vector3 = vector3.Floor();
			item = new DrawData(TextureAssets.AccBalloon[drawinfo.drawPlayer.balloonFront].Value, vector3, new Rectangle?(new Rectangle(0, TextureAssets.AccBalloon[drawinfo.drawPlayer.balloonFront].Height() / 4 * num, TextureAssets.AccBalloon[drawinfo.drawPlayer.balloonFront].Width(), TextureAssets.AccBalloon[drawinfo.drawPlayer.balloonFront].Height() / 4)), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(26 + drawinfo.drawPlayer.direction * 4), 28f + drawinfo.drawPlayer.gravDir * 6f), 1f, drawinfo.playerEffect, 0f);
			item.shader = drawinfo.cBalloonFront;
			drawinfo.DrawDataCache.Add(item);
		}

		// Token: 0x06004A47 RID: 19015 RVA: 0x0065A4A0 File Offset: 0x006586A0
		public static void DrawPlayer_11_Balloons(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.balloon <= 0)
			{
				return;
			}
			DrawData item;
			if (ArmorIDs.Balloon.Sets.UsesTorsoFraming[drawinfo.drawPlayer.balloon])
			{
				item = new DrawData(TextureAssets.AccBalloon[drawinfo.drawPlayer.balloon].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + drawinfo.bodyVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cBalloon;
				drawinfo.DrawDataCache.Add(item);
				return;
			}
			int num = (Main.hasFocus && (!Main.ingameOptionsWindow || !Main.autoPause)) ? (DateTime.Now.Millisecond % 800 / 200) : 0;
			Vector2 vector = Main.OffsetsPlayerOffhand[drawinfo.drawPlayer.bodyFrame.Y / 56];
			if (drawinfo.drawPlayer.direction != 1)
			{
				vector.X = (float)drawinfo.drawPlayer.width - vector.X;
			}
			if (drawinfo.drawPlayer.gravDir != 1f)
			{
				vector.Y -= (float)drawinfo.drawPlayer.height;
			}
			Vector2 vector2 = new Vector2(0f, 8f) + new Vector2(0f, 6f);
			Vector2 vector3;
			vector3..ctor((float)((int)(drawinfo.Position.X - Main.screenPosition.X + vector.X)), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + vector.Y * drawinfo.drawPlayer.gravDir)));
			vector3 = drawinfo.Position - Main.screenPosition + vector * new Vector2(1f, drawinfo.drawPlayer.gravDir) + new Vector2(0f, (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height)) + vector2;
			vector3 = vector3.Floor();
			item = new DrawData(TextureAssets.AccBalloon[drawinfo.drawPlayer.balloon].Value, vector3, new Rectangle?(new Rectangle(0, TextureAssets.AccBalloon[drawinfo.drawPlayer.balloon].Height() / 4 * num, TextureAssets.AccBalloon[drawinfo.drawPlayer.balloon].Width(), TextureAssets.AccBalloon[drawinfo.drawPlayer.balloon].Height() / 4)), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(26 + drawinfo.drawPlayer.direction * 4), 28f + drawinfo.drawPlayer.gravDir * 6f), 1f, drawinfo.playerEffect, 0f);
			item.shader = drawinfo.cBalloon;
			drawinfo.DrawDataCache.Add(item);
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x0065A838 File Offset: 0x00658A38
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
			if (!drawinfo.hidesBottomSkin && !drawinfo.isBottomOverriden)
			{
				if (drawinfo.isSitting)
				{
					PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.Players[drawinfo.skinVar, 10].Value, drawinfo.colorLegs, 0, false);
					return;
				}
				DrawData item2 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 10].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorLegs, drawinfo.drawPlayer.legRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item2);
			}
		}

		// Token: 0x06004A49 RID: 19017 RVA: 0x0065AB26 File Offset: 0x00658D26
		internal static bool IsBottomOverridden(ref PlayerDrawSet drawinfo)
		{
			return PlayerDrawLayers.ShouldOverrideLegs_CheckPants(ref drawinfo) || PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo);
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x0065AB3D File Offset: 0x00658D3D
		private static bool ShouldOverrideLegs_CheckPants(ref PlayerDrawSet drawinfo)
		{
			return drawinfo.drawPlayer.legs > 0 && ArmorIDs.Legs.Sets.OverridesLegs[drawinfo.drawPlayer.legs];
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x0065AB60 File Offset: 0x00658D60
		private static bool ShouldOverrideLegs_CheckShoes(ref PlayerDrawSet drawinfo)
		{
			int shoe = drawinfo.drawPlayer.shoe;
			return shoe > 0 && ArmorIDs.Shoe.Sets.OverridesLegs[shoe];
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x0065AB88 File Offset: 0x00658D88
		public static void DrawPlayer_12_Skin_Composite(ref PlayerDrawSet drawinfo)
		{
			if (!drawinfo.hidesTopSkin && !drawinfo.drawPlayer.invis)
			{
				Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
				vector.Y += drawinfo.torsoOffset;
				Vector2 vector2 = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
				vector2.Y -= 2f;
				vector += vector2 * (float)(-(float)drawinfo.playerEffect.HasFlag(2).ToDirectionInt());
				float bodyRotation = drawinfo.drawPlayer.bodyRotation;
				Vector2 vector3 = vector;
				Vector2 vector4 = vector;
				Vector2 bodyVect3 = drawinfo.bodyVect;
				Vector2 bodyVect2 = drawinfo.bodyVect;
				Vector2 compositeOffset_BackArm = PlayerDrawLayers.GetCompositeOffset_BackArm(ref drawinfo);
				vector3 += compositeOffset_BackArm;
				bodyVect3 + compositeOffset_BackArm;
				Vector2 compositeOffset_FrontArm = PlayerDrawLayers.GetCompositeOffset_FrontArm(ref drawinfo);
				bodyVect2 += compositeOffset_FrontArm;
				vector4 + compositeOffset_FrontArm;
				DrawData item2;
				if (drawinfo.drawFloatingTube)
				{
					List<DrawData> drawDataCache = drawinfo.DrawDataCache;
					item2 = new DrawData(TextureAssets.Extra[105].Value, vector, new Rectangle?(new Rectangle(0, 0, 40, 56)), drawinfo.floatingTubeColor, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
					{
						shader = drawinfo.cFloatingTube
					};
					drawDataCache.Add(item2);
				}
				List<DrawData> drawDataCache2 = drawinfo.DrawDataCache;
				item2 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 3].Value, vector, new Rectangle?(drawinfo.compTorsoFrame), drawinfo.colorBodySkin, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.skinDyePacked
				};
				drawDataCache2.Add(item2);
			}
			if (!drawinfo.hidesBottomSkin && !drawinfo.drawPlayer.invis && !drawinfo.isBottomOverriden)
			{
				if (drawinfo.isSitting)
				{
					PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.Players[drawinfo.skinVar, 10].Value, drawinfo.colorLegs, drawinfo.skinDyePacked, false);
				}
				else
				{
					DrawData item = new DrawData(TextureAssets.Players[drawinfo.skinVar, 10].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorLegs, drawinfo.drawPlayer.legRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
					{
						shader = drawinfo.skinDyePacked
					};
					drawinfo.DrawDataCache.Add(item);
				}
			}
			PlayerDrawLayers.DrawPlayer_12_SkinComposite_BackArmShirt(ref drawinfo);
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x0065AF90 File Offset: 0x00659190
		public static void DrawPlayer_12_SkinComposite_BackArmShirt(ref PlayerDrawSet drawinfo)
		{
			Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
			Vector2 vector2 = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
			vector2.Y -= 2f;
			vector += vector2 * (float)(-(float)drawinfo.playerEffect.HasFlag(2).ToDirectionInt());
			vector.Y += drawinfo.torsoOffset;
			float bodyRotation = drawinfo.drawPlayer.bodyRotation;
			Vector2 vector3 = vector;
			Vector2 position = vector;
			Vector2 bodyVect = drawinfo.bodyVect;
			Vector2 compositeOffset_BackArm = PlayerDrawLayers.GetCompositeOffset_BackArm(ref drawinfo);
			vector3 += compositeOffset_BackArm;
			position += drawinfo.backShoulderOffset;
			bodyVect += compositeOffset_BackArm;
			float rotation = bodyRotation + drawinfo.compositeBackArmRotation;
			bool flag = !drawinfo.drawPlayer.invis;
			bool flag2 = !drawinfo.drawPlayer.invis;
			bool flag3 = drawinfo.drawPlayer.body > 0;
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
						DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, vector3, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorBodySkin, rotation, bodyVect, 1f, drawinfo.playerEffect, 0f)
						{
							shader = drawinfo.skinDyePacked
						};
						drawDataCache.Add(drawData);
					}
					if (!flag5 && flag4)
					{
						List<DrawData> drawDataCache2 = drawinfo.DrawDataCache;
						DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 5].Value, vector3, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorBodySkin, rotation, bodyVect, 1f, drawinfo.playerEffect, 0f)
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
					Texture2D value = TextureAssets.ArmorBodyComposite[drawinfo.drawPlayer.body].Value;
					DrawData drawData;
					if (!drawinfo.hideCompositeShoulders)
					{
						CompositePlayerDrawContext context = CompositePlayerDrawContext.BackShoulder;
						drawData = new DrawData(value, position, new Rectangle?(drawinfo.compBackShoulderFrame), drawinfo.colorArmorBody, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
						{
							shader = drawinfo.cBody
						};
						PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, context, drawData);
					}
					PlayerDrawLayers.DrawPlayer_12_1_BalloonFronts(ref drawinfo);
					CompositePlayerDrawContext context2 = CompositePlayerDrawContext.BackArm;
					drawData = new DrawData(value, vector3, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorArmorBody, rotation, bodyVect, 1f, drawinfo.playerEffect, 0f)
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
						DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, vector3, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorBodySkin, rotation, bodyVect, 1f, drawinfo.playerEffect, 0f)
						{
							shader = drawinfo.skinDyePacked
						};
						drawDataCache3.Add(drawData);
					}
					if (!flag5 && flag4)
					{
						List<DrawData> drawDataCache4 = drawinfo.DrawDataCache;
						DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 5].Value, vector3, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorBodySkin, rotation, bodyVect, 1f, drawinfo.playerEffect, 0f)
						{
							shader = drawinfo.skinDyePacked
						};
						drawDataCache4.Add(drawData);
					}
				}
				if (!flag3)
				{
					drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 8].Value, vector3, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorUnderShirt, rotation, bodyVect, 1f, drawinfo.playerEffect, 0f));
					PlayerDrawLayers.DrawPlayer_12_1_BalloonFronts(ref drawinfo);
					drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 13].Value, vector3, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorShirt, rotation, bodyVect, 1f, drawinfo.playerEffect, 0f));
				}
			}
			if (drawinfo.drawPlayer.handoff > 0)
			{
				Texture2D value2 = TextureAssets.AccHandsOffComposite[drawinfo.drawPlayer.handoff].Value;
				CompositePlayerDrawContext context3 = CompositePlayerDrawContext.BackArmAccessory;
				DrawData drawData = new DrawData(value2, vector3, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorArmorBody, rotation, bodyVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.cHandOff
				};
				PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, context3, drawData);
			}
			if (drawinfo.drawPlayer.drawingFootball)
			{
				Main.instance.LoadProjectile(861);
				Texture2D value3 = TextureAssets.Projectile[861].Value;
				Rectangle rectangle = value3.Frame(1, 4, 0, 0, 0, 0);
				Vector2 origin = rectangle.Size() / 2f;
				Vector2 position2 = vector3 + new Vector2((float)(drawinfo.drawPlayer.direction * -2), drawinfo.drawPlayer.gravDir * 4f);
				drawinfo.DrawDataCache.Add(new DrawData(value3, position2, new Rectangle?(rectangle), drawinfo.colorArmorBody, bodyRotation + 0.7853982f * (float)drawinfo.drawPlayer.direction, origin, 0.8f, drawinfo.playerEffect, 0f));
			}
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x0065B5D8 File Offset: 0x006597D8
		public static void DrawPlayer_13_Leggings(ref PlayerDrawSet drawinfo)
		{
			Vector2 legsOffset = drawinfo.legsOffset;
			if (drawinfo.drawPlayer.legs == 169)
			{
				return;
			}
			if (drawinfo.isSitting && drawinfo.drawPlayer.legs != 140 && drawinfo.drawPlayer.legs != 217)
			{
				if (drawinfo.drawPlayer.legs > 0 && (!PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo) || drawinfo.drawPlayer.wearsRobe))
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
					return;
				}
			}
			else if (drawinfo.drawPlayer.legs == 140)
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
					Rectangle rectangle;
					rectangle..ctor(18 * flag.ToInt(), num * 26, 16, 24);
					float num2 = 12f;
					if (drawinfo.drawPlayer.bodyFrame.Height != 0)
					{
						num2 = 12f - Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height].Y;
					}
					if (drawinfo.drawPlayer.Directions.Y == -1f)
					{
						num2 -= 6f;
					}
					Vector2 scale;
					scale..ctor(1f, 1f);
					Vector2 vector = drawinfo.Position + drawinfo.drawPlayer.Size * new Vector2(0.5f, 0.5f + 0.5f * drawinfo.drawPlayer.gravDir);
					int direction = drawinfo.drawPlayer.direction;
					Vector2 vec = vector + new Vector2(0f, (0f - num2) * drawinfo.drawPlayer.gravDir) - Main.screenPosition + drawinfo.drawPlayer.legPosition;
					if (drawinfo.isSitting)
					{
						vec.Y += drawinfo.seatYOffset;
					}
					vec += legsOffset;
					vec = vec.Floor();
					DrawData item = new DrawData(value, vec, new Rectangle?(rectangle), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, rectangle.Size() * new Vector2(0.5f, 0.5f - drawinfo.drawPlayer.gravDir * 0.5f), scale, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cLegs;
					drawinfo.DrawDataCache.Add(item);
					return;
				}
			}
			else if (drawinfo.drawPlayer.legs > 0 && (!PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo) || drawinfo.drawPlayer.wearsRobe))
			{
				if (drawinfo.drawPlayer.invis)
				{
					return;
				}
				DrawData item2 = new DrawData(TextureAssets.ArmorLeg[drawinfo.drawPlayer.legs].Value, legsOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				item2.shader = drawinfo.cLegs;
				drawinfo.DrawDataCache.Add(item2);
				if (drawinfo.legsGlowMask == -1)
				{
					return;
				}
				if (drawinfo.legsGlowMask == 274)
				{
					for (int i = 0; i < 2; i++)
					{
						Vector2 position = legsOffset + new Vector2((float)Main.rand.Next(-10, 10) * 0.125f, (float)Main.rand.Next(-10, 10) * 0.125f) + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect;
						item2 = new DrawData(TextureAssets.GlowMask[drawinfo.legsGlowMask].Value, position, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.legsGlowColor, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
						item2.shader = drawinfo.cLegs;
						drawinfo.DrawDataCache.Add(item2);
					}
					return;
				}
				item2 = new DrawData(TextureAssets.GlowMask[drawinfo.legsGlowMask].Value, legsOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.legsGlowColor, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				item2.shader = drawinfo.cLegs;
				drawinfo.DrawDataCache.Add(item2);
				return;
			}
			else if (!drawinfo.drawPlayer.invis && !PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo))
			{
				DrawData item3 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 11].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorPants, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item3);
				item3 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 12].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorShoes, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item3);
			}
		}

		// Token: 0x06004A4F RID: 19023 RVA: 0x0065BF4C File Offset: 0x0065A14C
		private static void DrawSittingLongCoats(ref PlayerDrawSet drawinfo, int specialLegCoat, Texture2D textureToDraw, Color matchingColor, int shaderIndex = 0, bool glowmask = false)
		{
			Vector2 legsOffset = drawinfo.legsOffset;
			Vector2 position = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect;
			Rectangle legFrame = drawinfo.drawPlayer.legFrame;
			position += legsOffset;
			position.X += (float)(2 * drawinfo.drawPlayer.direction);
			legFrame.Y = legFrame.Height * 5;
			if (specialLegCoat == 160 || specialLegCoat == 173)
			{
				legFrame = drawinfo.drawPlayer.legFrame;
			}
			DrawData item = new DrawData(textureToDraw, position, new Rectangle?(legFrame), matchingColor, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
			item.shader = shaderIndex;
			drawinfo.DrawDataCache.Add(item);
		}

		// Token: 0x06004A50 RID: 19024 RVA: 0x0065C098 File Offset: 0x0065A298
		private static void DrawSittingLegs(ref PlayerDrawSet drawinfo, Texture2D textureToDraw, Color matchingColor, int shaderIndex = 0, bool glowmask = false)
		{
			Vector2 legsOffset = drawinfo.legsOffset;
			Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect;
			Rectangle legFrame = drawinfo.drawPlayer.legFrame;
			vector.Y -= 2f;
			vector.Y += drawinfo.seatYOffset;
			vector += legsOffset;
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
				vector.Y += 4f;
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
						goto IL_2A6;
					}
					if (legs != 143)
					{
						goto IL_2A6;
					}
				}
			}
			else if (legs <= 210)
			{
				if (legs - 193 > 1)
				{
					if (legs != 210)
					{
						goto IL_2A6;
					}
					if (glowmask)
					{
						Vector2 vector2;
						vector2..ctor((float)Main.rand.Next(-10, 10) * 0.125f, (float)Main.rand.Next(-10, 10) * 0.125f);
						vector += vector2;
						goto IL_2A6;
					}
					goto IL_2A6;
				}
				else
				{
					if (drawinfo.drawPlayer.body == 218)
					{
						num = -2;
						num7 = 2;
						vector.Y += 2f;
						goto IL_2A6;
					}
					goto IL_2A6;
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
					vector.Y += 2f;
					goto IL_2A6;
				}
				if (legs != 226)
				{
					goto IL_2A6;
				}
			}
			num = 0;
			num4 = 0;
			num2 = 6;
			vector.Y += 4f;
			legFrame.Y = legFrame.Height * 5;
			IL_2A6:
			for (int num8 = num3; num8 >= 0; num8--)
			{
				Vector2 position = vector + new Vector2((float)num, 2f) * new Vector2((float)drawinfo.drawPlayer.direction, 1f);
				Rectangle value = legFrame;
				value.Y += num8 * 2;
				value.Y += num2;
				value.Height -= num2;
				value.Height -= num8 * 2;
				if (num8 != num3)
				{
					value.Height = 2;
				}
				position.X += (float)(drawinfo.drawPlayer.direction * num4 * num8 + num6 * drawinfo.drawPlayer.direction);
				if (num8 != 0)
				{
					position.X += (float)(num7 * drawinfo.drawPlayer.direction);
				}
				position.Y += (float)num2;
				position.Y += (float)num5;
				DrawData item = new DrawData(textureToDraw, position, new Rectangle?(value), matchingColor, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = shaderIndex;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06004A51 RID: 19025 RVA: 0x0065C488 File Offset: 0x0065A688
		public static void DrawPlayer_14_Shoes(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.shoe <= 0 || PlayerDrawLayers.ShouldOverrideLegs_CheckPants(ref drawinfo))
			{
				return;
			}
			int num = drawinfo.cShoe;
			if (drawinfo.drawPlayer.shoe == 22 || drawinfo.drawPlayer.shoe == 23)
			{
				num = drawinfo.cFlameWaker;
			}
			if (drawinfo.isSitting)
			{
				PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.AccShoes[drawinfo.drawPlayer.shoe].Value, drawinfo.colorArmorLegs, num, false);
				return;
			}
			DrawData item = new DrawData(TextureAssets.AccShoes[drawinfo.drawPlayer.shoe].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
			item.shader = num;
			drawinfo.DrawDataCache.Add(item);
			if (drawinfo.drawPlayer.shoe == 25 || drawinfo.drawPlayer.shoe == 26)
			{
				PlayerDrawLayers.DrawPlayer_14_2_GlassSlipperSparkles(ref drawinfo);
			}
		}

		// Token: 0x06004A52 RID: 19026 RVA: 0x0065C62C File Offset: 0x0065A82C
		public static void DrawPlayer_14_2_GlassSlipperSparkles(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.headOnlyRender)
			{
				return;
			}
			if (drawinfo.shadow == 0f)
			{
				if (Main.rand.Next(60) == 0)
				{
					Rectangle r = Utils.CenteredRectangle(drawinfo.Position + drawinfo.drawPlayer.Size / 2f + new Vector2(0f, drawinfo.drawPlayer.gravDir * 16f), new Vector2(20f, 8f));
					int num = Dust.NewDust(r.TopLeft(), r.Width, r.Height, 204, 0f, 0f, 150, default(Color), 0.3f);
					Main.dust[num].fadeIn = 1f;
					Main.dust[num].velocity *= 0.1f;
					Main.dust[num].noLight = true;
					Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(drawinfo.drawPlayer.cShoe, drawinfo.drawPlayer);
					drawinfo.DustCache.Add(num);
				}
				if (drawinfo.drawPlayer.velocity.X != 0f && Main.rand.Next(10) == 0)
				{
					Rectangle r2 = Utils.CenteredRectangle(drawinfo.Position + drawinfo.drawPlayer.Size / 2f + new Vector2((float)(drawinfo.drawPlayer.direction * -2), drawinfo.drawPlayer.gravDir * 16f), new Vector2(6f, 8f));
					int num2 = Dust.NewDust(r2.TopLeft(), r2.Width, r2.Height, 204, 0f, 0f, 150, default(Color), 0.3f);
					Main.dust[num2].fadeIn = 1f;
					Main.dust[num2].velocity *= 0.1f;
					Main.dust[num2].noLight = true;
					Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(drawinfo.drawPlayer.cShoe, drawinfo.drawPlayer);
					drawinfo.DustCache.Add(num2);
				}
			}
		}

		// Token: 0x06004A53 RID: 19027 RVA: 0x0065C898 File Offset: 0x0065AA98
		public static void DrawPlayer_15_SkinLongCoat(ref PlayerDrawSet drawinfo)
		{
			if ((drawinfo.skinVar == 3 || drawinfo.skinVar == 8 || drawinfo.skinVar == 7) && drawinfo.drawPlayer.body <= 0 && !drawinfo.drawPlayer.invis)
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

		// Token: 0x06004A54 RID: 19028 RVA: 0x0065CA0C File Offset: 0x0065AC0C
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
						else
						{
							num = ((!drawinfo.drawPlayer.Male) ? 176 : 175);
						}
					}
					else
					{
						num = ((!drawinfo.drawPlayer.Male) ? 172 : 171);
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
						num = ((!drawinfo.drawPlayer.Male) ? 177 : 178);
						break;
					case 211:
						num = ((!drawinfo.drawPlayer.Male) ? 181 : 182);
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
				else
				{
					num = ((!drawinfo.drawPlayer.Male) ? 200 : 201);
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

		// Token: 0x06004A55 RID: 19029 RVA: 0x0065CDA4 File Offset: 0x0065AFA4
		public static void DrawPlayer_17_Torso(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.usesCompositeTorso)
			{
				PlayerDrawLayers.DrawPlayer_17_TorsoComposite(ref drawinfo);
				return;
			}
			if (drawinfo.drawPlayer.body > 0)
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
					Texture2D texture = drawinfo.drawPlayer.Male ? TextureAssets.ArmorBody[drawinfo.drawPlayer.body].Value : TextureAssets.FemaleBody[drawinfo.drawPlayer.body].Value;
					DrawData item = new DrawData(texture, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2)) + num), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
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
					DrawData item2 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 5].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
					{
						shader = drawinfo.skinDyePacked
					};
					drawinfo.DrawDataCache.Add(item2);
					return;
				}
			}
			else if (!drawinfo.drawPlayer.invis)
			{
				DrawData item3;
				if (!drawinfo.drawPlayer.Male)
				{
					item3 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 4].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorUnderShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(item3);
					item3 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(item3);
				}
				else
				{
					item3 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 4].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorUnderShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(item3);
					item3 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(item3);
				}
				item3 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 5].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.skinDyePacked
				};
				drawinfo.DrawDataCache.Add(item3);
			}
		}

		// Token: 0x06004A56 RID: 19030 RVA: 0x0065D7C8 File Offset: 0x0065B9C8
		public static void DrawPlayer_17_TorsoComposite(ref PlayerDrawSet drawinfo)
		{
			Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
			Vector2 vector2 = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
			vector2.Y -= 2f;
			vector += vector2 * (float)(-(float)drawinfo.playerEffect.HasFlag(2).ToDirectionInt());
			float bodyRotation = drawinfo.drawPlayer.bodyRotation;
			Vector2 vector3 = vector;
			Vector2 bodyVect = drawinfo.bodyVect;
			Vector2 compositeOffset_BackArm = PlayerDrawLayers.GetCompositeOffset_BackArm(ref drawinfo);
			vector3 + compositeOffset_BackArm;
			bodyVect += compositeOffset_BackArm;
			if (drawinfo.drawPlayer.body > 0)
			{
				if (!drawinfo.drawPlayer.invis || PlayerDrawLayers.IsArmorDrawnWhenInvisible(drawinfo.drawPlayer.body))
				{
					Texture2D value = TextureAssets.ArmorBodyComposite[drawinfo.drawPlayer.body].Value;
					CompositePlayerDrawContext context = CompositePlayerDrawContext.Torso;
					DrawData drawData = new DrawData(value, vector, new Rectangle?(drawinfo.compTorsoFrame), drawinfo.colorArmorBody, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
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

		// Token: 0x06004A57 RID: 19031 RVA: 0x0065DB70 File Offset: 0x0065BD70
		public static void DrawPlayer_18_OffhandAcc(ref PlayerDrawSet drawinfo)
		{
			if (!drawinfo.usesCompositeBackHandAcc && drawinfo.drawPlayer.handoff > 0)
			{
				DrawData item = new DrawData(TextureAssets.AccHandsOff[drawinfo.drawPlayer.handoff].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cHandOff;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06004A58 RID: 19032 RVA: 0x0065DCC4 File Offset: 0x0065BEC4
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

		// Token: 0x06004A59 RID: 19033 RVA: 0x0065DE38 File Offset: 0x0065C038
		public static void DrawPlayer_19_WaistAcc(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.waist > 0)
			{
				Rectangle value = drawinfo.drawPlayer.legFrame;
				if (ArmorIDs.Waist.Sets.UsesTorsoFraming[drawinfo.drawPlayer.waist])
				{
					value = drawinfo.drawPlayer.bodyFrame;
				}
				DrawData item = new DrawData(TextureAssets.AccWaist[drawinfo.drawPlayer.waist].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(value), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cWaist;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x0065DF7C File Offset: 0x0065C17C
		public static void DrawPlayer_20_NeckAcc(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.neck > 0)
			{
				DrawData item = new DrawData(TextureAssets.AccNeck[drawinfo.drawPlayer.neck].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cNeck;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06004A5B RID: 19035 RVA: 0x0065E0C4 File Offset: 0x0065C2C4
		public static void DrawPlayer_21_Head(ref PlayerDrawSet drawinfo)
		{
			Vector2 helmetOffset = drawinfo.helmetOffset;
			PlayerDrawLayers.DrawPlayer_21_Head_TheFace(ref drawinfo);
			bool flag = drawinfo.drawPlayer.head >= 0 && ArmorIDs.Head.Sets.IsTallHat[drawinfo.drawPlayer.head];
			bool flag2 = drawinfo.drawPlayer.head == 28;
			bool flag3 = drawinfo.drawPlayer.head == 39 || drawinfo.drawPlayer.head == 38;
			Vector2 vector;
			vector..ctor((float)(-(float)drawinfo.drawPlayer.bodyFrame.Width / 2 + drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height + 4));
			Vector2 position = (drawinfo.Position - Main.screenPosition + vector).Floor() + drawinfo.drawPlayer.headPosition + drawinfo.headVect;
			if (drawinfo.playerEffect.HasFlag(2))
			{
				int num = drawinfo.drawPlayer.bodyFrame.Height - drawinfo.hairFrontFrame.Height;
				position.Y += (float)num;
			}
			position += drawinfo.hairOffset;
			if (drawinfo.fullHair)
			{
				Color color = drawinfo.colorArmorHead;
				int shader = drawinfo.cHead;
				if (ArmorIDs.Head.Sets.UseSkinColor[drawinfo.drawPlayer.head])
				{
					color = ((!drawinfo.drawPlayer.isDisplayDollOrInanimate) ? drawinfo.colorHead : drawinfo.colorDisplayDollSkin);
					shader = drawinfo.skinDyePacked;
				}
				DrawData item = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = shader;
				drawinfo.DrawDataCache.Add(item);
				if (!drawinfo.drawPlayer.invis)
				{
					item = new DrawData(TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, position, new Rectangle?(drawinfo.hairFrontFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.hairDyePacked;
					drawinfo.DrawDataCache.Add(item);
				}
			}
			if (drawinfo.hatHair && !drawinfo.drawPlayer.invis)
			{
				DrawData item2 = new DrawData(TextureAssets.PlayerHairAlt[drawinfo.drawPlayer.hair].Value, position, new Rectangle?(drawinfo.hairFrontFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item2.shader = drawinfo.hairDyePacked;
				drawinfo.DrawDataCache.Add(item2);
			}
			if (drawinfo.drawPlayer.head == 270)
			{
				Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
				bodyFrame.Width += 2;
				DrawData item3 = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item3.shader = drawinfo.cHead;
				drawinfo.DrawDataCache.Add(item3);
				item3 = new DrawData(TextureAssets.GlowMask[drawinfo.headGlowMask].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item3.shader = drawinfo.cHead;
				drawinfo.DrawDataCache.Add(item3);
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
					color2 = ((!drawinfo.drawPlayer.isDisplayDollOrInanimate) ? drawinfo.colorHead : drawinfo.colorDisplayDollSkin);
					shader2 = drawinfo.skinDyePacked;
				}
				DrawData item4 = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame2), color2, drawinfo.drawPlayer.headRotation, headVect, 1f, drawinfo.playerEffect, 0f);
				item4.shader = shader2;
				drawinfo.DrawDataCache.Add(item4);
			}
			else if (drawinfo.drawPlayer.head == 259)
			{
				int verticalFrames = 27;
				Texture2D value = TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value;
				Rectangle rectangle = value.Frame(1, verticalFrames, 0, drawinfo.drawPlayer.rabbitOrderFrame.DisplayFrame, 0, 0);
				Vector2 origin = rectangle.Size() / 2f;
				int num2 = drawinfo.drawPlayer.babyBird.ToInt();
				Vector2 vector2 = PlayerDrawLayers.DrawPlayer_21_Head_GetSpecialHatDrawPosition(ref drawinfo, ref helmetOffset, new Vector2((float)(1 + num2 * 2), (float)(-26 + drawinfo.drawPlayer.babyBird.ToInt() * -6)));
				int hatStacks3 = PlayerDrawLayers.GetHatStacks(ref drawinfo, 4955);
				float num3 = 0.05235988f;
				float num4 = num3 * drawinfo.drawPlayer.position.X % 6.2831855f;
				for (int num5 = hatStacks3 - 1; num5 >= 0; num5--)
				{
					float x = Vector2.UnitY.RotatedBy((double)(num4 + num3 * (float)num5), default(Vector2)).X * ((float)num5 / 30f) * 2f - (float)(num5 * 2 * drawinfo.drawPlayer.direction);
					DrawData item5 = new DrawData(value, vector2 + new Vector2(x, (float)(num5 * -14) * drawinfo.drawPlayer.gravDir), new Rectangle?(rectangle), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, origin, 1f, drawinfo.playerEffect, 0f);
					item5.shader = drawinfo.cHead;
					drawinfo.DrawDataCache.Add(item5);
				}
				if (!drawinfo.drawPlayer.invis)
				{
					DrawData item6 = new DrawData(TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, position, new Rectangle?(drawinfo.hairFrontFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					item6.shader = drawinfo.hairDyePacked;
					drawinfo.DrawDataCache.Add(item6);
				}
			}
			else if (drawinfo.drawPlayer.head > 0 && !flag2)
			{
				if (!drawinfo.drawPlayer.invis || !flag3)
				{
					if (drawinfo.drawPlayer.head == 13)
					{
						int hatStacks2 = PlayerDrawLayers.GetHatStacks(ref drawinfo, 205);
						float num6 = 0.05235988f;
						float num7 = num6 * drawinfo.drawPlayer.position.X % 6.2831855f;
						for (int i = 0; i < hatStacks2; i++)
						{
							float num8 = Vector2.UnitY.RotatedBy((double)(num7 + num6 * (float)i), default(Vector2)).X * ((float)i / 30f) * 2f;
							DrawData item7 = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))) + num8, (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f - (float)(4 * i)))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
							item7.shader = drawinfo.cHead;
							drawinfo.DrawDataCache.Add(item7);
						}
					}
					else if (drawinfo.drawPlayer.head == 265)
					{
						int verticalFrames2 = 6;
						Texture2D value2 = TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value;
						Rectangle rectangle2 = value2.Frame(1, verticalFrames2, 0, drawinfo.drawPlayer.rabbitOrderFrame.DisplayFrame, 0, 0);
						Vector2 origin2 = rectangle2.Size() / 2f;
						Vector2 vector3 = PlayerDrawLayers.DrawPlayer_21_Head_GetSpecialHatDrawPosition(ref drawinfo, ref helmetOffset, new Vector2(0f, -9f));
						int hatStacks4 = PlayerDrawLayers.GetHatStacks(ref drawinfo, 5004);
						float num9 = 0.05235988f;
						float num10 = num9 * drawinfo.drawPlayer.position.X % 6.2831855f;
						int num11 = hatStacks4 * 4 + 2;
						int num12 = 0;
						bool flag4 = (Main.GlobalTimeWrappedHourly + 180f) % 600f < 60f;
						for (int num13 = num11 - 1; num13 >= 0; num13--)
						{
							int num14 = 0;
							if (num13 == num11 - 1)
							{
								rectangle2.Y = 0;
								num14 = 2;
							}
							else if (num13 == 0)
							{
								rectangle2.Y = rectangle2.Height * 5;
							}
							else
							{
								rectangle2.Y = rectangle2.Height * (num12++ % 4 + 1);
							}
							if (rectangle2.Y != rectangle2.Height * 3 || !flag4)
							{
								float x2 = Vector2.UnitY.RotatedBy((double)(num10 + num9 * (float)num13), default(Vector2)).X * ((float)num13 / 10f) * 4f - (float)num13 * 0.1f * (float)drawinfo.drawPlayer.direction;
								DrawData item8 = new DrawData(value2, vector3 + new Vector2(x2, (float)(num13 * -4 + num14) * drawinfo.drawPlayer.gravDir), new Rectangle?(rectangle2), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, origin2, 1f, drawinfo.playerEffect, 0f);
								item8.shader = drawinfo.cHead;
								drawinfo.DrawDataCache.Add(item8);
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
							color3 = ((!drawinfo.drawPlayer.isDisplayDollOrInanimate) ? drawinfo.colorHead : drawinfo.colorDisplayDollSkin);
							shader3 = drawinfo.skinDyePacked;
						}
						DrawData item9 = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame3), color3, drawinfo.drawPlayer.headRotation, headVect2, 1f, drawinfo.playerEffect, 0f);
						item9.shader = shader3;
						drawinfo.DrawDataCache.Add(item9);
						if (drawinfo.headGlowMask != -1)
						{
							if (drawinfo.headGlowMask == 309)
							{
								int num15 = PlayerDrawLayers.DrawPlayer_Head_GetTVScreen(drawinfo.drawPlayer);
								if (num15 != 0)
								{
									int num16 = 0;
									num16 += drawinfo.drawPlayer.bodyFrame.Y / 56;
									if (num16 >= Main.OffsetsPlayerHeadgear.Length)
									{
										num16 = 0;
									}
									Vector2 vector4 = Main.OffsetsPlayerHeadgear[num16];
									vector4.Y -= 2f;
									vector4 *= (float)(-(float)drawinfo.playerEffect.HasFlag(2).ToDirectionInt());
									Texture2D value3 = TextureAssets.GlowMask[drawinfo.headGlowMask].Value;
									int frameY = drawinfo.drawPlayer.miscCounter % 20 / 5;
									if (num15 == 5)
									{
										frameY = 0;
										if (drawinfo.drawPlayer.eyeHelper.EyeFrameToShow > 0)
										{
											frameY = 2;
										}
									}
									Rectangle value4 = value3.Frame(6, 4, num15, frameY, -2, 0);
									item9 = new DrawData(value3, vector4 + helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(value4), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
									item9.shader = drawinfo.cHead;
									drawinfo.DrawDataCache.Add(item9);
								}
							}
							else if (drawinfo.headGlowMask == 273)
							{
								for (int j = 0; j < 2; j++)
								{
									Vector2 position2 = new Vector2((float)Main.rand.Next(-10, 10) * 0.125f, (float)Main.rand.Next(-10, 10) * 0.125f) + helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect;
									item9 = new DrawData(TextureAssets.GlowMask[drawinfo.headGlowMask].Value, position2, new Rectangle?(bodyFrame3), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, headVect2, 1f, drawinfo.playerEffect, 0f);
									item9.shader = drawinfo.cHead;
									drawinfo.DrawDataCache.Add(item9);
								}
							}
							else
							{
								item9 = new DrawData(TextureAssets.GlowMask[drawinfo.headGlowMask].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame3), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, headVect2, 1f, drawinfo.playerEffect, 0f);
								item9.shader = drawinfo.cHead;
								drawinfo.DrawDataCache.Add(item9);
							}
						}
						if (drawinfo.drawPlayer.head == 211)
						{
							Color color4;
							color4..ctor(100, 100, 100, 0);
							ulong seed = (ulong)((long)(drawinfo.drawPlayer.miscCounter / 4 + 100));
							int num17 = 4;
							for (int k = 0; k < num17; k++)
							{
								float x3 = (float)Utils.RandomInt(ref seed, -10, 11) * 0.2f;
								float y = (float)Utils.RandomInt(ref seed, -14, 1) * 0.15f;
								item9 = new DrawData(TextureAssets.GlowMask[241].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + new Vector2(x3, y), new Rectangle?(drawinfo.drawPlayer.bodyFrame), color4, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
								item9.shader = drawinfo.cHead;
								drawinfo.DrawDataCache.Add(item9);
							}
						}
					}
				}
			}
			else if (!drawinfo.drawPlayer.invis && (drawinfo.drawPlayer.face < 0 || !ArmorIDs.Face.Sets.PreventHairDraw[drawinfo.drawPlayer.face]))
			{
				DrawData item10 = new DrawData(TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, position, new Rectangle?(drawinfo.hairFrontFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item10.shader = drawinfo.hairDyePacked;
				drawinfo.DrawDataCache.Add(item10);
			}
			if (drawinfo.drawPlayer.beard > 0 && (drawinfo.drawPlayer.head < 0 || !ArmorIDs.Head.Sets.PreventBeardDraw[drawinfo.drawPlayer.head]))
			{
				Vector2 beardDrawOffsetFromHelmet = drawinfo.drawPlayer.GetBeardDrawOffsetFromHelmet();
				Color color5 = drawinfo.colorArmorHead;
				if (ArmorIDs.Beard.Sets.UseHairColor[drawinfo.drawPlayer.beard])
				{
					color5 = drawinfo.colorHair;
				}
				DrawData item11 = new DrawData(TextureAssets.AccBeard[drawinfo.drawPlayer.beard].Value, beardDrawOffsetFromHelmet + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), color5, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item11.shader = drawinfo.cBeard;
				drawinfo.DrawDataCache.Add(item11);
			}
			if (drawinfo.drawPlayer.head == 205)
			{
				DrawData item12 = new DrawData(TextureAssets.Extra[77].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.skinDyePacked
				};
				drawinfo.DrawDataCache.Add(item12);
			}
			if (drawinfo.drawPlayer.head == 214 && !drawinfo.drawPlayer.invis)
			{
				Rectangle bodyFrame4 = drawinfo.drawPlayer.bodyFrame;
				bodyFrame4.Y = 0;
				float num18 = (float)drawinfo.drawPlayer.miscCounter / 300f;
				Color color6;
				color6..ctor(0, 0, 0, 0);
				float num19 = 0.8f;
				float num20 = 0.9f;
				if (num18 >= num19)
				{
					color6 = Color.Lerp(Color.Transparent, new Color(200, 200, 200, 0), Utils.GetLerpValue(num19, num20, num18, true));
				}
				if (num18 >= num20)
				{
					color6 = Color.Lerp(Color.Transparent, new Color(200, 200, 200, 0), Utils.GetLerpValue(1f, num20, num18, true));
				}
				color6 *= drawinfo.stealth * (1f - drawinfo.shadow);
				DrawData item13 = new DrawData(TextureAssets.Extra[90].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect - Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height], new Rectangle?(bodyFrame4), color6, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item13);
			}
			if (drawinfo.drawPlayer.head == 137)
			{
				DrawData item14 = new DrawData(TextureAssets.JackHat.Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), new Color(255, 255, 255, 255), drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item14);
				for (int l = 0; l < 7; l++)
				{
					Color color7;
					color7..ctor(110 - l * 10, 110 - l * 10, 110 - l * 10, 110 - l * 10);
					Vector2 vector5;
					vector5..ctor((float)Main.rand.Next(-10, 11) * 0.2f, (float)Main.rand.Next(-10, 11) * 0.2f);
					vector5.X = drawinfo.drawPlayer.itemFlamePos[l].X;
					vector5.Y = drawinfo.drawPlayer.itemFlamePos[l].Y;
					vector5 *= 0.5f;
					item14 = new DrawData(TextureAssets.JackHat.Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + vector5, new Rectangle?(drawinfo.drawPlayer.bodyFrame), color7, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(item14);
				}
			}
		}

		// Token: 0x06004A5C RID: 19036 RVA: 0x0065FDA0 File Offset: 0x0065DFA0
		public static void DrawPlayer_21_2_FinchNest(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.babyBird)
			{
				Rectangle bodyFrame5 = drawinfo.drawPlayer.bodyFrame;
				bodyFrame5.Y = 0;
				Vector2 vector6 = Vector2.Zero;
				Color color8 = drawinfo.colorArmorHead;
				if (drawinfo.drawPlayer.mount.Active && drawinfo.drawPlayer.mount.Type == 52)
				{
					Vector2 mountedCenter = drawinfo.drawPlayer.MountedCenter;
					color8 = drawinfo.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)mountedCenter.X / 16, (int)mountedCenter.Y / 16, Color.White), drawinfo.shadow);
					vector6 = new Vector2(0f, 6f) * drawinfo.drawPlayer.Directions;
				}
				DrawData item = new DrawData(TextureAssets.Extra[100].Value, vector6 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height] * drawinfo.drawPlayer.gravDir, new Rectangle?(bodyFrame5), color8, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06004A5D RID: 19037 RVA: 0x0065FF8C File Offset: 0x0065E18C
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

		// Token: 0x06004A5E RID: 19038 RVA: 0x0065FFF8 File Offset: 0x0065E1F8
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

		// Token: 0x06004A5F RID: 19039 RVA: 0x006600B0 File Offset: 0x0065E2B0
		private static Vector2 DrawPlayer_21_Head_GetSpecialHatDrawPosition(ref PlayerDrawSet drawinfo, ref Vector2 helmetOffset, Vector2 hatOffset)
		{
			Vector2 vector = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height] * drawinfo.drawPlayer.Directions;
			Vector2 vec = drawinfo.Position - Main.screenPosition + helmetOffset + new Vector2((float)(-(float)drawinfo.drawPlayer.bodyFrame.Width / 2 + drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height + 4)) + hatOffset * drawinfo.drawPlayer.Directions + vector;
			vec = vec.Floor();
			vec += drawinfo.drawPlayer.headPosition + drawinfo.headVect;
			if (drawinfo.drawPlayer.gravDir == -1f)
			{
				vec.Y += 12f;
			}
			return vec.Floor();
		}

		// Token: 0x06004A60 RID: 19040 RVA: 0x006601C8 File Offset: 0x0065E3C8
		private static void DrawPlayer_21_Head_TheFace(ref PlayerDrawSet drawinfo)
		{
			bool flag = drawinfo.drawPlayer.head > 0 && !ArmorIDs.Head.Sets.DrawHead[drawinfo.drawPlayer.head];
			if (flag || drawinfo.drawPlayer.faceHead <= 0)
			{
				if (!drawinfo.drawPlayer.invis && !flag)
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
						vector *= (float)(-(float)drawinfo.playerEffect.HasFlag(2).ToDirectionInt());
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
					if (drawinfo.drawPlayer.face > 0 && ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[drawinfo.drawPlayer.face])
					{
						item = new DrawData(TextureAssets.AccFace[drawinfo.drawPlayer.face].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cFace;
						drawinfo.DrawDataCache.Add(item);
					}
				}
				return;
			}
			Vector2 faceHeadOffsetFromHelmet = drawinfo.drawPlayer.GetFaceHeadOffsetFromHelmet();
			DrawData item2 = new DrawData(TextureAssets.AccFace[drawinfo.drawPlayer.faceHead].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + faceHeadOffsetFromHelmet, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
			item2.shader = drawinfo.cFaceHead;
			drawinfo.DrawDataCache.Add(item2);
			if (drawinfo.drawPlayer.face <= 0 || !ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[drawinfo.drawPlayer.face])
			{
				return;
			}
			float num = 0f;
			if (drawinfo.drawPlayer.face == 5 && drawinfo.drawPlayer.faceHead - 10 <= 3)
			{
				num = (float)(2 * drawinfo.drawPlayer.direction);
			}
			item2 = new DrawData(TextureAssets.AccFace[drawinfo.drawPlayer.face].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))) + num, (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
			item2.shader = drawinfo.cFace;
			drawinfo.DrawDataCache.Add(item2);
		}

		// Token: 0x06004A61 RID: 19041 RVA: 0x00660B74 File Offset: 0x0065ED74
		public static void DrawPlayer_21_1_Magiluminescence(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.shadow == 0f && drawinfo.drawPlayer.neck == 11 && !drawinfo.hideEntirePlayer)
			{
				Color colorArmorBody = drawinfo.colorArmorBody;
				Color value;
				value..ctor(140, 140, 35, 12);
				float amount = (float)(colorArmorBody.R + colorArmorBody.G + colorArmorBody.B) / 3f / 255f;
				value = Color.Lerp(value, Color.Transparent, amount);
				if (!(value == Color.Transparent))
				{
					DrawData item = new DrawData(TextureAssets.GlowMask[310].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), value, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cNeck;
					drawinfo.DrawDataCache.Add(item);
				}
			}
		}

		// Token: 0x06004A62 RID: 19042 RVA: 0x00660D2C File Offset: 0x0065EF2C
		public static void DrawPlayer_22_FaceAcc(ref PlayerDrawSet drawinfo)
		{
			Vector2 vector = Vector2.Zero;
			if (drawinfo.drawPlayer.mount.Active && drawinfo.drawPlayer.mount.Type == 52)
			{
				vector..ctor(28f, -2f);
			}
			vector *= drawinfo.drawPlayer.Directions;
			if (drawinfo.drawPlayer.face > 0 && !ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[drawinfo.drawPlayer.face])
			{
				Vector2 vector2 = Vector2.Zero;
				if (drawinfo.drawPlayer.face == 19)
				{
					vector2 = new Vector2(0f, -6f) * drawinfo.drawPlayer.Directions;
				}
				DrawData item = new DrawData(TextureAssets.AccFace[drawinfo.drawPlayer.face].Value, vector2 + vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cFace;
				drawinfo.DrawDataCache.Add(item);
			}
			if (drawinfo.drawPlayer.faceFlower > 0)
			{
				DrawData item2 = new DrawData(TextureAssets.AccFace[drawinfo.drawPlayer.faceFlower].Value, vector + drawinfo.helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item2.shader = drawinfo.cFaceFlower;
				drawinfo.DrawDataCache.Add(item2);
			}
			if (drawinfo.drawUnicornHorn)
			{
				DrawData item3 = new DrawData(TextureAssets.Extra[143].Value, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item3.shader = drawinfo.cUnicornHorn;
				drawinfo.DrawDataCache.Add(item3);
			}
			if (drawinfo.drawAngelHalo)
			{
				Color immuneAlphaPure = drawinfo.drawPlayer.GetImmuneAlphaPure(new Color(200, 200, 200, 150), drawinfo.shadow);
				immuneAlphaPure *= drawinfo.drawPlayer.stealth;
				Main.instance.LoadAccFace(7);
				DrawData item4 = new DrawData(TextureAssets.AccFace[7].Value, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), immuneAlphaPure, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				item4.shader = drawinfo.cAngelHalo;
				drawinfo.DrawDataCache.Add(item4);
			}
		}

		// Token: 0x06004A63 RID: 19043 RVA: 0x00661284 File Offset: 0x0065F484
		public static void DrawTiedBalloons(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.mount.Type == 34)
			{
				Texture2D value = TextureAssets.Extra[141].Value;
				Vector2 vector;
				vector..ctor(0f, 4f);
				Color colorMount = drawinfo.colorMount;
				int frameY = (int)(Main.GlobalTimeWrappedHourly * 3f + drawinfo.drawPlayer.position.X / 50f) % 3;
				Rectangle value2 = value.Frame(1, 3, 0, frameY, 0, 0);
				Vector2 origin;
				origin..ctor((float)(value2.Width / 2), (float)value2.Height);
				float rotation = (0f - drawinfo.drawPlayer.velocity.X) * 0.1f - drawinfo.drawPlayer.fullRotation;
				DrawData item = new DrawData(value, drawinfo.drawPlayer.MountedCenter + vector - Main.screenPosition, new Rectangle?(value2), colorMount, rotation, origin, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06004A64 RID: 19044 RVA: 0x00661394 File Offset: 0x0065F594
		public static void DrawStarboardRainbowTrail(ref PlayerDrawSet drawinfo, Vector2 commonWingPosPreFloor, Vector2 dirsVec)
		{
			if (drawinfo.shadow != 0f)
			{
				return;
			}
			int num = Math.Min(drawinfo.drawPlayer.availableAdvancedShadowsCount - 1, 30);
			float num2 = 0f;
			for (int num3 = num; num3 > 0; num3--)
			{
				EntityShadowInfo advancedShadow = drawinfo.drawPlayer.GetAdvancedShadow(num3);
				float num10 = num2;
				Vector2 position = drawinfo.drawPlayer.GetAdvancedShadow(num3 - 1).Position;
				num2 = num10 + Vector2.Distance(advancedShadow.Position, position);
			}
			float num4 = MathHelper.Clamp(num2 / 160f, 0f, 1f);
			Main.instance.LoadProjectile(250);
			Texture2D value = TextureAssets.Projectile[250].Value;
			float x = 1.7f;
			Vector2 origin;
			origin..ctor((float)(value.Width / 2), (float)(value.Height / 2));
			new Vector2((float)drawinfo.drawPlayer.width, (float)drawinfo.drawPlayer.height) / 2f;
			Color white = Color.White;
			white.A = 64;
			Vector2 vector2 = drawinfo.drawPlayer.DefaultSize * new Vector2(0.5f, 1f) + new Vector2(0f, -4f);
			if (dirsVec.Y < 0f)
			{
				vector2 = drawinfo.drawPlayer.DefaultSize * new Vector2(0.5f, 0f) + new Vector2(0f, 4f);
			}
			for (int num5 = num; num5 > 0; num5--)
			{
				EntityShadowInfo advancedShadow2 = drawinfo.drawPlayer.GetAdvancedShadow(num5);
				EntityShadowInfo advancedShadow3 = drawinfo.drawPlayer.GetAdvancedShadow(num5 - 1);
				Vector2 pos = advancedShadow2.Position + vector2 + advancedShadow2.HeadgearOffset;
				Vector2 pos2 = advancedShadow3.Position + vector2 + advancedShadow3.HeadgearOffset;
				pos = drawinfo.drawPlayer.RotatedRelativePoint(pos, true, false);
				pos2 = drawinfo.drawPlayer.RotatedRelativePoint(pos2, true, false);
				float num6 = (pos2 - pos).ToRotation() - 1.5707964f;
				num6 = 1.5707964f * (float)drawinfo.drawPlayer.direction;
				float num7 = Math.Abs(pos2.X - pos.X);
				Vector2 scale;
				scale..ctor(x, num7 / (float)value.Height);
				float num8 = 1f - (float)num5 / (float)num;
				num8 *= num8;
				num8 *= Utils.GetLerpValue(0f, 4f, num7, true);
				num8 *= 0.5f;
				num8 *= num8;
				Color color = white * num8 * num4;
				if (!(color == Color.Transparent))
				{
					DrawData item = new DrawData(value, pos - Main.screenPosition, null, color, num6, origin, scale, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cWings;
					drawinfo.DrawDataCache.Add(item);
					for (float num9 = 0.25f; num9 < 1f; num9 += 0.25f)
					{
						item = new DrawData(value, Vector2.Lerp(pos, pos2, num9) - Main.screenPosition, null, color, num6, origin, scale, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(item);
					}
				}
			}
		}

		// Token: 0x06004A65 RID: 19045 RVA: 0x00661714 File Offset: 0x0065F914
		public static void DrawMeowcartTrail(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.mount.Type == 33 && drawinfo.shadow <= 0f)
			{
				int num = Math.Min(drawinfo.drawPlayer.availableAdvancedShadowsCount - 1, 20);
				float num2 = 0f;
				for (int num3 = num; num3 > 0; num3--)
				{
					EntityShadowInfo advancedShadow = drawinfo.drawPlayer.GetAdvancedShadow(num3);
					float num8 = num2;
					Vector2 position = drawinfo.drawPlayer.GetAdvancedShadow(num3 - 1).Position;
					num2 = num8 + Vector2.Distance(advancedShadow.Position, position);
				}
				float num4 = MathHelper.Clamp(num2 / 160f, 0f, 1f);
				Main.instance.LoadProjectile(250);
				Texture2D value = TextureAssets.Projectile[250].Value;
				float x = 1.5f;
				Vector2 origin;
				origin..ctor((float)(value.Width / 2), 0f);
				Vector2 vector5 = new Vector2((float)drawinfo.drawPlayer.width, (float)drawinfo.drawPlayer.height) / 2f;
				Vector2 vector2;
				vector2..ctor((float)(-(float)drawinfo.drawPlayer.direction * 10), 15f);
				Color white = Color.White;
				white.A = 127;
				Vector2 vector3 = vector5 + vector2;
				vector3 = Vector2.Zero;
				Vector2 vector4 = drawinfo.drawPlayer.RotatedRelativePoint(drawinfo.drawPlayer.Center + vector3 + vector2, false, true) - drawinfo.drawPlayer.position;
				for (int num5 = num; num5 > 0; num5--)
				{
					EntityShadowInfo advancedShadow2 = drawinfo.drawPlayer.GetAdvancedShadow(num5);
					ref EntityShadowInfo advancedShadow3 = drawinfo.drawPlayer.GetAdvancedShadow(num5 - 1);
					Vector2 pos = advancedShadow2.Position + vector3;
					Vector2 pos2 = advancedShadow3.Position + vector3;
					pos += vector4;
					pos2 += vector4;
					pos = drawinfo.drawPlayer.RotatedRelativePoint(pos, true, false);
					pos2 = drawinfo.drawPlayer.RotatedRelativePoint(pos2, true, false);
					float rotation = (pos2 - pos).ToRotation() - 1.5707964f;
					float num6 = Vector2.Distance(pos, pos2);
					Vector2 scale;
					scale..ctor(x, num6 / (float)value.Height);
					float num7 = 1f - (float)num5 / (float)num;
					num7 *= num7;
					Color color = white * num7 * num4;
					DrawData item = new DrawData(value, pos - Main.screenPosition, null, color, rotation, origin, scale, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
				}
			}
		}

		// Token: 0x06004A66 RID: 19046 RVA: 0x006619BC File Offset: 0x0065FBBC
		public static void DrawPlayer_23_MountFront(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.mount.Active)
			{
				drawinfo.drawPlayer.mount.Draw(drawinfo.DrawDataCache, 2, drawinfo.drawPlayer, drawinfo.Position, drawinfo.colorMount, drawinfo.playerEffect, drawinfo.shadow);
				drawinfo.drawPlayer.mount.Draw(drawinfo.DrawDataCache, 3, drawinfo.drawPlayer, drawinfo.Position, drawinfo.colorMount, drawinfo.playerEffect, drawinfo.shadow);
			}
		}

		// Token: 0x06004A67 RID: 19047 RVA: 0x00661A48 File Offset: 0x0065FC48
		public static void DrawPlayer_24_Pulley(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.pulley && drawinfo.drawPlayer.itemAnimation == 0)
			{
				if (drawinfo.drawPlayer.pulleyDir == 2)
				{
					int num = -25;
					int num2 = 0;
					float rotation = 0f;
					DrawData item = new DrawData(TextureAssets.Pulley.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2) - (float)(9 * drawinfo.drawPlayer.direction)) + num2 * drawinfo.drawPlayer.direction), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)(drawinfo.drawPlayer.height / 2) + 2f * drawinfo.drawPlayer.gravDir + (float)num * drawinfo.drawPlayer.gravDir))), new Rectangle?(new Rectangle(0, TextureAssets.Pulley.Height() / 2 * drawinfo.drawPlayer.pulleyFrame, TextureAssets.Pulley.Width(), TextureAssets.Pulley.Height() / 2)), drawinfo.colorArmorHead, rotation, new Vector2((float)(TextureAssets.Pulley.Width() / 2), (float)(TextureAssets.Pulley.Height() / 4)), 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
					return;
				}
				int num3 = -26;
				int num4 = 10;
				float rotation2 = 0.35f * (float)(-(float)drawinfo.drawPlayer.direction);
				DrawData item2 = new DrawData(TextureAssets.Pulley.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2) - (float)(9 * drawinfo.drawPlayer.direction)) + num4 * drawinfo.drawPlayer.direction), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)(drawinfo.drawPlayer.height / 2) + 2f * drawinfo.drawPlayer.gravDir + (float)num3 * drawinfo.drawPlayer.gravDir))), new Rectangle?(new Rectangle(0, TextureAssets.Pulley.Height() / 2 * drawinfo.drawPlayer.pulleyFrame, TextureAssets.Pulley.Width(), TextureAssets.Pulley.Height() / 2)), drawinfo.colorArmorHead, rotation2, new Vector2((float)(TextureAssets.Pulley.Width() / 2), (float)(TextureAssets.Pulley.Height() / 4)), 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item2);
			}
		}

		// Token: 0x06004A68 RID: 19048 RVA: 0x00661CF0 File Offset: 0x0065FEF0
		public static void DrawPlayer_25_Shield(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.shield <= 0 || drawinfo.drawPlayer.shield >= TextureAssets.AccShield.Length)
			{
				return;
			}
			Vector2 zero = Vector2.Zero;
			if (drawinfo.drawPlayer.shieldRaised)
			{
				zero.Y -= 4f * drawinfo.drawPlayer.gravDir;
			}
			Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
			Vector2 zero2 = Vector2.Zero;
			Vector2 bodyVect = drawinfo.bodyVect;
			if (bodyFrame.Width != TextureAssets.AccShield[drawinfo.drawPlayer.shield].Value.Width)
			{
				bodyFrame.Width = TextureAssets.AccShield[drawinfo.drawPlayer.shield].Value.Width;
				bodyVect.X += (float)(bodyFrame.Width - TextureAssets.AccShield[drawinfo.drawPlayer.shield].Value.Width);
				if (drawinfo.playerEffect.HasFlag(1))
				{
					bodyVect.X = (float)bodyFrame.Width - bodyVect.X;
				}
			}
			DrawData item;
			if (drawinfo.drawPlayer.shieldRaised)
			{
				float num = (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f));
				float x = 2.5f + 1.5f * num;
				Color colorArmorBody = drawinfo.colorArmorBody;
				colorArmorBody.A = 0;
				colorArmorBody *= 0.45f - num * 0.15f;
				for (float num2 = 0f; num2 < 4f; num2 += 1f)
				{
					item = new DrawData(TextureAssets.AccShield[drawinfo.drawPlayer.shield].Value, zero2 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)) + zero + new Vector2(x, 0f).RotatedBy((double)(num2 / 4f * 6.2831855f), default(Vector2)), new Rectangle?(bodyFrame), colorArmorBody, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cShield;
					drawinfo.DrawDataCache.Add(item);
				}
			}
			item = new DrawData(TextureAssets.AccShield[drawinfo.drawPlayer.shield].Value, zero2 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)) + zero, new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
			item.shader = drawinfo.cShield;
			drawinfo.DrawDataCache.Add(item);
			if (drawinfo.drawPlayer.shieldRaised)
			{
				Color colorArmorBody2 = drawinfo.colorArmorBody;
				float num3 = (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 3.1415927f));
				colorArmorBody2.A = (byte)((float)colorArmorBody2.A * (0.5f + 0.5f * num3));
				colorArmorBody2 *= 0.5f + 0.5f * num3;
				item = new DrawData(TextureAssets.AccShield[drawinfo.drawPlayer.shield].Value, zero2 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)) + zero, new Rectangle?(bodyFrame), colorArmorBody2, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cShield;
			}
			if (drawinfo.drawPlayer.shieldRaised && drawinfo.drawPlayer.shieldParryTimeLeft > 0)
			{
				float num4 = (float)drawinfo.drawPlayer.shieldParryTimeLeft / 20f;
				float num5 = 1.5f * num4;
				Vector2 vector = zero2 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)) + zero;
				Color colorArmorBody3 = drawinfo.colorArmorBody;
				float num6 = 1f;
				Vector2 vector2 = drawinfo.Position + drawinfo.drawPlayer.Size / 2f - Main.screenPosition;
				Vector2 vector3 = vector - vector2;
				vector += vector3 * num5;
				num6 += num5;
				colorArmorBody3.A = (byte)((float)colorArmorBody3.A * (1f - num4));
				colorArmorBody3 *= 1f - num4;
				item = new DrawData(TextureAssets.AccShield[drawinfo.drawPlayer.shield].Value, vector, new Rectangle?(bodyFrame), colorArmorBody3, drawinfo.drawPlayer.bodyRotation, bodyVect, num6, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cShield;
				drawinfo.DrawDataCache.Add(item);
			}
			if (drawinfo.drawPlayer.mount.Cart)
			{
				drawinfo.DrawDataCache.Reverse(drawinfo.DrawDataCache.Count - 2, 2);
			}
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x00662444 File Offset: 0x00660644
		public static void DrawPlayer_26_SolarShield(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.solarShields > 0 && drawinfo.shadow == 0f && !drawinfo.drawPlayer.dead)
			{
				Texture2D value = TextureAssets.Extra[61 + drawinfo.drawPlayer.solarShields - 1].Value;
				Color color;
				color..ctor(255, 255, 255, 127);
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

		// Token: 0x06004A6A RID: 19050 RVA: 0x006625C4 File Offset: 0x006607C4
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
			Vector2 position;
			position..ctor((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y)));
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
				float num20 = (1f + num4 * 10f) / 11f;
				drawinfo.itemColor = drawinfo.itemColor.MultiplyRGBA(new Color(Vector4.Lerp(Vector4.One, new Vector4(0f, 0.12f, 0.16f, 0f), 1f - num4)));
			}
			bool flag = drawinfo.drawPlayer.itemAnimation > 0 && heldItem.useStyle != 0;
			bool flag2 = heldItem.holdStyle != 0 && !drawinfo.drawPlayer.pulley;
			if (!drawinfo.drawPlayer.CanVisuallyHoldItem(heldItem))
			{
				flag2 = false;
			}
			if (drawinfo.shadow != 0f || drawinfo.drawPlayer.frozen || (!flag && !flag2) || num <= 0 || drawinfo.drawPlayer.dead || heldItem.noUseGraphic || (drawinfo.drawPlayer.wet && heldItem.noWet && !ItemID.Sets.WaterTorches[num]) || (drawinfo.drawPlayer.happyFunTorchTime && drawinfo.drawPlayer.inventory[drawinfo.drawPlayer.selectedItem].createTile == 4 && drawinfo.drawPlayer.itemAnimation == 0))
			{
				return;
			}
			string name = drawinfo.drawPlayer.name;
			Color color;
			color..ctor(250, 250, 250, heldItem.alpha);
			Vector2 vector = Vector2.Zero;
			if (num <= 426)
			{
				if (num <= 104)
				{
					if (num == 46)
					{
						float amount = Utils.Remap(drawinfo.itemColor.ToVector3().Length() / 1.731f, 0.3f, 0.5f, 1f, 0f, true);
						color = Color.Lerp(Color.Transparent, new Color(255, 255, 255, 127) * 0.7f, amount);
						goto IL_50C;
					}
					if (num != 104)
					{
						goto IL_50C;
					}
				}
				else
				{
					if (num == 204)
					{
						vector = new Vector2(4f, -6f) * drawinfo.drawPlayer.Directions;
						goto IL_50C;
					}
					if (num != 426)
					{
						goto IL_50C;
					}
					goto IL_43C;
				}
			}
			else if (num <= 1506)
			{
				if (num != 797 && num != 1506)
				{
					goto IL_50C;
				}
				goto IL_43C;
			}
			else
			{
				if (num == 3349)
				{
					vector = new Vector2(2f, -2f) * drawinfo.drawPlayer.Directions;
					goto IL_50C;
				}
				if (num - 5094 > 1)
				{
					if (num - 5096 > 1)
					{
						goto IL_50C;
					}
					goto IL_43C;
				}
			}
			vector = new Vector2(4f, -4f) * drawinfo.drawPlayer.Directions;
			goto IL_50C;
			IL_43C:
			vector = new Vector2(6f, -6f) * drawinfo.drawPlayer.Directions;
			IL_50C:
			if (num == 3823)
			{
				vector..ctor((float)(7 * drawinfo.drawPlayer.direction), -7f * drawinfo.drawPlayer.gravDir);
			}
			if (num == 3827)
			{
				vector..ctor((float)(13 * drawinfo.drawPlayer.direction), -13f * drawinfo.drawPlayer.gravDir);
				color = heldItem.GetAlpha(drawinfo.itemColor);
				color = Color.Lerp(color, Color.White, 0.6f);
				color.A = 66;
			}
			Vector2 origin;
			origin..ctor((float)itemDrawFrame.Width * 0.5f - (float)itemDrawFrame.Width * 0.5f * (float)drawinfo.drawPlayer.direction, (float)itemDrawFrame.Height);
			if (heldItem.useStyle == 9 && drawinfo.drawPlayer.itemAnimation > 0)
			{
				Vector2 vector2;
				vector2..ctor(0.5f, 0.4f);
				if (heldItem.type == 5009 || heldItem.type == 5042)
				{
					vector2..ctor(0.26f, 0.5f);
					if (drawinfo.drawPlayer.direction == -1)
					{
						vector2.X = 1f - vector2.X;
					}
				}
				origin = itemDrawFrame.Size() * vector2;
			}
			if (drawinfo.drawPlayer.gravDir == -1f)
			{
				origin.Y = (float)itemDrawFrame.Height - origin.Y;
			}
			origin += vector;
			float num5 = drawinfo.drawPlayer.itemRotation;
			if (heldItem.useStyle == 8)
			{
				float num6 = position.X;
				int direction = drawinfo.drawPlayer.direction;
				position.X = num6 - 0f;
				num5 -= 1.5707964f * (float)drawinfo.drawPlayer.direction;
				origin.Y = 2f;
				origin.X += (float)(2 * drawinfo.drawPlayer.direction);
			}
			if (num == 425 || num == 507)
			{
				if (drawinfo.drawPlayer.gravDir == 1f)
				{
					if (drawinfo.drawPlayer.direction == 1)
					{
						drawinfo.itemEffect = 2;
					}
					else
					{
						drawinfo.itemEffect = 3;
					}
				}
				else if (drawinfo.drawPlayer.direction == 1)
				{
					drawinfo.itemEffect = 0;
				}
				else
				{
					drawinfo.itemEffect = 1;
				}
			}
			if ((num == 946 || num == 4707) && num5 != 0f)
			{
				position.Y -= 22f * drawinfo.drawPlayer.gravDir;
				num5 = -1.57f * (float)(-(float)drawinfo.drawPlayer.direction) * drawinfo.drawPlayer.gravDir;
			}
			ItemSlot.GetItemLight(ref drawinfo.itemColor, heldItem, false);
			if (num == 3476)
			{
				Texture2D value2 = TextureAssets.Extra[64].Value;
				Rectangle rectangle2 = value2.Frame(1, 9, 0, drawinfo.drawPlayer.miscCounter % 54 / 6, 0, 0);
				Vector2 vector3;
				vector3..ctor((float)(rectangle2.Width / 2 * drawinfo.drawPlayer.direction), 0f);
				Vector2 origin2 = rectangle2.Size() / 2f;
				DrawData item = new DrawData(value2, (drawinfo.ItemLocation - Main.screenPosition + vector3).Floor(), new Rectangle?(rectangle2), heldItem.GetAlpha(drawinfo.itemColor).MultiplyRGBA(new Color(new Vector4(0.5f, 0.5f, 0.5f, 0.8f))), drawinfo.drawPlayer.itemRotation, origin2, adjustedItemScale, drawinfo.itemEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
				value2 = TextureAssets.GlowMask[195].Value;
				item = new DrawData(value2, (drawinfo.ItemLocation - Main.screenPosition + vector3).Floor(), new Rectangle?(rectangle2), new Color(250, 250, 250, heldItem.alpha) * 0.5f, drawinfo.drawPlayer.itemRotation, origin2, adjustedItemScale, drawinfo.itemEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
				return;
			}
			if (num == 3779)
			{
				Texture2D texture2D = value;
				Rectangle rectangle3 = texture2D.Frame(1, 1, 0, 0, 0, 0);
				Vector2 vector4;
				vector4..ctor((float)(rectangle3.Width / 2 * drawinfo.drawPlayer.direction), 0f);
				Vector2 origin3 = rectangle3.Size() / 2f;
				float num7 = ((float)drawinfo.drawPlayer.miscCounter / 75f * 6.2831855f).ToRotationVector2().X * 1f + 0f;
				Color color2 = new Color(120, 40, 222, 0) * (num7 / 2f * 0.3f + 0.85f) * 0.5f;
				num7 = 2f;
				DrawData item;
				for (float num8 = 0f; num8 < 4f; num8 += 1f)
				{
					item = new DrawData(TextureAssets.GlowMask[218].Value, (drawinfo.ItemLocation - Main.screenPosition + vector4).Floor() + (num8 * 1.5707964f).ToRotationVector2() * num7, new Rectangle?(rectangle3), color2, drawinfo.drawPlayer.itemRotation, origin3, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
				}
				item = new DrawData(texture2D, (drawinfo.ItemLocation - Main.screenPosition + vector4).Floor(), new Rectangle?(rectangle3), heldItem.GetAlpha(drawinfo.itemColor).MultiplyRGBA(new Color(new Vector4(0.5f, 0.5f, 0.5f, 0.8f))), drawinfo.drawPlayer.itemRotation, origin3, adjustedItemScale, drawinfo.itemEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
				return;
			}
			if (num == 4049)
			{
				Texture2D value3 = TextureAssets.Extra[92].Value;
				Rectangle rectangle4 = value3.Frame(1, 4, 0, drawinfo.drawPlayer.miscCounter % 20 / 5, 0, 0);
				Vector2 vector5;
				vector5..ctor((float)(rectangle4.Width / 2 * drawinfo.drawPlayer.direction), 0f);
				vector5 += new Vector2((float)(-10 * drawinfo.drawPlayer.direction), 8f * drawinfo.drawPlayer.gravDir);
				Vector2 origin4 = rectangle4.Size() / 2f;
				DrawData item = new DrawData(value3, (drawinfo.ItemLocation - Main.screenPosition + vector5).Floor(), new Rectangle?(rectangle4), heldItem.GetAlpha(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, origin4, adjustedItemScale, drawinfo.itemEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
				return;
			}
			if (heldItem.useStyle == 5)
			{
				DrawData item;
				if (Item.staff[num])
				{
					float num9 = drawinfo.drawPlayer.itemRotation + 0.785f * (float)drawinfo.drawPlayer.direction;
					float num10 = 0f;
					float num11 = 0f;
					Vector2 origin5;
					origin5..ctor(0f, (float)itemDrawFrame.Height);
					if (num == 3210)
					{
						num10 = (float)(8 * -(float)drawinfo.drawPlayer.direction);
						num11 = (float)(2 * (int)drawinfo.drawPlayer.gravDir);
					}
					if (num == 3870)
					{
						Vector2 vector9 = (drawinfo.drawPlayer.itemRotation + 0.7853982f * (float)drawinfo.drawPlayer.direction).ToRotationVector2() * new Vector2((float)(-(float)drawinfo.drawPlayer.direction) * 1.5f, drawinfo.drawPlayer.gravDir) * 3f;
						num10 = (float)((int)vector9.X);
						num11 = (float)((int)vector9.Y);
					}
					if (num == 3787)
					{
						num11 = (float)((int)((float)(8 * (int)drawinfo.drawPlayer.gravDir) * (float)Math.Cos((double)num9)));
					}
					if (num == 3209)
					{
						Vector2 vector10 = (new Vector2(-8f, 0f) * drawinfo.drawPlayer.Directions).RotatedBy((double)drawinfo.drawPlayer.itemRotation, default(Vector2));
						num10 = vector10.X;
						num11 = vector10.Y;
					}
					if (drawinfo.drawPlayer.gravDir == -1f)
					{
						if (drawinfo.drawPlayer.direction == -1)
						{
							num9 += 1.57f;
							origin5..ctor((float)itemDrawFrame.Width, 0f);
							num10 -= (float)itemDrawFrame.Width;
						}
						else
						{
							num9 -= 1.57f;
							origin5 = Vector2.Zero;
						}
					}
					else if (drawinfo.drawPlayer.direction == -1)
					{
						origin5..ctor((float)itemDrawFrame.Width, (float)itemDrawFrame.Height);
						num10 -= (float)itemDrawFrame.Width;
					}
					ItemLoader.HoldoutOrigin(drawinfo.drawPlayer, ref origin5);
					item = new DrawData(value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + origin5.X + num10)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + num11))), new Rectangle?(itemDrawFrame), heldItem.GetAlpha(drawinfo.itemColor), num9, origin5, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
					if (num == 3870)
					{
						item = new DrawData(TextureAssets.GlowMask[238].Value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + origin5.X + num10)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + num11))), new Rectangle?(itemDrawFrame), new Color(255, 255, 255, 127), num9, origin5, adjustedItemScale, drawinfo.itemEffect, 0f);
						drawinfo.DrawDataCache.Add(item);
					}
					return;
				}
				if (num == 5118)
				{
					float rotation = drawinfo.drawPlayer.itemRotation + 1.57f * (float)drawinfo.drawPlayer.direction;
					Vector2 vector6;
					vector6..ctor(0f, (float)itemDrawFrame.Height * 0.5f);
					Vector2 origin6;
					origin6..ctor((float)itemDrawFrame.Width * 0.5f, (float)itemDrawFrame.Height);
					Vector2 spinningpoint = new Vector2(10f, 4f) * drawinfo.drawPlayer.Directions;
					spinningpoint = spinningpoint.RotatedBy((double)drawinfo.drawPlayer.itemRotation, default(Vector2));
					item = new DrawData(value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + vector6.X + spinningpoint.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + vector6.Y + spinningpoint.Y))), new Rectangle?(itemDrawFrame), heldItem.GetAlpha(drawinfo.itemColor), rotation, origin6, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
					return;
				}
				Vector2 vector7;
				vector7..ctor(0f, (float)(itemDrawFrame.Height / 2));
				Vector2 vector8 = Main.DrawPlayerItemPos(drawinfo.drawPlayer.gravDir, num);
				int num12 = (int)vector8.X;
				vector7.Y = vector8.Y;
				Vector2 origin7;
				origin7..ctor((float)(-(float)num12), (float)(itemDrawFrame.Height / 2));
				if (drawinfo.drawPlayer.direction == -1)
				{
					origin7..ctor((float)(itemDrawFrame.Width + num12), (float)(itemDrawFrame.Height / 2));
				}
				item = new DrawData(value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + vector7.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + vector7.Y))), new Rectangle?(itemDrawFrame), heldItem.GetAlpha(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, origin7, adjustedItemScale, drawinfo.itemEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
				if (heldItem.color != default(Color))
				{
					item = new DrawData(value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + vector7.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + vector7.Y))), new Rectangle?(itemDrawFrame), heldItem.GetColor(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, origin7, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
				}
				if (heldItem.glowMask != -1)
				{
					item = new DrawData(TextureAssets.GlowMask[(int)heldItem.glowMask].Value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + vector7.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + vector7.Y))), new Rectangle?(itemDrawFrame), new Color(250, 250, 250, heldItem.alpha), drawinfo.drawPlayer.itemRotation, origin7, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
				}
				if (num == 3788)
				{
					float num13 = ((float)drawinfo.drawPlayer.miscCounter / 75f * 6.2831855f).ToRotationVector2().X * 1f + 0f;
					Color color3 = new Color(80, 40, 252, 0) * (num13 / 2f * 0.3f + 0.85f) * 0.5f;
					for (float num14 = 0f; num14 < 4f; num14 += 1f)
					{
						item = new DrawData(TextureAssets.GlowMask[220].Value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + vector7.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + vector7.Y))) + (num14 * 1.5707964f + drawinfo.drawPlayer.itemRotation).ToRotationVector2() * num13, null, color3, drawinfo.drawPlayer.itemRotation, origin7, adjustedItemScale, drawinfo.itemEffect, 0f);
						drawinfo.DrawDataCache.Add(item);
					}
				}
				return;
			}
			else
			{
				DrawData item;
				if (drawinfo.drawPlayer.gravDir == -1f)
				{
					item = new DrawData(value, position, new Rectangle?(itemDrawFrame), heldItem.GetAlpha(drawinfo.itemColor), num5, origin, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
					if (heldItem.color != default(Color))
					{
						item = new DrawData(value, position, new Rectangle?(itemDrawFrame), heldItem.GetColor(drawinfo.itemColor), num5, origin, adjustedItemScale, drawinfo.itemEffect, 0f);
						drawinfo.DrawDataCache.Add(item);
					}
					if (heldItem.glowMask != -1)
					{
						item = new DrawData(TextureAssets.GlowMask[(int)heldItem.glowMask].Value, position, new Rectangle?(itemDrawFrame), new Color(250, 250, 250, heldItem.alpha), num5, origin, adjustedItemScale, drawinfo.itemEffect, 0f);
						drawinfo.DrawDataCache.Add(item);
					}
					return;
				}
				item = new DrawData(value, position, new Rectangle?(itemDrawFrame), heldItem.GetAlpha(drawinfo.itemColor), num5, origin, adjustedItemScale, drawinfo.itemEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
				if (heldItem.color != default(Color))
				{
					item = new DrawData(value, position, new Rectangle?(itemDrawFrame), heldItem.GetColor(drawinfo.itemColor), num5, origin, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
				}
				if (heldItem.glowMask != -1)
				{
					item = new DrawData(TextureAssets.GlowMask[(int)heldItem.glowMask].Value, position, new Rectangle?(itemDrawFrame), color, num5, origin, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
				}
				if (!heldItem.flame || drawinfo.shadow != 0f)
				{
					return;
				}
				try
				{
					Main.instance.LoadItemFlames(num);
					if (TextureAssets.ItemFlame[num].IsLoaded)
					{
						Color color4;
						color4..ctor(100, 100, 100, 0);
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
									color4..ctor(50, 50, 50, 0);
								}
							}
							else
							{
								color4..ctor(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
							}
						}
						else if (num != 5293)
						{
							if (num != 5322)
							{
								if (num == 5353)
								{
									color4..ctor(255, 255, 255, 200);
								}
							}
							else
							{
								color4..ctor(100, 100, 100, 150);
								num17 = (float)(-2 * drawinfo.drawPlayer.direction);
							}
						}
						else
						{
							color4..ctor(50, 50, 100, 20);
						}
						for (int i = 0; i < num15; i++)
						{
							float num18 = drawinfo.drawPlayer.itemFlamePos[i].X * adjustedItemScale * num16;
							float num19 = drawinfo.drawPlayer.itemFlamePos[i].Y * adjustedItemScale * num16;
							item = new DrawData(TextureAssets.ItemFlame[num].Value, new Vector2((float)((int)(position.X + num18 + num17)), (float)((int)(position.Y + num19))), new Rectangle?(itemDrawFrame), color4, num5, origin, adjustedItemScale, drawinfo.itemEffect, 0f);
							drawinfo.DrawDataCache.Add(item);
						}
					}
				}
				catch
				{
				}
				return;
			}
		}

		// Token: 0x06004A6B RID: 19051 RVA: 0x00663D84 File Offset: 0x00661F84
		public static void DrawPlayer_28_ArmOverItem(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.usesCompositeTorso)
			{
				PlayerDrawLayers.DrawPlayer_28_ArmOverItemComposite(ref drawinfo);
				return;
			}
			if (drawinfo.drawPlayer.body > 0)
			{
				Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
				int num = drawinfo.armorAdjust;
				bodyFrame.X += num;
				bodyFrame.Width -= num;
				if (drawinfo.drawPlayer.direction == -1)
				{
					num = 0;
				}
				if (drawinfo.drawPlayer.invis && (drawinfo.drawPlayer.body == 21 || drawinfo.drawPlayer.body == 22))
				{
					return;
				}
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
					Color color;
					color..ctor(100, 100, 100, 0);
					ulong seed = (ulong)((long)(drawinfo.drawPlayer.miscCounter / 4));
					int num2 = 4;
					for (int i = 0; i < num2; i++)
					{
						float num3 = (float)Utils.RandomInt(ref seed, -10, 11) * 0.2f;
						float num4 = (float)Utils.RandomInt(ref seed, -10, 1) * 0.15f;
						item = new DrawData(TextureAssets.GlowMask[240].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2)) + num), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + num3, (float)(drawinfo.drawPlayer.bodyFrame.Height / 2) + num4), new Rectangle?(bodyFrame), color, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
						item.shader = drawinfo.cBody;
						drawinfo.DrawDataCache.Add(item);
					}
					return;
				}
			}
			else if (!drawinfo.drawPlayer.invis)
			{
				DrawData item2 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.skinDyePacked
				};
				drawinfo.DrawDataCache.Add(item2);
				item2 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 8].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorUnderShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item2);
				item2 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 13].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item2);
			}
		}

		// Token: 0x06004A6C RID: 19052 RVA: 0x00664800 File Offset: 0x00662A00
		public static void DrawPlayer_28_ArmOverItemComposite(ref PlayerDrawSet drawinfo)
		{
			Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
			Vector2 vector2 = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
			vector2.Y -= 2f;
			vector += vector2 * (float)(-(float)drawinfo.playerEffect.HasFlag(2).ToDirectionInt());
			float bodyRotation = drawinfo.drawPlayer.bodyRotation;
			float rotation = drawinfo.drawPlayer.bodyRotation + drawinfo.compositeFrontArmRotation;
			Vector2 bodyVect = drawinfo.bodyVect;
			Vector2 compositeOffset_FrontArm = PlayerDrawLayers.GetCompositeOffset_FrontArm(ref drawinfo);
			bodyVect += compositeOffset_FrontArm;
			vector += compositeOffset_FrontArm;
			Vector2 position = vector + drawinfo.frontShoulderOffset;
			if (drawinfo.compFrontArmFrame.X / drawinfo.compFrontArmFrame.Width >= 7)
			{
				vector += new Vector2((float)((!drawinfo.playerEffect.HasFlag(1)) ? 1 : -1), (float)((!drawinfo.playerEffect.HasFlag(2)) ? 1 : -1));
			}
			bool invis = drawinfo.drawPlayer.invis;
			bool flag2 = drawinfo.drawPlayer.body > 0;
			int num2 = (drawinfo.compShoulderOverFrontArm > false) ? 1 : 0;
			int num3 = (!drawinfo.compShoulderOverFrontArm) ? 1 : 0;
			int num4 = (!drawinfo.compShoulderOverFrontArm) ? 1 : 0;
			bool flag = !drawinfo.hidesTopSkin;
			if (flag2)
			{
				if (!drawinfo.drawPlayer.invis || PlayerDrawLayers.IsArmorDrawnWhenInvisible(drawinfo.drawPlayer.body))
				{
					Texture2D value = TextureAssets.ArmorBodyComposite[drawinfo.drawPlayer.body].Value;
					for (int i = 0; i < 2; i++)
					{
						if (!drawinfo.drawPlayer.invis && i == num4 && flag)
						{
							if (drawinfo.missingArm)
							{
								List<DrawData> drawDataCache = drawinfo.DrawDataCache;
								DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorBodySkin, rotation, bodyVect, 1f, drawinfo.playerEffect, 0f)
								{
									shader = drawinfo.skinDyePacked
								};
								drawDataCache.Add(drawData);
							}
							if (drawinfo.missingHand)
							{
								List<DrawData> drawDataCache2 = drawinfo.DrawDataCache;
								DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 9].Value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorBodySkin, rotation, bodyVect, 1f, drawinfo.playerEffect, 0f)
								{
									shader = drawinfo.skinDyePacked
								};
								drawDataCache2.Add(drawData);
							}
						}
						if (i == num2 && !drawinfo.hideCompositeShoulders)
						{
							CompositePlayerDrawContext context = CompositePlayerDrawContext.FrontShoulder;
							DrawData drawData = new DrawData(value, position, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorArmorBody, bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f)
							{
								shader = drawinfo.cBody
							};
							PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, context, drawData);
						}
						if (i == num3)
						{
							CompositePlayerDrawContext context2 = CompositePlayerDrawContext.FrontArm;
							DrawData drawData = new DrawData(value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorArmorBody, rotation, bodyVect, 1f, drawinfo.playerEffect, 0f)
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
					if (j == num2)
					{
						if (flag)
						{
							List<DrawData> drawDataCache3 = drawinfo.DrawDataCache;
							DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, position, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorBodySkin, bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f)
							{
								shader = drawinfo.skinDyePacked
							};
							drawDataCache3.Add(drawData);
						}
						drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 8].Value, position, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorUnderShirt, bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f));
						drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 13].Value, position, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorShirt, bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f));
						drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, position, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorShirt, bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f));
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
					if (j == num3)
					{
						if (flag)
						{
							List<DrawData> drawDataCache4 = drawinfo.DrawDataCache;
							DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorBodySkin, rotation, bodyVect, 1f, drawinfo.playerEffect, 0f)
							{
								shader = drawinfo.skinDyePacked
							};
							drawDataCache4.Add(drawData);
						}
						drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 8].Value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorUnderShirt, rotation, bodyVect, 1f, drawinfo.playerEffect, 0f));
						drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 13].Value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorShirt, rotation, bodyVect, 1f, drawinfo.playerEffect, 0f));
						drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorShirt, rotation, bodyVect, 1f, drawinfo.playerEffect, 0f));
					}
				}
			}
			if (drawinfo.drawPlayer.handon > 0)
			{
				Texture2D value2 = TextureAssets.AccHandsOnComposite[drawinfo.drawPlayer.handon].Value;
				CompositePlayerDrawContext context3 = CompositePlayerDrawContext.FrontArmAccessory;
				DrawData drawData = new DrawData(value2, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorArmorBody, rotation, bodyVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.cHandOn
				};
				PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, context3, drawData);
			}
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x006650C0 File Offset: 0x006632C0
		public static void DrawPlayer_29_OnhandAcc(ref PlayerDrawSet drawinfo)
		{
			if (!drawinfo.usesCompositeFrontHandAcc && drawinfo.drawPlayer.handon > 0)
			{
				DrawData item = new DrawData(TextureAssets.AccHandsOn[drawinfo.drawPlayer.handon].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cHandOn;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x00665214 File Offset: 0x00663414
		public static void DrawPlayer_30_BladedGlove(ref PlayerDrawSet drawinfo)
		{
			Item heldItem = drawinfo.heldItem;
			if (heldItem.type <= -1 || !Item.claw[heldItem.type] || drawinfo.shadow != 0f)
			{
				return;
			}
			Main.instance.LoadItem(heldItem.type);
			Asset<Texture2D> asset = TextureAssets.Item[heldItem.type];
			if (!drawinfo.drawPlayer.frozen && (drawinfo.drawPlayer.itemAnimation > 0 || (heldItem.holdStyle != 0 && !drawinfo.drawPlayer.pulley)) && heldItem.type > 0 && !drawinfo.drawPlayer.dead && !heldItem.noUseGraphic && (!drawinfo.drawPlayer.wet || !heldItem.noWet))
			{
				if (drawinfo.drawPlayer.gravDir == -1f)
				{
					DrawData item = new DrawData(asset.Value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, asset.Width(), asset.Height())), heldItem.GetAlpha(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, new Vector2((float)asset.Width() * 0.5f - (float)asset.Width() * 0.5f * (float)drawinfo.drawPlayer.direction, 0f), drawinfo.drawPlayer.GetAdjustedItemScale(heldItem), drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
					return;
				}
				DrawData item2 = new DrawData(asset.Value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, asset.Width(), asset.Height())), heldItem.GetAlpha(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, new Vector2((float)asset.Width() * 0.5f - (float)asset.Width() * 0.5f * (float)drawinfo.drawPlayer.direction, (float)asset.Height()), drawinfo.drawPlayer.GetAdjustedItemScale(heldItem), drawinfo.itemEffect, 0f);
				drawinfo.DrawDataCache.Add(item2);
			}
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x00665486 File Offset: 0x00663686
		public static void DrawPlayer_31_ProjectileOverArm(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.heldProj >= 0 && drawinfo.shadow == 0f && drawinfo.heldProjOverHand)
			{
				drawinfo.projectileDrawPosition = drawinfo.DrawDataCache.Count;
			}
		}

		// Token: 0x06004A70 RID: 19056 RVA: 0x006654BC File Offset: 0x006636BC
		public static void DrawPlayer_32_FrontAcc(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.front > 0 && !drawinfo.drawPlayer.mount.Active)
			{
				Vector2 zero = Vector2.Zero;
				DrawData item = new DrawData(TextureAssets.AccFront[drawinfo.drawPlayer.front].Value, zero + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				item.shader = drawinfo.cFront;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06004A71 RID: 19057 RVA: 0x00665628 File Offset: 0x00663828
		public static void DrawPlayer_32_FrontAcc_FrontPart(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.front <= 0)
			{
				return;
			}
			Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
			int num = bodyFrame.Width / 2;
			bodyFrame.Width -= num;
			Vector2 bodyVect = drawinfo.bodyVect;
			if (drawinfo.playerEffect.HasFlag(1))
			{
				bodyVect.X -= (float)num;
			}
			Vector2 vector = Vector2.Zero + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
			DrawData item = new DrawData(TextureAssets.AccFront[drawinfo.drawPlayer.front].Value, vector, new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
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
				if (drawinfo.playerEffect.HasFlag(1))
				{
					num2 = rectangle.Width - 2;
					num4 = -2;
				}
				for (int i = 0; i < num3; i++)
				{
					value.X = rectangle.X + 2 * i;
					Color immuneAlpha = drawinfo.drawPlayer.GetImmuneAlpha(LiquidRenderer.GetShimmerGlitterColor(true, (float)i / 16f, 0f), drawinfo.shadow);
					immuneAlpha *= (float)drawinfo.colorArmorBody.A / 255f;
					item = new DrawData(TextureAssets.GlowMask[331].Value, vector + new Vector2((float)(num2 + i * num4), 0f), new Rectangle?(value), immuneAlpha, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cFront;
					drawinfo.DrawDataCache.Add(item);
				}
			}
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x006658F0 File Offset: 0x00663AF0
		public static void DrawPlayer_32_FrontAcc_BackPart(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.front <= 0)
			{
				return;
			}
			Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
			int num = bodyFrame.Width / 2;
			bodyFrame.Width -= num;
			bodyFrame.X += num;
			Vector2 bodyVect = drawinfo.bodyVect;
			if (!drawinfo.playerEffect.HasFlag(1))
			{
				bodyVect.X -= (float)num;
			}
			Vector2 vector = Vector2.Zero + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
			DrawData item = new DrawData(TextureAssets.AccFront[drawinfo.drawPlayer.front].Value, vector, new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
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
				if (drawinfo.playerEffect.HasFlag(1))
				{
					num2 = rectangle.Width - 2;
					num4 = -2;
				}
				for (int i = 0; i < num3; i++)
				{
					value.X = rectangle.X + 2 * i;
					Color immuneAlpha = drawinfo.drawPlayer.GetImmuneAlpha(LiquidRenderer.GetShimmerGlitterColor(true, (float)i / 16f, 0f), drawinfo.shadow);
					immuneAlpha *= (float)drawinfo.colorArmorBody.A / 255f;
					item = new DrawData(TextureAssets.GlowMask[331].Value, vector + new Vector2((float)(num2 + i * num4), 0f), new Rectangle?(value), immuneAlpha, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
					item.shader = drawinfo.cFront;
					drawinfo.DrawDataCache.Add(item);
				}
			}
		}

		// Token: 0x06004A73 RID: 19059 RVA: 0x00665BC4 File Offset: 0x00663DC4
		public static void DrawPlayer_33_FrozenOrWebbedDebuff(ref PlayerDrawSet drawinfo)
		{
			if (!drawinfo.drawPlayer.shimmering)
			{
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
					DrawData item2 = new DrawData(value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f + (float)num))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), null, color, drawinfo.drawPlayer.bodyRotation, value.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(item2);
				}
			}
		}

		// Token: 0x06004A74 RID: 19060 RVA: 0x00665F1C File Offset: 0x0066411C
		public static void DrawPlayer_34_ElectrifiedDebuffFront(ref PlayerDrawSet drawinfo)
		{
			if (!drawinfo.drawPlayer.electrified || drawinfo.shadow != 0f)
			{
				return;
			}
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

		// Token: 0x06004A75 RID: 19061 RVA: 0x006660B8 File Offset: 0x006642B8
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

		// Token: 0x06004A76 RID: 19062 RVA: 0x0066622C File Offset: 0x0066442C
		public static void DrawPlayer_36_CTG(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.shadow != 0f || drawinfo.drawPlayer.ownedLargeGems <= 0)
			{
				return;
			}
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
			if (num <= 0f)
			{
				return;
			}
			float num5 = 6.2831855f / num;
			float num6 = 0f;
			Vector2 vector;
			vector..ctor(1.3f, 0.65f);
			if (!flag)
			{
				vector = Vector2.One;
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
					Vector2 vector2 = (num4 + num5 * ((float)j - num6)).ToRotationVector2();
					float num7 = num2;
					if (flag)
					{
						num7 = MathHelper.Lerp(num2 * 0.7f, 1f, vector2.Y / 2f + 0.5f);
					}
					Texture2D value = TextureAssets.Gem[j].Value;
					DrawData item = new DrawData(value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - 80f))) + vector2 * vector * num3, null, new Color(250, 250, 250, (int)(Main.mouseTextColor / 2)), 0f, value.Size() / 2f, ((float)Main.mouseTextColor / 1000f + 0.8f) * num7, 0, 0f);
					list.Add(item);
				}
			}
			if (flag)
			{
				List<DrawData> list2 = list;
				Comparison<DrawData> comparison;
				if ((comparison = PlayerDrawLayers.<>O.<0>__CompareDrawSorterByYScale) == null)
				{
					comparison = (PlayerDrawLayers.<>O.<0>__CompareDrawSorterByYScale = new Comparison<DrawData>(DelegateMethods.CompareDrawSorterByYScale));
				}
				list2.Sort(comparison);
			}
			drawinfo.DrawDataCache.AddRange(list);
		}

		// Token: 0x06004A77 RID: 19063 RVA: 0x006664F4 File Offset: 0x006646F4
		public static void DrawPlayer_37_BeetleBuff(ref PlayerDrawSet drawinfo)
		{
			if ((!drawinfo.drawPlayer.beetleOffense && !drawinfo.drawPlayer.beetleDefense) || drawinfo.shadow != 0f)
			{
				return;
			}
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
					Vector2 vector = -drawinfo.drawPlayer.beetleVel[i] * (float)j;
					item = new DrawData(TextureAssets.Beetle.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)(drawinfo.drawPlayer.height / 2)))) + drawinfo.drawPlayer.beetlePos[i] + vector, new Rectangle?(new Rectangle(0, TextureAssets.Beetle.Height() / 3 * drawinfo.drawPlayer.beetleFrame + 1, TextureAssets.Beetle.Width(), TextureAssets.Beetle.Height() / 3 - 2)), colorArmorBody, 0f, new Vector2((float)(TextureAssets.Beetle.Width() / 2), (float)(TextureAssets.Beetle.Height() / 6)), 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(item);
				}
				item = new DrawData(TextureAssets.Beetle.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)(drawinfo.drawPlayer.height / 2)))) + drawinfo.drawPlayer.beetlePos[i], new Rectangle?(new Rectangle(0, TextureAssets.Beetle.Height() / 3 * drawinfo.drawPlayer.beetleFrame + 1, TextureAssets.Beetle.Width(), TextureAssets.Beetle.Height() / 3 - 2)), drawinfo.colorArmorBody, 0f, new Vector2((float)(TextureAssets.Beetle.Width() / 2), (float)(TextureAssets.Beetle.Height() / 6)), 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06004A78 RID: 19064 RVA: 0x006667DC File Offset: 0x006649DC
		public static void DrawPlayer_38_EyebrellaCloud(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.eyebrellaCloud && drawinfo.shadow == 0f)
			{
				Texture2D value = TextureAssets.Projectile[238].Value;
				int frameY = drawinfo.drawPlayer.miscCounter % 18 / 6;
				Rectangle value2 = value.Frame(1, 6, 0, frameY, 0, 0);
				Vector2 origin;
				origin..ctor((float)(value2.Width / 2), (float)(value2.Height / 2));
				Vector2 vector;
				vector..ctor(0f, -70f);
				Vector2 vector2 = drawinfo.drawPlayer.MountedCenter - new Vector2(0f, (float)drawinfo.drawPlayer.height * 0.5f) + vector - Main.screenPosition;
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
				Color color2;
				color2..ctor(r, g, b, 100);
				float num2 = (float)(drawinfo.drawPlayer.miscCounter % 50) / 50f;
				float num3 = 3f;
				DrawData item;
				for (int i = 0; i < 2; i++)
				{
					Vector2 vector3 = new Vector2((i == 0) ? (0f - num3) : num3, 0f).RotatedBy((double)(num2 * 6.2831855f * ((i == 0) ? 1f : -1f)), default(Vector2));
					item = new DrawData(value, vector2 + vector3, new Rectangle?(value2), color2 * 0.65f, 0f, origin, 1f, (drawinfo.drawPlayer.gravDir == -1f) ? 2 : 0, 0f);
					item.shader = drawinfo.cHead;
					item.ignorePlayerRotation = true;
					drawinfo.DrawDataCache.Add(item);
				}
				item = new DrawData(value, vector2, new Rectangle?(value2), color2, 0f, origin, 1f, (drawinfo.drawPlayer.gravDir == -1f) ? 2 : 0, 0f);
				item.shader = drawinfo.cHead;
				item.ignorePlayerRotation = true;
				drawinfo.DrawDataCache.Add(item);
			}
		}

		// Token: 0x06004A79 RID: 19065 RVA: 0x00666A68 File Offset: 0x00664C68
		private static Vector2 GetCompositeOffset_BackArm(ref PlayerDrawSet drawinfo)
		{
			return new Vector2((float)(6 * ((!drawinfo.playerEffect.HasFlag(1)) ? 1 : -1)), (float)(2 * ((!drawinfo.playerEffect.HasFlag(2)) ? 1 : -1)));
		}

		// Token: 0x06004A7A RID: 19066 RVA: 0x00666AB8 File Offset: 0x00664CB8
		private static Vector2 GetCompositeOffset_FrontArm(ref PlayerDrawSet drawinfo)
		{
			return new Vector2((float)(-5 * ((!drawinfo.playerEffect.HasFlag(1)) ? 1 : -1)), 0f);
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x00666AE4 File Offset: 0x00664CE4
		public static void DrawPlayer_TransformDrawData(ref PlayerDrawSet drawinfo)
		{
			Vector2 vector = drawinfo.Position - Main.screenPosition + drawinfo.rotationOrigin;
			Vector2 vector2 = drawinfo.drawPlayer.position + drawinfo.rotationOrigin;
			Matrix matrix = Matrix.CreateRotationZ(drawinfo.rotation);
			for (int i = 0; i < drawinfo.DustCache.Count; i++)
			{
				Vector2 position = Main.dust[drawinfo.DustCache[i]].position - vector2;
				position = Vector2.Transform(position, matrix);
				Main.dust[drawinfo.DustCache[i]].position = position + vector2;
			}
			for (int j = 0; j < drawinfo.GoreCache.Count; j++)
			{
				Vector2 position2 = Main.gore[drawinfo.GoreCache[j]].position - vector2;
				position2 = Vector2.Transform(position2, matrix);
				Main.gore[drawinfo.GoreCache[j]].position = position2 + vector2;
			}
			for (int k = 0; k < drawinfo.DrawDataCache.Count; k++)
			{
				DrawData value = drawinfo.DrawDataCache[k];
				if (!value.ignorePlayerRotation)
				{
					Vector2 position3 = value.position - vector;
					position3 = Vector2.Transform(position3, matrix);
					value.position = position3 + vector;
					value.rotation += drawinfo.rotation;
					drawinfo.DrawDataCache[k] = value;
				}
			}
		}

		// Token: 0x06004A7C RID: 19068 RVA: 0x00666C6C File Offset: 0x00664E6C
		public static void DrawPlayer_ScaleDrawData(ref PlayerDrawSet drawinfo, float scale)
		{
			if (scale != 1f)
			{
				Vector2 vector = drawinfo.Position + drawinfo.drawPlayer.Size * new Vector2(0.5f, 1f) - Main.screenPosition;
				for (int i = 0; i < drawinfo.DrawDataCache.Count; i++)
				{
					DrawData value = drawinfo.DrawDataCache[i];
					Vector2 vector2 = value.position - vector;
					value.position = vector + vector2 * scale;
					value.scale *= scale;
					drawinfo.DrawDataCache[i] = value;
				}
			}
		}

		// Token: 0x06004A7D RID: 19069 RVA: 0x00666D24 File Offset: 0x00664F24
		public static void DrawPlayer_AddSelectionGlow(ref PlayerDrawSet drawinfo)
		{
			if (!(drawinfo.selectionGlowColor == Color.Transparent))
			{
				Color selectionGlowColor = drawinfo.selectionGlowColor;
				List<DrawData> list = new List<DrawData>();
				list.AddRange(PlayerDrawLayers.GetFlatColoredCloneData(ref drawinfo, new Vector2(0f, -2f), selectionGlowColor));
				list.AddRange(PlayerDrawLayers.GetFlatColoredCloneData(ref drawinfo, new Vector2(0f, 2f), selectionGlowColor));
				list.AddRange(PlayerDrawLayers.GetFlatColoredCloneData(ref drawinfo, new Vector2(2f, 0f), selectionGlowColor));
				list.AddRange(PlayerDrawLayers.GetFlatColoredCloneData(ref drawinfo, new Vector2(-2f, 0f), selectionGlowColor));
				list.AddRange(drawinfo.DrawDataCache);
				drawinfo.DrawDataCache = list;
			}
		}

		// Token: 0x06004A7E RID: 19070 RVA: 0x00666DD8 File Offset: 0x00664FD8
		public static void DrawPlayer_MakeIntoFirstFractalAfterImage(ref PlayerDrawSet drawinfo)
		{
			if (!drawinfo.drawPlayer.isFirstFractalAfterImage)
			{
				if (drawinfo.drawPlayer.HeldItem.type == 4722)
				{
					int itemAnimation = drawinfo.drawPlayer.itemAnimation;
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

		// Token: 0x06004A7F RID: 19071 RVA: 0x00666E88 File Offset: 0x00665088
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
			foreach (DrawData item in drawDataCache)
			{
				if (item.texture != null)
				{
					item.Draw(PlayerDrawLayers.spriteBuffer);
				}
			}
			PlayerDrawLayers.spriteBuffer.UploadAndBind();
			DrawData cdd = default(DrawData);
			int num = 0;
			for (int i = 0; i <= drawDataCache.Count; i++)
			{
				if (drawinfo.projectileDrawPosition == i)
				{
					if (cdd.shader != 0)
					{
						Main.pixelShader.CurrentTechnique.Passes[0].Apply();
					}
					PlayerDrawLayers.spriteBuffer.Unbind();
					PlayerDrawLayers.DrawHeldProj(drawinfo, Main.projectile[drawinfo.drawPlayer.heldProj]);
					PlayerDrawLayers.spriteBuffer.Bind();
				}
				if (i != drawDataCache.Count)
				{
					cdd = drawDataCache[i];
					if (cdd.sourceRect == null)
					{
						cdd.sourceRect = new Rectangle?(cdd.texture.Frame(1, 1, 0, 0, 0, 0));
					}
					PlayerDrawHelper.SetShaderForData(drawinfo.drawPlayer, drawinfo.cHead, ref cdd);
					if (cdd.texture != null)
					{
						PlayerDrawLayers.spriteBuffer.DrawSingle(num++);
					}
				}
			}
			PlayerDrawLayers.spriteBuffer.Unbind();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x0066703C File Offset: 0x0066523C
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

		// Token: 0x06004A81 RID: 19073 RVA: 0x00667090 File Offset: 0x00665290
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
					DrawData cdd = drawDataCache[i];
					if (cdd.sourceRect == null)
					{
						cdd.sourceRect = new Rectangle?(cdd.texture.Frame(1, 1, 0, 0, 0, 0));
					}
					PlayerDrawHelper.SetShaderForData(drawinfo.drawPlayer, drawinfo.cHead, ref cdd);
					num = cdd.shader;
					if (cdd.texture != null)
					{
						cdd.Draw(spriteBatch);
					}
				}
			}
			pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06004A82 RID: 19074 RVA: 0x006671F4 File Offset: 0x006653F4
		public static void DrawPlayer_DrawSelectionRect(ref PlayerDrawSet drawinfo)
		{
			Vector2 lowest;
			Vector2 highest;
			SpriteRenderTargetHelper.GetDrawBoundary(drawinfo.DrawDataCache, out lowest, out highest);
			Utils.DrawRect(Main.spriteBatch, lowest + Main.screenPosition, highest + Main.screenPosition, Color.White);
		}

		// Token: 0x06004A83 RID: 19075 RVA: 0x00667235 File Offset: 0x00665435
		private static bool IsArmorDrawnWhenInvisible(int torsoID)
		{
			return torsoID - 21 > 1;
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x00667244 File Offset: 0x00665444
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

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06004A85 RID: 19077 RVA: 0x006672BC File Offset: 0x006654BC
		internal static IReadOnlyList<PlayerDrawLayer> FixedVanillaLayers
		{
			get
			{
				return new PlayerDrawLayer[]
				{
					PlayerDrawLayers.JimsCloak,
					PlayerDrawLayers.MountBack,
					PlayerDrawLayers.Carpet,
					PlayerDrawLayers.PortableStool,
					PlayerDrawLayers.ElectrifiedDebuffBack,
					PlayerDrawLayers.ForbiddenSetRing,
					PlayerDrawLayers.SafemanSun,
					PlayerDrawLayers.WebbedDebuffBack,
					PlayerDrawLayers.LeinforsHairShampoo,
					PlayerDrawLayers.Backpacks,
					PlayerDrawLayers.Tails,
					PlayerDrawLayers.Wings,
					PlayerDrawLayers.HairBack,
					PlayerDrawLayers.BackAcc,
					PlayerDrawLayers.HeadBack,
					PlayerDrawLayers.BalloonAcc,
					PlayerDrawLayers.Skin,
					PlayerDrawLayers.Leggings,
					PlayerDrawLayers.Shoes,
					PlayerDrawLayers.Robe,
					PlayerDrawLayers.SkinLongCoat,
					PlayerDrawLayers.ArmorLongCoat,
					PlayerDrawLayers.Torso,
					PlayerDrawLayers.OffhandAcc,
					PlayerDrawLayers.WaistAcc,
					PlayerDrawLayers.NeckAcc,
					PlayerDrawLayers.Head,
					PlayerDrawLayers.FinchNest,
					PlayerDrawLayers.FaceAcc,
					PlayerDrawLayers.MountFront,
					PlayerDrawLayers.Pulley,
					PlayerDrawLayers.JimsDroneRadio,
					PlayerDrawLayers.FrontAccBack,
					PlayerDrawLayers.Shield,
					PlayerDrawLayers.SolarShield,
					PlayerDrawLayers.ArmOverItem,
					PlayerDrawLayers.HandOnAcc,
					PlayerDrawLayers.BladedGlove,
					PlayerDrawLayers.ProjectileOverArm,
					PlayerDrawLayers.FrozenOrWebbedDebuff,
					PlayerDrawLayers.ElectrifiedDebuffFront,
					PlayerDrawLayers.IceBarrier,
					PlayerDrawLayers.CaptureTheGem,
					PlayerDrawLayers.BeetleBuff,
					PlayerDrawLayers.EyebrellaCloud
				};
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06004A86 RID: 19078 RVA: 0x0066745C File Offset: 0x0066565C
		public static PlayerDrawLayer FirstVanillaLayer
		{
			get
			{
				return PlayerDrawLayers.FixedVanillaLayers[0];
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06004A87 RID: 19079 RVA: 0x00667469 File Offset: 0x00665669
		public static PlayerDrawLayer LastVanillaLayer
		{
			get
			{
				IReadOnlyList<PlayerDrawLayer> fixedVanillaLayers = PlayerDrawLayers.FixedVanillaLayers;
				return fixedVanillaLayers[fixedVanillaLayers.Count - 1];
			}
		}

		/// <summary>
		/// Use to order this layer before the first vanilla layer. This layer will draw behind all vanilla layers.
		/// </summary>
		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06004A88 RID: 19080 RVA: 0x0066747D File Offset: 0x0066567D
		public static PlayerDrawLayer.Between BeforeFirstVanillaLayer
		{
			get
			{
				return new PlayerDrawLayer.Between(null, PlayerDrawLayers.FirstVanillaLayer);
			}
		}

		/// <summary>
		/// Use to order this layer after the last vanilla layer. This layer will draw after all vanilla layers.
		/// </summary>
		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06004A89 RID: 19081 RVA: 0x0066748A File Offset: 0x0066568A
		public static PlayerDrawLayer.Between AfterLastVanillaLayer
		{
			get
			{
				return new PlayerDrawLayer.Between(PlayerDrawLayers.LastVanillaLayer, null);
			}
		}

		// Token: 0x04005FAA RID: 24490
		private const int DEFAULT_MAX_SPRITES = 200;

		// Token: 0x04005FAB RID: 24491
		private static SpriteDrawBuffer spriteBuffer;

		/// <summary> Adds <see cref="F:Terraria.DataStructures.PlayerDrawSet.torsoOffset" /> to <see cref="F:Terraria.DataStructures.PlayerDrawSet.Position" /> and <see cref="F:Terraria.DataStructures.PlayerDrawSet.ItemLocation" /> vectors' Y axes. </summary>
		// Token: 0x04005FAC RID: 24492
		public static readonly PlayerDrawLayer.Transformation TorsoGroup = new VanillaPlayerDrawTransform(new VanillaPlayerDrawTransform.LayerFunction(PlayerDrawLayers.DrawPlayer_extra_TorsoPlus), new VanillaPlayerDrawTransform.LayerFunction(PlayerDrawLayers.DrawPlayer_extra_TorsoMinus), null);

		/// <summary> Adds <see cref="F:Terraria.DataStructures.PlayerDrawSet.mountOffSet" />/2 to <see cref="F:Terraria.DataStructures.PlayerDrawSet.Position" /> vector's Y axis. </summary>
		// Token: 0x04005FAD RID: 24493
		public static readonly PlayerDrawLayer.Transformation MountGroup = new VanillaPlayerDrawTransform(new VanillaPlayerDrawTransform.LayerFunction(PlayerDrawLayers.DrawPlayer_extra_MountPlus), new VanillaPlayerDrawTransform.LayerFunction(PlayerDrawLayers.DrawPlayer_extra_MountMinus), PlayerDrawLayers.TorsoGroup);

		/// <summary> Draws Jim's Cloak, if the player is wearing Jim's Leggings (a developer item). </summary>
		// Token: 0x04005FAE RID: 24494
		public static readonly PlayerDrawLayer JimsCloak = new VanillaPlayerDrawLayer("JimsCloak", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_01_2_JimsCloak), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the back textures of the player's mount. </summary>
		// Token: 0x04005FAF RID: 24495
		public static readonly PlayerDrawLayer MountBack = new VanillaPlayerDrawLayer("MountBack", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_02_MountBehindPlayer), null, false, null, null);

		/// <summary> Draws the Flying Carpet accessory, if the player has it equipped and is using it. </summary>
		// Token: 0x04005FB0 RID: 24496
		public static readonly PlayerDrawLayer Carpet = new VanillaPlayerDrawLayer("Carpet", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_03_Carpet), null, false, null, null);

		/// <summary> Draws the Step Stool accessory, if the player has it equipped and is using it. </summary>
		// Token: 0x04005FB1 RID: 24497
		public static readonly PlayerDrawLayer PortableStool = new VanillaPlayerDrawLayer("PortableStool", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_03_PortableStool), null, false, null, null);

		/// <summary> Draws the back textures of the Electrified debuff, if the player has it. </summary>
		// Token: 0x04005FB2 RID: 24498
		public static readonly PlayerDrawLayer ElectrifiedDebuffBack = new VanillaPlayerDrawLayer("ElectrifiedDebuffBack", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_04_ElectrifiedDebuffBack), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the 'Forbidden Sign' if the player has a full 'Forbidden Armor' set equipped. </summary>
		// Token: 0x04005FB3 RID: 24499
		public static readonly PlayerDrawLayer ForbiddenSetRing = new VanillaPlayerDrawLayer("ForbiddenSetRing", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_05_ForbiddenSetRing), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws a sun above the player's head if they have "Safeman's Sunny Day" headgear equipped. </summary>
		// Token: 0x04005FB4 RID: 24500
		public static readonly PlayerDrawLayer SafemanSun = new VanillaPlayerDrawLayer("SafemanSun", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_05_2_SafemanSun), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the back textures of the Webbed debuff, if the player has it. </summary>
		// Token: 0x04005FB5 RID: 24501
		public static readonly PlayerDrawLayer WebbedDebuffBack = new VanillaPlayerDrawLayer("WebbedDebuffBack", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_06_WebbedDebuffBack), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws effects of "Leinfors' Luxury Shampoo", if the player has it equipped. </summary>
		// Token: 0x04005FB6 RID: 24502
		public static readonly PlayerDrawLayer LeinforsHairShampoo = new VanillaPlayerDrawLayer("LeinforsHairShampoo", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_07_LeinforsHairShampoo), PlayerDrawLayers.TorsoGroup, true, null, null);

		/// <summary> Draws the player's held item's backpack. </summary>
		// Token: 0x04005FB7 RID: 24503
		public static readonly PlayerDrawLayer Backpacks = new VanillaPlayerDrawLayer("Backpacks", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_08_Backpacks), null, false, null, null);

		/// <summary> Draws the player's tails vanities. </summary>
		// Token: 0x04005FB8 RID: 24504
		public static readonly PlayerDrawLayer Tails = new VanillaPlayerDrawLayer("Tails", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_08_1_Tails), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the player's wings. </summary>
		// Token: 0x04005FB9 RID: 24505
		public static readonly PlayerDrawLayer Wings = new VanillaPlayerDrawLayer("Wings", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_09_Wings), null, false, null, null);

		/// <summary> Draws the player's under-headgear hair. </summary>
		// Token: 0x04005FBA RID: 24506
		public static readonly PlayerDrawLayer HairBack = new VanillaPlayerDrawLayer("HairBack", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_01_BackHair), PlayerDrawLayers.TorsoGroup, true, null, null);

		/// <summary> Draws the player's back accessories. </summary>
		// Token: 0x04005FBB RID: 24507
		public static readonly PlayerDrawLayer BackAcc = new VanillaPlayerDrawLayer("BackAcc", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_10_BackAcc), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the back textures of the player's head, including armor. </summary>
		// Token: 0x04005FBC RID: 24508
		public static readonly PlayerDrawLayer HeadBack = new VanillaPlayerDrawLayer("HeadBack", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_01_3_BackHead), PlayerDrawLayers.TorsoGroup, true, null, null);

		/// <summary> Draws the player's balloon accessory, if they have one. </summary>
		// Token: 0x04005FBD RID: 24509
		public static readonly PlayerDrawLayer BalloonAcc = new VanillaPlayerDrawLayer("BalloonAcc", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_11_Balloons), null, false, null, null);

		/// <summary> Draws the player's body and leg skin. </summary>
		// Token: 0x04005FBE RID: 24510
		public static readonly PlayerDrawLayer Skin = new VanillaPlayerDrawLayer("Skin", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_12_Skin), null, false, null, null);

		/// <summary> Draws the player's leg armor or pants and shoes. </summary>
		// Token: 0x04005FBF RID: 24511
		public static readonly PlayerDrawLayer Leggings = new VanillaPlayerDrawLayer("Leggings", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_13_Leggings), null, false, (PlayerDrawSet drawinfo) => !drawinfo.drawPlayer.wearsRobe || drawinfo.drawPlayer.body == 166, null);

		/// <summary> Draws the player's shoes. </summary>
		// Token: 0x04005FC0 RID: 24512
		public static readonly PlayerDrawLayer Shoes = new VanillaPlayerDrawLayer("Shoes", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_14_Shoes), null, false, null, null);

		/// <summary> Draws the player's robe. </summary>
		// Token: 0x04005FC1 RID: 24513
		public static readonly PlayerDrawLayer Robe = new VanillaPlayerDrawLayer("Robe", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_13_Leggings), null, false, (PlayerDrawSet drawinfo) => drawinfo.drawPlayer.wearsRobe && drawinfo.drawPlayer.body != 166, null);

		/// <summary> Draws the longcoat default clothing style, if the player has it. </summary>
		// Token: 0x04005FC2 RID: 24514
		public static readonly PlayerDrawLayer SkinLongCoat = new VanillaPlayerDrawLayer("SkinLongCoat", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_15_SkinLongCoat), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the currently equipped armor's longcoat, if it has one. </summary>
		// Token: 0x04005FC3 RID: 24515
		public static readonly PlayerDrawLayer ArmorLongCoat = new VanillaPlayerDrawLayer("ArmorLongCoat", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_16_ArmorLongCoat), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the player's body armor or shirts. </summary>
		// Token: 0x04005FC4 RID: 24516
		public static readonly PlayerDrawLayer Torso = new VanillaPlayerDrawLayer("Torso", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_17_Torso), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the player's off-hand accessory. </summary>
		// Token: 0x04005FC5 RID: 24517
		public static readonly PlayerDrawLayer OffhandAcc = new VanillaPlayerDrawLayer("OffhandAcc", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_18_OffhandAcc), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the player's waist accessory. </summary>
		// Token: 0x04005FC6 RID: 24518
		public static readonly PlayerDrawLayer WaistAcc = new VanillaPlayerDrawLayer("WaistAcc", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_19_WaistAcc), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the player's neck accessory. </summary>
		// Token: 0x04005FC7 RID: 24519
		public static readonly PlayerDrawLayer NeckAcc = new VanillaPlayerDrawLayer("NeckAcc", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_20_NeckAcc), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the player's head, including hair, armor, and etc. </summary>
		// Token: 0x04005FC8 RID: 24520
		public static readonly PlayerDrawLayer Head = new VanillaPlayerDrawLayer("Head", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_21_Head), PlayerDrawLayers.TorsoGroup, true, null, null);

		/// <summary> Draws a finch nest on the player's head, if the player has a finch summoned. </summary>
		// Token: 0x04005FC9 RID: 24521
		public static readonly PlayerDrawLayer FinchNest = new VanillaPlayerDrawLayer("FinchNest", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_21_2_FinchNest), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the player's face accessory. </summary>
		// Token: 0x04005FCA RID: 24522
		public static readonly PlayerDrawLayer FaceAcc = new VanillaPlayerDrawLayer("FaceAcc", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_22_FaceAcc), PlayerDrawLayers.TorsoGroup, true, null, null);

		/// <summary> Draws the front textures of the player's mount. </summary>
		// Token: 0x04005FCB RID: 24523
		public static readonly PlayerDrawLayer MountFront = new VanillaPlayerDrawLayer("MountFront", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_23_MountFront), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the pulley if the player is hanging on a rope. </summary>
		// Token: 0x04005FCC RID: 24524
		public static readonly PlayerDrawLayer Pulley = new VanillaPlayerDrawLayer("Pulley", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_24_Pulley), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the pulley if the player is hanging on a rope. </summary>
		// Token: 0x04005FCD RID: 24525
		public static readonly PlayerDrawLayer JimsDroneRadio = new VanillaPlayerDrawLayer("JimsDroneRadio", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_JimsDroneRadio), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the back part of player's front accessory. </summary>
		// Token: 0x04005FCE RID: 24526
		public static readonly PlayerDrawLayer FrontAccBack = new VanillaPlayerDrawLayer("FrontAccBack", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_32_FrontAcc_BackPart), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the player's shield accessory. </summary>
		// Token: 0x04005FCF RID: 24527
		public static readonly PlayerDrawLayer Shield = new VanillaPlayerDrawLayer("Shield", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_25_Shield), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the player's solar shield if the player has one. </summary>
		// Token: 0x04005FD0 RID: 24528
		public static readonly PlayerDrawLayer SolarShield = new VanillaPlayerDrawLayer("SolarShield", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_26_SolarShield), PlayerDrawLayers.MountGroup, false, null, null);

		/// <summary> Draws the player's main arm (including the armor's if applicable), when it should appear over the held item. </summary>
		// Token: 0x04005FD1 RID: 24529
		public static readonly PlayerDrawLayer ArmOverItem = new VanillaPlayerDrawLayer("ArmOverItem", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_28_ArmOverItem), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the player's hand on accessory. </summary>
		// Token: 0x04005FD2 RID: 24530
		public static readonly PlayerDrawLayer HandOnAcc = new VanillaPlayerDrawLayer("HandOnAcc", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_29_OnhandAcc), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the Bladed Glove item, if the player is currently using it. </summary>
		// Token: 0x04005FD3 RID: 24531
		public static readonly PlayerDrawLayer BladedGlove = new VanillaPlayerDrawLayer("BladedGlove", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_30_BladedGlove), PlayerDrawLayers.TorsoGroup, false, null, null);

		/// <summary> Draws the player's held projectile, if it should be drawn in front of the held item and arms. </summary>
		// Token: 0x04005FD4 RID: 24532
		public static readonly PlayerDrawLayer ProjectileOverArm = new VanillaPlayerDrawLayer("ProjectileOverArm", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_31_ProjectileOverArm), null, false, null, null);

		/// <summary> Draws the front textures of either Frozen or Webbed debuffs, if the player has one of them. </summary>
		// Token: 0x04005FD5 RID: 24533
		public static readonly PlayerDrawLayer FrozenOrWebbedDebuff = new VanillaPlayerDrawLayer("FrozenOrWebbedDebuff", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_33_FrozenOrWebbedDebuff), null, false, null, null);

		/// <summary> Draws the front textures of the Electrified debuff, if the player has it. </summary>
		// Token: 0x04005FD6 RID: 24534
		public static readonly PlayerDrawLayer ElectrifiedDebuffFront = new VanillaPlayerDrawLayer("ElectrifiedDebuffFront", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_34_ElectrifiedDebuffFront), null, false, null, null);

		/// <summary> Draws the textures of the Ice Barrier buff, if the player has it. </summary>
		// Token: 0x04005FD7 RID: 24535
		public static readonly PlayerDrawLayer IceBarrier = new VanillaPlayerDrawLayer("IceBarrier", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_35_IceBarrier), null, false, null, null);

		/// <summary> Draws a big gem above the player, if the player is currently in possession of a 'Capture The Gem' gem item. </summary>
		// Token: 0x04005FD8 RID: 24536
		public static readonly PlayerDrawLayer CaptureTheGem = new VanillaPlayerDrawLayer("CaptureTheGem", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_36_CTG), null, false, null, null);

		/// <summary> Draws the effects of Beetle Armor's Set buffs, if the player currently has any. </summary>
		// Token: 0x04005FD9 RID: 24537
		public static readonly PlayerDrawLayer BeetleBuff = new VanillaPlayerDrawLayer("BeetleBuff", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_37_BeetleBuff), null, false, null, null);

		/// <summary> Draws the effects of Eyebrella Cloud, if the player currently has it. </summary>
		// Token: 0x04005FDA RID: 24538
		public static readonly PlayerDrawLayer EyebrellaCloud = new VanillaPlayerDrawLayer("EyebrellaCloud", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_38_EyebrellaCloud), null, false, null, null);

		/// <summary> Draws the front part of player's front accessory. </summary>
		// Token: 0x04005FDB RID: 24539
		public static readonly PlayerDrawLayer FrontAccFront = new VanillaPlayerDrawLayer("FrontAccFront", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_32_FrontAcc_FrontPart), null, false, null, new PlayerDrawLayer.Multiple
		{
			{
				new PlayerDrawLayer.Between(PlayerDrawLayers.FaceAcc, PlayerDrawLayers.MountFront),
				(PlayerDrawSet drawinfo) => drawinfo.drawFrontAccInNeckAccLayer
			},
			{
				new PlayerDrawLayer.Between(PlayerDrawLayers.BladedGlove, PlayerDrawLayers.ProjectileOverArm),
				(PlayerDrawSet drawinfo) => !drawinfo.drawFrontAccInNeckAccLayer
			}
		});

		/// <summary> Draws the player's held item. </summary>
		// Token: 0x04005FDC RID: 24540
		public static readonly PlayerDrawLayer HeldItem = new VanillaPlayerDrawLayer("HeldItem", new VanillaPlayerDrawLayer.DrawFunc(PlayerDrawLayers.DrawPlayer_27_HeldItem), null, false, null, new PlayerDrawLayer.Multiple
		{
			{
				new PlayerDrawLayer.Between(PlayerDrawLayers.BalloonAcc, PlayerDrawLayers.Skin),
				(PlayerDrawSet drawinfo) => drawinfo.weaponDrawOrder == WeaponDrawOrder.BehindBackArm
			},
			{
				new PlayerDrawLayer.Between(PlayerDrawLayers.SolarShield, PlayerDrawLayers.ArmOverItem),
				(PlayerDrawSet drawinfo) => drawinfo.weaponDrawOrder == WeaponDrawOrder.BehindFrontArm
			},
			{
				new PlayerDrawLayer.Between(PlayerDrawLayers.BladedGlove, PlayerDrawLayers.ProjectileOverArm),
				(PlayerDrawSet drawinfo) => drawinfo.weaponDrawOrder == WeaponDrawOrder.OverFrontArm
			}
		});

		// Token: 0x04005FDD RID: 24541
		internal static IReadOnlyList<PlayerDrawLayer> VanillaLayers = PlayerDrawLayers.FixedVanillaLayers.Concat(new PlayerDrawLayer[]
		{
			PlayerDrawLayers.FrontAccFront,
			PlayerDrawLayers.HeldItem
		}).ToArray<PlayerDrawLayer>();

		// Token: 0x02000D5C RID: 3420
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007B8A RID: 31626
			public static Comparison<DrawData> <0>__CompareDrawSorterByYScale;
		}
	}
}
