using System;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace Terraria
{
	// Token: 0x02000051 RID: 81
	public class Animation
	{
		// Token: 0x06001088 RID: 4232 RVA: 0x00480A1D File Offset: 0x0047EC1D
		public static void Initialize()
		{
			Animation._animations = new List<Animation>();
			Animation._temporaryAnimations = new Dictionary<Point16, Animation>();
			Animation._awaitingRemoval = new List<Point16>();
			Animation._awaitingAddition = new List<Animation>();
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00480A48 File Offset: 0x0047EC48
		private void SetDefaults(int type)
		{
			this._tileType = 0;
			this._frame = 0;
			this._frameMax = 0;
			this._frameCounter = 0;
			this._frameCounterMax = 0;
			this._temporary = false;
			switch (type)
			{
			case 0:
				this._frameMax = 5;
				this._frameCounterMax = 12;
				this._frameData = new int[this._frameMax];
				for (int i = 0; i < this._frameMax; i++)
				{
					this._frameData[i] = i + 1;
				}
				return;
			case 1:
				this._frameMax = 5;
				this._frameCounterMax = 12;
				this._frameData = new int[this._frameMax];
				for (int j = 0; j < this._frameMax; j++)
				{
					this._frameData[j] = 5 - j;
				}
				return;
			case 2:
				this._frameCounterMax = 6;
				this._frameData = new int[]
				{
					1,
					2,
					2,
					2,
					1
				};
				this._frameMax = this._frameData.Length;
				return;
			case 3:
				this._frameMax = 5;
				this._frameCounterMax = 5;
				this._frameData = new int[this._frameMax];
				for (int k = 0; k < this._frameMax; k++)
				{
					this._frameData[k] = k;
				}
				return;
			case 4:
				this._frameMax = 3;
				this._frameCounterMax = 5;
				this._frameData = new int[this._frameMax];
				for (int l = 0; l < this._frameMax; l++)
				{
					this._frameData[l] = 9 + l;
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00480BB8 File Offset: 0x0047EDB8
		public static void NewTemporaryAnimation(int type, ushort tileType, int x, int y)
		{
			Point16 coordinates = new Point16(x, y);
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return;
			}
			Animation animation = new Animation();
			animation.SetDefaults(type);
			animation._tileType = tileType;
			animation._coordinates = coordinates;
			animation._temporary = true;
			Animation._awaitingAddition.Add(animation);
			if (Main.netMode == 2)
			{
				NetMessage.SendTemporaryAnimation(-1, type, (int)tileType, x, y);
			}
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x00480C28 File Offset: 0x0047EE28
		private static void RemoveTemporaryAnimation(short x, short y)
		{
			Point16 point = new Point16(x, y);
			if (Animation._temporaryAnimations.ContainsKey(point))
			{
				Animation._awaitingRemoval.Add(point);
			}
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00480C58 File Offset: 0x0047EE58
		public static void UpdateAll()
		{
			for (int i = 0; i < Animation._animations.Count; i++)
			{
				Animation._animations[i].Update();
			}
			if (Animation._awaitingAddition.Count > 0)
			{
				for (int j = 0; j < Animation._awaitingAddition.Count; j++)
				{
					Animation animation = Animation._awaitingAddition[j];
					Animation._temporaryAnimations[animation._coordinates] = animation;
				}
				Animation._awaitingAddition.Clear();
			}
			foreach (KeyValuePair<Point16, Animation> keyValuePair in Animation._temporaryAnimations)
			{
				keyValuePair.Value.Update();
			}
			if (Animation._awaitingRemoval.Count > 0)
			{
				for (int k = 0; k < Animation._awaitingRemoval.Count; k++)
				{
					Animation._temporaryAnimations.Remove(Animation._awaitingRemoval[k]);
				}
				Animation._awaitingRemoval.Clear();
			}
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00480D68 File Offset: 0x0047EF68
		public void Update()
		{
			if (this._temporary)
			{
				Tile tile = Main.tile[(int)this._coordinates.X, (int)this._coordinates.Y];
				if (tile != null && tile.type != this._tileType)
				{
					Animation.RemoveTemporaryAnimation(this._coordinates.X, this._coordinates.Y);
					return;
				}
			}
			this._frameCounter++;
			if (this._frameCounter >= this._frameCounterMax)
			{
				this._frameCounter = 0;
				this._frame++;
				if (this._frame >= this._frameMax)
				{
					this._frame = 0;
					if (this._temporary)
					{
						Animation.RemoveTemporaryAnimation(this._coordinates.X, this._coordinates.Y);
					}
				}
			}
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00480E34 File Offset: 0x0047F034
		public static bool GetTemporaryFrame(int x, int y, out int frameData)
		{
			Point16 key = new Point16(x, y);
			Animation animation;
			if (!Animation._temporaryAnimations.TryGetValue(key, out animation))
			{
				frameData = 0;
				return false;
			}
			frameData = animation._frameData[animation._frame];
			return true;
		}

		// Token: 0x04000E8D RID: 3725
		private static List<Animation> _animations;

		// Token: 0x04000E8E RID: 3726
		private static Dictionary<Point16, Animation> _temporaryAnimations;

		// Token: 0x04000E8F RID: 3727
		private static List<Point16> _awaitingRemoval;

		// Token: 0x04000E90 RID: 3728
		private static List<Animation> _awaitingAddition;

		// Token: 0x04000E91 RID: 3729
		private bool _temporary;

		// Token: 0x04000E92 RID: 3730
		private Point16 _coordinates;

		// Token: 0x04000E93 RID: 3731
		private ushort _tileType;

		// Token: 0x04000E94 RID: 3732
		private int _frame;

		// Token: 0x04000E95 RID: 3733
		private int _frameMax;

		// Token: 0x04000E96 RID: 3734
		private int _frameCounter;

		// Token: 0x04000E97 RID: 3735
		private int _frameCounterMax;

		// Token: 0x04000E98 RID: 3736
		private int[] _frameData;
	}
}
