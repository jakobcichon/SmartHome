using Microsoft.AspNetCore.Components;

namespace SmartHome.MobileApp.Components.Pages;

public partial class Home: ComponentBase, IDisposable
{
    private string _wifiStatus = "Checking";

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _wifiStatus = CheckWifiStatusFromProfiles(Connectivity.ConnectionProfiles);

        Connectivity.ConnectivityChanged += OnConnectivityChanged;
    }
    
    private void OnConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
    {
        _wifiStatus = CheckWifiStatusFromProfiles(Connectivity.ConnectionProfiles);
        InvokeAsync(StateHasChanged); 
    }
    
    public void Dispose()
    {
        // Unsubscribe to avoid memory leaks
        Connectivity.ConnectivityChanged -= OnConnectivityChanged;
    }

    private string CheckWifiStatusFromProfiles(IEnumerable<ConnectionProfile> profiles)
    {
        return profiles.Any(e => e == ConnectionProfile.WiFi) ? "ON" : "OFF";
    }
}