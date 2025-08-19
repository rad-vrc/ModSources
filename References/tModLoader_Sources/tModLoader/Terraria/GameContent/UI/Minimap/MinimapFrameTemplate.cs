using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.UI.Minimap
{
	// Token: 0x020004F7 RID: 1271
	public class MinimapFrameTemplate
	{
		// Token: 0x06003D93 RID: 15763 RVA: 0x005CB179 File Offset: 0x005C9379
		public MinimapFrameTemplate(string name, Vector2 frameOffset, Vector2 resetPosition, Vector2 zoomInPosition, Vector2 zoomOutPosition)
		{
			this.name = name;
			this.frameOffset = frameOffset;
			this.resetPosition = resetPosition;
			this.zoomInPosition = zoomInPosition;
			this.zoomOutPosition = zoomOutPosition;
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x005CB1A8 File Offset: 0x005C93A8
		public MinimapFrame CreateInstance(AssetRequestMode mode)
		{
			MinimapFrame minimapFrame = new MinimapFrame(MinimapFrameTemplate.LoadAsset<Texture2D>("Images\\UI\\Minimap\\" + this.name + "\\MinimapFrame", mode), this.frameOffset);
			minimapFrame.NameKey = this.name;
			minimapFrame.ConfigKey = this.name;
			minimapFrame.SetResetButton(MinimapFrameTemplate.LoadAsset<Texture2D>("Images\\UI\\Minimap\\" + this.name + "\\MinimapButton_Reset", mode), this.resetPosition);
			minimapFrame.SetZoomOutButton(MinimapFrameTemplate.LoadAsset<Texture2D>("Images\\UI\\Minimap\\" + this.name + "\\MinimapButton_ZoomOut", mode), this.zoomOutPosition);
			minimapFrame.SetZoomInButton(MinimapFrameTemplate.LoadAsset<Texture2D>("Images\\UI\\Minimap\\" + this.name + "\\MinimapButton_ZoomIn", mode), this.zoomInPosition);
			return minimapFrame;
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x005CB268 File Offset: 0x005C9468
		private static Asset<T> LoadAsset<T>(string assetName, AssetRequestMode mode) where T : class
		{
			return Main.Assets.Request<T>(assetName, mode);
		}

		// Token: 0x04005659 RID: 22105
		private string name;

		// Token: 0x0400565A RID: 22106
		private Vector2 frameOffset;

		// Token: 0x0400565B RID: 22107
		private Vector2 resetPosition;

		// Token: 0x0400565C RID: 22108
		private Vector2 zoomInPosition;

		// Token: 0x0400565D RID: 22109
		private Vector2 zoomOutPosition;
	}
}
