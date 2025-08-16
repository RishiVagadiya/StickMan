using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class AuthWithCredentials : MonoBehaviour
{
    public Action<string> OnStatusMessage;

    public async Task SignUp(string username, string password)
    {
        await UnityServices.InitializeAsync();

        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            OnStatusMessage?.Invoke("✅ SignUp Success!");
        }
        catch (AuthenticationException e)
        {
            OnStatusMessage?.Invoke("❌ SignUp Failed: " + e.Message);
        }
    }

    public async Task Login(string username, string password)
    {
        await UnityServices.InitializeAsync();

        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            OnStatusMessage?.Invoke("✅ Login Success!");
        }
        catch (AuthenticationException e)
        {
            OnStatusMessage?.Invoke("❌ Login Failed: " + e.Message);
        }
    }
}
