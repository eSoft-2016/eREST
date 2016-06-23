using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using eREST.BO;
using eREST.BL.Persona;

namespace eREST.BL.Empleado
{
    public class MantenimientoEmpleado
    {
        public void RegistrarEmpleado(eREST_EMPLEADOS pPersona) 
        {
            
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            dc.eREST_EMPLEADOS.InsertOnSubmit(pPersona);
            dc.SubmitChanges();
        }
        public void RegistrarEmpleado(eREST_PERSONAS pPersona, eREST_EMPLEADOS pEmpleado)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            MantenimientoPersona nuevaPersona = new MantenimientoPersona();
            nuevaPersona.RegistrarPersona(pPersona);

            pEmpleado.EMP_FK_PERSONA = pPersona.PER_PK_PERSONA;
            pEmpleado.EMP_FK_TIPOEMPLEADO = 1;
            var diaYhora = DateTime.Now;
            pEmpleado.EMP_FECHAREGISTRO = diaYhora.Date;

            dc.eREST_EMPLEADOS.InsertOnSubmit(pEmpleado); 
            dc.SubmitChanges();
        }
        public List<eREST_EMPLEADOS> ListarEmpresas()
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            List<eREST_EMPLEADOS> empresas = new List<eREST_EMPLEADOS>();
            empresas = (from c in dc.eREST_EMPLEADOS
                        select c).ToList();
            return empresas;
        }
        public List<eREST_spEmpleadosResult> ListarEmpleados()
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            List<eREST_spEmpleadosResult> empleados = dc.eREST_spEmpleados().ToList();
            return empleados;
        }
        public List<eREST_TIPOEMPLEADOS> ListarTipoEmpleados()
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            List<eREST_TIPOEMPLEADOS> empleados = (from c in dc.eREST_TIPOEMPLEADOS
                                                              select c).ToList();
            return empleados;
        }
        public eREST_TIPOEMPLEADOS ListarTipoEmpleado(int PK)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_TIPOEMPLEADOS empleados = (from c in dc.eREST_TIPOEMPLEADOS
                                             where c.TEM_PK_TIPOEMPLEADO == PK
                                             select c).First();
            return empleados;
        }
        public eREST_EMPLEADOS ConsultarEmpleado(int PK)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_EMPLEADOS empleados = (from c in dc.eREST_EMPLEADOS
                                             where c.EMP_FK_PERSONA == PK
                                             select c).First();
            return empleados;
        }
        public void EliminarEmpleado(int PK)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_PERSONAS Empleado = dc.eREST_PERSONAS.First(c => c.PER_PK_PERSONA == PK);
            Empleado.PER_ESTADO = 'I';
            dc.SubmitChanges();
        }
        public void EditarEmpleado(eREST_EMPLEADOS pPersona)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_EMPLEADOS rPersona = dc.eREST_EMPLEADOS.First(c => c.EMP_PK_EMPLEADO == pPersona.EMP_PK_EMPLEADO);
            rPersona.EMP_FK_TIPOEMPLEADO = pPersona.EMP_FK_TIPOEMPLEADO;
            dc.SubmitChanges();
        }
    }
}