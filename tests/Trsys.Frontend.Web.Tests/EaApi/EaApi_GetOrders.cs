using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Trsys.Frontend.Web.Tests.EaApi
{
    [TestClass]
    public class EaApi_GetOrders
    {
        private WebApplicationFactory<Startup> _factory;

        [TestInitialize]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [TestCleanup]
        public void Teardown()
        {
            _factory.Dispose();
        }

        [TestMethod]
        public async Task EmptyOrders_ReturnSuccessAndCorrectContent()
        {
            // Arrange
            var client = _factory.CreateClient();
            // Publisher setup
            await client.RegisterSecretKeyAsync("EmptyOrders_ReturnSuccessAndCorrectContent1", "Publisher");
            var publisherToken = await client.GenerateTokenAsync("EmptyOrders_ReturnSuccessAndCorrectContent1", "Publisher");
            // Subscriber setup
            await client.RegisterSecretKeyAsync("EmptyOrders_ReturnSuccessAndCorrectContent2", "Subscriber");
            var token = await client.GenerateTokenAsync("EmptyOrders_ReturnSuccessAndCorrectContent2", "Subscriber");
            // Set order text
            await client.PublishOrderAsync("EmptyOrders_ReturnSuccessAndCorrectContent1", publisherToken, "");

            // Act
            var response = await client.GetAsync("/api/orders", "EmptyOrders_ReturnSuccessAndCorrectContent2", "Subscriber", token: token);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.AreEqual("", await response.Content.ReadAsStringAsync());
        }

        [TestMethod]
        public async Task SingleOrder_ReturnSuccessAndCorrectContent()
        {
            // Arrange
            var client = _factory.CreateClient();
            // Publisher setup
            await client.RegisterSecretKeyAsync("SingleOrder_ReturnSuccessAndCorrectContent1", "Publisher");
            var publisherToken = await client.GenerateTokenAsync("SingleOrder_ReturnSuccessAndCorrectContent1", "Publisher");
            // Subscriber setup
            await client.RegisterSecretKeyAsync("SingleOrder_ReturnSuccessAndCorrectContent2", "Subscriber");
            var token = await client.GenerateTokenAsync("SingleOrder_ReturnSuccessAndCorrectContent2", "Subscriber");
            // Set order text
            await client.PublishOrderAsync("SingleOrder_ReturnSuccessAndCorrectContent1", publisherToken, "1:USDJPY:0:1:2:1617271883");

            await Task.Delay(10);

            // Act
            var response = await client.GetAsync("/api/orders", "SingleOrder_ReturnSuccessAndCorrectContent2", "Subscriber", token: token);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.AreEqual("1:USDJPY:0", await response.Content.ReadAsStringAsync());
        }

        [TestMethod]
        public async Task MultipleOrder_ReturnSuccessAndCorrectContent()
        {
            // Arrange
            var client = _factory.CreateClient();
            // Publisher setup
            await client.RegisterSecretKeyAsync("MultipleOrder_ReturnSuccessAndCorrectContent1", "Publisher");
            var publisherToken = await client.GenerateTokenAsync("MultipleOrder_ReturnSuccessAndCorrectContent1", "Publisher");
            // Subscriber setup
            await client.RegisterSecretKeyAsync("MultipleOrder_ReturnSuccessAndCorrectContent2", "Subscriber");
            var token = await client.GenerateTokenAsync("MultipleOrder_ReturnSuccessAndCorrectContent2", "Subscriber");
            // Set order text
            await client.PublishOrderAsync("MultipleOrder_ReturnSuccessAndCorrectContent1", publisherToken, "1:USDJPY:0:1:2:1617271883@2:EURUSD:1:2:3:1617271884");

            await Task.Delay(10);

            // Act
            var response = await client.GetAsync("/api/orders", "MultipleOrder_ReturnSuccessAndCorrectContent2", "Subscriber", token: token);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.AreEqual("1:USDJPY:0@2:EURUSD:1", await response.Content.ReadAsStringAsync());
        }

        [TestMethod]
        public async Task MultipleOrder2_ReturnSuccessAndCorrectContent()
        {
            // Arrange
            var client = _factory.CreateClient();
            // Publisher setup
            await client.RegisterSecretKeyAsync("MultipleOrder2_ReturnSuccessAndCorrectContent1", "Publisher");
            var publisherToken = await client.GenerateTokenAsync("MultipleOrder2_ReturnSuccessAndCorrectContent1", "Publisher");
            // Subscriber setup
            await client.RegisterSecretKeyAsync("MultipleOrder2_ReturnSuccessAndCorrectContent2", "Subscriber");
            var token = await client.GenerateTokenAsync("MultipleOrder2_ReturnSuccessAndCorrectContent2", "Subscriber");
            // Set order text
            await client.PublishOrderAsync("MultipleOrder2_ReturnSuccessAndCorrectContent1", publisherToken, "1:USDJPY:0:1:2:1617271883");
            await client.PublishOrderAsync("MultipleOrder2_ReturnSuccessAndCorrectContent1", publisherToken, "1:USDJPY:0:1:2:1617271883@2:EURUSD:1:2:3:1617271884");

            await Task.Delay(10);

            // Act
            var response = await client.GetAsync("/api/orders", "MultipleOrder2_ReturnSuccessAndCorrectContent2", "Subscriber", token: token);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.AreEqual("1:USDJPY:0@2:EURUSD:1", await response.Content.ReadAsStringAsync());
        }

        [TestMethod]
        public async Task SingleOrderThenEmptyOrder_ReturnSuccessAndCorrectContent()
        {
            // Arrange
            var client = _factory.CreateClient();
            // Publisher setup
            await client.RegisterSecretKeyAsync("SingleOrderThenEmptyOrder_ReturnSuccessAndCorrectContent1", "Publisher");
            var publisherToken = await client.GenerateTokenAsync("SingleOrderThenEmptyOrder_ReturnSuccessAndCorrectContent1", "Publisher");
            // Subscriber setup
            await client.RegisterSecretKeyAsync("SingleOrderThenEmptyOrder_ReturnSuccessAndCorrectContent2", "Subscriber");
            var token = await client.GenerateTokenAsync("SingleOrderThenEmptyOrder_ReturnSuccessAndCorrectContent2", "Subscriber");
            // Set order text
            await client.PublishOrderAsync("SingleOrderThenEmptyOrder_ReturnSuccessAndCorrectContent1", publisherToken, "1:USDJPY:0:1:2:1617271883");
            await client.PublishOrderAsync("SingleOrderThenEmptyOrder_ReturnSuccessAndCorrectContent1", publisherToken, "");

            await Task.Delay(10);

            // Act
            var response = await client.GetAsync("/api/orders", "SingleOrderThenEmptyOrder_ReturnSuccessAndCorrectContent2", "Subscriber", token: token);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.AreEqual("", await response.Content.ReadAsStringAsync());
        }

        [TestMethod]
        public async Task SingleOrder_MultipleSubscribers_ReturnSuccessAndCorrectContent()
        {
            // Arrange
            var client = _factory.CreateClient();
            // Publisher setup
            await client.RegisterSecretKeyAsync("SingleOrderThenEmptyOrder_ReturnSuccessAndCorrectContent1", "Publisher");
            var publisherToken = await client.GenerateTokenAsync("SingleOrderThenEmptyOrder_ReturnSuccessAndCorrectContent1", "Publisher");
            // Subscriber setup
            var tokens = new List<string>();
            for (var i = 0; i < 100; i++)
            {
                await client.RegisterSecretKeyAsync($"SingleOrder_MultipleSubscribers_ReturnSuccessAndCorrectContent{i + 1}", "Subscriber");
                var token = await client.GenerateTokenAsync($"SingleOrder_MultipleSubscribers_ReturnSuccessAndCorrectContent{i + 1}", "Subscriber");
                tokens.Add(token);
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            // Act
            await client.PublishOrderAsync("SingleOrderThenEmptyOrder_ReturnSuccessAndCorrectContent1", publisherToken, "1:USDJPY:0:1:2:1617271883");
            stopwatch.Stop();

            // Assert
            Assert.IsTrue(stopwatch.Elapsed < TimeSpan.FromMilliseconds(100));
            // no error on success
        }
    }
}
