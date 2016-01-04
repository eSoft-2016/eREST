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
using System.IO;
using System.Windows.Threading;
using System.Diagnostics;
using System.Timers;



namespace eREST.Pages
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Productos : Page
    {

        #region Global Variables

       

        #endregion

        #region Constructor
        public Productos()
        {
            InitializeComponent();  
        }
 
        
        #endregion

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            wmProducto ventis = new wmProducto();
            ventis.ShowDialog();}
        }

        #region Private Events
 
        #endregion

     
    }



