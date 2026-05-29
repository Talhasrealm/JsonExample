# JsonExample – OOP Assignment

A C# Console Application demonstrating JSON parsing, OOP inheritance, and NuGet packages.

## GitHub Commit History (one commit per task)

| Commit | Tag | Description |
|--------|-----|-------------|
| v1.0 | Task 1 | Created `users.json` and read + deserialised first entry |
| v1.1 | Task 2 | Added new entries to the JSON file programmatically |
| v1.2 | Task 3 | Loop to deserialise ALL entries and print to console |
| v1.3 | Task 4 | Inheritance – `Admin`, `RegularUser`, `Moderator` extend `User` |
| v1.4 | Task 5 | Created `user_types.json`, deserialised by Role using a factory switch |

---

## Project Structure

```
JsonExample/
├── JsonExample.csproj   # Project file – references Newtonsoft.Json 13.0.3
├── Program.cs           # All tasks in one file, each in its own static method
├── users.json           # Created for Task 1, extended in Task 2
├── user_types.json      # Created for Task 5 (Admin / Moderator / RegularUser)
└── README.md            # This file
```

---

## Task Descriptions

### Task 1 – Read & Parse JSON
- Created `users.json` with one initial `User` entry.
- Used `File.ReadAllText()` to load the raw JSON string.
- Used `JsonConvert.DeserializeObject<UserList>()` from **Newtonsoft.Json** to parse it into a C# object.
- Printed the first user's details to the console.

### Task 2 – Add New Entries
- Loaded the existing `users.json`.
- Created two new `User` objects and added them to the list.
- Re-serialised with `JsonConvert.SerializeObject(..., Formatting.Indented)` and saved back to the file.

### Task 3 – Deserialise All Entries with a Loop
- Loaded the updated `users.json` (now 3 users).
- Used a `for` loop to iterate over every entry and call `DisplayInfo()`.

### Task 4 – Inheritance
- `User` is the **base class** with `Name`, `Age`, `City` properties and a virtual `DisplayInfo()` method.
- `Admin` extends `User` and adds `Permissions` (list of strings).
- `RegularUser` extends `User` and adds `FavoriteCategory`.
- `Moderator` extends `User` and adds `AssignedSection`.
- A `List<User>` holds all subtypes – polymorphism calls the correct `DisplayInfo()` override.

### Task 5 – Typed JSON Deserialisation
- Created `user_types.json` with entries for all three roles.
- Loaded each entry as a raw `JObject`, read the `"Role"` field, and used a `switch` (factory pattern) to deserialise into the correct subclass.
- Printed each typed user's details with their role-specific fields.

---

## How to Run

1. Open the solution in **Visual Studio 2022** (or later).
2. Restore NuGet packages (automatic on first build).
3. Press **F5** to run.

Or via CLI:
```bash
dotnet restore
dotnet run
```

---

## Expected Console Output (summary)

```
============================================================
  TASK 1 – Read & parse users.json
============================================================
Raw JSON read from file: ...
First user from file:
  Name : John Doe
  Age  : 30
  City : New York

============================================================
  TASK 2 – Add new entries to the JSON file
============================================================
Two new users added. Updated users.json: ...

============================================================
  TASK 3 – Deserialise ALL entries and print with a loop
============================================================
Total users in file: 3

User #1:
  Name : John Doe
  Age  : 30
  City : New York
...

============================================================
  TASK 4 – Inheritance demo
============================================================
  [ADMIN]
  Name : Alice Smith
  ...

============================================================
  TASK 5 – Read user_types.json and deserialise by Role
============================================================
Loaded 3 typed users from user_types.json:
  [ADMIN]
  Name : Alice Smith
  ...
```