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

namespace JavaClassGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Attribute> attributelist = new List<Attribute>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        private void titlebar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        private void exit_MouseEnter(object sender, MouseEventArgs e)
        {
            //BrushConverter b = new BrushConverter();
            ((Label)sender).Foreground = Brushes.Gold;
            ((Label)sender).FontSize += 1;
        }

        private void exit_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Label)sender).Foreground = Brushes.White;
            ((Label)sender).FontSize -= 1;
        }
        private void addbutton_Click(object sender, RoutedEventArgs e)
        {
            if (attrname.Text == "")
                return;

            string type =  typebox.Text;
            attrlist.Items.Add(Helper.GetAccessorSymbol(accessorbox.SelectionBoxItem.ToString()) + " " + attrname.Text + " :" + type);
            attributelist.Add(new Attribute().Parse(attrname.Text + " " + accessorbox.SelectionBoxItem.ToString() + " " + type + " " + defaultvalue.Text));

            Update();
        }

        private void classname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (classname.Text == "")
                return;

            Update();
        }

        private void removebutton_Click(object sender, RoutedEventArgs e)
        {
            if (attrlist.SelectedIndex < 0)
                return;

            attrlist.Items.RemoveAt(attrlist.SelectedIndex);
            attributelist.RemoveAt(attrlist.SelectedIndex);

            Update();
        }
        public void Update()
        {
            output.Text = Parser.Parse(classname.Text, attributelist);
        }

        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}
