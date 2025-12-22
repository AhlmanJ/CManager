
/*
 
To understand how tests work, I have used both the lectures at school, YouTube, searched for information on the internet and asked chatGPT for help.
I asked chatGPT to "break down" the test, step by step in words and explain how to "mock" the repository.


------ Test 1 ------

In this code I have followed the lecture "Simulera beroenden i samband med enhetstester (Unit testing + NSubstitute)" but I did not get it to work like in the video.
I tried to create the parameters "FirstName", "LastName" etc.. by putting them in a variable "request" like in the video.
To solve this I asked chatGPT how to do a test like this. 
I had to create individual parameters instead of putting them in an object.
I also got help with changing from "Add(Arg.Any<Member>())" to instead "calling" any list " Arg.Any<List<CustomerModel>>() " to "mock" the repository.

------ Test 2 ------

No help with troubleshooting.

------ Test 3 ------

I got help troubleshooting chatGPT.

------ Test 4 ------

In test 4, I had to find information on how to test a method that has a bool as the "output value". I couldn't find anything online and so had to let chatGPT explain this to me.

It wasn't until now that I really understood how the whole test actually works. 
How to think when creating a "Mock" repository etc... Unfortunately there was very limited information on the internet (that I could find),
and therefore I have had to use chatGPT as a "teacher" who can break down code snippets and explain them to me. (In addition to the lectures at school).

------ Test 5 ------

How to throw Exception - Learnt byt chatGPT. ( Throws(new Exception("DB error")); )

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

 */

using CManager.Business.Services;
using CManager.Domain.Models;
using CManager.Infrastructure.Repositories;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace CManager.Tests.Business.Services;

public class CustomerService_Tests
{
    // Test 1.

    [Fact]

    public void CreateCustomer_ShouldReturn_True_IfCustomer_Is_Created()
    {
        // Arrange
        var customerRepository = Substitute.For<ICustomerRepository>();

        // Calling on the method "GetAllCustomers()" that returns a list CustomerModel.
        customerRepository.GetAllCustomers().Returns(new List<CustomerModel>());

        // Calling on the method "CreateCustomer" with a list as parameter. 
        customerRepository.CreateCustomer(Arg.Any<List<CustomerModel>>()).Returns(true);

        var customerService = new CustomerService(customerRepository);

        // Declaring the variables that i will use as parameters i "ACT".
        string firstName = "Firstname";
        string lastName = "Lastname";
        string email = "test@domain.com";
        string phoneNr = "0123456789;";
        string streetAddress = "Street";
        string zipCode = "12345";
        string city = "City";

        //Act
        var result = customerService.CreateCustomer(firstName, lastName, email, phoneNr, streetAddress, zipCode, city);

        // Assert
        Assert.True(result);
    }

    // Test 2.

    [Fact]

    public void DeleteCustomer_ShouldReturn_True_IfCustomer_Is_Deleted()
    {
        // Arrange
        var customerRepository = Substitute.For<ICustomerRepository>();
        customerRepository.DeleteCustomer("name").Returns(true);

        var customerService = new CustomerService(customerRepository);

        // Act
        var result = customerService.DeleteCustomer("name");

        // Assert
        Assert.True(result);
    }

    // Test 3.

    [Fact]

    public void GetCustomerByEmail_ShouldReturn_CustomerModel_WhenCustomerFound()
    {

        // ----------------------------------- I got help by chatGPT to troubleshoot this test --------------------------------------------------------

        // Arrange

        // Here I hadn't understood that I had to create a new instance of "new CustomerAddressModel". I had written "Address = { StreetAddress = " ", ... };
        var customerModel = new CustomerModel() 
        {   
            FirstName = "FirstName", 
            LastName = "LastName", 
            Email = "test@domain.com", 
            PhoneNr = "0123456789", 
            Address = new CustomerAddressModel
                { 
                    StreetAddress = "StreetAddress",
                    ZipCode = "12345",
                    City = "City" 
                }
        };


        var customerRepository = Substitute.For<ICustomerRepository>();

        // Here I had written " Returns( new customerModel()); " , instead of customerModel.
        customerRepository.GetCustomerByEmail("FirstName").Returns(customerModel);

        var customerService = new CustomerService(customerRepository);

        // Act
        var result = customerService.GetCustomerByEmail("FirstName");

        // Assert
        Assert.NotNull(result);

        //Checks that the text strings that I pass into the method are equal to those that are returned. (This i got information about from chatGPT)!
        Assert.Equal("FirstName", result.FirstName);
        Assert.Equal("LastName", result.LastName);


        // Error = System.NullReferenceException : Object reference not set to an instance of an object?
        // WHY? = Because I had not created a new instance of the "CustomerAddressModel" object.
    }

    [Fact]

    // Test 4.

    public void GetAllCustomers_ShouldReturn_ListOfCustomers_If_CustomersExists()
    {

        // Arrange
        var customerRepository = Substitute.For<ICustomerRepository>();

        // Creating a list of customers that I use in my mock repository.
        var customerList = new List<CustomerModel>
        {
            new CustomerModel { FirstName = "Firstname1", LastName = "Lastname1", Email = "Email1@Domain.com" },
            new CustomerModel { FirstName = "Firstname2", LastName = "Lastname2", Email = "Email2@Domain.com" }
        };

        // "Mock"-repository "returns" a list of customers to my service.
        customerRepository.GetAllCustomers().Returns(customerList);

        var customerService = new CustomerService(customerRepository);

        // Act

        // Run the method in service.
        var customers = customerService.GetAllCustomers(out bool hasError);

        // Assert
        Assert.NotEmpty(customers);
        Assert.False(hasError);
        // Compare the customer list with the result "customers".
        Assert.Equal(customerList, customers);
    }

    // Test 5.

    [Fact]

    public void GetAllCustomers_Bool_hasError_ShouldBecomeTrue_If_RepositoryFailsToLoadCustomerList()
    {
        // Arrange
        var customerRepository = Substitute.For<ICustomerRepository>();

        // How to throw Exception - Learnt byt chatGPT. ( Throws(new Exception("DB error")); )
        customerRepository.GetAllCustomers().Throws(new Exception("DB error"));

        var customerService = new CustomerService(customerRepository);

        // Act
        var result = customerService.GetAllCustomers(out bool hasError);

        // Assert
        Assert.True(hasError);
    }
}

