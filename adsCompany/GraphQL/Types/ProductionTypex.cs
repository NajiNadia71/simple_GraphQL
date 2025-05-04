using adsCompany.DbContexts;
using adsCompany.Entities;

public class ProductionTypex : ObjectType<Production>
{
    protected override void Configure(IObjectTypeDescriptor<Production> descriptor)
    {
        descriptor.Field(p => p.Id).Type<NonNullType<IdType>>();
        descriptor.Field(p => p.Title).Type<NonNullType<StringType>>();
        descriptor.Field(p => p.Count).Type<StringType>();
        descriptor.Field(p => p.CreateDate).Type<NonNullType<DateTimeType>>();
        
        descriptor.Field(p => p.Ads)
            .ResolveWith<Resolvers>(r => r.GetAds(default!, default!))
            .Type<ListType<AdType>>();
    }

    private class Resolvers
    {
        public IEnumerable<Ad> GetAds([Parent] Production production, [Service] SqliteDbContext dbContext)
        {
            return dbContext.Ads.Where(a => a.ProductionId == production.Id);
        }
    }
}