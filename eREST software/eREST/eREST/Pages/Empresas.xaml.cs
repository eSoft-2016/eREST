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
using System.Windows.Navigation;
using System.Windows.Shapes;

using eREST.BL.Empresa;
using eREST.BO;
using System.Collections.ObjectModel;
using eREST.ModalWindows.MaintenanceWindows;

namespace eREST.Pages
{
    /// <summary>
    /// Lógica de interacción para Empresas.xaml
    /// </summary>
    public partial class Empresas : Page
    {

        #region Global Variables

        bool registrar = true;
        eREST_EMPRESA Empresa = new eREST_EMPRESA();
        MantenimientoEmpresa blaMant = new MantenimientoEmpresa();

        int error = 0;
        #endregion
        public Empresas()
        {
            InitializeComponent();
            ListarEmpresas();
        }
        #region Private Functions
        private void ListarEmpresas()
        {
            try
            {
                btnEdit.IsEnabled = false;
                MantenimientoEmpresa blr = new MantenimientoEmpresa();
                List<eREST_EMPRESA> Lista = new List<eREST_EMPRESA>();
                Lista = blr.ListarEmpresas();
                if (Lista != null)
                {
                    dgEmpresas.ItemsSource = Lista;
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

        private void MostrarVentanaMat(eREST_EMPRESA pEmpresa)
        {
            wmEmpresa ventis = new wmEmpresa(pEmpresa);
            ventis.Closed += new EventHandler(ventis_Closed); ;
            ventis.ShowDialog();
        }

        #endregion

        #region Eventos privados
        void ventis_Closed(object sender, EventArgs e)
        {
            if (((wmEmpresa)sender).DialogResult == true)
            {
                ListarEmpresas();
            }
        }

        private void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            ListarEmpresas();
            dgEmpresas.SelectedIndex = -1;
        }


        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            MostrarVentanaMat(null);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            MostrarVentanaMat(Empresa);
        }

        private void dgEmpresas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                Empresa = ((DataGrid)sender).SelectedItem as eREST_EMPRESA;
                if (Empresa != null)
                {
                    btnEdit.IsEnabled = true;
                }
                else
                {
                    btnEdit.IsEnabled = false;
                }
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
            MantenimientoEmpresa blMant = new MantenimientoEmpresa();
            blMant.EliminarEmpresa(Empresa);
            ListarEmpresas();
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        private void clPermissionsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Empresa = ((DataGrid)sender).SelectedItem as eREST_EMPRESA;
                if (Empresa != null)
                {
                    btnEdit.IsEnabled = true;
                }
                else
                {
                    btnEdit.IsEnabled = false;
                }
            }
            catch
            {//(Exception ex)
                //ewMessage nwError = new ewMessage(ErrorMessages.titlePermissions + ex.Message.ToString(), UserMessages.titlePermissions);
                //nwError.ShowDialog();
                //if (nwError.DialogResult == true)
                //    nwError.Close();
            }
        }

        #endregion

  
    }

    
}
