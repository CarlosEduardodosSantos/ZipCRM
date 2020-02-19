using System.Collections.Generic;
using VipCRM.Application.MVC.ViewModels;

namespace VipCRM.Application.MVC.Interface
{
    public interface IOcorrenciaImagemAppService
    {
        OcorrenciaImagemViewModel ObterPorId(int id);
        IEnumerable<OcorrenciaImagemViewModel> ObterPorOcorrencia(int ocorrenciaId);
    }
}