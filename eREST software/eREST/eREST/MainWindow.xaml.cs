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
            LbUser.Text = "Eduardo Venegas";
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
            btn.Content = "Empleados";
            btn.Tag = "Pages/Empleados.xaml";

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
            btn2.Content = "Empresas";
            btn2.Tag = "Pages/Empresas.xaml";

            btn2.Click += btn_Click;
            stkButtons.Children.Add(btn2);

        }
        #endregion
    }
}
