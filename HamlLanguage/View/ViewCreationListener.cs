using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamlLanguage.View
{
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("haml")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal sealed class ViewCreationListener : IWpfTextViewCreationListener
    {
        [Import]
        IClassificationFormatMapService formatMapService = null;

        [Import]
        IClassificationTypeRegistryService typeRegistry = null;

        public void TextViewCreated(IWpfTextView textView)
        {
            textView.Properties.GetOrCreateSingletonProperty(() => 
            new FormatMapWatcher(textView, formatMapService.GetClassificationFormatMap(textView), typeRegistry));
        }
    }
}
