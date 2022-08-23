# Summary
This project will offer little calendars that you can print to your console.

# Status
Not stable

# Versioning
SemVer will be used once 1.0.0 is hit. Until then, minor versions will be used for improvements and patch versions will be used for bug fixes.

# Todos

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
  - [X] optional wrapping
  - [ ] ...
- [x] publish to NuGet
- [ ] CD to NuGet
- [ ] block merges containing the same version

# Examples

## Example 0

See [as-minical](https://github.com/rocketOC/as-minical) for an example command line program

## Example 1

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

	//2 blocks of separation beween months and no wraping
	mini.PrintActivity(commits, 2, true, -1);
	
	Console.WriteLine("\n\n");
	
	//no separation of months and no wraping
	mini.PrintActivity(commits, 0, true, -1);
}
```

Output:

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