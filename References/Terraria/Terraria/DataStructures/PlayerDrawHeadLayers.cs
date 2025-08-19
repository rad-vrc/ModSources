using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.ID;

namespace Terraria.DataStructures
{
	// Token: 0x02000455 RID: 1109
	public static class PlayerDrawHeadLayers
	{
		// Token: 0x06002C6B RID: 11371 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public static void DrawPlayer_0_(ref PlayerDrawHeadSet drawinfo)
		{
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x005B9210 File Offset: 0x005B7410
		public static void DrawPlayer_00_BackHelmet(ref PlayerDrawHeadSet drawinfo)
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
			Rectangle hairFrame = drawinfo.HairFrame;
			PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cHead, TextureAssets.ArmorHead[num].Value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(hairFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x005B933C File Offset: 0x005B753C
		public static void DrawPlayer_01_FaceSkin(ref PlayerDrawHeadSet drawinfo)
		{
			bool flag = drawinfo.drawPlayer.head == 38 || drawinfo.drawPlayer.head == 135 || drawinfo.drawPlayer.head == 269;
			if (!flag && drawinfo.drawPlayer.faceHead > 0 && drawinfo.drawPlayer.faceHead < ArmorIDs.Face.Count)
			{
				Vector2 faceHeadOffsetFromHelmet = drawinfo.drawPlayer.GetFaceHeadOffsetFromHelmet();
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cFaceHead, TextureAssets.AccFace[(int)drawinfo.drawPlayer.faceHead].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + faceHeadOffsetFromHelmet, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				if (drawinfo.drawPlayer.face > 0 && drawinfo.drawPlayer.face < ArmorIDs.Face.Count && ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int)drawinfo.drawPlayer.face])
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
					PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cFace, TextureAssets.AccFace[(int)drawinfo.drawPlayer.face].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))) + num, drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
					return;
				}
			}
			else if (!flag && !drawinfo.drawPlayer.isHatRackDoll)
			{
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.skinDyePacked, TextureAssets.Players[drawinfo.skinVar, 0].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, TextureAssets.Players[drawinfo.skinVar, 1].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorEyeWhites, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, TextureAssets.Players[drawinfo.skinVar, 2].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorEyes, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				if (drawinfo.drawPlayer.yoraiz0rDarkness)
				{
					PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.skinDyePacked, TextureAssets.Extra[67].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				}
				if (drawinfo.drawPlayer.face > 0 && drawinfo.drawPlayer.face < ArmorIDs.Face.Count && ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int)drawinfo.drawPlayer.face])
				{
					PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cFace, TextureAssets.AccFace[(int)drawinfo.drawPlayer.face].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				}
			}
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x005B9AC0 File Offset: 0x005B7CC0
		public static void DrawPlayer_02_DrawArmorWithFullHair(ref PlayerDrawHeadSet drawinfo)
		{
			if (drawinfo.fullHair)
			{
				Color color = drawinfo.colorArmorHead;
				int shaderTechnique = drawinfo.cHead;
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
					shaderTechnique = drawinfo.skinDyePacked;
				}
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, shaderTechnique, TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.HairFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				if (!drawinfo.hideHair)
				{
					Rectangle hairFrame = drawinfo.HairFrame;
					hairFrame.Y -= 336;
					if (hairFrame.Y < 0)
					{
						hairFrame.Y = 0;
					}
					PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.hairShaderPacked, TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + drawinfo.hairOffset, new Rectangle?(hairFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				}
			}
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x005B9D20 File Offset: 0x005B7F20
		public static void DrawPlayer_03_HelmetHair(ref PlayerDrawHeadSet drawinfo)
		{
			if (drawinfo.hideHair)
			{
				return;
			}
			if (drawinfo.hatHair)
			{
				Rectangle hairFrame = drawinfo.HairFrame;
				hairFrame.Y -= 336;
				if (hairFrame.Y < 0)
				{
					hairFrame.Y = 0;
				}
				if (!drawinfo.drawPlayer.invis)
				{
					PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.hairShaderPacked, TextureAssets.PlayerHairAlt[drawinfo.drawPlayer.hair].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(hairFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				}
			}
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x005B9E58 File Offset: 0x005B8058
		public static void DrawPlayer_04_CapricornMask(ref PlayerDrawHeadSet drawinfo)
		{
			Rectangle hairFrame = drawinfo.HairFrame;
			hairFrame.Width += 2;
			PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cHead, TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(hairFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x005B9F64 File Offset: 0x005B8164
		public static void DrawPlayer_04_RabbitOrder(ref PlayerDrawHeadSet drawinfo)
		{
			int verticalFrames = 27;
			Texture2D value = TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value;
			Rectangle rectangle = value.Frame(1, verticalFrames, 0, drawinfo.drawPlayer.rabbitOrderFrame.DisplayFrame, 0, 0);
			Vector2 origin = rectangle.Size() / 2f;
			int usedGravDir = 1;
			Vector2 value2 = PlayerDrawHeadLayers.DrawPlayer_04_GetHatDrawPosition(ref drawinfo, new Vector2(1f, -26f), usedGravDir);
			int hatStacks = PlayerDrawHeadLayers.GetHatStacks(ref drawinfo, 4955);
			float num = 0.05235988f;
			float num2 = num * drawinfo.drawPlayer.position.X % 6.2831855f;
			for (int i = hatStacks - 1; i >= 0; i--)
			{
				float x = Vector2.UnitY.RotatedBy((double)(num2 + num * (float)i), default(Vector2)).X * ((float)i / 30f) * 2f - (float)(i * 2 * drawinfo.drawPlayer.direction);
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cHead, value, value2 + new Vector2(x, (float)(i * -14) * drawinfo.scale), new Rectangle?(rectangle), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, origin, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
			if (!drawinfo.hideHair)
			{
				Rectangle hairFrame = drawinfo.HairFrame;
				hairFrame.Y -= 336;
				if (hairFrame.Y < 0)
				{
					hairFrame.Y = 0;
				}
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.hairShaderPacked, TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + drawinfo.hairOffset, new Rectangle?(hairFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x005BA1D8 File Offset: 0x005B83D8
		public static void DrawPlayer_04_BadgersHat(ref PlayerDrawHeadSet drawinfo)
		{
			int verticalFrames = 6;
			Texture2D value = TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value;
			Rectangle rectangle = value.Frame(1, verticalFrames, 0, drawinfo.drawPlayer.rabbitOrderFrame.DisplayFrame, 0, 0);
			Vector2 origin = rectangle.Size() / 2f;
			int num = 1;
			Vector2 value2 = PlayerDrawHeadLayers.DrawPlayer_04_GetHatDrawPosition(ref drawinfo, new Vector2(0f, -9f), num);
			int hatStacks = PlayerDrawHeadLayers.GetHatStacks(ref drawinfo, 5004);
			float num2 = 0.05235988f;
			float num3 = num2 * drawinfo.drawPlayer.position.X % 6.2831855f;
			int num4 = hatStacks * 4 + 2;
			int num5 = 0;
			bool flag = (Main.GlobalTimeWrappedHourly + 180f) % 600f < 60f;
			for (int i = num4 - 1; i >= 0; i--)
			{
				int num6 = 0;
				if (i == num4 - 1)
				{
					rectangle.Y = 0;
					num6 = 2;
				}
				else if (i == 0)
				{
					rectangle.Y = rectangle.Height * 5;
				}
				else
				{
					rectangle.Y = rectangle.Height * (num5++ % 4 + 1);
				}
				if (rectangle.Y != rectangle.Height * 3 || !flag)
				{
					float x = Vector2.UnitY.RotatedBy((double)(num3 + num2 * (float)i), default(Vector2)).X * ((float)i / 10f) * 4f - (float)i * 0.1f * (float)drawinfo.drawPlayer.direction;
					PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cHead, value, value2 + new Vector2(x, (float)((i * -4 + num6) * num)) * drawinfo.scale, new Rectangle?(rectangle), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, origin, drawinfo.scale, drawinfo.playerEffect, 0f);
				}
			}
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x005BA3BC File Offset: 0x005B85BC
		private static Vector2 DrawPlayer_04_GetHatDrawPosition(ref PlayerDrawHeadSet drawinfo, Vector2 hatOffset, int usedGravDir)
		{
			Vector2 value = new Vector2((float)drawinfo.drawPlayer.direction, (float)usedGravDir);
			return drawinfo.Position - Main.screenPosition + new Vector2((float)(-(float)drawinfo.bodyFrameMemory.Width / 2 + drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.bodyFrameMemory.Height + 4)) + hatOffset * value * drawinfo.scale + (drawinfo.drawPlayer.headPosition + drawinfo.headVect);
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x005BA464 File Offset: 0x005B8664
		private static int GetHatStacks(ref PlayerDrawHeadSet drawinfo, int itemId)
		{
			int num = 0;
			int num2 = 0;
			if (drawinfo.drawPlayer.armor[num2] != null && drawinfo.drawPlayer.armor[num2].type == itemId && drawinfo.drawPlayer.armor[num2].stack > 0)
			{
				num += drawinfo.drawPlayer.armor[num2].stack;
			}
			num2 = 10;
			if (drawinfo.drawPlayer.armor[num2] != null && drawinfo.drawPlayer.armor[num2].type == itemId && drawinfo.drawPlayer.armor[num2].stack > 0)
			{
				num += drawinfo.drawPlayer.armor[num2].stack;
			}
			if (num > 2)
			{
				num = 2;
			}
			return num;
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x005BA51C File Offset: 0x005B871C
		public static void DrawPlayer_04_HatsWithFullHair(ref PlayerDrawHeadSet drawinfo)
		{
			if (drawinfo.drawPlayer.head == 259)
			{
				PlayerDrawHeadLayers.DrawPlayer_04_RabbitOrder(ref drawinfo);
				return;
			}
			if (drawinfo.helmetIsOverFullHair)
			{
				if (!drawinfo.hideHair)
				{
					Rectangle hairFrame = drawinfo.HairFrame;
					hairFrame.Y -= 336;
					if (hairFrame.Y < 0)
					{
						hairFrame.Y = 0;
					}
					PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.hairShaderPacked, TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + drawinfo.hairOffset, new Rectangle?(hairFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				}
				if (drawinfo.drawPlayer.head != 0)
				{
					Color color = drawinfo.colorArmorHead;
					int shaderTechnique = drawinfo.cHead;
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
						shaderTechnique = drawinfo.skinDyePacked;
					}
					PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, shaderTechnique, TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				}
			}
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x005BA7A4 File Offset: 0x005B89A4
		public static void DrawPlayer_05_TallHats(ref PlayerDrawHeadSet drawinfo)
		{
			if (drawinfo.helmetIsTall)
			{
				Rectangle hairFrame = drawinfo.HairFrame;
				if (drawinfo.drawPlayer.head == 158)
				{
					hairFrame.Height -= 2;
				}
				int num = 0;
				if (hairFrame.Y == hairFrame.Height * 6)
				{
					hairFrame.Height -= 2;
				}
				else if (hairFrame.Y == hairFrame.Height * 7)
				{
					num = -2;
				}
				else if (hairFrame.Y == hairFrame.Height * 8)
				{
					num = -2;
				}
				else if (hairFrame.Y == hairFrame.Height * 9)
				{
					num = -2;
				}
				else if (hairFrame.Y == hairFrame.Height * 10)
				{
					num = -2;
				}
				else if (hairFrame.Y == hairFrame.Height * 13)
				{
					hairFrame.Height -= 2;
				}
				else if (hairFrame.Y == hairFrame.Height * 14)
				{
					num = -2;
				}
				else if (hairFrame.Y == hairFrame.Height * 15)
				{
					num = -2;
				}
				else if (hairFrame.Y == hairFrame.Height * 16)
				{
					num = -2;
				}
				hairFrame.Y += num;
				Color color = drawinfo.colorArmorHead;
				int shaderTechnique = drawinfo.cHead;
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
					shaderTechnique = drawinfo.skinDyePacked;
				}
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, shaderTechnique, TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f + (float)num) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(hairFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x005BA9F4 File Offset: 0x005B8BF4
		public static void DrawPlayer_06_NormalHats(ref PlayerDrawHeadSet drawinfo)
		{
			if (drawinfo.drawPlayer.head == 270)
			{
				PlayerDrawHeadLayers.DrawPlayer_04_CapricornMask(ref drawinfo);
				return;
			}
			if (drawinfo.drawPlayer.head == 265)
			{
				PlayerDrawHeadLayers.DrawPlayer_04_BadgersHat(ref drawinfo);
				return;
			}
			if (drawinfo.helmetIsNormal)
			{
				Rectangle hairFrame = drawinfo.HairFrame;
				Color color = drawinfo.colorArmorHead;
				int shaderTechnique = drawinfo.cHead;
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
					shaderTechnique = drawinfo.skinDyePacked;
				}
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, shaderTechnique, TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(hairFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				if (drawinfo.drawPlayer.head == 271)
				{
					int num = PlayerDrawLayers.DrawPlayer_Head_GetTVScreen(drawinfo.drawPlayer);
					if (num != 0)
					{
						Texture2D value = TextureAssets.GlowMask[309].Value;
						int frameY = drawinfo.drawPlayer.miscCounter % 20 / 5;
						if (num == 5)
						{
							frameY = 0;
							if (drawinfo.drawPlayer.eyeHelper.EyeFrameToShow > 0)
							{
								frameY = 2;
							}
						}
						Rectangle value2 = value.Frame(6, 4, num, frameY, -2, 0);
						PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cHead, value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(value2), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
					}
				}
			}
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x005BACBC File Offset: 0x005B8EBC
		public static void DrawPlayer_07_JustHair(ref PlayerDrawHeadSet drawinfo)
		{
			if (!drawinfo.helmetIsNormal && !drawinfo.helmetIsOverFullHair && !drawinfo.helmetIsTall && !drawinfo.hideHair)
			{
				Rectangle hairFrame = drawinfo.HairFrame;
				hairFrame.Y -= 336;
				if (hairFrame.Y < 0)
				{
					hairFrame.Y = 0;
				}
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.hairShaderPacked, TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + drawinfo.hairOffset, new Rectangle?(hairFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x005BAE08 File Offset: 0x005B9008
		public static void DrawPlayer_08_FaceAcc(ref PlayerDrawHeadSet drawinfo)
		{
			if (drawinfo.drawPlayer.beard > 0 && (drawinfo.drawPlayer.head < 0 || !ArmorIDs.Head.Sets.PreventBeardDraw[drawinfo.drawPlayer.head]))
			{
				Vector2 beardDrawOffsetFromHelmet = drawinfo.drawPlayer.GetBeardDrawOffsetFromHelmet();
				Color color = drawinfo.colorArmorHead;
				if (ArmorIDs.Beard.Sets.UseHairColor[(int)drawinfo.drawPlayer.beard])
				{
					color = drawinfo.colorHair;
				}
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cBeard, TextureAssets.AccBeard[(int)drawinfo.drawPlayer.beard].Value, beardDrawOffsetFromHelmet + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
			if (drawinfo.drawPlayer.face > 0 && drawinfo.drawPlayer.face < ArmorIDs.Face.Count && !ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int)drawinfo.drawPlayer.face])
			{
				Vector2 zero = Vector2.Zero;
				if (drawinfo.drawPlayer.face == 19)
				{
					zero = new Vector2(0f, -6f);
				}
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cFace, TextureAssets.AccFace[(int)drawinfo.drawPlayer.face].Value, zero + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
			if (drawinfo.drawPlayer.faceFlower > 0 && drawinfo.drawPlayer.faceFlower < ArmorIDs.Face.Count)
			{
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cFaceFlower, TextureAssets.AccFace[(int)drawinfo.drawPlayer.faceFlower].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
			if (drawinfo.drawUnicornHorn)
			{
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cUnicornHorn, TextureAssets.Extra[143].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
			if (drawinfo.drawAngelHalo)
			{
				Main.instance.LoadAccFace(7);
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cAngelHalo, TextureAssets.AccFace[7].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), new Color(200, 200, 200, 200), drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x005BB3A4 File Offset: 0x005B95A4
		public static void DrawPlayer_RenderAllLayers(ref PlayerDrawHeadSet drawinfo)
		{
			List<DrawData> drawData = drawinfo.DrawData;
			Effect pixelShader = Main.pixelShader;
			Projectile[] projectile = Main.projectile;
			SpriteBatch spriteBatch = Main.spriteBatch;
			for (int i = 0; i < drawData.Count; i++)
			{
				DrawData drawData2 = drawData[i];
				if (drawData2.sourceRect == null)
				{
					drawData2.sourceRect = new Rectangle?(drawData2.texture.Frame(1, 1, 0, 0, 0, 0));
				}
				PlayerDrawHelper.SetShaderForData(drawinfo.drawPlayer, drawinfo.cHead, ref drawData2);
				if (drawData2.texture != null)
				{
					drawData2.Draw(spriteBatch);
				}
			}
			pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x005BB44C File Offset: 0x005B964C
		public static void DrawPlayer_DrawSelectionRect(ref PlayerDrawHeadSet drawinfo)
		{
			Vector2 value;
			Vector2 value2;
			SpriteRenderTargetHelper.GetDrawBoundary(drawinfo.DrawData, out value, out value2);
			Utils.DrawRect(Main.spriteBatch, value + Main.screenPosition, value2 + Main.screenPosition, Color.White);
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x005BB490 File Offset: 0x005B9690
		public static void QuickCDD(List<DrawData> drawData, Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
		{
			drawData.Add(new DrawData(texture, position, sourceRectangle, color, rotation, origin, scale, effects, 0f));
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x005BB4BC File Offset: 0x005B96BC
		public static void QuickCDD(List<DrawData> drawData, int shaderTechnique, Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
		{
			drawData.Add(new DrawData(texture, position, sourceRectangle, color, rotation, origin, scale, effects, 0f)
			{
				shader = shaderTechnique
			});
		}
	}
}
