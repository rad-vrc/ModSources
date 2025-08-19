using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent.UI.ResourceSets;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class serves as a place for you to define your own logic for drawing the player's life and mana resources.<br />
	/// For modifying parts of the vanilla display sets, use <see cref="T:Terraria.ModLoader.ModResourceOverlay" />.
	/// </summary>
	// Token: 0x020001C7 RID: 455
	[Autoload(true, Side = ModSide.Client)]
	public abstract class ModResourceDisplaySet : ModType, IPlayerResourcesDisplaySet, IConfigKeyHolder, ILocalizedModType, IModType
	{
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x004E8F70 File Offset: 0x004E7170
		// (set) Token: 0x060023B5 RID: 9141 RVA: 0x004E8F78 File Offset: 0x004E7178
		public int Type { get; internal set; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x004E8F81 File Offset: 0x004E7181
		public bool Selected
		{
			get
			{
				return Main.ResourceSetsManager.ActiveSet == this;
			}
		}

		/// <summary>
		/// Gets the name for this resource display set based on its DisplayName and the current culture
		/// </summary>
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060023B7 RID: 9143 RVA: 0x004E8F90 File Offset: 0x004E7190
		public string DisplayedName
		{
			get
			{
				return this.DisplayName.Value;
			}
		}

		/// <summary>
		/// Included only for completion's sake.  Returns DisplayName.Key
		/// </summary>
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x004E8F9D File Offset: 0x004E719D
		public string NameKey
		{
			get
			{
				return this.DisplayName.Key;
			}
		}

		/// <summary>
		/// The name used to get this resource display set.  Returns <see cref="P:Terraria.ModLoader.ModType.FullName" />
		/// </summary>
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060023B9 RID: 9145 RVA: 0x004E8FAA File Offset: 0x004E71AA
		public string ConfigKey
		{
			get
			{
				return base.FullName;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x004E8FB2 File Offset: 0x004E71B2
		public virtual string LocalizationCategory
		{
			get
			{
				return "ResourceDisplaySets";
			}
		}

		/// <summary>
		/// The translations for the display name of this item.
		/// </summary>
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x004E8FB9 File Offset: 0x004E71B9
		public virtual LocalizedText DisplayName
		{
			get
			{
				return this.GetLocalization("DisplayName", new Func<string>(base.PrettyPrintName));
			}
		}

		/// <summary>
		/// The current snapshot of the life and mana stats for Main.LocalPlayer
		/// </summary>
		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x004E8FD2 File Offset: 0x004E71D2
		public static PlayerStatsSnapshot PlayerStats
		{
			get
			{
				return new PlayerStatsSnapshot(Main.LocalPlayer);
			}
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x004E8FDE File Offset: 0x004E71DE
		protected sealed override void Register()
		{
			ModTypeLookup<ModResourceDisplaySet>.Register(this);
			this.Type = ResourceDisplaySetLoader.Add(this);
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x004E8FF2 File Offset: 0x004E71F2
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x004E8FFC File Offset: 0x004E71FC
		public void Draw()
		{
			PlayerStatsSnapshot stats = ModResourceDisplaySet.PlayerStats;
			this.PreDrawResources(stats);
			Color color;
			color..ctor((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			bool drawText;
			if (ResourceOverlayLoader.PreDrawResourceDisplay(stats, this, true, ref color, out drawText))
			{
				this.DrawLife(Main.spriteBatch);
			}
			ResourceOverlayLoader.PostDrawResourceDisplay(stats, this, true, color, drawText);
			color..ctor((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			if (ResourceOverlayLoader.PreDrawResourceDisplay(stats, this, false, ref color, out drawText))
			{
				this.DrawMana(Main.spriteBatch);
			}
			ResourceOverlayLoader.PostDrawResourceDisplay(stats, this, false, color, drawText);
		}

		/// <summary>
		/// Allows you to initialize fields, textures, etc. before drawing occurs
		/// </summary>
		/// <param name="snapshot">A copy of <see cref="P:Terraria.ModLoader.ModResourceDisplaySet.PlayerStats" /></param>
		// Token: 0x060023C0 RID: 9152 RVA: 0x004E9092 File Offset: 0x004E7292
		public virtual void PreDrawResources(PlayerStatsSnapshot snapshot)
		{
		}

		/// <summary>
		/// Draw the life resources for your display set here
		/// </summary>
		/// <param name="spriteBatch"></param>
		// Token: 0x060023C1 RID: 9153 RVA: 0x004E9094 File Offset: 0x004E7294
		public virtual void DrawLife(SpriteBatch spriteBatch)
		{
		}

		/// <summary>
		/// Draw the mana resources for your display set here
		/// </summary>
		/// <param name="spriteBatch"></param>
		// Token: 0x060023C2 RID: 9154 RVA: 0x004E9096 File Offset: 0x004E7296
		public virtual void DrawMana(SpriteBatch spriteBatch)
		{
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x004E9098 File Offset: 0x004E7298
		public void TryToHover()
		{
			bool hoveringLife;
			if (this.PreHover(out hoveringLife))
			{
				if (hoveringLife)
				{
					CommonResourceBarMethods.DrawLifeMouseOver();
					return;
				}
				CommonResourceBarMethods.DrawManaMouseOver();
			}
		}

		/// <summary>
		/// Allows you to specify if the vanilla life/mana hover text should display
		/// </summary>
		/// <param name="hoveringLife">Whether the hover text should be for life (<see langword="true" />) or mana (<see langword="false" />)</param>
		// Token: 0x060023C4 RID: 9156 RVA: 0x004E90BD File Offset: 0x004E72BD
		public virtual bool PreHover(out bool hoveringLife)
		{
			hoveringLife = false;
			return false;
		}
	}
}
