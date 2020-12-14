using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamlLanguage
{
    public enum HamlTokenTypes
    {
        Keywords, HtmlEntity, HtmlAttributeName, Css, Linq,
        Operator,
        String,
        Number,
        UserTypes,
        Comment
    }
}
