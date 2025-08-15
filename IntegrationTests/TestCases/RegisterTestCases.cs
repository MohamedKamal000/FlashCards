using System.Collections;
using Application.Dtos.UserDtos;
using Bogus;

namespace IntegrationTests.TestCases;

public class RegisterTestCases : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var faker = new Faker<RegisterUserDto>()
            .RuleFor(r => r.Email, f => f.Internet.Email())
            .RuleFor(r => r.Name, f => f.Name.FirstName())
            .RuleFor(r => r.Bio, f => f.Lorem.Text())
            .RuleFor(r => r.PicturePath, f => f.Lorem.Lines())
            .RuleFor(r => r.Password, _ => GlobalTestInput.GLOBAL_TEST_PASSWORD);

        var list = new List<object[]>();
        list.Add([faker.Generate()]);
        
        return list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}