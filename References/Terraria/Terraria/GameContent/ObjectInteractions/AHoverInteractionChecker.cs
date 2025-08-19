using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameInput;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x0200025D RID: 605
	public abstract class AHoverInteractionChecker
	{
		// Token: 0x06001F7F RID: 8063 RVA: 0x00515080 File Offset: 0x00513280
		internal AHoverInteractionChecker.HoverStatus AttemptInteraction(Player player, Rectangle Hitbox)
		{
			Point point = Hitbox.ClosestPointInRect(player.Center).ToTileCoordinates();
			if (!player.IsInTileInteractionRange(point.X, point.Y, TileReachCheckSettings.Simple))
			{
				return AHoverInteractionChecker.HoverStatus.NotSelectable;
			}
			Matrix matrix = Matrix.Invert(Main.GameViewMatrix.ZoomMatrix);
			Vector2 value = Main.ReverseGravitySupport(Main.MouseScreen, 0f);
			Vector2.Transform(Main.screenPosition, matrix);
			Vector2 v = value + Main.screenPosition;
			bool flag = Hitbox.Contains(v.ToPoint());
			bool flag2 = flag;
			bool? flag3 = this.AttemptOverridingHoverStatus(player, Hitbox);
			if (flag3 != null)
			{
				flag2 = flag3.Value;
			}
			flag2 &= !player.lastMouseInterface;
			bool flag4 = !Main.SmartCursorIsUsed && !PlayerInput.UsingGamepad;
			if (!flag2)
			{
				if (!flag4)
				{
					return AHoverInteractionChecker.HoverStatus.SelectableButNotSelected;
				}
				return AHoverInteractionChecker.HoverStatus.NotSelectable;
			}
			else
			{
				Main.HasInteractibleObjectThatIsNotATile = true;
				if (flag)
				{
					this.DoHoverEffect(player, Hitbox);
				}
				if (PlayerInput.UsingGamepad)
				{
					player.GamepadEnableGrappleCooldown();
				}
				bool flag5 = this.ShouldBlockInteraction(player, Hitbox);
				if (Main.mouseRight && Main.mouseRightRelease && !flag5)
				{
					Main.mouseRightRelease = false;
					player.tileInteractAttempted = true;
					player.tileInteractionHappened = true;
					player.releaseUseTile = false;
					this.PerformInteraction(player, Hitbox);
				}
				if (!Main.SmartCursorIsUsed && !PlayerInput.UsingGamepad)
				{
					return AHoverInteractionChecker.HoverStatus.NotSelectable;
				}
				if (!flag4)
				{
					return AHoverInteractionChecker.HoverStatus.Selected;
				}
				return AHoverInteractionChecker.HoverStatus.NotSelectable;
			}
		}

		// Token: 0x06001F80 RID: 8064
		internal abstract bool? AttemptOverridingHoverStatus(Player player, Rectangle rectangle);

		// Token: 0x06001F81 RID: 8065
		internal abstract void DoHoverEffect(Player player, Rectangle hitbox);

		// Token: 0x06001F82 RID: 8066
		internal abstract bool ShouldBlockInteraction(Player player, Rectangle hitbox);

		// Token: 0x06001F83 RID: 8067
		internal abstract void PerformInteraction(Player player, Rectangle hitbox);

		// Token: 0x0200063D RID: 1597
		internal enum HoverStatus
		{
			// Token: 0x04006143 RID: 24899
			NotSelectable,
			// Token: 0x04006144 RID: 24900
			SelectableButNotSelected,
			// Token: 0x04006145 RID: 24901
			Selected
		}
	}
}
