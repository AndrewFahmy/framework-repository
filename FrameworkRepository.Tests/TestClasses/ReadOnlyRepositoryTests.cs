﻿#pragma warning disable IDE0073

using System.Linq;
using FrameworkRepository.Tests.Common;
using FrameworkRepository.Tests.Models;
using NUnit.Framework;

namespace FrameworkRepository.Tests
{
    [TestFixture]
    internal class ReadOnlyRepositoryTests
    {
        private InMemContext _context = null!;

        [SetUp]
        public void Setup()
        {
            _context = ContextFactory.CreateContext();
        }

        [Test]
        public void Should_ReturnUserObject_When_SearchingByID()
        {
            //arrange
            const int Id = 1;
            var repo = new ReadOnlyRepository<User, InMemContext>(_context);

            //act
            var user = repo.GetFirstOrDefault(p => p.Id == Id);

            //assert
            Assert.NotNull(user);
            Assert.AreEqual(user.Id, Id);
            Assert.AreEqual(user.FirstName, "Andrew");
        }

        [Test]
        public void Should_ReturnNull_When_SearchingByID()
        {
            //arrange
            const int Id = 20;
            var repo = new ReadOnlyRepository<User, InMemContext>(_context);

            //act
            var user = repo.GetFirstOrDefault(p => p.Id == Id);

            //assert
            Assert.Null(user);
        }

        [Test]
        public void Should_ReturnAllUsers_When_SelectingDataWithoutFilter()
        {
            //arrange            
            var repo = new ReadOnlyRepository<User, InMemContext>(_context);

            //act
            var data = repo.GetQueryable().ToList();

            //assert
            Assert.NotNull(data);
            Assert.AreEqual(data.Count, 5);
        }

        [Test]
        public void Should_ReturnTrue_When_SearchByValidID()
        {
            //arrange     
            const int Id = 3;
            var repo = new ReadOnlyRepository<User, InMemContext>(_context);

            //act
            var exists = repo.Exists(p => p.Id == Id);

            //assert
            Assert.AreEqual(exists, true);
        }


        [Test]
        public void Should_ReturnFalse_When_SearchByInvalidID()
        {
            //arrange     
            const int Id = 10;
            var repo = new ReadOnlyRepository<User, InMemContext>(_context);

            //act
            var exists = repo.Exists(p => p.Id == Id);

            //assert
            Assert.AreEqual(exists, false);
        }

        [Test]
        public void Should_ReturnValidCount_When_SearchingUsers()
        {
            //arrange           
            var repo = new ReadOnlyRepository<User, InMemContext>(_context);

            //act
            var count = repo.Count();

            //assert
            Assert.AreEqual(count, 5);
        }

        [Test]
        public void Should_ReturnValidCount_When_SearchingProducts()
        {
            //arrange   
            var repo = new ReadOnlyRepository<Product, InMemContext>(_context);

            //act
            var count = repo.Count();

            //assert
            Assert.AreEqual(count, 8);
        }

        [Test]
        public void Should_ReturnPrice_When_SearchingForMaxProductPrices()
        {
            //arrange   
            var repo = new ReadOnlyRepository<Product, InMemContext>(_context);

            //act
            var price = repo.Max(p => p.Price);

            //assert
            Assert.AreEqual(price, 1500);
        }
    }
}
