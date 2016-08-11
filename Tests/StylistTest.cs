using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
  public void Test1_EmptyDatabase()
  {
    //Arrange, Act
    int result = Stylist.GetAll().Count;

    //Assert
    Assert.Equal(0, result);
  }

  [Fact]
  public void Test3_Save()
  {
    //Arrange
    Stylist testStylist = new Stylist("Russ");

    //Act
    testStylist.Save();
    Stylist savedStylist = Stylist.GetAll()[0];

    int result = savedStylist.GetId();
    int testId = testStylist.GetId();

    //Assert
    Assert.Equal(testId, result);
  }



  public void Dispose()
    {
      Stylist.DeleteAll();

    }
  }
}
