using System;
using System.Collections;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class represents a DrawLayer for the player. Drawing should be done by adding Terraria.DataStructures.DrawData objects to drawInfo.DrawDataCache in the Draw method.
	/// </summary>
	// Token: 0x020001E6 RID: 486
	[Autoload(true)]
	public abstract class PlayerDrawLayer : ModType
	{
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x004F68E8 File Offset: 0x004F4AE8
		// (set) Token: 0x060025DA RID: 9690 RVA: 0x004F68F0 File Offset: 0x004F4AF0
		public bool Visible { get; private set; } = true;

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060025DB RID: 9691 RVA: 0x004F68F9 File Offset: 0x004F4AF9
		public virtual PlayerDrawLayer.Transformation Transform { get; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060025DC RID: 9692 RVA: 0x004F6901 File Offset: 0x004F4B01
		public IReadOnlyList<PlayerDrawLayer> ChildrenBefore
		{
			get
			{
				return this._childrenBefore;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060025DD RID: 9693 RVA: 0x004F6909 File Offset: 0x004F4B09
		public IReadOnlyList<PlayerDrawLayer> ChildrenAfter
		{
			get
			{
				return this._childrenAfter;
			}
		}

		/// <summary> Returns whether or not this layer should be rendered for the minimap icon. </summary>
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060025DE RID: 9694 RVA: 0x004F6911 File Offset: 0x004F4B11
		public virtual bool IsHeadLayer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x004F6914 File Offset: 0x004F4B14
		public void Hide()
		{
			this.Visible = false;
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x004F691D File Offset: 0x004F4B1D
		internal void AddChildBefore(PlayerDrawLayer child)
		{
			this._childrenBefore.Add(child);
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x004F692B File Offset: 0x004F4B2B
		internal void AddChildAfter(PlayerDrawLayer child)
		{
			this._childrenAfter.Add(child);
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x004F6939 File Offset: 0x004F4B39
		internal void ClearChildren()
		{
			this._childrenBefore.Clear();
			this._childrenAfter.Clear();
		}

		/// <summary> Returns the layer's default visibility. This is usually called as a layer is queued for drawing, but modders can call it too for information. </summary>
		/// <returns> Whether or not this layer will be visible by default. Modders can hide layers later, if needed.</returns>
		// Token: 0x060025E3 RID: 9699 RVA: 0x004F6951 File Offset: 0x004F4B51
		public virtual bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			return true;
		}

		/// <summary>
		/// Returns the layer's default position in regards to other layers. Make use of <see cref="T:Terraria.ModLoader.PlayerDrawLayer.BeforeParent" />, <see cref="T:Terraria.ModLoader.PlayerDrawLayer.AfterParent" />, <see cref="T:Terraria.ModLoader.PlayerDrawLayer.Between" />, or <see cref="T:Terraria.ModLoader.PlayerDrawLayer.Multiple" /> to indicate the position. Use other layers as arguments, usually vanilla layers contained in the <see cref="T:Terraria.DataStructures.PlayerDrawLayers" />. <see cref="P:Terraria.DataStructures.PlayerDrawLayers.BeforeFirstVanillaLayer" /> and <see cref="P:Terraria.DataStructures.PlayerDrawLayers.AfterLastVanillaLayer" /> are also possible values.
		/// </summary>
		// Token: 0x060025E4 RID: 9700
		public abstract PlayerDrawLayer.Position GetDefaultPosition();

		// Token: 0x060025E5 RID: 9701 RVA: 0x004F6954 File Offset: 0x004F4B54
		internal void ResetVisibility(PlayerDrawSet drawInfo)
		{
			foreach (PlayerDrawLayer playerDrawLayer in this.ChildrenBefore)
			{
				playerDrawLayer.ResetVisibility(drawInfo);
			}
			this.Visible = this.GetDefaultVisibility(drawInfo);
			foreach (PlayerDrawLayer playerDrawLayer2 in this.ChildrenAfter)
			{
				playerDrawLayer2.ResetVisibility(drawInfo);
			}
		}

		/// <summary> Draws this layer. This will be called multiple times a frame if a player afterimage is being drawn. If this layer shouldn't draw with each afterimage, check <code>if(drawinfo.shadow == 0f)</code> to only draw for the original player image.</summary>
		// Token: 0x060025E6 RID: 9702
		protected abstract void Draw(ref PlayerDrawSet drawInfo);

		// Token: 0x060025E7 RID: 9703 RVA: 0x004F69E8 File Offset: 0x004F4BE8
		public void DrawWithTransformationAndChildren(ref PlayerDrawSet drawInfo)
		{
			if (!this.Visible)
			{
				return;
			}
			PlayerDrawLayer.Transformation transform = this.Transform;
			if (transform != null)
			{
				transform.PreDrawRecursive(ref drawInfo);
			}
			foreach (PlayerDrawLayer playerDrawLayer in this.ChildrenBefore)
			{
				playerDrawLayer.DrawWithTransformationAndChildren(ref drawInfo);
			}
			this.Draw(ref drawInfo);
			foreach (PlayerDrawLayer playerDrawLayer2 in this.ChildrenAfter)
			{
				playerDrawLayer2.DrawWithTransformationAndChildren(ref drawInfo);
			}
			PlayerDrawLayer.Transformation transform2 = this.Transform;
			if (transform2 == null)
			{
				return;
			}
			transform2.PostDrawRecursive(ref drawInfo);
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x004F6AA0 File Offset: 0x004F4CA0
		protected sealed override void Register()
		{
			ModTypeLookup<PlayerDrawLayer>.Register(this);
			PlayerDrawLayerLoader.Add(this);
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x004F6AAE File Offset: 0x004F4CAE
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x004F6AB6 File Offset: 0x004F4CB6
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x040017DB RID: 6107
		private readonly List<PlayerDrawLayer> _childrenBefore = new List<PlayerDrawLayer>();

		// Token: 0x040017DC RID: 6108
		private readonly List<PlayerDrawLayer> _childrenAfter = new List<PlayerDrawLayer>();

		// Token: 0x02000986 RID: 2438
		public abstract class Transformation
		{
			// Token: 0x170008EF RID: 2287
			// (get) Token: 0x06005539 RID: 21817 RVA: 0x0069BDE7 File Offset: 0x00699FE7
			public virtual PlayerDrawLayer.Transformation Parent { get; }

			/// <summary>
			/// Add a transformation to the drawInfo
			/// </summary>
			// Token: 0x0600553A RID: 21818
			protected abstract void PreDraw(ref PlayerDrawSet drawInfo);

			/// <summary>
			/// Reverse a transformation from the drawInfo
			/// </summary>
			// Token: 0x0600553B RID: 21819
			protected abstract void PostDraw(ref PlayerDrawSet drawInfo);

			// Token: 0x0600553C RID: 21820 RVA: 0x0069BDEF File Offset: 0x00699FEF
			public void PreDrawRecursive(ref PlayerDrawSet drawInfo)
			{
				PlayerDrawLayer.Transformation parent = this.Parent;
				if (parent != null)
				{
					parent.PreDrawRecursive(ref drawInfo);
				}
				this.PreDraw(ref drawInfo);
			}

			// Token: 0x0600553D RID: 21821 RVA: 0x0069BE0A File Offset: 0x0069A00A
			public void PostDrawRecursive(ref PlayerDrawSet drawInfo)
			{
				this.PostDraw(ref drawInfo);
				PlayerDrawLayer.Transformation parent = this.Parent;
				if (parent == null)
				{
					return;
				}
				parent.PostDrawRecursive(ref drawInfo);
			}
		}

		/// <summary>
		/// A PlayerDrawLayer's position in the player rendering draw order. When a player is drawn, each "layer" is drawn from back to front.
		/// </summary>
		// Token: 0x02000987 RID: 2439
		public abstract class Position
		{
		}

		/// <summary>
		/// Places this layer between the two provided layers. This layer will draw over layer1 and under layer2.
		/// <para /> <see langword="null" /> can be used to indicate that the exact position of the layer doesn't matter aside from being before/after the other non-null argument. For example <c>new Between(PlayerDrawLayers.HairBack, null)</c> would place the layer anywhere after <c>HairBack</c>, while <c>new Between(null, PlayerDrawLayers.HairBack)</c> would be anywhere before <c>HairBack</c>. For ordering before or after all vanilla layers, the helper properties <see cref="P:Terraria.DataStructures.PlayerDrawLayers.BeforeFirstVanillaLayer" /> and <see cref="P:Terraria.DataStructures.PlayerDrawLayers.AfterLastVanillaLayer" /> can be used directly. 
		/// <para /> The layer parameters used must have fixed positions, meaning that layers registered using either <see cref="T:Terraria.ModLoader.PlayerDrawLayer.Multiple" />, <see cref="T:Terraria.ModLoader.PlayerDrawLayer.BeforeParent" />, or <see cref="T:Terraria.ModLoader.PlayerDrawLayer.AfterParent" /> are not valid. For vanilla layers, this includes <see cref="F:Terraria.DataStructures.PlayerDrawLayers.FrontAccFront" /> and <see cref="F:Terraria.DataStructures.PlayerDrawLayers.HeldItem" />. If ordering in relation to these layers, consider either using <see cref="T:Terraria.ModLoader.PlayerDrawLayer.AfterParent" /> or <see cref="T:Terraria.ModLoader.PlayerDrawLayer.BeforeParent" /> to draw at whatever positions that layer is actually drawn, or referencing a different layer for ordering instead.
		/// </summary>
		// Token: 0x02000988 RID: 2440
		public sealed class Between : PlayerDrawLayer.Position
		{
			// Token: 0x170008F0 RID: 2288
			// (get) Token: 0x06005540 RID: 21824 RVA: 0x0069BE34 File Offset: 0x0069A034
			public PlayerDrawLayer Layer1 { get; }

			// Token: 0x170008F1 RID: 2289
			// (get) Token: 0x06005541 RID: 21825 RVA: 0x0069BE3C File Offset: 0x0069A03C
			public PlayerDrawLayer Layer2 { get; }

			/// <inheritdoc cref="T:Terraria.ModLoader.PlayerDrawLayer.Between" />
			// Token: 0x06005542 RID: 21826 RVA: 0x0069BE44 File Offset: 0x0069A044
			public Between(PlayerDrawLayer layer1, PlayerDrawLayer layer2)
			{
				this.Layer1 = layer1;
				this.Layer2 = layer2;
			}

			// Token: 0x06005543 RID: 21827 RVA: 0x0069BE5A File Offset: 0x0069A05A
			public Between()
			{
			}
		}

		/// <summary>
		/// Places this layer into multiple <see cref="T:Terraria.ModLoader.PlayerDrawLayer.Position" />s. Use this as a helper for layers that need to move around in the draw order rather than making multiple <see cref="T:Terraria.ModLoader.PlayerDrawLayer" /> 'slots' manually.
		/// <para /> An example of this can be seen in <see href="https://github.com/tModLoader/tModLoader/blob/stable/patches/tModLoader/Terraria/DataStructures/PlayerDrawLayers.tML.cs#L158">PlayerDrawLayers.tML.cs</see>. Note how the conditions for <c>FrontAccFront</c> and <c>HeldItem</c> are mutually exclusive, ensuring that the layer will only be drawn once for a given player. 
		/// </summary>
		// Token: 0x02000989 RID: 2441
		public class Multiple : PlayerDrawLayer.Position, IEnumerable
		{
			// Token: 0x170008F2 RID: 2290
			// (get) Token: 0x06005545 RID: 21829 RVA: 0x0069BE75 File Offset: 0x0069A075
			public IList<ValueTuple<PlayerDrawLayer.Between, PlayerDrawLayer.Multiple.Condition>> Positions { get; } = new List<ValueTuple<PlayerDrawLayer.Between, PlayerDrawLayer.Multiple.Condition>>();

			// Token: 0x06005546 RID: 21830 RVA: 0x0069BE7D File Offset: 0x0069A07D
			public void Add(PlayerDrawLayer.Between position, PlayerDrawLayer.Multiple.Condition condition)
			{
				this.Positions.Add(new ValueTuple<PlayerDrawLayer.Between, PlayerDrawLayer.Multiple.Condition>(position, condition));
			}

			// Token: 0x06005547 RID: 21831 RVA: 0x0069BE91 File Offset: 0x0069A091
			public IEnumerator GetEnumerator()
			{
				return this.Positions.GetEnumerator();
			}

			// Token: 0x02000E21 RID: 3617
			// (Invoke) Token: 0x06006553 RID: 25939
			public delegate bool Condition(PlayerDrawSet drawInfo);
		}

		/// <summary>
		/// Places this layer immediately before (under) the provided parent layer. The visibility and draw order of the layer is also bound to the parent layer, if the parent layer is moved or hidden, this layer will also be moved or hidden.
		/// </summary>
		// Token: 0x0200098A RID: 2442
		public class BeforeParent : PlayerDrawLayer.Position
		{
			// Token: 0x170008F3 RID: 2291
			// (get) Token: 0x06005548 RID: 21832 RVA: 0x0069BE9E File Offset: 0x0069A09E
			public PlayerDrawLayer Parent { get; }

			/// <inheritdoc cref="T:Terraria.ModLoader.PlayerDrawLayer.BeforeParent" />
			// Token: 0x06005549 RID: 21833 RVA: 0x0069BEA6 File Offset: 0x0069A0A6
			public BeforeParent(PlayerDrawLayer parent)
			{
				this.Parent = parent;
			}
		}

		/// <summary>
		/// Places this layer immediately after (over) the provided parent layer. The visibility and draw order of the layer is also bound to the parent layer, if the parent layer is moved or hidden, this layer will also be moved or hidden.
		/// </summary>
		// Token: 0x0200098B RID: 2443
		public class AfterParent : PlayerDrawLayer.Position
		{
			// Token: 0x170008F4 RID: 2292
			// (get) Token: 0x0600554A RID: 21834 RVA: 0x0069BEB5 File Offset: 0x0069A0B5
			public PlayerDrawLayer Parent { get; }

			/// <inheritdoc cref="T:Terraria.ModLoader.PlayerDrawLayer.AfterParent" />
			// Token: 0x0600554B RID: 21835 RVA: 0x0069BEBD File Offset: 0x0069A0BD
			public AfterParent(PlayerDrawLayer parent)
			{
				this.Parent = parent;
			}
		}
	}
}
