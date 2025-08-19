using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Terraria.DataStructures;

namespace Terraria
{
	// Token: 0x02000022 RID: 34
	public class Animation
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00005D03 File Offset: 0x00003F03
		public static void Initialize()
		{
			Animation._animations = new List<Animation>();
			Animation._temporaryAnimations = new Dictionary<Point16, Animation>();
			Animation._awaitingRemoval = new List<Point16>();
			Animation._awaitingAddition = new List<Animation>();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005D30 File Offset: 0x00003F30
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
				break;
			case 1:
				this._frameMax = 5;
				this._frameCounterMax = 12;
				this._frameData = new int[this._frameMax];
				for (int j = 0; j < this._frameMax; j++)
				{
					this._frameData[j] = 5 - j;
				}
				break;
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
				break;
			case 3:
				this._frameMax = 5;
				this._frameCounterMax = 5;
				this._frameData = new int[this._frameMax];
				for (int k = 0; k < this._frameMax; k++)
				{
					this._frameData[k] = k;
				}
				break;
			case 4:
				this._frameMax = 3;
				this._frameCounterMax = 5;
				this._frameData = new int[this._frameMax];
				for (int l = 0; l < this._frameMax; l++)
				{
					this._frameData[l] = 9 + l;
				}
				break;
			}
			Animation.AnimationFrameData data;
			if (Animation.AnimationFrameDatas.TryGetValue(type, out data))
			{
				this._frameMax = data.frames.Length;
				this._frameCounterMax = data.frameRate;
				this._frameData = data.frames;
			}
		}

		/// <summary>
		/// Starts a temporary tile animation of the given type at the given tile coordinates. <paramref name="tileType" /> should match the current <see cref="P:Terraria.Tile.TileType" />. This method should be called on the server in multiplayer to properly sync.
		/// </summary>
		// Token: 0x060000E2 RID: 226 RVA: 0x00005EE8 File Offset: 0x000040E8
		public static void NewTemporaryAnimation(int type, ushort tileType, int x, int y)
		{
			Point16 coordinates = new Point16(x, y);
			if (x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY)
			{
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
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005F58 File Offset: 0x00004158
		private static void RemoveTemporaryAnimation(short x, short y)
		{
			Point16 point = new Point16(x, y);
			if (Animation._temporaryAnimations.ContainsKey(point))
			{
				Animation._awaitingRemoval.Add(point);
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005F88 File Offset: 0x00004188
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
			foreach (KeyValuePair<Point16, Animation> temporaryAnimation in Animation._temporaryAnimations)
			{
				temporaryAnimation.Value.Update();
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

		// Token: 0x060000E5 RID: 229 RVA: 0x00006098 File Offset: 0x00004298
		public unsafe void Update()
		{
			if (this._temporary)
			{
				Tile tile = Main.tile[(int)this._coordinates.X, (int)this._coordinates.Y];
				if (tile != null && *tile.type != this._tileType)
				{
					Animation.RemoveTemporaryAnimation(this._coordinates.X, this._coordinates.Y);
					return;
				}
			}
			this._frameCounter++;
			if (this._frameCounter < this._frameCounterMax)
			{
				return;
			}
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

		/// <summary>
		/// Gets the frame value of the temporary tile animation at the provided tile coordinates. Returns false if there is no animation.
		/// </summary>
		// Token: 0x060000E6 RID: 230 RVA: 0x00006170 File Offset: 0x00004370
		public static bool GetTemporaryFrame(int x, int y, out int frameData)
		{
			Point16 key = new Point16(x, y);
			Animation value;
			if (!Animation._temporaryAnimations.TryGetValue(key, out value))
			{
				frameData = 0;
				return false;
			}
			frameData = value._frameData[value._frame];
			return true;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000061AA File Offset: 0x000043AA
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x000061B1 File Offset: 0x000043B1
		public static int AnimationCount { get; private set; } = 5;

		// Token: 0x060000E9 RID: 233 RVA: 0x000061B9 File Offset: 0x000043B9
		public static void Unload()
		{
			Animation.AnimationCount = 5;
			Animation.AnimationFrameDatas.Clear();
		}

		/// <summary>
		/// Registers a temporary tile animation are returns a unique ID. The animation will play through the provided frames and the provided frameRate. Use the ID with <see cref="M:Terraria.Animation.NewTemporaryAnimation(System.Int32,System.UInt16,System.Int32,System.Int32)" /> to trigger and sync the tempory tile animation.
		/// </summary>
		/// <returns>A unique Id for this specific animation.</returns>
		// Token: 0x060000EA RID: 234 RVA: 0x000061CC File Offset: 0x000043CC
		public static int RegisterTemporaryAnimation(int frameRate, int[] frames)
		{
			int animationType = Animation.AnimationCount++;
			Animation.AnimationFrameDatas[animationType] = new Animation.AnimationFrameData(frameRate, frames);
			return animationType;
		}

		// Token: 0x04000089 RID: 137
		private static List<Animation> _animations;

		// Token: 0x0400008A RID: 138
		private static Dictionary<Point16, Animation> _temporaryAnimations;

		// Token: 0x0400008B RID: 139
		private static List<Point16> _awaitingRemoval;

		// Token: 0x0400008C RID: 140
		private static List<Animation> _awaitingAddition;

		// Token: 0x0400008D RID: 141
		private bool _temporary;

		// Token: 0x0400008E RID: 142
		private Point16 _coordinates;

		// Token: 0x0400008F RID: 143
		private ushort _tileType;

		// Token: 0x04000090 RID: 144
		private int _frame;

		// Token: 0x04000091 RID: 145
		private int _frameMax;

		// Token: 0x04000092 RID: 146
		private int _frameCounter;

		// Token: 0x04000093 RID: 147
		private int _frameCounterMax;

		// Token: 0x04000094 RID: 148
		private int[] _frameData;

		// Token: 0x04000096 RID: 150
		public static Dictionary<int, Animation.AnimationFrameData> AnimationFrameDatas = new Dictionary<int, Animation.AnimationFrameData>();

		// Token: 0x02000787 RID: 1927
		public class AnimationFrameData : IEquatable<Animation.AnimationFrameData>
		{
			// Token: 0x06004D9C RID: 19868 RVA: 0x0067352E File Offset: 0x0067172E
			public AnimationFrameData(int frameRate, int[] frames)
			{
				this.frameRate = frameRate;
				this.frames = frames;
				base..ctor();
			}

			// Token: 0x17000892 RID: 2194
			// (get) Token: 0x06004D9D RID: 19869 RVA: 0x00673544 File Offset: 0x00671744
			[Nullable(1)]
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[NullableContext(1)]
				[CompilerGenerated]
				get
				{
					return typeof(Animation.AnimationFrameData);
				}
			}

			// Token: 0x17000893 RID: 2195
			// (get) Token: 0x06004D9E RID: 19870 RVA: 0x00673550 File Offset: 0x00671750
			// (set) Token: 0x06004D9F RID: 19871 RVA: 0x00673558 File Offset: 0x00671758
			public int frameRate { get; set; }

			// Token: 0x17000894 RID: 2196
			// (get) Token: 0x06004DA0 RID: 19872 RVA: 0x00673561 File Offset: 0x00671761
			// (set) Token: 0x06004DA1 RID: 19873 RVA: 0x00673569 File Offset: 0x00671769
			public int[] frames { get; set; }

			// Token: 0x06004DA2 RID: 19874 RVA: 0x00673574 File Offset: 0x00671774
			[NullableContext(1)]
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("AnimationFrameData");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x06004DA3 RID: 19875 RVA: 0x006735C0 File Offset: 0x006717C0
			[NullableContext(1)]
			[CompilerGenerated]
			protected virtual bool PrintMembers(StringBuilder builder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				builder.Append("frameRate = ");
				builder.Append(this.frameRate.ToString());
				builder.Append(", frames = ");
				builder.Append(this.frames);
				return true;
			}

			// Token: 0x06004DA4 RID: 19876 RVA: 0x00673613 File Offset: 0x00671813
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator !=(Animation.AnimationFrameData left, Animation.AnimationFrameData right)
			{
				return !(left == right);
			}

			// Token: 0x06004DA5 RID: 19877 RVA: 0x0067361F File Offset: 0x0067181F
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator ==(Animation.AnimationFrameData left, Animation.AnimationFrameData right)
			{
				return left == right || (left != null && left.Equals(right));
			}

			// Token: 0x06004DA6 RID: 19878 RVA: 0x00673633 File Offset: 0x00671833
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<frameRate>k__BackingField)) * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(this.<frames>k__BackingField);
			}

			// Token: 0x06004DA7 RID: 19879 RVA: 0x00673673 File Offset: 0x00671873
			[NullableContext(2)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as Animation.AnimationFrameData);
			}

			// Token: 0x06004DA8 RID: 19880 RVA: 0x00673684 File Offset: 0x00671884
			[NullableContext(2)]
			[CompilerGenerated]
			public virtual bool Equals(Animation.AnimationFrameData other)
			{
				return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<frameRate>k__BackingField, other.<frameRate>k__BackingField) && EqualityComparer<int[]>.Default.Equals(this.<frames>k__BackingField, other.<frames>k__BackingField));
			}

			// Token: 0x06004DAA RID: 19882 RVA: 0x006736E5 File Offset: 0x006718E5
			[CompilerGenerated]
			protected AnimationFrameData([Nullable(1)] Animation.AnimationFrameData original)
			{
				this.frameRate = original.<frameRate>k__BackingField;
				this.frames = original.<frames>k__BackingField;
			}

			// Token: 0x06004DAB RID: 19883 RVA: 0x00673705 File Offset: 0x00671905
			[CompilerGenerated]
			public void Deconstruct(out int frameRate, out int[] frames)
			{
				frameRate = this.frameRate;
				frames = this.frames;
			}
		}
	}
}
