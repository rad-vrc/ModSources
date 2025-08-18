using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace BInfoAcc.Common
{
	public class ConfigServer : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[DefaultValue(false)]
		public bool easySell;

		[DefaultValue(true)]
		[ReloadRequired]
		public bool extendedRecipe;

        [DefaultValue(true)]
		public bool updatedPhones;

        [DefaultValue(false)]
        public bool simpleDisplay;

        [Range(30, 600)]
        [DefaultValue(60)]
        public int refreshRate;
   
        [Range(0.25f, 4f)]
        [Increment(.25f)]
        [DefaultValue(1f)]
        public float cycleRate;

        [DefaultValue(true)]
        public bool prioModBiomes;
    }
}
