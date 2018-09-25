// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using NuGet.Shared;

namespace NuGet.Packaging.Core
{
    public class LicenseMetadata : IEquatable<LicenseMetadata>
    {
        public static Version EmptyVersion = new Version(0,0);
        public static Version CurrentVersion = new Version(0, 0);
        public string LicenseExpression { get; }
        public string File { get; }
        public Version Version { get; }

        public LicenseMetadata(string licenseExpression, string file, Version version)
        {
            if (licenseExpression == null && file == null)
            {
                throw new ArgumentNullException($"{nameof(licenseExpression)} and {nameof(file)} cannot be null");
            }

            if (licenseExpression != null && file != null)
            {
                throw new ArgumentException($"{nameof(licenseExpression)} and {nameof(file)} are exclusive. Both cannot be set.");
            }

            LicenseExpression = licenseExpression;
            File = file;
            Version = version ?? throw new ArgumentNullException(nameof(version));
        }


        public bool Equals(LicenseMetadata other)
        {
            return Equals(other);
        }

        public override bool Equals(object obj)
        {
            var metadata = obj as LicenseMetadata;
            return metadata != null &&
                   LicenseExpression == metadata.LicenseExpression &&
                   File == metadata.File &&
                   Version == metadata.Version;
        }

        public override int GetHashCode()
        {
            var combiner = new HashCodeCombiner();

            combiner.AddObject(LicenseExpression);
            combiner.AddObject(File);
            combiner.AddObject(Version);

            return combiner.CombinedHash;
        }
    }
}
