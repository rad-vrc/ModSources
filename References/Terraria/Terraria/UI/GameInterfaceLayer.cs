using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameInput;

namespace Terraria.UI
{
	// Token: 0x02000081 RID: 129
	public class GameInterfaceLayer
	{
		// Token: 0x060011D9 RID: 4569 RVA: 0x0048F7AE File Offset: 0x0048D9AE
		public GameInterfaceLayer(string name, InterfaceScaleType scaleType)
		{
			this.Name = name;
			this.ScaleType = scaleType;
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0048F7C4 File Offset: 0x0048D9C4
		public bool Draw()
		{
			Matrix transformMatrix;
			if (this.ScaleType == InterfaceScaleType.Game)
			{
				PlayerInput.SetZoom_World();
				transformMatrix = Main.GameViewMatrix.ZoomMatrix;
			}
			else if (this.ScaleType == InterfaceScaleType.UI)
			{
				PlayerInput.SetZoom_UI();
				transformMatrix = Main.UIScaleMatrix;
			}
			else
			{
				PlayerInput.SetZoom_Unscaled();
				transformMatrix = Matrix.Identity;
			}
			bool result = false;
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, transformMatrix);
			try
			{
				result = this.DrawSelf();
			}
			catch (Exception e)
			{
				TimeLogger.DrawException(e);
			}
			Main.spriteBatch.End();
			return result;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0003266D File Offset: 0x0003086D
		protected virtual bool DrawSelf()
		{
			return true;
		}

		// Token: 0x04000FE9 RID: 4073
		public readonly string Name;

		// Token: 0x04000FEA RID: 4074
		public InterfaceScaleType ScaleType;
	}
}
