﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NuGet.Services.Entities;
using NuGetGallery.Frameworks;

namespace NuGetGallery
{
    public class PackageListViewModel
    {
        public PackageListViewModel(
            IReadOnlyCollection<ListPackageItemViewModel> packageViewModels,
            DateTime? indexTimestampUtc,
            string searchTerm,
            int totalCount,
            int pageIndex,
            int pageSize,
            UrlHelper url,
            bool includePrerelease,
            bool isPreviewSearch,
            string frameworks,
            string tfms,
            string packageType,
            string sortBy)
        {
            PageIndex = pageIndex;
            IndexTimestampUtc = indexTimestampUtc;
            PageSize = pageSize;
            TotalCount = totalCount;
            SearchTerm = searchTerm;
            int pageCount = (TotalCount + PageSize - 1) / PageSize;

            var pager = new PreviousNextPagerViewModel<ListPackageItemViewModel>(
                packageViewModels,
                PageIndex,
                pageCount,
                page => url.PackageList(page, searchTerm, includePrerelease, frameworks, tfms, packageType, sortBy));
            Items = pager.Items;
            Pager = pager;
            IncludePrerelease = includePrerelease;
            IsPreviewSearch = isPreviewSearch;
            Frameworks = frameworks;
            Tfms = tfms;
            PackageType = packageType;
            SortBy = sortBy;
        }

        public int FirstResultIndex => 1 + (PageIndex * PageSize);
        public int LastResultIndex => FirstResultIndex + Items.Count() - 1;

        public IReadOnlyCollection<ListPackageItemViewModel> Items { get; }

        public IPreviousNextPager Pager { get; }

        public int TotalCount { get; }

        public string SearchTerm { get;  }

        public int PageIndex { get; }

        public int PageSize { get; }

        public DateTime? IndexTimestampUtc { get; }

        public bool IncludePrerelease { get; }

        public bool IsPreviewSearch { get; }

        public string Frameworks { get; set; }

        public string Tfms { get; set; }

        public string PackageType { get; set; }

        public string SortBy { get; set; }

        public bool IsAdvancedSearchFlightEnabled { get; set; }

        public bool IsFrameworkFilteringEnabled {  get; set; }

        public Dictionary<string, FrameworkFilterHelper.FrameworkFilterGroup> FrameworkFilters = FrameworkFilterHelper.FrameworkFilters;

        public string TargetFrameworkInformationLink = "https://learn.microsoft.com/en-us/dotnet/standard/frameworks";
    }
}