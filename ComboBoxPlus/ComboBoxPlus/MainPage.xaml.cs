
namespace ComboBoxPlus;

public sealed partial class MainPage : Page
{
    #region ViewModel Property
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel),
        typeof(object),
        typeof(MainPage),
        new PropertyMetadata(default)
    );
    public object ViewModel
    {
        get => (object)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }
    #endregion ViewModel Property

    #region SelectedItem Property
    public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
        nameof(SelectedItem),
        typeof(object),
        typeof(MainPage),
        new PropertyMetadata(default)
    );
    public object SelectedItem
    {
        get => (object)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    #endregion SelectedItem Property


    public MainPage()
    {

        this.InitializeComponent();

        textBlockProg.Bind(TextBlock.TextProperty, this, nameof(SelectedItem));
        comboBoxProg.Bind(ComboBox.ItemsSourceProperty, this, nameof(ViewModel));
        comboBoxProg.Bind(ComboBox.SelectedItemProperty, this, nameof(SelectedItem), Microsoft.UI.Xaml.Data.BindingMode.TwoWay);
    }

    private void button_Click1(object sender, RoutedEventArgs e)
    {
        SelectedItem = "A";
    }

    private void button_Click2(object sender, RoutedEventArgs e)
    {
        ViewModel = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };
    }

    private void button_Click3(object sender, RoutedEventArgs e)
    {
        ViewModel = null;
        SelectedItem = null;
    }

}
