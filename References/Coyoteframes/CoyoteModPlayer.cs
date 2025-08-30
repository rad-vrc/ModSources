using System.ComponentModel;
using System.Reflection;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Coyoteframes
{
    public class CoyoteModPlayer : ModPlayer
    {
        public int CoyoteTimeFrames;
        public bool IsCoyoteTimeFrames;
        public bool IsJumpAlreadyMade;

        public override void PreUpdate()
        {

            if (Collision.TileCollision(Player.position, Player.velocity, Player.width, Player.height, true, false,
                    (int) Player.gravDir).Y == 0f)
            {
                CoyoteTimeFrames = ModContent.GetInstance<CoyoteConfig>().CoyoteTimeFramesDefault;
                IsCoyoteTimeFrames = false;
                IsJumpAlreadyMade = false;
            }
            else
            {
                if (CoyoteTimeFrames > 0)
                {
                    CoyoteTimeFrames--;
                    IsCoyoteTimeFrames = true;
                }
            }

        }

        public override void PostUpdateEquips()
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                if (CoyoteTimeFrames > 0 && IsCoyoteTimeFrames && Player.controlJump && Player.releaseJump && IsJumpAlreadyMade == false)
                {
                    IsJumpAlreadyMade = true;
                    Player.blockExtraJumps = true;
                    Player.velocity.Y = -Player.jumpSpeed * Player.gravDir;
                    Player.jump = Player.jumpHeight;
                }
            }
        }

        public override void OnEnterWorld()
        {
            if (Player.whoAmI == Main.myPlayer && ModContent.GetInstance<CoyoteConfig>().WelcomeMessageToggle)
            {
                Main.NewText(Language.GetTextValue("Mods.Coyoteframes.Welcome"), new Color(200, 235, 255));
            }
        }
    }
    public class CoyoteConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("Messages")] [DefaultValue(true)]
        public bool WelcomeMessageToggle;

        [Header("Frames")] [DefaultValue(9)] public int CoyoteTimeFramesDefault;

    }
    public class LuneWalPatch : ILoadable
    {
        private Mod LuneWoL => ModLoader.TryGetMod("LuneWoL", out Mod lune) ? lune : null;

        public bool IsLoadingEnabled(Mod mod)
        {
            return LuneWoL != null;
        }

        public void Load(Mod mod)
        {
            MonoModHooks.Modify(LuneWoL.Code.GetType("LuneWoL.LuneWoL").GetMethod("Load", BindingFlags.Instance | BindingFlags.Public), Callback);
        }

        public void Unload()
        {

        }

        private void Callback(ILContext il)
        {
            ILCursor cursor = new ILCursor(il);
            cursor.TryGotoNext(MoveType.Before, i => i.MatchLdstr("disable coyote frames mod... skill issue if you need that shit!!!! this cannot be turned off because i just dont feel like it LMFAO\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n"));
            cursor.RemoveRange(3);
        }
    }
}

