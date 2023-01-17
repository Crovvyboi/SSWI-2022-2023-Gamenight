using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.Models
{
    public class FoodstuffsTests
    {
        [Fact]
        public void Foodstuffs_Object_Creation()
        {
            // Arrange
            const int Exid = 3;
            const string Exname = "name";
            const string Exdescription = "description";
            const int Exuserid = 6;

            // Act
            Foodstuffs foodstuffs = new Foodstuffs()
            {
                id = Exid,
                Name = Exname,
                Description = Exdescription,
                userid = Exuserid
            };

            // Assert
            Assert.Equal(foodstuffs.id, Exid);
            Assert.Equal(foodstuffs.Name, Exname);
            Assert.Equal(foodstuffs.Description, Exdescription);
            Assert.Equal(foodstuffs.userid, Exuserid);

        }
    }
}
