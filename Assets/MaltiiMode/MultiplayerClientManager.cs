using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class MultiplayerClientManager : MonoBehaviour
{
    public async void JoinRelayAsClient(string joinCode)
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        try
        {
            // ✅ Join Relay Allocation using Join Code
            var joinAlloc = await Unity.Services.Relay.RelayService.Instance.JoinAllocationAsync(joinCode);

            // ✅ Set Unity Transport with Relay Data
            var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetRelayServerData(
                joinAlloc.RelayServer.IpV4,
                (ushort)joinAlloc.RelayServer.Port,
                joinAlloc.AllocationIdBytes,
                joinAlloc.Key,
                joinAlloc.ConnectionData,
                joinAlloc.HostConnectionData
            );

            // ✅ Start Client
            NetworkManager.Singleton.StartClient();
            Debug.Log("🔗 Client started and connected using code: " + joinCode);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("❌ Client connection failed: " + ex.Message);
        }
    }
}
