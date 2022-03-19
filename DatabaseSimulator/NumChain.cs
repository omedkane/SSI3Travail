namespace DatabaseSimulator
{
    public class NumChain: INumChain
	{
		private int Value = 0;

		public NumChain()
		{
		}

		public NumChain(int value)
		{
			this.Value = value;
		}

		public int Get()
		{
			return this.Value;
		}

		public NumChain Add(int num)
		{
			Value += num;
			return this;
		}

		public NumChain Sub(int num)
		{
			Value -= num;
			return this;
		}

		public NumChain Times(int num)
		{
			Value *= num;
			return this;
		}

		public NumChain DivBy(int num)
		{
			Value /= num;
			return this;
		}

		public NumChain Increment()
		{
			Value++;
			return this;
		}

		public NumChain Decrement()
		{
			Value--;
			return this;
		}

	}

}
