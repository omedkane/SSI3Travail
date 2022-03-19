using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseSimulator.Miscellaneous;
using DatabaseSimulator;
using System;
using DatabaseSimulator.Exceptions;

namespace DatabaseSimulatorTest;

[TestClass]
public class ObjectHelperTest
{
	[TestMethod]
	public void GetCommonProperties()
	{
		List<string> commonProps = ObjectHelper.GetCommonProperties<User, DummyUser>().ToList();

		List<string> expectedCommon =
			new List<string> { "FirstName", "Birthday", "Email" };

		Assert.AreEqual(
			commonProps.Intersect(expectedCommon).Count(),
			expectedCommon.Count
		);
	}

	[TestMethod]
	public void CopyTo()
	{
		User user =
			new User(
				firstName: "Lionel",
				lastName: "Messi",
				birthday: DateTime.Now,
				email: "leomessi@goat.com"
			);

		DummyUser dummyUser = new DummyUser();

		ObjectHelper.CopyTo(user, dummyUser);

		Assert.IsTrue(
			user.FirstName.Equals(dummyUser.FirstName) &&
			user.Birthday.Equals(dummyUser.Birthday) &&
			user.Email.Equals(dummyUser.Email)
		);
	}
	[TestMethod]
	public void Set()
	{
		DummyUser dummyUser = new DummyUser();
		string newName = "Michael";

		ObjectHelper.Set(dummyUser, "FirstName", newName);

		Assert.AreEqual(dummyUser.FirstName, newName);

		Assert.ThrowsException<ValueTypeMismatchException>(
			() => ObjectHelper.Set(dummyUser, "FirstName", 987456)
		);
		Assert.ThrowsException<InexistantPropertyException>(
			() => ObjectHelper.Set(dummyUser, "JeNexistePas", "PasDuTout")
		);
	}
}