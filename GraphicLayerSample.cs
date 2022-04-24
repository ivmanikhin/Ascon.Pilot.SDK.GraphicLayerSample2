using System;
using System.ComponentModel.Composition;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Xaml;
using System.Windows.Media;
using System.Xml.Serialization;
using Ascon.Pilot.SDK.Menu;
using Ascon.Pilot.SDK.CreateObjectSample;
using Ascon.Pilot.Theme.ColorScheme;
using Color = System.Windows.Media.Color;
using Point = System.Windows.Point;
using System.Threading.Tasks;

namespace Ascon.Pilot.SDK.CreateObjectSample
{
    public class ObjectLoader : IObserver<IDataObject>
    {
        private readonly IObjectsRepository _repository;
        private IDisposable _subscription;
        private TaskCompletionSource<IDataObject> _tcs;
        private long _changesetId;

        public ObjectLoader(IObjectsRepository repository)
        {
            _repository = repository;
        }

        public Task<IDataObject> Load(Guid id, long changesetId = 0)
        {
            _changesetId = changesetId;
            _tcs = new TaskCompletionSource<IDataObject>();
            _subscription = _repository.SubscribeObjects(new[] { id }).Subscribe(this);
            return _tcs.Task;
        }

        public void OnNext(IDataObject value)
        {
            if (value.State != DataState.Loaded)
                return;

            if (value.LastChange() < _changesetId)
                return;

            _tcs.TrySetResult(value);
            _subscription.Dispose();
        }

        public void OnError(Exception error) { }
        public void OnCompleted() { }
    }
}


namespace Ascon.Pilot.SDK.GraphicLayerSample
{
    [Export(typeof(IMenu<MainViewContext>))]
    public class GraphicLayerSample : IMenu<MainViewContext>, IHandle<UnloadedEventArgs>, IObserver<INotification>
    {
        private const string ServiceGraphicLayerMenu = "ServiceGraphicLayerMenu";
        private readonly IObjectModifier _modifier;
        private readonly IObjectsRepository _repository;

        private IPerson _currentPerson;
        private GraphicLayerElementSettingsView _settingsView;
        private GraphicLayerElementSettingsModel _model;
        private VerticalAlignment _verticalAlignment;
        private HorizontalAlignment _horizontalAlignment;

        private string _filePath = string.Empty;
        private double _xOffset;
        private double _yOffset;
        private double _scaleXY;
        private double _angle;
        //чтобы можно было на разные страницы ставить
        private int _pageNumber; 
        private bool _includeStamp;

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [ImportingConstructor]
        public GraphicLayerSample(IEventAggregator eventAggregator, IObjectModifier modifier, IObjectsRepository repository, IPilotDialogService dialogService)
        {
            var convertFromString = ColorConverter.ConvertFromString(dialogService.AccentColor);
            if (convertFromString != null)
            {
                var color = (Color)convertFromString;
                ColorScheme.Initialize(color, dialogService.Theme);
            }
            eventAggregator.Subscribe(this);
            _modifier = modifier;
            _repository = repository;
            var signatureNotifier = repository.SubscribeNotification(NotificationKind.ObjectSignatureChanged);
            var fileNotifier = repository.SubscribeNotification(NotificationKind.ObjectFileChanged);
            signatureNotifier.Subscribe(this);
            fileNotifier.Subscribe(this);
            CheckSettings();
        }

        public void Build(IMenuBuilder builder, MainViewContext context)
        {
            var menuItem = builder.ItemNames.First();
            builder.GetItem(menuItem).AddItem(ServiceGraphicLayerMenu, 0).WithHeader(GraphicLayerSample2.Properties.Resources.txtMenuItem);
        }

        public void OnMenuItemClick(string itemName, MainViewContext context)
        {
            var x = _xOffset.ToString(CultureInfo.InvariantCulture);
            var y = _yOffset.ToString(CultureInfo.InvariantCulture);
            var scale = _scaleXY.ToString(CultureInfo.InvariantCulture);
            var angle = _angle.ToString(CultureInfo.InvariantCulture);
            //см выше(PageNumber)
            var pageNumber = _pageNumber.ToString(CultureInfo.InvariantCulture);
            var includeStamp = _includeStamp;

            _model = new GraphicLayerElementSettingsModel(_filePath, x, y, scale, angle, _verticalAlignment, _horizontalAlignment, includeStamp, pageNumber);
            _model.OnSaveSettings += ReloadSettings;

            if (itemName != ServiceGraphicLayerMenu)
                return;

            _settingsView = new GraphicLayerElementSettingsView { DataContext = _model };
            //не было(возможно ASCON сам убрал в следующей версии)
            _settingsView.Unloaded += SettingsViewOnUnloaded; 

            var activeWindowHandle = GetForegroundWindow();
            new WindowInteropHelper(_settingsView)
            {
                Owner = activeWindowHandle
            };
            _settingsView.ShowDialog();
            System.Windows.Threading.Dispatcher.Run();
        }

