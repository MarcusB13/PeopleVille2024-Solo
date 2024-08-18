using System.Reflection;
namespace PeopleVilleEngine;


public class BaseLoader {
    private List<IAddOn> _addons = new List<IAddOn>();   
    public List<IAddOn> Addons { get { return _addons; } }

    private static BaseLoader? _baseLoader;
    private Village _village;
    private List<Action> OnAddonsLoaded = new List<Action>();

    public BaseLoader(Village village)
    {
        _village = village;
        _baseLoader = this;
    }

    public void SubscribeToOnAddonsLoaded(Action action)
    {
        OnAddonsLoaded.Add(action);
    }

    public static BaseLoader GetInstance()
    {
        return _baseLoader;
    }

    public void LoadAddons()
    {
        AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes());


        //Load from this Assembly
        IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes());
        LoadAddonsFromType(
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()),
            _addons);

        //Load from library Files
        string DirName = AppDomain.CurrentDomain.BaseDirectory;
        System.Console.WriteLine(DirName);
        var libraryFiles = Directory.EnumerateFiles(DirName + "addons").Where(f => Path.GetExtension(f) == ".dll");
        foreach (var libraryFile in libraryFiles)
        {
            LoadAddonsFromType(
                Assembly.LoadFrom(libraryFile).ExportedTypes,
                _addons);
        }
    }

    public void SendSubscriptions()
    {
        foreach (var action in OnAddonsLoaded)
        {
            action();
        }
    }

    private void LoadAddonsFromType(IEnumerable<Type> inputTypes, List<IAddOn> outputAddOns)
    {
        var createInteractionInterface = typeof(IAddOn);
        var createrTypes = inputTypes.Where(p => createInteractionInterface.IsAssignableFrom(p) && !p.IsInterface).ToList();

        foreach (var type in createrTypes)
        {
            Console.WriteLine($"Loading: {type}");
            IAddOn AddOn = (IAddOn)Activator.CreateInstance(type);
            if (AddOn != null) _addons.Add(AddOn);

            AddOn.Execute(_village);
            outputAddOns.Add(AddOn);
        }
}
}