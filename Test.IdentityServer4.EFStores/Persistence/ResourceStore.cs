﻿using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.IdentityServer4.EFStores.Persistence
{
    public class ResourceStore : IResourceStore
    {
        private readonly ConfigurationStoreContext _context;
        private readonly ILogger _logger;

        public ResourceStore(ConfigurationStoreContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("ResourceStore");
        }

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            var apiResource = _context.ApiResources.First(t => t.Name == name);
            apiResource.MapDataFromEntity();
            return Task.FromResult(apiResource.ApiResource);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));


            var apiResources = new List<ApiResource>();
            var apiResourcesEntities = from i in _context.ApiResources
                                       where scopeNames.Contains(i.Name)
                                       select i;

            foreach (var apiResourceEntity in apiResourcesEntities)
            {
                apiResourceEntity.MapDataFromEntity();

                apiResources.Add(apiResourceEntity.ApiResource);
            }

            return Task.FromResult(apiResources.AsEnumerable());
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));

            var identityResources = new List<IdentityResource>();
            var identityResourcesEntities = from i in _context.IdentityResources
                                            where scopeNames.Contains(i.Name)
                                            select i;

            foreach (var identityResourceEntity in identityResourcesEntities)
            {
                identityResourceEntity.MapDataFromEntity();

                identityResources.Add(identityResourceEntity.IdentityResource);
            }

            return Task.FromResult(identityResources.AsEnumerable());
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            var apiResourcesEntities = _context.ApiResources.ToList();
            var identityResourcesEntities = _context.IdentityResources.ToList();

            var apiResources = new List<ApiResource>();
            var identityResources = new List<IdentityResource>();

            foreach (var apiResourceEntity in apiResourcesEntities)
            {
                apiResourceEntity.MapDataFromEntity();

                apiResources.Add(apiResourceEntity.ApiResource);
            }

            foreach (var identityResourceEntity in identityResourcesEntities)
            {
                identityResourceEntity.MapDataFromEntity();

                identityResources.Add(identityResourceEntity.IdentityResource);
            }

            var result = new Resources(identityResources, apiResources);
            return Task.FromResult(result);
        }
    }
    
    //: IResourceStore
    //{
    //    private readonly IDatabaseScope _database;
    //    private readonly IGenericRepository<ApiResource> _apiRepository;
    //    private readonly IGenericRepository<IdentityResource> _identityResource;

    //    public ResourceStore(IDatabaseScope database, IGenericRepository<ApiResource> apiRepository, 
    //        IGenericRepository<IdentityResource> identityResource)
    //    {
    //        _database = database;
    //        _apiRepository = apiRepository;
    //        _identityResource = identityResource;
    //    }

    //    public Task<ApiResource> FindApiResourceAsync(string name)
    //    {
    //        return _apiRepository
    //            .AsReadOnly()
    //            .SingleOrDefaultAsync(s => s.Name == name);
    //    }

    //    public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
    //    {
    //        if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));


    //        var apiResources = new List<ApiResource>();
    //        var apiResourcesEntities = _apiRepository
    //                                   .Where(apiResources.) scopeNames.Contains(i.ApiResourceName)
    //                                   select i;

    //        foreach (var apiResourceEntity in apiResourcesEntities)
    //        {
    //            apiResourceEntity.MapDataFromEntity();

    //            apiResources.Add(apiResourceEntity.ApiResource);
    //        }

    //        return Task.FromResult(apiResources.AsEnumerable());
    //    }

    //    public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
    //    {
    //        if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));

    //        var identityResources = new List<IdentityResource>();
    //        var identityResourcesEntities = from i in _context.IdentityResources
    //                                        where scopeNames.Contains(i.IdentityResourceName)
    //                                        select i;

    //        foreach (var identityResourceEntity in identityResourcesEntities)
    //        {
    //            identityResourceEntity.MapDataFromEntity();

    //            identityResources.Add(identityResourceEntity.IdentityResource);
    //        }

    //        return Task.FromResult(identityResources.AsEnumerable());
    //    }

    //    public Task<Resources> GetAllResourcesAsync()
    //    {
    //        var apiResourcesEntities = _context.ApiResources.ToList();
    //        var identityResourcesEntities = _context.IdentityResources.ToList();

    //        var apiResources = new List<ApiResource>();
    //        var identityResources = new List<IdentityResource>();

    //        foreach (var apiResourceEntity in apiResourcesEntities)
    //        {
    //            apiResourceEntity.MapDataFromEntity();

    //            apiResources.Add(apiResourceEntity.ApiResource);
    //        }

    //        foreach (var identityResourceEntity in identityResourcesEntities)
    //        {
    //            identityResourceEntity.MapDataFromEntity();

    //            identityResources.Add(identityResourceEntity.IdentityResource);
    //        }

    //        var result = new Resources(identityResources, apiResources);
    //        return Task.FromResult(result);
    //    }
    //}
}
