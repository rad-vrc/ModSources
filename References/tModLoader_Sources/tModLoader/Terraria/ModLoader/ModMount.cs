using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class serves as a place for you to place all your properties and hooks for each mount.
	/// <br /> To use it, simply create a new class deriving from this one. Implementations will be registered automatically.
	/// <para /> Only one instance of ModMount will exist for each mount, so storing player specific data on the ModMount is not good.
	/// Modders can use <i>player.mount._mountSpecificData</i> or a ModPlayer class to store player specific data relating to a mount. Use SetMount to assign these fields.
	/// <para /> Note that texture autoloading is unique for ModMount, see <see cref="P:Terraria.ModLoader.ModMount.Texture" /> for more information.
	/// </summary>
	// Token: 0x020001B7 RID: 439
	public abstract class ModMount : ModType<Mount.MountData, ModMount>
	{
		/// <summary>
		/// The vanilla MountData object that is controlled by this ModMount.
		/// </summary>
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060021BF RID: 8639 RVA: 0x004E59FF File Offset: 0x004E3BFF
		public Mount.MountData MountData
		{
			get
			{
				return base.Entity;
			}
		}

		/// <summary>
		/// The index of this ModMount in the Mount.mounts array.
		/// </summary>
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x004E5A07 File Offset: 0x004E3C07
		// (set) Token: 0x060021C1 RID: 8641 RVA: 0x004E5A0F File Offset: 0x004E3C0F
		public int Type { get; internal set; }

		/// <summary>
		/// The file name of this type's texture file in the mod loader's file space.
		/// <para /> For mounts, this path isn't used directly to autoload a texture, but it is combined with <see cref="T:Terraria.ModLoader.MountTextureType" /> values to autoload textures for each of the different layers that mounts support, if that texture exists.
		/// <para /> ModMount typically want at least one of either a "Back" or "Front" texture. For example, a mount named "MyMount" would need to include a "MyMount_Back.png" or "MyMount_Front.png" texture.
		/// </summary>
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060021C2 RID: 8642 RVA: 0x004E5A18 File Offset: 0x004E3C18
		public virtual string Texture
		{
			get
			{
				return (base.GetType().Namespace + "." + this.Name).Replace('.', '/');
			}
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x004E5A3E File Offset: 0x004E3C3E
		protected override Mount.MountData CreateTemplateEntity()
		{
			return new Mount.MountData
			{
				ModMount = this
			};
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x004E5A4C File Offset: 0x004E3C4C
		protected sealed override void Register()
		{
			if (Mount.mounts == null || Mount.mounts.Length == MountID.Count)
			{
				Mount.Initialize();
			}
			this.Type = MountLoader.ReserveMountID();
			MountID.Search.Add(base.FullName, this.Type);
			ModTypeLookup<ModMount>.Register(this);
			MountLoader.mountDatas[this.Type] = this;
			foreach (object obj in Enum.GetValues(typeof(MountTextureType)))
			{
				MountTextureType textureType = (MountTextureType)obj;
				string extraTexture = this.GetExtraTexture(textureType);
				Asset<Texture2D> textureAsset;
				if (!string.IsNullOrEmpty(extraTexture) && ModContent.RequestIfExists<Texture2D>(extraTexture, out textureAsset, 2))
				{
					switch (textureType)
					{
					case MountTextureType.Back:
						this.MountData.backTexture = textureAsset;
						break;
					case MountTextureType.BackGlow:
						this.MountData.backTextureGlow = textureAsset;
						break;
					case MountTextureType.BackExtra:
						this.MountData.backTextureExtra = textureAsset;
						break;
					case MountTextureType.BackExtraGlow:
						this.MountData.backTextureExtraGlow = textureAsset;
						break;
					case MountTextureType.Front:
						this.MountData.frontTexture = textureAsset;
						break;
					case MountTextureType.FrontGlow:
						this.MountData.frontTextureGlow = textureAsset;
						break;
					case MountTextureType.FrontExtra:
						this.MountData.frontTextureExtra = textureAsset;
						break;
					case MountTextureType.FrontExtraGlow:
						this.MountData.frontTextureExtraGlow = textureAsset;
						break;
					}
				}
			}
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x004E5BBC File Offset: 0x004E3DBC
		public sealed override void SetupContent()
		{
			Mount.mounts[this.Type] = this.MountData;
			this.SetStaticDefaults();
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x004E5BD6 File Offset: 0x004E3DD6
		protected virtual string GetExtraTexture(MountTextureType textureType)
		{
			return this.Texture + "_" + textureType.ToString();
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x004E5BF5 File Offset: 0x004E3DF5
		public override ModMount NewInstance(Mount.MountData entity)
		{
			ModMount modMount = base.NewInstance(entity);
			modMount.Type = this.Type;
			return modMount;
		}

		/// <summary>
		/// Allows you to modify the properties after initial loading has completed.
		/// This is where you would set properties of this type of mount.
		/// </summary>
		// Token: 0x060021C8 RID: 8648 RVA: 0x004E5C0A File Offset: 0x004E3E0A
		public override void SetStaticDefaults()
		{
		}

		/// <summary>
		/// Allows you to modify the mount's jump height based on its state.
		/// </summary>
		/// <param name="mountedPlayer"></param>
		/// <param name="jumpHeight"></param>
		/// <param name="xVelocity"></param>
		// Token: 0x060021C9 RID: 8649 RVA: 0x004E5C0C File Offset: 0x004E3E0C
		public virtual void JumpHeight(Player mountedPlayer, ref int jumpHeight, float xVelocity)
		{
		}

		/// <summary>
		/// Allows you to modify the mount's jump speed based on its state.
		/// </summary>
		/// <param name="mountedPlayer"></param>
		/// <param name="jumpSeed"></param>
		/// <param name="xVelocity"></param>
		// Token: 0x060021CA RID: 8650 RVA: 0x004E5C0E File Offset: 0x004E3E0E
		public virtual void JumpSpeed(Player mountedPlayer, ref float jumpSeed, float xVelocity)
		{
		}

		/// <summary>
		/// Allows you to make things happen when mount is used (creating dust etc.) Can also be used for mount special abilities.
		/// </summary>
		/// <param name="player"></param>
		// Token: 0x060021CB RID: 8651 RVA: 0x004E5C10 File Offset: 0x004E3E10
		public virtual void UpdateEffects(Player player)
		{
		}

		/// <summary>
		/// Allows for manual updating of mount frame. Return false to stop the default frame behavior. Returns true by default.
		/// <para />Possible values for <paramref name="state" /> include:
		/// <br /> 0. Standing still on the ground or sliding
		/// <br /> 1. Moving on the ground
		/// <br /> 2. In the air, not flying. Hovering counts as this as well.
		/// <br /> 3. In the air, flying 
		/// <br /> 4. Flying in water
		/// </summary>
		/// <param name="mountedPlayer"></param>
		/// <param name="state"></param>
		/// <param name="velocity"></param>
		/// <returns></returns>
		// Token: 0x060021CC RID: 8652 RVA: 0x004E5C12 File Offset: 0x004E3E12
		public virtual bool UpdateFrame(Player mountedPlayer, int state, Vector2 velocity)
		{
			return true;
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x004E5C15 File Offset: 0x004E3E15
		internal virtual bool CustomBodyFrame()
		{
			return false;
		}

		/// <summary>
		/// Allows you to make things happen while the mouse is pressed while the mount is active. Called each tick the mouse is pressed.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="mousePosition"></param>
		/// <param name="toggleOn">Does nothing yet</param>
		// Token: 0x060021CE RID: 8654 RVA: 0x004E5C18 File Offset: 0x004E3E18
		public virtual void UseAbility(Player player, Vector2 mousePosition, bool toggleOn)
		{
		}

		/// <summary>
		/// Allows you to make things happen when the mount ability is aiming (while charging).
		/// </summary>
		/// <param name="player"></param>
		/// <param name="mousePosition"></param>
		// Token: 0x060021CF RID: 8655 RVA: 0x004E5C1A File Offset: 0x004E3E1A
		public virtual void AimAbility(Player player, Vector2 mousePosition)
		{
		}

		/// <summary>
		/// Allows you to make things happen when this mount is spawned in. Useful for player-specific initialization, utilizing player.mount._mountSpecificData or a ModPlayer class since ModMount is shared between all players.
		/// Custom dust spawning logic is also possible via the skipDust parameter.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="skipDust">Set to true to skip the vanilla dust spawning logic</param>
		// Token: 0x060021D0 RID: 8656 RVA: 0x004E5C1C File Offset: 0x004E3E1C
		public virtual void SetMount(Player player, ref bool skipDust)
		{
		}

		/// <summary>
		/// Allows you to make things happen when this mount is de-spawned. Useful for player-specific cleanup, see SetMount.
		/// Custom dust spawning logic is also possible via the skipDust parameter.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="skipDust">Set to true to skip the vanilla dust spawning logic</param>
		// Token: 0x060021D1 RID: 8657 RVA: 0x004E5C1E File Offset: 0x004E3E1E
		public virtual void Dismount(Player player, ref bool skipDust)
		{
		}

		/// <summary>
		/// Allows for complete customization of mount drawing. This method will be called once for each supported mount texture layer that exists. Use drawType to conditionally apply changes.
		/// drawType corresponds to the following: 0: backTexture, 1: backTextureExtra, 2: frontTexture. 3: frontTextureExtra
		/// Corresponding glow textures, such as backTextureGlow, are paired with their corresponding texture and passed into this method as well.
		/// Return false if you are manually adding DrawData to playerDrawData to replace the vanilla draw behavior, otherwise tweak ref variables to customize the drawing and add additional DrawData to playerDrawData.
		/// </summary>
		/// <param name="playerDrawData"></param>
		/// <param name="drawType">Corresponds to the following: 0: backTexture, 1: backTextureExtra, 2: frontTexture. 3: frontTextureExtra</param>
		/// <param name="drawPlayer"></param>
		/// <param name="texture"></param>
		/// <param name="glowTexture">The corresponding glow texture, if present</param>
		/// <param name="drawPosition"></param>
		/// <param name="frame"></param>
		/// <param name="drawColor"></param>
		/// <param name="glowColor"></param>
		/// <param name="rotation"></param>
		/// <param name="spriteEffects"></param>
		/// <param name="drawOrigin"></param>
		/// <param name="drawScale"></param>
		/// <param name="shadow"></param>
		/// <returns></returns>
		// Token: 0x060021D2 RID: 8658 RVA: 0x004E5C20 File Offset: 0x004E3E20
		public virtual bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
		{
			return true;
		}
	}
}
