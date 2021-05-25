using System;
using AutoMapper;
using MetricsManager.Controllers.Responses;
using MetricsManager.Controllers.Requests;
using MetricsManager.DAL.Models;
using System.ComponentModel;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<long, DateTimeOffset>().ConvertUsing<DateTimeOffsetConverter>();
            CreateMap<AgentInfo, AgentResponse>();
            CreateMap<AgentRequest, AgentInfo>();
            CreateMap<CpuMetric, CpuMetricResponse>().ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time));
            CreateMap<DotNetMetric, DotNetMetricResponse>().ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time));
            CreateMap<HddMetric, HddMetricResponse>().ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time));
            CreateMap<NetworkMetric, NetworkMetricResponse>().ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time));
            CreateMap<RamMetric, RamMetricResponse>().ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time));
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
