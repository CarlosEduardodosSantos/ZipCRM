using System;
using System.Collections.Generic;
using VipCRM.Core.Domain.Entites;
using VipCRM.Core.Domain.ValueObjects;

namespace VipCRM.Data.Interface.Repositories
{
    public interface IAlertaRepository
    {
        ValidationResult Adicionar(Alerta alerta);
        ValidationResult ConfirmarLeitura(Alerta alerta);
        Alerta ObterPorId(Guid id);
        IEnumerable<Alerta> ObterPendentes();
        IEnumerable<Alerta> ObterPorUsuarioId(int usuarioId);
    }
}