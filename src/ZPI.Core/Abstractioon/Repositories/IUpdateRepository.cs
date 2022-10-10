using System.Threading.Tasks;

namespace ZPI.Core.Abstraction.Repositories;
public interface IUpdateRepository<UpdateModel, ResultModel>
{
    public Task<ResultModel> UpdateAsync(UpdateModel updateModel);
    public ResultModel Update(UpdateModel updateModel) => UpdateAsync(updateModel).Result;
}