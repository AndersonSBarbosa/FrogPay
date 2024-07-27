using AutoMapper;
using FrogPay.Core.Exceptions;
using FrogPay.Domain.Entities;
using FrogPay.Repository.Interfaces;
using FrogPay.Services.Interfaces;
using FrogPay.Services.ViewModels.Loja;

namespace FrogPay.Services.Services
{
    public class LojaService : ILojaService
    {
        private readonly ILojaRepository _lojaRepository;
        private readonly IMapper _mapper;

        public LojaService(ILojaRepository lojaRepository, IMapper mapper)
        {
            _lojaRepository = lojaRepository;
            _mapper=mapper;
        }

        public async Task<Loja> CreateLojaAsync(CreateLojaViewModel loja)
        {
            try
            {
                    var item = _mapper.Map<Loja>(loja);
                    var ItemCreated = await _lojaRepository.CreateAsync(item);
                    return ItemCreated;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao criar o registro de Loja.", ex);
            }
        }

        public async Task<Loja> GetLojaByIdAsync(long id)
        {
            try
            {
                var loja = await _lojaRepository.GetAsync(id);

                if (loja == null)
                {
                    throw new InvalidOperationException("Loja não encontrada.");
                }

                return loja;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao buscar a loja pelo ID", ex);
            }
        }

        public async Task<Loja> UpdateLojaAsync(UpdateLojaViewModel loja)
        {
            try
            {
                var itemExists = await GetLojaByIdAsync(loja.Id);

                if (itemExists == null)
                    throw new DomainExceptions("não existe Loja com esse ID informado!");

                var itemUpdate = await _lojaRepository.UpdateAsync(_mapper.Map<Loja>(loja));

                return itemUpdate;
            }
            catch (Exception ex)
            {
                // Pode modificar para apresentar um retorno de erro mais detalhado
                throw new ApplicationException("Erro ao atualizar a loja.", ex);
            }
        }
    }
}
