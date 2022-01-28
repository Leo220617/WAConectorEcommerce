using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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
     
    public class ClientesController : ApiController
    {
        ModelCliente db = new ModelCliente();
        G g = new G();
        public async Task<HttpResponseMessage> Get([FromUri] Filtros filtro)
        {
            try
            {

                var Clientes = db.Clientes.ToList();

                if (!string.IsNullOrEmpty(filtro.Texto))
                {
                    Clientes = Clientes.Where(a => a.CardName.ToUpper().Contains(filtro.Texto.ToUpper())).ToList();
                }



                return Request.CreateResponse(HttpStatusCode.OK, Clientes);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        [Route("api/Clientes/IngresarClientes")]
        public HttpResponseMessage GetInsertarClientes()
        {
            try
            {
                var Datos = db.ConexionSAP.FirstOrDefault();


                var conexion = g.DevuelveCadena(db); //"server=" + Datos.ServerSQL + "; database=" + Datos.SQLBD + "; uid=" + Datos.SQLUser + "; pwd=" + Datos.SQLPass + ";";
                var SQL = db.Parametros.FirstOrDefault().SQLCliente;

                SqlConnection Cn = new SqlConnection(conexion);
                SqlCommand Cmd = new SqlCommand(SQL, Cn);
                SqlDataAdapter Da = new SqlDataAdapter(Cmd);
                DataSet Ds = new DataSet();
                Cn.Open();
                Da.Fill(Ds, "Detalle");

                foreach (DataRow item in Ds.Tables["Detalle"].Rows)
                {

                    try
                    {

                        var CardCode = item["CardCode"].ToString();
                        if (db.Clientes.Where(a => a.CardCode == CardCode).FirstOrDefault() == null)
                        {
                            Clientes Cliente = new Clientes();
                            Cliente.CardCode = item["CardCode"].ToString();
                            Cliente.CardName = item["CardName"].ToString();
                            Cliente.Cedula = item["Cedula"].ToString();
                            Cliente.Direccion = item["Direccion"].ToString();
                            Cliente.Email = item["Email"].ToString();
                            Cliente.Telefono = item["Telefono"].ToString();
                            Cliente.ListaPrecio = item["ListaPrecio"].ToString();

                            db.Clientes.Add(Cliente);
                            db.SaveChanges();
                        }
                        else
                        {
                            Clientes Cliente = db.Clientes.Where(a => a.CardCode == CardCode).FirstOrDefault();
                            db.Entry(Cliente).State = EntityState.Modified;
                            Cliente.CardCode = item["CardCode"].ToString();
                            Cliente.CardName = item["CardName"].ToString();
                            Cliente.Cedula = item["Cedula"].ToString();
                            Cliente.Direccion = item["Direccion"].ToString();
                            Cliente.Email = item["Email"].ToString();
                            Cliente.Telefono = item["Telefono"].ToString();
                            Cliente.ListaPrecio = item["ListaPrecio"].ToString();

                            db.SaveChanges();
                        }
                    }
                    catch (Exception ex1)
                    {
                        BitacoraErrores be = new BitacoraErrores();

                        be.Descripcion = ex1.Message;
                        be.StackTrace = ex1.StackTrace;
                        be.Fecha = DateTime.Now;

                        db.BitacoraErrores.Add(be);
                        db.SaveChanges();

                    }

                    
                }

                Cn.Close();



                return Request.CreateResponse(HttpStatusCode.OK, "");
            }
            catch (Exception ex)
            {
                BitacoraErrores be = new BitacoraErrores();

                be.Descripcion = ex.Message;
                be.StackTrace = ex.StackTrace;
                be.Fecha = DateTime.Now;

                db.BitacoraErrores.Add(be);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }



        [Route("api/Clientes/IngresarClientesUno")]
        public HttpResponseMessage GetInsertarClientesUno([FromUri]Filtros filtro)
        {
            try
            {
                var Datos = db.ConexionSAP.FirstOrDefault();


                var conexion = "server=" + Datos.ServerSQL + "; database=" + Datos.SQLBD + "; uid=" + Datos.SQLUser + "; pwd=" + Datos.SQLPass + ";";
                var SQL = db.Parametros.FirstOrDefault().SQLCliente;

                 
                    if(!string.IsNullOrEmpty(filtro.CardCode))
                    {
                        SQL += " and t0.CardCode='" + filtro.CardCode+ "'";
                    }
                    else if(!string.IsNullOrEmpty(filtro.CardName))
                    {
                        SQL += " and t0.CardName like '%" + filtro.CardName + "%'" ;

                    }else
                {
                    throw new Exception("No se digito un filtro valido");
                }
                 

                SqlConnection Cn = new SqlConnection(conexion);
                SqlCommand Cmd = new SqlCommand(SQL, Cn);
                SqlDataAdapter Da = new SqlDataAdapter(Cmd);
                DataSet Ds = new DataSet();
                Cn.Open();
                Da.Fill(Ds, "Detalle");

                foreach (DataRow item in Ds.Tables["Detalle"].Rows)
                {

                    try
                    {

                        var CardCode = item["CardCode"].ToString();
                        if (db.Clientes.Where(a => a.CardCode == CardCode).FirstOrDefault() == null)
                        {
                            Clientes Cliente = new Clientes();
                            Cliente.CardCode = item["CardCode"].ToString();
                            Cliente.CardName = item["CardName"].ToString();
                            Cliente.Cedula = item["Cedula"].ToString();
                            Cliente.Direccion = item["Direccion"].ToString();
                            Cliente.Email = item["Email"].ToString();
                            Cliente.Telefono = item["Telefono"].ToString();
                            Cliente.ListaPrecio = item["ListaPrecio"].ToString();


                            db.Clientes.Add(Cliente);
                            db.SaveChanges();
                        }
                        else
                        {
                            Clientes Cliente = db.Clientes.Where(a => a.CardCode == CardCode).FirstOrDefault();
                            db.Entry(Cliente).State = EntityState.Modified;
                            Cliente.CardCode = item["CardCode"].ToString();
                            Cliente.CardName = item["CardName"].ToString();
                            Cliente.Cedula = item["Cedula"].ToString();
                            Cliente.Direccion = item["Direccion"].ToString();
                            Cliente.Email = item["Email"].ToString();
                            Cliente.Telefono = item["Telefono"].ToString();
                            Cliente.ListaPrecio = item["ListaPrecio"].ToString();

                            db.SaveChanges();
                        }
                    }
                    catch (Exception ex1)
                    {
                        BitacoraErrores be = new BitacoraErrores();

                        be.Descripcion = ex1.Message;
                        be.StackTrace = ex1.StackTrace;
                        be.Fecha = DateTime.Now;

                        db.BitacoraErrores.Add(be);
                        db.SaveChanges();

                    }


                }

                Cn.Close();



                return Request.CreateResponse(HttpStatusCode.OK, "");
            }
            catch (Exception ex)
            {
                BitacoraErrores be = new BitacoraErrores();

                be.Descripcion = ex.Message;
                be.StackTrace = ex.StackTrace;
                be.Fecha = DateTime.Now;

                db.BitacoraErrores.Add(be);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("api/Clientes/Consultar")]
        public HttpResponseMessage GetOne([FromUri]int id)
        {
            try
            {



                var Cliente = db.Clientes.Where(a => a.id == id).FirstOrDefault();


                if (Cliente == null)
                {
                    throw new Exception("Este Cliente no se encuentra registrado");
                }

                return Request.CreateResponse(HttpStatusCode.OK, Cliente);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Clientes cliente)
        {
            try
            {

                var Parametros = db.Parametros.FirstOrDefault();
                var Cliente = db.Clientes.Where(a => a.id == cliente.id).FirstOrDefault();

                if (Cliente == null)
                {

                    var cardCode = "";
                    //Agregar cliente a SAP
                    var client = (SAPbobsCOM.BusinessPartners)Conexion.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                    client.CardCode = cliente.CardCode;
                    client.CardName = cliente.CardName;
                    client.CardType = SAPbobsCOM.BoCardTypes.cCustomer;
                    client.FederalTaxID = cliente.Cedula;
                    client.EmailAddress = cliente.Email;
                    client.Phone1 = cliente.Telefono;
                    client.Address = cliente.Direccion;
                    client.Valid = SAPbobsCOM.BoYesNoEnum.tYES;
                    client.Currency = "##";
                    client.Series = Parametros.SeriesClientes;
                    client.GroupCode = Parametros.GroupCodeClientes;
                    client.PriceListNum = int.Parse(cliente.ListaPrecio);

                    var respuest = client.Add();

                    if(respuest == 0)
                    {
                        cardCode = Conexion.Company.GetNewObjectKey();
                        cliente.CardCode = cardCode;
                    }
                    else
                    {
                        throw new Exception(Conexion.Company.GetLastErrorDescription());
                    }


                    Conexion.Desconectar();

                    //Termina Agregar Cliente a SAP


                    Cliente = new Clientes();
                    Cliente.CardCode = cliente.CardCode;
                    Cliente.CardName = cliente.CardName;
                    Cliente.Cedula = cliente.Cedula;
                    Cliente.Direccion = cliente.Direccion;
                    Cliente.Email = cliente.Email;
                    Cliente.Telefono = cliente.Telefono;
                    Cliente.ListaPrecio = cliente.ListaPrecio;

                    db.Clientes.Add(Cliente);
                    db.SaveChanges();




                }
                else
                {
                    throw new Exception("Este Cliente  YA existe");
                }


                return Request.CreateResponse(HttpStatusCode.OK, Cliente);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("api/Clientes/Actualizar")]
        public HttpResponseMessage Put([FromBody] Clientes cliente)
        {
            try
            {


                var Cliente = db.Clientes.Where(a => a.id == cliente.id).FirstOrDefault();

                if (Cliente != null)
                {
                    db.Entry(Cliente).State = EntityState.Modified;
                    Cliente.CardCode = cliente.CardCode;
                    Cliente.CardName = cliente.CardName;
                    Cliente.Cedula = cliente.Cedula;
                    Cliente.Direccion = cliente.Direccion;
                    Cliente.Email = cliente.Email;
                    Cliente.Telefono = cliente.Telefono;

                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Cliente no existe");
                }

                return Request.CreateResponse(HttpStatusCode.OK, Cliente);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("api/Clientes/Eliminar")]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            try
            {


                var Cliente = db.Clientes.Where(a => a.id == id).FirstOrDefault();


                if (Cliente != null)
                {


                    db.Clientes.Remove(Cliente);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Cliente no existe");
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