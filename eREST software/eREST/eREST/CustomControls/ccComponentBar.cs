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

using eREST.Resources;

namespace eREST.CustomControls
{
  
    /// <summary>
    /// CustomControl para representar los componentes del bar (mesas, barras, 
    /// </summary>
    public class ccComponentBar : Control
    {

        #region Dependency Properties

        //Propiedad de dependencia para asignar la imagen del mueble

        public static DependencyProperty propImage = DependencyProperty.Register("Image", typeof(string), typeof(ccComponentBar),
                                                                                new PropertyMetadata(fcImage));

        //Propiedad de dependencia para mostrar un icon al margen de la imagen principal del mueble para representar una posicion invalida del mismo 

        public static DependencyProperty propInvalidePosition = DependencyProperty.Register("IsInvalidePosition", typeof(string), typeof(ccComponentBar),
                                                                new PropertyMetadata(fcInvalidePosition));

        //Propiedad de dependencia para identificar el mueble con la numeración correspondiente. 

        public static DependencyProperty propIdentify = DependencyProperty.Register("Identify", typeof(string), typeof(ccComponentBar),
                                                                               new PropertyMetadata(fcIdentify));

        //Propiedad de dependencia para identificar el  tipo mueble

        public static DependencyProperty propType = DependencyProperty.Register("Type", typeof(string), typeof(ccComponentBar),
                                                                                new PropertyMetadata(fcType));


        //Propiedad de dependencia para definir la posicion en X

        public static DependencyProperty propPositionX = DependencyProperty.Register("PositionX", typeof(double), typeof(ccComponentBar),
                                                                                new PropertyMetadata(fcPositionX));

        //Propiedad de dependencia para definir la posicion en Y

        public static DependencyProperty propPositionY = DependencyProperty.Register("PositionY", typeof(double), typeof(ccComponentBar),
                                                                                new PropertyMetadata(fcPositionY));

        #endregion

        #region Properties


        /// <summary>
        /// Obtiene la imagen correspondiente al mueble
        /// </summary>

        public string Image
        {
            get { return (string)GetValue(propImage); }
            set { SetValue(propImage, value); }
        }

        /// <summary>
        /// Obtiene si la posicion del mueble dentro del área limitada es valida. 
        /// </summary>

        public string IsInvalidePosition 
        {
            get { return (string)GetValue(propInvalidePosition); }
            set { SetValue(propInvalidePosition, value); }
        }

        /// <summary>
        /// Obtiene el número de identificación de mueble
        /// </summary>

        public string Identify
        {
            get { return (string)GetValue(propIdentify); }
            set { SetValue(propIdentify, value); }
        }

        /// <summary>
        /// Obtiene el tipo de mueble
        /// </summary
        
        public string Type
        {
            get { return (string)GetValue(propType); }
            set { SetValue(propType, value); }
        }

        /// <summary>
        /// Obtiene la posicion en el eje X
        /// </summary

        public double PositionX
        {
            get { return (double)GetValue(propPositionX); }
            set { SetValue(propPositionX, value); }
        }

        /// <summary>
        /// Obtiene la posicion en el eje Y
        /// </summary

        public double PositionY
        {
            get { return (double)GetValue(propPositionY); }
            set { SetValue(propPositionY, value); }
        }

        #endregion

        #region Constructor
        static ccComponentBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ccComponentBar), new FrameworkPropertyMetadata(typeof(ccComponentBar)));
        }

        #endregion

        #region Private Methods

        private static void fcInvalidePosition(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ccComponentBar nwComponentBar = (ccComponentBar)d;
            nwComponentBar.IsInvalidePosition = (string)e.NewValue;
        }

        private static void fcImage(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ccComponentBar nwComponentBar = (ccComponentBar)d;
            nwComponentBar.Image = (string)e.NewValue;
        }

        private static void fcIdentify(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ccComponentBar nwComponentBar = (ccComponentBar)d;
            nwComponentBar.Identify = (string)e.NewValue;
        }

        //En este método se asigna la imagen según el tipo de mueble que se está creando
        private static void fcType(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ccComponentBar nwComponentBar = (ccComponentBar)d;
            nwComponentBar.Type = (string)e.NewValue;
            nwComponentBar.Image = OthersResoures.ResourceManager.GetString(nwComponentBar.Type); //Obtiene el nombre del recurso para la imagen.
        }

        private static void fcPositionX(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ccComponentBar nwComponent = (ccComponentBar)d;
            nwComponent.PositionX = (double)e.NewValue;
        }
        private static void fcPositionY(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ccComponentBar nwComponent = (ccComponentBar)d;
            nwComponent.PositionY = (double)e.NewValue; 
        }

        #endregion
      
    }
}
