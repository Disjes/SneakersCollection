using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakersCollection.Tests
{
    public class TestFixture : IDisposable
    {
        public TestFixture()
        {
            Mapper = AutoMapperTestConfig.Initialize();
        }

        public IMapper Mapper { get; }

        public void Dispose()
        { }
    }

}
