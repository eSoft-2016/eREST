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
<<<<<<< HEAD
        public void RegistrarEmpleado(eREST_PERSONA pPersona)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            MantenimientoPersona nuevaPersona = new MantenimientoPersona();
            eREST_EMPLEADO nuevoEmpleado = new eREST_EMPLEADO();
            nuevaPersona.RegistrarPersona(pPersona);

            nuevoEmpleado.EMP_FK_PERSONA = pPersona.PER_PK_PERSONA;
            nuevoEmpleado.EMP_FK_TIPOEMPLEADO = 1;
            var diaYhora = DateTime.Now;
            nuevoEmpleado.EMP_FECHAREGISTRO = diaYhora.Date;

            dc.eREST_EMPLEADOs.InsertOnSubmit(nuevoEmpleado);
=======
        public void RegistrarEmpleado(eREST_PERSONA pPersona, eREST_EMPLEADO pEmpleado)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            MantenimientoPersona nuevaPersona = new MantenimientoPersona();
            nuevaPersona.RegistrarPersona(pPersona);

            pEmpleado.EMP_FK_PERSONA = pPersona.PER_PK_PERSONA;
            pEmpleado.EMP_FK_TIPOEMPLEADO = 1;
            var diaYhora = DateTime.Now;
            pEmpleado.EMP_FECHAREGISTRO = diaYhora.Date;

            dc.eREST_EMPLEADOs.InsertOnSubmit(pEmpleado);
>>>>>>> origin/master
            dc.SubmitChanges();
        }
    }
}
