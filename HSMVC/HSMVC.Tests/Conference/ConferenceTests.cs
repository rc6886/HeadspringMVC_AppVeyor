using System;
using System.Linq;
using System.Transactions;
using HSMVC.DataAccess;
using HSMVC.Infrastructure;
using NUnit.Framework;
using Shouldly;

namespace HSMVC.Tests.Conference
{
    [TestFixture]
    public class ConferenceTests
    {
        private IConferenceRepository _repository;
        private TransactionScope _transactionScope;

        [SetUp]
        public void SetupTestFixture()
        {
            _repository = new ConferenceRepository(NHibernateHelper.OpenSession());
            _transactionScope = new TransactionScope();
        }

        [TearDown]
        public void TestDownTestFixture()
        {
            _transactionScope.Dispose();
        }

        [Test]
        public void ShouldGetAllConferences()
        {
            var conferences = _repository.GetAll().ToArray();

            conferences.Length.ShouldBe(3);

            conferences[0].Id.ShouldBe(Guid.Parse("0E3E638E-DA0B-47DF-B2FC-07F6CC7618DE"));
            conferences[1].Id.ShouldBe(Guid.Parse("C2AD9DAC-B936-442E-B46D-A73C0B69C147"));
            conferences[2].Id.ShouldBe(Guid.Parse("F41C8CFC-A6E3-4309-8023-D1425D294468"));
        }

        [Test]
        public void ShouldEditConference()
        {
            var conference = _repository.Load(Guid.Parse("0E3E638E-DA0B-47DF-B2FC-07F6CC7618DE"));

            conference.ChangeName("New Conference");
            conference.ChangeCost(241);
            conference.ChangeHashTag("#new");
            conference.ChangeDates(DateTime.Parse("01/01/2000"), DateTime.Parse("01/01/2005"));
            _repository.Save(conference);

            var editedConference = _repository.Load(Guid.Parse("0E3E638E-DA0B-47DF-B2FC-07F6CC7618DE"));

            editedConference.Name.ShouldBe("New Conference");
            editedConference.Cost.ShouldBe(241);
            editedConference.HashTag.ShouldBe("#new");
            editedConference.StartDate.ShouldBe(DateTime.Parse("01/01/2000"));
            editedConference.EndDate.ShouldBe(DateTime.Parse("01/01/2005"));
        }

        [Test]
        public void ShouldAddConference()
        {
            var conference = new Domain.Conference("Test Conference", "#Test", DateTime.Parse("03/03/2013"),
                DateTime.Parse("04/04/2014"), 127, 499, 2);

            _repository.Save(conference);

            var addedConference = _repository.FindByName("Test Conference");

            addedConference.Name.ShouldBe("Test Conference");
            addedConference.HashTag.ShouldBe("#Test");
            addedConference.StartDate.ShouldBe(DateTime.Parse("03/03/2013"));
            addedConference.EndDate.ShouldBe(DateTime.Parse("04/04/2014"));
            addedConference.Cost.ShouldBe(127);
            addedConference.AttendeeCount.ShouldBe(499);
            addedConference.SessionCount.ShouldBe(2);
        }
    }
}