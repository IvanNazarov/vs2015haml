using Microsoft.VisualStudio.Text.Tagging;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Language.StandardClassification;

namespace HamlLanguage.Classification
{
    internal sealed class HamlClassifier : ITagger<ClassificationTag>
    {
        private ITextBuffer _buffer;
        private ITagAggregator<HamlTokenTag> _aggregator;
        private IClassificationTypeRegistryService _service;
        private Dictionary<HamlTokenTypes, IClassificationType> _hamlTypes;

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged { add { } remove { } }

        internal  HamlClassifier(ITextBuffer buffer, 
            ITagAggregator<HamlTokenTag> hamlTagAggregator,
            IClassificationTypeRegistryService typeService)
        {
            _buffer = buffer;
            _aggregator = hamlTagAggregator;
            _service = typeService;
            _hamlTypes = new Dictionary<HamlTokenTypes, IClassificationType>();
            _hamlTypes[HamlTokenTypes.UserTypes] = typeService.GetClassificationType("interface name");
            _hamlTypes[HamlTokenTypes.Keywords] = typeService.GetClassificationType(PredefinedClassificationTypeNames.Keyword);
            _hamlTypes[HamlTokenTypes.HtmlEntity] = typeService.GetClassificationType("html entity");
            _hamlTypes[HamlTokenTypes.HtmlAttributeName] = typeService.GetClassificationType("html attribute name");
            _hamlTypes[HamlTokenTypes.Linq] = typeService.GetClassificationType(PredefinedClassificationTypeNames.Keyword);
            _hamlTypes[HamlTokenTypes.Operator] = typeService.GetClassificationType(PredefinedClassificationTypeNames.Operator);
            _hamlTypes[HamlTokenTypes.String] = typeService.GetClassificationType(PredefinedClassificationTypeNames.String);
            _hamlTypes[HamlTokenTypes.Number] = typeService.GetClassificationType(PredefinedClassificationTypeNames.Number);
            _hamlTypes[HamlTokenTypes.Comment] = typeService.GetClassificationType(PredefinedClassificationTypeNames.Comment);
        }

        public IEnumerable<ITagSpan<ClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (var tagSpan in _aggregator.GetTags(spans))
            {
                var tagSpans = tagSpan.Span.GetSpans(spans[0].Snapshot);
                yield return 
                    new TagSpan<ClassificationTag>(tagSpans[0], 
                                                   new ClassificationTag(_hamlTypes[tagSpan.Tag.Type]));
            }
        }
    }
}
