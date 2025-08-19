using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameInput;

namespace Terraria.GameContent.UI
{
	// Token: 0x020004D1 RID: 1233
	public class WiresUI
	{
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06003AC8 RID: 15048 RVA: 0x005AD00D File Offset: 0x005AB20D
		public static bool Open
		{
			get
			{
				return WiresUI.radial.active;
			}
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x005AD019 File Offset: 0x005AB219
		public static void HandleWiresUI(SpriteBatch spriteBatch)
		{
			WiresUI.radial.Update();
			WiresUI.radial.Draw(spriteBatch);
		}

		// Token: 0x040054C3 RID: 21699
		private static WiresUI.WiresRadial radial = new WiresUI.WiresRadial();

		// Token: 0x02000BD5 RID: 3029
		public static class Settings
		{
			// Token: 0x17000954 RID: 2388
			// (get) Token: 0x06005DF5 RID: 24053 RVA: 0x006CA0C4 File Offset: 0x006C82C4
			public static bool DrawWires
			{
				get
				{
					return (!Main.getGoodWorld || NPC.downedBoss3) && (Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].mech || (Main.player[Main.myPlayer].InfoAccMechShowWires && Main.player[Main.myPlayer].builderAccStatus[8] == 0));
				}
			}

			// Token: 0x17000955 RID: 2389
			// (get) Token: 0x06005DF6 RID: 24054 RVA: 0x006CA134 File Offset: 0x006C8334
			public static bool HideWires
			{
				get
				{
					return Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type == 3620;
				}
			}

			// Token: 0x17000956 RID: 2390
			// (get) Token: 0x06005DF7 RID: 24055 RVA: 0x006CA164 File Offset: 0x006C8364
			public static bool DrawToolModeUI
			{
				get
				{
					int type = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type;
					return type == 3611 || type == 3625;
				}
			}

			// Token: 0x17000957 RID: 2391
			// (get) Token: 0x06005DF8 RID: 24056 RVA: 0x006CA1AC File Offset: 0x006C83AC
			public static bool DrawToolAllowActuators
			{
				get
				{
					int type = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type;
					if (type == 3611)
					{
						WiresUI.Settings._lastActuatorEnabled = 2;
					}
					if (type == 3625)
					{
						WiresUI.Settings._lastActuatorEnabled = 1;
					}
					return WiresUI.Settings._lastActuatorEnabled == 2;
				}
			}

			// Token: 0x04007752 RID: 30546
			public static WiresUI.Settings.MultiToolMode ToolMode = WiresUI.Settings.MultiToolMode.Red;

			// Token: 0x04007753 RID: 30547
			private static int _lastActuatorEnabled;

			// Token: 0x02000E50 RID: 3664
			[Flags]
			public enum MultiToolMode
			{
				// Token: 0x04007D43 RID: 32067
				Red = 1,
				// Token: 0x04007D44 RID: 32068
				Green = 2,
				// Token: 0x04007D45 RID: 32069
				Blue = 4,
				// Token: 0x04007D46 RID: 32070
				Yellow = 8,
				// Token: 0x04007D47 RID: 32071
				Actuator = 16,
				// Token: 0x04007D48 RID: 32072
				Cutter = 32
			}
		}

		// Token: 0x02000BD6 RID: 3030
		public class WiresRadial
		{
			// Token: 0x06005DFA RID: 24058 RVA: 0x006CA20A File Offset: 0x006C840A
			public void Update()
			{
				this.FlowerUpdate();
				this.LineUpdate();
			}

			// Token: 0x06005DFB RID: 24059 RVA: 0x006CA218 File Offset: 0x006C8418
			private void LineUpdate()
			{
				bool value = true;
				float min = 0.75f;
				Player player = Main.player[Main.myPlayer];
				if (!WiresUI.Settings.DrawToolModeUI || Main.drawingPlayerChat)
				{
					value = false;
					min = 0f;
				}
				if (player.dead || Main.mouseItem.type > 0)
				{
					this._lineOpacity = 0f;
					return;
				}
				if (player.cursorItemIconEnabled && player.cursorItemIconID != 0 && player.cursorItemIconID != 3625)
				{
					this._lineOpacity = 0f;
					return;
				}
				if ((!player.cursorItemIconEnabled && ((!PlayerInput.UsingGamepad && !WiresUI.Settings.DrawToolAllowActuators) || player.mouseInterface || player.lastMouseInterface)) || Main.ingameOptionsWindow || Main.InGameUI.IsVisible)
				{
					this._lineOpacity = 0f;
					return;
				}
				float num = Utils.Clamp<float>(this._lineOpacity + 0.05f * (float)value.ToDirectionInt(), min, 1f);
				this._lineOpacity += 0.05f * (float)Math.Sign(num - this._lineOpacity);
				if (Math.Abs(this._lineOpacity - num) < 0.05f)
				{
					this._lineOpacity = num;
				}
			}

