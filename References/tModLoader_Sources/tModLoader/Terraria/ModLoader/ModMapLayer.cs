using System;
using System.Collections.Generic;
using Terraria.Map;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class is used to facilitate easily drawing icons and other things over the map. Pylons and spawn/bed icons are examples of vanilla map layers. Use <see cref="M:Terraria.ModLoader.ModSystem.PreDrawMapIconOverlay(System.Collections.Generic.IReadOnlyList{Terraria.Map.IMapLayer},Terraria.Map.MapOverlayDrawContext)" /> to selectively hide vanilla layers if needed.
	/// </summary>
	// Token: 0x020001B5 RID: 437
	public abstract class ModMapLayer : ModType, IMapLayer
	{
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x004E590C File Offset: 0x004E3B0C
		// (set) Token: 0x060021A4 RID: 8612 RVA: 0x004E5914 File Offset: 0x004E3B14
		public bool Visible { get; set; } = true;

		/// <summary>
		/// Returns the map layer's default position in regard to vanilla's map layer ordering. Make use of e.g. <see cref="T:Terraria.ModLoader.ModMapLayer.Before" />/<see cref="T:Terraria.ModLoader.ModMapLayer.After" />, and provide a map layer. You can also use <see cref="P:Terraria.ModLoader.ModMapLayer.BeforeFirstVanillaLayer" /> or <see cref="P:Terraria.ModLoader.ModMapLayer.AfterLastVanillaLayer" /> to put your layer at the start/end of the vanilla layer order.<br /><br />
		/// <b>NOTE:</b> The position must specify a vanilla <see cref="T:Terraria.Map.IMapLayer" /> otherwise an exception will be thrown. Use <see cref="M:Terraria.ModLoader.ModMapLayer.GetModdedConstraints" /> to order modded layers.<br /><br />
		/// By default, this hook positions this map layer after all vanilla layers.
		/// </summary>
		// Token: 0x060021A5 RID: 8613 RVA: 0x004E591D File Offset: 0x004E3B1D
		public virtual ModMapLayer.Position GetDefaultPosition()
		{
			return ModMapLayer.AfterLastVanillaLayer;
		}

		/// <summary>
		/// Modded layers are placed between vanilla layers via <see cref="M:Terraria.ModLoader.ModMapLayer.GetDefaultPosition" /> and, by default, are sorted in load order.<br />
		/// This hook allows you to sort this map layer before/after other modded layers that were placed between the same two vanilla layers.<br />
		/// Example:
		/// <para>
		/// <c>yield return new After(ModContent.GetInstance&lt;ExampleMapLayer&gt;());</c>
		/// </para>
		/// By default, this hook returns <see langword="null" />, which indicates that this map layer has no modded ordering constraints.
		/// </summary>
		// Token: 0x060021A6 RID: 8614 RVA: 0x004E5924 File Offset: 0x004E3B24
		public virtual IEnumerable<ModMapLayer.Position> GetModdedConstraints()
		{
			return null;
		}

		/// <summary>
		/// This method is called when this MapLayer is to be drawn. Map layers are drawn after the map itself is drawn. Use <see cref="M:Terraria.Map.MapOverlayDrawContext.Draw(Microsoft.Xna.Framework.Graphics.Texture2D,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Color,Terraria.DataStructures.SpriteFrame,System.Single,System.Single,Terraria.UI.Alignment)" /> as described in ExampleMod and in vanilla examples for full compatibility and simplicity of code.
		/// <para /> <paramref name="context" />: Contains the scaling and positional data of the map being drawn. You should use the MapOverlayDrawContext.Draw method for all drawing.
		/// <para /> <paramref name="text" />: The mouse hover text. Assign a value typically if the user is hovering over something you draw.
		/// <para /> Note that MapLayers are drawn on the fullscreen map, overlay map, and minimap. Use <see cref="F:Terraria.Main.mapFullscreen" /> and <see cref="F:Terraria.Main.mapStyle" /> to limit drawing to specific map modes if appropriate.
		/// </summary>
		/// <param name="context">Contains the scaling and positional data of the map being drawn. You should use the MapOverlayDrawContext.Draw method for all drawing</param>
		/// <param name="text">The mouse hover text. Assign a value typically if the user is hovering over something you draw</param>
		// Token: 0x060021A7 RID: 8615
		public abstract void Draw(ref MapOverlayDrawContext context, ref string text);

		// Token: 0x060021A8 RID: 8616 RVA: 0x004E5927 File Offset: 0x004E3B27
		protected sealed override void Register()
		{
			ModTypeLookup<ModMapLayer>.Register(this);
			MapLayerLoader.Add(this);
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x004E5935 File Offset: 0x004E3B35
		public static ModMapLayer.Position BeforeFirstVanillaLayer
		{
			get
			{
				return new ModMapLayer.Before(MapLayerLoader.FirstVanillaLayer);
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x004E5941 File Offset: 0x004E3B41
		public static ModMapLayer.Position AfterLastVanillaLayer
		{
			get
			{
				return new ModMapLayer.After(MapLayerLoader.LastVanillaLayer);
			}
		}

		// Token: 0x0200092C RID: 2348
		public abstract class Position
		{
		}

		// Token: 0x0200092D RID: 2349
		public sealed class Before : ModMapLayer.Position
		{
			// Token: 0x170008DF RID: 2271
			// (get) Token: 0x060053CF RID: 21455 RVA: 0x0069A486 File Offset: 0x00698686
			public IMapLayer Layer { get; }

			// Token: 0x060053D0 RID: 21456 RVA: 0x0069A48E File Offset: 0x0069868E
			public Before(IMapLayer layer)
			{
				this.Layer = layer;
			}
		}

		// Token: 0x0200092E RID: 2350
		public sealed class After : ModMapLayer.Position
		{
			// Token: 0x170008E0 RID: 2272
			// (get) Token: 0x060053D1 RID: 21457 RVA: 0x0069A49D File Offset: 0x0069869D
			public IMapLayer Layer { get; }

			// Token: 0x060053D2 RID: 21458 RVA: 0x0069A4A5 File Offset: 0x006986A5
			public After(IMapLayer layer)
			{
				this.Layer = layer;
			}
		}
	}
}
