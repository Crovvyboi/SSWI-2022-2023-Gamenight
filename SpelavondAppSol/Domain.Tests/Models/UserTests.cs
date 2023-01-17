using Domain.Models;
using Domain.Services;
using Moq;
using System;
using System.Collections.Generic;

namespace Domain.Tests.Models
{
    public class UserTests
    {
        [Fact]
        public void User_Object_Creation()
        {
            // Arrange
            const int ExId = 5;
            const string ExFirstName = "FirstName";
            const string ExLastName = "last Name";
            DateTime ExBirthday = DateTime.Now;
            const string ExEmail = "e@mail.com";
            const string ExCity = "City";
            const string ExStreet = "Street";
            const int ExHouseNumber = 2;

            // Act
            User user = new User()
            {
                Id = ExId,

                FirstName = ExFirstName,
                LastName = ExLastName,

                Birthday = ExBirthday,
                Email = ExEmail,
                Gender = Gender.Other,

                Name = "",
                City = ExCity,
                Street = ExStreet,
                HouseNumber = ExHouseNumber,
                HouseNumberAdditions = "",

                playerat = new List<GameNight>(),
                playergames = new List<PlayerGamenight>()

            };

            // Assert
            Assert.Equal(ExId, user.Id);

            Assert.Equal(user.FirstName, ExFirstName);
            Assert.Equal(user.LastName, ExLastName);
            Assert.Equal(user.Birthday, ExBirthday);
            Assert.Equal(user.Email, ExEmail);
            Assert.Equal(user.City, ExCity);
            Assert.Equal(user.Street, ExStreet);
            Assert.Equal(user.HouseNumber, ExHouseNumber);

            Assert.Empty(user.playerat);
            Assert.Empty(user.playergames);
            
            Assert.Empty(user.HouseNumberAdditions);
            Assert.Empty(user.Name);
        }

        [Fact]
        public void User_Is_Younger_Than_18()
        {
            // Arrange
            User user = new User()
            {
                Birthday = DateTime.Today

            };

            // Act
            bool result = user.isEighteen();

            // Assert
            Assert.False(result);

        }

        [Fact]
        public void User_Is_Older_Than_18()
        {
            // Arrange
            User user = new User()
            {
                Birthday = DateTime.Today.AddYears(-20)

            };

            // Act
            bool result = user.isEighteen();

            // Assert
            Assert.True(result);

        }
    }
}