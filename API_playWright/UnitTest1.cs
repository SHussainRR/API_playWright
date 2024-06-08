using System;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;

namespace API_playWright
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        public IPlaywright playwright;

        public IPage page;

        [Test]
        public async Task API_TEST()

        {
            playwright = await Playwright.CreateAsync();
            {
                var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                { Headless = false });

                page = await browser.NewPageAsync();
                string apiURL = "https://jsonplaceholder.typicode.com/posts/1";

                var response = await page.APIRequest.GetAsync(apiURL);

                if (response.Status == 200)
                {
                    Console.WriteLine("API is working successfull");
                }
                else
                {
                    Console.WriteLine("API is not working successfull");
                }
                string responseBody = await response.TextAsync();

                if (responseBody.Contains("\"body\": \"quia et suscipit\\nsuscipit recusandae consequuntur expedita et cum\\nreprehenderit molestiae ut ut quas totam\\nnostrum rerum est autem sunt rem eveniet architecto\""))
                {
                    Console.WriteLine("API response OK");
                }
                else
                {
                    Console.WriteLine("API response FAILED");
                }

            }

        }


        [Test]
        public async Task API_TEST_UI()

        {
            for (int i = 4; i <= 20; i++)
            {

                playwright = await Playwright.CreateAsync();
                {


                    var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                    { Headless = false });

                    page = await browser.NewPageAsync();
                    //string apiURL = "https://jsonplaceholder.typicode.com/posts/1";



                    var apiURL = "http://localhost:3000/posts";




                    var requestContect = await playwright.APIRequest.NewContextAsync();

                    var toSTring = i.ToString();
                    var jdonbody = new
                    {

                        id = toSTring,
                        title = $"a titile value {toSTring}",
                        view = $"250{toSTring}"

                    };

                    //send post requets with body

                    var response = await requestContect.PostAsync(apiURL, new APIRequestContextOptions
                    {

                        DataObject = jdonbody
                    });


                    //var response = await page.APIRequest.GetAsync(apiURL);

                    if (response.Status == 201)
                    {
                        Console.WriteLine("API is working successfull");
                    }
                    else
                    {
                        Console.WriteLine("API is not working successfull");
                    }
                    string responseBody = await response.TextAsync();

                    //if (responseBody.Contains("\"body\": \"quia et suscipit\\nsuscipit recusandae consequuntur expedita et cum\\nreprehenderit molestiae ut ut quas totam\\nnostrum rerum est autem sunt rem eveniet architecto\""))
                    if (responseBody.Contains(toSTring))
                    {
                        Console.WriteLine("API response OK");
                    }
                    else
                    {
                        Console.WriteLine("API response FAILED");
                    }
                    Console.WriteLine($"API response body is {responseBody} for key {toSTring}");
                    await browser.CloseAsync();

                }

            }
        }

    }
}