﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using GeoAPI;
using GeoAPI.Geometries;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;
using NetTopologySuite.IO;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class SqlServerNTSTypeMappingSourcePlugin : IRelationalTypeMappingSourcePlugin
    {
        private readonly HashSet<string> _spatialStoreTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "geometry",
            "geography"
        };

        private readonly SqlServerSpatialReader _reader;

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public SqlServerNTSTypeMappingSourcePlugin([NotNull] IGeometryServices geometryServices)
        {
            Check.NotNull(geometryServices, nameof(geometryServices));

            _reader = new SqlServerSpatialReader(geometryServices);
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual RelationalTypeMapping FindMapping(in RelationalTypeMappingInfo mappingInfo)
        {
            var clrType = mappingInfo.ClrType;
            var storeTypeName = mappingInfo.StoreTypeName;

            return (clrType != null && typeof(IGeometry).IsAssignableFrom(clrType)
                    || storeTypeName != null && _spatialStoreTypes.Contains(storeTypeName))
                ? new SqlServerGeometryTypeMapping(clrType ?? typeof(IGeometry), _reader, storeTypeName ?? "geometry")
                : null;
        }
    }
}
