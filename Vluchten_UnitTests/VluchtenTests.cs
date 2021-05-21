using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Vluchten_DAL;
using Vluchten_Models;

namespace Vluchten_UnitTests
{
    [TestClass]
    public class VluchtenTests
    {
        [TestMethod]
        public void ValideerPassagierGegevens_BijInvullen_EnLegeStringRetourneren()
        {
            //Arrange
            string foutmeldingen = "";

            Passagier passagier = new Passagier()
            { voornaam = "Iemand", achternaam = "ZijnAchternaam", emailadres = "ieman@hotmail.com", telefoonnummer = "088880000", nationaliteit = "Landse", plaats = "EenPlaats", id = 50 };

            //Act
            if (passagier.IsGeldig())
            {
                foutmeldingen = passagier.Error;
            }

            //Assert
            Assert.AreEqual("", foutmeldingen);
        }

        [TestMethod]
        public void PassagierIdMoetGroterOfGelijkZijnAanNul()
        {
            //Arrage
            Passagier passagier = new Passagier();

            //Act
            passagier.id = 27;

            //Assert
            Assert.IsTrue(passagier.id == 27);
        }
        
    }
}
