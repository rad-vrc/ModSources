using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.UI;

namespace Terraria.ModLoader
{
	/// <summary>
	/// A class that is used to customize aesthetic features of the main menu, such as the logo, background and music.
	/// </summary>
	// Token: 0x020001B6 RID: 438
	public abstract class ModMenu : ModType
	{
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x004E595C File Offset: 0x004E3B5C
		public UserInterface UserInterface { get; } = new UserInterface();

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x004E5964 File Offset: 0x004E3B64
		// (set) Token: 0x060021AE RID: 8622 RVA: 0x004E596C File Offset: 0x004E3B6C
		public bool IsNew { get; internal set; }

		// Token: 0x060021AF RID: 8623 RVA: 0x004E5975 File Offset: 0x004E3B75
		protected sealed override void Register()
		{
			MenuLoader.Add(this);
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x004E597D File Offset: 0x004E3B7D
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// The logo texture shown when this ModMenu is active. If not overridden, it will use the tModLoader logo.
		/// </summary>
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x004E5985 File Offset: 0x004E3B85
		public virtual Asset<Texture2D> Logo
		{
			get
			{
				Asset<Texture2D> result;
				if ((result = ModMenu.modLoaderLogo) == null)
				{
					result = (ModMenu.modLoaderLogo = ModLoader.ManifestAssets.Request<Texture2D>("Terraria.ModLoader.Logo"));
				}
				return result;
			}
		}

		/// <summary>
		/// The sun texture shown when this ModMenu is active. If not overridden, it will use the vanilla sun.
		/// </summary>
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x004E59A5 File Offset: 0x004E3BA5
		public virtual Asset<Texture2D> SunTexture
		{
			get
			{
				return TextureAssets.Sun;
			}
		}

		/// <summary>
		/// The moon texture shown when this ModMenu is active. If not overridden, it will use the vanilla moon.
		/// </summary>
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060021B3 RID: 8627 RVA: 0x004E59AC File Offset: 0x004E3BAC
		public virtual Asset<Texture2D> MoonTexture
		{
			get
			{
				return TextureAssets.Moon[Utils.Clamp<int>(Main.moonType, 0, 8)];
			}
		}

		/// <summary>
		/// The music that will be played while this ModMenu is active. If not overridden, it will use the vanilla music.
		/// </summary>
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x004E59C0 File Offset: 0x004E3BC0
		public virtual int Music
		{
			get
			{
				return 50;
			}
		}

		/// <summary>
		/// The background style that will be used when this ModMenu is active. If not overridden, it will use the vanilla background.
		/// </summary>
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x004E59C4 File Offset: 0x004E3BC4
		public virtual ModSurfaceBackgroundStyle MenuBackgroundStyle
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// Controls whether this ModMenu will be available to switch to. Useful if you want this menu to only be available at specific times.
		/// </summary>
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060021B6 RID: 8630 RVA: 0x004E59C7 File Offset: 0x004E3BC7
		public virtual bool IsAvailable
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Controls the name that shows up at the base of the screen when this ModMenu is active. If not overridden, it will use this mod's display name.
		/// </summary>
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x004E59CA File Offset: 0x004E3BCA
		public virtual string DisplayName
		{
			get
			{
				return base.Mod.DisplayName;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x004E59D7 File Offset: 0x004E3BD7
		public bool IsSelected
		{
			get
			{
				return MenuLoader.CurrentMenu == this;
			}
		}

		/// <summary>
		/// Called when this ModMenu is selected. Set the state of the UserInterface to a given UIState to make that UIState appear on the main menu.
		/// </summary>
		// Token: 0x060021B9 RID: 8633 RVA: 0x004E59E1 File Offset: 0x004E3BE1
		public virtual void OnSelected()
		{
		}

		/// <summary>
		/// Called when this ModMenu is deselected.
		/// </summary>
		// Token: 0x060021BA RID: 8634 RVA: 0x004E59E3 File Offset: 0x004E3BE3
		public virtual void OnDeselected()
		{
		}

		/// <summary>
		/// Called when this ModMenu's logic is updated.
		/// </summary>
		// Token: 0x060021BB RID: 8635 RVA: 0x004E59E5 File Offset: 0x004E3BE5
		public virtual void Update(bool isOnTitleScreen)
		{
		}

		/// <summary>
		/// Called just before the logo is drawn, and allows you to modify some of the parameters of the logo draw code.
		/// </summary>
		// Token: 0x060021BC RID: 8636 RVA: 0x004E59E7 File Offset: 0x004E3BE7
		public virtual bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
		{
			return true;
		}

		/// <summary>
		/// Called just after the logo is drawn, and gives the values of some of the parameters of the logo draw code.
		/// </summary>
		// Token: 0x060021BD RID: 8637 RVA: 0x004E59EA File Offset: 0x004E3BEA
		public virtual void PostDrawLogo(SpriteBatch spriteBatch, Vector2 logoDrawCenter, float logoRotation, float logoScale, Color drawColor)
		{
		}

		// Token: 0x040016E8 RID: 5864
		internal static Asset<Texture2D> modLoaderLogo;
	}
}
