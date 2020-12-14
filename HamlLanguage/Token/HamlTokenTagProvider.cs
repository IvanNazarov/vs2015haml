using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Text;
using System.ComponentModel.Composition;

namespace HamlLanguage
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("haml")]
    [TagType(typeof(HamlTokenTag))]
    internal sealed class HamlTokenTagProvider : ITaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            return new HamlTokenTagger(buffer) as ITagger<T>;
        }
    }
}
