using System;
using System.Collections.Generic;
using System.Globalization;
using NuGet.Common;

namespace NuGet.Packaging.Rules
{
    class UnrecognizedLicenseIdentifierRule : IPackageRule
    {
        public string MessageFormat { get; }

        public UnrecognizedLicenseIdentifierRule(string messageFormat)
        {
            MessageFormat = messageFormat ?? throw new ArgumentNullException(nameof(messageFormat));
        }

        public IEnumerable<PackagingLogMessage> Validate(PackageArchiveReader builder)
        {

            var nuspecReader = builder.NuspecReader;
            if (nuspecReader.GetLicenseMedata().LicenseExpression == "TODO NK")
            {
                yield return PackagingLogMessage.CreateWarning(
                    string.Format(CultureInfo.CurrentCulture, MessageFormat, nuspecReader.GetVersion().ToFullString()),
                    NuGetLogCode.NU5124);
            }
        }
    }
}
