namespace DatabaseSimulator;
public class User : TableModel
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public DateTime Birthday { get; set; }
	public string Email { get; set; }

	public User(string firstName, string lastName, DateTime birthday, string email) : base(Guid.NewGuid())
	{
		FirstName = firstName;
		LastName = lastName;
		Birthday = birthday;
		Email = email;
	}
}
public class DummyUser : TableModel
{
	public string FirstName { get; set; } = null!;
	public string Yettoode { get; set; } = null!;
	public DateTime Birthday { get; set; }
	public string Email { get; set; } = null!;

	public DummyUser(string firstName, string yettoode, DateTime birthday, string email)
	{
		FirstName = firstName;
		Yettoode = yettoode;
		Birthday = birthday;
		Email = email;
	}

	public DummyUser() { }
}