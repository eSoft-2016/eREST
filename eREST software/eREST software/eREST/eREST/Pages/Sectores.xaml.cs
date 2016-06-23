
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

using eREST.BO;
using eREST.BL.Sector;
using eREST.ModalWindows.MaintenanceWindows;
namespace eREST.Pages
{
    /// <summary>
    /// Lógica de interacción para Sectores.xaml
    /// </summary>
    public partial class Sectores : Page
    {
        #region Global Variables

        bool registrar = true;
        eREST_SECTORES Sector = new eREST_SECTORES();
        MantenimientoSector blaMannt = new MantenimientoSector();

        int error = 0;
        #endregion
        
        public Sectores()
        {
            InitializeComponent();
            ListarSectors();
        }

        #region Private Functions
        private void ListarSectors()
        {
            try
            {
                btnEdit.IsEnabled = false;
                MantenimientoSector blr = new MantenimientoSector();
                List<eREST_SECTORES> Lista = new List<eREST_SECTORES>();
                Lista = blr.ListarSectores();
                if (Lista != null)
                {
                    foreach (eREST_SECTORES sec in Lista)
                    {
                        dgSectores.Items.Add(sec);
                    }
                    
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

        private void MostrarVentanaMat(eREST_SECTORES pSector)
        {
            wmSector ventis = new wmSector(pSector);
            ventis.Closed += new EventHandler(ventis_Closed); ;
            ventis.ShowDialog();
        }

        #endregion

        #region Eventos privados
        void ventis_Closed(object sender, EventArgs e)
        {
            if (((wmSector)sender).DialogResult == true)
            {
                ListarSectors();
            }
        }

        private void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            ListarSectors();
            dgSectores.SelectedIndex = -1;
        }


        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            MostrarVentanaMat(null);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            MostrarVentanaMat(Sector);
        }

        private void dgSectors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                Sector = ((DataGrid)sender).SelectedItem as eREST_SECTORES;
                if (Sector != null)
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
            MantenimientoSector blMant = new MantenimientoSector();
            blMant.EliminarSector(Sector);
            ListarSectors();
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        private void clPermissionsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Sector = ((DataGrid)sender).SelectedItem as eREST_SECTORES;
                if (Sector != null)
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
