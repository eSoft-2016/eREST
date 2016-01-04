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

namespace eREST.CustomControls
{

      /// <summary>
      /// CustomControl que representa los botonos referentes al control de la pantalla (cerrar y minimizar).
      /// </summary>
    public class ccWindowButtons : Button
    {

        #region DependencyProperties

        //Propiedad de dependencia para cargar una imagen en un boton. 

        public static DependencyProperty propImage = DependencyProperty.Register("Image", typeof(string), typeof(ccWindowButtons),
                                                                                new PropertyMetadata(fcImage));
        #endregion

        #region Properties

        /// <summary>
        /// Obtiene la imagen del boton
        /// </summary>

        public string Image
        {
            get { return (string)GetValue(propImage); }
            set { SetValue(propImage, value); }
        }
        

        #endregion

        #region Constructor

        static ccWindowButtons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ccWindowButtons), new FrameworkPropertyMetadata(typeof(ccWindowButtons)));
        }

        #endregion 

        #region Private Methods

        private static void fcImage(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ccWindowButtons nwButton = (ccWindowButtons)d;
            nwButton.Image = e.NewValue as string;
        }

        #endregion
       
    }
}
