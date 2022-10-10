using Mapster;
using ZPI.Core.Domain;
using ZPI.Persistance.Entities;

namespace ZPI.IPersistance.Mappings;

public sealed class PersistanceMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AssetEntity, AssetModel>()
            .Map(d => d.Category, s => s.Category)
            .Map(d => d.FriendlyName, s => s.FriendlyName)
            .Map(d => d.Name, s => s.Name)
            .ShallowCopyForSameType(false);
    }
}