using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using eREST.BO;

namespace eREST.BL.Local
{
    public class MantenimientoLocal
    {

        /// <summary>
        /// Registrar la configuracion de mesas en el Local
        /// </summary>
        /// <param name="lista de mesas y barras"></param>
        public void AgregarMobiliario(eREST_MOBILIARIO Mobiliario)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            dc.eREST_MOBILIARIOs.InsertOnSubmit(Mobiliario);
            dc.SubmitChanges();
        }
    }
}