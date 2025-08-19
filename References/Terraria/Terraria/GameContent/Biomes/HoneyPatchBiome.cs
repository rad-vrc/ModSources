using System;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x020002D2 RID: 722
	public class HoneyPatchBiome : MicroBiome
	{
		// Token: 0x060022DB RID: 8923 RVA: 0x00548DC4 File Offset: 0x00546FC4
		public override bool Place(Point origin, StructureMap structures)
		{
			if (GenBase._tiles[origin.X, origin.Y].active() && WorldGen.SolidTile(origin.X, origin.Y, false))
			{
				return false;
			}
			Point point;
			if (!WorldUtils.Find(origin, Searches.Chain(new Searches.Down(80), new GenCondition[]
			{
				new Conditions.IsSolid()
			}), out point))
			{
				return false;
			}
			point.Y += 2;
			Ref<int> @ref = new Ref<int>(0);
			WorldUtils.Gen(point, new Shapes.Circle(8), Actions.Chain(new GenAction[]
			{
				new Modifiers.IsSolid(),
				new Actions.Scanner(@ref)
			}));
			if (@ref.Value < 20)
			{
				return false;
			}
			if (!structures.CanPlace(new Rectangle(point.X - 8, point.Y - 8, 16, 16), 0))
			{
				return false;
			}
			WorldUtils.Gen(point, new Shapes.Circle(8), Actions.Chain(new GenAction[]
			{
				new Modifiers.RadialDither(0.0, 10.0),
				new Modifiers.IsSolid(),
				new Actions.SetTile(229, true, true)
			}));
			ShapeData data = new ShapeData();
			WorldUtils.Gen(point, new Shapes.Circle(4, 3), Actions.Chain(new GenAction[]
			{
				new Modifiers.Blotches(2, 0.3),
				new Modifiers.IsSolid(),
				new Actions.ClearTile(true),
				new Modifiers.RectangleMask(-6, 6, 0, 3).Output(data),
				new Actions.SetLiquid(2, byte.MaxValue)
			}));
			WorldUtils.Gen(new Point(point.X, point.Y + 1), new ModShapes.InnerOutline(data, true), Actions.Chain(new GenAction[]
			{
				new Modifiers.IsEmpty(),
				new Modifiers.RectangleMask(-6, 6, 1, 3),
				new Actions.SetTile(59, true, true)
			}));
			structures.AddProtectedStructure(new Rectangle(point.X - 8, point.Y - 8, 16, 16), 0);
			return true;
		}
	}
}
