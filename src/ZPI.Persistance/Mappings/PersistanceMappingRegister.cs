using Mapster;
using ZPI.Core.Abstraction.Repositories;
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
            .Map(d => d.Name, s => s.Identifier)
            .ShallowCopyForSameType(false);

        config.NewConfig<AssetValueEntity, AssetValueModel>()
            .Map(d => d.AssetIdentifier, s => s.AssetIdentifier)
            .Map(d => d.TimeStamp, s => s.TimeStamp)
            .Map(d => d.Value, s => s.Value)
            .ShallowCopyForSameType(false);

        config.NewConfig<AssetValueEntity, AssetValueModel>()
            .Map(d => d.AssetIdentifier, s => s.AssetIdentifier)
            .Map(d => d.TimeStamp, s => s.TimeStamp)
            .Map(d => d.Value, s => s.Value)
            .ShallowCopyForSameType(false);

        config.NewConfig<IAssetsRepository.PatchAssetValue, AssetValueEntity>()
            .Map(d => d.AssetIdentifier, s => s.AssetName)
            .Map(d => d.TimeStamp, s => s.TimeStamp)
            .Map(d => d.Value, s => s.Value)
            .Ignore(d => d.Asset);

        config.NewConfig<Core.UseCases.PatchAssetValueUseCase.Input, AssetValueEntity>()
            .Inherits<IAssetsRepository.PatchAssetValue, AssetValueEntity>();

    }
}