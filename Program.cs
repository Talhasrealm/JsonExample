// =============================================================
//  JsonExample - OOP Assignment
//  Tasks 1–5 all in one file, run from Main().
// =============================================================
// Task 1: Read and parse users.json


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

// ─────────────────────────────────────────────────────────────
//  BASE CLASS  (used in Tasks 1-3 and extended in Task 4)
// ─────────────────────────────────────────────────────────────
public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }

    // Virtual method – subclasses can override it (Task 4)
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"  Name : {Name}");
        Console.WriteLine($"  Age  : {Age}");
        Console.WriteLine($"  City : {City}");
    }
}

// ─────────────────────────────────────────────────────────────
//  TASK 4 – Inheritance: specialised user types
// ─────────────────────────────────────────────────────────────

// 4a. Admin – has a list of permissions
public class Admin : User
{
    public List<string> Permissions { get; set; } = new List<string>();

    public override void DisplayInfo()
    {
        Console.WriteLine($"  [ADMIN]");
        base.DisplayInfo();
        Console.WriteLine($"  Permissions: {string.Join(", ", Permissions)}");
    }
}

// 4b. RegularUser – has a favourite category
public class RegularUser : User
{
    public string FavoriteCategory { get; set; }

    public override void DisplayInfo()
    {
        Console.WriteLine($"  [REGULAR USER]");
        base.DisplayInfo();
        Console.WriteLine($"  Favourite Category: {FavoriteCategory}");
    }
}

// 4c. Moderator – is assigned to a section
public class Moderator : User
{
    public string AssignedSection { get; set; }

    public override void DisplayInfo()
    {
        Console.WriteLine($"  [MODERATOR]");
        base.DisplayInfo();
        Console.WriteLine($"  Assigned Section: {AssignedSection}");
    }
}

// ─────────────────────────────────────────────────────────────
//  Helper wrapper classes used for JSON (de)serialisation
// ─────────────────────────────────────────────────────────────
public class UserList
{
    public List<User> Users { get; set; }
}

public class UserTypeList
{
    public List<JObject> UserTypes { get; set; }   // raw JObjects – we inspect "Role" to pick a type
}

// ─────────────────────────────────────────────────────────────
//  PROGRAM ENTRY POINT
// ─────────────────────────────────────────────────────────────
class Program
{
    static void Main(string[] args)
    {
        Separator("TASK 1 – Read & parse users.json");
        Task1_ReadJson();

        Separator("TASK 2 – Add new entries to the JSON file");
        Task2_AddEntries();

        Separator("TASK 3 – Deserialise ALL entries and print with a loop");
        Task3_DeserializeAll();

        Separator("TASK 4 – Inheritance demo (Admin / RegularUser / Moderator)");
        Task4_InheritanceDemo();

        Separator("TASK 5 – Read user_types.json and deserialise by Role");
        Task5_ReadUserTypes();

        Console.WriteLine("\nAll tasks complete. Press any key to exit.");
        Console.ReadKey();
    }

    // ──────────────────────────────────────────────────────────
    //  TASK 1
    //  Read the existing users.json and deserialise the first entry.
    // ──────────────────────────────────────────────────────────
    static void Task1_ReadJson()
    {
        string filePath = "users.json";

        // Read raw JSON text from file
        string jsonText = File.ReadAllText(filePath);
        Console.WriteLine("Raw JSON read from file:");
        Console.WriteLine(jsonText);

        // Deserialise into a UserList wrapper object
        UserList userList = JsonConvert.DeserializeObject<UserList>(jsonText);

        // Display the first user
        Console.WriteLine("First user from file:");
        userList.Users[0].DisplayInfo();
    }

