using Mapster;
using MapsterMapper;

namespace ZPI.Persistance.Mappings;

public sealed class PersistanceMapper : Mapper, IPersistanceMapper
{
    public PersistanceMapper(TypeAdapterConfig config) : base(config)
    {
    }
}
