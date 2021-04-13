using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WpfApp2
{
    public static class CustomCommands
    {
        public static RoutedCommand AddCustomCommand =
            new RoutedCommand("AddCustomCommand", typeof(WpfApp2.CustomCommands));
    }
}
