using eREST.BL.Empleado;
using eREST.BL.Persona;
using eREST.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace eREST.ModalWindows.MaintenanceWindows
{
    /// <summary>
    /// Lógica de interacción para wmDireccion.xaml
    /// </summary>
    public partial class wmDireccion : Window
    {
        #region Global Variables

        bool registrar = true;
        eREST_spDireccionesResult Direccion = new eREST_spDireccionesResult();
        eREST_PERSONAS Persona = new eREST_PERSONAS();
        MantenimientoPersona blaMant = new MantenimientoPersona();

        int error = 0;
        #endregion
        public wmDireccion(eREST_PERSONAS pPersona)
        {
            InitializeComponent();
            Persona = pPersona;
            ListarDirecciones();
        }
        #region Private Functions
        private void ListarDirecciones()
        {
            try
            {
                MantenimientoPersona blr = new MantenimientoPersona();
                List<eREST_spDireccionesResult> Lista = new List<eREST_spDireccionesResult>();
                Lista = blr.ListarDirecciones(Persona.PER_PK_PERSONA);
                if (Lista != null)
                {
                    dgDirecciones.ItemsSource = Lista;
                }
                else
                {
                    //ewMessage nwError = new ewMessage(ErrorMessages.errorGeneralList, UserMessages.titlePermissions);
                    //nwError.ShowDialog();
                    //if (nwError.DialogResult == true)
                    //    nwError.Close();
                }
            }
            catch
            {//(Exception ex)
                //ewMessage nwError = new ewMessage(ErrorMessages.errorGeneralList + ex.Message.ToString(), UserMessages.titlePermissions);
                //nwError.ShowDialog();
                //if (nwError.DialogResult == true)
                //    nwError.Close();
            }
        }
        #region Eventos privados
        

        private void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            ListarDirecciones();
            dgDirecciones.SelectedIndex = -1;
        }


        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            gridDireccion.Visibility = Visibility.Visible;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            gridDireccion.Visibility = Visibility.Visible;
        }

        private void dgEmpresas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                Direccion = ((DataGrid)sender).SelectedItem as eREST_spDireccionesResult;
                 
            }
            catch
            {//(Exception ex)
                //ewMessage nwError = new ewMessage(ErrorMessages.titlePermissions + ex.Message.ToString(), UserMessages.titlePermissions);
                //    nwError.ShowDialog();
                //    if (nwError.DialogResult == true)
                //        nwError.Close();
            }

        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //.EliminarEmpresa(Direccion);
            ListarDirecciones();
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {

            Persona =  blaMant.ConsultarPersona(Convert.ToInt32(Direccion.DIR_FK_PERSONA));
            wmDireccion ventis = new wmDireccion(Persona);
            ventis.ShowDialog();
        }

         
        #endregion

  

        #endregion
    }
}
