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
using eREST.BL;
using eREST.BO;
namespace eREST.ModalWindows.MaintenanceWindows
{
    /// <summary>
    /// Interaction logic for wmAgregarProducto.xaml
    /// </summary>
    public partial class wmAgregarProducto : Window
    {
        public wmAgregarProducto(string tipo,eREST_spListarInsumosResult pinsumo, string cantidad, string uniMedida)
        {
            InitializeComponent();
            insumo = pinsumo;
            CargarTipos();
            Tipo = tipo;
            if(tipo == "Editar")
            {
                txtCantidad.Text = cantidad;
                cmbUniMedida.Text = uniMedida;
            }

        }
        eREST_spListarInsumosResult insumo = new eREST_spListarInsumosResult();
        string Tipo;
        public void CargarTipos()
        {
            nombreTextBox.Text = insumo.INS_NOMBRE;
            txtDescripcion.Text = insumo.INS_DESCRIPCION;
            cmbUniMedida.Items.Add("Uni");
            cmbUniMedida.Items.Add("Li");
            cmbUniMedida.Items.Add("Oz");
            cmbUniMedida.Items.Add("Kg");
            cmbUniMedida.Items.Add("Gr");
            cmbUniMedida.Items.Add("Ml");
        }

        private void txtCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtCantidad.Text != "" && cmbUniMedida.SelectedItem.ToString() != "")
            {

                ((wmProducto)this.Owner).CargarIngrediente(insumo: insumo, cantidad:txtCantidad.Text,UniMedida:cmbUniMedida.SelectedItem.ToString());
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe llenar todos los campos");
            }
        }
    }
}
