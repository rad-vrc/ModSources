using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent.Animations;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Skies.CreditsRoll
{
	// Token: 0x02000571 RID: 1393
	public class CreditsRollComposer
	{
		// Token: 0x060041B5 RID: 16821 RVA: 0x005E98D0 File Offset: 0x005E7AD0
		public void FillSegments_Test(List<IAnimationSegment> segmentsList, out int endTime)
		{
			this._segments = segmentsList;
			int num = 0;
			int num2 = 80;
			Vector2 sceneAnchorPosition = Vector2.UnitY * -1f * (float)num2;
			num += this.PlaySegment_PrincessAndEveryoneThanksPlayer(num, sceneAnchorPosition).totalTime;
			this._endTime = num + 20;
			endTime = this._endTime;
		}

		// Token: 0x060041B6 RID: 16822 RVA: 0x005E9924 File Offset: 0x005E7B24
		public void FillSegments(List<IAnimationSegment> segmentsList, out int endTime, bool inGame)
		{
			this._segments = segmentsList;
			int num = 0;
			int num2 = 80;
			Vector2 vector4 = Vector2.UnitY * -1f * (float)num2;
			int num3 = 210;
			Vector2 vector2 = vector4 + Vector2.UnitX * 200f;
			Vector2 vector3 = vector2;
			if (!inGame)
			{
				vector2 = (vector3 = Vector2.UnitY * 80f);
			}
			int num4 = num3 * 3;
			int num5 = num3 * 3;
			int num6 = num4 - num5;
			if (!inGame)
			{
				num5 = 180;
				num6 = num4 - num5;
			}
			num += num5;
			num += this.PlaySegment_TextRoll(num, "CreditsRollCategory_Creator", vector2).totalTime;
			num += num3;
			num += this.PlaySegment_Grox_GuideRunningFromZombie(num, vector2).totalTime;
			num += num3;
			vector2.X *= -1f;
			num += this.PlaySegment_TextRoll(num, "CreditsRollCategory_ExecutiveProducer", vector2).totalTime;
			num += num3;
			num += this.PlaySegment_Grox_MerchantAndTravelingMerchantTryingToSellJunk(num, vector2).totalTime;
			num += num3;
			vector2.X *= -1f;
			num += this.PlaySegment_TextRoll(num, "CreditsRollCategory_Designer", vector2).totalTime;
			num += num3;
			num += this.PlaySegment_Grox_DemolitionistAndArmsDealerArguingThenNurseComes(num, vector2).totalTime;
			num += num3;
			vector2.X *= -1f;
			num += this.PlaySegment_TextRoll(num, "CreditsRollCategory_Programming", vector2).totalTime;
			num += num3;
			num += this.PlaySegment_TinkererAndMechanic(num, vector2).totalTime;
			num += num3;
			vector2.X *= 0f;
			num += this.PlaySegment_TextRoll(num, "CreditsRollCategory_Graphics", vector2).totalTime;
			num += num3;
			num += this.PlaySegment_Grox_DryadSayingByeToTavernKeep(num, vector2).totalTime;
			num += num3;
			vector2 = vector3;
			vector2.X *= -1f;
			num += this.PlaySegment_TextRoll(num, "CreditsRollCategory_Music", vector2).totalTime;
			num += num3;
			num += this.PlaySegment_Grox_WizardPartyGirlDyeTraderAndPainterPartyWithBunnies(num, vector2).totalTime;
			num += num3;
			vector2.X *= -1f;
			num += this.PlaySegment_TextRoll(num, "CreditsRollCategory_Sound", vector2).totalTime;
			num += num3;
			num += this.PlaySegment_ClothierChasingTruffle(num, vector2).totalTime;
			num += num3;
			vector2.X *= -1f;
			num += this.PlaySegment_TextRoll(num, "CreditsRollCategory_Dialog", vector2).totalTime;
			num += num3;
			num += this.PlaySegment_Grox_AnglerAndPirateTalkAboutFish(num, vector2).totalTime;
			num += num3;
			vector2.X *= 0f;
			num += this.PlaySegment_TextRoll(num, "CreditsRollCategory_QualityAssurance", vector2).totalTime;
			num += num3;
			num += this.PlaySegment_Grox_ZoologistAndPetsAnnoyGolfer(num, vector2).totalTime;
			num += num3;
			vector2 = vector3;
			vector2.X *= -1f;
			num += this.PlaySegment_TextRoll(num, "CreditsRollCategory_BusinessDevelopment", vector2).totalTime;
			num += num3;
			num += this.PlaySegment_Grox_SkeletonMerchantSearchesThroughBones(num, vector2).totalTime;
			num += num3;
			vector2.X *= -1f;
			num += this.PlaySegment_TextRoll(num, "CreditsRollCategory_Marketing", vector2).totalTime;
			num += num3;
			num += this.PlaySegment_DryadTurningToTree(num, vector2).totalTime;
			num += num3;
			vector2.X *= -1f;
			num += this.PlaySegment_TextRoll(num, "CreditsRollCategory_PublicRelations", vector2).totalTime;
			num += num3;
			num += this.PlaySegment_Grox_SteampunkerRepairingCyborg(num, vector2).totalTime;
			num += num3;
			vector2.X *= 0f;
			num += this.PlaySegment_TextRoll(num, "CreditsRollCategory_Webmaster", vector2).totalTime;
			num += num3;
			num += this.PlaySegment_Grox_SantaAndTaxCollectorThrowingPresents(num, vector2).totalTime;
			num += num3;
			num += this.PlaySegment_TextRoll(num, "CreditsRollCategory_Playtesting", vector2).totalTime;
			num += num3;
			num += this.PlaySegment_Grox_WitchDoctorGoingToHisPeople(num, vector2).totalTime;
			num += num3;
			num += this.PlaySegment_TextRoll(num, "CreditsRollCategory_SpecialThanksto", vector2).totalTime;
			num += num3;
			num += this.PlaySegment_PrincessAndEveryoneThanksPlayer(num, vector2).totalTime;
			num += num3;
			num += this.PlaySegment_TextRoll(num, "CreditsRollCategory_EndingNotes", vector2).totalTime;
			num += num6;
			this._endTime = num + 10;
			endTime = this._endTime;
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x005E9D44 File Offset: 0x005E7F44
		private SegmentInforReport PlaySegment_PrincessAndEveryoneThanksPlayer(int startTime, Vector2 sceneAnchorPosition)
		{
			sceneAnchorPosition.Y += 40f;
			int num = -2;
			int num2 = 2;
			List<int> list = new List<int>
			{
				228,
				178,
				550,
				208,
				160,
				209
			};
			List<int> list2 = new List<int>
			{
				353,
				633,
				207,
				588,
				227,
				368
			};
			List<int> list3 = new List<int>
			{
				22,
				19,
				18,
				17,
				38,
				54,
				108
			};
			List<int> list4 = new List<int>
			{
				663,
				20,
				441,
				107,
				124,
				229,
				369
			};
			List<CreditsRollComposer.SimplifiedNPCInfo> list5 = new List<CreditsRollComposer.SimplifiedNPCInfo>();
			for (int i = 0; i < list.Count; i++)
			{
				int npcType = list[i];
				list5.Add(new CreditsRollComposer.SimplifiedNPCInfo(npcType, new Vector2((float)(num - i), -1f)));
			}
			for (int j = 0; j < list3.Count; j++)
			{
				int npcType2 = list3[j];
				list5.Add(new CreditsRollComposer.SimplifiedNPCInfo(npcType2, new Vector2((float)(num - j) + 0.5f, 0f)));
			}
			for (int k = 0; k < list2.Count; k++)
			{
				int npcType3 = list2[k];
				list5.Add(new CreditsRollComposer.SimplifiedNPCInfo(npcType3, new Vector2((float)(num2 + k), -1f)));
			}
			for (int l = 0; l < list4.Count; l++)
			{
				int npcType4 = list4[l];
				list5.Add(new CreditsRollComposer.SimplifiedNPCInfo(npcType4, new Vector2((float)(num2 + l) - 0.5f, 0f)));
			}
			int num3 = 240;
			int num4 = 400;
			int num5 = num4 + num3;
			Asset<Texture2D> asset = TextureAssets.Extra[241];
			Rectangle rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 1f) + new Vector2(0f, -92f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> item = new Segments.SpriteSegment(asset, startTime, data, sceneAnchorPosition).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 51)).Then(new Actions.Sprites.Wait(num5)).Then(new Actions.Sprites.Fade(0f, 85));
			this._segments.Add(item);
			foreach (CreditsRollComposer.SimplifiedNPCInfo item2 in list5)
			{
				item2.SpawnNPC(new CreditsRollComposer.AddNPCMethod(this.AddWavingNPC), sceneAnchorPosition, startTime, num5);
			}
			float num6 = 3f;
			float num7 = -0.05f;
			int num8 = 60;
			float num9 = num6 * (float)num8 + num7 * ((float)(num8 * num8) * 0.5f);
			int num11 = startTime + num3;
			int num10 = 51;
			Segments.AnimationSegmentWithActions<Player> item3 = new Segments.PlayerSegment(num11 - num8 + num10, sceneAnchorPosition + new Vector2(0f, 0f - num9), this._originAtBottom).UseShaderEffect(new Segments.PlayerSegment.ImmediateSpritebatchForPlayerDyesEffect()).Then(new Actions.Players.Fade(0f)).With(new Actions.Players.LookAt(1)).With(new Actions.Players.Fade(1f, num8)).Then(new Actions.Players.Wait(num4 / 2)).With(new Actions.Players.MoveWithAcceleration(new Vector2(0f, num6), new Vector2(0f, num7), num8)).Then(new Actions.Players.Wait(num4 / 2 - 60)).With(new Actions.Players.LookAt(-1)).Then(new Actions.Players.Wait(120)).With(new Actions.Players.LookAt(1)).Then(new Actions.Players.Fade(0f, 85));
			this._segments.Add(item3);
			return new SegmentInforReport
			{
				totalTime = num5 + 85 + 60
			};
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x005EA1F0 File Offset: 0x005E83F0
		private void AddWavingNPC(int npcType, Vector2 sceneAnchoePosition, int lookDirection, int fromTime, int duration, int timeToJumpAt)
		{
			int num = 0;
			float num2 = 4f;
			float num3 = 0.2f;
			float num4 = num2 * 2f / num3;
			int num5 = NPCID.Sets.AttackType[npcType] * 6 + npcType % 13 * 2 + 20;
			int num6 = 0;
			if (npcType % 7 != 0)
			{
				num6 = 0;
			}
			bool flag2 = npcType == 663 || npcType == 108;
			bool flag = false;
			if (flag2)
			{
				num6 = 180;
			}
			int num7 = 240;
			int num8 = lookDirection;
			int num9 = -1;
			int duration2 = 0;
			if (npcType <= 227)
			{
				if (npcType != 54 && npcType != 107 && npcType != 227)
				{
					goto IL_B0;
				}
			}
			else if (npcType <= 353)
			{
				if (npcType != 229 && npcType != 353)
				{
					goto IL_B0;
				}
			}
			else if (npcType != 550 && npcType != 663)
			{
				goto IL_B0;
			}
			num8 *= -1;
			IL_B0:
			if (npcType - 207 <= 2 || npcType == 228 || npcType - 368 <= 1)
			{
				flag = true;
			}
			if (npcType <= 208)
			{
				if (npcType != 54)
				{
					if (npcType != 107)
					{
						if (npcType == 208)
						{
							num9 = 127;
						}
					}
					else
					{
						num9 = 0;
					}
				}
				else
				{
					num9 = 126;
				}
			}
			else if (npcType != 229)
			{
				if (npcType != 353)
				{
					if (npcType == 368)
					{
						num9 = 15;
					}
				}
				else
				{
					num9 = 136;
				}
			}
			else
			{
				num9 = 85;
			}
			if (num9 != -1)
			{
				duration2 = npcType % 6 * 20 + 60;
			}
			int num10 = duration - timeToJumpAt - num - num7;
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions = new Segments.NPCSegment(fromTime, npcType, sceneAnchoePosition, this._originAtBottom).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).With(new Actions.NPCs.LookAt(num8));
			if (flag)
			{
				animationSegmentWithActions.With(new Actions.NPCs.PartyHard());
			}
			animationSegmentWithActions.Then(new Actions.NPCs.Wait(num7)).Then(new Actions.NPCs.LookAt(lookDirection)).Then(new Actions.NPCs.Wait(timeToJumpAt)).Then(new Actions.NPCs.MoveWithAcceleration(new Vector2(0f, 0f - num2), new Vector2(0f, num3), (int)num4)).With(new Actions.NPCs.Move(new Vector2(0f, 1E-05f), (int)num4)).Then(new Actions.NPCs.Wait(num10 - 90 + num5)).Then(new Actions.NPCs.Wait(90 - num5));
			if (num6 > 0)
			{
				animationSegmentWithActions.With(new Actions.NPCs.Blink(num6));
			}
			animationSegmentWithActions.Then(new Actions.NPCs.Fade(3, 85));
			if (npcType == 663)
			{
				this.AddEmote(sceneAnchoePosition, fromTime, duration, num5, 17, lookDirection);
			}
			if (num9 != -1)
			{
				this.AddEmote(sceneAnchoePosition, fromTime, duration2, 0, num9, num8);
			}
			this._segments.Add(animationSegmentWithActions);
		}

		// Token: 0x060041B9 RID: 16825 RVA: 0x005EA478 File Offset: 0x005E8678
		private void AddEmote(Vector2 sceneAnchoePosition, int fromTime, int duration, int blinkTime, int emoteId, int direction)
		{
			Segments.EmoteSegment item = new Segments.EmoteSegment(emoteId, fromTime + duration - blinkTime, 60, sceneAnchoePosition + this._emoteBubbleOffsetWhenOnRight, (direction == 1) ? 1 : 0, default(Vector2));
			this._segments.Add(item);
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x005EA4C0 File Offset: 0x005E86C0
		private SegmentInforReport PlaySegment_TextRoll(int startTime, string sourceCategory, Vector2 anchorOffset = default(Vector2))
		{
			anchorOffset.Y -= 40f;
			int num = 80;
			LocalizedText[] array = Language.FindAll(Lang.CreateDialogFilter(sourceCategory + ".", null));
			for (int i = 0; i < array.Length; i++)
			{
				this._segments.Add(new Segments.LocalizedTextSegment((float)(startTime + i * num), array[i], anchorOffset));
			}
			return new SegmentInforReport
			{
				totalTime = array.Length * num + num * -1
			};
		}

		// Token: 0x060041BB RID: 16827 RVA: 0x005EA538 File Offset: 0x005E8738
		private SegmentInforReport PlaySegment_GuideEmotingAtRainbowPanel(int startTime)
		{
			Asset<Texture2D> asset = TextureAssets.Extra[156];
			DrawData data = new DrawData(asset.Value, Vector2.Zero, null, Color.White, 0f, asset.Size() / 2f, 0.25f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions = new Segments.SpriteSegment(asset, startTime, data, new Vector2(0f, -60f)).Then(new Actions.Sprites.Fade(0f, 0)).Then(new Actions.Sprites.Fade(1f, 60)).Then(new Actions.Sprites.Wait(60)).Then(new Actions.Sprites.Fade(0f, 60));
			this._segments.Add(animationSegmentWithActions);
			return new SegmentInforReport
			{
				totalTime = (int)animationSegmentWithActions.DedicatedTimeNeeded
			};
		}

		// Token: 0x060041BC RID: 16828 RVA: 0x005EA60C File Offset: 0x005E880C
		private SegmentInforReport PlaySegment_Grox_DryadSayingByeToTavernKeep(int startTime, Vector2 sceneAnchorPosition)
		{
			sceneAnchorPosition += this.GetSceneFixVector();
			int num2 = 0;
			sceneAnchorPosition.X += (float)num2;
			int num3 = 30;
			int num4 = 10;
			Asset<Texture2D> asset = TextureAssets.Extra[235];
			Rectangle rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 1f) + new Vector2((float)num2, -42f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions = new Segments.SpriteSegment(asset, startTime, data, sceneAnchorPosition + new Vector2((float)num4, 0f) + this._backgroundOffset).UseShaderEffect(new Segments.SpriteSegment.MaskedFadeEffect(null, "MaskedFade", 1, 1)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 60)).Then(new Actions.Sprites.Wait(120));
			this._segments.Add(animationSegmentWithActions);
			int num5 = 300;
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions2 = new Segments.NPCSegment(startTime, 20, sceneAnchorPosition + new Vector2((float)(num4 + num5), 0f), this._originAtBottom).Then(new Actions.NPCs.LookAt(1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(-1f, 0f), 120));
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions3 = new Segments.NPCSegment(startTime, 550, sceneAnchorPosition + new Vector2((float)(num4 + num5 - 16 - num3), 0f), this._originAtBottom).Then(new Actions.NPCs.LookAt(1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(-1f, 0f), 120));
			Asset<Texture2D> asset2 = TextureAssets.Extra[240];
			Rectangle rectangle2 = asset2.Frame(1, 8, 0, 0, 0, 0);
			DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(rectangle2), Color.White, 0f, rectangle2.Size() * new Vector2(0.5f, 1f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions4 = new Segments.SpriteSegment(asset2, startTime, data2, sceneAnchorPosition + new Vector2((float)num4, 2f)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 51));
			int num6 = startTime + (int)animationSegmentWithActions3.DedicatedTimeNeeded;
			int num7 = 90;
			animationSegmentWithActions3.Then(new Actions.NPCs.Move(new Vector2(-1f, 0f), 90));
			animationSegmentWithActions2.Then(new Actions.NPCs.Move(new Vector2(-1f, 0f), 30));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(60));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(90));
			num6 += 90;
			int num8 = num7 * 5;
			int num9 = num4 + num5 - 120 - 30;
			int num10 = num4 + num5 - 120 - 106 - num3;
			Segments.EmoteSegment item = new Segments.EmoteSegment(14, num6, num7, sceneAnchorPosition + new Vector2((float)num9, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			Segments.EmoteSegment item2 = new Segments.EmoteSegment(133, num6 + num7, num7, sceneAnchorPosition + new Vector2((float)num10, 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			Segments.EmoteSegment item3 = new Segments.EmoteSegment(78, num6 + num7 * 2, num7, sceneAnchorPosition + new Vector2((float)num9, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			Segments.EmoteSegment item4 = new Segments.EmoteSegment(15, num6 + num7 * 4, num7, sceneAnchorPosition + new Vector2((float)num10, 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			Segments.EmoteSegment item5 = new Segments.EmoteSegment(15, num6 + num7 * 4, num7, sceneAnchorPosition + new Vector2((float)num9, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			animationSegmentWithActions3.Then(new Actions.NPCs.LookAt(1));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num7 * 3));
			animationSegmentWithActions3.Then(new Actions.NPCs.ShowItem(num7, 353));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num7));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num8));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num8));
			num6 += num8;
			animationSegmentWithActions3.Then(new Actions.NPCs.Move(new Vector2(-1f, 0f), 30));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(30));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(30));
			num6 += 30;
			Main.instance.LoadNPC(550);
			Asset<Texture2D> asset3 = TextureAssets.Npc[550];
			Rectangle rectangle3 = asset3.Frame(1, Main.npcFrameCount[550], 0, 0, 0, 0);
			DrawData data3 = new DrawData(asset3.Value, Vector2.Zero, new Rectangle?(rectangle3), Color.White, 0f, rectangle3.Size() * new Vector2(0.5f, 1f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions5 = new Segments.SpriteSegment(asset3, num6, data3, sceneAnchorPosition + new Vector2((float)(num10 - 30), 2f)).Then(new Actions.Sprites.Fade(1f));
			animationSegmentWithActions5.Then(new Actions.Sprites.SimulateGravity(new Vector2(-0.2f, -0.35f), Vector2.Zero, 0f, 80)).With(new Actions.Sprites.SetFrameSequence(80, new Point[]
			{
				new Point(0, 2),
				new Point(0, 3),
				new Point(0, 4),
				new Point(0, 5),
				new Point(0, 6),
				new Point(0, 7),
				new Point(0, 8),
				new Point(0, 9),
				new Point(0, 10),
				new Point(0, 11),
				new Point(0, 12),
				new Point(0, 13),
				new Point(0, 14)
			}, 4, 0, 0)).With(new Actions.Sprites.Fade(0f, 85));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(80));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(80));
			num6 += 80;
			animationSegmentWithActions4.Then(new Actions.Sprites.SetFrameSequence(num6 - startTime, new Point[]
			{
				new Point(0, 0),
				new Point(0, 1),
				new Point(0, 2),
				new Point(0, 3),
				new Point(0, 4),
				new Point(0, 5),
				new Point(0, 6),
				new Point(0, 7)
			}, 5, 0, 0));
			Segments.EmoteSegment item6 = new Segments.EmoteSegment(10, num6, num7, sceneAnchorPosition + new Vector2((float)num9, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			animationSegmentWithActions4.Then(new Actions.Sprites.Fade(0f, num7 - 30));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num7));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num7));
			animationSegmentWithActions2.Then(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions.Then(new Actions.Sprites.Fade(0f, 127));
			num6 += 187;
			this._segments.Add(animationSegmentWithActions4);
			this._segments.Add(animationSegmentWithActions3);
			this._segments.Add(animationSegmentWithActions5);
			this._segments.Add(animationSegmentWithActions2);
			this._segments.Add(item);
			this._segments.Add(item2);
			this._segments.Add(item3);
			this._segments.Add(item5);
			this._segments.Add(item4);
			this._segments.Add(item6);
			return new SegmentInforReport
			{
				totalTime = num6 - startTime
			};
		}

		// Token: 0x060041BD RID: 16829 RVA: 0x005EAE98 File Offset: 0x005E9098
		private SegmentInforReport PlaySegment_Grox_SteampunkerRepairingCyborg(int startTime, Vector2 sceneAnchorPosition)
		{
			sceneAnchorPosition += this.GetSceneFixVector();
			int num2 = 30;
			sceneAnchorPosition.X += (float)num2;
			int num3 = 60;
			Asset<Texture2D> asset = TextureAssets.Extra[232];
			Rectangle rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 1f) + new Vector2((float)num2, -42f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions = new Segments.SpriteSegment(asset, startTime, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect(new Segments.SpriteSegment.MaskedFadeEffect(null, "MaskedFade", 1, 1)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 60)).Then(new Actions.Sprites.Wait(60));
			this._segments.Add(animationSegmentWithActions);
			asset = TextureAssets.Extra[233];
			rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 1f) + new Vector2((float)num2, -42f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions2 = new Segments.SpriteSegment(asset, startTime, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect(new Segments.SpriteSegment.MaskedFadeEffect(null, "MaskedFade", 1, 1)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 60)).Then(new Actions.Sprites.Wait(60));
			this._segments.Add(animationSegmentWithActions);
			this._segments.Add(animationSegmentWithActions2);
			Asset<Texture2D> asset2 = TextureAssets.Extra[230];
			Rectangle rectangle2 = asset2.Frame(1, 21, 0, 0, 0, 0);
			DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(rectangle2), Color.White, 0f, rectangle2.Size() * new Vector2(0.5f, 1f), 1f, 0, 0f);
			Segments.SpriteSegment spriteSegment = new Segments.SpriteSegment(asset2, startTime, data2, sceneAnchorPosition + new Vector2(0f, 4f));
			spriteSegment.Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 60)).Then(new Actions.Sprites.Wait(60));
			Asset<Texture2D> asset3 = TextureAssets.Extra[229];
			Rectangle rectangle3 = asset3.Frame(1, 2, 0, 0, 0, 0);
			DrawData data3 = new DrawData(asset3.Value, Vector2.Zero, new Rectangle?(rectangle3), Color.White, 0f, rectangle3.Size() * new Vector2(0.5f, 1f), 1f, 0, 0f);
			Segments.SpriteSegment spriteSegment2 = new Segments.SpriteSegment(asset3, startTime, data3, sceneAnchorPosition + new Vector2((float)num3, 4f));
			spriteSegment2.Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 60)).Then(new Actions.Sprites.Wait(60));
			int num4 = startTime + (int)spriteSegment.DedicatedTimeNeeded;
			int num5 = 120;
			spriteSegment.Then(new Actions.Sprites.SetFrameSequence(num5, new Point[]
			{
				new Point(0, 0),
				new Point(0, 1)
			}, 10, 0, 0));
			spriteSegment2.Then(new Actions.Sprites.Wait(num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			animationSegmentWithActions2.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			Point[] array = new Point[]
			{
				new Point(0, 2),
				new Point(0, 3),
				new Point(0, 4),
				new Point(0, 5),
				new Point(0, 6),
				new Point(0, 7),
				new Point(0, 8),
				new Point(0, 9),
				new Point(0, 10),
				new Point(0, 11),
				new Point(0, 12),
				new Point(0, 13),
				new Point(0, 14),
				new Point(0, 15),
				new Point(0, 16),
				new Point(0, 17),
				new Point(0, 18),
				new Point(0, 19),
				new Point(0, 20),
				new Point(0, 15),
				new Point(0, 16),
				new Point(0, 17),
				new Point(0, 18),
				new Point(0, 19),
				new Point(0, 20),
				new Point(0, 17),
				new Point(0, 18),
				new Point(0, 19),
				new Point(0, 20)
			};
			int num6 = 6;
			int num7 = num6 * array.Length;
			spriteSegment.Then(new Actions.Sprites.SetFrameSequence(array, num6, 0, 0));
			int durationInFrames = num7 / 2;
			spriteSegment2.Then(new Actions.Sprites.Wait(durationInFrames));
			spriteSegment2.Then(new Actions.Sprites.SetFrame(0, 1, 0, 0));
			spriteSegment2.Then(new Actions.Sprites.Wait(durationInFrames));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num7));
			animationSegmentWithActions2.Then(new Actions.Sprites.Wait(num7));
			num4 += num7;
			array = new Point[]
			{
				new Point(0, 17),
				new Point(0, 18),
				new Point(0, 19),
				new Point(0, 20)
			};
			spriteSegment.Then(new Actions.Sprites.SetFrameSequence(187, array, num6, 0, 0)).With(new Actions.Sprites.Fade(0f, 127));
			spriteSegment2.Then(new Actions.Sprites.Fade(0f, 127));
			animationSegmentWithActions.Then(new Actions.Sprites.Fade(0f, 127));
			num4 += 187;
			this._segments.Add(spriteSegment);
			this._segments.Add(spriteSegment2);
			return new SegmentInforReport
			{
				totalTime = num4 - startTime
			};
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x005EB56C File Offset: 0x005E976C
		private SegmentInforReport PlaySegment_Grox_SantaAndTaxCollectorThrowingPresents(int startTime, Vector2 sceneAnchorPosition)
		{
			sceneAnchorPosition += this.GetSceneFixVector();
			int num2 = 0;
			sceneAnchorPosition.X += (float)num2;
			int num3 = 120;
			Asset<Texture2D> asset = TextureAssets.Extra[236];
			Rectangle rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 1f) + new Vector2((float)num2, -42f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions = new Segments.SpriteSegment(asset, startTime, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect(new Segments.SpriteSegment.MaskedFadeEffect(null, "MaskedFade", 1, 1)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 60)).Then(new Actions.Sprites.Wait(120));
			this._segments.Add(animationSegmentWithActions);
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions2 = new Segments.NPCSegment(startTime, 142, sceneAnchorPosition + new Vector2(-30f, 0f), this._originAtBottom).Then(new Actions.NPCs.LookAt(1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(0.5f, 0f), 120));
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions3 = new Segments.NPCSegment(startTime, 441, sceneAnchorPosition + new Vector2((float)num3, 0f), this._originAtBottom).Then(new Actions.NPCs.LookAt(1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Wait(120));
			Asset<Texture2D> asset2 = TextureAssets.Extra[239];
			Rectangle rectangle2 = asset2.Frame(1, 8, 0, 0, 0, 0);
			DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(rectangle2), Color.White, 0f, rectangle2.Size() * new Vector2(0.5f, 1f), 1f, 1, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions4 = new Segments.SpriteSegment(asset2, startTime, data2, sceneAnchorPosition + new Vector2((float)(num2 - 44), 4f)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 51));
			int num4 = startTime + (int)animationSegmentWithActions2.DedicatedTimeNeeded;
			int num5 = 120;
			int num6 = 90;
			Segments.EmoteSegment item = new Segments.EmoteSegment(125, num4, num5, sceneAnchorPosition + new Vector2(30f, 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			Segments.EmoteSegment item2 = new Segments.EmoteSegment(10, num4, num5, sceneAnchorPosition + new Vector2((float)num3, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			int num7 = num5 + 30;
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num7));
			animationSegmentWithActions3.Then(new Actions.NPCs.LookAt(-1));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num7));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num7));
			num4 += num7;
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num6));
			num4 += num6;
			Segments.EmoteSegment item3 = new Segments.EmoteSegment(3, num4, num5, sceneAnchorPosition + new Vector2((float)num3, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			Segments.EmoteSegment item4 = new Segments.EmoteSegment(136, num4, num5, sceneAnchorPosition + new Vector2(30f, 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			Segments.EmoteSegment item5 = new Segments.EmoteSegment(15, num4, num5, sceneAnchorPosition + new Vector2((float)num3, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			animationSegmentWithActions2.Then(new Actions.NPCs.ShowItem(num6 + num5 + num5, 3749));
			animationSegmentWithActions4.Then(new Actions.Sprites.SetFrameSequence(num4 - startTime, new Point[]
			{
				new Point(0, 0),
				new Point(0, 1),
				new Point(0, 2),
				new Point(0, 3),
				new Point(0, 4),
				new Point(0, 5),
				new Point(0, 6),
				new Point(0, 7)
			}, 5, 0, 0));
			animationSegmentWithActions2.Then(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions3.Then(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions4.Then(new Actions.Sprites.Fade(0f, 127));
			animationSegmentWithActions.Then(new Actions.Sprites.Fade(0f, 127));
			num4 += 187;
			this._segments.Add(animationSegmentWithActions4);
			this._segments.Add(animationSegmentWithActions2);
			this._segments.Add(animationSegmentWithActions3);
			this._segments.Add(item);
			this._segments.Add(item2);
			this._segments.Add(item3);
			this._segments.Add(item4);
			this._segments.Add(item5);
			return new SegmentInforReport
			{
				totalTime = num4 - startTime
			};
		}

		// Token: 0x060041BF RID: 16831 RVA: 0x005EBB4C File Offset: 0x005E9D4C
		private SegmentInforReport PlaySegment_Grox_WitchDoctorGoingToHisPeople(int startTime, Vector2 sceneAnchorPosition)
		{
			sceneAnchorPosition += this.GetSceneFixVector();
			int num2 = 0;
			sceneAnchorPosition.X += (float)num2;
			int num3 = 60;
			Asset<Texture2D> asset = TextureAssets.Extra[231];
			Rectangle rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 1f) + new Vector2((float)num2, -42f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions = new Segments.SpriteSegment(asset, startTime, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect(new Segments.SpriteSegment.MaskedFadeEffect(null, "MaskedFade", 1, 1)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 60)).Then(new Actions.Sprites.Wait(120));
			this._segments.Add(animationSegmentWithActions);
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions2 = new Segments.NPCSegment(startTime, 228, sceneAnchorPosition + new Vector2(-60f, 0f), this._originAtBottom).Then(new Actions.NPCs.LookAt(1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(0.5f, 0f), 120));
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions3 = new Segments.NPCSegment(startTime, 663, sceneAnchorPosition + new Vector2(-110f, 0f), this._originAtBottom).Then(new Actions.NPCs.LookAt(1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(0.5f, 0f), 120));
			Point[] frameIndices = new Point[]
			{
				new Point(0, 3),
				new Point(0, 4),
				new Point(0, 5),
				new Point(0, 6),
				new Point(0, 7)
			};
			Point[] frameIndices2 = new Point[]
			{
				new Point(0, 3),
				new Point(0, 2),
				new Point(0, 1),
				new Point(0, 0)
			};
			Main.instance.LoadNPC(199);
			Asset<Texture2D> asset2 = TextureAssets.Npc[199];
			Rectangle rectangle2 = asset2.Frame(1, Main.npcFrameCount[199], 0, 0, 0, 0);
			DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(rectangle2), Color.White, 0f, rectangle2.Size() * new Vector2(0.5f, 1f), 1f, 0, 0f);
			new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(rectangle2), Color.White, 0f, rectangle2.Size() * new Vector2(0.5f, 1f), 1f, 1, 0f);
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions4 = new Segments.NPCSegment(startTime, 198, sceneAnchorPosition + new Vector2((float)(num3 * 2), 0f), this._originAtBottom).Then(new Actions.NPCs.LookAt(1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Wait(120));
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions5 = new Segments.SpriteSegment(asset2, startTime, data2, sceneAnchorPosition + new Vector2((float)(num3 * 3 - 20 + 120), 4f)).Then(new Actions.Sprites.SetFrame(0, 3, 0, 0)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 25)).Then(new Actions.Sprites.SimulateGravity(new Vector2(-1f, 0f), Vector2.Zero, 0f, 120)).With(new Actions.Sprites.SetFrameSequence(120, frameIndices, 6, 0, 0));
			int num4 = startTime + (int)animationSegmentWithActions2.DedicatedTimeNeeded;
			int num5 = 120;
			Segments.EmoteSegment item = new Segments.EmoteSegment(10, num4, num5, sceneAnchorPosition + new Vector2(0f, 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			int num6 = 6;
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions4.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions5.Then(new Actions.Sprites.SetFrameSequence(frameIndices2, num6, 0, 0));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			int durationInFrames = num5 - num6 * 4;
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions6 = new Segments.NPCSegment(num4 - num5 + num6 * 4, 198, sceneAnchorPosition + new Vector2((float)(num3 * 3 - 20), 0f), this._originAtBottom).Then(new Actions.NPCs.LookAt(-1)).Then(new Actions.NPCs.Wait(durationInFrames));
			Segments.EmoteSegment item2 = new Segments.EmoteSegment(92, num4, num5, sceneAnchorPosition + new Vector2(-50f, 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions2.Then(new Actions.NPCs.LookAt(-1));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions4.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions6.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			int num7 = 60;
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num7));
			animationSegmentWithActions2.Then(new Actions.NPCs.Move(new Vector2(0.5f, 0f), num7));
			animationSegmentWithActions4.Then(new Actions.NPCs.Wait(num7));
			animationSegmentWithActions6.Then(new Actions.NPCs.Wait(num7));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num7));
			num4 += num7;
			Segments.EmoteSegment item3 = new Segments.EmoteSegment(87, num4, num5, sceneAnchorPosition + new Vector2((float)(num3 * 2), 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions4.Then(new Actions.NPCs.Wait(num5)).Then(new Actions.NPCs.LookAt(-1));
			animationSegmentWithActions6.Then(new Actions.NPCs.Wait(num5)).Then(new Actions.NPCs.LookAt(-1));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			Segments.EmoteSegment item4 = new Segments.EmoteSegment(49, num4, num5, sceneAnchorPosition + new Vector2(30f, 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions4.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions6.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			int num8 = num5 + num5 / 2;
			Segments.EmoteSegment item5 = new Segments.EmoteSegment(10, num4, num5, sceneAnchorPosition + new Vector2((float)(num3 * 2), 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			Segments.EmoteSegment item6 = new Segments.EmoteSegment(0, num4 + num5 / 2, num5, sceneAnchorPosition + new Vector2((float)(num3 * 3 - 20), 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num8));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num8));
			animationSegmentWithActions4.Then(new Actions.NPCs.Wait(num8));
			animationSegmentWithActions6.Then(new Actions.NPCs.Wait(num8));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num8));
			num4 += num8;
			Segments.EmoteSegment item7 = new Segments.EmoteSegment(17, num4, num5, sceneAnchorPosition + new Vector2(-50f, 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			Segments.EmoteSegment item8 = new Segments.EmoteSegment(3, num4, num5, sceneAnchorPosition + new Vector2(30f, 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions4.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions6.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			animationSegmentWithActions3.Then(new Actions.NPCs.Move(new Vector2(-0.4f, 0f), 160)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions2.Then(new Actions.NPCs.Move(new Vector2(-0.5f, 0f), 160)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions4.Then(new Actions.NPCs.Move(new Vector2(-0.8f, 0f), 160)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions6.Then(new Actions.NPCs.Move(new Vector2(-0.8f, 0f), 160)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions.Then(new Actions.Sprites.Fade(0f, 127));
			num4 += 187;
			this._segments.Add(animationSegmentWithActions4);
			this._segments.Add(animationSegmentWithActions6);
			this._segments.Add(animationSegmentWithActions5);
			this._segments.Add(animationSegmentWithActions2);
			this._segments.Add(animationSegmentWithActions3);
			this._segments.Add(item);
			this._segments.Add(item2);
			this._segments.Add(item3);
			this._segments.Add(item4);
			this._segments.Add(item5);
			this._segments.Add(item6);
			this._segments.Add(item8);
			this._segments.Add(item7);
			return new SegmentInforReport
			{
				totalTime = num4 - startTime
			};
		}

		// Token: 0x060041C0 RID: 16832 RVA: 0x005EC586 File Offset: 0x005EA786
		private Vector2 GetSceneFixVector()
		{
			return new Vector2(0f - this._backgroundOffset.X, 0f);
		}

		// Token: 0x060041C1 RID: 16833 RVA: 0x005EC5A4 File Offset: 0x005EA7A4
		private SegmentInforReport PlaySegment_DryadTurningToTree(int startTime, Vector2 sceneAnchorPosition)
		{
			sceneAnchorPosition += this.GetSceneFixVector();
			Asset<Texture2D> asset = TextureAssets.Extra[217];
			Rectangle rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 1f) + new Vector2(0f, -42f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions = new Segments.SpriteSegment(asset, startTime, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect(new Segments.SpriteSegment.MaskedFadeEffect(null, "MaskedFade", 1, 1)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 60));
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions2 = new Segments.NPCSegment(startTime, 20, sceneAnchorPosition, this._originAtBottom).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(1f, 0f), 60)).Then(new Actions.NPCs.Wait(10)).Then(new Actions.NPCs.Fade(0));
			int num = startTime + (int)animationSegmentWithActions2.DedicatedTimeNeeded;
			Asset<Texture2D> asset2 = TextureAssets.Extra[215];
			Rectangle rectangle2 = asset2.Frame(1, 9, 0, 0, 0, 0);
			DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(rectangle2), Color.White, 0f, rectangle2.Size() * new Vector2(0.5f, 1f), 1f, 1, 0f);
			Vector2 vector = new Vector2(1f, 0f) * 60f + new Vector2(2f, 4f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions3 = new Segments.SpriteSegment(asset2, num, data2, sceneAnchorPosition + vector).Then(new Actions.Sprites.SetFrameSequence(new Point[]
			{
				new Point(0, 0),
				new Point(0, 1),
				new Point(0, 2),
				new Point(0, 3),
				new Point(0, 4),
				new Point(0, 5),
				new Point(0, 6),
				new Point(0, 7),
				new Point(0, 8)
			}, 8, 0, 0)).Then(new Actions.Sprites.Wait(30));
			num += (int)animationSegmentWithActions3.DedicatedTimeNeeded;
			Segments.AnimationSegmentWithActions<NPC> item = new Segments.NPCSegment(num, 46, sceneAnchorPosition + new Vector2(-100f, 0f), this._originAtBottom).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(1f, 0f), 60)).Then(new Actions.NPCs.Wait(90)).Then(new Actions.NPCs.Move(new Vector2(1f, 0f), 120)).With(new Actions.NPCs.Fade(3, 85));
			Segments.AnimationSegmentWithActions<NPC> item2 = new Segments.NPCSegment(num + 60, 299, sceneAnchorPosition + new Vector2(170f, 0f), this._originAtBottom).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(-1f, 0f), 60)).Then(new Actions.NPCs.Wait(60)).Then(new Actions.NPCs.Move(new Vector2(-1f, 0f), 90)).Then(new Actions.NPCs.Move(new Vector2(-1f, 0f), 85)).With(new Actions.NPCs.Fade(3, 85));
			float x = 1.5f;
			Segments.AnimationSegmentWithActions<NPC> item3 = new Segments.NPCSegment(num + 45, 74, sceneAnchorPosition + new Vector2(-80f, -70f), this._originAtBottom).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(x, 0f), 85)).With(new Actions.NPCs.MoveWithRotor(new Vector2(10f, 0f), 0.07391983f, new Vector2(0f, 1f), 85)).Then(new Actions.NPCs.Move(new Vector2(x, 0f), 85)).With(new Actions.NPCs.MoveWithRotor(new Vector2(4f, 0f), 0.07391983f, new Vector2(0f, 1f), 85)).With(new Actions.NPCs.Fade(3, 85));
			Segments.AnimationSegmentWithActions<NPC> item4 = new Segments.NPCSegment(num + 180, 656, sceneAnchorPosition + new Vector2(20f, 0f), this._originAtBottom).Then(new Actions.NPCs.Variant(1)).Then(new Actions.NPCs.LookAt(1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Wait(60)).Then(new Actions.NPCs.Move(new Vector2(0.5f, 0f), 60)).Then(new Actions.NPCs.Wait(60)).Then(new Actions.NPCs.DoBunnyRestAnimation(90)).Then(new Actions.NPCs.Wait(90)).With(new Actions.NPCs.Fade(3, 120));
			Segments.EmoteSegment item5 = new Segments.EmoteSegment(0, num + 360, 60, sceneAnchorPosition + new Vector2(36f, -10f), 1, Vector2.Zero);
			animationSegmentWithActions3.Then(new Actions.Sprites.Wait(420)).Then(new Actions.Sprites.Fade(0f, 120));
			num += 620;
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num - startTime - 180)).Then(new Actions.Sprites.Fade(0f, 120));
			this._segments.Add(animationSegmentWithActions);
			this._segments.Add(animationSegmentWithActions2);
			this._segments.Add(animationSegmentWithActions3);
			this._segments.Add(item);
			this._segments.Add(item2);
			this._segments.Add(item3);
			this._segments.Add(item4);
			this._segments.Add(item5);
			return new SegmentInforReport
			{
				totalTime = num - startTime
			};
		}

		// Token: 0x060041C2 RID: 16834 RVA: 0x005ECC18 File Offset: 0x005EAE18
		private SegmentInforReport PlaySegment_SantaItemExample(int startTime, Vector2 sceneAnchorPosition)
		{
			sceneAnchorPosition += this.GetSceneFixVector();
			int num = 0;
			for (int i = 0; i < num; i++)
			{
				int num2 = (int)Main.rand.NextFromList(new short[]
				{
					599,
					1958,
					3749,
					1869
				});
				Main.instance.LoadItem(num2);
				Asset<Texture2D> asset = TextureAssets.Item[num2];
				DrawData data = new DrawData(asset.Value, Vector2.Zero, null, Color.White, 0f, asset.Size() / 2f, 1f, 0, 0f);
				Vector2 initialVelocity = Vector2.UnitY * -12f + Main.rand.NextVector2Circular(6f, 3f).RotatedBy((double)((float)(i - num / 2) * 6.2831855f * 0.1f), default(Vector2));
				Vector2 gravityPerFrame = Vector2.UnitY * 0.2f;
				Segments.AnimationSegmentWithActions<Segments.LooseSprite> item = new Segments.SpriteSegment(asset, startTime, data, sceneAnchorPosition).Then(new Actions.Sprites.SimulateGravity(initialVelocity, gravityPerFrame, Main.rand.NextFloatDirection() * 0.2f, 60)).With(new Actions.Sprites.Fade(0f, 60));
				this._segments.Add(item);
			}
			Segments.AnimationSegmentWithActions<NPC> item2 = new Segments.NPCSegment(startTime, 142, sceneAnchorPosition, this._originAtBottom).Then(new Actions.NPCs.ShowItem(30, 267)).Then(new Actions.NPCs.Wait(10)).Then(new Actions.NPCs.ShowItem(30, 600)).Then(new Actions.NPCs.Wait(10)).Then(new Actions.NPCs.ShowItem(30, 2)).Then(new Actions.NPCs.Wait(10));
			this._segments.Add(item2);
			return new SegmentInforReport
			{
				totalTime = 170
			};
		}

		// Token: 0x060041C3 RID: 16835 RVA: 0x005ECDEC File Offset: 0x005EAFEC
		private SegmentInforReport PlaySegment_Grox_SkeletonMerchantSearchesThroughBones(int startTime, Vector2 sceneAnchorPosition)
		{
			sceneAnchorPosition += this.GetSceneFixVector();
			int num2 = 30;
			sceneAnchorPosition.X += (float)num2;
			int num3 = 100;
			Asset<Texture2D> asset = TextureAssets.Extra[220];
			Rectangle rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 1f) + new Vector2((float)num2, -42f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions = new Segments.SpriteSegment(asset, startTime, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect(new Segments.SpriteSegment.MaskedFadeEffect(null, "MaskedFade", 1, 1)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 60)).Then(new Actions.Sprites.Wait(60));
			this._segments.Add(animationSegmentWithActions);
			int num4 = 10;
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions2 = new Segments.NPCSegment(startTime, 453, sceneAnchorPosition + new Vector2((float)num4, 0f), this._originAtBottom).Then(new Actions.NPCs.LookAt(1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(1f, 0f), 60));
			Asset<Texture2D> asset2 = TextureAssets.Extra[227];
			DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, null, Color.White, 0f, asset2.Size() * new Vector2(0.5f, 1f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions3 = new Segments.SpriteSegment(asset2, startTime, data2, sceneAnchorPosition + new Vector2((float)num3, 2f)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 51)).Then(new Actions.Sprites.Wait(60));
			int num5 = startTime + (int)animationSegmentWithActions2.DedicatedTimeNeeded;
			int num6 = 90;
			Segments.EmoteSegment item = new Segments.EmoteSegment(87, num5, num6, sceneAnchorPosition + new Vector2((float)(60 + num4), 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions3.Then(new Actions.Sprites.Wait(num6));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num6));
			num5 += num6;
			Asset<Texture2D> asset3 = TextureAssets.Extra[228];
			Rectangle rectangle2 = asset3.Frame(1, 14, 0, 0, 0, 0);
			DrawData data3 = new DrawData(asset3.Value, Vector2.Zero, new Rectangle?(rectangle2), Color.White, 0f, rectangle2.Size() * new Vector2(0.5f, 1f), 1f, 1, 0f);
			Segments.SpriteSegment spriteSegment = new Segments.SpriteSegment(asset3, num5, data3, sceneAnchorPosition + new Vector2((float)(num3 - 10), 4f));
			spriteSegment.Then(new Actions.Sprites.SetFrameSequence(new Point[]
			{
				new Point(0, 1),
				new Point(0, 2),
				new Point(0, 3),
				new Point(0, 4)
			}, 5, 0, 0));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(20)).With(new Actions.NPCs.Fade(255));
			animationSegmentWithActions3.Then(new Actions.Sprites.Wait(20));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(20));
			num5 += 20;
			int num7 = 10;
			Main.instance.LoadItem(154);
			Asset<Texture2D> asset4 = TextureAssets.Item[154];
			DrawData drawData = new DrawData(asset4.Value, Vector2.Zero, null, Color.White, 0f, asset4.Size() / 2f, 1f, 0, 0f);
			Main.instance.LoadItem(1274);
			Asset<Texture2D> asset5 = TextureAssets.Item[1274];
			DrawData drawData2 = new DrawData(asset5.Value, Vector2.Zero, null, Color.White, 0f, asset5.Size() / 2f, 1f, 0, 0f);
			Vector2 anchorOffset = sceneAnchorPosition + new Vector2((float)num3, -8f);
			for (int i = 0; i < num7; i++)
			{
				Vector2 initialVelocity = Vector2.UnitY * -5f + Main.rand.NextVector2Circular(2.5f, 0.3f + Main.rand.NextFloat() * 0.2f).RotatedBy((double)((float)(i - num7 / 2) * 6.2831855f * 0.1f), default(Vector2));
				Vector2 gravityPerFrame = Vector2.UnitY * 0.1f;
				int targetTime = num5 + i * 10;
				DrawData data4 = drawData;
				Asset<Texture2D> asset6 = asset4;
				if (i == num7 - 3)
				{
					data4 = drawData2;
					asset6 = asset5;
				}
				Segments.AnimationSegmentWithActions<Segments.LooseSprite> item2 = new Segments.SpriteSegment(asset6, targetTime, data4, anchorOffset).Then(new Actions.Sprites.SimulateGravity(initialVelocity, gravityPerFrame, Main.rand.NextFloatDirection() * 0.2f, 60)).With(new Actions.Sprites.Fade(0f, 60));
				this._segments.Add(item2);
			}
			int num8 = 30 + num7 * 10;
			spriteSegment.Then(new Actions.Sprites.SetFrameSequence(num8, new Point[]
			{
				new Point(0, 5),
				new Point(0, 6),
				new Point(0, 7),
				new Point(0, 8)
			}, 5, 0, 0));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num8));
			animationSegmentWithActions3.Then(new Actions.Sprites.Wait(num8));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num8));
			num5 += num8;
			Segments.EmoteSegment item3 = new Segments.EmoteSegment(3, num5, num6, sceneAnchorPosition + new Vector2((float)(80 + num4), 4f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			spriteSegment.Then(new Actions.Sprites.Wait(num6)).With(new Actions.Sprites.SetFrame(0, 5, 0, 0));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions3.Then(new Actions.Sprites.Wait(num6));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num6));
			num5 += num6;
			spriteSegment.Then(new Actions.Sprites.SetFrameSequence(new Point[]
			{
				new Point(0, 9),
				new Point(0, 10),
				new Point(0, 11),
				new Point(0, 13)
			}, 5, 0, 0));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(20));
			animationSegmentWithActions3.Then(new Actions.Sprites.Wait(20));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(20));
			num5 += 20;
			int num9 = 90;
			spriteSegment.Then(new Actions.Sprites.Fade(0f));
			animationSegmentWithActions2.Then(new Actions.NPCs.ShowItem(num9, 3258)).With(new Actions.NPCs.Fade(-255)).With(new Actions.NPCs.LookAt(-1));
			animationSegmentWithActions3.Then(new Actions.Sprites.Wait(num9));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num9));
			num5 += num9;
			Segments.EmoteSegment item4 = new Segments.EmoteSegment(136, num5, num6, sceneAnchorPosition + new Vector2((float)(60 + num4), 0f) + this._emoteBubbleOffsetWhenOnRight, 0, new Vector2(-1f, 0f));
			animationSegmentWithActions2.Then(new Actions.NPCs.Move(new Vector2(-1f, 0f), num6));
			animationSegmentWithActions3.Then(new Actions.Sprites.Wait(num6));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num6));
			num5 += num6;
			animationSegmentWithActions2.Then(new Actions.NPCs.Move(new Vector2(-0.5f, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions3.Then(new Actions.Sprites.Fade(0f, 127));
			animationSegmentWithActions.Then(new Actions.Sprites.Fade(0f, 127));
			num5 += 187;
			this._segments.Add(animationSegmentWithActions3);
			this._segments.Add(animationSegmentWithActions2);
			this._segments.Add(spriteSegment);
			this._segments.Add(item);
			this._segments.Add(item3);
			this._segments.Add(item4);
			return new SegmentInforReport
			{
				totalTime = num5 - startTime
			};
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x005ED69C File Offset: 0x005EB89C
		private SegmentInforReport PlaySegment_Grox_MerchantAndTravelingMerchantTryingToSellJunk(int startTime, Vector2 sceneAnchorPosition)
		{
			sceneAnchorPosition += this.GetSceneFixVector();
			int num2 = 40;
			sceneAnchorPosition.X += (float)num2;
			int num3 = 62;
			Asset<Texture2D> asset = TextureAssets.Extra[223];
			Rectangle rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 1f) + new Vector2((float)num2, -42f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions = new Segments.SpriteSegment(asset, startTime, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect(new Segments.SpriteSegment.MaskedFadeEffect(null, "MaskedFade", 1, 1)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 60)).Then(new Actions.Sprites.Wait(60));
			this._segments.Add(animationSegmentWithActions);
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions2 = new Segments.NPCSegment(startTime, 17, sceneAnchorPosition, this._originAtBottom).Then(new Actions.NPCs.LookAt(1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Wait(60));
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions3 = new Segments.NPCSegment(startTime, 368, sceneAnchorPosition + new Vector2((float)num3, 0f), this._originAtBottom).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Wait(60));
			Asset<Texture2D> asset2 = TextureAssets.Extra[239];
			Rectangle rectangle2 = asset2.Frame(1, 8, 0, 0, 0, 0);
			DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(rectangle2), Color.White, 0f, rectangle2.Size() * new Vector2(0.5f, 1f), 1f, 1, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions4 = new Segments.SpriteSegment(asset2, startTime, data2, sceneAnchorPosition + new Vector2((float)(num2 - 128), 4f)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 51));
			int num4 = startTime + (int)animationSegmentWithActions2.DedicatedTimeNeeded;
			int num5 = 90;
			int num6 = 60;
			animationSegmentWithActions2.Then(new Actions.NPCs.ShowItem(num6, 8));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num6));
			num4 += num6;
			Segments.EmoteSegment item = new Segments.EmoteSegment(11, num4, num5, sceneAnchorPosition + new Vector2((float)num3, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions3.Then(new Actions.NPCs.ShowItem(num6, 2242));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num6));
			num4 += num6;
			Segments.EmoteSegment item2 = new Segments.EmoteSegment(11, num4, num5, sceneAnchorPosition + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			animationSegmentWithActions2.Then(new Actions.NPCs.ShowItem(num6, 88));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num6));
			num4 += num6;
			Segments.EmoteSegment item3 = new Segments.EmoteSegment(11, num4, num5, sceneAnchorPosition + new Vector2((float)num3, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions3.Then(new Actions.NPCs.ShowItem(num6, 4761));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num6));
			num4 += num6;
			Segments.EmoteSegment item4 = new Segments.EmoteSegment(11, num4, num5, sceneAnchorPosition + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			int num7 = num6 + 30;
			animationSegmentWithActions2.Then(new Actions.NPCs.ShowItem(num7, 2));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num7));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num7));
			num4 += num7;
			Segments.EmoteSegment item5 = new Segments.EmoteSegment(10, num4, num5, sceneAnchorPosition + new Vector2((float)num3, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num7));
			animationSegmentWithActions3.Then(new Actions.NPCs.ShowItem(num7, 52));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num7));
			num4 += num7;
			Segments.EmoteSegment item6 = new Segments.EmoteSegment(85, num4, num5, sceneAnchorPosition + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			Segments.EmoteSegment item7 = new Segments.EmoteSegment(85, num4, num5, sceneAnchorPosition + new Vector2((float)num3, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			animationSegmentWithActions4.Then(new Actions.Sprites.SetFrameSequence(num4 - startTime, new Point[]
			{
				new Point(0, 0),
				new Point(0, 1),
				new Point(0, 2),
				new Point(0, 3),
				new Point(0, 4),
				new Point(0, 5),
				new Point(0, 6),
				new Point(0, 7)
			}, 5, 0, 0));
			animationSegmentWithActions2.Then(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions3.Then(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions4.Then(new Actions.Sprites.Fade(0f, 127));
			animationSegmentWithActions.Then(new Actions.Sprites.Fade(0f, 127));
			num4 += 187;
			this._segments.Add(animationSegmentWithActions4);
			this._segments.Add(animationSegmentWithActions2);
			this._segments.Add(animationSegmentWithActions3);
			this._segments.Add(item);
			this._segments.Add(item2);
			this._segments.Add(item3);
			this._segments.Add(item4);
			this._segments.Add(item5);
			this._segments.Add(item6);
			this._segments.Add(item7);
			return new SegmentInforReport
			{
				totalTime = num4 - startTime
			};
		}

		// Token: 0x060041C5 RID: 16837 RVA: 0x005EDE0C File Offset: 0x005EC00C
		private SegmentInforReport PlaySegment_Grox_GuideRunningFromZombie(int startTime, Vector2 sceneAnchorPosition)
		{
			sceneAnchorPosition += this.GetSceneFixVector();
			int num2 = 12;
			sceneAnchorPosition.X += (float)num2;
			int num3 = 24;
			Asset<Texture2D> asset = TextureAssets.Extra[218];
			Rectangle rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 1f) + new Vector2((float)num2, -42f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions = new Segments.SpriteSegment(asset, startTime, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect(new Segments.SpriteSegment.MaskedFadeEffect(null, "MaskedFade", 1, 1)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 60));
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions2 = new Segments.NPCSegment(startTime, 3, sceneAnchorPosition + new Vector2((float)(num3 + 60), 0f), this._originAtBottom).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(-0.5f, 0f), 60));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait((int)animationSegmentWithActions2.DedicatedTimeNeeded));
			int num4 = startTime + (int)animationSegmentWithActions2.DedicatedTimeNeeded;
			animationSegmentWithActions2.Then(new Actions.NPCs.ZombieKnockOnDoor(60));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(60));
			num4 += 60;
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions3 = new Segments.NPCSegment(num4, 22, sceneAnchorPosition + new Vector2(-30f, 0f), this._originAtBottom).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(0.5f, 0f), 60));
			animationSegmentWithActions2.Then(new Actions.NPCs.ZombieKnockOnDoor((int)animationSegmentWithActions3.DedicatedTimeNeeded));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait((int)animationSegmentWithActions3.DedicatedTimeNeeded));
			num4 += (int)animationSegmentWithActions3.DedicatedTimeNeeded;
			int num5 = 90;
			Segments.EmoteSegment item = new Segments.EmoteSegment(87, num4, num5, sceneAnchorPosition + new Vector2(-4f, 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions2.Then(new Actions.NPCs.ZombieKnockOnDoor(num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5 - 1));
			num4 += num5;
			int num6 = 50;
			Segments.EmoteSegment item2 = new Segments.EmoteSegment(3, num4, num6, sceneAnchorPosition + new Vector2(-4f, 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			asset = TextureAssets.Extra[219];
			rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 1f) + new Vector2((float)num2, -42f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions4 = new Segments.SpriteSegment(asset, num4, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect(new Segments.SpriteSegment.MaskedFadeEffect(null, "MaskedFade", 1, 1)).Then(new Actions.Sprites.Fade(1f));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions2.Then(new Actions.NPCs.Move(new Vector2(-0.4f, 0f), num6));
			animationSegmentWithActions4.Then(new Actions.Sprites.Wait(num6));
			num4 += num6;
			Segments.EmoteSegment item3 = new Segments.EmoteSegment(134, num4, num5, sceneAnchorPosition + new Vector2(0f, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, new Vector2(-0.6f, 0f));
			animationSegmentWithActions3.Then(new Actions.NPCs.Move(new Vector2(-0.6f, 0f), num5));
			animationSegmentWithActions2.Then(new Actions.NPCs.Move(new Vector2(-0.4f, 0f), num5));
			animationSegmentWithActions4.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			animationSegmentWithActions3.Then(new Actions.NPCs.Move(new Vector2(-0.6f, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions2.Then(new Actions.NPCs.Move(new Vector2(-0.4f, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions4.Then(new Actions.Sprites.Fade(0f, 127));
			num4 += 187;
			this._segments.Add(animationSegmentWithActions);
			this._segments.Add(animationSegmentWithActions4);
			this._segments.Add(animationSegmentWithActions3);
			this._segments.Add(animationSegmentWithActions2);
			this._segments.Add(item);
			this._segments.Add(item2);
			this._segments.Add(item3);
			return new SegmentInforReport
			{
				totalTime = num4 - startTime
			};
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x005EE330 File Offset: 0x005EC530
		private SegmentInforReport PlaySegment_Grox_ZoologistAndPetsAnnoyGolfer(int startTime, Vector2 sceneAnchorPosition)
		{
			sceneAnchorPosition += this.GetSceneFixVector();
			int num2 = -28;
			sceneAnchorPosition.X += (float)num2;
			int num3 = 40;
			Asset<Texture2D> asset = TextureAssets.Extra[224];
			Rectangle rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 1f) + new Vector2((float)num2, -42f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions = new Segments.SpriteSegment(asset, startTime, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect(new Segments.SpriteSegment.MaskedFadeEffect(null, "MaskedFade", 1, 1)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 60)).Then(new Actions.Sprites.Wait(60));
			this._segments.Add(animationSegmentWithActions);
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions2 = new Segments.NPCSegment(startTime, 633, sceneAnchorPosition + new Vector2(-60f, 0f), this._originAtBottom).Then(new Actions.NPCs.ForceAltTexture(3)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(0.5f, 0f), 60));
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions3 = new Segments.NPCSegment(startTime, 656, sceneAnchorPosition + new Vector2((float)(num3 - 60), 0f), this._originAtBottom).Then(new Actions.NPCs.Variant(3)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(0.5f, 0f), 60));
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions4 = new Segments.NPCSegment(startTime, 638, sceneAnchorPosition + new Vector2((float)(num3 * 2 - 60), 0f), this._originAtBottom).Then(new Actions.NPCs.Variant(2)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(0.5f, 0f), 60));
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions5 = new Segments.NPCSegment(startTime, 637, sceneAnchorPosition + new Vector2((float)(num3 * 3 - 60), 0f), this._originAtBottom).Then(new Actions.NPCs.Variant(4)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(0.5f, 0f), 60));
			Main.instance.LoadProjectile(748);
			Asset<Texture2D> asset2 = TextureAssets.Projectile[748];
			DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, null, Color.White, 0f, asset2.Size() / 2f, 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions6 = new Segments.SpriteSegment(asset2, startTime, data2, sceneAnchorPosition + new Vector2((float)(num3 * 3 - 20), 0f)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 51)).Then(new Actions.Sprites.Wait(60));
			int num4 = startTime + (int)animationSegmentWithActions2.DedicatedTimeNeeded;
			int num5 = 90;
			float num6 = 0.5f;
			animationSegmentWithActions2.Then(new Actions.NPCs.Move(new Vector2(1f * num6, 0f), num5));
			animationSegmentWithActions3.Then(new Actions.NPCs.Move(new Vector2(0.5f * num6, 0f), num5));
			animationSegmentWithActions4.Then(new Actions.NPCs.Move(new Vector2(0.6f * num6, 0f), num5));
			animationSegmentWithActions5.Then(new Actions.NPCs.Move(new Vector2(0.8f * num6, 0f), num5));
			animationSegmentWithActions6.Then(new Actions.Sprites.SimulateGravity(new Vector2(0.82f * num6, 0f), Vector2.Zero, 0.07f, num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions7 = new Segments.NPCSegment(num4, 588, sceneAnchorPosition + new Vector2(-70f, 0f), this._originAtBottom).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(0.7f * num6, 0f), 60));
			int num7 = (int)animationSegmentWithActions7.DedicatedTimeNeeded;
			animationSegmentWithActions2.Then(new Actions.NPCs.Move(new Vector2(1f * num6, 0f), num7));
			animationSegmentWithActions3.Then(new Actions.NPCs.Move(new Vector2(0.85f * num6, 0f), num7));
			animationSegmentWithActions4.Then(new Actions.NPCs.Move(new Vector2(0.7f * num6, 0f), num7));
			animationSegmentWithActions5.Then(new Actions.NPCs.Move(new Vector2(0.65f * num6, 0f), num7));
			animationSegmentWithActions6.Then(new Actions.Sprites.SimulateGravity(new Vector2(1f * num6, 0f), Vector2.Zero, 0.07f, num7));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num7));
			num4 += num7;
			int num8 = 90;
			int num9 = num8 * 2 + num8 / 2;
			Segments.EmoteSegment item = new Segments.EmoteSegment(1, num4, num8, sceneAnchorPosition + new Vector2(-70f + 42f * num6, 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, new Vector2(1f * num6, 0f));
			Segments.EmoteSegment item2 = new Segments.EmoteSegment(15, num4 + num8 / 2, num8, sceneAnchorPosition + new Vector2((float)(80 + num7) * num6, 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, new Vector2(1f * num6, 0f));
			animationSegmentWithActions7.Then(new Actions.NPCs.Move(new Vector2(1f * num6, 0f), num9));
			animationSegmentWithActions2.Then(new Actions.NPCs.Move(new Vector2(1f * num6, 0f), num9));
			animationSegmentWithActions3.Then(new Actions.NPCs.Move(new Vector2(0.72f * num6, 0f), num9));
			animationSegmentWithActions4.Then(new Actions.NPCs.Move(new Vector2(0.7f * num6, 0f), num9));
			animationSegmentWithActions5.Then(new Actions.NPCs.Move(new Vector2(0.8f * num6, 0f), num9));
			animationSegmentWithActions6.Then(new Actions.Sprites.SimulateGravity(new Vector2(0.85f * num6, 0f), Vector2.Zero, 0.07f, num9));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num9));
			num4 += num9;
			animationSegmentWithActions2.Then(new Actions.NPCs.Move(new Vector2(0.5f * num6, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions7.Then(new Actions.NPCs.Move(new Vector2(0.5f * num6, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions3.Then(new Actions.NPCs.Move(new Vector2(0.6f * num6, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions4.Then(new Actions.NPCs.Move(new Vector2(0.7f * num6, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions5.Then(new Actions.NPCs.Move(new Vector2(0.6f * num6, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions6.Then(new Actions.Sprites.SimulateGravity(new Vector2(0.5f * num6, 0f), Vector2.Zero, 0.05f, 120)).With(new Actions.Sprites.Fade(0f, 127));
			animationSegmentWithActions.Then(new Actions.Sprites.Fade(0f, 127));
			num4 += 187;
			this._segments.Add(animationSegmentWithActions2);
			this._segments.Add(animationSegmentWithActions7);
			this._segments.Add(animationSegmentWithActions3);
			this._segments.Add(animationSegmentWithActions4);
			this._segments.Add(animationSegmentWithActions5);
			this._segments.Add(animationSegmentWithActions6);
			this._segments.Add(item2);
			this._segments.Add(item);
			return new SegmentInforReport
			{
				totalTime = num4 - startTime
			};
		}

		// Token: 0x060041C7 RID: 16839 RVA: 0x005EEBDC File Offset: 0x005ECDDC
		private SegmentInforReport PlaySegment_Grox_AnglerAndPirateTalkAboutFish(int startTime, Vector2 sceneAnchorPosition)
		{
			sceneAnchorPosition += this.GetSceneFixVector();
			int num2 = 30;
			sceneAnchorPosition.X += (float)num2;
			int num3 = 90;
			Asset<Texture2D> asset = TextureAssets.Extra[222];
			Rectangle rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 1f) + new Vector2((float)num2, -42f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions = new Segments.SpriteSegment(asset, startTime, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect(new Segments.SpriteSegment.MaskedFadeEffect(null, "MaskedFade", 1, 1)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 60)).Then(new Actions.Sprites.Wait(60));
			this._segments.Add(animationSegmentWithActions);
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions2 = new Segments.NPCSegment(startTime, 369, sceneAnchorPosition, this._originAtBottom).Then(new Actions.NPCs.LookAt(1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Wait(60));
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions3 = new Segments.NPCSegment(startTime, 229, sceneAnchorPosition + new Vector2((float)(num3 + 60), 0f), this._originAtBottom).Then(new Actions.NPCs.LookAt(-1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(-1f, 0f), 60));
			Asset<Texture2D> asset2 = TextureAssets.Extra[226];
			Rectangle rectangle2 = asset2.Frame(1, 8, 0, 0, 0, 0);
			DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(rectangle2), Color.White, 0f, rectangle2.Size() * new Vector2(0.5f, 1f), 1f, 1, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions4 = new Segments.SpriteSegment(asset2, startTime, data2, sceneAnchorPosition + new Vector2((float)(num3 / 2), 4f)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 51));
			int num4 = startTime + (int)animationSegmentWithActions2.DedicatedTimeNeeded;
			int num5 = 90;
			int num6 = num5 * 8;
			Segments.EmoteSegment item = new Segments.EmoteSegment(79, num4, num5, sceneAnchorPosition + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			Segments.EmoteSegment item2 = new Segments.EmoteSegment(65, num4 + num5, num5, sceneAnchorPosition + new Vector2((float)num3, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			Segments.EmoteSegment item3 = new Segments.EmoteSegment(136, num4 + num5 * 3, num5, sceneAnchorPosition + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			Segments.EmoteSegment item4 = new Segments.EmoteSegment(3, num4 + num5 * 5, num5, sceneAnchorPosition + new Vector2((float)num3, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			Segments.EmoteSegment item5 = new Segments.EmoteSegment(50, num4 + num5 * 6, num5, sceneAnchorPosition + new Vector2((float)num3, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			Segments.EmoteSegment item6 = new Segments.EmoteSegment(15, num4 + num5 * 6, num5, sceneAnchorPosition + this._emoteBubbleOffsetWhenOnRight, 0, new Vector2(-1f, 0f));
			Segments.EmoteSegment item7 = new Segments.EmoteSegment(2, num4 + num5 * 7, num5, sceneAnchorPosition + new Vector2((float)num3, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, new Vector2(-1.25f, 0f));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num5 * 4)).Then(new Actions.NPCs.ShowItem(num5, 2673)).Then(new Actions.NPCs.Wait(num5)).Then(new Actions.NPCs.Move(new Vector2(-1f, 0f), num5));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5 * 2)).Then(new Actions.NPCs.ShowItem(num5, 2480)).Then(new Actions.NPCs.Wait(num5 * 4)).Then(new Actions.NPCs.Move(new Vector2(-1.25f, 0f), num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num6));
			animationSegmentWithActions4.Then(new Actions.Sprites.SetFrameSequence(num6 + 60, new Point[]
			{
				new Point(0, 0),
				new Point(0, 1),
				new Point(0, 2),
				new Point(0, 3),
				new Point(0, 4),
				new Point(0, 5),
				new Point(0, 6),
				new Point(0, 7)
			}, 5, 0, 0));
			num4 += num6;
			animationSegmentWithActions2.Then(new Actions.NPCs.Move(new Vector2(-0.4f, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions3.Then(new Actions.NPCs.Move(new Vector2(-0.75f, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions4.Then(new Actions.Sprites.Fade(0f, 127));
			animationSegmentWithActions.Then(new Actions.Sprites.Fade(0f, 127));
			num4 += 187;
			this._segments.Add(animationSegmentWithActions4);
			this._segments.Add(animationSegmentWithActions2);
			this._segments.Add(animationSegmentWithActions3);
			this._segments.Add(item);
			this._segments.Add(item2);
			this._segments.Add(item3);
			this._segments.Add(item4);
			this._segments.Add(item5);
			this._segments.Add(item6);
			this._segments.Add(item7);
			return new SegmentInforReport
			{
				totalTime = num4 - startTime
			};
		}

		// Token: 0x060041C8 RID: 16840 RVA: 0x005EF200 File Offset: 0x005ED400
		private SegmentInforReport PlaySegment_Grox_WizardPartyGirlDyeTraderAndPainterPartyWithBunnies(int startTime, Vector2 sceneAnchorPosition)
		{
			sceneAnchorPosition += this.GetSceneFixVector();
			int num2 = -35;
			sceneAnchorPosition.X += (float)num2;
			int num3 = 34;
			Asset<Texture2D> asset = TextureAssets.Extra[221];
			Rectangle rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 1f) + new Vector2((float)num2, -42f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions = new Segments.SpriteSegment(asset, startTime, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect(new Segments.SpriteSegment.MaskedFadeEffect(null, "MaskedFade", 1, 1)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 60)).Then(new Actions.Sprites.Wait(60));
			this._segments.Add(animationSegmentWithActions);
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions2 = new Segments.NPCSegment(startTime, 227, sceneAnchorPosition, this._originAtBottom).Then(new Actions.NPCs.PartyHard()).Then(new Actions.NPCs.LookAt(1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Wait(60));
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions3 = new Segments.NPCSegment(startTime, 108, sceneAnchorPosition + new Vector2((float)num3, 0f), this._originAtBottom).Then(new Actions.NPCs.PartyHard()).Then(new Actions.NPCs.LookAt(-1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Wait(60));
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions4 = new Segments.NPCSegment(startTime, 207, sceneAnchorPosition + new Vector2((float)(num3 * 2 + 60), 0f), this._originAtBottom).Then(new Actions.NPCs.PartyHard()).Then(new Actions.NPCs.LookAt(-1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(-1f, 0f), 60));
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions5 = new Segments.NPCSegment(startTime, 656, sceneAnchorPosition + new Vector2((float)(num3 * 2), 0f), this._originAtBottom).Then(new Actions.NPCs.Variant(1)).Then(new Actions.NPCs.PartyHard()).Then(new Actions.NPCs.LookAt(1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(1f, 0f), 60));
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions6 = new Segments.NPCSegment(startTime, 540, sceneAnchorPosition + new Vector2((float)(num3 * 4 + 100), 0f), this._originAtBottom).Then(new Actions.NPCs.LookAt(-1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(-1f, 0f), 60));
			Asset<Texture2D> asset2 = TextureAssets.Extra[238];
			Rectangle rectangle2 = asset2.Frame(1, 4, 0, 0, 0, 0);
			DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(rectangle2), Color.White, 0f, rectangle2.Size() * new Vector2(0.5f, 1f), 1f, 1, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions7 = new Segments.SpriteSegment(asset2, startTime, data2, sceneAnchorPosition + new Vector2(60f, 2f)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 51));
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions8 = new Segments.SpriteSegment(asset2, startTime, data2, sceneAnchorPosition + new Vector2(150f, 2f)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 51));
			int num4 = startTime + (int)animationSegmentWithActions2.DedicatedTimeNeeded;
			int num5 = 90;
			int num6 = num5 * 4;
			Segments.EmoteSegment item = new Segments.EmoteSegment(127, num4, num5, sceneAnchorPosition + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			Segments.EmoteSegment item2 = new Segments.EmoteSegment(6, num4 + num5, num5, sceneAnchorPosition + new Vector2((float)num3, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			Segments.EmoteSegment item3 = new Segments.EmoteSegment(136, num4 + num5 * 2, num5, sceneAnchorPosition + new Vector2((float)(num3 * 2), 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			Segments.EmoteSegment item4 = new Segments.EmoteSegment(129, num4 + num5 * 3, num5, sceneAnchorPosition + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5 * 2)).Then(new Actions.NPCs.LookAt(1)).Then(new Actions.NPCs.Wait(num5)).Then(new Actions.NPCs.LookAt(-1)).Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions4.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num6));
			animationSegmentWithActions6.Then(new Actions.NPCs.Move(new Vector2(-0.5f, 0f), num6 / 3)).Then(new Actions.NPCs.Wait(num6 / 3)).Then(new Actions.NPCs.Move(new Vector2(0.4f, 0f), num6 / 3));
			animationSegmentWithActions5.Then(new Actions.NPCs.Move(new Vector2(-0.6f, 0f), num6 / 3)).Then(new Actions.NPCs.Move(new Vector2(0.5f, 0f), num6 / 3)).Then(new Actions.NPCs.Wait(num6 / 3));
			num4 += num6;
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions9 = new Segments.NPCSegment(num4 - 60, 208, sceneAnchorPosition + new Vector2((float)(num3 * 5 + 100), 0f), this._originAtBottom).Then(new Actions.NPCs.PartyHard()).Then(new Actions.NPCs.LookAt(-1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(-1f, 0f), 60));
			int num7 = (int)animationSegmentWithActions9.DedicatedTimeNeeded - 60;
			num4 += num7;
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num7));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num7));
			animationSegmentWithActions4.Then(new Actions.NPCs.Wait(num7));
			animationSegmentWithActions6.Then(new Actions.NPCs.Wait(num7));
			animationSegmentWithActions5.Then(new Actions.NPCs.Wait(num7));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num7));
			Segments.EmoteSegment item5 = new Segments.EmoteSegment(128, num4, num5, sceneAnchorPosition + new Vector2((float)(num3 * 5 + 40), 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			num4 += num5;
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions3.Then(new Actions.NPCs.LookAt(1)).Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions4.Then(new Actions.NPCs.LookAt(1)).Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions6.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions5.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions9.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			Segments.EmoteSegment item6 = new Segments.EmoteSegment(128, num4, num5, sceneAnchorPosition + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			Segments.EmoteSegment item7 = new Segments.EmoteSegment(128, num4, num5, sceneAnchorPosition + new Vector2((float)num3, 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			Segments.EmoteSegment item8 = new Segments.EmoteSegment(128, num4, num5, sceneAnchorPosition + new Vector2((float)(num3 * 2), 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			Segments.EmoteSegment item9 = new Segments.EmoteSegment(3, num4, num5, sceneAnchorPosition + new Vector2((float)(num3 * 5 - 10), 24f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			Segments.EmoteSegment item10 = new Segments.EmoteSegment(0, num4, num5, sceneAnchorPosition + new Vector2((float)(num3 * 4 - 20), 24f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions4.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions6.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions5.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions9.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			animationSegmentWithActions7.Then(new Actions.Sprites.SetFrameSequence(num4 - startTime, new Point[]
			{
				new Point(0, 0),
				new Point(0, 1),
				new Point(0, 2),
				new Point(0, 3)
			}, 10, 0, 0));
			animationSegmentWithActions8.Then(new Actions.Sprites.SetFrameSequence(num4 - startTime, new Point[]
			{
				new Point(0, 2),
				new Point(0, 3),
				new Point(0, 0),
				new Point(0, 1)
			}, 10, 0, 0));
			animationSegmentWithActions2.Then(new Actions.NPCs.Move(new Vector2(1f, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions3.Then(new Actions.NPCs.Move(new Vector2(1f, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions4.Then(new Actions.NPCs.Move(new Vector2(1f, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions6.Then(new Actions.NPCs.Move(new Vector2(0.5f, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions5.Then(new Actions.NPCs.Move(new Vector2(0.75f, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions9.Then(new Actions.NPCs.Move(new Vector2(0.5f, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions7.Then(new Actions.Sprites.Fade(0f, 127));
			animationSegmentWithActions8.Then(new Actions.Sprites.Fade(0f, 127));
			animationSegmentWithActions.Then(new Actions.Sprites.Fade(0f, 127));
			num4 += 187;
			this._segments.Add(animationSegmentWithActions7);
			this._segments.Add(animationSegmentWithActions8);
			this._segments.Add(animationSegmentWithActions2);
			this._segments.Add(animationSegmentWithActions3);
			this._segments.Add(animationSegmentWithActions4);
			this._segments.Add(animationSegmentWithActions6);
			this._segments.Add(animationSegmentWithActions5);
			this._segments.Add(animationSegmentWithActions9);
			this._segments.Add(item);
			this._segments.Add(item2);
			this._segments.Add(item3);
			this._segments.Add(item4);
			this._segments.Add(item5);
			this._segments.Add(item6);
			this._segments.Add(item7);
			this._segments.Add(item8);
			this._segments.Add(item9);
			this._segments.Add(item10);
			return new SegmentInforReport
			{
				totalTime = num4 - startTime
			};
		}

		// Token: 0x060041C9 RID: 16841 RVA: 0x005EFDE4 File Offset: 0x005EDFE4
		private SegmentInforReport PlaySegment_Grox_DemolitionistAndArmsDealerArguingThenNurseComes(int startTime, Vector2 sceneAnchorPosition)
		{
			sceneAnchorPosition += this.GetSceneFixVector();
			int num2 = -30;
			sceneAnchorPosition.X += (float)num2;
			Asset<Texture2D> asset = TextureAssets.Extra[234];
			Rectangle rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 1f) + new Vector2((float)num2, -42f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions = new Segments.SpriteSegment(asset, startTime, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect(new Segments.SpriteSegment.MaskedFadeEffect(null, "MaskedFade", 1, 1)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 60)).Then(new Actions.Sprites.Wait(120));
			this._segments.Add(animationSegmentWithActions);
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions2 = new Segments.NPCSegment(startTime, 38, sceneAnchorPosition, this._originAtBottom).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(1f, 0f), 60)).Then(new Actions.NPCs.Wait(60));
			int num3 = 90;
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions3 = new Segments.NPCSegment(startTime, 19, sceneAnchorPosition + new Vector2((float)(120 + num3), 0f), this._originAtBottom).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(-1f, 0f), 60)).Then(new Actions.NPCs.Wait(60));
			int num4 = startTime + (int)animationSegmentWithActions2.DedicatedTimeNeeded;
			int num5 = 360;
			int num6 = 90;
			int num7 = 45;
			int num8 = 135;
			int num9 = 180;
			Segments.EmoteSegment item = new Segments.EmoteSegment(81, num4, num6, sceneAnchorPosition + new Vector2(60f, 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			Segments.EmoteSegment item2 = new Segments.EmoteSegment(82, num4 + num7, num6, sceneAnchorPosition + new Vector2((float)(60 + num3), 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			Segments.EmoteSegment item3 = new Segments.EmoteSegment(135, num4 + num8, num6, sceneAnchorPosition + new Vector2(60f, 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			Segments.EmoteSegment item4 = new Segments.EmoteSegment(135, num4 + num9, num6, sceneAnchorPosition + new Vector2((float)(60 + num3), 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num5));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num5));
			num4 += num5;
			int num10 = num3 - 30;
			int num11 = 120;
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions4 = new Segments.NPCSegment(num4 - num11, 18, sceneAnchorPosition + new Vector2((float)(120 + num10), 0f), this._originAtBottom).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(-1f, 0f), 60)).Then(new Actions.NPCs.Wait(20));
			int num12 = (int)animationSegmentWithActions4.DedicatedTimeNeeded - num11;
			num4 += num12;
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num12));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num12));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num12));
			animationSegmentWithActions4.Then(new Actions.NPCs.LookAt(-1));
			Segments.EmoteSegment item5 = new Segments.EmoteSegment(77, num4, num6, sceneAnchorPosition + new Vector2((float)(60 + num10), 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			Segments.EmoteSegment item6 = new Segments.EmoteSegment(15, num4 + num6, num6, sceneAnchorPosition + new Vector2((float)(60 + num10), 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions4.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num6));
			num4 += num6;
			animationSegmentWithActions4.Then(new Actions.NPCs.LookAt(1));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions4.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num6));
			num4 += num6;
			Segments.EmoteSegment item7 = new Segments.EmoteSegment(10, num4, num6, sceneAnchorPosition + new Vector2(60f, 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			Segments.EmoteSegment item8 = new Segments.EmoteSegment(10, num4, num6, sceneAnchorPosition + new Vector2((float)(60 + num3), 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions4.Then(new Actions.NPCs.Wait(num6));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num6));
			num4 += num6;
			Vector2 vector;
			vector..ctor(-1f, 0f);
			Segments.EmoteSegment item9 = new Segments.EmoteSegment(77, num4, num6, sceneAnchorPosition + new Vector2((float)(60 + num3), 0f) + this._emoteBubbleOffsetWhenOnRight, 0, vector);
			Segments.EmoteSegment item10 = new Segments.EmoteSegment(77, num4 + num6 / 2, num6, sceneAnchorPosition + new Vector2(60f, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, vector);
			int num13 = num6 + num6 / 2;
			animationSegmentWithActions3.Then(new Actions.NPCs.Move(vector, num13));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num6 / 2)).Then(new Actions.NPCs.Move(vector, num6));
			animationSegmentWithActions4.Then(new Actions.NPCs.Wait(num6 / 2 + 20)).Then(new Actions.NPCs.Move(vector, num6 - 20));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num13));
			num4 += num13;
			animationSegmentWithActions2.Then(new Actions.NPCs.Move(new Vector2(-0.5f, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions3.Then(new Actions.NPCs.Move(new Vector2(-0.5f, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions4.Then(new Actions.NPCs.Move(new Vector2(-0.5f, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions.Then(new Actions.Sprites.Fade(0f, 127));
			num4 += 187;
			this._segments.Add(animationSegmentWithActions4);
			this._segments.Add(animationSegmentWithActions2);
			this._segments.Add(animationSegmentWithActions3);
			this._segments.Add(item);
			this._segments.Add(item2);
			this._segments.Add(item3);
			this._segments.Add(item4);
			this._segments.Add(item5);
			this._segments.Add(item6);
			this._segments.Add(item7);
			this._segments.Add(item8);
			this._segments.Add(item10);
			this._segments.Add(item9);
			return new SegmentInforReport
			{
				totalTime = num4 - startTime
			};
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x005F05A8 File Offset: 0x005EE7A8
		private SegmentInforReport PlaySegment_TinkererAndMechanic(int startTime, Vector2 sceneAnchorPosition)
		{
			sceneAnchorPosition += this.GetSceneFixVector();
			Asset<Texture2D> asset = TextureAssets.Extra[237];
			Rectangle rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 1f) + new Vector2(0f, -42f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions = new Segments.SpriteSegment(asset, startTime, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect(new Segments.SpriteSegment.MaskedFadeEffect(null, "MaskedFade", 1, 1)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 60));
			this._segments.Add(animationSegmentWithActions);
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions2 = new Segments.NPCSegment(startTime, 107, sceneAnchorPosition, this._originAtBottom).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(1f, 0f), 60)).Then(new Actions.NPCs.Wait(60));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait((int)animationSegmentWithActions2.DedicatedTimeNeeded));
			int num = startTime + (int)animationSegmentWithActions2.DedicatedTimeNeeded;
			int num2 = 24;
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions3 = new Segments.NPCSegment(num, 124, sceneAnchorPosition + new Vector2((float)(120 + num2), 0f), this._originAtBottom).Then(new Actions.NPCs.LookAt(-1)).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(new Vector2(-1f, 0f), 60));
			num += (int)animationSegmentWithActions3.DedicatedTimeNeeded;
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait((int)animationSegmentWithActions3.DedicatedTimeNeeded));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait((int)animationSegmentWithActions3.DedicatedTimeNeeded));
			int num3 = 120;
			Segments.EmoteSegment item = new Segments.EmoteSegment(0, num, num3, sceneAnchorPosition + new Vector2(60f, 0f) + this._emoteBubbleOffsetWhenOnLeft, 1, default(Vector2));
			Segments.EmoteSegment item2 = new Segments.EmoteSegment(0, num, num3, sceneAnchorPosition + new Vector2((float)(60 + num2), 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num3));
			animationSegmentWithActions3.Then(new Actions.NPCs.Wait(num3));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num3));
			num += num3;
			animationSegmentWithActions2.Then(new Actions.NPCs.Move(new Vector2(-0.5f, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions3.Then(new Actions.NPCs.Move(new Vector2(-0.5f, 0f), 120)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions.Then(new Actions.Sprites.Fade(0f, 127));
			num += 187;
			this._segments.Add(animationSegmentWithActions2);
			this._segments.Add(animationSegmentWithActions3);
			this._segments.Add(item);
			this._segments.Add(item2);
			return new SegmentInforReport
			{
				totalTime = num - startTime
			};
		}

		// Token: 0x060041CB RID: 16843 RVA: 0x005F0904 File Offset: 0x005EEB04
		private SegmentInforReport PlaySegment_ClothierChasingTruffle(int startTime, Vector2 sceneAnchorPosition)
		{
			sceneAnchorPosition += this.GetSceneFixVector();
			int num2 = 10;
			sceneAnchorPosition.X += (float)num2;
			Asset<Texture2D> asset = TextureAssets.Extra[225];
			Rectangle rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 1f) + new Vector2((float)num2, -42f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions = new Segments.SpriteSegment(asset, startTime, data, sceneAnchorPosition + this._backgroundOffset).UseShaderEffect(new Segments.SpriteSegment.MaskedFadeEffect(null, "MaskedFade", 1, 1)).Then(new Actions.Sprites.Fade(0f)).With(new Actions.Sprites.Fade(1f, 60));
			this._segments.Add(animationSegmentWithActions);
			Segments.AnimationSegmentWithActions<NPC> animationSegmentWithActions2 = new Segments.NPCSegment(startTime, 160, sceneAnchorPosition + new Vector2(20f, 0f), this._originAtBottom).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.LookAt(1)).Then(new Actions.NPCs.Wait(60)).Then(new Actions.NPCs.LookAt(-1));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait((int)animationSegmentWithActions2.DedicatedTimeNeeded));
			int num3 = startTime + (int)animationSegmentWithActions2.DedicatedTimeNeeded;
			int num4 = 60;
			Segments.EmoteSegment item = new Segments.EmoteSegment(10, num3, num4, sceneAnchorPosition + new Vector2(20f, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num4));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num4));
			num3 += num4;
			Segments.EmoteSegment item2 = new Segments.EmoteSegment(3, num3, num4, sceneAnchorPosition + new Vector2(20f, 0f) + this._emoteBubbleOffsetWhenOnRight, 0, default(Vector2));
			animationSegmentWithActions2.Then(new Actions.NPCs.Wait(num4));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(num4));
			num3 += num4;
			Vector2 vector;
			vector..ctor(1.2f, 0f);
			Vector2 vector2;
			vector2..ctor(1f, 0f);
			Segments.AnimationSegmentWithActions<NPC> item3 = new Segments.NPCSegment(num3, 54, sceneAnchorPosition + new Vector2(-100f, 0f), this._originAtBottom).Then(new Actions.NPCs.Fade(255)).With(new Actions.NPCs.Fade(-5, 51)).Then(new Actions.NPCs.Move(vector, 60)).Then(new Actions.NPCs.Move(vector, 130)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions2.Then(new Actions.NPCs.Move(vector2, 60)).Then(new Actions.NPCs.Move(vector2, 130)).With(new Actions.NPCs.Fade(2, 127));
			animationSegmentWithActions.Then(new Actions.Sprites.Wait(60)).Then(new Actions.Sprites.Wait(130)).With(new Actions.Sprites.Fade(0f, 127));
			int num5 = 10;
			int num6 = 40;
			int timeToPlay = 70;
			Segments.EmoteSegment item4 = new Segments.EmoteSegment(134, num3 + num5, timeToPlay, sceneAnchorPosition + new Vector2(20f, 0f) + this._emoteBubbleOffsetWhenOnLeft + vector2 * (float)num5, 1, vector2);
			Segments.EmoteSegment item5 = new Segments.EmoteSegment(15, num3 + num6, timeToPlay, sceneAnchorPosition + new Vector2(-100f, 0f) + this._emoteBubbleOffsetWhenOnLeft + vector * (float)num6, 1, vector);
			this._segments.Add(item3);
			this._segments.Add(animationSegmentWithActions2);
			this._segments.Add(item);
			this._segments.Add(item2);
			this._segments.Add(item4);
			this._segments.Add(item5);
			num3 += 200;
			return new SegmentInforReport
			{
				totalTime = num3 - startTime
			};
		}

		// Token: 0x040058FA RID: 22778
		private Vector2 _originAtBottom = new Vector2(0.5f, 1f);

		// Token: 0x040058FB RID: 22779
		private Vector2 _emoteBubbleOffsetWhenOnLeft = new Vector2(-14f, -38f);

		// Token: 0x040058FC RID: 22780
		private Vector2 _emoteBubbleOffsetWhenOnRight = new Vector2(14f, -38f);

		// Token: 0x040058FD RID: 22781
		private Vector2 _backgroundOffset = new Vector2(76f, 166f);

		// Token: 0x040058FE RID: 22782
		private int _endTime;

		// Token: 0x040058FF RID: 22783
		private List<IAnimationSegment> _segments;

		// Token: 0x02000C50 RID: 3152
		private struct SimplifiedNPCInfo
		{
			// Token: 0x06005FBF RID: 24511 RVA: 0x006D0CFF File Offset: 0x006CEEFF
			public SimplifiedNPCInfo(int npcType, Vector2 simplifiedPosition)
			{
				this._simplifiedPosition = simplifiedPosition;
				this._npcType = npcType;
			}

			// Token: 0x06005FC0 RID: 24512 RVA: 0x006D0D10 File Offset: 0x006CEF10
			public void SpawnNPC(CreditsRollComposer.AddNPCMethod methodToUse, Vector2 baseAnchor, int startTime, int totalSceneTime)
			{
				Vector2 properPosition = this.GetProperPosition(baseAnchor);
				int lookDirection = (this._simplifiedPosition.X <= 0f) ? 1 : -1;
				int num = 240;
				int timeToJumpAt = (totalSceneTime - num) / 2 - 20 + (int)(this._simplifiedPosition.X * -8f);
				methodToUse(this._npcType, properPosition, lookDirection, startTime, totalSceneTime, timeToJumpAt);
			}

			// Token: 0x06005FC1 RID: 24513 RVA: 0x006D0D71 File Offset: 0x006CEF71
			private Vector2 GetProperPosition(Vector2 baseAnchor)
			{
				return baseAnchor + this._simplifiedPosition * new Vector2(26f, 24f);
			}

			// Token: 0x04007914 RID: 30996
			private Vector2 _simplifiedPosition;

			// Token: 0x04007915 RID: 30997
			private int _npcType;
		}

		// Token: 0x02000C51 RID: 3153
		// (Invoke) Token: 0x06005FC3 RID: 24515
		private delegate void AddNPCMethod(int npcType, Vector2 sceneAnchoePosition, int lookDirection, int fromTime, int duration, int timeToJumpAt);
	}
}
