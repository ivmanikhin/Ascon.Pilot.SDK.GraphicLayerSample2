using System;
using System.Globalization;
using System.Windows;
using System.Xml.Linq;

namespace Ascon.Pilot.SDK.GraphicLayerSample
{
    public class GraphicLayerElementCreator
    {
        public const string TextBlockSample1 =
            "<TextBlock Foreground=\"Blue\" FontSize=\"20\" TextAlignment=\"Center\">" +
            "АО ССМО<LineBreak />«ГорСпецСму»<LineBreak /><LineBreak /><LineBreak />В ПРОИЗВОДСТВО РАБОТ</TextBlock>";

        public const string TextBlockSample2 =
            "<TextBlock Foreground=\"Red\" FontSize=\"20\">{0}</TextBlock>";


        public const string PathSample =
            "<Path Data=\"M 77.95,18.45 L 77.95,312.15 L 378.25,112.15 L 578.25,18.45 Z \"" +
            "  Stroke=\"#FF087000\" StrokeThickness=\"8\" StrokeStartLineCap=\"Round\"" +
            "  StrokeEndLineCap=\"Round\" StrokeDashCap=\"Round\" StrokeMiterLimit=\"8\"></Path>";

        /// <summary>
        /// Creates graphic layer element
        /// </summary>
        /// <param name="xOffsetFromSettings">X offset in mm</param>
        /// <param name="yOffsetFromSettings">Y offset in mm</param>
        /// <returns></returns>
        public static GraphicLayerElement Create(double xOffsetFromSettings, double yOffsetFromSettings, Point scale, double angle, int position, 
            VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment, string contentType, Guid elementId, int pageNumber, bool isFloating)
        {
            var dpi = 96;
            var xOffset = xOffsetFromSettings / 25.4 * dpi;
            var yOffset = yOffsetFromSettings / 25.4 * dpi;
            var contentId = Guid.NewGuid();
            var element = new GraphicLayerElement(elementId, contentId, xOffset, yOffset,
                position, scale, angle, verticalAlignment, horizontalAlignment, contentType, pageNumber - 1, isFloating);
            return element;
        }

        public static XElement CreateStamp1()
        {
            return XElement.Parse(string.Format(TextBlockSample1));
        }

        public static XElement CreateStamp2()
        {
            var currentTime = string.Format(CultureInfo.CurrentUICulture, "{0:dd MMM yyyy}", DateTime.Now);
            return XElement.Parse(string.Format(TextBlockSample2, currentTime));
        }

        public static XElement CreateStampWithDateTime()
        {
            var currentTime = DateTime.Now.ToString(CultureInfo.CurrentCulture);
            return XElement.Parse(string.Format(TextBlockSample2, currentTime));
        }
    }
}