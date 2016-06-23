using eREST.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eREST.BL.Orden
{
    public class MantenimientoDetalleOrden
    {
        public void AgregarDetalleOrden(eREST_DETALLEORDENES Orden)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            dc.eREST_DETALLEORDENES.InsertOnSubmit(Orden);
            dc.SubmitChanges();
        }

        public List<eREST_DETALLEORDENES> ListarOrdens(int PK)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            List<eREST_DETALLEORDENES> sect = new List<eREST_DETALLEORDENES>();
            sect = (from c in dc.eREST_DETALLEORDENES
                    where c.DOR_FK_ORDEN == PK
                    select c).ToList();
            return sect;
        }

        public void EditarDetalleOrden(eREST_DETALLEORDENES prOrden)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_DETALLEORDENES rOrden = dc.eREST_DETALLEORDENES.FirstOrDefault(c => c.DOR_PK_DETALLEORDEN == prOrden.DOR_PK_DETALLEORDEN);
            rOrden.DOR_FK_PRODUCTO = prOrden.DOR_FK_PRODUCTO;
            rOrden.DOR_FK_ORDEN = prOrden.DOR_FK_ORDEN;
            rOrden.DOR_FK_EMPLEADO = prOrden.DOR_FK_EMPLEADO;
            rOrden.DOR_CANTIDAD = prOrden.DOR_CANTIDAD; 
            dc.SubmitChanges();
        }
    }
}
