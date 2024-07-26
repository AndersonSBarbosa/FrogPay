using FrogPay.Domain.Entities;
using FrogPay.Repository.Interfaces;
using FrogPay.Services.Interfaces;

namespace FrogPay.Services.Services
{
    public class LojaService : ILojaService
    {
        private readonly ILojaRepository _lojaRepository;

        public LojaService(ILojaRepository lojaRepository)
        {
            _lojaRepository = lojaRepository;
        }

        public async Task CreateLojaAsync(Loja loja)
        {
            if (loja == null)
            {
                throw new ArgumentNullException(nameof(loja));
            }
            if (loja.Id == Guid.Empty)
            {
                loja.Id = Guid.NewGuid();
            }
            try
            {
                await _lojaRepository.CreateAsync(loja);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao criar.", ex);
            }
        }

        public async Task<Loja> GetLojaByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID inválido", nameof(id));
            }

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

        public async Task UpdateLojaAsync(Loja loja)
        {
            if (loja == null)
            {
                throw new ArgumentNullException(nameof(loja));
            }

            try
            {
                await _lojaRepository.UpdateAsync(loja);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao atualizar a loja.", ex);
            }
        }
    }
}
