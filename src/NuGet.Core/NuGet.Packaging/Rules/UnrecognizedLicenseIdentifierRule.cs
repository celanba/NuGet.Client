using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NuGet.Common;
using NuGet.Packaging.Licenses;

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
            var licenseMetadata = nuspecReader.GetLicenseMedata();
            if (licenseMetadata.Type == LicenseType.Expression)
            {
                return ValidateAllLicenseLeafs(licenseMetadata.LicenseExpression);
            }
            return Enumerable.Empty<PackagingLogMessage>();
        }

        private IEnumerable<PackagingLogMessage> ValidateAllLicenseLeafs(NuGetLicenseExpression expression)
        {
            switch (expression.Type)
            {
                case LicenseExpressionType.License:
                    var license = (NuGetLicense)expression;
                    if (!license.IsStandardLicense)
                    {
                        yield return PackagingLogMessage.CreateWarning(
                                        string.Format(CultureInfo.CurrentCulture, MessageFormat, license.Identifier),
                                        NuGetLogCode.NU5124);
                    }
                    break;

                case LicenseExpressionType.Operator:
                    var licenseOperator = (LicenseOperator)expression;
                    switch (licenseOperator.OperatorType)
                    {
                        case LicenseOperatorType.LogicalOperator:
                            var logicalOperator = (LogicalOperator)licenseOperator;
                            ValidateAllLicenseLeafs(logicalOperator.Left);
                            ValidateAllLicenseLeafs(logicalOperator.Right);
                            break;

                        case LicenseOperatorType.WithOperator:
                            var withOperator = (WithOperator)licenseOperator;
                            ValidateAllLicenseLeafs(withOperator.License);
                            break;

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
