using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PetRenamer.UI.MouseoverUI;
using PetRenamer.UI.RenamePetUI;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace PetRenamer
{
	// Token: 0x02000008 RID: 8
	[Autoload(true, Side = ModSide.Client)]
	public class PRUISystem : ModSystem
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002C80 File Offset: 0x00000E80
		public override void OnModLoad()
		{
			PRUISystem.petRenameInterface = new UserInterface();
			PRUISystem.mouseoverUI = new MouseoverUI();
			PRUISystem.mouseoverUI.Activate();
			PRUISystem.mouseoverUIInterface = new UserInterface();
			PRUISystem.mouseoverUIInterface.SetState(PRUISystem.mouseoverUI);
			RenamePetUI.LoadLocalization(base.Mod);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002CCF File Offset: 0x00000ECF
		public override void Unload()
		{
			PRUISystem.petRenameInterface = null;
			PRUISystem.mouseoverUIInterface = null;
			PRUISystem.mouseoverUI = null;
			UIQuitButton.asset = null;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002CE9 File Offset: 0x00000EE9
		internal static void ToggleRenamePetUI()
		{
			if (PRUISystem.petRenameInterface.CurrentState != null)
			{
				PRUISystem.CloseRenamePetUI();
				return;
			}
			PRUISystem.OpenRenamePetUI();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D04 File Offset: 0x00000F04
		internal static void OpenRenamePetUI()
		{
			RenamePetUI ui = new RenamePetUI();
			UIState state = new UIState();
			state.Append(ui);
			PRUISystem.petRenameInterface.SetState(state);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D2F File Offset: 0x00000F2F
		internal static void CloseRenamePetUI()
		{
			PRUISystem.petRenameInterface.SetState(null);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D3C File Offset: 0x00000F3C
		public override void PreSaveAndQuit()
		{
			if (PRUISystem.petRenameInterface.CurrentState != null)
			{
				RenamePetUI.saveItemInUI = true;
				PRUISystem.petRenameInterface.SetState(null);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D5B File Offset: 0x00000F5B
		public override void UpdateUI(GameTime gameTime)
		{
			this._lastUpdateUiGameTime = gameTime;
			UserInterface userInterface = PRUISystem.petRenameInterface;
			if (((userInterface != null) ? userInterface.CurrentState : null) != null)
			{
				PRUISystem.petRenameInterface.Update(gameTime);
			}
			PRUISystem.mouseoverUI.Update(gameTime);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002D90 File Offset: 0x00000F90
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int index = layers.FindIndex((GameInterfaceLayer layer) => layer.Name.Equals("Vanilla: Mouse Over"));
			if (index != -1)
			{
				layers.Insert(index + 1, new LegacyGameInterfaceLayer("PetRenamer: Mouse Over", delegate()
				{
					if (this._lastUpdateUiGameTime != null)
					{
						UserInterface userInterface = PRUISystem.mouseoverUIInterface;
						if (((userInterface != null) ? userInterface.CurrentState : null) != null)
						{
							PRUISystem.mouseoverUIInterface.Draw(Main.spriteBatch, this._lastUpdateUiGameTime);
						}
					}
					return true;
				}, InterfaceScaleType.UI));
			}
			index = layers.FindIndex((GameInterfaceLayer layer) => layer.Name.Equals("Vanilla: Mouse Text"));
			if (index != -1)
			{
				layers.Insert(index, new LegacyGameInterfaceLayer("PetRenamer: Rename Pet", delegate()
				{
					if (this._lastUpdateUiGameTime != null)
					{
						UserInterface userInterface = PRUISystem.petRenameInterface;
						if (((userInterface != null) ? userInterface.CurrentState : null) != null)
						{
							PRUISystem.petRenameInterface.Draw(Main.spriteBatch, this._lastUpdateUiGameTime);
						}
					}
					return true;
				}, InterfaceScaleType.UI));
			}
		}

		// Token: 0x04000021 RID: 33
		internal static UserInterface petRenameInterface;

		// Token: 0x04000022 RID: 34
		internal static UserInterface mouseoverUIInterface;

		// Token: 0x04000023 RID: 35
		internal static MouseoverUI mouseoverUI;

		// Token: 0x04000024 RID: 36
		private GameTime _lastUpdateUiGameTime;
	}
}
