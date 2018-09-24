// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using NuGet.Shared;

namespace NuGet.Packaging.Core
{
    public class LicenseMetadata : IEquatable<LicenseMetadata>
    {
        public LicenseMetadata(string licenseExpression, string src)
        {
            if (licenseExpression == null && src == null)
            {
                throw new ArgumentNullException("Cannot be null");
            }

            if (licenseExpression != null && src != null)
            {
                throw new ArgumentException("Both cannot be set");
            }

            LicenseExpression = licenseExpression;
            Src = src;
        }

        public string LicenseExpression { get; private set; }
        public string Src { get; private set; }


        public bool Equals(LicenseMetadata other)
        {
            return Equals(other);
        }

        public override bool Equals(object obj)
        {
            var metadata = obj as LicenseMetadata;
            return metadata != null &&
                   LicenseExpression == metadata.LicenseExpression &&
                   Src == metadata.Src;
        }

        public override int GetHashCode()
        {
            var combiner = new HashCodeCombiner();

            combiner.AddObject(LicenseExpression);
            combiner.AddObject(Src);

            return combiner.CombinedHash;
        }
    }
}
