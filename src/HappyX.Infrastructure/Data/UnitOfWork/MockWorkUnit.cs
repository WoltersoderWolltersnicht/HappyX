using HappyX.Infrastructure.Data.EF;

namespace HappyX.Infrastructure.Data.UnitOfWork;

public class MockWorkUnit : WorkUnit
{
    public MockWorkUnit(
        IServiceProvider serviceProvider) 
        : base(serviceProvider, null)
    {
    }
}