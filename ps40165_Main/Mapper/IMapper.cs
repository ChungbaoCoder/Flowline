namespace ps40165_Main.Mapper;

public interface IMapper<TSource, TDestination>
{
    TDestination Map(TSource src);
}
