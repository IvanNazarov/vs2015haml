using Microsoft.VisualStudio.Text.Tagging;

namespace HamlLanguage
{
    public class HamlTokenTag : ITag
    {
        public HamlTokenTypes Type { get; set; }

        public HamlTokenTag(HamlTokenTypes type)
        {
            Type = type;
        }
    }
}
