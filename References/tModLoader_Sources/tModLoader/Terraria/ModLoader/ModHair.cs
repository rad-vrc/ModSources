using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ID;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class allows you to define custom hair styles for a player, controlling the associated gender and unlock conditions.
	/// </summary>
	// Token: 0x020001B1 RID: 433
	public abstract class ModHair : ModTexturedType
	{
		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x004E4145 File Offset: 0x004E2345
		// (set) Token: 0x060020DC RID: 8412 RVA: 0x004E414D File Offset: 0x004E234D
		public int Type { get; internal set; }

		/// <summary>
		/// The path to the alternative texture used for when the hair is covered by a hat.
		/// </summary>
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x004E4156 File Offset: 0x004E2356
		public virtual string AltTexture
		{
			get
			{
				return this.Texture + "_Alt";
			}
		}

		/// <summary>
		/// Used to set the character gender based on hairstyle when randomizing a new character. <br />
		/// If <see cref="F:Terraria.ModLoader.Gender.Unspecified" />, the gender will be randomly rolled. <br />
		/// Note that all hairstyles can be selected with either gender. This is just a default for quick randomization.
		/// </summary>
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060020DE RID: 8414 RVA: 0x004E4168 File Offset: 0x004E2368
		public virtual Gender RandomizedCharacterCreationGender
		{
			get
			{
				return Gender.Unspecified;
			}
		}

		/// <summary>
		/// Determines whether this hairstyle is available during character creation. <br />
		/// This is distinctly different from <see cref="M:Terraria.ModLoader.ModHair.GetUnlockConditions" />, which determines whether the hairstyle
		/// is available in-game in the Stylist UI.
		/// </summary>
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x004E416B File Offset: 0x004E236B
		public virtual bool AvailableDuringCharacterCreation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x004E416E File Offset: 0x004E236E
		protected sealed override void Register()
		{
			ModTypeLookup<ModHair>.Register(this);
			this.Type = HairLoader.Register(this);
			HairID.Search.Add(base.FullName, this.Type);
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x004E4198 File Offset: 0x004E2398
		public sealed override void SetupContent()
		{
			this.AutoStaticDefaults();
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Automatically sets certain static defaults. Override this if you do not want the properties to be set for you.
		/// </summary>
		// Token: 0x060020E2 RID: 8418 RVA: 0x004E41A6 File Offset: 0x004E23A6
		public virtual void AutoStaticDefaults()
		{
			TextureAssets.PlayerHair[this.Type] = ModContent.Request<Texture2D>(this.Texture, 2);
			TextureAssets.PlayerHairAlt[this.Type] = ModContent.Request<Texture2D>(this.AltTexture, 2);
		}

		/// <summary>
		/// Gets the unlock conditions for this hairstyle. No conditions by default. <br />
		/// These conditions are used exclusively for the Stylist UI in-game; see <see cref="P:Terraria.ModLoader.ModHair.AvailableDuringCharacterCreation" /> for character creation.
		/// </summary>
		// Token: 0x060020E3 RID: 8419 RVA: 0x004E41D8 File Offset: 0x004E23D8
		public virtual IEnumerable<Condition> GetUnlockConditions()
		{
			return Enumerable.Empty<Condition>();
		}
	}
}
