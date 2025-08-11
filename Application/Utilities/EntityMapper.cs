namespace Application.Utilities;

public static class EntityMapper
{
    
    
    /// <summary>
    ///     Creates an Entity using the Dto values
    /// </summary>
    /// <param name="dto"></param>
    /// <typeparam name="T"> the Dto you want to use to Project</typeparam>
    /// <typeparam name="U"> the entity that will be returned</typeparam>
    /// <returns> returns a Entity contains the properties values
    ///           that is both in the dto and the entity</returns>
     public static U ProjectDtoToEntity<T,U>(T dto)
        where T : class
        where U : class 
    {
        Type T_type = typeof(T);
        Type U_type = typeof(U);
        var t_properties = T_type.GetProperties();
        var u_properties = U_type.GetProperties();
        var u_propertyNames = u_properties.Select(p => p.Name).ToArray();

        Dictionary<string, object?> mappingValuesToProperties = new Dictionary<string, object?>();
        
        foreach (var property in t_properties)
        {
            if (u_propertyNames.Contains(property.Name))
            {
                mappingValuesToProperties.Add(property.Name,property.GetValue(dto));
            }
        }

        var obj = U_type.Assembly.
            CreateInstance(U_type.FullName) as U;

        foreach (var property in u_properties)
        {
            if (mappingValuesToProperties.ContainsKey(property.Name))
            {
                property.SetValue(obj,mappingValuesToProperties[property.Name]);
            }
        }
        
        return obj;
    }

    
    
    /// <summary>
    ///     Creates a Dto using the Entity Values 
    /// </summary>
    /// <param name="dto"></param>
    /// <typeparam name="T"> the resulted dto</typeparam>
    /// <typeparam name="U"> the entity used to create the dto</typeparam>
    /// <returns> returns a Dto contains the properties values
    ///           that is both in the dto and the entity</returns>
    public static T ProjectEntityToDto<T, U>(U entity)
    where T : class
    where U : class
    {
        Type T_type = typeof(T);
        Type U_type = typeof(U);
        var t_properties = T_type.GetProperties();
        var u_properties = U_type.GetProperties();
        var t_propertyNames = t_properties.Select(p => p.Name).ToArray();

        Dictionary<string, object?> mappingValues = new Dictionary<string, object?>();

        foreach (var property in u_properties)
        {
            if (t_propertyNames.Contains(property.Name))
            {
                mappingValues.Add(property.Name,property.GetValue(entity));
            }
        }
        
        var dto_obj = T_type.Assembly.CreateInstance(T_type.FullName);

        foreach (var property in t_properties)
        {
            if (mappingValues.ContainsKey(property.Name))
            {
                property.SetValue(dto_obj,mappingValues[property.Name]);
            }
        }


        return dto_obj as T;
    }
    
}