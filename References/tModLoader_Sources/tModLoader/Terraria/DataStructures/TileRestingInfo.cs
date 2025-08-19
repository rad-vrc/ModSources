using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Holds data required for offsetting an entity when it rests on a tile (sitting/sleeping).
	/// </summary>
	// Token: 0x0200073B RID: 1851
	public struct TileRestingInfo
	{
		// Token: 0x06004B39 RID: 19257 RVA: 0x00669A4D File Offset: 0x00667C4D
		public TileRestingInfo(Entity restingEntity, Point anchorTilePosition, Vector2 visualOffset, int targetDirection, int directionOffset = 0, Vector2 finalOffset = default(Vector2), ExtraSeatInfo extraInfo = default(ExtraSeatInfo))
		{
			this.RestingEntity = restingEntity;
			this.AnchorTilePosition = anchorTilePosition;
			this.VisualOffset = visualOffset;
			this.TargetDirection = targetDirection;
			this.DirectionOffset = directionOffset;
			this.FinalOffset = finalOffset;
			this.ExtraInfo = extraInfo;
		}

		// Token: 0x06004B3A RID: 19258 RVA: 0x00669A84 File Offset: 0x00667C84
		public void Deconstruct(out Entity restingEntity, out Point anchorTilePosition, out Vector2 visualOffset, out int targetDirection, out int directionOffset, out Vector2 finalOffset, out ExtraSeatInfo extraInfo)
		{
			restingEntity = this.RestingEntity;
			anchorTilePosition = this.AnchorTilePosition;
			visualOffset = this.VisualOffset;
			targetDirection = this.TargetDirection;
			directionOffset = this.DirectionOffset;
			finalOffset = this.FinalOffset;
			extraInfo = this.ExtraInfo;
		}

		/// <summary>
		/// The resting entity (Player or NPC). Can be null if not available from the context.
		/// </summary>
		// Token: 0x04006059 RID: 24665
		public Entity RestingEntity;

		/// <summary>
		/// The bottom-most position of the resting tile in tile coordinates, affecting logic for resetting (invalid) resting state and used to align the hitbox.
		/// </summary>
		// Token: 0x0400605A RID: 24666
		public Point AnchorTilePosition;

		/// <summary>
		/// The visual offset of the entity, not affecting any logic.
		/// </summary>
		// Token: 0x0400605B RID: 24667
		public Vector2 VisualOffset;

		/// <summary>
		/// Direction the entity is facing while resting. Is 0 by default for beds.
		/// </summary>
		// Token: 0x0400605C RID: 24668
		public int TargetDirection;

		/// <summary>
		/// Length of the entity position offset applied in the X direction based on targetDirection.
		/// </summary>
		// Token: 0x0400605D RID: 24669
		public int DirectionOffset;

		/// <summary>
		/// Offset applied to the final anchor position. Use with caution, vanilla does not utilize it!
		/// </summary>
		// Token: 0x0400605E RID: 24670
		public Vector2 FinalOffset;

		/// <summary>
		/// Contains additional information, such as <see cref="F:Terraria.GameContent.ExtraSeatInfo.IsAToilet" />.
		/// </summary>
		// Token: 0x0400605F RID: 24671
		public ExtraSeatInfo ExtraInfo;
	}
}
