using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200045E RID: 1118
	internal class ReturnGatePlayerRenderer : IPlayerRenderer
	{
		// Token: 0x060036BB RID: 14011 RVA: 0x0057E898 File Offset: 0x0057CA98
		public void DrawPlayers(Camera camera, IEnumerable<Player> players)
		{
			foreach (Player player in players)
			{
				this.DrawReturnGateInWorld(camera, player);
			}
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x0057E8E4 File Offset: 0x0057CAE4
		public void DrawPlayerHead(Camera camera, Player drawPlayer, Vector2 position, float alpha = 1f, float scale = 1f, Color borderColor = default(Color))
		{
			this.DrawReturnGateInMap(camera, drawPlayer);
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x0057E8EE File Offset: 0x0057CAEE
		public void DrawPlayer(Camera camera, Player drawPlayer, Vector2 position, float rotation, Vector2 rotationOrigin, float shadow = 0f, float scale = 1f)
		{
			this.DrawReturnGateInWorld(camera, drawPlayer);
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x0057E8F8 File Offset: 0x0057CAF8
		private void DrawReturnGateInMap(Camera camera, Player player)
		{
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x0057E8FC File Offset: 0x0057CAFC
		private void DrawReturnGateInWorld(Camera camera, Player player)
		{
			Rectangle homeHitbox = Rectangle.Empty;
			if (!PotionOfReturnHelper.TryGetGateHitbox(player, out homeHitbox))
			{
				return;
			}
			AHoverInteractionChecker.HoverStatus hoverStatus = AHoverInteractionChecker.HoverStatus.NotSelectable;
			if (player == Main.LocalPlayer)
			{
				this._interactionChecker.AttemptInteraction(player, homeHitbox);
			}
			if (Main.SmartInteractPotionOfReturn)
			{
				hoverStatus = AHoverInteractionChecker.HoverStatus.Selected;
			}
			int num = (int)hoverStatus;
			if (player.PotionOfReturnOriginalUsePosition == null)
			{
				return;
			}
			SpriteBatch spriteBatch = camera.SpriteBatch;
			SamplerState sampler = camera.Sampler;
			spriteBatch.Begin(1, BlendState.AlphaBlend, sampler, DepthStencilState.None, camera.Rasterizer, null, camera.GameViewMatrix.TransformationMatrix);
			float opacity = (player.whoAmI == Main.myPlayer) ? 1f : 0.1f;
			Vector2 value = player.PotionOfReturnOriginalUsePosition.Value;
			Vector2 vector;
			vector..ctor(0f, -21f);
			Vector2 worldPosition = value + vector;
			Vector2 worldPosition2 = homeHitbox.Center.ToVector2();
			PotionOfReturnGateHelper potionOfReturnGateHelper = new PotionOfReturnGateHelper(PotionOfReturnGateHelper.GateType.ExitPoint, worldPosition, opacity);
			PotionOfReturnGateHelper potionOfReturnGateHelper2 = new PotionOfReturnGateHelper(PotionOfReturnGateHelper.GateType.EntryPoint, worldPosition2, opacity);
			if (!Main.gamePaused)
			{
				potionOfReturnGateHelper.Update();
				potionOfReturnGateHelper2.Update();
			}
			this._voidLensData.Clear();
			potionOfReturnGateHelper.DrawToDrawData(this._voidLensData, 0);
			potionOfReturnGateHelper2.DrawToDrawData(this._voidLensData, num);
			foreach (DrawData voidLensDatum in this._voidLensData)
			{
				voidLensDatum.Draw(spriteBatch);
			}
			spriteBatch.End();
		}

		// Token: 0x0400508D RID: 20621
		private List<DrawData> _voidLensData = new List<DrawData>();

		// Token: 0x0400508E RID: 20622
		private PotionOfReturnGateInteractionChecker _interactionChecker = new PotionOfReturnGateInteractionChecker();
	}
}