    // ──────────────────────────────────────────────────────────
    //  TASK 2
    //  Load users.json, add two new User objects, save back.
    // ──────────────────────────────────────────────────────────
    static void Task2_AddEntries()
    {
        string filePath = "users.json";

        // Load existing data
        string jsonText = File.ReadAllText(filePath);
        UserList userList = JsonConvert.DeserializeObject<UserList>(jsonText);

        // Create new users and add them to the list
        User newUser1 = new User { Name = "Jane Smith", Age = 25, City = "Los Angeles" };
        User newUser2 = new User { Name = "Carlos Lopez", Age = 40, City = "Madrid" };

        userList.Users.Add(newUser1);
        userList.Users.Add(newUser2);

        // Serialise back to JSON with indentation and save
        string updatedJson = JsonConvert.SerializeObject(userList, Formatting.Indented);
        File.WriteAllText(filePath, updatedJson);

        Console.WriteLine("Two new users added. Updated users.json:");
        Console.WriteLine(updatedJson);
    }

    // ──────────────────────────────────────────────────────────
    //  TASK 3
    //  Deserialise ALL entries and print each one using a loop.
    // ──────────────────────────────────────────────────────────
    static void Task3_DeserializeAll()
    {
        string filePath = "users.json";

        string jsonText = File.ReadAllText(filePath);
        UserList userList = JsonConvert.DeserializeObject<UserList>(jsonText);

        Console.WriteLine($"Total users in file: {userList.Users.Count}\n");

        // Loop through every user and display their info
        for (int i = 0; i < userList.Users.Count; i++)
        {
            Console.WriteLine($"User #{i + 1}:");
            userList.Users[i].DisplayInfo();
            Console.WriteLine();
        }
    }

    // ──────────────────────────────────────────────────────────
    //  TASK 4
    //  Demonstrate inheritance by creating Admin / RegularUser /
    //  Moderator objects directly in code.
    // ──────────────────────────────────────────────────────────
    static void Task4_InheritanceDemo()
    {
        // Polymorphic list – holds any subclass of User
        List<User> staff = new List<User>
        {
            new Admin
            {
                Name        = "Alice Smith",
                Age         = 28,
                City        = "London",
                Permissions = new List<string> { "read", "write", "delete", "manage_users" }
            },
            new RegularUser
            {
                Name             = "Bob Johnson",
                Age              = 22,
                City             = "Berlin",
                FavoriteCategory = "Technology"
            },
            new Moderator
            {
                Name            = "Carol White",
                Age             = 35,
                City            = "Paris",
                AssignedSection = "Forums"
            }
        };

        // Loop and call DisplayInfo() – polymorphism picks the right override
        foreach (User u in staff)
        {
            u.DisplayInfo();
            Console.WriteLine();
        }
    }

    // ──────────────────────────────────────────────────────────
    //  TASK 5
    //  Read user_types.json, inspect the "Role" field of each
    //  entry and deserialise into the correct subclass.
    // ──────────────────────────────────────────────────────────
    static void Task5_ReadUserTypes()
    {
        string filePath = "user_types.json";

        string jsonText = File.ReadAllText(filePath);
        UserTypeList rawList = JsonConvert.DeserializeObject<UserTypeList>(jsonText);
        List<User> typedUsers = new List<User>();

        // Factory: convert each raw JObject to the right subclass
        foreach (JObject obj in rawList.UserTypes)
        {
            string role = obj["Role"]?.ToString() ?? "RegularUser";

            User typed;
            switch (role)
            {
                case "Admin":
                    typed = obj.ToObject<Admin>();
                    break;
                case "Moderator":
                    typed = obj.ToObject<Moderator>();
                    break;
                default:          // "RegularUser" and any unknown role
                    typed = obj.ToObject<RegularUser>();
                    break;
            }
            typedUsers.Add(typed);
        }

        Console.WriteLine($"Loaded {typedUsers.Count} typed users from user_types.json:\n");

        foreach (User u in typedUsers)
        {
            u.DisplayInfo();
            Console.WriteLine();
        }
    }

    // ──────────────────────────────────────────────────────────
    //  Utility: print a visible separator between tasks
    // ──────────────────────────────────────────────────────────
    static void Separator(string title)
    {
        Console.WriteLine();
        Console.WriteLine(new string('=', 60));
        Console.WriteLine($"  {title}");
        Console.WriteLine(new string('=', 60));
    }
}