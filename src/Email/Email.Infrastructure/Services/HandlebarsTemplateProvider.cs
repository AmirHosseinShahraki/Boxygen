using Email.Application.Services.Interfaces;
using Email.Domain.Enums;
using Email.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using HandlebarsDotNet;

namespace Email.Infrastructure.Services;

public class HandlebarsTemplateProvider : ITemplateProvider
{
    private readonly Dictionary<Templates, HandlebarsTemplate<object, object>> _compiledTemplates = new();

    public HandlebarsTemplateProvider(IOptions<TemplatesConfig> configuration)
    {
        TemplatesConfig? templatesConfig = configuration.Value;
        foreach (Templates template in Enum.GetValues(typeof(Templates)))
        {
            string templateText = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                templatesConfig.Path, template + ".hbs"));
            HandlebarsTemplate<object, object>? compiledTemplate = Handlebars.Compile(templateText);
            _compiledTemplates.Add(template, compiledTemplate);
        }
    }

    public string Render(Templates templates, object data)
    {
        return _compiledTemplates[templates](data);
    }
}