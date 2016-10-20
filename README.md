# EFAdvancedQueryScenarios
Entity Framework advanced query and data unit test scenarios showcase solution

# Projects / libraries used
Showcase DB - AdventureWorks 2014 
https://msftdbprodsamples.codeplex.com/downloads/get/880661

Entity Framework 6 + HierarchyId 
https://www.nuget.org/packages/EntityFrameworkWithHierarchyId/

Context generated from DB by EntityFramework Reverse POCO Generator 
https://visualstudiogallery.msdn.microsoft.com/ee4fcff9-0c4c-4179-afd9-7a2fb90f5838

Some queries use LINQKit 
https://www.nuget.org/packages/LinqKit/

To work with project, download and restore DB to your SQL Server (I used EXPRESS 2014 free version), 
update connection string and restore Nuget packages.

If you want to regenerate DbContext files, you will need Entity Framework Power Tools 
https://visualstudiogallery.msdn.microsoft.com/72a60b14-1581-4b9b-89f2-846072eff19d
http://stackoverflow.com/questions/27999235/how-to-use-entity-framework-power-tools-in-visual-studio-2015

Project includes LINQPad files to invoke showcase queries https://www.linqpad.net/
