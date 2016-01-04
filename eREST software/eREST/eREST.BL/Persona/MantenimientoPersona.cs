using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using eREST.BO;

namespace eREST.BL.Persona
{
    public class MantenimientoPersona
    {
        public void RegistrarPersona(eREST_PERSONA pPersona)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            dc.eREST_PERSONAs.InsertOnSubmit(pPersona);
            dc.SubmitChanges();
        }
    }
}
