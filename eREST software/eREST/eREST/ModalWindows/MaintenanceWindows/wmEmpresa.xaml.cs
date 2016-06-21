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

using eREST.BO;
using eREST.BL.Empresa;

namespace eREST.ModalWindows.MaintenanceWindows
{
    /// <summary>
    /// Lógica de interacción para wmEmpresa.xaml
    /// </summary>
    public partial class wmEmpresa : Window
    {
        #region Global Variables

        bool registrar = true;
        eREST_EMPRESA Empresa = new eREST_EMPRESA();
        MantenimientoEmpresa blaMant = new MantenimientoEmpresa();

        int error = 0;
        #endregion

        #region Construcrot
        public wmEmpresa(eREST_EMPRESA pEmpresa)
        {
            InitializeComponent();
            Empresa = pEmpresa;
            Inicializar();
        }
        #endregion
        #region Private Functions
        private void Inicializar()
        {
            try
            {
                if (Empresa == null)
                {
                    gridEmpresa.DataContext = new eREST_EMPRESA();
                    registrar = true;
                }
                else
                {
                    gridEmpresa.DataContext = Empresa;
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
                    Empresa = gridEmpresa.DataContext as eREST_EMPRESA;
                    blaMant.RegistrarEmpresa(Empresa);  
                    this.DialogResult = true;
                    
                }
                else
                {
                    Empresa = gridEmpresa.DataContext as eREST_EMPRESA;
                    blaMant.EditarEmpresa(Empresa);
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
