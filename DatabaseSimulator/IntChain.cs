namespace DatabaseSimulator;

    public class IntChain: IIntChain
	{
		private int Value = 0;

		public IntChain()
		{
		}

		public IntChain(int value)
		{
			this.Value = value;
		}

		public int Get()
		{
			return this.Value;
		}

		public IntChain Add(int num)
		{
			Value += num;
			return this;
		}

		public IntChain Sub(int num)
		{
			Value -= num;
			return this;
		}

		public IntChain Times(int num)
		{
			Value *= num;
			return this;
		}

		public IntChain DivBy(int num)
		{
			Value /= num;
			return this;
		}

		public IntChain Increment()
		{
			Value++;
			return this;
		}

		public IntChain Decrement()
		{
			Value--;
			return this;
		}

	}


