using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text.Json.Nodes;

namespace TestProject1
{
    public class Tests
    {
        RestResponse ?response;
        JObject ?jsonObject;

        [SetUp]
        public void ExecuteAPI()
        {
            var client = new RestClient("https://api.tmsandbox.co.nz");

            var request = new RestRequest("/v1/Categories/6329/Details.json", Method.Get);

            response = client.Execute(request);

            jsonObject = JObject.Parse(response.Content);
        }

        [Test]
        public void ValidateName()
        {
            if ((string)jsonObject["Name"] == "Home & garden") Assert.Pass("Name is correct");
            else Assert.Fail("Incorrect Value for Name, Expected: Home & garden, Actual: " + (string)jsonObject["Name"]);
        }

        [Test]
        public void ValidateCanRelist()
        {
            if ((bool)jsonObject["CanRelist"]) Assert.Pass("CanRelist is correct");
            else Assert.Fail("Incorrect Value for CanRelist, Expected: true, Actual: " + (string)jsonObject["CanRelist"]);
        }

        [Test]
        public void ValidateFeaturePromotions()
        {
            var promotionsArray = jsonObject["Promotions"];
            var filteredArray = promotionsArray.Where(p => (string)p["Name"] == "Feature");
            foreach (var item in filteredArray)
            {
                if ((string)item["Description"] == "Better position in category") Assert.Pass("Description is correct");
                else Assert.Fail("Incorrect Value for Description, Expected: Better position in category, Actual: " + (string)jsonObject["Description"]);
            }
        }
    }
}