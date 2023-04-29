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

namespace TChat.Views;
/// <summary>
/// Interaction logic for ToolBarView.xaml
/// </summary>
public partial class ToolBarView : UserControl
{
    public ToolBarView()
    {
        InitializeComponent();
    }

    private void BtnMinimize_Click(object sender, RoutedEventArgs e)
    {
        Window window = Window.GetWindow(this);
        window.WindowState = WindowState.Minimized;
    }

    private void BtnClose_Click(object sender, RoutedEventArgs e)
    {
        Window window = Window.GetWindow(this);
        window.Close();
    }

    private void ContextMenuButton_Click(object sender, RoutedEventArgs e)
    {
        var contextMenu = ((Button)sender).ContextMenu;
        if (contextMenu != null)
        {
            contextMenu.IsOpen = !contextMenu.IsOpen;
        }
    }

    private void MyComboBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if(sender is ComboBox comboBox)
        {
            comboBox.IsDropDownOpen = !comboBox.IsDropDownOpen;
            e.Handled = true;
        }
    }
}
