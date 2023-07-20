using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationManagement.Data.Configuration
{
    /// <summary>
    /// These values can be changed eventually in the future so can be replaced with Azure App config, for different language/culture support can be stored in db table translations.
    /// </summary>
    public static class Constants
    {
        public const string translatorNameErrorMessage = "name cannot be empty";
        public const string addTranslatorErrorMessage = "Failed to AddTranslator";
        public const string translatorUpdatedStatusMessage = "Status updated";
        public const string translatorJobUpdatedStatusMessage = "Status updated";
        public const string unsupportedFileFormat = "Unsupported file format";
    }
}
