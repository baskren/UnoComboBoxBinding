using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace ComboBoxPlus;

internal static class BindingExtensions
{
    public static void Bind(this DependencyObject target, DependencyProperty targetProperty, object source, string path = null, BindingMode mode = BindingMode.OneWay)
    {
        var binding = new Binding
        {
            Source = source,
            Mode = mode
        };
        if (!string.IsNullOrWhiteSpace(path))
            binding.Path = new PropertyPath(path);
        BindingOperations.SetBinding(target, targetProperty, binding);
    }


}
