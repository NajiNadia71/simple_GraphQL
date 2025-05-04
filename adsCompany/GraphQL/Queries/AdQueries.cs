using adsCompany.DbContexts;
using adsCompany.Entities;
using Microsoft.EntityFrameworkCore;
using HotChocolate.Data;
using adsCompany.DTO;

public class AdQueries
{
    private readonly ILogger<AdQueries> _logger;
    private readonly SqliteDbContext _context;
    public AdQueries(ILogger<AdQueries> logger,SqliteDbContext context)
    {
        _logger = logger;
        _context = context;
    }
 public async Task<IEnumerable<AdDTO>> GetAds([Service] IDbContextFactory<SqliteDbContext> dbContextFactory)
    {
        using var context = dbContextFactory.CreateDbContext();
        _logger.LogInformation(context.Database.GetConnectionString());
        var res= await context.Ads.Include(a => a.Production).ToListAsync();
        _logger.LogInformation("Fetched {Count} ads", res.Count);
        
        return res.Select(a => new AdDTO{
            Id = a.Id,
            Title = a.Title,
            ProductionId = a.ProductionId,
            ProductionName = a.Production.Title,
            CreateDate = a.CreateDate.ToString(),
            Text = a.Text
        }).ToList();
    }

    public async Task<Ad?> GetAdById(
        [Service] IDbContextFactory<SqliteDbContext> dbContextFactory,
        int id)
    {
        using var context = dbContextFactory.CreateDbContext();
        return await context.Ads
            .Include(a => a.Production)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
    // public IQueryable<Ad> GetAds()
    // {
    //     try
    //     {
            
    //         _logger.LogInformation("Starting to fetch ads");
    //         // var context = _context.CreateDbContext();
    //         _logger.LogInformation("Context created successfully");

    //         var query = _context.Ads.Include(a => a.Production);
    //         _logger.LogInformation("Query created with {Count} ads", query.CountAsync());

    //         return query;
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError(ex, "Error fetching all ads");
    //         return null;
    //     }
    // }

    // public async Task<Ad?> GetAdById( [Service] IDbContextFactory<SqliteDbContext> dbContextFactory,int id)
    // {
    //     using var context = dbContextFactory.CreateDbContext();
    //     return await context.Ads.FirstOrDefaultAsync(a => a.Id == id);
    // }
}