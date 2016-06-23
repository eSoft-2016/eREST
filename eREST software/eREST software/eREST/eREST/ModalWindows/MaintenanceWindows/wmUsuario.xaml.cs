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
    /// Lógica de interacción para wmUsuario.xaml
    /// </summary>
    public partial class wmUsuario : Window
    {
        public wmUsuario()
        {
            InitializeComponent();
        }
        #region Private Functions
        /// <summary>
        /// Registra un usuario
        /// </summary>
        private void RegisterUser()
        {
             
        }

        #endregion

        #region Private Events

        #region Validate Error
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
             
        }

        private void ValidateError(object sender, ValidationErrorEventArgs e)
        {
             
        }


        #endregion

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            RegisterUser();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion
    }
}
