using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.Cinematics
{
	// Token: 0x02000744 RID: 1860
	public class DSTFilm : Film
	{
		// Token: 0x06004B84 RID: 19332 RVA: 0x0066B240 File Offset: 0x00669440
		public DSTFilm()
		{
			this.BuildSequence();
		}

		// Token: 0x06004B85 RID: 19333 RVA: 0x0066B24E File Offset: 0x0066944E
		public override void OnBegin()
		{
			this.PrepareScene();
			Main.hideUI = true;
			base.OnBegin();
		}

		// Token: 0x06004B86 RID: 19334 RVA: 0x0066B262 File Offset: 0x00669462
		public override void OnEnd()
		{
			this.ClearScene();
			Main.hideUI = false;
			base.OnEnd();
		}

		// Token: 0x06004B87 RID: 19335 RVA: 0x0066B278 File Offset: 0x00669478
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

		// Token: 0x06004B88 RID: 19336 RVA: 0x0066B378 File Offset: 0x00669578
		private void PrepareScene()
		{
			Main.dayTime = true;
			Main.time = 13500.0;
			Main.time = 43638.0;
			Main.windSpeedCurrent = (Main.windSpeedTarget = 0.36799997f);
			Main.windCounter = 2011;
			Main.cloudAlpha = 0f;
			Main.raining = true;
			Main.rainTime = 3600.0;
			Main.maxRaining = (Main.oldMaxRaining = (Main.cloudAlpha = 0.9f));
			Main.raining = true;
			Main.maxRaining = (Main.oldMaxRaining = (Main.cloudAlpha = 0.6f));
			Main.raining = true;
			Main.maxRaining = (Main.oldMaxRaining = (Main.cloudAlpha = 0.6f));
			this._startPoint = new Point(4050, 488).ToWorldCoordinates(8f, 8f);
			this._startPoint -= new Vector2(1280f, 0f);
		}

		// Token: 0x06004B89 RID: 19337 RVA: 0x0066B471 File Offset: 0x00669671
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

		// Token: 0x06004B8A RID: 19338 RVA: 0x0066B4A8 File Offset: 0x006696A8
		private void EquipDSTShaderItem(FrameEventData evt)
		{
			this._oldItem = Main.LocalPlayer.armor[3];
			Item item = new Item();
			item.SetDefaults(5113);
			Main.LocalPlayer.armor[3] = item;
		}

		// Token: 0x06004B8B RID: 19339 RVA: 0x0066B4E5 File Offset: 0x006696E5
		private void RemoveDSTShaderItem(FrameEventData evt)
		{
			Main.LocalPlayer.armor[3] = this._oldItem;
		}

		// Token: 0x06004B8C RID: 19340 RVA: 0x0066B4FC File Offset: 0x006696FC
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

		// Token: 0x06004B8D RID: 19341 RVA: 0x0066B5E4 File Offset: 0x006697E4
		private NPC PlaceNPCOnGround(int type, Vector2 position)
		{
			int x;
			int y;
			DSTFilm.FindFloorAt(position, out x, out y);
			if (type == 668)
			{
				y -= 240;
			}
			int start = 100;
			int num = NPC.NewNPC(new EntitySource_Film(null), x, y, type, start, 0f, 0f, 0f, 0f, 255);
			return Main.npc[num];
		}

		// Token: 0x06004B8E RID: 19342 RVA: 0x0066B640 File Offset: 0x00669840
		private void CreateChester(FrameEventData evt)
		{
			int x;
			int y;
			DSTFilm.FindFloorAt(this._startPoint + new Vector2(110f, 0f), out x, out y);
			y -= 240;
			int num = Projectile.NewProjectile(null, (float)x, (float)y, 0f, 0f, 960, 0, 0f, Main.myPlayer, -1f, 0f, 0f);
			this._chester = Main.projectile[num];
			this._chester.velocity.Y = 4f;
			this._chester.velocity.X = 6f;
		}

		// Token: 0x06004B8F RID: 19343 RVA: 0x0066B6E4 File Offset: 0x006698E4
		private void ControlPlayer(FrameEventData evt)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.isControlledByFilm = true;
			localPlayer.controlRight = true;
			int x;
			int y;
			DSTFilm.FindFloorAt(this._startPoint + new Vector2(150f, 0f), out x, out y);
			localPlayer.BottomLeft = new Vector2((float)x, (float)y);
			localPlayer.velocity.X = 6f;
		}

		// Token: 0x06004B90 RID: 19344 RVA: 0x0066B745 File Offset: 0x00669945
		private void StopBeforeCliff(FrameEventData evt)
		{
			Main.LocalPlayer.controlRight = false;
			this._chester.ai[0] = -2f;
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x0066B764 File Offset: 0x00669964
		private void TurnPlayerToTheLeft(FrameEventData evt)
		{
			Main.LocalPlayer.ChangeDir(-1);
			this._chester.velocity = new Vector2(-0.1f, 0f);
			this._chester.spriteDirection = (this._chester.direction = -1);
			this._deerclops.ai[0] = 1f;
			this._deerclops.ai[1] = 0f;
			this._deerclops.TargetClosest(true);
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x0066B7E0 File Offset: 0x006699E0
		private void DeerclopsAttack(FrameEventData evt)
		{
			Main.LocalPlayer.controlJump = true;
			this._chester.velocity.Y = -11.4f;
			this._deerclops.ai[0] = 1f;
			this._deerclops.ai[1] = 0f;
			this._deerclops.TargetClosest(true);
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x0066B840 File Offset: 0x00669A40
		private static void FindFloorAt(Vector2 position, out int x, out int y)
		{
			x = (int)position.X;
			y = (int)position.Y;
			int i = x / 16;
			int j = y / 16;
			while (!WorldGen.SolidTile(i, j, false))
			{
				j++;
			}
			y = j * 16;
		}

		// Token: 0x04006087 RID: 24711
		private NPC _deerclops;

		// Token: 0x04006088 RID: 24712
		private Projectile _chester;

		// Token: 0x04006089 RID: 24713
		private Vector2 _startPoint;

		// Token: 0x0400608A RID: 24714
		private Item _oldItem;
	}
}
