using adsCompany.DbContexts;
using adsCompany.Entities;

public class AdType : ObjectType<Ad>
{
    protected override void Configure(IObjectTypeDescriptor<Ad> descriptor)
    {
        descriptor.Field(a => a.Id).Type<NonNullType<IdType>>();
        descriptor.Field(a => a.Title).Type<NonNullType<StringType>>();
        descriptor.Field(a => a.ProductionId).Type<NonNullType<IntType>>();
        descriptor.Field(a => a.CreateDate).Type<NonNullType<DateTimeType>>();
        descriptor.Field(a => a.Text).Type<StringType>();
        descriptor.Field(a => a.Production)
            .ResolveWith<Resolvers>(r => r.GetProduction(default!, default!))
            .Type<ProductionTypex>();
    }

    private class Resolvers
    {
        public Production GetProduction([Parent] Ad ad, [Service] SqliteDbContext dbContext)
        {
            return dbContext.Productions.FirstOrDefault(p => p.Id == ad.ProductionId);
        }
    }
}