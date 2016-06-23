using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eREST.BO;
namespace eREST.BL
{
   public class MantenimientoInsumo
    {
        public int RegistrarInsumo(eREST_INSUMOS pInsumo)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            dc.eREST_INSUMOS.InsertOnSubmit(pInsumo);
            dc.SubmitChanges();
            return pInsumo.INS_PK_INSUMO;

        }
        public void RegistrarDetInsumo(eREST_DETBODEGAS pDetInsumo)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
           
            dc.eREST_DETBODEGAS.InsertOnSubmit(pDetInsumo);
            dc.SubmitChanges();
        }
        public void EditarInsumo(eREST_INSUMOS pInsumo)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_INSUMOS insumo = dc.eREST_INSUMOS.FirstOrDefault(c => c.INS_PK_INSUMO == pInsumo.INS_PK_INSUMO);
            insumo.INS_NOMBRE = pInsumo.INS_NOMBRE;
            insumo.INS_DESCRIPCION = pInsumo.INS_DESCRIPCION;
            dc.SubmitChanges();
        }
        public void EditarDetInsumo(eREST_DETBODEGAS pDetInsumo)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_DETBODEGAS detinsumo = dc.eREST_DETBODEGAS.FirstOrDefault(c => c.DB_FK_INSUMO == pDetInsumo.DB_FK_INSUMO);
            detinsumo.DB_CANTIDAD = pDetInsumo.DB_CANTIDAD;
            detinsumo.DB_UMEDIDA = pDetInsumo.DB_UMEDIDA;
            dc.SubmitChanges();
        }
        public List<eREST_spListarInsumosResult> ListarInsumos(string pNombre)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            return dc.eREST_spListarInsumos(pNombre).ToList();
        }
        public eREST_spListarInsumosResult Insumo(string pNombre)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            return dc.eREST_spListarInsumos(pNombre).FirstOrDefault();
        }
        public eREST_BODEGAS ObtenerBodegas(int idBodega)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            return dc.eREST_BODEGAS.FirstOrDefault(c => c.BOD_PK_BODEGA == idBodega);
        }
        public eREST_INSUMOS ObtenerInsumo(int idInsumo)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            return dc.eREST_INSUMOS.FirstOrDefault(c => c.INS_PK_INSUMO == idInsumo);
        }
        public void CambiarEstado(int pidInsumo)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_INSUMOS insumo = dc.eREST_INSUMOS.FirstOrDefault(c => c.INS_PK_INSUMO == pidInsumo);
            if (insumo.INS_ESTADO == false)
            {
                insumo.INS_ESTADO = true;
            }
            else insumo.INS_ESTADO = false;
            dc.SubmitChanges();

        }
        public int ObtenerIdTipo(string pNombre)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_BODEGAS tipo = dc.eREST_BODEGAS.FirstOrDefault(c => c.BOD_NOMBRE == pNombre);
            return tipo.BOD_PK_BODEGA;
        }

        public List<string> ListarTipo()
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            return (from c in dc.eREST_BODEGAS select c.BOD_NOMBRE).ToList();
        }
        public string TipoPorId(int pidTipo)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_TIPOBODEGAS tipo = dc.eREST_TIPOBODEGAS.FirstOrDefault(c => c.TBO_PK_TIPOBODEGA == pidTipo);
            return tipo.TBO_NOMBRE;
        }
    }
}
