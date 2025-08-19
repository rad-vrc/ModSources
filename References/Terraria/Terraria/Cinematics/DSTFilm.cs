using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.Cinematics
{
	// Token: 0x02000462 RID: 1122
	public class DSTFilm : Film
	{
		// Token: 0x06002D02 RID: 11522 RVA: 0x005BD51C File Offset: 0x005BB71C
		public DSTFilm()
		{
			this.BuildSequence();
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x005BD52A File Offset: 0x005BB72A
		public override void OnBegin()
		{
			this.PrepareScene();
			Main.hideUI = true;
			base.OnBegin();
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x005BD53E File Offset: 0x005BB73E
		public override void OnEnd()
		{
			this.ClearScene();
			Main.hideUI = false;
			base.OnEnd();
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x005BD554 File Offset: 0x005BB754
		private void BuildSequence()
		{
			base.AppendKeyFrames(new FrameEvent[]
			{
				new FrameEvent(this.EquipDSTShaderItem)
			});
			base.AppendEmptySequence(60);
			base.AppendKeyFrames(new FrameEvent[]
			{
				new FrameEvent(this.CreateDeerclops),
				new FrameEvent(this.CreateChester),
				new FrameEvent(this.ControlPlayer)
			});
			base.AppendEmptySequence(60);
			base.AppendEmptySequence(187);
			base.AppendKeyFrames(new FrameEvent[]
			{
				new FrameEvent(this.StopBeforeCliff)
			});
			base.AppendEmptySequence(20);
			base.AppendKeyFrames(new FrameEvent[]
			{
				new FrameEvent(this.TurnPlayerToTheLeft)
			});
			base.AppendEmptySequence(20);
			base.AppendKeyFrames(new FrameEvent[]
			{
				new FrameEvent(this.DeerclopsAttack)
			});
			base.AppendEmptySequence(60);
			base.AppendKeyFrames(new FrameEvent[]
			{
				new FrameEvent(this.RemoveDSTShaderItem)
			});
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x005BD654 File Offset: 0x005BB854
		private void PrepareScene()
		{
			Main.dayTime = true;
			Main.time = 13500.0;
			Main.time = 43638.0;
			Main.windSpeedCurrent = (Main.windSpeedTarget = 0.36799997f);
			Main.windCounter = 2011;
			Main.cloudAlpha = 0f;
			Main.raining = true;
			Main.rainTime = 3600;
			Main.maxRaining = (Main.oldMaxRaining = (Main.cloudAlpha = 0.9f));
			Main.raining = true;
			Main.maxRaining = (Main.oldMaxRaining = (Main.cloudAlpha = 0.6f));
			Main.raining = true;
			Main.maxRaining = (Main.oldMaxRaining = (Main.cloudAlpha = 0.6f));
			this._startPoint = new Point(4050, 488).ToWorldCoordinates(8f, 8f);
			this._startPoint -= new Vector2(1280f, 0f);
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x005BD749 File Offset: 0x005BB949
		private void ClearScene()
		{
			if (this._deerclops != null)
			{
				this._deerclops.active = false;
			}
			if (this._chester != null)
			{
				this._chester.active = false;
			}
			Main.LocalPlayer.isControlledByFilm = false;
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x005BD780 File Offset: 0x005BB980
		private void EquipDSTShaderItem(FrameEventData evt)
		{
			this._oldItem = Main.LocalPlayer.armor[3];
			Item item = new Item();
			item.SetDefaults(5113);
			Main.LocalPlayer.armor[3] = item;
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x005BD7BD File Offset: 0x005BB9BD
		private void RemoveDSTShaderItem(FrameEventData evt)
		{
			Main.LocalPlayer.armor[3] = this._oldItem;
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x005BD7D4 File Offset: 0x005BB9D4
		private void CreateDeerclops(FrameEventData evt)
		{
			this._deerclops = this.PlaceNPCOnGround(668, this._startPoint);
			this._deerclops.immortal = true;
			this._deerclops.dontTakeDamage = true;
			this._deerclops.takenDamageMultiplier = 0f;
			this._deerclops.immune[255] = 100000;
			this._deerclops.immune[Main.myPlayer] = 100000;
			this._deerclops.ai[0] = -1f;
			this._deerclops.velocity.Y = 4f;
			this._deerclops.velocity.X = 6f;
			NPC deerclops = this._deerclops;
			deerclops.position.X = deerclops.position.X - 24f;
			this._deerclops.direction = (this._deerclops.spriteDirection = 1);
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x005BD8BC File Offset: 0x005BBABC
		private NPC PlaceNPCOnGround(int type, Vector2 position)
		{
			int x;
			int num;
			DSTFilm.FindFloorAt(position, out x, out num);
			if (type == 668)
			{
				num -= 240;
			}
			int start = 100;
			int num2 = NPC.NewNPC(new EntitySource_Film(), x, num, type, start, 0f, 0f, 0f, 0f, 255);
			return Main.npc[num2];
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x005BD918 File Offset: 0x005BBB18
		private void CreateChester(FrameEventData evt)
		{
			int num;
			int num2;
			DSTFilm.FindFloorAt(this._startPoint + new Vector2(110f, 0f), out num, out num2);
			num2 -= 240;
			int num3 = Projectile.NewProjectile(null, (float)num, (float)num2, 0f, 0f, 960, 0, 0f, Main.myPlayer, -1f, 0f, 0f);
			this._chester = Main.projectile[num3];
			this._chester.velocity.Y = 4f;
			this._chester.velocity.X = 6f;
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x005BD9BC File Offset: 0x005BBBBC
		private void ControlPlayer(FrameEventData evt)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.isControlledByFilm = true;
			localPlayer.controlRight = true;
			int num;
			int num2;
			DSTFilm.FindFloorAt(this._startPoint + new Vector2(150f, 0f), out num, out num2);
			localPlayer.BottomLeft = new Vector2((float)num, (float)num2);
			localPlayer.velocity.X = 6f;
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x005BDA1D File Offset: 0x005BBC1D
		private void StopBeforeCliff(FrameEventData evt)
		{
			Main.LocalPlayer.controlRight = false;
			this._chester.ai[0] = -2f;
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x005BDA3C File Offset: 0x005BBC3C
		private void TurnPlayerToTheLeft(FrameEventData evt)
		{
			Main.LocalPlayer.ChangeDir(-1);
			this._chester.velocity = new Vector2(-0.1f, 0f);
			this._chester.spriteDirection = (this._chester.direction = -1);
			this._deerclops.ai[0] = 1f;
			this._deerclops.ai[1] = 0f;
			this._deerclops.TargetClosest(true);
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x005BDAB8 File Offset: 0x005BBCB8
		private void DeerclopsAttack(FrameEventData evt)
		{
			Main.LocalPlayer.controlJump = true;
			this._chester.velocity.Y = -11.4f;
			this._deerclops.ai[0] = 1f;
			this._deerclops.ai[1] = 0f;
			this._deerclops.TargetClosest(true);
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x005BDB18 File Offset: 0x005BBD18
		private static void FindFloorAt(Vector2 position, out int x, out int y)
		{
			x = (int)position.X;
			y = (int)position.Y;
			int i = x / 16;
			int num = y / 16;
			while (!WorldGen.SolidTile(i, num, false))
			{
				num++;
			}
			y = num * 16;
		}

		// Token: 0x04005126 RID: 20774
		private NPC _deerclops;

		// Token: 0x04005127 RID: 20775
		private Projectile _chester;

		// Token: 0x04005128 RID: 20776
		private Vector2 _startPoint;

		// Token: 0x04005129 RID: 20777
		private Item _oldItem;
	}
}
