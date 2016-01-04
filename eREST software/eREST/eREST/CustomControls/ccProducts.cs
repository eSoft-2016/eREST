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

namespace Asterisk.CustomControls
{
    public class ccProducts : Button
    {

        #region DependencyProperties

        //Propiedad de dependencia para cargar el nombre del producto en el boton.
        public static DependencyProperty TitleProp = DependencyProperty.Register("ProductName", typeof(string), typeof(ccProducts),
                                                                                  new UIPropertyMetadata(TituloPropAct));

        //Propiedad de dependencia para cargar el detalle del producto en el boton.
        public static DependencyProperty DetailProp = DependencyProperty.Register("ProductDetail", typeof(string), typeof(ccProducts),
                                                                                   new UIPropertyMetadata(DetallePropAct));
       
        //Propiedad de dependencia para cargar el valor del producto en el boton.
        public static DependencyProperty ValueProp = DependencyProperty.Register("ProductValue", typeof(string), typeof(ccProducts),
                                                                                  new UIPropertyMetadata(ValorPropAct));
        //Propiedad de dependencia para cargar el valor de que si el producto existe en el boton.
        public static DependencyProperty ExistProp = DependencyProperty.Register("ProductExist", typeof(bool), typeof(ccProducts),
                                                                                  new UIPropertyMetadata(ExistPropAct));
        
        //Propiedad de dependencia para cargar una imagen del producto en el boton.
        public static DependencyProperty ImageProp = DependencyProperty.Register("Image", typeof(ImageSource), typeof(ccProducts),
                                                                                 new PropertyMetadata(fcImage));
        
        #endregion

        #region Properties

        /// <summary>
        /// Obtiene el nombre del Producto
        /// </summary>
        public string ProductName
        {
            get { return (string)GetValue(TitleProp); }
            set { SetValue(TitleProp, value); }
        }

        /// <summary>
        /// Obtiene las Caracteristicas  del Producto
        /// </summary>
        public string ProductDetail
        {
            get { return (string)GetValue(DetailProp); }
            set { SetValue(DetailProp, value); }
        }
        /// <summary>
        /// Obtiene el valor del Producto
        /// </summary>
        public string ProductValue
        {
            get { return (string)GetValue(ValueProp); }
            set { SetValue(ValueProp, value); }
        } 
        /// <summary>
        /// Obtiene el valor de que si el Producto existe
        /// </summary>
        public bool ProductExist
        {
            get { return (bool)GetValue(ExistProp); }
            set { SetValue(ExistProp, value); }
        }

        /// <summary>
        /// Obtiene la imagen del Producto
        /// </summary>

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProp); }
            set { SetValue(ImageProp, value); }
        }


        #endregion

        #region Constructor

        static ccProducts()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ccProducts), new FrameworkPropertyMetadata(typeof(ccProducts)));
        }

        #endregion

        #region Private Methods

        private static void TituloPropAct(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ccProducts nwProduct = (ccProducts)d;
            nwProduct.ProductName = e.NewValue as string;
        }

        private static void DetallePropAct(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ccProducts nwProduct = (ccProducts)d;
            nwProduct.ProductDetail = e.NewValue as string;
        }

        private static void ValorPropAct(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ccProducts nwProduct = (ccProducts)d;
            nwProduct.ProductValue = e.NewValue as string;
        }
        private static void ExistPropAct(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ccProducts nwProduct = (ccProducts)d;
            nwProduct.ProductExist = (bool)e.NewValue;
        }

        private static void fcImage(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ccProducts nwProduct = (ccProducts)d;
            nwProduct.Image = e.NewValue as ImageSource;
        }

        #endregion

    }
}
