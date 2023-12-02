### Current behavior

In Uno.WinUI, setting a ComboBox's `SelectedItem` before setting its `ItemsSource` results in no item selected.  This is not the case when targeting Windows.



### Expected behavior

When a ComboBox's `SelectedItem` is set before setting its `ItemsSource`, the UI will show the `SelectedItem` after `ItemsSource` has been set.

### How to reproduce it (as minimally and precisely as possible)

1. Clone the following repo: https://github.com/baskren/UnoComboBoxBinding
2. Build and run Windows target
3. Click [SET SELECTED ITEM] and then click [SET ITEMS SOURCE]    
    <img width="743" alt="image" src="https://github.com/unoplatform/uno/assets/2528888/2fe31227-fdb2-4df9-86fb-2b2f60af2376">

4. Rebuild and run Wasm target
5. Click [SET SELECTED ITEM] and then click [SET ITEMS SOURCE]    
    ![image](https://github.com/unoplatform/uno/assets/2528888/cd8d0251-4394-474c-9005-d0b792eace72)
6. Note that this issue is not present if the order of button clicks is reversed.

### Workaround

Subclassing ComboBox seems to work:

```C#
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
        => SetBaseSelectedItem();
    

    public new object? SelectedItem
    {
        get => (object?)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    #endregion SelectedItem Property


    public ComboBoxP()
        => RegisterPropertyChangedCallback(ItemsSourceProperty, OnItemsSourceChanged);


    private void OnItemsSourceChanged(DependencyObject sender, DependencyProperty dp)
        => SetBaseSelectedItem();    


    void SetBaseSelectedItem()
    {
        if (ItemsSource is IEnumerable enumeragble)
        {
            foreach (var item in enumeragble)
            {
                if (item.Equals(SelectedItem))
                {
                    base.SelectedItem = SelectedItem;
                    return;
                }
            }
        }
    }

}
```

### Works on UWP/WinUI

Yes

### Environment

Uno.WinUI / Uno.WinUI.WebAssembly / Uno.WinUI.Skia

### NuGet package version(s)

```xml
    <PackageVersion Include="Microsoft.Extensions.Logging.Console" Version="7.0.0" />
    <PackageVersion Include="Microsoft.Windows.Compatibility" Version="7.0.5" />
    <PackageVersion Include="Microsoft.Windows.SDK.BuildTools" Version="10.0.22621.756" />
    <PackageVersion Include="Microsoft.WindowsAppSDK" Version="1.4.231008000" />
    <PackageVersion Include="P42.Uno.Markup" Version="4.9.45" />
    <PackageVersion Include="SkiaSharp.Skottie" Version="2.88.6" />
    <PackageVersion Include="SkiaSharp.Views.Uno.WinUI" Version="2.88.6" />
    <PackageVersion Include="Uno.Core.Extensions.Logging.Singleton" Version="4.0.1" />
    <PackageVersion Include="Uno.Extensions.Logging.OSLog" Version="1.7.0" />
    <PackageVersion Include="Uno.Extensions.Logging.WebAssembly.Console" Version="1.7.0" />
    <PackageVersion Include="Uno.Resizetizer" Version="1.2.1" />
    <PackageVersion Include="Uno.UI.Adapter.Microsoft.Extensions.Logging" Version="5.0.48" />
    <PackageVersion Include="Uno.UniversalImageLoader" Version="1.9.36" />
    <PackageVersion Include="Uno.Wasm.Bootstrap" Version="8.0.4" />
    <PackageVersion Include="Uno.Wasm.Bootstrap.DevServer" Version="8.0.4" />
    <PackageVersion Include="Uno.Wasm.Bootstrap.Server" Version="8.0.4" />
    <PackageVersion Include="Uno.WinUI" Version="5.0.48" />
    <PackageVersion Include="Uno.WinUI.Lottie" Version="5.0.48" />
    <PackageVersion Include="Uno.WinUI.DevServer" Version="5.0.48" />
    <PackageVersion Include="Uno.WinUI.Skia.Gtk" Version="5.0.48" />
    <PackageVersion Include="Uno.WinUI.Skia.Linux.FrameBuffer" Version="5.0.48" />
    <PackageVersion Include="Uno.WinUI.Skia.Wpf" Version="5.0.48" />
    <PackageVersion Include="Uno.WinUI.WebAssembly" Version="5.0.48" />
    <PackageVersion Include="Xamarin.Google.Android.Material" Version="1.10.0.1" />

    <PackageVersion Include="SkiaSharp" Version="2.88.6" />
```

### Affected platforms

WebAssembly, Android, iOS, Skia (WPF)

### IDE

Visual Studio 2022

### IDE version

Microsoft Visual Studio Community 2022 (ARM 64-bit) - Current Version 17.8.2

### Relevant plugins

n/a

### Anything else we need to know?

_No response_
