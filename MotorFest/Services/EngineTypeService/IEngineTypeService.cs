using MotorFest.Models.EngineType;

namespace MotorFest.Services.EngineTypeService
{
    public interface IEngineTypeService : ICreate<EngineTypeViewModel>, IGet<EngineTypeViewModel,int>, IUpdate<EngineTypeViewModel>, IDelete<EngineTypeViewModel>
    {
    }
}
