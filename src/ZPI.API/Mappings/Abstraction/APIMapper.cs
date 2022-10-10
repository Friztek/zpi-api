using Mapster;
using MapsterMapper;

namespace ZPI.API.Mappings;

public sealed class APIMapper : Mapper, IAPIMapper
{
    public APIMapper(TypeAdapterConfig config) : base(config)
    {
    }
}
