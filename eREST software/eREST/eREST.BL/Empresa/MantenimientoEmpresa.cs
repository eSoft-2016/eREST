using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eREST.BO;


namespace eREST.BL.Empresa
{
    public class MantenimientoEmpresa
    {
        public void RegistrarEmpresa(eREST_EMPRESA pEmpresa)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            dc.eREST_EMPRESAs.InsertOnSubmit(pEmpresa);
            dc.SubmitChanges();
          
        }
        public eREST_EMPRESA ConsultarEmpresa(int EMP_PK_EMPRESA)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_EMPRESA modEMPRESA = dc.eREST_EMPRESAs.First(c => c.EMP_PK_EMPRESA == EMP_PK_EMPRESA);
            return modEMPRESA;
        }
        public List<eREST_EMPRESA> ListarEmpresas()
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            List<eREST_EMPRESA> empresas = new List<eREST_EMPRESA>();
            empresas = (from c in dc.eREST_EMPRESAs 
                         select c).ToList();
            return empresas;
        }
        public void EditarEmpresa(eREST_EMPRESA pEmpresa)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_EMPRESA Empresa = dc.eREST_EMPRESAs.First(c => c.EMP_PK_EMPRESA == pEmpresa.EMP_PK_EMPRESA);
            Empresa.EMP_PK_EMPRESA = pEmpresa.EMP_PK_EMPRESA; 
            Empresa.EMP_NOMBRE = pEmpresa.EMP_NOMBRE;
            Empresa.EMP_TELEFONO = pEmpresa.EMP_TELEFONO;
            Empresa.EMP_DIRECCION = pEmpresa.EMP_DIRECCION;
            dc.SubmitChanges();
        }
        public void EliminarEmpresa(eREST_EMPRESA pEmpresa)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_EMPRESA Empresa = dc.eREST_EMPRESAs.First(c => c.EMP_PK_EMPRESA == pEmpresa.EMP_PK_EMPRESA);
            dc.eREST_EMPRESAs.DeleteOnSubmit(Empresa);
            dc.SubmitChanges();
        }
    }
}

 
