using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000064 RID: 100
	public class GenerationProgress
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x0048C914 File Offset: 0x0048AB14
		// (set) Token: 0x06001139 RID: 4409 RVA: 0x0048C92C File Offset: 0x0048AB2C
		public string Message
		{
			get
			{
				return string.Format(this._message, this.Value);
			}
			set
			{
				this._message = value.Replace("%", "{0:0.0%}");
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x0048C964 File Offset: 0x0048AB64
		// (set) Token: 0x0600113A RID: 4410 RVA: 0x0048C944 File Offset: 0x0048AB44
		public double Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = Utils.Clamp<double>(value, 0.0, 1.0);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x0048C96C File Offset: 0x0048AB6C
		public double TotalProgress
		{
			get
			{
				if (this.TotalWeight == 0.0)
				{
					return 0.0;
				}
				return (this.Value * this.CurrentPassWeight + this._totalProgress) / this.TotalWeight;
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0048C9A4 File Offset: 0x0048ABA4
		public void Set(double value)
		{
			this.Value = value;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0048C9AD File Offset: 0x0048ABAD
		public void Start(double weight)
		{
			this.CurrentPassWeight = weight;
			this._value = 0.0;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0048C9C5 File Offset: 0x0048ABC5
		public void End()
		{
			this._totalProgress += this.CurrentPassWeight;
			this._value = 0.0;
		}

		// Token: 0x04000F09 RID: 3849
		private string _message = "";

		// Token: 0x04000F0A RID: 3850
		private double _value;

		// Token: 0x04000F0B RID: 3851
		private double _totalProgress;

		// Token: 0x04000F0C RID: 3852
		public double TotalWeight;

		// Token: 0x04000F0D RID: 3853
		public double CurrentPassWeight = 1.0;
	}
}