			// Token: 0x06005DFC RID: 24060 RVA: 0x006CA340 File Offset: 0x006C8540
			private void FlowerUpdate()
			{
				Player player = Main.player[Main.myPlayer];
				if (!WiresUI.Settings.DrawToolModeUI)
				{
					this.active = false;
					return;
				}
				if ((player.mouseInterface || player.lastMouseInterface) && !this.OnWiresMenu)
				{
					this.active = false;
					return;
				}
				if (player.dead || Main.mouseItem.type > 0)
				{
					this.active = false;
					this.OnWiresMenu = false;
					return;
				}
				this.OnWiresMenu = false;
				if (!Main.mouseRight || !Main.mouseRightRelease || PlayerInput.LockGamepadTileUseButton || player.noThrow != 0 || Main.HoveringOverAnNPC || player.talkNPC != -1)
				{
					return;
				}
				if (this.active)
				{
					this.active = false;
					return;
				}
				if (!Main.SmartInteractShowingGenuine)
				{
					this.active = true;
					this.position = Main.MouseScreen;
					if (PlayerInput.UsingGamepad && Main.SmartCursorWanted)
					{
						this.position = new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / 2f;
					}
				}
			}

			// Token: 0x06005DFD RID: 24061 RVA: 0x006CA439 File Offset: 0x006C8639
			public void Draw(SpriteBatch spriteBatch)
			{
				this.DrawFlower(spriteBatch);
				this.DrawCursorArea(spriteBatch);
			}

			// Token: 0x06005DFE RID: 24062 RVA: 0x006CA44C File Offset: 0x006C864C
			private void DrawLine(SpriteBatch spriteBatch)
			{
				if (this.active || this._lineOpacity == 0f)
				{
					return;
				}
				Vector2 vector = Main.MouseScreen;
				Vector2 vector2;
				vector2..ctor((float)(Main.screenWidth / 2), (float)(Main.screenHeight - 70));
				if (PlayerInput.UsingGamepad)
				{
					vector = Vector2.Zero;
				}
				Vector2 vector3 = vector - vector2;
				Vector2.Dot(Vector2.Normalize(vector3), Vector2.UnitX);
				Vector2.Dot(Vector2.Normalize(vector3), Vector2.UnitY);
				vector3.ToRotation();
				vector3.Length();
				bool flag = false;
				bool drawToolAllowActuators = WiresUI.Settings.DrawToolAllowActuators;
				for (int i = 0; i < 6; i++)
				{
					if (drawToolAllowActuators || i != 5)
					{
						bool flag2 = WiresUI.Settings.ToolMode.HasFlag((WiresUI.Settings.MultiToolMode)(1 << i));
						if (i == 5)
						{
							flag2 = WiresUI.Settings.ToolMode.HasFlag(WiresUI.Settings.MultiToolMode.Actuator);
						}
						Vector2 vector4 = vector2 + Vector2.UnitX * (45f * ((float)i - 1.5f));
						int num = i ?? 3;
						if (i == 3)
						{
							num = 0;
						}
						switch (num)
						{
						case 0:
						case 1:
							vector4 = vector2 + new Vector2((45f + (float)(drawToolAllowActuators ? 15 : 0)) * (float)(2 - num), 0f) * this._lineOpacity;
							break;
						case 2:
						case 3:
							vector4 = vector2 + new Vector2((0f - (45f + (float)(drawToolAllowActuators ? 15 : 0))) * (float)(num - 1), 0f) * this._lineOpacity;
							break;
						case 4:
							flag2 = false;
							vector4 = vector2 - new Vector2(0f, drawToolAllowActuators ? 22f : 0f) * this._lineOpacity;
							break;
						case 5:
							vector4 = vector2 + new Vector2(0f, 22f) * this._lineOpacity;
							break;
						}
						bool flag3 = false;
						if (!PlayerInput.UsingGamepad)
						{
							flag3 = (Vector2.Distance(vector4, vector) < 19f * this._lineOpacity);
						}
						if (flag)
						{
							flag3 = false;
						}
						if (flag3)
						{
							flag = true;
						}
						Texture2D value = TextureAssets.WireUi[(WiresUI.Settings.ToolMode.HasFlag(WiresUI.Settings.MultiToolMode.Cutter) ? 8 : 0) + ((flag3 > false) ? 1 : 0)].Value;
						Texture2D texture2D = null;
						switch (i)
						{
						case 0:
						case 1:
						case 2:
						case 3:
							texture2D = TextureAssets.WireUi[2 + i].Value;
							break;
						case 4:
							texture2D = TextureAssets.WireUi[WiresUI.Settings.ToolMode.HasFlag(WiresUI.Settings.MultiToolMode.Cutter) ? 7 : 6].Value;
							break;
						case 5:
							texture2D = TextureAssets.WireUi[10].Value;
							break;
						}
						Color color = Color.White;
						Color color2 = Color.White;
						if (!flag2 && i != 4)
						{
							if (flag3)
							{
								color2..ctor(100, 100, 100);
								color2..ctor(120, 120, 120);
								color..ctor(200, 200, 200);
							}
							else
							{
								color2..ctor(150, 150, 150);
								color2..ctor(80, 80, 80);
								color..ctor(100, 100, 100);
							}
						}
						Utils.CenteredRectangle(vector4, new Vector2(40f));
						if (flag3)
						{
							if (Main.mouseLeft && Main.mouseLeftRelease)
							{
								switch (i)
								{
								case 0:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Red;
									break;
								case 1:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Green;
									break;
								case 2:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Blue;
									break;
								case 3:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Yellow;
									break;
								case 4:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Cutter;
									break;
								case 5:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Actuator;
									break;
								}
							}
							if (!Main.mouseLeft || Main.player[Main.myPlayer].mouseInterface)
							{
								Main.player[Main.myPlayer].mouseInterface = true;
							}
							this.OnWiresMenu = true;
						}
						spriteBatch.Draw(value, vector4, null, color * this._lineOpacity, 0f, value.Size() / 2f, this._lineOpacity, 0, 0f);
						spriteBatch.Draw(texture2D, vector4, null, color2 * this._lineOpacity, 0f, texture2D.Size() / 2f, this._lineOpacity, 0, 0f);
					}
				}
				if (Main.mouseLeft && Main.mouseLeftRelease && !flag)
				{
					this.active = false;
				}
			}

