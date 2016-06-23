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
        public void RegistrarEmpresa(eREST_EMPRESAS pEmpresa)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            dc.eREST_EMPRESAS.InsertOnSubmit(pEmpresa);
            dc.SubmitChanges();
          
        }
        public eREST_EMPRESAS ConsultarEmpresa(int EMP_PK_EMPRESA)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_EMPRESAS modEMPRESA = dc.eREST_EMPRESAS.First(c => c.EMP_PK_EMPRESA == EMP_PK_EMPRESA);
            return modEMPRESA;
        }
        public List<eREST_EMPRESAS> ListarEmpresas()
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            List<eREST_EMPRESAS> empresas = new List<eREST_EMPRESAS>();
            empresas = (from c in dc.eREST_EMPRESAS 
                         select c).ToList();
            return empresas;
        }
        public void EditarEmpresa(eREST_EMPRESAS pEmpresa)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_EMPRESAS Empresa = dc.eREST_EMPRESAS.First(c => c.EMP_PK_EMPRESA == pEmpresa.EMP_PK_EMPRESA);
            Empresa.EMP_PK_EMPRESA = pEmpresa.EMP_PK_EMPRESA; 
            Empresa.EMP_NOMBRE = pEmpresa.EMP_NOMBRE;
            Empresa.EMP_TELEFONO = pEmpresa.EMP_TELEFONO;
            Empresa.EMP_DIRECCION = pEmpresa.EMP_DIRECCION;
            dc.SubmitChanges();
        }
        public void EliminarEmpresa(eREST_EMPRESAS pEmpresa)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_EMPRESAS Empresa = dc.eREST_EMPRESAS.First(c => c.EMP_PK_EMPRESA == pEmpresa.EMP_PK_EMPRESA);
            dc.eREST_EMPRESAS.DeleteOnSubmit(Empresa);
            dc.SubmitChanges();
        }
    }
}

 
