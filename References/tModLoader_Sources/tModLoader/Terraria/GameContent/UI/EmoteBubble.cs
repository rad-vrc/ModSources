using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.GameContent.UI
{
	// Token: 0x020004C9 RID: 1225
	public class EmoteBubble : IEntityWithGlobals<GlobalEmoteBubble>
	{
		// Token: 0x06003A89 RID: 14985 RVA: 0x005A99A0 File Offset: 0x005A7BA0
		public static void UpdateAll()
		{
			Dictionary<int, EmoteBubble> obj = EmoteBubble.byID;
			lock (obj)
			{
				EmoteBubble.toClean.Clear();
				foreach (KeyValuePair<int, EmoteBubble> item in EmoteBubble.byID)
				{
					item.Value.Update();
					if (item.Value.lifeTime <= 0)
					{
						EmoteBubble.toClean.Add(item.Key);
					}
				}
				foreach (int item2 in EmoteBubble.toClean)
				{
					EmoteBubble.byID.Remove(item2);
				}
				EmoteBubble.toClean.Clear();
			}
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x005A9A9C File Offset: 0x005A7C9C
		public static void DrawAll(SpriteBatch sb)
		{
			Dictionary<int, EmoteBubble> obj = EmoteBubble.byID;
			lock (obj)
			{
				foreach (KeyValuePair<int, EmoteBubble> item in EmoteBubble.byID)
				{
					item.Value.Draw(sb);
				}
			}
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x005A9B1C File Offset: 0x005A7D1C
		public static Tuple<int, int> SerializeNetAnchor(WorldUIAnchor anch)
		{
			if (anch.type == WorldUIAnchor.AnchorType.Entity)
			{
				int item = 0;
				if (anch.entity is NPC)
				{
					item = 0;
				}
				else if (anch.entity is Player)
				{
					item = 1;
				}
				else if (anch.entity is Projectile)
				{
					item = 2;
				}
				int whoAmI = anch.entity.whoAmI;
				Projectile projectile = anch.entity as Projectile;
				if (projectile != null)
				{
					whoAmI = projectile.identity;
					item = (projectile.owner << 8 | item);
				}
				return Tuple.Create<int, int>(item, whoAmI);
			}
			return Tuple.Create<int, int>(0, 0);
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x005A9BA0 File Offset: 0x005A7DA0
		public static WorldUIAnchor DeserializeNetAnchor(int type, int meta)
		{
			int packedOwnerType = type;
			type = (packedOwnerType & 255);
			switch (type)
			{
			case 0:
				return new WorldUIAnchor(Main.npc[meta]);
			case 1:
				return new WorldUIAnchor(Main.player[meta]);
			case 2:
			{
				int owner = packedOwnerType >> 8;
				int whoAmI = Main.maxProjectiles;
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					Projectile projectile = Main.projectile[i];
					if (projectile.owner == owner && projectile.identity == meta && projectile.active)
					{
						whoAmI = i;
						break;
					}
				}
				return new WorldUIAnchor(Main.projectile[whoAmI]);
			}
			default:
				throw new Exception("How did you end up getting this?");
			}
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x005A9C41 File Offset: 0x005A7E41
		public static int AssignNewID()
		{
			return EmoteBubble.NextID++;
		}

		/// <summary>
		/// Use this method to spawn a emote bubble
		/// </summary>
		/// <param name="emoticon">The emote ID of the emote that will spawn.</param>
		/// <param name="bubbleAnchor">The <see cref="T:Terraria.GameContent.UI.WorldUIAnchor" /> instance for the emote. You can use <code>new WorldUIAnchor(Entity)</code> to get the instance.</param>
		/// <param name="time">How long this emote remains.</param>
		/// <returns>The <see cref="P:Terraria.GameContent.UI.EmoteBubble.WhoAmI" /> of this emote</returns>
		// Token: 0x06003A8E RID: 14990 RVA: 0x005A9C50 File Offset: 0x005A7E50
		public static int NewBubble(int emoticon, WorldUIAnchor bubbleAnchor, int time)
		{
			if (Main.netMode == 1)
			{
				return -1;
			}
			EmoteBubble emoteBubble = new EmoteBubble(emoticon, bubbleAnchor, time);
			emoteBubble.ID = EmoteBubble.AssignNewID();
			EmoteBubble.byID[emoteBubble.ID] = emoteBubble;
			if (Main.netMode == 2)
			{
				Tuple<int, int> tuple = EmoteBubble.SerializeNetAnchor(bubbleAnchor);
				NetMessage.SendData(91, -1, -1, null, emoteBubble.ID, (float)tuple.Item1, (float)tuple.Item2, (float)time, emoticon, 0, 0);
			}
			EmoteBubble.OnBubbleChange(emoteBubble.ID);
			EmoteBubbleLoader.OnSpawn(emoteBubble);
			return emoteBubble.ID;
		}

		/// <summary>
		/// Use this method to make NPCs use a random emote based on the pick emote table.
		/// </summary>
		/// <param name="bubbleAnchor">The <see cref="T:Terraria.GameContent.UI.WorldUIAnchor" /> instance for the emote. You can use <code>new WorldUIAnchor(NPC)</code> to get the instance.</param>
		/// <param name="time">How long this emote remains.</param>
		/// <param name="other">The <see cref="T:Terraria.GameContent.UI.WorldUIAnchor" /> instance from the other side of the conversation.</param>
		/// <returns>The <see cref="P:Terraria.GameContent.UI.EmoteBubble.WhoAmI" /> of this emote</returns>
		// Token: 0x06003A8F RID: 14991 RVA: 0x005A9CD8 File Offset: 0x005A7ED8
		public static int NewBubbleNPC(WorldUIAnchor bubbleAnchor, int time, WorldUIAnchor other = null)
		{
			if (Main.netMode == 1)
			{
				return -1;
			}
			EmoteBubble emoteBubble = new EmoteBubble(0, bubbleAnchor, time);
			emoteBubble.PickNPCEmote(other);
			if (emoteBubble.emote < 0)
			{
				return -1;
			}
			emoteBubble.ID = EmoteBubble.AssignNewID();
			EmoteBubble.byID[emoteBubble.ID] = emoteBubble;
			if (Main.netMode == 2)
			{
				Tuple<int, int> tuple = EmoteBubble.SerializeNetAnchor(bubbleAnchor);
				NetMessage.SendData(91, -1, -1, null, emoteBubble.ID, (float)tuple.Item1, (float)tuple.Item2, (float)time, emoteBubble.emote, emoteBubble.metadata, 0);
			}
			EmoteBubbleLoader.OnSpawn(emoteBubble);
			return emoteBubble.ID;
		}

		/// <summary>
		/// Try to find a NPC close enough (less than 200 pixels) to react to the emote sent by the player.
		/// </summary>
		/// <param name="emoteID"></param>
		/// <param name="player"></param>
		// Token: 0x06003A90 RID: 14992 RVA: 0x005A9D70 File Offset: 0x005A7F70
		public static void CheckForNPCsToReactToEmoteBubble(int emoteID, Player player)
		{
			for (int i = 0; i < 200; i++)
			{
				NPC nPC = Main.npc[i];
				if (nPC != null && nPC.active && nPC.aiStyle == 7 && nPC.townNPC && nPC.ai[0] < 2f && ((player.CanBeTalkedTo && player.Distance(nPC.Center) < 200f) || !Collision.CanHitLine(nPC.Top, 0, 0, player.Top, 0, 0)))
				{
					int direction = (nPC.position.X < player.position.X).ToDirectionInt();
					nPC.ai[0] = 19f;
					nPC.ai[1] = 220f;
					nPC.ai[2] = (float)player.whoAmI;
					nPC.direction = direction;
					nPC.netUpdate = true;
				}
			}
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x005A9E5C File Offset: 0x005A805C
		public EmoteBubble(int emotion, WorldUIAnchor bubbleAnchor, int time = 180)
		{
			this.anchor = bubbleAnchor;
			this.emote = emotion;
			this.lifeTime = time;
			this.lifeTimeStart = time;
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x005A9E88 File Offset: 0x005A8088
		private void Update()
		{
			this.lifeTime--;
			if (!EmoteBubbleLoader.UpdateFrame(this))
			{
				return;
			}
			if (this.lifeTime > 0)
			{
				int num = this.frameCounter + 1;
				this.frameCounter = num;
				if (num >= this.frameSpeed)
				{
					this.frameCounter = 0;
					num = this.frame + 1;
					this.frame = num;
					if (num >= 2)
					{
						this.frame = 0;
					}
				}
			}
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x005A9EF0 File Offset: 0x005A80F0
		private void Draw(SpriteBatch sb)
		{
			Texture2D value = TextureAssets.Extra[48].Value;
			SpriteEffects effect = 0;
			Vector2 position = this.GetPosition(out effect);
			if (this.anchor.type == WorldUIAnchor.AnchorType.Entity)
			{
				NPC npc = this.anchor.entity as NPC;
				if (npc != null)
				{
					NPCLoader.EmoteBubblePosition(npc, ref position, ref effect);
				}
			}
			position = position.Floor();
			bool flag = this.lifeTime < 6 || this.lifeTimeStart - this.lifeTime < 6;
			Rectangle value2 = value.Frame(8, EmoteBubble.EMOTE_SHEET_VERTICAL_FRAMES, (!flag) ? 1 : 0, 0, 0, 0);
			Vector2 origin;
			origin..ctor((float)(value2.Width / 2), (float)value2.Height);
			if (Main.player[Main.myPlayer].gravDir == -1f)
			{
				origin.Y = 0f;
				effect |= 2;
				position = Main.ReverseGravitySupport(position, 0f);
			}
			Rectangle emoteFrame = (this.emote >= 0 && this.emote < EmoteID.Count) ? value.Frame(8, 39, this.emote * 2 % 8 + this.frame, 1 + this.emote / 4, 0, 0) : new Rectangle(0, 0, 34, 28);
			if (this.emote == -1)
			{
				position += new Vector2((float)(effect.HasFlag(1) ? 1 : -1), (float)(-(float)emoteFrame.Height + 3));
			}
			else
			{
				if (this.ModEmoteBubble != null)
				{
					value = ModContent.Request<Texture2D>(this.ModEmoteBubble.Texture, 2).Value;
					emoteFrame = (EmoteBubbleLoader.GetFrame(this) ?? value.Frame(2, 1, this.frame, 0, 0, 0));
				}
				if (!EmoteBubbleLoader.PreDraw(this, sb, value, position, emoteFrame, origin, effect))
				{
					EmoteBubbleLoader.PostDraw(this, sb, value, position, emoteFrame, origin, effect);
					return;
				}
			}
			sb.Draw(TextureAssets.Extra[48].Value, position, new Rectangle?(value2), Color.White, 0f, origin, 1f, effect, 0f);
			if (flag)
			{
				return;
			}
			if (this.emote >= 0)
			{
				if ((this.emote == 87 || this.emote == 89) && effect.HasFlag(1))
				{
					effect &= -2;
					position.X += 4f;
				}
				sb.Draw(value, position, new Rectangle?(emoteFrame), Color.White, 0f, origin, 1f, effect, 0f);
			}
			else if (this.emote == -1)
			{
				value = TextureAssets.NpcHead[this.metadata].Value;
				float num = 1f;
				if ((float)value.Width / 22f > 1f)
				{
					num = 22f / (float)value.Width;
				}
				if ((float)value.Height / 16f > 1f / num)
				{
					num = 16f / (float)value.Height;
				}
				sb.Draw(value, position, null, Color.White, 0f, new Vector2((float)(value.Width / 2), 0f), num, effect, 0f);
			}
			EmoteBubbleLoader.PostDraw(this, sb, value, position, emoteFrame, origin, effect);
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x005AA214 File Offset: 0x005A8414
		private Vector2 GetPosition(out SpriteEffects effect)
		{
			switch (this.anchor.type)
			{
			case WorldUIAnchor.AnchorType.Entity:
				effect = ((this.anchor.entity.direction != -1) ? 1 : 0);
				return new Vector2(this.anchor.entity.Top.X, this.anchor.entity.VisualPosition.Y) + new Vector2((float)(-(float)this.anchor.entity.direction * this.anchor.entity.width) * 0.75f, 2f) - Main.screenPosition;
			case WorldUIAnchor.AnchorType.Tile:
				effect = 0;
				return this.anchor.pos - Main.screenPosition + new Vector2(0f, (0f - this.anchor.size.Y) / 2f);
			case WorldUIAnchor.AnchorType.Pos:
				effect = 0;
				return this.anchor.pos - Main.screenPosition;
			default:
				effect = 0;
				return new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / 2f;
			}
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x005AA34C File Offset: 0x005A854C
		public static void OnBubbleChange(int bubbleID)
		{
			EmoteBubble emoteBubble = EmoteBubble.byID[bubbleID];
			if (emoteBubble.anchor.type == WorldUIAnchor.AnchorType.Entity)
			{
				Player player = emoteBubble.anchor.entity as Player;
				if (player != null)
				{
					foreach (EmoteBubble value in EmoteBubble.byID.Values)
					{
						if (value.anchor.type == WorldUIAnchor.AnchorType.Entity && value.anchor.entity == player && value.ID != bubbleID)
						{
							value.lifeTime = 6;
						}
					}
					return;
				}
			}
		}

		/// <summary>
		/// Send a emote from <see cref="P:Terraria.Main.LocalPlayer" />. Should never be called on server.
		/// </summary>
		/// <param name="emoteId"></param>
		// Token: 0x06003A96 RID: 14998 RVA: 0x005AA3F8 File Offset: 0x005A85F8
		public static void MakeLocalPlayerEmote(int emoteId)
		{
			if (Main.netMode == 0)
			{
				EmoteBubble.NewBubble(emoteId, new WorldUIAnchor(Main.LocalPlayer), 360);
				EmoteBubble.CheckForNPCsToReactToEmoteBubble(emoteId, Main.LocalPlayer);
				return;
			}
			NetMessage.SendData(120, -1, -1, null, Main.myPlayer, (float)emoteId, 0f, 0f, 0, 0, 0);
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x005AA44C File Offset: 0x005A864C
		public void PickNPCEmote(WorldUIAnchor other = null)
		{
			Player plr = Main.player[(int)Player.FindClosest(((NPC)this.anchor.entity).Center, 0, 0)];
			List<int> list = new List<int>();
			bool flag = false;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && Main.npc[i].boss)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				if (Main.rand.Next(3) == 0)
				{
					this.ProbeTownNPCs(list);
				}
				if (Main.rand.Next(3) == 0)
				{
					this.ProbeEmotions(list);
				}
				if (Main.rand.Next(3) == 0)
				{
					this.ProbeBiomes(list, plr);
				}
				if (Main.rand.Next(2) == 0)
				{
					this.ProbeCritters(list);
				}
				if (Main.rand.Next(2) == 0)
				{
					this.ProbeItems(list, plr);
				}
				if (Main.rand.Next(5) == 0)
				{
					this.ProbeBosses(list);
				}
				if (Main.rand.Next(2) == 0)
				{
					this.ProbeDebuffs(list, plr);
				}
				if (Main.rand.Next(2) == 0)
				{
					this.ProbeEvents(list);
				}
				if (Main.rand.Next(2) == 0)
				{
					this.ProbeWeather(list, plr);
				}
				this.ProbeExceptions(list, plr, other);
			}
			else
			{
				this.ProbeCombat(list);
			}
			if (other == null)
			{
				other = new WorldUIAnchor();
			}
			int? modPickedEmote = NPCLoader.PickEmote((NPC)this.anchor.entity, plr, list, other);
			int? num = modPickedEmote;
			int num2 = -1;
			if (num.GetValueOrDefault() == num2 & num != null)
			{
				this.emote = -1;
				return;
			}
			if (modPickedEmote != null)
			{
				this.emote = modPickedEmote.Value;
				return;
			}
			if (list.Count > 0)
			{
				this.emote = list[Main.rand.Next(list.Count)];
			}
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x005AA60A File Offset: 0x005A880A
		private void ProbeCombat(List<int> list)
		{
			list.Add(16);
			list.Add(1);
			list.Add(2);
			list.Add(91);
			list.Add(93);
			list.Add(84);
			list.Add(84);
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x005AA644 File Offset: 0x005A8844
		private void ProbeWeather(List<int> list, Player plr)
		{
			if (Main.cloudBGActive > 0f)
			{
				list.Add(96);
			}
			if (Main.cloudAlpha > 0f)
			{
				if (!Main.dayTime)
				{
					list.Add(5);
				}
				list.Add(4);
				if (plr.ZoneSnow)
				{
					list.Add(98);
				}
				if (plr.position.X < 4000f || (plr.position.X > (float)(Main.maxTilesX * 16 - 4000) && (double)plr.position.Y < Main.worldSurface / 16.0))
				{
					list.Add(97);
				}
			}
			else
			{
				list.Add(95);
			}
			if (plr.ZoneHallow)
			{
				list.Add(6);
			}
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x005AA704 File Offset: 0x005A8904
		private void ProbeEvents(List<int> list)
		{
			if (BirthdayParty.PartyIsUp && Main.rand.Next(3) == 0)
			{
				list.Add(Utils.SelectRandom<int>(Main.rand, new int[]
				{
					127,
					128,
					129,
					126
				}));
			}
			if (Main.bloodMoon || (!Main.dayTime && Main.rand.Next(4) == 0))
			{
				list.Add(18);
			}
			if (Main.eclipse || (Main.hardMode && Main.rand.Next(4) == 0))
			{
				list.Add(19);
			}
			if ((!Main.dayTime || WorldGen.spawnMeteor) && NPC.downedBoss2)
			{
				list.Add(99);
			}
			if (Main.pumpkinMoon || ((NPC.downedHalloweenKing || NPC.downedHalloweenTree) && !Main.dayTime))
			{
				list.Add(20);
			}
			if (Main.snowMoon || ((NPC.downedChristmasIceQueen || NPC.downedChristmasSantank || NPC.downedChristmasTree) && !Main.dayTime))
			{
				list.Add(21);
			}
			if (DD2Event.Ongoing || DD2Event.DownedInvasionAnyDifficulty)
			{
				list.Add(133);
			}
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x005AA814 File Offset: 0x005A8A14
		private void ProbeDebuffs(List<int> list, Player plr)
		{
			if (plr.Center.Y > (float)(Main.maxTilesY * 16 - 3200) || plr.onFire || ((NPC)this.anchor.entity).onFire || plr.onFire2)
			{
				list.Add(9);
			}
			if (Main.rand.Next(2) == 0)
			{
				list.Add(11);
			}
			if (plr.poisoned || ((NPC)this.anchor.entity).poisoned || plr.ZoneJungle)
			{
				list.Add(8);
			}
			if (plr.inventory[plr.selectedItem].type == 215 || Main.rand.Next(3) == 0)
			{
				list.Add(10);
			}
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x005AA8E0 File Offset: 0x005A8AE0
		private void ProbeItems(List<int> list, Player plr)
		{
			list.Add(7);
			list.Add(73);
			list.Add(74);
			list.Add(75);
			list.Add(78);
			list.Add(90);
			if (plr.statLife < plr.statLifeMax2 / 2)
			{
				list.Add(84);
			}
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x005AA934 File Offset: 0x005A8B34
		private void ProbeTownNPCs(List<int> list)
		{
			for (int i = 0; i < NPCLoader.NPCCount; i++)
			{
				EmoteBubble.CountNPCs[i] = 0;
			}
			for (int j = 0; j < 200; j++)
			{
				if (Main.npc[j].active)
				{
					EmoteBubble.CountNPCs[Main.npc[j].type]++;
				}
			}
			int type = ((NPC)this.anchor.entity).type;
			for (int k = 0; k < NPCLoader.NPCCount; k++)
			{
				if (NPCID.Sets.FaceEmote[k] > 0 && EmoteBubble.CountNPCs[k] > 0 && k != type)
				{
					list.Add(NPCID.Sets.FaceEmote[k]);
				}
			}
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x005AA9E0 File Offset: 0x005A8BE0
		private void ProbeBiomes(List<int> list, Player plr)
		{
			if ((double)(plr.position.Y / 16f) < Main.worldSurface * 0.45)
			{
				list.Add(22);
				return;
			}
			if ((double)(plr.position.Y / 16f) > Main.rockLayer + (double)(Main.maxTilesY / 2) - 100.0)
			{
				list.Add(31);
				return;
			}
			if ((double)(plr.position.Y / 16f) > Main.rockLayer)
			{
				list.Add(30);
				return;
			}
			if (plr.ZoneHallow)
			{
				list.Add(27);
				return;
			}
			if (plr.ZoneCorrupt)
			{
				list.Add(26);
				return;
			}
			if (plr.ZoneCrimson)
			{
				list.Add(25);
				return;
			}
			if (plr.ZoneJungle)
			{
				list.Add(24);
				return;
			}
			if (plr.ZoneSnow)
			{
				list.Add(32);
				return;
			}
			if ((double)(plr.position.Y / 16f) < Main.worldSurface && (plr.position.X < 4000f || plr.position.X > (float)(16 * (Main.maxTilesX - 250))))
			{
				list.Add(29);
				return;
			}
			if (plr.ZoneDesert)
			{
				list.Add(28);
				return;
			}
			if (plr.ZoneForest)
			{
				list.Add(23);
			}
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x005AAB38 File Offset: 0x005A8D38
		private void ProbeCritters(List<int> list)
		{
			Vector2 center = this.anchor.entity.Center;
			float num = 1f;
			float num2 = 1f;
			if ((double)center.Y < Main.rockLayer * 16.0)
			{
				num2 = 0.2f;
			}
			else
			{
				num = 0.2f;
			}
			if (Main.rand.NextFloat() <= num)
			{
				if (Main.dayTime)
				{
					list.Add(13);
					list.Add(12);
					list.Add(68);
					list.Add(62);
					list.Add(63);
					list.Add(69);
					list.Add(70);
				}
				if (!Main.dayTime || (Main.dayTime && (Main.time < 5400.0 || Main.time > 48600.0)))
				{
					list.Add(61);
				}
				if (NPC.downedGoblins)
				{
					list.Add(64);
				}
				if (NPC.downedFrost)
				{
					list.Add(66);
				}
				if (NPC.downedPirates)
				{
					list.Add(65);
				}
				if (NPC.downedMartians)
				{
					list.Add(71);
				}
				if (WorldGen.crimson)
				{
					list.Add(67);
				}
			}
			if (Main.rand.NextFloat() <= num2)
			{
				list.Add(72);
				list.Add(69);
			}
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x005AAC74 File Offset: 0x005A8E74
		private void ProbeEmotions(List<int> list)
		{
			list.Add(0);
			list.Add(1);
			list.Add(2);
			list.Add(3);
			list.Add(15);
			list.Add(16);
			list.Add(17);
			list.Add(87);
			list.Add(91);
			list.Add(136);
			list.Add(134);
			list.Add(135);
			list.Add(137);
			list.Add(138);
			list.Add(139);
			if (Main.bloodMoon && !Main.dayTime)
			{
				int item = Utils.SelectRandom<int>(Main.rand, new int[]
				{
					16,
					1,
					138
				});
				list.Add(item);
				list.Add(item);
				list.Add(item);
			}
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x005AAD48 File Offset: 0x005A8F48
		private void ProbeBosses(List<int> list)
		{
			int num = 0;
			if ((!NPC.downedBoss1 && !Main.dayTime) || NPC.downedBoss1)
			{
				num = 1;
			}
			if (NPC.downedBoss2)
			{
				num = 2;
			}
			if (NPC.downedQueenBee || NPC.downedBoss3)
			{
				num = 3;
			}
			if (Main.hardMode)
			{
				num = 4;
			}
			if (NPC.downedMechBossAny)
			{
				num = 5;
			}
			if (NPC.downedPlantBoss)
			{
				num = 6;
			}
			if (NPC.downedGolemBoss)
			{
				num = 7;
			}
			if (NPC.downedAncientCultist)
			{
				num = 8;
			}
			int maxValue = 10;
			if (NPC.downedMoonlord)
			{
				maxValue = 1;
			}
			if ((num >= 1 && num <= 2) || (num >= 1 && Main.rand.Next(maxValue) == 0))
			{
				list.Add(39);
				if (WorldGen.crimson)
				{
					list.Add(41);
				}
				else
				{
					list.Add(40);
				}
				list.Add(51);
			}
			if ((num >= 2 && num <= 3) || (num >= 2 && Main.rand.Next(maxValue) == 0))
			{
				list.Add(43);
				list.Add(42);
			}
			if ((num >= 4 && num <= 5) || (num >= 4 && Main.rand.Next(maxValue) == 0))
			{
				list.Add(44);
				list.Add(47);
				list.Add(45);
				list.Add(46);
			}
			if ((num >= 5 && num <= 6) || (num >= 5 && Main.rand.Next(maxValue) == 0))
			{
				if (!NPC.downedMechBoss1)
				{
					list.Add(47);
				}
				if (!NPC.downedMechBoss2)
				{
					list.Add(45);
				}
				if (!NPC.downedMechBoss3)
				{
					list.Add(46);
				}
				list.Add(48);
			}
			if (num == 6 || (num >= 6 && Main.rand.Next(maxValue) == 0))
			{
				list.Add(48);
				list.Add(49);
				list.Add(50);
			}
			if (num == 7 || (num >= 7 && Main.rand.Next(maxValue) == 0))
			{
				list.Add(49);
				list.Add(50);
				list.Add(52);
			}
			if (num == 8 || (num >= 8 && Main.rand.Next(maxValue) == 0))
			{
				list.Add(52);
				list.Add(53);
			}
			if (NPC.downedPirates && Main.expertMode)
			{
				list.Add(59);
			}
			if (NPC.downedMartians)
			{
				list.Add(60);
			}
			if (NPC.downedChristmasIceQueen)
			{
				list.Add(57);
			}
			if (NPC.downedChristmasSantank)
			{
				list.Add(58);
			}
			if (NPC.downedChristmasTree)
			{
				list.Add(56);
			}
			if (NPC.downedHalloweenKing)
			{
				list.Add(55);
			}
			if (NPC.downedHalloweenTree)
			{
				list.Add(54);
			}
			if (NPC.downedEmpressOfLight)
			{
				list.Add(143);
			}
			if (NPC.downedQueenSlime)
			{
				list.Add(144);
			}
			if (NPC.downedDeerclops)
			{
				list.Add(150);
			}
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x005AAFD8 File Offset: 0x005A91D8
		private void ProbeExceptions(List<int> list, Player plr, WorldUIAnchor other)
		{
			NPC nPC = (NPC)this.anchor.entity;
			if (nPC.type == 17)
			{
				list.Add(80);
				list.Add(85);
				list.Add(85);
				list.Add(85);
				list.Add(85);
			}
			else if (nPC.type == 18)
			{
				list.Add(73);
				list.Add(73);
				list.Add(84);
				list.Add(75);
			}
			else if (nPC.type == 19)
			{
				if (other != null && ((NPC)other.entity).type == 22)
				{
					list.Add(1);
					list.Add(1);
					list.Add(93);
					list.Add(92);
				}
				else if (other != null && ((NPC)other.entity).type == 22)
				{
					list.Add(1);
					list.Add(1);
					list.Add(93);
					list.Add(92);
				}
				else
				{
					list.Add(82);
					list.Add(82);
					list.Add(85);
					list.Add(85);
					list.Add(77);
					list.Add(93);
				}
			}
			else if (nPC.type == 20)
			{
				if (list.Contains(121))
				{
					list.Add(121);
					list.Add(121);
				}
				list.Add(14);
				list.Add(14);
			}
			else if (nPC.type == 22)
			{
				if (!Main.bloodMoon)
				{
					if (other != null && ((NPC)other.entity).type == 19)
					{
						list.Add(1);
						list.Add(1);
						list.Add(93);
						list.Add(92);
					}
					else
					{
						list.Add(79);
					}
				}
				if (!Main.dayTime)
				{
					list.Add(16);
					list.Add(16);
					list.Add(16);
				}
			}
			else if (nPC.type == 37)
			{
				list.Add(43);
				list.Add(43);
				list.Add(43);
				list.Add(72);
				list.Add(72);
			}
			else if (nPC.type == 38)
			{
				if (Main.bloodMoon)
				{
					list.Add(77);
					list.Add(77);
					list.Add(77);
					list.Add(81);
				}
				else
				{
					list.Add(77);
					list.Add(77);
					list.Add(81);
					list.Add(81);
					list.Add(81);
					list.Add(90);
					list.Add(90);
				}
			}
			else if (nPC.type == 54)
			{
				if (Main.bloodMoon)
				{
					list.Add(43);
					list.Add(72);
					list.Add(1);
				}
				else
				{
					if (list.Contains(111))
					{
						list.Add(111);
					}
					list.Add(17);
				}
			}
			else if (nPC.type == 107)
			{
				if (other != null && ((NPC)other.entity).type == 124)
				{
					list.Remove(111);
					list.Add(0);
					list.Add(0);
					list.Add(0);
					list.Add(17);
					list.Add(17);
					list.Add(86);
					list.Add(88);
					list.Add(88);
				}
				else
				{
					if (list.Contains(111))
					{
						list.Add(111);
						list.Add(111);
						list.Add(111);
					}
					list.Add(91);
					list.Add(92);
					list.Add(91);
					list.Add(92);
				}
			}
			else if (nPC.type == 108)
			{
				list.Add(100);
				list.Add(89);
				list.Add(11);
			}
			if (nPC.type == 124)
			{
				if (other != null && ((NPC)other.entity).type == 107)
				{
					list.Remove(111);
					list.Add(0);
					list.Add(0);
					list.Add(0);
					list.Add(17);
					list.Add(17);
					list.Add(88);
					list.Add(88);
					return;
				}
				if (list.Contains(109))
				{
					list.Add(109);
					list.Add(109);
					list.Add(109);
				}
				if (list.Contains(108))
				{
					list.Remove(108);
					if (Main.hardMode)
					{
						list.Add(108);
						list.Add(108);
					}
					else
					{
						list.Add(106);
						list.Add(106);
					}
				}
				list.Add(43);
				list.Add(2);
				return;
			}
			else
			{
				if (nPC.type == 142)
				{
					list.Add(32);
					list.Add(66);
					list.Add(17);
					list.Add(15);
					list.Add(15);
					return;
				}
				if (nPC.type == 160)
				{
					list.Add(10);
					list.Add(89);
					list.Add(94);
					list.Add(8);
					return;
				}
				if (nPC.type == 178)
				{
					list.Add(83);
					list.Add(83);
					return;
				}
				if (nPC.type == 207)
				{
					list.Add(28);
					list.Add(95);
					list.Add(93);
					return;
				}
				if (nPC.type == 208)
				{
					list.Add(94);
					list.Add(17);
					list.Add(3);
					list.Add(77);
					return;
				}
				if (nPC.type == 209)
				{
					list.Add(48);
					list.Add(83);
					list.Add(5);
					list.Add(5);
					return;
				}
				if (nPC.type == 227)
				{
					list.Add(63);
					list.Add(68);
					return;
				}
				if (nPC.type == 228)
				{
					list.Add(24);
					list.Add(24);
					list.Add(95);
					list.Add(8);
					return;
				}
				if (nPC.type == 229)
				{
					list.Add(93);
					list.Add(9);
					list.Add(65);
					list.Add(120);
					list.Add(59);
					return;
				}
				if (nPC.type == 353)
				{
					if (list.Contains(104))
					{
						list.Add(104);
						list.Add(104);
					}
					if (list.Contains(111))
					{
						list.Add(111);
						list.Add(111);
					}
					list.Add(67);
					return;
				}
				if (nPC.type == 368)
				{
					list.Add(85);
					list.Add(7);
					list.Add(79);
					return;
				}
				if (nPC.type == 369)
				{
					if (!Main.bloodMoon)
					{
						list.Add(70);
						list.Add(70);
						list.Add(76);
						list.Add(76);
						list.Add(79);
						list.Add(79);
						if ((double)nPC.position.Y < Main.worldSurface)
						{
							list.Add(29);
							return;
						}
					}
				}
				else
				{
					if (nPC.type == 453)
					{
						list.Add(72);
						list.Add(69);
						list.Add(87);
						list.Add(3);
						return;
					}
					if (nPC.type == 441)
					{
						list.Add(100);
						list.Add(100);
						list.Add(1);
						list.Add(1);
						list.Add(1);
						list.Add(87);
					}
				}
				return;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06003AA3 RID: 15011 RVA: 0x005AB715 File Offset: 0x005A9915
		// (set) Token: 0x06003AA4 RID: 15012 RVA: 0x005AB71D File Offset: 0x005A991D
		public ModEmoteBubble ModEmoteBubble { get; internal set; }

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06003AA5 RID: 15013 RVA: 0x005AB726 File Offset: 0x005A9926
		int IEntityWithGlobals<GlobalEmoteBubble>.Type
		{
			get
			{
				return this.emote;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06003AA6 RID: 15014 RVA: 0x005AB72E File Offset: 0x005A992E
		public RefReadOnlyArray<GlobalEmoteBubble> EntityGlobals
		{
			get
			{
				return this._globals;
			}
		}

		/// <summary>
		/// The whoAmI indicator that indicates this <see cref="T:Terraria.GameContent.UI.EmoteBubble" />, can be used in <see cref="M:Terraria.GameContent.UI.EmoteBubble.GetExistingEmoteBubble(System.Int32)" />
		/// </summary>
		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06003AA7 RID: 15015 RVA: 0x005AB73B File Offset: 0x005A993B
		public int WhoAmI
		{
			get
			{
				return this.ID;
			}
		}

		/// <summary>
		/// Whether or not this emote is fully displayed
		/// <br>The first and the last 6 frames are for bubble-popping animation. The emote content is displayed after the animation</br>
		/// </summary>
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06003AA8 RID: 15016 RVA: 0x005AB743 File Offset: 0x005A9943
		public bool IsFullyDisplayed
		{
			get
			{
				return this.lifeTime >= 6 && this.lifeTimeStart - this.lifeTime >= 6;
			}
		}

		/// <summary>
		/// Gets the emote bubble that exists in the world by <see cref="P:Terraria.GameContent.UI.EmoteBubble.WhoAmI" />. Returns null if there is no corresponding emote
		/// </summary>
		/// <param name="whoAmI"></param>
		/// <returns></returns>
		// Token: 0x06003AA9 RID: 15017 RVA: 0x005AB763 File Offset: 0x005A9963
		public static EmoteBubble GetExistingEmoteBubble(int whoAmI)
		{
			return EmoteBubble.byID.GetValueOrDefault(whoAmI);
		}

		/// <summary>
		/// Send a emote from the player
		/// </summary>
		/// <param name="player"></param>
		/// <param name="emoteId"></param>
		/// <param name="syncBetweenClients">If true, this emote will be automatically synchronized between clients</param>
		// Token: 0x06003AAA RID: 15018 RVA: 0x005AB770 File Offset: 0x005A9970
		public static void MakePlayerEmote(Player player, int emoteId, bool syncBetweenClients = true)
		{
			int netMode = Main.netMode;
			bool flag = netMode == 0 || netMode == 2;
			if (flag || !syncBetweenClients)
			{
				EmoteBubble.NewBubble(emoteId, new WorldUIAnchor(player), 360);
				EmoteBubble.CheckForNPCsToReactToEmoteBubble(emoteId, player);
				return;
			}
			NetMessage.SendData(120, -1, -1, null, player.whoAmI, (float)emoteId, 0f, 0f, 0, 0, 0);
		}

		// Token: 0x0400540A RID: 21514
		internal static int[] CountNPCs = new int[(int)NPCID.Count];

		// Token: 0x0400540B RID: 21515
		internal static Dictionary<int, EmoteBubble> byID = new Dictionary<int, EmoteBubble>();

		// Token: 0x0400540C RID: 21516
		private static List<int> toClean = new List<int>();

		// Token: 0x0400540D RID: 21517
		private static int NextID;

		// Token: 0x0400540E RID: 21518
		internal int ID;

		// Token: 0x0400540F RID: 21519
		public WorldUIAnchor anchor;

		// Token: 0x04005410 RID: 21520
		public int lifeTime;

		// Token: 0x04005411 RID: 21521
		public int lifeTimeStart;

		/// <summary>
		/// This is the internal ID of this EmoteBubble.
		/// </summary>
		// Token: 0x04005412 RID: 21522
		public int emote;

		// Token: 0x04005413 RID: 21523
		public int metadata;

		// Token: 0x04005414 RID: 21524
		public int frameSpeed = 8;

		// Token: 0x04005415 RID: 21525
		public int frameCounter;

		// Token: 0x04005416 RID: 21526
		public int frame;

		// Token: 0x04005417 RID: 21527
		public const int EMOTE_SHEET_HORIZONTAL_FRAMES = 8;

		// Token: 0x04005418 RID: 21528
		public const int EMOTE_SHEET_EMOTES_PER_ROW = 4;

		// Token: 0x04005419 RID: 21529
		public static readonly int EMOTE_SHEET_VERTICAL_FRAMES = 2 + (EmoteID.Count - 1) / 4;

		// Token: 0x0400541B RID: 21531
		internal GlobalEmoteBubble[] _globals;
	}
}
