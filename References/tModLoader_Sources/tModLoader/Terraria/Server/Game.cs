using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Server
{
	// Token: 0x02000112 RID: 274
	public class Game : IDisposable
	{
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x004BEDA6 File Offset: 0x004BCFA6
		public GameComponentCollection Components
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x004BEDA9 File Offset: 0x004BCFA9
		// (set) Token: 0x06001946 RID: 6470 RVA: 0x004BEDAC File Offset: 0x004BCFAC
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

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x004BEDAE File Offset: 0x004BCFAE
		public GraphicsDevice GraphicsDevice
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06001948 RID: 6472 RVA: 0x004BEDB1 File Offset: 0x004BCFB1
		// (set) Token: 0x06001949 RID: 6473 RVA: 0x004BEDB8 File Offset: 0x004BCFB8
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

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x004BEDBA File Offset: 0x004BCFBA
		public bool IsActive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x004BEDBD File Offset: 0x004BCFBD
		// (set) Token: 0x0600194C RID: 6476 RVA: 0x004BEDC0 File Offset: 0x004BCFC0
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

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x004BEDC2 File Offset: 0x004BCFC2
		// (set) Token: 0x0600194E RID: 6478 RVA: 0x004BEDC5 File Offset: 0x004BCFC5
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

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x004BEDC7 File Offset: 0x004BCFC7
		public LaunchParameters LaunchParameters
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x004BEDCA File Offset: 0x004BCFCA
		public GameServiceContainer Services
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x004BEDCD File Offset: 0x004BCFCD
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x004BEDD4 File Offset: 0x004BCFD4
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

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x004BEDD6 File Offset: 0x004BCFD6
		public GameWindow Window
		{
			get
			{
				return null;
			}
		}

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06001954 RID: 6484 RVA: 0x004BEDDC File Offset: 0x004BCFDC
		// (remove) Token: 0x06001955 RID: 6485 RVA: 0x004BEE14 File Offset: 0x004BD014
		public event EventHandler<EventArgs> Activated;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06001956 RID: 6486 RVA: 0x004BEE4C File Offset: 0x004BD04C
		// (remove) Token: 0x06001957 RID: 6487 RVA: 0x004BEE84 File Offset: 0x004BD084
		public event EventHandler<EventArgs> Deactivated;

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06001958 RID: 6488 RVA: 0x004BEEBC File Offset: 0x004BD0BC
		// (remove) Token: 0x06001959 RID: 6489 RVA: 0x004BEEF4 File Offset: 0x004BD0F4
		public event EventHandler<EventArgs> Disposed;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x0600195A RID: 6490 RVA: 0x004BEF2C File Offset: 0x004BD12C
		// (remove) Token: 0x0600195B RID: 6491 RVA: 0x004BEF64 File Offset: 0x004BD164
		public event EventHandler<EventArgs> Exiting;

		// Token: 0x0600195C RID: 6492 RVA: 0x004BEF99 File Offset: 0x004BD199
		protected virtual bool BeginDraw()
		{
			return true;
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x004BEF9C File Offset: 0x004BD19C
		protected virtual void BeginRun()
		{
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x004BEF9E File Offset: 0x004BD19E
		public void Dispose()
		{
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x004BEFA0 File Offset: 0x004BD1A0
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x004BEFA2 File Offset: 0x004BD1A2
		protected virtual void Draw(GameTime gameTime)
		{
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x004BEFA4 File Offset: 0x004BD1A4
		protected virtual void EndDraw()
		{
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x004BEFA6 File Offset: 0x004BD1A6
		protected virtual void EndRun()
		{
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x004BEFA8 File Offset: 0x004BD1A8
		public void Exit()
		{
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x004BEFAA File Offset: 0x004BD1AA
		protected virtual void Initialize()
		{
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x004BEFAC File Offset: 0x004BD1AC
		protected virtual void LoadContent()
		{
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x004BEFAE File Offset: 0x004BD1AE
		protected virtual void OnActivated(object sender, EventArgs args)
		{
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x004BEFB0 File Offset: 0x004BD1B0
		protected virtual void OnDeactivated(object sender, EventArgs args)
		{
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x004BEFB2 File Offset: 0x004BD1B2
		protected virtual void OnExiting(object sender, EventArgs args)
		{
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x004BEFB4 File Offset: 0x004BD1B4
		public void ResetElapsedTime()
		{
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x004BEFB6 File Offset: 0x004BD1B6
		public void Run()
		{
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x004BEFB8 File Offset: 0x004BD1B8
		public void RunOneFrame()
		{
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x004BEFBA File Offset: 0x004BD1BA
		protected virtual bool ShowMissingRequirementMessage(Exception exception)
		{
			return true;
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x004BEFBD File Offset: 0x004BD1BD
		public void SuppressDraw()
		{
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x004BEFBF File Offset: 0x004BD1BF
		public void Tick()
		{
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x004BEFC1 File Offset: 0x004BD1C1
		protected virtual void UnloadContent()
		{
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x004BEFC3 File Offset: 0x004BD1C3
		protected virtual void Update(GameTime gameTime)
		{
		}
	}
}
