
using Cliente_Api_Anime.Azure;
using Cliente_Api_Anime.Models;
using System;
using System.Linq;
using Xunit;


namespace XUnitApiAnime
{
    public class UnitTestUsuario
    {
        [Fact]
        public void TestObtenerPlantas()
        {
            //Arrange
                bool estaVacio = false;

            //Act
                var Resultado = UsuarioAzure.ObtenerUsuarios();

                estaVacio = !Resultado.Any();


            //Assert
            Assert.False(estaVacio);




        }
    }
}
