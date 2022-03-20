using DatabaseSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DatabaseSimulatorTest
{
	[TestClass]
	public class NumChainGeneratedTests
	{
        #pragma warning disable CS8618

		private MockRepository mockRepository;

        #pragma warning restore CS8618




		[TestInitialize]
		public void TestInitialize()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);
		}

private IntChain CreateNumChain() {
    return new IntChain();
}

		[TestMethod]
		public void getValue_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var numChain = this.CreateNumChain();

			// Act
			var result = numChain.Get();

			// Assert
			Assert.Fail();
			this.mockRepository.VerifyAll();
		}

[TestMethod]
public void add_StateUnderTest_ExpectedBehavior()
{
    // Arrange
    var numChain = this.CreateNumChain();
    int num = 0;

    // Act
    var result = numChain.Add(
        num);

    // Assert
    Assert.Fail();
    this.mockRepository.VerifyAll();
}

		[TestMethod]
		public void substract_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var numChain = this.CreateNumChain();
			int num = 0;

			// Act
			var result = numChain.Sub(
				num);

			// Assert
			Assert.Fail();
			this.mockRepository.VerifyAll();
		}

		[TestMethod]
		public void multiplyBy_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var numChain = this.CreateNumChain();
			int num = 0;

			// Act
			var result = numChain.Times(
				num);

			// Assert
			Assert.Fail();
			this.mockRepository.VerifyAll();
		}

		[TestMethod]
		public void divideBy_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var numChain = this.CreateNumChain();
			int num = 0;

			// Act
			var result = numChain.DivBy(
				num);

			// Assert
			Assert.Fail();
			this.mockRepository.VerifyAll();
		}

		[TestMethod]
		public void increment_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var numChain = this.CreateNumChain();

			// Act
			var result = numChain.Increment();

			// Assert
			Assert.Fail();
			this.mockRepository.VerifyAll();
		}

		[TestMethod]
		public void decrement_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var numChain = this.CreateNumChain();

			// Act
			var result = numChain.Decrement();

			// Assert
			Assert.Fail();
			this.mockRepository.VerifyAll();
		}
	}
}
