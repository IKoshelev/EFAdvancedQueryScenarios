<Query Kind="Program">
  <Connection>
    <ID>12cbc757-1e16-40be-8699-ea64bc46705f</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Users\Trenzalore\Source\Repos\EntityFrameworkAdvancedQueryScenarios\EFAdvancedQueryScenarios\App\bin\Debug\EFDataContext.dll</CustomAssemblyPath>
    <CustomTypeName>EFDataContext.AdventureWorksDataContext</CustomTypeName>
    <CustomCxString>Data Source=TRENZALORE-PC\SQLEXPRESS;Initial Catalog=AdventureWorks2014;Integrated Security=True</CustomCxString>
  </Connection>
  <Reference Relative="..\App\bin\Debug\EFDataContext.dll">C:\Users\Trenzalore\Source\Repos\EntityFrameworkAdvancedQueryScenarios\EFAdvancedQueryScenarios\App\bin\Debug\EFDataContext.dll</Reference>
  <Reference Relative="..\App\bin\Debug\EntityFramework.dll">C:\Users\Trenzalore\Source\Repos\EntityFrameworkAdvancedQueryScenarios\EFAdvancedQueryScenarios\App\bin\Debug\EntityFramework.dll</Reference>
  <Reference Relative="..\App\bin\Debug\EntityFramework.SqlServer.dll">C:\Users\Trenzalore\Source\Repos\EntityFrameworkAdvancedQueryScenarios\EFAdvancedQueryScenarios\App\bin\Debug\EntityFramework.SqlServer.dll</Reference>
  <Reference Relative="..\App\bin\Debug\LinqKit.dll">C:\Users\Trenzalore\Source\Repos\EntityFrameworkAdvancedQueryScenarios\EFAdvancedQueryScenarios\App\bin\Debug\LinqKit.dll</Reference>
  <Reference Relative="..\App\bin\Debug\Newtonsoft.Json.dll">C:\Users\Trenzalore\Source\Repos\EntityFrameworkAdvancedQueryScenarios\EFAdvancedQueryScenarios\App\bin\Debug\Newtonsoft.Json.dll</Reference>
  <Reference Relative="..\App\bin\Debug\Repositories.dll">C:\Users\Trenzalore\Source\Repos\EntityFrameworkAdvancedQueryScenarios\EFAdvancedQueryScenarios\App\bin\Debug\Repositories.dll</Reference>
  <Reference Relative="..\App\bin\Debug\Services.dll">C:\Users\Trenzalore\Source\Repos\EntityFrameworkAdvancedQueryScenarios\EFAdvancedQueryScenarios\App\bin\Debug\Services.dll</Reference>
  <Reference Relative="..\App\bin\Debug\Shared.dll">C:\Users\Trenzalore\Source\Repos\EntityFrameworkAdvancedQueryScenarios\EFAdvancedQueryScenarios\App\bin\Debug\Shared.dll</Reference>
  <Namespace>Repositories</Namespace>
  <Namespace>Services</Namespace>
  <Namespace>Services.Model</Namespace>
  <Namespace>Shared.Dtos</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Data.Entity</Namespace>
  <Namespace>System.Linq.Expressions</Namespace>
</Query>

void Main()
{
	       var DataContext = this;

            var businessEntityRepo = new Repositories.BusinessEntityRepository(DataContext);

            var result = businessEntityRepo.GetBusinessEntities().Dump();
}

// Define other methods and classes here