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
        public void AgregarMobiliario(eREST_MOBILIARIOS Mobiliario)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            dc.eREST_MOBILIARIOS.InsertOnSubmit(Mobiliario);
            dc.SubmitChanges();
        }

        public List<eREST_MOBILIARIOS> ListarMobiliarios()
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            List<eREST_MOBILIARIOS> sect = new List<eREST_MOBILIARIOS>();
            sect = (from c in dc.eREST_MOBILIARIOS
                    where c.MOB_ESTADO==true
                    select c).ToList();
            return sect;
        }
        public bool ConsultarExistenciaDB(int Numero)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_MOBILIARIOS modMobiliario = dc.eREST_MOBILIARIOS.FirstOrDefault(c =>  c.MOB_NUMERO == Numero && c.MOB_ESTADO==true); 
            if (modMobiliario == null) return false;
            else return true;
        }
        public void EliminarExistenciaDB(int Numero)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_MOBILIARIOS modMobiliario = dc.eREST_MOBILIARIOS.FirstOrDefault(c => c.MOB_NUMERO == Numero && c.MOB_ESTADO == true);
            if (modMobiliario != null)
            {
                modMobiliario.MOB_ESTADO = false;
                dc.SubmitChanges();
            } 
        }
        public void EditarMobiliario(eREST_MOBILIARIOS prMobiliario)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_MOBILIARIOS rMobiliario = dc.eREST_MOBILIARIOS.FirstOrDefault(c => c.MOB_NUMERO == prMobiliario.MOB_NUMERO && c.MOB_ESTADO == true);
            rMobiliario.MOB_FK_SECTOR = prMobiliario.MOB_FK_SECTOR;
            rMobiliario.MOB_COORY = prMobiliario.MOB_COORY;
            rMobiliario.MOB_COORX = prMobiliario.MOB_COORX; 
            dc.SubmitChanges();
        }
        public void EliminarMobiliario(eREST_MOBILIARIOS prMobiliario)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_MOBILIARIOS rMobiliario = dc.eREST_MOBILIARIOS.First(c => c.MOB_PK_MOBILIARIO == prMobiliario.MOB_PK_MOBILIARIO);
            rMobiliario.MOB_ESTADO = false;
            dc.SubmitChanges();
        }
    }
}