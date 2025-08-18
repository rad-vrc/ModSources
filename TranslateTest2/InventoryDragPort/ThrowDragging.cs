namespace InventoryDrag.Config
{
    public class ThrowDragging
    {
        public bool PlaySound = true;
        [Terraria.ModLoader.Config.Range(0,100)]
        public int ThrowDelay = 10; // ticks
        public override bool Equals(object obj) => obj is ThrowDragging o && PlaySound == o.PlaySound && ThrowDelay == o.ThrowDelay;
        public override int GetHashCode() => System.HashCode.Combine(PlaySound, ThrowDelay);
    }
}
