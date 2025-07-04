using AutoMapper;
using CozyCare.BookingService.Applications.Interfaces;
using CozyCare.BookingService.DTOs.BookingStatuses;
using CozyCare.BookingService.Infrastructure;
using CozyCare.SharedKernel.Base;

namespace CozyCare.BookingService.Applications.Services
{
	public class BookingStatusService : IBookingStatusService
	{
		private readonly IBookingUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public BookingStatusService(IBookingUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<BaseResponse<IEnumerable<BStatusResponse>>> GetAllBookingStatusesAsync()
		{
			var statuses = await _unitOfWork.BookingStatuses.GetAllAsync();
			var response = _mapper.Map<IEnumerable<BStatusResponse>>(statuses);
			return  BaseResponse<IEnumerable<BStatusResponse>>.OkResponse(response);
		}

		public async Task<BaseResponse<BStatusResponse>> GetBookingStatusByIdAsync(int id)
		{
			var status = await _unitOfWork.BookingStatuses.GetByIdAsync(id);
			if (status == null)
			{
				return BaseResponse<BStatusResponse>.NotFoundResponse($"Booking status with ID {id} not found.");
			}
			var response = _mapper.Map<BStatusResponse>(status);
			return BaseResponse<BStatusResponse>.OkResponse(response);
		}

		public async Task<BaseResponse<BStatusResponse>> CreateBookingStatusAsync(BStatusRequest request)
		{
			var status = _mapper.Map<Domain.Entities.BookingStatus>(request);
			await _unitOfWork.BookingStatuses.AddAsync(status);
			await _unitOfWork.SaveChangesAsync();
			var response = _mapper.Map<BStatusResponse>(status);
			return BaseResponse<BStatusResponse>.OkResponse(response);
		}

		public async Task<BaseResponse<string>> UpdateBookingStatusAsync(int id, BStatusRequest request)
		{
			var status = await _unitOfWork.BookingStatuses.GetByIdAsync(id);
			if (status == null)
			{
				return BaseResponse<string>.NotFoundResponse($"Booking status with ID {id} not found.");
			}
			_mapper.Map(request, status);
			_unitOfWork.BookingStatuses.Update(status);
			await _unitOfWork.SaveChangesAsync();
			return BaseResponse<string>.OkResponse("Booking status updated successfully.");
		}

		public async Task<BaseResponse<string>> DeleteBookingStatusAsync(int id)
		{
			var status = await _unitOfWork.BookingStatuses.GetByIdAsync(id);
			if (status == null)
			{
				return BaseResponse<string>.NotFoundResponse($"Booking status with ID {id} not found.");
			}
			_unitOfWork.BookingStatuses.Delete(status);
			await _unitOfWork.SaveChangesAsync();
			return BaseResponse<string>.OkResponse("Booking status deleted successfully.");
		}
	}
}
