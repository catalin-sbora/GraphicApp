using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicApp.Abstractions
{
    public interface IGraphicPlugin
    {
        int Identifier { get; }
        string Name { get; }        
        IElementReader Reader { get; }
        IGraphicElement CreateElement();
        Type ElementType { get; }

    }
}
