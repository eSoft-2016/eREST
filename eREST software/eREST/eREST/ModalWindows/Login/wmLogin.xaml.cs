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


using eREST.CustomControls; 
using System.Collections.ObjectModel;
using System.Net.Mail;

namespace eREST
{
    /// <summary>
    /// Ventana de login donde se permite el ingreso a la aplicacion
    /// </summary>
    public partial class wmLogin : Window
    {
        #region Global Variables
         
        #endregion
        
        #region Constructor
        public wmLogin()
        {
            InitializeComponent();
        }
        #endregion 
       
        #region Private Events
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (ErrorLogin()){
                MainWindow nwWindow = new MainWindow();
                login.Close();
                nwWindow.ShowDialog();
            }
        }

        #endregion

        #region Private Functions 
        /// <summary>
        /// Investigar si la contraseña es correcta. 
        /// </summary>
        private Boolean ErrorLogin()
        { 
            return true;
        }
       
        #endregion

    }
}
