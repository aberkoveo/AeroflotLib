﻿using System.Reflection;
using AutoMapper;

namespace Integra.Application.Common.Mappings;

/// <summary>
/// Тип создает профили для AutoMApper на основе заданных типов.
/// Нужно для автоматического маппинга классов.
/// </summary>
public class AssemblyMappingProfile : Profile
{
    public AssemblyMappingProfile(Assembly assembly)
    {
        ApplyMappingsFromAssembly(assembly);
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(type => type.GetInterfaces()
                .Any(i => i.IsGenericType &&
                          i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Mapping");
            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}