using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using MusicSitePrimeBackend.Domain;
using MusicSitePrimeBackend.Services;

namespace MusicSitePrimeBackend.Endpoints.Auth;

public class Authentication : Endpoint<AuthenticationRequest, AuthenticationResponse>
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IAuthenticationService _service;

    public Authentication(UnitOfWork unitOfWork, IAuthenticationService service)
    {
        _unitOfWork = unitOfWork;
        _service = service;
    }

    public override void Configure()
    {
        Post();
        Routes(ApiRoutes.Authentication);
        AllowAnonymous();
    }

    public override async Task HandleAsync(AuthenticationRequest req, CancellationToken ct)
    {
        var user = await _unitOfWork.Users.GetBySecretOrDefaultAsync(req.Secret, ct);
        if (user is null)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        
        var token = _service.BuildTokenForUser(user);
        await SendAsync(new AuthenticationResponse {Token = token}, cancellation: ct);
    }
}

public class AuthenticationRequest
{
    [FromBody] public string Secret { get; init; }
}

public class AuthenticationResponse
{
    public string Token { get; init; }
}