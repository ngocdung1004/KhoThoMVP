using AutoMapper;
using KhoThoMVP.DTOs;
using KhoThoMVP.Models;

namespace KhoThoMVP.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User mappings
            CreateMap<User, UserDto>().ReverseMap();

            // Worker mappings
            CreateMap<Worker, WorkerDto>().ReverseMap();

            // JobType mappings
            CreateMap<JobType, JobTypeDto>().ReverseMap();

            // WorkerJobType mappings
            CreateMap<WorkerJobType, WorkerJobTypeDto>().ReverseMap();

            // Review mappings
            CreateMap<Review, ReviewDto>().ReverseMap();

            // Payment mappings
            CreateMap<Payment, PaymentDto>().ReverseMap();

            // Subscription mappings
            CreateMap<Subscription, SubscriptionDto>().ReverseMap();
        }
    }
}
