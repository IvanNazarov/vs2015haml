using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HamlLanguage.Classification
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "ut")]
    [Name("ut")]
    //this should be visible to the end user
    [UserVisible(false)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class HamlKeywordsFormatDefinition : ClassificationFormatDefinition
    {
        public HamlKeywordsFormatDefinition()
        {
            DisplayName = "User Types - Interfaces";
            ForegroundColor = Color.FromRgb(255, 127, 39);
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "variables")]
    [Name("variables")]
    //this should be visible to the end user
    [UserVisible(false)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class HamlVariablesFormatDefinition : ClassificationFormatDefinition
    {
        public HamlVariablesFormatDefinition()
        {
            DisplayName = "variables";
        }
    }

}
