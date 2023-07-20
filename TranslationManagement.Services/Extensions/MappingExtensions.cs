using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationManagement.Services.Extensions
{
    public static class MappingExtensions
    {
        public static List<TDestination> MapEntitiesWithDto<TSource, TDestination>(this IEnumerable<TSource> entities, IMapper mapper)
        {
            return mapper.Map<List<TDestination>>(entities);
        }
    }
}
