using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WATickets.Models;
using WATickets.Models.Cliente;

namespace WATickets.Controllers
{
    [Authorize]
    public class ImpuestosController : ApiController
    {
        ModelCliente db = new ModelCliente();

        public async Task<HttpResponseMessage> Get([FromUri] Filtros filtro)
        {
            try
            {

                var Imp = db.Impuestos.ToList();

                if (!string.IsNullOrEmpty(filtro.Texto))
                {
                    Imp = Imp.Where(a => a.Nombre.ToUpper().Contains(filtro.Texto.ToUpper())).ToList();
                }



                return Request.CreateResponse(HttpStatusCode.OK, Imp);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("api/Impuestos/Consultar")]
        public HttpResponseMessage GetOne([FromUri]int id)
        {
            try
            {



                var Impuestos = db.Impuestos.Where(a => a.id == id).FirstOrDefault();


                if (Impuestos == null)
                {
                    throw new Exception("Este Impuesto no se encuentra registrado");
                }

                return Request.CreateResponse(HttpStatusCode.OK, Impuestos);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Impuestos imp)
        {
            try
            {


                var Imp = db.Impuestos.Where(a => a.id == imp.id).FirstOrDefault();

                if (Imp == null)
                {
                    Imp = new Impuestos();
                    Imp.Nombre = imp.Nombre;
                    Imp.idSAP = imp.idSAP;
                    Imp.Valor = imp.Valor;

                    db.Impuestos.Add(Imp);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Este Impuesto YA existe");
                }


                return Request.CreateResponse(HttpStatusCode.OK, Imp);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("api/Impuestos/Actualizar")]
        public HttpResponseMessage Put([FromBody] Impuestos imp)
        {
            try
            {


                var Imp = db.Impuestos.Where(a => a.id == imp.id).FirstOrDefault();


                if (Imp != null)
                {
                    db.Entry(Imp).State = EntityState.Modified;
                    Imp.Nombre = imp.Nombre;
                    Imp.idSAP = imp.idSAP;
                    Imp.Valor = imp.Valor;

                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Impuesto no existe");
                }

                return Request.CreateResponse(HttpStatusCode.OK, Imp);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        [HttpDelete]
        [Route("api/Impuestos/Eliminar")]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            try
            {


                var Imp = db.Impuestos.Where(a => a.id == id).FirstOrDefault();


                if (Imp != null)
                {


                    db.Impuestos.Remove(Imp);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Impuestos no existe");
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}