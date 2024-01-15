namespace Email.Application.Services.Interfaces;

public interface ITokenGenerator
{
    public string Generate(int length);
}