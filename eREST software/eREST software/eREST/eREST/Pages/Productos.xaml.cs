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


using System.IO;
using System.Windows.Threading;
using System.Diagnostics;
using System.Timers;

using eREST.BL;
using eREST.BO;
using eREST.CustomControls;
using eREST.ModalWindows.MaintenanceWindows;
using eREST.BL.Producto;

namespace eREST.Pages
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Productos : Page
    {

        #region Global Variables

        byte[] ImageByte;
        MantenimientoProducto manPro = new MantenimientoProducto();

        #endregion


        public Productos()
        {
            InitializeComponent();
            cargarCategorias();
            CargarProducto();
        }
       
        public void cargarCategorias()
        {
            cmbCategoria.ItemsSource = manPro.ListarCategorias();
        }
        public void CargarProducto()
        {
            List<eREST_spListarProductosResult> lista = new List<eREST_spListarProductosResult>();
            wpProducto.Children.Clear();
            if (nombreTextBox.Text == "")
            {
                if(cmbSubCategoria.SelectedItem != null)
                {
                    lista = manPro.ListarProduto(null, cmbSubCategoria.SelectedItem.ToString());
                }
                 
            }
            else
            {
                 lista = manPro.ListarProduto(nombreTextBox.Text, cmbSubCategoria.SelectedItem.ToString());
            }
           
            foreach(eREST_spListarProductosResult nuevo in lista)
            {
                ccProducts producto = new ccProducts();

                producto.Template = Application.Current.Resources["ccProducts"] as ControlTemplate;
                producto.Background = Brushes.Beige;
                producto.Margin = new Thickness(5, 5, 5, 0);
                producto.Width = 120;
                producto.Height = 125;
                producto.DataContext = nuevo;
                producto.ProductExist = nuevo.PRO_ESTADO;
                producto.ProductName = nuevo.PRO_NOMBRE;
                producto.ProductValue = "₡" + nuevo.PRO_PRECIO.ToString();
                if (producto.ProductName.Length >= 15)
                {
                    producto.FontSize = 14;

                }
                else producto.FontSize = 16;
                

                if (producto.ProductExist)
                    producto.Foreground = Brushes.Green;
                else
                    producto.Foreground = Brushes.Red;

                producto.ProductDetail =  nuevo.SCA_NOMBRE;
                ImageByte = nuevo.PRO_IMAGEN.ToArray();
                BitmapImage pImage = new BitmapImage();
                byte[] imageBuffer = (byte[])ImageByte;
                if (imageBuffer.Length == 0)
                {
                    //pImage = new BitmapImage(new Uri(OthersResoures.noProductImage, UriKind.RelativeOrAbsolute));

                }
                else
                {
                    MemoryStream strem = new MemoryStream(imageBuffer);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(strem);
                    pImage.BeginInit();
                    MemoryStream Memo = new MemoryStream();
                    img.Save(Memo, System.Drawing.Imaging.ImageFormat.Bmp);
                    Memo.Seek(0, SeekOrigin.Begin);
                    pImage.StreamSource = Memo;
                    pImage.EndInit();
                }
                producto.Click += Producto_Click; 
                producto.MouseRightButtonUp += Producto_MouseRightButtonUp;
                producto.Image = pImage;
                wpProducto.Children.Add(producto);
            }
        }

        private void Producto_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            ccProducts producto = (ccProducts)sender;
            eREST_spListarProductosResult lista = producto.DataContext as eREST_spListarProductosResult;
            manPro.CambiarEstado(lista.PRO_PK_PRODUCTO);
            CargarProducto();
        }

        

        private void Producto_Click(object sender, RoutedEventArgs e)
        {
            ccProducts producto = (ccProducts)sender;
            eREST_spListarProductosResult lista = producto.DataContext as eREST_spListarProductosResult;
            List<eREST_spListarIngredientesResult> listaIngredientes = manPro.ListarIngredientes(lista.REC_PK_RECETA);
           
            wmProducto ventis = new wmProducto("Editar", ingredientes: listaIngredientes, Producto: lista);
            ventis.Closed += Ventis_Closed;
            ventis.ShowDialog();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            wmProducto ventis = new wmProducto(ptipo: "Agregar", ingredientes: null, Producto: null); 
            ventis.Closed += Ventis_Closed;
            ventis.ShowDialog();
        }

        private void Ventis_Closed(object sender, EventArgs e)
        {
            CargarProducto();
        }

        private void cmbCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbSubCategoria.ItemsSource = manPro.ListarSubCategorias(manPro.ObtenerIdCategorias(cmbCategoria.SelectedItem.ToString()));
            cmbSubCategoria.SelectedIndex = 0;
        }

        private void nombreTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CargarProducto();
        }

        private void cmbSubCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CargarProducto();
        }
    }

        

     
}



