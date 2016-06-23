using eREST.BL.Sector;
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
    /// Lógica de interacción para wmSector.xaml
    /// </summary>
    public partial class wmSector : Window
    {
        #region Global Variables

        bool registrar = true;
        eREST_SECTORES Sector = new eREST_SECTORES();
        MantenimientoSector blaMant = new MantenimientoSector();

        int error = 0;
        #endregion

        #region Construcrot 
        public wmSector(eREST_SECTORES pSector)
        {
            InitializeComponent();
             Sector = pSector;
            Inicializar();
        }
        
        #endregion
        #region Private Functions
        private void Inicializar()
        {
            try
            {
                if (Sector == null)
                {
                    gridSector.DataContext = new eREST_SECTORES();
                    registrar = true;
                }
                else
                {
                    gridSector.DataContext = Sector;
                    registrar = false;
                }
            }
            catch
            {//(Exception ex)
                //ewMessage nwError = new ewMessage(ex.Message.ToString(), UserMessages.titlePermissions);
                //nwError.ShowDialog();
                //if (nwError.DialogResult == true)
                //    nwError.Close();
            }
        }

        #endregion

        #region Private Events
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (registrar)
                {
                    Sector = gridSector.DataContext as eREST_SECTORES;
                    blaMant.RegistrarSector(Sector);
                    this.DialogResult = true;

                }
                else
                {
                    Sector = gridSector.DataContext as eREST_SECTORES;
                    blaMant.EditarSector(Sector);
                    this.DialogResult = true;
                }
            }
            catch
            {
                //ewMessage nwError = new ewMessage(ErrorMessages.errorRegisterPermission, UserMessages.titlePermissions);
                //nwError.ShowDialog();
                //if (nwError.DialogResult == true)
                //    nwError.Close();
                //this.DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #region Valide Error
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = error == 0;
            e.Handled = true;
        }

        private void ValidateError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                error++;
            else
                error--;
        }
        #endregion

        #endregion
    }
}
