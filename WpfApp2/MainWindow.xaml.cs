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
using DataLibrary;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private V4MainCollection V4Item;
        private CustomDataCollection V4CustomItem;
        public MainWindow()
        {
            InitializeComponent();
            V4Item = new V4MainCollection();
            V4CustomItem = new CustomDataCollection(V4Item);
            this.DataContext = V4Item;
            grid_AddCustom.DataContext = V4CustomItem;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            if (V4Item.is_changed == true)
            {
                const string message = "Несохранённые данные могут быть потеряны. Вы уверены, что хотите создать новый объект?";
                const string caption = "Создание нового объекта";
                var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    V4Item = new V4MainCollection();
                    DataContext = null;
                    DataContext = V4Item;
                }
                else
                {
                    Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
                    if (dialog.ShowDialog() == true)
                    {
                        V4Item.Save(dialog.FileName);
                        V4Item.is_changed = false;
                    }
                }
            }
            else
            {
                V4Item = new V4MainCollection();
                DataContext = null;
                DataContext = V4Item;
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (V4Item.is_changed == true)
            {
                const string message = "Несохранённые данные могут быть потеряны. Вы уверены, что хотите открыть новый файл?";
                const string caption = "Открыть файл";
                var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                    if (dialog.ShowDialog() == true)
                    {
                        try
                        {
                            V4Item = V4MainCollection.Load(dialog.FileName);
                            DataContext = null;
                            DataContext = V4Item;
                            V4Item.is_changed = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
                    if (dialog.ShowDialog() == true)
                    {
                        try
                        {
                            V4Item.Save(dialog.FileName);
                            V4Item.is_changed = false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            else
            {
                Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        V4Item = V4MainCollection.Load(dialog.FileName);
                        DataContext = null;
                        DataContext = V4Item;
                        V4Item.is_changed = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    V4Item.Save(dialog.FileName);
                    V4Item.is_changed = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Add_Defaults_Click(object sender, RoutedEventArgs e)
        {
            V4Item.AddDefaults();
        }

        private void Add_Default_V4DataCollection_Click(object sender, RoutedEventArgs e)
        {
            V4Item.AddDefault_DataCollection();
        }

        private void Add_Default_V4DataOnGrid_Click(object sender, RoutedEventArgs e)
        {
            V4Item.AddDefault_DataOnGrid();
        }

        private void Add_Element_from_File_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    V4DataOnGrid DataOnGrid_Item = new V4DataOnGrid("Item from the file", 2, dialog.FileName);
                    V4Item.Add(DataOnGrid_Item);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            V4Item.Remove_wpf((V4Data)listBox_Main.SelectedItem);
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            if (V4Item.is_changed == true)
            {
                const string message = "Несохранённые данные могут быть потеряны. Вы уверены, что хотите закрыть приложение?";
                const string caption = "Завершение работы приложения";
                var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
                    if (dialog.ShowDialog() == true)
                    {
                        V4Item.Save(dialog.FileName);
                        V4Item.is_changed = false;
                    }
                }
            }
        }

        private void FilterByDataCollection(object source, FilterEventArgs args)
        {
            if (args.Item != null)
            {
                if (args.Item.GetType().Name == "V4DataCollection")
                    args.Accepted = true;
                else
                    args.Accepted = false;
            }
        }

        private void FilterByDataOnGrid(object source, FilterEventArgs args)
        {
            if (args.Item != null)
            {
                if (args.Item.GetType().Name == "V4DataOnGrid")
                    args.Accepted = true;
                else
                    args.Accepted = false;
            }
        }

        private void OpenCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (V4Item.is_changed == true)
            {
                const string message = "Несохранённые данные могут быть потеряны. Вы уверены, что хотите открыть новый файл?";
                const string caption = "Открыть файл";
                var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                    if (dialog.ShowDialog() == true)
                    {
                        try
                        {
                            V4Item = V4MainCollection.Load(dialog.FileName);
                            DataContext = null;
                            DataContext = V4Item;
                            V4Item.is_changed = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
                    if (dialog.ShowDialog() == true)
                    {
                        try
                        {
                            V4Item.Save(dialog.FileName);
                            V4Item.is_changed = false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            else
            {
                Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        V4Item = V4MainCollection.Load(dialog.FileName);
                        DataContext = null;
                        DataContext = V4Item;
                        V4Item.is_changed = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void SaveCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    V4Item.Save(dialog.FileName);
                    V4Item.is_changed = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CanSaveCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            if (V4Item != null)
                if (V4Item.is_changed == true)
                    e.CanExecute = true;
                else
                    e.CanExecute = false;
        }

        private void RemoveCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            V4Item.Remove_wpf((V4Data)listBox_Main.SelectedItem);
        }

        private void CanRemoveCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            if (listBox_Main.SelectedItem != null)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void AddCustomCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            V4CustomItem.AddCustom();
        }

        private void CanAddCustomCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            foreach(FrameworkElement child in stackPanel_AddCustom.Children)
                if (Validation.GetHasError(child) == true)
                {
                    e.CanExecute = false;
                    return;
                }
            e.CanExecute = true;
        }

    }
}
