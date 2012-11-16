Solution (.sln) files should live in this level and each project in the solution should have a folder in this one where the .csproj and source code files live.

Solution structure from this folder example:

ProjectX.sln
Project.Web (folder)
Project.Framework (folder)

If the repository contains multiple solutions (for instance if there is both a 2008 and 2010 solution), they can live side by side.  Try to name the 2008 project like below 
as it is the outlier.  We should always try to stay on the latest version of the IDE where possible.  

Example:

ProjectX.sln
Project.Web (folder)
Project.Framework (folder)
ProjextX.2008.sln