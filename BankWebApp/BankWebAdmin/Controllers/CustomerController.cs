using Microsoft.AspNetCore.Mvc;
using BankWebAdmin.Models;
using System.Text;
using Newtonsoft.Json;

namespace BankWebAdmin.Controllers;


public class CustomerController : Controller
{

    private readonly IHttpClientFactory _ClientFactory;
    private HttpClient Client => _ClientFactory.CreateClient("api");

    public CustomerController(IHttpClientFactory clientFactory) => _ClientFactory = clientFactory;

    //Get:Customer/Index
    public async Task<IActionResult> Index()
    {
        var response = await Client.GetAsync("api/Customers");

        if (!response.IsSuccessStatusCode)
            throw new Exception();

        var result = await response.Content.ReadAsStringAsync();
        var customers = JsonConvert.DeserializeObject<List<CustomerDto>>(result);
        var LoginResponse = await Client.GetAsync("api/Logins");
        if (!LoginResponse.IsSuccessStatusCode)
            throw new Exception();
    //Get Logins, compare the CustomerID in Customers with the CustomerID in Logins to find the login of a customer
        var LoginResult = await LoginResponse.Content.ReadAsStringAsync();
        var logins = JsonConvert.DeserializeObject<List<LoginDto>>(LoginResult);

        foreach (var customer in customers)
        {
            foreach (var login in logins)
            {
                if (customer.CustomerID == login.CustomerID)
                {
                    customer.Login = login;
                }
            }
        }
        return View(customers);
    }
    //Get  Customer/BillPays
    public async Task<IActionResult> AllBillPays()
    {
        var response = await Client.GetAsync("api/BillPays");
        if (!response.IsSuccessStatusCode)
            throw new Exception();

        var result = await response.Content.ReadAsStringAsync();
        var billPays = JsonConvert.DeserializeObject<List<BillPayDto>>(result);
        
        return View(billPays);
    }

    //Get Customer/Accounts
    public async Task<IActionResult> Accounts(int? id)
    {
        var response = await Client.GetAsync("api/Accounts");
        if (!response.IsSuccessStatusCode)
            throw new Exception();

        var result = await response.Content.ReadAsStringAsync();
        var accounts = JsonConvert.DeserializeObject<List<AccountDto>>(result);

        return View(accounts);
    }

    //GET:Customers/Edit/1
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var response = await Client.GetAsync($"api/Customers/{id}");
        if (!response.IsSuccessStatusCode)
            throw new Exception();

        var result = await response.Content.ReadAsStringAsync();
        var customer = JsonConvert.DeserializeObject<CustomerDto>(result);
        return View(customer);
    }

    //Post:Customers/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int? id, CustomerDto customer)
    {
        if (id != customer.CustomerID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            var response = Client.PutAsync("api/Customers", content).Result;
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
        }
        return View(customer);
    }


    //Lock Login and change the LockState

    public async Task<IActionResult> Lock(string? id)
    {
        var response = await Client.GetAsync($"api/Logins/{id}");
        if (!response.IsSuccessStatusCode)
            throw new Exception();

        var result = await response.Content.ReadAsStringAsync();
        var login = JsonConvert.DeserializeObject<LoginDto>(result);

        if (ModelState.IsValid)
        {
            string State = "Lock";
            login.LoginState = State;
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var LoginResponse = Client.PutAsync("api/Logins", content).Result;
            return RedirectToAction("Index");

        }
        return RedirectToAction("Index");
    }

    //Unlock Login and change the LockState 
    public async Task<IActionResult> Unlock(string? id)
    {
        var response = await Client.GetAsync($"api/Logins/{id}");
        if (!response.IsSuccessStatusCode)
            throw new Exception();

        var result = await response.Content.ReadAsStringAsync();
        var login = JsonConvert.DeserializeObject<LoginDto>(result);

        if (ModelState.IsValid)
        {

            login.LoginState = null;
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var LoginResponse = Client.PutAsync("api/Logins", content).Result;
            return RedirectToAction("Index");

        }
        return RedirectToAction("Index");
    }

    //connect to json lock the billpay
    public async Task<IActionResult> LockBillPay(int? id)
    {
        var respone = await Client.GetAsync($"api/BillPays/{id}");
        if (!respone.IsSuccessStatusCode)
            throw new Exception();

        var result = await respone.Content.ReadAsStringAsync();
        var billPay = JsonConvert.DeserializeObject<BillPayDto>(result);

        if (ModelState.IsValid)
        {
            billPay.LockState = "Lock";

            var content = new StringContent(JsonConvert.SerializeObject(billPay), Encoding.UTF8, "application/json");
            Console.WriteLine(content);
            var billPayResponse = Client.PutAsync("api/BillPays", content).Result;
            Console.WriteLine(billPayResponse);
            return RedirectToAction("AllBillPays");
        }
        return RedirectToAction("AllBillPays");
    }
    //connect to json unlock the billpay
    public async Task<IActionResult> UnlockBillPay(int? id)
    {
        var respone = await Client.GetAsync($"api/BillPays/{id}");
        if (!respone.IsSuccessStatusCode)
            throw new Exception();

        var result = await respone.Content.ReadAsStringAsync();
        var billPay = JsonConvert.DeserializeObject<BillPayDto>(result);

        if (ModelState.IsValid)
        {
            billPay.LockState = null;

            var content = new StringContent(JsonConvert.SerializeObject(billPay), Encoding.UTF8, "application/json");
            Console.WriteLine(content);
            var billPayResponse = Client.PutAsync("api/BillPays", content).Result;
            Console.WriteLine(billPayResponse);
            return RedirectToAction("AllBillPays");
        }
        return RedirectToAction("AllBillPays");
    }

}

