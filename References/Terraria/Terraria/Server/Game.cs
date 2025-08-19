using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Server
{
	// Token: 0x0200007A RID: 122
	public class Game : IDisposable
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public GameComponentCollection Components
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x0006A9EF File Offset: 0x00068BEF
		// (set) Token: 0x06001192 RID: 4498 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public ContentManager Content
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public GraphicsDevice GraphicsDevice
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x0048E5EF File Offset: 0x0048C7EF
		// (set) Token: 0x06001195 RID: 4501 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public TimeSpan InactiveSleepTime
		{
			get
			{
				return TimeSpan.Zero;
			}
			set
			{
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x0003266D File Offset: 0x0003086D
		public bool IsActive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x0003266D File Offset: 0x0003086D
		// (set) Token: 0x06001198 RID: 4504 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public bool IsFixedTimeStep
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		// (set) Token: 0x0600119A RID: 4506 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public bool IsMouseVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public LaunchParameters LaunchParameters
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public GameServiceContainer Services
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0048E5EF File Offset: 0x0048C7EF
		// (set) Token: 0x0600119E RID: 4510 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public TimeSpan TargetElapsedTime
		{
			get
			{
				return TimeSpan.Zero;
			}
			set
			{
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public GameWindow Window
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060011A0 RID: 4512 RVA: 0x0048E5FC File Offset: 0x0048C7FC
		// (remove) Token: 0x060011A1 RID: 4513 RVA: 0x0048E634 File Offset: 0x0048C834
		public event EventHandler<EventArgs> Activated;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060011A2 RID: 4514 RVA: 0x0048E66C File Offset: 0x0048C86C
		// (remove) Token: 0x060011A3 RID: 4515 RVA: 0x0048E6A4 File Offset: 0x0048C8A4
		public event EventHandler<EventArgs> Deactivated;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060011A4 RID: 4516 RVA: 0x0048E6DC File Offset: 0x0048C8DC
		// (remove) Token: 0x060011A5 RID: 4517 RVA: 0x0048E714 File Offset: 0x0048C914
		public event EventHandler<EventArgs> Disposed;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060011A6 RID: 4518 RVA: 0x0048E74C File Offset: 0x0048C94C
		// (remove) Token: 0x060011A7 RID: 4519 RVA: 0x0048E784 File Offset: 0x0048C984
		public event EventHandler<EventArgs> Exiting;

		// Token: 0x060011A8 RID: 4520 RVA: 0x0003266D File Offset: 0x0003086D
		protected virtual bool BeginDraw()
		{
			return true;
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		protected virtual void BeginRun()
		{
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void Dispose()
		{
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		protected virtual void Draw(GameTime gameTime)
		{
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		protected virtual void EndDraw()
		{
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		protected virtual void EndRun()
		{
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void Exit()
		{
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		protected virtual void Initialize()
		{
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		protected virtual void LoadContent()
		{
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		protected virtual void OnActivated(object sender, EventArgs args)
		{
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		protected virtual void OnDeactivated(object sender, EventArgs args)
		{
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		protected virtual void OnExiting(object sender, EventArgs args)
		{
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void ResetElapsedTime()
		{
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void Run()
		{
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void RunOneFrame()
		{
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0003266D File Offset: 0x0003086D
		protected virtual bool ShowMissingRequirementMessage(Exception exception)
		{
			return true;
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void SuppressDraw()
		{
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void Tick()
		{
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		protected virtual void UnloadContent()
		{
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		protected virtual void Update(GameTime gameTime)
		{
		}
	}
}
