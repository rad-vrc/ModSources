using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x0200039D RID: 925
	internal class DefinitionOptionElement<T> : UIElement where T : EntityDefinition
	{
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060031EB RID: 12779 RVA: 0x005419F8 File Offset: 0x0053FBF8
		public static Asset<Texture2D> DefaultBackgroundTexture { get; } = TextureAssets.InventoryBack9;

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x005419FF File Offset: 0x0053FBFF
		// (set) Token: 0x060031ED RID: 12781 RVA: 0x00541A07 File Offset: 0x0053FC07
		public Asset<Texture2D> BackgroundTexture { get; set; } = DefinitionOptionElement<T>.DefaultBackgroundTexture;

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060031EE RID: 12782 RVA: 0x00541A10 File Offset: 0x0053FC10
		// (set) Token: 0x060031EF RID: 12783 RVA: 0x00541A18 File Offset: 0x0053FC18
		public string Tooltip { get; set; }

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060031F0 RID: 12784 RVA: 0x00541A21 File Offset: 0x0053FC21
		// (set) Token: 0x060031F1 RID: 12785 RVA: 0x00541A29 File Offset: 0x0053FC29
		public int Type { get; set; }

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060031F2 RID: 12786 RVA: 0x00541A32 File Offset: 0x0053FC32
		// (set) Token: 0x060031F3 RID: 12787 RVA: 0x00541A3A File Offset: 0x0053FC3A
		public int NullID { get; set; }

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060031F4 RID: 12788 RVA: 0x00541A43 File Offset: 0x0053FC43
		// (set) Token: 0x060031F5 RID: 12789 RVA: 0x00541A4B File Offset: 0x0053FC4B
		public T Definition { get; set; }

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060031F6 RID: 12790 RVA: 0x00541A54 File Offset: 0x0053FC54
		// (set) Token: 0x060031F7 RID: 12791 RVA: 0x00541A5C File Offset: 0x0053FC5C
		internal float Scale { get; set; } = 0.75f;

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060031F8 RID: 12792 RVA: 0x00541A65 File Offset: 0x0053FC65
		// (set) Token: 0x060031F9 RID: 12793 RVA: 0x00541A6D File Offset: 0x0053FC6D
		protected bool Unloaded { get; set; }

		// Token: 0x060031FA RID: 12794 RVA: 0x00541A78 File Offset: 0x0053FC78
		public DefinitionOptionElement(T definition, float scale = 0.75f)
		{
			this.SetItem(definition);
			this.Scale = scale;
			this.Width.Set((float)DefinitionOptionElement<T>.DefaultBackgroundTexture.Width() * scale, 0f);
			this.Height.Set((float)DefinitionOptionElement<T>.DefaultBackgroundTexture.Height() * scale, 0f);
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x00541AEC File Offset: 0x0053FCEC
		public virtual void SetItem(T item)
		{
			this.Definition = item;
			T t = this.Definition;
			this.Type = ((t != null) ? t.Type : this.NullID);
			T t2 = this.Definition;
			this.Unloaded = (t2 != null && t2.IsUnloaded);
			if (this.Definition == null || (this.Type == this.NullID && !this.Unloaded))
			{
				this.Tooltip = Lang.inter[23].Value;
				return;
			}
			if (this.Unloaded)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 3);
				defaultInterpolatedStringHandler.AppendFormatted(this.Definition.Name);
				defaultInterpolatedStringHandler.AppendLiteral(" [");
				defaultInterpolatedStringHandler.AppendFormatted(this.Definition.Mod);
				defaultInterpolatedStringHandler.AppendLiteral("] (");
				defaultInterpolatedStringHandler.AppendFormatted(Language.GetTextValue("Mods.ModLoader.Unloaded"));
				defaultInterpolatedStringHandler.AppendLiteral(")");
				this.Tooltip = defaultInterpolatedStringHandler.ToStringAndClear();
				return;
			}
			this.Tooltip = this.Definition.DisplayName + " [" + this.Definition.Mod + "]";
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x00541C30 File Offset: 0x0053FE30
		public virtual void SetScale(float scale)
		{
			this.Scale = scale;
			this.Width.Set((float)DefinitionOptionElement<T>.DefaultBackgroundTexture.Width() * scale, 0f);
			this.Height.Set((float)DefinitionOptionElement<T>.DefaultBackgroundTexture.Height() * scale, 0f);
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x00541C80 File Offset: 0x0053FE80
		public override int CompareTo(object obj)
		{
			DefinitionOptionElement<T> other = obj as DefinitionOptionElement<T>;
			return this.Type.CompareTo(other.Type);
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x00541CA8 File Offset: 0x0053FEA8
		public override string ToString()
		{
			return this.Definition.ToString();
		}
	}
}
