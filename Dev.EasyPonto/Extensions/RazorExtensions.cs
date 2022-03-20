using Dev.Business.Models.Funcionarios;
using System;
using System.Web;
using System.Web.Mvc;

namespace Dev.EasyPonto.Extensions
{
    public static class RazorExtensions
    {
        public static string FormatarDocumento(this WebViewPage page, TipoFuncionario tipoPessoa, string documento)
        {
            return tipoPessoa == TipoFuncionario.CLT
                ? Convert.ToUInt64(documento).ToString(@"000\.000\.000\-00")
                : Convert.ToUInt64(documento).ToString(@"00\.000\.000\/0000\-00");
        }

        public static bool ExibirNaURL(this WebViewPage value, Guid Id)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var urlTarget = urlHelper.Action("Edit", "Funcionarios", new { id = Id });
            var urlTarget2 = urlHelper.Action("ObterEndereco", "Funcionarios", new { id = Id });

            var urlEmUso = HttpContext.Current.Request.Path;

            return urlTarget == urlEmUso || urlTarget2 == urlEmUso;
        }
    }
}