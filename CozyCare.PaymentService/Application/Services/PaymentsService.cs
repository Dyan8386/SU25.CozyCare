using AutoMapper;
using CozyCare.PaymentService.Application.Interfaces;
using CozyCare.PaymentService.Domain.Entities;
using CozyCare.PaymentService.Infrastructure;
using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.PaymentService.Application.Services
{
    public class PaymentsService : IPaymentsService
    {
        private readonly IPaymentUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PaymentsService(IPaymentUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<BaseResponse<PaymentDto>> CreateAsync(CreatePaymentDto request)
        {
            var entity = _mapper.Map<Payment>(request);
            entity.createdDate = DateTime.UtcNow;

            await _uow.Payments.AddAsync(entity);
            await _uow.SaveChangesAsync();

            return BaseResponse<PaymentDto>.OkResponse(_mapper.Map<PaymentDto>(entity));
        }

        public async Task<BaseResponse<PaymentDto>> GetByIdAsync(int id)
        {
            var entity = await _uow.Payments.GetByIdAsync(id);
            if (entity == null)
                return BaseResponse<PaymentDto>.NotFoundResponse($"Payment #{id} not found");

            return BaseResponse<PaymentDto>.OkResponse(_mapper.Map<PaymentDto>(entity));
        }

        public async Task<BaseResponse<List<PaymentDto>>> GetAllAsync()
        {
            var list = await _uow.Payments.GetAllAsync();
            return BaseResponse<List<PaymentDto>>.OkResponse(
                list.Select(p => _mapper.Map<PaymentDto>(p)).ToList()
            );
        }
    }
}
