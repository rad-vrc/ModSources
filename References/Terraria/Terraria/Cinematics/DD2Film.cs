using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.ID;

namespace Terraria.Cinematics
{
	// Token: 0x02000461 RID: 1121
	public class DD2Film : Film
	{
		// Token: 0x06002CE6 RID: 11494 RVA: 0x005BC424 File Offset: 0x005BA624
		public DD2Film()
		{
			base.AppendKeyFrames(new FrameEvent[]
			{
				new FrameEvent(this.CreateDryad),
				new FrameEvent(this.CreateCritters)
			});
			base.AppendSequences(120, new FrameEvent[]
			{
				new FrameEvent(this.DryadStand),
				new FrameEvent(this.DryadLookRight)
			});
			base.AppendSequences(100, new FrameEvent[]
			{
				new FrameEvent(this.DryadLookRight),
				new FrameEvent(this.DryadInteract)
			});
			base.AddKeyFrame(base.AppendPoint - 20, new FrameEvent(this.CreatePortal));
			base.AppendSequences(30, new FrameEvent[]
			{
				new FrameEvent(this.DryadLookLeft),
				new FrameEvent(this.DryadStand)
			});
			base.AppendSequences(40, new FrameEvent[]
			{
				new FrameEvent(this.DryadConfusedEmote),
				new FrameEvent(this.DryadStand),
				new FrameEvent(this.DryadLookLeft)
			});
			base.AppendKeyFrame(new FrameEvent(this.CreateOgre));
			base.AddKeyFrame(base.AppendPoint + 60, new FrameEvent(this.SpawnJavalinThrower));
			base.AddKeyFrame(base.AppendPoint + 120, new FrameEvent(this.SpawnGoblin));
			base.AddKeyFrame(base.AppendPoint + 180, new FrameEvent(this.SpawnGoblin));
			base.AddKeyFrame(base.AppendPoint + 240, new FrameEvent(this.SpawnWitherBeast));
			base.AppendSequences(30, new FrameEvent[]
			{
				new FrameEvent(this.DryadStand),
				new FrameEvent(this.DryadLookLeft)
			});
			base.AppendSequences(30, new FrameEvent[]
			{
				new FrameEvent(this.DryadLookRight),
				new FrameEvent(this.DryadWalk)
			});
			base.AppendSequences(300, new FrameEvent[]
			{
				new FrameEvent(this.DryadAttack),
				new FrameEvent(this.DryadLookLeft)
			});
			base.AppendKeyFrame(new FrameEvent(this.RemoveEnemyDamage));
			base.AppendSequences(60, new FrameEvent[]
			{
				new FrameEvent(this.DryadLookRight),
				new FrameEvent(this.DryadStand),
				new FrameEvent(this.DryadAlertEmote)
			});
			base.AddSequences(base.AppendPoint - 90, 60, new FrameEvent[]
			{
				new FrameEvent(this.OgreLookLeft),
				new FrameEvent(this.OgreStand)
			});
			base.AddKeyFrame(base.AppendPoint - 12, new FrameEvent(this.OgreSwingSound));
			base.AddSequences(base.AppendPoint - 30, 50, new FrameEvent[]
			{
				new FrameEvent(this.DryadPortalKnock),
				new FrameEvent(this.DryadStand)
			});
			base.AppendKeyFrame(new FrameEvent(this.RestoreEnemyDamage));
			base.AppendSequences(40, new FrameEvent[]
			{
				new FrameEvent(this.DryadPortalFade),
				new FrameEvent(this.DryadStand)
			});
			base.AppendSequence(180, new FrameEvent(this.DryadStand));
			base.AddSequence(0, base.AppendPoint, new FrameEvent(this.PerFrameSettings));
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x005BC79C File Offset: 0x005BA99C
		private void PerFrameSettings(FrameEventData evt)
		{
			CombatText.clearAll();
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x005BC7A4 File Offset: 0x005BA9A4
		private void CreateDryad(FrameEventData evt)
		{
			this._dryad = this.PlaceNPCOnGround(20, this._startPoint);
			this._dryad.knockBackResist = 0f;
			this._dryad.immortal = true;
			this._dryad.dontTakeDamage = true;
			this._dryad.takenDamageMultiplier = 0f;
			this._dryad.immune[255] = 100000;
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x005BC814 File Offset: 0x005BAA14
		private void DryadInteract(FrameEventData evt)
		{
			if (this._dryad != null)
			{
				this._dryad.ai[0] = 9f;
				if (evt.IsFirstFrame)
				{
					this._dryad.ai[1] = (float)evt.Duration;
				}
				this._dryad.localAI[0] = 0f;
			}
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x005BC86C File Offset: 0x005BAA6C
		private void SpawnWitherBeast(FrameEventData evt)
		{
			int num = NPC.NewNPC(new EntitySource_Film(), (int)this._portal.Center.X, (int)this._portal.Bottom.Y, 568, 0, 0f, 0f, 0f, 0f, 255);
			NPC npc = Main.npc[num];
			npc.knockBackResist = 0f;
			npc.immortal = true;
			npc.dontTakeDamage = true;
			npc.takenDamageMultiplier = 0f;
			npc.immune[255] = 100000;
			npc.friendly = this._ogre.friendly;
			this._army.Add(npc);
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x005BC920 File Offset: 0x005BAB20
		private void SpawnJavalinThrower(FrameEventData evt)
		{
			int num = NPC.NewNPC(new EntitySource_Film(), (int)this._portal.Center.X, (int)this._portal.Bottom.Y, 561, 0, 0f, 0f, 0f, 0f, 255);
			NPC npc = Main.npc[num];
			npc.knockBackResist = 0f;
			npc.immortal = true;
			npc.dontTakeDamage = true;
			npc.takenDamageMultiplier = 0f;
			npc.immune[255] = 100000;
			npc.friendly = this._ogre.friendly;
			this._army.Add(npc);
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x005BC9D4 File Offset: 0x005BABD4
		private void SpawnGoblin(FrameEventData evt)
		{
			int num = NPC.NewNPC(new EntitySource_Film(), (int)this._portal.Center.X, (int)this._portal.Bottom.Y, 552, 0, 0f, 0f, 0f, 0f, 255);
			NPC npc = Main.npc[num];
			npc.knockBackResist = 0f;
			npc.immortal = true;
			npc.dontTakeDamage = true;
			npc.takenDamageMultiplier = 0f;
			npc.immune[255] = 100000;
			npc.friendly = this._ogre.friendly;
			this._army.Add(npc);
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x005BCA88 File Offset: 0x005BAC88
		private void CreateCritters(FrameEventData evt)
		{
			for (int i = 0; i < 5; i++)
			{
				float num = (float)i / 5f;
				NPC npc = this.PlaceNPCOnGround((int)Utils.SelectRandom<short>(Main.rand, new short[]
				{
					46,
					46,
					299,
					538
				}), this._startPoint + new Vector2((num - 0.25f) * 400f + Main.rand.NextFloat() * 50f - 25f, 0f));
				npc.ai[0] = 0f;
				npc.ai[1] = 600f;
				this._critters.Add(npc);
			}
			if (this._dryad == null)
			{
				return;
			}
			for (int j = 0; j < 10; j++)
			{
				float num2 = (float)j / 10f;
				int num3 = NPC.NewNPC(new EntitySource_Film(), (int)this._dryad.position.X + Main.rand.Next(-1000, 800), (int)this._dryad.position.Y - Main.rand.Next(-50, 300), 356, 0, 0f, 0f, 0f, 0f, 255);
				NPC npc2 = Main.npc[num3];
				npc2.ai[0] = Main.rand.NextFloat() * 4f - 2f;
				npc2.ai[1] = Main.rand.NextFloat() * 4f - 2f;
				npc2.velocity.X = Main.rand.NextFloat() * 4f - 2f;
				this._critters.Add(npc2);
			}
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x005BCC41 File Offset: 0x005BAE41
		private void OgreSwingSound(FrameEventData evt)
		{
			SoundEngine.PlaySound(SoundID.DD2_OgreAttack, this._ogre.Center);
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x005BCC5C File Offset: 0x005BAE5C
		private void DryadPortalKnock(FrameEventData evt)
		{
			if (this._dryad != null)
			{
				if (evt.Frame == 20)
				{
					NPC dryad = this._dryad;
					dryad.velocity.Y = dryad.velocity.Y - 7f;
					NPC dryad2 = this._dryad;
					dryad2.velocity.X = dryad2.velocity.X - 8f;
					SoundEngine.PlaySound(3, (int)this._dryad.Center.X, (int)this._dryad.Center.Y, 1, 1f, 0f);
				}
				if (evt.Frame >= 20)
				{
					this._dryad.ai[0] = 1f;
					this._dryad.ai[1] = (float)evt.Remaining;
					this._dryad.rotation += 0.05f;
				}
			}
			if (this._ogre != null)
			{
				if (evt.Frame > 40)
				{
					this._ogre.target = Main.myPlayer;
					this._ogre.direction = 1;
					return;
				}
				this._ogre.direction = -1;
				this._ogre.ai[1] = 0f;
				this._ogre.ai[0] = Math.Min(40f, this._ogre.ai[0]);
				this._ogre.target = 300 + this._dryad.whoAmI;
			}
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x005BCDC0 File Offset: 0x005BAFC0
		private void RemoveEnemyDamage(FrameEventData evt)
		{
			this._ogre.friendly = true;
			foreach (NPC npc in this._army)
			{
				npc.friendly = true;
			}
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x005BCE20 File Offset: 0x005BB020
		private void RestoreEnemyDamage(FrameEventData evt)
		{
			this._ogre.friendly = false;
			foreach (NPC npc in this._army)
			{
				npc.friendly = false;
			}
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x005BCE80 File Offset: 0x005BB080
		private void DryadPortalFade(FrameEventData evt)
		{
			if (this._dryad != null && this._portal != null)
			{
				if (evt.IsFirstFrame)
				{
					SoundEngine.PlaySound(SoundID.DD2_EtherianPortalDryadTouch, this._dryad.Center);
				}
				float num = (float)(evt.Frame - 7) / (float)(evt.Duration - 7);
				num = Math.Max(0f, num);
				this._dryad.color = new Color(Vector3.Lerp(Vector3.One, new Vector3(0.5f, 0f, 0.8f), num));
				this._dryad.Opacity = 1f - num;
				this._dryad.rotation += 0.05f * (num * 4f + 1f);
				this._dryad.scale = 1f - num;
				if (this._dryad.position.X < this._portal.Right.X)
				{
					NPC dryad = this._dryad;
					dryad.velocity.X = dryad.velocity.X * 0.95f;
					NPC dryad2 = this._dryad;
					dryad2.velocity.Y = dryad2.velocity.Y * 0.55f;
				}
				int num2 = (int)(6f * num);
				float num3 = this._dryad.Size.Length() / 2f;
				num3 /= 20f;
				for (int i = 0; i < num2; i++)
				{
					if (Main.rand.Next(5) == 0)
					{
						Dust dust = Dust.NewDustDirect(this._dryad.position, this._dryad.width, this._dryad.height, 27, this._dryad.velocity.X * 1f, 0f, 100, default(Color), 1f);
						dust.scale = 0.55f;
						dust.fadeIn = 0.7f;
						dust.velocity *= 0.1f * num3;
						dust.velocity += this._dryad.velocity;
					}
				}
			}
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x005BD09E File Offset: 0x005BB29E
		private void CreatePortal(FrameEventData evt)
		{
			this._portal = this.PlaceNPCOnGround(549, this._startPoint + new Vector2(-240f, 0f));
			this._portal.immortal = true;
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x005BD0D7 File Offset: 0x005BB2D7
		private void DryadStand(FrameEventData evt)
		{
			if (this._dryad != null)
			{
				this._dryad.ai[0] = 0f;
				this._dryad.ai[1] = (float)evt.Remaining;
			}
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x005BD108 File Offset: 0x005BB308
		private void DryadLookRight(FrameEventData evt)
		{
			if (this._dryad != null)
			{
				this._dryad.direction = 1;
				this._dryad.spriteDirection = 1;
			}
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x005BD12A File Offset: 0x005BB32A
		private void DryadLookLeft(FrameEventData evt)
		{
			if (this._dryad != null)
			{
				this._dryad.direction = -1;
				this._dryad.spriteDirection = -1;
			}
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x005BD14C File Offset: 0x005BB34C
		private void DryadWalk(FrameEventData evt)
		{
			this._dryad.ai[0] = 1f;
			this._dryad.ai[1] = 2f;
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x005BD172 File Offset: 0x005BB372
		private void DryadConfusedEmote(FrameEventData evt)
		{
			if (this._dryad != null && evt.IsFirstFrame)
			{
				EmoteBubble.NewBubble(87, new WorldUIAnchor(this._dryad), evt.Duration);
			}
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x005BD19F File Offset: 0x005BB39F
		private void DryadAlertEmote(FrameEventData evt)
		{
			if (this._dryad != null && evt.IsFirstFrame)
			{
				EmoteBubble.NewBubble(3, new WorldUIAnchor(this._dryad), evt.Duration);
			}
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x005BD1CC File Offset: 0x005BB3CC
		private void CreateOgre(FrameEventData evt)
		{
			int num = NPC.NewNPC(new EntitySource_Film(), (int)this._portal.Center.X, (int)this._portal.Bottom.Y, 576, 0, 0f, 0f, 0f, 0f, 255);
			this._ogre = Main.npc[num];
			this._ogre.knockBackResist = 0f;
			this._ogre.immortal = true;
			this._ogre.dontTakeDamage = true;
			this._ogre.takenDamageMultiplier = 0f;
			this._ogre.immune[255] = 100000;
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x005BD280 File Offset: 0x005BB480
		private void OgreStand(FrameEventData evt)
		{
			if (this._ogre != null)
			{
				this._ogre.ai[0] = 0f;
				this._ogre.ai[1] = 0f;
				this._ogre.velocity = Vector2.Zero;
			}
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x005BD2BE File Offset: 0x005BB4BE
		private void DryadAttack(FrameEventData evt)
		{
			if (this._dryad != null)
			{
				this._dryad.ai[0] = 14f;
				this._dryad.ai[1] = (float)evt.Remaining;
				this._dryad.dryadWard = false;
			}
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x005BD2FB File Offset: 0x005BB4FB
		private void OgreLookRight(FrameEventData evt)
		{
			if (this._ogre != null)
			{
				this._ogre.direction = 1;
				this._ogre.spriteDirection = 1;
			}
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x005BD31D File Offset: 0x005BB51D
		private void OgreLookLeft(FrameEventData evt)
		{
			if (this._ogre != null)
			{
				this._ogre.direction = -1;
				this._ogre.spriteDirection = -1;
			}
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x005BD340 File Offset: 0x005BB540
		public override void OnBegin()
		{
			Main.NewText("DD2Film: Begin", byte.MaxValue, byte.MaxValue, byte.MaxValue);
			Main.dayTime = true;
			Main.time = 27000.0;
			this._startPoint = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY - 32f);
			base.OnBegin();
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x005BD3A8 File Offset: 0x005BB5A8
		private NPC PlaceNPCOnGround(int type, Vector2 position)
		{
			int num = (int)position.X;
			int num2 = (int)position.Y;
			int i = num / 16;
			int num3 = num2 / 16;
			while (!WorldGen.SolidTile(i, num3, false))
			{
				num3++;
			}
			num2 = num3 * 16;
			int start = 100;
			if (type == 20)
			{
				start = 1;
			}
			else if (type == 576)
			{
				start = 50;
			}
			int num4 = NPC.NewNPC(new EntitySource_Film(), num, num2, type, start, 0f, 0f, 0f, 0f, 255);
			return Main.npc[num4];
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x005BD430 File Offset: 0x005BB630
		public override void OnEnd()
		{
			if (this._dryad != null)
			{
				this._dryad.active = false;
			}
			if (this._portal != null)
			{
				this._portal.active = false;
			}
			if (this._ogre != null)
			{
				this._ogre.active = false;
			}
			foreach (NPC npc in this._critters)
			{
				npc.active = false;
			}
			foreach (NPC npc2 in this._army)
			{
				npc2.active = false;
			}
			Main.NewText("DD2Film: End", byte.MaxValue, byte.MaxValue, byte.MaxValue);
			base.OnEnd();
		}

		// Token: 0x04005120 RID: 20768
		private NPC _dryad;

		// Token: 0x04005121 RID: 20769
		private NPC _ogre;

		// Token: 0x04005122 RID: 20770
		private NPC _portal;

		// Token: 0x04005123 RID: 20771
		private List<NPC> _army = new List<NPC>();

		// Token: 0x04005124 RID: 20772
		private List<NPC> _critters = new List<NPC>();

		// Token: 0x04005125 RID: 20773
		private Vector2 _startPoint;
	}
}
