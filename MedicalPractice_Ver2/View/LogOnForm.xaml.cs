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
using System.Windows.Shapes;

namespace MedicalPractice_Ver2.View
{
    /// <summary>
    /// Interaction logic for LogOnForm.xaml
    /// </summary>
    public partial class LogOnForm : Window
    {
        public LogOnForm()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Window main = new MainWindow();
            this.Hide();
            main.ShowDialog();
            this.Close();
        }
    }
}
