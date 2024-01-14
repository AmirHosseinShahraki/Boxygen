using Email.Domain.Enums;

namespace Email.Application.Services;

public interface ITemplateProvider
{
    public string Render(Template template, object data);
}