			// Token: 0x06005DFF RID: 24063 RVA: 0x006CA910 File Offset: 0x006C8B10
			private void DrawFlower(SpriteBatch spriteBatch)
			{
				if (!this.active)
				{
					return;
				}
				Vector2 vector = Main.MouseScreen;
				Vector2 vector2 = this.position;
				if (PlayerInput.UsingGamepad && Main.SmartCursorWanted)
				{
					vector = ((PlayerInput.GamepadThumbstickRight != Vector2.Zero) ? (this.position + PlayerInput.GamepadThumbstickRight * 40f) : ((!(PlayerInput.GamepadThumbstickLeft != Vector2.Zero)) ? this.position : (this.position + PlayerInput.GamepadThumbstickLeft * 40f)));
				}
				Vector2 vector3 = vector - vector2;
				Vector2.Dot(Vector2.Normalize(vector3), Vector2.UnitX);
				Vector2.Dot(Vector2.Normalize(vector3), Vector2.UnitY);
				float num = vector3.ToRotation();
				float num2 = vector3.Length();
				bool flag = false;
				bool drawToolAllowActuators = WiresUI.Settings.DrawToolAllowActuators;
				float num3 = (float)(4 + drawToolAllowActuators.ToInt());
				float num4 = drawToolAllowActuators ? 11f : -0.5f;
				for (int i = 0; i < 6; i++)
				{
					if (drawToolAllowActuators || i != 5)
					{
						bool flag2 = WiresUI.Settings.ToolMode.HasFlag((WiresUI.Settings.MultiToolMode)(1 << i));
						if (i == 5)
						{
							flag2 = WiresUI.Settings.ToolMode.HasFlag(WiresUI.Settings.MultiToolMode.Actuator);
						}
						Vector2 vector4 = vector2 + Vector2.UnitX * (45f * ((float)i - 1.5f));
						switch (i)
						{
						case 0:
						case 1:
						case 2:
						case 3:
						{
							float num5 = (float)i;
							if (i == 0)
							{
								num5 = 3f;
							}
							if (i == 3)
							{
								num5 = 0f;
							}
							vector4 = vector2 + Vector2.UnitX.RotatedBy((double)(num5 * 6.2831855f / num3 - 3.1415927f / num4), default(Vector2)) * 45f;
							break;
						}
						case 4:
							flag2 = false;
							vector4 = vector2;
							break;
						case 5:
							vector4 = vector2 + Vector2.UnitX.RotatedBy((double)((float)(i - 1) * 6.2831855f / num3 - 3.1415927f / num4), default(Vector2)) * 45f;
							break;
						}
						bool flag3 = false;
						if (i == 4)
						{
							flag3 = (num2 < 20f);
						}
						switch (i)
						{
						case 0:
						case 1:
						case 2:
						case 3:
						case 5:
						{
							float value = (vector4 - vector2).ToRotation().AngleTowards(num, 6.2831855f / (num3 * 2f)) - num;
							if (num2 >= 20f && Math.Abs(value) < 0.01f)
							{
								flag3 = true;
							}
							break;
						}
						case 4:
							flag3 = (num2 < 20f);
							break;
						}
						if (!PlayerInput.UsingGamepad)
						{
							flag3 = (Vector2.Distance(vector4, vector) < 19f);
						}
						if (flag)
						{
							flag3 = false;
						}
						if (flag3)
						{
							flag = true;
						}
						Texture2D value2 = TextureAssets.WireUi[(WiresUI.Settings.ToolMode.HasFlag(WiresUI.Settings.MultiToolMode.Cutter) ? 8 : 0) + ((flag3 > false) ? 1 : 0)].Value;
						Texture2D texture2D = null;
						switch (i)
						{
						case 0:
						case 1:
						case 2:
						case 3:
							texture2D = TextureAssets.WireUi[2 + i].Value;
							break;
						case 4:
							texture2D = TextureAssets.WireUi[WiresUI.Settings.ToolMode.HasFlag(WiresUI.Settings.MultiToolMode.Cutter) ? 7 : 6].Value;
							break;
						case 5:
							texture2D = TextureAssets.WireUi[10].Value;
							break;
						}
						Color color = Color.White;
						Color color2 = Color.White;
						if (!flag2 && i != 4)
						{
							if (flag3)
							{
								color2..ctor(100, 100, 100);
								color2..ctor(120, 120, 120);
								color..ctor(200, 200, 200);
							}
							else
							{
								color2..ctor(150, 150, 150);
								color2..ctor(80, 80, 80);
								color..ctor(100, 100, 100);
							}
						}
						Utils.CenteredRectangle(vector4, new Vector2(40f));
						if (flag3)
						{
							if (Main.mouseLeft && Main.mouseLeftRelease)
							{
								switch (i)
								{
								case 0:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Red;
									break;
								case 1:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Green;
									break;
								case 2:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Blue;
									break;
								case 3:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Yellow;
									break;
								case 4:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Cutter;
									break;
								case 5:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Actuator;
									break;
								}
							}
							Main.player[Main.myPlayer].mouseInterface = true;
							this.OnWiresMenu = true;
						}
						spriteBatch.Draw(value2, vector4, null, color, 0f, value2.Size() / 2f, 1f, 0, 0f);
						spriteBatch.Draw(texture2D, vector4, null, color2, 0f, texture2D.Size() / 2f, 1f, 0, 0f);
					}
				}
				if (Main.mouseLeft && Main.mouseLeftRelease && !flag)
				{
					this.active = false;
				}
			}

