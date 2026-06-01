# JsonExample – OOP Assignment

A C# Console Application demonstrating JSON parsing, OOP inheritance, and NuGet packages.

**GitHub:** https://github.com/Talhasrealm/JsonExample

---

## Version History

| Version | Commit Message | What Was Done |
|---------|---------------|---------------|
| v1.0 | Task 1: Read and parse users.json | Created users.json with one user entry. Used File.ReadAllText() to load the file and JsonConvert.DeserializeObject() from Newtonsoft.Json to parse it into a User object. Printed Name, Age, City to console. |
| v1.1 | Task 2: Added new entries to JSON | Loaded the existing users.json, created two new User objects (Jane Smith and Carlos Lopez), added them to the list, re-serialised with JsonConvert.SerializeObject() and saved back to file. |
| v1.2 | Task 3: Deserialise all entries with loop | Used a for loop to iterate over all entries in the deserialised UserList and called DisplayInfo() on each user, printing all 3 users to the console. |
| v1.3 | Task 4: Inheritance - Admin, RegularUser, Moderator | Created three subclasses extending the base User class. Admin adds Permissions list. RegularUser adds FavoriteCategory. Moderator adds AssignedSection. Each overrides DisplayInfo() using polymorphism. |
| v1.4 | Task 5: user_types.json with typed deserialisation | Created user_types.json with Admin, RegularUser and Moderator entries. Used JObject factory pattern with a switch statement to deserialise each entry into the correct subclass based on the Role field. |

---

## Task Comments

### Task 1 – Read & Parse JSON
- Created `users.json` manually with one user: John Doe, Age 30, New York
- Installed **Newtonsoft.Json** NuGet package (version 13.0.3)
- Used `File.ReadAllText()` to read the raw JSON string from disk
- Used `JsonConvert.DeserializeObject<UserList>()` to convert JSON into a C# object
- Called `DisplayInfo()` on the first user to print details to console

### Task 2 – Add New Entries
- Loaded the existing `users.json` file
- Created two new `User` objects: Jane Smith (25, Los Angeles) and Carlos Lopez (40, Madrid)
- Added both to the `userList.Users` list using `.Add()`
- Re-serialised the full list with `JsonConvert.SerializeObject(..., Formatting.Indented)`
- Saved the updated JSON back to `users.json` using `File.WriteAllText()`

### Task 3 – Deserialise All Entries with a Loop
- Loaded the updated `users.json` which now contains 3 users
- Deserialised the full list using `JsonConvert.DeserializeObject<UserList>()`
- Used a `for` loop with index `i` to iterate over every user
- Called `DisplayInfo()` on each user inside the loop
- Console prints all 3 users: John Doe, Jane Smith, Carlos Lopez

### Task 4 – Inheritance
- `User` is the **base class** with properties: Name, Age, City
- Added a `virtual DisplayInfo()` method to the base class so subclasses can override it
- Created `Admin` class — inherits User, adds `List<string> Permissions`, overrides `DisplayInfo()`
- Created `RegularUser` class — inherits User, adds `string FavoriteCategory`, overrides `DisplayInfo()`
- Created `Moderator` class — inherits User, adds `string AssignedSection`, overrides `DisplayInfo()`
- Used a `List<User>` to hold all three types — polymorphism calls the correct override automatically

### Task 5 – Typed JSON Deserialisation
- Created `user_types.json` with 3 entries: one Admin, one RegularUser, one Moderator
- Each entry has a `"Role"` field to identify its type
- Loaded each entry as a raw `JObject` using `JsonConvert.DeserializeObject<UserTypeList>()`
- Read the `"Role"` field from each JObject using `obj["Role"]?.ToString()`
- Used a `switch` statement (factory pattern) to call `obj.ToObject<Admin>()`, `obj.ToObject<Moderator>()` or `obj.ToObject<RegularUser>()` depending on the role
- Stored all results in a `List<User>` and printed each with `DisplayInfo()`

---
