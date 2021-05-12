using System;
using AutoMapper;
using MetricsAgent.DAL.Models;
using MetricsAgent.Controllers.Responses;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<long, DateTimeOffset>().ConvertUsing<DateTimeOffsetConverter>();
            CreateMap<CpuMetric, CpuMetricDto>().ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time));
            CreateMap<DotNetMetric, DotNetMetricDto>().ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time));
            CreateMap<HddMetric, HddMetricDto>().ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time));
            CreateMap<NetworkMetric, NetworkMetricDto>().ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time));
            CreateMap<RamMetric, RamMetricDto>().ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time));
        }

        private class DateTimeOffsetConverter : ITypeConverter<long, DateTimeOffset>
        {
            public DateTimeOffset Convert(long source, DateTimeOffset destination, ResolutionContext context)
            {
                return DateTimeOffset.FromUnixTimeSeconds(source);
            }                
        }
    }
}
