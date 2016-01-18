﻿using System;
using System.Collections.Generic;
using System.Linq;
using HSMVC.Domain;
using NHibernate;
using NHibernate.Linq;

namespace HSMVC.DataAccess
{
    public interface IConferenceRepository
    {
        IEnumerable<Conference> GetAll();
        IQueryable<Conference> Query();
        Conference Load(Guid id);
        Conference FindByName(string name);
        void Save(Conference conference);
    }

    public class ConferenceRepository : IConferenceRepository
    {
        private readonly ISession _session;

        public ConferenceRepository(ISession session)
        {
            _session = session;
        }

        public IEnumerable<Conference> GetAll()
        {
            return _session.Query<Conference>().ToList();
        }

        public IQueryable<Conference> Query()
        {
            return _session.Query<Conference>();
        }

        public Conference Load(Guid id)
        {
            return _session.Query<Conference>().Single(x => x.Id == id);
        }

        public Conference FindByName(string name)
        {
            return _session.Query<Conference>().SingleOrDefault(x => x.Name == name);
        }

        public void Save(Conference conference)
        {
            using (_session)
            {
                using (var transaction = _session.BeginTransaction())
                {
                    _session.SaveOrUpdate(conference);
                    transaction.Commit();
                }
            }
        }
    }
}