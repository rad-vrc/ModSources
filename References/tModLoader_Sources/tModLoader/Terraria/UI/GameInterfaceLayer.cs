using System;
using Microsoft.Xna.Framework;
using Terraria.GameInput;

namespace Terraria.UI
{
	// Token: 0x020000A2 RID: 162
	public class GameInterfaceLayer
	{
		// Token: 0x060014EA RID: 5354 RVA: 0x004A6ACB File Offset: 0x004A4CCB
		public GameInterfaceLayer(string name, InterfaceScaleType scaleType)
		{
			this.Name = name;
			this.ScaleType = scaleType;
			this.Active = true;
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x004A6AE8 File Offset: 0x004A4CE8
		public bool Draw()
		{
			if (!this.Active)
			{
				return true;
			}
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
			Main.spriteBatch.Begin(0, null, null, null, null, null, transformMatrix);
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

		// Token: 0x060014EC RID: 5356 RVA: 0x004A6B78 File Offset: 0x004A4D78
		protected virtual bool DrawSelf()
		{
			return true;
		}

		// Token: 0x040010DF RID: 4319
		public readonly string Name;

		// Token: 0x040010E0 RID: 4320
		public InterfaceScaleType ScaleType;

		// Token: 0x040010E1 RID: 4321
		public bool Active;
	}
}
