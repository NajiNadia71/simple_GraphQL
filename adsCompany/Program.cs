using adsCompany.DbContexts;
using adsCompany.Services;
using Microsoft.EntityFrameworkCore;
using NLog;
using HotChocolate.Execution;


using HotChocolate.Types;
using HotChocolate.Data;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Correct way to register DbContext
// builder.Services.AddDbContext<SqliteDbContext>(options =>
//     options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register DbContextFactory
// builder.Services.AddPooledDbContextFactory<SqliteDbContext>(options =>
//           {   options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
//             options.EnableSensitiveDataLogging();
//             options.EnableDetailedErrors();
//           });
// Register DbContext
builder.Services.AddPooledDbContextFactory<SqliteDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("connectionString")));
  
// Register services for dependency injection
builder.Services.AddScoped<AdQueries>();
builder.Services.AddScoped<AdMutations>();
// Add GraphQL server
builder.Services
            .AddGraphQLServer()
            
            .AddQueryType<AdQueries>()
            .AddMutationType<AdMutations>()
            .AddType<AdType>()
             .AddType<ProductionTypex>()

            .AddFiltering()
            .AddSorting()
            .AddErrorFilter<GraphQLErrorFilter>();
// Add services to the container.
// Configure NLog

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAll_Service, All_Service>();
builder.Services.AddDbContext<SqliteDbContext>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
        // Configure the HTTP request pipeline
        app.UseRouting();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SqliteDbContext>();
    // context.Database.EnsureCreated();
   var x = context.Database.GetDbConnection();
 
   Logger logger = LogManager.GetCurrentClassLogger();
    logger.Info("Number of ads in the database: {0}", x);
    // Seed data if needed
    // if (!context.Productions.Any())
    // {
    //     context.Productions.Add(new adsCompany.Entities.Production { Title = "Sample Production", Comment = "adljaldj" });
    //     context.SaveChanges();

    // }

}


app.MapGraphQL();
app.MapGraphQL("/graph");
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

        app.Run();
        

           
         






