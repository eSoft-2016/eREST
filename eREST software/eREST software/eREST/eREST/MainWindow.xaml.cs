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
using eREST.ModalWindows.MaintenanceWindows;
 
using System.Collections.ObjectModel;
 
namespace eREST
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        #endregion
        #region Private Events
        
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            PrincipalWindow.WindowState = System.Windows.WindowState.Minimized;
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;
            LbPage.Text = btn.Content.ToString();
            frmNavigator.Navigate(new Uri(btn.Tag.ToString(), UriKind.RelativeOrAbsolute));
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LbUser.Text = "Roberto Alvarado";
            addButton();
        }

        #endregion
        #region Private Functions

        /// <summary>
        /// Crea los botones en pantalla. 
        /// </summary>
            //for (int i = 0; i < 6; i++ )
            //    {
            //        Button btn = new Button();
            //        btn.Width = 300;
            //        btn.Height = 38;
            //        btn.Style = Application.Current.Resources["stMenuButton"] as Style;
            //        btn.Content = "Opcion #" + i; 
            //    if(i==1 || i==2)
            //        btn.Tag="Pages/Productos.xaml";
            //    else
            //        btn.Tag = "Pages/Usuarios.xaml";
        private void addButton()
        { 
            //for (int i = 0; i < 6; i++ )
            //    {
            //        Button btn = new Button();
            //        btn.Width = 300;
            //        btn.Height = 38;
            //        btn.Style = Application.Current.Resources["stMenuButton"] as Style;
            //        btn.Content = "Opcion #" + i; 
            //    if(i==1 )
            //        btn.Tag="Pages/Productos.xaml";
            //    else if (i == 2)
            //        btn.Tag = "Pages/ConfiguracionLocal.xaml";
            //    else
            //        btn.Tag = "Pages/Usuarios.xaml";

            //        btn.Click += btn_Click;
            //        stkButtons.Children.Add(btn);
            //    }
           

            //        btn.Click += btn_Click;
            //        stkButtons.Children.Add(btn);
            //    }


            Button btn = new Button();
            btn.Width = 300;
            btn.Height = 38;
            btn.Style = Application.Current.Resources["stMenuButton"] as Style;
            btn.Content = "Sectores";
            btn.Tag = "Pages/Sectores.xaml";

            btn.Click += btn_Click;
            stkButtons.Children.Add(btn);


            Button btn1 = new Button();
            btn1.Width = 300;
            btn1.Height = 38;
            btn1.Style = Application.Current.Resources["stMenuButton"] as Style;
            btn1.Content = "Configuracion del Local";
            btn1.Tag = "Pages/ConfiguracionLocal.xaml";

            btn1.Click += btn_Click;
            stkButtons.Children.Add(btn1);

            Button btn2 = new Button();
            btn2.Width = 300;
            btn2.Height = 38;
            btn2.Style = Application.Current.Resources["stMenuButton"] as Style;
            btn2.Content = "Empleados";
            btn2.Tag = "Pages/Empleados.xaml";

            btn2.Click += btn_Click;
            stkButtons.Children.Add(btn2);

            Button btn6 = new Button();
            btn6.Width = 300;
            btn6.Height = 38;
            btn6.Style = Application.Current.Resources["stMenuButton"] as Style;
            btn6.Content = "Empresas";
            btn6.Tag = "Pages/Empresas.xaml";

            btn6.Click += btn_Click;
            stkButtons.Children.Add(btn6);

            Button btn3 = new Button();
            btn3.Width = 300;
            btn3.Height = 38;
            btn3.Style = Application.Current.Resources["stMenuButton"] as Style;
            btn3.Content = "Productos";
            btn3.Tag = "Pages/Productos.xaml";

            btn3.Click += btn_Click;
            stkButtons.Children.Add(btn3);

            Button btn4 = new Button();
            btn4.Width = 300;
            btn4.Height = 38;
            btn4.Style = Application.Current.Resources["stMenuButton"] as Style;
            btn4.Content = "Bodegas";
            btn4.Tag = "Pages/Bodegas.xaml";

            btn4.Click += btn_Click;
            stkButtons.Children.Add(btn4);

            Button btn5 = new Button();
            btn5.Width = 300;
            btn5.Height = 38;
            btn5.Style = Application.Current.Resources["stMenuButton"] as Style;
            btn5.Content = "Insumos";
            btn5.Tag = "Pages/Insumos.xaml";

            btn5.Click += btn_Click;
            stkButtons.Children.Add(btn5);


        }
        #endregion
    }
}