        private void ReloadSettings(object sender, EventArgs e)
        {
            CheckSettings();
        }

        private void CheckSettings()
        {
            var path = Properties.Settings.Default.Path;
            _filePath = path;
            _includeStamp = Properties.Settings.Default.IncludeStamp;
            double.TryParse(Properties.Settings.Default.X, out _xOffset);
            double.TryParse(Properties.Settings.Default.Y, out _yOffset);
            _scaleXY = 1;
            try
            {
                var tmp = Properties.Settings.Default.Scale.Split(',', '.');
                double whole;
                double.TryParse(tmp[0], out whole);
                double fraction = 0;
                if (tmp.Length > 1)
                    double.TryParse("0" + CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator + tmp[1], out fraction);
                _scaleXY = whole + fraction;
            }
            catch (Exception) { }

            double.TryParse(Properties.Settings.Default.Angle, out _angle);
            //см выше(PageNumber)
            int.TryParse(Properties.Settings.Default.PageNumber, out _pageNumber);
            Enum.TryParse(Properties.Settings.Default.VerticalAligment, out _verticalAlignment);
            Enum.TryParse(Properties.Settings.Default.HorizontalAligment, out _horizontalAlignment);
        }

        //не было процедуры(возможно ASCON сам убрал в следующей версии)
        private void SettingsViewOnUnloaded(object sender, RoutedEventArgs e) 
        {
            _settingsView.Unloaded -= SettingsViewOnUnloaded;
            _model.OnSaveSettings -= ReloadSettings;
        }

