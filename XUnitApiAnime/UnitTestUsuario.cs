
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
        public void TestObtenerUsuarios()
        {
            //Arrange
                bool estaVacio = false;

            //Act
                var Resultado = UsuarioAzure.ObtenerUsuarios();

                estaVacio = !Resultado.Any();


            //Assert
            Assert.False(estaVacio);

        }

        public void TestObtenerUsuarioPorId()
        {
            int idProbar = 1;
            Usuario usuarioRetornado;

            usuarioRetornado = UsuarioAzure.ObtenerUsuarioPorId(idProbar);

            Assert.NotNull(usuarioRetornado);
        }

        public void TestObtenerUsuarioPorNombre()
        {
            string nombreUsu = "Ale";

            Usuario usuarioRetornado;

            usuarioRetornado = UsuarioAzure.ObtenerUsuarioPorNombre(nombreUsu);

            Assert.NotNull(usuarioRetornado);
        }
    }
}
