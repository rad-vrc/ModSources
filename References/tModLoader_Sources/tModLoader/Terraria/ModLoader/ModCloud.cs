using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class allows you to customize the behavior of a custom cloud.
	/// <para /> Modded clouds can be autoloaded automatically (see <see cref="P:Terraria.ModLoader.Mod.CloudAutoloadingEnabled" />) or manually registered (<see cref="M:Terraria.ModLoader.CloudLoader.AddCloudFromTexture(Terraria.ModLoader.Mod,System.String,System.Single,System.Boolean)" />), but autoloaded clouds default to being normal clouds with the default spawn chance. Make a ModCloud class if custom behavior is needed or use <see cref="M:Terraria.ModLoader.CloudLoader.AddCloudFromTexture(Terraria.ModLoader.Mod,System.String,System.Single,System.Boolean)" /> if customizing cloud category and spawn chance is all that is needed.
	/// <para /> This class is not instanced, all clouds of the same type will share the same instance.
	/// </summary>
	// Token: 0x020001A5 RID: 421
	[Autoload(true, Side = ModSide.Client)]
	public abstract class ModCloud : ModTexturedType
	{
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06002048 RID: 8264 RVA: 0x004E2D90 File Offset: 0x004E0F90
		// (set) Token: 0x06002049 RID: 8265 RVA: 0x004E2D98 File Offset: 0x004E0F98
		public int Type { get; internal set; }

		/// <summary>
		/// If true, this cloud will belong to the "Rare clouds" pool instead of the "Normal clouds" (see <see href="https://terraria.wiki.gg/wiki/Ambient_entities#List_of_clouds">the Terraria wiki for more information</see>). Rare clouds typically can only spawn after certain world conditions have been met. For example <see cref="F:Terraria.ID.CloudID.Rare_Skeletron" /> can only spawn if <see cref="F:Terraria.NPC.downedBoss3" /> is true. Rare clouds can be used by mods to highlight achievements such as defeating a boss.
		/// <para /> Defaults to false.
		/// </summary>
		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600204A RID: 8266 RVA: 0x004E2DA1 File Offset: 0x004E0FA1
		public virtual bool RareCloud
		{
			get
			{
				return this.rareCloud;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600204B RID: 8267 RVA: 0x004E2DA9 File Offset: 0x004E0FA9
		public override string Name
		{
			get
			{
				return this.nameOverride ?? base.Name;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x004E2DBB File Offset: 0x004E0FBB
		public override string Texture
		{
			get
			{
				return this.textureOverride ?? base.Texture;
			}
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x004E2DCD File Offset: 0x004E0FCD
		protected override void Register()
		{
			ModTypeLookup<ModCloud>.Register(this);
			CloudLoader.RegisterModCloud(this);
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x004E2DDB File Offset: 0x004E0FDB
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// The chance that this cloud can spawn. Return 1f to spawn just as often compared as vanilla clouds.
		/// <para /> A cloud can be either a "Normal cloud" or a "Rare cloud" if <see cref="P:Terraria.ModLoader.ModCloud.RareCloud" /> is true. The spawn chance is the chance to spawn within each cloud category, so there is no need to return a very small chance value for rare clouds, the logic already takes that into account. If <see cref="P:Terraria.ModLoader.ModCloud.RareCloud" /> is true, the spawn chance already takes into account secret seed adjustments so there is no need to implement that logic in this method either.
		/// <para /> See <see href="https://terraria.wiki.gg/wiki/Ambient_entities#List_of_clouds">the Terraria wiki for more information</see>.
		/// <para /> If checking for modded biomes using <c>Main.LocalPlayer</c>, be sure to check <c>Main.gameMenu</c> as well to prevent exceptions.
		/// <para /> Defaults to 1f.
		/// </summary>
		// Token: 0x0600204F RID: 8271 RVA: 0x004E2DE3 File Offset: 0x004E0FE3
		public virtual float SpawnChance()
		{
			return this.spawnChance;
		}

		/// <summary>
		/// Gets called when the Cloud spawns.
		/// </summary>
		// Token: 0x06002050 RID: 8272 RVA: 0x004E2DEB File Offset: 0x004E0FEB
		public virtual void OnSpawn(Cloud cloud)
		{
		}

		/// <summary>
		/// Return <c>true</c> to draw using vanilla drawing logic. Return <c>false</c> to prevent vanilla drawing logic and use this hook to draw the cloud manually.
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="cloud"></param>
		/// <param name="cloudIndex">The index of the cloud within Main.cloud. Note that clouds are shifted around as clouds spawn and despawn.</param>
		/// <param name="drawData">The calculated draw parameters</param>
		/// <returns></returns>
		// Token: 0x06002051 RID: 8273 RVA: 0x004E2DED File Offset: 0x004E0FED
		public virtual bool Draw(SpriteBatch spriteBatch, Cloud cloud, int cloudIndex, ref DrawData drawData)
		{
			return true;
		}

		// Token: 0x040016AE RID: 5806
		internal string nameOverride;

		// Token: 0x040016AF RID: 5807
		internal string textureOverride;

		// Token: 0x040016B0 RID: 5808
		internal float spawnChance = 1f;

		// Token: 0x040016B1 RID: 5809
		internal bool rareCloud;
	}
}
