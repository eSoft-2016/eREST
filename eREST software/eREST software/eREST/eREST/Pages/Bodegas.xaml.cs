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
using eREST.ModalWindows.MaintenanceWindows;
using eREST.BL;
using eREST.BO;
namespace eREST.Pages
{
    /// <summary>
    /// Interaction logic for Bodegas.xaml
    /// </summary>
    public partial class Bodegas : Page
    {
        public Bodegas()
        {
            InitializeComponent();
            Cargar();

        }
        eREST_spListarBodegasResult bodega = new eREST_spListarBodegasResult();
        public void Cargar()
        {
            try
            {
                    MantenimientoBodega mantBodega = new MantenimientoBodega();
                    List<eREST_spListarBodegasResult> Lista = mantBodega.ListarBodegas(nombreTextBox.Text);
                    dgBodegas.Items.Clear();
                    if (Lista != null)
                    {
                       foreach(eREST_spListarBodegasResult result in Lista)
                        {
                            dgBodegas.Items.Add(result);
                            
                        }
                    }
            }
            catch
            {

            }
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            wmBodega nueva = new wmBodega( ptipo:"Agregar", bodega:null);
            nueva.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoBodega mant = new MantenimientoBodega();
            mant.CambiarEstado(bodega.BOD_PK_BODEGA);

            Cargar();
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            wmBodega ventis = new wmBodega(ptipo:"Ver" ,bodega:bodega);
            ventis.Closed += Ventis_Closed;
            ventis.ShowDialog();
        }

        private void Ventis_Closed(object sender, EventArgs e)
        {
            Cargar();
        }

        private void dgBodegas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                bodega = ((DataGrid)sender).SelectedItem as eREST_spListarBodegasResult;
                if (bodega != null)
                {
                    btnEdit.IsEnabled = true;
                }
                else
                {
                    btnEdit.IsEnabled = false;
                }
            }
            catch
            {
            }
        }

        private void nombreTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Cargar();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            wmBodega ventis = new wmBodega(ptipo: "Editar", bodega: bodega);
            ventis.Closed += Ventis_Closed;
            ventis.ShowDialog();
        }
    }
}
