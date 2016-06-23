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

using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using eREST.BL;
using eREST.BO;
using eREST.UserControls;
using eREST.ModalWindows.MaintenanceWindows;
using eREST.BL.Producto;
namespace eREST.ModalWindows.MaintenanceWindows
{
    /// <summary>
    /// Lógica de interacción para mwProducto.xaml
    /// </summary>
    public partial class wmProducto : Window
    {
        public wmProducto(string ptipo, List<eREST_spListarIngredientesResult> ingredientes, eREST_spListarProductosResult Producto)
        {
            InitializeComponent();
            tipo = ptipo;
            cargarCategorias(); Cargar();
            if (tipo == "Editar")
            {
                this.Width = 780;
                grdEdit.Visibility = Visibility.Visible;
                brBorde.Width = 780;
                wpIngredientes.IsEnabled = false;
                gProduct.IsEnabled = false;
                pkProducto = Producto.PRO_PK_PRODUCTO;
                pkReceta = Producto.REC_PK_RECETA;
                foreach (eREST_spListarIngredientesResult ingre in ingredientes)
                {
                    
                    ucProducto nuevo = new ucProducto();
                    nuevo.txtNombre.Text = ingre.INS_NOMBRE;
                    if (ingre.DRE_ESTADO == false) { nuevo.txtEstado.Text = "Agotado"; nuevo.Foreground = System.Windows.Media.Brushes.Red; } else { nuevo.txtEstado.Text = "Activo"; nuevo.Foreground = System.Windows.Media.Brushes.Green; }
                    nuevo.btnEditar.Tag = ingre.INS_PK_INSUMO;
                    nuevo.btnEliminar.Tag = ingre.DRE_PK_DETRECETA;
                    nuevo.txtCantidad.Text = ingre.DRE_CANTIDAD.ToString();
                    nuevo.txtUniMedida.Text = ingre.DRE_UMEDIDA;
                    nuevo.btnEditar.Click += BtnEditar_Click;
                    nuevo.btnEliminar.Click += BtnEliminar_Click;
                    wpIngredientes.Children.Add(nuevo);
                }
                nombreTextBox.Text = Producto.PRO_NOMBRE;
                cmbCategoria.Text = Producto.CAT_NOMBRE;
                cmbSubCategoria.Text = Producto.SCA_NOMBRE;
                precioTextBox.Text = Producto.PRO_PRECIO.ToString();
                ImageByte = Producto.PRO_IMAGEN.ToArray();
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
                imgProduct.Source = pImage;

            }
        }
        string tipo;
        eREST_spListarInsumosResult insumo = new eREST_spListarInsumosResult();
        MantenimientoProducto manPro = new MantenimientoProducto();
        byte[] ImageByte;
        int pkReceta, pkProducto;
        public void Cargar()
        {
            try
            {
                    MantenimientoInsumo mantInsumo = new MantenimientoInsumo();
                    List<eREST_spListarInsumosResult> Lista = mantInsumo.ListarInsumos(nombreTextBox_Copy.Text);
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
        public void cargarCategorias()
        {
            cmbCategoria.ItemsSource = manPro.ListarCategorias();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        
        }

        private void nombreTextBox_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {
            Cargar();
        }
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        public void CargarIngrediente(eREST_spListarInsumosResult insumo, string cantidad, string UniMedida)
        {
            bool entro = false;
            foreach (ucProducto Producto in FindVisualChildren<ucProducto>(wpIngredientes))//verifica si existe el producto en el campo de facturacion
            {
                if(Convert.ToInt32(Producto.btnEditar.Tag)== insumo.INS_PK_INSUMO)
                {
                    entro = true;
                    Producto.txtCantidad.Text = cantidad;
                    Producto.txtUniMedida.Text = UniMedida;
                }
            }
            if (entro == false)
            {
                ucProducto nuevo = new ucProducto();
                nuevo.txtNombre.Text = insumo.INS_NOMBRE;
                if (insumo.INS_ESTADO == false)
                {
                    nuevo.txtEstado.Text = "Agotado";
                    nuevo.Foreground = System.Windows.Media.Brushes.Red;
                }

                else {
                    nuevo.txtEstado.Text = "Activo";
                    nuevo.Foreground = System.Windows.Media.Brushes.Green;
                }
                nuevo.btnEditar.Tag = insumo.INS_PK_INSUMO;
                if (tipo == "Editar")
                {
                    nuevo.btnEliminar.Tag = ("0" + insumo.INS_PK_INSUMO);
                }
                else
                {
                    nuevo.btnEliminar.Tag = insumo.INS_PK_INSUMO;
                }
                
                nuevo.txtCantidad.Text = cantidad;
                nuevo.txtUniMedida.Text = UniMedida;
                nuevo.btnEditar.Click += BtnEditar_Click;
                nuevo.btnEliminar.Click += BtnEliminar_Click;
                wpIngredientes.Children.Add(nuevo);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;
            foreach (ucProducto Producto in FindVisualChildren<ucProducto>(wpIngredientes))//verifica si existe el producto en el campo de facturacion
            {
                if (boton.Tag.ToString()[0].ToString() == "0")
                {
                    if (Convert.ToInt32(Producto.btnEditar.Tag) == Convert.ToInt32(boton.Tag.ToString().Remove(0, 1)))
                    {
                        wpIngredientes.Children.Remove(Producto);
                    }
                }
                else
                {
                    if ((Convert.ToInt32(Producto.btnEliminar.Tag) == Convert.ToInt32(boton.Tag) && Convert.ToInt32(Producto.btnEliminar.Tag.ToString()[0]) != 0))
                    {
                        MessageBoxResult m = MessageBox.Show("Se eliminara o activara el ingrediente", "Mensaje de Confirmación", MessageBoxButton.YesNo);
                        if (m == MessageBoxResult.Yes)
                        {
                            if (tipo == "Agregar")
                            {
                                wpIngredientes.Children.Remove(Producto);
                            }
                            else
                            {
                                if (Producto.txtEstado.Text == "Activo")
                                {
                                    Producto.txtEstado.Text = "Agotado";
                                    Producto.Foreground = System.Windows.Media.Brushes.Red;
                                    if (manPro.RevisaIngrediente(Convert.ToInt32(Producto.btnEliminar.Tag)) == true)
                                    {
                                        manPro.CambiarEstadoDetIngrediente(Convert.ToInt32(Producto.btnEliminar.Tag));
                                    }
                                    else
                                    {
                                        wpIngredientes.Children.Remove(Producto);
                                    }


                                }
                                else
                                {
                                    Producto.txtEstado.Text = "Activo";
                                    Producto.Foreground = System.Windows.Media.Brushes.Green;
                                    if (manPro.RevisaIngrediente(Convert.ToInt32(Producto.btnEliminar.Tag)) == true)
                                    {
                                        manPro.CambiarEstadoDetIngrediente(Convert.ToInt32(Producto.btnEliminar.Tag));
                                    }

                                }

                            }
                        }
                    }
                }
                
            }
        }


        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;
                foreach (ucProducto Producto in FindVisualChildren<ucProducto>(wpIngredientes))//verifica si existe el producto en el campo de facturacion
            {
                if (Convert.ToInt32(Producto.btnEditar.Tag) == Convert.ToInt32(boton.Tag))
                {
                    MantenimientoInsumo mantIns = new MantenimientoInsumo();
                    insumo = mantIns.Insumo(mantIns.ObtenerInsumo(Convert.ToInt32(boton.Tag)).INS_NOMBRE);
                    wmAgregarProducto nueva = new wmAgregarProducto("Editar", pinsumo: insumo, cantidad: Producto.txtCantidad.Text, uniMedida: Producto.txtUniMedida.Text);
                    nueva.Owner = this;
                    nueva.ShowDialog();
                }
            }
            
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            wmAgregarProducto nueva = new wmAgregarProducto("Agregar", pinsumo: insumo, cantidad: null, uniMedida:null);
            nueva.Owner = this;
            nueva.ShowDialog();
        }


        private void precioTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void dgInsumos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                insumo = ((DataGrid)sender).SelectedItem as eREST_spListarInsumosResult;
               
            }
            catch
            {
            }
        }

