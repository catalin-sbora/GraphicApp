using GraphicApp.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirclePlugin
{
    public class CircleElement : IGraphicElement
    {

        public int Ox { get; set; }
        public int Oy { get; set; }
        public int Radius { get; set; }
        public int Identifier 
        {
            get;
            set;
        }

        public void Draw()
        {
            Console.WriteLine($"Id: {Identifier} Circle: ({Ox},{Oy}) - Radius: {Radius}; ");
        }
    }
}
