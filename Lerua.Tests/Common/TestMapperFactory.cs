using AutoMapper;

namespace Lerua.Tests.Common
{
    public static class TestMapperFactory
    {
        public static IMapper CreateTestMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TestProfile>();
                // либо добавить другие профили, если есть
            });
            return config.CreateMapper();
        }
    }
}
