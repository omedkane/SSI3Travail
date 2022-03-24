using DatabaseSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseSimulatorTest;

[TestClass]
public class NumChainWrittenTests
{
    IntChain Chain = new(20);

    [TestMethod]
    public void Add()
    {
        Chain.Add(10);
        Assert.AreEqual(Chain.Get(), 30);
    }

    [TestMethod]
    public void Sub()
    {
        Chain.Sub(10);
        Assert.AreEqual(Chain.Get(), 10);
    }

    [TestMethod]
    public void Times()
    {
        Chain.Times(10);
        Assert.AreEqual(Chain.Get(), 200);
    }

    [TestMethod]
    public void DivBy()
    {
        Chain.DivBy(10);
        Assert.AreEqual(Chain.Get(), 2);
    }

    [TestMethod]
    public void Increment()
    {
        Chain.Increment();
        Assert.AreEqual(Chain.Get(), 21);
    }

    [TestMethod]
    public void DecrementBy()
    {
        Chain.Decrement();
        Assert.AreEqual(Chain.Get(), 19);
    }

    [TestMethod]
    public void ChainedOperationsTest()
    {
        Chain.Add(10).Sub(10).Times(2).DivBy(1).Increment().Decrement();

        Assert.AreEqual(Chain.Get(), 40);
    }
}
