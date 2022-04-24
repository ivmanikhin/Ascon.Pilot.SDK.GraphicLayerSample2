using System;
using System.Windows;

namespace Ascon.Pilot.SDK.GraphicLayerSample
{
    [Serializable]
    public class GraphicLayerElement : IGraphicLayerElement
    {
        public Guid ElementId { get; set; }
        public Guid ContentId { get; set; }
        public double OffsetY { get; set; }
        public double OffsetX { get; set; }
        public Point Scale { get; set; }
        public double Angle { get; set; }
        public int PositionId { get; set; }
        public int PageNumber { get; set; }
        public Point CornerPoint { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public string ContentType { get; set; }
        public bool IsFloating { get; set; }

        public GraphicLayerElement() { }

        public GraphicLayerElement(Guid elementId, Guid contentId, double offsetX, double offsetY, int positionId, Point scale, double angle, 
            VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment, string contentType, int pageNumber, bool isFloating)
        {
            if (pageNumber < 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), pageNumber, "pageNumber must be greater than or equal to 0");

            ElementId = elementId;
            ContentId = contentId;
            OffsetX = offsetX;
            OffsetY = offsetY;
            Scale = scale;
            Angle = angle;
            PositionId = positionId;
            VerticalAlignment = verticalAlignment;
            HorizontalAlignment = horizontalAlignment;
            ContentType = contentType;
            PageNumber = pageNumber;
            IsFloating = isFloating;
        }

        public string GetFileName()
        {
            return GraphicLayerElementConstants.GRAPHIC_LAYER_ELEMENT + ElementId;
        }

        public string GetContentFileName()
        {
            return GraphicLayerElementConstants.GRAPHIC_LAYER_ELEMENT_CONTENT + ContentId;
        }
    }
}