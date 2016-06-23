using eREST.BL.Local;
using eREST.BL.Mobiliario;
using eREST.BL.Producto;
using eREST.BO;
using eREST.CustomControls;
using eREST.ModalWindows.MaintenanceWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace eREST.Pages
{
    /// <summary>
    /// Lógica de interacción para Ordenes.xaml
    /// </summary>
    public partial class Ordenes : Page
    {
        #region Global Variables

        List<eREST_MOBILIARIOS> listFuniture = new List<eREST_MOBILIARIOS>();
        MantenimientoLocal mantMobi = new MantenimientoLocal();


        List<eREST_PRODUCTOS> listProducts = new List<eREST_PRODUCTOS>();
        MantenimientoProducto mantPro = new MantenimientoProducto();
        

        private bool pedido = false;

        #endregion  
        #region Constructor
        public Ordenes()
        { 
            InitializeComponent();
            Loaded += Ordenes_Loaded;
        }
        #endregion
        #region Private Events
       
        void Ordenes_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyConfiguration();
        }
        void nwComponentBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
             
                ccComponentBar nwFurnitur = (ccComponentBar)sender;
                wmOrden nwOrder = new wmOrden(nwFurnitur.DataContext as eREST_MOBILIARIOS);
                nwOrder.ShowDialog();
                if (nwOrder.DialogResult == true)
                    nwOrder.Close();
            
        }

        #endregion

        #region Private Functions

        /// <summary>
        ///  Aplica la configuracion definida en el modulo de configuracion de local.
        /// </summary>
        /// <param name="pListComponents">Recibe la lista de componentes agregados en configuracion de local.</param>
        private void ApplyConfiguration()
        {
            try
            {
                listFuniture = mantMobi.ListarMobiliarios();
                foreach (eREST_MOBILIARIOS itemFurniture in listFuniture)
                {
                    MantenimientoMobiliario mantMo = new MantenimientoMobiliario();
                    ccComponentBar nwComponentBar = new ccComponentBar(); //Crea un nuevo mueble de manera dinamica 
                    nwComponentBar.Width = 50;               //y le asigna los atributos del mueble seleccionado
                    nwComponentBar.Height = 50;
                    nwComponentBar.Type = mantMo.ConsultarMoviliarioLlave(itemFurniture.MOB_FK_TIPOMOBILIARIO);
                    nwComponentBar.Style = Application.Current.FindResource("stCompenentBar") as Style;
                    nwComponentBar.Identify = itemFurniture.MOB_NUMERO.ToString();

                   
                    if (nwComponentBar.Type.Equals("M4") || nwComponentBar.Type.Equals("M6") || nwComponentBar.Type.Equals("B"))
                        nwComponentBar.MouseLeftButtonDown += nwComponentBar_MouseLeftButtonDown;

                    Canvas.SetLeft(nwComponentBar, itemFurniture.MOB_COORX);
                    Canvas.SetTop(nwComponentBar, itemFurniture.MOB_COORY);

                    nwComponentBar.DataContext = itemFurniture;

                    cnvParent.Children.Add(nwComponentBar);
                }
            }
            catch (Exception e)
            {
                //ewMessage nwError = new ewMessage(ErrorMessages.errorGeneralList + e.Message.ToString(), UserMessages.titleFurniture);
                //nwError.ShowDialog();
                //if (nwError.DialogResult == true)
                //    nwError.Close();
            }
        }

        #endregion

        
    }
}
