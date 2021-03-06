﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore
{
#if !Test21
    [SpatialiteRequired]
    public class SpatialSqliteTest : SpatialTestBase<SpatialSqliteFixture>
    {
        public SpatialSqliteTest(SpatialSqliteFixture fixture)
            : base(fixture)
        {
        }
    }
#endif
}
