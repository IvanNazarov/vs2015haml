using Microsoft.VisualStudio.Text.Tagging;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using System.Text.RegularExpressions;

namespace HamlLanguage
{
    internal sealed class HamlTokenTagger : ITagger<HamlTokenTag>
    {
        private ITextBuffer _buffer;

        internal HamlTokenTagger(ITextBuffer buffer)
        {
            _buffer = buffer;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged { add { } remove { } }

        public IEnumerable<ITagSpan<HamlTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            if (spans.Count == 0)
                yield break;

            foreach (var span in spans)
            {
                ITextSnapshotLine line = span.Start.GetContainingLine();
                int location = line.Start.Position;
                var originalText = line.GetText();
                var text = originalText.ToLower();

                #region UserTypes
                var matches = Regex.Matches(originalText, @"(as\s[A-Z]\w+|-[A-Z]\w+)");
                foreach (Match match in matches)
                {
                    var tokenSpan = new SnapshotSpan(span.Snapshot, new Span(match.Index + location, match.Length));
                    if (tokenSpan.IntersectsWith(span))
                        yield return new TagSpan<HamlTokenTag>(tokenSpan, new HamlTokenTag(HamlTokenTypes.UserTypes));
                }

                #endregion

                #region Keywords
                matches = Regex.Matches(text, @"(\bvar\b|\bint\b|\bstring\b|\bbool\b|\bdouble\b|\bfloat\b|\bif\b|\bforeach\b|\bfor\b|\bwhile\b|\belse\b|\bas\b)");
                foreach (Match match in matches)
                {
                    var tokenSpan = new SnapshotSpan(span.Snapshot, new Span(match.Index + location, match.Length));
                    if (tokenSpan.IntersectsWith(span))
                        yield return new TagSpan<HamlTokenTag>(tokenSpan, new HamlTokenTag(HamlTokenTypes.Keywords));
                }
                #endregion

                #region HtmlEntity
                matches = Regex.Matches(text, @"(%\w+|#\w+|(^|\s)\.\w+)");
                foreach (Match match in matches)
                {
                    var tokenSpan = new SnapshotSpan(span.Snapshot, new Span(match.Index + location, match.Length));
                    if (tokenSpan.IntersectsWith(span))
                        yield return new TagSpan<HamlTokenTag>(tokenSpan, new HamlTokenTag(HamlTokenTypes.HtmlEntity));
                }
                #endregion

                #region Linq
                matches = Regex.Matches(text, @"(\bfrom\b|\bin\b|\bwhere\b|\bselect\b|\border\b|\bgroup\b|\bby\b)");
                foreach (Match match in matches)
                {
                    var tokenSpan = new SnapshotSpan(span.Snapshot, new Span(match.Index + location, match.Length));
                    if (tokenSpan.IntersectsWith(span))
                        yield return new TagSpan<HamlTokenTag>(tokenSpan, new HamlTokenTag(HamlTokenTypes.Linq));
                }
                #endregion

                #region Variable Or HtmlAttribute
                matches = Regex.Matches(text, @"(?:[\s|{|(|\,])(\w+)\s*?=");
                foreach (Match match in matches)
                {
                    var tokenSpan = new SnapshotSpan(span.Snapshot, new Span(match.Index + location, match.Length));
                    if (tokenSpan.IntersectsWith(span))
                        yield return new TagSpan<HamlTokenTag>(tokenSpan, new HamlTokenTag(HamlTokenTypes.HtmlAttributeName));
                }
                #endregion

                #region Operator
                matches = Regex.Matches(text, @"(\?|\=|\&|\-|\+|\\|\>|\<|\(|\)|\{|\}|\!)");
                foreach (Match match in matches)
                {
                    var tokenSpan = new SnapshotSpan(span.Snapshot, new Span(match.Index + location, match.Length));
                    if (tokenSpan.IntersectsWith(span))
                        yield return new TagSpan<HamlTokenTag>(tokenSpan, new HamlTokenTag(HamlTokenTypes.Operator));
                }
                #endregion

                #region String
                matches = Regex.Matches(text, "\".*?\"");
                foreach (Match match in matches)
                {
                    var tokenSpan = new SnapshotSpan(span.Snapshot, new Span(match.Index + location, match.Length));
                    if (tokenSpan.IntersectsWith(span))
                        yield return new TagSpan<HamlTokenTag>(tokenSpan, new HamlTokenTag(HamlTokenTypes.String));
                }
                #endregion

                #region Number
                matches = Regex.Matches(text, @"\d+");
                foreach (Match match in matches)
                {
                    var tokenSpan = new SnapshotSpan(span.Snapshot, new Span(match.Index + location, match.Length));
                    if (tokenSpan.IntersectsWith(span))
                        yield return new TagSpan<HamlTokenTag>(tokenSpan, new HamlTokenTag(HamlTokenTypes.Number));
                }
                #endregion

                #region Comment
                matches = Regex.Matches(text, @"\/\*(?:.*?)\*\/");
                foreach (Match match in matches)
                {
                    var tokenSpan = new SnapshotSpan(span.Snapshot, new Span(match.Index + location, match.Length));
                    if (tokenSpan.IntersectsWith(span))
                        yield return new TagSpan<HamlTokenTag>(tokenSpan, new HamlTokenTag(HamlTokenTypes.Comment));
                }
                #endregion

            }
        }

    }
}
