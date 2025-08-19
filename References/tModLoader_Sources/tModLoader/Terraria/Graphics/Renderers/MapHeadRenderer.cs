using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000457 RID: 1111
	public class MapHeadRenderer : INeedRenderTargetContent
	{
		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x0600369D RID: 13981 RVA: 0x0057DBC3 File Offset: 0x0057BDC3
		public bool IsReady
		{
			get
			{
				return !this._anyDirty;
			}
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x0057DBD0 File Offset: 0x0057BDD0
		public MapHeadRenderer()
		{
			for (int i = 0; i < this._playerRenders.Length; i++)
			{
				this._playerRenders[i] = new PlayerHeadDrawRenderTargetContent();
			}
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x0057DC20 File Offset: 0x0057BE20
		public void Reset()
		{
			this._anyDirty = false;
			this._drawData.Clear();
			for (int i = 0; i < this._playerRenders.Length; i++)
			{
				this._playerRenders[i].Reset();
			}
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x0057DC60 File Offset: 0x0057BE60
		public void DrawPlayerHead(Camera camera, Player drawPlayer, Vector2 position, float alpha = 1f, float scale = 1f, Color borderColor = default(Color))
		{
			PlayerHeadDrawRenderTargetContent playerHeadDrawRenderTargetContent = this._playerRenders[drawPlayer.whoAmI];
			playerHeadDrawRenderTargetContent.UsePlayer(drawPlayer);
			playerHeadDrawRenderTargetContent.UseColor(borderColor);
			playerHeadDrawRenderTargetContent.Request();
			this._anyDirty = true;
			this._drawData.Clear();
			if (playerHeadDrawRenderTargetContent.IsReady)
			{
				RenderTarget2D target = playerHeadDrawRenderTargetContent.GetTarget();
				this._drawData.Add(new DrawData(target, position, null, Color.White, 0f, target.Size() / 2f, scale, 0, 0f));
				this.RenderDrawData(drawPlayer);
			}
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x0057DCF8 File Offset: 0x0057BEF8
		private void RenderDrawData(Player drawPlayer)
		{
			Effect pixelShader = Main.pixelShader;
			Projectile[] projectile = Main.projectile;
			SpriteBatch spriteBatch = Main.spriteBatch;
			for (int i = 0; i < this._drawData.Count; i++)
			{
				DrawData cdd = this._drawData[i];
				if (cdd.sourceRect == null)
				{
					cdd.sourceRect = new Rectangle?(cdd.texture.Frame(1, 1, 0, 0, 0, 0));
				}
				PlayerDrawHelper.SetShaderForData(drawPlayer, drawPlayer.cHead, ref cdd);
				if (cdd.texture != null)
				{
					cdd.Draw(spriteBatch);
				}
			}
			pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x0057DD9C File Offset: 0x0057BF9C
		public void PrepareRenderTarget(GraphicsDevice device, SpriteBatch spriteBatch)
		{
			if (this._anyDirty)
			{
				for (int i = 0; i < this._playerRenders.Length; i++)
				{
					this._playerRenders[i].PrepareRenderTarget(device, spriteBatch);
				}
				this._anyDirty = false;
			}
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x0057DDDC File Offset: 0x0057BFDC
		private void CreateOutlines(float alpha, float scale, Color borderColor)
		{
			if (!(borderColor != Color.Transparent))
			{
				return;
			}
			List<DrawData> collection = new List<DrawData>(this._drawData);
			List<DrawData> list = new List<DrawData>(this._drawData);
			this._drawData.Clear();
			float num = 2f * scale;
			Color color = borderColor * (alpha * alpha);
			Color black = Color.Black;
			black *= alpha * alpha;
			int colorOnlyShaderIndex = ContentSamples.CommonlyUsedContentSamples.ColorOnlyShaderIndex;
			for (int i = 0; i < list.Count; i++)
			{
				DrawData value = list[i];
				value.shader = colorOnlyShaderIndex;
				value.color = black;
				list[i] = value;
			}
			int num2 = 2;
			Vector2 vector;
			for (int j = -num2; j <= num2; j++)
			{
				for (int k = -num2; k <= num2; k++)
				{
					if (Math.Abs(j) + Math.Abs(k) == num2)
					{
						vector..ctor((float)j * num, (float)k * num);
						for (int l = 0; l < list.Count; l++)
						{
							DrawData item = list[l];
							item.position += vector;
							this._drawData.Add(item);
						}
					}
				}
			}
			for (int m = 0; m < list.Count; m++)
			{
				DrawData value2 = list[m];
				value2.shader = colorOnlyShaderIndex;
				value2.color = color;
				list[m] = value2;
			}
			vector = Vector2.Zero;
			num2 = 1;
			for (int n = -num2; n <= num2; n++)
			{
				for (int num3 = -num2; num3 <= num2; num3++)
				{
					if (Math.Abs(n) + Math.Abs(num3) == num2)
					{
						vector..ctor((float)n * num, (float)num3 * num);
						for (int num4 = 0; num4 < list.Count; num4++)
						{
							DrawData item2 = list[num4];
							item2.position += vector;
							this._drawData.Add(item2);
						}
					}
				}
			}
			this._drawData.AddRange(collection);
		}

		// Token: 0x0400506F RID: 20591
		private bool _anyDirty;

		// Token: 0x04005070 RID: 20592
		private PlayerHeadDrawRenderTargetContent[] _playerRenders = new PlayerHeadDrawRenderTargetContent[255];

		// Token: 0x04005071 RID: 20593
		private readonly List<DrawData> _drawData = new List<DrawData>();
	}
}
