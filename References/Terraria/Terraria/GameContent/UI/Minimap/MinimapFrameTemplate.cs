using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.UI.Minimap
{
	// Token: 0x020003C0 RID: 960
	public class MinimapFrameTemplate
	{
		// Token: 0x06002A54 RID: 10836 RVA: 0x00599105 File Offset: 0x00597305
		public MinimapFrameTemplate(string name, Vector2 frameOffset, Vector2 resetPosition, Vector2 zoomInPosition, Vector2 zoomOutPosition)
		{
			this.name = name;
			this.frameOffset = frameOffset;
			this.resetPosition = resetPosition;
			this.zoomInPosition = zoomInPosition;
			this.zoomOutPosition = zoomOutPosition;
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x00599134 File Offset: 0x00597334
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

		// Token: 0x06002A56 RID: 10838 RVA: 0x005991F4 File Offset: 0x005973F4
		private static Asset<T> LoadAsset<T>(string assetName, AssetRequestMode mode) where T : class
		{
			return Main.Assets.Request<T>(assetName, mode);
		}

		// Token: 0x04004D24 RID: 19748
		private string name;

		// Token: 0x04004D25 RID: 19749
		private Vector2 frameOffset;

		// Token: 0x04004D26 RID: 19750
		private Vector2 resetPosition;

		// Token: 0x04004D27 RID: 19751
		private Vector2 zoomInPosition;

		// Token: 0x04004D28 RID: 19752
		private Vector2 zoomOutPosition;
	}
}
