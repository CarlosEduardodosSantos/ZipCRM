using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Web.Framework.Infrastructure;
using VipCRM.Web.Framework.UI.toastr;
using VipCRM.Web.Infrastructure;

namespace VipCRM.Web.Controllers
{
    [Authorize]
    public class OcorrenciaController : MessageControllerBase
    {
        private readonly IOcorrenciaAppService _appServer;
        private readonly IOcorrenciaImagemAppService _ocorrenciaImagemAppService;
        public OcorrenciaController(IOcorrenciaAppService appService, IOcorrenciaImagemAppService ocorrenciaImagemAppService)
        {
            _appServer = appService;
            _ocorrenciaImagemAppService = ocorrenciaImagemAppService;
        }

        // GET: Ocorrencia
        public ActionResult Index()
        {
            int usuarioId = int.Parse(((UserIdentity)User.Identity).UsuarioId);
            return View(_appServer.ObterOcorrenciasPorUsuario(usuarioId));


        }

        public ActionResult Pesquisa(string pesquisa)
        {
            int usuarioId = int.Parse(((UserIdentity)User.Identity).UsuarioId);
            return View(_appServer.ObterOcorrenciasPorPesquisa(usuarioId, pesquisa));


        }

        public ActionResult Iniciada()
        {
            int usuarioId = int.Parse(((UserIdentity)User.Identity).UsuarioId);
            return View(_appServer.ObterOcorrenciasPorUsuarioIniciada(usuarioId));
        }

        public ActionResult Roteiro()
        {
            int usuarioId = int.Parse(((UserIdentity)User.Identity).UsuarioId);
            return View(_appServer.ObterOcorrenciasPorUsuarioRoteiro(usuarioId));
        }

        public ActionResult Finalizadas(int dias)
        {
            int usuarioId = int.Parse(((UserIdentity)User.Identity).UsuarioId);
            return View(_appServer.ObterOcorrenciasPorUsuarioFinalizadas(usuarioId, dias));
        }
        public ActionResult FinalizadasAdmin(int dias)
        {
            return View(_appServer.ObterOcorrenciasPorUsuarioFinalizadas(0, dias));
        }
        public ActionResult Details(int id)
        {
            var model = _appServer.ObterOcorrenciaId(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            model.Problema = RemoveRTFFormatting(model.Problema);
            model.Solucao = RemoveRTFFormatting(model.Solucao);
            return View(model);
        }

        public ActionResult Iniciar(int id)
        {
            var model = _appServer.ObterOcorrenciaId(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            model.DataInicioVip = DateTime.Now;
            return View(model);
        }
        [HttpPost]
        public ActionResult Iniciar(OcorrenciaViewModel model)
        {

            //AddToastMessage("Configurações", "Para iniciar a ocorrência use o eRat Android!", ToastType.Info);
            //return RedirectToAction("Iniciar");

            int usuarioId = int.Parse(((UserIdentity)User.Identity).UsuarioId);
            if (!_appServer.ExisteOcorrenciasIniciadaNaoFinalizadas(usuarioId))
            {
                var iniciaOcorrencia = new IniciaOcorrenciaViewModel()
                {
                    OcorrenciaId = model.OcorrenciaId,
                    UsuarioId = usuarioId,
                    Latitude = "",
                    Longitude = ""
                };
                var result = _appServer.IniciarOcorrencia(iniciaOcorrencia);
                AddToastMessage("Configurações", "Ocorrência iniciado com sucesso!", ToastType.Success);
                return RedirectToAction("Iniciada");
            }
            else
            {
                AddToastMessage("Configurações", "Existem ocorrencias sem finalizar!", ToastType.Success);
                return RedirectToAction("Iniciar");
            }
            
        }

        public ActionResult Finalizar(int id)
        {
            var model = _appServer.ObterOcorrenciaId(id);
            model.DataFimVip = DateTime.Now;
            return View(model);
        }
        [HttpPost]
        public ActionResult Finalizar(OcorrenciaViewModel model, HttpPostedFileBase uploadFile)
        {

            //AddToastMessage("Configurações", "Para iniciar a ocorrência use o eRat Android!", ToastType.Info);
            //return RedirectToAction("Iniciar");
            //Luciano solicitou para que não deixasse finalizar/iniciar a ocorrencia pelo site e sim apenas pelo mobile
            
            if (ModelState.IsValid)
            {
                int usuarioId = int.Parse(((UserIdentity)User.Identity).UsuarioId);

                var image = ImageToBase64(uploadFile, ImageFormat.Jpeg);

                var finalizaOcorrencia  = new FinalizaOcorrenciaViewModel()
                {
                    OcorrenciaId = model.OcorrenciaId,
                    UsuarioId = usuarioId,
                    NumeroRat = model.Rat,
                   Solucao = model.Solucao,
                   ImagemRat = image,
                   Pendencia = model.IsPendente
                };

                var result = _appServer.FinalizaOcorrencia(finalizaOcorrencia);

                AddToastMessage("Configurações", "Ocorrência finalizada com sucesso!", ToastType.Success);
                return RedirectToAction("Roteiro");
            }
            return View(model);
        }

        public JsonResult ImageResult(int ocorrenciaId)
        {
            var imagens = _ocorrenciaImagemAppService.ObterPorOcorrencia(ocorrenciaId).FirstOrDefault();
            if (imagens == null) imagens = new OcorrenciaImagemViewModel();

            return Json(imagens, JsonRequestBehavior.AllowGet);
        }

        public string ImageToBase64(HttpPostedFileBase file, ImageFormat format)
        {
            if (file == null) return "";

            using (MemoryStream ms = new MemoryStream())
            {
                var image = Image.FromStream(file.InputStream);
                Bitmap newImage = new Bitmap(640, 480, PixelFormat.Format24bppRgb);


                using (Graphics graphics = Graphics.FromImage(newImage))
                {
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.DrawImage(image, 0, 0, 640, 480);
                }

                // Create an Encoder object for the Quality parameter.
                Encoder encoder = Encoder.Quality;

                // Create an EncoderParameters object. 
                EncoderParameters encoderParameters = new EncoderParameters(1);

                // Save the image as a JPEG file with quality level.
                EncoderParameter encoderParameter = new EncoderParameter(encoder, 1);
                encoderParameters.Param[0] = encoderParameter;
                newImage.Save(ms, ImageFormat.Jpeg);



                //Parte antiga abaixo


                // Convert Image to byte[]
                //if (image != null)
                //    image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        private string RemoveRTFFormatting(string rtfContent)
        {
            if (string.IsNullOrEmpty(rtfContent)) return "";

            rtfContent = rtfContent.Trim();


            Regex rtfRegEx = new Regex("({\\\\)(.+?)(})|(\\\\)(.+?)(\\b)",
                RegexOptions.IgnoreCase
                | RegexOptions.Multiline
                | RegexOptions.Singleline
                | RegexOptions.ExplicitCapture
                | RegexOptions.IgnorePatternWhitespace
                | RegexOptions.Compiled
                );
            string output = rtfRegEx.Replace(rtfContent, string.Empty);
            output = Regex.Replace(output, @"\}", string.Empty); //replacing the remaining braces


            return output.Remove(output.Length - 1); //to trim last char (line end)
        }
    }
}