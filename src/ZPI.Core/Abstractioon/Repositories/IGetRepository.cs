using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZPI.Core.Abstraction.Repositories;
public interface IGetRepository<GetModel, ResultModel>
{
    public Task<ResultModel> GetAsync(GetModel getModel);
    public ResultModel Get(GetModel getModel) => GetAsync(getModel).Result;
}