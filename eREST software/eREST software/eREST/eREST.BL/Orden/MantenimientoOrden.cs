using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using eREST.BO;

namespace eREST.BL
{
    public class MantenimientoOrden
    {
        public eREST_ORDENES AgregarOrden(eREST_ORDENES Orden)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            dc.eREST_ORDENES.InsertOnSubmit(Orden);
            dc.SubmitChanges();
            return Orden;
        }

        public List<eREST_ORDENES> ListarOrdens()
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            List<eREST_ORDENES> sect = new List<eREST_ORDENES>();
            sect = (from c in dc.eREST_ORDENES
                    where c.ORD_ESTADO == true
                    select c).ToList();
            return sect;
        }
       
        public void EditarOrden(eREST_ORDENES prOrden)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_ORDENES rOrden = dc.eREST_ORDENES.FirstOrDefault(c => c.ORD_PK_ORDEN == prOrden.ORD_PK_ORDEN);
            rOrden.ORD_TOTAL = prOrden.ORD_TOTAL;
            rOrden.ORD_IMPUESTO = prOrden.ORD_IMPUESTO;
            rOrden.ORD_ESTADO = prOrden.ORD_ESTADO;
            rOrden.ORD_FECHA = prOrden.ORD_FECHA;
            dc.SubmitChanges();
        }
    }
}
