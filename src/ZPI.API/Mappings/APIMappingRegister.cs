using Mapster;
using ZPI.API.DTos;
using ZPI.Core.Domain;

namespace ZPI.IAPI.Mappings;

public sealed class APIMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AssetModel, AssetDto>()
            .Map(d => d.Category, s => s.Category)
            .Map(d => d.FriendlyName, s => s.FriendlyName)
            .Map(d => d.Name, s => s.Name)
            .ShallowCopyForSameType(false);
    }
}