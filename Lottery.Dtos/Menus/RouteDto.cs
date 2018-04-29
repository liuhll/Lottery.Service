using AutoMapper.Attributes;
using Lottery.Dtos.Power;
using System.Collections.Generic;

namespace Lottery.Dtos.Menus
{
    [MapsFrom(typeof(PowerDto))]
    public class RouteDto
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string Component { get; set; }

        public bool Hidden { get; set; }

        public string Redirect { get; set; }

        [IgnoreMapFrom(typeof(PowerDto))]
        public MetaDto Meta { get; set; }

        [IgnoreMapFrom(typeof(PowerDto))]
        public ICollection<RouteDto> Children { get; set; }
    }
}