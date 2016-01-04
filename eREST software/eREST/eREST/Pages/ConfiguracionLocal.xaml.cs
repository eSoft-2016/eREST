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

using eREST.CustomControls;
using eREST.Resources;


using System.Net;
using System.Collections.ObjectModel;


namespace eREST.Pages
{
    /// <summary>
    /// Lógica de interacción para ConfiguracionLocal.xaml
    /// </summary>
    public partial class ConfiguracionLocal : Page
    {

        #region Global Variables

        int counterTables = 0, counterBar = 0;  //Contador para asignar el numero de mesa y contador para asignar el numero de barra.

        double positionLeft, positionRight, positionTop, positionBottom; //Posiciones del componente arrastrado
        double limitLeftRg1, limitRightRg1, limitTopRg1, limitBottomRg1; //Posiciones limite del rectangulo 1

        List<ccComponentBar> lsComponents = new List<ccComponentBar>();

        #endregion

        #region Constructor
        public ConfiguracionLocal()
        {
            InitializeComponent();
          

            Loaded += LocalConfiguration_Loaded;

            cnvParent.Visibility = Visibility.Visible;
            btnNew.Visibility = Visibility.Visible;
  

            #region Limits Rectangle One
            limitLeftRg1 = Canvas.GetLeft(rgLimitSquare); //Inicio los limites del rectangulo 1 donde se
            limitRightRg1 = limitLeftRg1 + rgLimitSquare.Width; //Admite posicionar objetos. 

            limitTopRg1 = Canvas.GetTop(rgLimitSquare);
            limitBottomRg1 = limitTopRg1 + rgLimitSquare.Height;
            #endregion

            
        }
        #endregion


        #region Private Events

        void LocalConfiguration_Loaded(object sender, RoutedEventArgs e)
        {
            //GetLocalConfiguration();
           
        }

        private void ccComponentBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ccComponentBar component = (ccComponentBar)sender;

            CreateFurnitures(component);

        }

