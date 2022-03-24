using System.Linq;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseSimulator;
using DatabaseSimulator.Exceptions;

namespace DatabaseSimulatorTest;

[TestClass]
public class DatabaseTest
{
    private readonly Database Database = new();
    private readonly User testSubject =
        new(
            firstName: "Lionel",
            lastName: "Messi",
            birthday: DateTime.Now,
            email: "leomessi@goat.com"
        );

    [TestMethod]
    public void GetTable()
    {
        Database.CreateTable(testSubject);

        Table<User> justAddedTable = Database.GetTable<User>();

        Assert.AreSame(justAddedTable.GetAll().First().Record, testSubject);
    }

    [TestMethod]
    public void ReadCreateAndRemoveTable()
    {
        // Makes sure there is no User Table
        Assert.ThrowsException<InexistantTableException>(() => Database.GetTable<User>());

        // Adds User Table With testSubject as first record
        Database.CreateTable(testSubject);

        // Get newly added table from the database
        Table<User> justAddedTable = Database.GetTable<User>();

        // Makes sure Row has been added !
        Assert.AreSame(justAddedTable.GetAll().First().Record, testSubject);

        Database.Remove<User>();

        // Makes sure there is no User Table ANYMORE
        Assert.ThrowsException<InexistantTableException>(() => Database.GetTable<User>());
    }

    [TestMethod]
    public void UpdateTable()
    {
        User user = testSubject;
        Database.CreateTable(user);
        Table<User> userTable = Database.GetTable<User>();

        Database.Update<User, DummyUser>();

        Table<DummyUser> dummyUserTable = Database.GetTable<DummyUser>();
        Row<DummyUser> dummyUser = dummyUserTable.GetAll().First();

        Assert.IsTrue(
            user.ID == dummyUser.ID
                && user.FirstName == dummyUser.Record.FirstName
                && user.Email == dummyUser.Record.Email
                && user.Birthday.Equals(dummyUser.Record.Birthday)
        );

        Assert.ThrowsException<InexistantPropertyException>(
            () => dummyUser.Update("LastName", "Messi")
        );
    }
}
