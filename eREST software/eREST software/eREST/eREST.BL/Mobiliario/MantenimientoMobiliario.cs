using eREST.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eREST.BL.Mobiliario
{
    public class MantenimientoMobiliario
    {
        public int ConsultarMoviliarionombre(string Nombre)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_TIPOMOBILIARIOS modTipoMobiliario = dc.eREST_TIPOMOBILIARIOS.First(c => c.TMO_NOMBRE == Nombre);
            return modTipoMobiliario.TMO_PK_TIPOMOBILIARIO;
        }
        public string ConsultarMoviliarioLlave(int llave)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_TIPOMOBILIARIOS modTipoMobiliario = dc.eREST_TIPOMOBILIARIOS.First(c => c.TMO_PK_TIPOMOBILIARIO == llave);
            return modTipoMobiliario.TMO_NOMBRE;
        }
    }
}
