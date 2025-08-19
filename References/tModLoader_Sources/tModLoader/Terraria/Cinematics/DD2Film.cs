using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.ID;

namespace Terraria.Cinematics
{
	// Token: 0x02000743 RID: 1859
	public class DD2Film : Film
	{
		// Token: 0x06004B68 RID: 19304 RVA: 0x0066A13C File Offset: 0x0066833C
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

		// Token: 0x06004B69 RID: 19305 RVA: 0x0066A4B4 File Offset: 0x006686B4
		private void PerFrameSettings(FrameEventData evt)
		{
			CombatText.clearAll();
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x0066A4BC File Offset: 0x006686BC
		private void CreateDryad(FrameEventData evt)
		{
			this._dryad = this.PlaceNPCOnGround(20, this._startPoint);
			this._dryad.knockBackResist = 0f;
			this._dryad.immortal = true;
			this._dryad.dontTakeDamage = true;
			this._dryad.takenDamageMultiplier = 0f;
			this._dryad.immune[255] = 100000;
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x0066A52C File Offset: 0x0066872C
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

		// Token: 0x06004B6C RID: 19308 RVA: 0x0066A584 File Offset: 0x00668784
		private void SpawnWitherBeast(FrameEventData evt)
		{
			int num = NPC.NewNPC(new EntitySource_Film(null), (int)this._portal.Center.X, (int)this._portal.Bottom.Y, 568, 0, 0f, 0f, 0f, 0f, 255);
			NPC nPC = Main.npc[num];
			nPC.knockBackResist = 0f;
			nPC.immortal = true;
			nPC.dontTakeDamage = true;
			nPC.takenDamageMultiplier = 0f;
			nPC.immune[255] = 100000;
			nPC.friendly = this._ogre.friendly;
			this._army.Add(nPC);
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x0066A638 File Offset: 0x00668838
		private void SpawnJavalinThrower(FrameEventData evt)
		{
			int num = NPC.NewNPC(new EntitySource_Film(null), (int)this._portal.Center.X, (int)this._portal.Bottom.Y, 561, 0, 0f, 0f, 0f, 0f, 255);
			NPC nPC = Main.npc[num];
			nPC.knockBackResist = 0f;
			nPC.immortal = true;
			nPC.dontTakeDamage = true;
			nPC.takenDamageMultiplier = 0f;
			nPC.immune[255] = 100000;
			nPC.friendly = this._ogre.friendly;
			this._army.Add(nPC);
		}

		// Token: 0x06004B6E RID: 19310 RVA: 0x0066A6EC File Offset: 0x006688EC
		private void SpawnGoblin(FrameEventData evt)
		{
			int num = NPC.NewNPC(new EntitySource_Film(null), (int)this._portal.Center.X, (int)this._portal.Bottom.Y, 552, 0, 0f, 0f, 0f, 0f, 255);
			NPC nPC = Main.npc[num];
			nPC.knockBackResist = 0f;
			nPC.immortal = true;
			nPC.dontTakeDamage = true;
			nPC.takenDamageMultiplier = 0f;
			nPC.immune[255] = 100000;
			nPC.friendly = this._ogre.friendly;
			this._army.Add(nPC);
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x0066A7A0 File Offset: 0x006689A0
		private void CreateCritters(FrameEventData evt)
		{
			for (int i = 0; i < 5; i++)
			{
				float num = (float)i / 5f;
				NPC nPC = this.PlaceNPCOnGround((int)Utils.SelectRandom<short>(Main.rand, new short[]
				{
					46,
					46,
					299,
					538
				}), this._startPoint + new Vector2((num - 0.25f) * 400f + Main.rand.NextFloat() * 50f - 25f, 0f));
				nPC.ai[0] = 0f;
				nPC.ai[1] = 600f;
				this._critters.Add(nPC);
			}
			if (this._dryad != null)
			{
				for (int j = 0; j < 10; j++)
				{
					float num3 = (float)j / 10f;
					int num2 = NPC.NewNPC(new EntitySource_Film(null), (int)this._dryad.position.X + Main.rand.Next(-1000, 800), (int)this._dryad.position.Y - Main.rand.Next(-50, 300), 356, 0, 0f, 0f, 0f, 0f, 255);
					NPC nPC2 = Main.npc[num2];
					nPC2.ai[0] = Main.rand.NextFloat() * 4f - 2f;
					nPC2.ai[1] = Main.rand.NextFloat() * 4f - 2f;
					nPC2.velocity.X = Main.rand.NextFloat() * 4f - 2f;
					this._critters.Add(nPC2);
				}
			}
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x0066A95C File Offset: 0x00668B5C
		private void OgreSwingSound(FrameEventData evt)
		{
			SoundEngine.PlaySound(SoundID.DD2_OgreAttack, new Vector2?(this._ogre.Center), null);
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x0066A97C File Offset: 0x00668B7C
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

		// Token: 0x06004B72 RID: 19314 RVA: 0x0066AAE0 File Offset: 0x00668CE0
		private void RemoveEnemyDamage(FrameEventData evt)
		{
			this._ogre.friendly = true;
			foreach (NPC npc in this._army)
			{
				npc.friendly = true;
			}
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x0066AB40 File Offset: 0x00668D40
		private void RestoreEnemyDamage(FrameEventData evt)
		{
			this._ogre.friendly = false;
			foreach (NPC npc in this._army)
			{
				npc.friendly = false;
			}
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x0066ABA0 File Offset: 0x00668DA0
		private void DryadPortalFade(FrameEventData evt)
		{
			if (this._dryad == null || this._portal == null)
			{
				return;
			}
			if (evt.IsFirstFrame)
			{
				SoundEngine.PlaySound(SoundID.DD2_EtherianPortalDryadTouch, new Vector2?(this._dryad.Center), null);
			}
			float val = (float)(evt.Frame - 7) / (float)(evt.Duration - 7);
			val = Math.Max(0f, val);
			this._dryad.color = new Color(Vector3.Lerp(Vector3.One, new Vector3(0.5f, 0f, 0.8f), val));
			this._dryad.Opacity = 1f - val;
			this._dryad.rotation += 0.05f * (val * 4f + 1f);
			this._dryad.scale = 1f - val;
			if (this._dryad.position.X < this._portal.Right.X)
			{
				NPC dryad = this._dryad;
				dryad.velocity.X = dryad.velocity.X * 0.95f;
				NPC dryad2 = this._dryad;
				dryad2.velocity.Y = dryad2.velocity.Y * 0.55f;
			}
			int num = (int)(6f * val);
			float num2 = this._dryad.Size.Length() / 2f;
			num2 /= 20f;
			for (int i = 0; i < num; i++)
			{
				if (Main.rand.Next(5) == 0)
				{
					Dust dust = Dust.NewDustDirect(this._dryad.position, this._dryad.width, this._dryad.height, 27, this._dryad.velocity.X * 1f, 0f, 100, default(Color), 1f);
					dust.scale = 0.55f;
					dust.fadeIn = 0.7f;
					dust.velocity *= 0.1f * num2;
					dust.velocity += this._dryad.velocity;
				}
			}
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x0066ADBF File Offset: 0x00668FBF
		private void CreatePortal(FrameEventData evt)
		{
			this._portal = this.PlaceNPCOnGround(549, this._startPoint + new Vector2(-240f, 0f));
			this._portal.immortal = true;
		}

		// Token: 0x06004B76 RID: 19318 RVA: 0x0066ADF8 File Offset: 0x00668FF8
		private void DryadStand(FrameEventData evt)
		{
			if (this._dryad != null)
			{
				this._dryad.ai[0] = 0f;
				this._dryad.ai[1] = (float)evt.Remaining;
			}
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x0066AE29 File Offset: 0x00669029
		private void DryadLookRight(FrameEventData evt)
		{
			if (this._dryad != null)
			{
				this._dryad.direction = 1;
				this._dryad.spriteDirection = 1;
			}
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x0066AE4B File Offset: 0x0066904B
		private void DryadLookLeft(FrameEventData evt)
		{
			if (this._dryad != null)
			{
				this._dryad.direction = -1;
				this._dryad.spriteDirection = -1;
			}
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x0066AE6D File Offset: 0x0066906D
		private void DryadWalk(FrameEventData evt)
		{
			this._dryad.ai[0] = 1f;
			this._dryad.ai[1] = 2f;
		}

		// Token: 0x06004B7A RID: 19322 RVA: 0x0066AE93 File Offset: 0x00669093
		private void DryadConfusedEmote(FrameEventData evt)
		{
			if (this._dryad != null && evt.IsFirstFrame)
			{
				EmoteBubble.NewBubble(87, new WorldUIAnchor(this._dryad), evt.Duration);
			}
		}

		// Token: 0x06004B7B RID: 19323 RVA: 0x0066AEC0 File Offset: 0x006690C0
		private void DryadAlertEmote(FrameEventData evt)
		{
			if (this._dryad != null && evt.IsFirstFrame)
			{
				EmoteBubble.NewBubble(3, new WorldUIAnchor(this._dryad), evt.Duration);
			}
		}

		// Token: 0x06004B7C RID: 19324 RVA: 0x0066AEEC File Offset: 0x006690EC
		private void CreateOgre(FrameEventData evt)
		{
			int num = NPC.NewNPC(new EntitySource_Film(null), (int)this._portal.Center.X, (int)this._portal.Bottom.Y, 576, 0, 0f, 0f, 0f, 0f, 255);
			this._ogre = Main.npc[num];
			this._ogre.knockBackResist = 0f;
			this._ogre.immortal = true;
			this._ogre.dontTakeDamage = true;
			this._ogre.takenDamageMultiplier = 0f;
			this._ogre.immune[255] = 100000;
		}

		// Token: 0x06004B7D RID: 19325 RVA: 0x0066AFA1 File Offset: 0x006691A1
		private void OgreStand(FrameEventData evt)
		{
			if (this._ogre != null)
			{
				this._ogre.ai[0] = 0f;
				this._ogre.ai[1] = 0f;
				this._ogre.velocity = Vector2.Zero;
			}
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x0066AFDF File Offset: 0x006691DF
		private void DryadAttack(FrameEventData evt)
		{
			if (this._dryad != null)
			{
				this._dryad.ai[0] = 14f;
				this._dryad.ai[1] = (float)evt.Remaining;
				this._dryad.dryadWard = false;
			}
		}

		// Token: 0x06004B7F RID: 19327 RVA: 0x0066B01C File Offset: 0x0066921C
		private void OgreLookRight(FrameEventData evt)
		{
			if (this._ogre != null)
			{
				this._ogre.direction = 1;
				this._ogre.spriteDirection = 1;
			}
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x0066B03E File Offset: 0x0066923E
		private void OgreLookLeft(FrameEventData evt)
		{
			if (this._ogre != null)
			{
				this._ogre.direction = -1;
				this._ogre.spriteDirection = -1;
			}
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x0066B060 File Offset: 0x00669260
		public override void OnBegin()
		{
			Main.NewText("DD2Film: Begin", byte.MaxValue, byte.MaxValue, byte.MaxValue);
			Main.dayTime = true;
			Main.time = 27000.0;
			this._startPoint = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY - 32f);
			base.OnBegin();
		}

		// Token: 0x06004B82 RID: 19330 RVA: 0x0066B0C8 File Offset: 0x006692C8
		private NPC PlaceNPCOnGround(int type, Vector2 position)
		{
			int num = (int)position.X;
			int num2 = (int)position.Y;
			int i = num / 16;
			int j = num2 / 16;
			while (!WorldGen.SolidTile(i, j, false))
			{
				j++;
			}
			num2 = j * 16;
			int start = 100;
			if (type != 20)
			{
				if (type == 576)
				{
					start = 50;
				}
			}
			else
			{
				start = 1;
			}
			int num3 = NPC.NewNPC(new EntitySource_Film(null), num, num2, type, start, 0f, 0f, 0f, 0f, 255);
			return Main.npc[num3];
		}

		// Token: 0x06004B83 RID: 19331 RVA: 0x0066B154 File Offset: 0x00669354
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

		// Token: 0x04006081 RID: 24705
		private NPC _dryad;

		// Token: 0x04006082 RID: 24706
		private NPC _ogre;

		// Token: 0x04006083 RID: 24707
		private NPC _portal;

		// Token: 0x04006084 RID: 24708
		private List<NPC> _army = new List<NPC>();

		// Token: 0x04006085 RID: 24709
		private List<NPC> _critters = new List<NPC>();

		// Token: 0x04006086 RID: 24710
		private Vector2 _startPoint;
	}
}
