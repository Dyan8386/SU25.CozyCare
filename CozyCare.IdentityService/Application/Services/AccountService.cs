using AutoMapper;
using CozyCare.IdentityService.Application.Interfaces;
using CozyCare.IdentityService.Domain.Entities;
using CozyCare.IdentityService.Infrastructure;
using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;

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
            var entity = _mapper.Map<Account>(request);
            entity.createdDate = DateTime.UtcNow;
            await _uow.Accounts.AddAsync(entity);
            await _uow.SaveChangesAsync();
            return BaseResponse<AccountDto>.OkResponse(_mapper.Map<AccountDto>(entity));
        }

        public async Task<BaseResponse<string>> DeleteAsync(int id)
        {
            var entity = await _uow.Accounts.GetByIdAsync(id);
            if (entity == null)
                return BaseResponse<string>.NotFoundResponse("Account not found");
            await _uow.Accounts.DeleteAsync(entity);
            await _uow.SaveChangesAsync();
            return BaseResponse<string>.OkResponse("Deleted successfully");
        }

        public async Task<BaseResponse<IEnumerable<AccountDto>>> GetAllAsync()
        {
            var list = await _uow.Accounts.GetAllAsync();
            var dtos = list.Select(a => _mapper.Map<AccountDto>(a));
            return BaseResponse<IEnumerable<AccountDto>>.OkResponse(dtos);
        }

        public async Task<BaseResponse<AccountDto>> GetByIdAsync(int id)
        {
            var account = await _uow.Accounts.GetByIdAsync(id);
            if (account == null)
                return BaseResponse<AccountDto>.NotFoundResponse("Account not found");
            return BaseResponse<AccountDto>.OkResponse(_mapper.Map<AccountDto>(account));
        }

        public async Task<BaseResponse<AccountDto>> UpdateAsync(int id, UpdateAccountRequestDto request)
        {
            var entity = await _uow.Accounts.GetByIdAsync(id);
            if (entity == null)
                return BaseResponse<AccountDto>.NotFoundResponse("Account not found");

            _mapper.Map(request, entity);
            entity.updatedDate = DateTime.UtcNow;
            _uow.Accounts.Update(entity);
            await _uow.SaveChangesAsync();
            return BaseResponse<AccountDto>.OkResponse(_mapper.Map<AccountDto>(entity));
        }
    }
}
