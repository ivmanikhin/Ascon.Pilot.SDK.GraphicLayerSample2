using System.Windows;

namespace Ascon.Pilot.SDK.GraphicLayerSample
{
    public partial class GraphicLayerElementSettingsView
    {
        public GraphicLayerElementSettingsView()
        {
            InitializeComponent();
        }

        private void OnSaveButtonClicked(object sender, RoutedEventArgs e)
        {
            var vertical = VerticalAlignment.Top;
            var horizontal = HorizontalAlignment.Left;
            if (LeftBottomCornerButton.IsChecked == true)
            {
                vertical = VerticalAlignment.Bottom;
                horizontal = HorizontalAlignment.Left;
            }
            if (RightTopCornerButton.IsChecked == true)
            {
                vertical = VerticalAlignment.Top;
                horizontal = HorizontalAlignment.Right;
            }

            if (RightBottomCornerButton.IsChecked == true)
            {
                vertical = VerticalAlignment.Bottom;
                horizontal = HorizontalAlignment.Right;
            }
                
            var model = DataContext as GraphicLayerElementSettingsModel;
            var includeStamp = StampCheckBox.IsChecked != null && StampCheckBox.IsChecked.Value;

            model.SaveSettings(PathButtonEdit.Text, TxbXOffset.Text, TxbYOffset.Text, TxbScale.Text, TxbAngle.Text, TxbPage.Text, vertical, horizontal, includeStamp);
            Close();
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}