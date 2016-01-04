﻿using System;
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
            dc.SubmitChanges();
        }
    }
}
