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

namespace eREST.UserControls
{
    /// <summary>
    /// Lógica de interacción para ucProductOrder.xaml
    /// </summary>
    public partial class ucProductOrder : UserControl
    {
        #region Dependency Properties

        //Propiedad de dependencia para asignar la imagen del producto

        public static DependencyProperty propImage = DependencyProperty.Register("Image", typeof(ImageSource), typeof(ucProductOrder),
                                                                                new PropertyMetadata(fcImage));


        // Propiedad de dependencia para el nombre del producto
        public static DependencyProperty propName = DependencyProperty.Register("ProductName", typeof(string), typeof(ucProductOrder),
                                                                                new PropertyMetadata(fcName));


        // Propiedad de dependencia para mostrar el precio del producto
        public static DependencyProperty propValue = DependencyProperty.Register("Value", typeof(double), typeof(ucProductOrder),
                                                                                new PropertyMetadata(fcValue));


        //Propiedad de dependencia para asignar el total de productos arrastrados (de la misma clase)

        public static DependencyProperty propTotal = DependencyProperty.Register("Total", typeof(double), typeof(ucProductOrder),
                                                                                new PropertyMetadata(fcTotal));


        #endregion

        #region Properties

        /// <summary>
        /// Obtiene la imagen del producto
        /// </summary>
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(propName); }
            set { SetValue(propName, value); }
        }

        /// <summary>
        /// Obtiene el nombre del producto
        /// </summary>
        public string ProductName
        {
            get { return (string)GetValue(propName); }
            set { SetValue(propName, value); }
        }

        /// <summary>
        /// Obtiene el precio del producto
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(propValue); }
            set { SetValue(propValue, value); }
        }

        /// <summary>
        /// Obtiene la cantidad del mismo producto
        /// </summary>
        public double Total
        {
            get { return (double)GetValue(propTotal); }
            set { SetValue(propTotal, value); }
        }


        #endregion

        #region Constructor
        public ucProductOrder()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        private static void fcImage(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ucProductOrder nwProduct = (ucProductOrder)d;
            nwProduct.Image = e.NewValue as ImageSource;

        }

        private static void fcName(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ucProductOrder nwProduct = (ucProductOrder)d;
            nwProduct.Name = e.NewValue.ToString();

            nwProduct.tbxName.Text = nwProduct.Name;
        }

        private static void fcValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ucProductOrder nwProduct = (ucProductOrder)d;
            nwProduct.Value = (double)e.NewValue;

            nwProduct.tbxValue.Text = "₡" + nwProduct.Value.ToString();
        }

        private static void fcTotal(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ucProductOrder nwProduct = (ucProductOrder)d;

            nwProduct.Total = double.Parse(e.NewValue.ToString());

            nwProduct.tbxTotal.Text = "x" + nwProduct.Total.ToString();

        }

        #endregion
    }
}
