using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;
//using System.Globalization;

namespace Ascon.Pilot.SDK.GraphicLayerSample
{
    public class GraphicLayerElementSettingsModel : INotifyPropertyChanged
    {
        private readonly DelegateCommand _selectImageCommand;
        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set            
            {
                _filePath = value;
                OnPropertyChanged("FilePath");
            }
        }

        private bool _includeStamp;
        public bool IncludeStamp
        {
            get { return _includeStamp; }
            set
            {
                _includeStamp = value;
                OnPropertyChanged("IncludeStamp");
            }
        }

        public string XOffsetStr { get; set; }
        public string YOffsetStr { get; set; }
        public string Scale { get; set; }
        public string Angle { get; set; }
        public string PageNumber { get; set; }

        public ICommand SelectImageCommand
        {
            get { return _selectImageCommand; }
        }

        public bool LeftBottomCornerButtonChecked { get; set; }
        public bool LeftTopCornerButtonChecked { get; set; }
        public bool RightTopCornerButtonChecked { get; set; }
        public bool RightBottomCornerButtonChecked { get; set; }

        public event EventHandler OnSaveSettings;

        public GraphicLayerElementSettingsModel(string filePath, string x, string y, string scale, string angle, 
            VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment, bool includeStamp, string pageNumber)
        {
            FilePath = filePath;
            XOffsetStr = x;
            YOffsetStr = y;
            Scale = scale;
            Angle = angle;
            IncludeStamp = includeStamp;
            PageNumber = pageNumber;

            if (verticalAlignment == VerticalAlignment.Top && horizontalAlignment == HorizontalAlignment.Left)
                LeftTopCornerButtonChecked = true;
            if (verticalAlignment == VerticalAlignment.Bottom && horizontalAlignment == HorizontalAlignment.Left)
                LeftBottomCornerButtonChecked = true;
            if (verticalAlignment == VerticalAlignment.Top && horizontalAlignment == HorizontalAlignment.Right)
                RightTopCornerButtonChecked = true;
            if (verticalAlignment == VerticalAlignment.Bottom && horizontalAlignment == HorizontalAlignment.Right)
                RightBottomCornerButtonChecked = true;

            _selectImageCommand = new DelegateCommand(ShowDialog);
        }

        private void ShowDialog()
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = ".png",
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg"
            };

            if (dialog.ShowDialog() == true)
                FilePath = dialog.FileName;
        }

        public void SaveSettings(string path, string xOffset, string yOffset, string scale, string angle, string pageNumber, VerticalAlignment vertical, HorizontalAlignment horizontal, bool includeStamp)
        {
            FilePath = path;

            XOffsetStr = xOffset;
            YOffsetStr = yOffset;
            Scale = scale; 
            Angle = angle;
            PageNumber = pageNumber;

            Properties.Settings.Default.Path = path;
            Properties.Settings.Default.X = XOffsetStr;
            Properties.Settings.Default.Y = YOffsetStr;
            Properties.Settings.Default.Scale = Scale;
            Properties.Settings.Default.Angle = Angle;
            Properties.Settings.Default.PageNumber = PageNumber;
            Properties.Settings.Default.VerticalAligment = vertical.ToString();
            Properties.Settings.Default.HorizontalAligment = horizontal.ToString();
            Properties.Settings.Default.IncludeStamp = includeStamp;
            Properties.Settings.Default.Save();

            var handler = OnSaveSettings;
            if (handler != null)
                handler.Invoke(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}