using AutoMapper;
using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using KhoThoMVP.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoThoMVP.Services
{
    public class BookingService : IBookingService
    {
        private readonly DungnnExe201Thodung5Context _context;
        private readonly IMapper _mapper;

        public BookingService(DungnnExe201Thodung5Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<BookingDto> GetBookingByIdAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByCustomerIdAsync(int customerId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByWorkerIdAsync(int workerId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.WorkerId == workerId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        //public async Task<BookingDto> CreateBookingAsync(CreateBookingDto dto)
        //{
        //    var booking = _mapper.Map<Booking>(dto);

        //    // Calculate total hours
        //    var duration = dto.EndTime - dto.StartTime;
        //    booking.TotalHours = (decimal)duration.TotalHours;

        //    // Get worker's hourly rate
        //    var workerRate = await _context.WorkerRates
        //        .FirstOrDefaultAsync(r => r.WorkerId == dto.WorkerID && r.JobTypeId == dto.JobTypeID);
        //    booking.HourlyRate = workerRate?.HourlyRate ?? 0;
        //    booking.TotalAmount = booking.TotalHours * booking.HourlyRate;

        //    _context.Bookings.Add(booking);
        //    await _context.SaveChangesAsync();
        //    return _mapper.Map<BookingDto>(booking);
        //}

        //public async Task<BookingDto> CreateBookingAsync(CreateBookingDto dto)
        //{
        //    using var transaction = await _context.Database.BeginTransactionAsync();
        //    try
        //    {
        //        var booking = _mapper.Map<Booking>(dto);

        //        // Calculate total hours
        //        var duration = dto.EndTime - dto.StartTime;
        //        booking.TotalHours = (decimal)duration.TotalHours;

        //        // Get worker's hourly rate
        //        var workerRate = await _context.WorkerRates
        //            .FirstOrDefaultAsync(r => r.WorkerId == dto.WorkerID && r.JobTypeId == dto.JobTypeID);
        //        booking.HourlyRate = workerRate?.HourlyRate ?? 0;
        //        booking.TotalAmount = booking.TotalHours * booking.HourlyRate;

        //        // Add booking
        //        _context.Bookings.Add(booking);
        //        await _context.SaveChangesAsync();

        //        // Create corresponding WorkerSchedule
        //        var workerSchedule = new WorkerSchedule
        //        {
        //            WorkerId = dto.WorkerID,
        //            DayOfWeek = ((int)dto.BookingDate.DayOfWeek),
        //            StartTime = dto.StartTime,
        //            EndTime = dto.EndTime,
        //            IsAvailable = false // Worker is not available during booking time
        //        };

        //        _context.WorkerSchedules.Add(workerSchedule);
        //        await _context.SaveChangesAsync();

        //        await transaction.CommitAsync();
        //        return _mapper.Map<BookingDto>(booking);
        //    }
        //    catch (Exception)
        //    {
        //        await transaction.RollbackAsync();
        //        throw;
        //    }
        //}

        //    public async Task<BookingDto> CreateBookingAsync(CreateBookingDto dto)
        //    {
        //        // Kiểm tra lịch trống trước khi tạo booking
        //        if (!await IsWorkerAvailable(dto.WorkerID, dto.BookingDate, dto.StartTime, dto.EndTime))
        //        {
        //            throw new InvalidOperationException("Worker is not available at the requested time.");
        //        }

        //        using var transaction = await _context.Database.BeginTransactionAsync();
        //        try
        //        {
        //            // Kiểm tra và lấy WorkerRate trước
        //            var workerRate = await _context.WorkerRates
        //.FirstOrDefaultAsync(r => r.WorkerId == dto.WorkerID && r.JobTypeId == dto.JobTypeID);

        //            if (workerRate == null && dto.HourlyRate <= 0)
        //            {
        //                throw new InvalidOperationException("Worker rate not found for this job type.");
        //            }

        //            var booking = _mapper.Map<Booking>(dto);

        //            // Tính toán tổng số giờ
        //            var duration = dto.EndTime - dto.StartTime;
        //            booking.TotalHours = (decimal)duration.TotalHours;

        //            // Gán hourly rate từ WorkerRate hoặc từ dto
        //            booking.HourlyRate = workerRate?.HourlyRate ?? dto.HourlyRate;

        //            // Tính tổng tiền
        //            booking.TotalAmount = booking.TotalHours * booking.HourlyRate;

        //            // Thêm booking vào cơ sở dữ liệu
        //            _context.Bookings.Add(booking);
        //            await _context.SaveChangesAsync();

        //            // Tạo WorkerSchedule tương ứng với booking
        //            var workerSchedule = new WorkerSchedule
        //            {
        //                WorkerId = dto.WorkerID,
        //                DayOfWeek = (int)dto.BookingDate.DayOfWeek,
        //                StartTime = dto.StartTime,
        //                EndTime = dto.EndTime,
        //                IsAvailable = false
        //            };

        //            _context.WorkerSchedules.Add(workerSchedule);
        //            await _context.SaveChangesAsync();
        //            await transaction.CommitAsync();

        //            return _mapper.Map<BookingDto>(booking);
        //        }
        //        catch (Exception)
        //        {
        //            await transaction.RollbackAsync();
        //            throw;
        //        }
        //    }

        public async Task<bool> IsWorkerAvailable(int workerId, DateTime bookingDate, TimeOnly startTime, TimeOnly endTime)
        {
            // Get all bookings for the worker on the specified date
            var existingBookings = await _context.Bookings
                .Where(b => b.WorkerId == workerId
                    && b.BookingDate == DateOnly.FromDateTime(bookingDate)
                    && b.Status != "Cancelled")
                .ToListAsync();

            // Check for time slot overlaps
            foreach (var booking in existingBookings)
            {
                // If new booking's start time falls within existing booking
                if (startTime >= booking.StartTime && startTime < booking.EndTime)
                    return false;

                // If new booking's end time falls within existing booking
                if (endTime > booking.StartTime && endTime <= booking.EndTime)
                    return false;

                // If new booking completely encompasses existing booking
                if (startTime <= booking.StartTime && endTime >= booking.EndTime)
                    return false;
            }

            return true;
        }

        public async Task<BookingDto> CreateBookingAsync(CreateBookingDto dto)
        {
            // Check worker availability
            if (!await IsWorkerAvailable(dto.WorkerID, dto.BookingDate, dto.StartTime, dto.EndTime))
            {
                throw new InvalidOperationException("Worker is not available at the requested time.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var workerRate = await _context.WorkerRates
                    .FirstOrDefaultAsync(r => r.WorkerId == dto.WorkerID && r.JobTypeId == dto.JobTypeID);

                if (workerRate == null && dto.HourlyRate <= 0)
                {
                    throw new InvalidOperationException("Worker rate not found for this job type.");
                }

                var booking = _mapper.Map<Booking>(dto);

                var duration = dto.EndTime - dto.StartTime;
                booking.TotalHours = (decimal)duration.TotalHours;
                booking.HourlyRate = workerRate?.HourlyRate ?? dto.HourlyRate;
                booking.TotalAmount = booking.TotalHours * booking.HourlyRate;

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return _mapper.Map<BookingDto>(booking);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }



        //private async Task<bool> IsWorkerAvailable(int workerId, DateTime bookingDate, TimeOnly startTime, TimeOnly endTime)
        //{
        //    // Chỉ kiểm tra xem có booking nào trùng thời gian không
        //    var existingBookings = await _context.Bookings
        //        .AnyAsync(b => b.WorkerId == workerId &&
        //                    b.BookingDate == DateOnly.FromDateTime(bookingDate) &&
        //                    ((b.StartTime <= startTime && b.EndTime > startTime) ||
        //                     (b.StartTime < endTime && b.EndTime >= endTime) ||
        //                     (b.StartTime >= startTime && b.EndTime <= endTime)));

        //    // Nếu không có booking trùng thì cho phép đặt lịch
        //    return !existingBookings;
        //}

        public async Task<BookingDto> UpdateBookingStatusAsync(int id, string status)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return null;

            booking.Status = status;
            await _context.SaveChangesAsync();
            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
