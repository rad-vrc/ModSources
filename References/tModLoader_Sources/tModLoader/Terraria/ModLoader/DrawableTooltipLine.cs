using System;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using Terraria.GameContent;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class serves as a way to store information about a line that will be drawn of tooltip for an item. You will create and manipulate objects of this class if you use the draw hooks for tooltips in ModItem and GlobalItem. For examples, see ExampleSword
	/// </summary>
	// Token: 0x02000158 RID: 344
	public sealed class DrawableTooltipLine : TooltipLine
	{
		/// <summary>
		/// The X position where the tooltip would be drawn that is not adjusted by mods.
		/// </summary>
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x004D11CC File Offset: 0x004CF3CC
		// (set) Token: 0x06001BCB RID: 7115 RVA: 0x004D11D4 File Offset: 0x004CF3D4
		public int OriginalX
		{
			get
			{
				return this._originalX;
			}
			internal set
			{
				this._originalX = value;
				this.X = value;
			}
		}

		/// <summary>
		/// The Y position where the tooltip would be drawn that is not adjusted by mods.
		/// </summary>
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x004D11F1 File Offset: 0x004CF3F1
		// (set) Token: 0x06001BCD RID: 7117 RVA: 0x004D11FC File Offset: 0x004CF3FC
		public int OriginalY
		{
			get
			{
				return this._originalY;
			}
			internal set
			{
				this._originalY = value;
				this.Y = value;
			}
		}

		/// <summary>
		/// The color the tooltip would be drawn in
		/// </summary>
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x004D1219 File Offset: 0x004CF419
		// (set) Token: 0x06001BCF RID: 7119 RVA: 0x004D1221 File Offset: 0x004CF421
		public Color Color { get; internal set; }

		/// <summary>
		/// If the tooltip line's color was overridden this will hold that color, it will be null otherwise
		/// </summary>
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x004D122A File Offset: 0x004CF42A
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x004D1232 File Offset: 0x004CF432
		public new Color? OverrideColor { get; internal set; }

		/// <summary>
		/// Creates a new DrawableTooltipLine object
		/// </summary>
		/// <param name="parent">The TooltipLine to make this DrawableTooltipLine from</param>
		/// <param name="index">The index of the line in the array</param>
		/// <param name="x">The X position where the tooltip would be drawn.</param>
		/// <param name="y">The Y position where the tooltip would be drawn.</param>
		/// <param name="color">The color the tooltip would be drawn in</param>
		// Token: 0x06001BD2 RID: 7122 RVA: 0x004D123C File Offset: 0x004CF43C
		public DrawableTooltipLine(TooltipLine parent, int index, int x, int y, Color color) : base(parent.Mod, parent.Name, parent.Text)
		{
			this.IsModifier = parent.IsModifier;
			this.IsModifierBad = parent.IsModifierBad;
			this.OverrideColor = parent.OverrideColor;
			this.OneDropLogo = parent.OneDropLogo;
			this.Text = parent.Text;
			this.Index = index;
			this.OriginalX = x;
			this.OriginalY = y;
			this.Color = color;
		}

		// Token: 0x040014DB RID: 5339
		private int _originalX;

		// Token: 0x040014DC RID: 5340
		private int _originalY;

		/// <summary>
		/// The text of this tooltip.
		/// </summary>
		// Token: 0x040014DD RID: 5341
		public new readonly string Text;

		/// <summary>
		/// The index of the tooltip in the array
		/// </summary>
		// Token: 0x040014DE RID: 5342
		public readonly int Index;

		/// <summary>
		/// Whether or not this tooltip gives prefix information. This will make it so that the tooltip is colored either green or red.
		/// </summary>
		// Token: 0x040014DF RID: 5343
		public new readonly bool IsModifier;

		/// <summary>
		/// If isModifier is true, this determines whether the tooltip is colored green or red.
		/// </summary>
		// Token: 0x040014E0 RID: 5344
		public new readonly bool IsModifierBad;

		/// <summary>
		/// The X position where the tooltip would be drawn.
		/// </summary>
		// Token: 0x040014E1 RID: 5345
		public int X;

		/// <summary>
		/// The Y position where the tooltip would be drawn.
		/// </summary>
		// Token: 0x040014E2 RID: 5346
		public int Y;

		/// <summary>
		/// Whether the tooltip is a One Drop logo or not. If it is, the tooltip text will be empty.
		/// </summary>
		// Token: 0x040014E5 RID: 5349
		public new readonly bool OneDropLogo;

		/// <summary>
		/// The font this tooltip would be drawn with
		/// </summary>
		// Token: 0x040014E6 RID: 5350
		public DynamicSpriteFont Font = FontAssets.MouseText.Value;

		/// <summary>
		/// The rotation this tooltip would be drawn in
		/// </summary>
		// Token: 0x040014E7 RID: 5351
		public float Rotation;

		/// <summary>
		/// The origin of this tooltip
		/// </summary>
		// Token: 0x040014E8 RID: 5352
		public Vector2 Origin = Vector2.Zero;

		/// <summary>
		/// The baseScale of this tooltip. When drawing the One Drop logo the scale is calculated by (baseScale.X + baseScale.Y) / 2
		/// </summary>
		// Token: 0x040014E9 RID: 5353
		public Vector2 BaseScale = Vector2.One;

		// Token: 0x040014EA RID: 5354
		public float MaxWidth = -1f;

		// Token: 0x040014EB RID: 5355
		public float Spread = 2f;
	}
}
