using MotorFest.Data;
using MotorFest.Models;

namespace MotorFest.Services.EngineTypeService
{
    public class EngineTypeService : IEngineTypeService
    {
        private readonly MotorFestDbContext dbContext;
        public EngineTypeService(MotorFestDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<EngineTypeViewModel> Create(EngineTypeViewModel engineTypeViewModel)
        {
            EngineType engineTypeEntity = new EngineType
            {
                Id = engineTypeViewModel.Id,
                Name = engineTypeViewModel.Name,

                LastUpdate = DateTime.UtcNow
            };
            await dbContext.EngineTypes.AddAsync(engineTypeEntity);
            await dbContext.SaveChangesAsync();
            return null;
        }

        public async Task<EngineTypeViewModel> Delete(int id)
        {
            var engineType = dbContext.EngineTypes.FirstOrDefault(x => x.Id == id);
            if (engineType != null)
            {
                dbContext.EngineTypes.Remove(engineType);
                await dbContext.SaveChangesAsync();
            }
            return null;
        }

        public ICollection<EngineTypeViewModel> GetAll()
        {

            return dbContext.EngineTypes

                 .Select(engineType => new EngineTypeViewModel
                 {
                     Id = engineType.Id,
                     Name = engineType.Name,

                 }).ToList();
        }

        public async Task<EngineTypeViewModel> GetById(int id)
        {
            var engineType = dbContext.EngineTypes.FirstOrDefault(x => x.Id == id);
            return new EngineTypeViewModel
            {

                Id = id,

                LastUpdate = engineType.LastUpdate,
                Name = engineType.Name
            };
        }

        public async Task<EngineTypeViewModel> Update(int id, EngineTypeViewModel engineType)
        {
            var engineTypeEntity = dbContext.Find<EngineType>(id);

            engineTypeEntity.Id = id;
            engineTypeEntity.Name = engineType.Name;
            engineTypeEntity.LastUpdate = DateTime.Now;

            dbContext.Update(engineTypeEntity);
            await dbContext.SaveChangesAsync();
            return engineType;
        }
    }
}
