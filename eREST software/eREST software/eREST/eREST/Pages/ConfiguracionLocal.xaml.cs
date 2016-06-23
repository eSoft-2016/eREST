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
using eREST.BO;
using eREST.ModalWindows.MaintenanceWindows;
using eREST.BL.Local;
using eREST.BL.Mobiliario;


namespace eREST.Pages
{
    /// <summary>
    /// Lógica de interacción para ConfiguracionLocal.xaml
    /// </summary>
    public partial class ConfiguracionLocal : Page
    {

        #region Global Variables

        int contadorMobiliario = 0;

        double positionLeft, positionRight, positionTop, positionBottom; //Posiciones del componente arrastrado
        double limitLeftRg1, limitRightRg1, limitTopRg1, limitBottomRg1; //Posiciones limite del rectangulo 1

        List<ccComponentBar> lsComponents = new List<ccComponentBar>();
        List<eREST_MOBILIARIOS> lsMobiliarios = new List<eREST_MOBILIARIOS>();
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
            GetLocalConfiguration();
           
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
                        wmSeletSector wmSec = new wmSeletSector();
                        wmSec.ShowDialog();
                        if (wmSec.DialogResult == true)
                        {
                            ccComponentBar New = new ccComponentBar();
                            for (int i = 0; i < lsComponents.Count; i++)
                            {
                                if(lsComponents[i].Identify == nwComponent.Identify)
                                lsComponents[i].ISec = wmSec.Tag.ToString();
                            } 
                            nwComponent.IsInvalidePosition = String.Empty;
                            nwComponent.ReleaseMouseCapture();

                        }
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
             SaveLocalConfiguration(lsComponents);
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            NewLocalConfiguration();
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
                    contadorMobiliario++;
                    nwComponentBar.Identify = contadorMobiliario.ToString();
                  

                    nwComponentBar.MouseLeftButtonDown += new MouseButtonEventHandler(nwComponentBar_MouseLeftButtonDown);
                    nwComponentBar.MouseLeftButtonUp += new MouseButtonEventHandler(nwComponentBar_MouseLeftButtonUp);
                    nwComponentBar.MouseMove += new MouseEventHandler(nwComponentBar_MouseMove);
                    nwComponentBar.MouseRightButtonDown += new MouseButtonEventHandler(nwComponentBar_MouseRightButtonDown);

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

        void nwComponentBar_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ccComponentBar ccC = sender as ccComponentBar;
            wmEliminar wmEli = new wmEliminar();
            wmEli.ShowDialog();
            if (wmEli.DialogResult == true)
            {
                MantenimientoLocal mantLo = new MantenimientoLocal();
                mantLo.EliminarExistenciaDB(Convert.ToInt32(ccC.Identify));
                lsComponents.Remove(ccC);
                cnvParent.Children.Remove(ccC);
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
                    eREST_MOBILIARIOS newMobiliario = new eREST_MOBILIARIOS();
                    MantenimientoMobiliario mantMob= new MantenimientoMobiliario();
                    MantenimientoLocal mantLo = new MantenimientoLocal();
                    
                    newMobiliario.MOB_COORX = itemList.PositionX;
                    newMobiliario.MOB_COORY = itemList.PositionY; 
                    newMobiliario.MOB_ESTADO= true;
                    newMobiliario.MOB_FK_TIPOMOBILIARIO = mantMob.ConsultarMoviliarionombre(itemList.Type);
                    newMobiliario.MOB_FK_SECTOR = Convert.ToInt16(itemList.ISec);
                    newMobiliario.MOB_NUMERO = Convert.ToInt16(itemList.Identify);
                    newMobiliario.MOB_PK_MOBILIARIO = Convert.ToInt32(itemList.PK);
                    if (mantLo.ConsultarExistenciaDB(Convert.ToInt32(newMobiliario.MOB_NUMERO)))
                    { 
                        mantLo.EditarMobiliario(newMobiliario);
                    }
                    else
                    {
                        mantLo.AgregarMobiliario(newMobiliario);
                    }
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
        private void GetLocalConfiguration()
        {
            try
            {
                MantenimientoLocal ManLoc = new MantenimientoLocal();
                lsMobiliarios = ManLoc.ListarMobiliarios(); //Saco la lista de muebles de base de datos. 
                contadorMobiliario = lsMobiliarios.Count;
                foreach (eREST_MOBILIARIOS itemFurniture in lsMobiliarios)
                {
                    MantenimientoMobiliario ManMo = new MantenimientoMobiliario();
                    ccComponentBar nwComponentBar = new ccComponentBar(); //Crea un nuevo mueble de manera dinamica 
                    nwComponentBar.Width = 50;               //y le asigna los atributos del mueble seleccionado
                    nwComponentBar.Height = 50;
                    nwComponentBar.Type = ManMo.ConsultarMoviliarioLlave(itemFurniture.MOB_FK_TIPOMOBILIARIO);
                    nwComponentBar.Style = Application.Current.FindResource("stCompenentBar") as Style;
                    nwComponentBar.Identify = itemFurniture.MOB_NUMERO.ToString();
                    nwComponentBar.ISec = itemFurniture.MOB_FK_SECTOR.ToString();
                     
                    nwComponentBar.MouseLeftButtonDown += new MouseButtonEventHandler(nwComponentBar_MouseLeftButtonDown);
                    nwComponentBar.MouseLeftButtonUp += new MouseButtonEventHandler(nwComponentBar_MouseLeftButtonUp);
                    nwComponentBar.MouseMove += new MouseEventHandler(nwComponentBar_MouseMove);
                    nwComponentBar.MouseRightButtonDown += new MouseButtonEventHandler(nwComponentBar_MouseRightButtonDown);
                    Canvas.SetLeft(nwComponentBar, itemFurniture.MOB_COORX);
                    Canvas.SetTop(nwComponentBar, itemFurniture.MOB_COORY);        
                    cnvParent.Children.Add(nwComponentBar); 
                    lsComponents.Add(nwComponentBar);
                }
            }
            catch
            {
                //ewMessage nwError = new ewMessage(ErrorMessages.errorGetLocalConf, ErrorMessages.titleLocalConfig);
                //nwError.ShowDialog();
                //if (nwError.DialogResult == true)
                //    nwError.Close();
            }
        }

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
                    contadorMobiliario = 0;
          
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
