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
            InitEvents();
            Update();
        }
        public void InitEvents()
        {
            copyjavaoutput.Click += (s, e) => Clipboard.SetText(output.Text);
            copycppoutput1.Click += (s, e) => Clipboard.SetText(output2.Text);
            exit.MouseLeftButtonDown += (s, e) => System.Diagnostics.Process.GetCurrentProcess().Kill();
            exit.MouseEnter += (s, e) => { ((Label)s).Foreground = Brushes.Gold; ((Label)s).FontSize += 1; };
            exit.MouseLeave += (s, e) => { ((Label)s).Foreground = Brushes.White; ((Label)s).FontSize -= 1; };
            classname.TextChanged += (s, e) => Update();
            window.MouseLeftButtonDown += (s, e) => { if (e.LeftButton == MouseButtonState.Pressed) this.DragMove(); };
            addbutton.Click += (s, e) => AddAttribute();
            removebutton.Click += (s, e) => RemoveAttribute();
        }

        public void AddAttribute()
        {
            if (attrname.Text.Trim() == "" || typebox.Text.Trim() == "")
                return;
            foreach (Attribute a in attributelist)
                if (a.name == attrname.Text)
                {
                    MessageBox.Show("Duplicate attribute name: " + attrname.Text,"Error",MessageBoxButton.OK,MessageBoxImage.Warning);
                    return;
                }

            attrlist.Items.Add(Helper.GetAccessorSymbol(accessorbox.SelectionBoxItem.ToString()) + " " + attrname.Text + " :" + typebox.Text);
            attributelist.Add(new Attribute().Parse(attrname.Text + " " + accessorbox.SelectionBoxItem.ToString() + " " + typebox.Text + " " + staticbox.IsChecked.ToString() + " " + defaultvalue.Text));

            Update();
        }

        private void RemoveAttribute()
        {
            if (attrlist.SelectedIndex < 0)
                return;
            int current = attrlist.SelectedIndex;
            attributelist.RemoveAt(attrlist.SelectedIndex);
            attrlist.Items.RemoveAt(attrlist.SelectedIndex);

            attrlist.SelectedIndex = current > 0 ? current - 1 : 0;

            Update();
        }
        public void Update()
        {
            output.Text = Parser.Java.Parse(classname.Text.Trim().Replace(" ", ""), attributelist);
            output2.Text = Parser.Cpp.Parse(classname.Text.Trim().Replace(" ", ""), attributelist);
        }


    }
}
