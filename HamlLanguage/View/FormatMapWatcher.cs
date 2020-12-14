using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;

namespace HamlLanguage.View
{
    internal class FormatMapWatcher
    {
        private IClassificationFormatMap _classificationFormatMap;
        private IWpfTextView _textView;
        private IClassificationTypeRegistryService _typeRegistry;

        public FormatMapWatcher(IWpfTextView textView, IClassificationFormatMap classificationFormatMap, IClassificationTypeRegistryService typeRegistry)
        {
            _textView = textView;
            _classificationFormatMap = classificationFormatMap;
            _typeRegistry = typeRegistry;
        }
    }
}