        private void cmbCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbSubCategoria.ItemsSource = manPro.ListarSubCategorias(manPro.ObtenerIdCategorias(cmbCategoria.SelectedItem.ToString()));
            cmbSubCategoria.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int conta = 0;
            
            if(nombreTextBox.Text != "" && precioTextBox.Text != "")
            {

            
            List<eREST_DETRECETAS> listaDetalle = new List<eREST_DETRECETAS>();
            foreach (ucProducto Producto in FindVisualChildren<ucProducto>(wpIngredientes))//verifica si existe el producto en el campo de facturacion
            {
                eREST_DETRECETAS nuevoDet = new eREST_DETRECETAS();
                nuevoDet.DRE_FK_INSUMO = Convert.ToInt32(Producto.btnEditar.Tag);
                nuevoDet.DRE_CANTIDAD = Convert.ToDouble(Producto.txtCantidad.Text);
                nuevoDet.DRE_UMEDIDA = Producto.txtUniMedida.Text;
                    nuevoDet.DRE_PK_DETRECETA = Convert.ToInt32(Producto.btnEliminar.Tag);
                    if (Producto.txtEstado.Text == "Activo")
                    {
                        nuevoDet.DRE_ESTADO = true;
                    }else nuevoDet.DRE_ESTADO = false;

                    listaDetalle.Add(nuevoDet);
                conta++;
            }
            if (conta != 0)
            {
                    //hacer editar
                    if(tipo == "Editar")
                    {
                        foreach (eREST_DETRECETAS detalle in listaDetalle)
                        {
                            detalle.DRE_FK_RECETA = pkReceta;
                            if (manPro.RevisaIngrediente(Convert.ToInt32(detalle.DRE_PK_DETRECETA)) == true)
                            {
                                manPro.EditarDetReceta(detalle);
                            }
                            else
                            {
                                manPro.AgregarDetReceta(detalle);
                            }
                        }
                        eREST_PRODUCTOS nuevoProd = new eREST_PRODUCTOS();
                        nuevoProd.PRO_NOMBRE = nombreTextBox.Text;
                        nuevoProd.PRO_PK_PRODUCTO = pkProducto;
                        nuevoProd.PRO_PRECIO = Convert.ToDouble(precioTextBox.Text);
                        nuevoProd.PRO_FK_RECETA = pkReceta;
                        nuevoProd.PRO_FK_SUBCATEGORIA = manPro.ObtenerIdSuCategorias(cmbSubCategoria.SelectedItem.ToString());
                        nuevoProd.PRO_IMAGEN = ImageByte;
                        manPro.EditarProducto(nuevoProd);
                        MessageBox.Show("Se ha editado el producto con exito!");
                        this.Close();
                    }
                    else
                    {
                        eREST_RECETAS nuevaRe = new eREST_RECETAS();
                        pkReceta = manPro.AgregarReceta(nuevaRe);
                        foreach (eREST_DETRECETAS detalle in listaDetalle)
                        {
                            detalle.DRE_FK_RECETA = pkReceta;
                            manPro.AgregarDetReceta(detalle);
                        }
                        eREST_PRODUCTOS nuevoPro = new eREST_PRODUCTOS();
                        nuevoPro.PRO_NOMBRE = nombreTextBox.Text;
                        nuevoPro.PRO_PRECIO = Convert.ToDouble(precioTextBox.Text);
                        nuevoPro.PRO_FK_RECETA = pkReceta;
                        nuevoPro.PRO_FK_SUBCATEGORIA = manPro.ObtenerIdSuCategorias(cmbSubCategoria.SelectedItem.ToString());
                        nuevoPro.PRO_IMAGEN = ImageByte;
                        manPro.AgregarProducto(nuevoPro);
                        MessageBox.Show("Se ha guardado el producto con exito!");
                        this.Close();
                    }
              
                }
            else
            {
                MessageBox.Show("Debe de agregar ingredientes");
            }
            }
            else
            {
                MessageBox.Show("Debe agregar un nombre y un precio al producto");
            }
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            LoadImage();
        }
        private void LoadImage()
        {
            try
            {
                OpenFileDialog Ofd = new OpenFileDialog();
                Ofd.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg";
                if (Ofd.ShowDialog() == true)
                {
                    FileStream _fileStream = new FileStream(Ofd.FileName, FileMode.Open);
                    ImageByte = new byte[Convert.ToInt32(_fileStream.Length)];
                    _fileStream.Read(ImageByte, 0, ImageByte.Length);
                    _fileStream.Close();
                }

                BitmapImage bmImage = new BitmapImage();
                bmImage.BeginInit();
                bmImage.UriSource = new Uri(Ofd.FileName, UriKind.Absolute);
                bmImage.EndInit();
                imgProduct.Source = bmImage;
            }
            catch
            {
            //    ewMessage nwError = new ewMessage(ErrorMessages.errorImageProduct, ErrorMessages.titleProduct);
                //nwError.ShowDialog();
                //if (nwError.DialogResult == true)
                //    nwError.Close();

            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            this.Width = 1270;
            grdEdit.Visibility = Visibility.Hidden;
            brBorde.Width = 1270;
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            wpIngredientes.IsEnabled = true;
            gProduct.IsEnabled = true;
        }
    }
}
