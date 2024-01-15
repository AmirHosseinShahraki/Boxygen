using Email.Application.Services;
using Email.Application.Services.Interfaces;
using Email.Domain.Enums;
using Email.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using HandlebarsDotNet;

namespace Email.Infrastructure.Services;

public class HandlebarsTemplateProvider : ITemplateProvider
{
    private readonly Dictionary<Template, HandlebarsTemplate<object, object>> _compiledTemplates = new();

    public HandlebarsTemplateProvider(IOptions<TemplatesConfig> configuration)
    {
        var templatesConfig = configuration.Value;
        foreach (Template template in Enum.GetValues(typeof(Template)))
        {
            string templateText = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                templatesConfig.Path, template + ".hbs"));
            var compiledTemplate = Handlebars.Compile(templateText);
            _compiledTemplates.Add(template, compiledTemplate);
        }
    }

    public string Render(Template template, object data)
    {
        return _compiledTemplates[template](data);
    }
}