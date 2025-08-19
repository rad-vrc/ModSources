using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ID;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class represents a type of dust that is added by a mod. Only one instance of this class will ever exist for each type of dust you add.<br />
	/// The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Dust">Basic Dust Guide</see> teaches the basics of making modded dust.
	/// </summary>
	// Token: 0x020001AE RID: 430
	[Autoload(true, Side = ModSide.Client)]
	public abstract class ModDust : ModTexturedType
	{
		/// <summary> Allows you to choose a type of dust for this type of dust to copy the behavior of. Defaults to -1, which means that no behavior is copied. </summary>
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x004E3DD3 File Offset: 0x004E1FD3
		// (set) Token: 0x060020AA RID: 8362 RVA: 0x004E3DDB File Offset: 0x004E1FDB
		public int UpdateType { get; set; } = -1;

		/// <summary> The sprite sheet that this type of dust uses. Normally a sprite sheet will consist of a vertical alignment of three 10 x 10 pixel squares, each one containing a possible look for the dust. </summary>
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x004E3DE4 File Offset: 0x004E1FE4
		// (set) Token: 0x060020AC RID: 8364 RVA: 0x004E3DEC File Offset: 0x004E1FEC
		public Asset<Texture2D> Texture2D { get; private set; }

		/// <summary> The ID of this type of dust. </summary>
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060020AD RID: 8365 RVA: 0x004E3DF5 File Offset: 0x004E1FF5
		// (set) Token: 0x060020AE RID: 8366 RVA: 0x004E3DFD File Offset: 0x004E1FFD
		public int Type { get; internal set; }

		// Token: 0x060020AF RID: 8367 RVA: 0x004E3E08 File Offset: 0x004E2008
		protected sealed override void Register()
		{
			ModTypeLookup<ModDust>.Register(this);
			DustLoader.dusts.Add(this);
			this.Type = DustLoader.ReserveDustID();
			DustID.Search.Add(base.FullName, this.Type);
			this.Texture2D = ((!string.IsNullOrEmpty(this.Texture)) ? ModContent.Request<Texture2D>(this.Texture, 2) : TextureAssets.Dust);
		}

		/// <summary>
		/// Allows drawing behind this dust, such as a trail, or modifying the way it is drawn. Return false to stop the normal dust drawing code (useful if you're manually drawing the dust itself). Returns true by default.
		/// </summary>
		/// <returns></returns>
		// Token: 0x060020B0 RID: 8368 RVA: 0x004E3E6D File Offset: 0x004E206D
		public virtual bool PreDraw(Dust dust)
		{
			return true;
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x004E3E70 File Offset: 0x004E2070
		internal void Draw(Dust dust, Color alpha, float scale)
		{
			Main.spriteBatch.Draw(this.Texture2D.Value, dust.position - Main.screenPosition, new Rectangle?(dust.frame), alpha, dust.rotation, new Vector2(4f, 4f), scale, 0, 0f);
			if (dust.color != default(Color))
			{
				Main.spriteBatch.Draw(this.Texture2D.Value, dust.position - Main.screenPosition, new Rectangle?(dust.frame), dust.GetColor(alpha), dust.rotation, new Vector2(4f, 4f), scale, 0, 0f);
			}
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x004E3F33 File Offset: 0x004E2133
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Allows you to modify the properties after initial loading has completed.
		/// This is where you would update ModDust's UpdateType property and modify the Terraria.GameContent.ChildSafety.SafeDust array.
		/// </summary>
		// Token: 0x060020B3 RID: 8371 RVA: 0x004E3F3B File Offset: 0x004E213B
		public override void SetStaticDefaults()
		{
		}

		/// <summary>
		/// Allows you to modify a dust's fields when it is created.
		/// </summary>
		// Token: 0x060020B4 RID: 8372 RVA: 0x004E3F3D File Offset: 0x004E213D
		public virtual void OnSpawn(Dust dust)
		{
		}

		/// <summary>
		/// Allows you to customize how you want this type of dust to behave. Return true to allow for vanilla dust updating to also take place; will return true by default. Normally you will want this to return false.
		/// </summary>
		// Token: 0x060020B5 RID: 8373 RVA: 0x004E3F3F File Offset: 0x004E213F
		public virtual bool Update(Dust dust)
		{
			return true;
		}

		/// <summary>
		/// Allows you to add behavior to this dust on top of the default dust behavior. Return true if you're applying your own behavior; return false to make the dust slow down by itself. Normally you will want this to return true.
		/// </summary>
		// Token: 0x060020B6 RID: 8374 RVA: 0x004E3F42 File Offset: 0x004E2142
		public virtual bool MidUpdate(Dust dust)
		{
			return false;
		}

		/// <summary>
		/// Allows you to override the color this dust will draw in. Return null to draw it in the normal light color; returns null by default. Note that the dust.noLight field makes the dust ignore lighting and draw in full brightness, and can be set in OnSpawn instead of having to return Color.White here.
		/// </summary>
		// Token: 0x060020B7 RID: 8375 RVA: 0x004E3F48 File Offset: 0x004E2148
		public virtual Color? GetAlpha(Dust dust, Color lightColor)
		{
			return null;
		}
	}
}
