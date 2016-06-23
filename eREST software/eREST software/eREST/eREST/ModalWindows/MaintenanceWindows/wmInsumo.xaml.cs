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
    /// Interaction logic for wmInsumo.xaml
    /// </summary>
    public partial class wmInsumo : Window
    {
        public wmInsumo(string ptipo, eREST_spListarInsumosResult pInsumo)
        {
            InitializeComponent();
            InitializeComponent();
            CargarTipos();
            tipo = ptipo;
            if (tipo == "Ver")
            {
                btnSave.IsEnabled = false;
                gInsumo.IsEnabled = false;
                gDetInsumo.IsEnabled = false;
                nombreTextBox.Text = pInsumo.BOD_NOMBRE;
                txtDescripcion.Text = pInsumo.INS_DESCRIPCION;
                cmbBodega.Text = pInsumo.BOD_NOMBRE;
                txtCantidad.Text = pInsumo.DB_CANTIDAD.ToString();
                cmbUniMedida.Text = pInsumo.DB_UMEDIDA;
            }
            else if (tipo == "Editar")
            {
                btnSave.IsEnabled = true;
                gInsumo.IsEnabled = true;
                gDetInsumo.IsEnabled = true;
                nombreTextBox.Text = pInsumo.INS_NOMBRE;
                txtDescripcion.Text = pInsumo.INS_DESCRIPCION;
                cmbBodega.Text = pInsumo.BOD_NOMBRE;
                txtCantidad.Text = pInsumo.DB_CANTIDAD.ToString();
                cmbUniMedida.Text = pInsumo.DB_UMEDIDA;
                pkInsumo = pInsumo.INS_PK_INSUMO;
                pkBodega = pInsumo.BOD_PK_BODEGA;
                pkDetBodega = pInsumo.DB_PK_DETBODEGA;
            }
        }
        MantenimientoInsumo mantInsumo = new MantenimientoInsumo();
        string tipo;
        int pkInsumo, pkBodega, pkDetBodega;
        public void CargarTipos()
        {
            List<string> lista = mantInsumo.ListarTipo();
            cmbBodega.ItemsSource = lista;
            cmbUniMedida.Items.Add("Uni");
            cmbUniMedida.Items.Add("Li");
            cmbUniMedida.Items.Add("Oz");
            cmbUniMedida.Items.Add("Kg");
            cmbUniMedida.Items.Add("Gr");
            cmbUniMedida.Items.Add("Ml");
            

        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCantidad.Text != "" && nombreTextBox.Text != "" && cmbUniMedida.SelectedItem.ToString() != "" && cmbBodega.SelectedItem.ToString() != "")
                {
                    eREST_INSUMOS insumo = new eREST_INSUMOS();
                    insumo.INS_ESTADO = true;
                    insumo.INS_DESCRIPCION = txtDescripcion.Text;
                    insumo.INS_NOMBRE = nombreTextBox.Text;
                    eREST_DETBODEGAS detInsumo = new eREST_DETBODEGAS();
                    detInsumo.DB_CANTIDAD = Convert.ToDouble(txtCantidad.Text);
                    detInsumo.DB_FK_BODEGA = mantInsumo.ObtenerIdTipo(cmbBodega.SelectedItem.ToString());
                    
                    detInsumo.DB_UMEDIDA = cmbUniMedida.SelectedItem.ToString();
                    
                    if (tipo == "Agregar")
                    {
                        detInsumo.DB_FK_INSUMO = mantInsumo.RegistrarInsumo(insumo);
                        mantInsumo.RegistrarDetInsumo(detInsumo);
                        MessageBox.Show("Se ha agregado el insumo correctamente");
                    }
                    else
                    {
                        insumo.INS_PK_INSUMO = pkInsumo;
                        detInsumo.DB_PK_DETBODEGA = pkDetBodega;
                        detInsumo.DB_FK_BODEGA = pkBodega;
                        detInsumo.DB_FK_INSUMO = insumo.INS_PK_INSUMO;
                        mantInsumo.EditarDetInsumo(detInsumo);
                        mantInsumo.EditarInsumo(insumo);
                        MessageBox.Show("Se ha editado el insumo correctamente");
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Debe completar todos los espacios requeridos");
                }
            }
            catch
            {
                MessageBox.Show("Algo ha ocurrido, verifique los datos digitados o llame a servicio de soporte");
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
