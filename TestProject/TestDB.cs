using DataContext;
using DataContext.Abstractions.Interfaces;
using DataContext.Abstractions.Models;
using DataContext.Context;
using Domain.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace TestProject
{
    [TestClass]
    public sealed class TestDB : TestBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            RegistrationDataContext.UseDB(services, Configuration);
        }

        [TestMethod]
        public async Task TestMethod_RefPositionRepository()
        {
            IRefPositionRepository repository = GetService<IRefPositionRepository>();
            
            IList<RefPosition> list = await repository.ToListAsync();
            Assert.IsNotNull(list);
            Assert.IsEmpty(list);
            Assert.HasCount(0, list);
            Assert.AreEqual(1, await repository.AddAsync(new RefPosition()
                {
                    Name = "Test",
                    StandardSalary = 10
                }
            ));
            list = await repository.ToListAsync();
            Assert.IsNotNull(list);
            Assert.IsNotEmpty(list);
            Assert.HasCount(1, list);
            Assert.AreEqual(1, list[0].Id);
            Assert.AreEqual("Test", list[0].Name);
            Assert.AreEqual(10, list[0].StandardSalary);
            Assert.IsNotNull(list[0].Positions);
            Assert.IsEmpty(list[0].Positions);
            
            var obj = list[0];
            obj.Name = "Test2";
            obj.StandardSalary = 20;
            Assert.AreEqual(1, await repository.UpdateAsync(obj));
            list = await repository.ToListAsync();
            Assert.IsNotNull(list);
            Assert.IsNotEmpty(list);
            Assert.HasCount(1, list);
            Assert.AreEqual(1, list[0].Id);
            Assert.AreEqual("Test2", list[0].Name);
            Assert.AreEqual(20, list[0].StandardSalary);
            Assert.IsNotNull(list[0].Positions);
            Assert.IsEmpty(list[0].Positions);

            Assert.AreEqual(1, await repository.DeleteAsync(x => x.Id == list[0].Id));
            list = await repository.ToListAsync();
            Assert.IsNotNull(list);
            Assert.IsEmpty(list);
            Assert.HasCount(0, list);
        }
        [TestMethod]
        public async Task TestMethod_DrivingSchoolRepository()
        {
            IDrivingSchoolRepository repository = GetService<IDrivingSchoolRepository>();

            IList<DrivingSchool> list = await repository.ToListAsync();
            Assert.IsNotNull(list);
            Assert.IsEmpty(list);
            Assert.HasCount(0, list);
            Assert.AreEqual(1, await repository.AddAsync(new DrivingSchool()
            {
                Name = "Test",
                Adress = "TestAdress"
            }
            ));
            list = await repository.ToListAsync();
            Assert.IsNotNull(list);
            Assert.IsNotEmpty(list);
            Assert.HasCount(1, list);
            Assert.AreEqual(1, list[0].Id);
            Assert.AreEqual("Test", list[0].Name);
            Assert.AreEqual("TestAdress", list[0].Adress);
            Assert.IsNotNull(list[0].Positions);
            Assert.IsEmpty(list[0].Positions);

            var obj = list[0];
            obj.Name = "Test2";
            obj.Adress = "TestAdress2";
            Assert.AreEqual(1, await repository.UpdateAsync(obj));
            list = await repository.ToListAsync();
            Assert.IsNotNull(list);
            Assert.IsNotEmpty(list);
            Assert.HasCount(1, list);
            Assert.AreEqual(1, list[0].Id);
            Assert.AreEqual("Test2", list[0].Name);
            Assert.AreEqual("TestAdress2", list[0].Adress);
            Assert.IsNotNull(list[0].Positions);
            Assert.IsEmpty(list[0].Positions);

            Assert.AreEqual(1, await repository.DeleteAsync(x => x.Id == list[0].Id));
            list = await repository.ToListAsync();
            Assert.IsNotNull(list);
            Assert.IsEmpty(list);
            Assert.HasCount(0, list);
        }
        [TestMethod]
        public async Task TestMethod_HavingsRepository()
        {
            IHavingsRepository repository = GetService<IHavingsRepository>();

            IList<Havings> list = await repository.ToListAsync();
            Assert.IsNotNull(list);
            Assert.IsEmpty(list);
            Assert.HasCount(0, list);
            Assert.AreEqual(1, await repository.AddAsync(new Havings()
            {
                Name = "Test",
            }
            ));
            list = await repository.ToListAsync();
            Assert.IsNotNull(list);
            Assert.IsNotEmpty(list);
            Assert.HasCount(1, list);
            Assert.AreEqual(1, list[0].Id);
            Assert.AreEqual("Test", list[0].Name);
            Assert.IsNull(list[0].IdPosition);
            Assert.IsNull(list[0].Position);

            var obj = list[0];
            obj.Name = "Test2";
            Assert.AreEqual(1, await repository.UpdateAsync(obj));
            list = await repository.ToListAsync();
            Assert.IsNotNull(list);
            Assert.IsNotEmpty(list);
            Assert.HasCount(1, list);
            Assert.AreEqual(1, list[0].Id);
            Assert.AreEqual("Test2", list[0].Name);
            Assert.IsNull(list[0].IdPosition);
            Assert.IsNull(list[0].Position);

            Assert.AreEqual(1, await repository.DeleteAsync(x => x.Id == list[0].Id));
            list = await repository.ToListAsync();
            Assert.IsNotNull(list);
            Assert.IsEmpty(list);
            Assert.HasCount(0, list);
        }
    }
}
