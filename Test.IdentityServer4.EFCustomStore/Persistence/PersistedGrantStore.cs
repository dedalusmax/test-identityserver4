using AutoMapper;
using AutoMapper.QueryableExtensions;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Data;
using Test.Data.Entities;

namespace Test.IdentityServer4.EFCustomStore.Persistence
{
    public class PersistedGrantStore : IPersistedGrantStore
    {
        private readonly IDatabaseScope _database;
        private readonly IGenericRepository<Grant> _entityRepository;

        public PersistedGrantStore(IDatabaseScope database, IGenericRepository<Grant> entityRepository)
        {
            _database = database;
            _entityRepository = entityRepository;
        }

        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            return await GetAllEntities(_entityRepository.AsReadOnly())
                .Where(s => s.SubjectId == subjectId)
                .ProjectTo<PersistedGrant>()
                .ToListAsync();
        }

        public async Task<PersistedGrant> GetAsync(string key)
        {
            var entity = await _entityRepository
                .AsReadOnly()
                .SingleOrDefaultAsync(r => r.Key == key);

            return Mapper.Map<Grant, PersistedGrant>(entity);
        }

        public async Task RemoveAllAsync(string subjectId, string clientId)
        {
            var grants = _entityRepository
                .AsReadOnly()
                .Where(s => s.SubjectId == subjectId && s.ClientId == clientId);

            foreach (var grant in grants)
            {
                _entityRepository.Delete(grant);
            }

            await _database.SaveAsync();
        }

        public async Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            var grants = _entityRepository
                .AsReadOnly()
                .Where(s => s.SubjectId == subjectId && s.ClientId == clientId && s.Type == type);

            foreach (var grant in grants)
            {
                _entityRepository.Delete(grant);
            }

            await _database.SaveAsync();
        }

        public async Task RemoveAsync(string key)
        {
            var grant = await _entityRepository
                .AsReadOnly()
                .SingleOrDefaultAsync(s => s.Key == key);

            _entityRepository.Delete(grant);

            await _database.SaveAsync();
        }

        public async Task StoreAsync(PersistedGrant grant)
        {
            var entity = await _entityRepository
                .AsReadOnly()
                .SingleOrDefaultAsync(s => s.Key == grant.Key);

            if (entity == null)
            {
                entity = new Grant();
                UpdateEntity(entity, grant);
                _entityRepository.Insert(entity);
            } else
            {
                UpdateEntity(entity, grant);
            }

            await _database.SaveAsync();
        }

        private void UpdateEntity(Grant entity, PersistedGrant model)
        {
            Mapper.Map<PersistedGrant, Grant>(model, entity);
        }

        public virtual IQueryable<Grant> GetAllEntities(IQueryable<Grant> query)
        {
            return query;
        }
    }
}
