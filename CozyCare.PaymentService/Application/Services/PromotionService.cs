using AutoMapper;
using CozyCare.PaymentService.Application.Interfaces;
using CozyCare.PaymentService.Domain.Entities;
using CozyCare.PaymentService.Infrastructure;
using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.PaymentService.Application.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPaymentUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PromotionService(IPaymentUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<BaseResponse<PromotionDto>> CreateAsync(CreatePromotionDto request)
        {
            var existing = await _uow.Promotions.GetByIdAsync(request.Code);
            if (existing != null)
                return BaseResponse<PromotionDto>.ConflictResponse($"Code '{request.Code}' already exists");

            var entity = _mapper.Map<Promotion>(request);
            entity.createdDate = DateTime.UtcNow;

            await _uow.Promotions.AddAsync(entity);
            await _uow.SaveChangesAsync();

            return BaseResponse<PromotionDto>.OkResponse(_mapper.Map<PromotionDto>(entity));
        }

        public async Task<BaseResponse<PromotionDto>> UpdateAsync(string code, UpdatePromotionDto request)
        {
            var entity = await _uow.Promotions.GetByIdAsync(code);
            if (entity == null)
                return BaseResponse<PromotionDto>.NotFoundResponse($"Promotion '{code}' not found");

            _mapper.Map(request, entity);

            await _uow.Promotions.UpdateAsync(entity);
            await _uow.SaveChangesAsync();

            return BaseResponse<PromotionDto>.OkResponse(_mapper.Map<PromotionDto>(entity));
        }

        public async Task<BaseResponse<PromotionDto>> GetByCodeAsync(string code)
        {
            var entity = await _uow.Promotions.GetByIdAsync(code);
            if (entity == null)
                return BaseResponse<PromotionDto>.NotFoundResponse($"Promotion '{code}' not found");

            return BaseResponse<PromotionDto>.OkResponse(_mapper.Map<PromotionDto>(entity));
        }

        public async Task<BaseResponse<List<PromotionDto>>> GetAllAsync()
        {
            var list = await _uow.Promotions.GetAllAsync();
            return BaseResponse<List<PromotionDto>>.OkResponse(
                list.Select(p => _mapper.Map<PromotionDto>(p)).ToList()
            );
        }
    }
}
