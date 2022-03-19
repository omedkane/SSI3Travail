using System.Linq;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseSimulator;
using DatabaseSimulator.Exceptions;

namespace DatabaseSimulatorTest;

[TestClass]
public class RowTest
{
	Row<User> Row = new Database().CreateTable(
		new User(
			firstName: "Lionel",
			lastName: "Messi",
			birthday: DateTime.Now,
			email: "leomessi@goat.com"
		)
	).GetAll().First();

	[TestMethod]
	public void UpdatePropWithVal()
	{
		Row.Update("FirstName", "Oumar");
		Row.Update("LastName", "Kane");

		Assert.IsTrue(
			Row.Record.FirstName == "Oumar" &&
			Row.Record.LastName == "Kane"
		);

		Assert.ThrowsException<PrimaryKeyException>(
			() => Row.Update("ID", new Guid())
		);
	}

	[TestMethod]
	public void UpdateWithAction()
	{
		string newEmail = "goku@dragonball.z";

		Row.Update(
			(record) =>
			{
				record.Email = newEmail;
			}
		);

		Assert.AreEqual(
			Row.Record.Email,
			newEmail
		);
	}
}