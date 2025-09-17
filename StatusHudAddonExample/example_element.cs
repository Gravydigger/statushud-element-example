using StatusHud;
using Vintagestory.API.Client;
using Vintagestory.API.MathTools;

namespace StatusHudAddonExample;

// Have a look at more examples at https://github.com/Gravydigger/statushud/tree/master/statushud/elements
public class StatusHudAddonExampleElement : StatusHudElement
{
    // Required.
    public const string Name = "example"; // Required. Used internally translating the text.
    private const string textKey = "shud-addon-example"; // Required. Used internally for setting changing the text.

    private readonly StatusHudAddonExampleRenderer renderer; // Required.
    private string numBase = "0base";

    // base(system, true) if you need the element to update faster (only used it if you really need too).
    public StatusHudAddonExampleElement(StatusHudSystem system) : base(system)
    {
        renderer = new StatusHudAddonExampleRenderer(this.system, this);

        this.system.capi.Event.RegisterRenderer(renderer, EnumRenderStage.Ortho);
    }

    public sealed override string[] ElementOptionList => ["0base", "1base"]; // Optional. Remove if you don't want to set options.
    public override string ElementName => Name;
    public override string ElementOption => numBase; // Optional. Remove if you don't want to set options.

    // Required.
    public override StatusHudAddonExampleRenderer GetRenderer()
    {
        return renderer;
    }

    // Required.
    public static string GetTextKey()
    {
        return textKey;
    }

    // Optional. Remove if you don't want to set options.
    public override void ConfigOptions(string value)
    {
        foreach (string words in ElementOptionList)
        {
            if (words == value)
            {
                numBase = value;
            }
        }
    }

    // Required. This is called every 1000ms (or 100ms if you set the 'fast' option).
    // Use this to set the main logic of whatever you are wanting to display.
    public override void Tick()
    {
        int item = system.capi.World.Player.InventoryManager.ActiveHotbarSlotNumber;

        if (numBase == ElementOptionList[1])
        {
            item++;
        }

        renderer.SetText(item.ToString());
    }

    // Required.
    public override void Dispose()
    {
        renderer.Dispose();
        system.capi.Event.UnregisterRenderer(renderer, EnumRenderStage.Ortho);
    }
}

public class StatusHudAddonExampleRenderer : StatusHudRenderer
{
    // Can remove if you are not referencing
    private readonly StatusHudAddonExampleElement element;

    public StatusHudAddonExampleRenderer(StatusHudSystem system, StatusHudAddonExampleElement element) : base(system)
    {
        this.element = element;
        // Remove the Vec4f if you want to use the base font colour.
        text = new StatusHudText(this.system.capi, StatusHudDateElement.GetTextKey(), system.Config, new Vec4f(0.85f, 0.44f, 0.15f, 1));
     
        // You can also use this if you want to set the color dynamically.
        // Note that you will have to call Reload() if you want the change to apply.
        // text.SetFontColour(new Vec4f(0.85f, 0.44f, 0.15f, 1));
    }

    // Required.
    public override void Reload()
    {
        text.ReloadText(pos);
    }

    // Required.
    public void SetText(string value)
    {
        text.Set(value);
    }

    // Required.
    protected override void Update()
    {
        base.Update();
        text.SetPos(pos);
    }

    // Required. This is where you would actually define what texture to used inside TexturesDict.
    protected override void Render()
    {
        system.capi.Render.RenderTexture(system.textures.TexturesDict["slot_select"].TextureId, x, y, w, h);
    }

    // Required.
    public override void Dispose()
    {
        base.Dispose();
        text.Dispose();
    }
}