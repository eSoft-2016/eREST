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

        public void RegistrarEmpleado(eREST_PERSONAS pPersona)
=======
        public void RegistrarEmpleado(eREST_PERSONA pPersona)
>>>>>>> origin/master
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            MantenimientoPersona nuevaPersona = new MantenimientoPersona();
            eREST_EMPLEADOS nuevoEmpleado = new eREST_EMPLEADOS();
            nuevaPersona.RegistrarPersona(pPersona);

            nuevoEmpleado.EMP_FK_PERSONA = pPersona.PER_PK_PERSONA;
            nuevoEmpleado.EMP_FK_TIPOEMPLEADO = 1;
            var diaYhora = DateTime.Now;
            nuevoEmpleado.EMP_FECHAREGISTRO = diaYhora.Date;

<<<<<<< HEAD
            dc.eREST_EMPLEADOS.InsertOnSubmit(nuevoEmpleado);
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

=======
            dc.eREST_EMPLEADOs.InsertOnSubmit(nuevoEmpleado);
>>>>>>> origin/master
            dc.SubmitChanges();
        }
    }
}