			// Token: 0x06005E00 RID: 24064 RVA: 0x006CAE44 File Offset: 0x006C9044
			private void DrawCursorArea(SpriteBatch spriteBatch)
			{
				if (this.active || this._lineOpacity == 0f)
				{
					return;
				}
				Vector2 vector = Main.MouseScreen + new Vector2((float)(10 - 9 * PlayerInput.UsingGamepad.ToInt()), 25f);
				Color color;
				color..ctor(50, 50, 50);
				bool drawToolAllowActuators = WiresUI.Settings.DrawToolAllowActuators;
				if (!drawToolAllowActuators)
				{
					if (!PlayerInput.UsingGamepad)
					{
						vector += new Vector2(-20f, 10f);
					}
					else
					{
						vector += new Vector2(0f, 10f);
					}
				}
				Texture2D value = TextureAssets.BuilderAcc.Value;
				Texture2D texture = value;
				Rectangle rectangle;
				rectangle..ctor(140, 2, 6, 6);
				Rectangle rectangle2;
				rectangle2..ctor(148, 2, 6, 6);
				Rectangle rectangle3;
				rectangle3..ctor(128, 0, 10, 10);
				float num = 1f;
				float scale = 1f;
				bool flag = false;
				if (flag && !drawToolAllowActuators)
				{
					num *= Main.cursorScale;
				}
				float num2 = this._lineOpacity;
				if (PlayerInput.UsingGamepad)
				{
					num2 *= Main.GamepadCursorAlpha;
				}
				for (int i = 0; i < 5; i++)
				{
					if (drawToolAllowActuators || i != 4)
					{
						float num3 = num2;
						Vector2 vec = vector + Vector2.UnitX * (45f * ((float)i - 1.5f));
						int num4 = i ?? 3;
						if (i == 1)
						{
							num4 = 2;
						}
						if (i == 2)
						{
							num4 = 1;
						}
						if (i == 3)
						{
							num4 = 0;
						}
						if (i == 4)
						{
							num4 = 5;
						}
						int num5 = num4;
						if (num5 != 1)
						{
							if (num5 == 2)
							{
								num5 = 1;
							}
						}
						else
						{
							num5 = 2;
						}
						bool flag2 = WiresUI.Settings.ToolMode.HasFlag((WiresUI.Settings.MultiToolMode)(1 << num5));
						if (num5 == 5)
						{
							flag2 = WiresUI.Settings.ToolMode.HasFlag(WiresUI.Settings.MultiToolMode.Actuator);
						}
						Color color2 = Color.HotPink;
						switch (num4)
						{
						case 0:
							color2..ctor(253, 58, 61);
							break;
						case 1:
							color2..ctor(83, 180, 253);
							break;
						case 2:
							color2..ctor(83, 253, 153);
							break;
						case 3:
							color2..ctor(253, 254, 83);
							break;
						case 5:
							color2 = Color.WhiteSmoke;
							break;
						}
						if (!flag2)
						{
							color2 = Color.Lerp(color2, Color.Black, 0.65f);
						}
						if (flag)
						{
							if (drawToolAllowActuators)
							{
								switch (num4)
								{
								case 0:
									vec = vector + new Vector2(-12f, 0f) * num;
									break;
								case 1:
									vec = vector + new Vector2(-6f, 12f) * num;
									break;
								case 2:
									vec = vector + new Vector2(6f, 12f) * num;
									break;
								case 3:
									vec = vector + new Vector2(12f, 0f) * num;
									break;
								case 5:
									vec = vector + new Vector2(0f, 0f) * num;
									break;
								}
							}
							else
							{
								vec = vector + new Vector2((float)(12 * (num4 + 1)), (float)(12 * (3 - num4))) * num;
							}
						}
						else if (drawToolAllowActuators)
						{
							switch (num4)
							{
							case 0:
								vec = vector + new Vector2(-12f, 0f) * num;
								break;
							case 1:
								vec = vector + new Vector2(-6f, 12f) * num;
								break;
							case 2:
								vec = vector + new Vector2(6f, 12f) * num;
								break;
							case 3:
								vec = vector + new Vector2(12f, 0f) * num;
								break;
							case 5:
								vec = vector + new Vector2(0f, 0f) * num;
								break;
							}
						}
						else
						{
							float num6 = 0.7f;
							switch (num4)
							{
							case 0:
								vec = vector + new Vector2(0f, -12f) * num * num6;
								break;
							case 1:
								vec = vector + new Vector2(-12f, 0f) * num * num6;
								break;
							case 2:
								vec = vector + new Vector2(0f, 12f) * num * num6;
								break;
							case 3:
								vec = vector + new Vector2(12f, 0f) * num * num6;
								break;
							}
						}
						vec = vec.Floor();
						spriteBatch.Draw(texture, vec, new Rectangle?(rectangle3), color * num3, 0f, rectangle3.Size() / 2f, scale, 0, 0f);
						spriteBatch.Draw(value, vec, new Rectangle?(rectangle), color2 * num3, 0f, rectangle.Size() / 2f, scale, 0, 0f);
						if (WiresUI.Settings.ToolMode.HasFlag(WiresUI.Settings.MultiToolMode.Cutter))
						{
							spriteBatch.Draw(value, vec, new Rectangle?(rectangle2), color * num3, 0f, rectangle2.Size() / 2f, scale, 0, 0f);
						}
					}
				}
			}

			// Token: 0x04007754 RID: 30548
			public Vector2 position;

			// Token: 0x04007755 RID: 30549
			public bool active;

			// Token: 0x04007756 RID: 30550
			public bool OnWiresMenu;

			// Token: 0x04007757 RID: 30551
			private float _lineOpacity;
		}
	}
}
