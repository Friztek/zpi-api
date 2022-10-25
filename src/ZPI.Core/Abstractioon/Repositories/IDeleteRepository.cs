using System.Threading.Tasks;

namespace ZPI.Core.Abstraction.Repositories;
public interface IDeleteRepository<DeleteModel, ResultModel>
{
    public Task DeleteAsync(DeleteModel deleteModel);
    public void Delete(DeleteModel deleteModel) => DeleteAsync(deleteModel);
}