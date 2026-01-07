
/*
 
To understand how tests work, I have used both the lectures at school, YouTube, searched for information on the internet, and asked chatGPT for help and explanation.
Unfortunately there was very limited information on the internet (that I could find),
and therefore I have had to use chatGPT as a "teacher" who can break down code snippets and explain them to me. (In addition to the lectures at school).

------ Test 1 ------ (Help by chatGPT)

In my first test, I have received help from chatGPT on how to create a "mock" with NSubstitute and how to build the test to get a correct result.
I asked chatGPT to "break down" the test, step by step in words and explain how to "mock" the repository.
In this test, I test both that the method in the service returns "true" and that the object sent to the repository has the same properties as the parameters sent into the service.  

------ Test 3 ------

In test 5, I had to find information on how to test a method that has a bool as the "output value". I couldn't find anything online and so had to let chatGPT explain this to me.

------ Test 4 ------

How to throw Exception - Learnt byt chatGPT. ( Throws(new Exception("DB error")); )

------ Test 8 ------

I got help by chatGPT to troubleshoot this test

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

    public void CreateCustomer_ShouldReturn_True_IfCustomer_Was_Created()
    {
        // Arrange
        var customerRepository = Substitute.For<ICustomerRepository>();
        var customerService = new CustomerService(customerRepository);

        // Declaring the variables that i will use as parameters i "ACT".
        string firstName = "Firstname";
        string lastName = "Lastname";
        string email = "test@domain.com";
        string phoneNr = "0123456789";
        string streetAddress = "Street";
        string zipCode = "12345";
        string city = "City";

        // Calling on the method "GetAllCustomers()" that returns a list CustomerModel.
        customerRepository.GetAllCustomers().Returns(new List<CustomerModel>());

        // Repository returns "true" if the CustomerModel object has the same properties as the parameters passed into the Service method CreateCustomer().
        customerRepository.CreateCustomer(Arg.Is<CustomerModel>(c =>
        c.FirstName == firstName &&
        c.LastName == lastName &&
        c.Email == email &&
        c.PhoneNr == phoneNr &&
        c.Address.StreetAddress == streetAddress &&
        c.Address.ZipCode == zipCode &&
        c.Address.City == city)).Returns(true);


        //Act
        var result = customerService.CreateCustomer(firstName, lastName, email, phoneNr, streetAddress, zipCode, city);

        // Assert
        Assert.True(result);
    }

    [Fact]

    public void CreateCustomer_ShouldReturn_False_IfCustomer_WasNot_Created()
    {
        // Arrange
        var customerRepository = Substitute.For<ICustomerRepository>();
        var customerService = new CustomerService(customerRepository);

        // Declaring the variables that i will use as parameters i "ACT".
        string firstName = "Firstname";
        string lastName = "Lastname";
        string email = "test@domain.com";
        string phoneNr = "0123456789";
        string streetAddress = "Street";
        string zipCode = "12345";
        string city = "City";

        // Calling on the method "GetAllCustomers()" that returns a list CustomerModel.
        customerRepository.GetAllCustomers().Returns(new List<CustomerModel>());

        // Repository will return "false".
        customerRepository.CreateCustomer(Arg.Any<CustomerModel>()).Returns(false);

        //Act
        var result = customerService.CreateCustomer(firstName, lastName, email, phoneNr, streetAddress, zipCode, city);

        // Assert
        Assert.False(result);
    }

    [Fact]

    // Test 3.
    public void GetAllCustomers_ShouldReturn_ListOfCustomers_If_CustomersExists()
    {

        // Arrange
        var customerRepository = Substitute.For<ICustomerRepository>();
        var customerService = new CustomerService(customerRepository);

        // Creating a list of customers that I use in my mock repository.
        var customerList = new List<CustomerModel>
        {
            new CustomerModel { FirstName = "Firstname1", LastName = "Lastname1", Email = "Email1@Domain.com" },
            new CustomerModel { FirstName = "Firstname2", LastName = "Lastname2", Email = "Email2@Domain.com" }
        };

        // "Mock"-repository "returns" a list of customers to my service.
        customerRepository.GetAllCustomers().Returns(customerList);

        // Act

        // Run the method in service.
        var customers = customerService.GetAllCustomers(out bool hasError);

        // Assert
        Assert.NotEmpty(customers);
        Assert.False(hasError);
        // Compare the customer list with the result "customers".
        Assert.Equal(customerList, customers);
    }

    // Test 4.
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
        Assert.True(hasError); // Check if hasError becomes True if repository throws an Exception.
        Assert.Empty(result); // Check if the return value is empty when repository throws an Exception.
        Assert.NotNull(result); // Make sure that the method does not return a Null value.
        Assert.IsType<CustomerModel[]>(result); // Verify that the service returns an array.
    }

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

    [Fact]

    public void DeleteCustomer_ShouldReturn_False_IfTheCustomerList_IsEmpty_Or_IfNoCustomerWasFoundInTheList()
    {
        // Arrange
        var customerRepository = Substitute.For<ICustomerRepository>();
        var customerService = new CustomerService(customerRepository);
        customerRepository.DeleteCustomer("name").Returns(false);

        // Act
        var result = customerService.DeleteCustomer("name");

        // Assert
        Assert.False(result);
    }

    [Fact]

    public void DeleteCustomer_ShouldReturn_False_IfEmailIsNull()
    {
        // Arrange

        string email = null!;

        var customerRepository = Substitute.For<ICustomerRepository>();
        var customerService = new CustomerService(customerRepository);
        

        // Act
        var result = customerService.DeleteCustomer(email);

        // Assert
        Assert.False(result);
    }

    // Test 8.
    [Fact]

    public void GetCustomerByEmail_ShouldReturn_CustomerModel_WhenCustomerFound()
    {

        // ----------------------------------- I got help by chatGPT to troubleshoot this test --------------------------------------------------------

        // Error = System.NullReferenceException : Object reference not set to an instance of an object?
        // WHY? = Because I had not created a new instance of the "CustomerAddressModel" object.

        // Arrange

        // Here I had not understood that I had to create a new instance of "new CustomerAddressModel". I had written "Address = { StreetAddress = " ", ... };
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
        var customerService = new CustomerService(customerRepository);

        customerRepository.GetCustomerByEmail("test@domain.com").Returns(customerModel);

        // Act

        var expectedCustomerModel = customerService.GetCustomerByEmail("test@domain.com");

        // Assert
        Assert.Equal(expectedCustomerModel, customerModel);
    }

    [Fact]

    public void GetCustomerByEmail_ShouldThrowException_If_CustomerCouldNotBeFound()
    {
        // Arrange

        var customerRepository = Substitute.For<ICustomerRepository>();
        var customerService = new CustomerService(customerRepository);
        string email = "test@domain.com";
        customerRepository.GetCustomerByEmail(email).Throws(new Exception("DB error"));

        // Act & Assert

        Assert.Throws<Exception>(() => customerService.GetCustomerByEmail(email));
    }

    [Fact]

    public void UpdateCustomer_ShouldReturn_False_If_CustomerIsNull()
    {
        // Arrange
        var customerRepository = Substitute.For<ICustomerRepository>();
        var customerService = new CustomerService(customerRepository);
        var customer = new CustomerModel();
        
        customerRepository.UpdateCustomer(customer).Returns(false);

        // Act
        var result = customerService.UpdateCustomer(customer);

        // Assert
        Assert.False(result);
    }

    [Fact]

    public void UpdateCustomer_ShouldReturn_True_If_TheCustomerIsUpdated()
    {
        // Arrange
        var customerRepository = Substitute.For<ICustomerRepository>();
        var customerService = new CustomerService(customerRepository);
        var customer = new CustomerModel()
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

        customerRepository.UpdateCustomer(customer).Returns(true);

        // Act
        var result = customerService.UpdateCustomer(customer);

        // Assert
        Assert.True(result);
    }
}
