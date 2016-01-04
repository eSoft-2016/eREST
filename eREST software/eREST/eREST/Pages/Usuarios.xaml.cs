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

using eREST.CustomControls;
using eREST.ModalWindows.MaintenanceWindows;

using System.Collections.ObjectModel;

namespace eREST.Pages
{
    /// <summary>
    /// Lógica de interacción para Usuarios.xaml
    /// </summary>
    public partial class Usuarios : Page
    {
        public Usuarios()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ShowWindowMaintenance();
        }
        /// <summary>
        /// Muestra la ventana de mantenimiento para usuarios
        /// </summary>
        /// <param name="pUser">Recibe el usuario a modificar</param>
        private void ShowWindowMaintenance()
        {
            wmUsuario ventis = new wmUsuario();

            ventis.ShowDialog();
        }

    }
}