        private void nwComponentBar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ccComponentBar nwComponent = (ccComponentBar)sender;
            try
            {
                if (nwComponent != null)
                {
                    if ((limitLeftRg1 < positionLeft) && (limitRightRg1 > positionRight) && (limitTopRg1 < positionTop) && (limitBottomRg1 > positionBottom))
                    { // No permite que se suele el mouse si no se esta en una posicion adecuada. 

                        nwComponent.IsInvalidePosition = String.Empty;
                        nwComponent.ReleaseMouseCapture();
                    }
                }
            }
            catch
            {
                //ewMessage nwError = new ewMessage(ErrorMessages.errorLocalConfPosition, ErrorMessages.titleLocalConfig);
                //nwError.ShowDialog();
                //if (nwError.DialogResult == true)
                //    nwError.Close();
            }
        }

        private void nwComponentBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ccComponentBar nwComponent = (ccComponentBar)sender;
            try
            {
                if (nwComponent != null)//Captura el mouse mientras se tenga el click presionado.
                {
                    nwComponent.CaptureMouse();
                }
            }
            catch
            {
                //ewMessage nwError = new ewMessage(ErrorMessages.errorLocalConfPosition, ErrorMessages.titleLocalConfig);
                //nwError.ShowDialog();
                //if (nwError.DialogResult == true)
                //    nwError.Close();
            }
        }

        private void nwComponentBar_MouseMove(object sender, MouseEventArgs e)
        {
            ccComponentBar nwComponent = (ccComponentBar)sender;
            try
            {
                if (nwComponent != null && nwComponent.IsMouseCaptured)
                {
                    positionLeft = Canvas.GetLeft(nwComponent);
                    positionRight = positionLeft + nwComponent.ActualWidth; //Obtengo las posiciones del mueble dentro del canvas

                    positionTop = Canvas.GetTop(nwComponent);
                    positionBottom = positionTop + nwComponent.ActualHeight;

                    if ((limitLeftRg1 < positionLeft) && (limitRightRg1 > positionRight) && (limitTopRg1 < positionTop) && (limitBottomRg1 > positionBottom))
                    {
                        foreach (ccComponentBar item in lsComponents)
                        {
                            if (nwComponent.PositionX + nwComponent.Height == item.PositionX && nwComponent.PositionY + Width == item.PositionY)
                                nwComponent.IsInvalidePosition = OthersResoures.DeniedPosition;
                            else
                                nwComponent.IsInvalidePosition = OthersResoures.ApprovedPosition;
                        }
                    }
                    else
                    {
                        nwComponent.IsInvalidePosition = OthersResoures.DeniedPosition;
                    }

                    if (e.GetPosition(cnvParent).X > -30 && e.GetPosition(cnvParent).X < 1072 && e.GetPosition(cnvParent).Y > 0 && e.GetPosition(cnvParent).Y < 515)
                    {
                        Canvas.SetLeft(nwComponent, e.GetPosition(cnvParent).X);  //Posiciona el elemento en el lugar que se encuentre el mouse 
                        Canvas.SetTop(nwComponent, e.GetPosition(cnvParent).Y);   //dentro del canvas. 
                    }
                }
            }
            catch
            {
                //ewMessage nwError = new ewMessage(ErrorMessages.errorMoveFurniture, ErrorMessages.titleLocalConfig);
                //nwError.ShowDialog();
                //if (nwError.DialogResult == true)
                //    nwError.Close();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //blFurniture = new blFurniture();

            //blFurniture.DeleteFurniture();

            //SaveLocalConfiguration(lsComponents);
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            //NewLocalConfiguration();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// Crea los muebles y le asigna los eventos correspondientes
        /// </summary>
        /// <param name="pComponent">Recibe un objeto de tipo ccComponentBar que representa un tipo de mueble</param>
        private void CreateFurnitures(ccComponentBar pComponent)
        {
            try
            {
                if (pComponent != null)
                {
                    ccComponentBar nwComponentBar = new ccComponentBar(); //Crea un nuevo mueble de manera dinamica 
                    nwComponentBar.Width = pComponent.Width;               //y le asigna los atributos del mueble seleccionado
                    nwComponentBar.Height = pComponent.Height;
                    nwComponentBar.Type = pComponent.Type;
                    nwComponentBar.Style = pComponent.Style;

                    if (nwComponentBar.Type.Equals("M4") || nwComponentBar.Type.Equals("M6")) //Verifico que sea una mesa para asignarle un numero
                    {
                        counterTables++;
                        nwComponentBar.Identify = counterTables.ToString();
                    }
                    if (nwComponentBar.Type.Equals("B"))
                    {
                        counterBar++;
                        nwComponentBar.Tag = counterBar;
                    }

                    nwComponentBar.MouseLeftButtonDown += new MouseButtonEventHandler(nwComponentBar_MouseLeftButtonDown);
                    nwComponentBar.MouseLeftButtonUp += new MouseButtonEventHandler(nwComponentBar_MouseLeftButtonUp);
                    nwComponentBar.MouseMove += new MouseEventHandler(nwComponentBar_MouseMove);

                    Canvas.SetTop(nwComponentBar, pComponent.Margin.Top); // Posiciona en el Canvas el mueble. 
                    Canvas.SetLeft(nwComponentBar, pComponent.Margin.Left);

                    cnvParent.Children.Add(nwComponentBar); //Agrega al canvas el nuevo objeto

                    nwComponentBar.CaptureMouse();

                    lsComponents.Add(nwComponentBar); //Guarda cada uno de los elementos creados.
                }
            }
            catch
            {
                //ewMessage nwError = new ewMessage(ErrorMessages.errorCreateFurniture, ErrorMessages.titleLocalConfig);
                //nwError.ShowDialog();
                //if (nwError.DialogResult == true)
                //    nwError.Close();
            }
        }

        /// <summary>
        /// Guarda la configuracion grafica del local
        /// </summary>
        /// <param name="pListComponents">Recibe una lista de muebles.</param>
        private void SaveLocalConfiguration(List<ccComponentBar> pListComponents)
        {
            try
            {
                foreach (ccComponentBar itemList in pListComponents)
                {
                    //Guarda las posiciones de las mesas en la lista, se asignan al objeto mueble hasta que se decide guardar la configuracion por el posible cambio
                    //de posicion con el "drag and drop"
                    itemList.PositionX = Canvas.GetLeft(itemList);
                    itemList.PositionY = Canvas.GetTop(itemList);

                    //nwFurniture = new clFurniture();

                    //nwFurniture.Tipo = itemList.Type.ToString();
                    //nwFurniture.PosicionX = itemList.PositionX;
                    //nwFurniture.PosicionY = itemList.PositionY;

                    //if (itemList.Type.Equals("M4") || itemList.Type.Equals("M6"))
                    //    nwFurniture.Numeracion = int.Parse(itemList.Identify);
                    //if (itemList.Type.Equals("B"))
                    //    nwFurniture.Numeracion = int.Parse(itemList.Tag.ToString());

                    //nwFurniture.FechaCreacion = DateTime.Now;

                    //nwFurniture.Usuario_Creacion = 2; //PRUEBA RECORDAR CAMBIAR POR EL USUARIO LOGEADO.

                    //SaveFurnitures(nwFurniture);
                }
            }
            catch
            {
                //ewMessage nwError = new ewMessage(ErrorMessages.errorSaveLocalConf, ErrorMessages.titleLocalConfig);
                //nwError.ShowDialog();
                //if (nwError.DialogResult == true)
                //    nwError.Close();
            }


        }

        /// <summary>
        /// Llama al procedimiento almacenado para que se registren cada uno de los muebles
        /// </summary>
        /// <param name="pNwFurniture"> Recibe la informacion de cada objeto de tipo mueble </param>
        //private void SaveFurnitures(clFurniture pNwFurniture)
        //{

        //    blFurniture = new blFurniture();

        //    if (!(blFurniture.RegisterFurniture(pNwFurniture)))
        //    {
        //        ewMessage nwError = new ewMessage(ErrorMessages.errorCreateFurniture, ErrorMessages.titleLocalConfig);
        //        nwError.ShowDialog();
        //        if (nwError.DialogResult == true)
        //            nwError.Close();
        //    }
        //}

        /// <summary>
        /// Obtiene la configuración de local almacenada en la base de datos.
        /// </summary>
        //private void GetLocalConfiguration()
        //{
        //    try
        //    {
        //        listFurniture = blFurniture.FurnitureList(); //Saco la lista de muebles de base de datos.

        //        ObservableCollection<clFurniture> listFurnitureTables = new ObservableCollection<clFurniture>();
        //        ObservableCollection<clFurniture> listFurnitureBar = new ObservableCollection<clFurniture>();

        //        foreach (clFurniture itemList in listFurniture)
        //        {
        //            if (itemList.Tipo.Equals("M4") || itemList.Tipo.Equals("M6"))
        //                listFurnitureTables.Add(itemList);
        //            if (itemList.Tipo.Equals("B"))
        //                listFurnitureBar.Add(itemList);
        //        }

        //        var listMaxTables = listFurnitureTables.OrderByDescending(x => x.Numeracion);
        //        var listMaxAux = listFurnitureBar.OrderByDescending(x => x.Numeracion);

        //        if (listMaxTables.Count() > 0)
        //            counterTables = listMaxTables.ElementAt(0).Numeracion;

        //        if (listMaxAux.Count() > 0)
        //            counterBar = listMaxAux.ElementAt(0).Numeracion;


        //        foreach (clFurniture itemFurniture in listFurniture)
        //        {
        //            ccComponentBar nwComponentBar = new ccComponentBar(); //Crea un nuevo mueble de manera dinamica 
        //            nwComponentBar.Width = 50;               //y le asigna los atributos del mueble seleccionado
        //            nwComponentBar.Height = 50;
        //            nwComponentBar.Type = itemFurniture.Tipo;
        //            nwComponentBar.Style = Application.Current.FindResource("stCompenentBar") as Style;

        //            if (nwComponentBar.Type.Equals("M4") || nwComponentBar.Type.Equals("M6")) //Verifico que sea una mesa para asignarle un numero
        //                nwComponentBar.Identify = itemFurniture.Numeracion.ToString();

        //            if (nwComponentBar.Type.Equals("M4") || nwComponentBar.Type.Equals("M6") || nwComponentBar.Type.Equals("B"))
        //                nwComponentBar.MouseLeftButtonDown += nwComponentBar_MouseLeftButtonDown;

        //            Canvas.SetLeft(nwComponentBar, itemFurniture.PosicionX);
        //            Canvas.SetTop(nwComponentBar, itemFurniture.PosicionY);


        //            cnvParent.Children.Add(nwComponentBar);

        //            lsComponents.Add(nwComponentBar);
        //        }
        //    }
        //    catch
        //    {
        //        ewMessage nwError = new ewMessage(ErrorMessages.errorGetLocalConf, ErrorMessages.titleLocalConfig);
        //        nwError.ShowDialog();
        //        if (nwError.DialogResult == true)
        //            nwError.Close();
        //    }
        //}

        ///// <summary>
        /// Limpia la base de datos de la configuración anterior y permite cargar una nueva.
        /// </summary>
        private void NewLocalConfiguration()
        {
            try
            {
                    foreach (ccComponentBar itemList in lsComponents)
                    cnvParent.Children.Remove(itemList);
                    lsComponents.Clear();

                    counterBar = 0;
                    counterTables = 0;
          
            }
            catch
            {
                //ewMessage nwError = new ewMessage(ErrorMessages.errorClearLocalConf, ErrorMessages.titleLocalConfig);
                //nwError.ShowDialog();
                //if (nwError.DialogResult == true)
                //    nwError.Close();
            }

        }

        #endregion
 
    }
}
