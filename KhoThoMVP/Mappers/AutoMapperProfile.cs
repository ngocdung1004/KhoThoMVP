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

            //new
            CreateMap<WorkerSchedule, WorkerScheduleDto>()
           .ForMember(dest => dest.StartTime,
               opt => opt.MapFrom(src => TimeSpan.FromTicks(src.StartTime.Ticks)))
           .ForMember(dest => dest.EndTime,
               opt => opt.MapFrom(src => TimeSpan.FromTicks(src.EndTime.Ticks)));

            CreateMap<CreateWorkerScheduleDto, WorkerSchedule>()
                .ForMember(dest => dest.StartTime,
                    opt => opt.MapFrom(src => TimeOnly.FromTimeSpan(src.StartTime)))
                .ForMember(dest => dest.EndTime,
                    opt => opt.MapFrom(src => TimeOnly.FromTimeSpan(src.EndTime)));

            CreateMap<Booking, BookingDto>()
            .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.BookingDate.ToDateTime(TimeOnly.MinValue)))  // Convert DateOnly to DateTime
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToTimeSpan()))  // Convert TimeOnly to TimeSpan
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToTimeSpan()));  // Convert TimeOnly to TimeSpan
            CreateMap<CreateBookingDto, Booking>()
            .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.BookingDate)))  // Convert DateTime to DateOnly
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))  // Directly map TimeOnly to TimeOnly
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))  // Directly map TimeOnly to TimeOnly
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));  // Set CreatedAt to current datetime

            CreateMap<WorkerRate, WorkerRateDto>().ReverseMap(); 
            CreateMap<CreateWorkerRateDto, WorkerRate>().ReverseMap(); 

            CreateMap<BookingPayment, BookingPaymentDto>().ReverseMap(); 
            CreateMap<CreateBookingPaymentDto, BookingPayment>().ReverseMap(); 

            CreateMap<BookingCancellation, BookingCancellationDto>().ReverseMap(); 
            CreateMap<CreateBookingCancellationDto, BookingCancellation>().ReverseMap(); 
        }
    }
}
