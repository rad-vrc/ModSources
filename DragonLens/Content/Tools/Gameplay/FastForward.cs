using DragonLens.Core.Systems.ThemeSystem;
using DragonLens.Core.Systems.ToolSystem;
using DragonLens.Helpers;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;

namespace DragonLens.Content.Tools.Gameplay
{
	internal class FastForward : Tool
	{
		public static int speedup = 0;

		public override string IconKey => "FastForward";

		public override bool HasRightClick => true;

		public override void OnActivate()
		{
			// 互換: QoLCompendium 系が有効な場合は高速化を無効化
			if (ModLoader.HasMod("QoLCompendium") || ModLoader.HasMod("QoLCompendium_Re"))
			{
				Main.NewText("Fast Forward disabled due to QoLCompendium compatibility", Color.Red);
				speedup = 0;
				return;
			}

			if (Main.netMode != NetmodeID.SinglePlayer)
			{
				Main.NewText(LocalizationHelper.GetToolText("FastForward.MultiplayerDisabled"), Color.Red);
				speedup = 0;
				return;
			}

			if (speedup < 4)
				speedup++;
			else
				speedup = 0;
		}

		public override void OnRightClick()
		{
			// 互換: QoLCompendium 系が有効な場合は高速化を無効化
			if (ModLoader.HasMod("QoLCompendium") || ModLoader.HasMod("QoLCompendium_Re"))
			{
				Main.NewText("Fast Forward disabled due to QoLCompendium compatibility", Color.Red);
				speedup = 0;
				return;
			}

			if (Main.netMode != NetmodeID.SinglePlayer)
			{
				Main.NewText(LocalizationHelper.GetToolText("FastForward.MultiplayerDisabled"), Color.Red);
				speedup = 0;
				return;
			}

			if (speedup > 0)
				speedup--;
			else
				speedup = 4;
		}

		public override void DrawIcon(SpriteBatch spriteBatch, Rectangle position)
		{
			base.DrawIcon(spriteBatch, position);

			if (speedup > 0)
			{
				GUIHelper.DrawOutline(spriteBatch, new Rectangle(position.X - 4, position.Y - 4, 46, 46), ThemeHandler.ButtonColor.InvertColor());

				Texture2D tex = Assets.Misc.GlowAlpha.Value;
				Color color = new Color(150, 255, 170) * (speedup / 4f);
				color.A = 0;
				var target = new Rectangle(position.X, position.Y, 38, 38);

				spriteBatch.Draw(tex, target, color);
			}
		}

		public override void SaveData(TagCompound tag)
		{
			tag["speedup"] = speedup;
		}

		public override void LoadData(TagCompound tag)
		{
			if (Main.netMode != NetmodeID.SinglePlayer)
			{
				speedup = 0;
				return;
			}

			speedup = tag.GetInt("speedup");
		}
	}

	internal class FastForwardSystem : ModSystem
	{
		private bool _compatDisabled;

		public override void Load()
		{
			// 互換: QoLCompendium 系が有効な場合はフック自体を無効化
			_compatDisabled = ModLoader.HasMod("QoLCompendium") || ModLoader.HasMod("QoLCompendium_Re");
			if (_compatDisabled)
			{
				FastForward.speedup = 0;
				return;
			}

			Terraria.On_Main.DoUpdate += UpdateExtraTimes;
		}

		public override void Unload()
		{
			if (!_compatDisabled)
				Terraria.On_Main.DoUpdate -= UpdateExtraTimes;
		}

		private void UpdateExtraTimes(Terraria.On_Main.orig_DoUpdate orig, Main self, ref GameTime gameTime)
		{
			orig(self, ref gameTime);

			for (int k = 0; k < FastForward.speedup; k++)
			{
				orig(self, ref gameTime);
			}
		}
	}
}
