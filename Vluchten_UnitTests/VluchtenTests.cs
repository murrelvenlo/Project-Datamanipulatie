using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
            //Arrange + Act
            Passagier passagier = new Passagier()
            { voornaam = "Iemand", achternaam = "ZijnAchternaam", emailadres = "ieman@hotmail.com", telefoonnummer = "088880000", nationaliteit = "Landse", plaats = "EenPlaats", id = 50 };


            //Assert
            Assert.IsTrue(passagier.IsGeldig());
        }


        [TestMethod]
        public void HetAantalVanEenVluchtOpVertrekOfBestemming()
        {

            //Arrange
            List<Vlucht> vluchten;



            //Act
            vluchten = DatabaseOperations.GewensteVluchtenZoeken("Amsterdam", "Paramaribo");

            //Asert
            Assert.AreNotEqual(vluchten.Count, 0);
        }

    }
}
