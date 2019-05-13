using FreeWheel.MovieDb.Api.Contexts;
using FreeWheel.MovieDb.Api.Controllers;
using FreeWheel.MovieDb.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using Xunit;

namespace FreeWheel.MovieDb.Api.UnitTest
{
    public class MoviesControllerTest
    {
        private readonly MoviesController _controller;
       
        private Mock<IConfiguration> _configMock = new Mock<IConfiguration>();

        public MoviesControllerTest()
        {
            _controller = new MoviesController(new MoviesService(new MoviesContext( new DbContextOptions<MoviesContext>())), _configMock.Object);
        }

        [Fact]
        public void TestMissingParameters()
        {

            var badRequest =_controller.GetMovies("", 0, "");

           Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)badRequest.Result).Value.ToString() == "The movie search criteria is missing or invalid.");
        }


        [Fact]
        public void TestGetOneMovie()
        {

            var request = _controller.GetMovies("Star Wars - The Empire Strikes Back", 1980, "SciFi").Value;

            Assert.True(request.Count() == 1);

            var starWarsMovie = JObject.FromObject(request.First()) ;

            Assert.True(starWarsMovie["Title"] == "Star Wars - The Empire Strikes Back");
        }

        //TODO: More tests, more tests!!
    }
}
