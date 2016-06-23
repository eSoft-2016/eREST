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
    /// CustomControl que representa los botones relacionados con mantenimiento en la aplicación
    /// </summary>
    public class ccMaintenanceButton : Button 
    {

        #region DependencyProperties

        //Propiedad de dependencia para cargar una imagen en un boton. 

        public static DependencyProperty propIcon = DependencyProperty.Register("Icon", typeof(string), typeof(ccMaintenanceButton),
                                                                                new PropertyMetadata(fcIcon));

        #endregion

        #region Properties

        /// <summary>
        /// Obtiene la imagen del boton
        /// </summary>

        public string Icon
        {
            get { return (string)GetValue(propIcon); }
            set { SetValue(propIcon, value); }
        }

        #endregion

        #region Constructor
        static ccMaintenanceButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ccMaintenanceButton), new FrameworkPropertyMetadata(typeof(ccMaintenanceButton)));
        }

        #endregion

        #region Private Methods
        private static void fcIcon(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ccMaintenanceButton nwButton = (ccMaintenanceButton)d;
            nwButton.Icon = e.NewValue as string;
        }

        #endregion

    }
}    
