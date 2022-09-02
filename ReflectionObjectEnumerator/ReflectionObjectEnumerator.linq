<Query Kind="Program" />

void Main()
{
    var appearance = new Appearance
    {
        Shape = new ShapeDescriptor
        {
            BoundingBox = new BoundingBox
            {
                top = 0f,
                left = 12f,
                right = 125f,
                bottom = 210f
            }
        },
        VehicleInfo = new VehicleInfo
        {
            Type = new MetadataInfo { Value = "tractor" },
            Brand = new MetadataInfo { Value = "DAF" },
            Model = new MetadataInfo { Value = "DX-95" }
        },
        Color = new ColorDescriptor
        {
            ColorCluster = new List<ColorCluster>
            {
                new ColorCluster
                {
                    Color = new Color { X = 3, Y = 128, Z = 128 }
                },
                new ColorCluster
                {
                    Color = new Color { X = 0, Y = 256, Z = 128 }   
                }
            }
        },
        HumanBody = new HumanBody
        {
            Clothing = new Clothing
            {
                Scarf = new Scarf
                {
                    Color = new ColorDescriptor
                    {
                        ColorCluster = new ColorCluster[]
                        {
                            new ColorCluster
                            {
                                Color = new Color { X = 229.5, Y = 204, Z = 25.5 }
                            }
                        }
                    }
                }
            }
        }
    };

    foreach (var searchCriterion in EnumerateSearchCriterionPaths(appearance))
    {
        Console.WriteLine($"{searchCriterion.Item1}: {searchCriterion.Item2}");
    }
}

// Define other methods and classes here
private static IEnumerable<(string, object, Type)> EnumerateSearchCriterionPaths(object @object)
{
    var type = @object.GetType();
    var queue = new Queue<(string ParentPath, PropertyInfo Property, object ContainingObject, Type ContainingObjecType)>();
    queue.Enqueue(("", null, @object, type));
    while (queue.Any())
    {
        var (parentPath, property, containingObject, containingObjectType) = queue.Dequeue();
        if (containingObjectType.IsClass)
        {
            if (property != null && Attribute.IsDefined(property, typeof(SearchCriterionAttribute), false))
            {
                yield return (parentPath, containingObject, containingObjectType);
            }
            else
            {
                foreach (var prop in containingObjectType.GetProperties())
                {
                    // Skip properties defined in .NET assemblies. Also skip not initialized properties.
                    if (containingObjectType.Assembly.FullName != type.Assembly.FullName ||
                        containingObject == null)
                    {
                        continue;
                    }
                    
                    var propertyValue = prop.GetValue(containingObject);
                    var propertyNameReplacement = GetReplacementName(prop);
                    if (propertyNameReplacement != null)
                    {
                        var replacementPropertyValue = ValueConvertor(propertyValue, prop.PropertyType);
                        var replacementProperty = containingObjectType.GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                            .Where(p => p.Name == propertyNameReplacement).FirstOrDefault();
                        replacementProperty?.SetValue(containingObject, replacementPropertyValue);
                    }

                    queue.Enqueue(($"{(parentPath == "" ? "" : parentPath + ".")}{prop.Name}", prop, propertyValue, prop.PropertyType));
                }
            }
        }
        else if (containingObjectType.IsArray)
        {
            Type elementType = containingObjectType.GetElementType();
            foreach (var element in (Array)containingObject)
            {
                queue.Enqueue((parentPath, null, element, elementType));   
            }
        }
        else if (containingObjectType.IsGenericType)
        {
            if (containingObjectType.GetGenericTypeDefinition() != typeof(IEnumerable<>).GetGenericTypeDefinition())
            {
                throw new InvalidOperationException("Only generics typed as IEnumerable<T> are supported.");
            }
            // The actual collection referenced by containingObject must implement IList. Other enumerables like HasSet, SortedSet, Dictionary, etc. are not supported!
            var length = ((IList)containingObject).Count;
            for (int i = 0; i < length; i++)
            {
                queue.Enqueue((parentPath, null, ((IList)containingObject)[i], containingObjectType.GetGenericArguments()[0]));
            }
        }
        else if (Attribute.IsDefined(property, typeof(SearchCriterionAttribute), false))
        {
            // A Struct property marked by SearchCriterionAttribure.        
            yield return (parentPath, containingObject, containingObjectType);
        }
    }
}

private static string GetReplacementName(PropertyInfo property)
{
    var searchCriterionAttribute = property.GetCustomAttributes(typeof(SearchCriterionAttribute)).SingleOrDefault() as SearchCriterionAttribute;
    return searchCriterionAttribute == null ? null : searchCriterionAttribute.ReplacementPropertyName;
}

private static string ValueConvertor(object propertyValue, Type propertyType)
{
    if (propertyType == typeof(Color))
    {
        //var colorApproximator = new ColorApproximator();
        //var realColorFromCamera = new ColorCluster { Color = (Color)propertyValue };
        //return colorApproximator.GetClosestPaletteColorKey(realColorFromCamera);
        return "awd";
    }
    else if (propertyType == typeof(double))
    {
        return string.Format("{0:G17}", propertyValue);
    }
    else if (propertyType == typeof(bool))
    {
        return FirstCharToLowerCase(propertyValue.ToString());
    }
    else
    {
        return propertyValue.ToString();
    }
}

private static string FirstCharToLowerCase(string str)
{
    if (string.IsNullOrEmpty(str) || char.IsLower(str[0]))
    {
        return str;
    }

    return char.ToLower(str[0]) + str.Substring(1);
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class SearchCriterionAttribute : Attribute
{
    public string ReplacementPropertyName { get; private set; }

    public SearchCriterionAttribute(string replacementPropertyName = null)
    {
        this.ReplacementPropertyName = replacementPropertyName;
    }
}

public class Appearance
{
    public ShapeDescriptor Shape { set; get; }
    public VehicleInfo VehicleInfo { set; get; }
    public ColorDescriptor Color { get; set; }
    public HumanBody HumanBody { set; get; }
}
public class ShapeDescriptor
{
    [SearchCriterion]
    public BoundingBox BoundingBox { set; get; }
    public CenterOfGravity CenterOfGravity { set; get; }

}
public class BoundingBox
{
    public float left { set; get; }
    public float top { set; get; }
    public float right { set; get; }
    public float bottom { set; get; }
}
public class CenterOfGravity
{
    public float x { set; get; }
    public float y { set; get; }
}
public class ColorDescriptor
{
    public IEnumerable<ColorCluster> ColorCluster { set; get; }
}
public class ColorCluster
{
    [SearchCriterion(nameof(PaletteColor))]
    public Color Color { set; get; }

    public double Weight { set; get; }
    
    public string PaletteColor { get; set; }
}

public class Color
{
    /// <summary>
    /// R
    /// </summary>
    public double X { set; get; }

    /// <summary>
    /// G
    /// </summary>
    public double Y { set; get; }

    /// <summary>
    /// B
    /// </summary>
    public double Z { set; get; }
}

public class VehicleInfo
{
    public MetadataInfo Type { set; get; }

    public MetadataInfo Brand { set; get; }

    public MetadataInfo Model { set; get; }
}

public class MetadataInfo
{
    public double Likelihood { set; get; }

    [SearchCriterion]
    public string Value { set; get; }
}

public class HumanBody
{
    public Clothing Clothing { set; get; }
}

public class Clothing
{
    public Scarf Scarf { set; get; }
}

public class Scarf
{
    public ColorDescriptor Color { set; get; }

    [SearchCriterion]
    public bool Wear { set; get; }
}