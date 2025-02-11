using System;

namespace Api.Interceptors;

[AttributeUsage(AttributeTargets.Method)]
public class RetryAttribute : Attribute
{
    public int MaxAttempts { get; set; }
} 