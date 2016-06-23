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


using System.Windows.Threading; 
using System.Collections.ObjectModel; 

using Microsoft.Win32;
using System.IO;
using System.ComponentModel;
using System.Net;
using eREST.BL.Producto;
using eREST.BO; 
using eREST.UserControls;
using eREST.BL.Local;
using eREST.BL.Mobiliario;
using eREST.BL;
using eREST.BL.Orden;
using eREST.Resources;

namespace eREST.ModalWindows.MaintenanceWindows
{
    /// <summary>
    /// Lógica de interacción para wmOrden.xaml
    /// </summary>
    public partial class wmOrden : Window
    {
        
        #region Global Variables
       
       MantenimientoProducto mantPro = new MantenimientoProducto();
       ObservableCollection<eREST_PRODUCTOS> ocProductsList = new ObservableCollection<eREST_PRODUCTOS>();
       
       eREST_ORDENES nwOrderSave = new eREST_ORDENES();

       ucProductWithButtons actualSelectProduct = new ucProductWithButtons();
        
       ObservableCollection <ucProductWithButtons> ocProductsDrop = new ObservableCollection<ucProductWithButtons>();
       ObservableCollection<ucProductOrder> ocProducts = new ObservableCollection<ucProductOrder>();

       ListBox dragSource = null; //Lista auxiliar para el drag and drop.
       Boolean block; //Habilita si hay un componente con los mismos datos

       eREST_MOBILIARIOS dataFurniture = new eREST_MOBILIARIOS();

       double total=0; 

       Boolean orderActive = false;

       #endregion

       #region Constructor 
        public wmOrden( eREST_MOBILIARIOS pFurniture)
        {
            InitializeComponent();
           Loaded += mwOrders_Loaded;
           dataFurniture = pFurniture;
        }
       #endregion 

       #region Private Events
       private void btnClose_Click(object sender, RoutedEventArgs e)
       {
           DialogResult = true;
       }
       private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
       {
           eREST_CATEGORIAS nwCategory = cbCategory.SelectedItem as eREST_CATEGORIAS;
           if (nwCategory == null)
               nwCategory = new eREST_CATEGORIAS();
           ListProducts(nwCategory.CAT_PK_CATEGORIA);
       }

       private void lstDrag_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
       {
           ListBox parent = (ListBox)sender;
           dragSource = parent;
           object data = GetDataFromListBox(dragSource, e.GetPosition(parent));
           if (data != null)
               DragDrop.DoDragDrop(parent, data, DragDropEffects.Copy);
       }

       private void lstDrop_Drop(object sender, DragEventArgs e)
       {
           DropFunction(sender, e);
       }

       void mwOrders_Loaded(object sender, RoutedEventArgs e)
       {
           //Listo los productos 
           ListProducts();
           //Cargo el codigo de mueble según su tipo y numeración.
           MantenimientoMobiliario MantMo = new MantenimientoMobiliario();
           if (MantMo.ConsultarMoviliarioLlave(Convert.ToInt32(dataFurniture.MOB_FK_TIPOMOBILIARIO)).Equals("M4") || MantMo.ConsultarMoviliarioLlave(Convert.ToInt32(dataFurniture.MOB_FK_TIPOMOBILIARIO)).Equals("M6"))
               tbxCode.Text = "Mesa: " + MantMo.ConsultarMoviliarioLlave(Convert.ToInt32(dataFurniture.MOB_FK_TIPOMOBILIARIO)) + "-" + dataFurniture.MOB_NUMERO;
           else
               tbxCode.Text = "Barra:  " + MantMo.ConsultarMoviliarioLlave(Convert.ToInt32(dataFurniture.MOB_FK_TIPOMOBILIARIO)) + "-" + dataFurniture.MOB_NUMERO;
           GetDetailOrder(dataFurniture.MOB_PK_MOBILIARIO);
       }

       #endregion 

       #region Private Functions

