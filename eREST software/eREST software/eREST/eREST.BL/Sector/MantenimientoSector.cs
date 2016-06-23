using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eREST.BO;
using eREST.BL.Sector;
namespace eREST.BL.Sector
{
    public class MantenimientoSector
    {
        public void RegistrarSector(eREST_SECTORES pSector)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            pSector.SEC_FK_EMPRESA = 1;
            dc.eREST_SECTORES.InsertOnSubmit(pSector);
            dc.SubmitChanges();
        }
        public void EditarSector(eREST_SECTORES pSector)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_SECTORES Sector = dc.eREST_SECTORES.First(c => c.SEC_PK_SECTOR == pSector.SEC_PK_SECTOR); 
            Sector.SEC_NOMBRE = pSector.SEC_NOMBRE;
            Sector.SEC_DESCRIPCION = pSector.SEC_DESCRIPCION; 
            dc.SubmitChanges();
        }
        public List<eREST_SECTORES> ListarSectores()
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            List<eREST_SECTORES> sect = new List<eREST_SECTORES>();
            sect = (from c in dc.eREST_SECTORES
                        select c).ToList();
            return sect;
        }
        public void EliminarSector(eREST_SECTORES pSector)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_SECTORES Sector = dc.eREST_SECTORES.First(c => c.SEC_PK_SECTOR == pSector.SEC_PK_SECTOR);
            dc.eREST_SECTORES.DeleteOnSubmit(Sector);
            dc.SubmitChanges();
        }
    }
}


 
