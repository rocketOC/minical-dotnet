Summary:
This project will offer little calendars that you can print to your console.

Status:
Not stable

Versioning:
SemVer will be used once 1.0.0 is hit. Until then, minor versions will be used for improvements and patch versions will be used for bug fixes.

Todos:

- [ ] make a stable release
- [ ] add tests
- [ ] add interfaces
- [ ] allow for access of array representations
  - testability
  - easier swaping of display options
- [ ] more display options
  - [ ] compact
  - [x] optional month labels
  - [ ] optional day labels
  - [ ] optional wrapping based on terminal width
  - [ ] ...
- [x] publish to NuGet
- [ ] CD to NuGet

Example 1:

```csharp
using RocketOC.MinicalDotnet;
using System;
using System.Collections.Generic;


void Main()
{
	var mini = new Minical();

	var commits = new List<DateOnly>(){
		new (2022, 07, 06),
		new (2022, 08, 02),
		new (2022, 08, 15),
	};

	//2 blocks of separation beween months
	mini.PrintActivity(commits, 2, true);
	
	Console.WriteLine("\n\n");
	
	//no separation of months
	mini.PrintActivity(commits, 0, true);
}
```

Example Output:

```
Jul `22                     Aug `22                             
+―――+―――+―――+―――+―――+       +―――+―――+―――+―――+                   
|   |   |   |   |   |       |   |   |   |   |                   
+―――+―――+―――+―――+―――+       +―――+―――+―――+―――+                   
|   |   |   |   |   |       |   |   |   |   |                   
+―――+―――+―――+―――+―――+       +―――+―――+―――+―――+                   
    |   |   |   |   |       |   |   |   |   |                   
    +―――+―――+―――+―――+       +―――+―――+―――+―――+―――+               
    | X |   |   |   |       |   |   |   |   |   |               
    +―――+―――+―――+―――+       +―――+―――+―――+―――+―――+               
    |   |   |   |   |       | X |   |   |   |   |               
    +―――+―――+―――+―――+       +―――+―――+―――+―――+―――+               
    |   |   |   |   |       |   |   | X |   |   |               
    +―――+―――+―――+―――+―――+   +―――+―――+―――+―――+―――+               
    |   |   |   |   |   |       |   |   |   |   |               
    +―――+―――+―――+―――+―――+       +―――+―――+―――+―――+               



Jul `22             Aug `22                     
+―――+―――+―――+―――+―――+―――+―――+―――+―――+           
|   |   |   |   |   |   |   |   |   |           
+―――+―――+―――+―――+―――+―――+―――+―――+―――+           
|   |   |   |   |   |   |   |   |   |           
+―――+―――+―――+―――+―――+―――+―――+―――+―――+           
    |   |   |   |   |   |   |   |   |           
    +―――+―――+―――+―――+―――+―――+―――+―――+―――+       
    | X |   |   |   |   |   |   |   |   |       
    +―――+―――+―――+―――+―――+―――+―――+―――+―――+       
    |   |   |   |   | X |   |   |   |   |       
    +―――+―――+―――+―――+―――+―――+―――+―――+―――+       
    |   |   |   |   |   |   | X |   |   |       
    +―――+―――+―――+―――+―――+―――+―――+―――+―――+       
    |   |   |   |   |   |   |   |   |   |       
    +―――+―――+―――+―――+―――+―――+―――+―――+―――+        
```