       /// <summary>
       /// Genera el drop del producto
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       private void DropFunction(object sender, DragEventArgs e)
       {
           ListBox parent = (ListBox)sender;


           MantenimientoDetalleOrden MantDeOrder = new MantenimientoDetalleOrden();
           eREST_DETALLEORDENES nwOrderDetail = new eREST_DETALLEORDENES();

           object data = e.Data.GetData(typeof(ucProductOrder));

           ucProductOrder prevProduct = (ucProductOrder)data;
           ucProductWithButtons nwProduct = new ucProductWithButtons();

           //Envío la información del user control del drag al user control de drop
           nwProduct.ucProductOrder.DataContext = prevProduct.DataContext;
           nwProduct.ucProductOrder.ProductName = prevProduct.ProductName;
           nwProduct.ucProductOrder.Value = prevProduct.Value;
           nwProduct.ucProductOrder.imgProduct.Source = prevProduct.imgProduct.Source;
           actualSelectProduct = nwProduct;


           block = false;



           foreach (ucProductWithButtons item in ocProductsDrop)
           {

               if (prevProduct.ProductName == item.ucProductOrder.ProductName)
               {
                   item.ucProductOrder.Total += 1; //Si hay un producto del mismo tipo suma la cantidad.
                   eREST_PRODUCTOS nwProductOrder = nwProduct.ucProductOrder.DataContext as eREST_PRODUCTOS;

                   nwOrderDetail.DOR_CANTIDAD = item.ucProductOrder.Total;
                   nwOrderDetail.DOR_ESTADO = false;
                   nwOrderDetail.DOR_FECHA = DateTime.Now;
                   nwOrderDetail.DOR_FK_ORDEN = nwOrderSave.ORD_PK_ORDEN;
                   nwOrderDetail.DOR_FK_PRODUCTO = nwProductOrder.PRO_PK_PRODUCTO;
                   nwOrderDetail.DOR_FK_EMPLEADO = 1;


                   List<eREST_DETALLEORDENES> OrdersDetails = new List<eREST_DETALLEORDENES>();
                   double subtotal=0;

                   OrdersDetails = MantDeOrder.ListarOrdens(nwOrderSave.ORD_PK_ORDEN);
                   foreach (eREST_DETALLEORDENES items in OrdersDetails)
                   {
                       if (items.DOR_FK_PRODUCTO == nwOrderDetail.DOR_FK_PRODUCTO)
                       {
                           nwOrderDetail.DOR_PK_DETALLEORDEN = items.DOR_PK_DETALLEORDEN;
                           MantDeOrder.EditarDetalleOrden(nwOrderDetail);

                           subtotal =  Convert.ToDouble((mantPro.Consultarproducto(items.DOR_FK_PRODUCTO).PRO_PRECIO * items.DOR_CANTIDAD+1));
                           total += subtotal;
                           tbxTotal.Text = total.ToString();
                       }
                           
                   }

                   block = true;
               }
           }
           if (block != true) //Si no hay del mismo tipo suma solamente uno y lo agrega nuevo al list box
           {
               nwProduct.ucProductOrder.Total += 1; 
               eREST_PRODUCTOS nwProductOrder = nwProduct.ucProductOrder.DataContext as eREST_PRODUCTOS;


               nwOrderDetail.DOR_CANTIDAD = nwProduct.ucProductOrder.Total;
               nwOrderDetail.DOR_ESTADO = false;
               nwOrderDetail.DOR_FECHA = DateTime.Now;
               nwOrderDetail.DOR_FK_ORDEN = nwOrderSave.ORD_PK_ORDEN;
               nwOrderDetail.DOR_FK_PRODUCTO = nwProductOrder.PRO_PK_PRODUCTO;  
               nwOrderDetail.DOR_FK_EMPLEADO = 1;

               ocProductsDrop.Add(nwProduct);
               parent.Items.Add(nwProduct);
               AddDetailOrder(nwOrderDetail);
           }
       }

       void nwProduct_btnDeleteClick(object sender, RoutedEventArgs e)
       {
           MessageBox.Show("d");
       }


