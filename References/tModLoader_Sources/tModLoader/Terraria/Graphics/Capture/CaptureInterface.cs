using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI.Chat;

namespace Terraria.Graphics.Capture
{
	// Token: 0x02000478 RID: 1144
	public class CaptureInterface
	{
		// Token: 0x06003771 RID: 14193 RVA: 0x00588547 File Offset: 0x00586747
		private static Dictionary<int, CaptureInterface.CaptureInterfaceMode> FillModes()
		{
			return new Dictionary<int, CaptureInterface.CaptureInterfaceMode>
			{
				{
					0,
					new CaptureInterface.ModeEdgeSelection()
				},
				{
					1,
					new CaptureInterface.ModeDragBounds()
				},
				{
					2,
					new CaptureInterface.ModeChangeSettings()
				}
			};
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x00588574 File Offset: 0x00586774
		public static Rectangle GetArea()
		{
			int num3 = Math.Min(CaptureInterface.EdgeA.X, CaptureInterface.EdgeB.X);
			int y = Math.Min(CaptureInterface.EdgeA.Y, CaptureInterface.EdgeB.Y);
			int num = Math.Abs(CaptureInterface.EdgeA.X - CaptureInterface.EdgeB.X);
			int num2 = Math.Abs(CaptureInterface.EdgeA.Y - CaptureInterface.EdgeB.Y);
			return new Rectangle(num3, y, num + 1, num2 + 1);
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x005885F8 File Offset: 0x005867F8
		public void Update()
		{
			PlayerInput.SetZoom_UI();
			this.UpdateCamera();
			if (CaptureInterface.CameraLock)
			{
				return;
			}
			bool toggleCameraMode = PlayerInput.Triggers.Current.ToggleCameraMode;
			if (toggleCameraMode && !this.KeyToggleActiveHeld && (Main.mouseItem.type == 0 || this.Active) && !Main.CaptureModeDisabled && !Main.player[Main.myPlayer].dead && !Main.player[Main.myPlayer].ghost)
			{
				this.ToggleCamera(!this.Active);
			}
			this.KeyToggleActiveHeld = toggleCameraMode;
			if (!this.Active)
			{
				return;
			}
			Main.blockMouse = true;
			if (CaptureInterface.JustActivated && Main.mouseLeftRelease && !Main.mouseLeft)
			{
				CaptureInterface.JustActivated = false;
			}
			Vector2 mouse;
			mouse..ctor((float)Main.mouseX, (float)Main.mouseY);
			if (this.UpdateButtons(mouse) && Main.mouseLeft)
			{
				return;
			}
			foreach (KeyValuePair<int, CaptureInterface.CaptureInterfaceMode> mode in CaptureInterface.Modes)
			{
				mode.Value.Selected = (mode.Key == this.SelectedMode);
				mode.Value.Update();
			}
			PlayerInput.SetZoom_Unscaled();
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x00588740 File Offset: 0x00586940
		public void Draw(SpriteBatch sb)
		{
			if (!this.Active)
			{
				return;
			}
			sb.End();
			sb.Begin(0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
			PlayerInput.SetZoom_UI();
			foreach (CaptureInterface.CaptureInterfaceMode captureInterfaceMode in CaptureInterface.Modes.Values)
			{
				captureInterfaceMode.Draw(sb);
			}
			sb.End();
			sb.Begin(0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
			PlayerInput.SetZoom_UI();
			Main.mouseText = false;
			Main.instance.GUIBarsDraw();
			this.DrawButtons(sb);
			Main.instance.DrawMouseOver();
			Utils.DrawBorderStringBig(sb, Lang.inter[81].Value, new Vector2((float)Main.screenWidth * 0.5f, 100f), Color.White, 1f, 0.5f, 0.5f, -1);
			Utils.DrawCursorSingle(sb, Main.cursorColor, float.NaN, Main.cursorScale, default(Vector2), 0, 0);
			this.DrawCameraLock(sb);
			sb.End();
			sb.Begin();
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x0058888C File Offset: 0x00586A8C
		public void ToggleCamera(bool On = true)
		{
			if (CaptureInterface.CameraLock)
			{
				return;
			}
			bool active = this.Active;
			this.Active = (CaptureInterface.Modes.ContainsKey(this.SelectedMode) && On);
			if (active != this.Active)
			{
				SoundEngine.PlaySound(On ? 10 : 11, -1, -1, 1, 1f, 0f);
			}
			foreach (KeyValuePair<int, CaptureInterface.CaptureInterfaceMode> mode in CaptureInterface.Modes)
			{
				mode.Value.ToggleActive(this.Active && mode.Key == this.SelectedMode);
			}
			if (On && !active)
			{
				CaptureInterface.JustActivated = true;
			}
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x00588958 File Offset: 0x00586B58
		private bool UpdateButtons(Vector2 mouse)
		{
			this.HoveredMode = -1;
			bool flag = !Main.graphics.IsFullScreen;
			int num = 9;
			for (int i = 0; i < num; i++)
			{
				if (new Rectangle(24 + 46 * i, 24, 42, 42).Contains(mouse.ToPoint()))
				{
					this.HoveredMode = i;
					bool flag2 = Main.mouseLeft && Main.mouseLeftRelease;
					int num2 = 0;
					if (i == num2++ && flag2)
					{
						CaptureInterface.QuickScreenshot();
					}
					if (i == num2++ && flag2 && CaptureInterface.EdgeAPinned && CaptureInterface.EdgeBPinned)
					{
						CaptureSettings captureSettings = new CaptureSettings();
						captureSettings.Area = CaptureInterface.GetArea();
						captureSettings.Biome = CaptureBiome.GetCaptureBiome(CaptureInterface.Settings.BiomeChoiceIndex);
						captureSettings.CaptureBackground = !CaptureInterface.Settings.TransparentBackground;
						captureSettings.CaptureEntities = CaptureInterface.Settings.IncludeEntities;
						captureSettings.UseScaling = CaptureInterface.Settings.PackImage;
						captureSettings.CaptureMech = WiresUI.Settings.DrawWires;
						if (captureSettings.Biome.WaterStyle != 13)
						{
							Main.liquidAlpha[13] = 0f;
						}
						CaptureInterface.StartCamera(captureSettings);
					}
					if (i == num2++ && flag2 && this.SelectedMode != 0)
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
						this.SelectedMode = 0;
						this.ToggleCamera(true);
					}
					if (i == num2++ && flag2 && this.SelectedMode != 1)
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
						this.SelectedMode = 1;
						this.ToggleCamera(true);
					}
					if (i == num2++ && flag2)
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
						CaptureInterface.ResetFocus();
					}
					if (i == num2++ && flag2 && Main.mapEnabled)
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
						Main.mapFullscreen = !Main.mapFullscreen;
					}
					if (i == num2++ && flag2 && this.SelectedMode != 2)
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
						this.SelectedMode = 2;
						this.ToggleCamera(true);
					}
					if (i == num2++ && flag2 && flag)
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
						Utils.OpenFolder(Path.Combine(Main.SavePath, "Captures"));
					}
					if (i == num2++ && flag2)
					{
						this.ToggleCamera(false);
						Main.blockMouse = true;
						Main.mouseLeftRelease = false;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x00588BD8 File Offset: 0x00586DD8
		public static void QuickScreenshot()
		{
			Point point = Main.ViewPosition.ToTileCoordinates();
			Point point2 = (Main.ViewPosition + Main.ViewSize).ToTileCoordinates();
			CaptureInterface.StartCamera(new CaptureSettings
			{
				Area = new Rectangle(point.X, point.Y, point2.X - point.X + 1, point2.Y - point.Y + 1),
				Biome = CaptureBiome.GetCaptureBiome(CaptureInterface.Settings.BiomeChoiceIndex),
				CaptureBackground = !CaptureInterface.Settings.TransparentBackground,
				CaptureEntities = CaptureInterface.Settings.IncludeEntities,
				UseScaling = CaptureInterface.Settings.PackImage,
				CaptureMech = WiresUI.Settings.DrawWires
			});
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x00588C84 File Offset: 0x00586E84
		private void DrawButtons(SpriteBatch sb)
		{
			sb.End();
			sb.Begin(0, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
			new Vector2((float)Main.mouseX, (float)Main.mouseY);
			int num = 9;
			for (int i = 0; i < num; i++)
			{
				Texture2D texture2D = TextureAssets.InventoryBack.Value;
				float num2 = 0.8f;
				Vector2 vector;
				vector..ctor((float)(24 + 46 * i), 24f);
				Color color = Main.inventoryBack * 0.8f;
				if (this.SelectedMode == 0 && i == 2)
				{
					texture2D = TextureAssets.InventoryBack14.Value;
				}
				else if (this.SelectedMode == 1 && i == 3)
				{
					texture2D = TextureAssets.InventoryBack14.Value;
				}
				else if (this.SelectedMode == 2 && i == 6)
				{
					texture2D = TextureAssets.InventoryBack14.Value;
				}
				else if (i >= 2 && i <= 3)
				{
					texture2D = TextureAssets.InventoryBack2.Value;
				}
				sb.Draw(texture2D, vector, null, color, 0f, default(Vector2), num2, 0, 0f);
				switch (i)
				{
				case 0:
					texture2D = TextureAssets.Camera[7].Value;
					break;
				case 1:
					texture2D = TextureAssets.Camera[0].Value;
					break;
				case 2:
				case 3:
				case 4:
					texture2D = TextureAssets.Camera[i].Value;
					break;
				case 5:
					texture2D = (Main.mapFullscreen ? TextureAssets.MapIcon[0].Value : TextureAssets.MapIcon[4].Value);
					break;
				case 6:
					texture2D = TextureAssets.Camera[1].Value;
					break;
				case 7:
					texture2D = TextureAssets.Camera[6].Value;
					break;
				case 8:
					texture2D = TextureAssets.Camera[5].Value;
					break;
				}
				sb.Draw(texture2D, vector + new Vector2(26f) * num2, null, Color.White, 0f, texture2D.Size() / 2f, 1f, 0, 0f);
				bool flag = false;
				if (i != 1)
				{
					if (i != 5)
					{
						if (i == 7)
						{
							if (Main.graphics.IsFullScreen)
							{
								flag = true;
							}
						}
					}
					else if (!Main.mapEnabled)
					{
						flag = true;
					}
				}
				else if (!CaptureInterface.EdgeAPinned || !CaptureInterface.EdgeBPinned)
				{
					flag = true;
				}
				if (flag)
				{
					sb.Draw(TextureAssets.Cd.Value, vector + new Vector2(26f) * num2, null, Color.White * 0.65f, 0f, TextureAssets.Cd.Value.Size() / 2f, 1f, 0, 0f);
				}
			}
			string text = "";
			switch (this.HoveredMode)
			{
			case -1:
				break;
			case 0:
				text = Lang.inter[111].Value;
				break;
			case 1:
				text = Lang.inter[67].Value;
				break;
			case 2:
				text = Lang.inter[69].Value;
				break;
			case 3:
				text = Lang.inter[70].Value;
				break;
			case 4:
				text = Lang.inter[78].Value;
				break;
			case 5:
				text = (Main.mapFullscreen ? Lang.inter[109].Value : Lang.inter[108].Value);
				break;
			case 6:
				text = Lang.inter[68].Value;
				break;
			case 7:
				text = Lang.inter[110].Value;
				break;
			case 8:
				text = Lang.inter[71].Value;
				break;
			default:
				text = "???";
				break;
			}
			int hoveredMode = this.HoveredMode;
			if (hoveredMode != 1)
			{
				if (hoveredMode != 5)
				{
					if (hoveredMode == 7)
					{
						if (Main.graphics.IsFullScreen)
						{
							text = text + "\n" + Lang.inter[113].Value;
						}
					}
				}
				else if (!Main.mapEnabled)
				{
					text = text + "\n" + Lang.inter[114].Value;
				}
			}
			else if (!CaptureInterface.EdgeAPinned || !CaptureInterface.EdgeBPinned)
			{
				text = text + "\n" + Lang.inter[112].Value;
			}
			if (text != "")
			{
				Main.instance.MouseText(text, 0, 0, -1, -1, -1, -1, 0);
			}
			sb.End();
			sb.Begin(0, null, null, null, null, null, Main.UIScaleMatrix);
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x005890FC File Offset: 0x005872FC
		private static bool GetMapCoords(int PinX, int PinY, int Goal, out Point result)
		{
			if (!Main.mapFullscreen)
			{
				result = new Point(-1, -1);
				return false;
			}
			int num20 = Main.maxTilesX / Main.textureMaxWidth;
			int num21 = Main.maxTilesY / Main.textureMaxHeight;
			float num4 = 10f;
			float num5 = 10f;
			float num6 = (float)(Main.maxTilesX - 10);
			float num7 = (float)(Main.maxTilesY - 10);
			float num8 = Main.mapFullscreenScale;
			float num9 = (float)Main.screenWidth / (float)Main.maxTilesX * 0.8f;
			if (Main.mapFullscreenScale < num9)
			{
				Main.mapFullscreenScale = num9;
			}
			if (Main.mapFullscreenScale > 16f)
			{
				Main.mapFullscreenScale = 16f;
			}
			num8 = Main.mapFullscreenScale;
			if (Main.mapFullscreenPos.X < num4)
			{
				Main.mapFullscreenPos.X = num4;
			}
			if (Main.mapFullscreenPos.X > num6)
			{
				Main.mapFullscreenPos.X = num6;
			}
			if (Main.mapFullscreenPos.Y < num5)
			{
				Main.mapFullscreenPos.Y = num5;
			}
			if (Main.mapFullscreenPos.Y > num7)
			{
				Main.mapFullscreenPos.Y = num7;
			}
			float x = Main.mapFullscreenPos.X;
			float y = Main.mapFullscreenPos.Y;
			float num10 = x * num8;
			y *= num8;
			float num11 = 0f - num10 + (float)(Main.screenWidth / 2);
			float num12 = 0f - y + (float)(Main.screenHeight / 2);
			num11 += num4 * num8;
			num12 += num5 * num8;
			float num13 = (float)(Main.maxTilesX / 840);
			num13 *= Main.mapFullscreenScale;
			float num14 = num11;
			float num15 = num12;
			float num16 = (float)TextureAssets.Map.Width();
			float num17 = (float)TextureAssets.Map.Height();
			if (Main.maxTilesX == 8400)
			{
				num13 *= 0.999f;
				num14 -= 40.6f * num13;
				num15 = num12 - 5f * num13;
				num16 -= 8.045f;
				num16 *= num13;
				num17 += 0.12f;
				num17 *= num13;
				if ((double)num13 < 1.2)
				{
					num17 += 1f;
				}
			}
			else if (Main.maxTilesX == 6400)
			{
				num13 *= 1.09f;
				num14 -= 38.8f * num13;
				num15 = num12 - 3.85f * num13;
				num16 -= 13.6f;
				num16 *= num13;
				num17 -= 6.92f;
				num17 *= num13;
				if ((double)num13 < 1.2)
				{
					num17 += 2f;
				}
			}
			else if (Main.maxTilesX == 6300)
			{
				num13 *= 1.09f;
				num14 -= 39.8f * num13;
				num15 = num12 - 4.08f * num13;
				num16 -= 26.69f;
				num16 *= num13;
				num17 -= 6.92f;
				num17 *= num13;
				if ((double)num13 < 1.2)
				{
					num17 += 2f;
				}
			}
			else if (Main.maxTilesX == 4200)
			{
				num13 *= 0.998f;
				num14 -= 37.3f * num13;
				num15 -= 1.7f * num13;
				num16 -= 16f;
				num16 *= num13;
				num17 -= 8.31f;
				num17 *= num13;
			}
			if (Goal != 0)
			{
				if (Goal != 1)
				{
					result = new Point(-1, -1);
					return false;
				}
				Vector2 vector;
				vector..ctor(num11, num12);
				Vector2 vector2 = new Vector2((float)PinX, (float)PinY) * num8 - new Vector2(10f * num8);
				result = (vector + vector2).ToPoint();
				return true;
			}
			else
			{
				int num18 = (int)((0f - num11 + (float)PinX) / num8 + num4);
				int num19 = (int)((0f - num12 + (float)PinY) / num8 + num5);
				bool flag = false;
				if ((float)num18 < num4)
				{
					flag = true;
				}
				if ((float)num18 >= num6)
				{
					flag = true;
				}
				if ((float)num19 < num5)
				{
					flag = true;
				}
				if ((float)num19 >= num7)
				{
					flag = true;
				}
				if (!flag)
				{
					result = new Point(num18, num19);
					return true;
				}
				result = new Point(-1, -1);
				return false;
			}
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x0058951C File Offset: 0x0058771C
		private static void ConstraintPoints()
		{
			int offScreenTiles = Lighting.OffScreenTiles;
			if (CaptureInterface.EdgeAPinned)
			{
				CaptureInterface.PointWorldClamp(ref CaptureInterface.EdgeA, offScreenTiles);
			}
			if (CaptureInterface.EdgeBPinned)
			{
				CaptureInterface.PointWorldClamp(ref CaptureInterface.EdgeB, offScreenTiles);
			}
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x00589554 File Offset: 0x00587754
		private static void PointWorldClamp(ref Point point, int fluff)
		{
			if (point.X < fluff)
			{
				point.X = fluff;
			}
			if (point.X > Main.maxTilesX - 1 - fluff)
			{
				point.X = Main.maxTilesX - 1 - fluff;
			}
			if (point.Y < fluff)
			{
				point.Y = fluff;
			}
			if (point.Y > Main.maxTilesY - 1 - fluff)
			{
				point.Y = Main.maxTilesY - 1 - fluff;
			}
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x005895C1 File Offset: 0x005877C1
		public bool UsingMap()
		{
			return CaptureInterface.CameraLock || CaptureInterface.Modes[this.SelectedMode].UsingMap();
		}

		// Token: 0x0600377D RID: 14205 RVA: 0x005895E1 File Offset: 0x005877E1
		public static void ResetFocus()
		{
			CaptureInterface.EdgeAPinned = false;
			CaptureInterface.EdgeBPinned = false;
			CaptureInterface.EdgeA = new Point(-1, -1);
			CaptureInterface.EdgeB = new Point(-1, -1);
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x00589608 File Offset: 0x00587808
		public void Scrolling()
		{
			int num = PlayerInput.ScrollWheelDelta / 120;
			num %= 30;
			if (num < 0)
			{
				num += 30;
			}
			int selectedMode = this.SelectedMode;
			this.SelectedMode -= num;
			while (this.SelectedMode < 0)
			{
				this.SelectedMode += 2;
			}
			while (this.SelectedMode > 2)
			{
				this.SelectedMode -= 2;
			}
			if (this.SelectedMode != selectedMode)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x00589694 File Offset: 0x00587894
		private void UpdateCamera()
		{
			if (CaptureInterface.CameraLock && CaptureInterface.CameraFrame == 4f)
			{
				CaptureManager.Instance.Capture(CaptureInterface.CameraSettings);
			}
			CaptureInterface.CameraFrame += (float)CaptureInterface.CameraLock.ToDirectionInt();
			if (CaptureInterface.CameraFrame < 0f)
			{
				CaptureInterface.CameraFrame = 0f;
			}
			if (CaptureInterface.CameraFrame > 5f)
			{
				CaptureInterface.CameraFrame = 5f;
			}
			if (CaptureInterface.CameraFrame == 5f)
			{
				CaptureInterface.CameraWaiting += 1f;
			}
			if (CaptureInterface.CameraWaiting > 60f)
			{
				CaptureInterface.CameraWaiting = 60f;
			}
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x00589738 File Offset: 0x00587938
		private void DrawCameraLock(SpriteBatch sb)
		{
			if (CaptureInterface.CameraFrame == 0f)
			{
				return;
			}
			sb.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Rectangle?(new Rectangle(0, 0, 1, 1)), Color.Black * (CaptureInterface.CameraFrame / 5f));
			if (CaptureInterface.CameraFrame != 5f)
			{
				return;
			}
			float num = CaptureInterface.CameraWaiting - 60f + 5f;
			if (num > 0f)
			{
				num /= 5f;
				float num2 = CaptureManager.Instance.GetProgress() * 100f;
				if (num2 > 100f)
				{
					num2 = 100f;
				}
				string text = num2.ToString("##") + " ";
				string text2 = "/ 100%";
				Vector2 vector = FontAssets.DeathText.Value.MeasureString(text);
				Vector2 vector2 = FontAssets.DeathText.Value.MeasureString(text2);
				Vector2 vector3;
				vector3..ctor(0f - vector.X, (0f - vector.Y) / 2f);
				Vector2 vector4;
				vector4..ctor(0f, (0f - vector2.Y) / 2f);
				ChatManager.DrawColorCodedStringWithShadow(sb, FontAssets.DeathText.Value, text, new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / 2f + vector3, Color.White * num, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
				ChatManager.DrawColorCodedStringWithShadow(sb, FontAssets.DeathText.Value, text2, new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / 2f + vector4, Color.White * num, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
			}
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x0058991E File Offset: 0x00587B1E
		public static void StartCamera(CaptureSettings settings)
		{
			SoundEngine.PlaySound(40, -1, -1, 1, 1f, 0f);
			CaptureInterface.CameraSettings = settings;
			CaptureInterface.CameraLock = true;
			CaptureInterface.CameraWaiting = 0f;
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x0058994B File Offset: 0x00587B4B
		public static void EndCamera()
		{
			CaptureInterface.CameraLock = false;
		}

		// Token: 0x0400512C RID: 20780
		private static Dictionary<int, CaptureInterface.CaptureInterfaceMode> Modes = CaptureInterface.FillModes();

		// Token: 0x0400512D RID: 20781
		public bool Active;

		// Token: 0x0400512E RID: 20782
		public static bool JustActivated;

		// Token: 0x0400512F RID: 20783
		private bool KeyToggleActiveHeld;

		// Token: 0x04005130 RID: 20784
		public int SelectedMode;

		// Token: 0x04005131 RID: 20785
		public int HoveredMode;

		// Token: 0x04005132 RID: 20786
		public static bool EdgeAPinned;

		// Token: 0x04005133 RID: 20787
		public static bool EdgeBPinned;

		// Token: 0x04005134 RID: 20788
		public static Point EdgeA;

		// Token: 0x04005135 RID: 20789
		public static Point EdgeB;

		// Token: 0x04005136 RID: 20790
		public static bool CameraLock;

		// Token: 0x04005137 RID: 20791
		private static float CameraFrame;

		// Token: 0x04005138 RID: 20792
		private static float CameraWaiting;

		// Token: 0x04005139 RID: 20793
		private const float CameraMaxFrame = 5f;

		// Token: 0x0400513A RID: 20794
		private const float CameraMaxWait = 60f;

		// Token: 0x0400513B RID: 20795
		private static CaptureSettings CameraSettings;

		// Token: 0x02000B88 RID: 2952
		public static class Settings
		{
			// Token: 0x04007649 RID: 30281
			public static bool PackImage = true;

			// Token: 0x0400764A RID: 30282
			public static bool IncludeEntities = true;

			// Token: 0x0400764B RID: 30283
			public static bool TransparentBackground;

			// Token: 0x0400764C RID: 30284
			public static int BiomeChoiceIndex = -1;

			// Token: 0x0400764D RID: 30285
			public static int ScreenAnchor = 0;

			// Token: 0x0400764E RID: 30286
			public static Color MarkedAreaColor = new Color(0.8f, 0.8f, 0.8f, 0f) * 0.3f;
		}

		// Token: 0x02000B89 RID: 2953
		private abstract class CaptureInterfaceMode
		{
			// Token: 0x06005D03 RID: 23811
			public abstract void Update();

			// Token: 0x06005D04 RID: 23812
			public abstract void Draw(SpriteBatch sb);

			// Token: 0x06005D05 RID: 23813
			public abstract void ToggleActive(bool tickedOn);

			// Token: 0x06005D06 RID: 23814
			public abstract bool UsingMap();

			// Token: 0x0400764F RID: 30287
			public bool Selected;
		}

		// Token: 0x02000B8A RID: 2954
		private class ModeEdgeSelection : CaptureInterface.CaptureInterfaceMode
		{
			// Token: 0x06005D08 RID: 23816 RVA: 0x006C596C File Offset: 0x006C3B6C
			public override void Update()
			{
				if (this.Selected)
				{
					PlayerInput.SetZoom_Context();
					Vector2 mouse;
					mouse..ctor((float)Main.mouseX, (float)Main.mouseY);
					this.EdgePlacement(mouse);
				}
			}

			// Token: 0x06005D09 RID: 23817 RVA: 0x006C59A0 File Offset: 0x006C3BA0
			public override void Draw(SpriteBatch sb)
			{
				if (this.Selected)
				{
					sb.End();
					sb.Begin(0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.CurrentWantedZoomMatrix);
					PlayerInput.SetZoom_Context();
					this.DrawMarkedArea(sb);
					this.DrawCursors(sb);
					sb.End();
					sb.Begin(0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
					PlayerInput.SetZoom_UI();
				}
			}

			// Token: 0x06005D0A RID: 23818 RVA: 0x006C5A1B File Offset: 0x006C3C1B
			public override void ToggleActive(bool tickedOn)
			{
			}

			// Token: 0x06005D0B RID: 23819 RVA: 0x006C5A1D File Offset: 0x006C3C1D
			public override bool UsingMap()
			{
				return true;
			}

			// Token: 0x06005D0C RID: 23820 RVA: 0x006C5A20 File Offset: 0x006C3C20
			private void EdgePlacement(Vector2 mouse)
			{
				if (CaptureInterface.JustActivated)
				{
					return;
				}
				Point result;
				if (!Main.mapFullscreen)
				{
					if (Main.mouseLeft)
					{
						CaptureInterface.EdgeAPinned = true;
						CaptureInterface.EdgeA = Main.MouseWorld.ToTileCoordinates();
					}
					if (Main.mouseRight)
					{
						CaptureInterface.EdgeBPinned = true;
						CaptureInterface.EdgeB = Main.MouseWorld.ToTileCoordinates();
					}
				}
				else if (CaptureInterface.GetMapCoords((int)mouse.X, (int)mouse.Y, 0, out result))
				{
					if (Main.mouseLeft)
					{
						CaptureInterface.EdgeAPinned = true;
						CaptureInterface.EdgeA = result;
					}
					if (Main.mouseRight)
					{
						CaptureInterface.EdgeBPinned = true;
						CaptureInterface.EdgeB = result;
					}
				}
				CaptureInterface.ConstraintPoints();
			}

			// Token: 0x06005D0D RID: 23821 RVA: 0x006C5ABC File Offset: 0x006C3CBC
			private void DrawMarkedArea(SpriteBatch sb)
			{
				if (!CaptureInterface.EdgeAPinned || !CaptureInterface.EdgeBPinned)
				{
					return;
				}
				int num = Math.Min(CaptureInterface.EdgeA.X, CaptureInterface.EdgeB.X);
				int num2 = Math.Min(CaptureInterface.EdgeA.Y, CaptureInterface.EdgeB.Y);
				int num3 = Math.Abs(CaptureInterface.EdgeA.X - CaptureInterface.EdgeB.X);
				int num4 = Math.Abs(CaptureInterface.EdgeA.Y - CaptureInterface.EdgeB.Y);
				if (!Main.mapFullscreen)
				{
					Rectangle value = Main.ReverseGravitySupport(new Rectangle(num * 16, num2 * 16, (num3 + 1) * 16, (num4 + 1) * 16));
					Rectangle value2 = Main.ReverseGravitySupport(new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth + 1, Main.screenHeight + 1));
					Rectangle result;
					Rectangle.Intersect(ref value2, ref value, ref result);
					if (result.Width != 0 && result.Height != 0)
					{
						result.Offset(-value2.X, -value2.Y);
						sb.Draw(TextureAssets.MagicPixel.Value, result, CaptureInterface.Settings.MarkedAreaColor);
						for (int i = 0; i < 2; i++)
						{
							sb.Draw(TextureAssets.MagicPixel.Value, new Rectangle(result.X, result.Y + ((i == 1) ? result.Height : -2), result.Width, 2), Color.White);
							sb.Draw(TextureAssets.MagicPixel.Value, new Rectangle(result.X + ((i == 1) ? result.Width : -2), result.Y, 2, result.Height), Color.White);
						}
					}
					return;
				}
				Point result2;
				CaptureInterface.GetMapCoords(num, num2, 1, out result2);
				Point result3;
				CaptureInterface.GetMapCoords(num + num3 + 1, num2 + num4 + 1, 1, out result3);
				Rectangle value3;
				value3..ctor(result2.X, result2.Y, result3.X - result2.X, result3.Y - result2.Y);
				Rectangle value4;
				value4..ctor(0, 0, Main.screenWidth + 1, Main.screenHeight + 1);
				Rectangle result4;
				Rectangle.Intersect(ref value4, ref value3, ref result4);
				if (result4.Width != 0 && result4.Height != 0)
				{
					result4.Offset(-value4.X, -value4.Y);
					sb.Draw(TextureAssets.MagicPixel.Value, result4, CaptureInterface.Settings.MarkedAreaColor);
					for (int j = 0; j < 2; j++)
					{
						sb.Draw(TextureAssets.MagicPixel.Value, new Rectangle(result4.X, result4.Y + ((j == 1) ? result4.Height : -2), result4.Width, 2), Color.White);
						sb.Draw(TextureAssets.MagicPixel.Value, new Rectangle(result4.X + ((j == 1) ? result4.Width : -2), result4.Y, 2, result4.Height), Color.White);
					}
				}
			}

			// Token: 0x06005D0E RID: 23822 RVA: 0x006C5DD8 File Offset: 0x006C3FD8
			private void DrawCursors(SpriteBatch sb)
			{
				float num = 1f / Main.cursorScale;
				float num2 = 0.8f / num;
				Vector2 vector = Main.screenPosition + new Vector2(30f);
				Vector2 max = vector + new Vector2((float)Main.screenWidth, (float)Main.screenHeight) - new Vector2(60f);
				if (Main.mapFullscreen)
				{
					vector -= Main.screenPosition;
					max -= Main.screenPosition;
				}
				Vector3 vector2 = Main.rgbToHsl(Main.cursorColor);
				Color color = Main.hslToRgb((vector2.X + 0.33f) % 1f, vector2.Y, vector2.Z, byte.MaxValue);
				Color color2 = Main.hslToRgb((vector2.X - 0.33f) % 1f, vector2.Y, vector2.Z, byte.MaxValue);
				color2 = (color = Color.White);
				bool flag = Main.player[Main.myPlayer].gravDir == -1f;
				if (!CaptureInterface.EdgeAPinned)
				{
					Utils.DrawCursorSingle(sb, color, 3.926991f, Main.cursorScale * num * num2, new Vector2((float)Main.mouseX - 5f + 12f, (float)Main.mouseY + 2.5f + 12f), 4, 0);
				}
				else
				{
					int specialMode = 0;
					float num3 = 0f;
					Vector2 zero = Vector2.Zero;
					if (!Main.mapFullscreen)
					{
						Vector2 vector3 = CaptureInterface.EdgeA.ToVector2() * 16f;
						if (!CaptureInterface.EdgeBPinned)
						{
							specialMode = 1;
							vector3 += Vector2.One * 8f;
							num3 = (-vector3 + Main.ReverseGravitySupport(new Vector2((float)Main.mouseX, (float)Main.mouseY), 0f) + Main.screenPosition).ToRotation();
							if (flag)
							{
								num3 = 0f - num3;
							}
							zero = Vector2.Clamp(vector3, vector, max);
							if (zero != vector3)
							{
								num3 = (vector3 - zero).ToRotation();
							}
						}
						else
						{
							Vector2 vector4;
							vector4..ctor((float)((CaptureInterface.EdgeA.X > CaptureInterface.EdgeB.X).ToInt() * 16), (float)((CaptureInterface.EdgeA.Y > CaptureInterface.EdgeB.Y).ToInt() * 16));
							vector3 += vector4;
							zero = Vector2.Clamp(vector3, vector, max);
							num3 = (CaptureInterface.EdgeB.ToVector2() * 16f + new Vector2(16f) - vector4 - zero).ToRotation();
							if (zero != vector3)
							{
								num3 = (vector3 - zero).ToRotation();
								specialMode = 1;
							}
							if (flag)
							{
								num3 *= -1f;
							}
						}
						Utils.DrawCursorSingle(sb, color, num3 - 1.5707964f, Main.cursorScale * num, Main.ReverseGravitySupport(zero - Main.screenPosition, 0f), 4, specialMode);
					}
					else
					{
						Point result = CaptureInterface.EdgeA;
						if (CaptureInterface.EdgeBPinned)
						{
							int num4 = (CaptureInterface.EdgeA.X > CaptureInterface.EdgeB.X).ToInt();
							int num5 = (CaptureInterface.EdgeA.Y > CaptureInterface.EdgeB.Y).ToInt();
							result.X += num4;
							result.Y += num5;
							CaptureInterface.GetMapCoords(result.X, result.Y, 1, out result);
							Point result2 = CaptureInterface.EdgeB;
							result2.X += 1 - num4;
							result2.Y += 1 - num5;
							CaptureInterface.GetMapCoords(result2.X, result2.Y, 1, out result2);
							zero = result.ToVector2();
							zero = Vector2.Clamp(zero, vector, max);
							num3 = (result2.ToVector2() - zero).ToRotation();
						}
						else
						{
							CaptureInterface.GetMapCoords(result.X, result.Y, 1, out result);
						}
						Utils.DrawCursorSingle(sb, color, num3 - 1.5707964f, Main.cursorScale * num, result.ToVector2(), 4, 0);
					}
				}
				if (!CaptureInterface.EdgeBPinned)
				{
					Utils.DrawCursorSingle(sb, color2, 0.7853981f, Main.cursorScale * num * num2, new Vector2((float)Main.mouseX + 2.5f + 12f, (float)Main.mouseY - 5f + 12f), 5, 0);
					return;
				}
				int specialMode2 = 0;
				float num6 = 0f;
				Vector2 zero2 = Vector2.Zero;
				if (!Main.mapFullscreen)
				{
					Vector2 vector5 = CaptureInterface.EdgeB.ToVector2() * 16f;
					if (!CaptureInterface.EdgeAPinned)
					{
						specialMode2 = 1;
						vector5 += Vector2.One * 8f;
						num6 = (-vector5 + Main.ReverseGravitySupport(new Vector2((float)Main.mouseX, (float)Main.mouseY), 0f) + Main.screenPosition).ToRotation();
						if (flag)
						{
							num6 = 0f - num6;
						}
						zero2 = Vector2.Clamp(vector5, vector, max);
						if (zero2 != vector5)
						{
							num6 = (vector5 - zero2).ToRotation();
						}
					}
					else
					{
						Vector2 vector6;
						vector6..ctor((float)((CaptureInterface.EdgeB.X >= CaptureInterface.EdgeA.X).ToInt() * 16), (float)((CaptureInterface.EdgeB.Y >= CaptureInterface.EdgeA.Y).ToInt() * 16));
						vector5 += vector6;
						zero2 = Vector2.Clamp(vector5, vector, max);
						num6 = (CaptureInterface.EdgeA.ToVector2() * 16f + new Vector2(16f) - vector6 - zero2).ToRotation();
						if (zero2 != vector5)
						{
							num6 = (vector5 - zero2).ToRotation();
							specialMode2 = 1;
						}
						if (flag)
						{
							num6 *= -1f;
						}
					}
					Utils.DrawCursorSingle(sb, color2, num6 - 1.5707964f, Main.cursorScale * num, Main.ReverseGravitySupport(zero2 - Main.screenPosition, 0f), 5, specialMode2);
					return;
				}
				Point result3 = CaptureInterface.EdgeB;
				if (CaptureInterface.EdgeAPinned)
				{
					int num7 = (CaptureInterface.EdgeB.X >= CaptureInterface.EdgeA.X).ToInt();
					int num8 = (CaptureInterface.EdgeB.Y >= CaptureInterface.EdgeA.Y).ToInt();
					result3.X += num7;
					result3.Y += num8;
					CaptureInterface.GetMapCoords(result3.X, result3.Y, 1, out result3);
					Point result4 = CaptureInterface.EdgeA;
					result4.X += 1 - num7;
					result4.Y += 1 - num8;
					CaptureInterface.GetMapCoords(result4.X, result4.Y, 1, out result4);
					zero2 = result3.ToVector2();
					zero2 = Vector2.Clamp(zero2, vector, max);
					num6 = (result4.ToVector2() - zero2).ToRotation();
				}
				else
				{
					CaptureInterface.GetMapCoords(result3.X, result3.Y, 1, out result3);
				}
				Utils.DrawCursorSingle(sb, color2, num6 - 1.5707964f, Main.cursorScale * num, result3.ToVector2(), 5, 0);
			}
		}

		// Token: 0x02000B8B RID: 2955
		private class ModeDragBounds : CaptureInterface.CaptureInterfaceMode
		{
			// Token: 0x06005D10 RID: 23824 RVA: 0x006C653C File Offset: 0x006C473C
			public override void Update()
			{
				if (this.Selected && !CaptureInterface.JustActivated)
				{
					PlayerInput.SetZoom_Context();
					Vector2 mouse;
					mouse..ctor((float)Main.mouseX, (float)Main.mouseY);
					this.DragBounds(mouse);
				}
			}

			// Token: 0x06005D11 RID: 23825 RVA: 0x006C6578 File Offset: 0x006C4778
			public override void Draw(SpriteBatch sb)
			{
				if (this.Selected)
				{
					sb.End();
					sb.Begin(0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.CurrentWantedZoomMatrix);
					PlayerInput.SetZoom_Context();
					this.DrawMarkedArea(sb);
					sb.End();
					sb.Begin(0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
					PlayerInput.SetZoom_UI();
				}
			}

			// Token: 0x06005D12 RID: 23826 RVA: 0x006C65EC File Offset: 0x006C47EC
			public override void ToggleActive(bool tickedOn)
			{
				if (!tickedOn)
				{
					this.currentAim = -1;
				}
			}

			// Token: 0x06005D13 RID: 23827 RVA: 0x006C65F8 File Offset: 0x006C47F8
			public override bool UsingMap()
			{
				return this.caughtEdge != -1;
			}

			// Token: 0x06005D14 RID: 23828 RVA: 0x006C6608 File Offset: 0x006C4808
			private void DragBounds(Vector2 mouse)
			{
				if (!CaptureInterface.EdgeAPinned || !CaptureInterface.EdgeBPinned)
				{
					bool flag = false;
					if (Main.mouseLeft)
					{
						flag = true;
					}
					if (flag)
					{
						bool flag2 = true;
						Point result;
						if (!Main.mapFullscreen)
						{
							result = (Main.screenPosition + mouse).ToTileCoordinates();
						}
						else
						{
							flag2 = CaptureInterface.GetMapCoords((int)mouse.X, (int)mouse.Y, 0, out result);
						}
						if (flag2)
						{
							if (!CaptureInterface.EdgeAPinned)
							{
								CaptureInterface.EdgeAPinned = true;
								CaptureInterface.EdgeA = result;
							}
							if (!CaptureInterface.EdgeBPinned)
							{
								CaptureInterface.EdgeBPinned = true;
								CaptureInterface.EdgeB = result;
							}
						}
						this.currentAim = 3;
						this.caughtEdge = 1;
					}
				}
				int num = Math.Min(CaptureInterface.EdgeA.X, CaptureInterface.EdgeB.X);
				int num2 = Math.Min(CaptureInterface.EdgeA.Y, CaptureInterface.EdgeB.Y);
				int num3 = Math.Abs(CaptureInterface.EdgeA.X - CaptureInterface.EdgeB.X);
				int num4 = Math.Abs(CaptureInterface.EdgeA.Y - CaptureInterface.EdgeB.Y);
				bool value = Main.player[Main.myPlayer].gravDir == -1f;
				int num5 = 1 - value.ToInt();
				int num6 = value.ToInt();
				Rectangle value2;
				Rectangle value3;
				if (!Main.mapFullscreen)
				{
					value2 = Main.ReverseGravitySupport(new Rectangle(num * 16, num2 * 16, (num3 + 1) * 16, (num4 + 1) * 16));
					value3 = Main.ReverseGravitySupport(new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth + 1, Main.screenHeight + 1));
					Rectangle result2;
					Rectangle.Intersect(ref value3, ref value2, ref result2);
					if (result2.Width == 0 || result2.Height == 0)
					{
						return;
					}
					result2.Offset(-value3.X, -value3.Y);
				}
				else
				{
					Point result3;
					CaptureInterface.GetMapCoords(num, num2, 1, out result3);
					Point result4;
					CaptureInterface.GetMapCoords(num + num3 + 1, num2 + num4 + 1, 1, out result4);
					value2..ctor(result3.X, result3.Y, result4.X - result3.X, result4.Y - result3.Y);
					value3..ctor(0, 0, Main.screenWidth + 1, Main.screenHeight + 1);
					Rectangle result2;
					Rectangle.Intersect(ref value3, ref value2, ref result2);
					if (result2.Width == 0 || result2.Height == 0)
					{
						return;
					}
					result2.Offset(-value3.X, -value3.Y);
				}
				this.dragging = false;
				if (!Main.mouseLeft)
				{
					this.currentAim = -1;
				}
				if (this.currentAim != -1)
				{
					this.dragging = true;
					Point point = default(Point);
					if (!Main.mapFullscreen)
					{
						point = Main.MouseWorld.ToTileCoordinates();
					}
					else
					{
						Point result5;
						if (!CaptureInterface.GetMapCoords((int)mouse.X, (int)mouse.Y, 0, out result5))
						{
							return;
						}
						point = result5;
					}
					int num7 = this.currentAim;
					if (num7 > 1)
					{
						if (num7 - 2 <= 1)
						{
							if (this.caughtEdge == 0)
							{
								CaptureInterface.EdgeA.X = point.X;
							}
							if (this.caughtEdge == 1)
							{
								CaptureInterface.EdgeB.X = point.X;
							}
						}
					}
					else
					{
						if (this.caughtEdge == 0)
						{
							CaptureInterface.EdgeA.Y = point.Y;
						}
						if (this.caughtEdge == 1)
						{
							CaptureInterface.EdgeB.Y = point.Y;
						}
					}
				}
				else
				{
					this.caughtEdge = -1;
					Rectangle drawbox = value2;
					drawbox.Offset(-value3.X, -value3.Y);
					this.inMap = drawbox.Contains(mouse.ToPoint());
					int i = 0;
					while (i < 4)
					{
						Rectangle bound = this.GetBound(drawbox, i);
						bound.Inflate(8, 8);
						if (bound.Contains(mouse.ToPoint()))
						{
							this.currentAim = i;
							switch (i)
							{
							case 0:
								if (CaptureInterface.EdgeA.Y < CaptureInterface.EdgeB.Y)
								{
									this.caughtEdge = num6;
									goto IL_48B;
								}
								this.caughtEdge = num5;
								goto IL_48B;
							case 1:
								if (CaptureInterface.EdgeA.Y >= CaptureInterface.EdgeB.Y)
								{
									this.caughtEdge = num6;
									goto IL_48B;
								}
								this.caughtEdge = num5;
								goto IL_48B;
							case 2:
								if (CaptureInterface.EdgeA.X < CaptureInterface.EdgeB.X)
								{
									this.caughtEdge = 0;
									goto IL_48B;
								}
								this.caughtEdge = 1;
								goto IL_48B;
							case 3:
								if (CaptureInterface.EdgeA.X >= CaptureInterface.EdgeB.X)
								{
									this.caughtEdge = 0;
									goto IL_48B;
								}
								this.caughtEdge = 1;
								goto IL_48B;
							default:
								goto IL_48B;
							}
						}
						else
						{
							i++;
						}
					}
				}
				IL_48B:
				CaptureInterface.ConstraintPoints();
			}

			// Token: 0x06005D15 RID: 23829 RVA: 0x006C6AA8 File Offset: 0x006C4CA8
			private Rectangle GetBound(Rectangle drawbox, int boundIndex)
			{
				switch (boundIndex)
				{
				case 0:
					return new Rectangle(drawbox.X, drawbox.Y - 2, drawbox.Width, 2);
				case 1:
					return new Rectangle(drawbox.X, drawbox.Y + drawbox.Height, drawbox.Width, 2);
				case 2:
					return new Rectangle(drawbox.X - 2, drawbox.Y, 2, drawbox.Height);
				case 3:
					return new Rectangle(drawbox.X + drawbox.Width, drawbox.Y, 2, drawbox.Height);
				default:
					return Rectangle.Empty;
				}
			}

			// Token: 0x06005D16 RID: 23830 RVA: 0x006C6B48 File Offset: 0x006C4D48
			public void DrawMarkedArea(SpriteBatch sb)
			{
				if (!CaptureInterface.EdgeAPinned || !CaptureInterface.EdgeBPinned)
				{
					return;
				}
				int num = Math.Min(CaptureInterface.EdgeA.X, CaptureInterface.EdgeB.X);
				int num2 = Math.Min(CaptureInterface.EdgeA.Y, CaptureInterface.EdgeB.Y);
				int num3 = Math.Abs(CaptureInterface.EdgeA.X - CaptureInterface.EdgeB.X);
				int num4 = Math.Abs(CaptureInterface.EdgeA.Y - CaptureInterface.EdgeB.Y);
				Rectangle result;
				if (!Main.mapFullscreen)
				{
					Rectangle value = Main.ReverseGravitySupport(new Rectangle(num * 16, num2 * 16, (num3 + 1) * 16, (num4 + 1) * 16));
					Rectangle value2 = Main.ReverseGravitySupport(new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth + 1, Main.screenHeight + 1));
					Rectangle.Intersect(ref value2, ref value, ref result);
					if (result.Width == 0 || result.Height == 0)
					{
						return;
					}
					result.Offset(-value2.X, -value2.Y);
				}
				else
				{
					Point result2;
					CaptureInterface.GetMapCoords(num, num2, 1, out result2);
					Point result3;
					CaptureInterface.GetMapCoords(num + num3 + 1, num2 + num4 + 1, 1, out result3);
					Rectangle value3;
					value3..ctor(result2.X, result2.Y, result3.X - result2.X, result3.Y - result2.Y);
					Rectangle value4;
					value4..ctor(0, 0, Main.screenWidth + 1, Main.screenHeight + 1);
					Rectangle.Intersect(ref value4, ref value3, ref result);
					if (result.Width == 0 || result.Height == 0)
					{
						return;
					}
					result.Offset(-value4.X, -value4.Y);
				}
				sb.Draw(TextureAssets.MagicPixel.Value, result, CaptureInterface.Settings.MarkedAreaColor);
				Rectangle rectangle = Rectangle.Empty;
				for (int i = 0; i < 2; i++)
				{
					if (this.currentAim != i)
					{
						this.DrawBound(sb, new Rectangle(result.X, result.Y + ((i == 1) ? result.Height : -2), result.Width, 2), 0);
					}
					else
					{
						rectangle..ctor(result.X, result.Y + ((i == 1) ? result.Height : -2), result.Width, 2);
					}
					if (this.currentAim != i + 2)
					{
						this.DrawBound(sb, new Rectangle(result.X + ((i == 1) ? result.Width : -2), result.Y, 2, result.Height), 0);
					}
					else
					{
						rectangle..ctor(result.X + ((i == 1) ? result.Width : -2), result.Y, 2, result.Height);
					}
				}
				if (rectangle != Rectangle.Empty)
				{
					this.DrawBound(sb, rectangle, 1 + this.dragging.ToInt());
				}
			}

			// Token: 0x06005D17 RID: 23831 RVA: 0x006C6E38 File Offset: 0x006C5038
			private void DrawBound(SpriteBatch sb, Rectangle r, int mode)
			{
				switch (mode)
				{
				case 0:
					sb.Draw(TextureAssets.MagicPixel.Value, r, Color.Silver);
					return;
				case 1:
				{
					Rectangle rectangle;
					rectangle..ctor(r.X - 2, r.Y, r.Width + 4, r.Height);
					sb.Draw(TextureAssets.MagicPixel.Value, rectangle, Color.White);
					rectangle..ctor(r.X, r.Y - 2, r.Width, r.Height + 4);
					sb.Draw(TextureAssets.MagicPixel.Value, rectangle, Color.White);
					sb.Draw(TextureAssets.MagicPixel.Value, r, Color.White);
					return;
				}
				case 2:
				{
					Rectangle rectangle;
					rectangle..ctor(r.X - 2, r.Y, r.Width + 4, r.Height);
					sb.Draw(TextureAssets.MagicPixel.Value, rectangle, Color.Gold);
					rectangle..ctor(r.X, r.Y - 2, r.Width, r.Height + 4);
					sb.Draw(TextureAssets.MagicPixel.Value, rectangle, Color.Gold);
					sb.Draw(TextureAssets.MagicPixel.Value, r, Color.Gold);
					return;
				}
				default:
					return;
				}
			}

			// Token: 0x04007650 RID: 30288
			public int currentAim = -1;

			// Token: 0x04007651 RID: 30289
			private bool dragging;

			// Token: 0x04007652 RID: 30290
			private int caughtEdge = -1;

			// Token: 0x04007653 RID: 30291
			private bool inMap;
		}

		// Token: 0x02000B8C RID: 2956
		private class ModeChangeSettings : CaptureInterface.CaptureInterfaceMode
		{
			// Token: 0x06005D19 RID: 23833 RVA: 0x006C6F98 File Offset: 0x006C5198
			private Rectangle GetRect()
			{
				Rectangle result;
				result..ctor(0, 0, 224, 170);
				if (CaptureInterface.Settings.ScreenAnchor == 0)
				{
					result.X = 227 - result.Width / 2;
					result.Y = 80;
				}
				return result;
			}

			// Token: 0x06005D1A RID: 23834 RVA: 0x006C6FE0 File Offset: 0x006C51E0
			private void ButtonDraw(int button, ref string key, ref string value)
			{
				switch (button)
				{
				case 0:
					key = Lang.inter[74].Value;
					value = Lang.inter[73 - CaptureInterface.Settings.PackImage.ToInt()].Value;
					return;
				case 1:
					key = Lang.inter[75].Value;
					value = Lang.inter[73 - CaptureInterface.Settings.IncludeEntities.ToInt()].Value;
					return;
				case 2:
					key = Lang.inter[76].Value;
					value = Lang.inter[73 - (!CaptureInterface.Settings.TransparentBackground).ToInt()].Value;
					return;
				case 3:
				case 4:
				case 5:
					break;
				case 6:
					key = "      " + Lang.menu[86].Value;
					value = "";
					break;
				default:
					return;
				}
			}

			// Token: 0x06005D1B RID: 23835 RVA: 0x006C70B4 File Offset: 0x006C52B4
			private void PressButton(int button)
			{
				bool flag = false;
				switch (button)
				{
				case 0:
					CaptureInterface.Settings.PackImage = !CaptureInterface.Settings.PackImage;
					flag = true;
					break;
				case 1:
					CaptureInterface.Settings.IncludeEntities = !CaptureInterface.Settings.IncludeEntities;
					flag = true;
					break;
				case 2:
					CaptureInterface.Settings.TransparentBackground = !CaptureInterface.Settings.TransparentBackground;
					flag = true;
					break;
				case 6:
					CaptureInterface.Settings.PackImage = true;
					CaptureInterface.Settings.IncludeEntities = true;
					CaptureInterface.Settings.TransparentBackground = false;
					CaptureInterface.Settings.BiomeChoiceIndex = -1;
					flag = true;
					break;
				}
				if (flag)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
			}

			// Token: 0x06005D1C RID: 23836 RVA: 0x006C714C File Offset: 0x006C534C
			private void DrawWaterChoices(SpriteBatch spritebatch, Point start, Point mouse)
			{
				Rectangle r;
				r..ctor(0, 0, 20, 20);
				for (int i = 0; i < 2; i++)
				{
					for (int j = 0; j < 7; j++)
					{
						if (i != 1 || j != 6)
						{
							int num5 = j + i * 7;
							r.X = start.X + 24 * j + 12 * i;
							r.Y = start.Y + 24 * i;
							int num2 = num5;
							int num3 = 0;
							if (r.Contains(mouse))
							{
								if (Main.mouseLeft && Main.mouseLeftRelease)
								{
									SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
									CaptureInterface.Settings.BiomeChoiceIndex = num2;
								}
								Main.instance.MouseText(Language.GetTextValue("CaptureBiomeChoice." + num2.ToString()), 0, 0, -1, -1, -1, -1, 0);
								num3++;
							}
							if (CaptureInterface.Settings.BiomeChoiceIndex == num2)
							{
								num3 += 2;
							}
							Texture2D value = TextureAssets.Extra[130].Value;
							int x = num5 * 18;
							Color white = Color.White;
							float num4 = 1f;
							if (num3 < 2)
							{
								num4 *= 0.5f;
							}
							if (num3 % 2 == 1)
							{
								spritebatch.Draw(TextureAssets.MagicPixel.Value, r.TopLeft(), new Rectangle?(new Rectangle(0, 0, 1, 1)), Color.Gold, 0f, Vector2.Zero, new Vector2(20f), 0, 0f);
							}
							else
							{
								spritebatch.Draw(TextureAssets.MagicPixel.Value, r.TopLeft(), new Rectangle?(new Rectangle(0, 0, 1, 1)), Color.White * num4, 0f, Vector2.Zero, new Vector2(20f), 0, 0f);
							}
							spritebatch.Draw(value, r.TopLeft() + new Vector2(2f), new Rectangle?(new Rectangle(x, 0, 16, 16)), Color.White * num4);
						}
					}
				}
			}

			// Token: 0x06005D1D RID: 23837 RVA: 0x006C733C File Offset: 0x006C553C
			private int UnnecessaryBiomeSelectionTypeConversion(int index)
			{
				switch (index)
				{
				case 0:
					return -1;
				case 1:
					return 0;
				case 2:
					return 2;
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
					return index;
				case 9:
					return 10;
				case 10:
					return 12;
				case 11:
					return 13;
				case 12:
					return 14;
				default:
					return 0;
				}
			}

			// Token: 0x06005D1E RID: 23838 RVA: 0x006C739C File Offset: 0x006C559C
			public override void Update()
			{
				if (!this.Selected || CaptureInterface.JustActivated)
				{
					return;
				}
				PlayerInput.SetZoom_UI();
				Point value;
				value..ctor(Main.mouseX, Main.mouseY);
				this.hoveredButton = -1;
				Rectangle rect = this.GetRect();
				this.inUI = rect.Contains(value);
				rect.Inflate(-20, -20);
				rect.Height = 16;
				int y = rect.Y;
				for (int i = 0; i < 7; i++)
				{
					rect.Y = y + i * 20;
					if (rect.Contains(value))
					{
						this.hoveredButton = i;
						break;
					}
				}
				if (Main.mouseLeft && Main.mouseLeftRelease && this.hoveredButton != -1)
				{
					this.PressButton(this.hoveredButton);
				}
			}

			// Token: 0x06005D1F RID: 23839 RVA: 0x006C7458 File Offset: 0x006C5658
			public override void Draw(SpriteBatch sb)
			{
				if (!this.Selected)
				{
					return;
				}
				sb.End();
				sb.Begin(0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.CurrentWantedZoomMatrix);
				PlayerInput.SetZoom_Context();
				((CaptureInterface.ModeDragBounds)CaptureInterface.Modes[1]).currentAim = -1;
				((CaptureInterface.ModeDragBounds)CaptureInterface.Modes[1]).DrawMarkedArea(sb);
				sb.End();
				sb.Begin(0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
				PlayerInput.SetZoom_UI();
				Rectangle rect = this.GetRect();
				Utils.DrawInvBG(sb, rect, new Color(63, 65, 151, 255) * 0.485f);
				for (int i = 0; i < 7; i++)
				{
					string key = "";
					string value = "";
					this.ButtonDraw(i, ref key, ref value);
					Color baseColor = Color.White;
					if (i == this.hoveredButton)
					{
						baseColor = Color.Gold;
					}
					ChatManager.DrawColorCodedStringWithShadow(sb, FontAssets.ItemStack.Value, key, rect.TopLeft() + new Vector2(20f, (float)(20 + 20 * i)), baseColor, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
					ChatManager.DrawColorCodedStringWithShadow(sb, FontAssets.ItemStack.Value, value, rect.TopRight() + new Vector2(-20f, (float)(20 + 20 * i)), baseColor, 0f, FontAssets.ItemStack.Value.MeasureString(value) * Vector2.UnitX, Vector2.One, -1f, 2f);
				}
				this.DrawWaterChoices(sb, (rect.TopLeft() + new Vector2((float)(rect.Width / 2 - 84), 90f)).ToPoint(), Main.MouseScreen.ToPoint());
			}

			// Token: 0x06005D20 RID: 23840 RVA: 0x006C763D File Offset: 0x006C583D
			public override void ToggleActive(bool tickedOn)
			{
				if (tickedOn)
				{
					this.hoveredButton = -1;
				}
			}

			// Token: 0x06005D21 RID: 23841 RVA: 0x006C7649 File Offset: 0x006C5849
			public override bool UsingMap()
			{
				return this.inUI;
			}

			// Token: 0x04007654 RID: 30292
			private const int ButtonsCount = 7;

			// Token: 0x04007655 RID: 30293
			private int hoveredButton = -1;

			// Token: 0x04007656 RID: 30294
			private bool inUI;
		}
	}
}
