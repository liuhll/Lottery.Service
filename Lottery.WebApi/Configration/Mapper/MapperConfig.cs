using System.Reflection;
using AutoMapper.Attributes;

namespace Lottery.WebApi.Configration.Mapper
{
    public class MapperConfig
    {
        public static void InitAutoMapperConfig(params string[] assenblyNames)
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                foreach (var assenblyName in assenblyNames)
                {
                    Assembly.Load(assenblyName).MapTypes(config);
                }
            });
        }
    }
}