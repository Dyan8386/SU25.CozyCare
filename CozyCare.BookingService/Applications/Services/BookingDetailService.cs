using AutoMapper;
using CozyCare.BookingService.Applications.Interfaces;
using CozyCare.BookingService.Domain.Entities;
using CozyCare.BookingService.DTOs.BookingDetails;
using CozyCare.BookingService.Infrastructure;
using CozyCare.SharedKernel.Base;

namespace CozyCare.BookingService.Applications.Services
{
	public class BookingDetailService : IBookingDetailService
	{
		private readonly IBookingUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public BookingDetailService(IBookingUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<BaseResponse<IEnumerable<BDetailResponse>>> GetAllBookingDetailsAsync()
		{
			var bookingDetails = await _unitOfWork.BookingDetails.GetAllAsync();
			var response = _mapper.Map<IEnumerable<BDetailResponse>>(bookingDetails);
			return BaseResponse<IEnumerable<BDetailResponse>>.OkResponse(response);
		}

		public async Task<BaseResponse<BDetailResponse>> GetBookingDetailByIdAsync(int id)
		{
			var bookingDetail = await _unitOfWork.BookingDetails.GetByIdAsync(id);
			if (bookingDetail == null)
			{
				return BaseResponse<BDetailResponse>.NotFoundResponse($"Booking detail with ID {id} not found.");
			}
			var response = _mapper.Map<BDetailResponse>(bookingDetail);
			return BaseResponse<BDetailResponse>.OkResponse(response);
		}

		public async Task<BaseResponse<BDetailResponse>> CreateBookingDetailAsync(BDetailRequest request)
		{
			var bookingDetail = _mapper.Map<BookingDetail>(request);
			await _unitOfWork.BookingDetails.AddAsync(bookingDetail);
			await _unitOfWork.SaveChangesAsync();
			var response = _mapper.Map<BDetailResponse>(bookingDetail);
			return BaseResponse<BDetailResponse>.OkResponse(response);
		}

		public async Task<BaseResponse<string>> UpdateBookingDetailAsync(int id, BDetailRequest request)
		{
			var bookingDetail = await _unitOfWork.BookingDetails.GetByIdAsync(id);
			if (bookingDetail == null)
			{
				return BaseResponse<string>.NotFoundResponse($"Booking detail with ID {id} not found.");
			}
			_mapper.Map(request, bookingDetail);
			_unitOfWork.BookingDetails.Update(bookingDetail);
			await _unitOfWork.SaveChangesAsync();
			return BaseResponse<string>.OkResponse("Booking detail updated successfully.");
		}

		public async Task<BaseResponse<string>> DeleteBookingDetailAsync(int id)
		{
			var bookingDetail = await _unitOfWork.BookingDetails.GetByIdAsync(id);
			if (bookingDetail == null)
			{
				return BaseResponse<string>.NotFoundResponse($"Booking detail with ID {id} not found.");
			}
			_unitOfWork.BookingDetails.Delete(bookingDetail);
			await _unitOfWork.SaveChangesAsync();
			return BaseResponse<string>.OkResponse("Booking detail deleted successfully.");
		}
	}
}
