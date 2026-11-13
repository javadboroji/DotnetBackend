# create New Project
_ dotnet new webapi :create New Project 
_--use-controllers :use Controller Instead of Minmal
_ -n DotnetBackendApi :project name
_  -o src/DotnetBackend.Api :project src created 
_ dotnet new classlib : a class that not run single
# add project To solution
_ dotnet sln add src/DotnetBackend.Api/DotnetBackend.Api.csproj
# Project archture
_ Project.Api :  Resposible conection with out and resive HTTP
_ Project.Domain : Resposible Bussins Logic
_ Project.Application : Responsible Use case Project[validate,checker,save data DB]
_ Project.Infrastructure : Resposible contection with Tolls and Tectology[orm_db,Repository,redis,rabitMq]
_ Entity : inner Domain 
# Dependency law
_ Domain  => any
_ Application => Domain 
_ Infrastructure => Domain + Application 
_ Api => Aplication + Infrastructure   
 Example :dotnet add src/DotnetBackend.Application reference src/DotnetBackend.Domain 
# Fluent API VS Data Annotations
 _ annotations:
    [Key]
    public Guid Id { get; private set; }
    [Required]
    public string Name { get; private set; }
_ Fluent :
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Name)
        .IsRequired()
        .HasMaxLength(200);
# Eccomerce Flow
_ Catalog → Identity → Inventory → Cart → Orders → Payments