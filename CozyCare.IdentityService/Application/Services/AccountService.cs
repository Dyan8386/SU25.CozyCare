using AutoMapper;
using CozyCare.IdentityService.Application.Interfaces;
using CozyCare.IdentityService.Domain.Entities;
using CozyCare.IdentityService.Infrastructure;
using CozyCare.SharedKernel.Base;
using CozyCare.SharedKernel.Utils;
using CozyCare.ViewModels.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CozyCare.IdentityService.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IIdentityUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AccountService(IIdentityUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<BaseResponse<AccountDto>> CreateAsync(CreateAccountRequestDto request)
        {
            var normalizedEmail = request.Email.ToLowerInvariant();
            var normalizedPhone = request.Phone?.Trim();

            // Check email exists
            var emailExists = await _uow.Accounts.Query()
                .AnyAsync(a => a.email.ToLower() == normalizedEmail);
            if (emailExists)
                return BaseResponse<AccountDto>.ErrorResponse("Email đã tồn tại");

            // Check phone exists
            if (!string.IsNullOrEmpty(normalizedPhone))
            {
                var phoneExists = await _uow.Accounts.Query()
                    .AnyAsync(a => a.phone != null && a.phone == normalizedPhone);
                if (phoneExists)
                    return BaseResponse<AccountDto>.ErrorResponse("Số điện thoại đã tồn tại");
            }

            var entity = _mapper.Map<Account>(request);
            entity.email = normalizedEmail;
            entity.phone = normalizedPhone;
            entity.password = PasswordHelper.HashPassword(request.Password);
            entity.createdDate = DateTime.UtcNow;
            entity.createdBy = request.CreatedBy;

            await _uow.Accounts.AddAsync(entity);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BaseResponse<AccountDto>.ErrorResponse($"Không thể tạo tài khoản: {ex.InnerException?.Message ?? ex.Message}");
            }

            return BaseResponse<AccountDto>.OkResponse(_mapper.Map<AccountDto>(entity));
        }

        public async Task<BaseResponse<string>> DeleteAsync(int id)
        {
            var entity = await _uow.Accounts.GetByIdAsync(id);
            if (entity == null)
                return BaseResponse<string>.NotFoundResponse("Tài khoản không tồn tại");

            await _uow.Accounts.DeleteAsync(entity);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BaseResponse<string>.ErrorResponse($"Không thể xoá tài khoản: {ex.InnerException?.Message ?? ex.Message}");
            }

            return BaseResponse<string>.OkResponse("Xoá thành công");
        }

        public async Task<BaseResponse<IEnumerable<AccountDto>>> GetAllAsync()
        {
            var list = await _uow.Accounts.GetAllAsync();
            var dtos = list.Select(_mapper.Map<AccountDto>);
            return BaseResponse<IEnumerable<AccountDto>>.OkResponse(dtos);
        }

        public async Task<BaseResponse<AccountDto>> GetByIdAsync(int id)
        {
            var account = await _uow.Accounts.GetByIdAsync(id);
            if (account == null)
                return BaseResponse<AccountDto>.NotFoundResponse("Tài khoản không tồn tại");

            return BaseResponse<AccountDto>.OkResponse(_mapper.Map<AccountDto>(account));
        }

        public async Task<BaseResponse<AccountDto>> UpdateAsync(int id, UpdateAccountRequestDto request)
        {
            var entity = await _uow.Accounts.GetByIdAsync(id);
            if (entity == null)
                return BaseResponse<AccountDto>.NotFoundResponse("Tài khoản không tồn tại");

            var normalizedPhone = request.Phone?.Trim();

            if (!string.IsNullOrEmpty(normalizedPhone) && normalizedPhone != entity.phone)
            {
                var phoneExists = await _uow.Accounts.Query()
                    .AnyAsync(a => a.phone != null && a.phone == normalizedPhone && a.accountId != id);
                if (phoneExists)
                    return BaseResponse<AccountDto>.ErrorResponse("Số điện thoại đã tồn tại");
            }

            _mapper.Map(request, entity);
            if (!string.IsNullOrEmpty(normalizedPhone))
                entity.phone = normalizedPhone;

            entity.updatedDate = DateTime.UtcNow;
            entity.updatedBy = request.UpdatedBy;

            _uow.Accounts.Update(entity);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BaseResponse<AccountDto>.ErrorResponse($"Không thể cập nhật tài khoản: {ex.InnerException?.Message ?? ex.Message}");
            }

            return BaseResponse<AccountDto>.OkResponse(_mapper.Map<AccountDto>(entity));
        }
    }
}
