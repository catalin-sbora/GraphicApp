using GraphicApp.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirclePlugin
{
    public class CircleReader : IElementReader
    {
        private void ReadElementData(CircleElement element)
        {
            Console.Write("Please enter Ox: ");
            element.Ox = ReadHelper.ReadIntValue();
            Console.Write("Please enter Oy: ");
            element.Oy = ReadHelper.ReadIntValue();
            Console.Write("Please enter Radius: ");
            element.Radius = ReadHelper.ReadIntValue();
        }
        public IGraphicElement ReadElement()
        {
            var element = new CircleElement();
            ReadElementData(element);
            return element;
        }

        public IGraphicElement UpdateElement(IGraphicElement graphicElement)
        {            
            if (graphicElement != null && graphicElement.GetType().Equals(typeof(CircleElement)))
            {
                var circleElement = graphicElement as CircleElement;
                ReadElementData(circleElement);
            }
            return graphicElement;
        }
    }
}
