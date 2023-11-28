﻿namespace User.Infrastructure.Helpers;

public record RabbitMQConfig
{
    public string Host { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}