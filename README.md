
GraphQL is a query language and server-side runtime for application programming interfaces (APIs) that gives API clients exactly the data they requested. As an alternative to REST, GraphQL allows developers to make requests to fetch data from multiple data sources with a single API call.
-----------------------------------------------------
Add HotChocolate 

dotnet add package HotChocolate.AspNetCore


dotnet add package HotChocolate.Data.EntityFramework



------------------------------------------------------

Queries are used to fetch data,Like [AdQueries](/adsCompany/GraphQL/Queries/AdQueries.cs)

Mutations are used to create, update, or delete data,Like [AdMutations](/adsCompany/GraphQL/Mutations/AdMutations.cs)

Must use resolver 
--------------------------------------------------------
Use Nitro to see the app running :
http://localhost:5001/graphql/
 

![Reult to see in Nitro](/img/result.png)