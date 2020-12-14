using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Text.Classification;

namespace HamlLanguage.Classification
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("haml")]
    [TagType(typeof(ClassificationTag))]
    internal sealed class HamlClassifierProvider : ITaggerProvider
    {
        [Export]
        [Name("haml")]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition HamlContentType = null;

        [Export]
        [FileExtension(".haml")]
        [ContentType("haml")]
        internal static FileExtensionToContentTypeDefinition HamlFileType = null;

        [Import]
        internal IClassificationTypeRegistryService ClassificationTypeRegistry = null;

        [Import]
        internal IBufferTagAggregatorFactoryService aggregatorFactory = null;


        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            ITagAggregator<HamlTokenTag> hamlTagAggregator =
                aggregatorFactory.CreateTagAggregator<HamlTokenTag>(buffer);
            return new HamlClassifier(buffer, hamlTagAggregator, ClassificationTypeRegistry) as ITagger<T>;
        }
    }
}
