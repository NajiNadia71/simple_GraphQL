using HotChocolate.Data;
using adsCompany.DbContexts;
using adsCompany.Entities;
using HotChocolate;

using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;


public class AdMutations
{

    private readonly ILogger<AdMutations> _logger;

    public AdMutations(ILogger<AdMutations> logger)
    {
        _logger = logger;
    }
    public async Task<Ad> CreateAd([Service] IDbContextFactory<SqliteDbContext> dbContextFactory, AdInput input)
    {
        try {
            using var context = dbContextFactory.CreateDbContext();
            var ad = new Ad
            {
                Title = input.Title,
                ProductionId = input.ProductionId,
                Text = input.Text,
                CreateDate = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            context.Ads.Add(ad);
            await context.SaveChangesAsync();
            return ad;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating ad");
            return null;
        }
        }


    public async Task<Ad?> UpdateAd([Service] IDbContextFactory<SqliteDbContext> dbContextFactory,int id,AdInput input)
{
    try {
        using var context = dbContextFactory.CreateDbContext();
        var ad = await context.Ads.FirstOrDefaultAsync(a => a.Id == id);
        if (ad == null) return null;

        ad.Title = input.Title;
        ad.ProductionId = input.ProductionId;
        ad.Text = input.Text;

        await context.SaveChangesAsync();
        return ad;
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error updating ad");
        return null;
    }
    }

    public async Task<bool> DeleteAd([Service] IDbContextFactory<SqliteDbContext> dbContextFactory,int id)
    {
        using var context = dbContextFactory.CreateDbContext();
        var ad = await context.Ads.FirstOrDefaultAsync(a => a.Id == id);
        if (ad == null) return false;

        context.Ads.Remove(ad);
        await context.SaveChangesAsync();
        return true;
    }

}