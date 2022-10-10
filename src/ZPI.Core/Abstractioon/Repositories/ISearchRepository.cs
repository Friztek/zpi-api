using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZPI.Core.Abstraction.Repositories;
public interface ISearchRepository<SearchModel, ResultModel>
{
    public Task<IEnumerable<ResultModel>> SearchAsync(SearchModel searchModel);
    public IEnumerable<ResultModel> Search(SearchModel searchModel) => SearchAsync(searchModel).Result;
}