        private void SaveToDataBaseRastr(IDataObject dataObject)
        {
            if (string.IsNullOrEmpty(_filePath))
                return;
            var builder = _modifier.Edit(dataObject);
            using (var fileStream = File.Open(_filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                var positionId = _currentPerson.MainPosition.Position;
                var byteArray = new byte[fileStream.Length];
                fileStream.Read(byteArray, 0, (int)fileStream.Length);
                var imageStream = new MemoryStream(byteArray);
                var scale = new Point(_scaleXY, _scaleXY);
                //что то нужное
                var name = GraphicLayerElementConstants.GRAPHIC_LAYER_ELEMENT + ToGuid(_currentPerson.Id); //не было

                var element = GraphicLayerElementCreator.Create(_xOffset, _yOffset, scale, _angle, positionId, _verticalAlignment,
                    _horizontalAlignment, GraphicLayerElementConstants.BITMAP, ToGuid(_currentPerson.Id), _pageNumber /* см выше(PageNumber) был 0*/, true);
                var serializer = new XmlSerializer(typeof(GraphicLayerElement));
                using (var stream = new MemoryStream())
                {
                    serializer.Serialize(stream, element);
                    //что то нужное 
                    builder.AddFile(name/*вместо было facsimileFileName*/, stream, DateTime.Now, DateTime.Now, DateTime.Now);
                    builder.AddFile(GraphicLayerElementConstants.GRAPHIC_LAYER_ELEMENT_CONTENT + element.ContentId, imageStream, DateTime.Now, DateTime.Now, DateTime.Now);
                }
                _modifier.Apply();
            }
        }

        private void SaveToDataBaseXaml(IDataObject dataObject, string xamlObject, Guid elementId)
        {
            var builder = _modifier.Edit(dataObject);
            var textBlocksStream = new MemoryStream();
            using (var writer = new StreamWriter(textBlocksStream))
            {
                writer.Write(xamlObject);
                writer.Flush();

                var positionId = _currentPerson.MainPosition.Position;
                var name = GraphicLayerElementConstants.GRAPHIC_LAYER_ELEMENT + elementId + "_" + positionId;
                var scale = new Point(_scaleXY, _scaleXY);

                var element = GraphicLayerElementCreator.Create(_xOffset, _yOffset, scale, _angle, positionId, 
                    _verticalAlignment, _horizontalAlignment, GraphicLayerElementConstants.XAML, elementId, _pageNumber/*см выше(PageNumber) был 0*/, true);
                var serializer = new XmlSerializer(typeof(GraphicLayerElement));
                using (var stream = new MemoryStream())
                {
                    serializer.Serialize(stream, element);
                    builder.AddFile(name, stream, DateTime.Now, DateTime.Now, DateTime.Now);
                }
                builder.AddFile(GraphicLayerElementConstants.GRAPHIC_LAYER_ELEMENT_CONTENT + element.ContentId, textBlocksStream, DateTime.Now, DateTime.Now, DateTime.Now);
                _modifier.Apply();
            }
        }

        public async void OnNext(INotification value)
        {
            if (string.IsNullOrEmpty(_filePath))
                return;

            _currentPerson = _repository.GetCurrentPerson();
            if (value.ChangeKind == NotificationKind.ObjectSignatureChanged && _currentPerson.Id == value.UserId)
            {
                var loaderForFirstSign = new ObjectLoader(_repository);
                var obj = await loaderForFirstSign.Load(value.ObjectId);
                
                if (obj.Files.Any(f => f.Name.Contains("Signature")))
                    AddGraphicLayer(obj);
                
                return;
            }
            if (value.ChangeKind == NotificationKind.ObjectFileChanged && _currentPerson.Id == value.UserId)
            {
                var loader = new ObjectLoader(_repository);
                var obj = await loader.Load(value.ObjectId);
                
                if (!obj.Files.Any(f => f.Name.Contains("Signature")))
                    return;
                    
                AddGraphicLayer(obj);
                
            }
        }

        private void AddGraphicLayer(IDataObject dataObject)
        {
            //удаление старых подписей, если есть
            foreach (var file in dataObject.Files) //новое
            {
                if (file.Name.Equals(GraphicLayerElementConstants.GRAPHIC_LAYER_ELEMENT + ToGuid(_currentPerson.Id)))
                {
                    var builder = _modifier.Edit(dataObject);
                    //builder.RemoveFile(file.Id);
                }
            }
            //уже не нужно facsimileFileName передавать в SaveToDataBaseRastr, 
            //тк там создается name вместо этого
            //var facsimileFileName = GraphicLayerElementConstants.GRAPHIC_LAYER_ELEMENT + ToGuid(_currentPerson.Id);
            //старое условие проверявшее, есть ли уже подпись в файле 
            //if (!dataObject.Files.Any(f => f.Name.Equals(facsimileFileName)))
            var signaturesCount = dataObject.Files.Count(f => f.Name.Contains("Signature"));
            var xpsFile = dataObject.ActualFileSnapshot.Files.FirstOrDefault(f =>
            {
                var extension = Path.GetExtension(f.Name);
                return extension != null && (extension.Equals(".xps") || extension.Equals(".dwfx"));
            });
            var requestsCount = xpsFile?.Signatures.Count;
            //пока нигде не используется(на будущее)
            //var currentPersonSignuterCount = xpsFile.Signatures.Count(f => _currentPerson.AllOrgUnits().Contains(f.PositionId) && (f.Sign != null)); //не было
            if (requestsCount == signaturesCount && _includeStamp)
            {
                var stamp1 = GraphicLayerElementCreator.CreateStamp1().ToString();
                SaveToDataBaseXaml(dataObject, stamp1, Guid.NewGuid());
                var stamp2 = GraphicLayerElementCreator.CreateStamp2().ToString();
                SaveToDataBaseXaml(dataObject, stamp2, Guid.NewGuid());
            }
            //теперь вместо передачи facsimileFileName 
            //в SaveToDataBaseRastr создается name
            SaveToDataBaseRastr(dataObject/*было dataObject, facsimileFileName*/);
        }

        public static Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        //изменение процедуры(возможно ASCON сам в следующей версии)
        public void Handle(UnloadedEventArgs message) { } 
        //было
        /*public void Handle(UnloadedEventArgs message)
        {
            _model.OnSaveSettings -= ReloadSettings;
        }*/
        public void OnError(Exception error) { }
        public void OnCompleted() { }
    }
}
