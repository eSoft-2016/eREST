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

using eREST.BL.Sector;
using eREST.BO;
namespace eREST.ModalWindows.MaintenanceWindows
{
    /// <summary>
    /// Lógica de interacción para wmSeletSector.xaml
    /// </summary>
    public partial class wmSeletSector : Window
    {
        public wmSeletSector()
        {
            InitializeComponent();
            CargarSectores();
        }
        void CargarSectores()
        {
            MantenimientoSector mantSec = new MantenimientoSector();
            List<eREST_SECTORES> lSec = mantSec.ListarSectores();
            foreach (eREST_SECTORES sec in lSec)
            {
                ListBoxItem isec = new ListBoxItem();
                isec.Content = sec.SEC_NOMBRE;
                isec.Tag = sec.SEC_PK_SECTOR;
                cbSector.Items.Add(isec);
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem isec = cbSector.SelectedItem as ListBoxItem;
            this.Tag = isec.Tag;
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void cbSector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSave.IsEnabled = true;
        }
    }
}
