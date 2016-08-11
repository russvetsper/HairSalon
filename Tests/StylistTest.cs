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
  public void Test2_Save()
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

  public void Test3_SameName()
    {
      //Arrange, Act
      Stylist firstStylist = new Stylist("Russ");
      Stylist secondStylist = new Stylist("Russ");

      //Assert
      Assert.Equal(firstStylist, secondStylist);
    }

    [Fact]
  public void Test4_FindStylist()
  {
    //Arrange
    Stylist testStylist = new Stylist("Russ");
    testStylist.Save();

    //Act
    Stylist foundStylist = Stylist.Find(testStylist.GetId());

    //Assert
    Assert.Equal(testStylist, foundStylist);
  }

  [Fact]
    public void Test5_UpdateStylist()
    {
      //Arrange
      string name = "Russ";
      Stylist testStylist = new Stylist(name);
      testStylist.Save();
      string newName = "Mike";

      //Act
      testStylist.Update(newName);

      string result = testStylist.GetName();

      //Assert
      Assert.Equal(newName, result);
    }

    [Fact]
    public void Test6_DeleteStylist()
    {
      Stylist firstStylist = new Stylist("Russ");
      firstStylist.Save();

      Stylist secondStylist = new Stylist("Rouz");
      secondStylist.Save();

      firstStylist.Delete();
      List<Stylist> allStylists = Stylist.GetAll();
      List<Stylist> afterDeleteFristStylist = new List<Stylist> {secondStylist};

      Assert.Equal(afterDeleteFristStylist, allStylists);
    }

    [Fact]
    public void Test7_GetClientsWithStylist()
    {
      Stylist testStylist = new Stylist("Russ");
      testStylist.Save();

      Client firstClient = new Client("Joe", testStylist.GetId());
      firstClient.Save();
      Client secondClient = new Client("Rouz", testStylist.GetId());
      secondClient.Save();


      List<Client> testClientList = new List<Client> {firstClient, secondClient};
      List<Client> resultClientList = testStylist.GetClients();

      Assert.Equal(testClientList, resultClientList);
    }







  public void Dispose()
    {
      Stylist.DeleteAll();

    }
  }
}
