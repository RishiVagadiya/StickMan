using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class MultiplayerHostManager : MonoBehaviour
{
    public string generatedJoinCode;

    public async void CreateHostLobby()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        try
        {
            // ✅ 1. Relay Allocation
            var allocation = await Unity.Services.Relay.RelayService.Instance.CreateAllocationAsync(1);
            generatedJoinCode = await Unity.Services.Relay.RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            Debug.Log("🎯 Relay Join Code: " + generatedJoinCode);

            // ✅ 2. Create Lobby with JoinCode as Metadata
            await LobbyService.Instance.CreateLobbyAsync("MyLobby", 2, new CreateLobbyOptions
            {
                Data = new System.Collections.Generic.Dictionary<string, DataObject>
                {
                    { "joinCode", new DataObject(DataObject.VisibilityOptions.Public, generatedJoinCode) }
                }
            });

            Debug.Log("🏠 Lobby Created");

            // ✅ 3. Set Relay as Network Transport
            var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetRelayServerData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
            );

            // ✅ 4. Start Hosting
            NetworkManager.Singleton.StartHost();
            Debug.Log("✅ Host Started!");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("❌ Host setup failed: " + ex.Message);
        }
    }
}
