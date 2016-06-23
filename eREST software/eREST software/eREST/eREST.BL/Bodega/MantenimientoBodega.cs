using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eREST.BO;
namespace eREST.BL
{
    public class MantenimientoBodega
    {
        public void RegistrarBodega(eREST_BODEGAS pBodega)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            dc.eREST_BODEGAS.InsertOnSubmit(pBodega);
            dc.SubmitChanges();

        }
        public void EditarBodega(eREST_BODEGAS pBodega)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_BODEGAS bodega = dc.eREST_BODEGAS.FirstOrDefault(c => c.BOD_PK_BODEGA == pBodega.BOD_PK_BODEGA);
            bodega.BOD_NOMBRE = pBodega.BOD_NOMBRE;
            bodega.BOD_LOCALIDAD = pBodega.BOD_LOCALIDAD;
            bodega.BOD_FK_TIPOBODEGA = pBodega.BOD_FK_TIPOBODEGA;
            dc.SubmitChanges();

        }
        public List<eREST_spListarBodegasResult> ListarBodegas(string pNombre)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            return dc.eREST_spListarBodegas(pNombre).ToList(); 
        }
        public eREST_BODEGAS ObtenerBodegas(int idBodega)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            return dc.eREST_BODEGAS.FirstOrDefault(c => c.BOD_PK_BODEGA == idBodega);
        }
        public void CambiarEstado(int pidBodega)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_BODEGAS bodega = dc.eREST_BODEGAS.FirstOrDefault(c => c.BOD_PK_BODEGA == pidBodega);
            if (bodega.BOD_ESTADO==false)
            {
                bodega.BOD_ESTADO = true;
            }else bodega.BOD_ESTADO = false;
            dc.SubmitChanges();
            
        }
        public int ObtenerIdTipo(string pNombre)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_TIPOBODEGAS tipo = dc.eREST_TIPOBODEGAS.FirstOrDefault(c => c.TBO_NOMBRE == pNombre);
            return tipo.TBO_PK_TIPOBODEGA;
        }

        public List<string> ListarTipo()
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            return (from c in dc.eREST_TIPOBODEGAS select c.TBO_NOMBRE).ToList();
        }
        public string TipoPorId(int pidTipo)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_TIPOBODEGAS tipo = dc.eREST_TIPOBODEGAS.FirstOrDefault(c => c.TBO_PK_TIPOBODEGA == pidTipo);
            return tipo.TBO_NOMBRE;
        }

    }
}
