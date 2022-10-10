using System.Threading.Tasks;

namespace ZPI.Core.Abstraction.Repositories;
public interface IDeleteRepository<DeleteModel, ResultModel>
{
    public Task<ResultModel> DeleteAsync(DeleteModel deleteModel);
    public ResultModel Delete(DeleteModel deleteModel) => DeleteAsync(deleteModel).Result;
}