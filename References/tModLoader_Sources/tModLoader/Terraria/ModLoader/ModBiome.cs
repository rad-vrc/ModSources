using System;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class represents a biome added by a mod. It exists to centralize various biome related hooks, handling a lot of biome boilerplate, such as netcode.
	/// <br />To check if a player is in the biome, use <see cref="M:Terraria.Player.InModBiome``1" />.
	/// <br />Unlike <see cref="T:Terraria.ModLoader.ModSceneEffect" />, this defaults <see cref="P:Terraria.ModLoader.ModBiome.Music" /> to 0 and <see cref="P:Terraria.ModLoader.ModBiome.Priority" /> to <see cref="F:Terraria.ModLoader.SceneEffectPriority.BiomeLow" />.
	/// </summary>
	// Token: 0x0200019F RID: 415
	public abstract class ModBiome : ModSceneEffect, IShoppingBiome, ILocalizedModType, IModType
	{
		/// <summary>
		/// <inheritdoc cref="P:Terraria.ModLoader.ModSceneEffect.Priority" path="/SharedSummary/node()" />
		/// <para /> Defaults to <see cref="F:Terraria.ModLoader.SceneEffectPriority.BiomeLow" />.
		/// </summary>
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x004E28DB File Offset: 0x004E0ADB
		public override SceneEffectPriority Priority
		{
			get
			{
				return SceneEffectPriority.BiomeLow;
			}
		}

		/// <summary>
		/// <inheritdoc cref="P:Terraria.ModLoader.ModSceneEffect.Music" path="/SharedSummary/node()" />
		/// <para /> Defaults to 0. If custom music is not implemented for this biome, set this to -1.
		/// </summary>
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001FEA RID: 8170 RVA: 0x004E28DE File Offset: 0x004E0ADE
		public override int Music
		{
			get
			{
				return 0;
			}
		}

		/// <summary>
		/// The torch item type that will be placed when under the effect of biome torches
		/// </summary>
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x004E28E1 File Offset: 0x004E0AE1
		public virtual int BiomeTorchItemType
		{
			get
			{
				return -1;
			}
		}

		/// <summary>
		/// The campfire item type that will be placed when under the effect of biome torches
		/// </summary>
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001FEC RID: 8172 RVA: 0x004E28E4 File Offset: 0x004E0AE4
		public virtual int BiomeCampfireItemType
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x004E28E7 File Offset: 0x004E0AE7
		internal int ZeroIndexType
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001FEE RID: 8174 RVA: 0x004E28EF File Offset: 0x004E0AEF
		public virtual string LocalizationCategory
		{
			get
			{
				return "Biomes";
			}
		}

		/// <summary>
		/// The display name for this biome in the bestiary.
		/// </summary>
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x004E28F6 File Offset: 0x004E0AF6
		public virtual LocalizedText DisplayName
		{
			get
			{
				return this.GetLocalization("DisplayName", new Func<string>(base.PrettyPrintName));
			}
		}

		/// <summary>
		/// The path to the 30x30 texture that will appear for this biome in the bestiary. Defaults to adding "_Icon" onto the usual namespace+classname derived texture path.
		/// <br /> Vanilla icons use a drop shadow at 40 percent opacity and the texture will be offset 1 pixel left and up from centered in the bestiary filter grid.
		/// </summary>
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x004E290F File Offset: 0x004E0B0F
		public virtual string BestiaryIcon
		{
			get
			{
				return (base.GetType().Namespace + "." + this.Name + "_Icon").Replace('.', '/');
			}
		}

		/// <summary>
		/// The path to the background texture that will appear for this biome behind NPC's in the bestiary. Defaults to adding "_Background" onto the usual namespace+classname derived texture path.
		/// </summary>
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x004E293A File Offset: 0x004E0B3A
		public virtual string BackgroundPath
		{
			get
			{
				return (base.GetType().Namespace + "." + this.Name + "_Background").Replace('.', '/');
			}
		}

		/// <summary>
		/// The color of the bestiary background.
		/// </summary>
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x004E2968 File Offset: 0x004E0B68
		public virtual Color? BackgroundColor
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x004E297E File Offset: 0x004E0B7E
		// (set) Token: 0x06001FF4 RID: 8180 RVA: 0x004E2986 File Offset: 0x004E0B86
		public ModBiomeBestiaryInfoElement ModBiomeBestiaryInfoElement { get; internal set; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x004E298F File Offset: 0x004E0B8F
		string IShoppingBiome.NameKey
		{
			get
			{
				return this.GetLocalizationKey("TownNPCDialogueName");
			}
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x004E299C File Offset: 0x004E0B9C
		protected sealed override void Register()
		{
			base.Type = LoaderManager.Get<BiomeLoader>().Register(this);
			base.RegisterSceneEffect(this);
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x004E29B8 File Offset: 0x004E0BB8
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
			this.ModBiomeBestiaryInfoElement = new ModBiomeBestiaryInfoElement(base.Mod, this.DisplayName.Key, this.BestiaryIcon, this.BackgroundPath, this.BackgroundColor);
			Language.GetOrRegister(((IShoppingBiome)this).NameKey, () => "the " + Regex.Replace(this.Name, "([A-Z])", " $1").Trim());
		}

		/// <summary>
		/// IsSceneEffectActive is auto-forwarded to read the result of IsBiomeActive.
		/// Do not need to implement when creating your ModBiome.
		/// </summary>
		// Token: 0x06001FF8 RID: 8184 RVA: 0x004E2A11 File Offset: 0x004E0C11
		public sealed override bool IsSceneEffectActive(Player player)
		{
			return player.modBiomeFlags[this.ZeroIndexType];
		}

		/// <summary>
		/// This is where you can set values for DisplayName.
		/// </summary>
		// Token: 0x06001FF9 RID: 8185 RVA: 0x004E2A24 File Offset: 0x004E0C24
		public override void SetStaticDefaults()
		{
		}

		/// <summary>
		/// Return true if the player is in the biome.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06001FFA RID: 8186 RVA: 0x004E2A26 File Offset: 0x004E0C26
		public virtual bool IsBiomeActive(Player player)
		{
			return false;
		}

		/// <summary>
		/// Override this hook to make things happen when the player enters the biome.
		/// </summary>
		// Token: 0x06001FFB RID: 8187 RVA: 0x004E2A29 File Offset: 0x004E0C29
		public virtual void OnEnter(Player player)
		{
		}

		/// <summary>
		/// Override this hook to make things happen when the player is in the biome.
		/// </summary>
		// Token: 0x06001FFC RID: 8188 RVA: 0x004E2A2B File Offset: 0x004E0C2B
		public virtual void OnInBiome(Player player)
		{
		}

		/// <summary>
		/// Override this hook to make things happen when the player leaves the biome.
		/// </summary>
		// Token: 0x06001FFD RID: 8189 RVA: 0x004E2A2D File Offset: 0x004E0C2D
		public virtual void OnLeave(Player player)
		{
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x004E2A2F File Offset: 0x004E0C2F
		bool IShoppingBiome.IsInBiome(Player player)
		{
			return this.IsSceneEffectActive(player);
		}
	}
}
