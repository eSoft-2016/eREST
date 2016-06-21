using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eREST.BO;
using eREST.BL.Empresa;
namespace eREST.BL.Sector
{
    public class MantenimientoSector
    {
        public void RegistrarSector(eREST_SECTORE pSector, eREST_EMPRESA pEmpresa)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            pSector.SEC_FK_EMPRESA = pEmpresa.EMP_PK_EMPRESA;
            dc.eREST_SECTOREs.InsertOnSubmit(pSector);
            dc.SubmitChanges();
        }
    }
}


 
