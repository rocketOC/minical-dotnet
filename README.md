Summary:
This project will offer little calendars that you can print to your console.

Status:
Not Stable

My Todos:
* make stable
* add tests
* add interfaces
* allow for access of array representations
    * testability
    * easier swaping of display options
* more display options
    * compact
    * optional month labels
    * optional wrapping based on character widths
    * ...
* publish to NuGet

Example 1:
```csharp
using RocketOC.MinicalDotnet;
using System;


void Main()
{
	var mini = new Minical();

	var commits = new List<DateOnly>(){
		new (2022, 07, 06),
		new (2022, 08, 02),
		new (2022, 08, 15),
	};

	//2 blocks of separation beween months
	mini.PrintActivity(commits, 2);
	
	Console.WriteLine("\n\n");
	
	//no separation of months
	mini.PrintActivity(commits, 0);
}
```

Example Output:

```
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