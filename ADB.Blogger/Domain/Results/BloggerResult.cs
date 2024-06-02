using ADB.Blogger.Application.Strategies;
using ADB.Blogger.Domain.Models;

namespace ADB.Blogger.Domain.Results;

public record BloggerResult<T>(string[] Actions, T Data);
