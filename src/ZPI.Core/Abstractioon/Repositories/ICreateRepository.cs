using System.Threading.Tasks;

namespace ZPI.Core.Abstraction.Repositories;
public interface ICreateRepository<CreateModel, ResultModel>
{
    public Task<ResultModel> CreateAsync(CreateModel createModel);
    public ResultModel Create(CreateModel createModel) => CreateAsync(createModel).Result;
}