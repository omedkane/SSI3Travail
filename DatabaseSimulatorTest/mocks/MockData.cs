using System;
using System.Collections.Generic;
using DatabaseSimulator;

static public class Mocks
{
    public static List<User> users = new List<User>
    {
        new User(
            firstName: "Klaus",
            lastName: "Mikaelson",
            birthday: DateTime.Now,
            email: "klaus@mikaelsons.vk"
        ),
        new User(
            firstName: "Elijah",
            lastName: "Mikaelson",
            birthday: DateTime.Now,
            email: "elijah@mikaelsons.vk"
        ),
        new User(
            firstName: "Rebekah",
            lastName: "Mikaelson",
            birthday: DateTime.Now,
            email: "rebekah@mikaelsons.vk"
        ),
        new User(
            firstName: "Stefan",
            lastName: "Salvatore",
            birthday: DateTime.Now,
            email: "stefan@salvatores.mfalls"
        ),
        new User(
            firstName: "Damon",
            lastName: "Salvatore",
            birthday: DateTime.Now,
            email: "damon@salvatores.mfalls"
        )
    };
}
