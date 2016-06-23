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
    /// Interaction logic for wmBodega.xaml
    /// </summary>
    public partial class wmBodega : Window
    {
        public wmBodega(string ptipo, eREST_spListarBodegasResult bodega)
        {
            InitializeComponent();
            CargarTipos();
            tipo = ptipo;
            if(tipo == "Ver")
            {
                btnSave.IsEnabled = false;
                gBodega.IsEnabled = false;
              
                nombreTextBox.Text = bodega.BOD_NOMBRE;
                txtLocalidad.Text = bodega.BOD_LOCALIDAD;
                cmbTipo.Text = bodega.TBO_NOMBRE;
            }
            else if(tipo == "Editar")
            {
                btnSave.IsEnabled = true;
                gBodega.IsEnabled = true;
                nombreTextBox.Text = bodega.BOD_NOMBRE;
                txtLocalidad.Text = bodega.BOD_LOCALIDAD;
                cmbTipo.Text = bodega.TBO_NOMBRE;
                pkBodega = bodega.BOD_PK_BODEGA;
            }
        }
        MantenimientoBodega mantBodega = new MantenimientoBodega();
        string tipo;
        int pkBodega;
        public void CargarTipos()
        {
            List<string> lista = mantBodega.ListarTipo();
            cmbTipo.ItemsSource = lista;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtLocalidad.Text != "" && nombreTextBox.Text != "" && cmbTipo.SelectedItem.ToString() != "")
                {
                    eREST_BODEGAS bodega = new eREST_BODEGAS();
                    bodega.BOD_ESTADO = true;
                    bodega.BOD_FK_TIPOBODEGA = mantBodega.ObtenerIdTipo(cmbTipo.SelectedItem.ToString());
                    bodega.BOD_NOMBRE = nombreTextBox.Text;
                    bodega.BOD_LOCALIDAD = txtLocalidad.Text;
                    if (tipo == "Agregar")
                    {
                        mantBodega.RegistrarBodega(bodega);
                        MessageBox.Show("Se ha agregado la bodega correctamente");
                    }else
                    {
                        bodega.BOD_PK_BODEGA = pkBodega;
                        mantBodega.EditarBodega(bodega);
                        MessageBox.Show("Se ha editado la bodega correctamente");
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Debe completar todos los espacios");
                }
            }
            catch
            {
                MessageBox.Show("Algo ha ocurrido, verifique los datos digitados o llame a servicio de soporte");
            }
            
        }
    }
}
