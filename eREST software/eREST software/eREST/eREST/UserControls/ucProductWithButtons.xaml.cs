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
    /// Lógica de interacción para ucProductWithButtons.xaml
    /// </summary>
    public partial class ucProductWithButtons : UserControl
    {
        #region Constructor
        public ucProductWithButtons()
        {
            InitializeComponent();
        }
        #endregion

        #region Custom Events

        public event RoutedEventHandler btnSelectClick;
        public event RoutedEventHandler btnDeleteClick;
        public event RoutedEventHandler ckSelectCheck;

        #endregion

        #region Private Events

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (btnSelectClick != null)
                btnSelectClick(this, new RoutedEventArgs());
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (btnDeleteClick != null)
                btnDeleteClick(this, new RoutedEventArgs());
        }

        private void ckSelect_Checked(object sender, RoutedEventArgs e)
        {
            if (ckSelectCheck != null)
                ckSelectCheck(this, new RoutedEventArgs());
        }

        #endregion
    }
}
