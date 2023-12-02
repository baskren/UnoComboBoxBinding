using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace ComboBoxPlus;

[Bindable]
public partial class ComboBoxP : ComboBox
{

    #region SelectedItem Property
    public static readonly new DependencyProperty SelectedItemProperty = DependencyProperty.Register(
        nameof(SelectedItem),
        typeof(object),
        typeof(ComboBoxP),
        new PropertyMetadata(default(object), (d,e) => ((ComboBoxP)d).OnSelectedItemChanged(e))
    );

    private void OnSelectedItemChanged(DependencyPropertyChangedEventArgs e)
    {
        if (ItemsSource is IEnumerable enumeragble)
        {
            foreach (var item in enumeragble)
            {
                if (item.Equals(SelectedItem))
                {
                    base.SelectedItem = e.NewValue;
                    return;
                }
            }
        }
    }

    public new object? SelectedItem
    {
        get => (object?)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    #endregion SelectedItem Property


    object? CachedSelectedItem;

    public ComboBoxP()
    {
        RegisterPropertyChangedCallback(ItemsSourceProperty, OnItemsSourceChanged);
    }

    private void OnItemsSourceChanged(DependencyObject sender, DependencyProperty dp)
    {
        base.SelectedItem = SelectedItem;
    }
}
