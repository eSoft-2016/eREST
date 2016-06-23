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
using eREST.BL;
using eREST.BL.Producto;
using eREST.BO;
using eREST.ModalWindows.MaintenanceWindows;
namespace eREST.Pages
{
    /// <summary>
    /// Interaction logic for Insumos.xaml
    /// </summary>
    public partial class Insumos : Page
    {
        public Insumos()
        {
            InitializeComponent();
            Cargar();
        }
        eREST_spListarInsumosResult insumo = new eREST_spListarInsumosResult();
        public void Cargar()
        {
            try
            {
                
                    MantenimientoInsumo mantInsumo = new MantenimientoInsumo();
                    List<eREST_spListarInsumosResult> Lista = mantInsumo.ListarInsumos(nombreTextBox.Text);
                    dgInsumos.Items.Clear();
                    if (Lista != null)
                    {
                        foreach (eREST_spListarInsumosResult result in Lista)
                        {
                            dgInsumos.Items.Add(result);

                        }

                    }

                

            }
            catch
            {

            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoInsumo mant = new MantenimientoInsumo();
            MantenimientoProducto manPro = new MantenimientoProducto();
            List<eREST_DETRECETAS> list = manPro.ListarDetallesPorInsumo(insumo.INS_PK_INSUMO);
            foreach(eREST_DETRECETAS cambiaEstado in list)
            {
                manPro.CambiarEstadoDetIngrediente(cambiaEstado.DRE_PK_DETRECETA);
            }
            mant.CambiarEstado(insumo.INS_PK_INSUMO);

            Cargar();
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            wmInsumo ventis = new wmInsumo(ptipo: "Ver", pInsumo: insumo);
            ventis.Closed += Ventis_Closed; 
            ventis.ShowDialog();
        }

        private void Ventis_Closed(object sender, EventArgs e)
        {
            Cargar();
        }

        private void nombreTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Cargar();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            wmInsumo nueva = new wmInsumo(ptipo: "Agregar", pInsumo: null);
            nueva.ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            wmInsumo ventis = new wmInsumo(ptipo: "Editar", pInsumo: insumo);
            ventis.Closed += Ventis_Closed;
            ventis.ShowDialog();
        }

        private void dgInsumos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                insumo = ((DataGrid)sender).SelectedItem as eREST_spListarInsumosResult;
                if (insumo != null)
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
    }
}
