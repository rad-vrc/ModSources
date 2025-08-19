using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Represents a builder toggle button shown in the top left corner of the screen while the inventory is shown. These toggles typically control wiring-related visibility or other building-related quality of life features.<para />
	/// The <see cref="M:Terraria.ModLoader.BuilderToggle.Active" /> method determines if the BuilderToggle should be shown to the user and is usually reliant on player-specific values. The <see cref="P:Terraria.ModLoader.BuilderToggle.CurrentState" /> property represents the current state of the toggle. For vanilla toggles a value of 0 is off and a value of 1 is on, but modded toggles can have <see cref="P:Terraria.ModLoader.BuilderToggle.NumberOfStates" /> values.
	/// </summary>
	// Token: 0x0200014B RID: 331
	public abstract class BuilderToggle : ModTexturedType, ILocalizedModType, IModType
	{
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x004CF2FE File Offset: 0x004CD4FE
		// (set) Token: 0x06001B0B RID: 6923 RVA: 0x004CF305 File Offset: 0x004CD505
		public static BuilderToggle RulerLine { get; private set; } = new RulerLineBuilderToggle();

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x004CF30D File Offset: 0x004CD50D
		// (set) Token: 0x06001B0D RID: 6925 RVA: 0x004CF314 File Offset: 0x004CD514
		public static BuilderToggle RulerGrid { get; private set; } = new RulerGridBuilderToggle();

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x004CF31C File Offset: 0x004CD51C
		// (set) Token: 0x06001B0F RID: 6927 RVA: 0x004CF323 File Offset: 0x004CD523
		public static BuilderToggle AutoActuate { get; private set; } = new AutoActuateBuilderToggle();

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x004CF32B File Offset: 0x004CD52B
		// (set) Token: 0x06001B11 RID: 6929 RVA: 0x004CF332 File Offset: 0x004CD532
		public static BuilderToggle AutoPaint { get; private set; } = new AutoPaintBuilderToggle();

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x004CF33A File Offset: 0x004CD53A
		// (set) Token: 0x06001B13 RID: 6931 RVA: 0x004CF341 File Offset: 0x004CD541
		public static BuilderToggle RedWireVisibility { get; private set; } = new RedWireVisibilityBuilderToggle();

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x004CF349 File Offset: 0x004CD549
		// (set) Token: 0x06001B15 RID: 6933 RVA: 0x004CF350 File Offset: 0x004CD550
		public static BuilderToggle BlueWireVisibility { get; private set; } = new BlueWireVisibilityBuilderToggle();

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001B16 RID: 6934 RVA: 0x004CF358 File Offset: 0x004CD558
		// (set) Token: 0x06001B17 RID: 6935 RVA: 0x004CF35F File Offset: 0x004CD55F
		public static BuilderToggle GreenWireVisibility { get; private set; } = new GreenWireVisibilityBuilderToggle();

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x004CF367 File Offset: 0x004CD567
		// (set) Token: 0x06001B19 RID: 6937 RVA: 0x004CF36E File Offset: 0x004CD56E
		public static BuilderToggle YellowWireVisibility { get; private set; } = new YellowWireVisibilityBuilderToggle();

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x004CF376 File Offset: 0x004CD576
		// (set) Token: 0x06001B1B RID: 6939 RVA: 0x004CF37D File Offset: 0x004CD57D
		public static BuilderToggle HideAllWires { get; private set; } = new HideAllWiresBuilderToggle();

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001B1C RID: 6940 RVA: 0x004CF385 File Offset: 0x004CD585
		// (set) Token: 0x06001B1D RID: 6941 RVA: 0x004CF38C File Offset: 0x004CD58C
		public static BuilderToggle ActuatorsVisibility { get; private set; } = new ActuatorsVisibilityBuilderToggle();

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001B1E RID: 6942 RVA: 0x004CF394 File Offset: 0x004CD594
		// (set) Token: 0x06001B1F RID: 6943 RVA: 0x004CF39B File Offset: 0x004CD59B
		public static BuilderToggle BlockSwap { get; private set; } = new BlockSwapBuilderToggle();

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001B20 RID: 6944 RVA: 0x004CF3A3 File Offset: 0x004CD5A3
		// (set) Token: 0x06001B21 RID: 6945 RVA: 0x004CF3AA File Offset: 0x004CD5AA
		public static BuilderToggle TorchBiome { get; private set; } = new TorchBiomeBuilderToggle();

		/// <summary>
		/// The path to the texture vanilla info displays use when hovering over an info display.
		/// </summary>
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06001B22 RID: 6946 RVA: 0x004CF3B2 File Offset: 0x004CD5B2
		public static string VanillaHoverTexture
		{
			get
			{
				return "Terraria/Images/UI/InfoIcon_13";
			}
		}

		/// <summary>
		/// The outline texture drawn when the icon is hovered. By default a circular outline texture is used. Override this method and return <c>Texture + "_Hover"</c> or any other texture path to specify a custom outline texture for use with icons that are not circular.
		/// </summary>
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x004CF3B9 File Offset: 0x004CD5B9
		public virtual string HoverTexture
		{
			get
			{
				return BuilderToggle.VanillaHoverTexture;
			}
		}

		/// <summary>
		/// This is the internal ID of this builder toggle.<para />
		/// Also serves as the index for <see cref="F:Terraria.Player.builderAccStatus" />.
		/// </summary>
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06001B24 RID: 6948 RVA: 0x004CF3C0 File Offset: 0x004CD5C0
		// (set) Token: 0x06001B25 RID: 6949 RVA: 0x004CF3C8 File Offset: 0x004CD5C8
		public int Type { get; internal set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x004CF3D1 File Offset: 0x004CD5D1
		public virtual string LocalizationCategory
		{
			get
			{
				return "BuilderToggles";
			}
		}

		/// <summary>
		/// This dictates whether or not this builder toggle should be active (displayed).<para />
		/// This is usually determined by player-specific values, typically set in <see cref="M:Terraria.ModLoader.ModItem.UpdateInventory(Terraria.Player)" />.
		/// </summary>
		// Token: 0x06001B27 RID: 6951 RVA: 0x004CF3D8 File Offset: 0x004CD5D8
		public virtual bool Active()
		{
			return false;
		}

		/// <summary>
		/// This is the number of different functionalities your builder toggle will have.<br />
		/// For a toggle that has an On and Off state, you'd need 2 states!<para />
		/// </summary>
		/// <value>Default value is 2</value>
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x004CF3DB File Offset: 0x004CD5DB
		// (set) Token: 0x06001B29 RID: 6953 RVA: 0x004CF3E3 File Offset: 0x004CD5E3
		public virtual int NumberOfStates { get; internal set; } = 2;

		/// <summary>
		/// Modify this if you want your builder toggle have custom ordering.
		/// You can specify which BuilderToggle to sort before/after
		/// </summary>
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06001B2A RID: 6954 RVA: 0x004CF3EC File Offset: 0x004CD5EC
		// (set) Token: 0x06001B2B RID: 6955 RVA: 0x004CF3F4 File Offset: 0x004CD5F4
		public virtual BuilderToggle.Position OrderPosition { get; internal set; } = new BuilderToggle.Default();

		/// <summary>
		/// This is the current state of this builder toggle. Every time the toggle is clicked, it will change.<para />
		/// The default state is 0. The state will be saved and loaded for the player to be consistent.
		/// </summary>
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x004CF3FD File Offset: 0x004CD5FD
		// (set) Token: 0x06001B2D RID: 6957 RVA: 0x004CF410 File Offset: 0x004CD610
		public int CurrentState
		{
			get
			{
				return Main.LocalPlayer.builderAccStatus[this.Type];
			}
			set
			{
				Main.LocalPlayer.builderAccStatus[this.Type] = value;
			}
		}

		/// <summary>
		/// This is the overlay color that is drawn on top of the texture.
		/// </summary>
		/// <value>Default value is <see cref="P:Microsoft.Xna.Framework.Color.White" /></value>
		// Token: 0x06001B2E RID: 6958 RVA: 0x004CF424 File Offset: 0x004CD624
		[Obsolete("Use Draw instead", true)]
		public virtual Color DisplayColorTexture()
		{
			return Color.White;
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x004CF42B File Offset: 0x004CD62B
		[Obsolete]
		internal Color DisplayColorTexture_Obsolete()
		{
			return this.DisplayColorTexture();
		}

		/// <summary>
		/// This is the value that will show up when hovering on the toggle icon.
		/// You can specify different values per each available <see cref="P:Terraria.ModLoader.BuilderToggle.CurrentState" />
		/// </summary>
		// Token: 0x06001B30 RID: 6960
		public abstract string DisplayValue();

		/// <summary>
		/// This allows you to change basic drawing parameters or to override the vanilla drawing completely.<para />
		/// This is for the icon itself. See <see cref="M:Terraria.ModLoader.BuilderToggle.DrawHover(Microsoft.Xna.Framework.Graphics.SpriteBatch,Terraria.DataStructures.BuilderToggleDrawParams@)" /> if you want to modify icon hover drawing.<para />
		/// Return false to stop vanilla drawing code from running. Returns true by default.
		/// </summary>
		/// <param name="spriteBatch">The spritebatch to draw on</param>
		/// <param name="drawParams">The draw parameters for the builder toggle icon</param>
		/// <returns>Whether to run vanilla icon drawing code</returns>
		// Token: 0x06001B31 RID: 6961 RVA: 0x004CF433 File Offset: 0x004CD633
		public virtual bool Draw(SpriteBatch spriteBatch, ref BuilderToggleDrawParams drawParams)
		{
			return true;
		}

		/// <summary>
		/// This allows you to change basic drawing parameters or to override the vanilla drawing completely.<para />
		/// This is for the icon hover. See <see cref="M:Terraria.ModLoader.BuilderToggle.Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch,Terraria.DataStructures.BuilderToggleDrawParams@)" /> if you want to modify icon drawing.<para />
		/// Return false to stop vanilla drawing code from running. Returns true by default.
		/// </summary>
		/// <param name="spriteBatch">The spritebatch to draw on</param>
		/// <param name="drawParams">The draw parameters for the builder toggle hover icon</param>
		/// <returns>Whether to run vanilla icon hover drawing code</returns>
		// Token: 0x06001B32 RID: 6962 RVA: 0x004CF436 File Offset: 0x004CD636
		public virtual bool DrawHover(SpriteBatch spriteBatch, ref BuilderToggleDrawParams drawParams)
		{
			return true;
		}

		/// <summary>
		/// Called when the toggle is left clicked and before vanilla operation takes place.<para />
		/// Return false to stop vanilla left click code (switching between states and playing sound) from running.<br />
		/// Returns true by default.
		/// </summary>
		/// <param name="sound">The click sound that will be played. Return null to mute.</param>
		/// <returns>Whether to run vanilla click code</returns>
		// Token: 0x06001B33 RID: 6963 RVA: 0x004CF439 File Offset: 0x004CD639
		public virtual bool OnLeftClick(ref SoundStyle? sound)
		{
			return true;
		}

		/// <summary>
		/// Called when the toggle is right clicked.<br />
		/// Use this if you want to implement special right click feature (such as cycling through states backwards).
		/// </summary>
		// Token: 0x06001B34 RID: 6964 RVA: 0x004CF43C File Offset: 0x004CD63C
		public virtual void OnRightClick()
		{
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x004CF43E File Offset: 0x004CD63E
		public sealed override void SetupContent()
		{
			ModContent.Request<Texture2D>(this.Texture, 2);
			ModContent.Request<Texture2D>(this.HoverTexture, 2);
			this.SetStaticDefaults();
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x004CF460 File Offset: 0x004CD660
		protected override void Register()
		{
			ModTypeLookup<BuilderToggle>.Register(this);
			this.Type = BuilderToggleLoader.Add(this);
		}

		// Token: 0x020008B7 RID: 2231
		public abstract class Position
		{
		}

		// Token: 0x020008B8 RID: 2232
		public sealed class Default : BuilderToggle.Position
		{
		}

		// Token: 0x020008B9 RID: 2233
		public sealed class Before : BuilderToggle.Position
		{
			// Token: 0x170008CF RID: 2255
			// (get) Token: 0x06005245 RID: 21061 RVA: 0x00698A71 File Offset: 0x00696C71
			public BuilderToggle Toggle { get; }

			// Token: 0x06005246 RID: 21062 RVA: 0x00698A79 File Offset: 0x00696C79
			public Before(BuilderToggle toggle)
			{
				this.Toggle = toggle;
			}
		}

		// Token: 0x020008BA RID: 2234
		public sealed class After : BuilderToggle.Position
		{
			// Token: 0x170008D0 RID: 2256
			// (get) Token: 0x06005247 RID: 21063 RVA: 0x00698A88 File Offset: 0x00696C88
			public BuilderToggle Toggle { get; }

			// Token: 0x06005248 RID: 21064 RVA: 0x00698A90 File Offset: 0x00696C90
			public After(BuilderToggle toggle)
			{
				this.Toggle = toggle;
			}
		}
	}
}
