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
        public eREST_PERSONAS RegistrarPersona(eREST_PERSONAS pPersona)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            dc.eREST_PERSONAS.InsertOnSubmit(pPersona);
            dc.SubmitChanges();
            return pPersona;
        }
        public eREST_PERSONAS ConsultarPersona(int PK)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_PERSONAS Persona = dc.eREST_PERSONAS.First(c => c.PER_PK_PERSONA == PK);
            return Persona;
        }
        public void EditarPersona(eREST_PERSONAS pPersona)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_PERSONAS rPersona = dc.eREST_PERSONAS.FirstOrDefault(c => c.PER_PK_PERSONA == pPersona.PER_PK_PERSONA);
            rPersona.PER_PNOMBRE = pPersona.PER_PNOMBRE;
            rPersona.PER_SNOMBRE = pPersona.PER_SNOMBRE;
            rPersona.PER_PAPELLIDO = pPersona.PER_PAPELLIDO;
            rPersona.PER_SAPELLIDO = pPersona.PER_SAPELLIDO;
            rPersona.PER_CEDULA = pPersona.PER_CEDULA;
            rPersona.PER_ESTADO = pPersona.PER_ESTADO;
            rPersona.PER_ESTADOCIVIL = pPersona.PER_ESTADOCIVIL;
            rPersona.PER_GENERO = pPersona.PER_GENERO;
            rPersona.PER_HDELINCUENCIA = pPersona.PER_HDELINCUENCIA;
            rPersona.PER_NACIONALIDAD = pPersona.PER_NACIONALIDAD;
            dc.SubmitChanges();
        }
        public List<eREST_PROVINCIAS> ListarProvincias()
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            List<eREST_PROVINCIAS> Provincias = new List<eREST_PROVINCIAS>();
            Provincias = (from c in dc.eREST_PROVINCIAS
                        select c).ToList();
            return Provincias;
        }
        public List<eREST_CANTONES> ListarCantones(int PK)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            List<eREST_CANTONES> Cantones = new List<eREST_CANTONES>();
            Cantones = (from c in dc.eREST_CANTONES
                        where c.CAN_FK_PROVINCIA == PK
                          select c).ToList();
            return Cantones;
        }
        public List<eREST_DISTRITOS> ListarDistritos(int PK)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            List<eREST_DISTRITOS> Distritos = new List<eREST_DISTRITOS>();
            Distritos = (from c in dc.eREST_DISTRITOS
                        where c.DIS_FK_CANTON == PK
                        select c).ToList();
            return Distritos;
        }
        public List<eREST_spDireccionesResult> ListarDirecciones(int PK)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            List<eREST_spDireccionesResult> Direcciones = dc.eREST_spDirecciones(PK).ToList();
            return Direcciones;
        }
    }
}
