using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamlLanguage.Classification
{
    internal static class OrdinaryClassificationDefinition
    {
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("ut")]
        internal static ClassificationTypeDefinition hamlKeywords = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("variables")]
        internal static ClassificationTypeDefinition hamlVariables = null;
    }
}
