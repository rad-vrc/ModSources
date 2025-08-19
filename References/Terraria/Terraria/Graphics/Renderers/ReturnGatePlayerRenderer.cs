using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200012B RID: 299
	internal class ReturnGatePlayerRenderer : IPlayerRenderer
	{
		// Token: 0x06001781 RID: 6017 RVA: 0x004D3594 File Offset: 0x004D1794
		public void DrawPlayers(Camera camera, IEnumerable<Player> players)
		{
			foreach (Player player in players)
			{
				this.DrawReturnGateInWorld(camera, player);
			}
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x004D35E0 File Offset: 0x004D17E0
		public void DrawPlayerHead(Camera camera, Player drawPlayer, Vector2 position, float alpha = 1f, float scale = 1f, Color borderColor = default(Color))
		{
			this.DrawReturnGateInMap(camera, drawPlayer);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x004D35EA File Offset: 0x004D17EA
		public void DrawPlayer(Camera camera, Player drawPlayer, Vector2 position, float rotation, Vector2 rotationOrigin, float shadow = 0f, float scale = 1f)
		{
			this.DrawReturnGateInWorld(camera, drawPlayer);
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		private void DrawReturnGateInMap(Camera camera, Player player)
		{
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x004D35F4 File Offset: 0x004D17F4
		private void DrawReturnGateInWorld(Camera camera, Player player)
		{
			Rectangle empty = Rectangle.Empty;
			if (!PotionOfReturnHelper.TryGetGateHitbox(player, out empty))
			{
				return;
			}
			AHoverInteractionChecker.HoverStatus hoverStatus = AHoverInteractionChecker.HoverStatus.NotSelectable;
			if (player == Main.LocalPlayer)
			{
				this._interactionChecker.AttemptInteraction(player, empty);
			}
			if (Main.SmartInteractPotionOfReturn)
			{
				hoverStatus = AHoverInteractionChecker.HoverStatus.Selected;
			}
			int selectionMode = (int)hoverStatus;
			if (player.PotionOfReturnOriginalUsePosition == null)
			{
				return;
			}
			SpriteBatch spriteBatch = camera.SpriteBatch;
			SamplerState sampler = camera.Sampler;
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, sampler, DepthStencilState.None, camera.Rasterizer, null, camera.GameViewMatrix.TransformationMatrix);
			float opacity = (player.whoAmI == Main.myPlayer) ? 1f : 0.1f;
			Vector2 value = player.PotionOfReturnOriginalUsePosition.Value;
			Vector2 value2 = new Vector2(0f, -21f);
			Vector2 worldPosition = value + value2;
			Vector2 worldPosition2 = empty.Center.ToVector2();
			PotionOfReturnGateHelper potionOfReturnGateHelper = new PotionOfReturnGateHelper(PotionOfReturnGateHelper.GateType.ExitPoint, worldPosition, opacity);
			PotionOfReturnGateHelper potionOfReturnGateHelper2 = new PotionOfReturnGateHelper(PotionOfReturnGateHelper.GateType.EntryPoint, worldPosition2, opacity);
			if (!Main.gamePaused)
			{
				potionOfReturnGateHelper.Update();
				potionOfReturnGateHelper2.Update();
			}
			this._voidLensData.Clear();
			potionOfReturnGateHelper.DrawToDrawData(this._voidLensData, 0);
			potionOfReturnGateHelper2.DrawToDrawData(this._voidLensData, selectionMode);
			foreach (DrawData drawData in this._voidLensData)
			{
				drawData.Draw(spriteBatch);
			}
			spriteBatch.End();
		}

		// Token: 0x0400144A RID: 5194
		private List<DrawData> _voidLensData = new List<DrawData>();

		// Token: 0x0400144B RID: 5195
		private PotionOfReturnGateInteractionChecker _interactionChecker = new PotionOfReturnGateInteractionChecker();
	}
}
