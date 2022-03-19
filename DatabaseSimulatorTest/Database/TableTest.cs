using System.Linq;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseSimulator;
using System.Collections.Generic;

namespace DatabaseSimulatorTest;

[TestClass]
public class TableTest
{
	Table<User> users = new Database().CreateTable(
			new User(
				firstName: "Lionel",
				lastName: "Messi",
				birthday: DateTime.Now,
				email: "leomessi@goat.com"
			)
		);

	User testSubject =
		new User(
			firstName: "Ronaldinho",
			lastName: "De Assis Moreira",
			birthday: DateTime.Now,
			email: "ronironi@goat.com"
		);

	[TestMethod]
	public void Insert()
	{
		users.Insert(testSubject);

		Assert.AreEqual(
			users.GetAll().Count,
			2
		);
		Assert.AreEqual(
			users.GetAll().Last().Record.FirstName,
			"Ronaldinho"
		);
	}

	[TestMethod]
	public void Find()
	{
		users.Insert(testSubject);

		Assert.AreSame(
			users.Find(testSubject.ID).Record,
			testSubject
		);
	}

	[TestMethod]
	public void FindAll()
	{
		User testSubject2 =
			new User(
				firstName: "Jackie",
				lastName: "Chan",
				birthday: DateTime.Now,
				email: "jackiechan@karate.ch"
			);
		User testSubject3 =
			new User(
				firstName: "Bruce",
				lastName: "Lee",
				birthday: DateTime.Now,
				email: "brucelee@kungfu.ch"
			);

		users.Insert(testSubject);
		users.Insert(testSubject2);
		users.Insert(testSubject3);

		List<Guid> listOfIDs =
			new List<Guid> {
				testSubject.ID,
				testSubject2.ID,
				testSubject3.ID,
				// - These two should not be found !
				new Guid(),
				Guid.NewGuid(),
			};

		QueryResult<Row<User>, Guid> queryResult = users.FindAll(listOfIDs);

		Assert.IsTrue(
			queryResult.Found.Count == 3 &&
			queryResult.NotFound.Count == 2
		);
	}

	[TestMethod]
	public void FindWhere()
	{
		users.InsertAll(Mocks.users);


		List<Row<User>> salvatores =
			users.FindWhere(
				user => user.LastName == "Salvatore"
			);
		List<Row<User>> mikaelsons =
			users.FindWhere(
				user => user.LastName == "Mikaelson"
			);

		Assert.AreEqual(mikaelsons.Count, 3);
		Assert.AreEqual(salvatores.Count, 2);
	}

	[TestMethod]
	public void FirstWhere()
	{
		users.InsertAll(Mocks.users);

		Row<User> firstSalvatore = users.FirstWhere(user => user.LastName == "Salvatore");

		Assert.AreEqual(firstSalvatore.Record.LastName, "Salvatore");
	}

	[TestMethod]
	public void UpdateAll()
	{
		users.InsertAll(Mocks.users);

		List<Row<User>> salvatores =
			users.FindWhere(
				user => user.LastName == "Salvatore"
			);
		List<Guid> salvatoreIDs = salvatores.Select(row => row.ID).ToList();

		users.UpdateAll(salvatoreIDs, user => user.Email = user.Email.Replace("mfalls", "virginia"));

		Assert.IsTrue(
			salvatores.All(row => row.Record.Email.Contains("virginia"))
		);
	}

	[TestMethod]
	public void UpdateWhere()
	{
		users.InsertAll(Mocks.users);

		users.UpdateWhere(
			user => user.LastName == "Mikaelson",
			user => user.LastName = "Michels"
		);

		Assert.IsTrue(
			users.FindWhere(user => user.LastName == "Michels").Count == 3 &&
			users.FindWhere(user => user.LastName == "Mikaelson").Count == 0
		);
	}

	[TestMethod]
	public void Remove()
	{
		// Table initial row count is 1, will be two after following insertion
		users.Insert(testSubject);

		users.Remove(testSubject.ID);

		Assert.AreEqual(
			users.GetAll().Count,
			1
		);

		Assert.AreNotSame(
			users.GetAll().First().Record,
			testSubject
		);
	}

	[TestMethod]
	public void RemoveWhere()
	{
		users.InsertAll(Mocks.users);
		int initialCount = users.Count;
		// There are 2 salvatores see Mocks
		List<Row<User>> salvatores = users.FindWhere(user => user.LastName == "Salvatore");

		Assert.AreEqual(
			salvatores.Count,
			2
		);

		users.RemoveWhere(user => user.LastName == "Salvatore");

		Assert.AreEqual(
			users.Count,
			initialCount - salvatores.Count
		);
	}
}