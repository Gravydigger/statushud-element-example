using StatusHud;
using Vintagestory.API.Client;
using Vintagestory.API.Common;

namespace StatusHudAddonExample;

public class StatusHudAddonExampleSystem : ModSystem
{
    public override bool ShouldLoad(EnumAppSide side)
    {
        return side == EnumAppSide.Client;
    }

    // Status Hud only works on client side
    public override void StartClientSide(ICoreClientAPI capi)
    {
        capi.Logger.Notification("Starting Status Hud Example Mod");

        // Outside creating the custom element, this is the only thing you need to do to add your element to StatusHud.
        // Just make sure it is called *before* the player joins the world (before or during the IsPlayerReady Event).
        StatusHudSystem.AddElementType(typeof(StatusHudAddonExampleElement));
    }
}