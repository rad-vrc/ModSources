using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Capture;

namespace Terraria.ModLoader
{
	/// <summary>
	/// ModSceneEffect is an abstract class that your classes can derive from. It serves as a container for handling exclusive SceneEffect content such as backgrounds, music, and water styling.
	/// </summary>
	// Token: 0x020001C9 RID: 457
	public abstract class ModSceneEffect : ModType
	{
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x004E9111 File Offset: 0x004E7311
		// (set) Token: 0x060023D1 RID: 9169 RVA: 0x004E9119 File Offset: 0x004E7319
		public int Type { get; internal set; }

		/// <summary>
		/// The ModWaterStyle that will apply to water.
		/// </summary>
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060023D2 RID: 9170 RVA: 0x004E9122 File Offset: 0x004E7322
		public virtual ModWaterStyle WaterStyle
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// The ModSurfaceBackgroundStyle that will draw its background when the player is on the surface.
		/// </summary>
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060023D3 RID: 9171 RVA: 0x004E9125 File Offset: 0x004E7325
		public virtual ModSurfaceBackgroundStyle SurfaceBackgroundStyle
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// The ModUndergroundBackgroundStyle that will draw its background when the player is underground.
		/// </summary>
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060023D4 RID: 9172 RVA: 0x004E9128 File Offset: 0x004E7328
		public virtual ModUndergroundBackgroundStyle UndergroundBackgroundStyle
		{
			get
			{
				return null;
			}
		}

		/// <SharedSummary>
		/// The music that will play. -1 for letting other music play, 0 for no music, &gt;0 for the given music to play (using <see cref="M:Terraria.ModLoader.MusicLoader.GetMusicSlot(Terraria.ModLoader.Mod,System.String)" /> or <see cref="T:Terraria.ID.MusicID" />).
		/// <br /><br /> See <see cref="F:Terraria.Main.swapMusic" /> for information about playing alternate music when the Otherworld soundtrack is enabled.
		/// </SharedSummary>
		/// <summary>
		/// <inheritdoc cref="P:Terraria.ModLoader.ModSceneEffect.Music" path="/SharedSummary/node()" />
		/// <para /> Defaults to -1.
		/// </summary>
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060023D5 RID: 9173 RVA: 0x004E912B File Offset: 0x004E732B
		public virtual int Music
		{
			get
			{
				return -1;
			}
		}

		/// <summary>
		/// The path to the texture that will display behind the map. Should be 115x65.
		/// </summary>
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x004E912E File Offset: 0x004E732E
		public virtual string MapBackground
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// If true, the map background (<see cref="P:Terraria.ModLoader.ModSceneEffect.MapBackground" />) will be forced to be drawn at full brightness (White). For example, the background map of the Mushroom biome draws at full brightness even when above ground.
		/// <para /> By default, this returns false, indicating that the sky color should be used if above surface level and full brightness otherwise. 
		/// <para /> Use <see cref="M:Terraria.ModLoader.ModSceneEffect.MapBackgroundColor(Microsoft.Xna.Framework.Color@)" /> instead to fully customize the map background draw color.
		/// </summary>
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060023D7 RID: 9175 RVA: 0x004E9131 File Offset: 0x004E7331
		public virtual bool MapBackgroundFullbright
		{
			get
			{
				return false;
			}
		}

		/// <SharedSummary>
		/// The <see cref="T:Terraria.ModLoader.SceneEffectPriority" /> of this SceneEffect layer. Determines the relative position compared to a vanilla SceneEffect.
		/// Analogously, if SceneEffect were competing in a wrestling match, this would be the 'Weight Class' that this SceneEffect is competing in.
		/// </SharedSummary>
		/// <summary>
		/// <inheritdoc cref="P:Terraria.ModLoader.ModSceneEffect.Priority" path="/SharedSummary/node()" />
		/// <para /> Defaults to <see cref="F:Terraria.ModLoader.SceneEffectPriority.None" />.
		/// </summary>
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x004E9134 File Offset: 0x004E7334
		public virtual SceneEffectPriority Priority
		{
			get
			{
				return SceneEffectPriority.None;
			}
		}

