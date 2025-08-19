using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameInput;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020005C5 RID: 1477
	public abstract class AHoverInteractionChecker
	{
		// Token: 0x060042C7 RID: 17095 RVA: 0x005FA6B4 File Offset: 0x005F88B4
		internal AHoverInteractionChecker.HoverStatus AttemptInteraction(Player player, Rectangle Hitbox)
		{
			Point point = Hitbox.ClosestPointInRect(player.Center).ToTileCoordinates();
			if (!player.IsInTileInteractionRange(point.X, point.Y, TileReachCheckSettings.Simple))
			{
				return AHoverInteractionChecker.HoverStatus.NotSelectable;
			}
			Matrix matrix = Matrix.Invert(Main.GameViewMatrix.ZoomMatrix);
			Vector2 vector = Main.ReverseGravitySupport(Main.MouseScreen, 0f);
			Vector2.Transform(Main.screenPosition, matrix);
			Vector2 v = vector + Main.screenPosition;
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

		// Token: 0x060042C8 RID: 17096
		internal abstract bool? AttemptOverridingHoverStatus(Player player, Rectangle rectangle);

		// Token: 0x060042C9 RID: 17097
		internal abstract void DoHoverEffect(Player player, Rectangle hitbox);

		// Token: 0x060042CA RID: 17098
		internal abstract bool ShouldBlockInteraction(Player player, Rectangle hitbox);

		// Token: 0x060042CB RID: 17099
		internal abstract void PerformInteraction(Player player, Rectangle hitbox);

		// Token: 0x02000C66 RID: 3174
		internal enum HoverStatus
		{
			// Token: 0x04007985 RID: 31109
			NotSelectable,
			// Token: 0x04007986 RID: 31110
			SelectableButNotSelected,
			// Token: 0x04007987 RID: 31111
			Selected
		}
	}
}
