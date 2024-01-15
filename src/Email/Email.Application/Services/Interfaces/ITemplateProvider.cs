using Email.Domain.Enums;

namespace Email.Application.Services.Interfaces;

public interface ITemplateProvider
{
    public string Render(Template template, object data);
}