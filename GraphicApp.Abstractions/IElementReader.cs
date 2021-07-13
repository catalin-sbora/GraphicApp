using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicApp.Abstractions
{
    public interface IElementReader
    {
        IGraphicElement ReadElement();
        IGraphicElement UpdateElement(IGraphicElement graphicElement);
    }
}
