using GraphicApp.Abstractions;
using System;

namespace CirclePlugin
{
    public class CirclePlugin : IGraphicPlugin
    {
        public CirclePlugin()
        {
            Reader = new CircleReader();
        }
        public int Identifier 
        {
            get
            {
                return 1;
            }
        }

        public string Name 
        {
            get 
            {
                return "Circle";
            }
        }

        public IElementReader Reader
        {
            get;
            private set;
            
        }
        public Type ElementType
        {
            get
            {
                return typeof(CircleElement);
            }
        }

        public IGraphicElement CreateElement()
        {
            return new CircleElement();
        }
    }
}
