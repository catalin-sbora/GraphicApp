using GraphicApp.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicApp
{
    public class GraphicGroupElement : IGraphicElement
    {
        private int lastIdentifier = -1; 
        public readonly Dictionary<int, IGraphicElement> elements = new Dictionary<int, IGraphicElement>();

        public int Identifier { get; set; }

        public IGraphicElement GetGraphicElement(int identifier)
        {
            return elements[identifier];
        }
        public void AddElement(IGraphicElement element)
        {
            element.Identifier = ++lastIdentifier;          
            elements[element.Identifier] = element;
        }

        public void RemoveElement(int identifier)
        {
            if (!elements.Remove(identifier))
            {
                throw new IndexOutOfRangeException("The specified element is not on this group");
            }
        }
        public void Draw()
        {
            foreach (var element in elements.Values)
            {
                element.Draw();
            }
        }
    }
}