		/// <summary>
		/// Used to apply secondary color shading for the capture camera. For example, darkening the background with the GlowingMushroom style.
		/// </summary>
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060023D9 RID: 9177 RVA: 0x004E9137 File Offset: 0x004E7337
		public virtual CaptureBiome.TileColorStyle TileColorStyle
		{
			get
			{
				return CaptureBiome.TileColorStyle.Normal;
			}
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x004E913A File Offset: 0x004E733A
		protected override void Register()
		{
			this.Type = LoaderManager.Get<SceneEffectLoader>().Register(this);
		}

		/// <summary>
		/// Forcefully registers the provided ModSceneEffect to LoaderManager.
		/// ModBiome and direct implementations call this.
		/// Does NOT cache the return type.
		/// </summary>
		// Token: 0x060023DB RID: 9179 RVA: 0x004E914D File Offset: 0x004E734D
		internal void RegisterSceneEffect(ModSceneEffect modSceneEffect)
		{
			LoaderManager.Get<SceneEffectLoader>().Register(modSceneEffect);
		}

		/// <summary>
		/// Is invoked when two or more modded SceneEffect layers are active within the same <see cref="P:Terraria.ModLoader.ModSceneEffect.Priority" /> group to attempt to determine which one should take precedence, if it matters.
		/// It's uncommon to have the need to assign a weight - you'd have to specifically believe that you don't need higher SceneEffectPriority, but do need to be the active SceneEffect within the priority you designated.
		/// Analogously, if SceneEffect were competing in a wrestling match, this would be how likely the SceneEffect should win within its weight class.
		/// Is intentionally bounded at a max of 100% (1) to reduce complexity. Defaults to 50% (0.5).
		/// Typical calculations may include: 1) how many tiles are present as a percentage of target amount; 2) how far away you are from the cause of the SceneEffect
		/// </summary>
		// Token: 0x060023DC RID: 9180 RVA: 0x004E915B File Offset: 0x004E735B
		public virtual float GetWeight(Player player)
		{
			return 0.5f;
		}

		/// <summary>
		/// Combines Priority and Weight to determine what SceneEffect should be active.
		/// Priority is used to do primary sorting with respect to vanilla SceneEffect.
		/// Weight will be used if multiple SceneEffect have the same SceneEffectPriority so as to attempt to distinguish them based on their needs.
		/// </summary>
		// Token: 0x060023DD RID: 9181 RVA: 0x004E9162 File Offset: 0x004E7362
		internal float GetCorrWeight(Player player)
		{
			return Math.Max(Math.Min(this.GetWeight(player), 1f), 0f) + (float)this.Priority;
		}

		/// <summary>
		/// Return true to make the SceneEffect apply its effects (as long as its priority and weight allow that).
		/// </summary>
		// Token: 0x060023DE RID: 9182 RVA: 0x004E9187 File Offset: 0x004E7387
		public virtual bool IsSceneEffectActive(Player player)
		{
			return false;
		}

		/// <summary>
		/// Allows you to create special visual effects in the area around the player. For example, the Blood Moon's red filter on the screen or the Slime Rain's falling slime in the background. You must create classes that override <see cref="T:Terraria.Graphics.Shaders.ScreenShaderData" /> or <see cref="T:Terraria.Graphics.Effects.CustomSky" />, add them in a Load hook, then call <see cref="M:Terraria.Player.ManageSpecialBiomeVisuals(System.String,System.Boolean,Microsoft.Xna.Framework.Vector2)" />. See the ExampleMod if you do not have access to the source code.
		/// <br /> This runs even if <see cref="M:Terraria.ModLoader.ModSceneEffect.IsSceneEffectActive(Terraria.Player)" /> returns false. Check <paramref name="isActive" /> for the active status.
		/// </summary>
		// Token: 0x060023DF RID: 9183 RVA: 0x004E918A File Offset: 0x004E738A
		public virtual void SpecialVisuals(Player player, bool isActive)
		{
		}

		/// <summary>
		/// Uses to customize the draw color of the map background (<see cref="P:Terraria.ModLoader.ModSceneEffect.MapBackground" />) drawn on the fullscreen map. <see cref="P:Terraria.ModLoader.ModSceneEffect.MapBackgroundFullbright" /> can be used for typical effects, but this method can be used if further customization is needed.
		/// </summary>
		/// <param name="color">White or Main.ColorOfTheSkies depending on if above ground and MapBackgroundUsesSkyColor value.</param>
		// Token: 0x060023E0 RID: 9184 RVA: 0x004E918C File Offset: 0x004E738C
		public virtual void MapBackgroundColor(ref Color color)
		{
		}
	}
}
