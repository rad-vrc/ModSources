using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using TranslateTest2.Common.GlobalNPCs; // WhipTagGNPC
using TranslateTest2.Common.GlobalItems; // PrefixGlobalItem
using TranslateTest2.Content.Buffs; // WhipTag
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TranslateTest2
{
    public static class SummonerPrefixUtils
    {
        public static float getDistance(Vector2 v1, Vector2 v2)
        {
            // XNA の最適化済み実装を利用して割り当てと double 演算を回避
            return Vector2.Distance(v1, v2);
        }

        public static void UseBlendState(this SpriteBatch sb, BlendState blend, SamplerState s = null)
        {
            sb.End();
            sb.Begin((SpriteSortMode)1, blend, s == null ? Main.DefaultSamplerState : s, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.ZoomMatrix);
        }

        public static bool LineThroughRect(
            Vector2 start,
            Vector2 end,
            Rectangle rect,
            int lineWidth = 4,
            int checkDistance = 8)
        {
            float num = 0.0f;
            return Collision.CheckAABBvLineCollision(TopLeft(rect), Size(rect), start, end, (float)lineWidth, ref num);
        }

        private static Vector2 TopLeft(Rectangle rect) => new Vector2(rect.X, rect.Y);
        private static Vector2 Size(Rectangle rect) => new Vector2(rect.Width, rect.Height);

        public static PrefixGlobalItem global(this Item item)
        {
            // 原典仕様: 既存インスタンスが無い場合は新規インスタンスを生成して返す（tMLにアタッチしない一時オブジェクト）
            PrefixGlobalItem existing;
            return item.TryGetGlobalItem<PrefixGlobalItem>(out existing) ? item.GetGlobalItem<PrefixGlobalItem>() : new PrefixGlobalItem();
        }

        public static void AddTag(this NPC npc, WhipTag tag)
        {
            WhipTagGNPC globalNpc = npc.GetGlobalNPC<WhipTagGNPC>();
            foreach (WhipTag tag1 in globalNpc.tags)
            {
                if (tag1.ID == tag.ID)
                {
                    tag1.TimeLeft = tag.TimeLeft;
                    tag1.TagDamage = tag.TagDamage;
                    tag1.TagDamageMult = tag.TagDamageMult;
                    tag1.CritAdd = tag.CritAdd;
                    return;
                }
            }
            globalNpc.tags.Add(tag);
        }

        public static bool isMinionSummonItem(this Item item, bool sentry = true)
        {
            if (item.shoot != ProjectileID.None)
            {
                Projectile projectile1 = new Projectile();
                projectile1.SetDefaults(item.shoot);
                if (projectile1.minion || (projectile1.sentry && sentry))
                {
                    return true;
                }
            }
            return false;
        }

        public static void Replace(this List<TooltipLine> tooltips, string targetStr, string to)
        {
            if (Main.dedServ)
                return;
            tooltips.FindAndReplace(targetStr, to);
        }

        public static void Replace(this List<TooltipLine> tooltips, string targetStr, int to)
        {
            if (Main.dedServ)
                return;
            tooltips.FindAndReplace(targetStr, to.ToString());
        }

        public static void Replace(this List<TooltipLine> tooltips, string targetStr, float to)
        {
            if (Main.dedServ)
                return;
            tooltips.FindAndReplace(targetStr, to.ToString());
        }

        public static int ToPercent(this float f) => (int)Math.Round((double)f * 100.0);

        public static void Replace(this TooltipLine tl, string targetStr, string to)
        {
            tl.Text = tl.Text.Replace(targetStr, to);
        }

        public static void FindAndReplace(
            this List<TooltipLine> tooltips,
            string replacedKey,
            string newKey)
        {
            TooltipLine tooltipLine = tooltips.FirstOrDefault<TooltipLine>((Func<TooltipLine, bool>)(x => x.Mod == "Terraria" && x.Text.Contains(replacedKey)));
            if (tooltipLine == null)
                return;
            tooltipLine.Text = tooltipLine.Text.Replace(replacedKey, newKey);
        }
    }
}