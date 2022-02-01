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
    public class InventarioController : ApiController
    {
        ModelCliente db = new ModelCliente();
        G g = new G();
        public async Task<HttpResponseMessage> Get([FromUri] Filtros filtro)
        {
            try
            {

                var Inventario = db.Inventario.ToList();

                if (!string.IsNullOrEmpty(filtro.ListPrice))
                {
                    Inventario = Inventario.Where(a => a.ListaPrecio == filtro.ListPrice).ToList();
                }

                if (!string.IsNullOrEmpty(filtro.Categoria))
                {
                    Inventario = Inventario.Where(a => a.Categoria == filtro.Categoria).ToList();
                }


                 return Request.CreateResponse(HttpStatusCode.OK, Inventario);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("api/Inventario/Consultar")]
        public HttpResponseMessage GetOne([FromUri]int id)
        {
            try
            {



                var Inventario = db.Inventario.Where(a => a.id == id).FirstOrDefault();


                if (Inventario == null)
                {
                    throw new Exception("Este Inventario no se encuentra registrado");
                }

                return Request.CreateResponse(HttpStatusCode.OK, Inventario);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("api/Inventario/IngresarInventario")]
        public HttpResponseMessage GetIngresarInventario()
        {
            try
            {
                var conexion = g.DevuelveCadena(db);
                var SQL = db.Parametros.FirstOrDefault().SQLInventario;
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
                        var ItemCode = item["ItemCode"].ToString();
                        var LisPrice = item["ListaPrecio"].ToString();
                        if (db.Inventario.Where(a => a.ItemCode == ItemCode && a.ListaPrecio == LisPrice).FirstOrDefault() == null)
                        {
                            Inventario INV = new Inventario();
                            INV.ItemCode = item["ItemCode"].ToString();
                            INV.ItemName = item["ItemName"].ToString();
                            INV.Cabys = item["Cabys"].ToString();
                            INV.TipoIVA = item["TipoIVA"].ToString();
                            INV.WhsCode = item["Bodega"].ToString();
                            INV.Categoria = item["Categoria"].ToString();
                            INV.OnHand = Convert.ToDecimal(item["OnHand"].ToString());
                            INV.IsCommited = Convert.ToDecimal(item["IsCommited"].ToString());
                            INV.Stock = Convert.ToDecimal(item["Stock"].ToString());
                            INV.Precio = Convert.ToDecimal(item["Precio"].ToString());
                            INV.Currency = item["Moneda"].ToString();
                            INV.TipoCambio = Convert.ToDecimal(item["TipoCambio"].ToString());
                            INV.ListaPrecio = item["ListaPrecio"].ToString();
                            INV.Total = INV.Precio * INV.TipoCambio;
                            INV.FechaActualizacion = DateTime.Now;
                            INV.FechaActPrec = DateTime.Now;

                            db.Inventario.Add(INV);
                            db.SaveChanges();
                        }
                        else
                        {

                            Inventario INV = db.Inventario.Where(a => a.ItemCode == ItemCode && a.ListaPrecio == LisPrice).FirstOrDefault();
                            db.Entry(INV).State = EntityState.Modified;
                            INV.ItemCode = item["ItemCode"].ToString();
                            INV.ItemName = item["ItemName"].ToString();
                            INV.Cabys = item["Cabys"].ToString();
                            INV.TipoIVA = item["TipoIVA"].ToString();
                            INV.WhsCode = item["Bodega"].ToString();
                            INV.Categoria = item["Categoria"].ToString();
                            INV.OnHand = Convert.ToDecimal(item["OnHand"].ToString());
                            INV.IsCommited = Convert.ToDecimal(item["IsCommited"].ToString());
                            INV.Stock = Convert.ToDecimal(item["Stock"].ToString());
                            INV.Precio = Convert.ToDecimal(item["Precio"].ToString());
                            INV.Currency = item["Moneda"].ToString();
                            INV.TipoCambio = Convert.ToDecimal(item["TipoCambio"].ToString());
                            INV.Total = INV.Precio * INV.TipoCambio;
                            INV.ListaPrecio = item["ListaPrecio"].ToString();

                            INV.FechaActualizacion = DateTime.Now;
                            INV.FechaActPrec = DateTime.Now;
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

        

        [Route("api/Inventario/IngresarInventarioUno")] 
        public HttpResponseMessage GetIngresarInventarioUno([FromUri] string id, string lp)
        {
            try
            {
                var conexion = g.DevuelveCadena(db);
                var SQL = db.Parametros.FirstOrDefault().SQLInventario;
                SQL += " where t0.ItemCode ='" + id + "' and t2.PriceList='" +lp + "'";
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
                        var ItemCode = item["ItemCode"].ToString();
                        var LisPrice = item["ListaPrecio"].ToString();
                        if (db.Inventario.Where(a => a.ItemCode == ItemCode && a.ListaPrecio == LisPrice).FirstOrDefault() == null)
                        {
                            Inventario INV = new Inventario();
                            INV.ItemCode = item["ItemCode"].ToString();
                            INV.ItemName = item["ItemName"].ToString();
                            INV.Cabys = item["Cabys"].ToString();
                            INV.TipoIVA = item["TipoIVA"].ToString();
                            INV.WhsCode = item["Bodega"].ToString();
                            INV.Categoria = item["Categoria"].ToString();
                            INV.OnHand = Convert.ToDecimal(item["OnHand"].ToString());
                            INV.IsCommited = Convert.ToDecimal(item["IsCommited"].ToString());
                            INV.Stock = Convert.ToDecimal(item["Stock"].ToString());
                            INV.Precio = Convert.ToDecimal(item["Precio"].ToString());
                            INV.Currency = item["Moneda"].ToString();
                            INV.TipoCambio = Convert.ToDecimal(item["TipoCambio"].ToString());
                            INV.ListaPrecio = item["ListaPrecio"].ToString();
                            INV.Total = INV.Precio * INV.TipoCambio;
                            INV.FechaActualizacion = DateTime.Now;
                            INV.FechaActPrec = DateTime.Now;

                            db.Inventario.Add(INV);
                            db.SaveChanges();
                        }
                        else
                        {

                            Inventario INV = db.Inventario.Where(a => a.ItemCode == ItemCode && a.ListaPrecio == LisPrice).FirstOrDefault();
                            db.Entry(INV).State = EntityState.Modified;
                            INV.ItemCode = item["ItemCode"].ToString();
                            INV.ItemName = item["ItemName"].ToString();
                            INV.Cabys = item["Cabys"].ToString();
                            INV.TipoIVA = item["TipoIVA"].ToString();
                            INV.WhsCode = item["Bodega"].ToString();
                            INV.Categoria = item["Categoria"].ToString();
                            INV.OnHand = Convert.ToDecimal(item["OnHand"].ToString());
                            INV.IsCommited = Convert.ToDecimal(item["IsCommited"].ToString());
                            INV.Stock = Convert.ToDecimal(item["Stock"].ToString());
                            INV.Precio = Convert.ToDecimal(item["Precio"].ToString());
                            INV.Currency = item["Moneda"].ToString();
                            INV.TipoCambio = Convert.ToDecimal(item["TipoCambio"].ToString());
                            INV.Total = INV.Precio * INV.TipoCambio;
                            INV.ListaPrecio = item["ListaPrecio"].ToString();

                            INV.FechaActualizacion = DateTime.Now;
                            INV.FechaActPrec = DateTime.Now;
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

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Inventario item)
        {
            try
            {

                var Item = db.Inventario.Where(a => a.id == item.id).FirstOrDefault();

                if (Item == null)
                {
                    //Agregar cliente a SAP
                    var client = (SAPbobsCOM.Items)Conexion.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);
                    client.ItemCode = item.ItemCode;
                    client.ItemName = item.ItemName;
                    client.WhsInfo.WarehouseCode = item.WhsCode;
                    client.ItemsGroupCode = Convert.ToInt32( item.Categoria); //transformar en la insercion 
                    client.PriceList.Price = Convert.ToDouble(item.Precio.Value);
                    client.PriceList.Currency = item.Currency;
                    client.PriceList.BasePriceList = int.Parse(item.ListaPrecio);

                    client.UserFields.Fields.Item("U_CABYS").Value = item.Cabys;
                    client.UserFields.Fields.Item("U_TipoIVA").Value = item.TipoIVA;
                  
                    var respuest = client.Add();

                    if (respuest == 0)
                    {
                        
                    }
                    else
                    {
                        throw new Exception(Conexion.Company.GetLastErrorDescription());
                    }


                    Conexion.Desconectar();

                    //Termina Agregar Cliente a SAP

                    Item = new Inventario();
                    Item.ItemCode = item.ItemCode;
                    Item.ItemName = item.ItemName;
                    Item.Cabys = item.Cabys;
                    Item.TipoIVA = item.TipoIVA;
                    Item.WhsCode = item.WhsCode;
                    var categoria = Convert.ToInt32(item.Categoria);
                    Item.Categoria = db.Categorias.Where( a => a.idSAP == categoria).FirstOrDefault().Nombre;
                    Item.OnHand = 0;
                    Item.IsCommited = 0;
                    Item.Stock = 0;
                    Item.Precio = item.Precio;
                    Item.Currency = item.Currency;
                    Item.ListaPrecio = item.ListaPrecio;
                
                    Item.Total = Item.Precio * 1;
                    Item.FechaActualizacion = DateTime.Now;
                    Item.FechaActPrec = DateTime.Now;

                    db.Inventario.Add(Item);
                    db.SaveChanges();


                }
                else
                {
                    throw new Exception("Este item  YA existe");

                }


                return Request.CreateResponse(HttpStatusCode.OK, Item);
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


    }
}