using CommunityToolkit.Maui.Views;

namespace SpotPdv.Controls;

public partial class ContextMenu : Popup
{
    private readonly Action<string> _onItemSelected;

    public ContextMenu(List<string> options)
    {
        InitializeComponent();

        foreach (var option in options)
        {
            var button = new Label
            {
                Text = option,
                TextColor = Colors.Black,
                Padding = new Thickness(10),
                GestureRecognizers = { new TapGestureRecognizer { Command = new Command(() => SelectItem(option)) } }
            };
            OptionsContainer.Children.Add(button);
        }
    }

    private void SelectItem(string selectedItem)
    {
        Close(selectedItem);
    }
}