       public static BitmapImage ImageSource(eREST_PRODUCTOS pProduct)
       {

           BitmapImage pImage = new BitmapImage();
           byte[] imageBuffer = pProduct.PRO_IMAGEN.ToArray();
           if (imageBuffer.Length == 0)
           {
               pImage = new BitmapImage(new Uri(OthersResoures.noProductImage, UriKind.RelativeOrAbsolute));

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
           return pImage;
       }


        /// <summary>
        /// Obtiene la informacion del list box
        /// </summary>
       private static object GetDataFromListBox(ListBox source, Point point)
       {
           UIElement element = source.InputHitTest(point) as UIElement;
           if (element != null)
           {
               object data = DependencyProperty.UnsetValue; 
               while (data == DependencyProperty.UnsetValue)
               {
                   data = source.ItemContainerGenerator.ItemFromContainer(element);
                   if (data == DependencyProperty.UnsetValue)
                   {
                       element = VisualTreeHelper.GetParent(element) as UIElement;
                   }
                   if (element == source)
                   {
                       return null;
                   }
               }
               if (data != DependencyProperty.UnsetValue)
               {
                   return data;
               }
           }
           return null;
       }

        /// <summary>
        /// Lista los productos existentes.
        /// </summary>
       private void ListProducts()
       {
           try
           {
               eREST_PRODUCTOS ProducFilter = new eREST_PRODUCTOS();
               MantenimientoProducto MantPro = new MantenimientoProducto(); //Para filtrar y listar productos
               List<eREST_PRODUCTOS> lista = new List<eREST_PRODUCTOS>();

               List<string> categories = new List<string>();


               categories = MantPro.ListarCategorias(); //Lista las categorias
               //Agrega una categoria extra para la opción de mostrar todas las categorías
               //ListBoxItem nwAllCategories = new ListBoxItem();
               //nwAllCategories.Content = ; 
               categories.Insert(0, "Todas las categorías");

               cbCategory.ItemsSource =categories; //Agrega las categorías al combo box


               //lista = mantPro.ListarProductos(); //Lista todos los productos. 

               if (lista != null)
               {
                   foreach (eREST_PRODUCTOS addItem in lista)
                   {
                       //Creo el producto y le asigno todas sus propiedades asociadas. 
                       ucProductOrder nwProduct = new ucProductOrder();
                       nwProduct.Template = Application.Current.Resources["stProductOrder"] as ControlTemplate;
                       nwProduct.Width = 250;
                       nwProduct.Height = 90;
                       nwProduct.imgProduct.Source = ImageSource(addItem);
                       nwProduct.DataContext = addItem;
                       nwProduct.ProductName = addItem.PRO_NOMBRE;
                       nwProduct.Value = Convert.ToDouble(addItem.PRO_PRECIO);
                       ocProducts.Add(nwProduct);
                       lstDrag.Items.Add(nwProduct);
                   }
               }
               else
               {
                   //ewMessage nwError = new ewMessage(ErrorMessages.errorGeneralList, UserMessages.titleOrders);
                   //nwError.ShowDialog();
                   //if (nwError.DialogResult == true)
                   //    nwError.Close();   
               }
           }
           catch (Exception ex)
           {
               //ewMessage nwError = new ewMessage(ErrorMessages.errorGeneralList+ex.Message.ToString(), UserMessages.titleOrders);
               //nwError.ShowDialog();
               //if (nwError.DialogResult == true)
               //    nwError.Close();
               
           }
       }

        /// <summary>
        /// Lista los productos con filtro
        /// </summary>
        /// <param name="pIdCategory">Id de la categoría para filtrar.</param>
       private void ListProducts(int pIdCategory)
       {
           MantenimientoProducto blProductFilter = new MantenimientoProducto();
           eREST_PRODUCTOS ProducFilter = new eREST_PRODUCTOS();
           List<eREST_PRODUCTOS> listProduct = new List<eREST_PRODUCTOS>();

           ProducFilter.PRO_FK_SUBCATEGORIA = pIdCategory;

          // listProduct = blProductFilter.FilterProductList(ProducFilter);

           if (listProduct != null)
           {
                lstDrag.Items.Clear();
                foreach (eREST_PRODUCTOS addItem in listProduct)
                   {
                       ucProductOrder usProduct = new ucProductOrder();
                       usProduct.Template = Application.Current.Resources["stProductOrder"] as ControlTemplate;
                       usProduct.Width = 250;
                       usProduct.Height = 90;
                       usProduct.imgProduct.Source = ImageSource(addItem);
                       usProduct.DataContext = addItem;
                       usProduct.ProductName = addItem.PRO_NOMBRE;
                       usProduct.Value = Convert.ToDouble(addItem.PRO_PRECIO);
                     
                       lstDrag.Items.Add(usProduct);
                   }
               
           }
           else
           {
               lstDrag.Items.Clear();
           }
        
       }

        /// <summary>
        /// Obtiene los detalles de orden. 
        /// </summary>
        /// <param name="pCodeFurniture">Recibe el codigo de mueble</param>

       private void GetDetailOrder(int pCodeFurniture)
       {
           MantenimientoOrden blOrders = new MantenimientoOrden();
           MantenimientoDetalleOrden blDetail = new MantenimientoDetalleOrden();
           eREST_ORDENES nwOrders = new eREST_ORDENES();
           List<eREST_ORDENES> ocOrders = new List<eREST_ORDENES>(); //Ordenes
           List<eREST_DETALLEORDENES> ocOrderDetail = new List<eREST_DETALLEORDENES>();//Detalles de orden
           orderActive = false;
           try
           {
                ocOrders = blOrders.ListarOrdens(); 
               if(ocOrders!=null)
               {
                   foreach (eREST_ORDENES itemOrder in ocOrders)
                   {
                       if(itemOrder.ORD_FK_MOBILIARIO == pCodeFurniture) //Se revisa para saber si la orden exista previamente. 
                       {
                           orderActive = true;
                           nwOrders = itemOrder; 
                           nwOrderSave=itemOrder;
                           break;
                       } 
                   }
                    if (orderActive == true)
                    {
                        ocOrderDetail = blDetail.ListarOrdens(nwOrders.ORD_PK_ORDEN);

                        List<eREST_PRODUCTOS> listProduct = new List<eREST_PRODUCTOS>();
                        double subtotal = 0;

                        if (ocOrderDetail != null)
                        {
                            foreach (eREST_DETALLEORDENES itemDetail in ocOrderDetail) //Se agregan los detalles de orden a la orden.
                            {
                                ucProductWithButtons nwProduct = new ucProductWithButtons();
                                nwProduct.ucProductOrder.DataContext = itemDetail;
                                nwProduct.ucProductOrder.ProductName = mantPro.Consultarproducto(itemDetail.DOR_FK_PRODUCTO).PRO_NOMBRE;
                                nwProduct.ucProductOrder.Value = Convert.ToDouble(mantPro.Consultarproducto(itemDetail.DOR_FK_PRODUCTO).PRO_PRECIO);
                                nwProduct.ucProductOrder.Total = itemDetail.DOR_CANTIDAD;
                                nwProduct.btnDeleteClick+=nwProduct_btnDeleteClick;
                                ocProductsDrop.Add(nwProduct);
                                lstDrop.Items.Add(nwProduct);

                                subtotal = Convert.ToDouble(itemDetail.DOR_CANTIDAD * mantPro.Consultarproducto(itemDetail.DOR_FK_PRODUCTO).PRO_PRECIO);
                                total += subtotal;

                                tbxTotal.Text = "  ₡"+ total.ToString();
                            }

                        }
                    }
               }
               else //si no hay orden activa se manda a crear una. 
               {
                   //nwOrders. = App.Users.ElementAt(0).IdUsuario;//poner usuario actual en la aplicacion
                   nwOrders.ORD_ESTADO = true;
                   nwOrders.ORD_FECHA = DateTime.Now;
                   nwOrders.ORD_FK_MOBILIARIO = pCodeFurniture;
                   nwOrders.ORD_TOTAL = 0;
                   nwOrders.ORD_IMPUESTO = true;
                   nwOrderSave = blOrders.AgregarOrden(nwOrders);
               }
           }
           catch
           {
               //ewMessage nwError = new ewMessage(ErrorMessages.errorGenerateOrder, ErrorMessages.titleOrder);
               //nwError.ShowDialog();
               //if (nwError.DialogResult == true)
               //    nwError.Close();
           }
       }

        /// <summary>
        /// Agregar cada producto arrastrado a la orden
        /// </summary>
        /// <param name="pOrderDetail">El detalle de orden arrastrado</param>
       private void AddDetailOrder(eREST_DETALLEORDENES pOrderDetail)
       {
           MantenimientoDetalleOrden nwOrderDetail = new MantenimientoDetalleOrden(); 
           nwOrderDetail.AgregarDetalleOrden(pOrderDetail); //Registra el detalle de orden al soltarlo. 
         
       }

        /// <summary>
        /// Realiza las modificaciones a al detalle de orden
        /// </summary>
        /// <param name="pOrderDetail">Recibe los detalles de orden a modificar</param>
       private void UpdateDetailOrder(eREST_DETALLEORDENES pOrderDetail)
       {
           MantenimientoDetalleOrden nwOrderDetail = new MantenimientoDetalleOrden();
           MantenimientoDetalleOrden blOrders = new MantenimientoDetalleOrden();
           List<eREST_DETALLEORDENES> ocOrdersDetail = new List<eREST_DETALLEORDENES>(); //Ordenes 
           blOrders.EditarDetalleOrden(pOrderDetail);
          
       }

       #endregion
    }
}
